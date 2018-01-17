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
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace SEOMacroscope
{

  public partial class MacroscopeDocumentDetails : MacroscopeUserControl
  {

    /**************************************************************************/

    private MacroscopeHttpImageLoader HttpImageLoader;
    private MacroscopeColumnSorter lvColumnSorter;
    private MacroscopeContextMenus ContextMenusCallbacks;

    private Object RenderListViewLock;

    //private Object RenderDocumentDetailsLock;
    //private Object RenderListViewMetaTagsLock;
    //private Object RenderDocumentHrefLangLock;


    /**************************************************************************/

    public MacroscopeDocumentDetails ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      /** Control Properties ----------------------------------------------- **/

      this.tabControlDocument.Multiline = false;
      this.listViewDocumentInfo.Dock = DockStyle.Fill;
      this.tableLayoutPanelHttpHeaders.Dock = DockStyle.Fill;
      this.textBoxHttpRequestHeaders.Dock = DockStyle.Fill;
      this.textBoxHttpResponseHeaders.Dock = DockStyle.Fill;
      this.listViewMetaTags.Dock = DockStyle.Fill;
      this.listViewHrefLang.Dock = DockStyle.Fill;
      this.listViewLinksIn.Dock = DockStyle.Fill;
      this.listViewLinksOut.Dock = DockStyle.Fill;
      this.listViewHyperlinksIn.Dock = DockStyle.Fill;
      this.listViewHyperlinksOut.Dock = DockStyle.Fill;
      this.listViewInsecureLinks.Dock = DockStyle.Fill;
      this.listViewImages.Dock = DockStyle.Fill;
      this.listViewStylesheets.Dock = DockStyle.Fill;
      this.listViewJavascripts.Dock = DockStyle.Fill;
      this.listViewAudios.Dock = DockStyle.Fill;
      this.listViewVideos.Dock = DockStyle.Fill;
      this.listViewKeywordAnalysis.Dock = DockStyle.Fill;
      this.listViewRemarks.Dock = DockStyle.Fill;
      this.textBoxDocumentTextRaw.Dock = DockStyle.Fill;
      this.textBoxDocumentTextCleaned.Dock = DockStyle.Fill;
      this.textBoxBodyTextRaw.Dock = DockStyle.Fill;
      this.listViewCustomFilters.Dock = DockStyle.Fill;

      this.HttpImageLoader = new MacroscopeHttpImageLoader();

      this.listViewDocInfo.Dock = DockStyle.Fill;

      /** ListView Sorters ------------------------------------------------- **/

      this.lvColumnSorter = new MacroscopeColumnSorter();

      this.listViewMetaTags.ColumnClick += this.CallbackColumnClick;
      this.listViewHrefLang.ColumnClick += this.CallbackColumnClick;
      this.listViewLinksIn.ColumnClick += this.CallbackColumnClick;
      this.listViewLinksOut.ColumnClick += this.CallbackColumnClick;
      this.listViewHyperlinksIn.ColumnClick += this.CallbackColumnClick;
      this.listViewHyperlinksOut.ColumnClick += this.CallbackColumnClick;
      this.listViewInsecureLinks.ColumnClick += this.CallbackColumnClick;
      this.listViewImages.ColumnClick += this.CallbackColumnClick;
      this.listViewStylesheets.ColumnClick += this.CallbackColumnClick;
      this.listViewJavascripts.ColumnClick += this.CallbackColumnClick;
      this.listViewAudios.ColumnClick += this.CallbackColumnClick;
      this.listViewVideos.ColumnClick += this.CallbackColumnClick;
      this.listViewKeywordAnalysis.ColumnClick += this.CallbackColumnClick;
      this.listViewRemarks.ColumnClick += this.CallbackColumnClick;
      this.listViewCustomFilters.ColumnClick += this.CallbackColumnClick;

      /** Context Menus ---------------------------------------------------- **/

      this.ContextMenusCallbacks = new MacroscopeContextMenus();

      this.copyRows.Click += this.ContextMenusCallbacks.CallbackCopyRowsClick;
      this.copyValues.Click += this.ContextMenusCallbacks.CallbackCopyValuesClick;

      this.copyDocumentListRows.Click += this.ContextMenusCallbacks.CallbackCopyRowsClick;
      this.copyDocumentListValues.Click += this.ContextMenusCallbacks.CallbackCopyValuesClick;

      this.openSourceUrlInBrowser.Click += this.ContextMenusCallbacks.CallbackOpenSourceUrlInBrowserClick;
      this.openTargetUrlInBrowser.Click += this.ContextMenusCallbacks.CallbackOpenTargetUrlInBrowserClick;

      this.copySourceUrl.Click += this.ContextMenusCallbacks.CallbackCopySourceUrlClick;
      this.copyTargetUrl.Click += this.ContextMenusCallbacks.CallbackCopyTargetClick;

      this.copyRawSourceUrl.Click += this.ContextMenusCallbacks.CallbackCopyRawSourceUrlClick;
      this.copyRawTargetUrl.Click += this.ContextMenusCallbacks.CallbackCopyRawTargetUrlClick;

      this.copyLinkText.Click += this.ContextMenusCallbacks.CallbackCopyLinkTextClick;
      this.copyAltText.Click += this.ContextMenusCallbacks.CallbackCopyAltTextClick;
      this.copyTitleText.Click += this.ContextMenusCallbacks.CallbackCopyTitleTextClick;

      this.listViewLinksIn.ContextMenuStrip = this.contextMenuStripDocumentLists;

      /** Collapsible Panels ----------------------------------------------- **/

      this.splitContainerDocumentDetails.Panel2Collapsed = true;

      /** Lock Objects ----------------------------------------------------- **/
      
      this.RenderListViewLock = new Object();


      //this.RenderDocumentDetailsLock = new Object();
      //this.RenderListViewMetaTagsLock = new Object();
      //this.RenderDocumentHrefLangLock = new Object();

      return;

    }

    /**************************************************************************/

    private void MacroscopeDocumentDetailsLoad ( object sender, EventArgs e )
    {
    }

    /**************************************************************************/

    private void CallbackColumnClick ( object sender, ColumnClickEventArgs e )
    {

      ListView TargetListView = sender as ListView;

      TargetListView.ListViewItemSorter = this.lvColumnSorter;

      if ( e.Column == lvColumnSorter.SortColumn )
      {
        if ( lvColumnSorter.Order == SortOrder.Ascending )
        {
          lvColumnSorter.Order = SortOrder.Descending;
        }
        else
        {
          lvColumnSorter.Order = SortOrder.Ascending;
        }
      }
      else
      {
        lvColumnSorter.SortColumn = e.Column;
        lvColumnSorter.Order = SortOrder.Ascending;
      }

      TargetListView.Sort();

      TargetListView.ListViewItemSorter = null;

    }

    /**************************************************************************/

    public void ClearData ()
    {

      this.listViewDocumentInfo.Items.Clear();

      this.textBoxHttpRequestHeaders.Text = "";
      this.textBoxHttpResponseHeaders.Text = "";

      this.listViewMetaTags.Items.Clear();
      this.listViewHrefLang.Items.Clear();
      this.listViewLinksIn.Items.Clear();
      this.listViewLinksOut.Items.Clear();
      this.listViewHyperlinksIn.Items.Clear();
      this.listViewHyperlinksOut.Items.Clear();
      this.listViewInsecureLinks.Items.Clear();
      this.listViewImages.Items.Clear();
      this.listViewStylesheets.Items.Clear();
      this.listViewJavascripts.Items.Clear();
      this.listViewAudios.Items.Clear();
      this.listViewVideos.Items.Clear();
      this.listViewKeywordAnalysis.Items.Clear();
      this.listViewRemarks.Items.Clear();

      this.textBoxDocumentTextRaw.Text = "";
      this.textBoxDocumentTextCleaned.Text = "";
      this.textBoxBodyTextRaw.Text = "";

      this.listViewCustomFilters.Items.Clear();

      this.pictureBoxDocumentDetailsImage.Image = null;
      this.listViewDocInfo.Columns.Clear();

    }

    /**************************************************************************/

    public bool UpdateDisplay ( MacroscopeJobMaster JobMaster, string Url )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeDocument msDoc = DocCollection.GetDocument( Url );

      if ( msDoc != null )
      {

        if ( this.InvokeRequired )
        {
          this.Invoke(
            new MethodInvoker(
              delegate
              {
                Cursor.Current = Cursors.WaitCursor;
                this.UpdateDocumentDetailsDisplay(
                  JobMaster: JobMaster,
                  DocCollection: DocCollection,
                  msDoc: msDoc
                );
                Cursor.Current = Cursors.Default;
              }
            )
          );
        }
        else
        {
          Cursor.Current = Cursors.WaitCursor;
          this.UpdateDocumentDetailsDisplay(
            JobMaster: JobMaster,
            DocCollection: DocCollection,
            msDoc: msDoc
          );
          Cursor.Current = Cursors.Default;
        }

        return ( true );

      }

      return ( false );

    }

    /**************************************************************************/

    private void UpdateDocumentDetailsDisplay (
      MacroscopeJobMaster JobMaster,
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc
    )
    {

      this.RenderDocumentDetails( JobMaster, msDoc );
      this.RenderDocumentHttpHeaders( JobMaster, msDoc );

      this.RenderListViewMetaTags( JobMaster, msDoc );

      this.RenderDocumentHrefLang( msDoc, JobMaster, DocCollection );

      this.RenderListViewLinksIn( JobMaster, msDoc );
      this.RenderListViewLinksOut( JobMaster, msDoc );

      this.RenderListViewHyperlinksIn( JobMaster, msDoc );
      this.RenderListViewHyperlinksOut( JobMaster, msDoc );

      this.RenderListViewInsecureLinks( JobMaster, msDoc );

      this.RenderListViewStylesheets( JobMaster, msDoc );

      this.RenderListViewJavascripts( JobMaster, msDoc );

      this.RenderListViewImages( JobMaster, msDoc );

      this.RenderListViewAudios( JobMaster, msDoc );

      this.RenderListViewVideos( JobMaster, msDoc );

      if ( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
      {
        this.RenderListViewKeywordAnalysis( JobMaster, msDoc );
      }

      this.RenderListViewRemarks( JobMaster, msDoc );

      this.RenderTextBoxDocumentTextRaw( JobMaster, msDoc );
      this.RenderTextBoxDocumentTextCleaned( JobMaster, msDoc );

      this.RenderTextBoxBodyTextRaw( JobMaster, msDoc );

      if ( MacroscopePreferencesManager.GetCustomFiltersEnable() )
      {
        this.RenderListViewCustomFilters( JobMaster, msDoc );
      }

      this.RenderDocumentPreview( JobMaster, msDoc );

    }

    /**************************************************************************/

    private void RenderDocumentDetails ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewDocumentInfo;
      List<KeyValuePair<string, string>> lItems = msDoc.DetailDocumentDetails();
      List<ListViewItem> ListViewItems = new List<ListViewItem>( lItems.Count );

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        for ( int i = 0 ; i < lItems.Count ; i++ )
        {

          KeyValuePair<string, string> kvItem = lItems[ i ];

          try
          {
            ListViewItem lvItem = new ListViewItem( kvItem.Key );
            lvItem.Name = kvItem.Key;
            lvItem.SubItems.Add( kvItem.Value );
            ListViewItems.Add( lvItem );
          }
          catch ( Exception ex )
          {
            DebugMsg( string.Format( "RenderDocumentDetails: {0}", ex.Message ) );
          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /**************************************************************************/

    private void RenderDocumentHttpHeaders ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      this.textBoxHttpRequestHeaders.Text = msDoc.GetHttpRequestHeadersAsText();

      this.textBoxHttpResponseHeaders.Text = string.Join(
        "",
        msDoc.GetHttpResponseStatusLineAsText(),
        msDoc.GetHttpResponseHeadersAsText()
      );

    }

    /** META Tags *************************************************************/

    private void RenderListViewMetaTags ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewMetaTags;
      List<ListViewItem> ListViewItems = new List<ListViewItem>();

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( KeyValuePair<string, string> KP in msDoc.IterateMetaTags() )
        {

          Application.DoEvents();

          ListViewItem lvItem = null;
          string MetaName = KP.Key;
          string MetaContent = KP.Value;
          string PairKey = string.Join( "::", MetaName, MetaContent );

          if ( TargetListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = TargetListView.Items[ PairKey ];
              lvItem.SubItems[ 0 ].Text = MetaName;
              lvItem.SubItems[ 1 ].Text = MetaContent;

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewMetaTags 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem( PairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = PairKey;

              lvItem.SubItems[ 0 ].Text = MetaName;
              lvItem.SubItems.Add( MetaContent );

              ListViewItems.Add( lvItem );

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewMetaTags 2: {0}", ex.Message ) );
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** HrefLang Tags *********************************************************/

    private void RenderDocumentHrefLang (
      MacroscopeDocument msDoc,
      MacroscopeJobMaster JobMaster,
      MacroscopeDocumentCollection DocCollection
    )
    {

      ListView TargetListView = this.listViewHrefLang;
      List<ListViewItem> ListViewItems = new List<ListViewItem>();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      Dictionary<string, string> LocalesList = JobMaster.GetLocales();

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();
        TargetListView.Columns.Clear();

        {
          TargetListView.Columns.Add( "URL", "URL" );
          TargetListView.Columns.Add( "Site Locale", "Site Locale" );
          TargetListView.Columns.Add( "Title", "Title" );
        }

        string KeyUrl = msDoc.GetUrl();

        if ( msDoc.GetIsHtml() )
        {

          Dictionary<string, MacroscopeHrefLang> HrefLangsTable = msDoc.GetHrefLangs();

          if ( HrefLangsTable != null )
          {

            {

              ListViewItem lvItem = new ListViewItem( KeyUrl );

              lvItem.Name = KeyUrl;

              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );

              lvItem.SubItems[ 0 ].Text = msDoc.GetUrl();
              lvItem.SubItems[ 1 ].Text = msDoc.GetLocale();
              lvItem.SubItems[ 2 ].Text = msDoc.GetTitle();

              ListViewItems.Add( lvItem );

              if ( msDoc.GetIsInternal() )
              {
                lvItem.SubItems[ 0 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
              }

            }

            foreach ( string Locale in LocalesList.Keys )
            {

              Application.DoEvents();

              if ( Locale != null )
              {

                if ( Locale == msDoc.GetLocale() )
                {
                  continue;
                }

                string DocHrefLangUrl = null;
                string Title = "";
                ListViewItem lvItem = new ListViewItem( Locale );

                lvItem.Name = Locale;

                lvItem.SubItems.Add( "" );
                lvItem.SubItems.Add( "" );
                lvItem.SubItems.Add( "" );

                if ( HrefLangsTable.ContainsKey( Locale ) )
                {

                  MacroscopeHrefLang HrefLangAlternate = HrefLangsTable[ Locale ];

                  if ( HrefLangAlternate != null )
                  {

                    DocHrefLangUrl = HrefLangAlternate.GetUrl();

                    if ( DocCollection.DocumentExists( DocHrefLangUrl ) )
                    {
                      Title = DocCollection.GetDocument( DocHrefLangUrl ).GetTitle();
                    }

                  }

                }

                lvItem.SubItems[ 1 ].Text = Locale;
                lvItem.SubItems[ 2 ].Text = Title;

                if ( DocHrefLangUrl != null )
                {

                  if ( AllowedHosts.IsInternalUrl( DocHrefLangUrl ) )
                  {
                    lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
                  }
                  else
                  {
                    lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
                  }

                  lvItem.SubItems[ 0 ].Text = DocHrefLangUrl;

                }
                else
                {

                  if ( AllowedHosts.IsInternalUrl( DocHrefLangUrl ) )
                  {
                    lvItem.SubItems[ 0 ].ForeColor = Color.Red;
                  }
                  else
                  {
                    lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
                  }

                  lvItem.SubItems[ 0 ].Text = "NOT SPECIFIED";

                }

                TargetListView.Items.Add( lvItem );

              }

            }

          }

          TargetListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );

          TargetListView.Columns[ "URL" ].Width = 300;
          TargetListView.Columns[ "Site Locale" ].Width = 100;
          TargetListView.Columns[ "Title" ].Width = 300;

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Links In **************************************************************/

    private void RenderListViewLinksIn ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewLinksIn;
      MacroscopeLinkList LinksIn = msDoc.GetLinksIn();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      List<ListViewItem> ListViewItems = null;
      int Count = 0;

      if ( LinksIn != null )
      {
        ListViewItems = new List<ListViewItem>( LinksIn.Count() );
      }
      else
      {
        TargetListView.BeginUpdate();
        TargetListView.Items.Clear();
        TargetListView.EndUpdate();
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        if ( LinksIn != null )
        {

          foreach ( MacroscopeLink Link in LinksIn.IterateLinks() )
          {

            Application.DoEvents();

            Count++;

            ListViewItem lvItem = null;
            string PairKey = string.Join( "____", Count.ToString(), Link.GetLinkGuid().ToString() );
            string DoFollow = "No Follow";

            if ( Link.GetDoFollow() )
            {
              DoFollow = "Follow";
            }

            if ( TargetListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = Count.ToString();
                lvItem.SubItems[ 1 ].Text = Link.GetLinkType().ToString();
                lvItem.SubItems[ 2 ].Text = Link.GetSourceUrl();
                lvItem.SubItems[ 3 ].Text = Link.GetTargetUrl();
                lvItem.SubItems[ 4 ].Text = DoFollow;
                lvItem.SubItems[ 5 ].Text = Link.GetAltText();
                lvItem.SubItems[ 6 ].Text = Link.GetRawSourceUrl();
                lvItem.SubItems[ 7 ].Text = Link.GetRawTargetUrl();

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewLinksIn 1: {0}", ex.Message ) );
              }

            }
            else
            {

              try
              {

                lvItem = new ListViewItem( PairKey );
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Name = PairKey;

                lvItem.SubItems[ 0 ].Text = Count.ToString();
                lvItem.SubItems.Add( Link.GetLinkType().ToString() );
                lvItem.SubItems.Add( Link.GetSourceUrl() );
                lvItem.SubItems.Add( Link.GetTargetUrl() );
                lvItem.SubItems.Add( DoFollow );
                lvItem.SubItems.Add( Link.GetAltText() );
                lvItem.SubItems.Add( Link.GetRawSourceUrl() );
                lvItem.SubItems.Add( Link.GetRawTargetUrl() );

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewLinksIn 2: {0}", ex.Message ) );
              }

            }

            if ( lvItem != null )
            {

              lvItem.ForeColor = Color.Blue;

              if ( AllowedHosts.IsAllowedFromUrl( Link.GetSourceUrl() ) )
              {
                lvItem.SubItems[ 2 ].ForeColor = Color.Green;
                lvItem.SubItems[ 5 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
                lvItem.SubItems[ 5 ].ForeColor = Color.Gray;
              }

              if ( AllowedHosts.IsAllowedFromUrl( Link.GetTargetUrl() ) )
              {
                lvItem.SubItems[ 3 ].ForeColor = Color.Green;
                lvItem.SubItems[ 6 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
                lvItem.SubItems[ 6 ].ForeColor = Color.Gray;
              }

              if ( AllowedHosts.IsAllowedFromUrl( Link.GetTargetUrl() ) )
              {
                if ( Link.GetDoFollow() )
                {
                  lvItem.SubItems[ 4 ].ForeColor = Color.Green;
                }
                else
                {
                  lvItem.SubItems[ 4 ].ForeColor = Color.Red;
                }
              }
              else
              {
                lvItem.SubItems[ 4 ].ForeColor = Color.Gray;
              }

            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Links Out *************************************************************/

    private void RenderListViewLinksOut ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewLinksOut;
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      List<ListViewItem> ListViewItems = null;
      int Count = 0;

      if ( msDoc != null )
      {
        ListViewItems = new List<ListViewItem>( msDoc.CountOutlinks() );
      }
      else
      {
        TargetListView.BeginUpdate();
        TargetListView.Items.Clear();
        TargetListView.EndUpdate();
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( MacroscopeLink Link in msDoc.IterateOutlinks() )
        {

          Application.DoEvents();

          Count++;

          ListViewItem lvItem = null;
          string PairKey = string.Join( "____", Count.ToString(), Link.GetLinkGuid().ToString() );

          string DoFollow = "No Follow";

          if ( Link.GetDoFollow() )
          {
            DoFollow = "Follow";
          }

          if ( TargetListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = TargetListView.Items[ PairKey ];
              lvItem.SubItems[ 0 ].Text = Count.ToString();
              lvItem.SubItems[ 1 ].Text = Link.GetLinkType().ToString();
              lvItem.SubItems[ 2 ].Text = Link.GetSourceUrl();
              lvItem.SubItems[ 3 ].Text = Link.GetTargetUrl();
              lvItem.SubItems[ 4 ].Text = DoFollow;
              lvItem.SubItems[ 5 ].Text = Link.GetAltText();
              lvItem.SubItems[ 6 ].Text = Link.GetRawSourceUrl();
              lvItem.SubItems[ 7 ].Text = Link.GetRawTargetUrl();

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewLinksOut 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem( PairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = PairKey;

              lvItem.SubItems[ 0 ].Text = Count.ToString();
              lvItem.SubItems.Add( Link.GetLinkType().ToString() );
              lvItem.SubItems.Add( Link.GetSourceUrl() );
              lvItem.SubItems.Add( Link.GetTargetUrl() );
              lvItem.SubItems.Add( DoFollow );
              lvItem.SubItems.Add( Link.GetAltText() );
              lvItem.SubItems.Add( Link.GetRawSourceUrl() );
              lvItem.SubItems.Add( Link.GetRawTargetUrl() );

              ListViewItems.Add( lvItem );

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewLinksOut 2: {0}", ex.Message ) );
            }

          }

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( AllowedHosts.IsAllowedFromUrl( Link.GetSourceUrl() ) )
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Green;
              lvItem.SubItems[ 6 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 6 ].ForeColor = Color.Gray;
            }

            if ( AllowedHosts.IsAllowedFromUrl( Link.GetTargetUrl() ) )
            {
              lvItem.SubItems[ 3 ].ForeColor = Color.Green;
              lvItem.SubItems[ 7 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 7 ].ForeColor = Color.Gray;
            }

            if ( AllowedHosts.IsAllowedFromUrl( Link.GetTargetUrl() ) )
            {
              if ( Link.GetDoFollow() )
              {
                lvItem.SubItems[ 4 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 4 ].ForeColor = Color.Red;
              }
            }
            else
            {
              lvItem.SubItems[ 4 ].ForeColor = Color.Gray;
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Hyperlinks In *********************************************************/

    private void RenderListViewHyperlinksIn ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewHyperlinksIn;
      MacroscopeHyperlinksIn HyperlinksIn = msDoc.GetHyperlinksIn();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      List<ListViewItem> ListViewItems = null;
      int Count = 0;

      if ( HyperlinksIn != null )
      {
        ListViewItems = new List<ListViewItem>( HyperlinksIn.Count() );
      }
      else
      {
        TargetListView.BeginUpdate();
        TargetListView.Items.Clear();
        TargetListView.EndUpdate();
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        if ( HyperlinksIn != null )
        {

          foreach ( MacroscopeHyperlinkIn HyperlinkIn in HyperlinksIn.IterateLinks() )
          {

            Application.DoEvents();

            Count++;

            ListViewItem lvItem = null;
            string PairKey = string.Join( "____", Count.ToString(), HyperlinkIn.GetLinkGuid().ToString() );
            string DoFollow = "No Follow";

            if ( HyperlinkIn.GetDoFollow() )
            {
              DoFollow = "Follow";
            }

            if ( TargetListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = Count.ToString();
                lvItem.SubItems[ 1 ].Text = HyperlinkIn.GetHyperlinkType().ToString();
                lvItem.SubItems[ 2 ].Text = HyperlinkIn.GetSourceUrl();
                lvItem.SubItems[ 3 ].Text = HyperlinkIn.GetTargetUrl();
                lvItem.SubItems[ 4 ].Text = DoFollow;
                lvItem.SubItems[ 5 ].Text = HyperlinkIn.GetLinkText();
                lvItem.SubItems[ 6 ].Text = HyperlinkIn.GetAltText();

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewHyperlinksIn 1: {0}", ex.Message ) );
              }

            }
            else
            {

              try
              {

                lvItem = new ListViewItem( PairKey );
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Name = PairKey;

                lvItem.SubItems[ 0 ].Text = Count.ToString();
                lvItem.SubItems.Add( HyperlinkIn.GetHyperlinkType().ToString() );
                lvItem.SubItems.Add( HyperlinkIn.GetSourceUrl() );
                lvItem.SubItems.Add( HyperlinkIn.GetTargetUrl() );
                lvItem.SubItems.Add( DoFollow );
                lvItem.SubItems.Add( HyperlinkIn.GetLinkText() );
                lvItem.SubItems.Add( HyperlinkIn.GetAltText() );

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewHyperlinksIn 2: {0}", ex.Message ) );
              }

            }

            if ( lvItem != null )
            {

              lvItem.ForeColor = Color.Blue;

              if ( AllowedHosts.IsAllowedFromUrl( HyperlinkIn.GetSourceUrl() ) )
              {
                lvItem.SubItems[ 2 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
              }

              if ( AllowedHosts.IsAllowedFromUrl( HyperlinkIn.GetTargetUrl() ) )
              {
                lvItem.SubItems[ 3 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
              }

              if ( AllowedHosts.IsAllowedFromUrl( HyperlinkIn.GetSourceUrl() ) )
              {
                if ( HyperlinkIn.GetDoFollow() )
                {
                  lvItem.SubItems[ 4 ].ForeColor = Color.Green;
                }
                else
                {
                  lvItem.SubItems[ 4 ].ForeColor = Color.Red;
                }
              }
              else
              {
                lvItem.SubItems[ 4 ].ForeColor = Color.Gray;
              }

            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Hyperlinks Out ********************************************************/

    private void RenderListViewHyperlinksOut ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewHyperlinksOut;
      MacroscopeHyperlinksOut HyperlinksOut = msDoc.GetHyperlinksOut();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      List<ListViewItem> ListViewItems = null;
      int Count = 0;

      if ( HyperlinksOut != null )
      {
        ListViewItems = new List<ListViewItem>( HyperlinksOut.Count() );
      }
      else
      {
        TargetListView.BeginUpdate();
        TargetListView.Items.Clear();
        TargetListView.EndUpdate();
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        lock ( HyperlinksOut )
        {

          foreach ( MacroscopeHyperlinkOut HyperlinkOut in HyperlinksOut.IterateLinks() )
          {

            Application.DoEvents();

            Count++;

            ListViewItem lvItem = null;
            string PairKey = string.Join( "____", Count.ToString(), HyperlinkOut.GetGuid().ToString() );
            string DoFollow = "No Follow";

            if ( HyperlinkOut.GetDoFollow() )
            {
              DoFollow = "Follow";
            }

            if ( TargetListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = Count.ToString();
                lvItem.SubItems[ 1 ].Text = HyperlinkOut.GetHyperlinkType().ToString();
                lvItem.SubItems[ 2 ].Text = msDoc.GetUrl();
                lvItem.SubItems[ 3 ].Text = HyperlinkOut.GetTargetUrl();
                lvItem.SubItems[ 4 ].Text = DoFollow;
                lvItem.SubItems[ 5 ].Text = HyperlinkOut.GetLinkTarget();
                lvItem.SubItems[ 6 ].Text = HyperlinkOut.GetLinkText();
                lvItem.SubItems[ 7 ].Text = HyperlinkOut.GetAltText();
                lvItem.SubItems[ 8 ].Text = HyperlinkOut.GetRawTargetUrl();

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewHyperlinksOut 1: {0}", ex.Message ) );
              }

            }
            else
            {

              try
              {

                lvItem = new ListViewItem( PairKey );
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Name = PairKey;

                lvItem.SubItems[ 0 ].Text = Count.ToString();
                lvItem.SubItems.Add( HyperlinkOut.GetHyperlinkType().ToString() );
                lvItem.SubItems.Add( msDoc.GetUrl() );
                lvItem.SubItems.Add( HyperlinkOut.GetTargetUrl() );
                lvItem.SubItems.Add( DoFollow );
                lvItem.SubItems.Add( HyperlinkOut.GetLinkTarget() );
                lvItem.SubItems.Add( HyperlinkOut.GetLinkText() );
                lvItem.SubItems.Add( HyperlinkOut.GetAltText() );
                lvItem.SubItems.Add( HyperlinkOut.GetRawTargetUrl() );

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewHyperlinksOut 2: {0}", ex.Message ) );
              }

            }

            if ( lvItem != null )
            {

              lvItem.ForeColor = Color.Blue;

              if ( AllowedHosts.IsAllowedFromUrl( msDoc.GetUrl() ) )
              {
                lvItem.SubItems[ 2 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
              }

              if ( AllowedHosts.IsAllowedFromUrl( HyperlinkOut.GetTargetUrl() ) )
              {
                lvItem.SubItems[ 3 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
              }

              if ( AllowedHosts.IsAllowedFromUrl( HyperlinkOut.GetTargetUrl() ) )
              {
                if ( HyperlinkOut.GetDoFollow() )
                {
                  lvItem.SubItems[ 4 ].ForeColor = Color.Green;
                }
                else
                {
                  lvItem.SubItems[ 4 ].ForeColor = Color.Red;
                }
              }
              else
              {
                lvItem.SubItems[ 4 ].ForeColor = Color.Gray;
              }

            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Insecure Links Out ****************************************************/

    private void RenderListViewInsecureLinks ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewInsecureLinks;
      List<string> DocList = msDoc.GetInsecureLinks();
      List<ListViewItem> ListViewItems = null;

      if ( DocList != null )
      {
        ListViewItems = new List<ListViewItem>( DocList.Count );
      }
      else
      {
        TargetListView.BeginUpdate();
        TargetListView.Items.Clear();
        TargetListView.EndUpdate();
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        if ( DocList.Count > 0 )
        {

          for ( int i = 0 ; i < DocList.Count ; i++ )
          {

            ListViewItem lvItem = null;
            string Url = DocList[ i ];
            string PairKey = Url;

            if ( TargetListView.Items.ContainsKey( Url ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = Url;

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewInsecureLinks 1: {0}", ex.Message ) );
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

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewInsecureLinks 2: {0}", ex.Message ) );
              }

            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Stylesheets ***********************************************************/

    private void RenderListViewStylesheets ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewStylesheets;
      MacroscopeLinkList LinkList = msDoc.GetOutlinks();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      List<ListViewItem> ListViewItems = null;
      int iCount = 1;

      if ( LinkList != null )
      {
        ListViewItems = new List<ListViewItem>( LinkList.Count() );
      }
      else
      {
        TargetListView.BeginUpdate();
        TargetListView.Items.Clear();
        TargetListView.EndUpdate();
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( MacroscopeLink Link in LinkList.IterateLinks() )
        {

          Application.DoEvents();

          string Url = Link.GetTargetUrl();
          string PairKey = Url;
          ListViewItem lvItem = null;
          MacroscopeConstants.InOutLinkType LinkType = Link.GetLinkType();

          if ( LinkType == MacroscopeConstants.InOutLinkType.STYLESHEET )
          {

            if ( TargetListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems[ 1 ].Text = Link.GetTargetUrl();

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewStylesheets 1: {0}", ex.Message ) );
              }

            }
            else
            {

              try
              {

                lvItem = new ListViewItem( PairKey );
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Name = PairKey;

                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems.Add( Link.GetTargetUrl() );

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewStylesheets 2: {0}", ex.Message ) );
              }

            }

            iCount++;

          }

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( AllowedHosts.IsInternalUrl( Link.GetTargetUrl() ) )
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Javascripts ***********************************************************/

    private void RenderListViewJavascripts ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewJavascripts;
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      MacroscopeLinkList LinkList = msDoc.GetOutlinks();
      List<ListViewItem> ListViewItems = null;
      int iCount = 1;

      if ( LinkList != null )
      {
        ListViewItems = new List<ListViewItem>( LinkList.Count() );
      }
      else
      {
        TargetListView.BeginUpdate();
        TargetListView.Items.Clear();
        TargetListView.EndUpdate();
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( MacroscopeLink Link in LinkList.IterateLinks() )
        {

          Application.DoEvents();

          ListViewItem lvItem = null;
          string Url = Link.GetTargetUrl();
          string PairKey = Url;
          MacroscopeConstants.InOutLinkType LinkType = Link.GetLinkType();

          if ( LinkType == MacroscopeConstants.InOutLinkType.SCRIPT )
          {

            if ( TargetListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems[ 1 ].Text = Link.GetTargetUrl();

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewJavascripts 1: {0}", ex.Message ) );
              }

            }
            else
            {

              try
              {

                lvItem = new ListViewItem( PairKey );
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Name = PairKey;

                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems.Add( Link.GetTargetUrl() );

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewJavascripts 2: {0}", ex.Message ) );
              }

            }

            iCount++;

          }

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( AllowedHosts.IsInternalUrl( Link.GetTargetUrl() ) )
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Images ****************************************************************/

    private void RenderListViewImages ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewImages;
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      MacroscopeLinkList LinkList = msDoc.GetOutlinks();
      List<ListViewItem> ListViewItems = null;
      int iCount = 1;

      if ( LinkList != null )
      {
        ListViewItems = new List<ListViewItem>( LinkList.Count() );
      }
      else
      {
        lock ( this.RenderListViewLock )
        {
          TargetListView.BeginUpdate();
          TargetListView.Items.Clear();
          TargetListView.EndUpdate();
        }
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( MacroscopeLink Link in LinkList.IterateLinks() )
        {

          Application.DoEvents();

          ListViewItem lvItem = null;
          string Url = Link.GetTargetUrl();
          string PairKey = Url;
          MacroscopeConstants.InOutLinkType LinkType = Link.GetLinkType();

          if ( LinkType == MacroscopeConstants.InOutLinkType.IMAGE )
          {

            if ( TargetListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems[ 1 ].Text = Link.GetTargetUrl();
                lvItem.SubItems[ 2 ].Text = Link.GetTitle();
                lvItem.SubItems[ 3 ].Text = Link.GetAltText();

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewImages 1: {0}", ex.Message ) );
              }

            }
            else
            {

              try
              {

                lvItem = new ListViewItem( PairKey );
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Name = PairKey;

                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems.Add( Link.GetTargetUrl() );
                lvItem.SubItems.Add( Link.GetTitle() );
                lvItem.SubItems.Add( Link.GetAltText() );

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewImages 2: {0}", ex.Message ) );
              }

            }

            iCount++;

          }

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( AllowedHosts.IsInternalUrl( Link.GetTargetUrl() ) )
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Audios ****************************************************************/

    private void RenderListViewAudios ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewAudios;
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      MacroscopeLinkList LinkList = msDoc.GetOutlinks();
      List<ListViewItem> ListViewItems = null;
      int iCount = 1;

      if ( LinkList != null )
      {
        ListViewItems = new List<ListViewItem>( LinkList.Count() );
      }
      else
      {
        lock ( this.RenderListViewLock )
        {
          TargetListView.BeginUpdate();
          TargetListView.Items.Clear();
          TargetListView.EndUpdate();
        }
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( MacroscopeLink Link in LinkList.IterateLinks() )
        {

          Application.DoEvents();

          ListViewItem lvItem = null;
          string Url = Link.GetTargetUrl();
          string PairKey = Url;
          MacroscopeConstants.InOutLinkType LinkType = Link.GetLinkType();

          if ( LinkType == MacroscopeConstants.InOutLinkType.AUDIO )
          {

            if ( TargetListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems[ 1 ].Text = Link.GetTargetUrl();

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewAudios 1: {0}", ex.Message ) );
              }

            }
            else
            {

              try
              {

                lvItem = new ListViewItem( PairKey );
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Name = PairKey;

                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems.Add( Link.GetTargetUrl() );

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewAudios 2: {0}", ex.Message ) );
              }

            }

            iCount++;

          }

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( AllowedHosts.IsInternalUrl( Link.GetTargetUrl() ) )
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Videos ****************************************************************/

    private void RenderListViewVideos ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewVideos;
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      MacroscopeLinkList LinkList = msDoc.GetOutlinks();
      List<ListViewItem> ListViewItems = null;
      int iCount = 1;

      if ( LinkList != null )
      {
        ListViewItems = new List<ListViewItem>( LinkList.Count() );
      }
      else
      {
        lock ( this.RenderListViewLock )
        {
          TargetListView.BeginUpdate();
          TargetListView.Items.Clear();
          TargetListView.EndUpdate();
        }
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( MacroscopeLink Link in LinkList.IterateLinks() )
        {

          Application.DoEvents();

          ListViewItem lvItem = null;
          string Url = Link.GetTargetUrl();
          string PairKey = Url;
          MacroscopeConstants.InOutLinkType LinkType = Link.GetLinkType();

          if ( LinkType == MacroscopeConstants.InOutLinkType.VIDEO )
          {

            if ( TargetListView.Items.ContainsKey( PairKey ) )
            {

              try
              {

                lvItem = TargetListView.Items[ PairKey ];
                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems[ 1 ].Text = Link.GetTargetUrl();

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewVideos 1: {0}", ex.Message ) );
              }

            }
            else
            {

              try
              {

                lvItem = new ListViewItem( PairKey );
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Name = PairKey;

                lvItem.SubItems[ 0 ].Text = iCount.ToString();
                lvItem.SubItems.Add( Link.GetTargetUrl() );

                ListViewItems.Add( lvItem );

              }
              catch ( Exception ex )
              {
                DebugMsg( string.Format( "RenderListViewVideos 2: {0}", ex.Message ) );
              }

            }

            iCount++;

          }

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( AllowedHosts.IsInternalUrl( Link.GetTargetUrl() ) )
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Keyword Analysis ******************************************************/

    private void RenderListViewKeywordAnalysis ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewKeywordAnalysis;
      Dictionary<string, int> DicTerms = msDoc.GetDeepKeywordAnalysisAsDictonary( Words: 1 );
      List<ListViewItem> ListViewItems = null;

      if ( DicTerms != null )
      {
        ListViewItems = new List<ListViewItem>( DicTerms.Count );
      }
      else
      {
        lock ( this.RenderListViewLock )
        {
          TargetListView.BeginUpdate();
          TargetListView.Items.Clear();
          TargetListView.EndUpdate();
        }
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( string Term in DicTerms.Keys )
        {

          Application.DoEvents();

          ListViewItem lvItem = null;
          string PairKey = Term;

          if ( TargetListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = TargetListView.Items[ PairKey ];
              lvItem.SubItems[ 0 ].Text = DicTerms[ Term ].ToString();
              lvItem.SubItems[ 1 ].Text = Term;

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewKeywordAnalysis 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem( PairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = PairKey;

              lvItem.SubItems[ 0 ].Text = DicTerms[ Term ].ToString();
              lvItem.SubItems.Add( Term );

              ListViewItems.Add( lvItem );

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewKeywordAnalysis 2: {0}", ex.Message ) );
            }

          }

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Remarks ***************************************************************/

    private void RenderListViewRemarks ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewRemarks;
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      List<ListViewItem> ListViewItems = new List<ListViewItem>();
      int Count = 1;

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        foreach ( string Remark in msDoc.IterateRemarks() )
        {

          Application.DoEvents();

          ListViewItem lvItem = null;
          string Url = msDoc.GetUrl();
          string KeyPair = string.Join( "::", Count, Url );



          if ( TargetListView.Items.ContainsKey( KeyPair ) )
          {

            try
            {

              lvItem = TargetListView.Items[ KeyPair ];
              lvItem.SubItems[ 0 ].Text = Count.ToString();
              lvItem.SubItems[ 1 ].Text = Url;
              lvItem.SubItems[ 2 ].Text = Remark;

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewJavascripts 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem( KeyPair );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = KeyPair;

              lvItem.SubItems[ 0 ].Text = Count.ToString();
              lvItem.SubItems.Add( Url );
              lvItem.SubItems.Add( Remark );

              ListViewItems.Add( lvItem );

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewJavascripts 2: {0}", ex.Message ) );
            }

          }

          Count++;

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( AllowedHosts.IsInternalUrl( Url ) )
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Document Text *************************************************************/

    private void RenderTextBoxDocumentTextRaw ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      TextBox BodyText = this.textBoxDocumentTextRaw;
      string DocumentText = "";

      if ( msDoc != null )
      {
        DocumentText = msDoc.GetDocumentTextRaw();
      }

      lock ( this.RenderListViewLock )
      {
        if ( !string.IsNullOrEmpty( DocumentText ) )
        {
          BodyText.Text = DocumentText;
        }
        else
        {
          BodyText.Text = "";
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    private void RenderTextBoxDocumentTextCleaned ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      TextBox BodyText = this.textBoxDocumentTextCleaned;
      string DocumentText = "";

      if ( msDoc != null )
      {
        DocumentText = msDoc.GetDocumentTextCleaned();
      }

      lock ( this.RenderListViewLock )
      {

        if ( !string.IsNullOrEmpty( DocumentText ) )
        {
          BodyText.Text = DocumentText;
        }
        else
        {
          BodyText.Text = "";
        }

      }

    }

    /** Body Text *************************************************************/

    private void RenderTextBoxBodyTextRaw ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      TextBox BodyText = this.textBoxBodyTextRaw;
      string DocumentText = "";

      if ( msDoc != null )
      {
        DocumentText = msDoc.GetBodyTextRaw();
      }

      lock ( this.RenderListViewLock )
      {
        if ( !string.IsNullOrEmpty( DocumentText ) )
        {
          BodyText.Text = DocumentText;
        }
        else
        {
          BodyText.Text = "";
        }
      }

    }

    /** Custom Filters ********************************************************/

    private void RenderListViewCustomFilters ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView TargetListView = this.listViewCustomFilters;
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      MacroscopeCustomFilters CustomFilter = JobMaster.GetCustomFilter();
      List<ListViewItem> ListViewItems = new List<ListViewItem>();
      int Count = 1;

      if ( !CustomFilter.CanApplyCustomFiltersToDocument( msDoc: msDoc ) )
      {
        return;
      }

      lock ( this.RenderListViewLock )
      {

        TargetListView.BeginUpdate();

        TargetListView.Items.Clear();

        for ( int Slot = 0 ; Slot < CustomFilter.GetSize() ; Slot++ )
        {

          Application.DoEvents();

          KeyValuePair<string, MacroscopeConstants.Contains> CustomFilterPair = CustomFilter.GetPattern( Slot: Slot );

          ListViewItem lvItem = null;
          string Url = msDoc.GetUrl();
          string KeyPair = string.Join( "::", Count, Url );

          string CustomFilterText = CustomFilterPair.Key;

          KeyValuePair<string, MacroscopeConstants.TextPresence> Pair = msDoc.GetCustomFilteredItem( Text: CustomFilterText );

          string CustomFilterItemValue;

          if ( ( Pair.Key != null ) && ( Pair.Value != MacroscopeConstants.TextPresence.UNDEFINED ) )
          {
            CustomFilterItemValue = Pair.Value.ToString();
          }
          else
          {
            CustomFilterItemValue = "";
          }

          if ( TargetListView.Items.ContainsKey( KeyPair ) )
          {

            try
            {

              lvItem = TargetListView.Items[ KeyPair ];
              lvItem.SubItems[ 0 ].Text = Count.ToString();
              lvItem.SubItems[ 1 ].Text = CustomFilterText;
              lvItem.SubItems[ 2 ].Text = CustomFilterPair.Value.ToString();
              lvItem.SubItems[ 3 ].Text = CustomFilterItemValue;

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewCustomFilters 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem( KeyPair );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = KeyPair;

              lvItem.SubItems[ 0 ].Text = Count.ToString();
              lvItem.SubItems.Add( CustomFilterText );
              lvItem.SubItems.Add( CustomFilterPair.Value.ToString() );
              lvItem.SubItems.Add( CustomFilterItemValue );

              ListViewItems.Add( lvItem );

            }
            catch ( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewCustomFilters 2: {0}", ex.Message ) );
            }

          }

          Count++;

          if ( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            if ( AllowedHosts.IsInternalUrl( Url ) )
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

          }

        }

        TargetListView.Items.AddRange( ListViewItems.ToArray() );

        TargetListView.EndUpdate();

      }

    }

    /** Document Preview ******************************************************/

    private void RenderDocumentPreview ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      if ( msDoc.GetIsImage() )
      {
        this.splitContainerDocumentDetails.Panel2Collapsed = false;
        this.RenderImagePreview( JobMaster: JobMaster, msDoc: msDoc );
      }
      else
      {
        this.splitContainerDocumentDetails.Panel2Collapsed = true;
        ClearDocumentPreviewListView();
      }

    }

    /** Clear Document Preview ListView ***************************************/

    private void ClearDocumentPreviewListView ()
    {
      ListView TargetListView = this.listViewDocInfo;
      TargetListView.Clear();
      this.pictureBoxDocumentDetailsImage.Image = null;
    }

    /** Image Preview *********************************************************/

    private async void RenderImagePreview ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {
      if ( msDoc.GetIsImage() )
      {
        Image LoadedImage = await this.HttpImageLoader.LoadImageFromUri( JobMaster: JobMaster, TargetUri: msDoc.GetUri() );
        if ( LoadedImage != null )
        {
          this.pictureBoxDocumentDetailsImage.Image = LoadedImage;
          this.RenderImagePreviewListView( msDoc, this.pictureBoxDocumentDetailsImage.Image );
        }
      }
      else
      {
        try
        {
          this.pictureBoxDocumentDetailsImage.Image = null;
        }
        catch ( Exception ex )
        {
          MessageBox.Show( ex.Message );
        }
      }
    }

    private void RenderImagePreviewListView ( MacroscopeDocument msDoc, Image iImage )
    {

      ListView TargetListView = this.listViewDocInfo;

      lock ( this.RenderListViewLock )
      {

        TargetListView.Clear();

        TargetListView.Columns.Add( "Property" );
        TargetListView.Columns.Add( "Value" );

        {
          ListViewItem lvItem = new ListViewItem( "Format" );
          lvItem.SubItems[ 0 ].Text = "PixelFormat";
          lvItem.SubItems.Add( msDoc.GetMimeType() );
          TargetListView.Items.Add( lvItem );
        }

        {
          ListViewItem lvItem = new ListViewItem( "WIDTH" );
          lvItem.SubItems[ 0 ].Text = "Width";
          lvItem.SubItems.Add( iImage.Width.ToString() );
          TargetListView.Items.Add( lvItem );
        }

        {
          ListViewItem lvItem = new ListViewItem( "HEIGHT" );
          lvItem.SubItems[ 0 ].Text = "Height";
          lvItem.SubItems.Add( iImage.Height.ToString() );
          TargetListView.Items.Add( lvItem );
        }

        TargetListView.Columns[ 0 ].Width = 150;
        TargetListView.Columns[ 1 ].Width = 150;

      }

    }

    /**************************************************************************/
  }

}
