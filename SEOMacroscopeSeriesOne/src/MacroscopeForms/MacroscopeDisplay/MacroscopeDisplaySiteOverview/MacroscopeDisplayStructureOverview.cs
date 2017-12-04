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
      
      this.SuppressDebugMsg = true;
      
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

      if( e.Node.Tag != null )
      {

        this.tvTreeView.BeginUpdate();
              
        switch( e.Node.Tag.ToString() )
        {

        /** SUMMARY ***********************************************************/

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
            
        /** RESPONSE TIMES ****************************************************/
                    
          case "RESPONSETIMES":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsResponseTimes" );
            break;
          case "FastestPageResponse":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsResponseTimes" );
            break;
          case "SlowestPageResponse":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsResponseTimes" );
            break;
          case "AveragePageDuration":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsResponseTimes" );
            break;

        /** LANGUAGES SPECIFIED ***********************************************/
            
          case "LANGUAGES_SPECIFIED":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsLanguagesSpecified" );
            break;
          case "LANGUAGES_SPECIFIED_PAGES":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsLanguagesSpecified" );
            break;
          case "LANGUAGES_SPECIFIED_PAGES_LANG":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsLanguagesSpecified" );
            break;

        /** READABILITY *******************************************************/

          case "TEXT_READABILITY":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsReadability" );
            break;
          case "TEXT_READABILITY_NODE":
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsReadability" );
            break;

        /** DEFAULT ***********************************************************/

          default:
            this.SiteStructurePanelCharts.SelectTabPage( TabName: "tabPageChartsSiteSummary" );
            break;

        }

        this.tvTreeView.EndUpdate();

      }

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

      try
      {

        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "SUMMARY" );
        
          Leaf.Name = "SUMMARY";
          Leaf.Tag = "SUMMARY";
          Leaf.Text = "Site Summary";

          Leaf.Nodes.Add( "Total URLs Found: 0" ).Tag = "UrlsFound";
          Leaf.Nodes.Add( "Total URLs Crawled: 0" ).Tag = "UrlsCrawled";
          Leaf.Nodes.Add( "Total Internal URLs: 0" ).Tag = "UrlsInternal";
          Leaf.Nodes.Add( "Total External URLs: 0" ).Tag = "UrlsExternal";
          
          this.CopyLeafNodeTagToName( Leaf: Leaf );

        }

        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "RESPONSETIMES" );

          Leaf.Tag = "RESPONSETIMES";
          Leaf.Text = "Response Times";

          Leaf.Nodes.Add( "Fastest Page Response: 0.0 secs" ).Tag = "FastestPageResponse";
          Leaf.Nodes.Add( "Slowest Page Response: 0.0 secs" ).Tag = "SlowestPageResponse";
          Leaf.Nodes.Add( "Average Page Duration: 0.0 secs" ).Tag = "AveragePageDuration";
        
          this.CopyLeafNodeTagToName( Leaf: Leaf );
                    
        }
      
        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "ROBOTS" );

          Leaf.Tag = "ROBOTS";
          Leaf.Text = "Robots.txt";

          Leaf.Nodes.Add( "URLs Blocked by Robots.txt: 0" ).Tag = "UrlsRobotsBlocked";
        
          this.CopyLeafNodeTagToName( Leaf: Leaf );
                    
        }

        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "SITEMAPS" );

          Leaf.Tag = "SITEMAPS";
          Leaf.Text = "Sitemaps";

          Leaf.Nodes.Add( "Sitemaps Found: 0" ).Tag = "SitemapsFound";
        
          this.CopyLeafNodeTagToName( Leaf: Leaf );
                    
        }

        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "ISSUES" );

          Leaf.Tag = "ISSUES";
          Leaf.Text = "Fetch Issues";

          TreeNode LeafWarnings = Leaf.Nodes.Add( "FETCH_WARNINGS" );

          LeafWarnings.Tag = "FETCH_WARNINGS";
          LeafWarnings.Name = "FETCH_WARNINGS";
          LeafWarnings.Text = "Fetch Warnings";

          TreeNode LeafErrors = Leaf.Nodes.Add( "FETCH_ERRORS" );

          LeafErrors.Tag = "FETCH_ERRORS";
          LeafErrors.Name = "FETCH_ERRORS";
          LeafErrors.Text = "Fetch Errors";

          this.CopyLeafNodeTagToName( Leaf: Leaf );
                    
        }

        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "CANONICALS_SPECIFIED" );

          Leaf.Tag = "CANONICALS_SPECIFIED";
          Leaf.Text = "Canonicals Specified";

          TreeNode LeafSpecified = Leaf.Nodes.Add( "CANONICALS_SPECIFIED_SPECIFIED" );

          LeafSpecified.Tag = "CANONICALS_SPECIFIED_SPECIFIED";
          LeafSpecified.Name = "CANONICALS_SPECIFIED_SPECIFIED";
          LeafSpecified.Text = "Specified: 0";

          TreeNode LeafNotSpecified = Leaf.Nodes.Add( "CANONICALS_SPECIFIED_NOT_SPECIFIED" );

          LeafNotSpecified.Tag = "CANONICALS_SPECIFIED_NOT_SPECIFIED";
          LeafNotSpecified.Name = "CANONICALS_SPECIFIED_NOT_SPECIFIED";
          LeafNotSpecified.Text = "Not Specified: 0";

          this.CopyLeafNodeTagToName( Leaf: Leaf );
                    
        }

        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "LANGUAGES_SPECIFIED" );

          Leaf.Tag = "LANGUAGES_SPECIFIED";
          Leaf.Text = "Languages Specified";

          TreeNode LeafTitles = Leaf.Nodes.Add( "LANGUAGES_SPECIFIED_PAGES" );

          LeafTitles.Tag = "LANGUAGES_SPECIFIED_PAGES";
          LeafTitles.Name = "LANGUAGES_SPECIFIED_PAGES";
          LeafTitles.Text = "Pages";

          this.CopyLeafNodeTagToName( Leaf: Leaf );
                    
        }
      
        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "LANGUAGES_DETECTED" );

          Leaf.Tag = "LANGUAGES_DETECTED";
          Leaf.Text = "Languages Detected";

          TreeNode LeafTitles = Leaf.Nodes.Add( "LANGUAGES_DETECTED_TITLES" );

          LeafTitles.Tag = "LANGUAGES_DETECTED_TITLES";
          LeafTitles.Name = "LANGUAGES_DETECTED_TITLES";
          LeafTitles.Text = "Titles";

          TreeNode LeafDescriptions = Leaf.Nodes.Add( "LANGUAGES_DETECTED_DESCRIPTIONS" );

          LeafDescriptions.Tag = "LANGUAGES_DETECTED_DESCRIPTIONS";
          LeafDescriptions.Name = "LANGUAGES_DETECTED_DESCRIPTIONS";
          LeafDescriptions.Text = "Descriptions";

          TreeNode LeafBodyTexts = Leaf.Nodes.Add( "LANGUAGES_DETECTED_BODYTEXTS" );

          LeafBodyTexts.Tag = "LANGUAGES_DETECTED_BODYTEXTS";
          LeafBodyTexts.Name = "LANGUAGES_DETECTED_BODYTEXTS";
          LeafBodyTexts.Text = "Contents";

          this.CopyLeafNodeTagToName( Leaf: Leaf );
                    
        }

        {

          TreeNode Leaf = this.tvTreeView.Nodes.Add( "TEXT_READABILITY" );

          Leaf.Tag = "TEXT_READABILITY";
          Leaf.Name = "TEXT_READABILITY";
          Leaf.Text = "Text Readability";
        
          this.CopyLeafNodeTagToName( Leaf: Leaf );
                    
        }

      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      this.tvTreeView.ExpandAll();

      this.tvTreeView.EndUpdate();

      this.SiteStructurePanelCharts.ClearAll();
      
      return;
      
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
      
      try
      {
      
        {
      
          SortedDictionary<string,double> DataPoints = new SortedDictionary<string,double> ();
      
          {
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsFound", true );
            int Count = JobMaster.GetPagesFound();
            if( Leaf.Length > 0 )
            {
              Leaf[ 0 ].Text = string.Format( "Total URLs Found: {0}", Count );
            }
            DataPoints.Add( "URLs Found", ( double )Count );
          }

          {
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsCrawled", true );
            int Count = DocCollection.CountDocuments();
            if( Leaf.Length > 0 )
            {
              Leaf[ 0 ].Text = string.Format( "Total URLs Crawled: {0}", Count );
            }
            DataPoints.Add( "URLs Crawled", ( double )Count );
          }

          {
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsInternal", true );
            int Count = DocCollection.CountUrlsInternal();
            if( Leaf.Length > 0 )
            {
              Leaf[ 0 ].Text = string.Format( "Total Internal URLs: {0}", Count );
            }
            DataPoints.Add( "Internal URLs", ( double )Count );
          }

          {
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsExternal", true );
            int Count = DocCollection.CountUrlsExternal();
            if( Leaf.Length > 0 )
            {
              Leaf[ 0 ].Text = string.Format( "Total External URLs: {0}", Count );
            }
            DataPoints.Add( "External URLs", ( double )Count );
          }

          this.SiteStructurePanelCharts.UpdateSiteSummary( DataPoints: DataPoints );

        }

        {

          SortedDictionary<string,double> DataPoints = new SortedDictionary<string,double> ();
          decimal Fastest = DocCollection.GetStatsDurationsFastest();
          decimal Slowest = DocCollection.GetStatsDurationsSlowest();
          decimal Average = DocCollection.GetStatsDurationAverage();

          {
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "FastestPageResponse", true );
            if( Leaf.Length > 0 )
            {
              Leaf[ 0 ].Text = string.Format( "Fastest Page Response: {0:0.00} secs", Fastest );
              DataPoints.Add( "Fastest Page Response", ( double )Fastest );
            }
            else
            {
              DataPoints.Add( "Fastest Page Response", 0 );
            }
          }

          {
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "SlowestPageResponse", true );
            if( Leaf.Length > 0 )
            {
              Leaf[ 0 ].Text = string.Format( "Slowest Page Response: {0:0.00} secs", Slowest );
              DataPoints.Add( "Slowest Page Response", ( double )Slowest );
            }
            else
            {
              DataPoints.Add( "Slowest Page Response", 0 );
            }
          }

          {
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "AveragePageDuration", true );
            if( Leaf.Length > 0 )
            {
              Leaf[ 0 ].Text = string.Format( "Average Page Duration: {0:0.00} secs", Average );
              DataPoints.Add( "Average Page Duration", ( double )Average );
            }
            else
            {
              DataPoints.Add( "Average Page Duration", 0 );
            }
          }

          this.SiteStructurePanelCharts.UpdateResponseTimes( DataPoints: DataPoints );
                    
        }

        {

          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "UrlsRobotsBlocked", true );
          int Count = JobMaster.GetBlockedByRobotsList().Count;

          if( Leaf.Length > 0 )
          {
            Leaf[ 0 ].Text = string.Format( "URLs Blocked by Robots.txt: {0}", Count );
          }
          
        }

        {

          TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "SitemapsFound", true );
          int Count = DocCollection.CountUrlsSitemaps();

          if( Leaf.Length > 0 )
          {
            Leaf[ 0 ].Text = string.Format( "Sitemaps Found: {0}", Count );
          }
        
        }

        {

          TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "FETCH_WARNINGS", true );

          if( Leaves.Length > 0 )
          {

            TreeNode Leaf = Leaves[ 0 ];

            if( Leaf != null )
            {

              Dictionary<string,int> dicMessages = DocCollection.GetStatsWarningsCount();

              Leaf.Nodes.Clear();

              foreach( string MessagesKey in dicMessages.Keys )
              {
                Leaf.Nodes.Add( string.Format( "{0}: {1}", MessagesKey, dicMessages[ MessagesKey ] ) );
              }

            }
        
          }

        }

        {

          TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "FETCH_ERRORS", true );

          if( Leaves.Length > 0 )
          {

            TreeNode Leaf = Leaves[ 0 ];

            if( Leaf != null )
            {

              Dictionary<string,int> dicMessages = DocCollection.GetStatsErrorsCount();

              Leaf.Nodes.Clear();

              foreach( string MessagesKey in dicMessages.Keys )
              {
                Leaf.Nodes.Add( string.Format( "{0}: {1}", MessagesKey, dicMessages[ MessagesKey ] ) );
              }

            }
          
          }

        }

        {

          Dictionary<Boolean,int> Canonicals = DocCollection.GetStatsCanonicalsCount();

          {
            
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "CANONICALS_SPECIFIED_SPECIFIED", true );
            
            if( ( Leaf.Length > 0 ) && ( Canonicals.ContainsKey( true ) ) )
            {
              Leaf[ 0 ].Text = string.Format( "Specified: {0}", Canonicals[ true ] );
            }
            else
            {
              Leaf[ 0 ].Text = string.Format( "Specified: {0}", 0 );
            }
          
          }

          {
          
            TreeNode [] Leaf = this.tvTreeView.Nodes.Find( "CANONICALS_SPECIFIED_NOT_SPECIFIED", true );
          
            if( ( Leaf.Length > 0 ) && ( Canonicals.ContainsKey( false ) ) )
            {
              Leaf[ 0 ].Text = string.Format( "Not Specified: {0}", Canonicals[ false ] );
            }
            else
            {
              Leaf[ 0 ].Text = string.Format( "Not Specified: {0}", 0 );
            }
          
          }

        }

        {

          SortedDictionary<string,double> DataPoints = new SortedDictionary<string,double> ();
          TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "LANGUAGES_SPECIFIED_PAGES", true );

          if( Leaves.Length > 0 )
          {

            TreeNode Leaf = Leaves[ 0 ];

            if( Leaf != null )
            {

              Dictionary<string,int> dicContents = DocCollection.GetStatsLanguagesPagesCount();

              Leaf.Nodes.Clear();

              foreach( string ContentKey in dicContents.Keys )
              {

                TreeNode LeafNode = Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
                LeafNode.Tag = "LANGUAGES_SPECIFIED_PAGES_LANG";
               
                DataPoints.Add( ContentKey, ( double )dicContents[ ContentKey ] );
                            
              }
              
            }
    
            this.SiteStructurePanelCharts.UpdateLanguagesSpecified( DataPoints: DataPoints );
            
          }

        }

        {

          TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "LANGUAGES_DETECTED_TITLES", true );

          if( Leaves.Length > 0 )
          {

            TreeNode Leaf = Leaves[ 0 ];

            if( Leaf != null )
            {

              Dictionary<string,int> dicContents = DocCollection.GetStatsLanguagesTitlesCount();

              Leaf.Nodes.Clear();

              foreach( string ContentKey in dicContents.Keys )
              {
                Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
              }

            }
        
          }

        }

        {

          TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "LANGUAGES_DETECTED_DESCRIPTIONS", true );

          if( Leaves.Length > 0 )
          {

            TreeNode Leaf = Leaves[ 0 ];

            if( Leaf != null )
            {

              Dictionary<string,int> dicContents = DocCollection.GetStatsLanguagesDescriptionsCount();

              Leaf.Nodes.Clear();

              foreach( string ContentKey in dicContents.Keys )
              {
                Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
              }

            }

          }

        }

        {

          TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "LANGUAGES_DETECTED_BODYTEXTS", true );

          if( Leaves.Length > 0 )
          {

            TreeNode Leaf = Leaves[ 0 ];

            if( Leaf != null )
            {

              Dictionary<string,int> dicContents = DocCollection.GetStatsLanguagesBodyTextsCount();

              Leaf.Nodes.Clear();

              foreach( string ContentKey in dicContents.Keys )
              {
                Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );
              }

            }
        
          }

        }
      
        {

          SortedDictionary<string,double> DataPoints = new SortedDictionary<string,double> ();

          TreeNode [] Leaves = this.tvTreeView.Nodes.Find( "TEXT_READABILITY", true );

          if( Leaves.Length > 0 )
          {

            TreeNode Leaf = Leaves[ 0 ];

            if( Leaf != null )
            {

              SortedDictionary<string,int> dicContents = DocCollection.GetStatsReadabilityGradeStringsCount();

              Leaf.Nodes.Clear();

              foreach( string ContentKey in dicContents.Keys )
              {

                TreeNode LeafLeaf = Leaf.Nodes.Add( string.Format( "{0}: {1}", ContentKey, dicContents[ ContentKey ] ) );

                LeafLeaf.Tag = "TEXT_READABILITY_NODE";

                DataPoints.Add( ContentKey, ( double )dicContents[ ContentKey ] );

              }

            }

            this.SiteStructurePanelCharts.UpdateReadability( DataPoints: DataPoints );

          }
          
        }

      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.DebugMsg( ex.Source );
      }

      this.tvTreeView.EndUpdate();
            
    }

    /**************************************************************************/

    private void CopyLeafNodeTagToName ( TreeNode Leaf )
    {
    
      foreach( TreeNode LeafNode in Leaf.Nodes )
      {
        LeafNode.Name = LeafNode.Tag.ToString();
      }
                        
    }
    
    /**************************************************************************/
  
  }

}
