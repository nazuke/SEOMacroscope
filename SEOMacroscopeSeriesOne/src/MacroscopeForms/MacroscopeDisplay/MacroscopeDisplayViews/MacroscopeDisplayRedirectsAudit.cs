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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDisplayRedirectsAudit.
  /// </summary>

  public sealed class MacroscopeDisplayRedirectsAudit : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private ToolStripLabel DocumentCount;
        
    /**************************************************************************/
        
    public MacroscopeDisplayRedirectsAudit ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.DocumentCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelRedirectsItems;

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

      if( !msDoc.GetIsRedirect() )
      {
        return;
      }

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      
      string OriginURL = msDoc.GetUrlRedirectFrom();
      int StatusCode = ( int )msDoc.GetStatusCode();
      string Status = msDoc.GetStatusCode().ToString();
      string DestinationURL = msDoc.GetUrlRedirectTo();

      string PairKey = string.Join( "", Url );
       
      if(
        ( !string.IsNullOrEmpty( OriginURL ) )
        && ( !string.IsNullOrEmpty( Status ) )
        && ( !string.IsNullOrEmpty( DestinationURL ) ) )
      {

        ListViewItem lvItem = null;

        if( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];
            lvItem.SubItems[ 0 ].Text = Url;
            lvItem.SubItems[ 1 ].Text = StatusCode.ToString();
            lvItem.SubItems[ 2 ].Text = Status;
            lvItem.SubItems[ 3 ].Text = OriginURL;
            lvItem.SubItems[ 4 ].Text = DestinationURL;

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayRedirectsAudit 1: {0}", ex.Message ) );
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
            lvItem.SubItems.Add( StatusCode.ToString() );
            lvItem.SubItems.Add( Status );
            lvItem.SubItems.Add( OriginURL );
            lvItem.SubItems.Add( DestinationURL );

            ListViewItems.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayRedirectsAudit 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          if( msDoc.GetIsInternal() )
          {

            for( int i = 0 ; i <= 4 ; i++ )
            {
              lvItem.SubItems[ i ].ForeColor = Color.Blue;
            }

            if( ( StatusCode >= 200 ) && ( StatusCode <= 299 ) )
            {
              for( int i = 0 ; i <= 4 ; i++ )
                lvItem.SubItems[ i ].ForeColor = Color.Green;
            }
            else
            if( ( StatusCode >= 300 ) && ( StatusCode <= 399 ) )
            {
              for( int i = 0 ; i <= 4 ; i++ )
                lvItem.SubItems[ i ].ForeColor = Color.Goldenrod;
            }
            else
            if( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
            {
              for( int i = 0 ; i <= 4 ; i++ )
              {
                lvItem.SubItems[ i ].ForeColor = Color.Red;
              }
            }

          }
          else
          {
            for( int i = 0 ; i <= 4 ; i++ )
            {
              lvItem.SubItems[ i ].ForeColor = Color.Gray;
            }
          }

          if( AllowedHosts.IsInternalUrl( OriginURL ) )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Gray;
          }
            
          if( AllowedHosts.IsInternalUrl( DestinationURL ) )
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Gray;
          }

        }

      }

    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
      this.DocumentCount.Text = string.Format( "Redirects: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/

  }

}
