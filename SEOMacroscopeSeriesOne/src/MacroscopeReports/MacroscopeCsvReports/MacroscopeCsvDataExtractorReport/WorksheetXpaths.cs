﻿/*

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
using System.Net;
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvDataExtractorReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetXpaths (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();

      {
        
        ws.WriteField( MacroscopeConstants.Url );
        ws.WriteField( MacroscopeConstants.StatusCode );
        ws.WriteField( MacroscopeConstants.Status );
        ws.WriteField( MacroscopeConstants.ContentType );
        ws.WriteField( "Extracted Label" );
        ws.WriteField( "Extracted Value" );
        
        ws.NextRecord();

      }

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        string DocUrl = msDoc.GetUrl();
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
        string MimeType = msDoc.GetMimeType();
        
        if( !this.DataExtractorXpaths.CanApplyDataExtractorsToDocument( msDoc: msDoc ) )
        {
          continue;
        }        

        foreach( KeyValuePair<string,string> DataExtractedPair in msDoc.IterateDataExtractedXpaths() )
        {

          string ExtractedLabel = DataExtractedPair.Key;
          string ExtractedValue = DataExtractedPair.Value;

          if( 
            string.IsNullOrEmpty( ExtractedLabel )
            || string.IsNullOrEmpty( ExtractedValue ) )
          {
            continue;
          }

          this.InsertAndFormatUrlCell( ws, msDoc );

          this.InsertAndFormatStatusCodeCell( ws, msDoc );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( Status ) );
        
          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( MimeType ) );
        
          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( ExtractedLabel ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( ExtractedValue ) );

          ws.NextRecord();

        }

      }

    }

    /**************************************************************************/

  }

}
