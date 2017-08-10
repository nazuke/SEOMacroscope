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
using System.Threading;
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvDuplicateContent : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageDuplicatePages (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      decimal DocCount = 0;
      decimal DocListCount = 0;
      decimal CountOuter = 0;
      decimal CountInner = 0;

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      Dictionary<string,Boolean> CrossCheckList;

      CrossCheckList = MacroscopeLevenshteinAnalysis.GetCrossCheckList(
        Capacity: DocCollection.CountDocuments()
      );
            
      DocCount = ( decimal )DocCollection.CountDocuments();
            
      {

        ws.WriteField( "Status Code" );
        ws.WriteField( "Status" );
        ws.WriteField( "Origin URL" );
        ws.WriteField( "Distance" );
        ws.WriteField( "Similar URL" );

        ws.NextRecord();
                
      }

      foreach( string UrlLeft in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDocLeft = DocCollection.GetDocument( UrlLeft );
        MacroscopeLevenshteinAnalysis LevenshteinAnalysis = null;

        CountOuter++;
        CountInner = 0;

        if( DocCount > 0 )
        {
          this.ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: -1,
            ProgressLabelMajor: string.Format( "Documents Processed: {0}", CountOuter ),
            MinorPercentage: ( ( decimal )100 / DocCount ) * CountOuter,
            ProgressLabelMinor: UrlLeft,
            SubMinorPercentage: 0,
            ProgressLabelSubMinor: ""
          );
        }

        if( msDocLeft.GetIsExternal() )
        {
          continue;
        }

        if( !msDocLeft.GetIsHtml() )
        {
          continue;
        }

        LevenshteinAnalysis = new MacroscopeLevenshteinAnalysis (
          msDoc: msDocLeft,
          SizeDifference: MacroscopePreferencesManager.GetMaxLevenshteinSizeDifference(),
          Threshold: MacroscopePreferencesManager.GetMaxLevenshteinDistance(),
          CrossCheckList: CrossCheckList,
          IPercentageDone: this
        );

        Dictionary<MacroscopeDocument,int> DocList;

        DocList = LevenshteinAnalysis.AnalyzeDocCollection(
          DocCollection: DocCollection
        );

        DocListCount = ( decimal )DocList.Count;
             
        foreach( MacroscopeDocument msDocDuplicate in DocList.Keys )
        {

          int StatusCode = ( int )msDocLeft.GetStatusCode();
          HttpStatusCode Status = msDocLeft.GetStatusCode();
          string UrlDuplicate = msDocDuplicate.GetUrl();
          int Distance = DocList[ msDocDuplicate ];

          CountInner++;
          
          if( DocCount > 0 )
          {
            this.ProgressForm.UpdatePercentages(
              Title: null,
              Message: null,
              MajorPercentage: -1,
              ProgressLabelMajor: string.Format( "Documents Processed: {0}", CountOuter ),
              MinorPercentage: ( ( decimal )100 / DocCount ) * CountOuter,
              ProgressLabelMinor: UrlLeft,
              SubMinorPercentage: ( ( decimal )100 / DocListCount ) * CountInner,
              ProgressLabelSubMinor: UrlDuplicate
            );
          }

          this.InsertAndFormatStatusCodeCell( ws, StatusCode );
          
          this.InsertAndFormatStatusCodeCell( ws, Status );

          this.InsertAndFormatUrlCell( ws, UrlLeft );
          
          this.InsertAndFormatContentCell( ws, Distance.ToString() );
         
          this.InsertAndFormatUrlCell( ws, UrlDuplicate );
          
          ws.NextRecord();
        
          if( this.ProgressForm.Cancelled() )
          {
            break;
          }
          
        }

        if( this.ProgressForm.Cancelled() )
        {
          break;
        }

        Thread.Yield();

      }

    }

    /**************************************************************************/

  }

}
