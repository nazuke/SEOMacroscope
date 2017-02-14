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

  public sealed class MacroscopeDisplayStructureKeywordAnalysis : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayStructureKeywordAnalysis ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
      : base( MainFormNew, lvListViewNew )
    {

      this.MainForm = MainFormNew;
      this.lvListView = lvListViewNew;

      if( MainForm.InvokeRequired )
      {
        MainForm.Invoke(
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

    public void RefreshKeywordAnalysisData ( MacroscopeDocumentCollection DocCollection )
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.RenderKeywordAnalysisListView( DocCollection );
            }
          )
        );
      }
      else
      {
        this.RenderKeywordAnalysisListView( DocCollection );
      }
    }

    /**************************************************************************/
    
    void RenderKeywordAnalysisListView ( MacroscopeDocumentCollection DocCollection )
    {

      Dictionary<string,int> DicTerms = DocCollection.GetDeepKeywordAnalysisAsDictonary();

      this.lvListView.BeginUpdate();
            
      this.lvListView.Items.Clear();

      foreach( string sTerm in DicTerms.Keys )
      {

        string sKeyPair = sTerm;
        ListViewItem lvItem = null;

        if( this.lvListView.Items.ContainsKey( sKeyPair ) )
        {

          try
          {

            lvItem = this.lvListView.Items[ sKeyPair ];
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

            this.lvListView.Items.Add( lvItem );

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
      
      this.lvListView.EndUpdate();

    }

    /**************************************************************************/
    
    protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
    {
    }
    
    /**************************************************************************/

  }

}
