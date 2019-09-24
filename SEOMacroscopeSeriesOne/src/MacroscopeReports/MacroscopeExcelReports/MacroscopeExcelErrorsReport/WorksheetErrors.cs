/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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

  public partial class MacroscopeExcelErrorsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetErrors (
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
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      
      {

        ws.Cell( iRow, iCol ).Value = "Status Code";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Status";
        iCol++;
                
        ws.Cell( iRow, iCol ).Value = "URL";

      }

      iColMax = iCol;

      iRow++;

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        MacroscopeHyperlinksIn HyperlinksIn = DocCollection.GetDocumentHyperlinksIn( msDoc.GetUrl() );
        int StatusCode = ( int )msDoc.GetStatusCode();
        string Status = msDoc.GetStatusCode().ToString();
          
        if(
          ( StatusCode >= 400 )
          && ( StatusCode <= 599 ) )
        {

          iCol = 1;

          this.InsertAndFormatContentCell( ws, iRow, iCol, StatusCode.ToString() );
          
          if( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Blue );
          }

          iCol++;
          
          this.InsertAndFormatContentCell( ws, iRow, iCol, Status );

          if( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Blue );
          }
              
          iCol++;

          this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc.GetUrl() );

          if( AllowedHosts.IsInternalUrl( Url: msDoc.GetUrl() ) )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
          }

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
