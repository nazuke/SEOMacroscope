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
using System.Windows.Forms;

namespace SEOMacroscope
{

	public class MacroscopeDisplayQueue : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;

		static Boolean ListViewConfigured = false;
				
		/**************************************************************************/

		public MacroscopeDisplayQueue ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;
		
			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayQueue();
							ConfigureListView( lvListView );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayQueue();
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

		public void RefreshData ( List<string> lQueue )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayQueue();
							lock( lvListView ) {
								lvListView.Items.Clear();
								this.RenderListView( lvListView, lQueue );
							}
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayQueue();
				lock( lvListView ) {
					lvListView.Items.Clear();
					this.RenderListView( lvListView, lQueue );
				}
			}
		}
		
		/**************************************************************************/
							
		void RenderListView ( ListView lvListView, List<string> lQueue )
		{

			int iCount = 1;
			int iPad = lQueue.Count.ToString().Length;

			foreach( string sURL in lQueue ) {

				string sPairKey = string.Join( "::", iCount.ToString(), sURL );

				if( lvListView.Items.ContainsKey( sPairKey ) ) {
			
					try {
						ListViewItem lvItem = lvListView.Items[ sURL ];
						lvItem.SubItems[ 0 ].Text = iCount.ToString( string.Format( "D{0}", iPad ) );
						lvItem.SubItems[ 1 ].Text = sURL;
					} catch( Exception ex ) {
						DebugMsg( string.Format( "MacroscopeDisplayQueue 1: {0}", ex.Message ) );
					}
			
				} else {
			
					try {
						ListViewItem lvItem = new ListViewItem ( sURL );
						lvItem.Name = sPairKey;
						lvItem.SubItems[ 0 ].Text = iCount.ToString( string.Format( "D{0}", iPad ) );
						lvItem.SubItems.Add( sURL );
						lvListView.Items.Add( lvItem );
					} catch( Exception ex ) {
						DebugMsg( string.Format( "MacroscopeDisplayQueue 2: {0}", ex.Message ) );
					}
			
				}
				
				iCount++;
				
			}
		
		}

		/**************************************************************************/

	}

}
