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
using System.Diagnostics;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public partial class MacroscopeUserControl : UserControl
  {

    /**************************************************************************/

    public MacroscopeUserControl ()
    {
    }

    /**************************************************************************/

    public void CopyListViewRowsTextToClipboard ( ListView TargetListView )
    {

      string TextToCopy = "";

      foreach( ListViewItem lvItem in TargetListView.SelectedItems )
      {

        TextToCopy += lvItem.Text;

        for( int i = 1 ; i < lvItem.SubItems.Count ; i++ )
        {
          TextToCopy += "\t" + lvItem.SubItems[ i ].Text;
        }

        TextToCopy += Environment.NewLine;

      }

      try
      {
        this.CopyTextToClipboard( TextToCopy );
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message );
      }

    }

    /**************************************************************************/

    public void CopyListViewValuesTextToClipboard ( ListView TargetListView )
    {

      string TextToCopy = "";

      foreach( ListViewItem lvItem in TargetListView.SelectedItems )
      {

        for( int i = 1 ; i < lvItem.SubItems.Count ; i++ )
        {

          if( i > 1 )
          {
            TextToCopy += "\t";
          }

          TextToCopy += lvItem.SubItems[ i ].Text;

        }

        TextToCopy += Environment.NewLine;

      }

      try
      {
        this.CopyTextToClipboard( TextToCopy );
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message );
      }

    }

    /**************************************************************************/

    public void CopyTextToClipboard ( string Text )
    {
      
      int Count = 10;
      
      while( Count > 0 )
      {
      
        try
        {
          Clipboard.SetText( Text );
          break;
        }
        catch( Exception ex )
        {
          DebugMsg( ex.Message );
        }
        
        Count--;
      
      }
    
    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    public void DebugMsg ( String Msg )
    {
      Debug.WriteLine( Msg );
    }

    /**************************************************************************/

  }

}
