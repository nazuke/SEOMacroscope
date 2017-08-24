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

    private MacroscopeMainForm MainForm;

    private TreeView tvTreeView;
   
    private Boolean TreeViewConfigured = false;

    private MacroscopeSiteStructurePanelCharts SiteStructurePanelCharts;

    /**************************************************************************/

    public MacroscopeDisplayStructureOverview ( MacroscopeMainForm MainForm, TreeView tvTreeView )
    {
      
      this.MainForm = MainForm;
      
      this.tvTreeView = tvTreeView;
      
      this.SiteStructurePanelCharts = this.MainForm.macroscopeSiteStructurePanelInstance.siteStructurePanelCharts;

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
                this.tvTreeView.NodeMouseClick += TreeViewSiteOverviewClick;
              }
            )
          );
        }
        else
        {
          this.ResetTreeView();
          this.tvTreeView.NodeMouseClick += TreeViewSiteOverviewClick;
        }
        this.TreeViewConfigured = true;
      }
    }

    /**************************************************************************/

    protected void TreeViewSiteOverviewClick ( object sender, TreeNodeMouseClickEventArgs e )
    {

      this.tvTreeView.BeginUpdate();

      DebugMsg( string.Format( "e.Node.Name: {0}", e.Node.Name ) );

      switch( e.Node.Name )
      {

        case "SUMMARY":
          this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsSiteSummary" );
          break;
        case "UrlsFound":
          this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsSiteSummary" );
          break;
        case "UrlsCrawled":
          this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsSiteSummary" );
          break;
        case "UrlsInternal":
          this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsSiteSummary" );
          break;
        case "UrlsExternal":
          this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsSiteSummary" );
          break;

        default:
          this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsSiteSummary" );
          break;

      }

      this.tvTreeView.EndUpdate();

      return;
      
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

        TreeNode Leaf = this.tvTreeView.Nodes.Add( "SUMMARY" );
        
        Leaf.Name = "SUMMARY";
        Leaf.Text = "Site Summary";
        Leaf.Nodes.Add( "Total URLs Found: 0" ).Name = "UrlsFound";
        Leaf.Nodes.Add( "Total URLs Crawled: 0" ).Name = "UrlsCrawled";
        Leaf.Nodes.Add( "Total Internal URLs: 0" ).Name = "UrlsInternal";
        Leaf.Nodes.Add( "Total External URLs: 0" ).Name = "UrlsExternal";

      }

      {

        TreeNode Leaf = this.tvTreeView.Nodes.Add( "RESPONSETIMES" );

        Leaf.Text = "Response Times";
        Leaf.Nodes.Add( "Fastest Page Response: 0.0 secs" ).Name = "FastestPageResponse";
        Leaf.Nodes.Add( "Slowest Page Response: 0.0 secs" ).Name = "SlowestPageResponse";
        Leaf.Nodes.Add( "Average Page Duration: 0.0 secs" ).Name = "AveragePageDuration";
        
      }
      
      {

        TreeNode Leaf = this.tvTreeView.Nodes.Add( "ROBOTS" );

        Leaf.Text = "Robots.txt";
        Leaf.Nodes.Add( "URLs Blocked by Robots.txt: 0" ).Name = "UrlsRobotsBlocked";
        
      }

      {

        TreeNode Leaf = this.tvTreeView.Nodes.Add( "SITEMAPS" );

        Leaf.Text = "Sitemaps";
        Leaf.Nodes.Add( "Sitemaps Found: 0" ).Name = "SitemapsFound";
        
      }

      {

        TreeNode Leaf = this.tvTreeView.Nodes.Add( "ISSUES" );

        Leaf.Text = "Fetch Issues";
        TreeNode nNodeWarnings = Leaf.Nodes.Add( "FETCH_WARNINGS" );
        nNodeWarnings.Name = "FETCH_WARNINGS";
        nNodeWarnings.Text = "Fetch Warnings";
        TreeNode nNodeErrors = Leaf.Nodes.Add( "FETCH_ERRORS" );
        nNodeErrors.Name = "FETCH_ERRORS";
        nNodeErrors.Text = "Fetch Errors";

      }

      {

        TreeNode Leaf = this.tvTreeView.Nodes.Add( "LANGUAGES_SPECIFIED" );

        Leaf.Text = "Languages Specified";

        TreeNode nNodeTitles = Leaf.Nodes.Add( "LANGUAGES_SPECIFIED_PAGES" );
        nNodeTitles.Name = "LANGUAGES_SPECIFIED_PAGES";
        nNodeTitles.Text = "Pages";

      }
      
      {

        TreeNode Leaf = this.tvTreeView.Nodes.Add( "LANGUAGES_DETECTED" );

        Leaf.Text = "Languages Detected";

        TreeNode LeafTitles = Leaf.Nodes.Add( "LANGUAGES_DETECTED_TITLES" );
        LeafTitles.Name = "LANGUAGES_DETECTED_TITLES";
        LeafTitles.Text = "Titles";

        TreeNode LeafDescriptions = Leaf.Nodes.Add( "LANGUAGES_DETECTED_DESCRIPTIONS" );
        LeafDescriptions.Name = "LANGUAGES_DETECTED_DESCRIPTIONS";
        LeafDescriptions.Text = "Descriptions";

        TreeNode LeafBodyTexts = Leaf.Nodes.Add( "LANGUAGES_DETECTED_BODYTEXTS" );
        LeafBodyTexts.Name = "LANGUAGES_DETECTED_BODYTEXTS";
        LeafBodyTexts.Text = "Contents";

      }

      {

        TreeNode Leaf = this.tvTreeView.Nodes.Add( "TEXT_READABILITY" );

        Leaf.Name = "TEXT_READABILITY";
        Leaf.Text = "Text Readability";
        
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

      this.tvTreeView.BeginUpdate();
      
      {
      
        SortedDictionary<string,double> DataPoints = new SortedDictionary<string,double> ();
      
        {
          int Count = JobMaster.GetPagesFound();
          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsFound", true );
          Leaf[ 0 ].Text = string.Format( "Total URLs Found: {0}", Count );
          DataPoints.Add( "URLs Found", ( double )Count );
        }

        {
          int Count = DocCollection.CountDocuments();
          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsCrawled", true );
          Leaf[ 0 ].Text = string.Format( "Total URLs Crawled: {0}", Count );
          DataPoints.Add( "URLs Crawled", ( double )Count );
        }

        {
          int Count = DocCollection.CountUrlsInternal();
          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsInternal", true );
          Leaf[ 0 ].Text = string.Format( "Total Internal URLs: {0}", Count );
          DataPoints.Add( "Internal URLs", ( double )Count );
        }

        {
          int Count = DocCollection.CountUrlsExternal();
          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsExternal", true );
          Leaf[ 0 ].Text = string.Format( "Total External URLs: {0}", Count );
          DataPoints.Add( "External URLs", ( double )Count );
        }

        this.SiteStructurePanelCharts.UpdateSiteSummary( DataPoints: DataPoints );

      }

      {
        decimal Fastest = DocCollection.GetStatsDurationsFastest();
        decimal Slowest = DocCollection.GetStatsDurationsSlowest();
        decimal Average = DocCollection.GetStatsDurationAverage();
        {
          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "FastestPageResponse", true );
          Leaf[ 0 ].Text = string.Format( "Fastest Page Response: {0:0.00} secs", Fastest );
        }
        {
          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "SlowestPageResponse", true );
          Leaf[ 0 ].Text = string.Format( "Slowest Page Response: {0:0.00} secs", Slowest );
        }
        {
          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "AveragePageDuration", true );
          Leaf[ 0 ].Text = string.Format( "Average Page Duration: {0:0.00} secs", Average );
        }
      }

      {
        int Count = JobMaster.GetBlockedByRobotsList().Count;
        TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsRobotsBlocked", true );
        Leaf[ 0 ].Text = string.Format( "URLs Blocked by Robots.txt: {0}", Count );
      }

      {
        int Count = DocCollection.CountUrlsSitemaps();
        TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "SitemapsFound", true );
        Leaf[ 0 ].Text = string.Format( "Sitemaps Found: {0}", Count );
      }

      {
        TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "FETCH_WARNINGS", true );
        TreeNode Leaf = Leaves[ 0 ];
        if( Leaf != null )
        {
          Leaf.Nodes.Clear();
          Dictionary<string,int> dicMessages = DocCollection.GetStatsWarningsCount();
          foreach( string MessagesKey in dicMessages.Keys )
          {
            Leaf.Nodes.Add( string.Format( "{0}: {1}", MessagesKey, dicMessages[ MessagesKey ] ) );
          }
        }
      }

      {
        TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "FETCH_ERRORS", true );
        TreeNode Leaf = Leaves[ 0 ];
        if( Leaf != null )
        {
          Leaf.Nodes.Clear();
          Dictionary<string,int> dicMessages = DocCollection.GetStatsErrorsCount();
          foreach( string MessagesKey in dicMessages.Keys )
          {
            Leaf.Nodes.Add( string.Format( "{0}: {1}", MessagesKey, dicMessages[ MessagesKey ] ) );
          }
        }
      }

      {
        TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "LANGUAGES_SPECIFIED_PAGES", true );
        TreeNode Leaf = Leaves[ 0 ];
        if( Leaf != null )
        {
          Leaf.Nodes.Clear();
          Dictionary<string,int> dicContents = DocCollection.GetStatsLanguagesPagesCount();
          foreach( string ContentKey in dicContents.Keys )
          {
            Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
          }
        }
      }

      {
        TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "LANGUAGES_DETECTED_TITLES", true );
        TreeNode Leaf = Leaves[ 0 ];
        if( Leaf != null )
        {
          Leaf.Nodes.Clear();
          Dictionary<string,int> dicContents = DocCollection.GetStatsLanguagesTitlesCount();
          foreach( string ContentKey in dicContents.Keys )
          {
            Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
          }
        }
      }

      {
        TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "LANGUAGES_DETECTED_DESCRIPTIONS", true );
        TreeNode Leaf = Leaves[ 0 ];
        if( Leaf != null )
        {
          Leaf.Nodes.Clear();
          Dictionary<string,int> dicContents = DocCollection.GetStatsLanguagesDescriptionsCount();
          foreach( string ContentKey in dicContents.Keys )
          {
            Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
          }
        }
      }

      {
        TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "LANGUAGES_DETECTED_BODYTEXTS", true );
        TreeNode Leaf = Leaves[ 0 ];
        if( Leaf != null )
        {
          Leaf.Nodes.Clear();
          Dictionary<string,int> dicContents = DocCollection.GetStatsLanguagesBodyTextsCount();
          foreach( string ContentKey in dicContents.Keys )
          {
            Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
          }
        }
      }
      
      {

        SortedDictionary<string,double> DataPoints = new SortedDictionary<string,double> ();
      



          
        
        TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "TEXT_READABILITY", true );
        TreeNode Leaf = Leaves[ 0 ];

        if( Leaf != null )
        {

          Leaf.Nodes.Clear();

          SortedDictionary<string,int> dicContents = DocCollection.GetStatsReadabilityGradeStringsCount();

          foreach( string ContentKey in dicContents.Keys )
          {

            Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
            
            DataPoints.Add( ContentKey, ( double )dicContents[ ContentKey ] );
            
            
          }
        }


        this.SiteStructurePanelCharts.UpdateReadability( DataPoints: DataPoints );

      }

      this.tvTreeView.EndUpdate();
            
    }

    /**************************************************************************/

  }

}
