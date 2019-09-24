/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

  The GitHub repository may be found at:

    https://github.com/nazuke/SEOMacroscope

  SEOMacroscope is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  SEOMacroscope is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

 */

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeJobHistory.
  /// </summary>

  [Serializable()]
  public class MacroscopeJobHistory : Macroscope
  {

    /**************************************************************************/

    private ConcurrentDictionary<ulong, bool> History;

    /**************************************************************************/

    public MacroscopeJobHistory ()
    {
      this.History = new ConcurrentDictionary<ulong, bool>();
    }

    /**************************************************************************/

    public void AddHistoryItem ( string Url )
    {

      ulong Key = UrlToDigest( Url: Url );

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

      ulong Key = UrlToDigest( Url: Url );

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

      ulong Key = UrlToDigest( Url: Url );

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

      ulong Key = UrlToDigest( Url: Url );
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

    public Dictionary<ulong, bool> GetHistory ()
    {

      Dictionary<ulong, bool> HistoryCopy = new Dictionary<ulong, bool>( this.History.Count );

      lock( this.History )
      {
        foreach( ulong Key in this.History.Keys )
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
