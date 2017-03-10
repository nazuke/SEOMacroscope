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

  public partial class MacroscopeExcelUriReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageLinks (
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

        ws.Cell( iRow, iCol ).Value = "Source URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Target URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Link Text";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Title Text";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Alt Text";

      }

      iColMax = iCol;

      iRow++;

      {

        foreach( string Url in DocCollection.DocumentKeys() )
        {

          MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
          MacroscopeHyperlinksOut HyperlinksOut = msDoc.GetHyperlinksOut();

          foreach( MacroscopeHyperlinkOut HyperlinkOut in HyperlinksOut.IterateLinks() )
          {

            string HyperlinkOutUrl = HyperlinkOut.GetUrlTarget();
            string LinkText = HyperlinkOut.GetLinkText();    
            string LinkTitle = HyperlinkOut.GetLinkTitle();      
            string AltText = HyperlinkOut.GetAltText();       

            if( HyperlinkOutUrl == null )
            {
              HyperlinkOutUrl = "";
            }

            iCol = 1;

            this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );

            if( AllowedHosts.IsInternalUrl( Url: Url ) )
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
            }
            else
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
            }

            iCol++;

            this.InsertAndFormatUrlCell( ws, iRow, iCol, HyperlinkOutUrl );

            if( ( HyperlinkOutUrl.Length > 0 ) && ( AllowedHosts.IsInternalUrl( Url: HyperlinkOutUrl ) ) )
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
            }
            else
            if( ( HyperlinkOutUrl.Length > 0 ) && ( AllowedHosts.IsExternalUrl( Url: HyperlinkOutUrl ) ) )
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
            }
            else
            {
              this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( HyperlinkOutUrl ) );
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
            }

            iCol++;

            this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( LinkText ) );

            iCol++;
            this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( LinkTitle ) );

            iCol++;

            this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( AltText ) );

            iRow++;

          }

        }

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
