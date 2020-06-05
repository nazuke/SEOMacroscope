/*

  This file is part of SEOMacroscope.

  Copyright 2020 Jason Holland.

  The GitHub repository may be found at:

    https://github.com/nazuke/SEOMacroscope

  SEOMacroscope is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  SEOMacroscope is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelPageMetadataReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageKeywords (
      MacroscopeJobMaster JobMaster,
      XLWorkbook wb,
      string WorksheetLabel
    )
    {
      var ws = wb.Worksheets.Add( WorksheetLabel );

      int iRow = 1;
      int iCol = 1;
      int iColMax = 1;

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();

      {

        ws.Cell( iRow, iCol ).Value = "URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Occurrences";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Keywords";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Keywords Length";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Number of Keywords";

      }

      iColMax = iCol;

      iRow++;

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        bool Proceed = false;

        if( msDoc.GetIsExternal() )
        {
          continue;
        }

        if( msDoc.GetIsRedirect() )
        {
          continue;
        }

        switch( msDoc.GetDocumentType() )
        {
          case MacroscopeConstants.DocumentType.HTML:
            Proceed = true;
            break;
          case MacroscopeConstants.DocumentType.PDF:
            Proceed = true;
            break;
          default:
            break;
        }

        if( Proceed )
        {

          iCol = 1;

          string Keywords = msDoc.GetKeywords();
          int Occurrences = 0;
          int KeywordsLength = msDoc.GetKeywordsLength();
          int KeywordsNumber = msDoc.GetKeywordsCount();

          if( KeywordsLength > 0 )
          {
            Occurrences = DocCollection.GetStatsKeywordsCount( msDoc );
          }

          this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );

          if( msDoc.GetIsInternal() )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, Occurrences );

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Keywords ) );

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, KeywordsLength );

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, KeywordsNumber );

          iRow++;

        }

      }

      {
        var rangeData = ws.Range( 1, 1, iRow - 1, iColMax );
        var excelTable = rangeData.CreateTable();
      }

    }

    /**************************************************************************/

  }

}
