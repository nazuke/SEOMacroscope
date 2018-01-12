/*

	This file is part of SEOMacroscope.

	Copyright 2018 Jason Holland.

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
  /// Create and manage named queues.
  /// </summary>

  public class MacroscopeNamedQueue<T> : Macroscope
  {

    /**************************************************************************/

    public enum MODE
    {
      IGNORE_HISTORY = 0,
      USE_HISTORY = 1
    }

    private Dictionary<string, Queue<T>> NamedQueues;

    private Dictionary<string, Dictionary<T, bool>> NamedQueuesIndex; // Ensures that a new queued item is not added twice

    private Dictionary<string, MacroscopeNamedQueue<T>.MODE> NamedQueuesMode;

    private Dictionary<string, Dictionary<string, bool>> NamedQueuesHistory; // Ensures that a previously seen item is not queued again

    /**************************************************************************/

    public MacroscopeNamedQueue ()
    {

      this.SuppressDebugMsg = true;

      this.NamedQueues = new Dictionary<string, Queue<T>>( 32 );

      this.NamedQueuesIndex = new Dictionary<string, Dictionary<T, bool>>( 4096 );

      this.NamedQueuesMode = new Dictionary<string, MacroscopeNamedQueue<T>.MODE>( 4096 );

      this.NamedQueuesHistory = new Dictionary<string, Dictionary<string, bool>>( 4096 );

    }

    /**************************************************************************/

    public Queue<T> CreateNamedQueue ( string Name )
    {

      Queue<T> NamedQueue;

      this.NamedQueuesMode.Add( Name, MacroscopeNamedQueue<T>.MODE.IGNORE_HISTORY );

      if ( this.NamedQueues.ContainsKey( Name ) )
      {
        NamedQueue = this.NamedQueues[ Name ];
      }
      else
      {

        NamedQueue = new Queue<T>( 4096 );

        lock ( this.NamedQueues )
        {

          this.NamedQueues.Add( Name, NamedQueue );

          lock ( this.NamedQueues[ Name ] )
          {
            Dictionary<T, bool> NamedQueueIndex = new Dictionary<T, bool>( 4096 );
            this.NamedQueuesIndex.Add( Name, NamedQueueIndex );
          }

        }

      }

      return ( NamedQueue );

    }

    /** -------------------------------------------------------------------- **/

    public Queue<T> CreateNamedQueue ( string Name, MacroscopeNamedQueue<T>.MODE QueueMode )
    {

      Queue<T> NamedQueue = this.CreateNamedQueue( Name: Name );

      this.NamedQueuesMode[ Name ] = QueueMode;

      lock ( this.NamedQueues )
      {

        lock ( this.NamedQueues[ Name ] )
        {

          Dictionary<string, bool> NamedQueueHistory = new Dictionary<string, bool>( 4096 );

          this.NamedQueuesHistory.Add( Name, NamedQueueHistory );

        }

      }

      return ( NamedQueue );

    }

    /**************************************************************************/

    public void DeleteNamedQueue ( string Name )
    {

      if ( this.NamedQueues.ContainsKey( Name ) )
      {

        lock ( this.NamedQueues )
        {

          this.NamedQueues.Remove( Name );

          lock ( this.NamedQueuesIndex )
          {
            this.NamedQueuesIndex.Remove( Name );
          }

          if ( this.NamedQueuesMode[ Name ] == MacroscopeNamedQueue<T>.MODE.USE_HISTORY )
          {
            lock ( this.NamedQueuesHistory )
            {
              this.NamedQueuesHistory.Remove( Name );
            }
          }

        }

      }

    }

    /**************************************************************************/

    public Queue<T> AddToNamedQueue ( string Name, T Item )
    {

      Queue<T> NamedQueue;
      bool Proceed = true;

      if ( this.NamedQueues.ContainsKey( Name ) )
      {
        NamedQueue = this.NamedQueues[ Name ];
      }
      else
      {
        throw ( new MacroscopeNamedQueueException( string.Format( "Named queue \"{0}\" does not exist", Name ) ) );
      }

      if ( this.NamedQueuesMode[ Name ] == MacroscopeNamedQueue<T>.MODE.USE_HISTORY )
      {

        lock ( this.NamedQueuesHistory[ Name ] )
        {

          // TODO: This does not work with reference values
          if ( this.NamedQueuesHistory[ Name ].ContainsKey( Item.ToString() ) )
          {
            Proceed = false;
          }
          else
          {
            this.NamedQueuesHistory[ Name ].Add( Item.ToString(), true );
          }

        }

      }

      if ( Proceed )
      {

        lock ( this.NamedQueues[ Name ] )
        {

          if ( !NamedQueue.Contains( Item ) )
          {

            lock ( this.NamedQueuesIndex[ Name ] )
            {
              if ( !this.NamedQueuesIndex[ Name ].ContainsKey( Item ) )
              {
                this.NamedQueuesIndex[ Name ].Add( Item, true );
                NamedQueue.Enqueue( Item );
              }
            }

          }

        }

      }

      return ( NamedQueue );

    }

    /**************************************************************************/

    /*
     * Check to see if queue has items waiting in it.
    */
    public bool PeekNamedQueue ( string Name )
    {

      bool Peek = false;

      if ( this.NamedQueues.ContainsKey( Name ) )
      {
        lock ( this.NamedQueues[ Name ] )
        {
          if ( this.NamedQueues[ Name ].Count > 0 )
          {
            Peek = true;
          }
        }
      }

      return ( Peek );

    }

    /**************************************************************************/

    public int CountNamedQueueItems ( string Name )
    {

      int Count = 0;

      if ( this.NamedQueues.ContainsKey( Name ) )
      {

        lock ( this.NamedQueues[ Name ] )
        {

          if ( this.NamedQueues[ Name ].Count > 0 )
          {
            Count = this.NamedQueues[ Name ].Count;
          }

        }

      }

      return ( Count );

    }

    /**************************************************************************/

    public void ClearAllNamedQueues ()
    {

      lock ( this.NamedQueues )
      {

        lock ( this.NamedQueuesIndex )
        {

          lock ( this.NamedQueuesHistory )
          {

            foreach ( string Name in this.NamedQueues.Keys )
            {
              this.NamedQueues[ Name ].Clear();
              this.NamedQueuesIndex[ Name ].Clear();

              if ( this.NamedQueuesHistory.ContainsKey( Name ) )
              {
                this.NamedQueuesHistory[ Name ].Clear();
              }

            }

          }

        }

      }

    }

    /**************************************************************************/

    public void ClearNamedQueue ( string Name )
    {

      lock ( this.NamedQueues )
      {

        lock ( this.NamedQueuesIndex )
        {

          lock ( this.NamedQueuesHistory )
          {

            this.NamedQueues[ Name ].Clear();
            this.NamedQueuesIndex[ Name ].Clear();

            if ( this.NamedQueuesHistory.ContainsKey( Name ) )
            {
              this.NamedQueuesHistory[ Name ].Clear();
            }

          }

        }

      }

    }

    /**************************************************************************/

    public T GetNamedQueueItem ( string Name )
    {

      T Item = default( T );

      if ( this.NamedQueues.ContainsKey( Name ) )
      {

        lock ( this.NamedQueues[ Name ] )
        {

          if ( this.NamedQueues[ Name ].Count > 0 )
          {

            Item = this.NamedQueues[ Name ].Dequeue();

            lock ( this.NamedQueuesIndex[ Name ] )
            {
              if ( !EqualityComparer<T>.Default.Equals( Item, default( T ) ) )
              {
                this.NamedQueuesIndex[ Name ].Remove( Item );
              }
            }

          }

        }

      }

      return ( Item );

    }

    /**************************************************************************/

    public T[] GetNamedQueueItemsAsArray ( string Name )
    {

      // TODO: implement this, such that items can be pulled from the queue without being deleted

      T[] ItemsArray = null;

      if ( this.NamedQueues.ContainsKey( Name ) )
      {

        lock ( this.NamedQueues[ Name ] )
        {

          ItemsArray = this.NamedQueues[ Name ].ToArray();

        }

      }

      return ( ItemsArray );

    }

    /**************************************************************************/

    public List<T> DrainNamedQueueItemsAsList ( string Name )
    {

      List<T> lItems = new List<T>();

      if ( this.NamedQueues.ContainsKey( Name ) )
      {

        T Item = this.GetNamedQueueItem( Name );

        do
        {
          if ( !EqualityComparer<T>.Default.Equals( Item, default( T ) ) )
          {
            lItems.Add( Item );
          }
          Item = this.GetNamedQueueItem( Name );
        } while ( !EqualityComparer<T>.Default.Equals( Item, default( T ) ) );

      }

      return ( lItems );

    }

    /**************************************************************************/

    public List<T> DrainNamedQueueItemsAsList ( string Name, int Limit )
    {

      List<T> Items = new List<T>();
      int Count = 0;

      if ( this.NamedQueues.ContainsKey( Name ) )
      {

        T Item = this.GetNamedQueueItem( Name );

        do
        {

          if ( !EqualityComparer<T>.Default.Equals( Item, default( T ) ) )
          {
            Items.Add( Item );
          }

          Item = this.GetNamedQueueItem( Name );

          Count++;

          if ( Count >= Limit )
          {
            break;
          }

        } while ( !EqualityComparer<T>.Default.Equals( Item, default( T ) ) );

      }

      return ( Items );

    }

    /**************************************************************************/

    public bool ForgetNamedQueueItem ( string Name, T Item )
    {

      bool Forgotten = false;

      if ( this.NamedQueuesIndex.ContainsKey( Name ) )
      {
        lock ( this.NamedQueuesIndex[ Name ] )
        {
          if ( this.NamedQueuesIndex[ Name ].ContainsKey( Item ) )
          {
            this.NamedQueuesIndex[ Name ].Remove( Item );
          }
          else
          {
            this.DebugMsg( string.Format( "Not in NamedQueuesIndex: {0} :: {1}", Name, Item.ToString() ) );
          }
        }
      }

      if ( this.NamedQueuesHistory.ContainsKey( Name ) )
      {
        lock ( this.NamedQueuesHistory[ Name ] )
        {
          if ( this.NamedQueuesHistory[ Name ].ContainsKey( Item.ToString() ) )
          {
            this.NamedQueuesHistory[ Name ].Remove( Item.ToString() );
            Forgotten = true;
          }
          else
          {
            this.DebugMsg( string.Format( "Not in NamedQueuesHistory: {0} :: {1}", Name, Item.ToString() ) );
          }
        }
      }

      return ( Forgotten );

    }

    /**************************************************************************/

  }

}
