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

	public class MacroscopeDocumentCollection : Macroscope
	{

		/**************************************************************************/

		Dictionary<string,MacroscopeDocument> DocCollection;

		Thread ThreadRecalculateDocCollection = null;
		Boolean ThreadRecalculateDocCollectionStop = false;
		Queue<int> ThreadRecalculateDocCollectionQueue;
		Semaphore ThreadRecalculateDocCollectionSemaphore;

		Dictionary<string,int> StatsTitles;

		/**************************************************************************/

		public MacroscopeDocumentCollection ()
		{

			DocCollection = new Dictionary<string,MacroscopeDocument> ( 4096 );

			{
				ThreadRecalculateDocCollectionQueue = new Queue<int> ( 4096 );
				ThreadRecalculateDocCollection = new Thread ( new ThreadStart ( this.WorkerRecalculateDocCollection ) );
				ThreadRecalculateDocCollectionSemaphore = new Semaphore ( 0, 1 );
				ThreadRecalculateDocCollection.Start();
			}

			StatsTitles = new Dictionary<string,int> ();

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
				msDoc = ( MacroscopeDocument )this.DocCollection[sKey];
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

		/**************************************************************************/

		void WorkerRecalculateDocCollection ()
		{

			Boolean bDoRun = true;
			
			ThreadRecalculateDocCollectionSemaphore.Release( 1 );
			
			do {

				if( this.WorkerRecalculateDocCollectionQueuePeek() ) {

					{
						int iDrainQueue = this.WorkerRecalculateDocCollectionQueueGet();
						do {
							iDrainQueue = this.WorkerRecalculateDocCollectionQueueGet();
						} while( iDrainQueue != -1 );
					}

					this.RecalculateDocCollection();

				}

				Thread.Sleep( 5000 );

				if( this.ThreadRecalculateDocCollectionStop == true ) {
					bDoRun = false;
				}

			} while( bDoRun == true );

		}

		/**************************************************************************/
				
		public void WorkerRecalculateDocCollectionShutdown ()
		{
			DebugMsg( "WorkerRecalculateLinksInShutdown Called" );
			this.ThreadRecalculateDocCollectionStop = true;
		}

		/**************************************************************************/

		public void WorkerRecalculateDocCollectionQueueAdd ( int iValue )
		{
			lock( this.ThreadRecalculateDocCollectionQueue ) {
				this.ThreadRecalculateDocCollectionQueue.Enqueue( iValue );
			}
		}
		
		/**************************************************************************/
		
		public int WorkerRecalculateDocCollectionQueueGet ()
		{
			int iValue = -1;
			try {
				if( this.ThreadRecalculateDocCollectionQueue.Count > 0 ) {
					lock( this.ThreadRecalculateDocCollectionQueue ) {
						iValue = this.ThreadRecalculateDocCollectionQueue.Dequeue();
					}
				}
			} catch( InvalidOperationException ex ) {
				DebugMsg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( iValue );
		}
	
		/**************************************************************************/
				
		public Boolean WorkerRecalculateDocCollectionQueuePeek ()
		{
			Boolean bPeek = false;
			try {
				lock( this.ThreadRecalculateDocCollectionQueue ) {
					if( this.ThreadRecalculateDocCollectionQueue.Count > 0 ) {
						bPeek = true;
					}
				}
			} catch( InvalidOperationException ex ) {
				DebugMsg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( bPeek );
		}

		/**************************************************************************/

		public void RecalculateDocCollection ()
		{

			DebugMsg( string.Format( "RecalculateDocCollection: CALLED" ) );

			ThreadRecalculateDocCollectionSemaphore.WaitOne();

			lock( this.DocCollection ) {

				this.ClearTitles();
				
				foreach( string sUrlTarget in this.DocCollection.Keys ) {

					MacroscopeDocument msDoc = this.Get( sUrlTarget );

					this.RecalculateTitles( msDoc );

					this.RecalculateLinksIn( sUrlTarget, msDoc );

				}
				
			}
			
			ThreadRecalculateDocCollectionSemaphore.Release();

		}

		/**************************************************************************/

		void ClearTitles ()
		{
			StatsTitles.Clear();
		}

		public int GetTitleCount ( string sTitle )
		{
			int iValue = 0;
			if( this.StatsTitles.ContainsKey( sTitle ) ) {
				iValue = this.StatsTitles[sTitle];
			}
			return( iValue );
		}

		void RecalculateTitles ( MacroscopeDocument msDoc )
		{
			string sTitle = msDoc.GetTitle();
			if( this.StatsTitles.ContainsKey( sTitle ) ) {
				this.StatsTitles[sTitle] = this.StatsTitles[sTitle] + 1;
			} else {
				this.StatsTitles.Add( sTitle, 1 );
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
