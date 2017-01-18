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
			DebugMsg( string.Format( "EXCEL sOutputPath: {0}", sOutputFilename ) );
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

			MacroscopeDocumentCollection htDocCollection = msJobMaster.GetDocCollection();

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
					string sSiteLocale = this.FormatIfMissing( msDoc.GetLocale() );
					string sMimeType = this.FormatIfMissing( msDoc.GetMimeType() );
					string sDateServer = this.FormatIfMissing( msDoc.GetDateServer() );
					string sCanonical = this.FormatIfMissing( msDoc.GetCanonical() );
					string sTitle = this.FormatIfMissing( msDoc.GetTitle() );
					
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
