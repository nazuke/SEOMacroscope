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
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvDataExtractorReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetCssSelectors (
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

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        string DocUrl = msDoc.GetUrl();
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
        string MimeType = msDoc.GetMimeType();
        
        if( !this.DataExtractorCssSelectors.CanApplyDataExtractorsToDocument( msDoc: msDoc ) )
        {
          continue;
        }        

        foreach( KeyValuePair<string,string> DataExtractedPair in msDoc.IterateDataExtractedCssSelectors() )
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
