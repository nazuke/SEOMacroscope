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
using System.Collections.Generic;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelPageContentsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageHeadings (
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

        ws.Cell( iRow, iCol ).Value = "Occurences";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Order";

        for( int i = 1 ; i <= 6 ; i++ )
        {
          iCol++;
          ws.Cell( iRow, iCol ).Value = string.Format( "H{0}", i );
        }

      }

      iColMax = iCol;

      iRow++;

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        bool Proceed = false;

        if( msDoc.GetIsExternal() )
        {
          continue;
        }
        
        if( msDoc.GetIsRedirect() )
        {
          continue;
        }

        if( msDoc.GetIsHtml() )
        {
          Proceed = true;
        }

        if( Proceed )
        {

          for( ushort HeadingLevel = 1 ; HeadingLevel <= MacroscopePreferencesManager.GetMaxHeadingDepth() ; HeadingLevel++ )
          {

            List<string> HeadingsList = msDoc.GetHeadings( HeadingLevel );

            for( int Order = 0 ; Order < HeadingsList.Count ; Order++ )
            {

              int Occurences = DocCollection.GetStatsHeadingsCount( HeadingLevel: HeadingLevel, Text: HeadingsList[ Order ] );

              iCol = 1;

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

              this.InsertAndFormatContentCell( ws, iRow, iCol, Occurences );

              if( ( Occurences > 1 ) && ( msDoc.GetIsInternal() ) )
              {
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Orange );
              }
              else
              if( msDoc.GetIsInternal() )
              {
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
              }
              else
              {
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
              }

              iCol++;

              this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( ( Order + 1 ).ToString() ) );

              this.InsertAndFormatContentCell( ws, iRow, ( int )( HeadingLevel + iCol ), this.FormatIfMissing( HeadingsList[ Order ] ) );

              iRow++;

            }

          }

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
