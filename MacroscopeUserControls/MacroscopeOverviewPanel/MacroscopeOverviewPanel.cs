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
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeOverviewPanel.
	/// </summary>

	public partial class MacroscopeOverviewPanel : UserControl
	{

		/**************************************************************************/

		MacroscopeColumnSorter lvColumnSorter;

		/**************************************************************************/

		public MacroscopeOverviewPanel ()
		{

			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();

			// TabPanel Properties
			tabControlMain.Multiline = false;

			this.SetStyle( ControlStyles.OptimizedDoubleBuffer, true );

			// ListView Docking

			tableLayoutPanelStructure.Dock = DockStyle.Fill;
			listViewStructure.Dock = DockStyle.Fill;

			treeViewHierarchy.Dock = DockStyle.Fill;
			listViewCanonicalAnalysis.Dock = DockStyle.Fill;
			listViewHrefLang.Dock = DockStyle.Fill;
			listViewErrors.Dock = DockStyle.Fill;
			listViewRedirectsAudit.Dock = DockStyle.Fill;
			listViewUriAnalysis.Dock = DockStyle.Fill;
			listViewPageTitles.Dock = DockStyle.Fill;
			listViewPageDescriptions.Dock = DockStyle.Fill;
			listViewPageKeywords.Dock = DockStyle.Fill;
			listViewPageHeadings.Dock = DockStyle.Fill;
			listViewStylesheets.Dock = DockStyle.Fill;
			listViewJavascripts.Dock = DockStyle.Fill;
			listViewImages.Dock = DockStyle.Fill;
			listViewAudios.Dock = DockStyle.Fill;
			listViewVideos.Dock = DockStyle.Fill;
			listViewRobots.Dock = DockStyle.Fill;
			listViewSitemaps.Dock = DockStyle.Fill;
			listViewEmailAddresses.Dock = DockStyle.Fill;
			listViewTelephoneNumbers.Dock = DockStyle.Fill;
			listViewHostnames.Dock = DockStyle.Fill;
			listViewHistory.Dock = DockStyle.Fill;

			tableLayoutPanelSearchCollection.Dock = DockStyle.Fill;
			listViewSearchCollection.Dock = DockStyle.Fill;

			// ListView Sorters
			lvColumnSorter = new MacroscopeColumnSorter ();

			listViewStructure.ListViewItemSorter = lvColumnSorter;
			listViewCanonicalAnalysis.ListViewItemSorter = lvColumnSorter;
			listViewHrefLang.ListViewItemSorter = lvColumnSorter;
			listViewErrors.ListViewItemSorter = lvColumnSorter;
			listViewRedirectsAudit.ListViewItemSorter = lvColumnSorter;
			listViewUriAnalysis.ListViewItemSorter = lvColumnSorter;
			listViewPageTitles.ListViewItemSorter = lvColumnSorter;
			listViewPageDescriptions.ListViewItemSorter = lvColumnSorter;
			listViewPageKeywords.ListViewItemSorter = lvColumnSorter;
			listViewPageHeadings.ListViewItemSorter = lvColumnSorter;
			listViewStylesheets.ListViewItemSorter = lvColumnSorter;
			listViewJavascripts.ListViewItemSorter = lvColumnSorter;
			listViewImages.ListViewItemSorter = lvColumnSorter;
			listViewAudios.ListViewItemSorter = lvColumnSorter;
			listViewVideos.ListViewItemSorter = lvColumnSorter;
			listViewRobots.ListViewItemSorter = lvColumnSorter;
			listViewSitemaps.ListViewItemSorter = lvColumnSorter;
			listViewEmailAddresses.ListViewItemSorter = lvColumnSorter;
			listViewTelephoneNumbers.ListViewItemSorter = lvColumnSorter;
			listViewHostnames.ListViewItemSorter = lvColumnSorter;
			listViewHistory.ListViewItemSorter = lvColumnSorter;
			listViewSearchCollection.ListViewItemSorter = lvColumnSorter;

			listViewStructure.ColumnClick += this.CallbackColumnClick;
			listViewCanonicalAnalysis.ColumnClick += this.CallbackColumnClick;
			listViewHrefLang.ColumnClick += this.CallbackColumnClick;
			listViewErrors.ColumnClick += this.CallbackColumnClick;
			listViewRedirectsAudit.ColumnClick += this.CallbackColumnClick;
			listViewUriAnalysis.ColumnClick += this.CallbackColumnClick;
			listViewPageTitles.ColumnClick += this.CallbackColumnClick;
			listViewPageDescriptions.ColumnClick += this.CallbackColumnClick;
			listViewPageKeywords.ColumnClick += this.CallbackColumnClick;
			listViewPageHeadings.ColumnClick += this.CallbackColumnClick;
			listViewStylesheets.ColumnClick += this.CallbackColumnClick;
			listViewJavascripts.ColumnClick += this.CallbackColumnClick;
			listViewImages.ColumnClick += this.CallbackColumnClick;
			listViewAudios.ColumnClick += this.CallbackColumnClick;
			listViewVideos.ColumnClick += this.CallbackColumnClick;
			listViewRobots.ColumnClick += this.CallbackColumnClick;
			listViewSitemaps.ColumnClick += this.CallbackColumnClick;
			listViewEmailAddresses.ColumnClick += this.CallbackColumnClick;
			listViewTelephoneNumbers.ColumnClick += this.CallbackColumnClick;
			listViewHostnames.ColumnClick += this.CallbackColumnClick;
			listViewHistory.ColumnClick += this.CallbackColumnClick;
			listViewSearchCollection.ColumnClick += this.CallbackColumnClick;

		}

		/**************************************************************************/

		void CallbackColumnClick ( object sender, ColumnClickEventArgs e )
		{

			ListView lvListView = sender as ListView;

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

			lvListView.Sort();

		}

		/**************************************************************************/

		[Conditional( "DEVMODE" )]
		public void DebugMsg ( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		/**************************************************************************/

	}

}
