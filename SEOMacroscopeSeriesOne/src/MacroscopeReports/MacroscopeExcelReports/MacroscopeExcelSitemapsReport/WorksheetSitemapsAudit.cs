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

  public partial class MacroscopeExcelSitemapsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetSitemapsAudit (
      MacroscopeJobMaster JobMaster,
      XLWorkbook wb,
      string WorksheetLabel,
      MacroscopeDocumentList DocumentList,
      bool InOut
    )
    {
      var ws = wb.Worksheets.Add( WorksheetLabel );

      int iRow = 1;
      int iCol = 1;
      int iColMax = 1;

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();

      {

        ws.Cell( iRow, iCol ).Value = "URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "In Sitemap";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Status Code";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Is Redirect";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Robots";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Sitemap";

      }

      iColMax = iCol;

      iRow++;

      foreach ( MacroscopeDocument msDoc in DocumentList.IterateDocuments() )
      {

        string Url = null;
        string Robots = null;
        string SitemapUrl = null;
        int StatusCode;

        if ( !msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
        {
          continue;
        }

        if ( msDoc.GetIsExternal() )
        {
          continue;
        }

        Url = msDoc.GetUrl();
        StatusCode = (int) msDoc.GetStatusCode();
        Robots = msDoc.GetAllowedByRobotsAsString();
        SitemapUrl = DocumentList.GetDocumentNote( msDoc: msDoc );
        
        iCol = 1;
        
        this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );

        if ( msDoc.GetIsInternal() )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
        }
        else
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
        }

        iCol++;
        
        this.InsertAndFormatContentCell( ws, iRow, iCol, InOut.ToString() );

        if ( InOut )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
        }
        else
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
        }

        iCol++;
        
        this.InsertAndFormatStatusCodeCell( ws, iRow, iCol, msDoc );

        iCol++;
        
        this.InsertAndFormatRedirectCell( ws, iRow, iCol, msDoc );

        iCol++;
        
        this.InsertAndFormatRobotsCell( ws, iRow, iCol, msDoc );

        iCol++;
        
        this.InsertAndFormatUrlCell( ws, iRow, iCol, SitemapUrl );

        if ( AllowedHosts.IsInternalUrl( Url: SitemapUrl ) )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
        }
        else
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
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
