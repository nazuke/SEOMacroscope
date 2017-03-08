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

  public partial class MacroscopeExcelUriReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageDuplicateContent (
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

        ws.Cell( iRow, iCol ).Value = "Distance";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Duplicate URL";

      }

      iColMax = iCol;

      iRow++;

      {
        
        MacroscopeLevenshteinAnalysis LevenshteinAnalysis = null;

        foreach( string UrlLeft in DocCollection.DocumentKeys() )
        {

          MacroscopeDocument msDocLeft = DocCollection.GetDocument( UrlLeft );

          LevenshteinAnalysis = new MacroscopeLevenshteinAnalysis (
            msDoc: msDocLeft,
            Threshold: 64
          );

          foreach( string UrlRight in DocCollection.DocumentKeys() )
          {

            if( UrlLeft == UrlRight )
            {
              continue;
            }
            
            MacroscopeDocument msDocRight = DocCollection.GetDocument( UrlRight );

            Dictionary<MacroscopeDocument,int> DocList = LevenshteinAnalysis.AnalyzeDocCollection(
                                                           DocCollection: DocCollection
                                                         );

            foreach( MacroscopeDocument msDocDuplicate in DocList.Keys )
            {
                      
              iCol = 1;
            
              string UrlDuplicate = msDocDuplicate.GetUrl();  
              int Distance = DocList[ msDocDuplicate ];
              
              this.InsertAndFormatUrlCell( ws, iRow, iCol, UrlLeft );
              
              iCol++;
              
              this.InsertAndFormatContentCell( ws, iRow, iCol, Distance.ToString() );
          
              iCol++;
          
              this.InsertAndFormatUrlCell( ws, iRow, iCol, UrlDuplicate );
              
              iRow++;

            }

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
