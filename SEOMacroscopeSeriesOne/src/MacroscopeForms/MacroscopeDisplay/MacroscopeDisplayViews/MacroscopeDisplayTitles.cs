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

  public sealed class MacroscopeDisplayTitles : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColUrl = 0;
    private const int ColPageLanguage = 1;
    private const int ColDetectedLanguage = 2;
    private const int ColOccurences = 3;
    private const int ColTitleText = 4;
    private const int ColLength = 5;
    private const int ColPixelWidth = 6;
    
    /**************************************************************************/

    public MacroscopeDisplayTitles ( MacroscopeMainForm MainForm, ListView TargetListView )
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

      Boolean Proceed = false;

      if( msDoc.GetIsExternal() )
      {
        return;
      }

      if( msDoc.GetIsRedirect() )
      {
        return;
      }
      
      if( msDoc.GetIsHtml() )
      {
        Proceed = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        Proceed = true;
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

        string PairKey = string.Join( "", Url, Text );

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
            
            lvItem.SubItems[ ColUrl ].Text = Url;
            lvItem.SubItems[ ColPageLanguage ].Text = PageLanguage;
            lvItem.SubItems[ ColDetectedLanguage ].Text = DetectedLanguage;
            lvItem.SubItems[ ColOccurences ].Text = TextOccurences.ToString();
            lvItem.SubItems[ ColTitleText ].Text = TextLabel;
            lvItem.SubItems[ ColLength ].Text = TextLength.ToString();
            lvItem.SubItems[ ColPixelWidth ].Text = TextPixelWidth.ToString();
            
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

            lvItem.SubItems[ ColUrl ].Text = Url;
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
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
          }

          // Title Language --------------------------------------------------//

          if( msDoc.GetIsInternal() )
          {

            lvItem.SubItems[ ColPageLanguage ].ForeColor = Color.Green;
            lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Green;
            
            if( DetectedLanguage != PageLanguage )
            {
              lvItem.SubItems[ ColPageLanguage ].ForeColor = Color.Red;
              lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Red;
            }

          }
          else
          {
            lvItem.SubItems[ ColPageLanguage ].ForeColor = Color.Gray;
            lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Gray;
          }

          // Check Missing Title ---------------------------------------------//

          if( TextLength <= 0 )
          {
            lvItem.SubItems[ ColTitleText ].Text = "MISSING";
            lvItem.SubItems[ ColTitleText ].ForeColor = Color.Red;
          }
          else
          if( TextLength < MacroscopePreferencesManager.GetTitleMinLen() )
          {
            lvItem.SubItems[ ColTitleText ].ForeColor = Color.Red;
          }
          else
          if( TextLength > MacroscopePreferencesManager.GetTitleMaxLen() )
          {
            lvItem.SubItems[ ColTitleText ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ ColTitleText ].ForeColor = Color.Green;
          }

          // Check Title Length ----------------------------------------------//

          if( TextLength < MacroscopePreferencesManager.GetTitleMinLen() )
          {
            lvItem.SubItems[ ColLength ].ForeColor = Color.Red;
          }
          else
          if( TextLength > MacroscopePreferencesManager.GetTitleMaxLen() )
          {
            lvItem.SubItems[ ColLength ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ ColLength ].ForeColor = Color.Green;
          }

          // Check Pixel Width -----------------------------------------------//

          if( TextPixelWidth > MacroscopePreferencesManager.GetTitleMaxPixelWidth() )
          {
            lvItem.SubItems[ ColPixelWidth ].ForeColor = Color.Red;
          }
          else
          if( TextPixelWidth >= ( MacroscopePreferencesManager.GetTitleMaxPixelWidth() - 20 ) )
          {
            lvItem.SubItems[ ColPixelWidth ].ForeColor = Color.Goldenrod;
          }
          else
          if( TextPixelWidth <= 0 )
          {
            lvItem.SubItems[ ColPixelWidth ].ForeColor = Color.Orange;
          }
          else
          {
            lvItem.SubItems[ ColPixelWidth ].ForeColor = Color.Green;
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
