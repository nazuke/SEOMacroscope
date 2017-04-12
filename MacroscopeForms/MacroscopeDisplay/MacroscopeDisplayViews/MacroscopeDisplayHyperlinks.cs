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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDisplayHyperlinks.
  /// </summary>

  public sealed class MacroscopeDisplayHyperlinks : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private ToolStripLabel UrlCount;

    /**************************************************************************/

    public MacroscopeDisplayHyperlinks ( MacroscopeMainForm MainForm, ListView lvListView )
      : base( MainForm, lvListView )
    {
      
      this.SuppressDebugMsg = false;

      this.MainForm = MainForm;
      this.lvListView = lvListView;
      this.UrlCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelHyperlinksUrls;
      
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

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        string Url = msDoc.GetUrl();

        if( Url.IndexOf( UrlFragment, StringComparison.CurrentCulture ) >= 0 )
        {

          this.RenderListView( msDoc: msDoc, Url: Url );

        }

      }

    }

    /**************************************************************************/

    public void RenderListViewSearchTargetUrls (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        string Url = msDoc.GetUrl();

        if( msDoc != null )
        {
          this.RenderListViewSearchTargetUrls(
            msDoc: msDoc,
            Url: Url,
            UrlFragment: UrlFragment
          );
        }

      }

    }

    /**************************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
    {
              
      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      MacroscopeHyperlinksOut HyperlinksOut = msDoc.GetHyperlinksOut();
      
      foreach( MacroscopeHyperlinkOut HyperlinkOut in HyperlinksOut.IterateLinks() )
      {

        ListViewItem lvItem = null;
        string UrlTarget = HyperlinkOut.GetTargetUrl();
        string sPairKey = string.Join( "::", Url, UrlTarget );
        string LinkText = HyperlinkOut.GetLinkText();
        string LinkTitle = HyperlinkOut.GetLinkTitle();
        string AltText = HyperlinkOut.GetAltText();

        string LinkTextLabel = LinkText;
        string LinkTitleLabel = LinkTitle;
        string AltTextLabel = AltText;
        
        string DoFollow = "No Follow";

        if( HyperlinkOut.GetDoFollow() )
        {
          DoFollow = "Follow";
        }

        if( LinkText.Length == 0 )
        {
          LinkTextLabel = "MISSING";
        }
        
        if( LinkTitle.Length == 0 )
        {
          LinkTitleLabel = "MISSING";
        }
        
        if( AltText.Length == 0 )
        {
          AltTextLabel = "MISSING";
        }

        if( this.lvListView.Items.ContainsKey( sPairKey ) )
        {

          try
          {

            lvItem = this.lvListView.Items[ sPairKey ];

            lvItem.SubItems[ 0 ].Text = Url;
            lvItem.SubItems[ 1 ].Text = UrlTarget;
            lvItem.SubItems[ 2 ].Text = DoFollow;
            lvItem.SubItems[ 3 ].Text = LinkTextLabel;
            lvItem.SubItems[ 4 ].Text = LinkTitleLabel;
            lvItem.SubItems[ 5 ].Text = AltTextLabel;

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

            lvItem.SubItems[ 0 ].Text = Url;
            lvItem.SubItems.Add( UrlTarget );
            lvItem.SubItems.Add( DoFollow );
            lvItem.SubItems.Add( LinkTextLabel );
            lvItem.SubItems.Add( LinkTitleLabel );
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
            lvItem.SubItems[ 0 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          }
          
          if( AllowedHosts.IsAllowedFromUrl( UrlTarget ) )
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }

          if( AllowedHosts.IsAllowedFromUrl( Url ) )
          {
            if( HyperlinkOut.GetDoFollow() )
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Red;
            }
          }
          else
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
          }

          if( LinkText.Length == 0 )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
          }
          
          if( LinkTitle.Length == 0 )
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Gray;
          }
          
          if( AltText.Length == 0 )
          {
            lvItem.SubItems[ 5 ].ForeColor = Color.Gray;
          }

          if(
            ( LinkText.Length == 0 )
            && ( LinkTitle.Length == 0 )
            && ( AltText.Length == 0 ) )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Red;
            lvItem.SubItems[ 4 ].ForeColor = Color.Red;
            lvItem.SubItems[ 5 ].ForeColor = Color.Red;
          }

        }

      }

      this.UrlCount.Text = string.Format( "URLs: {0}", lvListView.Items.Count );
              
    }

    /**************************************************************************/
    
    
    private void RenderListViewSearchTargetUrls ( MacroscopeDocument msDoc, string Url, string UrlFragment )
    {

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      MacroscopeHyperlinksOut HyperlinksOut = msDoc.GetHyperlinksOut();
      
      foreach( MacroscopeHyperlinkOut HyperlinkOut in HyperlinksOut.IterateLinks() )
      {

        string UrlTarget = HyperlinkOut.GetTargetUrl();
        string sPairKey = string.Join( "::", Url, UrlTarget );
        string LinkText = HyperlinkOut.GetLinkText();
        string LinkTitle = HyperlinkOut.GetLinkTitle();
        string AltText = HyperlinkOut.GetAltText();

        string LinkTextLabel = LinkText;
        string LinkTitleLabel = LinkTitle;
        string AltTextLabel = AltText;
        
        string DoFollow = "No Follow";

        if( HyperlinkOut.GetDoFollow() )
        {
          DoFollow = "Follow";
        }
        
        if( LinkText.Length == 0 )
        {
          LinkTextLabel = "MISSING";
        }
        
        if( LinkTitle.Length == 0 )
        {
          LinkTitleLabel = "MISSING";
        }
        
        if( AltText.Length == 0 )
        {
          AltTextLabel = "MISSING";
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

              lvItem.SubItems[ 0 ].Text = Url;
              lvItem.SubItems[ 1 ].Text = UrlTarget;
              lvItem.SubItems[ 2 ].Text = DoFollow;
              lvItem.SubItems[ 3 ].Text = LinkTextLabel;
              lvItem.SubItems[ 4 ].Text = LinkTitleLabel;
              lvItem.SubItems[ 5 ].Text = AltTextLabel;

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

              lvItem.SubItems[ 0 ].Text = Url;
              lvItem.SubItems.Add( UrlTarget );
              lvItem.SubItems.Add( DoFollow );
              lvItem.SubItems.Add( LinkTextLabel );
              lvItem.SubItems.Add( LinkTitleLabel );
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
              lvItem.SubItems[ 0 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
            }
          
            if( AllowedHosts.IsAllowedFromUrl( UrlTarget ) )
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

            if( AllowedHosts.IsAllowedFromUrl( Url ) )
            {
              if( HyperlinkOut.GetDoFollow() )
              {
                lvItem.SubItems[ 2 ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ 2 ].ForeColor = Color.Red;
              }
            }
            else
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
            }
            
            if( LinkText.Length == 0 )
            {
              lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
            }
          
            if( LinkTitle.Length == 0 )
            {
              lvItem.SubItems[ 4 ].ForeColor = Color.Gray;
            }
          
            if( AltText.Length == 0 )
            {
              lvItem.SubItems[ 5 ].ForeColor = Color.Gray;
            }

            if(
              ( LinkText.Length == 0 )
              && ( LinkTitle.Length == 0 )
              && ( AltText.Length == 0 ) )
            {
              lvItem.SubItems[ 3 ].ForeColor = Color.Red;
              lvItem.SubItems[ 4 ].ForeColor = Color.Red;
              lvItem.SubItems[ 5 ].ForeColor = Color.Red;
            }

          }

        }
       
      }

      this.UrlCount.Text = string.Format( "URLs: {0}", lvListView.Items.Count );
              
    }

    /**************************************************************************/

  }

}
