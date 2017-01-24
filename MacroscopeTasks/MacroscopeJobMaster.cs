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
				
		/** BEGIN: Configuration **/
		
		int ThreadsMax;
		int ThreadsRunning;
		Boolean ThreadsStop;
		object ThreadsLock = new object ();
		Dictionary<int,Boolean> ThreadsDict;

		string StartUrl;
		int Depth;
		int PageLimit;
		int PageLimitCount;

		/** END: Configuration **/

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

			DocCollection = new MacroscopeDocumentCollection ();
			AllowedHosts = new MacroscopeAllowedHosts ();
			
			// BEGIN: Named Queues
			NamedQueue = new MacroscopeNamedQueue ();
			{
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueUrlList );	
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayStructure );			
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayHierarchy );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayCanonicalAnalysis );	
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayHrefLang );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayRedirectsAudit );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayUriAnalysis );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayPageTitles );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayPageDescription );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayPageKeywords );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayPageHeadings );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayEmailAddresses );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayTelephoneNumbers );
				NamedQueue.CreateNamedQueue( MacroscopeConstants.NamedQueueDisplayHostnames );
			}
			// END: Named Queues

			this.AdjustThreadsMax();
			ThreadsRunning = 0;
			ThreadsStop = false;
			ThreadsDict = new Dictionary<int,Boolean> ();

			Depth = MacroscopePreferencesManager.GetDepth();
			PageLimit = MacroscopePreferencesManager.GetPageLimit();
			PageLimitCount = 0;

			PagesFound = 0;

			History = new Dictionary<string, bool> ( 4096 );

			Locales = new Dictionary<string,string> ( 32 );
			Robots = new MacroscopeRobots ();

		}

		/**************************************************************************/

		~MacroscopeJobMaster ()
		{
			DebugMsg( string.Format( "MacroscopeJobMaster: {0}", "DESTRUCTOR CALLED" ) );
			this.DocCollection = null;
		}
		
		/** Execute Job ***********************************************************/

		public Boolean Execute ()
		{

			DebugMsg( string.Format( "Start URL: {0}", this.StartUrl ) );

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

					for( int i = 0; i < this.ThreadsMax; i++ ) {
						if( this.CountRunningThreads() < this.ThreadsMax ) {
							lock( this.ThreadsLock ) {
								Boolean bNewThread = ThreadPool.QueueUserWorkItem( this.StartWorker, null );
								if( bNewThread ) {
									Thread.Sleep( 100 );
								}
							}
						}
					}

					this.AdjustThreadsMax();
					
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
			this.ThreadsDict[iThreadId] = true;
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
			NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayRedirectsAudit, sUrl );
			NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayUriAnalysis, sUrl );
			NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageTitles, sUrl );
			NamedQueue.AddToNamedQueue( MacroscopeConstants.NamedQueueDisplayPageDescription, sUrl );
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
			if( !this.SeenHistory( sUrl ) ) {
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
			DebugMsg( string.Format( "PeekUrlQueue: {0}", bPeek ) );
			return( bPeek );
		}
	
		public int CountUrlQueueItems ()
		{
			return( this.NamedQueue.CountNamedQueueItems( MacroscopeConstants.NamedQueueUrlList ) );
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
		
		public void AddHistory ( string sUrl )
		{
			if( !this.History.ContainsKey( sUrl ) ) {
				lock( this.History ) {
					this.History.Add( sUrl, true );
				}
			}
		}

		public Boolean SeenHistory ( string sUrl )
		{
			Boolean bSeen = false;
			if( this.History.ContainsKey( sUrl ) ) {
				bSeen = ( Boolean )this.History[sUrl];
			}
			return( bSeen );
		}

		public Dictionary<string,Boolean> GetHistory ()
		{
			Dictionary<string,Boolean> HistoryCopy = new Dictionary<string,Boolean> ( this.History.Count );
			lock( this.History ) {
				foreach( string sKey in this.History.Keys ) {
					HistoryCopy.Add( sKey, this.History[sKey] );
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
					this.Locales[sLocale] = sLocale;
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
