using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDisplayStructure : Macroscope
	{

		/**************************************************************************/

		MacroscopeMainForm msMainForm;
		DataTable dtTable;

		const string constURL = "URL";
		
		const string constStatus = "Status";
		const string constIsRedirect = "Redirect";
				
		const string constContentType = "Content Type";
		const string constLang = "Lang";
		
		const string constCanonical = "Canonical";
		
		const string constInhyperlinks = "Links In";
		const string constOuthyperlinks = "Links Out";
		
		const string constTitle = "Title";
		const string constTitleLen = "Title Length";
		
		const string constDescription = "Description";
		const string constDescriptionLen = "Description Length";
		
		const string constKeywords = "Keywords";
		const string constKeywordsLen = "Keywords Length";
		const string constKeywordsCount = "Keywords Count";
		
		const string constH1 = "First H1";
		const string constH2 = "First H2";
				
		/**************************************************************************/

		public MacroscopeDisplayStructure ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;
			dtTable = new DataTable ();
			
			dtTable.Columns.Add( constURL, typeof( string ) );

			dtTable.Columns.Add( constStatus, typeof( string ) );	
			dtTable.Columns.Add( constIsRedirect, typeof( string ) );

			dtTable.Columns.Add( constContentType, typeof( string ) );
			dtTable.Columns.Add( constLang, typeof( string ) );	

			dtTable.Columns.Add( constCanonical, typeof( string ) );

			dtTable.Columns.Add( constInhyperlinks, typeof( string ) );
			dtTable.Columns.Add( constOuthyperlinks, typeof( string ) );	

			dtTable.Columns.Add( constTitle, typeof( string ) );	
			dtTable.Columns.Add( constTitleLen, typeof( string ) );	
			
			dtTable.Columns.Add( constDescription, typeof( string ) );	
			dtTable.Columns.Add( constDescriptionLen, typeof( string ) );	
			
			dtTable.Columns.Add( constKeywords, typeof( string ) );
			dtTable.Columns.Add( constKeywordsLen, typeof( string ) );	
			dtTable.Columns.Add( constKeywordsCount, typeof( string ) );	
			
			dtTable.Columns.Add( constH1, typeof( string ) );	
			dtTable.Columns.Add( constH2, typeof( string ) );	

			msMainForm.GetDisplayStructure().DataSource = dtTable;

		}

		/**************************************************************************/

		public void RefreshData ( Hashtable htDocCollection )
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
				MacroscopeDocument msDoc = ( MacroscopeDocument )htDocCollection[sKeyURL];

				dtRow.SetField( constURL, msDoc.GetUrl() );

				dtRow.SetField( constStatus, msDoc.GetStatusCode() );
				dtRow.SetField( constIsRedirect, msDoc.GetIsRedirect() );

				dtRow.SetField( constContentType, msDoc.GetMimeType() );

				{
					string sLang = msDoc.GetLang();
					if( sLang == null ) {
						sLang = "";
					}
					dtRow.SetField( constLang, sLang );
				}
								
				dtRow.SetField( constCanonical, msDoc.GetCanonical() );

				dtRow.SetField( constInhyperlinks, msDoc.CountHyperlinksIn() );
				dtRow.SetField( constOuthyperlinks, msDoc.CountHyperlinksOut() );
												
				dtRow.SetField( constTitle, msDoc.GetTitle() );
				dtRow.SetField( constTitleLen, msDoc.GetTitleLength() );

				dtRow.SetField( constDescription, msDoc.GetDescription() );
				dtRow.SetField( constDescriptionLen, msDoc.GetDescriptionLength() );
				
				dtRow.SetField( constKeywords, msDoc.GetKeywords() );
				dtRow.SetField( constKeywordsLen, msDoc.GetKeywordsLength() );
				dtRow.SetField( constKeywordsCount, msDoc.GetKeywordsCount() );

				{
					ArrayList aHeadings = msDoc.GetHeadings1();
					string sText = "";
					if( aHeadings.Count > 0 ) {
						sText = ( string )aHeadings[0];
					}
					dtRow.SetField( constH1, sText );
				}
				
				{
					ArrayList aHeadings = msDoc.GetHeadings2();
					string sText = "";
					if( aHeadings.Count > 0 ) {
						sText = ( string )aHeadings[0];
					}
					dtRow.SetField( constH2, sText );
				}

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
