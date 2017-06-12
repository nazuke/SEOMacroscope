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

  public sealed class MacroscopeDisplayUriQueue : Macroscope
  {

    /**************************************************************************/

    private MacroscopeMainForm MainForm;

    private ListView DisplayListView;

    private Boolean ListViewConfigured = false;
    
    private ToolStripLabel UriQueueCount;

    /**************************************************************************/

    public MacroscopeDisplayUriQueue ( MacroscopeMainForm MainForm, ListView TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.UriQueueCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelUriQueueItems;
      
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

    private void ConfigureListView ()
    {
      if( !this.ListViewConfigured )
      {
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    public void ClearData ()
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.DisplayListView.Items.Clear();
            }
          )
        );
      }
      else
      {
        this.DisplayListView.Items.Clear();
      }
    }

    /**************************************************************************/

    public void RefreshData ( string [] UriQueue )
    {
      
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              lock( this.DisplayListView )
              {
                Cursor.Current = Cursors.WaitCursor;
                this.DisplayListView.BeginUpdate();
                this.DisplayListView.Items.Clear();
                this.RenderListView( UriQueue: UriQueue );
                this.RenderUrlCount();
                this.DisplayListView.EndUpdate();
                Cursor.Current = Cursors.Default;

              }
            }
          )
        );
      }
      else
      {
        lock( this.DisplayListView )
        {
          Cursor.Current = Cursors.WaitCursor;
          this.DisplayListView.BeginUpdate();
          this.DisplayListView.Items.Clear();
          this.RenderListView( UriQueue: UriQueue );
          this.RenderUrlCount();
          this.DisplayListView.EndUpdate();
          Cursor.Current = Cursors.Default;
        }
      }
      
      GC.Collect();

    }

    /**************************************************************************/

    private void RenderListView ( string [] UriQueue )
    {

      if( UriQueue.Length == 0 )
      {
        return;
      }
      
      List<ListViewItem> ListViewItems = new List<ListViewItem> ( 1 );
            
      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ( this.MainForm );
      int Item = 1;
      decimal Count = 0;
      decimal TotalDocs = ( decimal )UriQueue.Length;
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {

        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing URI Queue for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "URL {0} / {1}", Count, TotalDocs )
        );  

      }
      
      for( int i = 0 ; i < UriQueue.Length ; i++ )
      {

        ListViewItem lvItem = null;
        string Url = UriQueue[ i ];
        
        if( this.DisplayListView.Items.ContainsKey( Url ) )
        {

          try
          {
            lvItem = this.DisplayListView.Items[ Url ];
            lvItem.SubItems[ 0 ].Text = Item.ToString();
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "RenderListView 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem ( Url );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = Url;
            lvItem.SubItems[ 0 ].Text = Item.ToString();
            lvItem.SubItems.Add( Url );

            ListViewItems.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "RenderListView 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
                      
          if( AllowedHosts.IsInternalUrl( Url ) )
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }

        }

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {
          
          Count++;
          TotalDocs = ( decimal )UriQueue.Length;
          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "URL {0} / {1}", Count, TotalDocs )
          );
          
        }
       
        Item++;
        
      }
            
      this.DisplayListView.Items.AddRange( ListViewItems.ToArray() );
            
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }
      
      ProgressForm.Dispose();
      
    }

    /**************************************************************************/

    private void RenderUrlCount ()
    {
      this.UriQueueCount.Text = string.Format( "URL Queue Items: {0}", this.DisplayListView.Items.Count );
    }
       
    /**************************************************************************/
    
  }

}
