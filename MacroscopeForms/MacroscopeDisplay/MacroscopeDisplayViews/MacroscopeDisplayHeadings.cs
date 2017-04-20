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
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDisplayHeadings.
  /// </summary>

  public sealed class MacroscopeDisplayHeadings : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayHeadings ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.ConfigureListView();
            }
          )
        );
      }
      else
      {
        this.ConfigureListView();
      }

    }

    /**************************************************************************/

    protected override void ConfigureListView ()
    {
      if( !this.ListViewConfigured )
      {
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    protected override void RenderListView (
      List<ListViewItem> ListViewItems,
      MacroscopeDocument msDoc,
      string Url
    )
    {

      for( ushort HeadingLevel = 1 ; HeadingLevel <= MacroscopePreferencesManager.GetMaxHeadingDepth() ; HeadingLevel++ )
      {

        List<string> HeadingsList = msDoc.GetHeadings( HeadingLevel );

        for( int Count = 0 ; Count < HeadingsList.Count ; Count++ )
        {

          ListViewItem lvItem = null;
          string PairKey = string.Join( "::", Url, HeadingLevel, Count );
          int HeadingColIndex = HeadingLevel + 1;

          string TextLabel = HeadingsList[ Count ];
                        
          if( this.DisplayListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = this.DisplayListView.Items[ PairKey ];
              lvItem.SubItems[ 0 ].Text = Url;
              lvItem.SubItems[ 1 ].Text = ( Count + 1 ).ToString();
              lvItem.SubItems[ HeadingColIndex ].Text = TextLabel;

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeDisplayHeadings 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( PairKey );
              lvItem.UseItemStyleForSubItems = false;

              lvItem.Name = PairKey;

              lvItem.SubItems[ 0 ].Text = Url;
              lvItem.SubItems.Add( ( Count + 1 ).ToString() );

              for( ushort k = 1 ; k <= 6 ; k++ )
              {
                lvItem.SubItems.Add( "" );
              }

              lvItem.SubItems[ HeadingColIndex ].Text = TextLabel;

              ListViewItems.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeDisplayHeadings 2: {0}", ex.Message ) );
            }

          }

          if( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            // URL -----------------------------------------------------------//
          
            if( msDoc.GetIsInternal() )
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
            }

            // Check Missing H1 ----------------------------------------------//

            if( ( HeadingLevel == 1 ) && string.IsNullOrEmpty( TextLabel ) )
            {
              lvItem.SubItems[ HeadingColIndex ].Text = "MISSING";
              lvItem.SubItems[ HeadingColIndex ].ForeColor = Color.Red;
            }
            else
            {
              lvItem.SubItems[ HeadingColIndex ].ForeColor = Color.Green;
            }
          
          }

        }

      }

    }
    
    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
    }

    /**************************************************************************/

  }
  
}
