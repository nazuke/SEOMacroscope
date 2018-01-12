/*

	This file is part of SEOMacroscope.

	Copyright 2018 Jason Holland.

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

  public sealed class MacroscopeDisplayStructureLinkCounts : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private ToolStripLabel DocumentCount;

    /**************************************************************************/

    public MacroscopeDisplayStructureLinkCounts (
      MacroscopeMainForm MainForm,
      ListView TargetListView
    )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.DocumentCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelStructureItems;
      
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

    /** Render One ************************************************************/

    protected override void RenderListView (
      List<ListViewItem> ListViewItems,
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc,
      string Url
    )
    {

      lock( this.DisplayListView )
      {
        
        ListViewItem lvItem = null;

        if( this.DisplayListView.Items.ContainsKey( Url ) )
        {

          lvItem = this.DisplayListView.Items[ Url ];

        }

        if( lvItem != null )
        {

          int ColIndexInlinks = this.DisplayListView.Columns.IndexOfKey( MacroscopeConstants.Inlinks );
          int ColIndexOutlinks = this.DisplayListView.Columns.IndexOfKey( MacroscopeConstants.Outlinks );     
          int ColIndexInhyperlinks = this.DisplayListView.Columns.IndexOfKey( MacroscopeConstants.Inhyperlinks );      
          int ColIndexOuthyperlinks = this.DisplayListView.Columns.IndexOfKey( MacroscopeConstants.Outhyperlinks );

          lvItem.SubItems[ ColIndexInlinks ].Text = msDoc.CountInlinks().ToString();
          lvItem.SubItems[ ColIndexOutlinks ].Text = msDoc.CountOutlinks().ToString();        
          lvItem.SubItems[ ColIndexInhyperlinks ].Text = msDoc.CountHyperlinksIn().ToString();       
          lvItem.SubItems[ ColIndexOuthyperlinks ].Text = msDoc.CountHyperlinksOut().ToString();

        }

      }

    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
      this.DocumentCount.Text = string.Format( "Documents: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/
  
  }

}
