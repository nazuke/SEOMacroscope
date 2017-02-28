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

        ws.Cell( iRow, iCol ).Value = "Order";

        for( int i = 1 ; i <= 6 ; i++ )
        {
          iCol++;
          ws.Cell( iRow, iCol ).Value = string.Format( "H{0}", i );
        }

      }

      iColMax = iCol;

      iRow++;

      {

        foreach( string sKey in DocCollection.DocumentKeys() )
        {

          MacroscopeDocument msDoc = DocCollection.GetDocument( sKey );
          Boolean bProcess = false;

          if( msDoc.GetIsExternal() )
          {
            bProcess = false;
          }

          if( msDoc.GetIsHtml() )
          {
            bProcess = true;
          }
          else
          {
            bProcess = false;
          }

          if( bProcess )
          {

            for( ushort iHeadingIndex = 1 ; iHeadingIndex <= MacroscopePreferencesManager.GetMaxHeadingDepth() ; iHeadingIndex++ )
            {

              List<string> lHeadings = msDoc.GetHeadings( iHeadingIndex );

              for( int iCount = 0 ; iCount < lHeadings.Count ; iCount++ )
              {

                iCol = 1;

                this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );

                if( !msDoc.GetIsExternal() )
                {
                  ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Green );
                }
                else
                {
                  ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Gray );
                }

                iCol++;

                this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( ( iCount + 1 ).ToString() ) );

                this.InsertAndFormatContentCell( ws, iRow, ( int )( iHeadingIndex + iCol ), this.FormatIfMissing( lHeadings[ iCount ] ) );

                iRow++;

              }
            
            }

          }

        }

      }

      {
        var rangeData = ws.Range( 1, 1, iRow - 1, iColMax );
        var excelTable = rangeData.CreateTable();
        excelTable.Sort( "URL", XLSortOrder.Ascending, false, true );
      }

    }

    /**************************************************************************/

  }

}
