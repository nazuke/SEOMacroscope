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
	
	/// <summary>
	/// Description of MacroscopeDisplayHeadings.
	/// </summary>
	
	public class MacroscopeDisplayHeadings : MacroscopeDisplayListView
	{
		
		/**************************************************************************/

		static Boolean ListViewConfigured = false;
		
		/**************************************************************************/

		public MacroscopeDisplayHeadings ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
			: base( MainFormNew, lvListViewNew )
		{

			MainForm = MainFormNew;
			lvListView = lvListViewNew;
						
			if( MainForm.InvokeRequired ) {
				MainForm.Invoke(
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

				ListViewConfigured = true;

			}
		}

		/**************************************************************************/

		protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
		{

			for( ushort i = 1; i <= MacroscopePreferencesManager.GetMaxHeadingDepth(); i++ ) {

				List<string> lHeadings = msDoc.GetHeadings( i );

				for( int iCount = 0; iCount < lHeadings.Count; iCount++ ) {

					string sPairKey = string.Join( "::", sUrl, i, iCount );
					int iHeadingIndex = i + 1;
					
					if( this.lvListView.Items.ContainsKey( sPairKey ) ) {
							
						try {

							ListViewItem lvItem = this.lvListView.Items[ sPairKey ];
							lvItem.SubItems[ 0 ].Text = sUrl;
							lvItem.SubItems[ 1 ].Text = ( iCount + 1 ).ToString();
							lvItem.SubItems[ iHeadingIndex ].Text = lHeadings[ iCount ];

						} catch( Exception ex ) {
							DebugMsg( string.Format( "MacroscopeDisplayHeadings 1: {0}", ex.Message ) );
						}

					} else {
							
						try {

							ListViewItem lvItem = new ListViewItem ( sPairKey );

							lvItem.Name = sPairKey;

							lvItem.SubItems[ 0 ].Text = sUrl;
							lvItem.SubItems.Add( ( iCount + 1 ).ToString() );

							for( ushort k = 1; k <= 6; k++ ) {
								lvItem.SubItems.Add( "" );
							}

							lvItem.SubItems[ iHeadingIndex ].Text = lHeadings[ iCount ];
							
							this.lvListView.Items.Add( lvItem );

						} catch( Exception ex ) {
							DebugMsg( string.Format( "MacroscopeDisplayHeadings 2: {0}", ex.Message ) );
						}

					}
				
				}

			}

		}

		/**************************************************************************/

	}
}
