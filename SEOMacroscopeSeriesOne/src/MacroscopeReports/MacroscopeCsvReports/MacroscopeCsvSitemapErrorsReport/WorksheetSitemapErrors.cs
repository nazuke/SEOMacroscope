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

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        if ( msDoc.GetIsInternal() && msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.SITEMAPXML ) )
        {

          foreach ( MacroscopeLink Outlink in msDoc.IterateOutlinks() )
          {

            string TargetUrl = Outlink.GetTargetUrl();
            MacroscopeDocument msDocLinked = DocCollection.GetDocumentByUrl( Url: TargetUrl );
            bool InsertRow = false;

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

              this.InsertAndFormatUrlCell( ws, msDoc );

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
