/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  /// <summary>
  /// Analyze the HrefLang attributes of an HTML document.
  /// </summary>

  [Serializable()]
  public class MacroscopeHrefLang : MacroscopeAnalysis
  {

    /**************************************************************************/

    MacroscopeJobMaster MsJobMaster;
    string Locale;
    string Url;
    DateTime DateServer;
    DateTime DateModified;
    bool Available;

    /**************************************************************************/

    public MacroscopeHrefLang ( MacroscopeJobMaster JobMaster, string Locale, string Url ) : base()
    {

      this.SuppressDebugMsg = true;

      this.MsJobMaster = JobMaster;
      this.Locale = Locale;
      this.Url = Url;
      this.DateServer = new DateTime();
      this.DateModified = new DateTime();

      if ( MacroscopePreferencesManager.GetCheckHreflangs() )
      {
        this.Check();
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

    public DateTime GetDateServer ()
    {
      return ( this.DateServer );
    }

    /** -------------------------------------------------------------------- **/

    public DateTime GetDateModified ()
    {
      return ( this.DateModified );
    }

    /**************************************************************************/

    public bool IsAvailable ()
    {
      return ( this.Available );
    }

    /**************************************************************************/

    delegate bool FindHttpResponseHeaderCallback ( IEnumerable<string> HeaderValues );

    private bool FindHttpResponseHeader ( HttpResponseHeaders ResponseHeaders, string HeaderName, FindHttpResponseHeaderCallback Callback )
    {
      bool Success = false;
      foreach ( KeyValuePair<string, IEnumerable<string>> ResponseHeader in ResponseHeaders )
      {
        this.DebugMsg( string.Format( "ResponseHeader.key: {0} :: {1}", HeaderName.ToLower(), ResponseHeader.Key.ToLower() ) );
        if ( ResponseHeader.Key.ToLower().Equals( HeaderName.ToLower() ) )
        {
          IEnumerable<string> HeaderValues = ResponseHeader.Value;
          this.DebugMsg( string.Format( "FindHttpRequestHeader: {0} :: {1}", HeaderName, HeaderValues.First() ) );
          Success = Callback( HeaderValues );
          break;
        }
      }
      return ( Success );
    }

    /** -------------------------------------------------------------------- **/

    private bool FindHttpContentHeader ( HttpContentHeaders ContentHeaders, string HeaderName, FindHttpResponseHeaderCallback Callback )
    {
      bool Success = false;
      foreach ( KeyValuePair<string, IEnumerable<string>> ContentHeader in ContentHeaders )
      {
        this.DebugMsg( string.Format( "ContentHeader.key: {0} :: {1}", HeaderName.ToLower(), ContentHeader.Key.ToLower() ) );
        if ( ContentHeader.Key.ToLower().Equals( HeaderName.ToLower() ) )
        {
          IEnumerable<string> HeaderValues = ContentHeader.Value;
          this.DebugMsg( string.Format( "FindHttpContentHeader: {0} :: {1}", HeaderName, HeaderValues.First() ) );
          Success = Callback( HeaderValues );
          break;
        }
      }
      return ( Success );
    }

    /**************************************************************************/

    private void ProcessResponseHttpHeaders ( MacroscopeHttpTwoClientResponse Response )
    {

      HttpResponseMessage ResponseMessage = Response.GetResponse();
      HttpResponseHeaders ResponseHeaders = ResponseMessage.Headers;
      HttpContentHeaders ContentHeaders = ResponseMessage.Content.Headers;

      /** Date HTTP Header ------------------------------------------------- **/
      try
      {
        DateTimeOffset? HeaderValue = ResponseHeaders.Date;
        if ( HeaderValue != null )
        {
          this.DateServer = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValue.ToString() );
        }
      }
      catch ( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.DateServer = new DateTime();
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.DateServer = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValues.First().ToString() );
          return ( true );
        };
        if ( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "date", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "date", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.DateServer: {0}", this.DateServer ) );

      /** Last-Modified HTTP Header ---------------------------------------- **/
      try
      {
        DateTimeOffset? HeaderValue = ContentHeaders.LastModified;
        if ( HeaderValue != null )
        {
          this.DateModified = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValue.ToString() );
        }
      }
      catch ( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.DateModified = new DateTime();
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.DateModified = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValues.First().ToString() );
          return ( true );
        };
        if ( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "last-modified", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "last-modified", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.DateModified: {0}", this.DateModified ) );

      return;
    }

    /** Execute Head Request **************************************************/

    private void ConfigureHeadRequestHeadersCallback ( HttpRequestMessage Request )
    {
      // TODO: Implement authentication here:
      //this.AuthenticateRequest( Request: Request );
    }

    /** -------------------------------------------------------------------- **/

    private void PostProcessRequestHttpHeadersCallback ( HttpRequestMessage Request, HttpRequestHeaders DefaultRequestHeaders )
    {
    }

    /** -------------------------------------------------------------------- **/

    private async void Check ()
    {
      this.Available = await this._Check();
    }

    /** -------------------------------------------------------------------- **/

    private async Task<bool> _Check ()
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
      MacroscopeHttpTwoClient Client = this.MsJobMaster.GetHttpClient();
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

        this.ProcessResponseHttpHeaders( Response: ClientResponse );

      }

      return ( IsAvailableCheck );

    }

    /**************************************************************************/

  }

}
