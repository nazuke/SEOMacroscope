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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayHostnames : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private ToolStripLabel HostsCount;
        
    /**************************************************************************/

    public MacroscopeDisplayHostnames ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.HostsCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelHostsItems;
      
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
              this.DisplayListView.BeginUpdate();
              this.RenderListView( DocCollection );
              this.RenderUrlCount();
              this.DisplayListView.EndUpdate();
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.DisplayListView.BeginUpdate();
        this.RenderListView( DocCollection );
        this.RenderUrlCount();
        this.DisplayListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    /**************************************************************************/

    public new void RenderListView ( MacroscopeDocumentCollection DocCollection )
    {

      List<ListViewItem> ListViewItems = new List<ListViewItem> ( 1 );
      Dictionary<string,int> Hostnames = DocCollection.GetStatsHostnamesWithCount();

      foreach( string Hostname in Hostnames.Keys )
      {

        int Count = Hostnames[ Hostname ];

        this.RenderListView(
          ListViewItems: ListViewItems,
          Hostname: Hostname,
          Count: Count 
        );
      }

      this.DisplayListView.Items.AddRange( ListViewItems.ToArray() );
            
    }

    /**************************************************************************/

    public void RenderListView (
      List<ListViewItem> ListViewItems,
      string Hostname,
      int Count
    )
    {

      ListViewItem lvItem = null;
      bool IsInternal = MainForm.GetJobMaster().GetAllowedHosts().IsAllowed( Hostname );
      string PairKey = string.Join( "::", "HOST", Hostname );

      if( this.DisplayListView.Items.ContainsKey( PairKey ) )
      {

        try
        {

          lvItem = this.DisplayListView.Items[ PairKey ];
          lvItem.SubItems[ 0 ].Text = Hostname;
          lvItem.SubItems[ 1 ].Text = Count.ToString();
          lvItem.SubItems[ 2 ].Text = IsInternal.ToString();

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

          lvItem = new ListViewItem ( PairKey );

          lvItem.Name = PairKey;

          lvItem.SubItems[ 0 ].Text = Hostname;
          lvItem.SubItems.Add( Count.ToString() );

          if( IsInternal )
          {
            lvItem.SubItems.Add( "yes" );
          }
          else
          {
            lvItem.SubItems.Add( " no" );
          }

          ListViewItems.Add( lvItem );

        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "MacroscopeDisplayHostnames 2: {0}", ex.Message ) );
        }

      }

      if( lvItem != null )
      {

        lvItem.ForeColor = Color.Green;

        if( IsInternal )
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

    protected override void RenderListView (
      List<ListViewItem> ListViewItems,
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc,
      string Url
    )
    {
    }
    
    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
      this.HostsCount.Text = string.Format( "Hosts: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/

  }

}
