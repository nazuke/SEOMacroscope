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

		//Thread ThreadUpdateDisplay = null;
		//Boolean ThreadUpdateDisplayStop = false;
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
				//ThreadUpdateDisplay = new Thread ( new ThreadStart ( this.ThreadWorkerUpdateDisplay ) );
				//ThreadUpdateDisplay.Start();
			}

		}

		/**************************************************************************/

		~MacroscopeJobMaster ()
		{
			DebugMsg( string.Format( "MacroscopeJobMaster: {0}", "DESTRUCTOR CALLED" ) );
			//this.ShutdownWorkerUpdateDisplay();
		}

		/**************************************************************************/

		public Boolean Execute ()
		{

			DebugMsg( string.Format( "Start URL: {0}", this.StartUrl ) );

			this.ThreadsStop = false;

			this.msAllowedHosts.AddFromUrl( this.StartUrl );

			if( !this.PeekUrlQueue() ) {
				this.AddUrlQueue( this.StartUrl );
			}

			this.SpawnWorkers();
			
			DebugMsg( string.Format( "Pages Found: {0}", this.PagesFound ) );

			this.msMainForm.CallbackScanComplete();

			return( true );
			
		}

		/**************************************************************************/

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

			this.GetDocCollection().RecalculateDocCollection();
			
			DebugMsg( string.Format( "WorkersSpawn: STOPPED" ) );

		}
		
		/**************************************************************************/
		
		void StartWorker ( object thContext )
		{
			if( !this.ThreadsStop ) {
				MacroscopeJobWorker msJobWorker = new MacroscopeJobWorker ( this );
				string sURL = this.GetUrlQueue();
				if( sURL != null ) {
					this.IncRunningThreads();
					msJobWorker.Execute( sURL );
				}
			}
		}

		/**************************************************************************/

		public void NotifyWorkersDone ( string sURL )
		{
			this.DecRunningThreads();
			this.AddUpdateDisplayQueue( sURL );
		}
		
		/**************************************************************************/

		public void StopWorkers ()
		{
			this.ThreadsStop = true;
		}

		/**************************************************************************/
		
		public Boolean WorkersStopped ()
		{
			Boolean bIsStopped = false;
			int iThreadCount = this.CountRunningThreads();
			if( iThreadCount == 0 ) {
				bIsStopped = true;
			}
			this.GetDocCollection().RecalculateDocCollection();
			this.UpdateStatusBar();
			return( bIsStopped );
		}

		/**************************************************************************/

		/*
		public void ShutdownWorkerUpdateDisplay ()
		{
			DebugMsg( "WorkerUpdateDisplayShutdown Called" );
			this.GetDocCollection().ShutdownWorkerRecalculateDocCollection();
			this.ThreadUpdateDisplayStop = true;
		}
		*/

		/**************************************************************************/

		// TODO: retire this approach
		/*
		void ThreadWorkerUpdateDisplay ()
		{

			Boolean bDoUpdateDisplay = true;
			
			do {

				this.WorkerUpdateDisplay();

				Thread.Sleep( 5000 );

				if( this.ThreadUpdateDisplayStop == true ) {
					bDoUpdateDisplay = false;
				}

			} while( bDoUpdateDisplay == true );

		}
		*/
		
		/**************************************************************************/

		public void WorkerUpdateDisplay ()
		{

			if( this.PeekUpdateDisplayQueue() ) {

				List<string> lUrls = new List<string> ();

				{
					string sUrl = this.GetUpdateDisplayQueue();
					do {
						if( sUrl != null ) {
							lUrls.Add( sUrl );
						}
						sUrl = this.GetUpdateDisplayQueue();
					} while( sUrl != null );
				}
					
				if( lUrls.Count > 0 ) {

					foreach( string sUrl in lUrls ) {
						if( this.GetDocCollection().Contains( sUrl ) ) {
							this.UpdateDisplaySingle( sUrl );
						} else {
							this.AddUpdateDisplayQueue( sUrl );
						}
					}

					this.GetDocCollection().AddWorkerRecalculateDocCollectionQueue( 1 );

				}
					
			}

			this.UpdateStatusBar();

		}

		/**************************************************************************/

		void AdjustThreadsMax ()
		{
			ThreadsMax = MacroscopePreferencesManager.GetMaxThreads();
			ThreadPool.SetMaxThreads( ThreadsMax, ThreadsMax );
		}
		
		/**************************************************************************/
		
		void IncRunningThreads ()
		{
			int iThreadId = Thread.CurrentThread.ManagedThreadId;
			this.ThreadsDict[iThreadId] = true;
			this.ThreadsRunning++;
		}
		
		/**************************************************************************/

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
		
		/**************************************************************************/
				
		public int CountRunningThreads ()
		{
			int iRunningThreads = 0;
			iRunningThreads = this.ThreadsRunning;
			return( iRunningThreads );
		}

		/** Display Queue *********************************************************/

		public void AddUpdateDisplayQueue ( string sURL )
		{
			lock( this.UpdateDisplayQueue ) {
				this.UpdateDisplayQueue.Enqueue( sURL );
			}
		}
		
		public string GetUpdateDisplayQueue ()
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
				
		public Boolean PeekUpdateDisplayQueue ()
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

		/** URL Queue *************************************************************/

		public List<string> GetQueue ()
		{
			List<string> QueueCopy = new List<string> ( this.UrlQueue.Count );
			lock( this.UrlQueue ) {
				foreach( string sUrl in this.UrlQueue ) {
					DebugMsg( string.Format( "GetQueue: {0}", sUrl ) );
					QueueCopy.Add( sUrl );
				}
			}
			return( QueueCopy );
		}

		public void AddUrlQueue ( string sURL )
		{
			if( !this.SeenHistory( sURL ) ) {
				lock( this.UrlQueue ) {
					this.UrlQueue.Enqueue( sURL );
				}
			}
		}
		
		public string GetUrlQueue ()
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
			
		public Boolean PeekUrlQueue ()
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
	
		public int CountUrlQueue ()
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
				bSeen = ( Boolean )this.History[sURL];
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
		
		/**************************************************************************/

		public MacroscopeDocumentCollection GetDocCollection ()
		{
			return( this.msDocCollection );
		}
		
		/**************************************************************************/

		public MacroscopeAllowedHosts GetAllowedHosts ()
		{
			return( this.msAllowedHosts );
		}

		/**************************************************************************/
		
		public Dictionary<string,string> GetLocales ()
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
		
		public MacroscopeRobots GetRobots ()
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
