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
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /** Pure Text Out Links ***************************************************/

    private void ProcessPureTextOutlinks ( List<string> TextDoc, MacroscopeConstants.InOutLinkType LinkType )
    {
      foreach ( string Text in TextDoc )
      {
        this.ProcessPureTextOutlinks( TextDoc: Text, LinkType: LinkType );
      }
    }

    /** -------------------------------------------------------------------- **/

    private void ProcessPureTextOutlinks ( string TextDoc, MacroscopeConstants.InOutLinkType LinkType )
    {

      // BUG: Trailing punctuation in the detected URL can cause problems:
      Regex UrlRegex = new Regex(
        @"(https?://[^/]+/[^\s]*)",
        RegexOptions.IgnoreCase
        );

      Match UrlMatch = UrlRegex.Match( TextDoc );

      while ( UrlMatch.Success )
      {

        for ( int i = 0 ; i <= UrlMatch.Groups.Count ; i++ )
        {

          Group CaptureGroups = UrlMatch.Groups[ i ];
          CaptureCollection Captures = CaptureGroups.Captures;
          Capture Captured = null;
          string UrlProcessing = null;
          string UrlCleaned = null;

          if ( Captures.Count <= 0 )
          {
            continue;
          }

          Captured = Captures[ 0 ];
          UrlProcessing = Captured.Value;
          UrlProcessing = UrlProcessing.Trim();

          if ( !string.IsNullOrEmpty( UrlProcessing ) )
          {

            try
            {
              Uri PureTextUri = new Uri( UrlProcessing );
              if ( PureTextUri != null )
              {
                UrlCleaned = UrlProcessing;
              }
            }
            catch ( UriFormatException ex )
            {
              this.DebugMsg( string.Format( "ProcessPureTextOutlinks: {0}", ex.Message ) );
              UrlCleaned = null;
            }
            catch ( Exception ex )
            {
              this.DebugMsg( string.Format( "ProcessPureTextOutlinks: {0}", ex.Message ) );
              UrlCleaned = null;
            }

            if ( UrlCleaned != null )
            {

              MacroscopeLink Outlink;

              Outlink = this.AddDocumentOutlink(
                AbsoluteUrl: UrlCleaned,
                LinkType: LinkType,
                Follow: true
              );

              if ( Outlink != null )
              {
                Outlink.SetRawTargetUrl( TargetUrl: UrlCleaned );
              }

            }

          }

        }

        UrlMatch = UrlMatch.NextMatch();

      }

    }

    /**************************************************************************/

  }

}
