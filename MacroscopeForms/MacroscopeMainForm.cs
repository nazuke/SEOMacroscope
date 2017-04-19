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
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /**************************************************************************/

    Thread ThreadScanner;

    MacroscopeJobMaster JobMaster;
    MacroscopeCredentialsHttp CredentialsHttp;
    
    Boolean StartUrlDirty;

    MacroscopeDisplayStructure msDisplayStructure;
    MacroscopeDisplayHierarchy msDisplayHierarchy;

    MacroscopeDisplayRobots msDisplayRobots;
    MacroscopeDisplaySitemaps msDisplaySitemaps;

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

    private static object LockerOverviewTabPages = new object ();
    private static object LockerDocumentDetailsDisplay = new object ();
    private object LockerSiteStructureDisplay;
    private object LockerAuthenticationDialogue;

    private static object LockerTimerProgressBarScan = new object ();
    private static object LockerTimerTabPages = new object ();
    private static object LockerTimerSiteOverview = new object ();
    private static object LockerTimerStatusBar = new object ();
    private static object LockerTimerAuthentication = new object ();

        
    public System.Timers.Timer TimerProgressBarScan;
    public System.Timers.Timer TimerTabPages;
    public System.Timers.Timer TimerSiteOverview;
    public System.Timers.Timer TimerStatusBar;
    public System.Timers.Timer TimerAuthentication;

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

      this.JobMaster.SetIncludeExcludeUrls( IncludeExcludeUrls );

      this.StartUrlDirty = false;

      this.ConfigureOverviewTabPanelInstance();
      this.ConfigureDocumentDetailsInstance();
      this.ConfigureSiteStructurePanelInstance();

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
      
      this.ScanningControlsEnable( State: true );

    }

    /**************************************************************************/

    ~MacroscopeMainForm ()
    {
      DebugMsg( "MacroscopeMainForm DESTRUCTOR CALLED" );
      this.Cleanup();
    }

    /**************************************************************************/

    private void ConfigureOverviewTabPanelInstance ()
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

      this.msDisplayLinks = new MacroscopeDisplayLinks ( this, this.macroscopeOverviewTabPanelInstance.listViewLinks );
      this.msDisplayHyperlinks = new MacroscopeDisplayHyperlinks ( this, this.macroscopeOverviewTabPanelInstance.listViewHyperlinks );
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
      
      this.macroscopeOverviewTabPanelInstance.listViewStructure.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Click += this.CallbackStructureButtonShowAll;
      foreach( ToolStripDropDownItem ddItem in this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.DropDownItems )
      {
        ddItem.Click += this.CallbackStructureDocumentTypesFilterMenuItemClick;
      }
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.KeyUp += this.CallbackSearchTextBoxSearchUrlKeyUp;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.KeyUp += this.CallbackSearchTextBoxSearchKeyUp;

      // ListViewLinks
      
      this.macroscopeOverviewTabPanelInstance.listViewLinks.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripButtonLinksShowAll.Click += this.CallbackButtonLinksShowAll;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxLinksSearchSourceUrls.KeyUp += this.CallbackSearchTextBoxLinksSearchSourceUrlKeyUp;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxLinksSearchTargetUrls.KeyUp += this.CallbackSearchTextBoxLinksSearchTargetUrlKeyUp;
            
      // ListViewHyperlinks
      
      this.macroscopeOverviewTabPanelInstance.listViewHyperlinks.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripButtonHyperlinksShowAll.Click += this.CallbackButtonHyperlinksShowAll;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchSourceUrls.KeyUp += this.CallbackSearchTextBoxHyperlinksSearchSourceUrlKeyUp;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchTargetUrls.KeyUp += this.CallbackSearchTextBoxHyperlinksSearchTargetUrlKeyUp;

      // treeViewHierarchy etc...
      
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
      this.macroscopeOverviewTabPanelInstance.listViewStylesheets.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewJavascripts.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewImages.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewVideos.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewEmailAddresses.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewTelephoneNumbers.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.listViewHistory.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;

      // listViewSearchCollection
      
      this.macroscopeOverviewTabPanelInstance.listViewSearchCollection.ItemSelectionChanged += this.CallbackListViewShowDocumentDetailsOnUrlClick;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Click += this.CallbackSearchCollectionButtonClear;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.KeyUp += this.CallbackSearchCollectionTextBoxSearchKeyUp;

      // Context Menu Events

      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemOpenInBrowser.Click += this.CallbackOpenInBrowserClick;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemAddHostToAllowedHosts.Click += this.CallbackAddToAllowedHosts;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemRemoveFromAllowedHosts.Click += this.CallbackRemoveFromAllowedHosts;
      this.macroscopeOverviewTabPanelInstance.toolStripMenuItemResetEntry.Click += this.CallbackRetryFetchClick;

    }

    /**************************************************************************/

    private void ConfigureMenus ()
    {
      this.crawlParentDirectoriesToolStripMenuItem.Checked = MacroscopePreferencesManager.GetCrawlParentDirectories();
      this.crawlChildDirectoriesToolStripMenuItem.Checked = MacroscopePreferencesManager.GetCrawlChildDirectories();
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

    private void Cleanup ()
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

      DebugMsg( string.Format( "MacroscopeMainForm Cleanup: DONE." ) );
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

    public void SetUrl ( string sUrl )
    {
      this.textBoxStartUrl.Text = sUrl;
    }

    public string GetUrl ()
    {
      string sUrl = this.textBoxStartUrl.Text;
      sUrl = sUrl.Replace( "\r", "" );
      sUrl = sUrl.Replace( "\n", "" );
      this.textBoxStartUrl.Text = sUrl;
      return( this.textBoxStartUrl.Text );
    }

    /**************************************************************************/

    private void CallbackFormClosing ( object sender, FormClosingEventArgs e )
    {
      this.Cleanup();
    }

    /** MAIN MENU *************************************************************/

    private void CallbackFileExit ( object sender, EventArgs e )
    {
      DebugMsg( "CallbackFileExit Called" );
      this.Cleanup();
      Program.Exit();
    }

    /** Edit Menu *************************************************************/

    private void CallbackEditPreferencesClick ( object sender, EventArgs e )
    {
      MacroscopePrefsForm PreferencesForm = new MacroscopePrefsForm ();
      DialogResult PreferencesResult = PreferencesForm.ShowDialog();
      if( PreferencesResult == DialogResult.OK )
      {
        PreferencesForm.SavePrefsFormControlFields();
      }
      PreferencesForm.Dispose();
    }

    /** Help Menu *************************************************************/

    private void CallbackHelpBlogClick ( object sender, EventArgs e )
    {
      Process.Start( "https://nazuke.github.io/SEOMacroscope/blog/" );
    }

    private void CallbackHelpGitHubClick ( object sender, EventArgs e )
    {
      Process.Start( "https://github.com/nazuke/SEOMacroscope" );
    }
    
    private void CallbackHelpManualClick ( object sender, EventArgs e )
    {
      Process.Start( "https://nazuke.github.io/SEOMacroscope/manual/" );
    }
    
    private void CallbackHelpLicenceClick ( object sender, EventArgs e )
    {
      MacroscopeLicenceForm LicenceForm = new MacroscopeLicenceForm ();
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
      DialogueBoxError( "Error", "Please enter a valid URL" );
    }

    /** MAIN CONTROL STRIP CALLBACKS ******************************************/

    private void CallbackStartUrlTextChanged ( object sender, EventArgs e )
    {

      string sStartUrl = this.GetUrl();
      this.StartUrlDirty = true;

      if( MacroscopeUrlUtils.ValidateUrl( sStartUrl ) )
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

      if( MacroscopeUrlUtils.ValidateUrl( sStartUrl ) )
      {

        this.ScanningControlsStart( true );

        if( this.StartUrlDirty )
        {
          this.JobMaster.ClearAllQueues();
          this.JobMaster = new MacroscopeJobMaster (
            JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
            TaskController: this
          );
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

    private void CallackScanStartUrlListFileExecute ( string Path )
    {

      this.JobMaster.ClearAllQueues();
      this.JobMaster = new MacroscopeJobMaster (
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LISTFILE,
        TaskController: this
      );
      this.JobMaster.SetIncludeExcludeUrls( this.IncludeExcludeUrls );
      this.ClearDisplay();

      MacroscopeUrlListLoader msUrlListLoader = new MacroscopeUrlListLoader (
                                                  JobMaster: this.JobMaster,
                                                  Path: Path
                                                );

      if( msUrlListLoader != null )
      {

        if( msUrlListLoader.Execute() )
        {

          string sStartUrl = msUrlListLoader.GetUrlListItem( 0 );

          this.SetUrl( sStartUrl );

          if( MacroscopeUrlUtils.ValidateUrl( sStartUrl ) )
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

    /** SCAN FROM URL TEXT LIST ***********************************************/

    private void CallackScanStartUrlListFromClipboardExecute ( string [] UrlListText )
    {

      this.JobMaster.ClearAllQueues();
      this.JobMaster = new MacroscopeJobMaster (
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LISTTEXT,
        TaskController: this
      );
      this.JobMaster.SetIncludeExcludeUrls( this.IncludeExcludeUrls );
      this.ClearDisplay();

      MacroscopeUrlListLoader msUrlListLoader = new MacroscopeUrlListLoader (
                                                  JobMaster: this.JobMaster,
                                                  UrlListText: UrlListText
                                                );

      if( msUrlListLoader != null )
      {

        if( msUrlListLoader.Execute() )
        {

          string sStartUrl = msUrlListLoader.GetUrlListItem( 0 );

          this.SetUrl( sStartUrl );

          if( MacroscopeUrlUtils.ValidateUrl( sStartUrl ) )
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

    private void CallbackScanStop ( object sender, EventArgs e )
    {

      this.ScanningControlsStopping( true );

      this.JobMaster.StopWorkers();

      while( this.JobMaster.CountRunningThreads() > 0 )
      {
        Thread.Sleep( 100 );
      }

      this.ScanningControlsStopped( true );

    }

    /**************************************************************************/

    private void CallbackScanReset ( object sender, EventArgs e )
    {

      if( this.JobMaster.WorkersStopped() )
      {

        this.ScanningControlsReset( true );

        this.JobMaster.ClearAllQueues();

        this.JobMaster = new MacroscopeJobMaster (
          JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
          TaskController: this
        );

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
              this.UpdateFocusedTabPage();
            }
          )
        );
      }
      else
      {
        this.ScanningControlsComplete( true );
        this.UpdateFocusedTabPage();
      }
    }

    /** TAB PAGES *************************************************************/

    private void StartTabPageTimer ( int Delay )
    {
      this.TimerTabPages = new System.Timers.Timer ( Delay );
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
          this.TimerTabPages.Dispose();
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

        //DebugMsg( string.Format( "CallbackTabPageTimer: {0}", "OBTAINED LOCK" ) );
        
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
          //DebugMsg( string.Format( "CallbackTabPageTimer: {0}", "RELEASED LOCK" ) );
        }

      }
      else
      {
        //DebugMsg( string.Format( "CallbackTabPageTimer: {0}", "CANNOT OBTAIN LOCK" ) );
      }
      
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTabPageTimerExec ()
    {
      if( !MacroscopePreferencesManager.GetPauseDisplayDuringScan() )
      {
        this.UpdateFocusedTabPage();
      }
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTabControlDisplaySelectedIndexChanged ( Object sender, EventArgs e )
    {

      if( Monitor.TryEnter( LockerOverviewTabPages, 250 ) )
      {

        //DebugMsg( string.Format( "CallbackTabControlDisplaySelectedIndexChanged: {0}", "OBTAINED LOCK" ) );
        
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
          //DebugMsg( string.Format( "CallbackTabControlDisplaySelectedIndexChanged: {0}", "RELEASED LOCK" ) );
        }

      }
      else
      {
        //DebugMsg( string.Format( "CallbackTabControlDisplaySelectedIndexChanged: {0}", "CANNOT OBTAIN LOCK" ) );
      }

    }

    /** -------------------------------------------------------------------- **/

    private void UpdateFocusedTabPage ()
    {

      if( Monitor.TryEnter( LockerOverviewTabPages, 250 ) )
      {

        //DebugMsg( string.Format( "UpdateFocusedTabPage: {0}", "OBTAINED LOCK" ) );
        
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
          //DebugMsg( string.Format( "UpdateFocusedTabPage: {0}", "RELEASED LOCK" ) );
        }

      }
      else
      {
        //DebugMsg( string.Format( "UpdateFocusedTabPage: {0}", "CANNOT OBTAIN LOCK" ) );
      }

    }

    /** -------------------------------------------------------------------- **/

    private void UpdateTabPage ( string TabName )
    {
     
      switch( TabName )
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
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayHierarchy )
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

        case "tabPageLinks":
          this.msDisplayLinks.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayLinks )
          );
          break;
          
        case "tabPageHyperlinks":
          this.msDisplayHyperlinks.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayHyperlinks )
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
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageKeywords )
          );
          break;

        case "tabPagePageHeadings":
          this.msDisplayHeadings.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageHeadings )
          );
          break;

        case "tabPageStylesheets":
          this.msDisplayStylesheets.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayStylesheets )
          );
          break;

        case "tabPageJavascripts":
          this.msDisplayJavascripts.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayJavascripts )
          );
          break;

        case "tabPageImages":
          this.msDisplayImages.RefreshData(
            DocCollection: this.JobMaster.GetDocCollection(),
            UrlList: this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayImages )
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
          DebugMsg( string.Format( "UNKNOWN TAB: {0}", TabName ) );
          break;

      }
      
    }

    /** ListView Show Document Details on URL Click ***************************/

    private void CallbackListViewShowDocumentDetailsOnUrlClick ( object sender, ListViewItemSelectionChangedEventArgs e )
    {
      
      if( Monitor.TryEnter( LockerDocumentDetailsDisplay, 250 ) )
      {

        try
        {

          ListView lvListView = ( ListView )sender;
          ListViewItem lvItem = lvListView.Items[ e.ItemIndex ];
          string Url = "NONE";
          int UrlColumn = -1;

          lock( lvListView )
          {

            this.macroscopeDocumentDetailsInstance.Enabled = false;
            
            for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
            {
          
              if( lvListView.Columns[ i ].Text == "URL" )
              {
                UrlColumn = i;
                break;
              }
              else
              if( lvListView.Columns[ i ].Text == "Source URL" )
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

    /** Overview Tab Panel Context Menu Callbacks *****************************/

    private void CallbackOpenInBrowserClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView lvListView = msOwner.SourceControl as ListView;
      string Url = "NONE";
      int UrlColumn = -1;

      lock( lvListView )
      {

        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            UrlColumn = i;
            break;
          }
          else
          if( lvListView.Columns[ i ].Text == "Source URL" )
          {
            UrlColumn = i;
            break;
          }
          
        }

        if( UrlColumn > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
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
        this.OpenUrlInBrowser( Url );
      }

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackAddToAllowedHosts ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView lvListView = msOwner.SourceControl as ListView;
      string Url = "NONE";
      int UrlColumn = -1;

      lock( lvListView )
      {
        
        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            UrlColumn = i;
            break;
          }
          else
          if( lvListView.Columns[ i ].Text == "Source URL" )
          {
            UrlColumn = i;
            break;
          }
                    
        }
        if( UrlColumn > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
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
        this.JobMaster.GetAllowedHosts().AddFromUrl( Url );
        this.JobMaster.RetryLink( Url );
        this.RerunScanQueue();
      }

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackRemoveFromAllowedHosts ( object sender, EventArgs e )
    {

      ToolStripMenuItem tsMenuItem = sender as ToolStripMenuItem;
      ContextMenuStrip msOwner = tsMenuItem.Owner as ContextMenuStrip;
      ListView lvListView = msOwner.SourceControl as ListView;
      string Url = "NONE";
      int UrlColumn = -1;

      lock( lvListView )
      {
        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            UrlColumn = i;
            break;
          }
          else
          if( lvListView.Columns[ i ].Text == "Source URL" )
          {
            UrlColumn = i;
            break;
          }
                    
        }
        if( UrlColumn > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
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
      ListView lvListView = msOwner.SourceControl as ListView;
      string Url = "NONE";
      int UrlColumn = -1;

      lock( lvListView )
      {
        for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
        {
          
          if( lvListView.Columns[ i ].Text == "URL" )
          {
            UrlColumn = i;
            break;
          }
          else
          if( lvListView.Columns[ i ].Text == "Source URL" )
          {
            UrlColumn = i;
            break;
          }
                    
        }
        if( UrlColumn > -1 )
        {
          foreach( ListViewItem lvItem in lvListView.SelectedItems )
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
        this.JobMaster.RetryLink( Url );
        this.RerunScanQueue();
      }

    }

    /** EXTERNAL BROWSER ******************************************************/

    private void OpenUrlInBrowser ( string Url )
    {

      Uri OpenUrl = null;

      try
      {
        OpenUrl = new Uri ( Url );
      }
      catch( UriFormatException ex )
      {
        MessageBox.Show( ex.Message );
      }

      if( OpenUrl != null )
      {
        try
        {
          Process.Start( OpenUrl.ToString() );
        }
        catch( Exception ex )
        {
          MessageBox.Show( ex.Message );
        }
      }

    }

    /** STRUCTURE OVERVIEW PANEL TOOL STRIP CALLBACKS *************************/

    private void CallbackStructureDocumentTypesFilterMenuItemClick ( object sender, EventArgs e )
    {

      ToolStripDropDownItem FilterMenuItem = ( ToolStripDropDownItem )sender;

      DebugMsg( string.Format( "CallbackSearchCollectionDocumentTypesFilterMenuItemClick: {0}", FilterMenuItem.Tag ) );

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
          DebugMsg( string.Format( "CallbackStartUrlKeyUp: {0}", "RETURN" ) );
          MacroscopeSearchIndex	SearchIndex = this.JobMaster.GetDocCollection().GetSearchIndex();
          string SearchText = MacroscopeStringTools.CleanBodyText( SearchTextBox.Text );
          if( SearchText.Length > 0 )
          {
            SearchTextBox.Text = SearchText;
            List<MacroscopeDocument> DocList = SearchIndex.ExecuteSearchForDocuments(
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
      this.TimerSiteOverview = new System.Timers.Timer ( Delay );
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
          this.TimerSiteOverview.Dispose();
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
        
        //DebugMsg( string.Format( "CallbackSiteOverviewTimer: {0}", "OBTAINED LOCK" ) );
        
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
          //DebugMsg( string.Format( "CallbackSiteOverviewTimer: {0}", "RELEASED LOCK" ) );
        }
        
      }
      else
      {
        //DebugMsg( string.Format( "CallbackSiteOverviewTimer: {0}", "CANNOT OBTAIN LOCK" ) );
      }
            
    }

    /** -------------------------------------------------------------------- **/
    
    private void CallbackSiteOverviewTimerExec ()
    {
      lock( this.LockerSiteStructureDisplay )
      {
        this.UpdateSiteOverview();
      }
    }

    /** -------------------------------------------------------------------- **/
    
    private void UpdateSiteOverview ()
    {
      this.msSiteStructureOverview.RefreshData( this.JobMaster.GetDocCollection() );
      this.msSiteStructureSiteSpeed.RefreshSiteSpeedData( this.JobMaster.GetDocCollection() );
    }

    /** -------------------------------------------------------------------- **/
    
    private void UpdateSiteOverviewKeywordAnalysis ()
    {
      if( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
      {
        this.msSiteStructureKeywordAnalysis.RefreshKeywordAnalysisData( this.JobMaster.GetDocCollection() );
      }
    }

    /** SITE OVERVIEW PANEL CALLBACKS *****************************************/

    private void CallbackListViewSiteStructureKeywordsSearch ( object sender, EventArgs e )
    {

      ListView lvListView = ( ListView )sender;
      string KeywordTerm = "";
      int TermCol = -1;

      this.msDisplaySearchCollection.ClearData();
              
      for( int i = 0 ; i < lvListView.Columns.Count ; i++ )
      {
        if( lvListView.Columns[ i ].Text == "Term" )
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

        foreach( ListViewItem lvItem in lvListView.SelectedItems )
        {

          KeywordTerm = lvItem.SubItems[ TermCol ].Text;
          string SearchText = MacroscopeStringTools.CleanBodyText( KeywordTerm );

          if( SearchText.Length > 0 )
          {
            List<MacroscopeDocument> DocList = SearchIndex.ExecuteSearchForDocuments(
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

    private void ScanningControlsEnable ( Boolean State )
    {

      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;
      this.toolStripButtonRetryTimedOutLinks.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripButtonHyperlinksShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchSourceUrls.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchTargetUrls.Enabled = true;

    }

    private void ScanningControlsStart ( Boolean State )
    {

      this.loadUrlListToolStripMenuItem.Enabled = false;
      this.taskParametersToolStripMenuItem.Enabled = false;
      this.reportsToolStripMenuItem.Enabled = false;

      this.textBoxStartUrl.Enabled = false;
      this.ButtonStart.Enabled = false;
      this.ButtonStop.Enabled = true;
      this.ButtonReset.Enabled = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = false;
      this.toolStripButtonRetryTimedOutLinks.Enabled = false;
      
      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripButtonHyperlinksShowAll.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchSourceUrls.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchTargetUrls.Enabled = false;

    }

    private void ScanningControlsStopping ( Boolean State )
    {

      this.loadUrlListToolStripMenuItem.Enabled = false;
      this.taskParametersToolStripMenuItem.Enabled = false;
      this.reportsToolStripMenuItem.Enabled = false;

      this.textBoxStartUrl.Enabled = false;
      this.ButtonStart.Enabled = false;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = false;
      this.toolStripButtonRetryTimedOutLinks.Enabled = false;
      
      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripButtonHyperlinksShowAll.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchSourceUrls.Enabled = false;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchTargetUrls.Enabled = false;
      
    }

    private void ScanningControlsStopped ( Boolean State )
    {

      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = true;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;
      this.toolStripButtonRetryTimedOutLinks.Enabled = false;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripButtonHyperlinksShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchSourceUrls.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchTargetUrls.Enabled = true;
      
      this.UpdateProgressBarScan( 0 );

    }

    private void ScanningControlsReset ( Boolean State )
    {

      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;
      this.toolStripButtonRetryTimedOutLinks.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;
      
      this.macroscopeOverviewTabPanelInstance.toolStripButtonHyperlinksShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchSourceUrls.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchTargetUrls.Enabled = true;
      
      this.UpdateProgressBarScan( 0 );
    
    }

    private void ScanningControlsComplete ( Boolean State )
    {

      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = true;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;
      this.toolStripButtonRetryTimedOutLinks.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripStructureFilterMenu.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureButtonShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearchUrl.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;

      this.macroscopeOverviewTabPanelInstance.toolStripButtonHyperlinksShowAll.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchSourceUrls.Enabled = true;
      this.macroscopeOverviewTabPanelInstance.toolStripTextBoxHyperlinksSearchTargetUrls.Enabled = true;
      
    }

    /** MAIN SCANNING THREAD **************************************************/

    private void ScanningThread ()
    {

      DebugMsg( "Scanning Thread: Started." );

      this.SetVelocitySiteOverviewTimer( Delay: 4000 );
      this.StartProgressBarScanTimer( Delay: 1000 ); // 1000ms
      this.JobMaster.SetStartUrl( this.GetUrl() );
      this.JobMaster.Execute();
      this.StopProgressBarScanTimer();
      this.UpdateProgressBarScan( 0 );
      this.SetVelocitySiteOverviewTimer( Delay: 10000 );

      {
        Thread ThreadUpdateSiteOverviewKeywordAnalysis = new Thread ( new ThreadStart ( this.UpdateSiteOverviewKeywordAnalysis ) );
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

      if( this.JobMaster.WorkersStopped() )
      {

        this.ScanningControlsStart( true );

        MacroscopePreferencesManager.SavePreferences();

        this.ThreadScanner = new Thread ( new ThreadStart ( this.ScanningThread ) );
        this.ThreadScanner.Start();

      }

    }

    /** Authentication Dialogue Timer *****************************************/

    private void StartAuthenticationTimer ( int Delay )
    {
      this.TimerAuthentication = new System.Timers.Timer ( Delay );
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
          this.TimerAuthentication.Dispose();
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

        //DebugMsg( string.Format( "CallbackAuthenticationTimer: {0}", "OBTAINED LOCK" ) );
                
        try
        {
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
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "CallbackAuthenticationTimer: {0}", ex.Message ) );
        }
        finally
        {
          Monitor.Exit( LockerTimerAuthentication );
          //DebugMsg( string.Format( "CallbackAuthenticationTimer: {0}", "RELEASED LOCK" ) );
        }
              
      }
      else
      {
        //DebugMsg( string.Format( "CallbackAuthenticationTimer: {0}", "CANNOT OBTAIN LOCK" ) );
      }
            
    }

    /** -------------------------------------------------------------------- **/
        
    private void ShowAuthenticationDialogue ()
    {
      
      Boolean DoRerun = false;
      string RerunUrl = null;
      
      if( this.JobMaster != null )
      {

        if( this.CredentialsHttp.PeekCredentialRequest() )
        {

          lock( this.LockerAuthenticationDialogue )
          {

            MacroscopeCredentialRequest CredentialRequest = this.CredentialsHttp.DequeueCredentialRequest();
          
            if( this.CredentialsHttp.CredentialExists( CredentialRequest.GetDomain(), CredentialRequest.GetRealm() ) )
            {

              DebugMsg( string.Format( "CredentialExists: {0} :: {1}", CredentialRequest.GetDomain(), CredentialRequest.GetRealm() ) );

              DoRerun = true;
              RerunUrl = CredentialRequest.GetUrl();

            }
            else
            {

              MacroscopeGetCredentialsHttp CredentialsForm = new MacroscopeGetCredentialsHttp ();

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
        this.JobMaster.AddUrlQueueItem( RerunUrl );
        this.JobMaster.RetryLink( RerunUrl );
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

    /**************************************************************************/

    private void CopyTextToClipboard ( string sText )
    {
      Clipboard.SetText( sText );
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
        string [] UrlList = Regex.Split( UrlListText, "[\r\n]", RegexOptions.Singleline );
        this.CallackScanStartUrlListFromClipboardExecute( UrlList );     
      }
            
      LoadUrlListDialogue.Dispose();

    }

    /** TASK PARAMETERS CALLBACKS ********************************************/

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

    /** -------------------------------------------------------------------- **/
        
    private void CallbackCrawlChildDirectoriesToolStripMenuItemClick ( object sender, EventArgs e )
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

    /** -------------------------------------------------------------------- **/
        
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
