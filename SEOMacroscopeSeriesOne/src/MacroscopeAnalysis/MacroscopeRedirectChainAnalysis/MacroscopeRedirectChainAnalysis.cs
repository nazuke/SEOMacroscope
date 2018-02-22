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

      foreach ( MacroscopeDocument msDocStart in DocCollection.IterateDocuments() )
      {

        string UrlStart = msDocStart.GetUrl();
        List<MacroscopeDocument> RedirectChain = new List<MacroscopeDocument>();
        List<string> Circular = new List<string>();
        MacroscopeDocument msDocNext;

        if ( !IsValidDocument( msDoc: msDocStart ) )
        {
          continue;
        }

        RedirectChain.Add( msDocStart );
        Circular.Add( msDocStart.GetUrl() );
        msDocNext = DocCollection.GetDocument( msDocStart.GetUrlRedirectTo() );

        while ( msDocNext != null )
        {

          if ( Circular.Contains( msDocNext.GetUrl() ) )
          {
            break;
          }
          else
          {
            Circular.Add( msDocNext.GetUrl() );
          }

          RedirectChain.Add( msDocNext );

          if ( !IsValidDocument( msDoc: msDocNext ) )
          {
            break;
          }

        }

        RedirectChains.Add( RedirectChain );

      }

      return ( RedirectChains );

    }
    
    /**************************************************************************/

    private bool IsValidDocument ( MacroscopeDocument msDoc )
    {

      bool IsValid = true;

      if ( msDoc.GetIsExternal() )
      {
        IsValid = false;
      }

      if ( !msDoc.GetIsRedirect() )
      {
        IsValid = false;
      }

      return ( IsValid );

    }

    /**************************************************************************/

  }

}
