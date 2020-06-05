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
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** FILE MENU *************************************************************/

    public void ReconfigureFileMenu ()
    {

#if DEBUG
      this.loadSessionToolStripMenuItem.Visible = true;
      this.saveSessionAsToolStripMenuItem.Visible = true;
      this.toolStripSeparator30.Visible = true;
#else
      this.loadSessionToolStripMenuItem.Visible = true;
      this.saveSessionAsToolStripMenuItem.Visible = true;
      this.toolStripSeparator30.Visible = true;
#endif

      this.ReconfigureFileMenuRecentUrlsItems();

    }

    /** RECENT URLS　**********************************************************/

    public void ReconfigureFileMenuRecentUrlsItems ()
    {
      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker(
            delegate
            {
              this._ReconfigureFileMenuRecentUrlsItems();
            }
          )
        );
      }
      else
      {
        this._ReconfigureFileMenuRecentUrlsItems();
      }
      return;
    }

    /** -------------------------------------------------------------------- **/

    private void _ReconfigureFileMenuRecentUrlsItems ()
    {

      List<string> CrawlHistory = MacroscopePreferencesManager.GetCrawlHistory();
      ToolStripItemCollection RecentUrlItems = this.recentURLsToolStripMenuItem.DropDownItems;

      RecentUrlItems.Clear();
      CrawlHistory.Reverse();

      foreach( string Url in CrawlHistory )
      {

        string UrlTruncated = Url;
        ToolStripItem UrlItem = RecentUrlItems.Add( text: "..." );

        if( Url.Length > 64 )
        {
          UrlTruncated = Url.Substring( 0, 64 ) + "...";
        }

        UrlTruncated = UrlTruncated.Replace( "&", "&&" );
        UrlItem.Tag = Url;
        UrlItem.Text = UrlTruncated;
        UrlItem.Click += ClickCallbackFileMenuRecentUrlsItem;

      }

      {
        ToolStripSeparator separator = new ToolStripSeparator();
        RecentUrlItems.Add( separator );
      }

      {
        ToolStripItem UrlItem = RecentUrlItems.Add( text: "Clear Recent URLs" );
        UrlItem.Click += ClickCallbackFileMenuRecentUrlsClear;
      }

    }

    /** -------------------------------------------------------------------- **/

    private void ClickCallbackFileMenuRecentUrlsItem ( object sender, EventArgs e )
    {
      ToolStripItem UrlItem = (ToolStripItem) sender;
      string Url = UrlItem.Tag.ToString();
      this.SetUrl( Url: Url );
    }

    /** -------------------------------------------------------------------- **/

    private void ClickCallbackFileMenuRecentUrlsClear ( object sender, EventArgs e )
    {
      MacroscopePreferencesManager.ClearCrawlHistory();
      this.ReconfigureFileMenuRecentUrlsItems();
    }

    /**************************************************************************/

    private void CallbackFileExit ( object sender, EventArgs e )
    {
      DebugMsg( "CallbackFileExit Called" );
      Program.Exit();
    }

    /**************************************************************************/

  }

}
