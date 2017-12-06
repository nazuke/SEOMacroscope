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

  public sealed class MacroscopeDisplayRobots : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayRobots ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
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

    protected override void ConfigureListView ()
    {
      if( !this.ListViewConfigured )
      {
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    public void RefreshData ( MacroscopeJobMaster JobMaster )
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.DisplayListView.BeginUpdate();
              this.RenderListView( JobMaster );
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
        this.RenderListView( JobMaster );
        this.DisplayListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    /**************************************************************************/

    public void RenderListView ( MacroscopeJobMaster JobMaster )
    {

      Dictionary<String,Boolean> Blocked = JobMaster.GetBlockedByRobotsList();

      if( Blocked.Count == 0 )
      {
        return;
      }
           
      List<ListViewItem> ListViewItems = new List<ListViewItem> ( 1 );
      
      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = ( decimal ) Blocked.Count;
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
            
      foreach( string Url in Blocked.Keys )
      {

        Boolean IsInternal = JobMaster.GetAllowedHosts().IsInternalUrl( Url );

        this.RenderListView(
          ListViewItems: ListViewItems,
          Url: Url,
          IsBlocked: Blocked[ Url ], 
          IsInternal: IsInternal
        );
        
        Count++;
        MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
        ProgressForm.UpdatePercentages(
          Title: null,
          Message: null,
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );

      }
     
      this.DisplayListView.Items.AddRange( ListViewItems.ToArray() );
            
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }
      
      ProgressForm.Dispose();

    }

    /**************************************************************************/

    private void RenderListView (
      List<ListViewItem> ListViewItems,
      string Url,
      Boolean IsBlocked,
      Boolean IsInternal
    )
    {

      string PairKey = string.Join( "", Url );
      string Blocked = "";
      ListViewItem lvItem = null;
      
      if( IsBlocked )
      {
        Blocked = "BLOCKED";
      }

      if( this.DisplayListView.Items.ContainsKey( PairKey ) )
      {

        try
        {

          lvItem = this.DisplayListView.Items[ PairKey ];
          lvItem.SubItems[ 0 ].Text = Url;
          lvItem.SubItems[ 1 ].Text = Blocked;

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplayRobots 1: {0}", ex.Message ) );
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
          lvItem.SubItems.Add( Blocked );

          ListViewItems.Add( lvItem );

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplayRobots 2: {0}", ex.Message ) );
        }

      }

      if( lvItem != null )
      {

        lvItem.UseItemStyleForSubItems = false;
        lvItem.ForeColor = Color.Blue;

        if( IsInternal )
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Green;
          lvItem.SubItems[ 1 ].ForeColor = Color.Green;
          if( IsBlocked )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Red;
            lvItem.SubItems[ 1 ].ForeColor = Color.Red;
          }
        }
        else
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
        }

      }

    }

    /**************************************************************************/

    protected override void RenderListView ( List<ListViewItem> ListViewItems, MacroscopeDocument msDoc, string Url )
    {
    }
    
    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
    }

    /**************************************************************************/

  }

}
