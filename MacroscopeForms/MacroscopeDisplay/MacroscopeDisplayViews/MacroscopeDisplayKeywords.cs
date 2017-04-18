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
  /// Description of MacroscopeDisplayKeywords.
  /// </summary>

  public sealed class MacroscopeDisplayKeywords : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayKeywords ( MacroscopeMainForm MainForm, ListView lvListView )
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

      Boolean bProcess;

      if( msDoc.GetIsExternal() )
      {
        return;
      }

      if( msDoc.GetIsHtml() )
      {
        bProcess = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        bProcess = true;
      }
      else
      {
        bProcess = false;
      }

      if( bProcess )
      {

        ListViewItem lvItem = null;

        string Text = msDoc.GetKeywords();
        int Occurrences = 0;
        int KeywordsLength = msDoc.GetKeywordsLength();
        int TextNumber = msDoc.GetKeywordsCount();

        string PairKey = string.Join( "", Url, Text );

        if( KeywordsLength > 0 )
        {
          Occurrences = this.MainForm.GetJobMaster().GetDocCollection().GetStatsKeywordsCount( Text );
        }

        if( this.lvListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.lvListView.Items[ PairKey ];
            lvItem.SubItems[ 0 ].Text = Url;
            lvItem.SubItems[ 1 ].Text = Occurrences.ToString();
            lvItem.SubItems[ 2 ].Text = Text;
            lvItem.SubItems[ 3 ].Text = KeywordsLength.ToString();
            lvItem.SubItems[ 4 ].Text = TextNumber.ToString();

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayKeywords 1: {0}", ex.Message ) );
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
            lvItem.SubItems.Add( Occurrences.ToString() );
            lvItem.SubItems.Add( Text );
            lvItem.SubItems.Add( KeywordsLength.ToString() );
            lvItem.SubItems.Add( TextNumber.ToString() );

            ListViewItems.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayKeywords 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          // URL -------------------------------------------------------------//
          
          if( !msDoc.GetIsExternal() )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          }

          // Check Missing Text ----------------------------------------------//

          if( !msDoc.GetIsExternal() )
          {
            if( KeywordsLength <= 0 )
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Red;
              lvItem.SubItems[ 2 ].ForeColor = Color.Red;
              lvItem.SubItems[ 3 ].ForeColor = Color.Red;
              lvItem.SubItems[ 4 ].ForeColor = Color.Red;
            }
            else
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
              lvItem.SubItems[ 2 ].ForeColor = Color.Green;
              lvItem.SubItems[ 3 ].ForeColor = Color.Green;
              lvItem.SubItems[ 4 ].ForeColor = Color.Green;
            }
          }
          else
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 4 ].ForeColor = Color.Gray;
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
