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
      this.AuthenticateRequest( Request: Request );
    }

    /** -------------------------------------------------------------------- **/

    private async Task ProcessPdfPage ()
    {

      Stopwatch TimeDuration = new Stopwatch();
      long FinalDuration;

      TimeDuration.Start();

      try
      {
        await this._ProcessPdfPage();
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "ProcessPdfPage: {0}", ex.Message ) );
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

    private async Task _ProcessPdfPage ()
    {

      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse ClientResponse = null;
      string ResponseErrorCondition = null;

      try
      {

        ClientResponse = await Client.Get(
          this.GetUri(),
          this.ConfigurePdfPageRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback,
          MacroscopeHttpTwoClient.DecodeResponseContentAs.BYTES
        );

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "_ProcessPdfPage :: MacroscopeDocumentException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.AddRemark( "_ProcessPdfPage", ex.Message );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "_ProcessPdfPage :: Exception: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.AddRemark( "_ProcessPdfPage", ex.Message );
      }

      if ( ClientResponse != null )
      {

        MacroscopePdfTools pdfTools;

        this.ProcessResponseHttpHeaders( Response: ClientResponse );

        { // Probe Locale
          //this.Locale = "en"; // Implement locale probing
          this.Locale = "x-default"; // Implement locale probing
          this.SetHreflang( HrefLangLocale: this.Locale, Url: this.DocUrl );
        }

        { // Canonical
          this.Canonical = this.DocUrl;
          this.DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
        }

        /** Get Response Body ---------------------------------------------- **/

        try
        {

          byte[] RawData = ClientResponse.GetContentAsBytes();
          this.SetContentLength( Length: RawData.Length );

          pdfTools = new MacroscopePdfTools( PdfData: RawData );

          if ( pdfTools.GetHasError() )
          {
            this.AddRemark( "CORRUPT_PDF", Observation: pdfTools.GetErrorMessage() );
          }

          this.SetWasDownloaded( true );

        }
        catch ( Exception ex )
        {

          this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
          this.SetStatusCode( HttpStatusCode.BadRequest );
          pdfTools = null;
          this.SetContentLength( Length: 0 );

        }

        /** Title ---------------------------------------------------------- **/

        if ( pdfTools != null )
        {

          string Text = pdfTools.GetTitle();

          if ( !string.IsNullOrEmpty( Text ) )
          {
            this.SetTitle( Text, MacroscopeConstants.TextProcessingMode.NO_PROCESSING );
            this.DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
          }
          else
          {
            this.DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
          }

        }


        /** Description ---------------------------------------------------- **/

        if ( pdfTools != null )
        {

          string Text = pdfTools.GetDescription();

          if ( !string.IsNullOrEmpty( Text ) )
          {
            this.SetDescription( Text, MacroscopeConstants.TextProcessingMode.NO_PROCESSING );
            this.DebugMsg( string.Format( "TITLE: {0}", this.GetDescription() ) );
          }
          else
          {
            this.DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
          }

        }

        /** Body Text ------------------------------------------------------ **/

        if ( pdfTools != null )
        {

          this.SetBodyText( Text: "" );

          if ( pdfTools.GetHasError() )
          {
            this.AddRemark( "PDF_ERROR", Observation: pdfTools.GetErrorMessage() );
          }
          else
          {
            string Text = pdfTools.GetTextAsString();
            if ( !string.IsNullOrEmpty( Text ) )
            {
              this.SetDocumentText( Text: Text );
              this.SetBodyText( Text: Text );
            }
          }

          this.DebugMsg( string.Format( "BODY TEXT: {0}", this.GetBodyTextRaw() ) );

        }

        /** ---------------------------------------------------------------- **/

      }

      if ( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

    }

    /**************************************************************************/

  }

}
