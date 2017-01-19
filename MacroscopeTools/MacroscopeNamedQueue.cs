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

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeNamedQueue.
	/// </summary>

	public class MacroscopeNamedQueue : Macroscope
	{
		
		/**************************************************************************/

		Dictionary<string,Queue<string>> NamedQueues;

		Dictionary<string,Dictionary<string,Boolean>> NamedQueuesIndex;

		/**************************************************************************/

		public MacroscopeNamedQueue ()
		{
			NamedQueues = new Dictionary<string,Queue<string>> ( 32 );
			NamedQueuesIndex = new Dictionary<string,Dictionary<string,Boolean>> ( 4096 );
		}

		/**************************************************************************/

		public Queue<string> CreateNamedQueue ( string sName )
		{
			Queue<string> NamedQueue;
			if( this.NamedQueues.ContainsKey( sName ) ) {
				NamedQueue = this.NamedQueues[ sName ];
			} else {
				NamedQueue = new Queue<string> ( 4096 );
				lock( this.NamedQueues ) {
					this.NamedQueues.Add( sName, NamedQueue );
					lock( this.NamedQueues[sName] ) {
						Dictionary<string,Boolean> NamedQueueIndex = new Dictionary<string,Boolean> ( 4096 );
						this.NamedQueuesIndex.Add( sName, NamedQueueIndex );
					}
				}
			}
			return( NamedQueue );
		}
		
		/**************************************************************************/
		
		public void DeleteNamedQueue ( string sName )
		{
			if( this.NamedQueues.ContainsKey( sName ) ) {
				lock( this.NamedQueues ) {
					this.NamedQueues.Remove( sName );
					lock( this.NamedQueuesIndex ) {
						this.NamedQueuesIndex.Remove( sName );
					}
				}
			}
		}

		/**************************************************************************/

		public Queue<string> AddToNamedQueue ( string sName, string sItem )
		{
			Queue<string> NamedQueue;
			if( this.NamedQueues.ContainsKey( sName ) ) {
				NamedQueue = this.NamedQueues[ sName ];
			} else {
				NamedQueue = this.CreateNamedQueue( sName );
			}
			lock( this.NamedQueues[sName] ) {
				if( !this.NamedQueuesIndex[ sName ].ContainsKey( sItem ) ) {
					lock( this.NamedQueuesIndex[sName] ) {
						this.NamedQueuesIndex[ sName ].Add( sItem, true );
						NamedQueue.Enqueue( sItem );
					}
				}
			}
			return( NamedQueue );
		}

		/**************************************************************************/

		public Boolean PeekNamedQueue ( string sName )
		{
			DebugMsg( string.Format( "PeekNamedQueue: {0}", sName ) );
			Boolean bPeek = false;
			if( this.NamedQueues.ContainsKey( sName ) ) {
				lock( this.NamedQueues[sName] ) {
					if( this.NamedQueues[ sName ].Count > 0 ) {
						bPeek = true;
					}
				}
			}
			return( bPeek );
		}

		/**************************************************************************/

		public int CountNamedQueueItems ( string sName )
		{
			int iCount = 0;
			if( this.NamedQueues.ContainsKey( sName ) ) {
				lock( this.NamedQueues[sName] ) {
					if( this.NamedQueues[ sName ].Count > 0 ) {
						iCount = this.NamedQueues[ sName ].Count;
					}
				}
			}
			return( iCount );
		}

		/**************************************************************************/

		public string GetNamedQueueItem ( string sName )
		{
			string sItem = null;
			if( this.NamedQueues.ContainsKey( sName ) ) {
				lock( this.NamedQueues[sName] ) {
					lock( this.NamedQueuesIndex[sName] ) {
						if( this.NamedQueues[ sName ].Count > 0 ) {
							sItem = this.NamedQueues[ sName ].Dequeue();
							if( sItem != null ) {
								this.NamedQueuesIndex[ sName ].Remove( sItem );
							}
						}
					}
				}
			}
			return( sItem );
		}

		/**************************************************************************/

		public List<string> GetNamedQueueItemsAsList ( string sName )
		{
			// TODO: implement this, such that items can be pulled from the queue without being deleted
			List<string> lItems = new List<string> ();
			if( this.NamedQueues.ContainsKey( sName ) ) {
				string sItem = this.GetNamedQueueItem( sName );
				do {
					if( sItem != null ) {
						lItems.Add( sItem );
					}
					sItem = this.GetNamedQueueItem( sName );
				} while( sItem != null );
			}
			return( lItems );
		}

		/**************************************************************************/

		public List<string> DrainNamedQueueItemsAsList ( string sName )
		{
			List<string> lItems = new List<string> ();
			if( this.NamedQueues.ContainsKey( sName ) ) {
				string sItem = this.GetNamedQueueItem( sName );
				do {
					if( sItem != null ) {
						lItems.Add( sItem );
					}
					sItem = this.GetNamedQueueItem( sName );
				} while( sItem != null );
			}
			return( lItems );
		}
		
		/**************************************************************************/

	}

}
