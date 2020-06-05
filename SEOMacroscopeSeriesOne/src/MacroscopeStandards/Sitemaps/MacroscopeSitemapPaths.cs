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

namespace SEOMacroscope
{

  public class MacroscopeSitemapPaths : Macroscope
  {

    /**************************************************************************/

    private List<string> SitemapPaths;

    /**************************************************************************/

    public MacroscopeSitemapPaths ()
    {

      this.SuppressDebugMsg = true;

      this.SitemapPaths = new List<string>();

      this.SitemapPaths.Add( "/sitemap.xml" );

    }

    /**************************************************************************/

    public List<string> GetSitemapPaths ()
    {

      List<string> Copy = new List<string>( this.SitemapPaths.Count );

      lock( this.SitemapPaths )
      {
        foreach( string Path in this.SitemapPaths )
        {
          Copy.Add( Path );
        }
      }

      return ( Copy );

    }

    /**************************************************************************/

    public IEnumerable<string> IterateSitemapPaths ()
    {
      lock( this.SitemapPaths )
      {
        foreach( string Path in this.SitemapPaths )
        {
          yield return Path;
        }
      }
    }

    /** Generate Sitemap URL **************************************************/

    public static string GenerateSitemapUrl ( string Url, string SitemapPath )
    {

      string SitemapUrl = null;
      Uri BaseUri = null;
      string BaseUriPort = "";
      Uri SitemapUri = null;
      string NewSitemapUrl = null;

      try
      {

        BaseUri = new Uri( Url, UriKind.Absolute );

        if( BaseUri.Port > 0 )
        {
          BaseUriPort = string.Format( ":{0}", BaseUri.Port );
        }

        SitemapUri = new Uri(
          string.Format(
            "{0}://{1}{2}{3}",
            BaseUri.Scheme,
            BaseUri.Host,
            BaseUriPort,
            SitemapPath
          ),
          UriKind.Absolute
        );

        NewSitemapUrl = SitemapUri.ToString();

      }
      catch( InvalidOperationException ex )
      {
        DebugMsgStatic( string.Format( "GenerateSitemapUrl: {0}", ex.Message ) );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "GenerateSitemapUrl: {0}", ex.Message ) );
      }

      if( !string.IsNullOrEmpty( NewSitemapUrl ) )
      {
        SitemapUrl = NewSitemapUrl;
      }

      return ( SitemapUrl );

    }

    /**************************************************************************/

  }

}
