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
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController, IDisposable
  {

    /**************************************************************************/

    Thread ThreadScanner;

    MacroscopeJobMaster JobMaster;
    MacroscopeCredentialsHttp CredentialsHttp;

    string StartUrlString;
    bool StartUrlDirty;
    bool EnableStartUrlCallbacks;

    MacroscopeContextMenus ContextMenusCallbacks;
    
    MacroscopeDisplayStructure msDisplayStructure;
    MacroscopeDisplayStructureLinkCounts msDisplayStructureLinkCounts;
        
    MacroscopeDisplayHierarchy msDisplayHierarchy;

    MacroscopeDisplayRobots msDisplayRobots;
    MacroscopeDisplaySitemaps msDisplaySitemaps;
    MacroscopeDisplaySitemapErrors msDisplaySitemapErrors;

    MacroscopeDisplayCanonical msDisplayCanonical;
    MacroscopeDisplayHrefLang msDisplayHrefLang;
    MacroscopeDisplayErrors msDisplayErrors;
    MacroscopeDisplayRedirectsAudit msDisplayRedirectsAudit;

    MacroscopeDisplayLinks msDisplayLinks;
    MacroscopeDisplayHyperlinks msDisplayHyperlinks;
    MacroscopeDisplayUriAnalysis msDisplayUriAnalysis;

    MacroscopeDisplayTitles msDisplayTitles;
    MacroscopeDisplayDescriptions msDisplayDescriptions;
    MacroscopeDisplayKeywords msDisplayKeywords;
    MacroscopeDisplayHeadings msDisplayHeadings;
    MacroscopeDisplayPageText msDisplayPageText;
    MacroscopeDisplayStylesheets msDisplayStylesheets;
    MacroscopeDisplayJavascripts msDisplayJavascripts;
    MacroscopeDisplayImages msDisplayImages;
    MacroscopeDisplayAudios msDisplayAudios;
    MacroscopeDisplayVideos msDisplayVideos;

    MacroscopeDisplayEmailAddresses msDisplayEmailAddresses;
    MacroscopeDisplayTelephoneNumbers msDisplayTelephoneNumbers;
    MacroscopeDisplayHostnames msDisplayHostnames;

    MacroscopeDisplayCustomFilters msDisplayCustomFilters;

    MacroscopeDisplayDataExtractorCssSelectors msDisplayDisplayDataExtractorCssSelectors;
    MacroscopeDisplayDataExtractorRegexes msDisplayDisplayDataExtractorRegexes;
    MacroscopeDisplayDataExtractorXpaths msDisplayDisplayDataExtractorXpaths;
    
    MacroscopeDisplayUriQueue msDisplayUriQueue;
    MacroscopeDisplayHistory msDisplayHistory;
    
    MacroscopeDisplaySearchCollection msDisplaySearchCollection;

    MacroscopeDisplayStructureOverview msSiteStructureOverview;
    MacroscopeDisplayStructureKeywordAnalysis msSiteStructureKeywordAnalysis;
    MacroscopeDisplayStructureSiteSpeed msSiteStructureSiteSpeed;

    MacroscopeIncludeExcludeUrls IncludeExcludeUrls;
    MacroscopeXpathRestrictions XpathRestrictions;
    MacroscopeCustomFilters CustomFilter;

    MacroscopeDataExtractorCssSelectors DataExtractorCssSelectors;
    MacroscopeDataExtractorRegexes DataExtractorRegexes;
    MacroscopeDataExtractorXpaths DataExtractorXpaths;

    MacroscopeDisplayRemarks msDisplayRemarks;

    private static object LockerOverviewTabPages = new object ();
    private static object LockerDocumentDetailsDisplay = new object ();
    private object LockerSiteStructureDisplay;
    private object LockerAuthenticationDialogue;

    private static object LockerTimerProgressBarScan = new object ();
    private static object LockerTimerTabPages = new object ();
    private static object LockerTimerSiteOverview = new object ();
    private static object LockerTimerStatusBar = new object ();
    private static object LockerTimerAuthentication = new object ();
    
    public System.Timers.Timer TimerProgressBarScan = new System.Timers.Timer ( 10 );
    public System.Timers.Timer TimerTabPages = new System.Timers.Timer ( 10 );
    public System.Timers.Timer TimerSiteOverview = new System.Timers.Timer ( 10 );
    public System.Timers.Timer TimerStatusBar = new System.Timers.Timer ( 10 );
    public System.Timers.Timer TimerAuthentication = new System.Timers.Timer ( 10 );

    /**************************************************************************/

    public MacroscopeMainForm ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.JobMaster = new MacroscopeJobMaster (
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );
      
      this.CredentialsHttp = new MacroscopeCredentialsHttp ();

      this.IncludeExcludeUrls = new MacroscopeIncludeExcludeUrls ();
      this.JobMaster.SetIncludeExcludeUrls( NewIncludeExcludeUrls: this.IncludeExcludeUrls );

      this.XpathRestrictions = new MacroscopeXpathRestrictions ();

      /** Custom Filters --------------------------------------------------- **/

      this.InitializeCustomFilters();

      /** Data Extractors -------------------------------------------------- **/

      this.InitializeDataExtractors(
        InitializeCssSelectors: true,
        InitializeRegexes: true,
        InitializeXpaths: true
      );

      /** ------------------------------------------------------------------ **/

      this.StartUrlString = "";
      this.StartUrlDirty = false;
      this.EnableStartUrlCallbacks = true;

      this.ConfigureOverviewTabPanelInstance();
      this.ConfigureDocumentDetailsInstance();
      this.ConfigureSiteStructurePanelInstance();

      /** Events ----------------------------------------------------------- **/

      this.FormClosing += this.CallbackFormClosing;
      
      // TODO: migrate menu callbacks to here
      
      this.ButtonStart.Click += this.CallbackScanStart;
      this.ButtonStop.Click += this.CallbackScanStop;
      this.ButtonReset.Click += this.CallbackScanReset;

      this.toolStripButtonRetryBrokenLinks.Click += this.CallbackRetryBrokenLinksClick;
      this.toolStripButtonRetryTimedOutLinks.Click += this.CallbackRetryTimedOutLinksClick;
      this.toolStripButtonRecalculateLinkCount.Click += this.CallbackRecalculateLinkCountsClick;
      this.toolStripButtonRecalculateClickPaths.Click += this.CallbackRecalculateClickPathsClick;
      
      /** ------------------------------------------------------------------ **/

      this.SetUrl( MacroscopePreferencesManager.GetStartUrl() );

      #if DEBUG
      this.textBoxStartUrl.Text = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" );
      #endif

      this.LockerSiteStructureDisplay = new object ();

      this.StartTabPageTimer( Delay: 4000 ); // 4000ms
      this.StartSiteOverviewTimer( Delay: 4000 ); // 4000ms
      this.StartStatusBarTimer( Delay: 1000 ); // 1000ms

      // Authentication Dialogue
      this.LockerAuthenticationDialogue = new object ();
      this.StartAuthenticationTimer( Delay: 1000 ); // 1000ms
      
      this.ScanningControlsConfigure();
      
      this.ScanningControlsEnable();

    }

    /** Self Destruct Sequence ************************************************/

    protected override void Dispose ( bool disposing )
    {
      if ( disposing )
      {
        if ( components != null )
        {
          components.Dispose();
        }
      }
      this.JobMaster.Dispose();
      base.Dispose( disposing );
    }

    /** Initialize Custom Filters *********************************************/

    private void InitializeCustomFilters ()
    {

      if( MacroscopePreferencesManager.GetCustomFiltersEnable() )
      {
        this.customFiltersToolStripMenuItem.Enabled = true;
        this.customFiltersExcelReportToolStripMenuItem.Enabled = true;
      }
      else
      {
        this.customFiltersToolStripMenuItem.Enabled = false;
        this.customFiltersExcelReportToolStripMenuItem.Enabled = false;
      }

      this.CustomFilter = new MacroscopeCustomFilters (
        Size: MacroscopePreferencesManager.GetCustomFiltersMaxItems()
      );

      this.JobMaster.SetCustomFilter( NewCustomFilter: this.CustomFilter );

    }

    /** Initialize Data Extractors ********************************************/

    private void InitializeDataExtractors (
      bool InitializeCssSelectors,
      bool InitializeRegexes,
      bool InitializeXpaths
    )
    {
    
      if( MacroscopePreferencesManager.GetDataExtractorsEnable() )
      {
        this.dataExtractorsToolStripMenuItem.Enabled = true;
        this.dataExtractorsExcelReportToolStripMenuItem.Enabled = true;
      }
      else
      {
        this.dataExtractorsToolStripMenuItem.Enabled = false;
        this.dataExtractorsExcelReportToolStripMenuItem.Enabled = false;
      }

      if( InitializeCssSelectors )
      {

        this.DataExtractorCssSelectors = new MacroscopeDataExtractorCssSelectors (
          Size: MacroscopePreferencesManager.GetDataExtractorsMaxItemsCssSelectors()
        );
      
        this.JobMaster.SetDataExtractorCssSelectors(
          NewDataExtractor: this.DataExtractorCssSelectors
        );

      }
      
      if( InitializeRegexes )
      {

        this.DataExtractorRegexes = new MacroscopeDataExtractorRegexes (
          Size: MacroscopePreferencesManager.GetDataExtractorsMaxItemsRegexes()
        );
      
        this.JobMaster.SetDataExtractorRegexes( 
          NewDataExtractor: this.DataExtractorRegexes
        );

      }

      if( InitializeXpaths )
      {

        this.DataExtractorXpaths = new MacroscopeDataExtractorXpaths (
          Size: MacroscopePreferencesManager.GetDataExtractorsMaxItemsXpaths() 
        );
      
        this.JobMaster.SetDataExtractorXpaths( 
          NewDataExtractor: this.DataExtractorXpaths
        );

      }

    }

    /**************************************************************************/
    
    private void ConfigureOverviewTabPanelInstance ()
    {
      
      this.ConfigureMenus();

      /** ListView Reference Objects --------------------------------------- **/

      this.msDisplayStructure = new MacroscopeDisplayStructure ( this, this.macroscopeOverviewTabPanelInstance.listViewStructure );  
      this.msDisplayStructureLinkCounts = new MacroscopeDisplayStructureLinkCounts ( this, this.macroscopeOverviewTabPanelInstance.listViewStructure );

      this.msDisplayHierarchy = new MacroscopeDisplayHierarchy ( this, this.macroscopeOverviewTabPanelInstance.treeViewHierarchy );

      this.msDisplayRobots = new MacroscopeDisplayRobots ( this, this.macroscopeOverviewTabPanelInstance.listViewRobots );

      this.msDisplaySitemaps = new MacroscopeDisplaySitemaps ( this, this.macroscopeOverviewTabPanelInstance.listViewSitemaps );
      this.msDisplaySitemapErrors = new MacroscopeDisplaySitemapErrors( this, this.macroscopeOverviewTabPanelInstance.listViewSitemapErrors );

      this.msDisplayCanonical = new MacroscopeDisplayCanonical ( this, this.macroscopeOverviewTabPanelInstance.listViewCanonicalAnalysis );
      this.msDisplayHrefLang = new MacroscopeDisplayHrefLang ( this, this.macroscopeOverviewTabPanelInstance.listViewHrefLang );
      this.msDisplayErrors = new MacroscopeDisplayErrors ( this, this.macroscopeOverviewTabPanelInstance.listViewErrors );
      this.msDisplayHostnames = new MacroscopeDisplayHostnames ( this, this.macroscopeOverviewTabPanelInstance.listViewHostnames );
      this.msDisplayRedirectsAudit = new MacroscopeDisplayRedirectsAudit ( this, this.macroscopeOverviewTabPanelInstance.listViewRedirectsAudit );

      this.msDisplayLinks = new MacroscopeDisplayLinks ( this, this.macroscopeOverviewTabPanelInstance.listViewLinks );
      this.msDisplayHyperlinks = new MacroscopeDisplayHyperlinks ( this, this.macroscopeOverviewTabPanelInstance.listViewHyperlinks );
      this.msDisplayUriAnalysis = new MacroscopeDisplayUriAnalysis ( this, this.macroscopeOverviewTabPanelInstance.listViewUriAnalysis );

      this.msDisplayTitles = new MacroscopeDisplayTitles ( this, this.macroscopeOverviewTabPanelInstance.listViewPageTitles );
      this.msDisplayDescriptions = new MacroscopeDisplayDescriptions ( this, this.macroscopeOverviewTabPanelInstance.listViewPageDescriptions );
      this.msDisplayKeywords = new MacroscopeDisplayKeywords ( this, this.macroscopeOverviewTabPanelInstance.listViewPageKeywords );
      this.msDisplayHeadings = new MacroscopeDisplayHeadings ( this, this.macroscopeOverviewTabPanelInstance.listViewPageHeadings );
      this.msDisplayPageText = new MacroscopeDisplayPageText ( this, this.macroscopeOverviewTabPanelInstance.listViewPageText );
      this.msDisplayStylesheets = new MacroscopeDisplayStylesheets ( this, this.macroscopeOverviewTabPanelInstance.listViewStylesheets );
      this.msDisplayJavascripts = new MacroscopeDisplayJavascripts ( this, this.macroscopeOverviewTabPanelInstance.listViewJavascripts );
      this.msDisplayImages = new MacroscopeDisplayImages ( this, this.macroscopeOverviewTabPanelInstance.listViewImages );
      this.msDisplayAudios = new MacroscopeDisplayAudios ( this, this.macroscopeOverviewTabPanelInstance.listViewAudios );
      this.msDisplayVideos = new MacroscopeDisplayVideos ( this, this.macroscopeOverviewTabPanelInstance.listViewVideos );
      this.msDisplayEmailAddresses = new MacroscopeDisplayEmailAddresses ( this, this.macroscopeOverviewTabPanelInstance.listViewEmailAddresses );
      this.msDisplayTelephoneNumbers = new MacroscopeDisplayTelephoneNumbers ( this, this.macroscopeOverviewTabPanelInstance.listViewTelephoneNumbers );

      this.msDisplayCustomFilters = new MacroscopeDisplayCustomFilters ( this, this.macroscopeOverviewTabPanelInstance.listViewCustomFilters );
      this.msDisplayCustomFilters.ResetColumns( CustomFilter: this.CustomFilter );

      this.msDisplayDisplayDataExtractorCssSelectors = new MacroscopeDisplayDataExtractorCssSelectors ( this, this.macroscopeOverviewTabPanelInstance.listViewDataExtractorCssSelectors );
      this.msDisplayDisplayDataExtractorCssSelectors.ResetColumns();
      
      this.msDisplayDisplayDataExtractorRegexes = new MacroscopeDisplayDataExtractorRegexes ( this, this.macroscopeOverviewTabPanelInstance.listViewDataExtractorRegexes );
      this.msDisplayDisplayDataExtractorRegexes.ResetColumns();

      this.msDisplayDisplayDataExtractorXpaths = new MacroscopeDisplayDataExtractorXpaths ( this, this.macroscopeOverviewTabPanelInstance.listViewDataExtractorXpaths );
      this.msDisplayDisplayDataExtractorXpaths.ResetColumns();

      this.msDisplayRemarks = new MacroscopeDisplayRemarks ( this, this.macroscopeOverviewTabPanelInstance.listViewRemarks );      

      this.msDisplayUriQueue = new MacroscopeDisplayUriQueue ( this, this.macroscopeOverviewTabPanelInstance.listViewUriQueue );
      this.msDisplayHistory = new MacroscopeDisplayHistory ( this, this.macroscopeOverviewTabPanelInstance.listViewHistory );
      
      this.msDisplaySearchCollection = new MacroscopeDisplaySearchCollection ( this, this.macroscopeOverviewTabPanelInstance.listViewSearchCollection );

      /** Appearance ------------------------------------------------------- **/
      
      this.macroscopeOverviewTabPanelInstance.Dock = DockStyle.Fill;

      /** Events ----------------------------------------------------------- **/
      
      this.macroscopeOverviewTabPanelInstance.tabControlMain.Click += this.CallbackTabControlDisplaySelectedIndexChanged;

      /** listViewStructure ------------------------------------------------ **/
            
      this.macroscopeOverviewTabPanelInstance.listViewStructure.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Click += this.CallbackStructureButtonShowAll;
      foreach( ToolStripDropDownItem ddItem in this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.DropDownItems )
      {
        ddItem.Click += this.CallbackStructureDocumentTypesFilterMenuItemClick;
      }
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.KeyUp += this.CallbackSearchTextBoxSearchUrlKeyUp;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.KeyUp += this.CallbackSearchTextBoxSearchKeyUp;
      
      /** ListViewLinks ---------------------------------------------------- **/

      this.macroscopeOverviewTabPanelInstance.listViewLinks.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripButtonLinksShowAll.Click += this.CallbackButtonLinksShowAll;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxLinksSearchSourceUrls.KeyUp += this.CallbackSearchTextBoxLinksSearchSourceUrlKeyUp;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxLinksSearchTargetUrls.KeyUp += this.CallbackSearchTextBoxLinksSearchTargetUrlKeyUp;
      
      /** ListViewHyperlinks ----------------------------------------------- **/
      
      this.macroscopeOverviewTabPanelInstance.listViewHyperlinks.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripButtonHyperlinksShowAll.Click += this.CallbackButtonHyperlinksShowAll;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchSourceUrls.KeyUp += this.CallbackSearchTextBoxHyperlinksSearchSourceUrlKeyUp;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchTargetUrls.KeyUp += this.CallbackSearchTextBoxHyperlinksSearchTargetUrlKeyUp;
      this.macroscopeOverviewTabPanelInstance.treeViewHierarchy.NodeMouseClick += this.CallbackHierarchyNodeMouseClick;
      this.macroscopeOverviewTabPanelInstance.listViewRobots.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewSitemaps.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewCanonicalAnalysis.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewHrefLang.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewErrors.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewRedirectsAudit.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewUriAnalysis.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewPageTitles.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewPageDescriptions.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewPageHeadings.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewPageText.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewStylesheets.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewJavascripts.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewImages.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewVideos.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewEmailAddresses.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewTelephoneNumbers.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;

      this.macroscopeOverviewTabPanelInstance.listViewCustomFilters.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;

      this.macroscopeOverviewTabPanelInstance.listViewDataExtractorCssSelectors.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewDataExtractorRegexes.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewDataExtractorXpaths.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;

      this.macroscopeOverviewTabPanelInstance.listViewHistory.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;

      /** listViewSearchCollection ----------------------------------------- **/
      
      this.macroscopeOverviewTabPanelInstance.listViewSearchCollection.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Click += this.CallbackSearchCollectionButtonClear;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.KeyUp += this.CallbackSearchCollectionTextBoxSearchKeyUp;

      /** Context Menu Events ---------------------------------------------- **/

      this.ContextMenusCallbacks = new MacroscopeContextMenus ();

      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemCopyUrl.Click += ContextMenusCallbacks.CallbackCopyUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemOpenInBrowser.Click += ContextMenusCallbacks.CallbackOpenUrlInBrowserClick;

      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemAddHostToAllowedHosts.Click += this.CallbackAddToAllowedHosts;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemRemoveFromAllowedHosts.Click += this.CallbackRemoveFromAllowedHosts;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemResetEntry.Click += this.CallbackRetryFetchClick;

    }

    /** Menus *****************************************************************/

    private void ConfigureMenus ()
    {
      this.crawlParentDirectoriesToolStripMenuItem.Checked = MacroscopePreferencesManager.GetCrawlParentDirectories();
      this.crawlChildDirectoriesToolStripMenuItem.Checked = MacroscopePreferencesManager.GetCrawlChildDirectories();
      this.InitializeViewMenu();
    }

    /**************************************************************************/

    private void ConfigureDocumentDetailsInstance ()
    {
      this.macroscopeDocumentDetailsInstance.Dock = DockStyle.Fill;
    }

    /**************************************************************************/

    private void ConfigureSiteStructurePanelInstance ()
    {
      
      this.msSiteStructureOverview = new MacroscopeDisplayStructureOverview (
        this,
        this.macroscopeSiteStructurePanelInstance.treeViewSiteOverview
      );
      
      this.macroscopeSiteStructurePanelInstance.Dock = DockStyle.Fill;

      this.msSiteStructureSiteSpeed = new MacroscopeDisplayStructureSiteSpeed (
        this,
        this.macroscopeSiteStructurePanelInstance.listViewSiteSpeedSlowest,
        this.macroscopeSiteStructurePanelInstance.listViewSiteSpeedFastest,
        this.macroscopeSiteStructurePanelInstance.toolStripLabelSiteSpeedAverage
      );

      this.msSiteStructureKeywordAnalysis = new MacroscopeDisplayStructureKeywordAnalysis (
        this,
        this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis1,
        this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis2,
        this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis3,
        this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis4
      );

      this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis1.Click += this.CallbackListViewSiteStructureKeywordsSearch;
      this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis2.Click += this.CallbackListViewSiteStructureKeywordsSearch;
      this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis3.Click += this.CallbackListViewSiteStructureKeywordsSearch;
      this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis4.Click += this.CallbackListViewSiteStructureKeywordsSearch;

    }

    /**************************************************************************/

    public MacroscopeJobMaster GetJobMaster ()
    {
      return( this.JobMaster );
    }

    /** Credentials ***********************************************************/
    
    public MacroscopeCredentialsHttp IGetCredentialsHttp ()
    {
      return( this.CredentialsHttp );
    }

    /** Start URL *************************************************************/

    public void SetUrl ( string Url )
    {

      this.textBoxStartUrl.Text = Url;

      //this.ScanReset( JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE );
      
    }

    /** -------------------------------------------------------------------- **/
    
    public string GetUrl ()
    {

      string Url = this.textBoxStartUrl.Text;

      Url = MacroscopeStringTools.StripNewLines( Text: Url );

      this.textBoxStartUrl.Text = Url;

      return( this.textBoxStartUrl.Text );

    }

    /** Reset Scan ************************************************************/

    private void ScanReset ( MacroscopeConstants.RunTimeMode JobRunTimeMode )
    {

      this.JobMaster.ClearAllQueues();

      this.JobMaster = new MacroscopeJobMaster (
        JobRunTimeMode: JobRunTimeMode,
        TaskController: this
      );

      this.IncludeExcludeUrls.ClearExplicitIncludeUrls();
      this.IncludeExcludeUrls.ClearExplicitExcludeUrls();

      this.JobMaster.SetIncludeExcludeUrls( NewIncludeExcludeUrls: this.IncludeExcludeUrls );

      this.JobMaster.SetCustomFilter( NewCustomFilter: this.CustomFilter );

      this.JobMaster.SetDataExtractorCssSelectors( NewDataExtractor: this.DataExtractorCssSelectors );
      this.JobMaster.SetDataExtractorRegexes( NewDataExtractor: this.DataExtractorRegexes );
      this.JobMaster.SetDataExtractorXpaths( NewDataExtractor: this.DataExtractorXpaths );
        
      this.ClearDisplay();

    }

    /**************************************************************************/

    private void CallbackFormClosing ( object sender, FormClosingEventArgs e )
    {
      this.CallbackScanStop( sender, e );
    }

    /** DIALOGUE BOXES ********************************************************/

    private void DialogueBoxWarning ( string Title, string Message )
    {
      MessageBox.Show(
        Message,
        Title,
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1
      );
    }

    private void DialogueBoxError ( string Title, string Message )
    {
      MessageBox.Show(
        Message,
        Title,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error,
        MessageBoxDefaultButton.Button1
      );
    }

    private void DialogueBoxStartUrlInvalid ()
    {
      this.DialogueBoxError( "Error", "Please enter a valid URL" );
    }

    /** MAIN CONTROL STRIP CALLBACKS ******************************************/

    private void CallbackStartUrlTextChanged ( object sender, EventArgs e )
    {

      string NewStartUrl = this.GetUrl();

      if ( !NewStartUrl.Equals( this.StartUrlString ) )
      {

        this.StartUrlDirty = true;

        if ( this.EnableStartUrlCallbacks )
        {
          this.ScanReset( JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE );
        }

        if ( MacroscopeHttpUrlUtils.ValidateUrl( Url: NewStartUrl ) )
        {

          this.StartUrlString = NewStartUrl;

          MacroscopePreferencesManager.SetStartUrl( Url: NewStartUrl );

        }

      }

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackStartUrlKeyUp ( object sender, KeyEventArgs e )
    {
      
      switch ( e.Modifiers )
      {
        case Keys.Shift:
          DebugMsg( string.Format( "CallbackStartUrlKeyUp: {0}", "SHIFT" ) );
          return;
        case Keys.Control:
          DebugMsg( string.Format( "CallbackStartUrlKeyUp: {0}", "CONTROL" ) );
          return;
        case Keys.Alt:
          DebugMsg( string.Format( "CallbackStartUrlKeyUp: {0}", "ALT" ) );
          return;
        default:
          break;
      }

      switch ( e.KeyCode )
      {
        case Keys.Return:
          DebugMsg( string.Format( "CallbackStartUrlKeyUp: {0}", "RETURN" ) );
          this.CallackScanStartExecute();
          break;
        case Keys.Escape:
          DebugMsg( string.Format( "CallbackStartUrlKeyUp: {0}", "ESCAPE" ) );
          this.textBoxStartUrl.Text = "";
          break;
        default:
          break;
      }

      return;

    }

    /**************************************************************************/

    private void CallbackScanStart ( object sender, EventArgs e )
    {
      this.CallackScanStartExecute();
    }

    /** NORMAL SCAN ***********************************************************/

    private void CallackScanStartExecute ()
    {

      string NewStartUrl = this.GetUrl();

      if( MacroscopeHttpUrlUtils.ValidateUrl( Url: NewStartUrl ) )
      {

        this.ScanningControlsStart();
        
        if( this.StartUrlDirty )
        {
          
          this.ScanReset( JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE );

          this.StartUrlDirty = false;
          
        }

        this.StartUrlString = NewStartUrl;


        MacroscopePreferencesManager.SetStartUrl( Url: NewStartUrl );

        MacroscopePreferencesManager.SavePreferences();

        this.ThreadScanner = new Thread ( new ThreadStart ( this.ScanningThread ) );
        this.ThreadScanner.Start();

      }
      else
      {

        DialogueBoxStartUrlInvalid();

      }

    }

    /** SCAN FROM URL LIST FILE ***********************************************/

    private void CallackScanStartUrlListFileExecute ( string Path )
    {

      MacroscopeUrlListLoader msUrlListLoader = null;
      
      this.ScanReset( JobRunTimeMode: MacroscopeConstants.RunTimeMode.LISTFILE );

      msUrlListLoader = new MacroscopeUrlListLoader (
        JobMaster: this.JobMaster,
        Path: Path
      );

      if( msUrlListLoader != null )
      {

        if( msUrlListLoader.Execute() )
        {

          string NewStartUrl = msUrlListLoader.GetUrlListItem( 0 );

          this.EnableStartUrlCallbacks = false;

          this.SetUrl( Url: NewStartUrl );

          this.EnableStartUrlCallbacks = true;

          if( MacroscopeHttpUrlUtils.ValidateUrl( Url: NewStartUrl ) )
          {

            this.ScanningControlsStart();

            this.StartUrlString = NewStartUrl;

            MacroscopePreferencesManager.SetStartUrl( Url: NewStartUrl );

            MacroscopePreferencesManager.SavePreferences();

            this.ThreadScanner = new Thread ( new ThreadStart ( this.ScanningThread ) );
            this.ThreadScanner.Start();

          }
          else
          {
            DialogueBoxStartUrlInvalid();
          }

        }
        else
        {
          DialogueBoxError(
            Title: "Load URL List Error",
            Message: "The URL list specified could not loaded" 
          );
        }
      }

    }

    /** SCAN FROM URL TEXT LIST ***********************************************/

    private void CallackScanStartUrlListFromClipboardExecute ( string [] UrlListText )
    {

      MacroscopeUrlListLoader UrlListLoader = null;
        
      this.ScanReset( JobRunTimeMode: MacroscopeConstants.RunTimeMode.LISTTEXT );

      UrlListLoader = new MacroscopeUrlListLoader (
        JobMaster: this.JobMaster,
        UrlListText: UrlListText
      );

      if( UrlListLoader != null )
      {

        if( UrlListLoader.Execute() )
        {

          string NewStartUrl = UrlListLoader.GetUrlListItem( 0 );

          this.EnableStartUrlCallbacks = false;

          this.SetUrl( Url: NewStartUrl );

          this.EnableStartUrlCallbacks = true;

          if( MacroscopeHttpUrlUtils.ValidateUrl( Url: NewStartUrl ) )
          {

            this.ScanningControlsStart();

            this.StartUrlString = NewStartUrl;

            MacroscopePreferencesManager.SetStartUrl( Url: NewStartUrl );

            MacroscopePreferencesManager.SavePreferences();

            this.ThreadScanner = new Thread ( new ThreadStart ( this.ScanningThread ) );
            this.ThreadScanner.Start();

          }
          else
          {
            DialogueBoxStartUrlInvalid();
          }

        }
        else
        {
          DialogueBoxError( "Load URL List Error", "The URL list specified could not loaded" );
        }
      }

    }

    /**************************************************************************/

    private void CallbackScanStop ( object sender, EventArgs e )
    {

      this.ScanningControlsStopping();

      this.JobMaster.StopWorkers();

      while( this.JobMaster.CountRunningThreads() > 0 )
      {
        Thread.Yield();
        Thread.Sleep( 100 );
      }

      this.ScanningControlsStopped();

    }

    /**************************************************************************/

    private void CallbackScanReset ( object sender, EventArgs e )
    {

      if( this.JobMaster.AreWorkersStopped() )
      {

        this.ScanReset( JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE );

        this.ScanningControlsReset();
        
      }

    }

    /** TAB PAGES *************************************************************/

    private void StartTabPageTimer ( int Delay )
    {
      this.TimerTabPages.Interval = Delay;
      this.TimerTabPages.Elapsed += this.CallbackTabPageTimer;
      this.TimerTabPages.AutoReset = true;
      this.TimerTabPages.Enabled = true;
      this.TimerTabPages.Start();
    }

    /** -------------------------------------------------------------------- **/

    private void StopTabPageTimer ()
    {
      if( this.TimerTabPages != null )
      {
        try
        {
          this.TimerTabPages.Stop();
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "StopTabPageTimer: {0}", ex.Message ) );
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTabPageTimer ( Object self, ElapsedEventArgs e )
    {

      if( Monitor.TryEnter( LockerTimerTabPages, 1000 ) )
      {
       
        try
        {

          if( this.InvokeRequired )
          {
            this.Invoke(
              new MethodInvoker (
                delegate
                {
                  this.CallbackTabPageTimerExec();
                }
              )
            );
          }
          else
          {
            this.CallbackTabPageTimerExec();
          }

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "CallbackTabPageTimer: {0}", ex.Message ) );
        }
        finally
        {
          Monitor.Exit( LockerTimerTabPages );
        }

      }
      
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTabPageTimerExec ()
    {
      if( !MacroscopePreferencesManager.GetPauseDisplayDuringScan() )
      {

        this.TimerTabPages.Stop();

        this.UpdateFocusedTabPage();

        this.TimerTabPages.Start();

      }
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTabControlDisplaySelectedIndexChanged ( Object sender, EventArgs e )
    {

      if( Monitor.TryEnter( LockerOverviewTabPages, 250 ) )
      {
       
        try
        {
          TabControl tcDisplay = this.macroscopeOverviewTabPanelInstance.tabControlMain;
          string TabPageName = tcDisplay.TabPages[ tcDisplay.SelectedIndex ].Name;
          this.UpdateTabPage( TabPageName );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "CallbackTabControlDisplaySelectedIndexChanged: {0}", ex.Message ) );
        }
        finally
        {
          Monitor.Exit( LockerOverviewTabPages );
        }

      }

    }

    /** -------------------------------------------------------------------- **/

    private bool IsTabPageEnabled ( string TabName )
    {
      TabControl OverviewTabControl = this.macroscopeOverviewTabPanelInstance.tabControlMain;
      bool IsEnabled = false;
      try
      {
        int ChosenTabIndex = OverviewTabControl.TabPages.IndexOfKey( key: TabName );
        if( ChosenTabIndex >= 0 )
        {
          IsEnabled = OverviewTabControl.TabPages[ ChosenTabIndex ].Enabled;
        }
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "EnableTabPage: {0}", ex.Message ) );
      }
      return ( IsEnabled );
    }

    /** -------------------------------------------------------------------- **/

    private void EnableTabPage ( string TabName )
    {
      TabControl OverviewTabControl = this.macroscopeOverviewTabPanelInstance.tabControlMain;
      try
      {
        int ChosenTabIndex = OverviewTabControl.TabPages.IndexOfKey( key: TabName );
        if( ChosenTabIndex >= 0 )
        {
          OverviewTabControl.TabPages[ ChosenTabIndex ].Enabled = true;
        }
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "EnableTabPage: {0}", ex.Message ) );
      }
    }

    /** -------------------------------------------------------------------- **/

    private void DisableTabPage ( string TabName )
    {
      TabControl OverviewTabControl = this.macroscopeOverviewTabPanelInstance.tabControlMain;
      try
      {
        int ChosenTabIndex = OverviewTabControl.TabPages.IndexOfKey( key: TabName );
        if( ChosenTabIndex >= 0 )
        {
          OverviewTabControl.TabPages[ ChosenTabIndex ].Enabled = false;
        }
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "DisableTabPage: {0}", ex.Message ) );
      }
    }

    /** -------------------------------------------------------------------- **/

    private void UpdateFocusedTabPage ()
    {

      if( Monitor.TryEnter( LockerOverviewTabPages, 250 ) )
      {
       
        try
        {

          TabControl tcDisplay = this.macroscopeOverviewTabPanelInstance.tabControlMain;
          string TabPageName = tcDisplay.TabPages[ tcDisplay.SelectedIndex ].Name;

          if( this.JobMaster != null )
          {
            if( this.JobMaster.PeekUpdateDisplayQueue() )
            {
              this.UpdateTabPage( TabPageName );
              this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayQueue );
            }
          }

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "UpdateFocusedTabPage: {0}", ex.Message ) );
        }
        finally
        {
          Monitor.Exit( LockerOverviewTabPages );
        }

      }

    }

    /** -------------------------------------------------------------------- **/

    private void SelectTabPage ( string TabName )
    {

      TabControl OverviewTabControl = this.macroscopeOverviewTabPanelInstance.tabControlMain;

      try
      {

        int ChosenTabIndex = OverviewTabControl.TabPages.IndexOfKey( key: TabName );

        OverviewTabControl.SelectTab( index: ChosenTabIndex );

        this.UpdateTabPage( TabName: TabName );

      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "SelectTabPage: {0}", ex.Message ) );
      }
      
    }
  
    /** -------------------------------------------------------------------- **/
        
    private void UpdateTabPage ( string TabName )
    {

      if( !this.IsTabPageEnabled( TabName: TabName ) )
      {
        return;
      }
      
      switch( TabName )
      {

        case MacroscopeConstants.tabPageStructureOverview:
          this.msDisplayStructure.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayStructure )
          );
          break;
          
        case MacroscopeConstants.tabPageStructureLinkCounts:
          this.msDisplayStructureLinkCounts.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayStructureLinkCounts )
          );
          break;

        case MacroscopeConstants.tabPageHierarchy:
          this.msDisplayHierarchy.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayHierarchy )
          );
          break;

        case MacroscopeConstants.tabPageRobots:
          this.msDisplayRobots.RefreshData(
            this.JobMaster
          );
          break;

        case MacroscopeConstants.tabPageSitemaps:
          this.msDisplaySitemaps.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplaySitemaps )
          );
          break;

        case MacroscopeConstants.tabPageSitemapErrors:
          this.msDisplaySitemapErrors.RefreshDataSitemapErrors( DocCollection: this.JobMaster.GetDocCollection() );
          break;

        case MacroscopeConstants.tabPageCanonicalAnalysis:
          this.msDisplayCanonical.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayCanonicalAnalysis )
          );
          break;

        case MacroscopeConstants.tabPageHrefLangAnalysis:
          this.msDisplayHrefLang.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            LocalesList: JobMaster.GetLocales()
          );
          break;

        case MacroscopeConstants.tabPageErrors:
          this.msDisplayErrors.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case MacroscopeConstants.tabPageHostnames:
          this.msDisplayHostnames.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;
          
        case MacroscopeConstants.tabPageRedirectsAudit:
          this.msDisplayRedirectsAudit.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case MacroscopeConstants.tabPageLinks:
          this.msDisplayLinks.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayLinks )
          );
          break;
          
        case MacroscopeConstants.tabPageHyperlinks:
          this.msDisplayHyperlinks.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayHyperlinks )
          );
          break;

        case MacroscopeConstants.tabPageUriAnalysis:
          this.msDisplayUriAnalysis.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayUriAnalysis )
          );
          break;

        case MacroscopeConstants.tabPagePageTitles:
          this.msDisplayTitles.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageTitles )
          );
          break;

        case MacroscopeConstants.tabPagePageDescriptions:
          this.msDisplayDescriptions.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageDescriptions )
          );
          break;

        case MacroscopeConstants.tabPagePageKeywords:
          this.msDisplayKeywords.RefreshData(
            this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageKeywords )
          );
          break;

        case MacroscopeConstants.tabPagePageHeadings:
          this.msDisplayHeadings.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageHeadings )
          );
          break;

        case MacroscopeConstants.tabPagePageText:
          this.msDisplayPageText.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageText )
          );
          break;

        case MacroscopeConstants.tabPageStylesheets:
          this.msDisplayStylesheets.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayStylesheets )
          );
          break;

        case MacroscopeConstants.tabPageJavascripts:
          this.msDisplayJavascripts.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayJavascripts )
          );
          break;

        case MacroscopeConstants.tabPageImages:
          this.msDisplayImages.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayImages )
          );
          break;

        case MacroscopeConstants.tabPageAudios:
          this.msDisplayAudios.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayAudios )
          );
          break;

        case MacroscopeConstants.tabPageVideos:
          this.msDisplayVideos.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayVideos )
          );
          break;

        case MacroscopeConstants.tabPageEmailAddresses:
          this.msDisplayEmailAddresses.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case MacroscopeConstants.tabPageTelephoneNumbers:
          this.msDisplayTelephoneNumbers.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case MacroscopeConstants.tabPageCustomFilters:
          if( MacroscopePreferencesManager.GetCustomFiltersEnable() )
          {
            this.msDisplayCustomFilters.RefreshData(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayCustomFilters ),
              CustomFilter: this.CustomFilter
            );
          }
          break;

        case MacroscopeConstants.tabPageDataExtractors:
          if( MacroscopePreferencesManager.GetDataExtractorsEnable() )
          {
            this.msDisplayDisplayDataExtractorCssSelectors.RefreshData(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlList: this.JobMaster.DrainDisplayQueueAsList(
                MacroscopeConstants.NamedQueueDisplayDataExtractorsCssSelectors
              ),
              DataExtractor: this.DataExtractorCssSelectors
            );
            this.msDisplayDisplayDataExtractorRegexes.RefreshData(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlList: this.JobMaster.DrainDisplayQueueAsList(
                MacroscopeConstants.NamedQueueDisplayDataExtractorsRegexes
              ),
              DataExtractor: this.DataExtractorRegexes
            );
            this.msDisplayDisplayDataExtractorXpaths.RefreshData(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlList: this.JobMaster.DrainDisplayQueueAsList(
                MacroscopeConstants.NamedQueueDisplayDataExtractorsXpaths
              ),
              DataExtractor: this.DataExtractorXpaths
            );
          }
          break;

        case MacroscopeConstants.tabPageRemarks:
          this.msDisplayRemarks.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayRemarks )
          );
          break;

        case MacroscopeConstants.tabPageUriQueue:
          this.msDisplayUriQueue.RefreshData(
            UriQueue: this.JobMaster.GetUrlQueueAsArray()
          );
          break;
          
        case MacroscopeConstants.tabPageHistory:
          this.msDisplayHistory.RefreshData(
            History: this.JobMaster.GetJobHistory().GetHistory()
          );
          break;

        case MacroscopeConstants.tabPageSearch:
          break;

        default:
          DebugMsg( string.Format( "UNKNOWN TAB: {0}", TabName ) );
          break;

      }
      
    }

    /** -------------------------------------------------------------------- **/

    private KeyValuePair<string,ListView> GetTabPageListView ()
    {
      
      KeyValuePair<string,ListView> KeyPair;
      string TabName = this.macroscopeOverviewTabPanelInstance.tabControlMain.SelectedTab.Name;
      string TabLabel = this.macroscopeOverviewTabPanelInstance.tabControlMain.SelectedTab.Text;
      string SelectedTabLabel = TabLabel;
      ListView CurrentListView = null;

      switch( TabName )
      {

        case MacroscopeConstants.tabPageStructureOverview:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewStructure;
          break;

        case MacroscopeConstants.tabPageRobots:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewRobots;
          break;

        case MacroscopeConstants.tabPageSitemaps:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewSitemaps;
          break;
          
        case MacroscopeConstants.tabPageCanonicalAnalysis:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewCanonicalAnalysis;
          break;

        case MacroscopeConstants.tabPageHrefLangAnalysis:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewHrefLang;
          break;

        case MacroscopeConstants.tabPageErrors:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewErrors;
          break;

        case MacroscopeConstants.tabPageHostnames:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewHostnames;
          break;
          
        case MacroscopeConstants.tabPageRedirectsAudit:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewRedirectsAudit;
          break;

        case MacroscopeConstants.tabPageLinks:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewLinks;
          break;
          
        case MacroscopeConstants.tabPageHyperlinks:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewHyperlinks;
          break;

        case MacroscopeConstants.tabPageUriAnalysis:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewUriAnalysis;
          break;

        case MacroscopeConstants.tabPagePageTitles:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewPageTitles;
          break;

        case MacroscopeConstants.tabPagePageDescriptions:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewPageDescriptions;
          break;

        case MacroscopeConstants.tabPagePageKeywords:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewPageKeywords;
          break;

        case MacroscopeConstants.tabPagePageHeadings:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewPageHeadings;
          break;

        case MacroscopeConstants.tabPagePageText:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewPageText;
          break;

        case MacroscopeConstants.tabPageStylesheets:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewStylesheets;
          break;

        case MacroscopeConstants.tabPageJavascripts:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewJavascripts;
          break;

        case MacroscopeConstants.tabPageImages:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewImages;
          break;

        case MacroscopeConstants.tabPageAudios:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewAudios;
          break;

        case MacroscopeConstants.tabPageVideos:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewVideos;
          break;

        case MacroscopeConstants.tabPageEmailAddresses:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewEmailAddresses;
          break;

        case MacroscopeConstants.tabPageTelephoneNumbers:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewTelephoneNumbers;
          break;

        case MacroscopeConstants.tabPageCustomFilters:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewCustomFilters;
          break;

        case MacroscopeConstants.tabPageDataExtractors:

          string SubTabName = this.macroscopeOverviewTabPanelInstance.tabControlDataExtractors.SelectedTab.Name;
          string SubTabLabel = this.macroscopeOverviewTabPanelInstance.tabControlDataExtractors.SelectedTab.Text;

          SelectedTabLabel = SubTabLabel;

          switch( SubTabName )
          {
            case MacroscopeConstants.tabPageCssSelectors:
              CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewDataExtractorCssSelectors;
              break;
            case MacroscopeConstants.tabPageRegexes:
              CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewDataExtractorRegexes;
              break;
            case MacroscopeConstants.tabPageXpaths:
              CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewDataExtractorXpaths;
              break;
            default:
              DebugMsg( string.Format( "UNKNOWN SUB TAB: {0}", SubTabName ) );
              break;
          }
          
          break;

        case MacroscopeConstants.tabPageRemarks:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewRemarks;
          break;

        case MacroscopeConstants.tabPageUriQueue:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewUriQueue;
          break;
          
        case MacroscopeConstants.tabPageHistory:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewHistory;
          break;

        case MacroscopeConstants.tabPageSearch:
          CurrentListView = this.macroscopeOverviewTabPanelInstance.listViewSearchCollection;
          break;

        default:
          DebugMsg( string.Format( "UNKNOWN TAB: {0}", TabName ) );
          break;

      }
      
      KeyPair = new KeyValuePair<string,ListView> (
        key: SelectedTabLabel,
        value: CurrentListView
      );

      return( KeyPair );

    }

    /** ListView Show Document Details on URL Click ***************************/

    private void CallbackListViewShowDocumentDetailsOnUrlClick (
      object sender,
      ListViewItemSelectionChangedEventArgs e
    )
    {
      
      if( Monitor.TryEnter( LockerDocumentDetailsDisplay, 250 ) )
      {

        try
        {

          ListView TargetListView = ( ListView )sender;
          ListViewItem lvItem = TargetListView.Items[ e.ItemIndex ];
          string Url = "NONE";
          int UrlColumn = -1;

          lock( sender )
          {

            this.macroscopeDocumentDetailsInstance.Enabled = false;
            
            for( int i = 0 ; i < TargetListView.Columns.Count ; i++ )
            {
              
              if( TargetListView.Columns[ i ].Text == "URL" )
              {
                UrlColumn = i;
                break;
              }
              else
              if( TargetListView.Columns[ i ].Text == "Source URL" )
              {
                UrlColumn = i;
                break;
              }

            }

            if( UrlColumn > -1 )
            {

              if( lvItem != null )
              {
                Url = lvItem.SubItems[ UrlColumn ].Text;
                this.macroscopeDocumentDetailsInstance.UpdateDisplay( this.JobMaster, Url );
              }
              
            }
            else
            {
              MessageBox.Show( "URL column not found" );
              this.macroscopeDocumentDetailsInstance.ClearData();
            }

          }

          this.macroscopeDocumentDetailsInstance.Enabled = true;

        }
        finally
        {
          Monitor.Exit( LockerDocumentDetailsDisplay );
        }

      }

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackAddToAllowedHosts ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      string Url = "NONE";
      int UrlColumn = -1;

      lock( sender )
      {
        
        for( int i = 0 ; i < TargetListView.Columns.Count ; i++ )
        {
          if( TargetListView.Columns[ i ].Text == "URL" )
          {
            UrlColumn = i;
            break;
          }
          else
          if( TargetListView.Columns[ i ].Text == "Source URL" )
          {
            UrlColumn = i;
            break;
          }
          
        }
        if( UrlColumn > -1 )
        {
          foreach( ListViewItem lvItem in TargetListView.SelectedItems )
          {
            Url = lvItem.SubItems[ UrlColumn ].Text.ToString();
          }
        }
        else
        {
          MessageBox.Show( "URL column not found" );
        }
      }

      if( Url != null )
      {
        this.JobMaster.GetAllowedHosts().AddFromUrl( Url: Url );
        this.JobMaster.RetryLink( Url: Url );
        this.RerunScanQueue();
      }

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackRemoveFromAllowedHosts ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      string Url = "NONE";
      int UrlColumn = -1;

      lock( sender )
      {
        for( int i = 0 ; i < TargetListView.Columns.Count ; i++ )
        {
          
          if( TargetListView.Columns[ i ].Text == "URL" )
          {
            UrlColumn = i;
            break;
          }
          else
          if( TargetListView.Columns[ i ].Text == "Source URL" )
          {
            UrlColumn = i;
            break;
          }
          
        }
        if( UrlColumn > -1 )
        {
          foreach( ListViewItem lvItem in TargetListView.SelectedItems )
          {
            Url = lvItem.SubItems[ UrlColumn ].Text.ToString();
          }
        }
        else
        {
          MessageBox.Show( "URL column not found" );
        }
      }

      if( Url != null )
      {
        this.JobMaster.GetAllowedHosts().RemoveFromUrl( Url );
      }

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackRetryFetchClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView TargetListView = msOwner.SourceControl as ListView;
      string Url = "NONE";
      int UrlColumn = -1;

      lock( sender )
      {
        for( int i = 0 ; i < TargetListView.Columns.Count ; i++ )
        {
          
          if( TargetListView.Columns[ i ].Text == "URL" )
          {
            UrlColumn = i;
            break;
          }
          else
          if( TargetListView.Columns[ i ].Text == "Source URL" )
          {
            UrlColumn = i;
            break;
          }
          
        }
        if( UrlColumn > -1 )
        {
          foreach( ListViewItem lvItem in TargetListView.SelectedItems )
          {
            Url = lvItem.SubItems[ UrlColumn ].Text;
          }
        }
        else
        {
          MessageBox.Show( "URL column not found" );
        }
      }

      if( Url != null )
      {
        this.JobMaster.RetryLink( Url: Url );
        this.RerunScanQueue();
      }

      return;

    }

    /** STRUCTURE OVERVIEW PANEL TOOL STRIP CALLBACKS *************************/

    private void CallbackStructureDocumentTypesFilterMenuItemClick ( object sender, EventArgs e )
    {

      ToolStripDropDownItem FilterMenuItem = ( ToolStripDropDownItem )sender;
      MacroscopeConstants.DocumentType DocumentType = MacroscopeConstants.DocumentType.ALL;

      switch( FilterMenuItem.Tag.ToString() )
      {
        case "ALL":
          DocumentType = MacroscopeConstants.DocumentType.ALL;
          break;
        case "INTERNALURL":
          DocumentType = MacroscopeConstants.DocumentType.INTERNALURL;
          break;
        case "EXTERNALURL":
          DocumentType = MacroscopeConstants.DocumentType.EXTERNALURL;
          break;
        case "HTML":
          DocumentType = MacroscopeConstants.DocumentType.HTML;
          break;
        case "CSS":
          DocumentType = MacroscopeConstants.DocumentType.CSS;
          break;
        case "JAVASCRIPT":
          DocumentType = MacroscopeConstants.DocumentType.JAVASCRIPT;
          break;
        case "IMAGE":
          DocumentType = MacroscopeConstants.DocumentType.IMAGE;
          break;
        case "PDF":
          DocumentType = MacroscopeConstants.DocumentType.PDF;
          break;
        case "AUDIO":
          DocumentType = MacroscopeConstants.DocumentType.AUDIO;
          break;
        case "VIDEO":
          DocumentType = MacroscopeConstants.DocumentType.VIDEO;
          break;
        case "SITEMAPXML":
          DocumentType = MacroscopeConstants.DocumentType.SITEMAPXML;
          break;
        case "SITEMAPTEXT":
          DocumentType = MacroscopeConstants.DocumentType.SITEMAPTEXT;
          break;
        case "MISC":
          DocumentType = MacroscopeConstants.DocumentType.BINARY;
          break;
      }

      this.msDisplayStructure.ClearData();

      this.msDisplayStructure.RefreshData(
        DocCollection: this.JobMaster.GetDocCollection(),
        DocumentType: DocumentType
      );

    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackStructureButtonShowAll ( object sender, EventArgs e )
    {

      this.msDisplayStructure.ClearData();

      this.msDisplayStructure.RefreshData(
        this.JobMaster.GetDocCollection()
      );

    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackSearchTextBoxSearchUrlKeyUp ( object sender, KeyEventArgs e )
    {
      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;
      switch( e.KeyCode )
      {
        case Keys.Return:
          string UrlFragment = SearchTextBox.Text;
          DebugMsg( string.Format( "CallbackSearchTextBoxSearchUrlKeyUp: {0}", UrlFragment ) );
          if( UrlFragment.Length > 0 )
          {
            SearchTextBox.Text = UrlFragment;
            this.msDisplayStructure.ClearData();
            this.msDisplayStructure.RefreshData(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlFragment: UrlFragment
            );
          }
          break;
      }
    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackSearchTextBoxSearchKeyUp ( object sender, KeyEventArgs e )
    {

      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;

      switch( e.KeyCode )
      {

        case Keys.Return:

          DebugMsg( string.Format( "CallbackSearchTextBoxSearchKeyUp: {0}", "RETURN" ) );

          MacroscopeSearchIndex	SearchIndex = this.JobMaster.GetDocCollection().GetSearchIndex();
          string SearchText = MacroscopeStringTools.CleanHtmlText( Text: SearchTextBox.Text );

          if( SearchText.Length > 0 )
          {
            List<MacroscopeDocument> DocList = null;
            
            SearchTextBox.Text = SearchText;

            DocList = SearchIndex.ExecuteSearchForDocuments(
              MacroscopeSearchIndex.SearchMode.AND,
              SearchText.Split( ' ' )
            );

            this.msDisplayStructure.ClearData();
            
            this.msDisplayStructure.RefreshData( DocList );
            
          }

          break;

      }

    }

    /** SITE OVERVIEW PANEL ***************************************************/

    private void StartSiteOverviewTimer ( int Delay )
    {
      this.TimerSiteOverview.Interval = Delay;
      this.TimerSiteOverview.Elapsed += this.CallbackSiteOverviewTimer;
      this.TimerSiteOverview.AutoReset = true;
      this.TimerSiteOverview.Enabled = true;
      this.TimerSiteOverview.Start();
    }

    /** -------------------------------------------------------------------- **/
    
    private void SetVelocitySiteOverviewTimer ( int Delay )
    {
      try
      {
        this.TimerSiteOverview.Interval = Delay;
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "SetVelocitySiteOverviewTimer: {0}", ex.Message ) );
      }
    }

    /** -------------------------------------------------------------------- **/
    
    private void StopSiteOverviewTimer ()
    {
      if( this.TimerSiteOverview != null )
      {
        try
        {
          this.TimerSiteOverview.Stop();
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "StopSiteOverviewTimer: {0}", ex.Message ) );
        }
      }
    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackSiteOverviewTimer ( Object self, ElapsedEventArgs e )
    {
      
      if( Monitor.TryEnter( LockerTimerSiteOverview, 1000 ) )
      {
        
        try
        {

          if( this.InvokeRequired )
          {
            this.Invoke(
              new MethodInvoker (
                delegate
                {
                  this.CallbackSiteOverviewTimerExec();
                }
              )
            );
          }
          else
          {
            this.CallbackSiteOverviewTimerExec();
          }

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "CallbackSiteOverviewTimer: {0}", ex.Message ) );
        }
        finally
        {
          Monitor.Exit( LockerTimerSiteOverview );
        }

      }
      
    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackSiteOverviewTimerExec ()
    {
      
      this.TimerSiteOverview.Stop();
                
      this.UpdateSiteOverview();
      
      this.TimerSiteOverview.Start();
                
    }

    /** -------------------------------------------------------------------- **/
    
    private void UpdateSiteOverview ()
    {

      lock( this.LockerSiteStructureDisplay )
      {

        this.msSiteStructureOverview.RefreshData( DocCollection: this.JobMaster.GetDocCollection() );
        
        this.msSiteStructureSiteSpeed.RefreshSiteSpeedData( DocCollection: this.JobMaster.GetDocCollection() );

      }

    }

    /** -------------------------------------------------------------------- **/
    
    private void UpdateSiteOverviewKeywordAnalysis ()
    {

      if( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
      {

        MacroscopeDocumentCollection DocCollection = this.JobMaster.GetDocCollection();

        lock( DocCollection )
        {

          this.msSiteStructureKeywordAnalysis.RefreshKeywordAnalysisData(
            DocCollection: DocCollection
          );

        }
        
      }

    }

    /** SITE OVERVIEW PANEL CALLBACKS *****************************************/

    private void CallbackListViewSiteStructureKeywordsSearch ( object sender, EventArgs e )
    {

      ListView TargetListView = ( ListView )sender;
      string KeywordTerm = "";
      int TermCol = -1;

      this.msDisplaySearchCollection.ClearData();
      
      for( int i = 0 ; i < TargetListView.Columns.Count ; i++ )
      {
        if( TargetListView.Columns[ i ].Text == "Term" )
        {
          TermCol = i;
          break;
        }
      }

      if( TermCol > -1 )
      {
        
        TabControl tcDisplay = this.macroscopeOverviewTabPanelInstance.tabControlMain;
        MacroscopeSearchIndex SearchIndex = this.JobMaster.GetDocCollection().GetSearchIndex();

        tcDisplay.SelectedIndex = tcDisplay.TabPages.IndexOfKey( "tabPageSearch" );

        foreach( ListViewItem lvItem in TargetListView.SelectedItems )
        {

          KeywordTerm = lvItem.SubItems[ TermCol ].Text;
          string SearchText = MacroscopeStringTools.CleanHtmlText( Text: KeywordTerm );
          List<MacroscopeDocument> DocList;
          
          if( SearchText.Length > 0 )
          {

            DocList = SearchIndex.ExecuteSearchForDocuments(
              MacroscopeSearchIndex.SearchMode.AND,
              SearchText.Split( ' ' )
            );

            this.msDisplaySearchCollection.RefreshData( DocList );

          }

        }
        
      }
      else
      {
        MessageBox.Show( "Term column not found" );
        this.msDisplaySearchCollection.ClearData();
      }

    }

    /** Whole Display *********************************************************/

    public void ClearDisplay ()
    {
      
      this.msDisplayStructure.ClearData();
      this.msDisplayHierarchy.ClearData();
      this.msDisplaySearchCollection.ClearData();
      
      this.msDisplayRobots.ClearData();
      this.msDisplaySitemaps.ClearData();
      this.msDisplaySitemapErrors.ClearData();

      this.msDisplayCanonical.ClearData();
      this.msDisplayHrefLang.ClearData();
      
      this.msDisplayErrors.ClearData();
      this.msDisplayRedirectsAudit.ClearData();

      this.msDisplayLinks.ClearData();
      this.msDisplayHyperlinks.ClearData();
      this.msDisplayUriAnalysis.ClearData();
      
      this.msDisplayTitles.ClearData();
      this.msDisplayDescriptions.ClearData();
      this.msDisplayKeywords.ClearData();
      this.msDisplayHeadings.ClearData();
      this.msDisplayPageText.ClearData();

      this.msDisplayStylesheets.ClearData();
      this.msDisplayJavascripts.ClearData();
      this.msDisplayImages.ClearData();
      this.msDisplayAudios.ClearData();
      this.msDisplayVideos.ClearData();
      
      this.msDisplayEmailAddresses.ClearData();
      this.msDisplayTelephoneNumbers.ClearData();

      this.msDisplayHostnames.ClearData();

      this.msDisplayCustomFilters.ClearData();
      this.msDisplayCustomFilters.ResetColumns( CustomFilter: this.CustomFilter );

      this.msDisplayDisplayDataExtractorCssSelectors.ClearData();
      this.msDisplayDisplayDataExtractorCssSelectors.ResetColumns();
      this.msDisplayDisplayDataExtractorRegexes.ClearData();
      this.msDisplayDisplayDataExtractorRegexes.ResetColumns();
      this.msDisplayDisplayDataExtractorXpaths.ClearData();
      this.msDisplayDisplayDataExtractorXpaths.ResetColumns();

      this.msDisplayRemarks.ClearData();
            
      this.msDisplayUriQueue.ClearData();
      this.msDisplayHistory.ClearData();

      this.macroscopeDocumentDetailsInstance.ClearData();

      this.msSiteStructureOverview.ClearData();
      this.msSiteStructureSiteSpeed.ClearData();
      this.msSiteStructureKeywordAnalysis.ClearData();

    }

    /** MAIN SCANNING THREAD **************************************************/

    private void ScanningThread ()
    {

      DebugMsg( "Scanning Thread: Started." );

      this.SetVelocitySiteOverviewTimer( Delay: 4000 );
      this.StartProgressBarScanTimer( Delay: 1000 ); // 1000ms
      this.JobMaster.SetStartUrl( Url: this.GetUrl() );
      this.JobMaster.Execute();
      this.StopProgressBarScanTimer();
      this.UpdateProgressBarScan( Percentage: 0 );
      this.SetVelocitySiteOverviewTimer( Delay: 10000 );

      {

        Thread ThreadUpdateSiteOverviewKeywordAnalysis;

        ThreadUpdateSiteOverviewKeywordAnalysis = new Thread (
          new ThreadStart ( this.UpdateSiteOverviewKeywordAnalysis )
        );

        ThreadUpdateSiteOverviewKeywordAnalysis.Start();

      }

      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker (
            delegate
            {
              this.UpdateFocusedTabPage();
            }
          )
        );
      }
      else
      {
        this.UpdateFocusedTabPage();
      }
      
      DebugMsg( "Scanning Thread: Done." );

    }

    /** RERUN SCAN ************************************************************/

    private void RerunScanQueue ()
    {

      if( this.JobMaster.AreWorkersStopped() )
      {

        this.ScanningControlsStart();

        MacroscopePreferencesManager.SavePreferences();
 
        this.ThreadScanner = new Thread ( new ThreadStart ( this.ScanningThread ) );
        this.ThreadScanner.Start();

      }

      return;

    }

    /** Authentication Dialogue Timer *****************************************/

    private void StartAuthenticationTimer ( int Delay )
    {
      this.TimerAuthentication.Interval = Delay;
      this.TimerAuthentication.Elapsed += this.CallbackAuthenticationTimer;
      this.TimerAuthentication.AutoReset = true;
      this.TimerAuthentication.Enabled = true;
      this.TimerAuthentication.Start();
    }

    /** -------------------------------------------------------------------- **/
    
    private void StopAuthenticationTimer ()
    {
      if( this.TimerAuthentication != null )
      {
        try
        {
          this.TimerAuthentication.Stop();
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "StopAuthenticationTimer: {0}", ex.Message ) );
        }
      }
    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackAuthenticationTimer ( Object self, ElapsedEventArgs e )
    {
      
      if( Monitor.TryEnter( LockerTimerAuthentication, 1000 ) )
      {

        try
        {

          this.TimerAuthentication.Stop();

          if( this.InvokeRequired )
          {
            this.Invoke(
              new MethodInvoker (
                delegate
                {
                  this.ShowAuthenticationDialogue();
                }
              )
            );
          }
          else
          {
            this.ShowAuthenticationDialogue();
          }

          this.TimerAuthentication.Start();

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "CallbackAuthenticationTimer: {0}", ex.Message ) );
        }
        finally
        {
          Monitor.Exit( LockerTimerAuthentication );
        }
        
      }
      
    }

    /** -------------------------------------------------------------------- **/
    
    private void ShowAuthenticationDialogue ()
    {
      
      bool DoRerun = false;
      string RerunUrl = null;
      
      if( this.JobMaster != null )
      {

        if( this.CredentialsHttp.PeekCredentialRequest() )
        {

          lock( this.LockerAuthenticationDialogue )
          {

            MacroscopeCredentialRequest CredentialRequest;
            MacroscopeGetCredentialsHttp CredentialsForm;
            
            CredentialRequest = this.CredentialsHttp.DequeueCredentialRequest();
                        
            if( 
              this.CredentialsHttp.CredentialExists(
                Domain: CredentialRequest.GetDomain(),
                Realm: CredentialRequest.GetRealm()
              ) )
            {

              DebugMsg(
                string.Format(
                  "CredentialExists: {0} :: {1}",
                  CredentialRequest.GetDomain(),
                  CredentialRequest.GetRealm()
                )
              );

              DoRerun = true;
              RerunUrl = CredentialRequest.GetUrl();

            }
            else
            {

              CredentialsForm = new MacroscopeGetCredentialsHttp ();

              CredentialsForm.labelMessage.Text = string.Format(
                "The website at \"{0}\" is requesting credentials for the Realm \"{1}\"",
                CredentialRequest.GetDomain(),
                CredentialRequest.GetRealm()
              );

              DialogResult CredentialsFormResult = CredentialsForm.ShowDialog();

              if( CredentialsFormResult == DialogResult.OK )
              {
                string sUsername = CredentialsForm.textBoxUsername.Text;
                string sPassword = CredentialsForm.maskedTextBoxPassword.Text;

                this.CredentialsHttp.AddCredential(
                  Domain: CredentialRequest.GetDomain(),
                  Realm: CredentialRequest.GetRealm(),
                  Username: sUsername,
                  Password: sPassword
                );

                DoRerun = true;
                RerunUrl = CredentialRequest.GetUrl();

              }

              CredentialsForm.Dispose();

            }
            
          }
          
        }

      }

      if( DoRerun )
      {
        this.JobMaster.AddUrlQueueItem( Url: RerunUrl );
        this.JobMaster.RetryLink( Url: RerunUrl );
        this.RerunScanQueue();
      }

    }

    /** Operation Toolbar Callbacks *******************************************/

    private void CallbackRetryBrokenLinksClick ( object sender, EventArgs e )
    {
      this.JobMaster.RetryBrokenLinks();
      this.RerunScanQueue();
    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackRetryTimedOutLinksClick ( object sender, EventArgs e )
    {
      this.JobMaster.RetryTimedOutLinks();
      this.RerunScanQueue();
    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackRecalculateLinkCountsClick ( object sender, EventArgs e )
    {
      this.RecalculateLinkCounts();
    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackRecalculateClickPathsClick ( object sender, EventArgs e )
    {
      this.RecalculateClickPathAnalysis();
    }

    /** Recalculate Link Counts  **********************************************/

    public void RecalculateLinkCounts ()
    {

      MacroscopeDocumentCollection DocCollection = this.JobMaster.GetDocCollection();

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {
        this.JobMaster.AddUpdateDisplayQueue( Url: msDoc.GetUrl() );
      }

      DocCollection.RecalculateDocCollection();

      this.UpdateTabPage( MacroscopeConstants.tabPageStructureLinkCounts );

    }

    /** Recalculate Click Path Analysis ***************************************/

    public void RecalculateClickPathAnalysis ()
    {

      this.JobMaster.GetDocCollection().RecalculateClickPaths();

      // TODO: Implement this:
      //this.UpdateTabPage( MacroscopeConstants.tabPageStructureClickPathAnalysis );
            
    }

    /** Load List *************************************************************/

    private void CallbackLoadUrlListTextFile ( object sender, EventArgs e )
    {

      OpenFileDialog Dialog = new OpenFileDialog ();

      Dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "txt";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {
        string Path = Dialog.FileName;

        if( File.Exists( Path ) )
        {
          this.CallackScanStartUrlListFileExecute( Path );
        }
        else
        {
          DialogueBoxError( "Load URL List Error", "The text file specified could not be found" );
        }

      }

      Dialog.Dispose();

    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackLoadUrlListTextFromClipboard ( object sender, EventArgs e )
    {

      MacroscopeLoadUrlListFromClipboard LoadUrlListDialogue = new MacroscopeLoadUrlListFromClipboard ();

      if( LoadUrlListDialogue.ShowDialog() == DialogResult.OK )
      {

        string UrlListText = LoadUrlListDialogue.GetUrlsText();
        string [] UrlList = Regex.Split( UrlListText, "[\\r\\n]+", RegexOptions.Singleline );

        this.CallackScanStartUrlListFromClipboardExecute( UrlListText: UrlList );

      }
      
      LoadUrlListDialogue.Dispose();

    }

    /** TASK PARAMETERS CALLBACKS *********************************************/

    private void CallbackIncludeUrlItemsClick ( object sender, EventArgs e )
    {

      string IncludePatternsText = this.IncludeExcludeUrls.FetchIncludeUrlPatterns();
      MacroscopeIncludeUrlPatterns IncludeUrlPatternsForm = new MacroscopeIncludeUrlPatterns ( PatternsText: IncludePatternsText );

      DialogResult IncludeUrlPatternsResult = IncludeUrlPatternsForm.ShowDialog();

      if( IncludeUrlPatternsResult == DialogResult.OK )
      {
        string NewIncludeUrlPatternsText = IncludeUrlPatternsForm.GetPatternsText();
        this.IncludeExcludeUrls.LoadIncludeUrlPatterns( IncludeUrlPatternsText: NewIncludeUrlPatternsText );
      }

      IncludeUrlPatternsForm.Dispose();

    }
    
    /** -------------------------------------------------------------------- **/

    private void CallbackExcludeUrlItemsClick ( object sender, EventArgs e )
    {

      string ExcludeUrlPatternsText = this.IncludeExcludeUrls.FetchExcludeUrlPatterns();
      MacroscopeExcludeUrlPatterns ExcludeUrlPatternsForm = new MacroscopeExcludeUrlPatterns ( PatternsText: ExcludeUrlPatternsText );

      DialogResult ExcludeUrlPatternsResult = ExcludeUrlPatternsForm.ShowDialog();

      if( ExcludeUrlPatternsResult == DialogResult.OK )
      {
        string NewExcludeUrlPatternsResult = ExcludeUrlPatternsForm.GetPatternsText();
        this.IncludeExcludeUrls.LoadExcludeUrlPatterns( ExcludeUrlPatternsText: NewExcludeUrlPatternsResult );
      }
      
      ExcludeUrlPatternsForm.Dispose();

    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackCrawlParentDirectoriesToolStripMenuItemClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem CrawlMenuItem = sender as ToolStripMenuItem;

      if( CrawlMenuItem.Checked )
      {
        CrawlMenuItem.Checked = false;
        MacroscopePreferencesManager.SetCrawlParentDirectories( false );
      }
      else
      {
        CrawlMenuItem.Checked = true;
        MacroscopePreferencesManager.SetCrawlParentDirectories( true );
      }

      MacroscopePreferencesManager.SavePreferences();

    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackCrawlChildDirectoriesToolStripMenuItemClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem CrawlMenuItem = sender as ToolStripMenuItem;

      if( CrawlMenuItem.Checked )
      {
        CrawlMenuItem.Checked = false;
        MacroscopePreferencesManager.SetCrawlChildDirectories( false );
      }
      else
      {
        CrawlMenuItem.Checked = true;
        MacroscopePreferencesManager.SetCrawlChildDirectories( true );
      }

      MacroscopePreferencesManager.SavePreferences();

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackCustomFilterClick ( object sender, EventArgs e )
    {

      MacroscopeCustomFilterForm CustomFilterForm = new MacroscopeCustomFilterForm ( CustomFilter: this.CustomFilter );

      DialogResult CustomFilterResult = CustomFilterForm.ShowDialog();

      if( CustomFilterResult == DialogResult.OK )
      {

        this.CustomFilter = CustomFilterForm.GetCustomFilter();

        this.JobMaster.SetCustomFilter( NewCustomFilter: this.CustomFilter );
        
        this.msDisplayCustomFilters.ResetColumns( CustomFilter: this.CustomFilter );

      }

      CustomFilterForm.Dispose();

    }

    /** Data Extractors ---------------------------------------------------- **/

    private void CallbackDataExtractorsCssSelectorsClick ( object sender, EventArgs e )
    {

      MacroscopeDataExtractorCssSelectorsForm DataExtractorsForm;
      DialogResult DataExtractorsResult;
      
      DataExtractorsForm = new MacroscopeDataExtractorCssSelectorsForm (
        NewDataExtractor: this.DataExtractorCssSelectors
      );

      DataExtractorsResult = DataExtractorsForm.ShowDialog();

      if( DataExtractorsResult == DialogResult.OK )
      {

        this.DataExtractorCssSelectors = DataExtractorsForm.GetDataExtractor();

        this.JobMaster.SetDataExtractorCssSelectors(
          NewDataExtractor: this.DataExtractorCssSelectors
        );
        
        this.msDisplayDisplayDataExtractorCssSelectors.ResetColumns();

      }

      DataExtractorsForm.Dispose();

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackDataExtractorsRegularExpressionsClick ( object sender, EventArgs e )
    {

      MacroscopeDataExtractorRegexesForm DataExtractorsForm;
      DialogResult DataExtractorsResult;
      
      DataExtractorsForm = new MacroscopeDataExtractorRegexesForm (
        NewDataExtractor: this.DataExtractorRegexes
      );

      DataExtractorsResult = DataExtractorsForm.ShowDialog();

      if( DataExtractorsResult == DialogResult.OK )
      {

        this.DataExtractorRegexes = DataExtractorsForm.GetDataExtractor();

        this.JobMaster.SetDataExtractorRegexes( 
          NewDataExtractor: this.DataExtractorRegexes 
        );
        
        this.msDisplayDisplayDataExtractorCssSelectors.ResetColumns();

      }

      DataExtractorsForm.Dispose();

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackDataExtractorsXpathsClick ( object sender, EventArgs e )
    {

      // TODO: Implement XPaths form

      MacroscopeDataExtractorXpathsForm DataExtractorsForm;
      DialogResult DataExtractorsResult;
      
      DataExtractorsForm = new MacroscopeDataExtractorXpathsForm (
        NewDataExtractor: this.DataExtractorXpaths
      );

      DataExtractorsResult = DataExtractorsForm.ShowDialog();

      if( DataExtractorsResult == DialogResult.OK )
      {

        this.DataExtractorXpaths = DataExtractorsForm.GetDataExtractor();

        this.JobMaster.SetDataExtractorXpaths( 
          NewDataExtractor: this.DataExtractorXpaths 
        );
        
        this.msDisplayDisplayDataExtractorXpaths.ResetColumns();

      }

      DataExtractorsForm.Dispose();

    }

    /** Credentials -------------------------------------------------------- **/
    
    private void CallbackClearHTTPAuthenticationToolStripMenuItemClick ( object sender, EventArgs e )
    {
      this.CredentialsHttp.ClearAll();
    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    private static void DebugMsg ( String Msg )
    {
      System.Diagnostics.Debug.WriteLine( Msg );
    }



    /**************************************************************************/

  }

}
