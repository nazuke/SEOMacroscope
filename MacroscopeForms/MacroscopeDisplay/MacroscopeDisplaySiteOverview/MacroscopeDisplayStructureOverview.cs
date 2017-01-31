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
		
		/**************************************************************************/

		public MacroscopeDisplayStructureOverview ( MacroscopeMainForm MainFormNew, TreeView tvTreeViewNew )
		{
			MainForm = MainFormNew;
			tvTreeView = tvTreeViewNew;
			ConfigureTreeView();
		}

		/**************************************************************************/
		
		void ConfigureTreeView ()
		{
			if( !TreeViewConfigured )
			{
				this.tvTreeView.PathSeparator = "/";
				this.ResetTreeView();
				this.TreeViewConfigured = true;
			}
		}
				
		/**************************************************************************/

		void ResetTreeView ()
		{
			this.tvTreeView.BeginUpdate();
			
			this.tvTreeView.Nodes.Clear();
			
			{
			
				TreeNode nNode = this.tvTreeView.Nodes.Add( "SUMMARY" );
				nNode.Text = "Site Summary";
				nNode.Nodes.Add( "Total URLs Found: 0" ).Name = "UrlsFound";
				nNode.Nodes.Add( "Total URLs Crawled: 0" ).Name = "UrlsCrawled";
				nNode.Nodes.Add( "Total Internal URLs: 0" ).Name = "UrlsInternal";
				nNode.Nodes.Add( "Total External URLs: 0" ).Name = "UrlsExternal";
			}
			
			{
				TreeNode nNode = this.tvTreeView.Nodes.Add( "ROBOTS" );
				nNode.Text = "Robots.txt";
				nNode.Nodes.Add( "URLs Blocked by Robots.txt: 0" ).Name = "UrlsRobotsBlocked";
			}
			
			{
				TreeNode nNode = this.tvTreeView.Nodes.Add( "SITEMAPS" );
				nNode.Text = "Sitemaps";
				nNode.Nodes.Add( "Sitemaps Found: 0" ).Name = "SitemapsFound";
			}
	
			{
				TreeNode nNode = this.tvTreeView.Nodes.Add( "ERRORS" );
				nNode.Text = "Fetch Errors";
			}
			
			this.tvTreeView.ExpandAll();

			this.tvTreeView.EndUpdate();
		}

		/**************************************************************************/

		public void RefreshData ( MacroscopeDocumentCollection DocCollection )
		{
			if( this.MainForm.InvokeRequired )
			{
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.RenderTreeView( DocCollection );
						}
					)
				);
			}
			else
			{
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
				int iCount = JobMaster.GetPagesFound();
				TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "UrlsFound", true );
				tnNode[ 0 ].Text = string.Format( "Total URLs Found: {0}", iCount );
			}

			{
				int iCount = DocCollection.CountDocuments();
				TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "UrlsCrawled", true );
				tnNode[ 0 ].Text = string.Format( "Total URLs Crawled: {0}", iCount );
			}

			{
				int iCount = DocCollection.CountUrlsInternal();
				TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "UrlsInternal", true );
				tnNode[ 0 ].Text = string.Format( "Total Internal URLs: {0}", iCount );
			}
			
			{
				int iCount = DocCollection.CountUrlsExternal();
				TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "UrlsExternal", true );
				tnNode[ 0 ].Text = string.Format( "Total External URLs: {0}", iCount );
			}
			
			{
				int iCount = JobMaster.GetBlockedByRobotsList().Count;
				TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "UrlsRobotsBlocked", true );
				tnNode[ 0 ].Text = string.Format( "URLs Blocked by Robots.txt: {0}", iCount );
			}
	
			{
				int iCount = DocCollection.CountUrlsSitemaps();
				TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "SitemapsFound", true );
				tnNode[ 0 ].Text = string.Format( "Sitemaps Found: {0}", iCount );
			}

			{
				TreeNode tnNodeErrors = this.tvTreeView.Nodes[ "ERRORS" ];
				if( tnNodeErrors != null )
				{
					tnNodeErrors.Nodes.Clear();
					Dictionary<string,int> dicErrors = DocCollection.GetStatsErrorsCount();
					foreach( string sErrorCondition in dicErrors.Keys )
					{
						tnNodeErrors.Nodes.Add( string.Format( "{0}: {1}", sErrorCondition, dicErrors[ sErrorCondition ] ) );
					}
				}
			}

		}

		/**************************************************************************/

	}

}
