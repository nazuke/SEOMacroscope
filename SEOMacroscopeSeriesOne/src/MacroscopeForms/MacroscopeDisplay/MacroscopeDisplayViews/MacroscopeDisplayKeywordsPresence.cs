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

    private const int COL_URL = 0;
    private const int COL_KEYWORD = 1;
    private const int COL_PRESENCE = 2;

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

      List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence;
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

        KeywordPresence = DocCollection.GetKeywordPresenceAnalysis( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {

          MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS Present = Pair.Value;
          string Keyword = Pair.Key;
          string PairKey = string.Join( "", UrlToDigest( Url: Url ).ToString(), Keyword );

          if( this.DisplayListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = this.DisplayListView.Items[ PairKey ];
              lvItem.SubItems[ COL_URL ].Text = Url;
              lvItem.SubItems[ COL_KEYWORD ].Text = Keyword;
              lvItem.SubItems[ COL_PRESENCE ].Text = Present.ToString();

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

              lvItem.SubItems[ COL_URL ].Text = Url;
              lvItem.SubItems.Add( Keyword );
              lvItem.SubItems.Add( Present.ToString() );

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
              lvItem.SubItems[ COL_URL ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ COL_URL ].ForeColor = Color.Gray;
            }

            // Check Missing Text ----------------------------------------------//

            if( msDoc.GetIsInternal() )
            {

              switch( Present )
              {
                case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.KEYWORDS_METATAG_EMPTY:
                  lvItem.SubItems[ COL_URL ].ForeColor = Color.Red;
                  lvItem.SubItems[ COL_KEYWORD ].ForeColor = Color.Red;
                  lvItem.SubItems[ COL_PRESENCE ].ForeColor = Color.Red;
                  break;
                case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_TITLE:
                  lvItem.SubItems[ COL_URL ].ForeColor = Color.Green;
                  lvItem.SubItems[ COL_KEYWORD ].ForeColor = Color.Green;
                  lvItem.SubItems[ COL_PRESENCE ].ForeColor = Color.Green;
                  break;
                case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_TITLE:
                  lvItem.SubItems[ COL_URL ].ForeColor = Color.Red;
                  lvItem.SubItems[ COL_KEYWORD ].ForeColor = Color.Red;
                  lvItem.SubItems[ COL_PRESENCE ].ForeColor = Color.Red;
                  break;
                case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_DESCRIPTION:
                  lvItem.SubItems[ COL_URL ].ForeColor = Color.Green;
                  lvItem.SubItems[ COL_KEYWORD ].ForeColor = Color.Green;
                  lvItem.SubItems[ COL_PRESENCE ].ForeColor = Color.Green;
                  break;
                case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_DESCRIPTION:
                  lvItem.SubItems[ COL_URL ].ForeColor = Color.Orange;
                  lvItem.SubItems[ COL_KEYWORD ].ForeColor = Color.Orange;
                  lvItem.SubItems[ COL_PRESENCE ].ForeColor = Color.Orange;
                  break;
                case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_BODY:
                  lvItem.SubItems[ COL_URL ].ForeColor = Color.Green;
                  lvItem.SubItems[ COL_KEYWORD ].ForeColor = Color.Green;
                  lvItem.SubItems[ COL_PRESENCE ].ForeColor = Color.Green;
                  break;
                case MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_BODY:
                  lvItem.SubItems[ COL_URL ].ForeColor = Color.Red;
                  lvItem.SubItems[ COL_KEYWORD ].ForeColor = Color.Red;
                  lvItem.SubItems[ COL_PRESENCE ].ForeColor = Color.Red;
                  break;
                default:
                  break;
              }

            }
            else
            {
              lvItem.SubItems[ COL_KEYWORD ].ForeColor = Color.Gray;
              lvItem.SubItems[ COL_PRESENCE ].ForeColor = Color.Gray;
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
