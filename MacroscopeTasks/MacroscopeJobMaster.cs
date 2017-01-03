using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RobotsTxt;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeJobMaster : Macroscope
	{

		MacroscopeMainForm msMainForm;
		public MacroscopeJobLocker msLocker;
		
		/** BEGIN: Configuration **/
		
		int MaxThreads;

		public string StartUrl { get; set; }
		public uint Depth { get; set; }
		public int PageLimit { get; set; }
		public int PageLimitCount { get; set; }
		public Boolean SameSite { get; set; }
		public Boolean ProbeHrefLangs { get; set; }

		/** END: Configuration **/

		int PagesFound;

		Queue<string> UrlQueue;

		Hashtable History;
		Hashtable DocCollection;
		Hashtable Locales;

		MacroscopeRobots msRobots;
				
		/**************************************************************************/

		public MacroscopeJobMaster ( MacroscopeMainForm msMainFormNew )
		{
			msMainForm = msMainFormNew;
			msLocker = new MacroscopeJobLocker ();
			
			MaxThreads = 8;

			Depth = MacroscopePreferences.GetDepth();
			PageLimit = MacroscopePreferences.GetPageLimit();
			PageLimitCount = 0;
			SameSite = MacroscopePreferences.GetSameSite();
			ProbeHrefLangs = MacroscopePreferences.GetProbeHreflangs();
			PagesFound = 0;
			UrlQueue = new Queue<string> ( 4096 );
			History = new Hashtable ( 4096 );
			DocCollection = new Hashtable ( 4096 );
			Locales = new Hashtable ( 32 );
			msRobots = new MacroscopeRobots ();
		}

		/**************************************************************************/

		public Boolean Execute ()
		{

			debug_msg( "Run", 1 );

			debug_msg( string.Format( "Start URL: {0}", this.StartUrl ), 1 );

			//this.PageLimitCount = 0;
			//this.Recurse( StartUrl, StartUrl, 0 );

			this.AddUrlQueue( this.StartUrl );
			
			this.WorkersSeedStartUrl();
			
			this.WorkersSpawn();
			
			debug_msg( string.Format( "Pages Found: {0}", this.PagesFound ), 1 );

			debug_msg( "Done", 1 );
			//this.msMainForm.CallbackScanComplete();
			return( true );
		}

		/**************************************************************************/
		
		void WorkersSeedStartUrl ()
		{
			MacroscopeJobWorker msJobWorker = new MacroscopeJobWorker ( this );
			msJobWorker.Execute( this.GetUrlQueue() );			
		}

		/**************************************************************************/

		void WorkersSpawn ()
		{

			Boolean bDoRun = true;
			
			ThreadPool.SetMaxThreads( this.MaxThreads, this.MaxThreads );
			
			while( bDoRun == true ) {
					
				ThreadPool.QueueUserWorkItem( this.WorkerStart, null );
				
				Thread.Sleep( 1000 );
				
				bDoRun = this.PeekUrlQueue();

			}
			
		}
		
		/**************************************************************************/
		
		void WorkerStart ( object ThreadContext )
		{

			MacroscopeJobWorker msJobWorker = new MacroscopeJobWorker ( this );

			string sURL = this.GetUrlQueue();

			debug_msg( string.Format( "sURL: {0}", sURL ) );

			if( sURL != null ) {
				msJobWorker.Execute( sURL );
			}

		}

		/**************************************************************************/

		public void WorkersNotifyDone ( string sURL )
		{
			debug_msg( string.Format( "WorkersNotifyDone: {0}", sURL ) );
			this.UpdateDisplay();
		}
		
		/**************************************************************************/




		/**************************************************************************/
		
		public void AddUrlQueue ( string sURL )
		{
			lock( msLocker ) {
				debug_msg( string.Format( "AddUrlQueue: {0}", sURL ) );
				this.UrlQueue.Enqueue( sURL );
			}
		}
		
		/**************************************************************************/
		
		public string GetUrlQueue ()
		{
			string sURL = null;
			lock( msLocker ) {
				debug_msg( string.Format( "GetUrlQueue: {0}", this.UrlQueue.Count.ToString() ) );
				if( this.UrlQueue.Count > 0 ) {
					sURL = this.UrlQueue.Dequeue();
				}
			}
			return( sURL );
		}
	
		/**************************************************************************/
				
		public Boolean PeekUrlQueue ()
		{
			Boolean bPeek = false;
			lock( msLocker ) {
				debug_msg( string.Format( "PeekUrlQueue: {0}", this.UrlQueue.Count.ToString() ) );
				//if( this.UrlQueue.Count < this.MaxThreads ) {
				//	Thread.Sleep( 3000 );
				//}
				if( this.UrlQueue.Count > 0 ) {
					bPeek = true;
				}
			}
			return( bPeek );
		}

		/**************************************************************************/
				
		public void AddHistory ( string sURL )
		{
			if( !this.History.ContainsKey( sURL ) ) {
				this.History.Add( sURL, true );
			}
		}

		/**************************************************************************/

		public Hashtable GetDocCollection ()
		{
			return( this.DocCollection );
		}
		
		/**************************************************************************/
		
		public Hashtable GetLocales ()
		{
			return( this.Locales );
		}

		/**************************************************************************/
		
		public void AddLocales ( string sLocale )
		{			
			if( !this.Locales.ContainsKey( sLocale ) ) {
				this.Locales[sLocale] = sLocale;
			}
		}

		/**************************************************************************/
		
		public MacroscopeRobots GetRobots ()
		{
			return( this.msRobots );
		}
		
		/**************************************************************************/
		
		
		public void UpdateDisplay ()
		{
			lock( this.msLocker ) {
				this.msMainForm.UpdateDisplayStructure( this );
			}
		}
				
		/**************************************************************************/
		
		/*
		public Boolean Recurse ( string sParentURL, string sURL, int iDepth )
		{
			MacroscopeDocument msDoc = new MacroscopeDocument ( sURL );

			if( !msRobots.ApplyRobotRule( sURL ) ) {
				debug_msg( string.Format( "Disallowed by robots.txt: {0}", sURL ), 1 );
				return( false );
			}

			if( this.DocCollection.ContainsKey( sURL ) ) {

				debug_msg( string.Format( "ADDING INLINK FOR: {0}", sURL ), 2 );
				debug_msg( string.Format( "PARENT: {0}", sParentURL ), 3 );
								
				msDoc = ( MacroscopeDocument )this.DocCollection[sURL];
				if( msDoc != null ) {
					msDoc.AddHyperlinkIn( sParentURL );
				}
				return( true );

			} else {
				this.DocCollection.Add( sURL, msDoc );
				msDoc.AddHyperlinkIn( sParentURL );
			}
			
			if( msDoc.Depth > this.Depth ) {
				//debug_msg( string.Format( "TOO DEEP: {0}", msDoc.depth ), 3 );
				this.DocCollection.Remove( sURL );
				return( true );
			}

			if( this.ProbeHrefLangs ) {
				msDoc.probe_hreflangs = true;
			}

			if( msDoc.Execute() ) {
			
				this.PageLimitCount++;

				{
					string sLocale = msDoc.Locale;
					Hashtable htHrefLangs = ( Hashtable )msDoc.GetHrefLangs();
					if( sLocale != null ) {
						if( !this.Locales.ContainsKey( sLocale ) ) {
							this.Locales[sLocale] = sLocale;
						}
					}
					foreach( string sKeyLocale in htHrefLangs.Keys ) {
						if( !this.Locales.ContainsKey( sKeyLocale ) ) {
							this.Locales[sKeyLocale] = sKeyLocale;
						}
					}
				}

				Hashtable htOutlinks = msDoc.GetOutlinks();

				foreach( string sOutlinkKey in htOutlinks.Keys ) {
					string sOutlinkURL = ( string )htOutlinks[sOutlinkKey];
					//debug_msg( string.Format( "Outlink: {0}", sOutlinkURL ), 2 );

					if( sOutlinkURL != null ) {

						Boolean bProceed = true;

						if( this.PageLimit < 0 ) {
							bProceed = true;
						} else if( this.PageLimit > -1 ) {
							if( this.PageLimitCount >= this.PageLimit ) {
								debug_msg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", this.PageLimit, this.PageLimitCount ), 2 );
								bProceed = false;
							}
						}

						if( bProceed ) {
							if( MacroscopeURLTools.verify_same_host( this.StartUrl, sOutlinkURL ) ) {

								if( this.History.ContainsKey( sOutlinkURL ) ) {
									//debug_msg( string.Format( "ALREADY SEEN: {0}", sOutlinkURL ), 2 );
								} else {
									debug_msg( string.Format( "RECURSING INTO: {0}", sOutlinkURL ), 2 );
									this.PagesFound++;
									this.History.Add( sOutlinkURL, true );

									this.msJobThread.Update();

									this.Recurse( sURL, sOutlinkURL, iDepth + 1 );
								}

							} else {
								//debug_msg( string.Format( "FOREIGN HOST: {0}", sOutlinkURL ), 2 );
							}

						} else {
							break;
						}

					}

				}

			} else {
				debug_msg( string.Format( "EXECUTE FAILED: {0}", sURL ), 2 );
			}

			return( true );
		}
	
		 */

		/**************************************************************************/

	}

}
