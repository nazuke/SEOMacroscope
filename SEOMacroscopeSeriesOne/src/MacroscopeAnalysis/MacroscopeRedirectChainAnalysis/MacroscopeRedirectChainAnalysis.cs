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

namespace SEOMacroscope
{

  public class MacroscopeRedirectChainAnalysis : MacroscopeAnalysis
  {

    /**************************************************************************/

    public MacroscopeRedirectChainAnalysis () : base()
    {
    }

    /**************************************************************************/

    public List<List<MacroscopeDocument>> AnalyzeRedirectChains ( MacroscopeDocumentCollection DocCollection )
    {

      List<List<MacroscopeDocument>> RedirectChains = new List<List<MacroscopeDocument>>();
      int MaxHops = MacroscopePreferencesManager.GetRedirectChainsMaxHops() - 1;

      foreach ( MacroscopeDocument msDocStart in DocCollection.IterateDocuments() )
      {

        int IHOP = 1;
        List<MacroscopeDocument> RedirectChain = new List<MacroscopeDocument>();
        MacroscopeDocument msDocNext;

        if ( !msDocStart.GetIsRedirect() )
        {
          continue;
        }

        RedirectChain.Add( msDocStart );

        msDocNext = DocCollection.GetDocument( msDocStart.GetUrlRedirectTo() );

        if( msDocNext == null )
        {
          this.DebugMsg( string.Format( "AnalyzeRedirectChains: {0}", msDocStart.GetUrlRedirectTo() ) );
        }

        while( msDocNext != null )
        {

          if ( IHOP > MaxHops )
          {
            break;
          }

          RedirectChain.Add( msDocNext );

          if ( msDocNext.GetIsRedirect() )
          {

            string RedirectedFromUrl = msDocNext.GetUrl();
            string RedirectedToUrl = msDocNext.GetUrlRedirectTo();

            msDocNext = DocCollection.GetDocument( RedirectedToUrl );

            if( msDocNext == null )
            {
              this.DebugMsg( string.Format( "AnalyzeRedirectChains: {0}", RedirectedToUrl ) );
            }

          }
          else
          {
            break;
          }

          IHOP++;

        }

        RedirectChains.Add( RedirectChain );

      }

      return ( RedirectChains );

    }
    
    /**************************************************************************/

  }

}
