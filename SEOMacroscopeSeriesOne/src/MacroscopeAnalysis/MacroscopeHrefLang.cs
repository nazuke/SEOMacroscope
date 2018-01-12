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
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  /// <summary>
  /// Analyze the HrefLang attributes of an HTML document.
  /// </summary>

  public class MacroscopeHrefLang : Macroscope
  {

    /**************************************************************************/

    string Locale;
    string Url;
    DateTime DateModified;
    DateTime DateServer;
    bool Available;

    /**************************************************************************/

    public MacroscopeHrefLang ( string Locale, string Url )
    {

      bool CheckHrefLang = MacroscopePreferencesManager.GetCheckHreflangs();

      this.SuppressDebugMsg = true;

      this.Locale = Locale;
      this.Url = Url;

      if ( CheckHrefLang )
      {
        this.Available = await this.Check();
      }
      else
      {
        this.Available = false;
      }

    }

    /**************************************************************************/

    public string GetLocale ()
    {
      return ( this.Locale );
    }

    /**************************************************************************/

    public string GetUrl ()
    {
      return ( this.Url );
    }

    /**************************************************************************/

    public DateTime GetDateModified ()
    {
      return ( this.DateModified );
    }

    /** -------------------------------------------------------------------- **/

    public DateTime GetDateServer ()
    {
      return ( this.DateServer );
    }

    /**************************************************************************/

    public bool IsAvailable ()
    {
      return ( this.Available );
    }

    /**************************************************************************/

    // TODO: Fix this so that it is HTTP/2 compliant
    /*
    private bool Check ()
    {
      
      // TODO: Increase level of detail here. 

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      bool IsAvailableCheck = false;

      try
      {

        req = WebRequest.CreateHttp( this.Url );
        req.Method = "HEAD";
        req.Timeout = 10000;
        req.KeepAlive = false;
        req.Host = MacroscopeUrlUtils.GetHostnameAndPortFromUrl( this.Url );
        req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        
        MacroscopePreferencesManager.EnableHttpProxy( req );

        using( res = ( HttpWebResponse )req.GetResponse() )
        {

          DebugMsg( string.Format( "MacroscopeHrefLang Status: {0}", res.StatusCode ) );

          if( res.StatusCode == HttpStatusCode.OK )
          {

            IsAvailableCheck = true;

            this.ProcessResponseHttpHeaders( req: req, res: res );

          }
          else
          {
            IsAvailableCheck = false;
          }

          res.Close();
        
        }

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "MacroscopeHrefLang UriFormatException: {0}", ex.Message ) );
      }
      catch( WebException ex )
      {
        DebugMsg( string.Format( "MacroscopeHrefLang WebException: {0}", ex.Message ) );
      }

      return( IsAvailableCheck );

    }
    */

    /**************************************************************************/

    private void ProcessResponseHttpHeaders ( HttpWebRequest req, HttpWebResponse res )
    {

      foreach ( string HttpHeaderName in res.Headers )
      {

        if ( HttpHeaderName.ToLower().Equals( "date" ) )
        {
          string DateString = res.GetResponseHeader( HttpHeaderName );
          this.DateServer = MacroscopeDateTools.ParseHttpDate( DateString: DateString );
        }

        if ( HttpHeaderName.ToLower().Equals( "last-modified" ) )
        {
          string DateString = res.GetResponseHeader( HttpHeaderName );
          this.DateModified = MacroscopeDateTools.ParseHttpDate( DateString: DateString );
        }

      }

      if ( this.DateServer.Date == new DateTime().Date )
      {
        this.DateServer = DateTime.UtcNow;
      }

      if ( this.DateModified.Date == new DateTime().Date )
      {
        this.DateModified = this.DateServer;
      }

    }











    /** Execute Head Request **************************************************/

    private void ConfigureHeadRequestHeadersCallback ( HttpRequestMessage Request )
    {
      //this.AuthenticateRequest( Request: Request );
    }

    /** -------------------------------------------------------------------- **/

    private void PostProcessRequestHttpHeadersCallback ( HttpRequestMessage Request )
    {
    }

    /** -------------------------------------------------------------------- **/

    private async Task<bool> Check ()
    {

      bool IsAvailableCheck = false;

      try
      {
        IsAvailableCheck = await this._ExecuteHeadCheck();
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "Check :: Exception: {0}", ex.Message ) );
      }

      return ( IsAvailableCheck );

    }

    /** -------------------------------------------------------------------- **/

    private async Task<bool> _ExecuteHeadCheck ()
    {

      bool IsAvailableCheck = false;
      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse ClientResponse = null;
      Uri DocUri = null;

      try
      {

        DocUri = new Uri( this.Url );

        ClientResponse = await Client.Head(
          DocUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback
         );

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "_ExecuteHeadCheck :: MacroscopeDocumentException: {0}", ex.Message ) );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "_ExecuteHeadCheck :: Exception: {0}", ex.Message ) );
      }

      if ( ClientResponse != null )
      {

        try
        {
          this.DebugMsg( string.Format( "StatusCode: {0}", ClientResponse.GetResponse().StatusCode ) );
          if ( ClientResponse.GetResponse() != null )
          {
            if ( ClientResponse.GetResponse().StatusCode == HttpStatusCode.OK )
            {
              IsAvailableCheck = true;
            }
          }
          else
          {
            throw new MacroscopeDocumentException( "Bad Response in _ExecuteHeadCheck" );
          }
        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "_ExecuteHeadCheck :: Exception: {0}", ex.Message ) );
        }

      }
      else
      {
        // NO-OP
      }

      return ( IsAvailableCheck );

    }

    /**************************************************************************/

  }

}
