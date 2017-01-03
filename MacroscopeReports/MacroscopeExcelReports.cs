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

		public void WriteXslxFileOverview( MacroscopeJobMaster msJob, string sOutputFilename )
		{				
			string sOutputPath = sOutputFilename + ".xlsx";
			var wb = new XLWorkbook ();
			debug_msg( string.Format( "EXCEL sOutputPath: {0}", sOutputPath ), 1 );
			this.BuildWorksheetOverview( msJob, wb, "Macroscope Overview", false );
			wb.SaveAs( sOutputPath );
		}

		/**************************************************************************/

		void BuildWorksheetOverview( MacroscopeJobMaster msJob, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
		{				
			var ws = wb.Worksheets.Add( sWorksheetLabel );
			
			int iRow = 1;
			int iCol = 1;
			int iColMax = 1;

			Hashtable htDocCollection = ( Hashtable )msJob.GetDocCollection();

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

					string sURL = this.FormatIfMissing( msDoc.GetUrl() );
					string sStatusCode = this.FormatIfMissing( msDoc.GetStatusCode().ToString() );
					string sSiteLocale = this.FormatIfMissing( msDoc.Locale );	
					string sMimeType = this.FormatIfMissing( msDoc.MimeType );	
					string sDateServer = this.FormatIfMissing( msDoc.GetDateServer() );
					string sCanonical = this.FormatIfMissing( msDoc.Canonical );
					string sTitle = this.FormatIfMissing( msDoc.Title );
					
					this.InsertAndFormatContentCell( ws, iRow, iCol, sURL );
					iCol++;
					
					this.InsertAndFormatContentCell( ws, iRow, iCol, sStatusCode );
					iCol++;

					this.InsertAndFormatContentCell( ws, iRow, iCol, sSiteLocale );
					iCol++;

					this.InsertAndFormatContentCell( ws, iRow, iCol, sMimeType );
					iCol++;	
					
					this.InsertAndFormatContentCell( ws, iRow, iCol, sDateServer );
					iCol++;	

					this.InsertAndFormatContentCell( ws, iRow, iCol, sCanonical );
					iCol++;

					this.InsertAndFormatContentCell( ws, iRow, iCol, sTitle );

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

		public void WriteXslxFileHrefLang( MacroscopeJobMaster msJob, string sOutputFilename )
		{				
			string sOutputPath = sOutputFilename + ".xlsx";
			var wb = new XLWorkbook ();
			debug_msg( string.Format( "EXCEL sOutputPath: {0}", sOutputPath ), 1 );
			this.BuildWorksheetHrefLang( msJob, wb, "Macroscope HrefLang", false );
			wb.SaveAs( sOutputPath );
		}

		/**************************************************************************/

		void BuildWorksheetHrefLang( MacroscopeJobMaster msJob, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
		{				
			var ws = wb.Worksheets.Add( sWorksheetLabel );
			
			int iRow = 1;
			int iCol = 1;
			int iColMax = 1;

			Hashtable htLocales = ( Hashtable )msJob.GetLocales();
			Hashtable htDocCollection = ( Hashtable )msJob.GetDocCollection();
			
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

		void InsertAndFormatContentCell( IXLWorksheet ws, int iRow, int iCol, string sValue )
		{
			ws.Cell( iRow, iCol ).Value = sValue;
			if( sValue == "MISSING" ) {
				ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
			}
		}
		
		/**************************************************************************/
		
		string FormatIfMissing( string sString )
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
