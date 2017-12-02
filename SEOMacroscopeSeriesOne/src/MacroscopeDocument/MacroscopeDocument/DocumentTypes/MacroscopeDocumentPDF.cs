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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private void ConfigurePdfPageRequestHeadersCallback ( HttpRequestMessage Request )
    {
    }

    /** -------------------------------------------------------------------- **/

    private async Task ProcessPdfPage ()
    {
      Stopwatch TimeDuration = new Stopwatch();
      long FinalDuration;
      TimeDuration.Start();
      await this._ProcessPdfPage();
      TimeDuration.Stop();
      FinalDuration = TimeDuration.ElapsedMilliseconds;
      if( FinalDuration > 0 )
      {
        this.Duration = FinalDuration;
      }
      else
      {
        this.Duration = 0;
      }
    }

    /** -------------------------------------------------------------------- **/

    private async Task _ProcessPdfPage ()
    {

      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      Uri DocUri;
      string ResponseErrorCondition = null;
      Boolean Authenticating = false;
      
      try
      {

        DocUri = new Uri( this.DocUrl );
        Response = await Client.Get( DocUri, this.ConfigurePdfPageRequestHeadersCallback, this.PostProcessRequestHttpHeadersCallback );

        // TODO: Fix this:
        //IsAuthenticating = this.AuthenticateRequest( req );

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "ProcessPdfPage :: UriFormatException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "ProcessPdfPage :: Exception: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }

      if( Response != null )
      {

        MacroscopePdfTools pdfTools;

        this.ProcessResponseHttpHeaders( Response: Response );

        if( Authenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        { // Probe Locale
          //this.Locale = "en"; // Implement locale probing
          this.Locale = "x-default"; // Implement locale probing
          this.SetHreflang( HrefLangLocale: this.Locale, Url: this.DocUrl );
        }

        { // Canonical
          this.Canonical = this.DocUrl;
          DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
        }

        { // Get Response Body
          try
          {

            byte [] RawData = Response.GetContentAsBytes();
            this.SetContentLength( Length: RawData.Length );

            pdfTools = new MacroscopePdfTools ( RawData );
						
            if( pdfTools.GetHasError() )
            {
              this.AddRemark( Observation: pdfTools.GetErrorMessage() );
            }
            
            this.SetWasDownloaded( true );

          }
          catch( WebException ex )
          {

            DebugMsg( string.Format( "WebException: {0}", ex.Message ) );

            if( ex.Response != null )
            {
              this.SetStatusCode( ( ( HttpWebResponse )ex.Response ).StatusCode );
            }
            else
            {
              this.SetStatusCode( ( HttpStatusCode )ex.Status );
            }

            pdfTools = null;
            this.SetContentLength( Length: 0 );

          }
          catch( Exception ex )
          {

            DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
            this.SetStatusCode( HttpStatusCode.BadRequest );
            pdfTools = null;
            this.SetContentLength( Length: 0 );

          }

        }

        /** Title ---------------------------------------------------------- **/
        
        {
          if( pdfTools != null )
          {
            string DocumentTitle = pdfTools.GetTitle();
            if( DocumentTitle != null )
            {
              this.SetTitle( DocumentTitle, MacroscopeConstants.TextProcessingMode.NO_PROCESSING );
              DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
            }
            else
            {
              DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
            }
          }
        }

        /** ---------------------------------------------------------------- **/
        
      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }
            
    }

    /**************************************************************************/

  }

}
