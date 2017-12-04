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
using System.Diagnostics;
using System.Threading;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayStructureKeywordAnalysis : Macroscope
  {

    /**************************************************************************/

    private MacroscopeMainForm MainForm;
          
    private List<ListView> TargetListViews = new List<ListView> ( 4 );

    /**************************************************************************/

    public MacroscopeDisplayStructureKeywordAnalysis (
      MacroscopeMainForm MainForm,
      ListView TargetListView1,
      ListView TargetListView2,
      ListView TargetListView3,
      ListView TargetListView4
    )
    {

      this.SuppressDebugMsg = true;
      
      this.MainForm = MainForm;
      
      this.TargetListViews.Add( TargetListView1 );
      this.TargetListViews.Add( TargetListView2 );
      this.TargetListViews.Add( TargetListView3 );
      this.TargetListViews.Add( TargetListView4 );

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
                this.TargetListViews[ i ].BeginUpdate();
                this.TargetListViews[ i ].Items.Clear();
                this.TargetListViews[ i ].EndUpdate();
              }
            }
          )
        );
      }
      else
      {
        for( int i = 0 ; i <= 3 ; i++ )
        {
          this.TargetListViews[ i ].BeginUpdate();
          this.TargetListViews[ i ].Items.Clear();
          this.TargetListViews[ i ].EndUpdate();
        }
      }
    }

    /**************************************************************************/

    public void RefreshKeywordAnalysisData ( MacroscopeDocumentCollection DocCollection )
    {

      if( DocCollection.CountDocuments() == 0 )
      {
        return;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RefreshKeywordAnalysisDataProgress( DocCollection: DocCollection );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RefreshKeywordAnalysisDataProgress( DocCollection: DocCollection );
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RefreshKeywordAnalysisDataProgress ( MacroscopeDocumentCollection DocCollection )
    {

      MacroscopeDoublePercentageProgressForm ProgressForm = new MacroscopeDoublePercentageProgressForm ( this.MainForm );

      decimal MajorPercentage = 0;

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {  

        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing keyword terms collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: "",       
          MinorPercentage: 0,
          ProgressLabelMinor: ""
        );

      }

      try
      {
        ProgressForm.TopMost = true;
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "ProgressForm.Show(): {0}", ex.Message ) );
      }

      for( int i = 0 ; i <= 3 ; i++ )
      {

        List<ListViewItem> ListViewItems = new List<ListViewItem> ( DocCollection.CountDocuments() );

        Application.DoEvents();
        
        if( !ProgressForm.Cancelled() )
        {

          Dictionary<string,int> DicTerms = DocCollection.GetDeepKeywordAnalysisAsDictonary( Words: i + 1 );


          if( MacroscopePreferencesManager.GetShowProgressDialogues() )
          {

            MajorPercentage = ( ( decimal )100 / ( decimal )4 ) * ( decimal )( i + 1 );
        
            ProgressForm.UpdatePercentages(
              Title: null,
              Message: null,
              MajorPercentage: MajorPercentage,
              ProgressLabelMajor: string.Format( "{0} Word Keywords", i + 1 ),       
              MinorPercentage: 0,
              ProgressLabelMinor: ""
            );
        
          }
          
          this.TargetListViews[ i ].BeginUpdate();

          this.RenderKeywordAnalysisListView(
            ListViewItems: ListViewItems,
            TargetListView: this.TargetListViews[ i ],
            DicTerms: DicTerms,
            ProgressForm: ProgressForm
          );
               
          this.TargetListViews[ i ].Items.AddRange( ListViewItems.ToArray() );
      
          this.TargetListViews[ i ].EndUpdate();
        
        }

      }

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }

      ProgressForm.Dispose();

    }

    /**************************************************************************/
    
    private void RenderKeywordAnalysisListView (
      List<ListViewItem> ListViewItems,
      ListView TargetListView,
      Dictionary<string,int> DicTerms,
      MacroscopeDoublePercentageProgressForm ProgressForm
    )
    {

      decimal Count = 0;
      decimal TotalTerms = ( decimal )DicTerms.Count;
      decimal MinorPercentage = 0;

      if( TotalTerms <= 0 )
      {
        return;
      }
      
      try
      {
          
        foreach( string KeywordTerm in DicTerms.Keys )
        {

          string PairKey = KeywordTerm;
          ListViewItem lvItem = null;

          if( TargetListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = TargetListView.Items[ PairKey ];
              lvItem.SubItems[ 0 ].Text = DicTerms[ KeywordTerm ].ToString();
              lvItem.SubItems[ 1 ].Text = KeywordTerm;

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

              lvItem = new ListViewItem ( PairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = PairKey;

              lvItem.SubItems[ 0 ].Text = DicTerms[ KeywordTerm ].ToString();
              lvItem.SubItems.Add( KeywordTerm );

              ListViewItems.Add( lvItem );

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

          Count++;
          MinorPercentage = ( ( decimal )100 / TotalTerms ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: -1,
            ProgressLabelMajor: null,       
            MinorPercentage: MinorPercentage,
            ProgressLabelMinor: string.Format( "Keyword: {0}", Count )
          );

          Application.DoEvents();
        
          if( ProgressForm.Cancelled() )
          {
            break;
          }

        }

      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "MacroscopeDisplayStructureKeywordAnalysis 3: {0}", ex.Message ) );
      }

    }

    /**************************************************************************/

  }

}
