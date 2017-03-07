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

  /// <summary>
  /// Description of MacroscopeDisplayDescriptions.
  /// </summary>

  public sealed class MacroscopeDisplayDescriptions : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayDescriptions ( MacroscopeMainForm MainForm, ListView lvListView )
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

      Boolean bProcess;

      if( msDoc.GetIsExternal() )
      {
        return;
      }

      if( msDoc.GetIsHtml() )
      {
        bProcess = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        bProcess = true;
      }
      else
      {
        bProcess = false;
      }

      if( bProcess )
      {

        string sDescription = msDoc.GetDescription();
        int iOccurrences = this.MainForm.GetJobMaster().GetDocCollection().GetStatsDescriptionCount( sDescription );
        int iDescriptionLength = msDoc.GetDescriptionLength();
        string sPairKey = string.Join( "", Url, sDescription );

        ListViewItem lvItem = null;

        if( this.lvListView.Items.ContainsKey( sPairKey ) )
        {

          try
          {

            lvItem = this.lvListView.Items[ sPairKey ];
            lvItem.SubItems[ 0 ].Text = Url;
            lvItem.SubItems[ 1 ].Text = iOccurrences.ToString();
            lvItem.SubItems[ 2 ].Text = sDescription;
            lvItem.SubItems[ 3 ].Text = iDescriptionLength.ToString();

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayDescriptions 1: {0}", ex.Message ) );
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
            lvItem.SubItems.Add( iOccurrences.ToString() );
            lvItem.SubItems.Add( sDescription );
            lvItem.SubItems.Add( iDescriptionLength.ToString() );

            this.lvListView.Items.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayDescriptions 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          lvItem.SubItems[ 0 ].ForeColor = Color.Green;
                    
          if( iDescriptionLength < MacroscopePreferencesManager.GetDescriptionMinLen() )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Red;
          }
          else
          if( iDescriptionLength > MacroscopePreferencesManager.GetDescriptionMaxLen() )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Green;
          }

        }

      }

    }

    /**************************************************************************/

  }
}
