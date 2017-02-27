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

    public void WriteXslx ( MacroscopeJobMaster msJobMaster, string sOutputFilename )
    {
      var wb = new XLWorkbook ();
      DebugMsg( string.Format( "EXCEL sOutputPath: {0}", sOutputFilename ) );
      this.BuildWorksheet( msJobMaster, wb, "Macroscope Overview", false );
      try
      {
        wb.SaveAs( sOutputFilename );
      }
      catch( System.IO.IOException )
      {
        MacroscopeCannotSaveExcelFileException CannotSaveExcelFileException = new MacroscopeCannotSaveExcelFileException (
                                                                                string.Format( "Cannot write to Excel file at {0}", sOutputFilename )
                                                                              );
        throw CannotSaveExcelFileException;
      }
    }

    /**************************************************************************/

    void BuildWorksheet ( MacroscopeJobMaster msJobMaster, XLWorkbook wb, string sWorksheetLabel, Boolean bCheck )
    {
      var ws = wb.Worksheets.Add( sWorksheetLabel );

      int iRow = 1;
      int iCol = 1;
      int iColMax = 1;

      MacroscopeDocumentCollection DocCollection = msJobMaster.GetDocCollection();

      {

        ws.Cell( iRow, iCol ).Value = "Address";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Status Code";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Redirect";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Duration";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Content-Type";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Locale";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Server Date";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Modified Date";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Canonical";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Title"; 
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Error Condition";

        for( int i = 1 ; i <= iCol ; i++ )
        {
          ws.Cell( iRow, i ).Style.Font.SetBold();
        }

      }

      iColMax = iCol;

      iRow++;

      {

        foreach( string sKey in DocCollection.DocumentKeys() )
        {

          iCol = 1;

          MacroscopeDocument msDoc = DocCollection.GetDocument( sKey );

          this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );
          iCol++;

          this.InsertAndFormatStatusCodeCell( ws, iRow, iCol, msDoc );
          iCol++;
          
          this.InsertAndFormatRedirectCell( ws, iRow, iCol, msDoc );
          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetDurationInSecondsFormatted().ToString() ) );
          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetMimeType() ) );
          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetLocale() ) );
          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetDateServer() ) );
          iCol++;
          
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetDateModified() ) );
          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetCanonical() ) );
          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetTitle() ) );
          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetErrorCondition() ) );

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
