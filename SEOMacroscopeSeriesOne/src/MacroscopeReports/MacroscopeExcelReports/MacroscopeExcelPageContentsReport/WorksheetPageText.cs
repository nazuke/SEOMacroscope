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

  public partial class MacroscopeExcelPageContentsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageText (
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

        ws.Cell( iRow, iCol ).Value = "Page Locale";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Page Language";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Detected Language";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Word Count";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Readability Method";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Readability Grade";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Readability Grade Description";

      }

      iColMax = iCol;

      iRow++;

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        bool Proceed = false;

        if ( msDoc.GetIsExternal() )
        {
          continue;
        }

        if ( msDoc.GetIsRedirect() )
        {
          continue;
        }

        switch ( msDoc.GetDocumentType() )
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

        if ( Proceed )
        {

          iCol = 1;

          string PageLocale = msDoc.GetLocale();
          string PageLanguage = msDoc.GetIsoLanguageCode();
          string DetectedLanguage = msDoc.GetDocumentTextLanguage();
          int WordCount = msDoc.GetWordCount();
          string ReadabilityGradeType = MacroscopeAnalyzeReadability.FormatAnalyzeReadabilityMethod( ReadabilityMethod: msDoc.GetReadabilityGradeMethod() );
          string ReadabilityGrade = msDoc.GetReadabilityGrade().ToString( "00.00" );
          string ReadabilityGradeDescription = msDoc.GetReadabilityGradeDescription();

          if ( string.IsNullOrEmpty( PageLocale ) )
          {
            PageLocale = "";
          }

          if ( string.IsNullOrEmpty( PageLanguage ) )
          {
            PageLanguage = "";
          }

          if ( string.IsNullOrEmpty( DetectedLanguage ) )
          {
            DetectedLanguage = "";
          }

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

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( PageLocale ) );

          if ( msDoc.GetIsInternal() )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( PageLanguage ) );

          if ( PageLanguage != DetectedLanguage )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( DetectedLanguage ) );

          if ( PageLanguage != DetectedLanguage )
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, WordCount );

          if ( msDoc.GetIsInternal() )
          {
            if ( WordCount > 0 )
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
            }
            else
            {
              ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
            }
          }
          else
          {
            ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Gray );
          }

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, ReadabilityGradeType );

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, ReadabilityGrade );

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, ReadabilityGradeDescription );

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
