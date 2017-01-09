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
using System.Text;
using RobotsTxt;
using System.Threading;
using System.Diagnostics;

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

			ThreadsMax = 4;
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

		}

		/**************************************************************************/

		public Boolean Execute ()
		{

			debug_msg( "Run", 1 );

			debug_msg( string.Format( "Start URL: {0}", this.StartUrl ), 1 );

			this.ThreadsStop = false;
			 							
			if( !this.UrlQueuePeek() ) {
				this.UrlQueueAdd( this.StartUrl );
			}

			this.WorkersSpawn();
			
			debug_msg( string.Format( "Pages Found: {0}", this.PagesFound ), 1 );

			debug_msg( "Done", 1 );
								
			this.msMainForm.CallbackScanComplete();

			return( true );
		}

		/**************************************************************************/

		void WorkersSpawn ()
		{

			Boolean bDoRun = true;

			while( bDoRun == true ) {

				this.UpdateStatusBar();

				if( this.ThreadsStop == true ) {

					debug_msg( string.Format( "WorkersSpawn: {0}", "STOPPING" ) );
					bDoRun = false;
					break;

				} else {

					for( int i = 0; i < this.ThreadsMax; i++ ) {
						Boolean bNewThread = ThreadPool.QueueUserWorkItem( this.WorkerStart, null );
						Thread.Sleep( 100 );
					}
						
					Thread.Sleep( 1000 );

					if( this.RunningThreadsCount() == 0 ) {
						if( !this.UrlQueuePeek() ) {
							bDoRun = false;
						}
					}

				}
			}
			
			this.DocCollectionGet().RecalculateLinksIn();
						
			this.UpdateDisplay();
						
			this.UpdateStatusBar();

			debug_msg( string.Format( "WorkersSpawn: STOPPED" ) );

		}
		
		/**************************************************************************/
		
		void WorkerStart ( object thContext )
		{
			MacroscopeJobWorker msJobWorker = new MacroscopeJobWorker ( this );
			string sURL = this.UrlQueueGet();
			if( sURL != null ) {
				this.RunningThreadsInc();
				msJobWorker.Execute( sURL );
			}
		}

		/**************************************************************************/

		public void WorkersNotifyDone ( string sURL )
		{

			debug_msg( string.Format( "WorkersNotifyDone: {0}", sURL ) );

			this.RunningThreadsDec();

			this.DocCollectionGet().RecalculateLinksIn();
			
			this.UpdateDisplaySingle( sURL );
			
			this.UpdateDisplay();
			
			this.UpdateStatusBar();

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
			this.UpdateStatusBar();
			return( bIsStopped );
		}

		/**************************************************************************/

		void RunningThreadsInc ()
		{

			int iThreadId = Thread.CurrentThread.ManagedThreadId;
			this.ThreadsDict[iThreadId] = true;
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
				bSeen = ( Boolean )this.History[sURL];
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
					this.Locales[sLocale] = sLocale;
				}
			}
		}

		/**************************************************************************/
		
		public MacroscopeRobots RobotsGet ()
		{
			return( this.msRobots );
		}
		
		/**************************************************************************/

		public void UpdateDisplay ()
		{
			if( this.ThreadsStop == true ) {
				return;
			}
			lock( this.DisplayLock ) {
				try {
					
					this.msMainForm.UpdateDisplay();
					
					
					//this.msMainForm.UpdateDisplayHrefLang( this );
					
					
					
					
				} catch( ArgumentException ex ) {
					debug_msg( string.Format( "UpdateDisplay: {0}", ex.Message ), 1 );
				}
			}
		}

		/**************************************************************************/
		
		public void UpdateDisplaySingle ( string sURL )
		{
			if( this.ThreadsStop == true ) {
				return;
			}
			lock( this.DisplayLock ) {
				try {
					this.msMainForm.UpdateDisplaySingle( this, sURL );
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

		void DumpHistory ()
		{
			lock( this.History ) {
				debug_msg( "" );
				debug_msg( "DUMPING HISTORY:" );
				foreach( string sKey in this.History.Keys ) {
					debug_msg( string.Format( "DumpHistory: {0} => {1}", sKey, this.History[sKey].ToString() ) );
				}
				debug_msg( "HISTORY DUMP COMPLETE." );
				debug_msg( "" );
			}
		}
		
		/**************************************************************************/
				
	}

}
