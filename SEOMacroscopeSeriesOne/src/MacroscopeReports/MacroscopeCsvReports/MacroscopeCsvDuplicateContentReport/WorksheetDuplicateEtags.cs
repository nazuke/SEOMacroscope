/*

  This file is part of SEOMacroscope.

  Copyright 2020 Jason Holland.

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
using System.Net;
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvDuplicateContentReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageDuplicateEtags (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      decimal CountOuter = 0;
      decimal CountInner = 0;
      decimal DocCount = 0;

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();

      Dictionary<string,int> DuplicatesList = new Dictionary<string, int> ( DocCollection.CountDocuments() );
      Dictionary<string,MacroscopeDocument> DuplicatesDocList = new Dictionary<string, MacroscopeDocument> ( DocCollection.CountDocuments() );

      DocCount = ( decimal )DocCollection.CountDocuments();

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        string Etag = msDoc.GetEtag();

        if( ( Etag != null ) && ( Etag.Length > 0 ) )
        {
          
          if( !DuplicatesDocList.ContainsKey( msDoc.GetUrl() ) )
          {
            DuplicatesDocList.Add( msDoc.GetUrl(), msDoc );
          }

          if( DuplicatesList.ContainsKey( Etag ) )
          {
            DuplicatesList[ Etag ] = DuplicatesList[ Etag ] + 1;
          }
          else
          {
            DuplicatesList.Add( Etag, 1 );
          }
        
        }

      }

      {

        ws.WriteField( "Status Code" );
        ws.WriteField( "Status" );
        ws.WriteField( "Occurrences" );
        ws.WriteField( "ETag" );
        ws.WriteField( "URL" );

        ws.NextRecord();
                
      }

      foreach( string Etag in DuplicatesList.Keys )
      {

        CountOuter++;
        CountInner = 0;
                
        if( DuplicatesList[ Etag ] > 1 )
        {

          foreach( MacroscopeDocument msDoc in  DuplicatesDocList.Values )
          {

            CountInner++;
            
            if( DocCount > 0 )
            {
              this.ProgressForm.UpdatePercentages(
                Title: null,
                Message: null,
                MajorPercentage: -1,
                ProgressLabelMajor: string.Format( "Documents Processed: {0}", CountOuter ),
                MinorPercentage: ( ( decimal )100 / DocCount ) * CountOuter,
                ProgressLabelMinor: Etag,
                SubMinorPercentage: ( ( decimal )100 / DocCount ) * CountInner,
                ProgressLabelSubMinor: msDoc.GetUrl()
              );
            }

            if( msDoc.GetEtag() == Etag )
            {

              int StatusCode = ( int )msDoc.GetStatusCode();
              HttpStatusCode Status = msDoc.GetStatusCode();
              int Occurrences = DuplicatesList[ Etag ];
          
              this.InsertAndFormatStatusCodeCell( ws, StatusCode );
          
              this.InsertAndFormatStatusCodeCell( ws, Status );
          
              this.InsertAndFormatContentCell( ws, Occurrences );

              this.InsertAndFormatContentCell( ws, msDoc.GetEtag() );

              this.InsertAndFormatUrlCell( ws, msDoc );

              ws.NextRecord();
              
            }

          }

        }

      }

    }

    /**************************************************************************/

  }

}
