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

  public sealed class MacroscopeDisplayHostnames : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayHostnames ( MacroscopeMainForm MainForm, ListView lvListView )
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

    public new void RefreshData ( MacroscopeDocumentCollection DocCollection )
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.lvListView.BeginUpdate();
              this.RenderListView( DocCollection );
              this.lvListView.EndUpdate();
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.lvListView.BeginUpdate();
        this.RenderListView( DocCollection );
        this.lvListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    /**************************************************************************/

    public new void RenderListView ( MacroscopeDocumentCollection DocCollection )
    {

      Dictionary<string,int> Hostnames = DocCollection.GetStatsHostnamesWithCount();

      foreach( string Hostname in Hostnames.Keys )
      {
        int Count = Hostnames[ Hostname ];
        this.RenderListView( Hostname, Count );
      }

    }

    /**************************************************************************/

    public void RenderListView ( string Hostname, int Count )
    {

      ListViewItem lvItem = null;
      Boolean bIsInternal = MainForm.GetJobMaster().GetAllowedHosts().IsAllowed( Hostname );
      string sPairKey = string.Join( "::", "HOST", Hostname );

      if( this.lvListView.Items.ContainsKey( sPairKey ) )
      {

        try
        {

          lvItem = this.lvListView.Items[ sPairKey ];
          lvItem.SubItems[ 0 ].Text = Hostname;
          lvItem.SubItems[ 1 ].Text = Count.ToString();
          lvItem.SubItems[ 2 ].Text = bIsInternal.ToString();

        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "MacroscopeDisplayHostnames 1: {0}", ex.Message ) );
        }

      }
      else
      {

        try
        {

          lvItem = new ListViewItem ( sPairKey );

          lvItem.Name = sPairKey;

          lvItem.SubItems[ 0 ].Text = Hostname;
          lvItem.SubItems.Add( Count.ToString() );
          lvItem.SubItems.Add( bIsInternal.ToString() );

          this.lvListView.Items.Add( lvItem );

        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "MacroscopeDisplayHostnames 2: {0}", ex.Message ) );
        }

      }

      if( lvItem != null )
      {

        lvItem.ForeColor = Color.Green;

        if( bIsInternal )
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Green;
          lvItem.SubItems[ 1 ].ForeColor = Color.Green;
          lvItem.SubItems[ 2 ].ForeColor = Color.Green;
        }
        else
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
        }
        
      }

    }

    /**************************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
    {
    }

    /**************************************************************************/

  }

}
