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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{
		
	/// <summary>
	/// Description of MacroscopeDisplayErrors.
	/// </summary>

	public class MacroscopeDisplayErrors : MacroscopeDisplayListView
	{
		
		/**************************************************************************/

		static Boolean ListViewConfigured = false;
		
		/**************************************************************************/

		public MacroscopeDisplayErrors ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
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

			foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() ) {

				Boolean bProceed = false;

				if( ( msDoc.GetStatusCode() >= 400 ) && ( msDoc.GetStatusCode() <= 499 ) ) {
					bProceed = true;
				} else if( ( msDoc.GetStatusCode() >= 500 ) && ( msDoc.GetStatusCode() <= 599 ) ) {
					bProceed = true;
				}
				
				if( bProceed ) {
					this.RenderListView( msDoc, msDoc.GetUrl() );
				} else {
					RemoveFromListView( msDoc.GetUrl() );
				}
			
			}
		
		}

		/**************************************************************************/

		protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
		{

			string sPairKey = sUrl;
			string sStatus = msDoc.GetStatusCode().ToString();
			
			this.lvListView.BeginUpdate();
			
			ListViewItem lvItem = null;
			
			if( this.lvListView.Items.ContainsKey( sPairKey ) ) {
							
				try {

					lvItem = this.lvListView.Items[ sPairKey ];
											
					lvItem.SubItems[ 0 ].Text = sUrl;
					lvItem.SubItems[ 1 ].Text = sStatus;

				} catch( Exception ex ) {
					this.DebugMsg( string.Format( "RenderListView 1: {0}", ex.Message ) );
				}

			} else {
							
				try {

					lvItem = new ListViewItem ( sPairKey );

					lvItem.Name = sPairKey;

					lvItem.SubItems[ 0 ].Text = sUrl;
					lvItem.SubItems.Add( sStatus );

					this.lvListView.Items.Add( lvItem );

				} catch( Exception ex ) {
					this.DebugMsg( string.Format( "RenderListView 2: {0}", ex.Message ) );
				}

			}

			if( lvItem != null ) {

				lvItem.UseItemStyleForSubItems = false;
				lvItem.ForeColor = Color.Blue;
				
				if( Regex.IsMatch( sStatus, "^[2]" ) ) {
					lvItem.SubItems[ 1 ].ForeColor = Color.Green;
				} else if( Regex.IsMatch( sStatus, "^[3]" ) ) {
					lvItem.SubItems[ 1 ].ForeColor = Color.Goldenrod;
				} else if( Regex.IsMatch( sStatus, "^[45]" ) ) {
					lvItem.SubItems[ 1 ].ForeColor = Color.Red;
				} else {
					lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
				}

			}

			this.lvListView.EndUpdate();

		}

		/**************************************************************************/

		void RemoveFromListView ( string sUrl )
		{

			string sPairKey = sUrl;

			if( this.lvListView.Items.ContainsKey( sPairKey ) ) {

				this.lvListView.BeginUpdate();

				this.lvListView.Items.Remove( this.lvListView.Items[ sPairKey ] );

				this.lvListView.EndUpdate();

			}

		}

		/**************************************************************************/
	
	}

}
