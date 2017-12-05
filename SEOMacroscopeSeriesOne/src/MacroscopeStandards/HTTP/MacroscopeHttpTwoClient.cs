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
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SEOMacroscope
{

  public class MacroscopeHttpTwoClient : Macroscope
  {

    // TODO: finish this class

    /**************************************************************************/

    private static HttpClient Client;
    private static WinHttpHandler HttpHandler;

    /**************************************************************************/

    static MacroscopeHttpTwoClient ()
    {

      // https://msdn.microsoft.com/en-us/library/system.net.http.winhttphandler(v=vs.105).aspx

      SuppressStaticDebugMsg = false;

      HttpHandler = new WinHttpHandler();
      HttpHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      HttpHandler.AutomaticRedirection = false;

      MacroscopePreferencesManager.EnableHttpProxy( HttpHandler: HttpHandler );

      Client = new HttpClient( HttpHandler );

      Client.Timeout = new TimeSpan( hours: 0, minutes: 0, seconds: 60 );

    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeHttpTwoClient ()
    {
      this.SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public WinHttpHandler GetHttpHandler ()
    {
      return ( HttpHandler );
    }

    /**************************************************************************/

    public async Task<MacroscopeHttpTwoClientResponse> Head (
      Uri Url,
      Action<HttpRequestMessage> ConfigureCustomRequestHeadersCallback,
      Action<HttpRequestMessage> PostProcessRequestHttpHeadersCallback
    )
    {

      MacroscopeHttpTwoClientResponse ClientResponse = new MacroscopeHttpTwoClientResponse();

      using( HttpRequestMessage Request = new HttpRequestMessage( HttpMethod.Head, Url ) )
      {

        Request.Version = new Version( 2, 0 );

        try
        {
          this.ConfigureDefaultRequestHeaders( Request: Request );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Head: {0}", ex.Message ) );
        }

        try
        {
          ConfigureCustomRequestHeadersCallback( Request );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Head: {0}", ex.Message ) );
        }

        try
        {
          using( HttpResponseMessage Response = await Client.SendAsync( Request ) )
          {

            ClientResponse.SetResponse( RequestResponse: Response );

            foreach( KeyValuePair<string, IEnumerable<string>> Item in Response.Headers )
            {
              foreach( string Value in Item.Value )
              {
                this.DebugMsg( string.Format( "HEAD RESPONSE: {0} => {1}", Item.Key, Value ) );
                ClientResponse.AddConsolidatedHttpHeader( Name: Item.Key, Value: Value );
              }
            }

            using( HttpContent ResponseContent = Response.Content )
            {
              // TODO: add options to get string and/or bytes[] here:
              ClientResponse.SetContentAsString( ResponseContent.ReadAsStringAsync().Result );
              foreach( KeyValuePair<string, IEnumerable<string>> Item in ResponseContent.Headers )
              {
                foreach( string Value in Item.Value )
                {
                  this.DebugMsg( string.Format( "HEAD RESPONSECONTENT: {0} => {1}", Item.Key, Value ) );
                  ClientResponse.AddConsolidatedHttpHeader( Name: Item.Key, Value: Value );
                }
              }
            }

          }
        }
        catch( TimeoutException ex )
        {
          this.DebugMsg( ex.Message );
        }

        try
        {
          PostProcessRequestHttpHeadersCallback( Request );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Get: {0}", ex.Message ) );
        }

      }

      return ( ClientResponse );

    }

    /**************************************************************************/

    public async Task<MacroscopeHttpTwoClientResponse> Get ( Uri Url, Action<HttpRequestMessage> ConfigureCustomRequestHeadersCallback, Action<HttpRequestMessage> PostProcessRequestHttpHeadersCallback )
    {

      MacroscopeHttpTwoClientResponse ClientResponse = new MacroscopeHttpTwoClientResponse();

      using( HttpRequestMessage Request = new HttpRequestMessage( HttpMethod.Get, Url ) )
      {

        Request.Version = new Version( 2, 0 );

        try
        {
          this.ConfigureDefaultRequestHeaders( Request: Request );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Get: {0}", ex.Message ) );
        }

        try
        {
          ConfigureCustomRequestHeadersCallback( Request );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Get: {0}", ex.Message ) );
        }

        try
        {

          using( HttpResponseMessage Response = await Client.SendAsync( Request ) )
          {

            ClientResponse.SetResponse( RequestResponse: Response );

            foreach( KeyValuePair<string, IEnumerable<string>> Item in Response.Headers )
            {
              foreach( string Value in Item.Value )
              {
                this.DebugMsg( string.Format( "HEAD RESPONSE: {0} => {1}", Item.Key, Value ) );
                ClientResponse.AddConsolidatedHttpHeader( Name: Item.Key, Value: Value );
              }
            }

            using( HttpContent ResponseContent = Response.Content )
            {

              // TODO: add options to get string and/or bytes[] here:
              // TODO: there is an encoding bug here:
              ClientResponse.SetContentAsString( ResponseContent.ReadAsStringAsync().Result );

              foreach( KeyValuePair<string, IEnumerable<string>> Item in ResponseContent.Headers )
              {
                foreach( string Value in Item.Value )
                {
                  this.DebugMsg( string.Format( "HEAD RESPONSECONTENT: {0} => {1}", Item.Key, Value ) );
                  ClientResponse.AddConsolidatedHttpHeader( Name: Item.Key, Value: Value );
                }
              }

            }

          }
        }
        catch( TimeoutException ex )
        {
          this.DebugMsg( ex.Message );
        }

        try
        {
          PostProcessRequestHttpHeadersCallback( Request );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Get: {0}", ex.Message ) );
        }

      }

      return ( ClientResponse );

    }

    /**************************************************************************/

    /*
    public async Task<MacroscopeHttpTwoClientResponse> Post ()
    {

      // TODO: Implement this

      MacroscopeHttpTwoClientResponse Response = null;

      return ( Response );

    }
    */

    /**************************************************************************/

    private void ConfigureDefaultRequestHeaders ( HttpRequestMessage Request )
    {
      
      try
      {
        Request.Headers.Host = Request.RequestUri.Host;
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "Get: {0}", ex.Message ) );
      }

      try
      {
        ProductHeaderValue ProductHeaderValueUserAgent = new ProductHeaderValue( this.UserAgent() );
        ProductInfoHeaderValue ProductInfoHeaderValueUserAgent = new ProductInfoHeaderValue( ProductHeaderValueUserAgent );
        Request.Headers.UserAgent.Clear();
        Request.Headers.UserAgent.Add( ProductInfoHeaderValueUserAgent );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        throw ex;
      }

      try
      {
        Request.Headers.Accept.Clear();
        Request.Headers.Accept.Add( new MediaTypeWithQualityHeaderValue( "*/*", 1 ) );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        throw ex;
      }

      try
      {
        Request.Headers.AcceptCharset.Clear();
        Request.Headers.AcceptCharset.Add( new StringWithQualityHeaderValue( "utf-8", 1 ) );
        Request.Headers.AcceptCharset.Add( new StringWithQualityHeaderValue( "us-ascii", 0.9 ) );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        throw ex;
      }

      try
      {
        Request.Headers.AcceptEncoding.Clear();
        Request.Headers.AcceptEncoding.Add( new StringWithQualityHeaderValue( "gzip", 1 ) );
        Request.Headers.AcceptEncoding.Add( new StringWithQualityHeaderValue( "deflate", 0.9 ) );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        throw ex;
      }


    }

    /**************************************************************************/

  }

}
