/*

  This file is part of SEOMacroscope.

  Copyright 2018 Jason Holland.

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

  public partial class MacroscopeExcelSitemapsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetSitemapXmlErrors (
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

        ws.Cell( iRow, iCol ).Value = "Sitemap URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Status Code";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Robots";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "URL";

      }

      iColMax = iCol;

      iRow++;

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        if ( msDoc.GetIsInternal() && msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.SITEMAPXML ) )
        {

          foreach ( MacroscopeLink Outlink in msDoc.IterateOutlinks() )
          {

            string TargetUrl = Outlink.GetTargetUrl();
            MacroscopeDocument msDocLinked = DocCollection.GetDocumentByUrl( Url: TargetUrl );
            bool InsertRow = false;

            if ( msDocLinked.GetIsInternal() )
            {
              int StatusCode = (int) msDocLinked.GetStatusCode();
              if ( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
              {
                InsertRow = true;
              }
              if ( !msDocLinked.GetAllowedByRobots() )
              {
                InsertRow = true;
              }
            }

            if ( InsertRow )
            {

              iCol = 1;

              this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );

              if ( AllowedHosts.IsInternalUrl( Url: msDoc.GetUrl() ) )
              {
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
              }
              else
              {
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
              }

              iCol++;

              this.InsertAndFormatStatusCodeCell( ws, iRow, iCol, msDoc );

              iCol++;

              this.InsertAndFormatRobotsCell( ws, iRow, iCol, msDoc );

              iCol++;

              this.InsertAndFormatUrlCell( ws, iRow, iCol, TargetUrl );

              if ( AllowedHosts.IsInternalUrl( Url: TargetUrl ) )
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
