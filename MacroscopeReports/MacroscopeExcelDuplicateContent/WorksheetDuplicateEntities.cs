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
using System.Net;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelDuplicateContent : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageDuplicateEntities (
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

      Dictionary<string,int> DuplicatesList = new Dictionary<string, int> ( DocCollection.CountDocuments() );
      Dictionary<string,MacroscopeDocument> DuplicatesDocList = new Dictionary<string, MacroscopeDocument> ( DocCollection.CountDocuments() );

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        string Checksum = msDoc.GetChecksum();
        
        if( DuplicatesList.ContainsKey( Checksum ) )
        {

          DuplicatesList[ Checksum ] = DuplicatesList[ Checksum ] + 1;

          if( !DuplicatesDocList.ContainsKey( Url ) )
          {
            DuplicatesDocList.Add( Url, msDoc );
          }

        }
        else
        {

          DuplicatesList.Add( Checksum, 1 );

        }

      }

      {

        ws.Cell( iRow, iCol ).Value = "Status Code";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Status";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Count";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Checksum";
        iCol++;   

        ws.Cell( iRow, iCol ).Value = "URL";

      }

      iColMax = iCol;

      iRow++;

      foreach( string Url in DuplicatesDocList.Keys )
      {



        MacroscopeDocument msDoc = DuplicatesDocList[ Url ];
        string Checksum = msDoc.GetChecksum();
        


        

        
        
        
        if( DuplicatesList[ Checksum ] > 1 )
        {

          iCol = 1;

          int StatusCode = ( int )msDoc.GetStatusCode();
          HttpStatusCode Status = msDoc.GetStatusCode();
          int Count = DuplicatesList[ Checksum ];
          
          this.InsertAndFormatContentCell( ws, iRow, iCol, StatusCode.ToString() );
          iCol++;
          
          this.InsertAndFormatContentCell( ws, iRow, iCol, Status.ToString() );
          iCol++;
          
          this.InsertAndFormatContentCell( ws, iRow, iCol, Count.ToString() );
          iCol++;

          this.InsertAndFormatContentCell( ws, iRow, iCol, msDoc.GetChecksum() );
          iCol++;

          this.InsertAndFormatUrlCell( ws, iRow, iCol, Url );

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
