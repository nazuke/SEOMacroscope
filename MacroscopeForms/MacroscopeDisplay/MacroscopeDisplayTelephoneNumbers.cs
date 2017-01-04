using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDisplayTelephoneNumbers : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;
		DataTable dtTable;

		const string constTelephoneNumber = "Telephone Number";
		const string constURL = "URL";

		/**************************************************************************/

		public MacroscopeDisplayTelephoneNumbers ( MacroscopeMainForm msMainFormNew )
		{
			msMainForm = msMainFormNew;
			dtTable = new DataTable ();
			msMainForm.GetDisplayTelephoneNumbers().DataSource = null;
		}

		/**************************************************************************/
				
		public void RefreshData ( Hashtable htDocCollection )
		{

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							msMainForm.GetDisplayTelephoneNumbers().DataSource = null;
						}
					)
				);
			} else {
				msMainForm.GetDisplayTelephoneNumbers().DataSource = null;
			}

			this.dtTable.Rows.Clear();
			this.dtTable.Columns.Clear();
			this.dtTable.Clear();

			this.dtTable.Columns.Add( constTelephoneNumber, typeof( string ) );
			this.dtTable.Columns.Add( constURL, typeof( string ) );

						lock( htDocCollection ) {
			
			foreach( string sKeyURL in htDocCollection.Keys ) {

				MacroscopeDocument msDoc = ( MacroscopeDocument )htDocCollection[sKeyURL];

				if( msDoc.GetIsHtml() ) {
				
					Hashtable htTelephoneNumbers = ( Hashtable )msDoc.GetTelephoneNumbers();

					foreach( string sTelephoneNumber in htTelephoneNumbers.Keys ) {
						DataRow dtRow = this.dtTable.NewRow();
						dtRow.SetField( constTelephoneNumber, sTelephoneNumber );
						dtRow.SetField( constURL, sKeyURL );
						this.dtTable.Rows.Add( dtRow );
					}

				}

			}

			}
				
			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							msMainForm.GetDisplayTelephoneNumbers().DataSource = this.dtTable;
						}
					)
				);
			} else {
				msMainForm.GetDisplayTelephoneNumbers().DataSource = this.dtTable;
			}

		}
						
		/**************************************************************************/

	}

}
