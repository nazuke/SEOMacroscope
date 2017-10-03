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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HttpTwo;

namespace SEOMacroscope
{

  public class MacroscopeHttpProtocolProbe : Macroscope
  {

    /**************************************************************************/

    public enum HttpProtocolVersion
    {
      HTTP_UNKNOWN = 0,
      HTTP_ONE_POINT_ONE = 1,
      HTTP_TWO = 2,
      HTTP_THREE = 3
    }

    /**************************************************************************/

    private Dictionary<string, HttpProtocolVersion> Cache;

    /**************************************************************************/

    public MacroscopeHttpProtocolProbe ()
    {

      this.SuppressDebugMsg = false;

      this.Cache = new Dictionary<string, HttpProtocolVersion>( 16 );

    }

    /**************************************************************************/

    public async Task<HttpProtocolVersion> Probe (
      string Url
      )
    {

      HttpProtocolVersion HttpProtocolVersionProbed = HttpProtocolVersion.HTTP_UNKNOWN;
      Boolean IsHttpTwo = await this.ProbeHttpTwo( Url: Url );

      if( IsHttpTwo )
      {

        HttpProtocolVersionProbed = HttpProtocolVersion.HTTP_TWO;

      }
      else
      {

        Boolean IsHttpOnePointOne = this.ProbeHttpOnePointOne( Url: Url );

        if( IsHttpOnePointOne )
        {
          HttpProtocolVersionProbed = HttpProtocolVersion.HTTP_ONE_POINT_ONE;
        }
        else
        {
          HttpProtocolVersionProbed = HttpProtocolVersion.HTTP_UNKNOWN;
        }

      }

      this.DebugMsg( string.Format( "HttpProtocolVersionProbed: {0}", HttpProtocolVersionProbed ) );

      return ( HttpProtocolVersionProbed );

    }

    /** HTTP/2 ****************************************************************/

    private async Task<Boolean> ProbeHttpTwo ( string Url )
    {

      Boolean IsHttpTwo = false;
      Uri DocumentUri;
      Http2Client Client;
      NameValueCollection Headers;
      byte[] data;
      Http2Client.Http2Response response = null;

      DocumentUri = new Uri( Url );
      Client = new Http2Client( DocumentUri );
      Headers = new NameValueCollection();

      Client.ConnectionSettings.ConnectionTimeout = new TimeSpan(
        hours: 0,
        minutes: 0,
        seconds: MacroscopePreferencesManager.GetRequestTimeout()
        );

      //Headers.Add( "User-Agent", this.UserAgent() );

      data = null;

      try
      {

        response = await Client.Send( DocumentUri, HttpMethod.Head, Headers, data );

        IsHttpTwo = true;

      }
      catch( TimeoutException ex )
      {
        IsHttpTwo = false;
      }

      /*
      this.DebugMsg( response.Status.ToString() );

      foreach( string Key in response.Headers.Keys )
      {
        this.DebugMsg( string.Format( "{0}: {1}", Key.ToString(), response.Headers[ Key ].ToString() ) );
      }
      */

      return ( IsHttpTwo );

    }

    /** HTTP/1.1 **************************************************************/

    private Boolean ProbeHttpOnePointOne ( string Url )
    {

      Boolean IsHttpOnePointOne = false;

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string ResponseErrorCondition = null;

      try
      {

        req = WebRequest.CreateHttp( Url );

        req.Method = "HEAD";
        req.Timeout = MacroscopePreferencesManager.GetRequestTimeout() * 1000;
        req.KeepAlive = false;
        req.AllowAutoRedirect = false;
        req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        this.PrepareRequestHttpHeaders( Request: req );

        MacroscopePreferencesManager.EnableHttpProxy( req );

        res = ( HttpWebResponse ) req.GetResponse();

      }
      catch( UriFormatException ex )
      {
        this.DebugMsg( string.Format( "ExecuteHeadRequest :: UriFormatException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( TimeoutException ex )
      {
        this.DebugMsg( string.Format( "ExecuteHeadRequest :: TimeoutException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( WebException ex )
      {
        this.DebugMsg( string.Format( "ExecuteHeadRequest :: WebException: {0}", ex.Message ) );
        res = ( HttpWebResponse ) ex.Response;
        ResponseErrorCondition = ex.Status.ToString();
      }

      this.DebugMsg( string.Format( "ResponseErrorCondition: {0}", ResponseErrorCondition ) );

      if( res != null )
      {

        this.DebugMsg( string.Format( "StatusCode: {0}", res.StatusCode ) );

        foreach( string HttpHeaderKey in res.Headers )
        {
          this.DebugMsg( string.Format( "RES HEADERS: {0} => {1}", HttpHeaderKey, res.GetResponseHeader( HttpHeaderKey ) ) );
        }

        IsHttpOnePointOne = true;

        res.Close();

        res.Dispose();

      }

      return ( IsHttpOnePointOne );

    }

    /** HTTP/1.1 Headers ******************************************************/

    // https://en.wikipedia.org/wiki/List_of_HTTP_header_fields

    private void PrepareRequestHttpHeaders ( HttpWebRequest Request )
    {

      Request.Host = Request.RequestUri.Host;

      Request.UserAgent = this.UserAgent();

      Request.Accept = "*/*";

      Request.Headers.Add( "Accept-Charset", "utf-8, us-ascii" );

      Request.Headers.Add( "Accept-Encoding", "gzip, deflate" );

      Request.Headers.Add( "Accept-Language", "*" );

      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/DNT
      Request.Headers.Add( "DNT", "1" );

      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Upgrade-Insecure-Requests
      Request.Headers.Add( "Upgrade-Insecure-Requests", "1" );

      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control
      Request.Headers.Add( "Cache-Control", "max-age=0" );

    }

    /**************************************************************************/

  }

}
