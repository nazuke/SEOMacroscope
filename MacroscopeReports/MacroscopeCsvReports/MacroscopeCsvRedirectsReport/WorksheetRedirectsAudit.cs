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

        ws.WriteField( "Origin URL" );
        ws.WriteField( "Status Code" );
        ws.WriteField( "Status" );
        ws.WriteField( "Destination URL" );

        ws.NextRecord();
                
      }

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url: Url );

        if( !msDoc.GetIsRedirect() )
        {
          continue;
        }

        string OriginURL = msDoc.GetUrlRedirectFrom();
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
        string DestinationURL = msDoc.GetUrlRedirectTo();

        if( string.IsNullOrEmpty( OriginURL ) )
        {
          continue;
        }

        if( string.IsNullOrEmpty( DestinationURL ) )
        {
          continue;
        }

        this.InsertAndFormatUrlCell( ws, OriginURL );

        this.InsertAndFormatContentCell( ws, StatusCode );
          
        this.InsertAndFormatContentCell( ws, Status );
          
        this.InsertAndFormatUrlCell( ws, DestinationURL );

        ws.NextRecord();
                
      }

    }

    /**************************************************************************/

  }

}
