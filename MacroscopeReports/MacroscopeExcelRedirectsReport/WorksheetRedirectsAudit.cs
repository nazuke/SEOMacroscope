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

  public partial class MacroscopeExcelRedirectsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageRedirectsAudit (
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
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      
      {

        ws.Cell( iRow, iCol ).Value = "Origin URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Status Code";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Status";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Destination URL";

      }

      iColMax = iCol;

      iRow++;

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );

        if( !msDoc.GetIsRedirect() )
        {
          continue;
        }

        string OriginURL = msDoc.GetUrlRedirectFrom();
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
        string DestinationURL = msDoc.GetUrlRedirectTo();

        if( OriginURL == null )
        {
          continue;
        }

        if( DestinationURL == null )
        {
          continue;
        }

        iCol = 1;

        this.InsertAndFormatUrlCell( ws, iRow, iCol, OriginURL );

        if( AllowedHosts.IsInternalUrl( Url: OriginURL ) )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
        }
        else
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
        }

        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, StatusCode );
          
        iCol++;
          
        this.InsertAndFormatContentCell( ws, iRow, iCol, Status );
          
        iCol++;

        this.InsertAndFormatUrlCell( ws, iRow, iCol, DestinationURL );

        if( AllowedHosts.IsInternalUrl( Url: DestinationURL ) )
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

      return;
      
    }

    /**************************************************************************/

  }

}
