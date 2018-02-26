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

  public partial class MacroscopeExcelOverviewReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetOverview (
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

        ws.Cell( iRow, iCol ).Value = "Status Code";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Status";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Redirect";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Robots";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Duration";
        iCol++;
       
        ws.Cell( iRow, iCol ).Value = "Crawled Date";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Server Date";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Modified Date";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Expires Date";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Content-Type";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Locale";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Language";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Canonical";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Page Depth";
        iCol++;     

        ws.Cell( iRow, iCol ).Value = "Links In";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Links Out";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Hyperlinks In";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Hyperlinks Out";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Title";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Title Length";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Description";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Description Length";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Error Condition";

        for( int i = 1 ; i <= iCol ; i++ )
        {
          ws.Cell( iRow, i ).Style.Font.SetBold();
        }

      }

      iColMax = iCol;

      iRow++;

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        iCol = 1;

        this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );
        iCol++;

        this.InsertAndFormatStatusCodeCell( ws, iRow, iCol, msDoc );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetStatusCode().ToString() ) );
        iCol++;

        this.InsertAndFormatRedirectCell( ws, iRow, iCol, msDoc );
        iCol++;

        this.InsertAndFormatRobotsCell( ws, iRow, iCol, msDoc );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.GetDurationInSecondsFormatted() );
        iCol++;

        this.InsertAndFormatDateCell( ws, iRow, iCol, msDoc.GetCrawledDate() );
        iCol++;
        
        this.InsertAndFormatDateCell( ws, iRow, iCol, msDoc.GetDateServer() );
        iCol++;
          
        this.InsertAndFormatDateCell( ws, iRow, iCol, msDoc.GetDateModified() );
        iCol++;

        this.InsertAndFormatDateCell( ws, iRow, iCol, msDoc.GetDateExpires() );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetMimeType() ) );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetLocale() ) );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetIsoLanguageCode() ) );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetCanonical() ) );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.GetDepth().ToString() );
        iCol++;  

        this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.CountInlinks() );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.CountOutlinks() );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.CountHyperlinksIn() );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.CountHyperlinksOut() );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetTitle() ) );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.GetTitleLength().ToString() );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetDescription() ) );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.GetDescriptionLength() );
        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetErrorCondition() ) );

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
