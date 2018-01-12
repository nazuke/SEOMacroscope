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
using System.Diagnostics;
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

  /// <summary>
  /// Description of MacroscopeDocument.
  /// </summary>

  public partial class MacroscopeDocument : Macroscope
  {

    // TODO: Finish implementing authentication

    /** Execute Head Request **************************************************/

    private void ConfigureHeadRequestHeadersCallback ( HttpRequestMessage Request )
    {
      this.AuthenticateRequest( Request: Request );
    }

    /** -------------------------------------------------------------------- **/

    private void PostProcessRequestHttpHeadersCallback ( HttpRequestMessage Request )
    {
      this.PostProcessRequestHttpHeaders( Request: Request );
    }

    /** -------------------------------------------------------------------- **/

    private async Task ExecuteHeadRequest ()
    {

      Stopwatch TimeDuration = new Stopwatch();
      long FinalDuration;

      TimeDuration.Start();

      try
      {
        await this._ExecuteHeadRequest();
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "ExecuteHeadRequest :: Exception: {0}", ex.Message ) );
      }

      TimeDuration.Stop();
      FinalDuration = TimeDuration.ElapsedMilliseconds;

      if ( FinalDuration > 0 )
      {
        this.Duration = FinalDuration;
      }
      else
      {
        this.Duration = 0;
      }

    }

    /** -------------------------------------------------------------------- **/

    private async Task _ExecuteHeadRequest ()
    {

      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse ClientResponse = null;
      Uri DocUri = null;
      string ResponseErrorCondition = null;

      this.SetProcessInlinks();
      this.SetProcessHyperlinksIn();

      try
      {

        DocUri = new Uri( this.DocUrl );

        ClientResponse = await Client.Head(
          DocUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback
         );

        this.CrawledDate = DateTime.UtcNow;

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "_ExecuteHeadRequest :: MacroscopeDocumentException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.SetStatusCode( HttpStatusCode.BadRequest );
        this.AddRemark( ex.Message );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "_ExecuteHeadRequest :: Exception: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.SetStatusCode( HttpStatusCode.BadRequest );
        this.AddRemark( ex.Message );
      }

      if ( ClientResponse != null )
      {

        try
        {

          this.DebugMsg( string.Format( "StatusCode: {0}", ClientResponse.GetResponse().StatusCode ) );

          if ( ClientResponse.GetResponse() != null )
          {
            this.SetErrorCondition( ClientResponse.GetResponse().ReasonPhrase );
          }
          else
          {
            throw new MacroscopeDocumentException( "Bad Response in ExecuteHeadRequest" );
          }

          this.ProcessResponseHttpHeaders( Response: ClientResponse );


          if ( this.GetIsRedirect() )
          {

            string Location = this.GetUrlRedirectTo();

            if ( !string.IsNullOrEmpty( Location ) )
            {

              MacroscopeLink OutLink = null;

              this.SetUrlRedirectTo( Url: Location );

              OutLink = this.AddDocumentOutlink(
                AbsoluteUrl: Location,
                LinkType: MacroscopeConstants.InOutLinkType.REDIRECT,
                Follow: true
              );

              OutLink.SetRawTargetUrl( TargetUrl: this.GetUrlRedirectToRaw() );

            }

          }

        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "_ExecuteHeadRequest :: Exception: {0}", ex.Message ) );
          ResponseErrorCondition = ex.Message;
        }

      }
      else
      {
        // NO-OP
      }

      if ( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

      return;

    }

    /**************************************************************************/

  }

}
