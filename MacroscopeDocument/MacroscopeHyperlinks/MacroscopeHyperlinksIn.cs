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
using System.Collections;
using System.Collections.Generic;

namespace SEOMacroscope
{
	
	/// <summary>
	/// Description of MacroscopeHyperlinksIn.
	/// </summary>
	
	public class MacroscopeHyperlinksIn : Macroscope
	{
	
		/**************************************************************************/
		
		object Locker = new object ();
		Dictionary<string,List<MacroscopeHyperlinkIn>> Links;
		int LinkId;

		/**************************************************************************/
				
		public MacroscopeHyperlinksIn ()
		{
			Links = new Dictionary<string,List<MacroscopeHyperlinkIn>> ( 256 );
			LinkId = 1;
		}

		/**************************************************************************/

		public void Add (
			string sType,
			string sMethod,
			int iLinkClass,
			string sUrlOrigin,
			string sUrlTarget,
			string sLinkText,
			string sAltText
		)
		{

			MacroscopeHyperlinkIn hlHyperlinkIn = new MacroscopeHyperlinkIn ( LinkId, sType, sMethod, iLinkClass, sUrlOrigin, sUrlTarget, sLinkText, sAltText );

			List<MacroscopeHyperlinkIn> lLinkList;

			
			
			debug_msg( string.Format( "MacroscopeHyperlinksIn Add sUrlOrigin: {0}", sUrlOrigin ) );
			debug_msg( string.Format( "MacroscopeHyperlinksIn Add sUrlTarget: {0}", sUrlTarget ) );
			
			
			
			
			
			
			
			lock( this.Locker ) {

				if( this.Links.ContainsKey( sUrlOrigin ) ) {

					lLinkList = ( List<MacroscopeHyperlinkIn> )this.Links[ sUrlOrigin ];
					lLinkList.Add( hlHyperlinkIn );
				} else {

					lLinkList = new List<MacroscopeHyperlinkIn> ( 256 );
					lLinkList.Add( hlHyperlinkIn );
					this.Links.Add( sUrlOrigin, lLinkList );

				}

				LinkId++;
			}
			
		}

		/**************************************************************************/

		public void Clear ()
		{
			lock( this.Locker ) {
				this.Links.Clear();
				LinkId = 1;
			}
		}

		/**************************************************************************/

		public Dictionary<string,List<MacroscopeHyperlinkIn>>.KeyCollection Keys ()
		{
			return( this.Links.Keys );
		}

		/**************************************************************************/

		public Boolean ContainsKey ( string sUrlOrigin )
		{
			return( this.Links.ContainsKey( sUrlOrigin ) );
		}

		/**************************************************************************/
		
		public List<MacroscopeHyperlinkIn> GetLinks ( string sUrlOrigin )
		{

			List<MacroscopeHyperlinkIn> lLinkList = new List<MacroscopeHyperlinkIn> ( this.Links.Count );

			if( this.Links.ContainsKey( sUrlOrigin ) ) {

				lock( this.Locker ) {
				
					lock( this.Links ) {

						List<MacroscopeHyperlinkIn> lLinksList = this.Links[ sUrlOrigin ];

						for( int i = 0; i < lLinksList.Count; i++ ) {
							debug_msg( string.Format( "MacroscopeHyperlinksIn : GetLinks: {0}", i ) );
							lLinkList.Add( lLinksList[ i ] );
						}
					
					}
				
				}

			}

			return( lLinkList );

		}

		/**************************************************************************/
		
		public int Count ()
		{
			return( this.Links.Count );
		}

		/**************************************************************************/



		/**************************************************************************/

	}

}
