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
    
    private Boolean TreeViewConfigured = false;

    /**************************************************************************/

    public MacroscopeDisplayStructureOverview ( MacroscopeMainForm MainForm, TreeView tvTreeView )
    {
      
      this.MainForm = MainForm;
      
      this.tvTreeView = tvTreeView;
      
      this.ConfigureTreeView();
    
    }

    /**************************************************************************/

    private void ConfigureTreeView ()
    {
      if( !this.TreeViewConfigured )
      {
        this.tvTreeView.PathSeparator = "/";
        if( this.MainForm.InvokeRequired )
        {
          this.MainForm.Invoke(
            new MethodInvoker (
              delegate
              {
                this.ResetTreeView();
              }
            )
          );
        }
        else
        {
          this.ResetTreeView();
        }
        this.TreeViewConfigured = true;
      }
    }

    /**************************************************************************/
    
    public void ClearData ()
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.ResetTreeView();
            }
          )
        );
      }
      else
      {
        this.ResetTreeView();
      }
    }

    /**************************************************************************/

    private void ResetTreeView ()
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
        TreeNode nNode = this.tvTreeView.Nodes.Add( "RESPONSETIMES" );
        nNode.Text = "Response Times";
        nNode.Nodes.Add( "Fastest Page Response: 0.0 secs" ).Name = "FastestPageResponse";
        nNode.Nodes.Add( "Slowest Page Response: 0.0 secs" ).Name = "SlowestPageResponse";
        nNode.Nodes.Add( "Average Page Duration: 0.0 secs" ).Name = "AveragePageDuration";
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
        TreeNode nNode = this.tvTreeView.Nodes.Add( "ISSUES" );
        nNode.Text = "Fetch Issues";
        TreeNode nNodeWarnings = nNode.Nodes.Add( "FETCH_WARNINGS" );
        nNodeWarnings.Name = "FETCH_WARNINGS";
        nNodeWarnings.Text = "Fetch Warnings";
        TreeNode nNodeErrors = nNode.Nodes.Add( "FETCH_ERRORS" );
        nNodeErrors.Name = "FETCH_ERRORS";
        nNodeErrors.Text = "Fetch Errors";
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
      this.tvTreeView.ExpandAll();
      this.tvTreeView.EndUpdate();
    }

    /**************************************************************************/

    private void RenderTreeViewSummary ( MacroscopeDocumentCollection DocCollection )
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
        decimal Fastest = DocCollection.GetStatsDurationsFastest();
        decimal Slowest = DocCollection.GetStatsDurationsSlowest();
        decimal Average = DocCollection.GetStatsDurationAverage();
        {
          TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "FastestPageResponse", true );
          tnNode[ 0 ].Text = string.Format( "Fastest Page Response: {0:0.00} secs", Fastest );
        }
        {
          TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "SlowestPageResponse", true );
          tnNode[ 0 ].Text = string.Format( "Slowest Page Response: {0:0.00} secs", Slowest );
        }
        {
          TreeNode [] tnNode = this.tvTreeView.Nodes.Find( "AveragePageDuration", true );
          tnNode[ 0 ].Text = string.Format( "Average Page Duration: {0:0.00} secs", Average );
        }
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
        TreeNode [] tnNodes = this.tvTreeView.Nodes.Find( "FETCH_WARNINGS", true );
        TreeNode tnNode = tnNodes[ 0 ];
        if( tnNode != null )
        {
          tnNode.Nodes.Clear();
          Dictionary<string,int> dicMessages = DocCollection.GetStatsWarningsCount();
          foreach( string MessagesKey in dicMessages.Keys )
          {
            tnNode.Nodes.Add( string.Format( "{0}: {1}", MessagesKey, dicMessages[ MessagesKey ] ) );
          }
        }
      }

      {
        TreeNode [] tnNodes = this.tvTreeView.Nodes.Find( "FETCH_ERRORS", true );
        TreeNode tnNode = tnNodes[ 0 ];
        if( tnNode != null )
        {
          tnNode.Nodes.Clear();
          Dictionary<string,int> dicMessages = DocCollection.GetStatsErrorsCount();
          foreach( string MessagesKey in dicMessages.Keys )
          {
            tnNode.Nodes.Add( string.Format( "{0}: {1}", MessagesKey, dicMessages[ MessagesKey ] ) );
          }
        }
      }

    }

    /**************************************************************************/

  }

}
