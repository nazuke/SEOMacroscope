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

  public sealed class MacroscopeDisplaySitemapsAudit : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int COL_URL = 0;
    private const int COL_IN_SITEMAP = 1;
    private const int COL_STATUS_CODE = 2;
    private const int COL_IS_REDIRECT = 3;
    private const int COL_ROBOTS = 4;
    private const int COL_SITEMAP = 5;

    /**************************************************************************/

    public MacroscopeDisplaySitemapsAudit ( MacroscopeMainForm MainForm, ListView TargetListView )
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

    public void RefreshDataSitemapsAudit ( MacroscopeDocumentCollection DocCollection )
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
              this.RenderListViewSitemapsAudit( DocCollection: DocCollection );
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
        this.RenderListViewSitemapsAudit( DocCollection: DocCollection );
        this.RenderUrlCount();
        this.DisplayListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    private void RenderListViewSitemapsAudit ( MacroscopeDocumentCollection DocCollection )
    {

      MacroscopeDocumentList DocumentsNotInSitemaps = DocCollection.GetDocumentsNotInSitemaps();
      MacroscopeDocumentList DocumentsInSitemaps = DocCollection.GetDocumentsInSitemaps();

      this._RenderListViewSitemapsAudit(
        DocCollection: DocCollection,
        DocumentList: DocumentsNotInSitemaps,
        InOut: false
      );

      this._RenderListViewSitemapsAudit(
        DocCollection: DocCollection,
        DocumentList: DocumentsInSitemaps,
        InOut: true
      );

      return;

    }

    /** -------------------------------------------------------------------- **/

    private void _RenderListViewSitemapsAudit (
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocumentList DocumentList,
      bool InOut
    )
    {

      List<ListViewItem> ListViewItems = new List<ListViewItem>( 1 );

      foreach ( MacroscopeDocument msDoc in DocumentList.IterateDocuments() )
      {

        string Url = null;
        string Robots = null;
        string SitemapUrl = null;
        string PairKey = null;
        ListViewItem lvItem = null;
        int StatusCode;

        if ( !msDoc.GetIsHtml() )
        {
          continue;
        }

        if ( msDoc.GetIsExternal() )
        {
          continue;
        }

        Url = msDoc.GetUrl();
        StatusCode = (int)msDoc.GetStatusCode();
        Robots = msDoc.GetAllowedByRobotsAsString();
        SitemapUrl = DocumentList.GetDocumentNote( msDoc: msDoc );
        PairKey = string.Join( "::::::::", Url );

        if ( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];
            lvItem.SubItems[ COL_URL ].Text = Url;
            lvItem.SubItems[ COL_IN_SITEMAP ].Text = InOut.ToString();
            lvItem.SubItems[ COL_STATUS_CODE ].Text = msDoc.GetStatusCode().ToString();
            lvItem.SubItems[ COL_IS_REDIRECT ].Text = msDoc.GetIsRedirect().ToString();
            lvItem.SubItems[ COL_ROBOTS ].Text = Robots;
            lvItem.SubItems[ COL_SITEMAP ].Text = SitemapUrl;
            
          }
          catch ( Exception ex )
          {
            DebugMsg( string.Format( "_RenderListViewSitemapsAudit 1: {0}", ex.Message ) );
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
            lvItem.SubItems.Add( InOut.ToString() );
            lvItem.SubItems.Add( msDoc.GetStatusCode().ToString() );
            lvItem.SubItems.Add( msDoc.GetIsRedirect().ToString() );
            lvItem.SubItems.Add( Robots );
            lvItem.SubItems.Add( SitemapUrl );

            ListViewItems.Add( lvItem );

          }
          catch ( Exception ex )
          {
            DebugMsg( string.Format( "_RenderListViewSitemapsAudit 2: {0}", ex.Message ) );
          }

        }

        try
        {

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( msDoc.GetIsInternal() )
            {
              lvItem.SubItems[ COL_URL ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ COL_URL ].ForeColor = Color.Gray;
            }

            if ( InOut )
            {
              lvItem.SubItems[ COL_IN_SITEMAP ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ COL_IN_SITEMAP ].ForeColor = Color.Red;
            }

            if ( ( StatusCode >= 200 ) && ( StatusCode <= 299 ) )
            {
              lvItem.SubItems[ COL_STATUS_CODE ].ForeColor = Color.Green;
            }
            else
            if ( ( StatusCode >= 300 ) && ( StatusCode <= 399 ) )
            {
              lvItem.SubItems[ COL_STATUS_CODE ].ForeColor = Color.Goldenrod;
            }
            else
            if ( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
            {
              lvItem.SubItems[ COL_STATUS_CODE ].ForeColor = Color.Red;
            }
            else
            {
              lvItem.SubItems[ COL_STATUS_CODE ].ForeColor = Color.Blue;
            }

            if ( StatusCode == 410 )
            {
              lvItem.SubItems[ COL_STATUS_CODE ].ForeColor = Color.Purple;
            }

            if ( msDoc.GetIsRedirect() )
            {
              lvItem.SubItems[ COL_IS_REDIRECT ].ForeColor = Color.Goldenrod;
            }
            else
            {
              lvItem.SubItems[ COL_IS_REDIRECT ].ForeColor = Color.Gray;
            }

            if ( !msDoc.GetAllowedByRobots() )
            {
              lvItem.SubItems[ COL_ROBOTS ].ForeColor = Color.Red;
            }
            else
            {
              lvItem.SubItems[ COL_ROBOTS ].ForeColor = Color.Green;
            }

            if ( msDoc.GetIsInternal() )
            {
              lvItem.SubItems[ COL_SITEMAP ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ COL_SITEMAP ].ForeColor = Color.Gray;
            }

          }
          else
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
          }

        }
        catch ( Exception ex )
        {
          DebugMsg( string.Format( "_RenderListViewSitemapsAudit 3: {0}", ex.Message ) );
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
