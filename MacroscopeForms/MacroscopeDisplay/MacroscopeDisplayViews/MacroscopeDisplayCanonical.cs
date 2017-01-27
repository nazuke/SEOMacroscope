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
using System.Windows.Forms;

namespace SEOMacroscope
{

	public class MacroscopeDisplayCanonical : MacroscopeDisplayListView
	{
		
		/**************************************************************************/

		static Boolean ListViewConfigured = false;
		
		/**************************************************************************/

		public MacroscopeDisplayCanonical ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
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

				//this.lvListView.Sorting = SortOrder.Ascending;

				ListViewConfigured = true;

			}
		}

		/**************************************************************************/

		protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
		{

			if( msDoc.GetIsHtml() ) {
				
				string sCanonical = msDoc.GetCanonical();

				this.lvListView.BeginUpdate();
								
				if( lvListView.Items.ContainsKey( sUrl ) ) {
							
					try {

						ListViewItem lvItem = lvListView.Items[ sUrl ];
						lvItem.SubItems[ 0 ].Text = sUrl;
						lvItem.SubItems[ 1 ].Text = sCanonical;

					} catch( Exception ex ) {
						DebugMsg( string.Format( "MacroscopeDisplayCanonical 1: {0}", ex.Message ) );
					}

				} else {
							
					try {

						ListViewItem lvItem = new ListViewItem ( sUrl );

						lvItem.Name = sUrl;

						lvItem.SubItems[ 0 ].Text = sUrl;
						lvItem.SubItems.Add( sCanonical );

						lvListView.Items.Add( lvItem );

					} catch( Exception ex ) {
						DebugMsg( string.Format( "MacroscopeDisplayCanonical 2: {0}", ex.Message ) );
					}
				
				}
				
				this.lvListView.EndUpdate();
				
			}
			
		}

		/**************************************************************************/

	}

}
