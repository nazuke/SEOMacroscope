/*

	This file is part of SEOMacroscope.

	Copyright 2018 Jason Holland.

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

  public sealed class MacroscopeDisplayKeywordsPresence : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayKeywordsPresence ( MacroscopeMainForm MainForm, ListView TargetListView )
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

      List<KeyValuePair<string, MacroscopeIntenseKeywordAnalysis.KEYWORD_STATUS>> KeywordPresence;
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
        default:
          break;
      }

      if( Proceed )
      {

        ListViewItem lvItem = null;

        KeywordPresence = DocCollection.GetIntenseKeywordAnalysis( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeIntenseKeywordAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {

          MacroscopeIntenseKeywordAnalysis.KEYWORD_STATUS Present = Pair.Value;
          string Keyword = Pair.Key;
          string PairKey = string.Join( "", UrlToDigest( Url: Url ).ToString(), Keyword );

          if( this.DisplayListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = this.DisplayListView.Items[ PairKey ];
              lvItem.SubItems[ 0 ].Text = Url;
              lvItem.SubItems[ 1 ].Text = Present.ToString();
              lvItem.SubItems[ 2 ].Text = Keyword;

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeDisplayKeywordsPresence 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem( PairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = PairKey;

              lvItem.SubItems[ 0 ].Text = Url;
              lvItem.SubItems.Add( Present.ToString() );
              lvItem.SubItems.Add( Keyword );

              ListViewItems.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeDisplayKeywordsPresence 2: {0}", ex.Message ) );
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

              switch( Present )
              {
                case MacroscopeIntenseKeywordAnalysis.KEYWORD_STATUS.KEYWORDS_METATAG_EMPTY:
                  lvItem.SubItems[ 0 ].ForeColor = Color.Red;
                  lvItem.SubItems[ 1 ].ForeColor = Color.Red;
                  lvItem.SubItems[ 2 ].ForeColor = Color.Red;
                  break;
                case MacroscopeIntenseKeywordAnalysis.KEYWORD_STATUS.MISSING_IN_BODY_TEXT:
                  lvItem.SubItems[ 0 ].ForeColor = Color.Red;
                  lvItem.SubItems[ 1 ].ForeColor = Color.Red;
                  lvItem.SubItems[ 2 ].ForeColor = Color.Red;
                  break;
                case MacroscopeIntenseKeywordAnalysis.KEYWORD_STATUS.PRESENT_IN_BODY_TEXT:
                  lvItem.SubItems[ 0 ].ForeColor = Color.Green;
                  lvItem.SubItems[ 1 ].ForeColor = Color.Green;
                  lvItem.SubItems[ 2 ].ForeColor = Color.Green;
                  break;
                default:
                  break;
              }

            }
            else
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
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
