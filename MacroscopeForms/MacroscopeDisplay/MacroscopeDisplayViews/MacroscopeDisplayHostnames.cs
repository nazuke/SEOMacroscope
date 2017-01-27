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

	public class MacroscopeDisplayHostnames : MacroscopeDisplayListView
	{
		
		/**************************************************************************/

		static Boolean ListViewConfigured = false;
		
		/**************************************************************************/

		public MacroscopeDisplayHostnames ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
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

		public new void RefreshData ( MacroscopeDocumentCollection DocCollection )
		{
			if( this.MainForm.InvokeRequired ) {
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.RenderListView( DocCollection );
						}
					)
				);
			} else {
				this.RenderListView( DocCollection );
			}
		}

		/**************************************************************************/

		public new void RenderListView ( MacroscopeDocumentCollection DocCollection )
		{
			this.DebugMsg( string.Format( "RenderListView: {0}", "OVERRIDE" ) );

			Dictionary<string,int> sHostnames = DocCollection.GetHostnamesWithCount();

			this.DebugMsg( string.Format( "MacroscopeDisplayHostnames 1: {0}", sHostnames.Count ) );

			foreach( string sHostname in sHostnames.Keys ) {
				int iCount = sHostnames[ sHostname ];
				this.RenderListView( sHostname, iCount );
			}
		}

		/**************************************************************************/

		public void RenderListView ( string sHostname, int iCount )
		{
			string sPairKey = string.Join( "::", "HOST", sHostname );

			this.lvListView.BeginUpdate();

			Boolean bIsInternal = MainForm.GetJobMaster().GetAllowedHosts().IsAllowed( sHostname );

			if( this.lvListView.Items.ContainsKey( sPairKey ) ) {
							
				try {

					ListViewItem lvItem = this.lvListView.Items[ sPairKey ];
					lvItem.SubItems[ 0 ].Text = sHostname;
					lvItem.SubItems[ 1 ].Text = iCount.ToString();
					lvItem.SubItems[ 2 ].Text = bIsInternal.ToString();

				} catch( Exception ex ) {
					this.DebugMsg( string.Format( "MacroscopeDisplayHostnames 1: {0}", ex.Message ) );
				}

			} else {
							
				try {

					ListViewItem lvItem = new ListViewItem ( sPairKey );

					lvItem.Name = sPairKey;

					lvItem.SubItems[ 0 ].Text = sHostname;
					lvItem.SubItems.Add( iCount.ToString() );
					lvItem.SubItems.Add( bIsInternal.ToString() );

					this.lvListView.Items.Add( lvItem );

				} catch( Exception ex ) {
					this.DebugMsg( string.Format( "MacroscopeDisplayHostnames 2: {0}", ex.Message ) );
				}

			}

			this.lvListView.EndUpdate();
			
		}

		/**************************************************************************/

		protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
		{
		}
		
		/**************************************************************************/
	
	}

}
