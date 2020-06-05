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

  public sealed class MacroscopeDisplayHistory : Macroscope
  {

    /**************************************************************************/

    private MacroscopeMainForm MainForm;

    private ListView DisplayListView;
    private Object DisplayListViewLocker;

    private bool ListViewConfigured = false;

    private ToolStripLabel DocumentCount;

    private const int ColUrl = 0;
    private const int ColVisited = 1;
    private const int ColInDocCollection = 2;

    /**************************************************************************/

    public MacroscopeDisplayHistory ( MacroscopeMainForm MainForm, ListView TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.DocumentCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelHistoryItems;

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

    private void ConfigureListView ()
    {

      this.DisplayListViewLocker = new object();

      if( !this.ListViewConfigured )
      {
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    public void ClearData ()
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker(
            delegate
            {
              this.DisplayListView.Items.Clear();
            }
          )
        );
      }
      else
      {
        this.DisplayListView.Items.Clear();
      }
    }

    /**************************************************************************/

    public void RefreshData ( Dictionary<ulong, bool> History, MacroscopeDocumentCollection DocCollection )
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker(
            delegate
            {
              lock( this.DisplayListViewLocker )
              {
                Cursor.Current = Cursors.WaitCursor;
                this.DisplayListView.BeginUpdate();
                this.RenderListView( History: History, DocCollection: DocCollection );
                this.RenderUrlCount();
                this.DisplayListView.EndUpdate();
                Cursor.Current = Cursors.Default;

              }
            }
          )
        );
      }
      else
      {
        lock( this.DisplayListViewLocker )
        {
          Cursor.Current = Cursors.WaitCursor;
          this.DisplayListView.BeginUpdate();
          this.RenderListView( History: History, DocCollection: DocCollection );
          this.RenderUrlCount();
          this.DisplayListView.EndUpdate();
          Cursor.Current = Cursors.Default;
        }
      }
    }

    /**************************************************************************/

    private void RenderListView ( Dictionary<ulong, bool> History, MacroscopeDocumentCollection DocCollection )
    {

      if( History.Count == 0 )
      {
        return;
      }

      List<ListViewItem> ListViewItems = new List<ListViewItem>( 1 );

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = (decimal) History.Count;
      decimal MajorPercentage = ( (decimal) 100 / TotalDocs ) * Count;

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {

        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );

      }

      foreach( ulong DocKey in History.Keys )
      {

        ListViewItem lvItem = null;
        MacroscopeDocument msDoc = DocCollection.GetDocumentByDocKey( DocKey: DocKey );
        string PairKey = DocKey.ToString();

        if( msDoc != null )
        {

          string Url = msDoc.GetUrl();
          string Visited = "No";
          string InDocCollection = "No";

          if( History.ContainsKey( DocKey ) && History[ DocKey ] )
          {
            Visited = "Yes";
          }

          if( DocCollection.ContainsDocument( Url: Url ) )
          {
            InDocCollection = "Yes";
          }

          if( this.DisplayListView.Items.ContainsKey( PairKey ) )
          {

            try
            {
              lvItem = this.DisplayListView.Items[ PairKey ];
              lvItem.SubItems[ ColUrl ].Text = Url;
              lvItem.SubItems[ ColVisited ].Text = Visited;
              lvItem.SubItems[ ColInDocCollection ].Text = InDocCollection;
            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListView 1: {0}", ex.Message ) );
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
              lvItem.SubItems.Add( Visited );
              lvItem.SubItems.Add( InDocCollection );

              ListViewItems.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListView 2: {0}", ex.Message ) );
            }

          }

          if( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if( AllowedHosts.IsInternalUrl( Url ) )
            {
              lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
              if( History.ContainsKey( DocKey ) && History[ DocKey ] )
              {
                lvItem.SubItems[ ColVisited ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ ColVisited ].ForeColor = Color.Red;
              }
              lvItem.SubItems[ ColInDocCollection ].ForeColor = Color.Blue;
            }
            else
            {
              lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
              lvItem.SubItems[ ColVisited ].ForeColor = Color.Gray;
              lvItem.SubItems[ ColInDocCollection ].ForeColor = Color.Gray;
            }

          }

        }

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {

          Count++;
          TotalDocs = (decimal) History.Count;
          MajorPercentage = ( (decimal) 100 / TotalDocs ) * Count;

          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );

        }

      }

      this.DisplayListView.Items.AddRange( ListViewItems.ToArray() );

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }

      if( ProgressForm != null )
      {
        ProgressForm.Dispose();
      }

    }

    /**************************************************************************/

    private void RenderUrlCount ()
    {
      this.DocumentCount.Text = string.Format( "Items: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/

  }

}
