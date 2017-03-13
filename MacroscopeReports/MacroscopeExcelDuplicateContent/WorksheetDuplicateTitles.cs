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

  public partial class MacroscopeExcelDuplicateContent : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageDuplicateTitles (
      MacroscopeJobMaster JobMaster,
      XLWorkbook wb,
      string sWorksheetLabel
    )
    {
      
      var ws = wb.Worksheets.Add( sWorksheetLabel );

      int iRow = 1;
      int iCol = 1;
      int iColMax = 1;
      
      decimal Count = 0;
      decimal DocCount = 0;
      
      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      
      DocCount = ( decimal )DocCollection.CountDocuments();

      {

        ws.Cell( iRow, iCol ).Value = "URL";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Occurrences";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Title";

      }

      iColMax = iCol;

      iRow++;

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        Boolean bProcess = false;

        if( DocCount > 0 )
        {
          Count++;
          this.ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: -1,
            ProgressLabelMajor: string.Format( "Documents Processed: {0}", Count ),
            MinorPercentage: ( ( decimal )100 / DocCount ) * Count,
            ProgressLabelMinor: Url,
            SubMinorPercentage: -1,
            ProgressLabelSubMinor: null
          );
        }
        
        if( AllowedHosts.IsInternalUrl( Url: Url ) )
        {
          if( msDoc.GetIsHtml() )
          {
            bProcess = true;
          }
          else
          if( msDoc.GetIsPdf() )
          {
            bProcess = true;
          }
          else
          {
            bProcess = false;
          }
        }
          
        if( bProcess )
        {

          string Title = msDoc.GetTitle();
          int Occurrences = DocCollection.GetStatsTitleCount( Title );
            
          if( Occurrences > 1 )
          {
            
            iCol = 1;

            this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );

            if( !msDoc.GetIsExternal() )
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
            }
            else
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
            }

            iCol++;

            this.InsertAndFormatContentCell( ws, iRow, iCol, Occurrences );

            if( Occurrences > 1 )
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Orange );
            }
            else
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
            }

            iCol++;

            this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Title ) );

            iRow++;
            
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
