﻿/*

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
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SEOMacroscope
{

  [Serializable()]
  public class MacroscopeHttpTwoClient : Macroscope
  {

    // TODO: Finish implementing authentication

    // TODO: Finish this class

    /**************************************************************************/

    private static HttpClient Client;
    private static WinHttpHandler HttpHandler;

    private static CookieContainer CookieMonster;

    /**************************************************************************/

    static MacroscopeHttpTwoClient ()
    {

      // https://msdn.microsoft.com/en-us/library/system.net.http.winhttphandler(v=vs.105).aspx

      SuppressStaticDebugMsg = false;

      HttpHandler = new WinHttpHandler();
      HttpHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      HttpHandler.AutomaticRedirection = false;

      HttpHandler.PreAuthenticate = true;

      HttpHandler.SendTimeout = new TimeSpan( hours: 0, minutes: 0, seconds: MacroscopePreferencesManager.GetRequestTimeout() );
      HttpHandler.ReceiveHeadersTimeout = new TimeSpan( hours: 0, minutes: 0, seconds: MacroscopePreferencesManager.GetRequestTimeout() );
      HttpHandler.ReceiveDataTimeout = new TimeSpan( hours: 0, minutes: 0, seconds: MacroscopePreferencesManager.GetRequestTimeout() );

      CookieMonster = new CookieContainer();
      HttpHandler.CookieContainer = CookieMonster;
      HttpHandler.CookieUsePolicy = CookieUsePolicy.UseSpecifiedCookieContainer;

      ConfigureProxy();

      Client = new HttpClient( HttpHandler );

    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeHttpTwoClient ()
    {
      this.SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public static WinHttpHandler GetHttpHandler ()
    {
      return ( HttpHandler );
    }

    /**************************************************************************/

    public static void ClearCookieMonster ()
    {
      CookieMonster = new CookieContainer();
      return;
    }

    /** -------------------------------------------------------------------- **/

    public static CookieContainer GetCookieMonster ()
    {
      return ( CookieMonster );
    }

    /**************************************************************************/

    private static void ConfigureProxy ()
    {
      switch( MacroscopePreferencesManager.GetProxyType() )
      {
        case 0:
          HttpHandler.WindowsProxyUsePolicy = WindowsProxyUsePolicy.DoNotUseProxy;
          break;
        case 1:
          HttpHandler.WindowsProxyUsePolicy = WindowsProxyUsePolicy.UseWinInetProxy;
          break;
        case 2:
          HttpHandler.WindowsProxyUsePolicy = WindowsProxyUsePolicy.UseWinHttpProxy;
          break;
        default:
          HttpHandler.WindowsProxyUsePolicy = WindowsProxyUsePolicy.DoNotUseProxy;
          break;
      }
    }

    /**************************************************************************/

    public async Task<MacroscopeHttpTwoClientResponse> Head (
      Uri Url,
      Action<HttpRequestMessage> PreProcessCustomRequestHeadersCallback,
      Action<HttpRequestMessage, HttpRequestHeaders> PostProcessRequestHttpHeadersCallback
    )
    {

      MacroscopeHttpTwoClientResponse ClientResponse = new MacroscopeHttpTwoClientResponse();
      HttpRequestMessage Request = new HttpRequestMessage( HttpMethod.Head, Url );

      Request.Version = new Version( 2, 0 );

      try
      {
        this.ConfigureDefaultRequestHeaders( Request: Request );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Head: {0}", ex.Message ) );
      }

      try
      {
        PreProcessCustomRequestHeadersCallback( Request );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Head: {0}", ex.Message ) );
      }

      try
      {
        PostProcessRequestHttpHeadersCallback( Request, Client.DefaultRequestHeaders );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Head: {0}", ex.Message ) );
      }

      try
      {

        HttpResponseMessage Response = await Client.SendAsync( Request );

        ClientResponse.SetResponse( RequestResponse: Response );

        foreach( KeyValuePair<string, IEnumerable<string>> Item in Response.Headers )
        {
          foreach( string Value in Item.Value )
          {
            this.DebugMsg( string.Format( "HEAD RESPONSE: {0} => {1}", Item.Key, Value ) );
            ClientResponse.AddConsolidatedHttpHeader( Name: Item.Key, Value: Value );
          }
        }

        HttpContent ResponseContent = Response.Content;
        ClientResponse.SetContentAsBytes( ResponseContent.ReadAsByteArrayAsync().Result );
        foreach( KeyValuePair<string, IEnumerable<string>> Item in ResponseContent.Headers )
        {
          foreach( string Value in Item.Value )
          {
            this.DebugMsg( string.Format( "HEAD RESPONSECONTENT: {0} => {1}", Item.Key, Value ) );
            ClientResponse.AddConsolidatedHttpHeader( Name: Item.Key, Value: Value );
          }
        }

      }
      catch( UriFormatException ex )
      {
        this.DebugMsg( ex.Message );
        throw new MacroscopeDocumentException( ex.Message );
      }
      catch( HttpRequestException ex )
      {
        string message = ex.Message;
        if( ex.InnerException != null )
        {
          message = ex.InnerException.Message;
        }
        this.DebugMsg( message );
        throw new MacroscopeDocumentException( message );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        throw new MacroscopeDocumentException( ex.Message );
      }

      return ( ClientResponse );

    }

    /**************************************************************************/

    public async Task<MacroscopeHttpTwoClientResponse> Get (
      Uri Url,
      Action<HttpRequestMessage> ConfigureCustomRequestHeadersCallback,
      Action<HttpRequestMessage, HttpRequestHeaders> PostProcessRequestHttpHeadersCallback
    )
    {

      MacroscopeHttpTwoClientResponse ClientResponse = new MacroscopeHttpTwoClientResponse();
      HttpRequestMessage Request = new HttpRequestMessage( HttpMethod.Get, Url );

      Request.Version = new Version( 2, 0 );

      try
      {
        this.ConfigureDefaultRequestHeaders( Request: Request );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Get: {0}", ex.Message ) );
      }

      try
      {
        ConfigureCustomRequestHeadersCallback( Request );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Get: {0}", ex.Message ) );
      }


      try
      {
        PostProcessRequestHttpHeadersCallback( Request, Client.DefaultRequestHeaders );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Get: {0}", ex.Message ) );
      }

      try
      {

        HttpResponseMessage Response = await Client.SendAsync( Request );
        HttpContent ResponseContent;

        ClientResponse.SetResponse( RequestResponse: Response );

        foreach( KeyValuePair<string, IEnumerable<string>> Item in Response.Headers )
        {
          foreach( string Value in Item.Value )
          {
            this.DebugMsg( string.Format( "HEAD RESPONSE: {0} => {1}", Item.Key, Value ) );
            ClientResponse.AddConsolidatedHttpHeader( Name: Item.Key, Value: Value );
          }
        }

        ResponseContent = Response.Content;

        ClientResponse.SetContentAsBytes( ResponseContent.ReadAsByteArrayAsync().Result );


        foreach( KeyValuePair<string, IEnumerable<string>> Item in ResponseContent.Headers )
        {
          foreach( string Value in Item.Value )
          {
            this.DebugMsg( string.Format( "HEAD RESPONSECONTENT: {0} => {1}", Item.Key, Value ) );
            ClientResponse.AddConsolidatedHttpHeader( Name: Item.Key, Value: Value );
          }
        }

      }
      catch( UriFormatException ex )
      {
        this.DebugMsg( ex.Message );
        throw new MacroscopeDocumentException( ex.Message );
      }
      catch( HttpRequestException ex )
      {
        string message = ex.Message;
        if( ex.InnerException != null )
        {
          message = ex.InnerException.Message;
        }
        this.DebugMsg( message );
        throw new MacroscopeDocumentException( message );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        throw new MacroscopeDocumentException( ex.Message );
      }

      // TODO: Make exceptions from here log a Remark instead.

      return ( ClientResponse );

    }

    /**************************************************************************/

    // TODO: Implement this:
    /*
    public async Task<MacroscopeHttpTwoClientResponse> Post ()
    {

      ConfigureProxy();

      MacroscopeHttpTwoClientResponse ClientResponse = null;
      return ( ClientResponse );
    }
    */

    /**************************************************************************/

    // https://en.wikipedia.org/wiki/List_of_HTTP_header_fields

    private void ConfigureDefaultRequestHeaders ( HttpRequestMessage Request )
    {

      try
      {
        Request.Headers.Host = Request.RequestUri.Host;
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Get: {0}", ex.Message ) );
      }

      try
      {
        ProductHeaderValue ProductHeaderValueUserAgent = new ProductHeaderValue( this.UserAgentName(), this.UserAgentVersion() );
        ProductInfoHeaderValue ProductInfoHeaderValueUserAgent = new ProductInfoHeaderValue( ProductHeaderValueUserAgent );
        Request.Headers.UserAgent.Clear();
        Request.Headers.UserAgent.Add( ProductInfoHeaderValueUserAgent );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      try
      {
        if( Request.Headers.Accept != null )
        {
          Request.Headers.Accept.Clear();
          Request.Headers.Accept.Add( new MediaTypeWithQualityHeaderValue( "*/*", 1 ) );
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      try
      {
        if( Request.Headers.AcceptCharset != null )
        {
          Request.Headers.AcceptCharset.Clear();
          Request.Headers.AcceptCharset.Add( new StringWithQualityHeaderValue( "utf-8", 1 ) );
          Request.Headers.AcceptCharset.Add( new StringWithQualityHeaderValue( "us-ascii", 0.9 ) );
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      try
      {
        if( Request.Headers.AcceptEncoding != null )
        {
          Request.Headers.AcceptEncoding.Clear();
          Request.Headers.AcceptEncoding.Add( new StringWithQualityHeaderValue( "gzip", 1 ) );
          Request.Headers.AcceptEncoding.Add( new StringWithQualityHeaderValue( "deflate", 0.9 ) );
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        throw;
      }

      try
      {
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control
        if( Request.Headers.CacheControl != null )
        {
          Request.Headers.CacheControl.MaxAge = new TimeSpan( 0, 0, 0 );
          Request.Headers.CacheControl.NoCache = true;
          Request.Headers.CacheControl.MustRevalidate = true;
          Request.Headers.CacheControl.ProxyRevalidate = true;
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      try
      {
        if( Request.Headers.AcceptLanguage != null )
        {
          Request.Headers.AcceptLanguage.Clear();
          Request.Headers.AcceptLanguage.Add( new StringWithQualityHeaderValue( "*", 1 ) );
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      try
      {
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/DNT
        Request.Headers.Add( "DNT", "1" );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      try
      {
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Upgrade-Insecure-Requests
        Request.Headers.Add( "Upgrade-Insecure-Requests", "1" );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

    }

    /**************************************************************************/

  }

}
