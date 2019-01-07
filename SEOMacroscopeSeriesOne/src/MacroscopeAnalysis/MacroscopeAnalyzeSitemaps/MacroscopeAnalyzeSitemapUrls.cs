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

  [Serializable()]
  public class MacroscopeAnalyzeSitemapUrls : MacroscopeAnalysis
  {

    /**************************************************************************/

    public MacroscopeAnalyzeSitemapUrls () : base()
    {
      this.SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public List<MacroscopeDocumentList> AnalyzeInSitemaps ( MacroscopeDocumentCollection DocCollection )
    {

      Dictionary<string, Dictionary<string, bool>> UrlMap = this.BuildSitemapUrlList( DocCollection: DocCollection );
      MacroscopeDocumentList InSitemapsDocumentList = new MacroscopeDocumentList();
      MacroscopeDocumentList NotInSitemapsDocumentList = new MacroscopeDocumentList();
      List<MacroscopeDocumentList> DocumentLists = new List<MacroscopeDocumentList>( 2 );

      DocumentLists.Add( NotInSitemapsDocumentList );
      DocumentLists.Add( InSitemapsDocumentList );

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        bool InSitemap = false;
        string DocumentNote = null;
        string Url = msDoc.GetUrl();

        if ( msDoc.GetIsExternal() )
        {
          continue;
        }

        if ( !msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
        {
          continue;
        }

        foreach ( string SitemapUrl in UrlMap.Keys )
        {
          if ( UrlMap[ SitemapUrl ].ContainsKey( Url ) )
          {
            InSitemap = true;
            DocumentNote = SitemapUrl;
          }
        }

        if ( InSitemap )
        {
          InSitemapsDocumentList.AddDocument( msDoc: msDoc );
          InSitemapsDocumentList.AddDocumentNote( msDoc: msDoc, Note: DocumentNote );
        }
        else
        {
          NotInSitemapsDocumentList.AddDocument( msDoc: msDoc );
        }

      }

      return ( DocumentLists );

    }

    /**************************************************************************/

    private Dictionary<string, Dictionary<string, bool>> BuildSitemapUrlList ( MacroscopeDocumentCollection DocCollection )
    {

      Dictionary<string, Dictionary<string, bool>> UrlMap = new Dictionary<string, Dictionary<string, bool>>();

      try
      {

        MacroscopeDocumentList SitemapDocumentList = this.FindSitemaps( DocCollection: DocCollection );

        foreach( MacroscopeDocument msDoc in SitemapDocumentList.IterateDocuments() )
        {

          string SitemapUrl = msDoc.GetUrl();

          if( !UrlMap.ContainsKey( SitemapUrl ) )
          {
            UrlMap.Add( SitemapUrl, new Dictionary<string, bool>() );
          }

          foreach( MacroscopeLink Outlink in msDoc.IterateOutlinks() )
          {

            string TargetUrl = Outlink.GetTargetUrl();

            if( !UrlMap[ SitemapUrl ].ContainsKey( TargetUrl ) )
            {
              UrlMap[ SitemapUrl ].Add( TargetUrl, false );
            }

          }

        }

      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "BuildSitemapUrlList: {0}", ex.Message ) );
      }

      return ( UrlMap );

      }

    /**************************************************************************/

    private MacroscopeDocumentList FindSitemaps ( MacroscopeDocumentCollection DocCollection )
    {

      MacroscopeDocumentList SitemapDocumentList = new MacroscopeDocumentList();

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {
        if (
          msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.SITEMAPTEXT )
          || msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.SITEMAPXML ) )
        {
          SitemapDocumentList.AddDocument( msDoc: msDoc );
        }
      }

      return ( SitemapDocumentList );

    }

    /**************************************************************************/

  }

}
