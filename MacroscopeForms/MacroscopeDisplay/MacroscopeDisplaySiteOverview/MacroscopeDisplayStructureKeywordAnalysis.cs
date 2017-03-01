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

  public sealed class MacroscopeDisplayStructureKeywordAnalysis : Macroscope
  {

    /**************************************************************************/

    private MacroscopeMainForm MainForm;
          
    private List<ListView> lvListViews = new List<ListView> ( 4 );

    /**************************************************************************/

    public MacroscopeDisplayStructureKeywordAnalysis (
      MacroscopeMainForm MainForm,
      ListView lvListView1,
      ListView lvListView2,
      ListView lvListView3,
      ListView lvListView4
    )
    {

      this.SuppressDebugMsg = false;
      
      this.MainForm = MainForm;
      
      this.lvListViews.Add( lvListView1 );
      this.lvListViews.Add( lvListView2 );
      this.lvListViews.Add( lvListView3 );
      this.lvListViews.Add( lvListView4 );

    }

    /**************************************************************************/

    public void ClearData ()
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              for( int i = 0 ; i <= 3 ; i++ )
              {
                this.lvListViews[ i ].Items.Clear();
              }
            }
          )
        );
      }
      else
      {
        for( int i = 0 ; i <= 3 ; i++ )
        {
          this.lvListViews[ i ].Items.Clear();
        }
      }
    }

    /**************************************************************************/

    public void RefreshKeywordAnalysisData ( MacroscopeDocumentCollection DocCollection )
    {

      for( int i = 0 ; i <= 3 ; i++ )
      {

        Dictionary<string,int> DicTerms = DocCollection.GetDeepKeywordAnalysisAsDictonary( Words: i + 1 );

        if( this.MainForm.InvokeRequired )
        {
          this.MainForm.Invoke(
            new MethodInvoker (
              delegate
              {
                Cursor.Current = Cursors.WaitCursor;
                this.RenderKeywordAnalysisListView( this.lvListViews[ i ], DicTerms );
                Cursor.Current = Cursors.Default;
              }
            )
          );
        }
        else
        {
          Cursor.Current = Cursors.WaitCursor;
          this.RenderKeywordAnalysisListView( this.lvListViews[ i ], DicTerms );
          Cursor.Current = Cursors.Default;
        }
      
      }

    }

    /**************************************************************************/
    
    private void RenderKeywordAnalysisListView (
      ListView lvListView,
      Dictionary<string,int> DicTerms
    )
    {

      lvListView.BeginUpdate();
            
      foreach( string sTerm in DicTerms.Keys )
      {

        string sKeyPair = sTerm;
        ListViewItem lvItem = null;

        if( lvListView.Items.ContainsKey( sKeyPair ) )
        {

          try
          {

            lvItem = lvListView.Items[ sKeyPair ];
            lvItem.SubItems[ 0 ].Text = DicTerms[ sTerm ].ToString();
            lvItem.SubItems[ 1 ].Text = sTerm;

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayStructureKeywordAnalysis 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem ( sKeyPair );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = sKeyPair;

            lvItem.SubItems[ 0 ].Text = DicTerms[ sTerm ].ToString();
            lvItem.SubItems.Add( sTerm );

            lvListView.Items.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayStructureKeywordAnalysis 2: {0}", ex.Message ) );
          }

        }
        
        if( lvItem != null )
        {
          lvItem.ForeColor = Color.Blue;
        }

      }

      lvListView.EndUpdate();

    }

    /**************************************************************************/

  }

}
