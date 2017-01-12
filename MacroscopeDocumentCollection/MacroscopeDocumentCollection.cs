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

	public class MacroscopeDocumentCollection : Macroscope
	{

		/**************************************************************************/

		Hashtable DocCollection;

		Thread ThreadRecalculateLinksIn = null;
		Boolean ThreadRecalculateLinksInStop = false;
		Queue<int> ThreadRecalculateLinksInQueue;

		Semaphore ThreadRecalculateLinksInSemaphone;

		/**************************************************************************/

		public MacroscopeDocumentCollection ()
		{

			DocCollection = Hashtable.Synchronized( new Hashtable ( 4096 ) );

			{
				ThreadRecalculateLinksInQueue = new Queue<int> ( 4096 );
				ThreadRecalculateLinksIn = new Thread ( new ThreadStart ( this.WorkerRecalculateLinksIn ) );
				ThreadRecalculateLinksInSemaphone = new Semaphore ( 0, 1 );
				ThreadRecalculateLinksIn.Start();
			}

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

		/**************************************************************************/

		void WorkerRecalculateLinksIn ()
		{

			Boolean bDoRun = true;
			
			ThreadRecalculateLinksInSemaphone.Release( 1 );
			
			do {

				if( this.WorkerRecalculateLinksInQueuePeek() ) {

					{
						int iDrainQueue = this.WorkerRecalculateLinksInQueueGet();
						do {
							iDrainQueue = this.WorkerRecalculateLinksInQueueGet();
						} while( iDrainQueue != -1 );
					}

					this.RecalculateLinksIn();

				}

				Thread.Sleep( 5000 );

				if( this.ThreadRecalculateLinksInStop == true ) {
					bDoRun = false;
				}

			} while( bDoRun == true );

		}

		/**************************************************************************/
				
		public void WorkerRecalculateLinksInShutdown ()
		{
			debug_msg( "WorkerRecalculateLinksInShutdown Called" );
			this.ThreadRecalculateLinksInStop = true;
		}

		/**************************************************************************/

		public void WorkerRecalculateLinksInQueueAdd ( int iValue )
		{
			lock( this.ThreadRecalculateLinksInQueue ) {
				this.ThreadRecalculateLinksInQueue.Enqueue( iValue );
			}
		}
		
		/**************************************************************************/
		
		public int WorkerRecalculateLinksInQueueGet ()
		{
			int iValue = -1;
			try {
				if( this.ThreadRecalculateLinksInQueue.Count > 0 ) {
					lock( this.ThreadRecalculateLinksInQueue ) {
						iValue = this.ThreadRecalculateLinksInQueue.Dequeue();
					}
				}
			} catch( InvalidOperationException ex ) {
				debug_msg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( iValue );
		}
	
		/**************************************************************************/
				
		public Boolean WorkerRecalculateLinksInQueuePeek ()
		{
			Boolean bPeek = false;
			try {
				lock( this.ThreadRecalculateLinksInQueue ) {
					if( this.ThreadRecalculateLinksInQueue.Count > 0 ) {
						bPeek = true;
					}
				}
			} catch( InvalidOperationException ex ) {
				debug_msg( string.Format( "InvalidOperationException: {0}", ex.Message ) );
			}
			return( bPeek );
		}

		/**************************************************************************/
		
		public void RecalculateLinksIn ()
		{

			debug_msg( string.Format( "RecalculateLinksIn: CALLED" ), 1 );

			ThreadRecalculateLinksInSemaphone.WaitOne();

			lock( this.DocCollection ) {

				foreach( string sUrlTarget in this.DocCollection.Keys ) {

					MacroscopeDocument msDoc = this.Get( sUrlTarget );

					msDoc.ClearHyperlinksIn();

					//debug_msg( string.Format( "RecalculateLinksIn sUrlTarget: {0}", sUrlTarget ), 0 );

					foreach( string sUrlOrigin in this.DocCollection.Keys ) {

						//debug_msg( string.Format( "RecalculateLinksIn sUrlOrigin: {0}", sUrlOrigin ), 1 );

						foreach( MacroscopeHyperlinkOut HyperlinkOut in msDoc.GetHyperlinksOut().IterateLinks( sUrlTarget ) ) {

							if( sUrlTarget == HyperlinkOut.GetUrlTarget() ) {

								msDoc.AddHyperlinkIn( "", "", MacroscopeHyperlinkIn.LINKTEXT, sUrlOrigin, sUrlTarget, "", "" );

							}

						}

					}

				}
				
			}
			
			ThreadRecalculateLinksInSemaphone.Release();

		}

		/**************************************************************************/

	}

}
