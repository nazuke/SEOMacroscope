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

  public sealed class MacroscopeDisplayHistory : Macroscope
  {

    /**************************************************************************/

    private MacroscopeMainForm MainForm;

    private ListView DisplayListView;

    private Boolean ListViewConfigured = false;
    
    /**************************************************************************/

    public MacroscopeDisplayHistory ( MacroscopeMainForm MainForm, ListView TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;

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

    public void RefreshData ( Dictionary<string,Boolean> History )
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
                this.RenderListView( History: History );
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
          this.RenderListView( History: History );
          this.DisplayListView.EndUpdate();
          Cursor.Current = Cursors.Default;
        }
      }
    }

    /**************************************************************************/

    private void RenderListView ( Dictionary<string,Boolean> History )
    {

      if( History.Count == 0 )
      {
        return;
      }
      
      List<ListViewItem> ListViewItems = new List<ListViewItem> ( 1 );
            
      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = ( decimal )History.Count;
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {

        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );  

      }
      
      foreach( string Url in History.Keys )
      {

        ListViewItem lvItem = null;
        string Visited = "No";

        if( History[ Url ] )
        {
          Visited = "Yes";
        }

        if( this.DisplayListView.Items.ContainsKey( Url ) )
        {

          try
          {
            lvItem = this.DisplayListView.Items[ Url ];
            lvItem.SubItems[ 1 ].Text = Visited;
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
            lvItem.SubItems.Add( Visited );

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

          if( AllowedHosts.IsInternalUrl( Url ) )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Green;
            if( History[ Url ] )
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Red;
            }
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
            lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
          }

        }

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {
          
          Count++;
          TotalDocs = ( decimal )History.Count;
          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );
          
        }
      
      }
            
      this.DisplayListView.Items.AddRange( ListViewItems.ToArray() );
            
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }
      
      ProgressForm.Dispose();
      
    }

    /**************************************************************************/

  }

}
