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
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeOverviewPanel.
  /// </summary>

  public partial class MacroscopeOverviewPanel : MacroscopeUserControl
  {

    /**************************************************************************/

    private MacroscopeColumnSorter lvColumnSorter;

    /**************************************************************************/

    public MacroscopeOverviewPanel ()
    {

      // The InitializeComponent() call is required for Windows Forms designer support.
      InitializeComponent();

      // TabPanel Properties
      this.tabControlMain.Multiline = true;

      this.SetStyle( ControlStyles.OptimizedDoubleBuffer, true );

      // ListView Docking

      this.tableLayoutPanelStructure.Dock = DockStyle.Fill;
      this.listViewStructure.Dock = DockStyle.Fill;

      this.treeViewHierarchy.Dock = DockStyle.Fill;

      this.tableLayoutPanelRobots.Dock = DockStyle.Fill;
      this.listViewRobots.Dock = DockStyle.Fill;

      this.listViewSitemaps.Dock = DockStyle.Fill;
      this.listViewSitemapErrors.Dock = DockStyle.Fill;
      this.listViewSitemapsAudit.Dock = DockStyle.Fill;

      this.listViewCanonicalAnalysis.Dock = DockStyle.Fill;
      this.listViewHrefLang.Dock = DockStyle.Fill;

      this.tableLayoutPanelErrors.Dock = DockStyle.Fill;
      this.listViewErrors.Dock = DockStyle.Fill;

      this.tableLayoutPanelHostnames.Dock = DockStyle.Fill;
      this.listViewHostnames.Dock = DockStyle.Fill;

      this.tableLayoutPanelRedirects.Dock = DockStyle.Fill;
      this.listViewRedirectsAudit.Dock = DockStyle.Fill;

      this.tableLayoutPanelRedirectChains.Dock = DockStyle.Fill;
      this.listViewRedirectChains.Dock = DockStyle.Fill;

      this.tableLayoutPanelLinks.Dock = DockStyle.Fill;
      this.listViewLinks.Dock = DockStyle.Fill;

      this.tableLayoutPanelHyperlinks.Dock = DockStyle.Fill;
      this.listViewHyperlinks.Dock = DockStyle.Fill;

      this.listViewUriAnalysis.Dock = DockStyle.Fill;
      this.listViewOrphanedPages.Dock = DockStyle.Fill;

      this.listViewPageTitles.Dock = DockStyle.Fill;
      this.listViewPageDescriptions.Dock = DockStyle.Fill;
      this.listViewPageKeywords.Dock = DockStyle.Fill;
      this.listViewPageHeadings.Dock = DockStyle.Fill;
      this.listViewPageText.Dock = DockStyle.Fill;
      this.listViewStylesheets.Dock = DockStyle.Fill;
      this.listViewJavascripts.Dock = DockStyle.Fill;
      this.listViewImages.Dock = DockStyle.Fill;
      this.listViewAudios.Dock = DockStyle.Fill;
      this.listViewVideos.Dock = DockStyle.Fill;
      this.listViewEmailAddresses.Dock = DockStyle.Fill;
      this.listViewTelephoneNumbers.Dock = DockStyle.Fill;

      this.tableLayoutPanelCustomFilters.Dock = DockStyle.Fill;
      this.listViewCustomFilters.Dock = DockStyle.Fill;

      this.tabControlDataExtractors.Dock = DockStyle.Fill;
      this.tableLayoutPanelDataExtractorCssSelectors.Dock = DockStyle.Fill;
      this.listViewDataExtractorCssSelectors.Dock = DockStyle.Fill;
      this.tableLayoutPanelDataExtractorRegexes.Dock = DockStyle.Fill;
      this.listViewDataExtractorRegexes.Dock = DockStyle.Fill;
      this.tableLayoutPanelDataExtractorXpaths.Dock = DockStyle.Fill;
      this.listViewDataExtractorXpaths.Dock = DockStyle.Fill;

      this.tableLayoutPanelRemarks.Dock = DockStyle.Fill;
      this.listViewRemarks.Dock = DockStyle.Fill;

      this.tableLayoutPanelUriQueue.Dock = DockStyle.Fill;
      this.listViewUriQueue.Dock = DockStyle.Fill;

      this.tableLayoutPanelHistory.Dock = DockStyle.Fill;
      this.listViewHistory.Dock = DockStyle.Fill;

      this.tableLayoutPanelSearchCollection.Dock = DockStyle.Fill;
      this.listViewSearchCollection.Dock = DockStyle.Fill;

      // ListView Sorters
      this.lvColumnSorter = new MacroscopeColumnSorter();

      this.listViewStructure.ColumnClick += this.CallbackColumnClick;
      this.listViewRobots.ColumnClick += this.CallbackColumnClick;
      this.listViewSitemaps.ColumnClick += this.CallbackColumnClick;
      this.listViewSitemapErrors.ColumnClick += this.CallbackColumnClick;
      this.listViewSitemapsAudit.ColumnClick += this.CallbackColumnClick;
      this.listViewCanonicalAnalysis.ColumnClick += this.CallbackColumnClick;
      this.listViewHrefLang.ColumnClick += this.CallbackColumnClick;
      this.listViewErrors.ColumnClick += this.CallbackColumnClick;
      this.listViewHostnames.ColumnClick += this.CallbackColumnClick;
      this.listViewRedirectsAudit.ColumnClick += this.CallbackColumnClick;
      this.listViewRedirectChains.ColumnClick += this.CallbackColumnClick;
      this.listViewLinks.ColumnClick += this.CallbackColumnClick;
      this.listViewHyperlinks.ColumnClick += this.CallbackColumnClick;
      this.listViewUriAnalysis.ColumnClick += this.CallbackColumnClick;
      this.listViewOrphanedPages.ColumnClick += this.CallbackColumnClick;
      this.listViewPageTitles.ColumnClick += this.CallbackColumnClick;
      this.listViewPageDescriptions.ColumnClick += this.CallbackColumnClick;
      this.listViewPageKeywords.ColumnClick += this.CallbackColumnClick;
      this.listViewPageHeadings.ColumnClick += this.CallbackColumnClick;
      this.listViewPageText.ColumnClick += this.CallbackColumnClick;
      this.listViewStylesheets.ColumnClick += this.CallbackColumnClick;
      this.listViewJavascripts.ColumnClick += this.CallbackColumnClick;
      this.listViewImages.ColumnClick += this.CallbackColumnClick;
      this.listViewAudios.ColumnClick += this.CallbackColumnClick;
      this.listViewVideos.ColumnClick += this.CallbackColumnClick;
      this.listViewEmailAddresses.ColumnClick += this.CallbackColumnClick;
      this.listViewTelephoneNumbers.ColumnClick += this.CallbackColumnClick;
      this.listViewCustomFilters.ColumnClick += this.CallbackColumnClick;
      this.listViewDataExtractorCssSelectors.ColumnClick += this.CallbackColumnClick;
      this.listViewDataExtractorRegexes.ColumnClick += this.CallbackColumnClick;
      this.listViewDataExtractorXpaths.ColumnClick += this.CallbackColumnClick;

      this.listViewRemarks.ColumnClick += this.CallbackColumnClick;

      this.listViewUriQueue.ColumnClick += this.CallbackColumnClick;
      this.listViewHistory.ColumnClick += this.CallbackColumnClick;
      this.listViewSearchCollection.ColumnClick += this.CallbackColumnClick;

    }

    /**************************************************************************/

    private void CallbackColumnClick ( object sender, ColumnClickEventArgs e )
    {

      try
      {

        ListView TargetListView = sender as ListView;

        TargetListView.ListViewItemSorter = this.lvColumnSorter;

        if( e.Column == this.lvColumnSorter.SortColumn )
        {
          if( this.lvColumnSorter.Order == SortOrder.Ascending )
          {
            this.lvColumnSorter.Order = SortOrder.Descending;
          }
          else
          {
            this.lvColumnSorter.Order = SortOrder.Ascending;
          }
        }
        else
        {
          this.lvColumnSorter.SortColumn = e.Column;
          this.lvColumnSorter.Order = SortOrder.Ascending;
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

    [Conditional( "DEVMODE" )]
    new public void DebugMsg ( String Msg )
    {
      System.Diagnostics.Debug.WriteLine( Msg );
    }

    /**************************************************************************/

  }

}
