using System;
using System.Collections;
using ClosedXML.Excel;

namespace SEOMacroscope
{
	
	public class MacroscopeExcelReports
	{

		/**************************************************************************/

		public MacroscopeExcelReports ()
		{
		}

		/**************************************************************************/
		
		public void write_xslx_file_hreflangs( MacroscopeJob msJob, string sOutputFilename )
		{				
			string sOutputPath = sOutputFilename + ".xlsx";
			var wb = new XLWorkbook ();
			debug_msg( string.Format( "EXCEL sOutputPath: {0}", sOutputPath ), 1 );
			this.build_worksheet_hreflangs( msJob, wb, "Macroscope", false );
			wb.SaveAs( sOutputPath );
		}

		/**************************************************************************/

		void build_worksheet_hreflangs( MacroscopeJob msJob, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
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
				ws.Cell( iRow, iCol ).Style.Font.SetBold();
				iCol++;

				ws.Cell( iRow, iCol ).Value = "Title";
				ws.Cell( iRow, iCol ).Style.Font.SetBold();
				iCol++;

				foreach( string sLocale in htLocales.Keys ) {
					debug_msg( string.Format( "EXCEL sLocale: {0}", sLocale ), 2 );
					htLocaleCols[ sLocale ] = iCol;
					ws.Cell( iRow, iCol ).Value = sLocale;
					ws.Cell( iRow, iCol ).Style.Font.SetBold();
					iCol++;
				}

			}
			
			iColMax = iCol;
			
			iRow++;

			{
				
				foreach( string sKey in htDocCollection.Keys ) {

					MacroscopeDocument msDoc = ( MacroscopeDocument )htDocCollection[ sKey ];
					Hashtable htHrefLangs = ( Hashtable )msDoc.get_hreflangs();
					
					string sSiteLocale;
					string sTitle;

					if( msDoc.locale == null ) {
						sSiteLocale = "MISSING";
					} else {
						sSiteLocale = msDoc.locale;
					}
					
					if( msDoc.title == null ) {
						sTitle = "MISSING";
					} else {
						sTitle = msDoc.title;
					}

					ws.Cell( iRow, 1 ).Value = sSiteLocale;
					if( sSiteLocale == "MISSING" ) {
						ws.Cell( iRow, 1 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, 2 ).Value = sTitle;
					if( sTitle == "MISSING" ) {
						ws.Cell( iRow, 3 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, ( int )htLocaleCols[ msDoc.locale ] ).Value = msDoc.get_url();

					foreach( string sLocale in htLocales.Keys ) {
						if( htHrefLangs.ContainsKey( sLocale ) ) {
							MacroscopeHrefLang msHrefLang = (MacroscopeHrefLang)htHrefLangs[ sLocale ];
							ws.Cell( iRow, ( int )htLocaleCols[ sLocale ] ).Value = msHrefLang.get_url();
						} else {
							ws.Cell( iRow, ( int )htLocaleCols[ sLocale ] ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
							ws.Cell( iRow, ( int )htLocaleCols[ sLocale ] ).Value = "MISSING";					
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

		void debug_msg( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		void debug_msg( String sMsg, int iOffset )
		{
			String sMsgPadded = new String ( ' ', iOffset * 2 ) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}

		/**************************************************************************/
		
	}
	
}
