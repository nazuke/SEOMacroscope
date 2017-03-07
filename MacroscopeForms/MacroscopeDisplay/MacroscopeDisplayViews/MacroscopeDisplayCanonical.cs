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
using System.Net;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayCanonical : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayCanonical ( MacroscopeMainForm MainForm, ListView lvListView )
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

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
    {
      
      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      
      if( msDoc.GetIsHtml() )
      {

        string sCanonical = msDoc.GetCanonical();
        HttpStatusCode StatusCode = msDoc.GetStatusCode();
        string sCanonicalLabel = sCanonical;
        ListViewItem lvItem = null;

        if( sCanonical.Length == 0 )
        {
          sCanonicalLabel = "MISSING";
        }

        this.lvListView.BeginUpdate();

        if( lvListView.Items.ContainsKey( Url ) )
        {

          try
          {

            lvItem = lvListView.Items[ Url ];
            lvItem.SubItems[ 0 ].Text = Url;
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

            lvItem = new ListViewItem ( Url );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = Url;

            lvItem.SubItems[ 0 ].Text = Url;
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

          if( AllowedHosts.IsInternalUrl( Url ) )
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
          }

          if( ( (int)StatusCode >= 100 ) && ( (int)StatusCode <= 299 ) )
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Green;
          }
          else
          if( ( (int)StatusCode >= 300 ) && ( (int)StatusCode <= 399 ) )
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Orange;
          }
          else
          if( ( (int)StatusCode >= 400 ) && ( (int)StatusCode <= 599 ) )
          {
            lvItem.SubItems[ 1 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
          }

          if( sCanonical.Length == 0 )
          {
            if( AllowedHosts.IsInternalUrl( Url ) )
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
              lvItem.SubItems[ 2 ].ForeColor = Color.Green;
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
