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
      this.SuppressDebugMsg = true;
      this.ColumnToSort = 0;
      this.OrderOfSort = SortOrder.None;
      this.ObjectCompare = new CaseInsensitiveComparer ();
    }

    /**************************************************************************/

    public int Compare ( object x, object y )
    {

      int compareResult;
      int returncompareResult = 0;
      ListViewItem listviewX, listviewY;
      object [] ObjectPair;
      string ColumnName;
      listviewX = ( ListViewItem )x;
      listviewY = ( ListViewItem )y;

      if(
        ( this.ColumnToSort > listviewX.SubItems.Count )
        || ( this.ColumnToSort > listviewY.SubItems.Count ) )
      {
        return( returncompareResult);
      }

      ColumnName = listviewX.ListView.Columns[ this.ColumnToSort ].Text;

      if( Regex.IsMatch( ColumnName, @"\s+Date" ) )
      {
        ObjectPair = DetermineValueTypeDate(
          listviewX.SubItems[ this.ColumnToSort ].Text,
          listviewY.SubItems[ this.ColumnToSort ].Text
        );
      }
      else
      {
        ObjectPair = DetermineValueType(
          listviewX.SubItems[ this.ColumnToSort ].Text,
          listviewY.SubItems[ this.ColumnToSort ].Text
        );
      }
      
      compareResult = ObjectCompare.Compare( ObjectPair[ 0 ], ObjectPair[ 1 ] );

      if( this.OrderOfSort == SortOrder.Ascending )
      {
        returncompareResult = compareResult;
      }
      else
      if( this.OrderOfSort == SortOrder.Descending )
      {
        returncompareResult = -compareResult;
      }
      else
      {
        returncompareResult = 0;
      }

      return ( returncompareResult );

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
        Regex.IsMatch( TextX, @"^[0-9]+$" )
        && Regex.IsMatch( TextY, @"^[0-9]+$" ) )
      {
        decimal DecimalX = decimal.Parse( TextX );
        decimal DecimalY = decimal.Parse( TextY );
        ObjectPair[ 0 ] = DecimalX;
        ObjectPair[ 1 ] = DecimalY;
      }
      else
      if(
        Regex.IsMatch( TextX, @"^[0-9]+\.[0-9]+$" )
        && Regex.IsMatch( TextY, @"^[0-9]+\.[0-9]+$" ) )
      {
        decimal DecimalX = decimal.Parse( TextX );
        decimal DecimalY = decimal.Parse( TextY );
        ObjectPair[ 0 ] = DecimalX;
        ObjectPair[ 1 ] = DecimalY;
      }

      return( ObjectPair );
      
    }

    /** -------------------------------------------------------------------- **/

    private object[] DetermineValueTypeDate ( string TextX, string TextY )
    {

      object [] ObjectPair = new object[2];

      ObjectPair[ 0 ] = TextX;
      ObjectPair[ 1 ] = TextY;

      try
      {

        DateTime DateX = DateTime.Parse( TextX );
        DateTime DateY = DateTime.Parse( TextY );

        ObjectPair[ 0 ] = DateX;
        ObjectPair[ 1 ] = DateY;

      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( ex.Message ) );
      }

      return( ObjectPair );
      
    }

    /**************************************************************************/

  }

}
