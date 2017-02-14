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

  public sealed class MacroscopeDisplaySitemaps : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplaySitemaps ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
      : base( MainFormNew, lvListViewNew )
    {

      MainForm = MainFormNew;
      lvListView = lvListViewNew;

      if( MainForm.InvokeRequired )
      {
        MainForm.Invoke(
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

    protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
    {

      DebugMsg( string.Format( "MacroscopeDisplaySitemaps sUrl: {0}", sUrl ) );

      if( !msDoc.GetIsSitemapXml() )
      {
        return;
      }

      string sPairKey = string.Join( "", sUrl );

      ListViewItem lvItem = null;
      int iCount = msDoc.CountOutlinks();

      if( this.lvListView.Items.ContainsKey( sPairKey ) )
      {

        try
        {

          lvItem = this.lvListView.Items[ sPairKey ];
          lvItem.SubItems[ 0 ].Text = sUrl;
          lvItem.SubItems[ 1 ].Text = iCount.ToString();

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplaySitemaps 1: {0}", ex.Message ) );
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
          lvItem.SubItems.Add( iCount.ToString() );

          this.lvListView.Items.Add( lvItem );

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplaySitemaps 2: {0}", ex.Message ) );
        }

      }

      if( lvItem != null )
      {

        lvItem.ForeColor = Color.Blue;

        if( iCount <= 0 )
        {
          lvItem.ForeColor = Color.OrangeRed;
        }

      }

    }

    /**************************************************************************/

  }


}
