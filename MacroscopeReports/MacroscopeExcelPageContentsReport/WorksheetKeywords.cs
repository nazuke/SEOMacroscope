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

  public partial class MacroscopeExcelPageContentsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageKeywords (
      MacroscopeJobMaster JobMaster,
      XLWorkbook wb,
      string sWorksheetLabel
    )
    {
      var ws = wb.Worksheets.Add( sWorksheetLabel );

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

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        Boolean Proceed = false;

        if( msDoc.GetIsExternal() )
        {
          return;
        }
            
        if( msDoc.GetIsHtml() )
        {
          Proceed = true;
        }
        else
        if( msDoc.GetIsPdf() )
        {
          Proceed = true;
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
            Occurrences = DocCollection.GetStatsKeywordsCount( Keywords );
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

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Occurrences.ToString() ) );

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Keywords ) );

          iCol++;
          
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( KeywordsLength.ToString() ) );

          iCol++;
          
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( KeywordsNumber.ToString() ) );

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
