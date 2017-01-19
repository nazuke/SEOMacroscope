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
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeJobMaster : Macroscope
	{

		MacroscopeMainForm msMainForm;
		MacroscopeDocumentCollection msDocCollection;
		MacroscopeAllowedHosts msAllowedHosts;
		
		Object DisplayLock;

		/** BEGIN: Named Queues **/

		const string constNamedQueueUrlList = "UrlQueue";

		public const string NamedQueueDisplayStructure = "DisplayStructure";
		public const string NamedQueueDisplayHierarchy = "DisplayHierarchy";
		public const string NamedQueueDisplayCanonicalAnalysis = "CanonicalAnalysis";
		public const string NamedQueueDisplayHrefLang = "DisplayHrefLang";
		public const string NamedQueueDisplayRedirectsAudit = "RedirectsAudit";
		public const string NamedQueueDisplayUriAnalysis = "UriAnalysis";
		public const string NamedQueueDisplayPageTitles = "PageTitles";
		public const string NamedQueueDisplayPageDescription = "PageDescription";
		public const string NamedQueueDisplayPageKeywords = "PageKeywords";
		public const string NamedQueueDisplayPageHeadings = "PageHeadings";
		public const string NamedQueueDisplayEmailAddresses = "EmailAddresses";
		public const string NamedQueueDisplayTelephoneNumbers = "TelephoneNumbers";
		public const string NamedQueueDisplayHostnames = "DisplayHostnames";

		MacroscopeNamedQueue NamedQueue;

		/** END: Named Queues **/

		/** BEGIN: Configuration **/
		
		int ThreadsMax;
		int ThreadsRunning;
		Boolean ThreadsStop;
		Dictionary<int,Boolean> ThreadsDict;

		string StartUrl;
		int Depth;
		int PageLimit;
		int PageLimitCount;
		Boolean SameSite;
		Boolean ProbeHrefLangs;

		/** END: Configuration **/

		int PagesFound;

		Hashtable History;
		Dictionary<string,string> Locales;

		MacroscopeRobots msRobots;
				
		/**************************************************************************/

		public MacroscopeJobMaster ( MacroscopeMainForm msMainFormNew )
		{

			msMainForm = msMainFormNew;
			msDocCollection = new MacroscopeDocumentCollection ();
			msAllowedHosts = new MacroscopeAllowedHosts ();
			
			DisplayLock = new Object ();

			// BEGIN: Named Queues
			NamedQueue = new MacroscopeNamedQueue ();
			{
				NamedQueue.CreateNamedQueue( constNamedQueueUrlList );	
				NamedQueue.CreateNamedQueue( NamedQueueDisplayStructure );			
				NamedQueue.CreateNamedQueue( NamedQueueDisplayHierarchy );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayCanonicalAnalysis );	
				NamedQueue.CreateNamedQueue( NamedQueueDisplayHrefLang );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayRedirectsAudit );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayUriAnalysis );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayPageTitles );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayPageDescription );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayPageKeywords );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayPageHeadings );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayEmailAddresses );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayTelephoneNumbers );
				NamedQueue.CreateNamedQueue( NamedQueueDisplayHostnames );
			}
			// END: Named Queues

			this.AdjustThreadsMax();
			ThreadsRunning = 0;
			ThreadsStop = false;
			ThreadsDict = new Dictionary<int,Boolean> ();

			Depth = MacroscopePreferencesManager.GetDepth();
			PageLimit = MacroscopePreferencesManager.GetPageLimit();
			PageLimitCount = 0;

			SameSite = MacroscopePreferencesManager.GetSameSite();
			ProbeHrefLangs = MacroscopePreferencesManager.GetProbeHreflangs();
			PagesFound = 0;

			History = Hashtable.Synchronized( new Hashtable ( 4096 ) );

			Locales = new Dictionary<string,string> ( 32 );
			msRobots = new MacroscopeRobots ();

		}

		/**************************************************************************/

		~MacroscopeJobMaster ()
		{
			DebugMsg( string.Format( "MacroscopeJobMaster: {0}", "DESTRUCTOR CALLED" ) );
			//this.msDocCollection.ShutdownWorkerRecalculateDocCollection();
			this.msDocCollection = null;
		}
		
		/** Execute Job ***********************************************************/

		public Boolean Execute ()
		{

			DebugMsg( string.Format( "Start URL: {0}", this.StartUrl ) );

			this.ThreadsStop = false;

			this.msAllowedHosts.AddFromUrl( this.StartUrl );

			if( !this.PeekUrlQueue() ) {
				this.AddUrlQueueItem( this.StartUrl );
			}

			this.SpawnWorkers();
			
			DebugMsg( string.Format( "Pages Found: {0}", this.PagesFound ) );

			this.msMainForm.CallbackScanComplete();

			return( true );
			
		}

		/** Manage Workers ********************************************************/

		void SpawnWorkers ()
		{

			Boolean bDoRun = true;

			while( bDoRun == true ) {

				if( this.ThreadsStop == true ) {

					DebugMsg( string.Format( "WorkersSpawn: {0}", "STOPPING" ) );
					bDoRun = false;
					break;

				} else {

					for( int i = 0; i < this.ThreadsMax; i++ ) {
						if( this.CountRunningThreads() < this.ThreadsMax ) {
							Boolean bNewThread = ThreadPool.QueueUserWorkItem( this.StartWorker, null );
						}
						Thread.Sleep( 100 );
					}
						
					Thread.Sleep( 2000 );
					
					this.AdjustThreadsMax();
					
					if(
						( this.CountRunningThreads() == 0 )
						&& ( !this.PeekUrlQueue() ) ) {
						bDoRun = false;
					}

				}

				Thread.Sleep( 100 );

			}

			this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
			
			DebugMsg( string.Format( "WorkersSpawn: STOPPED" ) );

		}
			
		void StartWorker ( object thContext )
		{
			if( !this.ThreadsStop ) {
				MacroscopeJobWorker msJobWorker = new MacroscopeJobWorker ( this );
				string sURL = this.GetUrlQueueItem();
				if( sURL != null ) {
					this.IncRunningThreads();
					msJobWorker.Execute( sURL );
				}
			}
		}

		public void NotifyWorkersDone ( string sURL )
		{
			this.DecRunningThreads();
			this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue();
			this.AddUpdateDisplayQueue( sURL );
		}
		
		public void StopWorkers ()
		{
			this.ThreadsStop = true;
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

		void AdjustThreadsMax ()
		{
			ThreadsMax = MacroscopePreferencesManager.GetMaxThreads();
			ThreadPool.SetMaxThreads( ThreadsMax, ThreadsMax );
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

		public void AddUpdateDisplayQueue ( string sURL )
		{
			// TODO: Add more queues

			NamedQueue.AddToNamedQueue( NamedQueueDisplayStructure, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayHierarchy, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayCanonicalAnalysis, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayHrefLang, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayRedirectsAudit, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayUriAnalysis, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayPageTitles, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayPageDescription, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayPageKeywords, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayPageHeadings, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayEmailAddresses, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayTelephoneNumbers, sURL );
			NamedQueue.AddToNamedQueue( NamedQueueDisplayHostnames, sURL );

		}

		public List<string> DrainDisplayQueueAsList ( string sNamedQueueName )
		{
			return( this.NamedQueue.DrainNamedQueueItemsAsList( sNamedQueueName ) );
		}

		/** URL Queue *************************************************************/

		public List<string> GetUrlQueueAsList ()
		{
			return( this.NamedQueue.GetNamedQueueItemsAsList( constNamedQueueUrlList ) );
		}

		public void AddUrlQueueItem ( string sURL )
		{
			if( !this.SeenHistory( sURL ) ) {
				this.NamedQueue.AddToNamedQueue( constNamedQueueUrlList, sURL );
			}
		}

		public string GetUrlQueueItem ()
		{
			return( this.NamedQueue.GetNamedQueueItem( constNamedQueueUrlList ) );
		}
			
		public Boolean PeekUrlQueue ()
		{
			Boolean bPeek = this.NamedQueue.PeekNamedQueue( constNamedQueueUrlList );
			DebugMsg( string.Format( "PeekUrlQueue: {0}", bPeek ) );
			return( bPeek );
		}
	
		public int CountUrlQueueItems ()
		{
			return( this.NamedQueue.CountNamedQueueItems( constNamedQueueUrlList ) );
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

		public int GetPageLimitCount ()
		{
			return( this.PageLimitCount );
		}

		public void IncPageLimitCount ()
		{
			this.PageLimitCount++;
		}

		public void SetPageLimitCount ( int iValue )
		{
			this.PageLimitCount = iValue;
		}

		/** HrefLang Tags *********************************************************/

		public Boolean GetProbeHrefLangs ()
		{
			return( this.ProbeHrefLangs );
		}

		public void SetProbeHrefLangs ( Boolean bState )
		{
			this.ProbeHrefLangs = bState;
		}

		/** History ***************************************************************/

		public void AddHistory ( string sURL )
		{
			if( !this.History.ContainsKey( sURL ) ) {
				lock( this.History ) {
					this.History.Add( sURL, true );
				}
			}
		}

		public Boolean SeenHistory ( string sURL )
		{
			Boolean bSeen = false;
			if( this.History.ContainsKey( sURL ) ) {
				bSeen = ( Boolean )this.History[ sURL ];
			}
			return( bSeen );
		}

		public Hashtable GetHistory ()
		{
			Hashtable HistoryCopy;
			lock( this.History ) {
				HistoryCopy = ( Hashtable )this.History.Clone();
			}
			return( HistoryCopy );
		}

		public void ClearHistory ()
		{
			lock( this.History ) {
				this.History.Clear();
			}
		}
		
		/** Document Collection ***************************************************/

		public MacroscopeDocumentCollection GetDocCollection ()
		{
			return( this.msDocCollection );
		}
		
		/**************************************************************************/

		public MacroscopeAllowedHosts GetAllowedHosts ()
		{
			return( this.msAllowedHosts );
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
			return( this.msRobots );
		}

		/**************************************************************************/
				
	}

}
