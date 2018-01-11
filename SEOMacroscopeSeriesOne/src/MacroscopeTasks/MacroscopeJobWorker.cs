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
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeJobWorker.
  /// </summary>

  public class MacroscopeJobWorker : Macroscope
  {

    /**************************************************************************/

    private MacroscopeJobMaster JobMaster;
    private MacroscopeDocumentCollection DocCollection;
    private MacroscopeAllowedHosts AllowedHosts;
    private MacroscopeIncludeExcludeUrls IncludeExcludeUrls;

    private int CrawlDelay;

    /**************************************************************************/

    public MacroscopeJobWorker ( MacroscopeJobMaster JobMaster )
    {

      this.SuppressDebugMsg = true;

      this.JobMaster = JobMaster;

      this.DocCollection = this.JobMaster.GetDocCollection();

      this.AllowedHosts = this.JobMaster.GetAllowedHosts();

      this.IncludeExcludeUrls = this.JobMaster.GetIncludeExcludeUrls();

      if( MacroscopePreferencesManager.GetCrawlDelay() > 0 )
      {
        this.CrawlDelay = MacroscopePreferencesManager.GetCrawlDelay();
      }

      if( MacroscopePreferencesManager.GetFollowRobotsProtocol() )
      {
        if( this.JobMaster.GetCrawlDelay() > 0 )
        {
          this.CrawlDelay = this.JobMaster.GetCrawlDelay();
        }
      }

    }

    /**************************************************************************/

    public async void Execute ()
    {

      int MaxFetches = MacroscopePreferencesManager.GetMaxFetchesPerWorker();

      while( MaxFetches > 0 )
      {

        if( this.JobMaster.GetThreadsStop() )
        {

          DebugMsg( string.Format( "JobMaster.GetThreadsStop: {0}", this.JobMaster.GetThreadsStop() ) );
          break;

        }
        else
        {

          MacroscopeJobItem JobItem = this.JobMaster.GetUrlQueueItem();
          string Url = null;

          if( JobItem != null )
          {
            Url = JobItem.GetItemUrl();
          }

          if( !string.IsNullOrEmpty( Url ) )
          {
            if( !this.CheckIncludeExcludeUrl( Url ) )
            {
              Url = null;
            }
          }

          if( !string.IsNullOrEmpty( Url ) )
          {

            if(
              !MacroscopePreferencesManager.GetCrawlParentDirectories()
              && !MacroscopePreferencesManager.GetCrawlChildDirectories()
              && Url != this.JobMaster.GetStartUrl() )
            {
              Url = null;
            }
            else if(
            !MacroscopePreferencesManager.GetCrawlParentDirectories()
            || !MacroscopePreferencesManager.GetCrawlChildDirectories() )
            {

              DebugMsg( string.Format( "Running Parent/Child Check: {0}", Url ) );

              if(
                MacroscopePreferencesManager.GetCrawlParentDirectories()
                && ( !string.IsNullOrEmpty( Url ) ) )
              {
                if( !this.JobMaster.IsWithinParentDirectory( Url ) )
                {
                  Url = null;
                }
              }

              if(
                MacroscopePreferencesManager.GetCrawlChildDirectories()
                && ( !string.IsNullOrEmpty( Url ) ) )
              {
                if( !this.JobMaster.IsWithinChildDirectory( Url ) )
                {
                  Url = null;
                }
              }

            }
            else
            {
              DebugMsg( string.Format( "Skipping Parent/Child Check: {0}", Url ) );
            }

          }

          if( !string.IsNullOrEmpty( Url ) )
          {

            DebugMsg( string.Format( "Execute: {0}", Url ) );

            int Tries = MacroscopePreferencesManager.GetMaxRetries();

            do
            {

              DebugMsg( string.Format( "Trying Fetch: {0} :: {1}", Tries, Url ) );

              MacroscopeConstants.FetchStatus FetchStatus = MacroscopeConstants.FetchStatus.VOID;

              try
              {
                FetchStatus = await this.Fetch( Url );
              }
              catch( Exception ex )
              {
                DebugMsg( string.Format( "FetchStatus: {0}", ex.Message ) );
                DebugMsg( string.Format( "Url: {0}", Url ) );
                DebugMsg( string.Format( "FetchStatus: {0}", FetchStatus ) );
              }

              if( ( FetchStatus == MacroscopeConstants.FetchStatus.ERROR ) || ( FetchStatus == MacroscopeConstants.FetchStatus.NETWORK_ERROR ) )
              {
                DebugMsg( string.Format( "Fetch Failed: {0} :: {1}", Tries, Url ) );
                Thread.Sleep( 1000 );
              }
              else
              {
                this.JobMaster.NotifyWorkersFetched( Url );
                break;
              }

              /*
              if( 
                ( this.Fetch( Url ) == MacroscopeConstants.FetchStatus.ERROR )
                || ( this.Fetch( Url ) == MacroscopeConstants.FetchStatus.ERROR ) ) {
                DebugMsg( string.Format( "Fetch Failed: {0} :: {1}", Tries, Url ) );
                Thread.Sleep( 1000 );
              } else {
                this.JobMaster.NotifyWorkersFetched( Url );
                break;
              }
              */

              Tries--;

            } while( Tries > 0 );

            if( this.CrawlDelay > 0 )
            {
              DebugMsg( string.Format( "CRAWL DELAY: Sleeping for {0} seconds...", this.CrawlDelay ) );
              Thread.Sleep( CrawlDelay * 1000 );
            }

          }

        }

        MaxFetches--;

        Thread.Yield();

      }

      this.JobMaster.NotifyWorkersDone();

    }

    /** Check Include/Exclude URL *********************************************/

    private bool CheckIncludeExcludeUrl ( string Url )
    {

      bool Success = true;

      if( ( this.IncludeExcludeUrls != null ) && ( this.IncludeExcludeUrls.UseIncludeUrlPatterns() ) )
      {
        if( this.IncludeExcludeUrls.MatchesIncludeUrlPattern( Url ) )
        {
          DebugMsg( string.Format( "CheckIncludeExcludeUrl: MATCHES INCLUDE URL: {0}", Url ) );
        }
        else
        {
          DebugMsg( string.Format( "CheckIncludeExcludeUrl: DOES NOT MATCH INCLUDE URL: {0}", Url ) );
          Success = false;
        }
      }

      if( ( this.IncludeExcludeUrls != null ) && ( this.IncludeExcludeUrls.UseExcludeUrlPatterns() ) )
      {
        if( this.IncludeExcludeUrls.MatchesExcludeUrlPattern( Url ) )
        {
          DebugMsg( string.Format( "CheckIncludeExcludeUrl: MATCHES EXCLUDE URL: {0}", Url ) );
          Success = false;
        }
        else
        {
          DebugMsg( string.Format( "CheckIncludeExcludeUrl: DOES NOT MATCH EXCLUDE URL: {0}", Url ) );
        }
      }

      return ( Success );

    }

    /**************************************************************************/

    private async Task<MacroscopeConstants.FetchStatus> Fetch ( string Url )
    {

      MacroscopeDocument msDoc = this.DocCollection.GetDocument( Url );
      MacroscopeConstants.FetchStatus FetchStatus = MacroscopeConstants.FetchStatus.VOID;
      bool BlockedByRobotsRule;

      if( msDoc != null )
      {

        if( msDoc.GetAuthenticationRealm() != null )
        {
          if( msDoc.GetAuthenticationType() == MacroscopeConstants.AuthenticationType.BASIC )
          {

            MacroscopeCredential Credential;

            Credential = this.JobMaster.GetCredentialsHttp().GetCredential(
              msDoc.GetHostAndPort(),
              msDoc.GetAuthenticationRealm()
            );

            if( Credential != null )
            {
              msDoc = this.DocCollection.CreateDocument(
                Credential: Credential,
                Url: Url
              );
            }

          }

        }

      }
      else
      {
        msDoc = this.DocCollection.CreateDocument( Url );
      }

      msDoc.SetFetchStatus( MacroscopeConstants.FetchStatus.OK );

      if( !MacroscopeDnsTools.CheckValidHostname( Url: Url ) )
      {

        DebugMsg( string.Format( "Fetch :: CheckValidHostname: {0}", "NOT OK" ) );

        msDoc.SetStatusCode( HttpStatusCode.BadGateway );

        FetchStatus = MacroscopeConstants.FetchStatus.NETWORK_ERROR;

        msDoc.SetFetchStatus( MacroscopeConstants.FetchStatus.NETWORK_ERROR );

      }

      this.JobMaster.GetJobHistory().AddHistoryItem( Url: Url );

      if( await this.JobMaster.GetRobots().CheckRobotRule( Url: Url ) )
      {
        msDoc.SetAllowedByRobots( true );
      }
      else
      {
        msDoc.SetAllowedByRobots( false );
      }

      BlockedByRobotsRule = await this.JobMaster.GetRobots().ApplyRobotRule( Url: Url );

      if( !BlockedByRobotsRule )
      {
        DebugMsg( string.Format( "Disallowed by robots.txt: {0}", Url ) );
        this.JobMaster.AddToBlockedByRobots( Url );
        FetchStatus = MacroscopeConstants.FetchStatus.ROBOTS_DISALLOWED;
        msDoc.SetFetchStatus( MacroscopeConstants.FetchStatus.ROBOTS_DISALLOWED );
        this.JobMaster.GetJobHistory().VisitedHistoryItem( Url: msDoc.GetUrl() );
      }
      else
      {
        this.JobMaster.RemoveFromBlockedByRobots( Url );
      }

//      this.JobMaster.GetJobHistory().AddHistoryItem( Url: Url );

      if( this.AllowedHosts.IsExternalUrl( Url: Url ) )
      {
        DebugMsg( string.Format( "IsExternalUrl: {0}", Url ) );
        msDoc.SetIsExternal( State: true );
      }

      if ( this.DocCollection.ContainsDocument( Url ) )
      {
        if ( !this.DocCollection.GetDocument( Url ).GetIsDirty() )
        {
          FetchStatus = MacroscopeConstants.FetchStatus.ALREADY_SEEN;
          return ( FetchStatus );
        }
      }
      else
      {
        ; // NO-OP
      }

      if( this.JobMaster.GetDepth() > 0 )
      {
        int Depth = MacroscopeUrlUtils.FindUrlDepth( Url );
        if( Depth > this.JobMaster.GetDepth() )
        {
          DebugMsg( string.Format( "TOO DEEP: {0}", Depth ) );
          FetchStatus = MacroscopeConstants.FetchStatus.SKIPPED;
          return ( FetchStatus );
        }
      }

      if( await msDoc.Execute() )
      {

        this.DocCollection.AddDocument( Url, msDoc );

        if( msDoc.GetStatusCode() == HttpStatusCode.Unauthorized )
        {

          if( msDoc.GetAuthenticationType() == MacroscopeConstants.AuthenticationType.BASIC )
          {

            MacroscopeCredentialsHttp CredentialsHttp = this.JobMaster.GetCredentialsHttp();

            CredentialsHttp.EnqueueCredentialRequest(
              Domain: msDoc.GetHostAndPort(),
              Realm: msDoc.GetAuthenticationRealm(),
              Url: msDoc.GetUrl()
            );

            this.JobMaster.AddUrlQueueItem( Url: msDoc.GetUrl() );

          }

        }

        this.JobMaster.GetJobHistory().VisitedHistoryItem( Url: msDoc.GetUrl() );

        this.JobMaster.IncPageLimitCount();

        if( msDoc.GetIsRedirect() )
        {

          DebugMsg( string.Format( "REDIRECTION DETECTED GetUrl: {0}", msDoc.GetUrl() ) );
          DebugMsg( string.Format( "REDIRECTION DETECTED From: {0}", msDoc.GetUrlRedirectFrom() ) );

          if( MacroscopePreferencesManager.GetCheckRedirects() )
          {

            string Hostname = msDoc.GetHostAndPort();

            string HostnameFrom = MacroscopeAllowedHosts.ParseHostnameFromUrl( msDoc.GetUrlRedirectFrom() );

            string UrlRedirectTo = msDoc.GetUrlRedirectTo();

            string HostnameTo = MacroscopeAllowedHosts.ParseHostnameFromUrl( UrlRedirectTo );

            DebugMsg( string.Format( "REDIRECTION DETECTED UrlRedirectTo: {0}", UrlRedirectTo ) );
            DebugMsg( string.Format( "REDIRECTION DETECTED HostnameTo: {0}", HostnameTo ) );

            if( MacroscopePreferencesManager.GetFollowRedirects() )
            {
              if( MacroscopePreferencesManager.GetCheckExternalLinks() )
              {
                this.AllowedHosts.AddFromUrl( Url: UrlRedirectTo );
              }
              else
              {
                if( this.AllowedHosts.IsInternalUrl( Url: UrlRedirectTo ) )
                {
                  this.AllowedHosts.AddFromUrl( Url: UrlRedirectTo );
                }
              }
            }

          }

          this.JobMaster.AddUrlQueueItem( Url: msDoc.GetUrlRedirectTo() );

        }
        else
        {

          this.ProcessHrefLangLanguages( msDoc ); // Process Languages from HrefLang

          this.ProcessOutlinks( msDoc: msDoc ); // Process Outlinks from document

        }

        FetchStatus = MacroscopeConstants.FetchStatus.SUCCESS;

      }
      else
      {
        DebugMsg( string.Format( "EXECUTE FAILED: {0}", Url ) );
        FetchStatus = MacroscopeConstants.FetchStatus.ERROR;
      }

      return ( FetchStatus );

    }

    /**************************************************************************/

    private void ProcessHrefLangLanguages ( MacroscopeDocument msDoc )
    {

      string Locale = msDoc.GetLocale();
      Dictionary<string, MacroscopeHrefLang> HrefLangsTable = msDoc.GetHrefLangs();

      if( Locale != null )
      {
        this.JobMaster.AddLocales( Locale );
      }

      foreach( string KeyLocale in HrefLangsTable.Keys )
      {
        this.JobMaster.AddLocales( KeyLocale );
      }

    }

    /**************************************************************************/

    private void ProcessOutlinks ( MacroscopeDocument msDoc )
    {

      if(
        ( this.JobMaster.GetRunTimeMode() == MacroscopeConstants.RunTimeMode.LISTFILE )
        || ( this.JobMaster.GetRunTimeMode() == MacroscopeConstants.RunTimeMode.LISTTEXT )
        || ( this.JobMaster.GetRunTimeMode() == MacroscopeConstants.RunTimeMode.SITEMAP ) )
      {

        if( !MacroscopePreferencesManager.GetScanSitesInList() )
        {
          return;
        }

      }

      foreach( MacroscopeLink Outlink in msDoc.IterateOutlinks() )
      {

        bool Proceed = true;

        if( !Outlink.GetDoFollow() )
        {
          continue;
        }

        if( Outlink.GetTargetUrl() == null )
        {
          continue;
        }

        if( this.JobMaster.GetJobHistory().SeenHistoryItem( Url: Outlink.GetTargetUrl() ) )
        {
          continue;
        }

        if( this.JobMaster.GetPageLimit() > -1 )
        {
          if( this.JobMaster.GetPageLimitCount() >= this.JobMaster.GetPageLimit() )
          {
            this.DebugMsg(
              string.Format(
                "PAGE LIMIT REACHED: {0} :: {1}",
                this.JobMaster.GetPageLimit(),
                this.JobMaster.GetPageLimitCount()
              )
            );
            Proceed = false;
          }
        }

        if( Proceed )
        {

          this.JobMaster.AddUrlQueueItem(
            Url: Outlink.GetTargetUrl(),
            Check: true
          );

        }

      }

    }

    /**************************************************************************/

  }

}
