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
using System.Collections.Generic;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelKeywordAnalysisReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetKeywordTerms (
      MacroscopeJobMaster JobMaster,
      XLWorkbook wb,
      string WorksheetLabel,
      Dictionary<string,int> DicTerms
    )
    {
      var ws = wb.Worksheets.Add( WorksheetLabel );
      decimal TermTotal = DicTerms.Count;
      decimal TermCount = 0;
      
      int iRow = 1;
      int iCol = 1;
      int iColMax = 1;

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();

      {

        ws.Cell( iRow, iCol ).Value = "Occurrences";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Term";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "URL";

      }

      iColMax = iCol;

      iRow++;

      foreach( string Term in DicTerms.Keys )
      {

        MacroscopeDocumentList DocumentList = DocCollection.GetDeepKeywordAnalysDocumentList( Term );

        decimal DocTotal = ( decimal )DocumentList.CountDocuments();
        decimal DocCount = 0;
        TermCount++;
        
        if( TermTotal > 0 )
        {
          this.ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: -1,
            ProgressLabelMajor: null,
            MinorPercentage: ( ( decimal )100 / TermTotal ) * TermCount,
            ProgressLabelMinor: "Keywords Processed",
            SubMinorPercentage: -1,
            ProgressLabelSubMinor: null
          );
        }

        foreach( MacroscopeDocument msDoc in DocumentList.IterateDocuments() )
        {

          DocCount++;
        
          if( DocTotal > 0 )
          {
            this.ProgressForm.UpdatePercentages(
              Title: null,
              Message: null,
              MajorPercentage: -1,
              ProgressLabelMajor: null,
              MinorPercentage: -1,
              ProgressLabelMinor: null,
              SubMinorPercentage: ( ( decimal )100 / DocTotal ) * DocCount,
              ProgressLabelSubMinor: "Documents Processed"
            );
          }

          iCol = 1;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( DicTerms[ Term ].ToString() ) );

          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, this.FormatIfMissing( Term ) );

          iCol++;
                    
          this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc.GetUrl() );
                    
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
