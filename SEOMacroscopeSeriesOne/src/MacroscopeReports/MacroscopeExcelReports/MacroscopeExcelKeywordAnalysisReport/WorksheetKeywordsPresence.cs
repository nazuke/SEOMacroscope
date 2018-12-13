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
using System.Collections.Generic;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelKeywordAnalysisReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetKeywordsPresence (
      MacroscopeJobMaster JobMaster,
      XLWorkbook wb,
      string WorksheetLabel,
      MacroscopeDocumentCollection DocCollection
    )
    {

      var ws = wb.Worksheets.Add( WorksheetLabel );
      int iRow = 1;
      int iCol = 1;
      int iColMax = 1;
      decimal DocCount = 0;
      decimal DocTotal = (decimal) DocCollection.CountDocuments();

      {

        ws.Cell( iRow, iCol ).Value = "Presence";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Keyword";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "URL";

      }

      iColMax = iCol;

      iRow++;

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence;

        KeywordPresence = DocCollection.GetKeywordPresenceAnalysis( msDoc: msDoc );

        if( DocCount > 0 )
        {
          this.ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: -1,
            ProgressLabelMajor: null,
            MinorPercentage: ( (decimal) 100 / DocTotal ) * (decimal) DocCount,
            ProgressLabelMinor: "Documents Processed"
          );
        }

        if( KeywordPresence != null )
        {

          foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
          {

            MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS Present = Pair.Value;
            string Keyword = Pair.Key;

            iCol = 1;

            this.InsertAndFormatContentCell( ws, iRow, iCol, Pair.Value.ToString() );

            switch( Pair.Value )
            {
              case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.KEYWORDS_METATAG_EMPTY:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
                break;
              case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MALFORMED_KEYWORDS_METATAG:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
                break;
              case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_TITLE:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
                break;
              case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_TITLE:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
                break;
              case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_DESCRIPTION:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
                break;
              case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_DESCRIPTION:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Orange );
                break;
              case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_BODY:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Green );
                break;
              case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_BODY:
                ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
                break;
              default:
                break;
            }

            iCol++;

            this.InsertAndFormatContentCell( ws, iRow, iCol, Keyword );

            iCol++;

            this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc.GetUrl() );

            iRow++;

          }

        }

        DocCount++;

      }

      {
        var rangeData = ws.Range( 1, 1, iRow - 1, iColMax );
        var excelTable = rangeData.CreateTable();
      }

    }

    /**************************************************************************/

  }

}
