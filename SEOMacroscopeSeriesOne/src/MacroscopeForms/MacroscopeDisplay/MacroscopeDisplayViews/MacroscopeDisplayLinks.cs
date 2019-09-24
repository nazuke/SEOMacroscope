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
using System.Net;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDisplayLinks.
  /// </summary>

  public sealed class MacroscopeDisplayLinks : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColType = 0;
    private const int ColUrl = 1;
    private const int ColUrlTarget = 2;
    private const int ColStatusCode = 3;
    private const int ColStatus = 4;
    private const int ColDoFollow = 5;
    private const int ColAltTextLabel = 6;
    private const int ColRawSourceUrl = 7;
    private const int ColRawTargetUrl = 8;

    private ToolStripLabel UrlCount;

    /**************************************************************************/

    public MacroscopeDisplayLinks ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.SuppressDebugMsg = true;

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.UrlCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelLinksUrls;

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

    protected override void ConfigureListView ()
    {
      if( !this.ListViewConfigured )
      {
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    public new void ClearData ()
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker(
            delegate
            {
              this.DisplayListView.Items.Clear();
              this.RenderUrlCount();
            }
          )
        );
      }
      else
      {
        this.DisplayListView.Items.Clear();
        this.RenderUrlCount();
      }
    }

    /**************************************************************************/

    public void RefreshDataSearchSourceUrls (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      if( DocCollection.CountDocuments() <= 0 )
      {
        return;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker(
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListViewSearchSourceUrls(
                DocCollection: DocCollection,
                UrlFragment: UrlFragment
              );
              this.RenderUrlCount();
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderListViewSearchSourceUrls(
          DocCollection: DocCollection,
          UrlFragment: UrlFragment
        );
        this.RenderUrlCount();
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RefreshDataSearchTargetUrls (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      if( DocCollection.CountDocuments() <= 0 )
      {
        return;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker(
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListViewSearchTargetUrls(
                DocCollection: DocCollection,
                UrlFragment: UrlFragment
              );
              this.RenderUrlCount();
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderListViewSearchTargetUrls(
          DocCollection: DocCollection,
          UrlFragment: UrlFragment
        );
        this.RenderUrlCount();
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RenderListViewSearchSourceUrls (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      List<ListViewItem> ListViewItems = new List<ListViewItem>( DocCollection.CountDocuments() );

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = (decimal) DocCollection.CountDocuments();

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {

        ProgressForm.UpdatePercentages(
          Title: "Displaying Links",
          Message: "Processing links in document collection for display:",
          MajorPercentage: ( (decimal) 100 / TotalDocs ) * Count,
          ProgressLabelMajor: "Documents Processed"
        );

      }

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        string Url = msDoc.GetUrl();

        if( Url.IndexOf( UrlFragment, StringComparison.CurrentCulture ) >= 0 )
        {

          this.RenderListView(
            ListViewItems: ListViewItems,
            DocCollection: DocCollection,
            msDoc: msDoc,
            Url: Url
          );

        }

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {

          Count++;
          TotalDocs = (decimal) DocCollection.CountDocuments();

          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: ( (decimal) 100 / TotalDocs ) * Count,
            ProgressLabelMajor: null
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

    public void RenderListViewSearchTargetUrls (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      List<ListViewItem> ListViewItems = new List<ListViewItem>( DocCollection.CountDocuments() );

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = (decimal) DocCollection.CountDocuments();

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {

        ProgressForm.UpdatePercentages(
          Title: "Displaying Links",
          Message: "Processing links in document collection for display:",
          MajorPercentage: ( (decimal) 100 / TotalDocs ) * Count,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );

      }

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        string Url = msDoc.GetUrl();

        if( msDoc != null )
        {
          this.RenderListViewSearchTargetUrls(
            ListViewItems: ListViewItems,
            msDoc: msDoc,
            Url: Url,
            UrlFragment: UrlFragment
          );
        }

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {

          Count++;

          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: ( (decimal) 100 / TotalDocs ) * Count,
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

    protected override void RenderListView (
      List<ListViewItem> ListViewItems,
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc,
      string Url
    )
    {

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();

      foreach( MacroscopeLink Link in msDoc.IterateOutlinks() )
      {

        ListViewItem lvItem = null;
        string LinkType = Link.GetLinkType().ToString();
        string UrlTarget = Link.GetTargetUrl();
        HttpStatusCode StatusCode = HttpStatusCode.NotFound;
        string StatusCodeText = "Not crawled";
        string StatusText = "Not crawled";
        string PairKey = string.Join( ":", UrlToDigest( Url: Url ), UrlToDigest( Url: UrlTarget ) );
        string DoFollow = "No Follow";
        string AltText = Link.GetAltText();
        string AltTextLabel = AltText;
        string RawSourceUrl = Link.GetRawSourceUrl();
        string RawTargetUrl = Link.GetRawTargetUrl();

        try
        {
          if( DocCollection.ContainsDocument( Url: Link.GetTargetUrl() ) )
          {
            StatusCode = DocCollection.GetDocumentByUrl( Url: Link.GetTargetUrl() ).GetStatusCode();
            StatusCodeText = ((int)StatusCode).ToString();
            StatusText = StatusCode.ToString();
          }
        }
        catch( Exception ex )
        {
          this.DebugMsg( ex.Message );
        }

        if( Link.GetDoFollow() )
        {
          DoFollow = "Follow";
        }

        if( string.IsNullOrEmpty( AltText ) )
        {
          AltTextLabel = "";
        }

        if( string.IsNullOrEmpty( RawSourceUrl ) )
        {
          RawSourceUrl = "";
        }

        if( string.IsNullOrEmpty( RawTargetUrl ) )
        {
          RawTargetUrl = "";
        }

        if( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];

            lvItem.SubItems[ ColType ].Text = LinkType;
            lvItem.SubItems[ ColUrl ].Text = Url;
            lvItem.SubItems[ ColUrlTarget ].Text = UrlTarget;
            lvItem.SubItems[ ColStatusCode ].Text = StatusCodeText;
            lvItem.SubItems[ ColStatus].Text = StatusText;
            lvItem.SubItems[ ColDoFollow ].Text = DoFollow;
            lvItem.SubItems[ ColAltTextLabel ].Text = AltTextLabel;
            lvItem.SubItems[ ColRawSourceUrl ].Text = RawSourceUrl;
            lvItem.SubItems[ ColRawTargetUrl ].Text = RawTargetUrl;

          }
          catch( Exception ex )
          {
            this.DebugMsg( string.Format( "MacroscopeDisplayLinks 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem( PairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = PairKey;

            lvItem.SubItems[ ColType ].Text = LinkType;
            lvItem.SubItems.Add( Url );
            lvItem.SubItems.Add( UrlTarget );
            lvItem.SubItems.Add( StatusCodeText );
            lvItem.SubItems.Add( StatusText );
            lvItem.SubItems.Add( DoFollow );
            lvItem.SubItems.Add( AltTextLabel );
            lvItem.SubItems.Add( RawSourceUrl );
            lvItem.SubItems.Add( RawTargetUrl );

            ListViewItems.Add( lvItem );

          }
          catch( Exception ex )
          {
            this.DebugMsg( string.Format( "MacroscopeDisplayLinks 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          for( int i = 0 ; i < lvItem.SubItems.Count ; i++ )
          {
            lvItem.SubItems[ i ].ForeColor = Color.Blue;
          }

          if( AllowedHosts.IsAllowedFromUrl( Url ) )
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
          }

          if( AllowedHosts.IsAllowedFromUrl( UrlTarget ) )
          {
            lvItem.SubItems[ ColUrlTarget ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ ColUrlTarget ].ForeColor = Color.Gray;
          }

          if( AllowedHosts.IsAllowedFromUrl( UrlTarget ) )
          {
            if( Link.GetDoFollow() )
            {
              lvItem.SubItems[ ColDoFollow ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ ColDoFollow ].ForeColor = Color.Red;
            }
          }
          else
          {
            lvItem.SubItems[ ColDoFollow ].ForeColor = Color.Gray;
          }

        }

      }

    }

    /**************************************************************************/

    private void RenderListViewSearchTargetUrls (
      List<ListViewItem> ListViewItems,
      MacroscopeDocument msDoc,
      string Url,
      string UrlFragment
    )
    {

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      MacroscopeDocumentCollection DocCollection= this.MainForm.GetJobMaster().GetDocCollection();

      foreach( MacroscopeLink Link in msDoc.IterateOutlinks() )
      {

        string LinkType = Link.GetLinkType().ToString();
        string UrlTarget = Link.GetTargetUrl();
        HttpStatusCode StatusCode = HttpStatusCode.NotFound;
        string StatusCodeText = "Not crawled";
        string StatusText = "Not crawled";
        string PairKey = string.Join( ":", UrlToDigest( Url: Url ), UrlToDigest( Url: UrlTarget ) ).ToString();
        string DoFollow = "No Follow";
        string AltText = Link.GetAltText();
        string AltTextLabel = AltText;

        string RawSourceUrl = Link.GetRawSourceUrl();
        string RawTargetUrl = Link.GetRawTargetUrl();

        try
        {
          if( DocCollection.ContainsDocument( Url: Link.GetTargetUrl() ) )
          {
            StatusCode = DocCollection.GetDocumentByUrl( Url: Link.GetTargetUrl() ).GetStatusCode();
            StatusCodeText = ( (int) StatusCode ).ToString();
            StatusText = StatusCode.ToString();
          }
        }
        catch( Exception ex )
        {
          this.DebugMsg( ex.Message );
        }

        if( Link.GetDoFollow() )
        {
          DoFollow = "Follow";
        }

        if( string.IsNullOrEmpty( AltText ) )
        {
          AltTextLabel = "";
        }

        if( string.IsNullOrEmpty( RawSourceUrl ) )
        {
          RawSourceUrl = "";
        }

        if( string.IsNullOrEmpty( RawTargetUrl ) )
        {
          RawTargetUrl = "";
        }

        if(
          ( UrlTarget != null )
          && ( UrlTarget.IndexOf( UrlFragment, StringComparison.CurrentCulture ) >= 0 ) )
        {

          ListViewItem lvItem = null;

          if( this.DisplayListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = this.DisplayListView.Items[ PairKey ];

              lvItem.SubItems[ ColType ].Text = LinkType;
              lvItem.SubItems[ ColUrl ].Text = Url;
              lvItem.SubItems[ ColUrlTarget ].Text = UrlTarget;
              lvItem.SubItems[ ColStatusCode ].Text = StatusCodeText;
              lvItem.SubItems[ ColStatus ].Text = StatusText;
              lvItem.SubItems[ ColDoFollow ].Text = DoFollow;
              lvItem.SubItems[ ColAltTextLabel ].Text = AltTextLabel;
              lvItem.SubItems[ ColRawSourceUrl ].Text = RawSourceUrl;
              lvItem.SubItems[ ColRawTargetUrl ].Text = RawTargetUrl;

            }
            catch( Exception ex )
            {
              this.DebugMsg( string.Format( "MacroscopeDisplayLinks 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem( PairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = PairKey;

              lvItem.SubItems[ ColType ].Text = LinkType;
              lvItem.SubItems.Add( Url );
              lvItem.SubItems.Add( UrlTarget );
              lvItem.SubItems.Add( StatusCodeText );
              lvItem.SubItems.Add( StatusText );
              lvItem.SubItems.Add( DoFollow );
              lvItem.SubItems.Add( AltTextLabel );
              lvItem.SubItems.Add( RawSourceUrl );
              lvItem.SubItems.Add( RawTargetUrl );

              ListViewItems.Add( lvItem );

            }
            catch( Exception ex )
            {
              this.DebugMsg( string.Format( "MacroscopeDisplayLinks 2: {0}", ex.Message ) );
            }

          }

          if( lvItem != null )
          {

            for( int i = 0 ; i < lvItem.SubItems.Count ; i++ )
            {
              lvItem.SubItems[ i ].ForeColor = Color.Blue;
            }

            if( AllowedHosts.IsAllowedFromUrl( Url ) )
            {
              lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
            }

            if( AllowedHosts.IsAllowedFromUrl( UrlTarget ) )
            {
              lvItem.SubItems[ ColUrlTarget ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ ColUrlTarget ].ForeColor = Color.Gray;
            }

            if( AllowedHosts.IsAllowedFromUrl( UrlTarget ) )
            {
              if( Link.GetDoFollow() )
              {
                lvItem.SubItems[ ColDoFollow ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ ColDoFollow ].ForeColor = Color.Red;
              }
            }
            else
            {
              lvItem.SubItems[ ColDoFollow ].ForeColor = Color.Gray;
            }

          }

        }

      }

    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
      this.UrlCount.Text = string.Format( "URLs: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/

  }

}
