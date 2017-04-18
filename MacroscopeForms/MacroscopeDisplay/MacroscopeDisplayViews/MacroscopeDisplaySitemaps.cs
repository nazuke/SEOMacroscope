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

  public sealed class MacroscopeDisplaySitemaps : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplaySitemaps ( MacroscopeMainForm MainForm, ListView lvListView )
      : base( MainForm, lvListView )
    {

      this.MainForm = MainForm;
      this.lvListView = lvListView;

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

    protected override void RenderListView (
      List<ListViewItem> ListViewItems,
      MacroscopeDocument msDoc,
      string Url
    )
    {

      Boolean Proceed = false;

      if( msDoc.GetIsSitemapXml() )
      {
        Proceed = true;
      }
      else
      if( msDoc.GetIsSitemapText() )
      {
        Proceed = true;
      }
      
      if( !Proceed )
      {
        return;
      }

      string PairKey = string.Join( "", Url );

      ListViewItem lvItem = null;
      int Count = msDoc.CountOutlinks();

      if( this.lvListView.Items.ContainsKey( PairKey ) )
      {

        try
        {

          lvItem = this.lvListView.Items[ PairKey ];
          lvItem.SubItems[ 0 ].Text = Url;
          lvItem.SubItems[ 1 ].Text = Count.ToString();

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

          lvItem = new ListViewItem ( PairKey );
          lvItem.UseItemStyleForSubItems = false;
          lvItem.Name = PairKey;

          lvItem.SubItems[ 0 ].Text = Url;
          lvItem.SubItems.Add( Count.ToString() );

          ListViewItems.Add( lvItem );

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplaySitemaps 2: {0}", ex.Message ) );
        }

      }

      if( lvItem != null )
      {

        lvItem.ForeColor = Color.Blue;

        if( !msDoc.GetIsExternal() )
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Green;
          if( Count <= 0 )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Red;
            lvItem.SubItems[ 1 ].ForeColor = Color.Red;
          }
        }
        else
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
        }

      }

    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
    }

    /**************************************************************************/

  }

}
