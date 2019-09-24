/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayRemarks : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColUrl = 0;
    private const int ColStatusCode = 1;
    private const int ColStatus = 2;
    private const int ColObservation = 3;

    private ToolStripLabel DocumentCount;
        
    /**************************************************************************/

    public MacroscopeDisplayRemarks ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.DocumentCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelRemarksItems;
      
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
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc,
      string Url
    )
    {

      foreach( KeyValuePair<string,string> RemarkPair in msDoc.IterateRemarks() )
      {

        ListViewItem lvItem = null;
        string PairKey = string.Join( @"::::", Url, RemarkPair.Value );
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
      
        if( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];
            lvItem.SubItems[ ColUrl ].Text = Url;
            lvItem.SubItems[ ColStatusCode ].Text = StatusCode;
            lvItem.SubItems[ ColStatus ].Text = Status;       
            lvItem.SubItems[ ColObservation ].Text = RemarkPair.Value;

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayRemarks 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem ( PairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = PairKey;

            lvItem.SubItems[ ColUrl ].Text = Url;
            lvItem.SubItems.Add( StatusCode );         
            lvItem.SubItems.Add( Status );       
            lvItem.SubItems.Add( RemarkPair.Value );

            ListViewItems.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayRemarks 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          // URL -------------------------------------------------------------//
          
          if( msDoc.GetIsInternal() )
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
          }

        }

      }

    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
      this.DocumentCount.Text = string.Format( "Observations: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/

  }

}
