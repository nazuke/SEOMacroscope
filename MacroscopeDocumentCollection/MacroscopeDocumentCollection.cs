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

		public new Boolean SuppressDebugMsg = false;
				
		/**************************************************************************/

		Dictionary<string,MacroscopeDocument> DocCollection;

		const string constRecalculateDocCollection = "RecalculateDocCollection";
		MacroscopeNamedQueue NamedQueue;

		Dictionary<string,int> StatsTitles;
		Dictionary<string,Boolean> StatsTitlesHistory;

		Semaphore SemaphoreRecalc;
		System.Timers.Timer TimeRecalc;

		/**************************************************************************/

		public MacroscopeDocumentCollection ()
		{
			
			this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZING..." );
			
			DocCollection = new Dictionary<string,MacroscopeDocument> ( 4096 );

			NamedQueue = new MacroscopeNamedQueue ();
			NamedQueue.CreateNamedQueue( constRecalculateDocCollection );

			StatsTitles = new Dictionary<string,int> ();
			StatsTitlesHistory = new Dictionary<string,Boolean> ();
			
			SemaphoreRecalc = new Semaphore ( 0, 1 );
			StartRecalcTimer();

			this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZED." );
			
		}

		/**************************************************************************/

		~MacroscopeDocumentCollection ()
		{
			this.DebugMsg( "MacroscopeDocumentCollection DESTRUCTOR CALLED" );
			this.StopRecalcTimer();
		}

		/**************************************************************************/

		public Boolean Contains ( string sKey )
		{
			Boolean sResult = false;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				sResult = true;
			}
			return( sResult );
		}

		/**************************************************************************/
		
		public int Count ()
		{
			return( this.DocCollection.Count );
		}
				
		/**************************************************************************/
				
		public void Add ( string sKey, MacroscopeDocument msDoc )
		{
			if( this.DocCollection.ContainsKey( sKey ) ) {
				this.Remove( sKey );
			}
			lock( this.DocCollection ) {
				this.DocCollection.Add( sKey, msDoc );
			}
		}

		/**************************************************************************/

		public Boolean Exists ( string sKey )
		{
			Boolean bExists = false;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				bExists = true;
			}
			return( bExists );
		}

		/**************************************************************************/

		public MacroscopeDocument Get ( string sKey )
		{
			MacroscopeDocument msDoc = null;
			if( this.DocCollection.ContainsKey( sKey ) ) {
				msDoc = ( MacroscopeDocument )this.DocCollection[ sKey ];
			}
			return( msDoc );
		}

		/**************************************************************************/

		public void Remove ( string sKey )
		{
			if( this.DocCollection.ContainsKey( sKey ) ) {
				lock( this.DocCollection ) {
					this.DocCollection.Remove( sKey );
				}
			}
		}

		/**************************************************************************/
						
		public List<string> Keys ()
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
			SemaphoreRecalc.Release( 1 );
			this.TimeRecalc = new System.Timers.Timer ( 1000 );
			this.TimeRecalc.Elapsed += this.WorkerRecalculateDocCollection;
			this.TimeRecalc.AutoReset = true;
			this.TimeRecalc.Enabled = true;
			this.TimeRecalc.Start();
		}

		void StopRecalcTimer ()
		{
			try {
				this.TimeRecalc.Stop();
				this.TimeRecalc.Dispose();
			} catch( Exception ex ) {
				this.DebugMsg( string.Format( "StopStatusBarTimer: {0}", ex.Message ) );
			}
		}
		
		void WorkerRecalculateDocCollection ( Object self, ElapsedEventArgs e )
		{

			//this.DebugMsg( "CALC: STARTING" );

			if( this.PeekWorkerRecalculateDocCollectionQueue() ) {

				Boolean bDrainQueue = this.GetWorkerRecalculateDocCollectionQueue();
				do {
					bDrainQueue = this.GetWorkerRecalculateDocCollectionQueue();
				} while( bDrainQueue );

				this.RecalculateDocCollection();
			}

		}

		/**************************************************************************/

		public void AddWorkerRecalculateDocCollectionQueue ()
		{
			this.NamedQueue.AddToNamedQueue( constRecalculateDocCollection, "calc" );
		}
		
		/**************************************************************************/
		
		public Boolean GetWorkerRecalculateDocCollectionQueue ()
		{
			Boolean bResult = false;
			try {
				if( this.NamedQueue.PeekNamedQueue( constRecalculateDocCollection ) ) {
					bResult = true;
					this.NamedQueue.GetNamedQueueItem( constRecalculateDocCollection );
				}
			} catch( InvalidOperationException ex ) {
				this.DebugMsg( string.Format( "GetWorkerRecalculateDocCollectionQueue: {0}", ex.Message ) );
			}
			return( bResult );
		}
	
		/**************************************************************************/
				
		public Boolean PeekWorkerRecalculateDocCollectionQueue ()
		{
			return( this.NamedQueue.PeekNamedQueue( constRecalculateDocCollection ) );
		}

		/**************************************************************************/

		public void RecalculateDocCollection ()
		{

			this.DebugMsg( string.Format( "RecalculateDocCollection: CALLED" ) );

			SemaphoreRecalc.WaitOne();

			lock( this.DocCollection ) {

				foreach( string sUrlTarget in this.DocCollection.Keys ) {

					MacroscopeDocument msDoc = this.Get( sUrlTarget );

					this.RecalculateTitles( msDoc );

					this.RecalculateLinksIn( sUrlTarget, msDoc );

				}
				
			}

			SemaphoreRecalc.Release();

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
			
				this.DebugMsg( string.Format( "RecalculateTitles Processing: {0}", sUrl ) );
					
				if( this.StatsTitlesHistory.ContainsKey( sUrl ) ) {
					this.DebugMsg( string.Format( "RecalculateTitles Already Seen: {0}", sUrl ) );
				} else {

					this.DebugMsg( string.Format( "RecalculateTitles Adding: {0}", sTitle ) );

					this.StatsTitlesHistory.Add( sUrl, true );

					if( this.StatsTitles.ContainsKey( sTitle ) ) {
						this.StatsTitles[ sTitle ] = this.StatsTitles[ sTitle ] + 1;
					} else {
						this.StatsTitles.Add( sTitle, 1 );
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
