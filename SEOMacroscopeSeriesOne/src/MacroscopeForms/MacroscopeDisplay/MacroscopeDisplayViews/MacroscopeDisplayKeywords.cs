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

    public MacroscopeDisplayKeywords ( MacroscopeMainForm MainForm, ListView TargetListView )
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
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc,
      string Url
    )
    {

      bool Proceed = false;

      if( msDoc.GetIsExternal() )
      {
        return;
      }

      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Proceed = true;
          break;
        default:
          break;
      }

      if ( Proceed )
      {

        ListViewItem lvItem = null;

        string Text = msDoc.GetKeywords();
        int Occurrences = 0;
        int KeywordsLength = msDoc.GetKeywordsLength();
        int TextNumber = msDoc.GetKeywordsCount();

        string PairKey = string.Join( "", Url, Text );

        if( KeywordsLength > 0 )
        {
          Occurrences = DocCollection.GetStatsKeywordsCount( msDoc );
        }

        if( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];
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
          
          if( msDoc.GetIsInternal() )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          }

          // Check Missing Text ----------------------------------------------//

          if( msDoc.GetIsInternal() )
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
