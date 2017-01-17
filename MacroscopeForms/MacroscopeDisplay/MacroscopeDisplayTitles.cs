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

	public class MacroscopeDisplayTitles : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;
		
		static Boolean ListViewConfigured = false;
		
		/**************************************************************************/

		public MacroscopeDisplayTitles ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;
			
			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayTitles();
							ConfigureListView( lvListView );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayTitles();
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
							ListView lvListView = this.msMainForm.GetDisplayTitles();
							lvListView.Items.Clear();
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayTitles();
				lvListView.Items.Clear();
			}
		}

		/**************************************************************************/
				
		public void RefreshData ( MacroscopeDocumentCollection htDocCollection )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayTitles();
							this.RenderListView( lvListView, htDocCollection );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayTitles();
				this.RenderListView( lvListView, htDocCollection );
			}
		}

		/**************************************************************************/
				
		public void RefreshDataSingle ( MacroscopeDocument msDoc, string sURL )
		{

			MacroscopeDocumentCollection htDocCollection = this.msMainForm.GetJobMaster().DocCollectionGet();
			
			string sTitle = msDoc.GetTitle();
			int iCount = htDocCollection.GetTitleCount( sTitle );

			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayTitles();
							this.RenderListViewSingle( lvListView, msDoc, sURL, sTitle, iCount );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayTitles();
				this.RenderListViewSingle( lvListView, msDoc, sURL, sTitle, iCount );
			}
			
		}

		/**************************************************************************/

		void RenderListView ( ListView lvListView, MacroscopeDocumentCollection htDocCollection )
		{

			foreach( string sKeyURL in htDocCollection.Keys() ) {

				MacroscopeDocument msDoc = htDocCollection.Get( sKeyURL );

				string sTitle = msDoc.GetTitle();
				int iCount = htDocCollection.GetTitleCount( sTitle );

				this.RenderListViewSingle( lvListView, msDoc, sKeyURL, sTitle, iCount );

			}

		}
		
		/**************************************************************************/

		void RenderListViewSingle ( ListView lvListView, MacroscopeDocument msDoc, string sKeyURL, string sTitle, int iCount )
		{

			if( msDoc.GetIsHtml() ) {

				string sPairKey = string.Join( "", sKeyURL, sTitle );
				string sTitleLength = sTitle.Length.ToString();

				if( lvListView.Items.ContainsKey( sPairKey ) ) {
							
					try {

						ListViewItem lvItem = lvListView.Items[ sPairKey ];
						lvItem.SubItems[ 0 ].Text = sKeyURL;
						lvItem.SubItems[ 1 ].Text = iCount.ToString();
						lvItem.SubItems[ 2 ].Text = sTitle;
						lvItem.SubItems[ 3 ].Text = sTitleLength;

					} catch( Exception ex ) {
						debug_msg( string.Format( "MacroscopeDisplayTitles 1: {0}", ex.Message ) );
					}

				} else {
							
					try {

						ListViewItem lvItem = new ListViewItem ( sPairKey );

						lvItem.Name = sPairKey;

						lvItem.SubItems[ 0 ].Text = sKeyURL;
						lvItem.SubItems.Add( iCount.ToString() );
						lvItem.SubItems.Add( sTitle );
						lvItem.SubItems.Add( sTitleLength );

						lvListView.Items.Add( lvItem );

					} catch( Exception ex ) {
						debug_msg( string.Format( "MacroscopeDisplayTitles 2: {0}", ex.Message ) );
					}

				}
						
			}

		}

		/**************************************************************************/

	}

}
