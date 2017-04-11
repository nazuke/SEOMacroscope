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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeColumnSorter.
  /// </summary>

  public class MacroscopeColumnSorter : Macroscope, IComparer
  {

    /**************************************************************************/

    private int ColumnToSort;
    private SortOrder OrderOfSort;
    private CaseInsensitiveComparer ObjectCompare;

    /**************************************************************************/

    public MacroscopeColumnSorter ()
    {
      this.ColumnToSort = 0;
      this.OrderOfSort = SortOrder.None;
      this.ObjectCompare = new CaseInsensitiveComparer ();
    }

    /**************************************************************************/

    public int Compare ( object x, object y )
    {

      int compareResult;
      ListViewItem listviewX, listviewY;

      listviewX = ( ListViewItem )x;
      listviewY = ( ListViewItem )y;

      if(
        ( this.ColumnToSort > listviewX.SubItems.Count )
        || ( this.ColumnToSort > listviewY.SubItems.Count ) )
      {
        return 0;
      }

      object [] ObjectPair = DetermineValueType(
                               listviewX.SubItems[ this.ColumnToSort ].Text,
                               listviewY.SubItems[ this.ColumnToSort ].Text
                             );

      compareResult = ObjectCompare.Compare( ObjectPair[ 0 ], ObjectPair[ 1 ] );

      if( this.OrderOfSort == SortOrder.Ascending )
      {
        return compareResult;
      }
      else
      if( this.OrderOfSort == SortOrder.Descending )
      {
        return ( -compareResult );
      }
      else
      {
        return 0;
      }

    }

    /**************************************************************************/

    public int SortColumn
    {
      set
      {
        this.ColumnToSort = value;
      }
      get
      {
        return this.ColumnToSort;
      }
    }

    /**************************************************************************/

    public SortOrder Order
    {
      set
      {
        this.OrderOfSort = value;
      }
      get
      {
        return this.OrderOfSort;
      }
    }

    /**************************************************************************/

    private object[] DetermineValueType ( string TextX, string TextY )
    {
      
      object [] ObjectPair = new object[2];

      ObjectPair[ 0 ] = TextX;
      ObjectPair[ 1 ] = TextY;

      if(
        Regex.IsMatch( TextX, "^[0-9]+$" )
        && Regex.IsMatch( TextY, "^[0-9]+$" ) )
      {
        decimal DecimalX = decimal.Parse( TextX );
        decimal DecimalY = decimal.Parse( TextY );
        ObjectPair[ 0 ] = DecimalX;
        ObjectPair[ 1 ] = DecimalY;
      }

      if(
        Regex.IsMatch( TextX, "^[0-9]+\\.[0-9]+$" )
        && Regex.IsMatch( TextY, "^[0-9]+\\.[0-9]+$" ) )
      {
        decimal DecimalX = decimal.Parse( TextX );
        decimal DecimalY = decimal.Parse( TextY );
        ObjectPair[ 0 ] = DecimalX;
        ObjectPair[ 1 ] = DecimalY;
      }

      // TODO: Add dates, etc.

      return( ObjectPair );
      
    }

    /**************************************************************************/

  }

}
