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
