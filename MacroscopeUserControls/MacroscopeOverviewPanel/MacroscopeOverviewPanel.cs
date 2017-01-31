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

		public MacroscopeOverviewPanel ()
		{
 
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();

			// TabPanel Properties
			tabControlMain.Multiline = false;

			this.SetStyle( ControlStyles.OptimizedDoubleBuffer, true );			

			// ListView Docking
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
			listViewRobots.Dock = DockStyle.Fill;
			listViewSitemaps.Dock = DockStyle.Fill;
			listViewEmailAddresses.Dock = DockStyle.Fill;
			listViewTelephoneNumbers.Dock = DockStyle.Fill;
			listViewHostnames.Dock = DockStyle.Fill;
			listViewHistory.Dock = DockStyle.Fill;

			// ListView Sorters
			// TODO: Implement this.
			
		}
	
		/**************************************************************************/

		void CallbackColumnClick ( object sender, ColumnClickEventArgs e )
		{

			// TODO: Implement this.
			
			ListView lvListView = ( ListView )sender;

			DebugMsg( string.Format( "CallbackColumnClick lvListView: {0}", lvListView.ToString() ) );
			DebugMsg( string.Format( "CallbackColumnClick Column: {0}", e.Column.ToString() ) );

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
