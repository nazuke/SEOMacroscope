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
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvRedirectsReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageRedirectsAudit (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      
      {

        ws.WriteField( "URL" );
        ws.WriteField( "Status Code" );
        ws.WriteField( "Status" );
        ws.WriteField( "Destination URL" );

        ws.NextRecord();
                
      }

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        if( !msDoc.GetIsRedirect() )
        {
          continue;
        }

        string Url = msDoc.GetUrl();
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
        string DestinationURL = msDoc.GetUrlRedirectTo();

        if( string.IsNullOrEmpty( Url ) )
        {
          continue;
        }

        if( string.IsNullOrEmpty( DestinationURL ) )
        {
          continue;
        }

        this.InsertAndFormatUrlCell( ws, Url );

        this.InsertAndFormatContentCell( ws, StatusCode );
          
        this.InsertAndFormatContentCell( ws, Status );
          
        this.InsertAndFormatUrlCell( ws, DestinationURL );

        ws.NextRecord();
                
      }

    }

    /**************************************************************************/

  }

}
