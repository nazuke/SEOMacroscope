/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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

  public sealed class MacroscopeDisplayTelephoneNumbers : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayTelephoneNumbers ( MacroscopeMainForm MainForm, ListView TargetListView )
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

    protected override void RenderListView (
      List<ListViewItem> ListViewItems,
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc,
      string Url
    )
    {

      if( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
      {

        Dictionary<string,string> TelephoneNumbers = msDoc.GetTelephoneNumbers();
                 
        foreach( string TelephoneNumber in TelephoneNumbers.Keys )
        {

          string PairKey = string.Join( "", TelephoneNumber, Url );
          ListViewItem lvItem = null;
          
          if( this.DisplayListView.Items.ContainsKey( PairKey ) )
          {

            try
            {

              lvItem = this.DisplayListView.Items[ PairKey ];
              lvItem.SubItems[ 0 ].Text = TelephoneNumber;
              lvItem.SubItems[ 1 ].Text = Url;

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeDisplayTelephoneNumbers 1: {0}", ex.Message ) );
            }

          }
          else
          {

            try
            {

              lvItem = new ListViewItem ( PairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = PairKey;

              lvItem.SubItems[ 0 ].Text = TelephoneNumber;
              lvItem.SubItems.Add( Url );

              ListViewItems.Add( lvItem );

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeDisplayTelephoneNumbers 2: {0}", ex.Message ) );
            }

          }

          if( lvItem != null )
          {

            lvItem.ForeColor = Color.Blue;

            // URL -------------------------------------------------------------//
          
            if( msDoc.GetIsInternal() )
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ 1 ].ForeColor = Color.Gray;
            }

          }

        }

      }

    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
    }

    /**************************************************************************/

  }

}
