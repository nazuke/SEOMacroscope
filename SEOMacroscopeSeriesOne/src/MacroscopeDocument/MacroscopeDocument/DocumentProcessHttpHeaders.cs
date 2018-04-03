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
using System.Collections;
using System.Collections.Specialized;
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

    private void PostProcessRequestHttpHeaders ( HttpRequestMessage Request )
    {
      List<string> Lines = new List<string>();
      lock( Request )
      {
        if( Request != null )
        {
          foreach( var HeaderItem in Request.Headers )
          {
            foreach( var HeaderValue in HeaderItem.Value )
            {
              if( !string.IsNullOrEmpty( HeaderValue ) )
              {
                Lines.Add( string.Concat( HeaderItem.Key, ": ", HeaderValue ) );
              }
            }
          }
        }
      }
      lock( this.RawHttpRequestHeaders )
      {
        Lines.Sort();
        this.RawHttpRequestHeaders = string.Join(
          Environment.NewLine,
          string.Join( " ", Request.Method.ToString(), Request.RequestUri.ToString() ),
          string.Join( Environment.NewLine, Lines )
        );
      }
    }

    /**************************************************************************/

    private void ProcessResponseHttpHeaders ( MacroscopeHttpTwoClientResponse Response )
    {

      bool IsRedirectUrl = false;

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
        this.SetIsRedirect();
      }

      /** Raw HTTP Headers ------------------------------------------------- **/

      this.SetHttpResponseStatusLine( Response: Response );

      this.SetHttpResponseHeaders( Response: Response );

      /** Server Information ----------------------------------------------- **/
      /*{
        this.ServerName = ResponseHeaders.Server.First().ToString();
      }*/

      /** PROBE HTTP HEADERS ----------------------------------------------- **/

      /** Server HTTP Header ----------------------------------------------- **/
      try
      {
        HttpHeaderValueCollection<ProductInfoHeaderValue> HeaderValue = ResponseHeaders.Server;
        if( HeaderValue != null )
        {
          if( HeaderValue.FirstOrDefault() != null )
          {
            this.SetServerName( HeaderValue.FirstOrDefault().ToString() );
          }
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.SetServerName( HeaderValues.First().ToString() );
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "server", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "server", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.ServerName: {0}", this.ServerName ) );

      /** Content-Type HTTP Header ----------------------------------------- **/
      try
      {
        MediaTypeHeaderValue HeaderValue = ContentHeaders.ContentType;
        if( HeaderValue != null )
        {
          this.DebugMsg( string.Format( "HeaderValue: {0}", HeaderValue ) );
          this.MimeType = HeaderValue.MediaType;
          if( HeaderValue.CharSet != null )
          {
            this.SetCharacterSet( HeaderValue.CharSet );
            // TODO: Implement character set probing
            this.SetCharacterEncoding( NewEncoding: new UTF8Encoding() );
          }
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "MediaType Exception: {0}", ex.Message ) );
        this.MimeType = MacroscopeConstants.DefaultMimeType;
      }

      this.DebugMsg( string.Format( "this.MimeType: {0}", this.MimeType ) );

      /** Content-Length HTTP Header --------------------------------------- **/
      try
      {
        long? HeaderValue = null;
        if( ContentHeaders.Contains( "Content-Length" ) )
        {
          HeaderValue = ContentHeaders.ContentLength;
        }
        if( HeaderValue != null )
        {
          this.ContentLength = HeaderValue;
        }
        else
        {
          this.ContentLength = 0;
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.SetContentLength( Length: 0 );
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.SetContentLength( Length: long.Parse( HeaderValues.FirstOrDefault() ) );
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "content-length", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "content-length", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.GetContentLength(): {0}", this.GetContentLength() ) );

      /** Content-Encoding HTTP Header ------------------------------------- **/
      try
      {
        ICollection<string> HeaderValue = ContentHeaders.ContentEncoding;
        if( HeaderValue != null )
        {
          this.ContentEncoding = HeaderValue.FirstOrDefault();
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.ContentEncoding = HeaderValues.FirstOrDefault();
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "content-encoding", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "content-encoding", Callback: Callback );
        }
      }

      if( string.IsNullOrEmpty( this.CompressionMethod ) && ( !string.IsNullOrEmpty( this.ContentEncoding ) ) )
      {
        this.IsCompressed = true;
        this.CompressionMethod = this.ContentEncoding;
      }

      this.DebugMsg( string.Format( "this.ContentEncoding: {0}", this.ContentEncoding ) );
      this.DebugMsg( string.Format( "this.CompressionMethod: {0}", this.CompressionMethod ) );

      /** Date HTTP Header ------------------------------------------------- **/
      try
      {
        DateTimeOffset? HeaderValue = ResponseHeaders.Date;
        if( HeaderValue != null )
        {
          this.DateServer = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValue.ToString() );
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.DateServer = new DateTime();
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.DateServer = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValues.First().ToString() );
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "date", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "date", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.DateServer: {0}", this.DateServer ) );

      /** Last-Modified HTTP Header ---------------------------------------- **/
      try
      {
        DateTimeOffset? HeaderValue = ContentHeaders.LastModified;
        if( HeaderValue != null )
        {
          this.DateModified = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValue.ToString() );
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.DateModified = new DateTime();
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.DateModified = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValues.First().ToString() );
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "last-modified", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "last-modified", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.DateModified: {0}", this.DateModified ) );

      /** Expires HTTP Header ---------------------------------------------- **/
      try
      {
        DateTimeOffset? HeaderValue = ContentHeaders.Expires;
        if( HeaderValue != null )
        {
          this.DateExpires = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValue.ToString() );
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.DateExpires = new DateTime();
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.DateExpires = MacroscopeDateTools.ParseHttpDate( DateString: HeaderValues.First().ToString() );
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "expires", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "expires", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.DateExpires: {0}", this.DateExpires ) );

      /** HTST Policy HTTP Header ------------------------------------------ **/
      // https://www.owasp.org/index.php/HTTP_Strict_Transport_Security_Cheat_Sheet
      // Strict-Transport-Security: max-age=31536000; includeSubDomains; preload
      {
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.HypertextStrictTransportPolicy = true;
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "strict-transport-security", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "strict-transport-security", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.HypertextStrictTransportPolicy: {0}", this.HypertextStrictTransportPolicy ) );

      /** Location (Redirect) HTTP Header ---------------------------------- **/
      try
      {
        Uri HeaderValue = ResponseHeaders.Location;
        if( HeaderValue != null )
        {
          this.SetUrlRedirectTo( Url: HeaderValue.ToString() );
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          this.SetUrlRedirectTo( Url: HeaderValues.FirstOrDefault().ToString() );
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "location", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "location", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.GetIsRedirect(): {0}", this.GetIsRedirect() ) );
      this.DebugMsg( string.Format( "this.GetUrlRedirectTo(): {0}", this.GetUrlRedirectTo() ) );

      /** Link HTTP Headers ------------------------------------------------ **/
      {
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          foreach( string HeaderValue in HeaderValues )
          {
            this.DebugMsg( string.Format( "HeaderValue: {0}", HeaderValue ) );
            this.ProcessHttpLinkHeader( HttpLinkHeader: HeaderValue );
          }
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "link", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "link", Callback: Callback );
        }
      }

      /** ETag HTTP Header ------------------------------------------------- **/
      try
      {
        EntityTagHeaderValue HeaderValue = ResponseHeaders.ETag;
        if( HeaderValue != null )
        {
          string ETagValue = HeaderValue.Tag;
          if( !string.IsNullOrEmpty( ETagValue ) )
          {
            this.SetEtag( HeaderValue.Tag );
          }
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        FindHttpResponseHeaderCallback Callback = delegate ( IEnumerable<string> HeaderValues )
        {
          string HeaderValue = HeaderValues.FirstOrDefault();
          if( HeaderValue != null )
          {
            if( !string.IsNullOrEmpty( HeaderValue ) )
            {
              this.SetEtag( HeaderValue );
            }
          }
          return ( true );
        };
        if( !this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "etag", Callback: Callback ) )
        {
          this.FindHttpContentHeader( ContentHeaders: ContentHeaders, HeaderName: "etag", Callback: Callback );
        }
      }

      this.DebugMsg( string.Format( "this.Etag: {0}", this.Etag ) );

      /** WWW-AUTHENTICATE HTTP Header ------------------------------------- **/
      // Reference: http://httpbin.org/basic-auth/user/passwd
      try
      {
        HttpHeaderValueCollection<AuthenticationHeaderValue> HeaderValue = ResponseHeaders.WwwAuthenticate;
        if( HeaderValue != null )
        {
          string Scheme = null;
          string Realm = null;
          foreach( AuthenticationHeaderValue AuthenticationValue in HeaderValue )
          {
            Scheme = AuthenticationValue.Scheme;
            string Parameter = AuthenticationValue.Parameter;
            Match Matched = Regex.Match( Parameter, "^[^\"]+\"([^\"]+)\"" );
            if( Matched.Success )
            {
              Realm = Matched.Groups[ 1 ].Value;
            }
          }
          if( !string.IsNullOrEmpty( Scheme ) && !string.IsNullOrEmpty( Realm ) )
          {
            if( Scheme.ToLower() == "basic" )
            {
              this.SetAuthenticationType( MacroscopeConstants.AuthenticationType.BASIC );
              this.SetAuthenticationRealm( Realm );
            }
            else
            {
              this.SetAuthenticationType( MacroscopeConstants.AuthenticationType.UNSUPPORTED );
            }
          }
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }
      this.DebugMsg( string.Format( "WwwAuthenticate: \"{0}\", Realm: \"{1}\"", this.GetAuthenticationType(), this.GetAuthenticationRealm() ) );

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
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );
        }
        else
        if( reIsCss.IsMatch( this.MimeType ) )
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.CSS );
        }
        else
        if( reIsJavascript.IsMatch( this.MimeType ) )
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.JAVASCRIPT );
        }
        else
        if( reIsImage.IsMatch( this.MimeType ) )
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.IMAGE );
        }
        else
        if( reIsPdf.IsMatch( this.MimeType ) )
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.PDF );
        }
        else
        if( reIsAudio.IsMatch( this.MimeType ) )
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.AUDIO );
        }
        else
        if( reIsVideo.IsMatch( this.MimeType ) )
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.VIDEO );
        }
        else
        if( reIsXml.IsMatch( this.MimeType ) )
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.XML );
        }
        else
        if( reIsText.IsMatch( this.MimeType ) )
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.TEXT );
        }
        else
        {
          this.SetDocumentType( Type: MacroscopeConstants.DocumentType.BINARY );
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
      return ( true );
    };
    this.FindHttpResponseHeader( ResponseHeaders: ResponseHeaders, HeaderName: "content-length", Callback: Callback );
    */

    delegate bool FindHttpResponseHeaderCallback ( IEnumerable<string> HeaderValues );

    private bool FindHttpResponseHeader (
      HttpResponseHeaders ResponseHeaders,
      string HeaderName,
      FindHttpResponseHeaderCallback Callback
    )
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

    private bool FindHttpContentHeader (
      HttpContentHeaders ContentHeaders,
      string HeaderName,
      FindHttpResponseHeaderCallback Callback
    )
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

            LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
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
