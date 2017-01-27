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

	public class MacroscopeDisplayHistory : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm MainForm;
		ListView lvListView;
		
		static Boolean ListViewConfigured = false;

		/**************************************************************************/

		public MacroscopeDisplayHistory ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
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

		public void ClearData ()
		{
			if( this.MainForm.InvokeRequired ) {
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.lvListView.Items.Clear();
						}
					)
				);
			} else {
				this.lvListView.Items.Clear();
			}
		}

		/**************************************************************************/
				
		public void RefreshData ( Dictionary<string,Boolean> htHistory )
		{
			if( this.MainForm.InvokeRequired ) {
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							lock( this.lvListView ) {
								this.RenderListView( htHistory );
							}
						}
					)
				);
			} else {
				lock( this.lvListView ) {
					this.RenderListView( htHistory );
				}
			}
		}
		
		/**************************************************************************/
							
		void RenderListView ( Dictionary<string,Boolean> htHistory )
		{

			foreach( string sUrl in htHistory.Keys ) {
			
				string sVisited = htHistory[ sUrl ].ToString();
			
				this.lvListView.BeginUpdate();
								
				if( this.lvListView.Items.ContainsKey( sUrl ) ) {
			
					try {
						ListViewItem lvItem = this.lvListView.Items[ sUrl ];
						lvItem.SubItems[ 1 ].Text = sVisited;
					} catch( Exception ex ) {
						DebugMsg( string.Format( "RenderListView 1: {0}", ex.Message ) );
					}
			
				} else {
			
					try {
						ListViewItem lvItem = new ListViewItem ( sUrl );
						lvItem.Name = sUrl;
						lvItem.SubItems.Add( sVisited );
						this.lvListView.Items.Add( lvItem );
					} catch( Exception ex ) {
						DebugMsg( string.Format( "RenderListView 2: {0}", ex.Message ) );
					}
			
				}
			
				this.lvListView.EndUpdate();
				
			}
		
		}

		/**************************************************************************/

	}

}
