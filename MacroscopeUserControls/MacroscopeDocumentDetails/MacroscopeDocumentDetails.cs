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
						delegate {
							this.RenderDocumentDetails( msDoc );
						}
					)
				);
			} else {
				this.RenderDocumentDetails( msDoc );
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

	}

}
