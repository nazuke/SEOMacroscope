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

    /** Execute Head Request **************************************************/

    private void ConfigureHeadRequestHeadersCallback ( HttpRequestMessage Request )
    {
    }

    /** -------------------------------------------------------------------- **/

    private void PostProcessRequestHttpHeadersCallback ( HttpRequestMessage Request )
    {
      this.PostProcessRequestHttpHeaders( Request: Request );
    }

    /** -------------------------------------------------------------------- **/

    private async void ExecuteHeadRequest ()
    {

      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      Uri DocUri = null;
      string ResponseErrorCondition = null;
      Boolean IsAuthenticating = false;

      this.SetProcessInlinks();
      this.SetProcessHyperlinksIn();

      try
      {

        DocUri = new Uri( this.DocUrl );
        Response = await Client.Head( DocUri, this.ConfigureHeadRequestHeadersCallback, this.PostProcessRequestHttpHeadersCallback );

        // TODO: Fix this:
        //IsAuthenticating = this.AuthenticateRequest( req );

        this.CrawledDate = DateTime.UtcNow;

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
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "ExecuteHeadRequest :: Exception: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }

      this.DebugMsg( string.Format( "ResponseErrorCondition: {0}", ResponseErrorCondition ) );

      ;

      if( Response != null )
      {

        this.DebugMsg( string.Format( "StatusCode: {0}", Response.GetResponse().StatusCode ) );

        this.SetErrorCondition( Response.GetResponse().ReasonPhrase );

        foreach( var HeaderItem in Response.GetResponse().Headers )
        {
          this.DebugMsg( string.Format( "RES HEADERS:{0} => {1}", HeaderItem.Key, HeaderItem.Value ) );
        }

        this.ProcessResponseHttpHeaders( Response: Response );

        if( IsAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        if( this.IsRedirect )
        {

          this.IsRedirect = true;

          string Location = Response.GetResponse().Headers.GetValues( "Location" ).ToString();

          if( !string.IsNullOrEmpty( Location ) )
          {

            string LocationUnescaped = Uri.UnescapeDataString( stringToUnescape: Location );

            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LocationUnescaped );

            if( !string.IsNullOrEmpty( LinkUrlAbs ) )
            {

              MacroscopeLink OutLink;

              this.SetUrlRedirectFrom( Url: DocUri.ToString() );

              this.SetUrlRedirectTo( Url: LinkUrlAbs );

              OutLink = this.AddDocumentOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.REDIRECT,
                Follow: true
              );

              OutLink.SetRawTargetUrl( TargetUrl: Location );

            }

          }

        }

      }
      else
      {
        throw new MacroscopeDocumentException( "ExecuteHeadRequest failure" );
      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

      return;

    }





    /*
    private void ExecuteHeadRequestOLD ()
    {

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string OriginalUrl = this.DocUrl;
      string ResponseErrorCondition = null;
      Boolean IsAuthenticating = false;

      this.SetProcessInlinks();
      this.SetProcessHyperlinksIn();

      try
      {

        req = WebRequest.CreateHttp( this.DocUrl );

        req.Method = "HEAD";
        req.Timeout = this.Timeout;
        req.KeepAlive = false;
        req.AllowAutoRedirect = false;
        req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        this.PrepareRequestHttpHeaders( Request: req );

        IsAuthenticating = this.AuthenticateRequest( req );

        MacroscopePreferencesManager.EnableHttpProxy( req );

        this.CrawledDate = DateTime.UtcNow;

        res = (HttpWebResponse) req.GetResponse();

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
        res = (HttpWebResponse) ex.Response;
        ResponseErrorCondition = ex.Status.ToString();
      }

      this.DebugMsg( string.Format( "ResponseErrorCondition: {0}", ResponseErrorCondition ) );

      if( res != null )
      {

        this.DebugMsg( string.Format( "StatusCode: {0}", res.StatusCode ) );

        this.SetErrorCondition( res.StatusDescription );

        foreach( string HttpHeaderKey in res.Headers )
        {
          this.DebugMsg( string.Format( "RES HEADERS: {0} => {1}", HttpHeaderKey, res.GetResponseHeader( HttpHeaderKey ) ) );
        }

        this.ProcessResponseHttpHeaders( req, res );

        if( IsAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        if( this.IsRedirect )
        {

          this.IsRedirect = true;

          string Location = res.GetResponseHeader( "Location" );

          if( !string.IsNullOrEmpty( Location ) )
          {

            Location = Uri.UnescapeDataString( stringToUnescape: Location );

            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: Location );

            if( !string.IsNullOrEmpty( LinkUrlAbs ) )
            {

              MacroscopeLink OutLink;

              this.SetUrlRedirectFrom( Url: OriginalUrl );

              this.SetUrlRedirectTo( Url: LinkUrlAbs );

              OutLink = this.AddDocumentOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.REDIRECT,
                Follow: true
              );

              OutLink.SetRawTargetUrl( res.GetResponseHeader( "Location" ) );

            }

          }

        }

        res.Close();

        res.Dispose();

      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

      this.PostProcessRequestHttpHeaders( Request: req );

    }
    */
    
    /**************************************************************************/

  }

}
