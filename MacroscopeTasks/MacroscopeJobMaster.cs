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
		
		Object DisplayLock;
		
		/** BEGIN: Configuration **/
		
		int ThreadsMax;
		int ThreadsRunning;
		Boolean ThreadsStop;
		Dictionary<int,Boolean> ThreadsDict;

		Thread ThreadUpdateDisplay = null;
		Boolean ThreadUpdateDisplayStop = false;
		Queue<string> UpdateDisplayQueue;
		
		public string StartUrl { get; set; }
		public int Depth { get; set; }
		public int PageLimit { get; set; }
		public int PageLimitCount { get; set; }
		public Boolean SameSite { get; set; }
		public Boolean ProbeHrefLangs { get; set; }

		/** END: Configuration **/

		int PagesFound;

		Queue<string> UrlQueue;
		
		Hashtable History;
		Hashtable Locales;

		MacroscopeRobots msRobots;
				
		/**************************************************************************/

		public MacroscopeJobMaster ( MacroscopeMainForm msMainFormNew )
		{

			msMainForm = msMainFormNew;
			msDocCollection = new MacroscopeDocumentCollection ();
			
			DisplayLock = new Object ();

			ThreadsMax = 8;
			ThreadsRunning = 0;
			ThreadsStop = false;
			ThreadsDict = new Dictionary<int,Boolean> ();

			ThreadPool.SetMaxThreads( ThreadsMax, ThreadsMax );
						
			Depth = MacroscopePreferences.GetDepth();
			PageLimit = MacroscopePreferences.GetPageLimit();
			PageLimitCount = 0;
			SameSite = MacroscopePreferences.GetSameSite();
			ProbeHrefLangs = MacroscopePreferences.GetProbeHreflangs();
			PagesFound = 0;

			UrlQueue = new Queue<string> ( 4096 );

			History = Hashtable.Synchronized( new Hashtable ( 4096 ) );

			Locales = Hashtable.Synchronized( new Hashtable ( 32 ) );
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

			debug_msg( string.Format( "MacroscopeJobMaster: {0}", "DESTRUCTOR CALLED" ), 0 );

			this.WorkerUpdateDisplayShutdown();

		}

		/**************************************************************************/

		public Boolean Execute ()
		{

			debug_msg( string.Format( "Start URL: {0}", this.StartUrl ), 1 );

			this.ThreadsStop = false;
			 							
			if( !this.UrlQueuePeek() ) {
				this.UrlQueueAdd( this.StartUrl );
			}

			this.WorkersSpawn();
			
			debug_msg( string.Format( "Pages Found: {0}", this.PagesFound ), 1 );

			this.msMainForm.CallbackScanComplete();

			return( true );
			
		}

		/**************************************************************************/

		void WorkersSpawn ()
		{

			Boolean bDoRun = true;

			while( bDoRun == true ) {

				if( this.ThreadsStop == true ) {

					debug_msg( string.Format( "WorkersSpawn: {0}", "STOPPING" ) );
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

					if(
						( this.RunningThreadsCount() == 0 )
						&& ( !this.UrlQueuePeek() ) ) {
						bDoRun = false;
					}

				}

				Thread.Sleep( 100 );

			}

			this.DocCollectionGet().RecalculateLinksIn();
			
			debug_msg( string.Format( "WorkersSpawn: STOPPED" ) );

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
			this.DocCollectionGet().RecalculateLinksIn();
			this.UpdateStatusBar();
			return( bIsStopped );
		}

		/**************************************************************************/

		public void WorkerUpdateDisplayShutdown ()
		{
			debug_msg( "WorkerUpdateDisplayShutdown Called" );
			this.DocCollectionGet().WorkerRecalculateLinksInShutdown();
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

						this.DocCollectionGet().WorkerRecalculateLinksInQueueAdd( 1 );

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
				debug_msg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
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
				debug_msg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
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
				debug_msg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
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
				debug_msg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
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
				debug_msg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( iCount );
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
		
		/**************************************************************************/
				
		public Boolean HistorySeen ( string sURL )
		{
			Boolean bSeen = false;
			if( this.History.ContainsKey( sURL ) ) {
				bSeen = ( Boolean )this.History[ sURL ];
			}
			return( bSeen );
		}

		/**************************************************************************/
				
		public Hashtable HistoryGet ()
		{
			Hashtable HistoryCopy;
			lock( this.History ) {
				HistoryCopy = ( Hashtable )this.History.Clone();
			}
			return( HistoryCopy );
		}
		
		/**************************************************************************/
		
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
		
		public Hashtable LocalesGet ()
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
					debug_msg( string.Format( "UpdateDisplaySingle: {0}", ex.Message ), 1 );
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
