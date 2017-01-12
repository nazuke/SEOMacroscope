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
	/// Description of MacroscopeHyperlinksOut.
	/// </summary>
	
	public class MacroscopeHyperlinksOut : Macroscope
	{
	
		/**************************************************************************/
				
		object Locker = new object ();
		Dictionary<string,List<MacroscopeHyperlinkOut>> Links;

		/**************************************************************************/
						
		public MacroscopeHyperlinksOut ()
		{
			
			Links = new Dictionary<string,List<MacroscopeHyperlinkOut>> ( 256 );

		}
	
		/**************************************************************************/

		public void Clear ()
		{
			this.Links.Clear();
		}
		
		/**************************************************************************/
				
		public MacroscopeHyperlinkOut Add ( string sUrlOrigin, string sUrlTarget )
		{

			//debug_msg( string.Format( "MacroscopeHyperlinkOut sUrlOrigin: {0}", sUrlOrigin ), 1 );
			//debug_msg( string.Format( "MacroscopeHyperlinkOut sUrlTarget: {0}", sUrlTarget ), 1 );	

			MacroscopeHyperlinkOut hlHyperlinkOut = new MacroscopeHyperlinkOut ();
			List<MacroscopeHyperlinkOut> lLinkList;
	
			hlHyperlinkOut.SetUrlOrigin( sUrlOrigin );
			hlHyperlinkOut.SetUrlTarget( sUrlTarget );

			if( this.Links.ContainsKey( sUrlOrigin ) ) {

				lLinkList = ( List<MacroscopeHyperlinkOut> )this.Links[ sUrlOrigin ];
				lLinkList.Add( hlHyperlinkOut );

			} else {

				lLinkList = new List<MacroscopeHyperlinkOut> ( 256 );
				lLinkList.Add( hlHyperlinkOut );

				lock( this.Links ) {
					this.Links.Add( sUrlOrigin, lLinkList );
				}

			}

			return( hlHyperlinkOut );
			
		}

		/**************************************************************************/

		public void Remove ( string sUrlOrigin )
		{
			lock( this.Links ) {
				if( this.Links.ContainsKey( sUrlOrigin ) ) {
					this.Links.Remove( sUrlOrigin );
				}
			}
		}

		/**************************************************************************/
		
		public Boolean ContainsKey ( string sUrlOrigin )
		{
			return( this.Links.ContainsKey( sUrlOrigin ) );
		}

		/**************************************************************************/

		public IEnumerable IterateKeys ()
		{
			lock( this.Links ) {
				foreach( string sUrl in this.Links.Keys ) {
					yield return sUrl;
				}
			}
		}

		/**************************************************************************/

		public List<MacroscopeHyperlinkOut> GetLinksList ( string sUrl )
		{
			List<MacroscopeHyperlinkOut> lHyperlinks = new List<MacroscopeHyperlinkOut> ( this.Links.Count );
			if( this.Links.ContainsKey( sUrl ) ) {
				lock( this.Links ) {
					List<MacroscopeHyperlinkOut> lLinksList = this.Links[ sUrl ];
					for( int i = 0; i < lLinksList.Count; i++ ) {
						lHyperlinks.Add( lLinksList[ i ] );
					}
				}
			}
			return( lHyperlinks );
		}

		/**************************************************************************/
				
		public IEnumerable IterateLinks ( string sUrl )
		{
			if( this.Links.ContainsKey( sUrl ) ) {
				lock( this.Links ) {
					List<MacroscopeHyperlinkOut> lLinksList = this.Links[ sUrl ];
					for( int i = 0; i < lLinksList.Count; i++ ) {
						yield return lLinksList[ i ];
					}
				}
			}
		}

		/**************************************************************************/

		public int Count ()
		{
			int iCount = 0;
			if( this.Links.Count > 0 ) {
				lock( this.Links.Keys ) {
					foreach( string sUrl in this.Links.Keys ) {
						List<MacroscopeHyperlinkOut> lLinkList = ( List<MacroscopeHyperlinkOut> )this.Links[ sUrl ];
						iCount += lLinkList.Count;
					}
				}
			}
			return( iCount );
		}

		/**************************************************************************/
				
	}

}
