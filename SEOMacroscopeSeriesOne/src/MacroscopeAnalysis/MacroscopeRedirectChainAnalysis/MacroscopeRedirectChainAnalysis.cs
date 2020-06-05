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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  [Serializable()]
  public class MacroscopeRedirectChainAnalysis : MacroscopeAnalysis
  {

    /**************************************************************************/

    private MacroscopeHttpTwoClient HttpClient;
    private Dictionary<string, MacroscopeRedirectChainDocStruct> RedirectChainDocCache;

    /**************************************************************************/

    public MacroscopeRedirectChainAnalysis ( MacroscopeHttpTwoClient Client ) : base()
    {
      this.SuppressDebugMsg = true;
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
      int MaxHops = MacroscopePreferencesManager.GetRedirectChainsMaxHops();
      MacroscopeRedirectChainDocStruct StructStart;
      int IHOP = 0;
      string PrevUrl = null;
      string NextUrl = null;

      try
      {

        try
        {
          StructStart = new MacroscopeRedirectChainDocStruct(
          NewStatusCode: StatusCode,
          NewUrl: StartUrl,
          NewRedirectUrl: RedirectUrl
        );

          RedirectChain.Add( StructStart );

          PrevUrl = StructStart.Url;
          NextUrl = StructStart.RedirectUrl;
        }
        catch( Exception ex )
        {
          this.DebugMsg( ex.Message );
        }

        do
        {

          MacroscopeRedirectChainDocStruct StructNext;

          try
          {

            if( !string.IsNullOrEmpty( PrevUrl ) )
            {
              NextUrl = MacroscopeHttpUrlUtils.MakeUrlAbsolute( PrevUrl, NextUrl );
            }

              StructNext = await this.Probe( Url: NextUrl );

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

          }
          catch( Exception ex )
          {
            this.DebugMsg( ex.Message );
          }

          IHOP++;

        }
        while( IHOP < MaxHops );

      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      return ( RedirectChain );

    }

    /** Perform Probe *********************************************************/

    private async Task<MacroscopeRedirectChainDocStruct> Probe ( string Url )
    {

      MacroscopeRedirectChainDocStruct RedirectChainDocStruct;

      try
      {

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

    }
      catch(Exception ex )
      {
        RedirectChainDocStruct = new MacroscopeRedirectChainDocStruct();



        this.DebugMsg ( ex.Message );
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
        if( ResponseHeader.Key.ToLower().Equals( HeaderName.ToLower() ) )
        {
          IEnumerable<string> HeaderValues = ResponseHeader.Value;
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
        if( ContentHeader.Key.ToLower().Equals( HeaderName.ToLower() ) )
        {
          IEnumerable<string> HeaderValues = ContentHeader.Value;
          Success = Callback( HeaderValues );
          break;
        }
      }
      return ( Success );
    }

    /**************************************************************************/

  }

}
