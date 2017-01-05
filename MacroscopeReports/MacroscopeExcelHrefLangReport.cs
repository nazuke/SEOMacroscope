using System;
using System.Collections;
using ClosedXML.Excel;

namespace SEOMacroscope
{
	
	public class MacroscopeExcelHrefLangReport : MacroscopeExcelReports
	{

		/**************************************************************************/

		public MacroscopeExcelHrefLangReport ()
		{
		}

		/**************************************************************************/

		public void WriteXslx( MacroscopeJobMaster msJobMaster, string sOutputFilename )
		{				
			var wb = new XLWorkbook ();
			debug_msg( string.Format( "EXCEL sOutputPath: {0}", sOutputFilename ), 1 );
			this.BuildWorksheet( msJobMaster, wb, "Macroscope HrefLang", false );
			wb.SaveAs( sOutputFilename );
		}

		/**************************************************************************/

		void BuildWorksheet( MacroscopeJobMaster msJobMaster, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
		{				
			var ws = wb.Worksheets.Add( sWorksheetLabel );
			
			int iRow = 1;
			int iCol = 1;
			int iColMax = 1;

			Hashtable htLocales = ( Hashtable )msJobMaster.GetLocales();
			MacroscopeDocumentCollection htDocCollection = msJobMaster.GetDocCollection();
			
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
				
				foreach( string sKey in htDocCollection.Keys() ) {

					MacroscopeDocument msDoc = htDocCollection.Get( sKey );
					Hashtable htHrefLangs = ( Hashtable )msDoc.GetHrefLangs();
					
					string sSiteLocale = this.FormatIfMissing( msDoc.Locale );
					string sTitle = this.FormatIfMissing( msDoc.Title );

					ws.Cell( iRow, 1 ).Value = sSiteLocale;
					if( sSiteLocale == "MISSING" ) {
						ws.Cell( iRow, 1 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, 2 ).Value = sTitle;
					if( sTitle == "MISSING" ) {
						ws.Cell( iRow, 2 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, ( int )htLocaleCols[ msDoc.Locale ] ).Value = msDoc.GetUrl();

					foreach( string sLocale in htLocales.Keys ) {
						if( sLocale != null ) {
							if( htHrefLangs.ContainsKey( sLocale ) ) {
								MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[ sLocale ];
								ws.Cell( iRow, ( int )htLocaleCols[ sLocale ] ).Value = msHrefLang.GetUrl();
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
		
	}
	
}
