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
		
		/** BEGIN: Configuration **/
		
		int ThreadsMax;
		int ThreadsRunning;
		Boolean ThreadsStop;
		Dictionary<int,Boolean> ThreadsDict;

		Thread ThreadUpdateDisplay = null;
		Boolean ThreadUpdateDisplayStop = false;
		Queue<string> UpdateDisplayQueue;
		
		string StartUrl;
		int Depth;
		int PageLimit;
		int PageLimitCount;
		Boolean SameSite;
		Boolean ProbeHrefLangs;

		/** END: Configuration **/

		int PagesFound;

		Queue<string> UrlQueue;
		
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

			this.AdjustThreadsMax();
			ThreadsRunning = 0;
			ThreadsStop = false;
			ThreadsDict = new Dictionary<int,Boolean> ();

			//ThreadPool.SetMaxThreads( ThreadsMax, ThreadsMax );
						
			Depth = MacroscopePreferencesManager.GetDepth();
			PageLimit = MacroscopePreferencesManager.GetPageLimit();
			PageLimitCount = 0;
			SameSite = MacroscopePreferencesManager.GetSameSite();
			ProbeHrefLangs = MacroscopePreferencesManager.GetProbeHreflangs();
			PagesFound = 0;

			UrlQueue = new Queue<string> ( 4096 );

			History = Hashtable.Synchronized( new Hashtable ( 4096 ) );

			Locales = new Dictionary<string,string> ( 32 );
			msRobots = new MacroscopeRobots ();

			{
				UpdateDisplayQueue = new Queue<string> ( 4096 );
				ThreadUpdateDisplay = new Thread ( new ThreadStart ( this.WorkerUpdateDisplay ) );
				ThreadUpdateDisplay.Start();
			}

		}

		/**************************************************************************/

		~MacroscopeJobMaster ()
		{
			DebugMsg( string.Format( "MacroscopeJobMaster: {0}", "DESTRUCTOR CALLED" ) );
			this.WorkerUpdateDisplayShutdown();
		}

		/**************************************************************************/

		public Boolean Execute ()
		{

			DebugMsg( string.Format( "Start URL: {0}", this.StartUrl ) );

			this.ThreadsStop = false;

			this.msAllowedHosts.AddFromUrl( this.StartUrl );

			if( !this.UrlQueuePeek() ) {
				this.UrlQueueAdd( this.StartUrl );
			}

			this.WorkersSpawn();
			
			DebugMsg( string.Format( "Pages Found: {0}", this.PagesFound ) );

			this.msMainForm.CallbackScanComplete();

			return( true );
			
		}

		/**************************************************************************/

		void WorkersSpawn ()
		{

			Boolean bDoRun = true;

			while( bDoRun == true ) {

				if( this.ThreadsStop == true ) {

					DebugMsg( string.Format( "WorkersSpawn: {0}", "STOPPING" ) );
					bDoRun = false;
					break;

				} else {

					for( int i = 0; i < this.ThreadsMax; i++ ) {
						if( this.RunningThreadsCount() < this.ThreadsMax ) {
							Boolean bNewThread = ThreadPool.QueueUserWorkItem( this.WorkerStart, null );
						}
						Thread.Sleep( 100 );
					}
						
					Thread.Sleep( 2000 );
					
					this.AdjustThreadsMax();
					
					if(
						( this.RunningThreadsCount() == 0 )
						&& ( !this.UrlQueuePeek() ) ) {
						bDoRun = false;
					}

				}

				Thread.Sleep( 100 );

			}

			this.DocCollectionGet().RecalculateDocCollection();
			
			DebugMsg( string.Format( "WorkersSpawn: STOPPED" ) );

		}
		
		/**************************************************************************/
		
		void WorkerStart ( object thContext )
		{
			if( !this.ThreadsStop ) {
				MacroscopeJobWorker msJobWorker = new MacroscopeJobWorker ( this );
				string sURL = this.UrlQueueGet();
				if( sURL != null ) {
					this.RunningThreadsInc();
					msJobWorker.Execute( sURL );
				}
			}
		}

		/**************************************************************************/

		public void WorkersNotifyDone ( string sURL )
		{
			this.RunningThreadsDec();
			this.UpdateDisplayQueueAdd( sURL );
		}
		
		/**************************************************************************/

		public void WorkersStop ()
		{
			this.ThreadsStop = true;
		}

		/**************************************************************************/
		
		public Boolean WorkersStopped ()
		{
			Boolean bIsStopped = false;
			int iThreadCount = this.RunningThreadsCount();
			if( iThreadCount == 0 ) {
				bIsStopped = true;
			}
			this.DocCollectionGet().RecalculateDocCollection();
			this.UpdateStatusBar();
			return( bIsStopped );
		}

		/**************************************************************************/

		public void WorkerUpdateDisplayShutdown ()
		{
			DebugMsg( "WorkerUpdateDisplayShutdown Called" );
			this.DocCollectionGet().WorkerRecalculateDocCollectionShutdown();
			this.ThreadUpdateDisplayStop = true;
		}
		
		/**************************************************************************/

		void WorkerUpdateDisplay ()
		{

			Boolean bDoUpdateDisplay = true;
			
			do {

				if( this.UpdateDisplayQueuePeek() ) {

					List<string> lUrls = new List<string> ();

					{
						string sUrl = this.UpdateDisplayQueueGet();
						do {
							if( sUrl != null ) {
								lUrls.Add( sUrl );
							}
							sUrl = this.UpdateDisplayQueueGet();
						} while( sUrl != null );
					}
					
					if( lUrls.Count > 0 ) {

						foreach( string sUrl in lUrls ) {
							if( this.DocCollectionGet().Contains( sUrl ) ) {
								this.UpdateDisplaySingle( sUrl );
							} else {
								this.UpdateDisplayQueueAdd( sUrl );
							}
						}

						this.DocCollectionGet().WorkerRecalculateDocCollectionQueueAdd( 1 );

					}
					
				}

				this.UpdateStatusBar();

				Thread.Sleep( 5000 );

				if( this.ThreadUpdateDisplayStop == true ) {
					bDoUpdateDisplay = false;
				}

			} while( bDoUpdateDisplay == true );

		}

		/**************************************************************************/

		void AdjustThreadsMax ()
		{
			ThreadsMax = MacroscopePreferencesManager.GetMaxThreads();
			ThreadPool.SetMaxThreads( ThreadsMax, ThreadsMax );
		}
		
		/**************************************************************************/
		
		void RunningThreadsInc ()
		{
			int iThreadId = Thread.CurrentThread.ManagedThreadId;
			this.ThreadsDict[ iThreadId ] = true;
			this.ThreadsRunning++;
		}
		
		/**************************************************************************/

		void RunningThreadsDec ()
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
		
		/**************************************************************************/
				
		public int RunningThreadsCount ()
		{
			int iRunningThreads = 0;
			iRunningThreads = this.ThreadsRunning;
			return( iRunningThreads );
		}

		/**************************************************************************/

		public void UpdateDisplayQueueAdd ( string sURL )
		{
			lock( this.UpdateDisplayQueue ) {
				this.UpdateDisplayQueue.Enqueue( sURL );
			}
		}
		
		/**************************************************************************/
		
		public string UpdateDisplayQueueGet ()
		{
			string sURL = null;
			try {
				if( this.UpdateDisplayQueue.Count > 0 ) {
					lock( this.UpdateDisplayQueue ) {
						sURL = this.UpdateDisplayQueue.Dequeue();
					}
				}
			} catch( InvalidOperationException ex ) {
				DebugMsg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( sURL );
		}
	
		/**************************************************************************/
				
		public Boolean UpdateDisplayQueuePeek ()
		{
			Boolean bPeek = false;
			try {
				lock( this.UpdateDisplayQueue ) {
					if( this.UpdateDisplayQueue.Count > 0 ) {
						bPeek = true;
					}
				}
			} catch( InvalidOperationException ex ) {
				DebugMsg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( bPeek );
		}

		/**************************************************************************/

		public void UrlQueueAdd ( string sURL )
		{
			if( !this.HistorySeen( sURL ) ) {
				lock( this.UrlQueue ) {
					this.UrlQueue.Enqueue( sURL );
				}
			}
		}
		
		/**************************************************************************/
		
		public string UrlQueueGet ()
		{
			string sURL = null;
			try {
				if( this.UrlQueue.Count > 0 ) {
					lock( this.UrlQueue ) {
						sURL = this.UrlQueue.Dequeue();
					}
				}
			} catch( InvalidOperationException ex ) {
				DebugMsg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( sURL );
		}
	
		/**************************************************************************/
				
		public Boolean UrlQueuePeek ()
		{
			Boolean bPeek = false;
			try {
				lock( this.UrlQueue ) {
					if( this.UrlQueue.Count > 0 ) {
						bPeek = true;
					}
				}
			} catch( InvalidOperationException ex ) {
				DebugMsg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( bPeek );
		}

		/**************************************************************************/
		
		public int UrlQueueCount ()
		{
			int iCount = 0;
			try {
				lock( this.UrlQueue ) {
					if( this.UrlQueue.Count > 0 ) {
						iCount = this.UrlQueue.Count;
					}
				}
			} catch( InvalidOperationException ex ) {
				DebugMsg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( iCount );
		}

		/**************************************************************************/

		public void SetStartUrl ( string sUrl )
		{
			this.StartUrl = sUrl;
		}
		
		public string GetStartUrl ()
		{
			return( this.StartUrl );
		}

		/**************************************************************************/

		public int GetDepth ()
		{
			return( this.Depth );
		}

		public void SetGetDepth ( int iValue )
		{
			this.Depth = iValue;
		}

		/**************************************************************************/

		public int GetPageLimit ()
		{
			return( this.PageLimit );
		}

		/**************************************************************************/

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

		/**************************************************************************/

		public Boolean GetProbeHrefLangs ()
		{
			return( this.ProbeHrefLangs );
		}

		public void SetProbeHrefLangs ( Boolean bState )
		{
			this.ProbeHrefLangs = bState;
		}

		/**************************************************************************/

		public void HistoryAdd ( string sURL )
		{
			if( !this.History.ContainsKey( sURL ) ) {
				lock( this.History ) {
					this.History.Add( sURL, true );
				}
			}
		}

		public Boolean HistorySeen ( string sURL )
		{
			Boolean bSeen = false;
			if( this.History.ContainsKey( sURL ) ) {
				bSeen = ( Boolean )this.History[ sURL ];
			}
			return( bSeen );
		}

		public Hashtable HistoryGet ()
		{
			Hashtable HistoryCopy;
			lock( this.History ) {
				HistoryCopy = ( Hashtable )this.History.Clone();
			}
			return( HistoryCopy );
		}

		public void HistoryClear ()
		{
			lock( this.History ) {
				this.History.Clear();
			}
		}
		
		/**************************************************************************/

		public MacroscopeDocumentCollection DocCollectionGet ()
		{
			return( this.msDocCollection );
		}
		
		/**************************************************************************/

		public MacroscopeAllowedHosts GetAllowedHosts ()
		{
			return( this.msAllowedHosts );
		}

		/**************************************************************************/
		
		public Dictionary<string,string> LocalesGet ()
		{
			return( this.Locales );
		}

		/**************************************************************************/
		
		public void AddLocales ( string sLocale )
		{			
			if( !this.Locales.ContainsKey( sLocale ) ) {
				lock( this.Locales ) {
					this.Locales[ sLocale ] = sLocale;
				}
			}
		}

		/**************************************************************************/
		
		public MacroscopeRobots RobotsGet ()
		{
			return( this.msRobots );
		}
		
		/**************************************************************************/
		
		public void UpdateDisplaySingle ( string sURL )
		{
			if( this.ThreadsStop == true ) {
				return;
			}
			lock( this.DisplayLock ) {
				try {
					this.msMainForm.UpdateDisplaySingle( sURL );
				} catch( ArgumentException ex ) {
					DebugMsg( string.Format( "UpdateDisplaySingle: {0}", ex.Message ) );
				}
			}
		}

		/**************************************************************************/

		public void UpdateStatusBar ()
		{
			this.msMainForm.UpdateStatusBar();
		}

		/**************************************************************************/
				
	}

}
