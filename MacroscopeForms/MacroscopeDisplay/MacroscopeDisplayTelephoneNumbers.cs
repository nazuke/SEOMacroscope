using System;
using System.Collections;
using System.Windows.Forms;

namespace SEOMacroscope
{

	public class MacroscopeDisplayTelephoneNumbers : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;

		/**************************************************************************/

		public MacroscopeDisplayTelephoneNumbers ( MacroscopeMainForm msMainFormNew )
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
							ListView lvListView = this.msMainForm.GetDisplayTelephoneNumbers();
							this.RenderListView( lvListView, htDocCollection );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayTelephoneNumbers();
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
				
					Hashtable htTelephoneNumbers = ( Hashtable )msDoc.GetTelephoneNumbers();

					foreach( string sTelephoneNumber in htTelephoneNumbers.Keys ) {

						string sPairKey = string.Join( "", sTelephoneNumber, sURL );

						if( lvListView.Items.ContainsKey( sPairKey ) ) {
							
							try {

								ListViewItem lvItem = lvListView.Items[ sPairKey ];
								lvItem.SubItems[ 0 ].Text = sTelephoneNumber;
								lvItem.SubItems[ 1 ].Text = sURL;

							} catch( Exception ex ) {
								debug_msg( string.Format( "MacroscopeDisplayTelephoneNumbers 1: {0}", ex.Message ) );
							}

						} else {
							
							try {

								ListViewItem lvItem = new ListViewItem ( sPairKey );

								lvItem.Name = sPairKey;

								lvItem.SubItems[ 0 ].Text = sTelephoneNumber;
								lvItem.SubItems.Add( sURL );

								lvListView.Items.Add( lvItem );

							} catch( Exception ex ) {
								debug_msg( string.Format( "MacroscopeDisplayTelephoneNumbers 2: {0}", ex.Message ) );
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
