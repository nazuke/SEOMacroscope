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

  public sealed class MacroscopeDisplayCanonical : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayCanonical ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
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
      
      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      
      if( msDoc.GetIsHtml() )
      {

        string sCanonical = msDoc.GetCanonical();
        int StatusCode = msDoc.GetStatusCode();
        string sCanonicalLabel = sCanonical;
        ListViewItem lvItem = null;

        if( sCanonical.Length == 0 )
        {
          sCanonicalLabel = "MISSING";
        }

        this.lvListView.BeginUpdate();

        if( lvListView.Items.ContainsKey( sUrl ) )
        {

          try
          {

            lvItem = lvListView.Items[ sUrl ];
            lvItem.SubItems[ 0 ].Text = sUrl;
            lvItem.SubItems[ 1 ].Text = StatusCode.ToString();
            lvItem.SubItems[ 2 ].Text = sCanonicalLabel;

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayCanonical 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem ( sUrl );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = sUrl;

            lvItem.SubItems[ 0 ].Text = sUrl;
            lvItem.SubItems.Add( StatusCode.ToString() );
            lvItem.SubItems.Add( sCanonicalLabel );

            lvListView.Items.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayCanonical 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Gray;

          if( AllowedHosts.IsInternalUrl( sUrl ) )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.ForestGreen;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          }

          if( ( StatusCode >= 100 ) && ( StatusCode <= 299 ) )
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.ForestGreen;
          }
          else
          if( ( StatusCode >= 300 ) && ( StatusCode <= 399 ) )
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Orange;
          }
          else
          if( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
          }

          if( sCanonical.Length == 0 )
          {
            if( AllowedHosts.IsInternalUrl( sUrl ) )
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Red;
            }
            else
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
            }
          }
          else
          {
            if( AllowedHosts.IsInternalUrl( sCanonical ) )
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.ForestGreen;
            }
            else
            {
              lvItem.SubItems[ 2 ].ForeColor = Color.Red;
            }
          }
          
        }
              
        this.lvListView.EndUpdate();

      }

    }

    /**************************************************************************/

  }

}
