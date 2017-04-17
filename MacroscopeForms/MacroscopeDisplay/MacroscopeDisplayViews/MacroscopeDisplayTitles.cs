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
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayTitles : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayTitles ( MacroscopeMainForm MainForm, ListView lvListView )
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

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
    {

      Boolean bProcess;
      MacroscopeDocumentCollection DocCollection = this.MainForm.GetJobMaster().GetDocCollection();
      
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

        string Text = msDoc.GetTitle();
        string TextLabel = Text;
        int TextCount = 0;
        int TextLength = Text.Length;
        int TextPixelWidth = msDoc.GetTitlePixelWidth();

        string PairKey = string.Join( "", Url, Text );

        ListViewItem lvItem = null;

        if( TextLength > 0 )
        {
          TextCount = DocCollection.GetStatsTitleCount( msDoc: msDoc );
        }
        else
        {
          TextLabel = "MISSING";
        }

        if( this.lvListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.lvListView.Items[ PairKey ];
            lvItem.SubItems[ 0 ].Text = Url;
            lvItem.SubItems[ 1 ].Text = TextCount.ToString();
            lvItem.SubItems[ 2 ].Text = TextLabel;
            lvItem.SubItems[ 3 ].Text = TextLength.ToString();
            lvItem.SubItems[ 4 ].Text = TextPixelWidth.ToString();

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayTitles 1: {0}", ex.Message ) );
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
            lvItem.SubItems.Add( TextCount.ToString() );
            lvItem.SubItems.Add( TextLabel );
            lvItem.SubItems.Add( TextLength.ToString() );
            lvItem.SubItems.Add( TextPixelWidth.ToString() );

            this.lvListView.Items.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayTitles 2: {0}", ex.Message ) );
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
          
          // Check Missing Title ---------------------------------------------//

          if( TextLength <= 0 )
          {
            lvItem.SubItems[ 2 ].Text = "MISSING";
            lvItem.SubItems[ 2 ].ForeColor = Color.Red;
          }
          else
          if( TextLength < MacroscopePreferencesManager.GetTitleMinLen() )
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Red;
          }
          else
          if( TextLength > MacroscopePreferencesManager.GetTitleMaxLen() )
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Green;
          }

          // Check Title Length ----------------------------------------------//

          if( TextLength < MacroscopePreferencesManager.GetTitleMinLen() )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Red;
          }
          else
          if( TextLength > MacroscopePreferencesManager.GetTitleMaxLen() )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Green;
          }

          // Check Pixel Width -----------------------------------------------//

          if( TextPixelWidth > MacroscopePreferencesManager.GetTitleMaxPixelWidth() )
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Red;
          }
          else
          if( TextPixelWidth >= ( MacroscopePreferencesManager.GetTitleMaxPixelWidth() - 20 ) )
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Goldenrod;
          }
          else
          if( TextPixelWidth <= 0 )
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Orange;
          }
          else
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Green;
          }

        }

      }

    }

    /**************************************************************************/

  }

}
