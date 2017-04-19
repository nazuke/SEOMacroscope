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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayUriAnalysis : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayUriAnalysis ( MacroscopeMainForm MainForm, ListView lvListView )
      : base( MainForm, lvListView )
    {

      this.MainForm = MainForm;
      this.lvListView = lvListView;

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

      string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
      string Status = msDoc.GetStatusCode().ToString();
      string Checksum = msDoc.GetChecksum();
      int Count = this.MainForm.GetJobMaster().GetDocCollection().GetStatsChecksumCount( Checksum: Checksum );
      string PairKey = string.Join( "", Url );
      ListViewItem lvItem = null;

      if( this.lvListView.Items.ContainsKey( PairKey ) )
      {

        try
        {

          lvItem = this.lvListView.Items[ PairKey ];
          lvItem.SubItems[ 0 ].Text = Url;
          lvItem.SubItems[ 1 ].Text = StatusCode;   
          lvItem.SubItems[ 2 ].Text = Status;
          lvItem.SubItems[ 3 ].Text = Count.ToString();
          lvItem.SubItems[ 4 ].Text = Checksum;

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplayUriAnalysis 1: {0}", ex.Message ) );
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
          lvItem.SubItems.Add( StatusCode );
          lvItem.SubItems.Add( Status );
          lvItem.SubItems.Add( Count.ToString() );
          lvItem.SubItems.Add( Checksum );

          ListViewItems.Add( lvItem );

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplayUriAnalysis 2: {0}", ex.Message ) );
        }

      }

      if( lvItem != null )
      {

        lvItem.ForeColor = Color.Blue;

        if( msDoc.GetIsInternal() )
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Green;
        }
        else
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
        }

        if( Regex.IsMatch( StatusCode, "^[2]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Green;
          lvItem.SubItems[ 2 ].ForeColor = Color.Green;
        }
        else
        if( Regex.IsMatch( StatusCode, "^[3]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Goldenrod;
          lvItem.SubItems[ 2 ].ForeColor = Color.Goldenrod;
        }
        else
        if( Regex.IsMatch( StatusCode, "^[45]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Red;
          lvItem.SubItems[ 2 ].ForeColor = Color.Red;
        }
        else
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
          lvItem.SubItems[ 2 ].ForeColor = Color.Blue;
        }

        if( Count > 1 )
        {
          lvItem.SubItems[ 2 ].ForeColor = Color.Red;
          lvItem.SubItems[ 3 ].ForeColor = Color.Red;
          lvItem.SubItems[ 4 ].ForeColor = Color.Red;
        }
        else
        {
          lvItem.SubItems[ 2 ].ForeColor = Color.Blue;
          lvItem.SubItems[ 3 ].ForeColor = Color.Blue;
          lvItem.SubItems[ 4 ].ForeColor = Color.Blue;
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
