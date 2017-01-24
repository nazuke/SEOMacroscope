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
using System.Timers;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDocumentCollection : Macroscope
	{

		/**************************************************************************/

		public override Boolean SuppressDebugMsg { get; protected set; }
				
		/**************************************************************************/

		Dictionary<string,MacroscopeDocument> DocCollection;

		MacroscopeNamedQueue NamedQueue;

		Dictionary<string,Boolean> StatsHistory;
		Dictionary<string,int> StatsHostnames;
		Dictionary<string,int> StatsTitles;
		Dictionary<string,int> StatsDescriptions;

		Semaphore SemaphoreRecalc;
		System.Timers.Timer TimerRecalc;

		/**************************************************************************/

		public MacroscopeDocumentCollection ()
		{
			
			SuppressDebugMsg = true;
			
			this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZING..." );
			
			DocCollection = new Dictionary<string,MacroscopeDocument> ( 4096 );

			NamedQueue = new MacroscopeNamedQueue ();
			NamedQueue.CreateNamedQueue( MacroscopeConstants.RecalculateDocCollection );

			StatsHistory = new Dictionary<string,Boolean> ( 1024 );
			StatsHostnames = new Dictionary<string,int> ( 16 );
			StatsTitles = new Dictionary<string,int> ( 1024 );
			StatsDescriptions = new Dictionary<string,int> ( 1024 );
			
			SemaphoreRecalc = new Semaphore ( 0, 1 );
			this.StartRecalcTimer();

			this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZED." );
			
		}

		/**************************************************************************/

		~MacroscopeDocumentCollection ()
		{
			this.DebugMsg( "MacroscopeDocumentCollection DESTRUCTOR CALLED" );
			this.StopRecalcTimer();
		}

		/** Document Collection Methods *******************************************/

		public Boolean ContainsDocument ( string sKey )
		{
			Boolean sResult = false;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				sResult = true;
			}
			return( sResult );
		}

		/**************************************************************************/
		
		public int CountDocuments ()
		{
			return( this.DocCollection.Count );
		}
				
		/**************************************************************************/
				
		public void AddDocument ( string sKey, MacroscopeDocument msDoc )
		{
			if( this.DocCollection.ContainsKey( sKey ) ) {
				this.RemoveDocument( sKey );
			}
			lock( this.DocCollection ) {
				this.DocCollection.Add( sKey, msDoc );
			}
		}

		/**************************************************************************/

		public Boolean DocumentExists ( string sKey )
		{
			Boolean bExists = false;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				bExists = true;
			}
			return( bExists );
		}

		/**************************************************************************/

		public MacroscopeDocument GetDocument ( string sKey )
		{
			MacroscopeDocument msDoc = null;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				msDoc = ( MacroscopeDocument )this.DocCollection[ sKey ];
			}
			return( msDoc );
		}

		/**************************************************************************/

		public void RemoveDocument ( string sKey )
		{
			if( this.DocCollection.ContainsKey( sKey ) ) {
				lock( this.DocCollection ) {
					this.DocCollection.Remove( sKey );
				}
			}
		}

		/**************************************************************************/
						
		public List<string> DocumentKeys ()
		{
			List<string> lKeys = new List<string> ();
			lock( this.DocCollection ) {
				foreach( string sKey in this.DocCollection.Keys ) {
					lKeys.Add( sKey );
				}
			}
			return( lKeys );
		}

		/** Recalculate Stats Across DocCollection ********************************/

		void StartRecalcTimer ()
		{
			this.DebugMsg( string.Format( "StartRecalcTimer: {0}", "STARTING..." ) );
			SemaphoreRecalc.Release( 1 );
			this.DebugMsg( string.Format( "StartRecalcTimer SemaphoreRecalc: {0}", "RELEASED" ) );
			this.TimerRecalc = new System.Timers.Timer ( 2000 );
			this.TimerRecalc.Elapsed += this.WorkerRecalculateDocCollection;
			this.TimerRecalc.AutoReset = true;
			this.TimerRecalc.Enabled = true;
			this.TimerRecalc.Start();
			this.DebugMsg( string.Format( "StartRecalcTimer: {0}", "STARTED." ) );
		}

		void StopRecalcTimer ()
		{
			try {
				this.TimerRecalc.Stop();
				this.TimerRecalc.Dispose();
			} catch( Exception ex ) {
				this.DebugMsg( string.Format( "StopRecalcTimer: {0}", ex.Message ) );
			}
		}

		void WorkerRecalculateDocCollection ( Object self, ElapsedEventArgs e )
		{
			DebugMsg( string.Format( "WorkerRecalculateDocCollection: {0}", "CALLED" ) );
			try {
				Boolean bDrainQueue = this.DrainWorkerRecalculateDocCollectionQueue();
				DebugMsg( string.Format( "bDrainQueue: {0}", bDrainQueue ) );
				if( bDrainQueue ) {
					this.RecalculateDocCollection();
				}
			} catch( Exception ex ) {
				DebugMsg( string.Format( "WorkerRecalculateDocCollection: {0}", ex.Message ) );
			}
		}

		/**************************************************************************/

		public void AddWorkerRecalculateDocCollectionQueue ()
		{
			this.NamedQueue.AddToNamedQueue( MacroscopeConstants.RecalculateDocCollection, "calc" );
		}
		
		/**************************************************************************/
		
		public Boolean DrainWorkerRecalculateDocCollectionQueue ()
		{
			Boolean bResult = false;
			try {
				if( this.NamedQueue.PeekNamedQueue( MacroscopeConstants.RecalculateDocCollection ) ) {
					bResult = true;
					this.NamedQueue.DrainNamedQueueItemsAsList( MacroscopeConstants.RecalculateDocCollection );
				}
			} catch( InvalidOperationException ex ) {
				this.DebugMsg( string.Format( "DrainWorkerRecalculateDocCollectionQueue: {0}", ex.Message ) );
			}
			return( bResult );
		}

		/**************************************************************************/

		public void RecalculateDocCollection ()
		{

			this.DebugMsg( string.Format( "RecalculateDocCollection: CALLED" ) );

			SemaphoreRecalc.WaitOne();

			lock( this.DocCollection ) {

				foreach( string sUrlTarget in this.DocCollection.Keys ) {

					MacroscopeDocument msDoc = this.GetDocument( sUrlTarget );

					this.RecalculateLinksIn( sUrlTarget, msDoc );

					if( this.StatsHistory.ContainsKey( sUrlTarget ) ) {

						this.DebugMsg( string.Format( "RecalculateDocCollection Already Seen: {0}", sUrlTarget ) );

					} else {

						this.DebugMsg( string.Format( "RecalculateDocCollection Adding: {0}", sUrlTarget ) );
					
						this.StatsHistory.Add( sUrlTarget, true );
					
						this.RecalculateHostnames( msDoc );
					
						this.RecalculateTitles( msDoc );
						
						this.RecalculateDescriptions( msDoc );

					}
					
				}
				
			}

			SemaphoreRecalc.Release();

		}

		/** Hostnames *************************************************************/

		void ClearHostnames ()
		{
			this.StatsHostnames.Clear();
		}

		public Dictionary<string,int> GetHostnamesWithCount ()
		{
			Dictionary<string,int> dicHostnames = new Dictionary<string,int> ( this.StatsHostnames.Count );
			lock( this.StatsHostnames ) {
				foreach( string sHostname in this.StatsHostnames.Keys ) {
					dicHostnames.Add( sHostname, this.StatsHostnames[ sHostname ] );
				}
			}
			return( dicHostnames );
		}

		public int GetHostnamesCount ( string sHostname )
		{
			int iValue = 0;
			if( this.StatsHostnames.ContainsKey( sHostname ) ) {
				iValue = this.StatsHostnames[ sHostname ];
			}
			return( iValue );
		}

		void RecalculateHostnames ( MacroscopeDocument msDoc )
		{
			string sUrl = msDoc.GetUrl();
			string sHostname = msDoc.GetHostname();
			if( ( sHostname != null ) && ( sHostname.Length > 0 ) ) {
				sHostname = sHostname.ToLower();
				if( this.StatsHostnames.ContainsKey( sHostname ) ) {
					lock( this.StatsHostnames ) {
						this.StatsHostnames[ sHostname ] = this.StatsHostnames[ sHostname ] + 1;
					}
				} else {
					lock( this.StatsHostnames ) {
						this.StatsHostnames.Add( sHostname, 1 );
					}
				}
			}
		}

		/** Titles ****************************************************************/

		void ClearTitles ()
		{
			this.StatsTitles.Clear();
		}

		public int GetTitleCount ( string sTitle )
		{
			int iValue = 0;
			if( this.StatsTitles.ContainsKey( sTitle ) ) {
				iValue = this.StatsTitles[ sTitle ];
			}
			return( iValue );
		}

		void RecalculateTitles ( MacroscopeDocument msDoc )
		{
			
			Boolean bProcess;
			
			if( msDoc.GetIsHtml() ) {
				bProcess = true;
			} else if( msDoc.GetIsPdf() ) {
				bProcess = true;
			} else {
				bProcess = false;
			}
			
			if( bProcess ) {
			
				string sUrl = msDoc.GetUrl();
				string sTitle = msDoc.GetTitle();

				if( this.StatsTitles.ContainsKey( sTitle ) ) {
					lock( this.StatsTitles ) {
						this.StatsTitles[ sTitle ] = this.StatsTitles[ sTitle ] + 1;
					}
				} else {
					lock( this.StatsTitles ) {
						this.StatsTitles.Add( sTitle, 1 );
					}
				}

			}
			
		}

		/** Descriptions **********************************************************/

		void ClearDescriptions ()
		{
			this.StatsDescriptions.Clear();
		}

		public int GetDescriptionCount ( string sDescription )
		{
			int iValue = 0;
			if( this.StatsDescriptions.ContainsKey( sDescription ) ) {
				iValue = this.StatsDescriptions[ sDescription ];
			}
			return( iValue );
		}

		void RecalculateDescriptions ( MacroscopeDocument msDoc )
		{
			
			Boolean bProcess;
			
			if( msDoc.GetIsHtml() ) {
				bProcess = true;
			} else if( msDoc.GetIsPdf() ) {
				bProcess = true;
			} else {
				bProcess = false;
			}
			
			if( bProcess ) {
			
				string sUrl = msDoc.GetUrl();
				string sDescription = msDoc.GetDescription();

				if( this.StatsDescriptions.ContainsKey( sDescription ) ) {
					lock( this.StatsDescriptions ) {
						this.StatsDescriptions[ sDescription ] = this.StatsDescriptions[ sDescription ] + 1;
					}
				} else {
					lock( this.StatsDescriptions ) {
						this.StatsDescriptions.Add( sDescription, 1 );
					}
				}

			}
			
		}

		/**************************************************************************/

		void RecalculateLinksIn ( string sUrlTarget, MacroscopeDocument msDoc )
		{

			msDoc.ClearHyperlinksIn();

			foreach( string sUrlOrigin in this.DocCollection.Keys ) {

				foreach( MacroscopeHyperlinkOut HyperlinkOut in msDoc.GetHyperlinksOut().IterateLinks( sUrlTarget ) ) {

					if( sUrlTarget == HyperlinkOut.GetUrlTarget() ) {
						msDoc.AddHyperlinkIn( "", "", MacroscopeHyperlinkIn.LINKTEXT, sUrlOrigin, sUrlTarget, "", "" );
					}

				}

			}

		}

		/**************************************************************************/

	}

}
