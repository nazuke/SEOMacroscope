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

    MacroscopeDocumentCollection DocumentCollection;

    /**************************************************************************/

    public MacroscopeRedirectChainAnalysis ( MacroscopeDocumentCollection DocCollection ) : base()
    {
      this.DocumentCollection = DocCollection;
    }

    /**************************************************************************/

    public async Task<List<MacroscopeRedirectChainDocStruct>> AnalyzeRedirectChains ( MacroscopeDocument msDocStart )
    {

      List<MacroscopeRedirectChainDocStruct> RedirectChain = new List<MacroscopeRedirectChainDocStruct>();
      int MaxHops = MacroscopePreferencesManager.GetRedirectChainsMaxHops() - 1;

      if( msDocStart.GetIsRedirect() )
      {

        MacroscopeRedirectChainDocStruct StructStart;
        HttpStatusCode StatusCode = msDocStart.GetStatusCode();
        string Url = msDocStart.GetUrl();
        string RedirectUrl = msDocStart.GetUrlRedirectTo();
        int IHOP = 1;

        StructStart = new MacroscopeRedirectChainDocStruct(
          NewStatusCode: StatusCode,
          NewUrl: Url,
          NewRedirectUrl: RedirectUrl
        );

        RedirectChain.Add( StructStart );

        do
        {

          MacroscopeRedirectChainDocStruct StructNext = await this.Probe( Url: RedirectUrl );

          RedirectChain.Add( StructNext );

          switch( StructNext.StatusCode )
          {
            case HttpStatusCode.Moved:
              Url = RedirectUrl;
              RedirectUrl = StructNext.RedirectUrl;
              break;
            case HttpStatusCode.Redirect:
              Url = RedirectUrl;
              RedirectUrl = StructNext.RedirectUrl;
              break;
            case HttpStatusCode.SeeOther:
              Url = RedirectUrl;
              RedirectUrl = StructNext.RedirectUrl;
              break;
            case HttpStatusCode.TemporaryRedirect:
              Url = RedirectUrl;
              RedirectUrl = StructNext.RedirectUrl;
              break;
            default:
              IHOP = MaxHops;
              break;
          }

          this.DebugMsg( string.Format( "AnalyzeRedirectChains: {0}", RedirectUrl ) );

          IHOP++;

        }
        while( IHOP < MaxHops );

      }

      return ( RedirectChain );

    }

    /** Perform Probe *********************************************************/

    private async Task<MacroscopeRedirectChainDocStruct> Probe ( string Url )
    {
      MacroscopeRedirectChainDocStruct RedirectChainDocStruct = new MacroscopeRedirectChainDocStruct();
      try
      {
        RedirectChainDocStruct = await this._ExecuteHeadCheck( Url: Url );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "_Probe :: Exception: {0}", ex.Message ) );
      }
      return ( RedirectChainDocStruct );
    }

    /** Execute Head Request **************************************************/

    private async Task<MacroscopeRedirectChainDocStruct> _ExecuteHeadCheck ( string Url )
    {

      MacroscopeHttpTwoClient Client = this.DocumentCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse ClientResponse = null;
      Uri DocUri = null;
      MacroscopeRedirectChainDocStruct RedirectChainDocStruct = new MacroscopeRedirectChainDocStruct();

      try
      {

        DocUri = new Uri( Url );

        ClientResponse = await Client.Head(
          DocUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback
         );

      }
      catch( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "_ExecuteHeadCheck :: MacroscopeDocumentException: {0}", ex.Message ) );
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

    private void PostProcessRequestHttpHeadersCallback ( HttpRequestMessage Request )
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
