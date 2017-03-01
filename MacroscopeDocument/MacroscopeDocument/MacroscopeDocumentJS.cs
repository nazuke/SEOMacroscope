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
using System.Text.RegularExpressions;
using System.Net;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private void ProcessJavascriptPage ()
    {

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string sErrorCondition = null;
      Boolean bAuthenticating = false;
      
      try
      {

        req = WebRequest.CreateHttp( this.Url );
        req.Method = "HEAD";
        req.Timeout = this.Timeout;
        req.KeepAlive = false;
        req.UserAgent = this.UserAgent();
        bAuthenticating = this.AuthenticateRequest( req );
                                      
        MacroscopePreferencesManager.EnableHttpProxy( req );
                
        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( WebException ex )
      {

        DebugMsg( string.Format( "ProcessJavascriptPage :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ProcessJavascriptPage :: WebException: {0}", ex.Status ) );
        DebugMsg( string.Format( "ProcessJavascriptPage :: WebException: {0}", ( int )ex.Status ) );

        sErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        this.ProcessHttpHeaders( req, res );

        if( bAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        { // Title
          MatchCollection reMatches = Regex.Matches( this.Url, "/([^/]+)$" );
          string sTitle = null;
          foreach( Match match in reMatches )
          {
            if( match.Groups[ 1 ].Value.Length > 0 )
            {
              sTitle = match.Groups[ 1 ].Value.ToString();
              break;
            }
          }
          if( sTitle != null )
          {
            this.SetTitle( sTitle, MacroscopeConstants.TextProcessingMode.NO_PROCESSING );
            DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
          }
          else
          {
            DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
          }
        }

        res.Close();

      }

      if( sErrorCondition != null )
      {
        this.ProcessErrorCondition( sErrorCondition );
      }

    }

    /**************************************************************************/

  }

}
