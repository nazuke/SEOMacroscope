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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  public class MacroscopeRedirectChainAnalysis : MacroscopeAnalysis
  {

    /**************************************************************************/

    private MacroscopeHttpTwoClient HttpClient;
    private Dictionary<string, MacroscopeRedirectChainDocStruct> RedirectChainDocCache;

    /**************************************************************************/

    public MacroscopeRedirectChainAnalysis ( MacroscopeHttpTwoClient Client ) : base()
    {
      this.HttpClient = Client;
      this.RedirectChainDocCache = new Dictionary<string, MacroscopeRedirectChainDocStruct>();
    }

    /**************************************************************************/

    public async Task<List<MacroscopeRedirectChainDocStruct>> AnalyzeRedirectChains (
        HttpStatusCode StatusCode,
        string StartUrl,
        string RedirectUrl
      )
    {

      List<MacroscopeRedirectChainDocStruct> RedirectChain = new List<MacroscopeRedirectChainDocStruct>();
      int MaxHops = MacroscopePreferencesManager.GetRedirectChainsMaxHops() - 1;
      MacroscopeRedirectChainDocStruct StructStart;
      int IHOP = 0;
      string PrevUrl = null;
      string NextUrl = null;

      StructStart = new MacroscopeRedirectChainDocStruct(
        NewStatusCode: StatusCode,
        NewUrl: StartUrl,
        NewRedirectUrl: RedirectUrl
      );

      RedirectChain.Add( StructStart );

      PrevUrl = StructStart.Url;
      NextUrl = StructStart.RedirectUrl;

      do
      {

        this.DebugMsg( string.Format( "PrevUrl: {0}", PrevUrl ) );
        this.DebugMsg( string.Format( "NextUrl: {0}", NextUrl ) );

        if( !string.IsNullOrEmpty( PrevUrl ) )
        {
          NextUrl = MacroscopeHttpUrlUtils.MakeUrlAbsolute( PrevUrl, NextUrl );
        }

        this.DebugMsg( string.Format( "PrevUrl: {0}", PrevUrl ) );
        this.DebugMsg( string.Format( "NextUrl: {0}", NextUrl ) );

        MacroscopeRedirectChainDocStruct StructNext = await this.Probe( Url: NextUrl );

        RedirectChain.Add( StructNext );

        PrevUrl = StructNext.Url;
        NextUrl = StructNext.RedirectUrl;

        switch( StructNext.StatusCode )
        {
          case HttpStatusCode.Found:
            break;
          case HttpStatusCode.Moved:
            break;
          case HttpStatusCode.SeeOther:
            break;
          case HttpStatusCode.TemporaryRedirect:
            break;
          default:
            IHOP = MaxHops;
            break;
        }

        this.DebugMsg( string.Format( "IHOP: {0}", IHOP ) );

        IHOP++;

      }
      while( IHOP < MaxHops );

      return ( RedirectChain );

    }

    /** Perform Probe *********************************************************/

    private async Task<MacroscopeRedirectChainDocStruct> Probe ( string Url )
    {

      MacroscopeRedirectChainDocStruct RedirectChainDocStruct;

      if( this.RedirectChainDocCache.ContainsKey( Url ) )
      {
        lock( this.RedirectChainDocCache )
        {
        RedirectChainDocStruct = this.RedirectChainDocCache[ Url ];
        }
      }
      else
      {
        RedirectChainDocStruct = new MacroscopeRedirectChainDocStruct();
        try
        {
          RedirectChainDocStruct = await this._ExecuteHeadCheck( Url: Url );
          lock( this.RedirectChainDocCache )
          {
            if( this.RedirectChainDocCache.ContainsKey( Url ) )
            {
              this.RedirectChainDocCache.Remove( Url );
            }
            this.RedirectChainDocCache.Add( Url, RedirectChainDocStruct );
          }
        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "_Probe :: Exception: {0}", ex.Message ) );
        }

      }

      return ( RedirectChainDocStruct );

    }

    /** Execute Head Request **************************************************/

    private async Task<MacroscopeRedirectChainDocStruct> _ExecuteHeadCheck ( string Url )
    {

      MacroscopeHttpTwoClientResponse ClientResponse = null;
      Uri DocUri = null;
      MacroscopeRedirectChainDocStruct RedirectChainDocStruct = new MacroscopeRedirectChainDocStruct();

      try
      {

        DocUri = new Uri( Url );

        ClientResponse = await this.HttpClient.Head(
          DocUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback
         );

      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "_ExecuteHeadCheck :: Exception: {0}", ex.Message ) );
      }

      if( ClientResponse != null )
      {
        RedirectChainDocStruct = this.ProcessResponseHttpHeaders( Url: Url, Response: ClientResponse );
      }

      return ( RedirectChainDocStruct );

    }

    /**************************************************************************/

    private void ConfigureHeadRequestHeadersCallback ( HttpRequestMessage Request )
    {
    }

    /**************************************************************************/

    private void PostProcessRequestHttpHeadersCallback ( HttpRequestMessage Request, HttpRequestHeaders DefaultRequestHeaders )
    {
    }

    /**************************************************************************/

    private MacroscopeRedirectChainDocStruct ProcessResponseHttpHeaders ( string Url, MacroscopeHttpTwoClientResponse Response )
    {

      HttpResponseMessage ResponseMessage = Response.GetResponse();
      HttpResponseHeaders ResponseHeaders = ResponseMessage.Headers;
      HttpContentHeaders ContentHeaders = ResponseMessage.Content.Headers;
      MacroscopeRedirectChainDocStruct RedirectChainDocStruct = new MacroscopeRedirectChainDocStruct();

      /** HTTP Status Code ------------------------------------------------- **/

      RedirectChainDocStruct.StatusCode = Response.GetResponse().StatusCode;

      /** URL ------------------------------------------------- **/

      RedirectChainDocStruct.Url = Url;

      /** Location HTTP Header --------------------------------------------- **/

      try
      {
        Uri HeaderValue = ResponseHeaders.Location;
        if( HeaderValue != null )
        {
          RedirectChainDocStruct.RedirectUrl = HeaderValue.ToString();
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          RedirectChainDocStruct.RedirectUrl = HeaderValues.First().ToString();
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "location", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "location", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "RedirectChainDocStruct.TargetUrl: {0}", RedirectChainDocStruct.RedirectUrl ) );

      /** ------------------------------------------------------------------ **/

      return ( RedirectChainDocStruct );

    }

    /**************************************************************************/

    delegate bool FindHttpResponseHeaderCallback ( IEnumerable<string> HeaderValues );

    private bool FindHttpResponseHeader ( HttpResponseHeaders ResponseHeaders, string HeaderName, FindHttpResponseHeaderCallback Callback )
    {
      bool Success = false;
      foreach( KeyValuePair<string, IEnumerable<string>> ResponseHeader in ResponseHeaders )
      {
        this.DebugMsg( string.Format( "ResponseHeader.key: {0} :: {1}", HeaderName.ToLower(), ResponseHeader.Key.ToLower() ) );
        if( ResponseHeader.Key.ToLower().Equals( HeaderName.ToLower() ) )
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
      foreach( KeyValuePair<string, IEnumerable<string>> ContentHeader in ContentHeaders )
      {
        this.DebugMsg( string.Format( "ContentHeader.key: {0} :: {1}", HeaderName.ToLower(), ContentHeader.Key.ToLower() ) );
        if( ContentHeader.Key.ToLower().Equals( HeaderName.ToLower() ) )
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

  }

}
