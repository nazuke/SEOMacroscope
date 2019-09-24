/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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

  public sealed class MacroscopeDisplaySitemapErrors : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplaySitemapErrors ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;

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
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    public void RefreshDataSitemapErrors ( MacroscopeDocumentCollection DocCollection )
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
              this.RenderListViewSitemapErrors( DocCollection: DocCollection );
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
        this.RenderListViewSitemapErrors( DocCollection: DocCollection );
        this.RenderUrlCount();
        this.DisplayListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    private void RenderListViewSitemapErrors ( MacroscopeDocumentCollection DocCollection )
    {

      List<ListViewItem> ListViewItems = new List<ListViewItem>( 1 );
      List<Dictionary<string, string>> CompiledTable = DocCollection.GetSitemapErrorsAsTable();

      foreach ( Dictionary<string, string> Entry in CompiledTable )
      {

        string SitemapUrl = Entry[ "sitemap_url" ];
        string StatusCode = Entry[ "status_code" ];
        string Robots = Entry[ "robots" ];
        string TargetUrl = Entry[ "target_url" ];

        string PairKey = string.Join( "::::::::", SitemapUrl, TargetUrl );

        MacroscopeDocument msDoc = DocCollection.GetDocumentByUrl( Url: SitemapUrl );
        MacroscopeDocument msDocLinked = DocCollection.GetDocumentByUrl( Url: TargetUrl );

        ListViewItem lvItem = null;

        if ( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];
            lvItem.SubItems[ 0 ].Text = SitemapUrl;
            lvItem.SubItems[ 1 ].Text = StatusCode;
            lvItem.SubItems[ 2 ].Text = Robots;
            lvItem.SubItems[ 3 ].Text = TargetUrl;

          }
          catch ( Exception ex )
          {
            DebugMsg( string.Format( "RenderListViewSitemapErrors 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem( PairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = PairKey;

            lvItem.SubItems[ 0 ].Text = SitemapUrl;
            lvItem.SubItems.Add( StatusCode );
            lvItem.SubItems.Add( Robots );
            lvItem.SubItems.Add( TargetUrl );

            ListViewItems.Add( lvItem );

          }
          catch ( Exception ex )
          {
            DebugMsg( string.Format( "RenderListViewSitemapErrors 2: {0}", ex.Message ) );
          }

        }

        if ( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          if ( msDoc.GetIsInternal() )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          }


          if ( !msDocLinked.GetAllowedByRobots() )
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Green;
          }

          if ( msDocLinked.GetIsInternal() )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Green;
          }
        }
        else
        {
          lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
        }

      }

      this.DisplayListView.Items.AddRange( ListViewItems.ToArray() );

      return;
      
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
    }

    /**************************************************************************/

  }

}
