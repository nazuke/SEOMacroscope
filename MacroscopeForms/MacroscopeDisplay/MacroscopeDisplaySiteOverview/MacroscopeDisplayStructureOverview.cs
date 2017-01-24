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
using System.Windows.Forms;

namespace SEOMacroscope
{
	
	/// <summary>
	/// Description of MacroscopeDisplayStructureOverview.
	/// </summary>
	
	public class MacroscopeDisplayStructureOverview : Macroscope
	{
	
		/**************************************************************************/

		public MacroscopeMainForm MainForm;

		public TreeView tvTreeView;
		Boolean TreeViewConfigured = false;
		
		Dictionary<string,int> IntStats;

		/**************************************************************************/

		public MacroscopeDisplayStructureOverview ( MacroscopeMainForm MainFormNew, TreeView tvTreeViewNew )
		{

			MainForm = MainFormNew;
			tvTreeView = tvTreeViewNew;

			IntStats = new Dictionary<string,int> ();

			IntStats.Add( "iUrlsFound", 0 );
			IntStats.Add( "iUrlsCrawled", 0 );

			ConfigureTreeView();

		}

		/**************************************************************************/
		
		void ConfigureTreeView ()
		{
			if( !TreeViewConfigured ) {
				this.ResetTreeView();
				this.TreeViewConfigured = true;
			}
		}
				
		/**************************************************************************/

		void ResetTreeView ()
		{
			this.tvTreeView.BeginUpdate();
			
			this.tvTreeView.Nodes.Clear();
			
			TreeNode nSummary = this.tvTreeView.Nodes.Add( "SUMMARY" );
			nSummary.Text = "Site Summary";
			nSummary.Nodes.Add( "iUrlsFound" ).Name = "iUrlsFound";
			nSummary.Nodes.Add( "iUrlsCrawled" ).Name = "iUrlsCrawled";
			
			this.tvTreeView.ExpandAll();

			this.tvTreeView.EndUpdate();
		}

		/**************************************************************************/

		public void RefreshData ( MacroscopeDocumentCollection DocCollection )
		{
			if( this.MainForm.InvokeRequired ) {
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.RenderTreeView( DocCollection );
						}
					)
				);
			} else {
				this.RenderTreeView( DocCollection );
			}
		}

		/**************************************************************************/

		public void RenderTreeView ( MacroscopeDocumentCollection DocCollection )
		{
			this.tvTreeView.BeginUpdate();
			this.RenderTreeViewSummary( DocCollection );
			this.tvTreeView.EndUpdate();
		}

		/**************************************************************************/

		void RenderTreeViewSummary ( MacroscopeDocumentCollection DocCollection )
		{

			MacroscopeJobMaster JobMaster = this.MainForm.GetJobMaster();

			{
				int iUrlsFound = JobMaster.CountHistory();
				TreeNode[] nUrlsFound = this.tvTreeView.Nodes.Find( "iUrlsFound", true );
				nUrlsFound[ 0 ].Text = string.Format( "Total URLs Found: {0}", iUrlsFound );
			}

			{
				int iUrlsCrawled = DocCollection.CountDocuments();
				TreeNode[] nUrlsCrawled = this.tvTreeView.Nodes.Find( "iUrlsCrawled", true );
				nUrlsCrawled[ 0 ].Text = string.Format( "Total URLs Crawled: {0}", iUrlsCrawled );
			}

		}

		/**************************************************************************/

	}

}
