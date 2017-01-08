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
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDisplayHistory : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;

		const string constURL = "url";
		const string constVisited = "visited";

		/**************************************************************************/

		public MacroscopeDisplayHistory ( MacroscopeMainForm msMainFormNew )
		{
			msMainForm = msMainFormNew;
		}

		/**************************************************************************/
				
		public void RefreshData ( Hashtable htHistory )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							ListView lvHistory = this.msMainForm.GetDisplayHistory();
							lock( lvHistory ) {
								this.RenderListView( lvHistory, htHistory );
							}
						}
					)
				);
			} else {
				ListView lvHistory = this.msMainForm.GetDisplayHistory();
				lock( lvHistory ) {
					this.RenderListView( lvHistory, htHistory );
				}
			}
		}
		
		/**************************************************************************/
							
		void RenderListView ( ListView lvListView, Hashtable htHistory )
		{
			lvListView.SuspendLayout();
			lvListView.Sorting = SortOrder.Ascending;
			foreach( string sURL in htHistory.Keys ) {
				string sVisited = htHistory[ sURL ].ToString();
				if( lvListView.Items.ContainsKey( sURL ) ) {
					try {
						ListViewItem lvItem = lvListView.Items[ sURL ];
						lvItem.SubItems[ 1 ].Text = sVisited;
					} catch( Exception ex ) {
						debug_msg( string.Format( "RenderListView 1: {0}", ex.Message ) );
					}
				} else {
					try {
						ListViewItem lvItem = new ListViewItem ( sURL );
						lvItem.Name = sURL;
						lvItem.SubItems.Add( sVisited );
						lvListView.Items.Add( lvItem );
					} catch( Exception ex ) {
						debug_msg( string.Format( "RenderListView 2: {0}", ex.Message ) );
					}
				}
			}
			lvListView.ResumeLayout();
		}

		/**************************************************************************/

	}

}
