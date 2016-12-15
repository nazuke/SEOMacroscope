using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;
using ClosedXML.Excel;
using System.Threading;

namespace SEOMacroscope
{
	
	public class MacroscopeExcelReports
	{

		/**************************************************************************/

		public MacroscopeExcelReports ()
		{
		}

		/**************************************************************************/

		
		void write_xslx_file( string sOutputFilename )
		{				
			var wb = new XLWorkbook ();
			//this.build_worksheet( wb, "Macroscope", false );
			/*
			foreach (string sLocaleBase in this.htBaseURLs.Keys) {
				this.build_worksheet( wb, sLocaleBase, true );
			}
			*/
			wb.SaveAs( sOutputFilename + ".xlsx" );
		}

		/**************************************************************************/
/*
		void build_worksheet( XLWorkbook wb, string sLocaleBase, Boolean bCheck )
		{				
			var ws = wb.Worksheets.Add( sLocaleBase );
			
			int iRow = 1;
			int iCol = 1;
			int iColMax = 1;
			
			Hashtable htLocaleCols = new Hashtable ();
			
			{
			
				ws.Cell( iRow, iCol ).Value = "Site Locale";
				ws.Cell( iRow, iCol ).Style.Font.SetBold();
				iCol++;
				
				ws.Cell( iRow, iCol ).Value = "Site Breadcrumb";
				ws.Cell( iRow, iCol ).Style.Font.SetBold();
				iCol++;
				
				ws.Cell( iRow, iCol ).Value = "Label";
				ws.Cell( iRow, iCol ).Style.Font.SetBold();
				iCol++;

				foreach (string sLocale in this.htLocales.Keys) {
					htLocaleCols[ sLocale ] = iCol;
					ws.Cell( iRow, iCol ).Value = sLocale;
					ws.Cell( iRow, iCol ).Style.Font.SetBold();
					iCol++;
				}
			
			}
			
			iColMax = iCol;
			
			iRow++;

			{
				foreach (string sHref in this.htMappedURLs.Keys) {
				
					MegaMenuMappedURL mURL = (MegaMenuMappedURL)this.htMappedURLs[ sHref ];
				
					string sSiteLocale;
					string sBreadcrumb;
					string sLabel;

					if (bCheck && ( !mURL.locale.Equals( sLocaleBase ) )) {
						continue;
					}

					if (mURL.locale == null) {
						sSiteLocale = "MISSING";
					} else {
						sSiteLocale = mURL.locale.ToString();
					}
					
					if (mURL.breadcrumb == null) {
						sBreadcrumb = "MISSING";
					} else {
						sBreadcrumb = mURL.breadcrumb.ToString();
					}
					
					if (mURL.label == null) {
						sLabel = "MISSING";
					} else {
						sLabel = mURL.label.ToString();
					}
				
					ws.Cell( iRow, 1 ).Value = sSiteLocale;
					if (sSiteLocale == "MISSING") {
						ws.Cell( iRow, 1 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, 2 ).Value = sBreadcrumb;
					if (sBreadcrumb == "MISSING") {
						ws.Cell( iRow, 2 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, 3 ).Value = sLabel;
					if (sLabel == "MISSING") {
						ws.Cell( iRow, 3 ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
					}

					ws.Cell( iRow, (int)htLocaleCols[ mURL.locale ] ).Value = mURL.href.ToString();

					foreach (string sLocale in this.htLocales.Keys) {
						if (mURL.alternates.ContainsKey( sLocale )) {
							ws.Cell( iRow, (int)htLocaleCols[ sLocale ] ).Value = mURL.alternates[ sLocale ];
						} else {
							ws.Cell( iRow, (int)htLocaleCols[ sLocale ] ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
							ws.Cell( iRow, (int)htLocaleCols[ sLocale ] ).Value = "MISSING";					
						}

					}

					iRow++;

				}

			}

			{
				var rangeData = ws.Range( 1, 1, iRow - 1, iColMax - 1 );
				var excelTable = rangeData.CreateTable();
				//excelTable.Sort( "Site Locale", XLSortOrder.Ascending, false, true );				
			}



			ws.Columns().AdjustToContents();

		}
*/

		/**************************************************************************/
		
	}
	
}
