/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Text;

namespace SEOMacroscope
{

  /// <summary>
  /// MacroscopeStringLookup is responsible for maintaining a centralized
  /// store of URL to ulong mappings.
  /// </summary>

  [Serializable()]
  public static class MacroscopeStringLookup
  {

    /**************************************************************************/

    private static object Locker;
    private static ulong Counter;
    private static Dictionary<string, ulong> Mappings;

    /**************************************************************************/

    static MacroscopeStringLookup ()
    {
      Locker = new object();
      Counter = 0;
      Mappings = new Dictionary<string, ulong>( 4096 );
    }

    /**************************************************************************/

    public static ulong Lookup ( string Text )
    {

      ulong stored;

      lock( Locker )
      {

        if( Mappings.ContainsKey( key: Text ) )
        {
          stored = Mappings[ Text ];
        }
        else
        {
          lock( Mappings )
          {
            Counter++;
            stored = Counter;
            Mappings.Add( key: Text, value: stored );
          }
        }

      }

      return ( stored );

    }

    /**************************************************************************/

  }

}
