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
using System.Collections.Generic;
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvRedirectsReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageRedirectChains (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      List<List<MacroscopeDocument>> RedirectChains = DocCollection.GetMacroscopeRedirectChains();
      int MaxHops = 1;

      foreach ( List<MacroscopeDocument> DocList in RedirectChains )
      {
        int iHop = 1;
        foreach ( MacroscopeDocument msDoc in DocList )
        {
          iHop++;
        }
        if ( iHop > MaxHops )
        {
          MaxHops = iHop;
        }
      }

      for ( int iHop = 1 ; iHop < MaxHops ; iHop++ )
      {
        ws.WriteField( string.Format( "Hop {0} URL", iHop ) );
        ws.WriteField( string.Format( "Hop {0} Status", iHop ) );
      }

      ws.NextRecord();

      foreach ( List<MacroscopeDocument> DocList in RedirectChains )
      {
        foreach ( MacroscopeDocument msDoc in DocList )
        {
          string Url = msDoc.GetUrl();
          string StatusCode = ( (int) msDoc.GetStatusCode() ).ToString();
          this.InsertAndFormatUrlCell( ws, Url );
          this.InsertAndFormatContentCell( ws, StatusCode );
        }
        ws.NextRecord();
      }

    }

    /**************************************************************************/

  }

}
