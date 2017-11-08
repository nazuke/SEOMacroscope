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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDocument.
  /// </summary>

  public partial class MacroscopeDocument : Macroscope
  {

    /** HTTP Headers **********************************************************/

    // https://en.wikipedia.org/wiki/List_of_HTTP_header_fields
    // TODO: Deprecate this:
    private void DEPRECATEDPrepareRequestHttpHeaders ( HttpWebRequest Request )
    {
      /*
      Request.Host = this.GetHostAndPort();

      Request.UserAgent = this.UserAgent();


      Request.CookieContainer = this.DocCollection.GetJobMaster().GetCookieJar();

      Request.Headers.Add( "Accept-Charset", "utf-8, us-ascii" );

      Request.Headers.Add( "Accept-Encoding", "gzip, deflate" );

      Request.Headers.Add( "Accept-Language", "*" );

      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/DNT
      Request.Headers.Add( "DNT", "1" );

      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Upgrade-Insecure-Requests
      Request.Headers.Add( "Upgrade-Insecure-Requests", "1" );

      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control
      Request.Headers.Add( "Cache-Control", "max-age=0" );

      //this.PostProcessRequestHttpHeaders( Request: Request );
      */
    }

    /**************************************************************************/


    private void PostProcessRequestHttpHeaders ( HttpRequestMessage Request )
    {
      string Headers = "";
      if( Request != null )
      {
        foreach( var HeaderItem in Request.Headers )
        {
          Headers = string.Concat( Headers, HeaderItem.Key, ": ", HeaderItem.Value, Environment.NewLine );
        }
        Headers = string.Concat( Headers, Environment.NewLine );
      }
      this.RawHttpRequestHeaders = Headers;
    }

    /**************************************************************************/

    private void ProcessResponseHttpHeaders ( MacroscopeHttpTwoClientResponse Response )
    {

      Boolean IsRedirectUrl = false;

      HttpResponseMessage ResponseMessage = Response.GetResponse();
      HttpResponseHeaders ResponseHeaders = ResponseMessage.Headers;
      HttpContentHeaders ContentHeaders = ResponseMessage.Content.Headers;

      /** Status Code ------------------------------------------------------ **/
      this.SetStatusCode( ResponseMessage.StatusCode );
      this.SetErrorCondition( ResponseMessage.ReasonPhrase );

      try
      {

        switch( this.GetStatusCode() )
        {

          // 200 Range

          case HttpStatusCode.OK:
            IsRedirectUrl = false;
            break;

          // 300 Range

          case HttpStatusCode.Moved:
            this.SetErrorCondition( HttpStatusCode.Moved.ToString() );
            IsRedirectUrl = true;
            break;

          case HttpStatusCode.SeeOther:
            this.SetErrorCondition( HttpStatusCode.SeeOther.ToString() );
            IsRedirectUrl = true;
            break;

          case HttpStatusCode.Redirect:
            this.SetErrorCondition( HttpStatusCode.Redirect.ToString() );
            IsRedirectUrl = true;
            break;

          // 400 Range

          case HttpStatusCode.BadRequest:
            this.SetErrorCondition( HttpStatusCode.BadRequest.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.Unauthorized:
            this.SetErrorCondition( HttpStatusCode.Unauthorized.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.PaymentRequired:
            this.SetErrorCondition( HttpStatusCode.PaymentRequired.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.Forbidden:
            this.SetErrorCondition( HttpStatusCode.Forbidden.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.NotFound:
            this.SetErrorCondition( HttpStatusCode.NotFound.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.MethodNotAllowed:
            this.SetErrorCondition( HttpStatusCode.MethodNotAllowed.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.Gone:
            this.SetErrorCondition( HttpStatusCode.Gone.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.RequestUriTooLong:
            this.SetErrorCondition( HttpStatusCode.RequestUriTooLong.ToString() );
            IsRedirectUrl = false;
            break;

          // Unhandled

          default:
            throw new MacroscopeDocumentException( "Unhandled HttpStatusCode Type" );

        }

      }
      catch( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
      }

      if( IsRedirectUrl )
      {
        this.IsRedirect = true;
      }

      /** Raw HTTP Headers ------------------------------------------------- **/

      this.SetHttpResponseStatusLine( Response: Response );

      this.SetHttpResponseHeaders( Response: Response );

      /** Server Information ----------------------------------------------- **/
      {
        this.ServerName = ResponseHeaders.Server.First().ToString();
      }







      /** Probe HTTP Headers ----------------------------------------------- **/

      {
        MediaTypeHeaderValue HeaderValue = ContentHeaders.ContentType;
        this.DebugMsg( string.Format( "HeaderValue: {0}", HeaderValue ) );
        this.MimeType = HeaderValue.MediaType;
        if( HeaderValue.CharSet != null )
        {
          this.SetCharacterSet( HeaderValue.CharSet );
          // TODO: Implement character set probing
          this.SetCharacterEncoding( NewEncoding: new UTF8Encoding() );
        }
      }


      try
      {
        long? HeaderValue = ContentHeaders.ContentLength;
        this.DebugMsg( string.Format( "HeaderValue: {0}", HeaderValue ) );
        if( HeaderValue != null )
        {
          this.ContentLength = HeaderValue;
        }
      }
      catch( Exception ex )
      {
        this.ContentLength = 0;
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          foreach( string HeaderValue in HeaderValues )
          {
            this.DebugMsg( string.Format( "HeaderValue: {0}", HeaderValue ) );
          }
          this.ContentLength = long.Parse( HeaderValues.First() );
        };
        this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "content-length", Callback: Callback );
      }


      /*
      if( ResponseHeader.Key.ToLower().Equals( "content-length" ) )
      {
        this.ContentLength = long.Parse( ResponseHeader.Value.First() );
      }
      */



      /*
      foreach( KeyValuePair<string, IEnumerable<string>> ResponseHeader in ResponseHeaders )
      {

        this.SuppressDebugMsg = false;

        //this.DebugMsg( string.Format( "HTTP HEADER: {0} :: {1}", ResponseHeader, res.GetResponseHeader( sHeader ) ) );


        foreach( string Value in ResponseHeader.Value )
        {
          this.DebugMsg( string.Format( "ResponseHeader: {0} :: {1}", ResponseHeader.Key, Value ) );
        }






        

        



        if( ResponseHeader.Key.ToLower().Equals( "www-authenticate" ) )
        {

          // EXAMPLE: WWW-Authenticate: Basic realm="Access to the staging site"

          string NewAuthenticationType = "";
          string NewAuthenticationRealm = "";
          string NewAuthenticationValue = ResponseHeader.Value.First();

          MatchCollection matches = Regex.Matches( NewAuthenticationValue, "^\\s*(Basic)\\s+realm=\"([^\"]+)\"", RegexOptions.IgnoreCase );

          this.DebugMsg( string.Format( "www-authenticate: \"{0}\"", NewAuthenticationValue ) );

          foreach( Match match in matches )
          {
            NewAuthenticationType = match.Groups[ 1 ].Value;
            NewAuthenticationRealm = match.Groups[ 2 ].Value;
          }

          this.DebugMsg( string.Format( "www-authenticate: \"{0}\" :: \"{1}\"", NewAuthenticationType, NewAuthenticationRealm ) );

          if( NewAuthenticationType.ToLower() == "basic" )
          {
            this.SetAuthenticationType( MacroscopeConstants.AuthenticationType.BASIC );
          }
          else
          {
            this.SetAuthenticationType( MacroscopeConstants.AuthenticationType.UNSUPPORTED );
          }

          this.SetAuthenticationRealm( NewAuthenticationRealm );

        }

        if( ResponseHeader.Key.ToLower().Equals( "date" ) )
        {
          string DateString = ResponseHeader.Value.First();
          this.DateServer = MacroscopeDateTools.ParseHttpDate(DateString: DateString);
        }

        if( ResponseHeader.Key.ToLower().Equals( "last-modified" ) )
        {
          string DateString =  ResponseHeader.Value.First();
          this.DateModified = MacroscopeDateTools.ParseHttpDate(DateString: DateString);
        }

        if( ResponseHeader.Key.ToLower().Equals( "expires" ) )
        {
          string DateString = ResponseHeader.Value.First();
          this.DateExpires = MacroscopeDateTools.ParseHttpDate(DateString: DateString);
        }

        if( ResponseHeader.Key.ToLower().Equals( "content-encoding" ) )
        {
          if( string.IsNullOrEmpty( this.CompressionMethod ) )
          {
            this.IsCompressed = true;
            this.CompressionMethod = ResponseHeader.Value.First();
          }
        }

        // Process HTST Policy
        // https://www.owasp.org/index.php/HTTP_Strict_Transport_Security_Cheat_Sheet
        // Strict-Transport-Security: max-age=31536000; includeSubDomains; preload
        if( ResponseHeader.Key.ToLower().Equals( "strict-transport-security" ) )
        {
          this.HypertextStrictTransportPolicy = true;
          // TODO: implement includeSubDomains
        }

        // Link HTTP Headers
        if( ResponseHeader.Key.ToLower().Equals( "link" ) )
        {
          this.ProcessHttpLinkHeader( HttpLinkHeader: ResponseHeader.Value.First() );
        }


        // Process Etag
        if( ResponseHeader.Key.ToLower().Equals( "etag" ) )
        {
          string ETag = ResponseHeader.Value.First();
          if( ( ETag != null ) && ( ETag.Length > 0 ) )
          {
            ETag = Regex.Replace( ETag, "[\"'\\s]+", "", RegexOptions.Singleline );
          }
          else
          {
            ETag = "";
          }
          this.SetEtag( ETag );
        }

      }

      */


      /** Process Dates ---------------------------------------------------- **/
      {
        if( this.DateServer.Date == new DateTime().Date )
        {
          this.DateServer = DateTime.UtcNow;
        }
        if( this.DateModified.Date == new DateTime().Date )
        {
          this.DateModified = this.DateServer;
        }
      }

      /** Process MIME Type ------------------------------------------------ **/
      {

        Regex reIsHtml = new Regex( @"^(text/html|application/xhtml+xml)", RegexOptions.IgnoreCase );
        Regex reIsCss = new Regex( @"^text/css", RegexOptions.IgnoreCase );
        Regex reIsJavascript = new Regex( @"^(application/javascript|text/javascript)", RegexOptions.IgnoreCase );
        Regex reIsImage = new Regex( @"^image/(gif|png|jpeg|bmp|webp|vnd.microsoft.icon|x-icon)", RegexOptions.IgnoreCase );
        Regex reIsPdf = new Regex( @"^application/pdf", RegexOptions.IgnoreCase );
        Regex reIsAudio = new Regex( @"^audio/[a-z0-9]+", RegexOptions.IgnoreCase );
        Regex reIsVideo = new Regex( @"^video/[a-z0-9]+", RegexOptions.IgnoreCase );
        Regex reIsXml = new Regex( @"^(application|text)/(atom\+xml|xml)", RegexOptions.IgnoreCase );
        Regex reIsText = new Regex( @"^(text)/(plain)", RegexOptions.IgnoreCase );

        if( reIsHtml.IsMatch( this.MimeType ) )
        {
          this.SetIsHtml();
        }
        else
        if( reIsCss.IsMatch( this.MimeType ) )
        {
          this.SetIsCss();
        }
        else
        if( reIsJavascript.IsMatch( this.MimeType ) )
        {
          this.SetIsJavascript();
        }
        else
        if( reIsImage.IsMatch( this.MimeType ) )
        {
          this.SetIsImage();
        }
        else
        if( reIsPdf.IsMatch( this.MimeType ) )
        {
          this.SetIsPdf();
        }
        else
        if( reIsAudio.IsMatch( this.MimeType ) )
        {
          this.SetIsAudio();
        }
        else
        if( reIsVideo.IsMatch( this.MimeType ) )
        {
          this.SetIsVideo();
        }
        else
        if( reIsXml.IsMatch( this.MimeType ) )
        {
          this.SetIsXml();
        }
        else
        if( reIsText.IsMatch( this.MimeType ) )
        {
          this.SetIsText();
        }
        else
        {
          this.SetIsBinary();
        }

      }

      return;

    }

    /**************************************************************************/

    /*
FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
{
        foreach( string HeaderValue in HeaderValues )
{
  this.DebugMsg( string.Format( "HeaderValue: {0}", HeaderValue ) );
            }
                      string mt = HeaderValues.First();
this.MimeType = HeaderValues.First();
        };
this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "content-length", Callback: Callback );
*/

    delegate void FindHttpResponseHeaderCallback ( IEnumerable<string> HeaderValues );
    delegate void FindHttpContentHeaderCallback ( IEnumerable<string> HeaderValues );

    private void FindHttpResponseHeader ( HttpResponseHeaders ResponseHeaders, string HeaderName, FindHttpResponseHeaderCallback Callback )
    {

      foreach( KeyValuePair<string, IEnumerable<string>> ResponseHeader in ResponseHeaders )
      {

        this.DebugMsg( string.Format( "ResponseHeader.key: {0} :: {1}", HeaderName.ToLower(), ResponseHeader.Key.ToLower() ) );



        if( ResponseHeader.Key.ToLower().Equals( HeaderName.ToLower() ) )
        {
          IEnumerable<string> HeaderValues = ResponseHeader.Value;
          this.DebugMsg( string.Format( "FindHttpRequestHeader: {0} :: {1}", HeaderName, HeaderValues.First() ) );
          Callback( HeaderValues );
          break;
        }
      }

      return;

    }



    private void FindHttpContentHeader ( HttpContentHeaders ContentHeaders, string HeaderName, FindHttpContentHeaderCallback Callback )
    {

      foreach( KeyValuePair<string, IEnumerable<string>> ContentHeader in ContentHeaders )
      {

        this.DebugMsg( string.Format( "ContentHeader.key: {0} :: {1}", HeaderName.ToLower(), ContentHeader.Key.ToLower() ) );



        if( ContentHeader.Key.ToLower().Equals( HeaderName.ToLower() ) )
        {
          IEnumerable<string> HeaderValues = ContentHeader.Value;
          this.DebugMsg( string.Format( "FindHttpContentHeader: {0} :: {1}", HeaderName, HeaderValues.First() ) );
          Callback( HeaderValues );
          break;
        }
      }

      return;

    }













    /**************************************************************************/

    private void ProcessHttpLinkHeader ( string HttpLinkHeader )
    {

      // https://webmasters.googleblog.com/2011/09/pagination-with-relnext-and-relprev.html

      // Link: <http://www.example.com/downloads/white-paper.pdf>; rel="canonical"

      string[] HttpLinkHeaderItems = Regex.Split( HttpLinkHeader, @",\s*" );

      for( int i = 0 ; i < HttpLinkHeaderItems.Length ; i++ )
      {

        string Url = null;
        string Rel = null;
        MatchCollection matches;

        matches = Regex.Matches( HttpLinkHeader, "<([^<>]+)>\\s*;\\srel=\"([^\"]+)\"" );

        foreach( Match match in matches )
        {
          Url = match.Groups[ 1 ].Value;
          Rel = match.Groups[ 2 ].Value;
        }

        if(
          ( !string.IsNullOrEmpty( Rel ) )
          && ( !string.IsNullOrEmpty( Url ) ) )
        {

          string LinkUrl = null;
          string LinkUrlAbs = null;
          MacroscopeConstants.InOutLinkType LinkType = MacroscopeConstants.InOutLinkType.RELATED;

          switch( Rel.ToLower() )
          {
            case @"canonical":
              this.SetCanonical( Url: Url );
              break;
            case @"shortlink":
              this.SetLinkShortLink( Url: Url );
              break;
            case @"first":
              this.SetLinkFirst( Url: Url );
              break;
            case @"prev":
              this.SetLinkPrev( Url: Url );
              break;
            case @"next":
              this.SetLinkNext( Url: Url );
              break;
            case @"last":
              this.SetLinkLast( Url: Url );
              break;
            default:
              this.DebugMsgForced( string.Format( "Link Rel: {0} :: {1}", Rel, Url ) );
              break;
          }

          LinkUrl = Uri.UnescapeDataString( stringToUnescape: Url );

          if( !string.IsNullOrEmpty( LinkUrlAbs ) )
          {

            LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute(
              BaseHref: this.GetBaseHref(),
              BaseUrl: this.DocUrl,
              Url: LinkUrl
            );

            if( !string.IsNullOrEmpty( LinkUrlAbs ) )
            {
              this.AddDocumentOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: LinkType,
                Follow: true
              );
            }

          }

        }

      }

      return;

    }

    /**************************************************************************/

  }

}
