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
using System.Collections.Concurrent;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeJobHistory.
  /// </summary>

  public class MacroscopeJobHistory : Macroscope
  {

    /**************************************************************************/

    private ConcurrentDictionary<int, bool> History;

    /**************************************************************************/

    public MacroscopeJobHistory ()
    {
      this.History = new ConcurrentDictionary<int, bool>();
    }

    /**************************************************************************/

    public void AddHistoryItem ( string Url )
    {

      int Key = UrlToDigest( Url: Url );

      lock( this.History )
      {
        if( !this.History.ContainsKey( Key ) )
        {
          this.History.TryAdd( Key, false );
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    public void VisitedHistoryItem ( string Url )
    {

      int Key = UrlToDigest( Url: Url );

      lock( this.History )
      {
        if( this.History.ContainsKey( Key ) )
        {
          this.History[ Key ] = true;
        }
        else
        {
          this.History.TryAdd( Key, true );
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    public void ResetHistoryItem ( string Url )
    {

      int Key = UrlToDigest( Url: Url );

      lock( this.History )
      {
        if( this.History.ContainsKey( Key ) )
        {
          bool result = false;
          this.History.TryRemove( Key, out result );
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    public bool SeenHistoryItem ( string Url )
    {

      int Key = UrlToDigest( Url: Url );
      bool Seen = false;

      lock( this.History )
      {

        if( this.History.ContainsKey( Key ) )
        {
          this.History.TryGetValue( Key, out Seen );
        }

      }

      return ( Seen );

    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<int, bool> GetHistory ()
    {

      Dictionary<int, bool> HistoryCopy = new Dictionary<int, bool>( this.History.Count );

      lock( this.History )
      {
        foreach( int Key in this.History.Keys )
        {
          HistoryCopy.Add( Key, this.History[ Key ] );
        }
      }

      return ( HistoryCopy );

    }

    /** -------------------------------------------------------------------- **/

    public void ClearHistory ()
    {
      lock( this.History )
      {
        this.History.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    public int CountHistory ()
    {

      int Total = 0;

      lock( this.History )
      {
        Total = this.History.Count;
      }

      return ( Total );

    }

    /**************************************************************************/

  }

}
