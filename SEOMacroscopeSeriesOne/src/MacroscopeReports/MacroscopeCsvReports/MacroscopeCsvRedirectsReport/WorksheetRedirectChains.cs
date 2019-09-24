/*

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
      List<List<MacroscopeRedirectChainDocStruct>> RedirectChains = DocCollection.GetMacroscopeRedirectChains();
      int MaxHops = 1;

      foreach ( List<MacroscopeRedirectChainDocStruct> DocList in RedirectChains )
      {
        int iHop = 1;
        foreach ( MacroscopeRedirectChainDocStruct RedirectChainDocStruct in DocList )
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

      foreach ( List<MacroscopeRedirectChainDocStruct> DocList in RedirectChains )
      {
        foreach ( MacroscopeRedirectChainDocStruct RedirectChainDocStruct in DocList )
        {
          string Url = RedirectChainDocStruct.Url;
          string StatusCode = RedirectChainDocStruct.StatusCode.ToString();
          this.InsertAndFormatUrlCell( ws, Url );
          this.InsertAndFormatContentCell( ws, StatusCode );
        }
        ws.NextRecord();
      }

    }

    /**************************************************************************/

  }

}
