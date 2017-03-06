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
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  public partial class MacroscopeDocumentDetails : MacroscopeUserControl
  {

    /**************************************************************************/

    private MacroscopeUrlLoader UrlLoader;

    private MacroscopeColumnSorter lvColumnSorter;
        
    /**************************************************************************/

    public MacroscopeDocumentDetails ()
    {

      // The InitializeComponent() call is required for Windows Forms designer support.
      InitializeComponent();

      // Control Properties
      this.listViewDocumentInfo.Dock = DockStyle.Fill;
      this.textBoxHttpHeaders.Dock = DockStyle.Fill;
      this.listViewHrefLang.Dock = DockStyle.Fill;
      this.listViewLinksIn.Dock = DockStyle.Fill;
      this.listViewLinksOut.Dock = DockStyle.Fill;
      this.listViewInsecureLinks.Dock = DockStyle.Fill;
      this.listViewImages.Dock = DockStyle.Fill;
      this.listViewStylesheets.Dock = DockStyle.Fill;
      this.listViewJavascripts.Dock = DockStyle.Fill;
      this.listViewAudios.Dock = DockStyle.Fill;
      this.listViewVideos.Dock = DockStyle.Fill;
      this.listViewKeywordAnalysis.Dock = DockStyle.Fill;

      this.UrlLoader = new MacroscopeUrlLoader ();
      this.listViewDocInfo.Dock = DockStyle.Fill;

      // ListView Sorters
      this.lvColumnSorter = new MacroscopeColumnSorter ();

      this.listViewHrefLang.ListViewItemSorter = lvColumnSorter;
      this.listViewLinksIn.ListViewItemSorter = lvColumnSorter;
      this.listViewLinksOut.ListViewItemSorter = lvColumnSorter;
      this.listViewInsecureLinks.ListViewItemSorter = lvColumnSorter;
      this.listViewImages.ListViewItemSorter = lvColumnSorter;
      this.listViewStylesheets.ListViewItemSorter = lvColumnSorter;
      this.listViewJavascripts.ListViewItemSorter = lvColumnSorter;
      this.listViewAudios.ListViewItemSorter = lvColumnSorter;
      this.listViewVideos.ListViewItemSorter = lvColumnSorter;
      this.listViewKeywordAnalysis.ListViewItemSorter = lvColumnSorter;

      this.listViewHrefLang.ColumnClick += this.CallbackColumnClick;
      this.listViewLinksIn.ColumnClick += this.CallbackColumnClick;
      this.listViewLinksOut.ColumnClick += this.CallbackColumnClick;
      this.listViewInsecureLinks.ColumnClick += this.CallbackColumnClick;
      this.listViewImages.ColumnClick += this.CallbackColumnClick;
      this.listViewStylesheets.ColumnClick += this.CallbackColumnClick;
      this.listViewJavascripts.ColumnClick += this.CallbackColumnClick;
      this.listViewAudios.ColumnClick += this.CallbackColumnClick;
      this.listViewVideos.ColumnClick += this.CallbackColumnClick;
      this.listViewKeywordAnalysis.ColumnClick += this.CallbackColumnClick;

    }

    /**************************************************************************/

    private void MacroscopeDocumentDetailsLoad ( object sender, EventArgs e )
    {
    }

    /**************************************************************************/

    private void CallbackColumnClick ( object sender, ColumnClickEventArgs e )
    {

      ListView lvListView = sender as ListView;

      if( e.Column == lvColumnSorter.SortColumn )
      {
        if( lvColumnSorter.Order == SortOrder.Ascending )
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

      lvListView.Sort();

    }

    /**************************************************************************/
    
    private void CallbackDocumentDetailsContextMenuStripCopyRowsClick ( object sender, EventArgs e )
    {
      this.CopyListViewRowsTextToClipboard( this.listViewDocumentInfo );
    }

    private void CallbackDocumentDetailsContextMenuStripCopyValuesClick ( object sender, EventArgs e )
    {
      this.CopyListViewValuesTextToClipboard( this.listViewDocumentInfo );
    }
    
    /**************************************************************************/

    public void ClearData ()
    {

      this.listViewDocumentInfo.Items.Clear();
      this.textBoxHttpHeaders.Text = "";
      this.listViewHrefLang.Items.Clear();
      this.listViewLinksIn.Items.Clear();
      this.listViewLinksOut.Items.Clear();
      this.listViewInsecureLinks.Items.Clear();
      this.listViewImages.Items.Clear();
      this.listViewStylesheets.Items.Clear();
      this.listViewJavascripts.Items.Clear();
      this.listViewAudios.Items.Clear();
      this.listViewVideos.Items.Clear();
      this.listViewKeywordAnalysis.Items.Clear();

      this.pictureBoxDocumentDetailsImage.Image = null;
      this.listViewDocInfo.Columns.Clear();

    }

    /**************************************************************************/

    public Boolean UpdateDisplay ( MacroscopeJobMaster JobMaster, string sUrl )
    {

      // TODO: This blows up if the page is from a redirect. Probably need to use the original URL

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeDocument msDoc = DocCollection.GetDocument( sUrl );

      if( msDoc != null )
      {

        if( this.InvokeRequired )
        {
          this.Invoke(
            new MethodInvoker (
              delegate
              {
                this.UpdateDocumentDetailsDisplay( JobMaster, msDoc );
              }
            )
          );
        }
        else
        {
          this.UpdateDocumentDetailsDisplay( JobMaster, msDoc );
        }

        return( true );

      }
      else
      {

        return( false );

      }

    }

    /**************************************************************************/

    private async void UpdateDocumentDetailsDisplay ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      Cursor.Current = Cursors.WaitCursor;

      int [] count = new int[12];
      int count_i = 0;
      
      count[ count_i++ ] = await this.RenderDocumentDetails( msDoc );

      count[ count_i++ ] = await this.RenderDocumentHttpHeaders( msDoc );
      
      count[ count_i++ ] = await this.RenderDocumentHrefLang( msDoc, JobMaster.GetLocales(), JobMaster.GetDocCollection() );
      
      count[ count_i++ ] = await this.RenderListViewHyperlinksIn( msDoc );

      count[ count_i++ ] = await this.RenderListViewHyperlinksOut( msDoc );

      count[ count_i++ ] = await this.RenderListViewInsecureLinks( msDoc );
      
      count[ count_i++ ] = await this.RenderListViewStylesheets( JobMaster, msDoc );
      
      count[ count_i++ ] = await this.RenderListViewJavascripts( JobMaster, msDoc );
            
      count[ count_i++ ] = await this.RenderListViewImages( JobMaster, msDoc );
      
      count[ count_i++ ] = await this.RenderListViewAudios( JobMaster, msDoc );
            
      count[ count_i++ ] = await this.RenderListViewVideos( JobMaster, msDoc );
            
      if( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
      {
        count[ count_i++ ] = await this.RenderListViewKeywordAnalysis( JobMaster, msDoc );
      }
            
      this.RenderDocumentPreview( msDoc );
     
      Cursor.Current = Cursors.Default;

    }

    /**************************************************************************/

    private async Task<int> RenderDocumentDetails ( MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewDocumentInfo;
      int count = 0;
      
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      List<KeyValuePair<string,string>> lItems = msDoc.DetailDocumentDetails();

      for( int i = 0 ; i < lItems.Count ; i++ )
      {

        KeyValuePair<string,string> kvItem = lItems[ i ];
        count++;
        
        try
        {
          ListViewItem lvItem = new ListViewItem ( kvItem.Key );
          lvItem.Name = kvItem.Key;
          lvItem.SubItems.Add( kvItem.Value );
          lvListView.Items.Add( lvItem );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "RenderDocumentDetails: {0}", ex.Message ) );
        }

      }

      lvListView.EndUpdate();
      
      return( count );
      
    }

    /**************************************************************************/

    private async Task<int> RenderDocumentHttpHeaders ( MacroscopeDocument msDoc )
    {
      int count = 0;
      this.textBoxHttpHeaders.Text = string.Join(
        "",
        msDoc.GetHttpStatusLineAsText(),
        msDoc.GetHttpHeadersAsText()
      );
      count++;
      return( count );
    }

    /**************************************************************************/

    private async Task<int> RenderDocumentHrefLang ( MacroscopeDocument msDoc, Dictionary<string,string> htLocales, MacroscopeDocumentCollection DocCollection )
    {

      ListView lvListView = this.listViewHrefLang;
      int count = 0;
      
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();
      lvListView.Columns.Clear();

      {
        lvListView.Columns.Add( "URL", "URL" );
        lvListView.Columns.Add( "Site Locale", "Site Locale" );
        lvListView.Columns.Add( "Title", "Title" );
      }

      string sKeyUrl = msDoc.GetUrl();

      if( msDoc.GetIsHtml() )
      {

        Dictionary<string,MacroscopeHrefLang> htHrefLangs = msDoc.GetHrefLangs();

        if( htHrefLangs != null )
        {

          {

            ListViewItem lvItem = new ListViewItem ( sKeyUrl );

            lvItem.Name = sKeyUrl;

            lvItem.SubItems.Add( "" );
            lvItem.SubItems.Add( "" );
            lvItem.SubItems.Add( "" );

            lvItem.SubItems[ 0 ].Text = msDoc.GetUrl();
            lvItem.SubItems[ 1 ].Text = msDoc.GetLocale();
            lvItem.SubItems[ 2 ].Text = msDoc.GetTitle();

            lvListView.Items.Add( lvItem );

          }

          foreach( string sLocale in htLocales.Keys )
          {


            if( sLocale != null )
            {

              if( sLocale == msDoc.GetLocale() )
              {
                continue;
              }

              string sHrefLangUrl = null;
              string sTitle = "";
              count++;
              ListViewItem lvItem = new ListViewItem ( sLocale );

              lvItem.Name = sLocale;

              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );

              if( htHrefLangs.ContainsKey( sLocale ) )
              {

                MacroscopeHrefLang msHrefLang = htHrefLangs[ sLocale ];

                if( msHrefLang != null )
                {

                  sHrefLangUrl = msHrefLang.GetUrl();

                  if( DocCollection.DocumentExists( sHrefLangUrl ) )
                  {
                    sTitle = DocCollection.GetDocument( sHrefLangUrl ).GetTitle();
                  }

                }

              }

              lvItem.SubItems[ 1 ].Text = sLocale;
              lvItem.SubItems[ 2 ].Text = sTitle;

              if( sHrefLangUrl != null )
              {
                lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
                lvItem.SubItems[ 0 ].Text = sHrefLangUrl;
              }
              else
              {
                lvItem.SubItems[ 0 ].ForeColor = Color.Red;
                lvItem.SubItems[ 0 ].Text = "MISSING";

              }

              lvListView.Items.Add( lvItem );

            }

          }

        }

        lvListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );

        lvListView.Columns[ "URL" ].Width = 300;
        lvListView.Columns[ "Site Locale" ].Width = 100;
        lvListView.Columns[ "Title" ].Width = 300;

      }

      lvListView.EndUpdate();
            
      return( count );
    }

    /** Hyperlinks In *********************************************************/

    private async Task<int> RenderListViewHyperlinksIn ( MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewLinksIn;
      MacroscopeHyperlinksIn HyperlinksIn = msDoc.GetHyperlinksIn();
      int count = 0;

      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      if( HyperlinksIn != null )
      {
        foreach( MacroscopeHyperlinkIn HyperlinkIn in HyperlinksIn.IterateLinks(  ) )
        {

          ListViewItem lvItem = null;
          string sPairKey = HyperlinkIn.GetLinkGuid().ToString();
          count++;
          
          if( lvListView.Items.ContainsKey( sPairKey ) )
          {

            try
            {

              lvItem = lvListView.Items[ sPairKey ];
              lvItem.SubItems[ 0 ].Text = HyperlinkIn.GetHyperlinkType().ToString();
              lvItem.SubItems[ 1 ].Text = HyperlinkIn.GetUrlOrigin();
              lvItem.SubItems[ 2 ].Text = HyperlinkIn.GetUrlTarget();
              lvItem.SubItems[ 3 ].Text = HyperlinkIn.GetLinkText();
              lvItem.SubItems[ 4 ].Text = HyperlinkIn.GetAltText();

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewHyperlinksIn 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( sPairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sPairKey;

              lvItem.SubItems[ 0 ].Text = HyperlinkIn.GetHyperlinkType().ToString();
              lvItem.SubItems.Add( HyperlinkIn.GetUrlOrigin() );
              lvItem.SubItems.Add( HyperlinkIn.GetUrlTarget() );
              lvItem.SubItems.Add( HyperlinkIn.GetLinkText() );
              lvItem.SubItems.Add( HyperlinkIn.GetAltText() );

              lvListView.Items.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewHyperlinksIn 2: {0}", ex.Message ) );
            }

          }

        }

      }

      lvListView.EndUpdate();
          
      return( count );
      
    }

    /** Hyperlinks Out ********************************************************/

    private async Task<int> RenderListViewHyperlinksOut ( MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewLinksOut;
      MacroscopeHyperlinksOut HyperlinksOut = msDoc.GetHyperlinksOut();
      int count = 0;
      
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      lock( HyperlinksOut )
      {

        foreach( MacroscopeHyperlinkOut HyperlinkOut in HyperlinksOut.IterateLinks(  ) )
        {

          ListViewItem lvItem = null;
          string sKey = HyperlinkOut.GetGuid();
          count++;
            
          if( lvListView.Items.ContainsKey( sKey ) )
          {

            try
            {

              lvItem = lvListView.Items[ sKey ];
              lvItem.SubItems[ 0 ].Text = HyperlinkOut.GetHyperlinkType().ToString();
              lvItem.SubItems[ 1 ].Text = msDoc.GetUrl();
              lvItem.SubItems[ 2 ].Text = HyperlinkOut.GetUrlTarget();
              lvItem.SubItems[ 3 ].Text = HyperlinkOut.GetLinkText();
              lvItem.SubItems[ 4 ].Text = HyperlinkOut.GetAltText();
              lvItem.SubItems[ 5 ].Text = HyperlinkOut.GetFollow().ToString();

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewHyperlinksOut 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( sKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sKey;

              lvItem.SubItems[ 0 ].Text = HyperlinkOut.GetHyperlinkType().ToString();
              lvItem.SubItems.Add( msDoc.GetUrl() );
              lvItem.SubItems.Add( HyperlinkOut.GetUrlTarget() );
              lvItem.SubItems.Add( HyperlinkOut.GetLinkText() );
              lvItem.SubItems.Add( HyperlinkOut.GetAltText() );
              lvItem.SubItems.Add( HyperlinkOut.GetFollow().ToString() );

              lvListView.Items.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewHyperlinksOut 2: {0}", ex.Message ) );
            }

          }

        }

      }

      lvListView.EndUpdate();
         
      return( count );
    }

    /** Insecure Links Out ****************************************************/

    private async Task<int> RenderListViewInsecureLinks ( MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewInsecureLinks;
      List<string> DocList = msDoc.GetInsecureLinks();
      int count = 0;
      
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      if( DocList.Count > 0 )
      {
      
        for( int i = 0 ; i < DocList.Count ; i++ )
        {

          ListViewItem lvItem = null;
          string sUrl = DocList[ i ];
          string sPairKey = sUrl;
          count++;

          if( lvListView.Items.ContainsKey( sUrl ) )
          {

            try
            {

              lvItem = lvListView.Items[ sPairKey ];
              lvItem.SubItems[ 0 ].Text = sUrl;

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewInsecureLinks 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( sPairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sPairKey;

              lvItem.SubItems[ 0 ].Text = sUrl;

              lvListView.Items.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewInsecureLinks 2: {0}", ex.Message ) );
            }

          }

        }

      }
      
      lvListView.EndUpdate();
            
      return( count );
      
    }

    /** Stylesheets ***********************************************************/

    private async Task<int> RenderListViewStylesheets ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewStylesheets;
      Dictionary<string,MacroscopeOutlink> DicOutlinks = msDoc.GetOutlinks();
      int iCount = 1;
      int count = 0;
            
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      foreach( string sUrl in DicOutlinks.Keys )
      {

        string sKeyPair = sUrl;
        ListViewItem lvItem = null;
        MacroscopeConstants.OutlinkType LinkType = DicOutlinks[ sUrl ].GetLinkType();
        count++;
        
        if( LinkType == MacroscopeConstants.OutlinkType.STYLESHEET )
        {

          if( lvListView.Items.ContainsKey( sKeyPair ) )
          {

            try
            {

              lvItem = lvListView.Items[ sKeyPair ];
              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems[ 1 ].Text = DicOutlinks[ sUrl ].GetAbsoluteUrl();

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewStylesheets 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( sKeyPair );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sKeyPair;

              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems.Add( DicOutlinks[ sUrl ].GetAbsoluteUrl() );

              lvListView.Items.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewStylesheets 2: {0}", ex.Message ) );
            }

          }

          iCount++;

        }
        
        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          if( JobMaster.GetAllowedHosts().IsInternalUrl( DicOutlinks[ sUrl ].GetAbsoluteUrl() ) )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
            lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }

        }
                
      }

      lvListView.EndUpdate();
      
      return( count );
            
    }

    /** Javascripts ***********************************************************/

    private async Task<int> RenderListViewJavascripts ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewJavascripts;
      Dictionary<string,MacroscopeOutlink> DicOutlinks = msDoc.GetOutlinks();
      int iCount = 1;
      int count = 0;
            
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      foreach( string sUrl in DicOutlinks.Keys )
      {

        ListViewItem lvItem = null;
        string sKeyPair = sUrl;
        MacroscopeConstants.OutlinkType LinkType = DicOutlinks[ sUrl ].GetLinkType();
        count++;
        
        if( LinkType == MacroscopeConstants.OutlinkType.SCRIPT )
        {

          if( lvListView.Items.ContainsKey( sKeyPair ) )
          {

            try
            {

              lvItem = lvListView.Items[ sKeyPair ];
              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems[ 1 ].Text = DicOutlinks[ sUrl ].GetAbsoluteUrl();

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewJavascripts 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( sKeyPair );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sKeyPair;

              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems.Add( DicOutlinks[ sUrl ].GetAbsoluteUrl() );

              lvListView.Items.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewJavascripts 2: {0}", ex.Message ) );
            }

          }

          iCount++;

        }
        
        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          if( JobMaster.GetAllowedHosts().IsInternalUrl( DicOutlinks[ sUrl ].GetAbsoluteUrl() ) )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
            lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }

        }
                
      }

      lvListView.EndUpdate();
          
      return( count );
      
    }

    /** Images ****************************************************************/

    private async Task<int> RenderListViewImages ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewImages;
      Dictionary<string,MacroscopeOutlink> DicOutlinks = msDoc.GetOutlinks();
      int iCount = 1;
      int count = 0;
            
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      foreach( string sUrl in DicOutlinks.Keys )
      {

        ListViewItem lvItem = null;
        string sKeyPair = sUrl;
        MacroscopeConstants.OutlinkType LinkType = DicOutlinks[ sUrl ].GetLinkType();
        count++;
        
        if( LinkType == MacroscopeConstants.OutlinkType.IMAGE )
        {

          if( lvListView.Items.ContainsKey( sKeyPair ) )
          {

            try
            {

              lvItem = lvListView.Items[ sKeyPair ];
              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems[ 1 ].Text = DicOutlinks[ sUrl ].GetAbsoluteUrl();
              lvItem.SubItems[ 2 ].Text = DicOutlinks[ sUrl ].GetTitle();
              lvItem.SubItems[ 3 ].Text = DicOutlinks[ sUrl ].GetAltText();
              
            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewImages 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( sKeyPair );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sKeyPair;

              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems.Add( DicOutlinks[ sUrl ].GetAbsoluteUrl() );
              lvItem.SubItems.Add( DicOutlinks[ sUrl ].GetTitle() );
              lvItem.SubItems.Add( DicOutlinks[ sUrl ].GetAltText() );

              lvListView.Items.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewImages 2: {0}", ex.Message ) );
            }

          }

          iCount++;

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          if( JobMaster.GetAllowedHosts().IsInternalUrl( DicOutlinks[ sUrl ].GetAbsoluteUrl() ) )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
            lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }

        }

      }

      lvListView.EndUpdate();
         
      return( count );
      
    }
    
    /** Audios ****************************************************************/

    private async Task<int> RenderListViewAudios ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewAudios;
      Dictionary<string,MacroscopeOutlink> DicOutlinks = msDoc.GetOutlinks();
      int iCount = 1;
      int count = 0;
            
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      foreach( string sUrl in DicOutlinks.Keys )
      {

        ListViewItem lvItem = null;
        string sKeyPair = sUrl;
        MacroscopeConstants.OutlinkType LinkType = DicOutlinks[ sUrl ].GetLinkType();
        count++;
        
        if( LinkType == MacroscopeConstants.OutlinkType.AUDIO )
        {

          if( lvListView.Items.ContainsKey( sKeyPair ) )
          {

            try
            {

              lvItem = lvListView.Items[ sKeyPair ];
              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems[ 1 ].Text = DicOutlinks[ sUrl ].GetAbsoluteUrl();

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewAudios 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( sKeyPair );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sKeyPair;

              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems.Add( DicOutlinks[ sUrl ].GetAbsoluteUrl() );

              lvListView.Items.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewAudios 2: {0}", ex.Message ) );
            }

          }

          iCount++;

        }
        
        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          if( JobMaster.GetAllowedHosts().IsInternalUrl( DicOutlinks[ sUrl ].GetAbsoluteUrl() ) )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
            lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }

        }
        
      }

      lvListView.EndUpdate();
         
      return( count );
      
    }

    /** Videos ****************************************************************/

    private async Task<int> RenderListViewVideos ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewVideos;
      Dictionary<string,MacroscopeOutlink> DicOutlinks = msDoc.GetOutlinks();
      int iCount = 1;
      int count = 0;
            
      lvListView.BeginUpdate();
      
      lvListView.Items.Clear();

      foreach( string sUrl in DicOutlinks.Keys )
      {

        ListViewItem lvItem = null;
        string sKeyPair = sUrl;
        MacroscopeConstants.OutlinkType LinkType = DicOutlinks[ sUrl ].GetLinkType();
        count++;
        
        if( LinkType == MacroscopeConstants.OutlinkType.VIDEO )
        {

          if( lvListView.Items.ContainsKey( sKeyPair ) )
          {

            try
            {

              lvItem = lvListView.Items[ sKeyPair ];
              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems[ 1 ].Text = DicOutlinks[ sUrl ].GetAbsoluteUrl();

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewVideos 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( sKeyPair );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sKeyPair;

              lvItem.SubItems[ 0 ].Text = iCount.ToString();
              lvItem.SubItems.Add( DicOutlinks[ sUrl ].GetAbsoluteUrl() );

              lvListView.Items.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "RenderListViewVideos 2: {0}", ex.Message ) );
            }

          }

          iCount++;

        }
        
        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          if( JobMaster.GetAllowedHosts().IsInternalUrl( DicOutlinks[ sUrl ].GetAbsoluteUrl() ) )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
            lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }

        }
                
      }

      lvListView.EndUpdate();
      
      return( count );
      
    }

    /** Keyword Analysis ******************************************************/

    private async Task<int> RenderListViewKeywordAnalysis ( MacroscopeJobMaster JobMaster, MacroscopeDocument msDoc )
    {

      ListView lvListView = this.listViewKeywordAnalysis;
      Dictionary<string,int> DicTerms = msDoc.GetDeepKeywordAnalysisAsDictonary( Words: 1 );
      int count = 0;
      
      lvListView.BeginUpdate();
            
      lvListView.Items.Clear();

      foreach( string sTerm in DicTerms.Keys )
      {

        ListViewItem lvItem = null;
        string sKeyPair = sTerm;
        count++;
        
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
            DebugMsg( string.Format( "RenderListViewKeywordAnalysis 1: {0}", ex.Message ) );
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
            DebugMsg( string.Format( "RenderListViewKeywordAnalysis 2: {0}", ex.Message ) );
          }

        }
        
        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

        }
                
      }
      
      lvListView.EndUpdate();
      
      return( count );
      
    }

    /** Document Preview ******************************************************/

    private void RenderDocumentPreview ( MacroscopeDocument msDoc )
    {

      if( msDoc.GetIsImage() )
      {
        this.RenderImagePreview( msDoc );
      }
      else
      {
        ClearDocumentPreviewListView();
      }

    }

    /** Clear Document Preview ListView ***************************************/

    private void ClearDocumentPreviewListView ()
    {
      ListView lvListView = this.listViewDocInfo;
      lvListView.Clear();
      this.pictureBoxDocumentDetailsImage.Image = null;
    }

    /** Image Preview *********************************************************/

    private void RenderImagePreview ( MacroscopeDocument msDoc )
    {
      if( msDoc.GetIsImage() )
      {
        MemoryStream msStream = this.UrlLoader.LoadMemoryStreamFromUrl( msDoc.GetUrl() );
        if( msStream != null )
        {
          this.pictureBoxDocumentDetailsImage.Image = Image.FromStream( msStream );
          this.RenderImagePreviewListView( msDoc, this.pictureBoxDocumentDetailsImage.Image );
        }
      }
      else
      {
        try
        {
          this.pictureBoxDocumentDetailsImage.Image = null;
        }
        catch( Exception ex )
        {
          MessageBox.Show( ex.Message );
        }
      }
    }

    private void RenderImagePreviewListView ( MacroscopeDocument msDoc, Image iImage )
    {

      ListView lvListView = this.listViewDocInfo;

      lvListView.Clear();

      lvListView.Columns.Add( "Property" );
      lvListView.Columns.Add( "Value" );

      {
        ListViewItem lvItem = new ListViewItem ( "Format" );
        lvItem.SubItems[ 0 ].Text = "PixelFormat";
        lvItem.SubItems.Add( msDoc.GetMimeType() );
        lvListView.Items.Add( lvItem );
      }

      {
        ListViewItem lvItem = new ListViewItem ( "WIDTH" );
        lvItem.SubItems[ 0 ].Text = "Width";
        lvItem.SubItems.Add( iImage.Width.ToString() );
        lvListView.Items.Add( lvItem );
      }

      {
        ListViewItem lvItem = new ListViewItem ( "HEIGHT" );
        lvItem.SubItems[ 0 ].Text = "Height";
        lvItem.SubItems.Add( iImage.Height.ToString() );
        lvListView.Items.Add( lvItem );
      }

      lvListView.Columns[ 0 ].Width = 150;
      lvListView.Columns[ 1 ].Width = 150;

    }

    /**************************************************************************/
  }

}
