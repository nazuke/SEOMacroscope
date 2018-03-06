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

    private void ConfigureTextPageRequestHeadersCallback ( HttpRequestMessage Request )
    {
      this.AuthenticateRequest( Request: Request );
    }

    /** -------------------------------------------------------------------- **/

    private async Task ProcessTextPage ()
    {

      Stopwatch TimeDuration = new Stopwatch();
      long FinalDuration;

      TimeDuration.Start();

      try
      {
        await this._ProcessTextPage();
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

    private async Task _ProcessTextPage ()
    {

      List<string> TextDoc = new List<string>();
      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      string ResponseErrorCondition = null;

      try
      {

        Response = await Client.Get(
          this.GetUri(),
          this.ConfigureTextPageRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback,
          MacroscopeHttpTwoClient.DecodeResponseContentAs.STRING
        );

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "_ProcessTextPage :: MacroscopeDocumentException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.SetStatusCode( HttpStatusCode.BadRequest );
        this.AddRemark( "_ProcessTextPage", ex.Message );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "_ProcessTextPage :: Exception: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.SetStatusCode( HttpStatusCode.BadRequest );
        this.AddRemark( "_ProcessTextPage", ex.Message );
      }

      if ( Response != null )
      {

        string RawData = "";

        this.ProcessResponseHttpHeaders( Response: Response );

        /** Get Response Body ---------------------------------------------- **/

        try
        {

          DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );

          RawData = Response.GetContentAsString();

          this.SetContentLength( Length: RawData.Length ); // May need to find bytes length

          this.SetWasDownloaded( true );

          this.SetChecksum( RawData );

        }
        catch ( Exception ex )
        {
          DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
          this.SetStatusCode( HttpStatusCode.BadRequest );
          RawData = "";
          this.SetContentLength( Length: 0 );
        }

        /** ---------------------------------------------------------------- **/

        if ( !string.IsNullOrEmpty( RawData ) )
        {

          string[] Lines = Regex.Split( RawData, @"[\r\n]+" );
          TextDoc = Lines.ToList();

          DebugMsg( string.Format( "TextDoc: {0}", TextDoc.Count ) );

        }
        else
        {
          DebugMsg( string.Format( "RawData: {0}", "EMPTY" ) );
        }

        /** Custom Filters ------------------------------------------------- **/

        if ( !string.IsNullOrEmpty( RawData ) )
        {

          if (
            MacroscopePreferencesManager.GetCustomFiltersEnable()
            && MacroscopePreferencesManager.GetCustomFiltersApplyToText() )
          {

            MacroscopeCustomFilters CustomFilter = this.DocCollection.GetJobMaster().GetCustomFilter();

            if ( ( CustomFilter != null ) && ( CustomFilter.IsEnabled() ) )
            {
              this.ProcessGenericCustomFiltered(
                CustomFilter: CustomFilter,
                GenericText: RawData
              );
            }

          }

        }

        /** Data Extractors ------------------------------------------------ **/

        if ( !string.IsNullOrEmpty( RawData ) )
        {

          if (
            MacroscopePreferencesManager.GetDataExtractorsEnable()
            && MacroscopePreferencesManager.GetDataExtractorsApplyToText() )
          {
            this.ProcessGenericDataExtractors( GenericText: RawData );
          }

        }

        /** Process Text Document ------------------------------------------ **/

        if ( ( TextDoc != null ) && ( TextDoc.Count > 0 ) )
        {

          this.SetDocumentText( Text: string.Join( Environment.NewLine, TextDoc ) );

          if ( this.GetPath().EndsWith( "robots.txt", StringComparison.InvariantCultureIgnoreCase ) )
          {

            long? TextSize = this.GetContentLength();
            long? RobotsMaxTextSize = 1024 * 512;

            this.ProcessRobotsTextOutlinks( TextDoc: TextDoc );

            if ( this.DetectSitemapTextDocument( TextDoc: TextDoc ) )
            {
              DebugMsg( string.Format( "ProcessTextPage: {0} :: {1}", "SITEMAP DETECTED", this.GetUrl() ) );
              this.SetDocumentType( Type: MacroscopeConstants.DocumentType.SITEMAPTEXT );
              this.ProcessSitemapTextOutlinks( TextDoc: TextDoc );
            }

            if ( TextSize > RobotsMaxTextSize )
            {
              this.AddRemark( "ROBOTS_TOO_BIG", "Robots.txt is larger than 512KB" );
            }

          }
          else
          {
            if ( this.GetIsInternal() )
            {
              this.ProcessPureTextOutlinks( TextDoc: TextDoc, LinkType: MacroscopeConstants.InOutLinkType.PURETEXT );
            }
          }

        }
        else
        {
          this.SetDocumentText( Text: "" );
        }

        /** ---------------------------------------------------------------- **/

      }

      if ( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

    }

    /**************************************************************************/

    bool DetectSitemapTextDocument ( List<string> TextDoc )
    {

      bool IsSitemap = false;

      foreach ( string Url in TextDoc )
      {

        string UrlProcessing = Regex.Replace( Url, @"\s+", "" );

        if ( !string.IsNullOrEmpty( UrlProcessing ) )
        {

          try
          {

            Uri SitemapUri = new Uri( UrlProcessing );

            if ( SitemapUri != null )
            {
              if ( ( SitemapUri.Scheme == "http" ) || ( SitemapUri.Scheme == "https" ) )
              {
                IsSitemap = true;
              }
              else
              {
                IsSitemap = false;
                break;
              }
            }

          }
          catch ( UriFormatException ex )
          {
            DebugMsg( string.Format( "DetectSitemapTextDocument: UriFormatException: {0}", ex.Message ) );
            DebugMsg( string.Format( "DetectSitemapTextDocument: UriFormatException: {0}", this.GetUrl() ) );
            IsSitemap = false;
            break;
          }
          catch ( Exception ex )
          {
            DebugMsg( string.Format( "DetectSitemapTextDocument: Exception: {0}", ex.Message ) );
            DebugMsg( string.Format( "DetectSitemapTextDocument: Exception: {0}", this.GetUrl() ) );
            IsSitemap = false;
            break;
          }

        }

      }

      DebugMsg( string.Format( "DetectSitemapTextDocument: IsSitemap: {0} :: {1}", IsSitemap, this.GetUrl() ) );

      return ( IsSitemap );

    }

    /** Text Sitemap Out Links ************************************************/

    private void ProcessSitemapTextOutlinks ( List<string> TextDoc )
    {

      if ( this.GetIsExternal() )
      {
        return;
      }

      foreach ( string Url in TextDoc )
      {

        string UrlProcessing = Regex.Replace( Url, @"\s+", "" );
        string UrlCleaned = null;

        if ( !string.IsNullOrEmpty( UrlProcessing ) )
        {

          try
          {
            Uri SitemapUri = new Uri( UrlProcessing );
            if ( SitemapUri != null )
            {
              UrlCleaned = UrlProcessing;
            }
          }
          catch ( UriFormatException ex )
          {
            DebugMsg( string.Format( "ProcessSitemapTextOutlinks: {0}", ex.Message ) );
            UrlCleaned = null;
          }
          catch ( Exception ex )
          {
            DebugMsg( string.Format( "ProcessSitemapTextOutlinks: {0}", ex.Message ) );
            UrlCleaned = null;
          }

          if ( UrlCleaned != null )
          {

            MacroscopeLink Outlink;

            Outlink = this.AddDocumentOutlink(
              AbsoluteUrl: UrlCleaned,
              LinkType: MacroscopeConstants.InOutLinkType.SITEMAPTEXT,
              Follow: true
            );

            if ( Outlink != null )
            {
              Outlink.SetRawTargetUrl( UrlCleaned );
            }

          }

        }

      }

    }

    /** Robots.txt Out Links **************************************************/

    private void ProcessRobotsTextOutlinks ( List<string> TextDoc )
    {

      if ( this.GetIsExternal() )
      {
        return;
      }

      foreach ( string Url in TextDoc )
      {

        if ( Regex.IsMatch( Url, @"^Sitemap:\s*[^\s]+", RegexOptions.IgnoreCase ) )
        {

          string UrlProcessing = Regex.Replace( Url, @"^(Sitemap:\s*)", "", RegexOptions.IgnoreCase );
          string UrlCleaned = null;

          UrlProcessing = UrlProcessing.Trim();

          if ( !string.IsNullOrEmpty( UrlProcessing ) )
          {

            try
            {
              Uri SitemapUri = new Uri( UrlProcessing );
              if ( SitemapUri != null )
              {
                UrlCleaned = UrlProcessing;
              }
            }
            catch ( UriFormatException ex )
            {
              DebugMsg( string.Format( "ProcessRobotsTextOutlinks: {0}", ex.Message ) );
              UrlCleaned = null;
            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "ProcessRobotsTextOutlinks: {0}", ex.Message ) );
              UrlCleaned = null;
            }

            if ( UrlCleaned != null )
            {

              MacroscopeLink Outlink;

              Outlink = this.AddDocumentOutlink(
                AbsoluteUrl: UrlCleaned,
                LinkType: MacroscopeConstants.InOutLinkType.ROBOTSTEXT,
                Follow: true
              );

              if ( Outlink != null )
              {
                Outlink.SetRawTargetUrl( UrlCleaned );
              }

            }

          }

        }

      }

    }

    /**************************************************************************/

  }

}
