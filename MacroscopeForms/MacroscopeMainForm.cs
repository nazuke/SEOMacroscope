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
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /**************************************************************************/

    Thread ThreadScanner;

    MacroscopeJobMaster JobMaster;

    Boolean StartUrlDirty;

    MacroscopeDisplayStructure msDisplayStructure;
    MacroscopeDisplayHierarchy msDisplayHierarchy;

    MacroscopeDisplayRobots msDisplayRobots;
    MacroscopeDisplaySitemaps msDisplaySitemaps;

    MacroscopeDisplayCanonical msDisplayCanonical;
    MacroscopeDisplayHrefLang msDisplayHrefLang;
    MacroscopeDisplayErrors msDisplayErrors;
    MacroscopeDisplayRedirectsAudit msDisplayRedirectsAudit;

    MacroscopeDisplayUriAnalysis msDisplayUriAnalysis;

    MacroscopeDisplayTitles msDisplayTitles;
    MacroscopeDisplayDescriptions msDisplayDescriptions;
    MacroscopeDisplayKeywords msDisplayKeywords;
    MacroscopeDisplayHeadings msDisplayHeadings;
    MacroscopeDisplayStylesheets msDisplayStylesheets;
    MacroscopeDisplayJavascripts msDisplayJavascripts;
    MacroscopeDisplayImages msDisplayImages;
    MacroscopeDisplayAudios msDisplayAudios;
    MacroscopeDisplayVideos msDisplayVideos;

    MacroscopeDisplayEmailAddresses msDisplayEmailAddresses;
    MacroscopeDisplayTelephoneNumbers msDisplayTelephoneNumbers;
    MacroscopeDisplayHostnames msDisplayHostnames;
    MacroscopeDisplayHistory msDisplayHistory;
    MacroscopeDisplaySearchCollection msDisplaySearchCollection;

    MacroscopeDisplayStructureOverview msSiteStructureOverview;
    MacroscopeDisplayStructureKeywordAnalysis msSiteStructureKeywordAnalysis;
    MacroscopeDisplayStructureSiteSpeed msSiteStructureSiteSpeed;

    MacroscopeIncludeExcludeUrls IncludeExcludeUrls;

    Semaphore SemaphoreOverviewTabPages;
    Semaphore SemaphoreSiteStructureDisplay;
    
    public System.Timers.Timer TimerProgressBarScan;
    public System.Timers.Timer TimerTabPages;
    public System.Timers.Timer TimerSiteOverview;
    public System.Timers.Timer TimerStatusBar;

    /**************************************************************************/

    public MacroscopeMainForm ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.JobMaster = new MacroscopeJobMaster ( MacroscopeConstants.RunTimeMode.LIVE, this );

      this.IncludeExcludeUrls = new MacroscopeIncludeExcludeUrls ();

      this.JobMaster.SetIncludeExcludeUrls( IncludeExcludeUrls );

      this.StartUrlDirty = false;

      this.ConfigureOverviewTabPanelInstance();
      this.ConfigureDocumentDetailsInstance();
      this.ConfigureSiteStructurePanelInstance();

      this.SetUrl( MacroscopePreferencesManager.GetStartUrl() );

      #if DEBUG
			this.textBoxStartUrl.Text = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" );
      #endif

      this.SemaphoreOverviewTabPages = new Semaphore ( 0, 1 );
      this.SemaphoreOverviewTabPages.Release( 1 );

      this.SemaphoreSiteStructureDisplay = new Semaphore ( 0, 1 );
      this.SemaphoreSiteStructureDisplay.Release( 1 );

      //this.StartProgressBarScanTimer( Delay: 1000 ); // 1000ms
      this.StartTabPageTimer( Delay: 4000 ); // BROKEN // 4000ms
      this.StartSiteOverviewTimer( Delay: 4000 ); // 4000ms
      this.StartStatusBarTimer( Delay: 1000 ); // 1000ms

      this.ScanningControlsEnable( true );

    }

    /**************************************************************************/

    ~MacroscopeMainForm ()
    {
      DebugMsg( "MacroscopeMainForm DESTRUCTOR CALLED" );
      this.Cleanup();
    }

    /**************************************************************************/

    void ConfigureOverviewTabPanelInstance ()
    {
      
      this.ConfigureMenus();

      // ListView Reference Objects

      this.msDisplayStructure = new MacroscopeDisplayStructure ( this, this.macroscopeOverviewTabPanelInstance.listViewStructure );
      this.msDisplayHierarchy = new MacroscopeDisplayHierarchy ( this, this.macroscopeOverviewTabPanelInstance.treeViewHierarchy );
      this.msDisplayRobots = new MacroscopeDisplayRobots ( this, this.macroscopeOverviewTabPanelInstance.listViewRobots );
      this.msDisplaySitemaps = new MacroscopeDisplaySitemaps ( this, this.macroscopeOverviewTabPanelInstance.listViewSitemaps );
      this.msDisplayCanonical = new MacroscopeDisplayCanonical ( this, this.macroscopeOverviewTabPanelInstance.listViewCanonicalAnalysis );
      this.msDisplayHrefLang = new MacroscopeDisplayHrefLang ( this, this.macroscopeOverviewTabPanelInstance.listViewHrefLang );
      this.msDisplayErrors = new MacroscopeDisplayErrors ( this, this.macroscopeOverviewTabPanelInstance.listViewErrors );
      this.msDisplayRedirectsAudit = new MacroscopeDisplayRedirectsAudit ( this, this.macroscopeOverviewTabPanelInstance.listViewRedirectsAudit );
      this.msDisplayUriAnalysis = new MacroscopeDisplayUriAnalysis ( this, this.macroscopeOverviewTabPanelInstance.listViewUriAnalysis );
      this.msDisplayTitles = new MacroscopeDisplayTitles ( this, this.macroscopeOverviewTabPanelInstance.listViewPageTitles );
      this.msDisplayDescriptions = new MacroscopeDisplayDescriptions ( this, this.macroscopeOverviewTabPanelInstance.listViewPageDescriptions );
      this.msDisplayKeywords = new MacroscopeDisplayKeywords ( this, this.macroscopeOverviewTabPanelInstance.listViewPageKeywords );
      this.msDisplayHeadings = new MacroscopeDisplayHeadings ( this, this.macroscopeOverviewTabPanelInstance.listViewPageHeadings );
      this.msDisplayStylesheets = new MacroscopeDisplayStylesheets ( this, this.macroscopeOverviewTabPanelInstance.listViewStylesheets );
      this.msDisplayJavascripts = new MacroscopeDisplayJavascripts ( this, this.macroscopeOverviewTabPanelInstance.listViewJavascripts );
      this.msDisplayImages = new MacroscopeDisplayImages ( this, this.macroscopeOverviewTabPanelInstance.listViewImages );
      this.msDisplayAudios = new MacroscopeDisplayAudios ( this, this.macroscopeOverviewTabPanelInstance.listViewAudios );
      this.msDisplayVideos = new MacroscopeDisplayVideos ( this, this.macroscopeOverviewTabPanelInstance.listViewVideos );
      this.msDisplayEmailAddresses = new MacroscopeDisplayEmailAddresses ( this, this.macroscopeOverviewTabPanelInstance.listViewEmailAddresses );
      this.msDisplayTelephoneNumbers = new MacroscopeDisplayTelephoneNumbers ( this, this.macroscopeOverviewTabPanelInstance.listViewTelephoneNumbers );
      this.msDisplayHostnames = new MacroscopeDisplayHostnames ( this, this.macroscopeOverviewTabPanelInstance.listViewHostnames );
      this.msDisplayHistory = new MacroscopeDisplayHistory ( this, this.macroscopeOverviewTabPanelInstance.listViewHistory );
      this.msDisplaySearchCollection = new MacroscopeDisplaySearchCollection ( this, this.macroscopeOverviewTabPanelInstance.listViewSearchCollection );

      // Appearance

      this.macroscopeOverviewTabPanelInstance.Dock = DockStyle.Fill;

      // Events --------------------------------------------------------------//

      this.macroscopeOverviewTabPanelInstance.tabControlMain.Click += this.CallbackTabControlDisplaySelectedIndexChanged;

      // listViewStructure
      this.macroscopeOverviewTabPanelInstance.listViewStructure.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Click += this.CallbackStructureButtonShowAll;
      foreach( ToolStripDropDownItem ddItem in this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.DropDownItems )
      {
        ddItem.Click += this.CallbackStructureDocumentTypesFilterMenuItemClick;
      }
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.KeyUp += this.CallbackSearchTextBoxSearchUrlKeyUp;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.KeyUp += this.CallbackSearchTextBoxSearchKeyUp;

      // treeViewHierarchy etc...
      this.macroscopeOverviewTabPanelInstance.treeViewHierarchy.NodeMouseClick += this.CallbackHierarchyNodeMouseClick;
      this.macroscopeOverviewTabPanelInstance.listViewRobots.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewSitemaps.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewCanonicalAnalysis.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewHrefLang.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewErrors.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewRedirectsAudit.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewUriAnalysis.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewPageTitles.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewPageDescriptions.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewPageHeadings.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewStylesheets.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewJavascripts.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewImages.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewVideos.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewEmailAddresses.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewTelephoneNumbers.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewHistory.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;

      // listViewSearchCollection
      this.macroscopeOverviewTabPanelInstance.listViewSearchCollection.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Click += this.CallbackSearchCollectionButtonClear;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.KeyUp += this.CallbackSearchCollectionTextBoxSearchKeyUp;

      // Context Menu Events

      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemOpenInBrowser.Click += this.CallbackOpenInBrowserClick;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemAddHostToAllowedHosts.Click += this.CallbackAddToAllowedHosts;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemRemoveFromAllowedHosts.Click += this.CallbackRemoveFromAllowedHosts;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemResetEntry.Click += this.CallbackRetryFetchClick;

    }

    /**************************************************************************/

    void ConfigureMenus ()
    {
      this.crawlParentDirectoriesToolStripMenuItem.Checked = MacroscopePreferencesManager.GetCrawlParentDirectories();
      this.crawlChildDirectoriesToolStripMenuItem.Checked = MacroscopePreferencesManager.GetCrawlChildDirectories();
    }

    /**************************************************************************/

    void ConfigureDocumentDetailsInstance ()
    {
      this.macroscopeDocumentDetailsInstance.Dock = DockStyle.Fill;
    }

    /**************************************************************************/

    void ConfigureSiteStructurePanelInstance ()
    {
      
      this.msSiteStructureOverview = new MacroscopeDisplayStructureOverview (
        this,
        this.macroscopeSiteStructurePanelInstance.treeViewSiteOverview
      );
      
      this.macroscopeSiteStructurePanelInstance.Dock = DockStyle.Fill;
      
      this.msSiteStructureKeywordAnalysis = new MacroscopeDisplayStructureKeywordAnalysis (
        this,
        this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis1,
        this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis2,
        this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis3,
        this.macroscopeSiteStructurePanelInstance.listViewKeywordAnalysis4
      );

      this.msSiteStructureSiteSpeed = new MacroscopeDisplayStructureSiteSpeed (
        this,
        this.macroscopeSiteStructurePanelInstance.listViewSiteSpeedSlowest,
        this.macroscopeSiteStructurePanelInstance.listViewSiteSpeedFastest,
        this.macroscopeSiteStructurePanelInstance.toolStripLabelSiteSpeedAverage
      );

    }

    /**************************************************************************/

    void Cleanup ()
    {

      DebugMsg( string.Format( "MacroscopeMainForm Cleanup: CALLED..." ) );

      MacroscopePreferencesManager.SavePreferences();

      if( this.ThreadScanner != null )
      {
        DebugMsg( "Cleaning up ThreadScanner" );
        this.ThreadScanner.Abort();
      }

      this.StopProgressBarScanTimer();
      this.StopTabPageTimer();
      this.StopSiteOverviewTimer();
      this.StopStatusBarTimer();

      if( this.JobMaster != null )
      {
        this.JobMaster.ClearAllQueues();
      }

      this.JobMaster = null;

      this.SemaphoreOverviewTabPages.Dispose();

      DebugMsg( string.Format( "MacroscopeMainForm Cleanup: DONE." ) );
    }

    /**************************************************************************/

    public MacroscopeJobMaster GetJobMaster ()
    {
      return( this.JobMaster );
    }

    /** Start URL *************************************************************/

    public void SetUrl ( string sUrl )
    {
      this.textBoxStartUrl.Text = sUrl;
    }

    public string GetUrl ()
    {
      return( this.textBoxStartUrl.Text );
    }

    /**************************************************************************/

    void CallbackFormClosing ( object sender, FormClosingEventArgs e )
    {
      this.Cleanup();
    }

    /** MAIN MENU *************************************************************/

    void CallbackFileExit ( object sender, EventArgs e )
    {
      DebugMsg( "CallbackFileExit Called" );
      this.Cleanup();
      Program.Exit();
    }

    /** Edit Menu *************************************************************/

    private void CallbackEditPreferencesClick ( object sender, EventArgs e )
    {
      MacroscopePrefsForm PreferencesForm = new MacroscopePrefsForm ();
      MacroscopePrefsControl PrefsControl = PreferencesForm.macroscopePrefsControlInstance;
      PreferencesForm.buttonPrefsDefault.Click += this.SetPrefsFormControlFieldToDefaults;
      this.SetPrefsFormControlFields( PrefsControl: PrefsControl );
      DialogResult PreferencesResult = PreferencesForm.ShowDialog();
      if( PreferencesResult == DialogResult.OK )
      {
        this.SavePrefsFormControlFields( PrefsControl: PrefsControl );
      }
    }

    private void SetPrefsFormControlFieldToDefaults ( object sender, EventArgs e )
    {
      Button DefaultsButton = ( Button )sender;
      MacroscopePrefsForm PreferencesForm = ( MacroscopePrefsForm )DefaultsButton.TopLevelControl;
      MacroscopePrefsControl PrefsControl = PreferencesForm.macroscopePrefsControlInstance;
      MacroscopePreferencesManager.SetDefaultValues();
      this.SetPrefsFormControlFields( PrefsControl );
    }

    private void SetPrefsFormControlFields ( MacroscopePrefsControl PrefsControl )
    {

      { //Configure Form Fields

        // Spidering Control

        PrefsControl.numericUpDownDepth.Minimum = -1;
        PrefsControl.numericUpDownDepth.Maximum = 10000;

        PrefsControl.numericUpDownPageLimit.Minimum = -1;
        PrefsControl.numericUpDownPageLimit.Maximum = 10000;

        PrefsControl.numericUpDownMaxRetries.Minimum = 0;
        PrefsControl.numericUpDownMaxRetries.Maximum = 10;

      }

      {

        // WebProxy Options

        PrefsControl.textBoxHttpProxyHost.Text = MacroscopePreferencesManager.GetHttpProxyHost();
        PrefsControl.numericUpDownHttpProxyPort.Value = MacroscopePreferencesManager.GetHttpProxyPort();

        // Spidering Control

        PrefsControl.numericUpDownMaxThreads.Value = MacroscopePreferencesManager.GetMaxThreads();
        PrefsControl.numericUpDownDepth.Value = MacroscopePreferencesManager.GetDepth();
        PrefsControl.numericUpDownPageLimit.Value = MacroscopePreferencesManager.GetPageLimit();
        PrefsControl.numericUpDownRequestTimeout.Value = ( Decimal )MacroscopePreferencesManager.GetRequestTimeout();
        PrefsControl.numericUpDownMaxRetries.Value = ( Decimal )MacroscopePreferencesManager.GetMaxRetries();

        PrefsControl.checkBoxCheckExternalLinks.Checked = MacroscopePreferencesManager.GetCheckExternalLinks();
        PrefsControl.checkBoxFollowRobotsProtocol.Checked = MacroscopePreferencesManager.GetFollowRobotsProtocol();
        PrefsControl.checkBoxFollowSitemapLinks.Checked = MacroscopePreferencesManager.GetFollowSitemapLinks();
        PrefsControl.checkBoxFollowRedirects.Checked = MacroscopePreferencesManager.GetFollowRedirects();
        PrefsControl.checkBoxFollowNoFollow.Checked = MacroscopePreferencesManager.GetFollowNoFollow();
        PrefsControl.checkBoxFollowCanonicalLinks.Checked = MacroscopePreferencesManager.GetFollowCanonicalLinks();
        PrefsControl.checkBoxFollowHrefLangLinks.Checked = MacroscopePreferencesManager.GetFollowHrefLangLinks();

        PrefsControl.checkBoxFetchStylesheets.Checked = MacroscopePreferencesManager.GetFetchStylesheets();
        PrefsControl.checkBoxFetchJavascripts.Checked = MacroscopePreferencesManager.GetFetchImages();
        PrefsControl.checkBoxFetchImages.Checked = MacroscopePreferencesManager.GetFetchImages();
        PrefsControl.checkBoxFetchPdfs.Checked = MacroscopePreferencesManager.GetFetchPdfs();
        PrefsControl.checkBoxFetchAudio.Checked = MacroscopePreferencesManager.GetFetchAudio();
        PrefsControl.checkBoxFetchVideo.Checked = MacroscopePreferencesManager.GetFetchVideo();
        PrefsControl.checkBoxFetchXml.Checked = MacroscopePreferencesManager.GetFetchXml();
        PrefsControl.checkBoxFetchBinaries.Checked = MacroscopePreferencesManager.GetFetchBinaries();

        // Analysis Options

        PrefsControl.checkBoxCheckHreflangs.Checked = MacroscopePreferencesManager.GetCheckHreflangs();
        PrefsControl.checkBoxScanSitesInList.Checked = MacroscopePreferencesManager.GetScanSitesInList();
        PrefsControl.checkBoxWarnAboutInsecureLinks.Checked = MacroscopePreferencesManager.GetWarnAboutInsecureLinks();

        // SEO Options

        PrefsControl.numericUpDownTitleMinLen.Value = MacroscopePreferencesManager.GetTitleMinLen();
        PrefsControl.numericUpDownTitleMaxLen.Value = MacroscopePreferencesManager.GetTitleMaxLen();
        PrefsControl.numericUpDownTitleMinWords.Value = MacroscopePreferencesManager.GetTitleMinWords();
        PrefsControl.numericUpDownTitleMaxWords.Value = MacroscopePreferencesManager.GetTitleMaxWords();
        PrefsControl.numericUpDownTitleMaxPixelWidth.Value = MacroscopePreferencesManager.GetTitleMaxPixelWidth();

        PrefsControl.numericUpDownDescriptionMinLen.Value = MacroscopePreferencesManager.GetDescriptionMinLen();
        PrefsControl.numericUpDownDescriptionMaxLen.Value = MacroscopePreferencesManager.GetDescriptionMaxLen();
        PrefsControl.numericUpDownDescriptionMinWords.Value = MacroscopePreferencesManager.GetDescriptionMinWords();
        PrefsControl.numericUpDownDescriptionMaxWords.Value = MacroscopePreferencesManager.GetDescriptionMaxWords();

        PrefsControl.numericUpDownMaxHeadingDepth.Value = MacroscopePreferencesManager.GetMaxHeadingDepth();

        PrefsControl.checkBoxAnalyzeKeywordsInText.Checked = MacroscopePreferencesManager.GetAnalyzeKeywordsInText();

      }

    }

    private void SavePrefsFormControlFields ( MacroscopePrefsControl PrefsControl )
    {

      // WebProxy Options

      MacroscopePreferencesManager.SetHttpProxyHost( PrefsControl.textBoxHttpProxyHost.Text );
      MacroscopePreferencesManager.SetHttpProxyPort( ( int )PrefsControl.numericUpDownHttpProxyPort.Value );

      // Spidering Control

      MacroscopePreferencesManager.SetMaxThreads( ( int )PrefsControl.numericUpDownMaxThreads.Value );
      MacroscopePreferencesManager.SetDepth( ( int )PrefsControl.numericUpDownDepth.Value );
      MacroscopePreferencesManager.SetPageLimit( ( int )PrefsControl.numericUpDownPageLimit.Value );
      MacroscopePreferencesManager.SetRequestTimeout( ( int )PrefsControl.numericUpDownRequestTimeout.Value );
      MacroscopePreferencesManager.SetMaxRetries( ( int )PrefsControl.numericUpDownMaxRetries.Value );

      MacroscopePreferencesManager.SetCheckExternalLinks( PrefsControl.checkBoxCheckExternalLinks.Checked );
      MacroscopePreferencesManager.SetFollowRobotsProtocol( PrefsControl.checkBoxFollowRobotsProtocol.Checked );
      MacroscopePreferencesManager.SetFollowSitemapLinks( PrefsControl.checkBoxFollowSitemapLinks.Checked );
      MacroscopePreferencesManager.SetFollowRedirects( PrefsControl.checkBoxFollowRedirects.Checked );
      MacroscopePreferencesManager.SetFollowNoFollow( PrefsControl.checkBoxFollowNoFollow.Checked );
      MacroscopePreferencesManager.SetFollowCanonicalLinks( PrefsControl.checkBoxFollowCanonicalLinks.Checked );
      MacroscopePreferencesManager.SetFollowHrefLangLinks( PrefsControl.checkBoxFollowHrefLangLinks.Checked );

      MacroscopePreferencesManager.SetFetchStylesheets( PrefsControl.checkBoxFetchStylesheets.Checked );
      MacroscopePreferencesManager.SetFetchImages( PrefsControl.checkBoxFetchJavascripts.Checked );
      MacroscopePreferencesManager.SetFetchImages( PrefsControl.checkBoxFetchImages.Checked );
      MacroscopePreferencesManager.SetFetchPdfs( PrefsControl.checkBoxFetchPdfs.Checked );
      MacroscopePreferencesManager.SetFetchAudio( PrefsControl.checkBoxFetchAudio.Checked );
      MacroscopePreferencesManager.SetFetchVideo( PrefsControl.checkBoxFetchVideo.Checked );
      MacroscopePreferencesManager.SetFetchXml( PrefsControl.checkBoxFetchXml.Checked );
      MacroscopePreferencesManager.SetFetchBinaries( PrefsControl.checkBoxFetchBinaries.Checked );

      // Analysis Options

      MacroscopePreferencesManager.SetCheckHreflangs( PrefsControl.checkBoxCheckHreflangs.Checked );
      MacroscopePreferencesManager.SetScanSitesInList( PrefsControl.checkBoxScanSitesInList.Checked );
      MacroscopePreferencesManager.SetWarnAboutInsecureLinks( PrefsControl.checkBoxWarnAboutInsecureLinks.Checked );

      // SEO Options

      MacroscopePreferencesManager.SetTitleMinLen( ( int )PrefsControl.numericUpDownTitleMinLen.Value );
      MacroscopePreferencesManager.SetTitleMaxLen( ( int )PrefsControl.numericUpDownTitleMaxLen.Value );
      MacroscopePreferencesManager.SetTitleMinWords( ( int )PrefsControl.numericUpDownTitleMinWords.Value );
      MacroscopePreferencesManager.SetTitleMaxWords( ( int )PrefsControl.numericUpDownTitleMaxWords.Value );
      MacroscopePreferencesManager.SetTitleMaxPixelWidth( ( int )PrefsControl.numericUpDownTitleMaxPixelWidth.Value );

      MacroscopePreferencesManager.SetDescriptionMinLen( ( int )PrefsControl.numericUpDownDescriptionMinLen.Value );
      MacroscopePreferencesManager.SetDescriptionMaxLen( ( int )PrefsControl.numericUpDownDescriptionMaxLen.Value );
      MacroscopePreferencesManager.SetDescriptionMinWords( ( int )PrefsControl.numericUpDownDescriptionMinWords.Value );
      MacroscopePreferencesManager.SetDescriptionMaxWords( ( int )PrefsControl.numericUpDownDescriptionMaxWords.Value );

      MacroscopePreferencesManager.SetMaxHeadingDepth( ( ushort )PrefsControl.numericUpDownMaxHeadingDepth.Value );

      MacroscopePreferencesManager.SetAnalyzeKeywordsInText( PrefsControl.checkBoxAnalyzeKeywordsInText.Checked );

      // Tidy Up

      MacroscopePreferencesManager.SavePreferences();
      MacroscopePreferencesManager.ConfigureHttpProxy();

    }

    /** Help Menu *************************************************************/

    private void CallbackHelpBlogClick ( object sender, EventArgs e )
    {
      Process.Start( "https://seo-macroscope.blogspot.com/" );
    }

    private void CallbackHelpGitHubClick ( object sender, EventArgs e )
    {
      Process.Start( "https://github.com/nazuke/SEOMacroscope" );
    }
    
    private void CallbackHelpLicenceClick ( object sender, EventArgs e )
    {
      MacroscopeLicenceForm LicenceForm = new MacroscopeLicenceForm ();
      string LicenceText;
      StreamReader Reader = new StreamReader ( Assembly.GetExecutingAssembly().GetManifestResourceStream( "LICENCE" ) );
      LicenceText = Reader.ReadToEnd();
      Reader.Close();
      Reader.Dispose();
      LicenceForm.richTextBoxLicence.Text = LicenceText.ToString();
      LicenceForm.ShowDialog();
      LicenceForm.Dispose();
    }

    private void CallbackHelpAboutClick ( object sender, EventArgs e )
    {
      MacroscopeAboutForm AboutForm = new MacroscopeAboutForm ();
      AboutForm.ShowDialog();
      AboutForm.Dispose();
    }

    /** DIALOGUE BOXES ********************************************************/

    private void DialogueBoxWarning ( string sTitle, string sMessage )
    {
      MessageBox.Show(
        sMessage,
        sTitle,
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button1
      );
    }

    private void DialogueBoxError ( string sTitle, string sMessage )
    {
      MessageBox.Show(
        sMessage,
        sTitle,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error,
        MessageBoxDefaultButton.Button1
      );
    }

    private void DialogueBoxStartUrlInvalid ()
    {
      DialogueBoxError( "Error", "Please enter a valid URL" );
    }

    /** MAIN CONTROL STRIP CALLBACKS ******************************************/

    private void CallbackStartUrlTextChanged ( object sender, EventArgs e )
    {

      string sStartUrl = this.GetUrl();
      this.StartUrlDirty = true;

      if( MacroscopeUrlTools.ValidateUrl( sStartUrl ) )
      {
        MacroscopePreferencesManager.SetStartUrl( sStartUrl );
      }

    }

    private void CallbackStartUrlKeyUp ( object sender, KeyEventArgs e )
    {

      switch( e.KeyCode )
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

    }

    /**************************************************************************/

    private void CallbackScanStart ( object sender, EventArgs e )
    {
      this.CallackScanStartExecute();
    }

    /** NORMAL SCAN ***********************************************************/

    private void CallackScanStartExecute ()
    {

      string sStartUrl = this.GetUrl();

      if( MacroscopeUrlTools.ValidateUrl( sStartUrl ) )
      {

        this.ScanningControlsStart( true );

        if( this.StartUrlDirty )
        {
          this.JobMaster.ClearAllQueues();
          this.JobMaster = new MacroscopeJobMaster ( MacroscopeConstants.RunTimeMode.LIVE, this );
          this.JobMaster.SetIncludeExcludeUrls( this.IncludeExcludeUrls );
          this.ClearDisplay();
          this.StartUrlDirty = false;
        }

        MacroscopePreferencesManager.SetStartUrl( sStartUrl );

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

    private void CallackScanStartUrlListFileExecute ( string sPath )
    {

      this.JobMaster.ClearAllQueues();
      this.JobMaster = new MacroscopeJobMaster ( MacroscopeConstants.RunTimeMode.LISTFILE, this );
      this.JobMaster.SetIncludeExcludeUrls( this.IncludeExcludeUrls );
      this.ClearDisplay();

      MacroscopeUrlListLoader msUrlListLoader = new MacroscopeUrlListLoader ( this.JobMaster, sPath );

      if( msUrlListLoader != null )
      {

        if( msUrlListLoader.Execute() )
        {

          string sStartUrl = msUrlListLoader.GetUrlListItem( 0 );

          this.SetUrl( sStartUrl );

          if( MacroscopeUrlTools.ValidateUrl( sStartUrl ) )
          {

            this.ScanningControlsStart( true );

            MacroscopePreferencesManager.SetStartUrl( sStartUrl );

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

    void CallbackScanStop ( object sender, EventArgs e )
    {

      this.ScanningControlsStopping( true );

      this.JobMaster.StopWorkers();

      while( this.JobMaster.CountRunningThreads() > 0 )
      {
        DebugMsg( "CallbackScanStop: WAITING" );
        Thread.Sleep( 100 );
      }

      this.ScanningControlsStopped( true );

    }

    /**************************************************************************/

    void CallbackScanReset ( object sender, EventArgs e )
    {

      if( this.JobMaster.WorkersStopped() )
      {

        this.ScanningControlsReset( true );

        this.JobMaster.ClearAllQueues();

        this.JobMaster = new MacroscopeJobMaster ( MacroscopeConstants.RunTimeMode.LIVE, this );

        this.JobMaster.SetIncludeExcludeUrls( this.IncludeExcludeUrls );

        this.ClearDisplay();

      }

    }

    /**************************************************************************/

    public void ICallbackScanComplete ()
    {
      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker (
            delegate
            {
              this.ScanningControlsComplete( true );
            }
          )
        );
      }
      else
      {
        this.ScanningControlsComplete( true );
      }
    }

    /** TAB PAGES *************************************************************/

    void StartTabPageTimer ( int Delay )
    {
      this.TimerTabPages = new System.Timers.Timer ( Delay );
      this.TimerTabPages.Elapsed += this.CallbackTabPageTimer;
      this.TimerTabPages.AutoReset = true;
      this.TimerTabPages.Enabled = true;
      this.TimerTabPages.Start();
    }

    void StopTabPageTimer ()
    {
      try
      {
        this.TimerTabPages.Stop();
        this.TimerTabPages.Dispose();
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "StopTabPageTimer: {0}", ex.Message ) );
      }
    }

    void CallbackTabPageTimer ( Object self, ElapsedEventArgs e )
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

    void CallbackTabPageTimerExec ()
    {

      this.SemaphoreOverviewTabPages.WaitOne();

      //DebugMsg( string.Format( "CallbackTabPageTimerExec: {0}", "SEMAPHORE ACQUIRED" ) );

      TabControl tcDisplay = this.macroscopeOverviewTabPanelInstance.tabControlMain;
      string sTabPageName = tcDisplay.TabPages[ tcDisplay.SelectedIndex ].Name;

      if( this.JobMaster.PeekUpdateDisplayQueue() )
      {
        this.UpdateTabPage( sTabPageName );
        this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayQueue );
      }

      this.SemaphoreOverviewTabPages.Release( 1 );

      //DebugMsg( string.Format( "CallbackTabPageTimerExec: {0}", "SEMAPHORE RELEASED" ) );

    }

    void CallbackTabControlDisplaySelectedIndexChanged ( Object sender, EventArgs e )
    {
      TabControl tcDisplay = this.macroscopeOverviewTabPanelInstance.tabControlMain;
      string sTabPageName = tcDisplay.TabPages[ tcDisplay.SelectedIndex ].Name;
      this.UpdateTabPage( sTabPageName );
    }

    void UpdateTabPage ( string sName )
    {

      switch( sName )
      {

        case "tabPageStructureOverview":
          this.msDisplayStructure.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayStructure )
          );
          break;

        case "tabPageHierarchy":
          this.msDisplayHierarchy.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList:	this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayHierarchy )
          );
          break;

        case "tabPageRobots":
          this.msDisplayRobots.RefreshData(
            this.JobMaster
          );
          break;

        case "tabPageSitemaps":
          this.msDisplaySitemaps.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplaySitemaps )
          );
          break;
          
        case "tabPageCanonicalAnalysis":
          this.msDisplayCanonical.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayCanonicalAnalysis )
          );
          break;

        case "tabPageHrefLangAnalysis":
          this.msDisplayHrefLang.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            LocalesList: JobMaster.GetLocales()
          );
          break;

        case "tabPageErrors":
          this.msDisplayErrors.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case "tabPageRedirectsAudit":
          this.msDisplayRedirectsAudit.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case "tabPageUriAnalysis":
          this.msDisplayUriAnalysis.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayUriAnalysis )
          );
          break;

        case "tabPagePageTitles":
          this.msDisplayTitles.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageTitles )
          );
          break;

        case "tabPagePageDescriptions":
          this.msDisplayDescriptions.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageDescriptions )
          );
          break;

        case "tabPagePageKeywords":
          this.msDisplayKeywords.RefreshData(
            this.JobMaster.GetDocCollection(),
            UrlList:	this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageKeywords )
          );
          break;

        case "tabPagePageHeadings":
          this.msDisplayHeadings.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList:	this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageHeadings )
          );
          break;

        case "tabPageStylesheets":
          this.msDisplayStylesheets.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList:	this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayStylesheets )
          );
          break;

        case "tabPageJavascripts":
          this.msDisplayJavascripts.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList:	this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayJavascripts )
          );
          break;

        case "tabPageImages":
          this.msDisplayImages.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList:	this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayImages )
          );
          break;

        case "tabPageAudios":
          this.msDisplayAudios.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayAudios )
          );
          break;

        case "tabPageVideos":
          this.msDisplayVideos.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayVideos )
          );
          break;

        case "tabPageEmailAddresses":
          this.msDisplayEmailAddresses.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case "tabPageTelephoneNumbers":
          this.msDisplayTelephoneNumbers.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case "tabPageHostnames":
          this.msDisplayHostnames.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection()
          );
          break;

        case "tabPageHistory":
          this.msDisplayHistory.RefreshData(
            this.JobMaster.GetHistory()
          );
          break;

        case "tabPageSearch":
          break;

        default:
          DebugMsg( string.Format( "UNKNOWN TAB: {0}", sName ) );
          break;

      }

    }

    /** ListView Show Document Details on URL Click ***************************/

    void CallbackListViewShowDocumentDetailsOnUrlClick ( object sender, EventArgs e )
    {

      ListView lvListView = ( ListView )sender;
      string sUrl = "NONE";
      int iUrlCol = -1;

      lock( lvListView )
      {

        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            iUrlCol = i;
            break;
          }
        }

        if( iUrlCol > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
          {
            sUrl = lvItem.SubItems[ iUrlCol ].Text.ToString();
            this.macroscopeDocumentDetailsInstance.UpdateDisplay( this.JobMaster, sUrl );
          }
        }
        else
        {
          MessageBox.Show( "URL column not found" );
          this.macroscopeDocumentDetailsInstance.ClearData();
        }

      }

    }

    /** Overview Tab Panel Context Menu Callbacks *****************************/

    void CallbackOpenInBrowserClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView lvListView = msOwner.SourceControl as ListView;
      string sUrl = "NONE";
      int iUrlCol = -1;

      lock( lvListView )
      {

        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            iUrlCol = i;
            break;
          }
        }

        if( iUrlCol > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
          {
            sUrl = lvItem.SubItems[ iUrlCol ].Text.ToString();
          }
        }
        else
        {
          MessageBox.Show( "URL column not found" );
        }

      }

      if( sUrl != null )
      {
        this.OpenUrlInBrowser( sUrl );
      }

    }

    void CallbackAddToAllowedHosts ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView lvListView = msOwner.SourceControl as ListView;
      string sUrl = "NONE";
      int iUrlCol = -1;

      lock( lvListView )
      {
        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            iUrlCol = i;
            break;
          }
        }
        if( iUrlCol > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
          {
            sUrl = lvItem.SubItems[ iUrlCol ].Text.ToString();
          }
        }
        else
        {
          MessageBox.Show( "URL column not found" );
        }
      }

      if( sUrl != null )
      {
        this.JobMaster.GetAllowedHosts().AddFromUrl( sUrl );
        this.JobMaster.RetryLink( sUrl );
        this.RerunScanQueue();
      }

    }

    void CallbackRemoveFromAllowedHosts ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView lvListView = msOwner.SourceControl as ListView;
      string sUrl = "NONE";
      int iUrlCol = -1;

      lock( lvListView )
      {
        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            iUrlCol = i;
            break;
          }
        }
        if( iUrlCol > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
          {
            sUrl = lvItem.SubItems[ iUrlCol ].Text.ToString();
          }
        }
        else
        {
          MessageBox.Show( "URL column not found" );
        }
      }

      if( sUrl != null )
      {
        this.JobMaster.GetAllowedHosts().RemoveFromUrl( sUrl );
      }

    }

    void CallbackRetryFetchClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView lvListView = msOwner.SourceControl as ListView;
      string sUrl = "NONE";
      int iUrlCol = -1;

      lock( lvListView )
      {
        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            iUrlCol = i;
            break;
          }
        }
        if( iUrlCol > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
          {
            sUrl = lvItem.SubItems[ iUrlCol ].Text.ToString();
          }
        }
        else
        {
          MessageBox.Show( "URL column not found" );
        }
      }

      if( sUrl != null )
      {
        this.GetJobMaster().RetryLink( sUrl );
        this.RerunScanQueue();
      }

    }

    /** EXTERNAL BROWSER ******************************************************/

    void OpenUrlInBrowser ( string sUrl )
    {

      Uri uUrl = null;

      try
      {
        uUrl = new Uri ( sUrl );
      }
      catch( UriFormatException ex )
      {
        MessageBox.Show( ex.Message );
      }

      if( uUrl != null )
      {
        try
        {
          Process.Start( uUrl.ToString() );
        }
        catch( Exception ex )
        {
          MessageBox.Show( ex.Message );
        }
      }

    }

    /** STRUCTURE OVERVIEW PANEL TOOL STRIP CALLBACKS *************************/

    void CallbackStructureDocumentTypesFilterMenuItemClick ( object sender, EventArgs e )
    {
      ToolStripDropDownItem FilterMenuItem = ( ToolStripDropDownItem )sender;
      DebugMsg( string.Format( "CallbackSearchCollectionDocumentTypesFilterMenuItemClick: {0}", FilterMenuItem.Tag ) );
      MacroscopeConstants.DocumentType DocumentType = MacroscopeConstants.DocumentType.ALL;
      switch( FilterMenuItem.Tag.ToString() )
      {
        case "ALL":
          DocumentType = MacroscopeConstants.DocumentType.ALL;
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
        case "MISC":
          DocumentType = MacroscopeConstants.DocumentType.BINARY;
          break;
      }
      this.msDisplayStructure.ClearData();
      this.msDisplayStructure.RefreshData(
        this.JobMaster.GetDocCollection(),
        DocumentType
      );
    }

    void CallbackStructureButtonShowAll ( object sender, EventArgs e )
    {
      this.msDisplayStructure.ClearData();
      this.msDisplayStructure.RefreshData(
        this.JobMaster.GetDocCollection()
      );
    }

    void CallbackSearchTextBoxSearchUrlKeyUp ( object sender, KeyEventArgs e )
    {
      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;
      switch( e.KeyCode )
      {
        case Keys.Return:
          string sUrlFragment = SearchTextBox.Text;
          DebugMsg( string.Format( "CallbackSearchTextBoxSearchUrlKeyUp: {0}", sUrlFragment ) );
          if( sUrlFragment.Length > 0 )
          {
            SearchTextBox.Text = sUrlFragment;
            this.msDisplayStructure.ClearData();
            this.msDisplayStructure.RefreshData(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlFragment: sUrlFragment
            );
          }
          break;
      }
    }

    void CallbackSearchTextBoxSearchKeyUp ( object sender, KeyEventArgs e )
    {
      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;
      switch( e.KeyCode )
      {
        case Keys.Return:
          DebugMsg( string.Format( "CallbackStartUrlKeyUp: {0}", "RETURN" ) );
          MacroscopeSearchIndex	SearchIndex = this.JobMaster.GetDocCollection().GetSearchIndex();
          string sText = MacroscopeStringTools.CleanBodyText( SearchTextBox.Text );
          if( sText.Length > 0 )
          {
            SearchTextBox.Text = sText;
            List<MacroscopeDocument> DocList = SearchIndex.ExecuteSearchForDocuments(
                                                 MacroscopeSearchIndex.SearchMode.AND,
                                                 sText.Split( ' ' )
                                               );
            this.msDisplayStructure.ClearData();
            this.msDisplayStructure.RefreshData( DocList );
          }
          break;
      }
    }

    /** HIERARCHY PANEL CALLBACKS *********************************************/

    void CallbackHierarchyNodeMouseClick ( object sender, TreeNodeMouseClickEventArgs e )
    {

      string sUrl = null;

      try
      {
        sUrl = e.Node.Tag.ToString();
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "CallbackHierarchyNodeMouseClick: {0}", ex.Message ) );
      }

      if( sUrl != null )
      {
        this.macroscopeDocumentDetailsInstance.UpdateDisplay( this.JobMaster, sUrl );
      }
      else
      {
        this.macroscopeDocumentDetailsInstance.ClearData();
      }

    }

    /** SEARCH COLLECTION PANEL CALLBACKS *************************************/

    void CallbackSearchCollectionButtonClear ( object sender, EventArgs e )
    {
      this.msDisplaySearchCollection.ClearData();
    }

    void CallbackSearchCollectionTextBoxSearchKeyUp ( object sender, KeyEventArgs e )
    {

      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;

      DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp: {0}", "CALLED" ) );

      switch( e.KeyCode )
      {

        case Keys.Return:

          DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp: {0}", "RETURN" ) );

          MacroscopeSearchIndex	SearchIndex = this.JobMaster.GetDocCollection().GetSearchIndex();

          string sText = MacroscopeStringTools.CleanBodyText( SearchTextBox.Text );

          if( sText.Length > 0 )
          {

            SearchTextBox.Text = sText;

            DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp sText: {0}", sText ) );

            List<MacroscopeDocument> DocList = SearchIndex.ExecuteSearchForDocuments(
                                                 MacroscopeSearchIndex.SearchMode.AND,
                                                 sText.Split( ' ' )
                                               );

            this.msDisplaySearchCollection.ClearData();

            DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp DocList: {0}", DocList.Count ) );

            this.msDisplaySearchCollection.RefreshData( DocList );

          }

          break;

        case Keys.Escape:

          DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp: {0}", "ESCAPE" ) );
          SearchTextBox.Text = "";

          break;

        default:
          break;

      }

    }

    /** SITE OVERVIEW PANEL ***************************************************/

    void StartSiteOverviewTimer ( int Delay )
    {
      this.TimerSiteOverview = new System.Timers.Timer ( Delay );
      this.TimerSiteOverview.Elapsed += this.CallbackSiteOverviewTimer;
      this.TimerSiteOverview.AutoReset = true;
      this.TimerSiteOverview.Enabled = true;
      this.TimerSiteOverview.Start();
    }

    void SetVelocitySiteOverviewTimer ( int Delay )
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

    void StopSiteOverviewTimer ()
    {
      try
      {
        this.TimerSiteOverview.Stop();
        this.TimerSiteOverview.Dispose();
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "StopSiteOverviewTimer: {0}", ex.Message ) );
      }
    }

    void CallbackSiteOverviewTimer ( Object self, ElapsedEventArgs e )
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

    void CallbackSiteOverviewTimerExec ()
    {
      
      this.SemaphoreSiteStructureDisplay.WaitOne();

      //DebugMsg( string.Format( "SemaphoreSiteStructureDisplay: {0}", "OBTAINED" ) );
              
      this.UpdateSiteOverview();
      
      this.SemaphoreSiteStructureDisplay.Release( 1 );
      
      //DebugMsg( string.Format( "SemaphoreSiteStructureDisplay: {0}", "RELEASED" ) );
    
    }

    void UpdateSiteOverview ()
    {
      this.msSiteStructureOverview.RefreshData( this.JobMaster.GetDocCollection() );
      this.msSiteStructureSiteSpeed.RefreshSiteSpeedData( this.JobMaster.GetDocCollection() );
    }

    private void UpdateSiteOverviewKeywordAnalysis ()
    {
      if( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
      {
        this.msSiteStructureKeywordAnalysis.RefreshKeywordAnalysisData( this.JobMaster.GetDocCollection() );
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
      
      this.msDisplayCanonical.ClearData();
      this.msDisplayHrefLang.ClearData();
      
      this.msDisplayErrors.ClearData();
      this.msDisplayRedirectsAudit.ClearData();
      this.msDisplayUriAnalysis.ClearData();
      
      this.msDisplayTitles.ClearData();
      this.msDisplayDescriptions.ClearData();
      this.msDisplayKeywords.ClearData();
      this.msDisplayHeadings.ClearData();

      this.msDisplayStylesheets.ClearData();
      this.msDisplayJavascripts.ClearData();
      this.msDisplayImages.ClearData();
      this.msDisplayAudios.ClearData();
      this.msDisplayVideos.ClearData();
      
      this.msDisplayEmailAddresses.ClearData();
      this.msDisplayTelephoneNumbers.ClearData();

      this.msDisplayHostnames.ClearData();
      this.msDisplayHistory.ClearData();

      this.macroscopeDocumentDetailsInstance.ClearData();

      this.msSiteStructureOverview.ClearData();
      this.msSiteStructureSiteSpeed.ClearData();
      this.msSiteStructureKeywordAnalysis.ClearData();

    }

    /** Scanning Controls *****************************************************/

    void ScanningControlsEnable ( Boolean bState )
    {

      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;

    }

    void ScanningControlsStart ( Boolean bState )
    {

      this.loadUrlListToolStripMenuItem.Enabled = false;
      this.taskParametersToolStripMenuItem.Enabled = false;
      this.reportsToolStripMenuItem.Enabled = false;

      this.textBoxStartUrl.Enabled = false;
      this.ButtonStart.Enabled = false;
      this.ButtonStop.Enabled = true;
      this.ButtonReset.Enabled = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = false;

    }

    void ScanningControlsStopping ( Boolean bState )
    {

      this.loadUrlListToolStripMenuItem.Enabled = false;
      this.taskParametersToolStripMenuItem.Enabled = false;
      this.reportsToolStripMenuItem.Enabled = false;

      this.textBoxStartUrl.Enabled = false;
      this.ButtonStart.Enabled = false;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = false;

    }

    void ScanningControlsStopped ( Boolean bState )
    {

      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = true;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;

    }

    void ScanningControlsReset ( Boolean bState )
    {

      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;
      
      this.UpdateProgressBarScan( 0 );
    
    }

    void ScanningControlsComplete ( Boolean bState )
    {

      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = true;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;

    }

    /** MAIN SCANNING THREAD **************************************************/

    void ScanningThread ()
    {

      DebugMsg( "Scanning Thread: Started." );

      this.SetVelocitySiteOverviewTimer( Delay: 4000 );
      this.StartProgressBarScanTimer( Delay: 1000 ); // 1000ms
      this.JobMaster.SetStartUrl( this.GetUrl() );
      this.JobMaster.Execute();
      this.StopProgressBarScanTimer();
      this.UpdateProgressBarScan( 100 );
      this.SetVelocitySiteOverviewTimer( Delay: 10000 );

      {
        Thread ThreadUpdateSiteOverviewKeywordAnalysis = new Thread ( new ThreadStart ( this.UpdateSiteOverviewKeywordAnalysis ) );
        ThreadUpdateSiteOverviewKeywordAnalysis.Start();
      }

      DebugMsg( "Scanning Thread: Done." );

    }

    /** RERUN SCAN ************************************************************/

    void RerunScanQueue ()
    {

      if( this.JobMaster.WorkersStopped() )
      {

        this.ScanningControlsStart( true );

        MacroscopePreferencesManager.SavePreferences();

        this.ThreadScanner = new Thread ( new ThreadStart ( this.ScanningThread ) );
        this.ThreadScanner.Start();

      }

    }

    /** Scan Progress Bar *****************************************************/

    private void StartProgressBarScanTimer ( int Delay )
    {
      this.TimerProgressBarScan = new System.Timers.Timer ( Delay );
      this.TimerProgressBarScan.Elapsed += this.CallbackProgressBarScanTimer;
      this.TimerProgressBarScan.AutoReset = true;
      this.TimerProgressBarScan.Enabled = true;
      this.TimerProgressBarScan.Start();
    }

    private void StopProgressBarScanTimer ()
    {
      try
      {
        this.TimerProgressBarScan.Stop();
        this.TimerProgressBarScan.Dispose();
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "StopProgressBarScanTimer: {0}", ex.Message ) );
      }
    }

    private void CallbackProgressBarScanTimer ( Object self, ElapsedEventArgs e )
    {
      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker (
            delegate
            {
              this.UpdateProgressBarScan();
            }
          )
        );
      }
      else
      {
        this.UpdateProgressBarScan();
      }
    }

    private void UpdateProgressBarScan ( int Percentage )
    {
      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker (
            delegate
            {
              this.ProgressBarScan.Value = Percentage;    
            }
          )
        );
      }
      else
      {
        this.ProgressBarScan.Value = Percentage;    
      }
    }
        
    private void UpdateProgressBarScan ()
    {

      int iPercentage = 0;

      if( this.JobMaster != null )
      {

        List<decimal> Counts = this.JobMaster.GetProgress();
        decimal iTotal = Counts[ 0 ];
        decimal iProcessed = Counts[ 1 ];
        decimal iQueued = Counts[ 2 ];
        iPercentage = ( int )( ( 100 / iTotal ) * iProcessed );

        if( iPercentage < 0 )
        {
          iPercentage = 0;
        }
        else
        if( iPercentage > 100 )
        {
          iPercentage = 100;
        }

        DebugMsg( string.Format( "ProgressBarScan: iTotal {0}", iTotal ) );
        DebugMsg( string.Format( "ProgressBarScan: iProcessed {0}", iProcessed ) );
        DebugMsg( string.Format( "ProgressBarScan: iQueued {0}", iQueued ) );
        DebugMsg( string.Format( "ProgressBarScan: iPercentage {0}", iPercentage ) );

      }

      DebugMsg( string.Format( "ProgressBarScan: {0}", this.ProgressBarScan.Value ) );

      this.ProgressBarScan.Value = iPercentage;

    }

    /** Status Bar ************************************************************/

    void StartStatusBarTimer ( int Delay )
    {
      this.TimerStatusBar = new System.Timers.Timer ( Delay );
      this.TimerStatusBar.Elapsed += this.CallbackStatusBarTimer;
      this.TimerStatusBar.AutoReset = true;
      this.TimerStatusBar.Enabled = true;
      this.TimerStatusBar.Start();
    }

    void StopStatusBarTimer ()
    {
      try
      {
        this.TimerStatusBar.Stop();
        this.TimerStatusBar.Dispose();
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "StopStatusBarTimer: {0}", ex.Message ) );
      }
    }

    void CallbackStatusBarTimer ( Object self, ElapsedEventArgs e )
    {
      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker (
            delegate
            {
              this.UpdateStatusBar();
            }
          )
        );
      }
      else
      {
        this.UpdateStatusBar();
      }
    }

    void UpdateStatusBar ()
    {
      if( this.JobMaster != null )
      {
        this.toolStripThreads.Text = string.Format( "Threads: {0}", this.JobMaster.CountRunningThreads() );
        this.toolStripUrlCount.Text = string.Format( "URLs in Queue: {0}", this.JobMaster.CountUrlQueueItems() );
        this.toolStripFound.Text = string.Format( "URLs Crawled: {0}", this.JobMaster.GetDocCollection().CountDocuments() );
      }
    }

    /** Operation Toolbar Callbacks *******************************************/

    void CallbackRetryBrokenLinksClick ( object sender, EventArgs e )
    {

      DebugMsg( string.Format( "CallbackRetryBrokenLinksClick: {0}", "CALLED" ) );

      this.JobMaster.RetryBrokenLinks();

      this.RerunScanQueue();

    }

    /**************************************************************************/

    void CopyTextToClipboard ( string sText )
    {
      Clipboard.SetText( sText );
    }

    /** Load List *************************************************************/

    void CallbackLoadUrlListTextFile ( object sender, EventArgs e )
    {

      OpenFileDialog Dialog = new OpenFileDialog ();

      Dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "txt";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {
        string sPath = Dialog.FileName;

        if( File.Exists( sPath ) )
        {
          this.CallackScanStartUrlListFileExecute( sPath );
        }
        else
        {
          DialogueBoxError( "Load URL List Error", "The text file specified could not be found" );
        }

      }

      Dialog.Dispose();

    }

    /** TASK PARAMETERS CALLBACKS ********************************************/

    void CallbackIncludeUrlItemsClick ( object sender, EventArgs e )
    {

      MacroscopeIncludeUrlPatterns IncludeUrlPatterns = new MacroscopeIncludeUrlPatterns ();
      string IncludePatternsText = this.IncludeExcludeUrls.FetchIncludeUrlPatterns();

      if( IncludePatternsText.Length > 0 )
      {
        IncludeUrlPatterns.textBoxPatterns.Text = IncludePatternsText;
      }

      DialogResult IncludeUrlPatternsResult = IncludeUrlPatterns.ShowDialog();

      if( IncludeUrlPatternsResult == DialogResult.OK )
      {
        this.IncludeExcludeUrls.LoadIncludeUrlPatterns( IncludeUrlPatternsText: IncludeUrlPatterns.textBoxPatterns.Text );
      }

    }

    void CallbackExcludeUrlItemsClick ( object sender, EventArgs e )
    {

      MacroscopeExcludeUrlPatterns ExcludeUrlPatterns = new MacroscopeExcludeUrlPatterns ();
      string ExcludeUrlPatternsText = this.IncludeExcludeUrls.FetchExcludeUrlPatterns();

      if( ExcludeUrlPatternsText.Length > 0 )
      {
        ExcludeUrlPatterns.textBoxPatterns.Text = ExcludeUrlPatternsText;
      }

      DialogResult ExcludeUrlPatternsResult = ExcludeUrlPatterns.ShowDialog();

      if( ExcludeUrlPatternsResult == DialogResult.OK )
      {
        this.IncludeExcludeUrls.LoadExcludeUrlPatterns( ExcludeUrlPatternsText: ExcludeUrlPatterns.textBoxPatterns.Text );
      }

    }

    void CallbackCrawlParentDirectoriesToolStripMenuItemClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem CrawlMenuItem = sender as ToolStripMenuItem;

      DebugMsg( string.Format( "CrawlMenuItem: {0}", CrawlMenuItem.Checked ) );

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

    void CallbackCrawlChildDirectoriesToolStripMenuItemClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem CrawlMenuItem = sender as ToolStripMenuItem;

      DebugMsg( string.Format( "CrawlMenuItem: {0}", CrawlMenuItem.CheckState ) );

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

    /** Report Save Dialogue Boxes ********************************************/

    void CallbackSaveOverviewExcelReport ( object sender, EventArgs e )
    {
      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      if( Dialog.ShowDialog() == DialogResult.OK )
      {
        string sPath = Dialog.FileName;
        MacroscopeExcelOverviewReport msExcelReport = new MacroscopeExcelOverviewReport ();
        try
        {
          msExcelReport.WriteXslx( this.JobMaster, sPath );
        }
        catch( MacroscopeCannotSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Overview Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Overview Excel Report", ex.Message );
        }
      }
      Dialog.Dispose();
    }

    void CallbackSaveHrefLangExcelReport ( object sender, EventArgs e )
    {
      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      if( Dialog.ShowDialog() == DialogResult.OK )
      {
        string sPath = Dialog.FileName;
        MacroscopeExcelHrefLangReport msExcelReport = new MacroscopeExcelHrefLangReport ();
        try
        {
          msExcelReport.WriteXslx( this.JobMaster, sPath );
        }
        catch( MacroscopeCannotSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving HrefLang Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving HrefLang Excel Report", ex.Message );
        }
      }
      Dialog.Dispose();
    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    static void DebugMsg ( String sMsg )
    {
      System.Diagnostics.Debug.WriteLine( sMsg );
    }

    /**************************************************************************/

  }

}
