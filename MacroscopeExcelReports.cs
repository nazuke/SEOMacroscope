using System;
using System.Collections;
using ClosedXML.Excel;

namespace SEOMacroscope
{
	
	public class MacroscopeExcelReports : Macroscope
	{

		/**************************************************************************/

		public MacroscopeExcelReports ()
		{
		}

		/**************************************************************************/

		public void write_xslx_file_overview( MacroscopeJob msJob, string sOutputFilename )
		{				
			string sOutputPath = sOutputFilename + ".xlsx";
			var wb = new XLWorkbook ();
			debug_msg( string.Format( "EXCEL sOutputPath: {0}", sOutputPath ), 1 );
			this.build_worksheet_overview( msJob, wb, "Macroscope Overview", false );
			wb.SaveAs( sOutputPath );
		}

		/**************************************************************************/

		void build_worksheet_overview( MacroscopeJob msJob, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
		{				
			var ws = wb.Worksheets.Add( sWorksheetLabel );
			
			int iRow = 1;
			int iCol = 1;
			int iColMax = 1;

			Hashtable htDocCollection = ( Hashtable )msJob.get_doc_collection();

			{
			
				ws.Cell( iRow, iCol ).Value = "Address";
				iCol++;

				ws.Cell( iRow, iCol ).Value = "Status";
				iCol++;

				ws.Cell( iRow, iCol ).Value = "Locale";
				iCol++;
				
				ws.Cell( iRow, iCol ).Value = "Content-Type";
				iCol++;
				
				ws.Cell( iRow, iCol ).Value = "Server Date";
				iCol++;

				ws.Cell( iRow, iCol ).Value = "Canonical";
				iCol++;

				ws.Cell( iRow, iCol ).Value = "Title";

				for( int i = 1; i <= iCol; i++ ) {
					ws.Cell( iRow, i ).Style.Font.SetBold();
				}

			}
			
			iColMax = iCol;
			
			iRow++;

			{
				
				foreach( string sKey in htDocCollection.Keys ) {

					iCol = 1;
					
					MacroscopeDocument msDoc = ( MacroscopeDocument )htDocCollection[ sKey ];

					string sURL = format_if_missing( msDoc.get_url() );
					string sStatusCode = format_if_missing( msDoc.get_status_code().ToString() );
					string sSiteLocale = format_if_missing( msDoc.locale );	
					string sMimeType = format_if_missing( msDoc.mime_type );	
					string sDateServer = format_if_missing( msDoc.get_date_server() );
					string sCanonical = format_if_missing( msDoc.canonical );
					string sTitle = format_if_missing( msDoc.title );
					
					this.insert_and_format_content_cell( ws, iRow, iCol, sURL );
					iCol++;
					
					this.insert_and_format_content_cell( ws, iRow, iCol, sStatusCode );
					iCol++;

					this.insert_and_format_content_cell( ws, iRow, iCol, sSiteLocale );
					iCol++;

					this.insert_and_format_content_cell( ws, iRow, iCol, sMimeType );
					iCol++;	
					
					this.insert_and_format_content_cell( ws, iRow, iCol, sDateServer );
					iCol++;	

					this.insert_and_format_content_cell( ws, iRow, iCol, sCanonical );
					iCol++;

					this.insert_and_format_content_cell( ws, iRow, iCol, sTitle );

					iRow++;

				}

			}

			{
				var rangeData = ws.Range( 1, 1, iRow - 1, iColMax );
				var excelTable = rangeData.CreateTable();
				excelTable.Sort( "Address", XLSortOrder.Ascending, false, true );				
			}

			ws.Columns().AdjustToContents();

		}

		/**************************************************************************/

		public void write_xslx_file_hreflang( MacroscopeJob msJob, string sOutputFilename )
		{				
			string sOutputPath = sOutputFilename + ".xlsx";
			var wb = new XLWorkbook ();
			debug_msg( string.Format( "EXCEL sOutputPath: {0}", sOutputPath ), 1 );
			this.build_worksheet_hreflang( msJob, wb, "Macroscope HrefLang", false );
			wb.SaveAs( sOutputPath );
		}

		/**************************************************************************/

		void build_worksheet_hreflang( MacroscopeJob msJob, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
		{				
			var ws = wb.Worksheets.Add( sWorksheetLabel );
			
			int iRow = 1;
			int iCol = 1;
			int iColMax = 1;

			Hashtable htLocales = ( Hashtable )msJob.get_locales();
			Hashtable htDocCollection = ( Hashtable )msJob.get_doc_collection();
			
			Hashtable htLocaleCols = new Hashtable ();
			
			{
			
				ws.Cell( iRow, iCol ).Value = "Site Locale";
				iCol++;

				ws.Cell( iRow, iCol ).Value = "Title";
				iCol++;

				foreach( string sLocale in htLocales.Keys ) {
					debug_msg( string.Format( "EXCEL sLocale: {0}", sLocale ), 2 );
					htLocaleCols[ sLocale ] = iCol;
					ws.Cell( iRow, iCol ).Value = sLocale;
					iCol++;
				}

				for( int i = 1; i <= iCol; i++ ) {
					ws.Cell( iRow, i ).Style.Font.SetBold();
				}

			}
			
			iColMax = iCol;
			
			iRow++;

			{
				
				foreach( string sKey in htDocCollection.Keys ) {

					MacroscopeDocument msDoc = ( MacroscopeDocument )htDocCollection[ sKey ];
					Hashtable htHrefLangs = ( Hashtable )msDoc.GetHreflangs();
					
					string sSiteLocale = format_if_missing( msDoc.locale );
					string sTitle = format_if_missing( msDoc.title );

					ws.Cell( iRow, 1 ).Value = sSiteLocale;
					if( sSiteLocale == "MISSING" ) {
						ws.Cell( iRow, 1 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, 2 ).Value = sTitle;
					if( sTitle == "MISSING" ) {
						ws.Cell( iRow, 2 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, ( int )htLocaleCols[ msDoc.locale ] ).Value = msDoc.get_url();

					foreach( string sLocale in htLocales.Keys ) {
						if( sLocale != null ) {
							if( htHrefLangs.ContainsKey( sLocale ) ) {
								MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[ sLocale ];
								ws.Cell( iRow, ( int )htLocaleCols[ sLocale ] ).Value = msHrefLang.get_url();
							} else {
								ws.Cell( iRow, ( int )htLocaleCols[ sLocale ] ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
								ws.Cell( iRow, ( int )htLocaleCols[ sLocale ] ).Value = "MISSING";					
							}
						}
					}

					iRow++;

				}

			}

			{
				var rangeData = ws.Range( 1, 1, iRow - 1, iColMax - 1 );
				var excelTable = rangeData.CreateTable();
				excelTable.Sort( "Title", XLSortOrder.Ascending, false, true );				
			}

			ws.Columns().AdjustToContents();

		}

		/**************************************************************************/

		void insert_and_format_content_cell( IXLWorksheet ws, int iRow, int iCol, string sValue )
		{
			ws.Cell( iRow, iCol ).Value = sValue;
			if( sValue == "MISSING" ) {
				ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
			}
		}
		
		/**************************************************************************/
		
		string format_if_missing( string sString )
		{
			string sFormatted;
			if( sString == null ) {
				sFormatted = "MISSING";
			} else {
				sFormatted = sString;
			}
			return( sFormatted );
		}
		
		/**************************************************************************/
		
	}
	
}
