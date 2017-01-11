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

	public class MacroscopeDocumentCollection : Macroscope
	{

		/**************************************************************************/

		Hashtable DocCollection;

		/**************************************************************************/

		public MacroscopeDocumentCollection ()
		{
			DocCollection = Hashtable.Synchronized( new Hashtable ( 4096 ) );
		}

		/**************************************************************************/

		public Boolean Contains ( string sKey )
		{
			Boolean sResult = false;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				sResult = true;
			}
			return( sResult );
		}

		/**************************************************************************/
		
		public int Count ()
		{
			return( this.DocCollection.Count );
		}
				
		/**************************************************************************/
				
		public void Add ( string sKey, MacroscopeDocument msDoc )
		{
			if( this.DocCollection.ContainsKey( sKey ) ) {
				this.Remove( sKey );
			}
			lock( this.DocCollection ) {
				this.DocCollection.Add( sKey, msDoc );
			}
		}

		/**************************************************************************/

		public Boolean Exists ( string sKey )
		{
			Boolean bExists = false;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				bExists = true;
			}
			return( bExists );
		}

		/**************************************************************************/

		public MacroscopeDocument Get ( string sKey )
		{
			MacroscopeDocument msDoc = null;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				msDoc = ( MacroscopeDocument )this.DocCollection[ sKey ];
			}
			return( msDoc );
		}

		/**************************************************************************/

		public void Remove ( string sKey )
		{
			if( this.DocCollection.ContainsKey( sKey ) ) {
				lock( this.DocCollection ) {
					this.DocCollection.Remove( sKey );
				}
			}
		}

		/**************************************************************************/
						
		public List<string> Keys ()
		{
			List<string> lKeys = new List<string> ();
			lock( this.DocCollection ) {
				foreach( string sKey in this.DocCollection.Keys ) {
					lKeys.Add( sKey );
				}
			}
			return( lKeys );
		}

		/**************************************************************************/

		public void RecalculateLinksIn ()
		{

			lock( this.DocCollection ) {

				foreach( string sUrlTarget in this.DocCollection.Keys ) {

					MacroscopeDocument msDoc = this.Get( sUrlTarget );

					msDoc.ClearHyperlinksIn();

					//debug_msg( string.Format( "RecalculateLinksIn sUrlTarget: {0}", sUrlTarget ), 0 );

					foreach( string sUrlOrigin in this.DocCollection.Keys ) {

						//debug_msg( string.Format( "RecalculateLinksIn sUrlOrigin: {0}", sUrlOrigin ), 1 );

						List<MacroscopeHyperlinkOut> lHyperlinksOut = msDoc.GetHyperlinksOut().GetLinks( sUrlTarget );

						for( int i = 0; i < lHyperlinksOut.Count; i++ ) {

							MacroscopeHyperlinkOut HyperlinkOut = ( MacroscopeHyperlinkOut )lHyperlinksOut[ i ];

							if( sUrlTarget == HyperlinkOut.GetUrlTarget() ) {

								//debug_msg( string.Format( "RecalculateLinksIn lHyperlinksOut: {0} :: {1}", i, sUrlTarget ), 2 );

								msDoc.AddHyperlinkIn( "", "", MacroscopeHyperlinkIn.LINKTEXT, sUrlOrigin, sUrlTarget, "", "" );

							}

						}

					}

				}
				
			}

		}

		/**************************************************************************/

	}

}
