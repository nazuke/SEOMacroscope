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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace SEOMacroscope
{

	public partial class MacroscopeDocumentDetails : MacroscopeUserControl
	{

		/**************************************************************************/
		
		public MacroscopeDocumentDetails ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//

		}

		/**************************************************************************/
		
		void MacroscopeDocumentDetailsLoad ( object sender, EventArgs e )
		{
			debug_msg( string.Format( "MacroscopeDocumentDetailsLoad: {0}", "initialize" ) );
		}

		/**************************************************************************/

		public void UpdateDisplay ( MacroscopeJobMaster msJobMaster, string sURL )
		{

			// TODO: This blows up if the page is from a redirect. Probably need to use the original URL

			MacroscopeDocumentCollection msDocCollection = msJobMaster.DocCollectionGet();
			MacroscopeDocument msDoc = msDocCollection.Get( sURL );

			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate
						{
							this.RenderDocumentDetails( msDoc );
							this.RenderListViewHyperlinksIn( msDoc );
						}
					)
				);
			} else {
				this.RenderDocumentDetails( msDoc );
				this.RenderListViewHyperlinksIn( msDoc );
			}

		}

		/**************************************************************************/

		void RenderDocumentDetails ( MacroscopeDocument msDoc )
		{

			ListView lvListView = this.listViewDocumentInfo;

			lvListView.SuspendLayout();
			lvListView.Items.Clear();

			List<KeyValuePair<string,string>> lItems = msDoc.DetailDocumentDetails();

			for( int i = 0; i < lItems.Count; i++ ) {

				KeyValuePair<string,string> kvItem = lItems[ i ];

				debug_msg( string.Format( "RenderDocumentDetails: {0} => {1}", kvItem.Key, kvItem.Value ) );
				
				try {
					ListViewItem lvItem = new ListViewItem ( kvItem.Key );
					lvItem.Name = kvItem.Key;
					lvItem.SubItems.Add( kvItem.Value );
					lvListView.Items.Add( lvItem );
				} catch( Exception ex ) {
					debug_msg( string.Format( "RenderDocumentDetails: {0}", ex.Message ) );
				}
				
			}

			lvListView.ResumeLayout();

		}

		/**************************************************************************/

		void RenderListViewHyperlinksIn ( MacroscopeDocument msDoc )
		{

			ListView lvListView = this.listViewLinksIn;
			MacroscopeHyperlinksIn hlHyperlinksIn = ( MacroscopeHyperlinksIn )msDoc.GetHyperlinksIn();

			lvListView.SuspendLayout();

			foreach( string sUrlOrigin in hlHyperlinksIn.Keys() ) {

				foreach( MacroscopeHyperlinkIn hlHyperlinkIn in hlHyperlinksIn.GetLinks( sUrlOrigin ) ) {

					string sPairKey = string.Join( "::", sUrlOrigin, hlHyperlinkIn.GetLinkId().ToString() );

					if( lvListView.Items.ContainsKey( sPairKey ) ) {
							
						try {

							ListViewItem lvItem = lvListView.Items[ sPairKey ];
							lvItem.SubItems[ 0 ].Text = hlHyperlinkIn.GetLinkClass();
							lvItem.SubItems[ 1 ].Text = hlHyperlinkIn.GetUrlOrigin();
							lvItem.SubItems[ 2 ].Text = hlHyperlinkIn.GetUrlTarget();
							lvItem.SubItems[ 3 ].Text = hlHyperlinkIn.GetLinkText();
							lvItem.SubItems[ 4 ].Text = hlHyperlinkIn.GetAltText();

						} catch( Exception ex ) {
							debug_msg( string.Format( "RenderListViewHyperlinksIn 1: {0}", ex.Message ) );
						}

					} else {
							
						try {

							ListViewItem lvItem = new ListViewItem ( sPairKey );

							lvItem.Name = sPairKey;

							lvItem.SubItems[ 0 ].Text = hlHyperlinkIn.GetLinkClass();
							lvItem.SubItems.Add( hlHyperlinkIn.GetUrlOrigin() );						
							lvItem.SubItems.Add( hlHyperlinkIn.GetUrlTarget() );
							lvItem.SubItems.Add( hlHyperlinkIn.GetLinkText() );
							lvItem.SubItems.Add( hlHyperlinkIn.GetAltText() );

							lvListView.Items.Add( lvItem );

						} catch( Exception ex ) {
							debug_msg( string.Format( "RenderListViewHyperlinksIn 2: {0}", ex.Message ) );
						}

					}

				}

			}

			lvListView.ResumeLayout();
					
		}

		/**************************************************************************/

	}

}
