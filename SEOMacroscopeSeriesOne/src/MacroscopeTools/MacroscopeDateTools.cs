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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDateTools.
  /// </summary>

  public class MacroscopeDateTools : Macroscope
  {

    /**************************************************************************/

    static MacroscopeDateTools ()
    {
      
      SuppressStaticDebugMsg = true;
      
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDateTools ()
    {
      
      this.SuppressDebugMsg = true;
      
    }

    /**************************************************************************/
    
    public static DateTime ParseHttpDate ( string HeaderField, string DateString )
    {

      DateTime ParsedDate = DateTime.UtcNow;

      try
      {
        ParsedDate = DateTime.Parse( DateString );
      }
      catch( FormatException ex )
      {
        DebugMsg( string.Format( "ParseHttpDate: {0}", ex.Message ), true );
        ParsedDate = DateTime.UtcNow;
      }

      return( ParsedDate );

    }
        
    /**************************************************************************/

  }

}
