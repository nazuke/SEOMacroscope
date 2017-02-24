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
using System.Text.RegularExpressions;
using System.Threading;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeJobMaster.
  /// </summary>

  public class MacroscopeJobMaster : Macroscope
  {

    /**************************************************************************/

    private MacroscopeConstants.RunTimeMode RuntimeMode;

    private IMacroscopeTaskController TaskController;

    private MacroscopeDocumentCollection DocCollection;
    private MacroscopeAllowedHosts AllowedHosts;
    private MacroscopeNamedQueue NamedQueue;
    private MacroscopeRobots Robots;
    private MacroscopeIncludeExcludeUrls IncludeExcludeUrls;

    private int CrawlDelay;
    private int ThreadsMax;
    private int ThreadsRunning;
    private Boolean ThreadsStop;
    private object ThreadsLock = new object ();
    private Dictionary<int,Boolean> ThreadsDict;

    private Semaphore SemaphoreWorkers;

    private string StartUrl;
    private int Depth;
    private int PageLimit;
    private int PageLimitCount;

    private int PagesFound;

    private string ParentStartingDirectory;
    private string ChildStartingDirectory;

    private Dictionary<string,Boolean> History;

    private Dictionary<string,Dictionary<string,Boolean>> Progress;

    private Dictionary<string,string> Locales;

    private Dictionary<string,Boolean> BlockedByRobots;

    /**************************************************************************/

    public MacroscopeJobMaster (
      MacroscopeConstants.RunTimeMode RuntimeMode
    )
    {
      this.SuppressDebugMsg = false;
      this.TaskController = null;
      InitializeJobMaster( RuntimeMode );
    }

    public MacroscopeJobMaster (
      MacroscopeConstants.RunTimeMode RuntimeMode,
      IMacroscopeTaskController TaskController
    )
    {
      this.SuppressDebugMsg = false;
      this.TaskController = TaskController;
      InitializeJobMaster( RuntimeMode );
    }

    /**************************************************************************/

    void InitializeJobMaster ( MacroscopeConstants.RunTimeMode iRuntimeMode )
    {

      this.RuntimeMode = iRuntimeMode;

      this.DocCollection = new MacroscopeDocumentCollection ( this );
      this.AllowedHosts = new MacroscopeAllowedHosts ();

      // BEGIN: Named Queues
      this.NamedQueue = new MacroscopeNamedQueue ();
      {
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueUrlList );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayStructure );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayHierarchy );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayCanonicalAnalysis );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayHrefLang );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayErrors );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayRedirectsAudit );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayUriAnalysis );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayPageTitles );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayPageDescriptions );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayPageKeywords );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayPageHeadings );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayStylesheets );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayImages );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayJavascripts );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayAudios );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayVideos );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplaySitemaps );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayEmailAddresses );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayTelephoneNumbers );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayHostnames );
      }
      // END: Named Queues

      this.CrawlDelay = 0;

      this.AdjustThreadsMax();
      this.ThreadsRunning = 0;
      this.ThreadsStop = false;
      this.ThreadsDict = new Dictionary<int,Boolean> ();

      this.SemaphoreWorkers = new Semaphore ( 0, this.ThreadsMax );
      this.SemaphoreWorkers.Release( this.ThreadsMax );

      this.Depth = MacroscopePreferencesManager.GetDepth();
      this.PageLimit = MacroscopePreferencesManager.GetPageLimit();
      this.PageLimitCount = 0;

      this.PagesFound = 0;

      {
        this.ParentStartingDirectory = "";
        this.ChildStartingDirectory = "";
      }

      this.History = new Dictionary<string, bool> ( 4096 );

      this.InitProgress();

      this.Locales = new Dictionary<string,string> ( 32 );

      this.Robots = new MacroscopeRobots ();
      this.BlockedByRobots = new Dictionary<string,Boolean> ();

    }

    /**************************************************************************/

    ~MacroscopeJobMaster ()
    {
      DebugMsg( string.Format( "MacroscopeJobMaster: {0}", "DESTRUCTOR" ) );
      this.DocCollection = null;
      this.SemaphoreWorkers.Dispose();
    }

    /** Runtime Mode **********************************************************/

    public void SetRuntimeMode ( MacroscopeConstants.RunTimeMode iRuntimeMode )
    {
      this.RuntimeMode = iRuntimeMode;
    }

    public MacroscopeConstants.RunTimeMode GetRuntimeMode ()
    {
      return( this.RuntimeMode );
    }

    /** Include/Exclude URLs **************************************************/

    public void SetIncludeExcludeUrls ( MacroscopeIncludeExcludeUrls IncludeExcludeUrlsNew )
    {
      this.IncludeExcludeUrls = IncludeExcludeUrlsNew;
    }

    public MacroscopeIncludeExcludeUrls GetIncludeExcludeUrls ()
    {
      return( this.IncludeExcludeUrls );
    }

    /** Execute Job ***********************************************************/

    public Boolean Execute ()
    {

      DebugMsg( string.Format( "Start URL: {0}", this.StartUrl ) );

      this.StartUrl = MacroscopeUrlTools.SanitizeUrl( this.StartUrl );
      
      this.DetermineStartingDirectory();
      
      this.SetThreadsStop( false );

      this.AllowedHosts.AddFromUrl( this.StartUrl );

      if( !this.PeekUrlQueue() )
      {
        this.AddUrlQueueItem( this.StartUrl );
      }

      {
        this.ProbeRobotsFile( this.StartUrl );
        this.SetCrawlDelay( this.StartUrl );
      }

      this.SpawnWorkers();

      DebugMsg( string.Format( "Pages Found: {0}", this.GetPagesFound() ) );

      if( this.TaskController != null )
      {
        this.TaskController.ICallbackScanComplete();
      }

      this.AddUpdateDisplayQueue( this.StartUrl );

      return( true );

    }

    /** Manage Workers ********************************************************/

    void SpawnWorkers ()
    {

      Boolean bDoRun = true;

      while( bDoRun == true )
      {

        if( this.GetThreadsStop() )
        {

          DebugMsg( string.Format( "SpawnWorkers: {0}", "STOPPING" ) );

          bDoRun = false;
          break;

        }
        else
        {

          if( this.CountRunningThreads() < this.ThreadsMax )
          {

            SemaphoreWorkers.WaitOne();

            DebugMsg( string.Format( "SpawnWorkers THREADS: {0} :: {1}", this.ThreadsMax, this.CountRunningThreads() ) );

            Boolean bNewThread = ThreadPool.QueueUserWorkItem( this.StartWorker, null );

            if( bNewThread )
            {
              Thread.Sleep( 100 );
            }

            this.AdjustThreadsMax();

          }

          if(
            ( this.CountRunningThreads() == 0 )
            && ( !this.PeekUrlQueue() ) )
          {
            bDoRun = false;
          }

        }

      }

      this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();

      DebugMsg( string.Format( "SpawnWorkers: STOPPED" ) );

    }

    void StartWorker ( object thContext )
    {
      if( !this.GetThreadsStop() )
      {
        MacroscopeJobWorker JobWorker = new MacroscopeJobWorker ( this );
        this.IncRunningThreads();
        JobWorker.Execute();
      }
      SemaphoreWorkers.Release( 1 );
    }

    public void NotifyWorkersFetched ( string Url )
    {
      DebugMsg( string.Format( "NotifyWorkersFetched: {0}", Url ) );
      this.PagesFound++;
      this.AddUpdateDisplayQueue( Url );
      this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
      this.UpdateProgress( Url, true );
    }

    public void NotifyWorkersDone ()
    {
      this.DecRunningThreads();
      this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
    }

    public void StopWorkers ()
    {
      this.SetThreadsStop( true );
    }

    public Boolean WorkersStopped ()
    {
      Boolean bIsStopped = false;
      int iThreadCount = this.CountRunningThreads();
      if( iThreadCount == 0 )
      {
        bIsStopped = true;
      }
      this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
      return( bIsStopped );
    }

    /** Track Thread Count ****************************************************/

    void SetThreadsStop ( Boolean bState )
    {
      this.ThreadsStop = bState;
    }

    public Boolean GetThreadsStop ()
    {
      return( this.ThreadsStop );
    }

    void AdjustThreadsMax ()
    {
      ThreadsMax = MacroscopePreferencesManager.GetMaxThreads();
    }

    void IncRunningThreads ()
    {
      int iThreadId = Thread.CurrentThread.ManagedThreadId;
      this.ThreadsDict[ iThreadId ] = true;
      this.ThreadsRunning++;
    }

    void DecRunningThreads ()
    {
      if( this.ThreadsRunning > 0 )
      {
        int iThreadId = Thread.CurrentThread.ManagedThreadId;
        if( this.ThreadsDict.ContainsKey( iThreadId ) )
        {
          lock( this.ThreadsDict )
          {
            this.ThreadsDict.Remove( iThreadId );
          }
        }
        this.ThreadsRunning--;
      }
    }

    public int CountRunningThreads ()
    {
      int iRunningThreads = 0;
      iRunningThreads = this.ThreadsRunning;
      return( iRunningThreads );
    }

    /** Queues ****************************************************************/

    public void ClearAllQueues ()
    {
      this.NamedQueue.ClearAllNamedQueues();
    }

    /** Display Queue *********************************************************/

    public Boolean PeekUpdateDisplayQueue ()
    {
      return( NamedQueue.PeekNamedQueue( MacroscopeConstants.NamedQueueDisplayQueue ) );
    }

    public void AddUpdateDisplayQueue ( string sUrl )
    {
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayQueue, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayStructure, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayHierarchy, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayCanonicalAnalysis, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayHrefLang, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayErrors, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayRedirectsAudit, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayUriAnalysis, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageTitles, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageDescriptions, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageKeywords, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageHeadings, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayStylesheets, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayImages, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayJavascripts, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayAudios, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayVideos, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplaySitemaps, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayEmailAddresses, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayTelephoneNumbers, sUrl );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayHostnames, sUrl );
    }

    public List<string> DrainDisplayQueueAsList ( string sNamedQueueName )
    {
      return( this.NamedQueue.DrainNamedQueueItemsAsList( sNamedQueueName ) );
    }

    /** URL Queue *************************************************************/

    public List<string> GetUrlQueueAsList ()
    {
      return( this.NamedQueue.GetNamedQueueItemsAsList( MacroscopeConstants.NamedQueueUrlList ) );
    }

    public void AddUrlQueueItem ( string Url )
    {
      
      if( !this.SeenHistoryItem( Url ) )
      {
        this.NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueUrlList, Url );
      }

      this.AddToProgress( Url );

    }

    public string GetUrlQueueItem ()
    {
      return( this.NamedQueue.GetNamedQueueItem( MacroscopeConstants.NamedQueueUrlList ) );
    }

    public List<string> DrainUrlQueueAsList ()
    {
      return( this.NamedQueue.DrainNamedQueueItemsAsList( MacroscopeConstants.NamedQueueUrlList, 5 ) );
    }

    public Boolean PeekUrlQueue ()
    {
      Boolean bPeek = this.NamedQueue.PeekNamedQueue( MacroscopeConstants.NamedQueueUrlList );
      return( bPeek );
    }

    public int CountUrlQueueItems ()
    {
      return( this.NamedQueue.CountNamedQueueItems( MacroscopeConstants.NamedQueueUrlList ) );
    }

    /** Retry Broken Links ****************************************************/

    public void RetryBrokenLinks ()
    {

      foreach( MacroscopeDocument msDoc in this.DocCollection.IterateDocuments() )
      {

        string sUrl = msDoc.GetUrl();

        switch( msDoc.GetStatusCode() )
        {

        // Bogus Range

          case 0:
            this.ResetLink( sUrl );
            break;

        // 200 Range

          case 200:
            break;

        // 400 Range

          case 400:
            this.ResetLink( sUrl );
            break;
          case 403:
            this.ResetLink( sUrl );
            break;
          case 404:
            this.ResetLink( sUrl );
            break;
          case 410:
            this.ResetLink( sUrl );
            break;
          case 408:
            this.ResetLink( sUrl );
            break;
          case 429:
            this.ResetLink( sUrl );
            break;
          case 451:
            this.ResetLink( sUrl );
            break;

        // 500 Range

          case 500:
            this.ResetLink( sUrl );
            break;
          case 501:
            this.ResetLink( sUrl );
            break;
          case 502:
            this.ResetLink( sUrl );
            break;
          case 503:
            this.ResetLink( sUrl );
            break;
          case 504:
            this.ResetLink( sUrl );
            break;

        // Default

          default:
            break;

        }

      }

    }

    public void RetryLink ( string sUrl )
    {
      this.ResetLink( sUrl );
    }

    void ResetLink ( string sUrl )
    {

      MacroscopeDocument msDoc = this.DocCollection.GetDocument( sUrl );

      if( msDoc != null )
      {

        msDoc.SetIsDirty();

        this.ResetHistoryItem( sUrl );

        this.AddUrlQueueItem( sUrl );

      }
      else
      {

        DebugMsg( string.Format( "ResetLink ERROR: {0}", sUrl ) );

      }

    }

    /** Start URL *************************************************************/

    public void SetStartUrl ( string sUrl )
    {
      this.StartUrl = sUrl;
    }

    public string GetStartUrl ()
    {
      return( this.StartUrl );
    }

    /** Page Depth ************************************************************/

    public int GetDepth ()
    {
      return( this.Depth );
    }

    public void SetGetDepth ( int iValue )
    {
      this.Depth = iValue;
    }

    /** Page Limit ************************************************************/

    public int GetPageLimit ()
    {
      return( this.PageLimit );
    }

    /** Page Limit Count ******************************************************/

    public void SetPageLimitCount ( int iValue )
    {
      this.PageLimitCount = iValue;
    }

    public int GetPageLimitCount ()
    {
      return( this.PageLimitCount );
    }

    public void IncPageLimitCount ()
    {
      this.PageLimitCount++;
    }

    public int GetPagesFound ()
    {
      return( this.PagesFound );
    }

    /** Crawl Parent / Child Directories **************************************/

    public void DetermineStartingDirectory ()
    {
      
      Uri StartUri = new Uri ( this.GetStartUrl() );
      string Path = StartUri.AbsolutePath;

      Path = Regex.Replace( Path, "/[^/]*$", "/", RegexOptions.IgnoreCase );

      if( Path.Length == 0 )
      {
        Path = "/";
      }

      this.ParentStartingDirectory = string.Join(
        "",
        StartUri.Scheme,
        "://",
        StartUri.Host,
        Path
      );


      this.ChildStartingDirectory = string.Join(
        "",
        StartUri.Scheme,
        "://",
        StartUri.Host,
        Path
      );

    }

    public Boolean IsWithinParentDirectory ( string Url )
    {

      Boolean IsWithin = false;
      Uri CurrentUri = new Uri ( Url );

      if(
        ( CurrentUri.Scheme.ToLower() == "http" )
        || ( CurrentUri.Scheme.ToLower() == "https" ) )
      {

        string Path = CurrentUri.AbsolutePath;
        Path = Regex.Replace( Path, "/[^/]*$", "/", RegexOptions.IgnoreCase );
        if( Path.Length == 0 )
        {
          Path = "/";
        }

        string CurrentUriString = string.Join(
                                    "",
                                    CurrentUri.Scheme,
                                    "://",
                                    CurrentUri.Host,
                                    Path
                                  );

        int ParentStartingDirectoryLength = this.ParentStartingDirectory.Length;
        int CurrentUriStringLength = CurrentUriString.Length;

        if( ParentStartingDirectoryLength >= CurrentUriStringLength )
        {
          if( this.ParentStartingDirectory.StartsWith( CurrentUriString, StringComparison.Ordinal ) )
          {
            IsWithin = true;
          }

        }

      }

      return( IsWithin );
      
    }

    public Boolean IsWithinChildDirectory ( string Url )
    {
      
      Boolean IsWithin = false;
      Uri CurrentUri = new Uri ( Url );

      if(
        ( CurrentUri.Scheme.ToLower() == "http" )
        || ( CurrentUri.Scheme.ToLower() == "https" ) )
      {

        string Path = CurrentUri.AbsolutePath;
        Path = Regex.Replace( Path, "/[^/]*$", "/", RegexOptions.IgnoreCase );
        if( Path.Length == 0 )
        {
          Path = "/";
        }

        string CurrentUriString = string.Join(
                                    "",
                                    CurrentUri.Scheme,
                                    "://",
                                    CurrentUri.Host,
                                    Path
                                  );

        int ChildStartingDirectoryLength = this.ChildStartingDirectory.Length;
        int CurrentUriStringLength = CurrentUriString.Length;
        
        if( CurrentUriStringLength >= ChildStartingDirectoryLength )
        {
          if( CurrentUriString.StartsWith( this.ChildStartingDirectory, StringComparison.Ordinal ) )
          {
            IsWithin = true;
          }

        }
        
      }

      return( IsWithin );
      
    }

    /** History ***************************************************************/

    public void AddHistoryItem ( string sUrl )
    {
      if( !this.History.ContainsKey( sUrl ) )
      {
        lock( this.History )
        {
          this.History.Add( sUrl, false );
        }
      }
    }
    
    public void VisitedHistoryItem ( string sUrl )
    {
      if( this.History.ContainsKey( sUrl ) )
      {
        lock( this.History )
        {
          this.History[ sUrl ] = true;
        }
      }
    }

    public void ResetHistoryItem ( string sUrl )
    {
      if( this.History.ContainsKey( sUrl ) )
      {
        lock( this.History )
        {
          this.History[ sUrl ] = false;
        }
      }
    }

    public Boolean SeenHistoryItem ( string sUrl )
    {
      Boolean bSeen = false;
      if( this.History.ContainsKey( sUrl ) )
      {
        bSeen = this.History[ sUrl ];
      }
      return( bSeen );
    }

    public Dictionary<string,Boolean> GetHistory ()
    {
      Dictionary<string,Boolean> HistoryCopy = new Dictionary<string,Boolean> ( this.History.Count );
      lock( this.History )
      {
        foreach( string sKey in this.History.Keys )
        {
          HistoryCopy.Add( sKey, this.History[ sKey ] );
        }
      }
      return( HistoryCopy );
    }

    public void ClearHistory ()
    {
      lock( this.History )
      {
        this.History.Clear();
      }
    }

    public int CountHistory ()
    {
      return( this.History.Count );
    }

    /** Progress **************************************************************/

    private void InitProgress ()
    {
      this.Progress = new Dictionary<string,Dictionary<string,Boolean>> ();
      this.Progress.Add( "list", new Dictionary<string,Boolean> ( 4096 ) );
      this.Progress.Add( "done", new Dictionary<string,Boolean> ( 4096 ) );
      this.Progress.Add( "wait", new Dictionary<string,Boolean> ( 4096 ) );
    }

    public void AddToProgress ( string Url )
    {

      if( this.AllowedHosts.IsAllowedFromUrl( Url ) )
      {
        DebugMsg( string.Format( "AddToProgress: INTERNAL: {0}", Url ) );

        lock( this.Progress )
        {
          
          if( !this.Progress[ "list" ].ContainsKey( Url ) )
          {

            this.Progress[ "list" ].Add( Url, false );

            if( !this.Progress[ "wait" ].ContainsKey( Url ) )
            {
              this.Progress[ "wait" ].Add( Url, true );
            }
            
          }

        }

      }
      else
      {
        DebugMsg( string.Format( "AddToProgress: EXTERNAL: {0}", Url ) );
      }

    }

    public void UpdateProgress ( string Url, Boolean bState )
    {

      if( bState && this.AllowedHosts.IsAllowedFromUrl( Url ) )
      {

        DebugMsg( string.Format( "UpdateProgress: INTERNAL: {0}", Url ) );

        lock( this.Progress )
        {

          if( this.Progress[ "list" ].ContainsKey( Url ) )
          {
            
            this.Progress[ "list" ][ Url ] = true;
            
            if( this.Progress[ "wait" ].ContainsKey( Url ) )
            {
              this.Progress[ "wait" ].Remove( Url );
            }

            if( this.Progress[ "done" ].ContainsKey( Url ) )
            {
              this.Progress[ "done" ][Url] = true;
            } else {
              this.Progress[ "done" ].Add( Url, true );
            }

          }
          else
          {

            DebugMsg( string.Format( "UpdateProgress: NOT IN LIST: {0}", Url ) );

          }

        }

      }
      else
      {
        DebugMsg( string.Format( "UpdateProgress: EXTERNAL: {0} :: {1}", bState, Url ) );
      }

    }

    public List<decimal> GetProgress ()
    {
      List<decimal> Counts = new List<decimal> ( 3 );
      lock( this.Progress )
      {
        Counts.Add( this.Progress[ "list" ].Count );
        Counts.Add( this.Progress[ "done" ].Count );
        Counts.Add( this.Progress[ "wait" ].Count );
      }
      return( Counts );
    }

    /** Document Collection ***************************************************/

    public MacroscopeDocumentCollection GetDocCollection ()
    {
      return( this.DocCollection );
    }

    /** Allowed Hosts *********************************************************/

    public MacroscopeAllowedHosts GetAllowedHosts ()
    {
      return( this.AllowedHosts );
    }

    /** Locales ***************************************************************/

    public Dictionary<string,string> GetLocales ()
    {
      return( this.Locales );
    }

    public void AddLocales ( string sLocale )
    {
      if( !this.Locales.ContainsKey( sLocale ) )
      {
        lock( this.Locales )
        {
          this.Locales[ sLocale ] = sLocale;
        }
      }
    }

    /** Robots ****************************************************************/

    public MacroscopeRobots GetRobots ()
    {
      return( this.Robots );
    }

    void SetCrawlDelay ( string sUrl )
    {
      this.CrawlDelay = this.Robots.GetCrawlDelay( sUrl );
    }

    public int GetCrawlDelay ()
    {
      return( this.CrawlDelay );
    }

    public void ProbeRobotsFile ( string sUrl )
    {
      if( MacroscopePreferencesManager.GetFollowSitemapLinks() )
      {
        List<string> lSitemaps = Robots.GetSitemapsAsList( sUrl );
        if( lSitemaps.Count > 0 )
        {
          for( int i = 0 ; i < lSitemaps.Count ; i++ )
          {
            this.AddUrlQueueItem( lSitemaps[ i ] );
          }
        }
      }
    }

    public void AddToBlockedByRobots ( string sUrl )
    {
      if( !this.BlockedByRobots.ContainsKey( sUrl ) )
      {
        lock( this.BlockedByRobots )
        {
          this.BlockedByRobots[ sUrl ] = true;
        }
      }
    }

    public void RemoveFromBlockedByRobots ( string sUrl )
    {
      if( this.BlockedByRobots.ContainsKey( sUrl ) )
      {
        lock( this.BlockedByRobots )
        {
          this.BlockedByRobots.Remove( sUrl );
        }
      }
    }

    public Dictionary<string,Boolean> GetBlockedByRobotsList ()
    {
      Dictionary<string,Boolean> dicCopy = new Dictionary<string,Boolean> ();
      lock( this.BlockedByRobots )
      {
        foreach( string sUrl in this.BlockedByRobots.Keys )
        {
          dicCopy.Add( sUrl, this.BlockedByRobots[ sUrl ] );
        }
      }
      return( dicCopy );
    }

    /**************************************************************************/

  }

}
