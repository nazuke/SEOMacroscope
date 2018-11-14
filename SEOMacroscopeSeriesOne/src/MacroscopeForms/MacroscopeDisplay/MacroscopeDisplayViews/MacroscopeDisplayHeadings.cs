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
  /// Description of MacroscopeDisplayHeadings.
  /// </summary>

  public sealed class MacroscopeDisplayHeadings : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColUrl = 0;
    private const int ColOccurences = 1;
    private const int ColOrder = 2;
    private const int ColH1 = 3;
    private const int ColH2 = 4;
    private const int ColH3 = 5;
    private const int ColH4 = 6;
    private const int ColH5 = 7;
    private const int ColH6 = 8;

    private const int ColH1Offset = 2;
        
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

      if( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
      {
        Proceed = true;
      }
      
      if( Proceed )
      {
      
        for( ushort HeadingLevel = 1 ; HeadingLevel <= MacroscopePreferencesManager.GetMaxHeadingDepth() ; HeadingLevel++ )
        {

          List<string> HeadingsList = msDoc.GetHeadings( HeadingLevel );

          for( int Order = 0 ; Order < HeadingsList.Count ; Order++ )
          {

            ListViewItem lvItem = null;
            string PairKey = string.Join( ":", UrlToDigest( Url ), UrlToDigest( HeadingLevel.ToString() ), UrlToDigest( Order.ToString() ) ).ToString();
            int HeadingColIndex = HeadingLevel + ColH1Offset;
            string TextLabel = HeadingsList[ Order ];
            int Occurences = DocCollection.GetStatsHeadingsCount( HeadingLevel: HeadingLevel, Text: TextLabel );

            if( this.DisplayListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = this.DisplayListView.Items[ PairKey ];
                lvItem.SubItems[ ColUrl ].Text = Url;
                lvItem.SubItems[ ColOccurences ].Text = Occurences.ToString();
                lvItem.SubItems[ ColOrder ].Text = ( Order + 1 ).ToString();
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

                lvItem.SubItems[ ColUrl ].Text = Url;
                lvItem.SubItems.Add( Occurences.ToString() );
                lvItem.SubItems.Add( ( Order + 1 ).ToString() );

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
                lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
              }

              // Occurences ----------------------------------------------------//
          
              if( ( Occurences > 1 ) && ( msDoc.GetIsInternal() ) )
              {
                lvItem.SubItems[ ColOccurences ].ForeColor = Color.Orange;
              }
              else
              if( msDoc.GetIsInternal() )
              {
                lvItem.SubItems[ ColOccurences ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ ColOccurences ].ForeColor = Color.Gray;
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

    }
    
    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
    }

    /**************************************************************************/

  }
  
}
