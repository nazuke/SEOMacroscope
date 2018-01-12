/*

  This file is part of SEOMacroscope.

  Copyright 2018 Jason Holland.

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

  public partial class MacroscopeCsvBrokenLinksReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageRedirectedLinks (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      
      {

        ws.WriteField( "Status Code" );
        ws.WriteField( "Status" );
        ws.WriteField( "Origin URL" );
        ws.WriteField( "Destination URL" );

        ws.NextRecord();
                
      }

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        MacroscopeHyperlinksIn HyperlinksIn = DocCollection.GetDocumentHyperlinksIn( Url );
        int StatusCode = ( int )msDoc.GetStatusCode();
        string Status = msDoc.GetStatusCode().ToString();
          
        if(
          ( StatusCode >= 300 )
          && ( StatusCode <= 399 )
          && ( HyperlinksIn != null ) )
        {

          foreach( MacroscopeHyperlinkIn HyperlinkIn in HyperlinksIn.IterateLinks() )
          {

            string OriginUrl = HyperlinkIn.GetSourceUrl();

            if(
              ( OriginUrl != null )
              && ( OriginUrl.Length > 0 ) )
            {

              this.InsertAndFormatContentCell( ws, StatusCode.ToString() );

              this.InsertAndFormatContentCell( ws, Status );

              this.InsertAndFormatUrlCell( ws, OriginUrl );

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
