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
using System.Windows.Forms;

namespace SEOMacroscope
{

	public class MacroscopeDisplayHistory : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;

		static Boolean ListViewConfigured = false;
				
		const string constURL = "url";
		const string constVisited = "visited";

		/**************************************************************************/

		public MacroscopeDisplayHistory ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;
		
			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayHistory();
							ConfigureListView( lvListView );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayHistory();
				ConfigureListView( lvListView );
			}
		
		}

		/**************************************************************************/
		
		static void ConfigureListView ( ListView lvListView )
		{
			if( !ListViewConfigured ) {
				lvListView.Sorting = SortOrder.Ascending;	
			}
		}
		
		/**************************************************************************/

		public void ClearData ()
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayHistory();
							lvListView.Items.Clear();
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayHistory();
				lvListView.Items.Clear();
			}
		}

		/**************************************************************************/
				
		public void RefreshData ( Hashtable htHistory )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayHistory();
							lock( lvListView ) {
								this.RenderListView( lvListView, htHistory );
							}
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayHistory();
				lock( lvListView ) {
					this.RenderListView( lvListView, htHistory );
				}
			}
		}
		
		/**************************************************************************/
							
		void RenderListView ( ListView lvListView, Hashtable htHistory )
		{

			foreach( string sURL in htHistory.Keys ) {
			
				string sVisited = htHistory[ sURL ].ToString();
			
				if( lvListView.Items.ContainsKey( sURL ) ) {
			
					try {
						ListViewItem lvItem = lvListView.Items[ sURL ];
						lvItem.SubItems[ 1 ].Text = sVisited;
					} catch( Exception ex ) {
						DebugMsg( string.Format( "RenderListView 1: {0}", ex.Message ) );
					}
			
				} else {
			
					try {
						ListViewItem lvItem = new ListViewItem ( sURL );
						lvItem.Name = sURL;
						lvItem.SubItems.Add( sVisited );
						lvListView.Items.Add( lvItem );
					} catch( Exception ex ) {
						DebugMsg( string.Format( "RenderListView 2: {0}", ex.Message ) );
					}
			
				}
			
			}
		
		}

		/**************************************************************************/

	}

}
