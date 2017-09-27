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
  /// Description of MacroscopeJobHistory.
  /// </summary>

  public class MacroscopeJobHistory
  {

    /**************************************************************************/
        
    private Dictionary<string,Boolean> History;
    
    /**************************************************************************/

    public MacroscopeJobHistory ()
    {

      this.History = new Dictionary<string, bool> ( 4096 );
       
    }

    /**************************************************************************/

    public void AddHistoryItem ( string Url )
    {
      if( !this.History.ContainsKey( Url ) )
      {
        lock( this.History )
        {
          this.History.Add( Url, false );
        }
      }
    }
    
    /** -------------------------------------------------------------------- **/

    public void VisitedHistoryItem ( string Url )
    {
      if( this.History.ContainsKey( Url ) )
      {
        lock( this.History )
        {
          this.History[ Url ] = true;
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public void ResetHistoryItem ( string Url )
    {
      if( this.History.ContainsKey( Url ) )
      {
        lock( this.History )
        {
          this.History.Remove( Url );
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public Boolean SeenHistoryItem ( string Url )
    {
      Boolean Seen = false;
      if( this.History.ContainsKey( Url ) )
      {
        Seen = this.History[ Url ];
      }
      return( Seen );
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string,Boolean> GetHistory ()
    {
      Dictionary<string,Boolean> HistoryCopy = new Dictionary<string,Boolean> ( this.History.Count );
      lock( this.History )
      {
        foreach( string sKey in this.History.Keys )
        {
          HistoryCopy.Add( sKey, this.History[ sKey ] );
        }
      }
      return( HistoryCopy );
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
      return( this.History.Count );
    }

    /**************************************************************************/

  }

}
