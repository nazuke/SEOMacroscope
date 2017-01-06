using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDisplayEmailAddresses : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;

		/**************************************************************************/

		public MacroscopeDisplayEmailAddresses ( MacroscopeMainForm msMainFormNew )
		{
			msMainForm = msMainFormNew;
		}

		/**************************************************************************/
				
		public void RefreshData ( MacroscopeDocumentCollection htDocCollection )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							ListView lvListView = this.msMainForm.GetDisplayEmailAddresses();
							this.RenderListView( lvListView, htDocCollection );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayEmailAddresses();
				this.RenderListView( lvListView, htDocCollection );
			}
		}

		/**************************************************************************/

		void RenderListView ( ListView lvListView, MacroscopeDocumentCollection htDocCollection )
		{

			lvListView.SuspendLayout();
			lvListView.Sorting = SortOrder.Ascending;

			foreach( string sURL in htDocCollection.Keys() ) {

				MacroscopeDocument msDoc = htDocCollection.Get( sURL );

				if( msDoc.GetIsHtml() ) {
				
					Hashtable htEmailAddresses = ( Hashtable )msDoc.GetEmailAddresses();

					foreach( string sEmailAddress in htEmailAddresses.Keys ) {

						string sPairKey = string.Join( "", sEmailAddress, sURL );

						if( lvListView.Items.ContainsKey( sPairKey ) ) {
							
							try {

								ListViewItem lvItem = lvListView.Items[ sPairKey ];
								lvItem.SubItems[ 0 ].Text = sEmailAddress;
								lvItem.SubItems[ 1 ].Text = sURL;

							} catch( Exception ex ) {
								debug_msg( string.Format( "MacroscopeDisplayEmailAddresses 1: {0}", ex.Message ) );
							}

						} else {
							
							try {

								ListViewItem lvItem = new ListViewItem ( sPairKey );

								lvItem.Name = sPairKey;

								lvItem.SubItems[ 0 ].Text = sEmailAddress;
								lvItem.SubItems.Add( sURL );

								lvListView.Items.Add( lvItem );

							} catch( Exception ex ) {
								debug_msg( string.Format( "MacroscopeDisplayEmailAddresses 2: {0}", ex.Message ) );
							}

						}
						
					}
					
				}

			}
			
			lvListView.ResumeLayout();
		}
		
		/**************************************************************************/

	}

}
