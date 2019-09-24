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
using System.Collections.Generic;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelContactDetailsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetTelephoneNumbers (
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

        ws.Cell( iRow, iCol ).Value = "Telephone Number";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "URL";
        
      }

      iColMax = iCol;

      iRow++;

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        if ( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
        {

          Dictionary<string,string> TelephoneNumbers = msDoc.GetTelephoneNumbers();

          foreach( string TelephoneNumber in TelephoneNumbers.Keys )
          {

            iCol = 1;

            this.InsertAndFormatContentCell( ws, iRow, iCol, TelephoneNumber );
                                    
            iCol++;
                                    
            this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );
        
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
