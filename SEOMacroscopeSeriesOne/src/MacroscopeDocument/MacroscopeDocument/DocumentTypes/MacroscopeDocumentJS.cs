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

    private void ConfigureJavascriptPageRequestHeadersCallback ( HttpRequestMessage Request )
    {
    }

    /** -------------------------------------------------------------------- **/

    private async Task ProcessJavascriptPage ()
    {

      Stopwatch TimeDuration = new Stopwatch();
      long FinalDuration;

      TimeDuration.Start();

      try
      {
        await this._ProcessJavascriptPage();
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "ProcessJavascriptPage :: Exception: {0}", ex.Message ) );
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

    private async Task _ProcessJavascriptPage ()
    {

      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      Uri DocUri;
      string ResponseErrorCondition = null;
      Boolean IsAuthenticating = false;
      
      try
      {

        DocUri = new Uri( this.DocUrl );
        Response = await Client.Get( DocUri, this.ConfigureJavascriptPageRequestHeadersCallback, this.PostProcessRequestHttpHeadersCallback );

        // TODO: Fix this:
        //IsAuthenticating = this.AuthenticateRequest( req );

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "_ProcessJavascriptPage :: MacroscopeDocumentException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.SetStatusCode( HttpStatusCode.BadRequest );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "_ProcessJavascriptPage :: Exception: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.SetStatusCode( HttpStatusCode.BadRequest );
      }

      if ( Response != null )
      {

        string RawData = "";

        this.ProcessResponseHttpHeaders( Response: Response );

        /** ---------------------------------------------------------------- **/

        if( IsAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        /** Get Response Body ---------------------------------------------- **/
        
        try
        {

          DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );

          /*
          Encoding encUseEncoding = Encoding.UTF8;

          if( this.GetCharacterEncoding() != null )
          {
            encUseEncoding = this.GetCharacterEncoding();
          }
          else
          {
            encUseEncoding = this.JavascriptSniffCharset();
          }
          */

          RawData = Response.GetContentAsString();
          this.SetContentLength( Length: RawData.Length ); // May need to find bytes length
          this.SetChecksum( RawData );

        }
        catch( Exception ex )
        {

          DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
          this.SetStatusCode( HttpStatusCode.Ambiguous );
          RawData = "";
          this.SetContentLength( Length: 0 );

        }

        /** Custom Filters ------------------------------------------------- **/

        if( !string.IsNullOrEmpty( RawData ) )
        {

          if(
            MacroscopePreferencesManager.GetCustomFiltersEnable()
            && MacroscopePreferencesManager.GetCustomFiltersApplyToJavascripts() )
          {
          
            MacroscopeCustomFilters CustomFilter = this.DocCollection.GetJobMaster().GetCustomFilter();

            if( ( CustomFilter != null ) && ( CustomFilter.IsEnabled() ) )
            {
              this.ProcessGenericCustomFiltered(
                CustomFilter: CustomFilter, 
                GenericText: RawData
              );
            }

          }
          
        }

        /** Data Extractors ------------------------------------------------ **/

        if( !string.IsNullOrEmpty( RawData ) )
        {

          if(
            MacroscopePreferencesManager.GetDataExtractorsEnable()
            && MacroscopePreferencesManager.GetDataExtractorsApplyToJavascripts() )
          {
            this.ProcessGenericDataExtractors( GenericText: RawData );
          }

        }

        /** Title ---------------------------------------------------------- **/

        {
          MatchCollection reMatches = Regex.Matches( this.DocUrl, "/([^/]+)$" );
          string DocumentTitle = null;
          foreach( Match match in reMatches )
          {
            if( match.Groups[ 1 ].Value.Length > 0 )
            {
              DocumentTitle = match.Groups[ 1 ].Value.ToString();
              break;
            }
          }
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

        /** ---------------------------------------------------------------- **/

      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }
            
    }

    /** Sniff Charset *********************************************************/

    Encoding JavascriptSniffCharset ()
    {

      Encoding encSniffed = Encoding.UTF8;

      // TODO: Implement code to download JS and detect charset

      return( encSniffed );

    }

    /**************************************************************************/

  }

}
