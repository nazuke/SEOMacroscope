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
using System.Diagnostics;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeContextMenus.
  /// </summary>

  public class MacroscopeContextMenus : MacroscopeUserControl
  {

    /**************************************************************************/

    public MacroscopeContextMenus ()
    {
    }

    /** OPEN URL IN BROWSER ***************************************************/

    public void CallbackOpenUrlInBrowserClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "URL";
      
      if( !OpenUrlInBrowserSelector( TargetListView: TargetListView, ColumnName: ColumnName ) )
      {
        MessageBox.Show( "URL column not found" ); 
      }

    }

    /** -------------------------------------------------------------------- **/

    public void CallbackOpenSourceUrlInBrowserClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Source URL";
      
      if( !OpenUrlInBrowserSelector( TargetListView: TargetListView, ColumnName: ColumnName ) )
      {
        MessageBox.Show( "URL column not found" ); 
      }

    }

    /** -------------------------------------------------------------------- **/

    public void CallbackOpenTargetUrlInBrowserClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Target URL";
      
      if( !OpenUrlInBrowserSelector( TargetListView: TargetListView, ColumnName: ColumnName ) )
      {
        MessageBox.Show( "URL column not found" ); 
      }

    }

    /** -------------------------------------------------------------------- **/

    private Boolean OpenUrlInBrowserSelector ( ListView TargetListView, string ColumnName )
    {

      string Url = null;
      int UrlColumn = -1;
      Boolean Success = false;
      
      lock( TargetListView )
      {

        for( int i = 0 ; i < TargetListView.Columns.Count ; i++ )
        {
          
          if( TargetListView.Columns[ i ].Text.Equals( ColumnName ) )
          {
            UrlColumn = i;
            break;
          }
          
        }

        if( UrlColumn > -1 )
        {
          foreach( ListViewItem lvItem in TargetListView.SelectedItems )
          {
            Url = lvItem.SubItems[ UrlColumn ].Text.ToString();
          }
        }

      }

      if( Url != null )
      {
        Success = true;
        this.OpenUrlInBrowser( Url );
      }
      
      return( Success );
      
    }

    /** EXTERNAL BROWSER ******************************************************/

    private void OpenUrlInBrowser ( string Url )
    {

      Uri OpenUrl = null;

      try
      {
        OpenUrl = new Uri ( Url );
      }
      catch( UriFormatException ex )
      {
        MessageBox.Show( ex.Message );
      }

      if( OpenUrl != null )
      {
        try
        {
          // TODO: bug here when opening URL with query string
          Process.Start( OpenUrl.ToString() );
        }
        catch( Exception ex )
        {
          MessageBox.Show( ex.Message );
        }
      }

    }

    /** COPY ROWS AND VALUES **************************************************/
    
    public void CallbackCopyRowsClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;

      this.CopyListViewRowsTextToClipboard( TargetListView: TargetListView );

    }

    /** -------------------------------------------------------------------- **/

    public void CallbackCopyValuesClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;

      this.CopyListViewValuesTextToClipboard( TargetListView: TargetListView );

    }

    /** COPY COLUMN VALUES ****************************************************/

    public void CallbackCopyUrlClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "URL";
      int ColumnIndex;

      ColumnIndex = this.FindColumnIndex(
        TargetListView: TargetListView,
        ColumnName: ColumnName
      );

      if( ColumnIndex > -1 )
      {
        this.CopyColumnValuesToClipboard(
          TargetListView: TargetListView,
          ColumnIndex: ColumnIndex
        );
      }

    }

    /** -------------------------------------------------------------------- **/
    
    public void CallbackCopySourceUrlClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Source URL";
      int ColumnIndex;

      ColumnIndex = this.FindColumnIndex(
        TargetListView: TargetListView,
        ColumnName: ColumnName
      );

      if( ColumnIndex > -1 )
      {
        this.CopyColumnValuesToClipboard(
          TargetListView: TargetListView,
          ColumnIndex: ColumnIndex
        );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void CallbackCopyTargetClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Target URL";
      int ColumnIndex;

      ColumnIndex = this.FindColumnIndex( 
        TargetListView: TargetListView, 
        ColumnName: ColumnName
      );

      if( ColumnIndex > -1 )
      {
        this.CopyColumnValuesToClipboard( 
          TargetListView: TargetListView, 
          ColumnIndex: ColumnIndex
        );
      }

    }
    
    /** -------------------------------------------------------------------- **/   
        
    public void CallbackCopyRawSourceUrlClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Raw Source URL";
      int ColumnIndex;
      
      ColumnIndex = this.FindColumnIndex( 
        TargetListView: TargetListView, 
        ColumnName: ColumnName 
      );

      if( ColumnIndex > -1 )
      {
        this.CopyColumnValuesToClipboard( 
          TargetListView: TargetListView, 
          ColumnIndex: ColumnIndex 
        );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void CallbackCopyRawTargetUrlClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Raw Target URL";
      int ColumnIndex;
      
      ColumnIndex = this.FindColumnIndex( 
        TargetListView: TargetListView, 
        ColumnName: ColumnName 
      );

      if( ColumnIndex > -1 )
      {
        this.CopyColumnValuesToClipboard( 
          TargetListView: TargetListView, 
          ColumnIndex: ColumnIndex 
        );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void CallbackCopyLinkTextClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Link Text";
      int ColumnIndex;

      ColumnIndex = this.FindColumnIndex( 
        TargetListView: TargetListView, 
        ColumnName: ColumnName 
      );

      if( ColumnIndex > -1 )
      {
        this.CopyColumnValuesToClipboard( 
          TargetListView: TargetListView, 
          ColumnIndex: ColumnIndex 
        );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void CallbackCopyAltTextClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Alt Text";
      int ColumnIndex;

      ColumnIndex = this.FindColumnIndex( 
        TargetListView: TargetListView, 
        ColumnName: ColumnName 
      );

      if( ColumnIndex > -1 )
      {
        this.CopyColumnValuesToClipboard( 
          TargetListView: TargetListView, 
          ColumnIndex: ColumnIndex 
        );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void CallbackCopyTitleTextClick ( object sender, EventArgs e )
    {
      
      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      const string ColumnName = "Title Text";
      int ColumnIndex;

      ColumnIndex = this.FindColumnIndex( 
        TargetListView: TargetListView, 
        ColumnName: ColumnName 
      );

      if( ColumnIndex > -1 )
      {
        this.CopyColumnValuesToClipboard( 
          TargetListView: TargetListView, 
          ColumnIndex: ColumnIndex 
        );
      }

    }

    /** -------------------------------------------------------------------- **/

    private int FindColumnIndex ( ListView TargetListView, string ColumnName )
    {

      int ColumnIndex = -1;
      
      lock( TargetListView )
      {

        for( int i = 0 ; i < TargetListView.Columns.Count ; i++ )
        {

          string ColumnKey = TargetListView.Columns[ i ].Text;
          
          if( ColumnKey.Equals( ColumnName ) )
          {
            ColumnIndex = i;
            break;
          }
          
        }

      }

      return( ColumnIndex );
      
    }

    /** -------------------------------------------------------------------- **/

    private void CopyColumnValuesToClipboard ( ListView TargetListView, int ColumnIndex )
    {

      string TextToCopy = "";

      foreach( ListViewItem lvItem in TargetListView.SelectedItems )
      {
        TextToCopy += lvItem.SubItems[ ColumnIndex ].Text;
        TextToCopy += Environment.NewLine;
      }

      try
      {
        this.CopyTextToClipboard( Text: TextToCopy );
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message );
      }

    }

    /**************************************************************************/

  }

}
