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
  /// Create and manage named queues.
  /// </summary>

  public class MacroscopeNamedQueue : Macroscope
  {

    /**************************************************************************/

    public enum MODE
    {
      IGNORE_HISTORY = 0,
      USE_HISTORY = 1
    }
    
    private Dictionary<string,Queue<string>> NamedQueues;

    private Dictionary<string,Dictionary<string,Boolean>> NamedQueuesIndex;

    private Dictionary<string,MacroscopeNamedQueue.MODE> NamedQueuesMode;
    
    private Dictionary<string,Dictionary<string,Boolean>> NamedQueuesHistory;

    /**************************************************************************/

    public MacroscopeNamedQueue ()
    {
     
      this.NamedQueues = new Dictionary<string,Queue<string>> ( 32 );

      this.NamedQueuesIndex = new Dictionary<string,Dictionary<string,Boolean>> ( 4096 );
      
      this.NamedQueuesMode = new Dictionary<string,MacroscopeNamedQueue.MODE> ( 4096 );

      this.NamedQueuesHistory = new Dictionary<string,Dictionary<string,Boolean>> ( 4096 );

    }

    /**************************************************************************/

    public Queue<string> CreateNamedQueue ( string Name )
    {

      Queue<string> NamedQueue;

      this.NamedQueuesMode.Add( Name, MacroscopeNamedQueue.MODE.IGNORE_HISTORY );

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

    /** -------------------------------------------------------------------- **/

    public Queue<string> CreateNamedQueue ( string Name, MacroscopeNamedQueue.MODE QueueMode )
    {

      Queue<string> NamedQueue = this.CreateNamedQueue( Name: Name );

      this.NamedQueuesMode[ Name ] = QueueMode;

      lock( this.NamedQueues )
      {

        lock( this.NamedQueues[Name] )
        {
          Dictionary<string,Boolean> NamedQueueHistory = new Dictionary<string,Boolean> ( 4096 );
          this.NamedQueuesHistory.Add( Name, NamedQueueHistory );
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

          if( this.NamedQueuesMode[ Name ] == MacroscopeNamedQueue.MODE.USE_HISTORY )
          {
            lock( this.NamedQueuesHistory )
            {
              this.NamedQueuesHistory.Remove( Name );
            }
          }

        }

      }

    }

    /**************************************************************************/

    public Queue<string> AddToNamedQueue ( string Name, string Item )
    {

      Queue<string> NamedQueue;
      Boolean Proceed = true;
      
      if( this.NamedQueues.ContainsKey( Name ) )
      {
        NamedQueue = this.NamedQueues[ Name ];
      }
      else
      {
        throw( new MacroscopeNamedQueueException ( string.Format( "Named queue \"{0}\" does not exist", Name ) ) );
      }

      if( this.NamedQueuesMode[ Name ] == MacroscopeNamedQueue.MODE.USE_HISTORY )
      {
        lock( this.NamedQueuesHistory[Name] )
        {
          if( this.NamedQueuesHistory[ Name ].ContainsKey( Item ) )
          {
            Proceed = false;
            throw( new MacroscopeNamedQueueException ( string.Format( "Item already seen in queue \"{0}\"", Name ) ) );
          }
          else
          {
            this.NamedQueuesHistory[ Name ].Add( Item, true );
          }
        }
      }

      if( Proceed )
      {
        
        lock( this.NamedQueues[Name] )
        {

          if( !NamedQueue.Contains( Item ) )
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

        }
      
      }
      
      return( NamedQueue );

    }

    /**************************************************************************/

    public Boolean PeekNamedQueue ( string Name )
    {

      Boolean Peek = false;

      if( this.NamedQueues.ContainsKey( Name ) )
      {
        lock( this.NamedQueues[Name] )
        {
          if( this.NamedQueues[ Name ].Count > 0 )
          {
            Peek = true;
          }
        }
      }

      return( Peek );

    }

    /**************************************************************************/

    public int CountNamedQueueItems ( string Name )
    {

      int Count = 0;

      if( this.NamedQueues.ContainsKey( Name ) )
      {

        lock( this.NamedQueues[Name] )
        {

          if( this.NamedQueues[ Name ].Count > 0 )
          {
            Count = this.NamedQueues[ Name ].Count;
          }

        }

      }

      return( Count );

    }

    /**************************************************************************/

    public void ClearAllNamedQueues ()
    {

      lock( this.NamedQueues )
      {

        lock( this.NamedQueuesIndex )
        {

          lock( this.NamedQueuesHistory )
          {

            foreach( string Name in this.NamedQueues.Keys )
            {
              this.NamedQueues[ Name ].Clear();
              this.NamedQueuesIndex[ Name ].Clear();

              if( this.NamedQueuesHistory.ContainsKey( Name ) )
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

      lock( this.NamedQueues )
      {

        lock( this.NamedQueuesIndex )
        {

          lock( this.NamedQueuesHistory )
          {

            this.NamedQueues[ Name ].Clear();
            this.NamedQueuesIndex[ Name ].Clear();

            if( this.NamedQueuesHistory.ContainsKey( Name ) )
            {
              this.NamedQueuesHistory[ Name ].Clear();
            }

          }
          
        }

      }

    }

    /**************************************************************************/

    public string GetNamedQueueItem ( string Name )
    {
      string Item = null;

      if( this.NamedQueues.ContainsKey( Name ) )
      {

        lock( this.NamedQueues[Name] )
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

    public string [] GetNamedQueueItemsAsArray ( string Name )
    {
		
      // TODO: implement this, such that items can be pulled from the queue without being deleted

      string [] ItemsArray = null;

      if( this.NamedQueues.ContainsKey( Name ) )
      {
        
        lock( this.NamedQueues[Name] )
        {

          ItemsArray = this.NamedQueues[ Name ].ToArray();

        }

      }

      return( ItemsArray );

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

    public void  ForgetNamedQueueItem ( string Name, string Item )
    {

      if( this.NamedQueuesIndex.ContainsKey( Name ) )
      {
              
        if( this.NamedQueuesIndex[ Name ].ContainsKey( Item ) )
        {

          lock( this.NamedQueuesIndex[Name] )
          {
            this.NamedQueuesIndex[ Name ].Remove( Item );
                
          }

        }

      }
  
    }

    /**************************************************************************/
    
  }

}
