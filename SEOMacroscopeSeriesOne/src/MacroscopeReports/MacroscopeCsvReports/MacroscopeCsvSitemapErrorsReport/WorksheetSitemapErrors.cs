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

  public partial class MacroscopeCsvSitemapErrorsReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetSitemapErrors (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();

      {

        ws.WriteField( "Sitemap URL" );
        ws.WriteField( "Status Code" );
        ws.WriteField( "Robots" );
        ws.WriteField( "URL" );

        ws.NextRecord();

      }

      foreach ( string SitemapUrl in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url: SitemapUrl );

        if ( msDoc.GetIsInternal() && msDoc.GetIsSitemapXml() )
        {

          foreach ( MacroscopeHyperlinkOut HyperlinkOut in msDoc.IterateHyperlinksOut() )
          {

            string TargetUrl = HyperlinkOut.GetTargetUrl();
            MacroscopeDocument msDocLinked = DocCollection.GetDocument( Url: TargetUrl );
            Boolean InsertRow = false;

            if ( msDocLinked.GetIsInternal() )
            {
              int StatusCode = (int) msDocLinked.GetStatusCode();
              if ( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
              {
                InsertRow = true;
              }
              if ( !msDocLinked.GetAllowedByRobots() )
              {
                InsertRow = true;
              }
            }

            if ( InsertRow )
            {

              this.InsertAndFormatUrlCell( ws, SitemapUrl );

              this.InsertAndFormatStatusCodeCell( ws, msDoc );

              this.InsertAndFormatRobotsCell( ws, msDoc );

              this.InsertAndFormatUrlCell( ws, TargetUrl );

              ws.NextRecord();

            }

          }

        }

      }

    }

  }

  /**************************************************************************/

}
