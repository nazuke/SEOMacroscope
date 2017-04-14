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

    public MacroscopeDisplayRobots ( MacroscopeMainForm MainForm, ListView lvListView )
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

    public void RefreshData ( MacroscopeJobMaster JobMaster )
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.lvListView.BeginUpdate();
              this.RenderListView( JobMaster );
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
        this.RenderListView( JobMaster );
        this.lvListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    /**************************************************************************/

    public void RenderListView ( MacroscopeJobMaster JobMaster )
    {

      Dictionary<String,Boolean> dicBlocked = JobMaster.GetBlockedByRobotsList();

      if( dicBlocked.Count == 0 )
      {
        return;
      }
            
      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = ( decimal )dicBlocked.Count;
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
            
      foreach( string Url in dicBlocked.Keys )
      {

        Boolean bInternal = JobMaster.GetAllowedHosts().IsInternalUrl( Url );
        this.RenderListView( Url, dicBlocked[ Url ], bInternal );
        
        Count++;
        MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
        ProgressForm.UpdatePercentages(
          Title: null,
          Message: null,
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );

      }
     
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }
      
      ProgressForm.Dispose();

    }

    /**************************************************************************/

    private void RenderListView ( string Url, Boolean bBlocked, Boolean bInternal )
    {

      string sPairKey = string.Join( "", Url );
      string sBlocked = "";
      ListViewItem lvItem = null;
      
      if( bBlocked )
      {
        sBlocked = "blocked";
      }

      if( this.lvListView.Items.ContainsKey( sPairKey ) )
      {

        try
        {

          lvItem = this.lvListView.Items[ sPairKey ];
          lvItem.SubItems[ 0 ].Text = Url;
          lvItem.SubItems[ 1 ].Text = sBlocked;

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

          lvItem = new ListViewItem ( sPairKey );
          lvItem.UseItemStyleForSubItems = false;
          lvItem.Name = sPairKey;

          lvItem.SubItems[ 0 ].Text = Url;
          lvItem.SubItems.Add( sBlocked );

          this.lvListView.Items.Add( lvItem );

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

        if( bInternal )
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
        }
        else
        {
          lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
        }

        if( bBlocked )
        {
          if( bInternal )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          }
          lvItem.SubItems[ 1 ].ForeColor = Color.Red;
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
