/*

  This file is part of SEOMacroscope.

  Copyright 2020 Jason Holland.

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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDecimalSorter.
  /// </summary>

  public class MacroscopeDecimalSorter : Macroscope, IComparer<decimal>
  {

    /**************************************************************************/

    public enum SortOrder
    {
      ASCENDING = 1,
      DESCENDING = 2
    }

    MacroscopeDecimalSorter.SortOrder Ordering;

    /**************************************************************************/

    public MacroscopeDecimalSorter ( MacroscopeDecimalSorter.SortOrder Ordering )
    {
      this.Ordering = Ordering;
    }

    /**************************************************************************/

    public int Compare ( decimal x, decimal y )
    {
      decimal dX = ( decimal )x;
      decimal dY = ( decimal )y;
      
      int sorted = 0;
      
      if( dX == dY )
      {
        sorted = 0;
      }
      else
      if( dX > dY )
      {
        sorted = 1;
      }
      else
      {
        sorted = -1;
      }

      if( this.Ordering == MacroscopeDecimalSorter.SortOrder.DESCENDING )
      {
        sorted = 0 - sorted;
      }

      return( sorted );

    }

    /**************************************************************************/

  }

}
