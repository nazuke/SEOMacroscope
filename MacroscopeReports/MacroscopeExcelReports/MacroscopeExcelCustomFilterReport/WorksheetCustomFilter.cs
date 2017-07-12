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
using System.Collections.Generic;
using System.Net;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelCustomFilterReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetCustomFilter (
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

      Dictionary<string,int> FilterColsTable = new Dictionary<string,int> ( CustomFilter.GetSize() );
      
      const int FilterColOffset = 4;

      {

        ws.Cell( iRow, iCol ).Value = MacroscopeConstants.Url;
        iCol++;

        ws.Cell( iRow, iCol ).Value = MacroscopeConstants.StatusCode;
        iCol++;

        ws.Cell( iRow, iCol ).Value = MacroscopeConstants.Status;
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = MacroscopeConstants.ContentType;

        for( int Slot = 0 ; Slot < CustomFilter.GetSize() ; Slot++ )
        {

          string FilterPattern = CustomFilter.GetPattern( Slot ).Key;

          iCol++;

          if( FilterColsTable.ContainsKey( FilterPattern ) || string.IsNullOrEmpty( FilterPattern ) )
          {
            FilterColsTable.Add( string.Format( "EMPTY{0}", Slot + 1 ), Slot + FilterColOffset );
            ws.Cell( iRow, iCol ).Value = string.Format( "EMPTY{0}", Slot + 1 );
          }
          else
          {
            FilterColsTable.Add( FilterPattern, Slot + FilterColOffset );
            ws.Cell( iRow, iCol ).Value = FilterPattern;
          }

        }
        
      }

      iColMax = iCol;

      iRow++;

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        string DocUrl = msDoc.GetUrl();
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
        string MimeType = msDoc.GetMimeType();
        
        if( !CustomFilter.CanApplyCustomFiltersToDocument( msDoc: msDoc ) )
        {
          continue;
        }

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

        this.InsertAndFormatStatusCodeCell( ws, iRow, iCol, msDoc );

        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( msDoc.GetStatusCode().ToString() ) );

        iCol++;

        this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( MimeType ) );

        iCol++;

        for( int Slot = 0 ; Slot < this.CustomFilter.GetSize() ; Slot++ )
        {

          string FilterPattern = this.CustomFilter.GetPattern( Slot: Slot ).Key;
          KeyValuePair<string, MacroscopeConstants.TextPresence> Pair = msDoc.GetCustomFilteredItem( Text: FilterPattern );

          if( ( Pair.Key != null ) && ( Pair.Value != MacroscopeConstants.TextPresence.UNDEFINED ) )
          {

            string CustomFilterItemValue = MacroscopeConstants.TextPresenceLabels[ Pair.Value ];

            this.InsertAndFormatContentCell( ws, iRow, iCol, CustomFilterItemValue );

            switch( Pair.Value )
            {
              case MacroscopeConstants.TextPresence.CONTAINS:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
                break;
              case MacroscopeConstants.TextPresence.NOTCONTAINS:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
                break;
              case MacroscopeConstants.TextPresence.MUSTCONTAIN:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
                break;
              case MacroscopeConstants.TextPresence.SHOULDNOTCONTAIN:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
                break;
              default:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
                break;
            }

          }
          else
          {

            this.InsertAndFormatContentCell( ws, iRow, iCol, "" );
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );

          }

          iCol++;

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
