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

      this.SuppressDebugMsg = false;

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

    public void Execute ()
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

          string Url = this.JobMaster.GetUrlQueueItem();

          if( Url != null )
          {
            if( !this.CheckIncludeExcludeUrl( Url ) )
            {
              Url = null;
            }
          }

          if( Url != null )
          {

            if(
              !MacroscopePreferencesManager.GetCrawlParentDirectories()
              && !MacroscopePreferencesManager.GetCrawlChildDirectories()
              && Url != this.JobMaster.GetStartUrl() )
            {
              Url = null;
            }
            else
            if(
              !MacroscopePreferencesManager.GetCrawlParentDirectories()
              || !MacroscopePreferencesManager.GetCrawlChildDirectories() )
            {

              DebugMsg( string.Format( "Running Parent/Child Check: {0}", Url ) ); 

              if( 
                MacroscopePreferencesManager.GetCrawlParentDirectories()
                && ( Url != null ) )
              {
                if( !this.JobMaster.IsWithinParentDirectory( Url ) )
                {
                  Url = null;
                }
              }
              
              if( 
                MacroscopePreferencesManager.GetCrawlChildDirectories()
                && ( Url != null ) )
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

          if( Url != null )
          {

            DebugMsg( string.Format( "Execute: {0}", Url ) );

            int Tries = MacroscopePreferencesManager.GetMaxRetries();

            do
            {
              
              DebugMsg( string.Format( "Trying Fetch: {0} :: {1}", Tries, Url ) );
              
              if( 
                ( this.Fetch( Url ) == MacroscopeConstants.FetchStatus.ERROR )
                || ( this.Fetch( Url ) == MacroscopeConstants.FetchStatus.ERROR ) )
              {
                DebugMsg( string.Format( "Fetch Failed: {0} :: {1}", Tries, Url ) );
                Thread.Sleep( 1000 );
              }
              else
              {
                this.JobMaster.NotifyWorkersFetched( Url );
                break;
              }
              
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

    private Boolean CheckIncludeExcludeUrl ( string Url )
    {

      Boolean Success = true;

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

      return( Success );

    }

    /**************************************************************************/

    private MacroscopeConstants.FetchStatus Fetch ( string Url )
    {

      MacroscopeDocument msDoc = this.DocCollection.GetDocument( Url );
      MacroscopeConstants.FetchStatus FetchStatus = MacroscopeConstants.FetchStatus.VOID;

      if( msDoc != null )
      {

        if( msDoc.GetAuthenticationRealm() != null )
        {
          if( msDoc.GetAuthenticationType() == MacroscopeConstants.AuthenticationType.BASIC )
          {

            MacroscopeCredential Credential = this.JobMaster.GetCredentialsHttp().GetCredential(
                                                msDoc.GetHostname(),
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

      if( !this.JobMaster.GetRobots().ApplyRobotRule( Url ) )
      {

        DebugMsg( string.Format( "Disallowed by robots.txt: {0}", Url ) );

        this.JobMaster.AddToBlockedByRobots( Url );

        FetchStatus = MacroscopeConstants.FetchStatus.ROBOTS_DISALLOWED;

        msDoc.SetFetchStatus( MacroscopeConstants.FetchStatus.ROBOTS_DISALLOWED );

        this.JobMaster.VisitedHistoryItem( msDoc.GetUrl() );
                
      }
      else
      {
        this.JobMaster.RemoveFromBlockedByRobots( Url );
      }

      this.JobMaster.AddHistoryItem( Url );

      if( this.AllowedHosts.IsExternalUrl( Url ) )
      {
        DebugMsg( string.Format( "IsExternalUrl: {0}", Url ) );
        msDoc.SetIsExternal( true );
      }

      if( this.DocCollection.ContainsDocument( Url ) )
      {
        if( !this.DocCollection.GetDocument( Url ).GetIsDirty() )
        {
          FetchStatus = MacroscopeConstants.FetchStatus.ALREADY_SEEN;
          return( FetchStatus );
        }
      }

      if( this.JobMaster.GetDepth() > 0 )
      {
        int Depth = MacroscopeUrlUtils.FindUrlDepth( Url );
        if( Depth > this.JobMaster.GetDepth() )
        {
          DebugMsg( string.Format( "TOO DEEP: {0}", Depth ) );
          FetchStatus = MacroscopeConstants.FetchStatus.SKIPPED;
          return( FetchStatus );
        }
      }

      if( msDoc.Execute() )
      {

        this.DocCollection.AddDocument( Url, msDoc );

        if( msDoc.GetStatusCode() == HttpStatusCode.Unauthorized )
        {
          if( msDoc.GetAuthenticationType() == MacroscopeConstants.AuthenticationType.BASIC )
          {
            MacroscopeCredentialsHttp CredentialsHttp = this.JobMaster.GetCredentialsHttp();
            CredentialsHttp.EnqueueCredentialRequest( msDoc.GetHostname(), msDoc.GetAuthenticationRealm(), msDoc.GetUrl() );
            this.JobMaster.AddUrlQueueItem( msDoc.GetUrl() );
          }
        }

        this.JobMaster.VisitedHistoryItem( msDoc.GetUrl() );

        this.JobMaster.IncPageLimitCount();

        if( msDoc.GetIsRedirect() )
        {

          DebugMsg( string.Format( "REDIRECTION DETECTED GetUrl: {0}", msDoc.GetUrl() ) );
          DebugMsg( string.Format( "REDIRECTION DETECTED From: {0}", msDoc.GetUrlRedirectFrom() ) );

          if( MacroscopePreferencesManager.GetFollowRedirects() )
          {

            string sHostname = msDoc.GetHostname();
            string sHostnameFrom = MacroscopeAllowedHosts.ParseHostnameFromUrl( msDoc.GetUrlRedirectFrom() );
            string sUrlRedirectTo = msDoc.GetUrlRedirectTo();
            string sHostnameTo = MacroscopeAllowedHosts.ParseHostnameFromUrl( sUrlRedirectTo );

            DebugMsg( string.Format( "REDIRECTION DETECTED sUrlRedirectTo: {0}", sUrlRedirectTo ) );
            DebugMsg( string.Format( "REDIRECTION DETECTED sHostnameTo: {0}", sHostnameTo ) );
            
          }

          this.JobMaster.AddUrlQueueItem( msDoc.GetUrlRedirectTo() );

        }
        else
        {

          this.ProcessHrefLangLanguages( msDoc ); // Process Languages from HrefLang

          this.ProcessOutlinks( msDoc ); // Process Outlinks from document

        }

        FetchStatus = MacroscopeConstants.FetchStatus.SUCCESS;

      }
      else
      {
        DebugMsg( string.Format( "EXECUTE FAILED: {0}", Url ) );
        FetchStatus = MacroscopeConstants.FetchStatus.ERROR;
      }

      return( FetchStatus );

    }

    /**************************************************************************/

    private void ProcessHrefLangLanguages ( MacroscopeDocument msDoc )
    {

      string sLocale = msDoc.GetLocale();
      Dictionary<string,MacroscopeHrefLang> HrefLangs = msDoc.GetHrefLangs();

      if( sLocale != null )
      {
        this.JobMaster.AddLocales( sLocale );
      }

      foreach( string KeyLocale in HrefLangs.Keys )
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
        DebugMsg( string.Format( "ProcessOutlinks LISTMODE: {0}", this.JobMaster.GetRunTimeMode() ) );
        if( !MacroscopePreferencesManager.GetScanSitesInList() )
        {
          return;
        }
      }

      foreach( MacroscopeLink Outlink in msDoc.IterateOutlinks() )
      {

        Boolean Proceed = true;

        if( !Outlink.GetDoFollow() )
        {
          continue;
        }

        if( Outlink.GetTargetUrl() == null )
        {
          continue;
        }

        if( this.JobMaster.SeenHistoryItem( Outlink.GetTargetUrl() ) )
        {
          continue;
        }

        /*
				if( !this.AllowedHosts.IsAllowedFromUrl( Outlink.AbsoluteUrl ) ) {
					//DebugMsg( string.Format( "FOREIGN HOST: {0}", Outlink.AbsoluteUrl ) );
					continue;
				}
				*/

        if( this.JobMaster.GetPageLimit() > -1 )
        {
          if( this.JobMaster.GetPageLimitCount() >= this.JobMaster.GetPageLimit() )
          {
            DebugMsg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", this.JobMaster.GetPageLimit(), this.JobMaster.GetPageLimitCount() ) );
            Proceed = false;
          }
        }

        if( Proceed )
        {

          this.JobMaster.AddUrlQueueItem( Outlink.GetTargetUrl() );

        }

      }

    }

    /**************************************************************************/

  }

}
