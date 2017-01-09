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

	public class MacroscopeDisplayCanonical : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;
		
		static Boolean ListViewConfigured = false;
		
		/**************************************************************************/

		public MacroscopeDisplayCanonical ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;
			
			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							ListView lvListView = this.msMainForm.GetDisplayCanonicalAnalysis();
							ConfigureListView( lvListView );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayCanonicalAnalysis();
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
						delegate {
							ListView lvListView = this.msMainForm.GetDisplayCanonicalAnalysis();
							lvListView.Items.Clear();
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayCanonicalAnalysis();
				lvListView.Items.Clear();
			}
		}

		/**************************************************************************/
				
		public void RefreshData ( MacroscopeDocumentCollection htDocCollection )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							ListView lvListView = this.msMainForm.GetDisplayCanonicalAnalysis();
							this.RenderListView( lvListView, htDocCollection );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayCanonicalAnalysis();
				this.RenderListView( lvListView, htDocCollection );
			}
		}

		/**************************************************************************/
				
		public void RefreshDataSingle ( MacroscopeDocument msDoc, string sURL )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							ListView lvListView = this.msMainForm.GetDisplayCanonicalAnalysis();
							this.RenderListViewSingle( lvListView, msDoc, sURL );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayCanonicalAnalysis();
				this.RenderListViewSingle( lvListView, msDoc, sURL );
			}
		}

		/**************************************************************************/

		void RenderListView ( ListView lvListView, MacroscopeDocumentCollection htDocCollection )
		{

			foreach( string sKeyURL in htDocCollection.Keys() ) {
				MacroscopeDocument msDoc = htDocCollection.Get( sKeyURL );
				this.RenderListViewSingle( lvListView, msDoc, sKeyURL );
			}

		}
		
		/**************************************************************************/

		void RenderListViewSingle ( ListView lvListView, MacroscopeDocument msDoc, string sKeyURL )
		{

			lvListView.SuspendLayout();

			if( msDoc.GetIsHtml() ) {
				
				string sCanonical = msDoc.GetCanonical();

				if( lvListView.Items.ContainsKey( sKeyURL ) ) {
							
					try {

						ListViewItem lvItem = lvListView.Items[sKeyURL];
						lvItem.SubItems[0].Text = sKeyURL;
						lvItem.SubItems[1].Text = sCanonical;

					} catch( Exception ex ) {
						debug_msg( string.Format( "MacroscopeDisplayCanonical 1: {0}", ex.Message ) );
					}

				} else {
							
					try {

						ListViewItem lvItem = new ListViewItem ( sKeyURL );

						lvItem.Name = sKeyURL;

						lvItem.SubItems[0].Text = sKeyURL;
						lvItem.SubItems.Add( sCanonical );

						lvListView.Items.Add( lvItem );

					} catch( Exception ex ) {
						debug_msg( string.Format( "MacroscopeDisplayCanonical 2: {0}", ex.Message ) );
					}
				
				}
	
			}

			lvListView.ResumeLayout();	
					
		}

		/**************************************************************************/

	}

}
