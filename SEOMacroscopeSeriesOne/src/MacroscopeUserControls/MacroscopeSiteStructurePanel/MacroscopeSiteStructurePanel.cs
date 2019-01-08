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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeSiteStructurePanel.
  /// </summary>

  public partial class MacroscopeSiteStructurePanel : MacroscopeUserControl
  {

    /**************************************************************************/

    private MacroscopeColumnSorter lvColumnSorter;

    /**************************************************************************/

    public MacroscopeSiteStructurePanel ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      /** Column Sorters ******************************************************/

      this.lvColumnSorter = new MacroscopeColumnSorter();

      /** Site Overview *******************************************************/

      this.tabControlSiteStructure.Dock = DockStyle.Fill;
      this.splitContainerSiteOverview.Dock = DockStyle.Fill;
      this.treeViewSiteOverview.Dock = DockStyle.Fill;

      /** Keyword Analysis ****************************************************/

      this.tabControlKeywordAnalysisPhrases.Dock = DockStyle.Fill;

      this.listViewKeywordAnalysis1.Dock = DockStyle.Fill;
      this.listViewKeywordAnalysis2.Dock = DockStyle.Fill;
      this.listViewKeywordAnalysis3.Dock = DockStyle.Fill;
      this.listViewKeywordAnalysis4.Dock = DockStyle.Fill;

      this.listViewKeywordAnalysis1.ColumnClick += this.CallbackColumnClick;
      this.listViewKeywordAnalysis2.ColumnClick += this.CallbackColumnClick;
      this.listViewKeywordAnalysis3.ColumnClick += this.CallbackColumnClick;
      this.listViewKeywordAnalysis4.ColumnClick += this.CallbackColumnClick;

      /** Site Speed **********************************************************/

      this.tableLayoutPanelSiteSpeed.Dock = DockStyle.Fill;
      this.tabControlSiteSpeed.Dock = DockStyle.Fill;
      this.listViewSiteSpeedSlowest.Dock = DockStyle.Fill;
      this.listViewSiteSpeedFastest.Dock = DockStyle.Fill;

      this.listViewSiteSpeedSlowest.Columns[ 0 ].TextAlign = HorizontalAlignment.Right;
      this.listViewSiteSpeedFastest.Columns[ 0 ].TextAlign = HorizontalAlignment.Right;

      this.listViewSiteSpeedSlowest.Sorting = SortOrder.None;
      this.listViewSiteSpeedFastest.Sorting = SortOrder.None;

      this.toolStripLabelSiteSpeedAverage.Text = string.Format( "Average Response Time: {0:0.00}s", 0 );


      /** Charts **************************************************************/

      this.splitContainerSiteOverview.Panel2Collapsed = false;

      this.siteStructurePanelCharts.Dock = DockStyle.Fill;

    }

    /**************************************************************************/

    private void CallbackColumnClick ( object sender, ColumnClickEventArgs e )
    {

      try
      {

        ListView TargetListView = sender as ListView;

        TargetListView.ListViewItemSorter = this.lvColumnSorter;

        if( e.Column == lvColumnSorter.SortColumn )
        {
          if( lvColumnSorter.Order == SortOrder.Ascending )
          {
            lvColumnSorter.Order = SortOrder.Descending;
          }
          else
          {
            lvColumnSorter.Order = SortOrder.Ascending;
          }
        }
        else
        {
          lvColumnSorter.SortColumn = e.Column;
          lvColumnSorter.Order = SortOrder.Ascending;
        }

        TargetListView.Sort();

        TargetListView.ListViewItemSorter = null;

      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "CallbackColumnClick: {0}", ex.Message ) );
      }

    }

    /**************************************************************************/

  }

}
