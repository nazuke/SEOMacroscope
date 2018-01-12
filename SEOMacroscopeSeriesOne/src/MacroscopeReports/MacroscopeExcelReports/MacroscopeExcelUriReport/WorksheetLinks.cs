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

  public partial class MacroscopeExcelUriReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageLinks (
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

        ws.Cell( iRow, iCol ).Value = "URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Link Type";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Source URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Target URL";
        iCol++;
       
        ws.Cell( iRow, iCol ).Value = "Follow";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Alt Text";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Raw Source URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Raw Target URL";
        
      }

      iColMax = iCol;

      iRow++;

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );

        foreach( MacroscopeLink Link in msDoc.IterateOutlinks() )
        {

          string LinkType = Link.GetLinkType().ToString();
            
          string SourceUrl = Link.GetSourceUrl();
          string TargetUrl = Link.GetTargetUrl();

          string AltText = Link.GetAltText();
        
          string RawSourceUrl = Link.GetRawSourceUrl();
          string RawTargetUrl = Link.GetRawTargetUrl();

          string DoFollow = "No Follow";

          if( Link.GetDoFollow() )
          {
            DoFollow = "Follow";
          }

          if( string.IsNullOrEmpty( AltText ) )
          {
            AltText = "";
          }

          if( string.IsNullOrEmpty( RawSourceUrl ) )
          {
            RawSourceUrl = "";
          }

          if( string.IsNullOrEmpty( RawTargetUrl ) )
          {
            RawTargetUrl = "";
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

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( LinkType ) );
            
          iCol++;
                        
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( SourceUrl ) );
            
          iCol++;
            
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( TargetUrl ) );
            
          iCol++;
            
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( DoFollow ) );
            
          iCol++;
            
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( AltText ) );
            
          iCol++;
            
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( RawSourceUrl ) );
            
          iCol++;
            
          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( RawTargetUrl ) );

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
