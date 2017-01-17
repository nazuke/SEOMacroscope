/*
	
	This file is part of SEOMacroscope.
	
	Copyright 2017 Jason Holland.
	
	The GitHub repository may be found at:
	
		https://github.com/nazuke/SEOMacroscope
	
	Foobar is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.
	
	Foobar is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.
	
	You should have received a copy of the GNU General Public License
	along with Foobar.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections;
using System.Collections.Generic;
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

		public void WriteXslx ( MacroscopeJobMaster msJobMaster, string sOutputFilename )
		{				
			var wb = new XLWorkbook ();
			debug_msg( string.Format( "EXCEL sOutputPath: {0}", sOutputFilename ) );
			this.BuildWorksheet( msJobMaster, wb, "Macroscope HrefLang", false );
			wb.SaveAs( sOutputFilename );
		}

		/**************************************************************************/

		void BuildWorksheet ( MacroscopeJobMaster msJobMaster, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
		{				
			var ws = wb.Worksheets.Add( sWorksheetLabel );
			
			int iRow = 1;
			int iCol = 1;
			int iColMax = 1;

			Dictionary<string,string> htLocales = msJobMaster.LocalesGet();
			MacroscopeDocumentCollection htDocCollection = msJobMaster.DocCollectionGet();
			
			Hashtable htLocaleCols = new Hashtable ();
			
			{
			
				ws.Cell( iRow, iCol ).Value = "Site Locale";
				iCol++;

				ws.Cell( iRow, iCol ).Value = "Title";
				iCol++;

				foreach( string sLocale in htLocales.Keys ) {
					debug_msg( string.Format( "EXCEL sLocale: {0}", sLocale ) );
					htLocaleCols[sLocale] = iCol;
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
					Dictionary<string,MacroscopeHrefLang> htHrefLangs = msDoc.GetHrefLangs();
					
					string sSiteLocale = this.FormatIfMissing( msDoc.GetLocale() );
					string sTitle = this.FormatIfMissing( msDoc.GetTitle() );

					ws.Cell( iRow, 1 ).Value = sSiteLocale;
					if( sSiteLocale == "MISSING" ) {
						ws.Cell( iRow, 1 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, 2 ).Value = sTitle;
					if( sTitle == "MISSING" ) {
						ws.Cell( iRow, 2 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}
					
					ws.Cell( iRow, ( int )htLocaleCols[msDoc.GetLocale()] ).Value = msDoc.GetUrl();

					foreach( string sLocale in htLocales.Keys ) {
						if( sLocale != null ) {
							if( htHrefLangs.ContainsKey( sLocale ) ) {
								MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[sLocale];
								ws.Cell( iRow, ( int )htLocaleCols[sLocale] ).Value = msHrefLang.GetUrl();
							} else {
								ws.Cell( iRow, ( int )htLocaleCols[sLocale] ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
								ws.Cell( iRow, ( int )htLocaleCols[sLocale] ).Value = "MISSING";					
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
