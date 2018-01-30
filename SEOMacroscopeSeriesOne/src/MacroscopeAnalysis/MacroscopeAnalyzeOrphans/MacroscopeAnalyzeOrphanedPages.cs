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

  public class MacroscopeAnalyzeOrphanedPages : MacroscopeAnalysis
  {

    /**************************************************************************/

    public MacroscopeAnalyzeOrphanedPages () : base()
    {
    }

    /**************************************************************************/

    // TODO: Check that this is complete
    public MacroscopeDocumentList AnalyzeOrphanedDocumentsInCollection ( MacroscopeDocumentCollection DocCollection )
    {

      MacroscopeDocumentList OrphanedDocumentList = new MacroscopeDocumentList();

      foreach ( MacroscopeDocument msDocLeft in DocCollection.IterateDocuments() )
      {

        bool IsOrphan = true;
        string UrlLeft = msDocLeft.GetUrl();

        if ( !IsValidDocument( msDoc: msDocLeft ) )
        {
          continue;
        }

        foreach ( MacroscopeDocument msDocRight in DocCollection.IterateDocuments() )
        {

          if ( msDocLeft.GetUrl().Equals( msDocRight.GetUrl() ) )
          {
            continue;
          }

          if ( !this.IsValidDocument( msDoc: msDocRight ) )
          {
            continue;
          }

          foreach ( MacroscopeHyperlinkOut HyperlinkOut in msDocRight.IterateHyperlinksOut() )
          {
            if ( UrlLeft.Equals( HyperlinkOut.GetTargetUrl() ) )
            {
              IsOrphan = false;
            }
            else
            if ( UrlLeft.Equals( HyperlinkOut.GetRawTargetUrl() ) )
            {
              IsOrphan = false;
            }
            if ( IsOrphan )
            {
              break;
            }
          }

          if ( IsOrphan )
          {
            break;
          }

        }

        if ( IsOrphan )
        {
          OrphanedDocumentList.AddDocument( msDoc: msDocLeft );
          //msDocLeft.AddRemark( "This appears to be an orphaned page, not linked to from any other HTML page in this collection." );
          //msDocLeft.AddRemark( "This page appears to only be referenced from one or more sitemaps." );
        }
        else
        {
          // NO-OP
        }

      }

      return ( OrphanedDocumentList );

    }

    /**************************************************************************/

    private bool IsValidDocument ( MacroscopeDocument msDoc )
    {

      bool IsValid = true;
      if ( msDoc.GetIsExternal() )
      {
        IsValid = false;
      }

      if ( !msDoc.GetIsHtml() )
      {
        IsValid = false;
      }

      return ( IsValid );

    }

    /**************************************************************************/

  }

}
