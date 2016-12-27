using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDisplayStructure
	{

		/**************************************************************************/

		MacroscopeMainForm msMainForm;
		DataTable dtTable;

		const string constURL = "URL";
		const string constStatus = "Status";
		const string constContentType = "Content Type";
		const string constLocale = "Locale";
		
		const string constCanonical = "Canonical";
		
		const string constInlinks = "Inlinks";
		const string constOutlinks = "Outlinks";
		
		const string constTitle = "Title";
		const string constTitleLen = "Title Length";
		
		const string constDescription = "Description";
		const string constDescriptionLen = "Description Length";
		
		/**************************************************************************/

		public MacroscopeDisplayStructure ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;
			dtTable = new DataTable ();
			
			dtTable.Columns.Add( constURL, typeof( string ) );
			dtTable.Columns.Add( constStatus, typeof( string ) );			
			dtTable.Columns.Add( constContentType, typeof( string ) );			
			dtTable.Columns.Add( constLocale, typeof( string ) );	

			dtTable.Columns.Add( constCanonical, typeof( string ) );

			dtTable.Columns.Add( constInlinks, typeof( string ) );
			dtTable.Columns.Add( constOutlinks, typeof( string ) );	

			dtTable.Columns.Add( constTitle, typeof( string ) );	
			dtTable.Columns.Add( constTitleLen, typeof( string ) );	
			dtTable.Columns.Add( constDescription, typeof( string ) );	
			dtTable.Columns.Add( constDescriptionLen, typeof( string ) );	
												
			msMainForm.GetDisplayStructure().DataSource = dtTable;

		}

		/**************************************************************************/

		public void RefreshData( Hashtable htDocCollection )
		{

			
			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke( new MethodInvoker ( delegate {
					msMainForm.GetDisplayStructure().DataSource = null;
				}
				) );
			} else {
				msMainForm.GetDisplayStructure().DataSource = null;
			}

			dtTable.Rows.Clear();

			foreach( string sKeyURL in htDocCollection.Keys ) {

				DataRow dtRow = dtTable.NewRow();
				MacroscopeDocument msDoc = ( MacroscopeDocument )htDocCollection[ sKeyURL ];

				dtRow.SetField( constURL, msDoc.get_url() );

				dtRow.SetField( constStatus, msDoc.get_status_code() );
				dtRow.SetField( constContentType, msDoc.get_mime_type() );
				dtRow.SetField( constLocale, msDoc.get_locale() );
								
				dtRow.SetField( constCanonical, msDoc.get_canonical() );

				dtRow.SetField( constInlinks, msDoc.count_inlinks() );
				dtRow.SetField( constOutlinks, msDoc.count_outlinks() );
												
				dtRow.SetField( constTitle, msDoc.get_title() );
				dtRow.SetField( constTitleLen, msDoc.get_title().Length );

				dtRow.SetField( constDescription, msDoc.get_description() );
				dtRow.SetField( constDescriptionLen, msDoc.get_description().Length );

				dtTable.Rows.Add( dtRow );

			}


			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke( new MethodInvoker ( delegate {
					msMainForm.GetDisplayStructure().DataSource = this.dtTable;
				}
				) );
			} else {
				msMainForm.GetDisplayStructure().DataSource = this.dtTable;
			}



						
		}

		/**************************************************************************/

	}

}
