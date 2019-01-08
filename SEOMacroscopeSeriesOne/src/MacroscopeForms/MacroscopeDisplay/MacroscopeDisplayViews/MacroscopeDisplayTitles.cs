/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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

  public sealed class MacroscopeDisplayTitles : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int COL_URL = 0;
    private const int COL_PAGE_LANGUAGE = 1;
    private const int COL_DETECTED_LANGUAGE = 2;
    private const int COL_OCCURENCES = 3;
    private const int COL_TITLE_TEXT = 4;
    private const int COL_LENGTH = 5;
    private const int COL_PIXEL_WIDTH = 6;

    /**************************************************************************/

    public MacroscopeDisplayTitles ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker(
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

      switch( msDoc.GetDocumentType() )
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

      if( Proceed )
      {

        string PageLanguage = msDoc.GetIsoLanguageCode();
        string DetectedLanguage = msDoc.GetTitleLanguage();
        string Text = msDoc.GetTitle();
        string TextLabel = Text;
        int TextOccurences = 0;
        int TextLength = Text.Length;
        int TextPixelWidth = msDoc.GetTitlePixelWidth();

        string PairKey = string.Join( ":", UrlToDigest( Url ), UrlToDigest( Text ) );

        ListViewItem lvItem = null;

        if( string.IsNullOrEmpty( PageLanguage ) )
        {
          PageLanguage = "";
        }

        if( string.IsNullOrEmpty( DetectedLanguage ) )
        {
          DetectedLanguage = "";
        }

        if( TextLength > 0 )
        {
          TextOccurences = DocCollection.GetStatsTitleCount( msDoc: msDoc );
        }
        else
        {
          TextLabel = "MISSING";
        }

        if( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];

            lvItem.SubItems[ COL_URL ].Text = Url;
            lvItem.SubItems[ COL_PAGE_LANGUAGE ].Text = PageLanguage;
            lvItem.SubItems[ COL_DETECTED_LANGUAGE ].Text = DetectedLanguage;
            lvItem.SubItems[ COL_OCCURENCES ].Text = TextOccurences.ToString();
            lvItem.SubItems[ COL_TITLE_TEXT ].Text = TextLabel;
            lvItem.SubItems[ COL_LENGTH ].Text = TextLength.ToString();
            lvItem.SubItems[ COL_PIXEL_WIDTH ].Text = TextPixelWidth.ToString();

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

            lvItem = new ListViewItem( PairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = PairKey;

            lvItem.SubItems[ COL_URL ].Text = Url;
            lvItem.SubItems.Add( PageLanguage );
            lvItem.SubItems.Add( DetectedLanguage );
            lvItem.SubItems.Add( TextOccurences.ToString() );
            lvItem.SubItems.Add( TextLabel );
            lvItem.SubItems.Add( TextLength.ToString() );
            lvItem.SubItems.Add( TextPixelWidth.ToString() );

            ListViewItems.Add( lvItem );

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

          if( msDoc.GetIsInternal() )
          {
            lvItem.SubItems[ COL_URL ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ COL_URL ].ForeColor = Color.Gray;
          }

          // Title Language --------------------------------------------------//

          if( msDoc.GetIsInternal() )
          {

            lvItem.SubItems[ COL_PAGE_LANGUAGE ].ForeColor = Color.Green;
            lvItem.SubItems[ COL_DETECTED_LANGUAGE ].ForeColor = Color.Green;

            if( DetectedLanguage != PageLanguage )
            {
              lvItem.SubItems[ COL_PAGE_LANGUAGE ].ForeColor = Color.Red;
              lvItem.SubItems[ COL_DETECTED_LANGUAGE ].ForeColor = Color.Red;
            }

          }
          else
          {
            lvItem.SubItems[ COL_PAGE_LANGUAGE ].ForeColor = Color.Gray;
            lvItem.SubItems[ COL_DETECTED_LANGUAGE ].ForeColor = Color.Gray;
          }

          // Check Missing Title ---------------------------------------------//

          if( TextLength <= 0 )
          {
            lvItem.SubItems[ COL_TITLE_TEXT ].Text = "MISSING";
            lvItem.SubItems[ COL_TITLE_TEXT ].ForeColor = Color.Red;
          }
          else
          if( TextLength < MacroscopePreferencesManager.GetTitleMinLen() )
          {
            lvItem.SubItems[ COL_TITLE_TEXT ].ForeColor = Color.Red;
          }
          else
          if( TextLength > MacroscopePreferencesManager.GetTitleMaxLen() )
          {
            lvItem.SubItems[ COL_TITLE_TEXT ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ COL_TITLE_TEXT ].ForeColor = Color.Green;
          }

          // Check Title Length ----------------------------------------------//

          if( TextLength < MacroscopePreferencesManager.GetTitleMinLen() )
          {
            lvItem.SubItems[ COL_LENGTH ].ForeColor = Color.Red;
          }
          else
          if( TextLength > MacroscopePreferencesManager.GetTitleMaxLen() )
          {
            lvItem.SubItems[ COL_LENGTH ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ COL_LENGTH ].ForeColor = Color.Green;
          }

          // Check Pixel Width -----------------------------------------------//

          if( TextPixelWidth > MacroscopePreferencesManager.GetTitleMaxPixelWidth() )
          {
            lvItem.SubItems[ COL_PIXEL_WIDTH ].ForeColor = Color.Red;
          }
          else
          if( TextPixelWidth >= ( MacroscopePreferencesManager.GetTitleMaxPixelWidth() - 20 ) )
          {
            lvItem.SubItems[ COL_PIXEL_WIDTH ].ForeColor = Color.Goldenrod;
          }
          else
          if( TextPixelWidth <= 0 )
          {
            lvItem.SubItems[ COL_PIXEL_WIDTH ].ForeColor = Color.Orange;
          }
          else
          {
            lvItem.SubItems[ COL_PIXEL_WIDTH ].ForeColor = Color.Green;
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
