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

  public partial class MacroscopeExcelPageMetadataReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageCombinedTextMetadata (
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

        ws.Cell( iRow, iCol ).Value = "Page Language";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Detected Language";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Author";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Title";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Description";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Keywords";

      }

      iColMax = iCol;

      iRow++;

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        bool Proceed = false;

        if( msDoc.GetIsExternal() )
        {
          continue;
        }

        if( msDoc.GetIsRedirect() )
        {
          continue;
        }

        switch( msDoc.GetDocumentType() )
        {
          case MacroscopeConstants.DocumentType.HTML:
            Proceed = true;
            break;
          case MacroscopeConstants.DocumentType.PDF:
            Proceed = true;
            break;
          default:
            break;
        }

        if( Proceed )
        {

          iCol = 1;

          string PageLanguage = msDoc.GetIsoLanguageCode();
          string DetectedLanguage = msDoc.GetTitleLanguage();
          string Author = msDoc.GetAuthor();
          string Title = msDoc.GetTitle();
          string Description = msDoc.GetDescription();
          string Keywords = msDoc.GetKeywords();

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

          switch( msDoc.GetDocumentType() )
          {
            case MacroscopeConstants.DocumentType.HTML:
              this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( PageLanguage ) );
              break;
            case MacroscopeConstants.DocumentType.PDF:
              this.InsertAndFormatContentCell( ws, iRow, iCol, PageLanguage );
              break;
            default:
              break;
          }

          if( PageLanguage != DetectedLanguage )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( DetectedLanguage ) );

          if( PageLanguage != DetectedLanguage )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Author ) );

          if( Author.Length > 0 )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Title ) );

          if( Title.Length <= 0 )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
            ws.Cell( iRow, iCol ).Value = "MISSING";
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Description ) );

          if( Description.Length <= 0 )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
            ws.Cell( iRow, iCol ).Value = "MISSING";
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Keywords ) );

          if( Keywords.Length <= 0 )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
            ws.Cell( iRow, iCol ).Value = "MISSING";
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
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
