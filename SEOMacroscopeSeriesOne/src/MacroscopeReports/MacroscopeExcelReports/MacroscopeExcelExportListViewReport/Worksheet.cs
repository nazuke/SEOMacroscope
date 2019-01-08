/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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

  public partial class MacroscopeExcelExportListViewReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetListView (
      MacroscopeJobMaster JobMaster,
      XLWorkbook wb,
      string WorksheetLabel
    )
    {
      
      var ws = wb.Worksheets.Add( WorksheetLabel );

      int iRow = 1;
      int iCol = 0;
      int iColMax = 1;

      for( int i = 0 ; i < this.TargetListView.Columns.Count ; i++ )
      {

        string ColumnName = this.TargetListView.Columns[ i ].Text;

        iCol++;

        ws.Cell( iRow, iCol ).Value = ColumnName;

      }
      
      for( int i = 1 ; i <= iCol ; i++ )
      {
        ws.Cell( iRow, i ).Style.Font.SetBold();
      }

      iColMax = iCol;

      iRow++;

      for( int j = 0 ; j < this.TargetListView.Items.Count ; j++ )
      {

        iCol = 0;

        for( int k = 0 ; k < this.TargetListView.Items[ j ].SubItems.Count ; k++ )
        {

          string CellValue = this.TargetListView.Items[ j ].SubItems[ k ].Text;

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, CellValue );

        }
      
        iRow++;

      }

      {
        var rangeData = ws.Range( 1, 1, iRow - 1, iColMax );
        var excelTable = rangeData.CreateTable();
      }

    }

    /**************************************************************************/

  }

}
