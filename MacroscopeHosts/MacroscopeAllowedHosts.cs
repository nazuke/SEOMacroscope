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
using System.Diagnostics;

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeAllowedHosts.
	/// </summary>

	public class MacroscopeAllowedHosts : Macroscope
	{

		/**************************************************************************/

		Dictionary<string,Boolean> Hostnames;

		/**************************************************************************/

		public MacroscopeAllowedHosts ()
		{
			Hostnames = new Dictionary<string,Boolean> ( 32 );
		}

		/**************************************************************************/

		public void Add ( string sHostname )
		{
			if( !this.Hostnames.ContainsKey( sHostname ) ) {
				this.Hostnames.Add( sHostname, true );
			} else {
				this.Hostnames[ sHostname ] = true;
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
			if( !this.Hostnames.ContainsKey( sHostname ) ) {
				this.Hostnames.Remove( sHostname );
			}
		}
		
		/**************************************************************************/
		
		public void Allow ( string sHostname )
		{
			if( this.Hostnames.ContainsKey( sHostname ) ) {
				this.Hostnames[ sHostname ] = true;
			} else {
				this.Hostnames.Add( sHostname, true );
			}
		}
		
		/**************************************************************************/
		
		public void Disallow ( string sHostname )
		{
			if( this.Hostnames.ContainsKey( sHostname ) ) {
				this.Hostnames[ sHostname ] = false;
			} else {
				this.Hostnames.Add( sHostname, false );
			}
		}

		/**************************************************************************/
				
		public Boolean IsAllowed ( string sHostname )
		{
			Boolean bIsAllowed = false;
			if( this.Hostnames.ContainsKey( sHostname ) ) {
				bIsAllowed = this.Hostnames[ sHostname ];
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

		[Conditional( "DEVMODE" )]
		public void DumpAllowedHosts ()
		{
			lock( this.Hostnames ) {
				foreach( string sUrl in this.Hostnames.Keys ) {
					DebugMsg( string.Format( "ALLOWED_HOST: {0}", sUrl ) );
				}
			}
		}

		/**************************************************************************/
		
	}

}
