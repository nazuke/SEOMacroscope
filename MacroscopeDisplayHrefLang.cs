using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDisplayHrefLang
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;
		DataTable dtTable;

		/**************************************************************************/

		public MacroscopeDisplayHrefLang ( MacroscopeMainForm msMainFormNew )
		{
			msMainForm = msMainFormNew;
			dtTable = new DataTable ();
			msMainForm.GetDisplayHrefLang().DataSource = null;
		}

		/**************************************************************************/
				
		public void RefreshData( Hashtable htDocCollection, Hashtable htLocales )
		{

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke( new MethodInvoker ( delegate {
					msMainForm.GetDisplayHrefLang().DataSource = null;
				}
				) );
			} else {
				msMainForm.GetDisplayHrefLang().DataSource = null;
			}

			this.dtTable.Rows.Clear();
			this.dtTable.Columns.Clear();
			this.dtTable.Clear();

			{
				this.dtTable.Columns.Add( "Site Locale", typeof( string ) );
				this.dtTable.Columns.Add( "Title", typeof( string ) );
				foreach( string sLocale in htLocales.Keys ) {
					this.dtTable.Columns.Add( sLocale, typeof( string ) );
				}
			}

			foreach( string sKeyURL in htDocCollection.Keys ) {

				MacroscopeDocument msDoc = ( MacroscopeDocument )htDocCollection[ sKeyURL ];
				Hashtable htHrefLangs = ( Hashtable )msDoc.get_hreflangs();

				DataRow dtRow = this.dtTable.NewRow();

				dtRow.SetField( "Site Locale", msDoc.get_locale() );
				dtRow.SetField( "Title", msDoc.get_title() );
				dtRow.SetField( msDoc.locale, msDoc.get_url() );

				foreach( string sLocale in htLocales.Keys ) {
					if( sLocale != null ) {
						if( htHrefLangs.ContainsKey( sLocale ) ) {
							MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[ sLocale ];
							dtRow.SetField( sLocale, msHrefLang.get_url() );
						} else {
							dtRow.SetField( sLocale, "MISSSING" );
						}
					}
				}

				this.dtTable.Rows.Add( dtRow );
				
			}

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke( new MethodInvoker ( delegate {
					msMainForm.GetDisplayHrefLang().DataSource = this.dtTable;
				}
				) );
			} else {
				msMainForm.GetDisplayHrefLang().DataSource = this.dtTable;
			}

			
			

			
		}
						
		/**************************************************************************/

	}

}
