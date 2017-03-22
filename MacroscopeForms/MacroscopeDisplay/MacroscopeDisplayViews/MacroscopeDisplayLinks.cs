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

  /// <summary>
  /// Description of MacroscopeDisplayLinks.
  /// </summary>

  public sealed class MacroscopeDisplayLinks : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private ToolStripLabel UrlCount;
        
    /**************************************************************************/

    public MacroscopeDisplayLinks ( MacroscopeMainForm MainForm, ListView lvListView )
      : base( MainForm, lvListView )
    {
      
      this.SuppressDebugMsg = true;

      this.MainForm = MainForm;
      this.lvListView = lvListView;
      this.UrlCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelLinksUrls;
      
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
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
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListViewSearchSourceUrls(
                DocCollection: DocCollection,
                UrlFragment: UrlFragment
              );
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
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListViewSearchTargetUrls(
                DocCollection: DocCollection,
                UrlFragment: UrlFragment
              );
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
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RenderListViewSearchSourceUrls (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ();
      decimal Count = 0;
      decimal TotalDocs = ( decimal )DocCollection.CountDocuments();
      ProgressForm.Show();
      
      ProgressForm.UpdatePercentages(
        Title: "Displaying Links",
        Message: "Processing links in document collection for display:",
        MajorPercentage: ( ( decimal )100 / TotalDocs ) * Count,
        ProgressLabelMajor: "Documents Processed"
      );
      
      foreach( string Url in DocCollection.DocumentKeys() )
      {

        if( Url.IndexOf( UrlFragment, StringComparison.CurrentCulture ) >= 0 )
        {

          MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
          
          this.RenderListView(
            msDoc: msDoc,
            Url: Url 
          );

        }

        Count++;
                
        ProgressForm.UpdatePercentages(
          Title: null,
          Message: null,
          MajorPercentage: ( ( decimal )100 / TotalDocs ) * Count,
          ProgressLabelMajor: null
        );
        
      }

      ProgressForm.Close();
      
      ProgressForm.Dispose();
      
    }

    /**************************************************************************/

    public void RenderListViewSearchTargetUrls (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ();
      decimal Count = 0;
      decimal TotalDocs = ( decimal )DocCollection.CountDocuments();
      ProgressForm.Show();
      
      ProgressForm.UpdatePercentages(
        Title: "Displaying Links",
        Message: "Processing links in document collection for display:",
        MajorPercentage: ( ( decimal )100 / TotalDocs ) * Count,
        ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
      );
      
      
      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );

        if( msDoc != null )
        {
          this.RenderListViewSearchTargetUrls(
            msDoc: msDoc,
            Url: Url,
            UrlFragment: UrlFragment
          );
        }

        Count++;
                
        ProgressForm.UpdatePercentages(
          Title: null,
          Message: null,
          MajorPercentage: ( ( decimal )100 / TotalDocs ) * Count,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );
                
      }

      ProgressForm.Close();
      
      ProgressForm.Dispose();
      
    }

    /**************************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
    {
              
      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();

      this.lvListView.BeginUpdate();

      foreach( MacroscopeLink Link in msDoc.IterateOutlinks() )
      {

        ListViewItem lvItem = null;
        string LinkType = Link.GetLinkType().ToString();
        string UrlTarget = Link.GetTargetUrl();
        string sPairKey = string.Join( "::", Url, UrlTarget );
        string Follow = Link.GetDoFollow().ToString();
        string AltText = Link.GetAltText();
        string AltTextLabel = AltText;
        
        if( string.IsNullOrEmpty( AltText ) )
        {
          AltTextLabel = "";
        }

        if( this.lvListView.Items.ContainsKey( sPairKey ) )
        {

          try
          {

            lvItem = this.lvListView.Items[ sPairKey ];

            lvItem.SubItems[ 0 ].Text = LinkType;
            lvItem.SubItems[ 1 ].Text = Url;
            lvItem.SubItems[ 2 ].Text = UrlTarget;
            lvItem.SubItems[ 3 ].Text = Follow;
            lvItem.SubItems[ 4 ].Text = AltTextLabel;

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

            lvItem = new ListViewItem ( sPairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = sPairKey;

            lvItem.SubItems[ 0 ].Text = LinkType;
            lvItem.SubItems.Add( Url );
            lvItem.SubItems.Add( UrlTarget );
            lvItem.SubItems.Add( Follow );
            lvItem.SubItems.Add( AltTextLabel );

            this.lvListView.Items.Add( lvItem );

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
            lvItem.SubItems[ 1 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }
          
          if( AllowedHosts.IsAllowedFromUrl( UrlTarget ) )
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
          }

        }

      }

      this.lvListView.EndUpdate();

      this.UrlCount.Text = string.Format( "URLs: {0}", lvListView.Items.Count );

    }

    /**************************************************************************/
    
    
    private void RenderListViewSearchTargetUrls ( MacroscopeDocument msDoc, string Url, string UrlFragment )
    {

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();

      this.lvListView.BeginUpdate();

      foreach( MacroscopeLink Link in msDoc.IterateOutlinks() )
      {

        string LinkType = Link.GetLinkType().ToString();
        string UrlTarget = Link.GetTargetUrl();
        string sPairKey = string.Join( "::", Url, UrlTarget );
        string Follow = Link.GetDoFollow().ToString();
        string AltText = Link.GetAltText();
        string AltTextLabel = AltText;
        
        if( string.IsNullOrEmpty( AltText ) )
        {
          AltTextLabel = "";
        }

        if(
          ( UrlTarget != null )
          && ( UrlTarget.IndexOf( UrlFragment, StringComparison.CurrentCulture ) >= 0 ) )
        {

          ListViewItem lvItem = null;
                  
          if( this.lvListView.Items.ContainsKey( sPairKey ) )
          {

            try
            {

              lvItem = this.lvListView.Items[ sPairKey ];

              lvItem.SubItems[ 0 ].Text = LinkType;
              lvItem.SubItems[ 1 ].Text = Url;
              lvItem.SubItems[ 2 ].Text = UrlTarget;
              lvItem.SubItems[ 3 ].Text = Follow;
              lvItem.SubItems[ 4 ].Text = AltTextLabel;

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

              lvItem = new ListViewItem ( sPairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sPairKey;

              lvItem.SubItems[ 0 ].Text = LinkType;
              lvItem.SubItems.Add( Url );
              lvItem.SubItems.Add( UrlTarget );
              lvItem.SubItems.Add( Follow );
              lvItem.SubItems.Add( AltTextLabel );

              this.lvListView.Items.Add( lvItem );

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
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }
          
            if( AllowedHosts.IsAllowedFromUrl( UrlTarget ) )
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
            }

          }

        }

      }

      this.lvListView.EndUpdate();

      this.UrlCount.Text = string.Format( "URLs: {0}", lvListView.Items.Count );

    }

    /**************************************************************************/

  }

}
