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
using System.Windows.Forms;

namespace SEOMacroscope
{

	public class MacroscopeDisplayTelephoneNumbers : MacroscopeDisplayListView
	{
		
		/**************************************************************************/

		static Boolean ListViewConfigured = false;
				
		/**************************************************************************/

		public MacroscopeDisplayTelephoneNumbers ( MacroscopeMainForm msMainFormNew, ListView lvListViewNew )
			: base( msMainFormNew, lvListViewNew )
		{

			msMainForm = msMainFormNew;
			lvListView = lvListViewNew;

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ConfigureListView();
						}
					)
				);
			} else {
				ConfigureListView();
			}

		}

		/**************************************************************************/
		
		void ConfigureListView ()
		{
			if( !ListViewConfigured ) {
				this.lvListView.Sorting = SortOrder.Ascending;	
			}
		}

		/**************************************************************************/

		protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
		{

			if( msDoc.GetIsHtml() ) {
				
				Dictionary<string,string> htTelephoneNumbers = msDoc.GetTelephoneNumbers();

				foreach( string sTelephoneNumber in htTelephoneNumbers.Keys ) {

					string sPairKey = string.Join( "", sTelephoneNumber, sUrl );

					if( this.lvListView.Items.ContainsKey( sPairKey ) ) {
							
						try {

							ListViewItem lvItem = this.lvListView.Items[ sPairKey ];
							lvItem.SubItems[ 0 ].Text = sTelephoneNumber;
							lvItem.SubItems[ 1 ].Text = sUrl;

						} catch( Exception ex ) {
							DebugMsg( string.Format( "MacroscopeDisplayTelephoneNumbers 1: {0}", ex.Message ) );
						}

					} else {
							
						try {

							ListViewItem lvItem = new ListViewItem ( sPairKey );

							lvItem.Name = sPairKey;

							lvItem.SubItems[ 0 ].Text = sTelephoneNumber;
							lvItem.SubItems.Add( sUrl );

							this.lvListView.Items.Add( lvItem );

						} catch( Exception ex ) {
							DebugMsg( string.Format( "MacroscopeDisplayTelephoneNumbers 2: {0}", ex.Message ) );
						}

					}
						
				}
					
			}

		}
		
		/**************************************************************************/
		
	}

}
