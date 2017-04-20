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

  public sealed class MacroscopeDisplaySearchCollection : MacroscopeDisplayListView
  {

    /**************************************************************************/
        
    private ToolStripLabel DocumentCount;
        
    /**************************************************************************/

    public MacroscopeDisplaySearchCollection (
      MacroscopeMainForm MainForm,
      ListView TargetListView
    )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.DocumentCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionDocumentsNumber;
      
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

      string Title = msDoc.GetTitle();
      string Description = msDoc.GetDescription();
      string Keywords = msDoc.GetKeywords();

      string PairKey = string.Join( "", Url );

      ListViewItem lvItem = null;

      if( this.DisplayListView.Items.ContainsKey( PairKey ) )
      {

        try
        {

          lvItem = this.DisplayListView.Items[ PairKey ];
          lvItem.SubItems[ 0 ].Text = Url;
          lvItem.SubItems[ 1 ].Text = Title;
          lvItem.SubItems[ 2 ].Text = Description;
          lvItem.SubItems[ 3 ].Text = Keywords;

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplaySearchCollection 1: {0}", ex.Message ) );
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
          lvItem.SubItems.Add( Title );
          lvItem.SubItems.Add( Description );
          lvItem.SubItems.Add( Keywords );

          ListViewItems.Add( lvItem );

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "MacroscopeDisplaySearchCollection 2: {0}", ex.Message ) );
        }

      }

      this.DocumentCount.Text = string.Format( "Documents: {0}", DisplayListView.Items.Count );
              
    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
    }

    /**************************************************************************/

  }

}
