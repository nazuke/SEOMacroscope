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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDisplayErrors.
  /// </summary>

  public sealed class MacroscopeDisplayErrors : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayErrors ( MacroscopeMainForm MainForm, ListView lvListView )
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
              this.RenderListView( DocCollection );
            }
          )
        );
      }
      else
      {
        this.RenderListView( DocCollection );
      }
    }

    /**************************************************************************/

    public new void RenderListView ( MacroscopeDocumentCollection DocCollection )
    {

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        Boolean bProceed = false;

        if( ( ( int )msDoc.GetStatusCode() >= 400 ) && ( ( int )msDoc.GetStatusCode() <= 499 ) )
        {
          bProceed = true;
        }
        else
        if( ( ( int )msDoc.GetStatusCode() >= 500 ) && ( ( int )msDoc.GetStatusCode() <= 599 ) )
        {
          bProceed = true;
        }

        if( bProceed )
        {
          this.RenderListView( msDoc, msDoc.GetUrl() );
        }
        else
        {
          RemoveFromListView( msDoc.GetUrl() );
        }

      }

    }

    /**************************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
    {

      string sPairKey = Url;
      string sStatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
      string sStatus = msDoc.GetStatusCode().ToString();
      ListViewItem lvItem = null;

      this.lvListView.BeginUpdate();

      if( this.lvListView.Items.ContainsKey( sPairKey ) )
      {

        try
        {

          lvItem = this.lvListView.Items[ sPairKey ];

          lvItem.SubItems[ 0 ].Text = Url;
          lvItem.SubItems[ 1 ].Text = sStatusCode;
          lvItem.SubItems[ 2 ].Text = sStatus;
          lvItem.SubItems[ 3 ].Text = msDoc.GetErrorCondition();

        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "MacroscopeDisplayErrors 1: {0}", ex.Message ) );
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
          lvItem.SubItems.Add( sStatusCode );
          lvItem.SubItems.Add( sStatus );
          lvItem.SubItems.Add( msDoc.GetErrorCondition() );

          this.lvListView.Items.Add( lvItem );

        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "MacroscopeDisplayErrors 2: {0}", ex.Message ) );
        }

      }

      if( lvItem != null )
      {

        lvItem.ForeColor = Color.Blue;

        if( Regex.IsMatch( sStatusCode, "^[2]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Green;
          lvItem.SubItems[ 2 ].ForeColor = Color.Green;
        }
        else
        if( Regex.IsMatch( sStatusCode, "^[3]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Goldenrod;
          lvItem.SubItems[ 2 ].ForeColor = Color.Goldenrod;
        }
        else
        if( Regex.IsMatch( sStatusCode, "^[45]" ) )
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Red;
          lvItem.SubItems[ 2 ].ForeColor = Color.Red;
        }
        else
        {
          lvItem.SubItems[ 1 ].ForeColor = Color.Blue;
          lvItem.SubItems[ 2 ].ForeColor = Color.Blue;
        }

      }

      this.lvListView.EndUpdate();

    }

    /**************************************************************************/

    private void RemoveFromListView ( string Url )
    {

      string sPairKey = Url;

      if( this.lvListView.Items.ContainsKey( sPairKey ) )
      {

        this.lvListView.BeginUpdate();

        this.lvListView.Items.Remove( this.lvListView.Items[ sPairKey ] );

        this.lvListView.EndUpdate();

      }

    }

    /**************************************************************************/

  }

}
