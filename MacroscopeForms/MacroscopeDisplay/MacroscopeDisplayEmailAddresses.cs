using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDisplayEmailAddresses : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;
		DataTable dtTable;
		
		const string constEmailAddress = "Email Address";
		const string constURL = "URL";

		/**************************************************************************/

		public MacroscopeDisplayEmailAddresses ( MacroscopeMainForm msMainFormNew )
		{
			msMainForm = msMainFormNew;
			dtTable = new DataTable ();
			msMainForm.GetDisplayEmailAddresses().DataSource = null;
		}

		/**************************************************************************/
				
		public void RefreshData ( Hashtable htDocCollection )
		{

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							msMainForm.GetDisplayEmailAddresses().DataSource = null;
						}
					)
				);
			} else {
				msMainForm.GetDisplayEmailAddresses().DataSource = null;
			}

			this.dtTable.Rows.Clear();
			this.dtTable.Columns.Clear();
			this.dtTable.Clear();

			this.dtTable.Columns.Add( constEmailAddress, typeof( string ) );
			this.dtTable.Columns.Add( constURL, typeof( string ) );

			foreach( string sKeyURL in htDocCollection.Keys ) {

				MacroscopeDocument msDoc = ( MacroscopeDocument )htDocCollection[sKeyURL];

				if( msDoc.GetIsHtml() ) {
				
					Hashtable htEmailAddresses = ( Hashtable )msDoc.GetEmailAddresses();
					
					foreach( string sEmailAddress in htEmailAddresses.Keys ) {
						DataRow dtRow = this.dtTable.NewRow();
						dtRow.SetField( constEmailAddress, sEmailAddress );
						dtRow.SetField( constURL, sKeyURL );
						this.dtTable.Rows.Add( dtRow );
					}

				}

			}

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							msMainForm.GetDisplayEmailAddresses().DataSource = this.dtTable;
						}
					)
				);
			} else {
				msMainForm.GetDisplayEmailAddresses().DataSource = this.dtTable;
			}
			
		}
						
		/**************************************************************************/

	}

}
