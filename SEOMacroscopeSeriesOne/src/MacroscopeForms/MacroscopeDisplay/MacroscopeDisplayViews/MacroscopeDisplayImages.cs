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
using System.Net;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDisplayImages.
  /// </summary>

  public sealed class MacroscopeDisplayImages : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColUrl = 0;
    private const int ColStatusCode = 1;
    private const int ColMimeType = 2;
    private const int ColFileSize = 3;
    
    /**************************************************************************/
        
    public MacroscopeDisplayImages ( MacroscopeMainForm MainForm, ListView TargetListView )
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

      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      if( !msDoc.GetIsImage() )
      {
        return;
      }

      string StatusCode = msDoc.GetStatusCode().ToString();
      string MimeType = msDoc.GetMimeType();
      string FileSize = msDoc.GetContentLength().ToString();

      string PairKey = string.Join( "", Url );

      ListViewItem lvItem = null;

      if( this.DisplayListView.Items.ContainsKey( PairKey ) )
      {

        try
        {

          lvItem = this.DisplayListView.Items[ PairKey ];
          lvItem.SubItems[ ColUrl ].Text = Url;
          lvItem.SubItems[ ColStatusCode ].Text = StatusCode;
          lvItem.SubItems[ ColMimeType ].Text = MimeType;
          lvItem.SubItems[ ColFileSize ].Text = FileSize;

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplayImages 1: {0}", ex.Message ) );
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
          lvItem.SubItems.Add( MimeType );
          lvItem.SubItems.Add( FileSize );

          ListViewItems.Add( lvItem );

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplayImages 2: {0}", ex.Message ) );
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

        // Status Code -------------------------------------------------------//

        if( msDoc.GetStatusCode() != HttpStatusCode.OK )
        {
          lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Red;
        }
        else
        {
          lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Green;
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
