using System;
using System.Collections;
using ClosedXML.Excel;

namespace SEOMacroscope
{
	
	public class MacroscopeExcelOverviewReport : MacroscopeExcelReports
	{

		/**************************************************************************/

		public MacroscopeExcelOverviewReport ()
		{
		}

		/**************************************************************************/

		public void WriteXslx( MacroscopeJobMaster msJobMaster, string sOutputFilename )
		{				
			var wb = new XLWorkbook ();
			debug_msg( string.Format( "EXCEL sOutputPath: {0}", sOutputFilename ), 1 );
			this.BuildWorksheet( msJobMaster, wb, "Macroscope Overview", false );
			wb.SaveAs( sOutputFilename );
		}

		/**************************************************************************/

		void BuildWorksheet( MacroscopeJobMaster msJobMaster, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
		{				
			var ws = wb.Worksheets.Add( sWorksheetLabel );
			
			int iRow = 1;
			int iCol = 1;
			int iColMax = 1;

			MacroscopeDocumentCollection htDocCollection = msJobMaster.DocCollectionGet();

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
				
				foreach( string sKey in htDocCollection.Keys() ) {

					iCol = 1;
					
					MacroscopeDocument msDoc = htDocCollection.Get( sKey );

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
		
	}
	
}
