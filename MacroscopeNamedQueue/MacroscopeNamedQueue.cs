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

namespace SEOMacroscope
{

	/// <summary>
	/// Create and manage names queues.
	/// </summary>

	public class MacroscopeNamedQueue : Macroscope
	{

		/**************************************************************************/

		private Dictionary<string,Queue<string>> NamedQueues;

		private Dictionary<string,Dictionary<string,Boolean>> NamedQueuesIndex;

		/**************************************************************************/

		public MacroscopeNamedQueue ()
		{
			NamedQueues = new Dictionary<string,Queue<string>> ( 32 );
			NamedQueuesIndex = new Dictionary<string,Dictionary<string,Boolean>> ( 4096 );
		}

		/**************************************************************************/

		public Queue<string> CreateNamedQueue ( string Name )
		{
			Queue<string> NamedQueue;
			if( this.NamedQueues.ContainsKey( Name ) )
			{
				NamedQueue = this.NamedQueues[ Name ];
			}
			else
			{
				NamedQueue = new Queue<string> ( 4096 );
				lock( this.NamedQueues )
				{
					this.NamedQueues.Add( Name, NamedQueue );
					lock( this.NamedQueues[Name] )
					{
						Dictionary<string,Boolean> NamedQueueIndex = new Dictionary<string,Boolean> ( 4096 );
						this.NamedQueuesIndex.Add( Name, NamedQueueIndex );
					}
				}
			}
			return( NamedQueue );
		}

		/**************************************************************************/

		public void DeleteNamedQueue ( string Name )
		{
			if( this.NamedQueues.ContainsKey( Name ) )
			{
				lock( this.NamedQueues )
				{
					this.NamedQueues.Remove( Name );
					lock( this.NamedQueuesIndex )
					{
						this.NamedQueuesIndex.Remove( Name );
					}
				}
			}
		}

		/**************************************************************************/

		public Queue<string> AddToNamedQueue ( string Name, string Item )
		{
			Queue<string> NamedQueue;
			if( this.NamedQueues.ContainsKey( Name ) )
			{
				NamedQueue = this.NamedQueues[ Name ];
			}
			else
			{
				NamedQueue = this.CreateNamedQueue( Name );
			}
			lock( this.NamedQueues[Name] )
			{
				if( !this.NamedQueuesIndex[ Name ].ContainsKey( Item ) )
				{
					lock( this.NamedQueuesIndex[Name] )
					{
						this.NamedQueuesIndex[ Name ].Add( Item, true );
						NamedQueue.Enqueue( Item );
					}
				}
			}
			return( NamedQueue );
		}

		/**************************************************************************/

		public Boolean PeekNamedQueue ( string Name )
		{
			//DebugMsg( string.Format( "PeekNamedQueue: {0}", Name ) );
			Boolean bPeek = false;
			if( this.NamedQueues.ContainsKey( Name ) )
			{
				lock( this.NamedQueues[Name] )
				{
					if( this.NamedQueues[ Name ].Count > 0 )
					{
						bPeek = true;
					}
				}
			}
			return( bPeek );
		}

		/**************************************************************************/

		public int CountNamedQueueItems ( string Name )
		{
			int iCount = 0;
			if( this.NamedQueues.ContainsKey( Name ) )
			{
				lock( this.NamedQueues[Name] )
				{
					if( this.NamedQueues[ Name ].Count > 0 )
					{
						iCount = this.NamedQueues[ Name ].Count;
					}
				}
			}
			return( iCount );
		}

		/**************************************************************************/

		public void ClearAllNamedQueues ()
		{
			lock( this.NamedQueues )
			{
				lock( this.NamedQueuesIndex )
				{
					foreach( string Name in this.NamedQueues.Keys )
					{
						this.NamedQueues[ Name ].Clear();
						this.NamedQueuesIndex[ Name ].Clear();
					}
				}
			}
		}

		/**************************************************************************/

		public void ClearNamedQueue ( string Name )
		{
			lock( this.NamedQueues )
			{
				lock( this.NamedQueuesIndex )
				{
					this.NamedQueues[ Name ].Clear();
					this.NamedQueuesIndex[ Name ].Clear();
				}
			}
		}

		/**************************************************************************/

		public string GetNamedQueueItem ( string Name )
		{
			string Item = null;

			lock( this.NamedQueues[Name] )
			{

				if( this.NamedQueues.ContainsKey( Name ) )
				{

					if( this.NamedQueues[ Name ].Count > 0 )
					{

						Item = this.NamedQueues[ Name ].Dequeue();

						if( Item != null )
						{

							lock( this.NamedQueuesIndex[Name] )
							{
								this.NamedQueuesIndex[ Name ].Remove( Item );
							}

						}

					}

				}

			}
			return( Item );
		}

		/**************************************************************************/

		public List<string> GetNamedQueueItemsAsList ( string Name )
		{
			// TODO: implement this, such that items can be pulled from the queue without being deleted
			List<string> lItems = new List<string> ();
			if( this.NamedQueues.ContainsKey( Name ) )
			{
				string Item = this.GetNamedQueueItem( Name );
				do
				{
					if( Item != null )
					{
						lItems.Add( Item );
					}
					Item = this.GetNamedQueueItem( Name );
				} while( Item != null );
			}
			return( lItems );
		}

		/**************************************************************************/

		public List<string> DrainNamedQueueItemsAsList ( string Name )
		{

			List<string> lItems = new List<string> ();

			if( this.NamedQueues.ContainsKey( Name ) )
			{

				string Item = this.GetNamedQueueItem( Name );

				do
				{
					if( Item != null )
					{
						lItems.Add( Item );
					}
					Item = this.GetNamedQueueItem( Name );
				} while( Item != null );

			}

			return( lItems );

		}

		/**************************************************************************/

		public List<string> DrainNamedQueueItemsAsList ( string Name, int iLimit )
		{
			List<string> lItems = new List<string> ();
			int iCount = 0;

			if( this.NamedQueues.ContainsKey( Name ) )
			{

				string Item = this.GetNamedQueueItem( Name );

				do
				{

					if( Item != null )
					{
						lItems.Add( Item );
					}

					Item = this.GetNamedQueueItem( Name );

					iCount++;

					if( iCount >= iLimit )
					{
						break;
					}

				} while( Item != null );

			}

			return( lItems );
		}

		/**************************************************************************/

	}

}
