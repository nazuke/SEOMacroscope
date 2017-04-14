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
using System.Net;
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

    //private EventLog JobMasterLog;
    //private Guid JobGuid;
    //private long JobMasterInstanceCounter = 1;
    
    private MacroscopeConstants.RunTimeMode RunTimeMode;

    private IMacroscopeTaskController TaskController;
    MacroscopeCredentialsHttp CredentialsHttp;
        
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
      MacroscopeConstants.RunTimeMode JobRunTimeMode
    )
    {
      this.SuppressDebugMsg = true;
      this.TaskController = null;
      InitializeJobMaster( JobRunTimeMode: JobRunTimeMode );
    }

    public MacroscopeJobMaster (
      MacroscopeConstants.RunTimeMode JobRunTimeMode,
      IMacroscopeTaskController TaskController
    )
    {
      this.SuppressDebugMsg = true;
      this.TaskController = TaskController;
      InitializeJobMaster( JobRunTimeMode: JobRunTimeMode );
    }

    /**************************************************************************/

    private void InitializeJobMaster ( MacroscopeConstants.RunTimeMode JobRunTimeMode )
    {

      /*
      {
        this.JobMasterLog = new EventLog ();
        this.JobMasterLog.Source = MacroscopeConstants.MainEventLogSourceName;
        this.JobGuid = Guid.NewGuid();
        this.LogEntry( string.Format( "Starting Job" ) );
      }
      */
     
      this.RunTimeMode = JobRunTimeMode;
      
      this.CredentialsHttp = this.TaskController.IGetCredentialsHttp();
      
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
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayLinks );
        this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayHyperlinks );
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

    /** Event Log *************************************************************/
    
    /*
    private void LogEntry ( string Message )
    {

      string [] Messages = {
        string.Format(
          "{0} :: {1}",
          this.JobGuid,
          Message
        )
      };

      EventInstance ev = new EventInstance ( JobMasterInstanceCounter, 1 );

      this.JobMasterLog.WriteEvent( ev, Messages );

      this.JobMasterInstanceCounter++;

    }
    */
   
    /** Runtime Mode **********************************************************/

    public void SetRunTimeMode ( MacroscopeConstants.RunTimeMode JobRunTimeMode )
    {
      this.RunTimeMode = JobRunTimeMode;
    }

    public MacroscopeConstants.RunTimeMode GetRunTimeMode ()
    {
      return( this.RunTimeMode );
    }

    /** Credentials **********************************************************/

    public MacroscopeCredentialsHttp GetCredentialsHttp ()
    {
      return( this.CredentialsHttp );
    }

    /** Include/Exclude URLs **************************************************/

    public void SetIncludeExcludeUrls ( MacroscopeIncludeExcludeUrls IncludeExcludeUrls )
    {
      this.IncludeExcludeUrls = IncludeExcludeUrls;
    }

    public MacroscopeIncludeExcludeUrls GetIncludeExcludeUrls ()
    {
      return( this.IncludeExcludeUrls );
    }

    /** Execute Job ***********************************************************/

    public Boolean Execute ()
    {

      DebugMsg( string.Format( "Start URL: {0}", this.StartUrl ) );

      //this.LogEntry( string.Format( "Executing with Start URL: {0}", this.StartUrl ) );

      this.StartUrl = MacroscopeUrlUtils.SanitizeUrl( this.StartUrl );
      
      this.DetermineStartingDirectory();
      
      this.SetThreadsStop( false );

      this.AllowedHosts.AddFromUrl( this.StartUrl );

      if( !this.PeekUrlQueue() )
      {

        string RobotsUrl = MacroscopeRobots.GenerateRobotUrl( Url: this.StartUrl );

        if( !string.IsNullOrEmpty( RobotsUrl ) )
        {
          this.AddUrlQueueItem( RobotsUrl );
        }

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

    private void SpawnWorkers ()
    {

      Boolean DoRun = true;

      while( DoRun == true )
      {

        if( this.GetThreadsStop() )
        {

          DebugMsg( string.Format( "SpawnWorkers: {0}", "STOPPING" ) );

          DoRun = false;
          break;

        }
        else
        {

          if( this.CountRunningThreads() < this.ThreadsMax )
          {

            SemaphoreWorkers.WaitOne();

            Boolean NewThreadStarted = ThreadPool.QueueUserWorkItem( this.StartWorker, null );

            if( NewThreadStarted )
            {
              Thread.Sleep( 100 );
            }

            this.AdjustThreadsMax();

          }

          if(
            ( this.CountRunningThreads() == 0 )
            && ( !this.PeekUrlQueue() ) )
          {
            DoRun = false;
          }

        }

      }

      this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();

      DebugMsg( string.Format( "SpawnWorkers: STOPPED" ) );

    }

    /** -------------------------------------------------------------------- **/

    private void StartWorker ( object thContext )
    {

      if( !this.GetThreadsStop() )
      {

        MacroscopeJobWorker JobWorker = new MacroscopeJobWorker ( this );

        this.IncRunningThreads();

        JobWorker.Execute();

      }

      SemaphoreWorkers.Release( 1 );

    }

    /** -------------------------------------------------------------------- **/

    public void NotifyWorkersFetched ( string Url )
    {
      DebugMsg( string.Format( "NotifyWorkersFetched: {0}", Url ) );
      this.PagesFound++;
      this.AddUpdateDisplayQueue( Url );
      this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
      this.UpdateProgress( Url, true );
    }

    /** -------------------------------------------------------------------- **/

    public void NotifyWorkersDone ()
    {
      this.DecRunningThreads();
      this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
    }

    /** -------------------------------------------------------------------- **/

    public void StopWorkers ()
    {
      this.SetThreadsStop( true );
    }

    /** -------------------------------------------------------------------- **/

    public Boolean WorkersStopped ()
    {

      Boolean IsStopped = false;

      if( this.CountRunningThreads() == 0 )
      {
        IsStopped = true;
      }

      this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();

      return( IsStopped );

    }

    /** Track Thread Count ****************************************************/

    private void SetThreadsStop ( Boolean Stopped )
    {
      this.ThreadsStop = Stopped;
    }

    /** -------------------------------------------------------------------- **/

    public Boolean GetThreadsStop ()
    {
      return( this.ThreadsStop );
    }

    /** -------------------------------------------------------------------- **/

    private void AdjustThreadsMax ()
    {
      ThreadsMax = MacroscopePreferencesManager.GetMaxThreads();
    }

    /** -------------------------------------------------------------------- **/

    private void IncRunningThreads ()
    {
      int iThreadId = Thread.CurrentThread.ManagedThreadId;
      this.ThreadsDict[ iThreadId ] = true;
      this.ThreadsRunning++;
    }

    /** -------------------------------------------------------------------- **/

    private void DecRunningThreads ()
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

    /** -------------------------------------------------------------------- **/

    public int CountRunningThreads ()
    {
      return( this.ThreadsRunning );
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

    /** -------------------------------------------------------------------- **/

    public void AddUpdateDisplayQueue ( string Url )
    {
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayQueue, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayStructure, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayHierarchy, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayCanonicalAnalysis, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayHrefLang, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayErrors, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayRedirectsAudit, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayLinks, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayHyperlinks, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayUriAnalysis, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageTitles, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageDescriptions, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageKeywords, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageHeadings, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayStylesheets, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayImages, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayJavascripts, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayAudios, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayVideos, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplaySitemaps, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayEmailAddresses, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayTelephoneNumbers, Url );
      NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayHostnames, Url );
    }

    /** -------------------------------------------------------------------- **/

    public List<string> DrainDisplayQueueAsList ( string NamedQueueName )
    {
      return( this.NamedQueue.DrainNamedQueueItemsAsList( NamedQueueName ) );
    }

    /** URL Queue *************************************************************/

    public List<string> GetUrlQueueAsList ()
    {
      return( this.NamedQueue.GetNamedQueueItemsAsList( MacroscopeConstants.NamedQueueUrlList ) );
    }

    /** -------------------------------------------------------------------- **/

    public void AddUrlQueueItem ( string Url )
    {
      
      if( !this.SeenHistoryItem( Url ) )
      {
        this.NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueUrlList, Url );
      }

      this.AddToProgress( Url );

    }

    /** -------------------------------------------------------------------- **/

    public string GetUrlQueueItem ()
    {
      return( this.NamedQueue.GetNamedQueueItem( MacroscopeConstants.NamedQueueUrlList ) );
    }

    /** -------------------------------------------------------------------- **/

    public List<string> DrainUrlQueueAsList ()
    {
      return( this.NamedQueue.DrainNamedQueueItemsAsList( MacroscopeConstants.NamedQueueUrlList, 5 ) );
    }

    /** -------------------------------------------------------------------- **/

    public Boolean PeekUrlQueue ()
    {
      Boolean bPeek = this.NamedQueue.PeekNamedQueue( MacroscopeConstants.NamedQueueUrlList );
      return( bPeek );
    }

    /** -------------------------------------------------------------------- **/

    public int CountUrlQueueItems ()
    {
      return( this.NamedQueue.CountNamedQueueItems( MacroscopeConstants.NamedQueueUrlList ) );
    }

    /** Retry Broken Links ****************************************************/

    public void RetryBrokenLinks ()
    {

      foreach( MacroscopeDocument msDoc in this.DocCollection.IterateDocuments() )
      {

        string Url = msDoc.GetUrl();

        switch( msDoc.GetStatusCode() )
        {

        // Bogus Range

          case 0:
            this.ResetLink( Url );
            break;

        // 200 Range

          case HttpStatusCode.OK:
            break;

        // 400 Range

          case HttpStatusCode.BadRequest:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.Unauthorized:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.Forbidden:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.NotFound:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.Gone:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.RequestTimeout:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.RequestUriTooLong:
            this.ResetLink( Url );
            break;

        // 500 Range

          case HttpStatusCode.InternalServerError:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.NotImplemented:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.BadGateway:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.ServiceUnavailable:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.GatewayTimeout:
            this.ResetLink( Url );
            break;

        // Default

          default:
            break;

        }

      }

    }

    /** -------------------------------------------------------------------- **/

    public void RetryTimedOutLinks ()
    {
      foreach( MacroscopeDocument msDoc in this.DocCollection.IterateDocuments() )
      {
        string Url = msDoc.GetUrl();
        switch( msDoc.GetStatusCode() )
        {
          case HttpStatusCode.RequestTimeout:
            this.ResetLink( Url );
            break;
          case HttpStatusCode.GatewayTimeout:
            this.ResetLink( Url );
            break;
          default:
            break;
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public void RetryLink ( string Url )
    {
      this.ResetLink( Url );
    }

    private void ResetLink ( string Url )
    {
      MacroscopeDocument msDoc = this.DocCollection.GetDocument( Url );
      if( msDoc != null )
      {
        msDoc.SetIsDirty();
        this.ResetHistoryItem( Url );
        this.AddUrlQueueItem( Url );
      }
      else
      {
        DebugMsg( string.Format( "ResetLink ERROR: {0}", Url ) );
      }
    }

    /** Start URL *************************************************************/

    public void SetStartUrl ( string Url )
    {
      this.StartUrl = Url;
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

    public void SetGetDepth ( int Value )
    {
      this.Depth = Value;
    }

    /** Page Limit ************************************************************/

    public int GetPageLimit ()
    {
      return( this.PageLimit );
    }

    /** Page Limit Count ******************************************************/

    public void SetPageLimitCount ( int Value )
    {
      this.PageLimitCount = Value;
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

    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/

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

    public void AddHistoryItem ( string Url )
    {
      if( !this.History.ContainsKey( Url ) )
      {
        lock( this.History )
        {
          this.History.Add( Url, false );
        }
      }
    }
    
    /** -------------------------------------------------------------------- **/

    public void VisitedHistoryItem ( string Url )
    {
      if( this.History.ContainsKey( Url ) )
      {
        lock( this.History )
        {
          this.History[ Url ] = true;
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public void ResetHistoryItem ( string Url )
    {
      if( this.History.ContainsKey( Url ) )
      {
        lock( this.History )
        {
          this.History[ Url ] = false;
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public Boolean SeenHistoryItem ( string Url )
    {
      Boolean bSeen = false;
      if( this.History.ContainsKey( Url ) )
      {
        bSeen = this.History[ Url ];
      }
      return( bSeen );
    }

    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/

    public void ClearHistory ()
    {
      lock( this.History )
      {
        this.History.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/

    public void UpdateProgress ( string Url, Boolean State )
    {

      if( State && this.AllowedHosts.IsAllowedFromUrl( Url ) )
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
              this.Progress[ "done" ][ Url ] = true;
            }
            else
            {
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
        DebugMsg( string.Format( "UpdateProgress: EXTERNAL: {0} :: {1}", State, Url ) );
      }

    }

    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/

    public void AddLocales ( string Locale )
    {
      lock( this.Locales )
      {
        if( !this.Locales.ContainsKey( Locale ) )
        {
          this.Locales[ Locale ] = Locale;
        }
      }
    }

    /** Robots ****************************************************************/

    public MacroscopeRobots GetRobots ()
    {
      return( this.Robots );
    }

    /** -------------------------------------------------------------------- **/

    private void SetCrawlDelay ( string Url )
    {
      this.CrawlDelay = this.Robots.GetCrawlDelay( Url );
    }

    /** -------------------------------------------------------------------- **/

    public int GetCrawlDelay ()
    {
      return( this.CrawlDelay );
    }

    /** -------------------------------------------------------------------- **/

    public void ProbeRobotsFile ( string Url )
    {
      if( MacroscopePreferencesManager.GetFollowSitemapLinks() )
      {
        List<string> SitemapList = Robots.GetSitemapsAsList( Url );
        if( SitemapList.Count > 0 )
        {
          for( int i = 0 ; i < SitemapList.Count ; i++ )
          {
            this.AddUrlQueueItem( SitemapList[ i ] );
          }
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public void AddToBlockedByRobots ( string Url )
    {
      lock( this.BlockedByRobots )
      {
        if( !this.BlockedByRobots.ContainsKey( Url ) )
        {
          this.BlockedByRobots[ Url ] = true;
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public void RemoveFromBlockedByRobots ( string Url )
    {
      lock( this.BlockedByRobots )
      {
        if( this.BlockedByRobots.ContainsKey( Url ) )
        {
          this.BlockedByRobots.Remove( Url );
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string,Boolean> GetBlockedByRobotsList ()
    {
      Dictionary<string,Boolean> DicCopy = new Dictionary<string,Boolean> ();
      lock( this.BlockedByRobots )
      {
        foreach( string Url in this.BlockedByRobots.Keys )
        {
          DicCopy.Add( Url, this.BlockedByRobots[ Url ] );
        }
      }
      return( DicCopy );
    }

    /**************************************************************************/

  }

}
