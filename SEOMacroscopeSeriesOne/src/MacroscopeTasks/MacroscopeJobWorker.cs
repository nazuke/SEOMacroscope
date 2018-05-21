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
    private MacroscopeJobHistory JobHistory;
    private MacroscopeDocumentCollection DocCollection;
    private MacroscopeAllowedHosts AllowedHosts;
    private MacroscopeIncludeExcludeUrls IncludeExcludeUrls;

    private int CrawlDelay;

    /**************************************************************************/

    public MacroscopeJobWorker ( MacroscopeJobMaster JobMaster )
    {

      this.SuppressDebugMsg = true;

      this.JobMaster = JobMaster;
      this.JobHistory = this.JobMaster.GetJobHistory();
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

          this.DebugMsg( string.Format( "JobMaster.GetThreadsStop: {0}", this.JobMaster.GetThreadsStop() ) );
          break;

        }
        else
        {

          MacroscopeJobItem JobItem = this.JobMaster.GetUrlQueueItem();
          string Url = null;
          string RedirectedFromUrl = null;

          if( JobItem != null )
          {
            Url = JobItem.GetItemUrl();
            RedirectedFromUrl = JobItem.GetItemRedirectedFromUrl();
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

              this.DebugMsg( string.Format( "Running Parent/Child Check: {0}", Url ) );

              if(
                MacroscopePreferencesManager.GetCrawlParentDirectories()
                && ( !string.IsNullOrEmpty( Url ) ) )
              {
                if( !MacroscopeHttpUrlUtils.IsWithinParentDirectory( StartUrl: this.JobMaster.GetParentStartingDirectory(), Url: Url ) )
                {
                  Url = null;
                }
              }

              if(
                MacroscopePreferencesManager.GetCrawlChildDirectories()
                && ( !string.IsNullOrEmpty( Url ) ) )
              {
                if( !MacroscopeHttpUrlUtils.IsWithinChildDirectory( StartUrl: this.JobMaster.GetChildStartingDirectory(), Url: Url ) )
                {
                  Url = null;
                }
              }

            }
            else
            {
              this.DebugMsg( string.Format( "Skipping Parent/Child Check: {0}", Url ) );
            }

          }

          if( !string.IsNullOrEmpty( Url ) )
          {
            if( MacroscopePreferencesManager.GetDepth() >= 0 )
            {
              if( MacroscopeHttpUrlUtils.FindUrlDepth( Url: Url ) > MacroscopePreferencesManager.GetDepth() )
              {
                this.DebugMsg( string.Format( "URL Too Deep: {0}", Url ) );
                Url = null;
              }
            }
          }

          if( !string.IsNullOrEmpty( Url ) )
          {

            this.DebugMsg( string.Format( "Execute: {0}", Url ) );

            int Tries = MacroscopePreferencesManager.GetMaxRetries();

            JobHistory.AddHistoryItem( Url: Url );

            do
            {

              this.DebugMsg( string.Format( "Trying Fetch: {0} :: {1}", Tries, Url ) );

              MacroscopeConstants.FetchStatus FetchStatus = MacroscopeConstants.FetchStatus.VOID;

              try
              {
                if( !string.IsNullOrEmpty( RedirectedFromUrl ) )
                {
                  FetchStatus = await this.Fetch( Url, RedirectedFromUrl );
                }
                else
                {
                  FetchStatus = await this.Fetch( Url );
                }
              }
              catch( Exception ex )
              {
                this.DebugMsg( string.Format( "FetchStatus: {0}", ex.Message ) );
                this.DebugMsg( string.Format( "Url: {0}", Url ) );
                this.DebugMsg( string.Format( "FetchStatus: {0}", FetchStatus ) );
              }

              switch( FetchStatus )
              {
                case MacroscopeConstants.FetchStatus.ERROR:
                  this.DebugMsg( string.Format( "Fetch Failed: {0} :: {1}", Tries, Url ) );
                  Thread.Sleep( 25 );
                  break;
                case MacroscopeConstants.FetchStatus.NETWORK_ERROR:
                  this.DebugMsg( string.Format( "Fetch Failed: {0} :: {1}", Tries, Url ) );
                  Thread.Sleep( 25 );
                  break;
                default:
                  this.JobMaster.NotifyWorkersFetched( Url: Url );
                  Tries = 0;
                  break;
              }

              Tries--;

            } while( Tries > 0 );

            if( this.CrawlDelay > 0 )
            {
              this.DebugMsg( string.Format( "CRAWL DELAY: Sleeping for {0} seconds...", this.CrawlDelay ) );
              Thread.Sleep( CrawlDelay * 1000 );
            }

          }

        }

        MaxFetches--;

        //Thread.Yield();

      }

      this.JobMaster.NotifyWorkersDone();

    }

    /**************************************************************************/

    private async Task<MacroscopeConstants.FetchStatus> Fetch ( string Url, string RedirectedFromUrl = null )
    {

      MacroscopeDocument msDoc = null;
      MacroscopeConstants.FetchStatus FetchStatus = MacroscopeConstants.FetchStatus.VOID;
      bool BlockedByRobotsRule;

      if( MacroscopePreferencesManager.GetPageLimit() > -1 )
      {
        int PagesFound = this.JobMaster.GetPagesFound();
        int PageLimit = MacroscopePreferencesManager.GetPageLimit();
        if( PagesFound >= PageLimit )
        {
          this.DebugMsg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", PageLimit, PagesFound ) );
          return ( FetchStatus );
        }
      }

      if( this.DocCollection.ContainsDocument( Url: Url ) )
      {

        msDoc = this.DocCollection.GetDocument( Url: Url );

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
        msDoc = this.DocCollection.CreateDocument( Url: Url );
      }

      if( !string.IsNullOrEmpty( RedirectedFromUrl ) )
      {
        msDoc.SetUrlRedirectFrom( Url: RedirectedFromUrl );
      }

      msDoc.SetFetchStatus( MacroscopeConstants.FetchStatus.OK );

      if( !MacroscopeDnsTools.CheckValidHostname( Url: Url ) )
      {
        this.DebugMsg( string.Format( "Fetch :: CheckValidHostname: {0}", "NOT OK" ) );
        msDoc.SetStatusCode( HttpStatusCode.BadGateway );
        FetchStatus = MacroscopeConstants.FetchStatus.NETWORK_ERROR;
        msDoc.SetFetchStatus( FetchStatus );
      }

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

        this.DebugMsg( string.Format( "Disallowed by robots.txt: {0}", Url ) );

        this.JobMaster.AddToBlockedByRobots( Url );

        FetchStatus = MacroscopeConstants.FetchStatus.ROBOTS_DISALLOWED;

        msDoc.SetFetchStatus( FetchStatus );

        JobHistory.VisitedHistoryItem( Url: msDoc.GetUrl() );

      }
      else
      {
        this.JobMaster.RemoveFromBlockedByRobots( Url );
      }

      if( this.AllowedHosts.IsExternalUrl( Url: Url ) )
      {
        this.DebugMsg( string.Format( "IsExternalUrl: {0}", Url ) );
        msDoc.SetIsExternal( State: true );
      }

      if( this.DocCollection.ContainsDocument( Url: Url ) )
      {
        if( !this.DocCollection.GetDocument( Url ).GetIsDirty() )
        {
          FetchStatus = MacroscopeConstants.FetchStatus.ALREADY_SEEN;
          return ( FetchStatus );
        }
      }

      if( MacroscopePreferencesManager.GetDepth() >= 0 )
      {
        int Depth = MacroscopeHttpUrlUtils.FindUrlDepth( Url: Url );
        if( Depth > MacroscopePreferencesManager.GetDepth() )
        {
          this.DebugMsg( string.Format( "URL Too Deep: {0}", Depth ) );
          FetchStatus = MacroscopeConstants.FetchStatus.SKIPPED;
          return ( FetchStatus );
        }
      }

      /** ------------------------------------------------------------------ **/

      if( !await msDoc.Execute() )
      {
        this.DebugMsg( string.Format( "EXECUTE FAILED: {0}", Url ) );
        FetchStatus = MacroscopeConstants.FetchStatus.ERROR;
      }

      /** ------------------------------------------------------------------ **/

      {

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

        if( msDoc.GetIsRedirect() )
        {

          this.DebugMsg( string.Format( "REDIRECTION DETECTED GetUrl: {0}", msDoc.GetUrl() ) );
          this.DebugMsg( string.Format( "REDIRECTION DETECTED From: {0}", msDoc.GetUrlRedirectFrom() ) );

          if( MacroscopePreferencesManager.GetCheckRedirects() )
          {

            string Hostname = msDoc.GetHostAndPort();
            string HostnameFrom = MacroscopeAllowedHosts.ParseHostnameFromUrl( msDoc.GetUrlRedirectFrom() );
            string UrlRedirectTo = msDoc.GetUrlRedirectTo();
            string HostnameTo = MacroscopeAllowedHosts.ParseHostnameFromUrl( UrlRedirectTo );

            this.DebugMsg( string.Format( "REDIRECTION DETECTED UrlRedirectTo: {0}", UrlRedirectTo ) );
            this.DebugMsg( string.Format( "REDIRECTION DETECTED HostnameTo: {0}", HostnameTo ) );

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

          this.JobMaster.ProcessOutlinks( msDoc: msDoc ); // Process Outlinks from document

        }

        FetchStatus = MacroscopeConstants.FetchStatus.SUCCESS;

      }

      /** ------------------------------------------------------------------ **/

      if( DocCollection.ContainsDocument( msDoc: msDoc ) )
      {
        JobHistory.VisitedHistoryItem( Url: Url );
      }
      else
      {
        this.DebugMsg( string.Format( "OOPS: {0}", Url ) );
      }

      /** ------------------------------------------------------------------ **/

      return ( FetchStatus );

    }

    /** Check Include/Exclude URL *********************************************/

    private bool CheckIncludeExcludeUrl ( string Url )
    {

      bool Success = true;

      if( ( this.IncludeExcludeUrls != null ) && ( this.IncludeExcludeUrls.UseIncludeUrlPatterns() ) )
      {
        if( this.IncludeExcludeUrls.MatchesIncludeUrlPattern( Url ) )
        {
          this.DebugMsg( string.Format( "CheckIncludeExcludeUrl: MATCHES INCLUDE URL: {0}", Url ) );
        }
        else
        {
          this.DebugMsg( string.Format( "CheckIncludeExcludeUrl: DOES NOT MATCH INCLUDE URL: {0}", Url ) );
          Success = false;
        }
      }

      if( ( this.IncludeExcludeUrls != null ) && ( this.IncludeExcludeUrls.UseExcludeUrlPatterns() ) )
      {
        if( this.IncludeExcludeUrls.MatchesExcludeUrlPattern( Url ) )
        {
          this.DebugMsg( string.Format( "CheckIncludeExcludeUrl: MATCHES EXCLUDE URL: {0}", Url ) );
          Success = false;
        }
        else
        {
          this.DebugMsg( string.Format( "CheckIncludeExcludeUrl: DOES NOT MATCH EXCLUDE URL: {0}", Url ) );
        }
      }

      return ( Success );

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

  }

}
