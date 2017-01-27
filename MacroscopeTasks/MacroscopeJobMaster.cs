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
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeJobMaster : Macroscope
	{

		MacroscopeMainForm MainForm;
		MacroscopeDocumentCollection DocCollection;
		MacroscopeAllowedHosts AllowedHosts;
		MacroscopeNamedQueue NamedQueue;
		MacroscopeRobots Robots;

		int ThreadsMax;
		int ThreadsRunning;
		Boolean ThreadsStop;
		object ThreadsLock = new object ();
		Dictionary<int,Boolean> ThreadsDict;

		Semaphore SemaphoreWorkers;
		
		string StartUrl;
		int Depth;
		int PageLimit;
		int PageLimitCount;

		int PagesFound;

		Dictionary<string,Boolean> History;

		Dictionary<string,string> Locales;

		/**************************************************************************/

		public MacroscopeJobMaster ()
		{
			MainForm = null;
			InitializeJobMaster();
		}
		
		public MacroscopeJobMaster ( MacroscopeMainForm MainFormNew )
		{
			MainForm = MainFormNew;
			InitializeJobMaster();
		}

		/**************************************************************************/

		void InitializeJobMaster ()
		{

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
				this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayEmailAddresses );
				this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayTelephoneNumbers );
				this.NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayHostnames );
			}
			// END: Named Queues

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

			this.History = new Dictionary<string, bool> ( 4096 );

			this.Locales = new Dictionary<string,string> ( 32 );
			this.Robots = new MacroscopeRobots ();

		}

		/**************************************************************************/

		~MacroscopeJobMaster ()
		{
			DebugMsg( string.Format( "MacroscopeJobMaster: {0}", "DESTRUCTOR" ) );
			this.DocCollection = null;
			this.SemaphoreWorkers.Dispose();
		}
		
		/** Execute Job ***********************************************************/

		public Boolean Execute ()
		{

			DebugMsg( string.Format( "Start URL: {0}", this.StartUrl ) );

			this.StartUrl = MacroscopeUrlTools.SanitizeUrl( this.StartUrl );

			this.SetThreadsStop( false );

			this.AllowedHosts.AddFromUrl( this.StartUrl );

			if( !this.PeekUrlQueue() ) {
				this.AddUrlQueueItem( this.StartUrl );
			}

			this.SpawnWorkers();
			
			DebugMsg( string.Format( "Pages Found: {0}", this.GetPagesFound() ) );

			if( this.MainForm != null ) {
				this.MainForm.CallbackScanComplete();
			}

			this.AddUpdateDisplayQueue( this.StartUrl );
						
			return( true );
			
		}

		/** Manage Workers ********************************************************/

		void SpawnWorkers ()
		{

			Boolean bDoRun = true;

			while( bDoRun == true ) {

				if( this.GetThreadsStop() ) {

					DebugMsg( string.Format( "SpawnWorkers: {0}", "STOPPING" ) );
					
					bDoRun = false;
					break;

				} else {

					if( this.CountRunningThreads() < this.ThreadsMax ) {

						SemaphoreWorkers.WaitOne();

						DebugMsg( string.Format( "SpawnWorkers THREADS: {0} :: {1}", this.ThreadsMax, this.CountRunningThreads() ) );

						Boolean bNewThread = ThreadPool.QueueUserWorkItem( this.StartWorker, null );
					
						if( bNewThread ) {
							Thread.Sleep( 100 );
						}
					
						this.AdjustThreadsMax();

					}
					
					if(
						( this.CountRunningThreads() == 0 )
						&& ( !this.PeekUrlQueue() ) ) {
						bDoRun = false;
					}

				}

			}

			this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
			
			DebugMsg( string.Format( "SpawnWorkers: STOPPED" ) );

		}

		void StartWorker ( object thContext )
		{
			if( !this.GetThreadsStop() ) {
				MacroscopeJobWorker JobWorker = new MacroscopeJobWorker ( this );
				this.IncRunningThreads();
				JobWorker.Execute();		
			}
			SemaphoreWorkers.Release( 1 );				
		}

		public void NotifyWorkersFetched ( string sUrl )
		{
			DebugMsg( string.Format( "NotifyWorkersFetched: {0}", sUrl ) );
			this.PagesFound++;
			this.AddUpdateDisplayQueue( sUrl );
			this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
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
			if( iThreadCount == 0 ) {
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
			if( this.ThreadsRunning > 0 ) {
				int iThreadId = Thread.CurrentThread.ManagedThreadId;
				if( this.ThreadsDict.ContainsKey( iThreadId ) ) {
					lock( this.ThreadsDict ) {
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
			// TODO: Add more queues

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

		public void AddUrlQueueItem ( string sUrl )
		{
			if( !this.SeenHistoryItem( sUrl ) ) {
				this.NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueUrlList, sUrl );
			}
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

			foreach( MacroscopeDocument msDoc in this.DocCollection.IterateDocuments() ) {
				
				string sUrl = msDoc.GetUrl();

				switch( msDoc.GetStatusCode() ) {
				
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

		void ResetLink ( string sUrl )
		{
			
			MacroscopeDocument msDoc = this.DocCollection.GetDocument( sUrl );
			
			if( msDoc != null ) {
			
				msDoc.SetIsDirty();
			
				this.ResetHistoryItem( sUrl );
			
				this.AddUrlQueueItem( sUrl );

			} else {

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

		/** History ***************************************************************/

		public int GetPagesFound ()
		{
			return( this.PagesFound );
		}
		
		public void AddHistoryItem ( string sUrl )
		{
			if( !this.History.ContainsKey( sUrl ) ) {
				lock( this.History ) {
					this.History.Add( sUrl, true );
				}
			}
		}

		public void ResetHistoryItem ( string sUrl )
		{
			if( this.History.ContainsKey( sUrl ) ) {
				lock( this.History ) {
					this.History[ sUrl ] = false;
				}
			}
		}

		public Boolean SeenHistoryItem ( string sUrl )
		{
			Boolean bSeen = false;
			if( this.History.ContainsKey( sUrl ) ) {
				bSeen = this.History[ sUrl ];
			}
			return( bSeen );
		}

		public Dictionary<string,Boolean> GetHistory ()
		{
			Dictionary<string,Boolean> HistoryCopy = new Dictionary<string,Boolean> ( this.History.Count );
			lock( this.History ) {
				foreach( string sKey in this.History.Keys ) {
					HistoryCopy.Add( sKey, this.History[ sKey ] );
				}
			}
			return( HistoryCopy );
		}

		public void ClearHistory ()
		{
			lock( this.History ) {
				this.History.Clear();
			}
		}

		public int CountHistory ()
		{
			return( this.History.Count );
		}

		/** Document Collection ***************************************************/

		public MacroscopeDocumentCollection GetDocCollection ()
		{
			return( this.DocCollection );
		}
		
		/**************************************************************************/

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
			if( !this.Locales.ContainsKey( sLocale ) ) {
				lock( this.Locales ) {
					this.Locales[ sLocale ] = sLocale;
				}
			}
		}

		/** Robots ****************************************************************/
		
		public MacroscopeRobots GetRobots ()
		{
			return( this.Robots );
		}

		/**************************************************************************/
				
	}

}
