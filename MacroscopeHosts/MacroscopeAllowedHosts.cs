/*
	
	This file is part of SEOMacroscope.
	
	Copyright 2017 Jason Holland.
	
	The GitHub repository may be found at:
	
		https://github.com/nazuke/SEOMacroscope
	
	Foobar is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.
	
	Foobar is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.
	
	You should have received a copy of the GNU General Public License
	along with Foobar.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeAllowedHosts.
	/// </summary>

	public class MacroscopeAllowedHosts : Macroscope
	{

		/**************************************************************************/

		Dictionary<string,Boolean> Hosts;

		/**************************************************************************/

		public MacroscopeAllowedHosts ()
		{
			Hosts = new Dictionary<string,Boolean> ( 32 );
		}

		/**************************************************************************/

		public void Add ( string sHostname )
		{
			if( !Hosts.ContainsKey( sHostname ) ) {
				Hosts.Add( sHostname, true );
			} else {
				Hosts[ sHostname ] = true;
			}
		}
		
		/**************************************************************************/
		
		public void AddFromUrl ( string sUrl )
		{
			Uri uFromUrl = new Uri ( sUrl, UriKind.Absolute );
			this.Add( uFromUrl.Host );
		}

		/**************************************************************************/
				
		public void Remove ( string sHostname )
		{
			if( !Hosts.ContainsKey( sHostname ) ) {
				Hosts.Remove( sHostname );
			}
		}
		
		/**************************************************************************/
		
		public void Allow ( string sHostname )
		{
			if( Hosts.ContainsKey( sHostname ) ) {
				Hosts[ sHostname ] = true;
			} else {
				Hosts.Add( sHostname, true );
			}
		}
		
		/**************************************************************************/
		
		public void Disallow ( string sHostname )
		{
			if( Hosts.ContainsKey( sHostname ) ) {
				Hosts[ sHostname ] = false;
			} else {
				Hosts.Add( sHostname, false );
			}
		}

		/**************************************************************************/
				
		public Boolean IsAllowed ( string sHostname )
		{
			Boolean bIsAllowed = false;
			if( Hosts.ContainsKey( sHostname ) ) {
				bIsAllowed = Hosts[ sHostname ];
			}
			return( bIsAllowed );
		}

		/**************************************************************************/
		
		public Boolean IsAllowedFromUrl ( string sUrl )
		{
			Uri uFromUrl = new Uri ( sUrl, UriKind.Absolute );
			return( this.IsAllowed( uFromUrl.Host ) );
		}

		/**************************************************************************/

	}

}
