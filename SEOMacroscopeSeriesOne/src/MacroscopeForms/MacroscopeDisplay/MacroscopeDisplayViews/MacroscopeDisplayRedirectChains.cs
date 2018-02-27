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
  /// Description of MacroscopeDisplayRedirectChains.
  /// </summary>

  public sealed class MacroscopeDisplayRedirectChains : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private ToolStripLabel DocumentCount;
    private const int MAX_HOPS = 10;

    /**************************************************************************/

    public MacroscopeDisplayRedirectChains ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.DocumentCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelRedirectsItems;

      if ( this.MainForm.InvokeRequired )
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
      if ( !this.ListViewConfigured )
      {
        this.ConfigureListViewColumns();
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    public new void ClearData ()
    {
      if ( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker(
            delegate
            {
              this.ConfigureListViewColumns();
              this.DisplayListView.Items.Clear();
              this.RenderUrlCount();
            }
          )
        );
      }
      else
      {
        this.ConfigureListViewColumns();
        this.DisplayListView.Items.Clear();
        this.RenderUrlCount();
      }
    }

    /**************************************************************************/

    private void ConfigureListViewColumns ()
    {
      this.DisplayListView.Columns.Clear();
      for ( int iHop = 1 ; iHop <= MAX_HOPS ; iHop++ )
      {
        this.DisplayListView.Columns.Add( string.Format( "HOP_{0}_URL", iHop ), string.Format( "Hop {0} URL", iHop ) );
        this.DisplayListView.Columns.Add( string.Format( "HOP_{0}_STATUS", iHop ), string.Format( "Hop {0} Status", iHop ) );
      }
      this.DisplayListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );
    }

    /**************************************************************************/

    public void RefreshDataRedirectChains ( MacroscopeDocumentCollection DocCollection )
    {

      if ( DocCollection.CountDocuments() <= 0 )
      {
        return;
      }

      if ( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker(
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.DisplayListView.BeginUpdate();
              this.RenderListViewRedirectChains( DocCollection );
              this.RenderUrlCount();
              this.DisplayListView.EndUpdate();
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.DisplayListView.BeginUpdate();
        this.RenderListViewRedirectChains( DocCollection );
        this.RenderUrlCount();
        this.DisplayListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    private void RenderListViewRedirectChains ( MacroscopeDocumentCollection DocCollection )
    {

      List<ListViewItem> ListViewItems = new List<ListViewItem>( DocCollection.CountDocuments() );
      List<List<MacroscopeDocument>> RedirectChains = DocCollection.GetMacroscopeRedirectChains();

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = (decimal) DocCollection.CountDocuments();
      decimal MajorPercentage = ( (decimal) 100 / TotalDocs ) * Count;

      if ( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {

        ProgressForm.ControlBox = false;

        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );

      }

      foreach ( List<MacroscopeDocument> DocList in RedirectChains )
      {

        Application.DoEvents();

        if ( DocList.Count > 0 )
        {
          this.RenderListViewRedirectChains(
            ListViewItems: ListViewItems,
            DocCollection: DocCollection,
            DocList: DocList
          );
        }

        if ( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {

          Count++;
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
      this.DisplayListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );
      this.DisplayListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );

      if ( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }

      ProgressForm.Dispose();

    }

    /**************************************************************************/

    private void RenderListViewRedirectChains (
      List<ListViewItem> ListViewItems,
      MacroscopeDocumentCollection DocCollection,
      List<MacroscopeDocument> DocList
    )
    {

      ListViewItem lvItem = null;
      string PairKey = string.Join( "", DocList[ 0 ].GetUrl() );
      int IHOP = 0;

      if ( this.DisplayListView.Items.ContainsKey( PairKey ) )
      {
        try
        {
          lvItem = this.DisplayListView.Items[ PairKey ];
        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "MacroscopeDisplayRedirectChains 1: {0}", ex.Message ) );
        }
      }
      else
      {
        try
        {
          lvItem = new ListViewItem( PairKey );
          lvItem.UseItemStyleForSubItems = false;
          lvItem.Name = PairKey;
          lvItem.SubItems[ 0 ].Text = "";
          for ( int i = 1 ; i < MAX_HOPS ; i++ )
          {
            lvItem.SubItems.Add( "" );
          }
          ListViewItems.Add( lvItem );
        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "MacroscopeDisplayRedirectChains 2: {0}", ex.Message ) );
        }
      }
      
      foreach ( MacroscopeDocument msDoc in DocList )
      {

        string Url = msDoc.GetUrl();
        string StatusCode = ( (int) msDoc.GetStatusCode() ).ToString();

        if ( IHOP > ( MAX_HOPS * 2 ) )
        {
          break;
        }

        try
        {
          lvItem.SubItems[ IHOP ].Text = Url;
          IHOP++;
          lvItem.SubItems[ IHOP ].Text = StatusCode.ToString();
          IHOP++;
        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "MacroscopeDisplayRedirectChains 1: {0}", ex.Message ) );
        }

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
    }
    
    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
      this.DocumentCount.Text = string.Format( "Redirect Chains: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/

  }

}
