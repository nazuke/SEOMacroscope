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
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public partial class MacroscopeMainForm : Form
	{
		
		/**************************************************************************/

		Thread ThreadScanner;

		MacroscopeJobMaster JobMaster;

		Boolean StartUrlDirty;

		MacroscopeDisplayStructure msDisplayStructure;
		MacroscopeDisplayHierarchy msDisplayHierarchy;
		MacroscopeDisplayCanonical msDisplayCanonical;
		MacroscopeDisplayHrefLang msDisplayHrefLang;

		MacroscopeDisplayErrors msDisplayErrors;

		MacroscopeDisplayRedirectsAudit msDisplayRedirectsAudit;

		MacroscopeDisplayTitles msDisplayTitles;
		MacroscopeDisplayDescriptions msDisplayDescriptions;
		MacroscopeDisplayKeywords msDisplayKeywords;
		MacroscopeDisplayHeadings msDisplayHeadings;
		MacroscopeDisplayRobots msDisplayRobots;
		MacroscopeDisplaySitemaps msDisplaySitemaps;
		MacroscopeDisplayEmailAddresses msDisplayEmailAddresses;
		MacroscopeDisplayTelephoneNumbers msDisplayTelephoneNumbers;
		MacroscopeDisplayHostnames msDisplayHostnames;
		MacroscopeDisplayHistory msDisplayHistory;

		MacroscopeDisplayStructureOverview msSiteStructureOverview;

		Semaphore SemaphoreTabPages;
				
		public System.Timers.Timer TimerTabPages;
		public System.Timers.Timer TimerSiteOverview;
		public System.Timers.Timer TimerStatusBar;
			
		/**************************************************************************/

		public MacroscopeMainForm ()
		{
			
			InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

			JobMaster = new MacroscopeJobMaster ( MacroscopeConstants.RunTimeMode.LIVE, this );

			StartUrlDirty = false;

			ConfigureOverviewTabPanelInstance();
			ConfigureDocumentDetailsInstance();
			ConfigureSiteStructurePanelInstance();
			
			SetUrl( MacroscopePreferencesManager.GetStartUrl() );

			#if DEBUG
			textBoxStartUrl.Text = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" );
			#endif

			this.SemaphoreTabPages = new Semaphore ( 0, 1 );
			this.SemaphoreTabPages.Release( 1 );
			
			StartTabPageTimer();
			StartSiteOverviewTimer();
			StartStatusBarTimer();
			
			ScanningControlsEnable( true );

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
			
			// ListView Reference Objects
			this.msDisplayStructure = new MacroscopeDisplayStructure ( this, this.macroscopeOverviewTabPanelInstance.listViewStructure );
			this.msDisplayHierarchy = new MacroscopeDisplayHierarchy ( this, this.macroscopeOverviewTabPanelInstance.treeViewHierarchy );
			this.msDisplayCanonical = new MacroscopeDisplayCanonical ( this, this.macroscopeOverviewTabPanelInstance.listViewCanonicalAnalysis );
			this.msDisplayHrefLang = new MacroscopeDisplayHrefLang ( this, this.macroscopeOverviewTabPanelInstance.listViewHrefLang );
			this.msDisplayErrors = new MacroscopeDisplayErrors ( this, this.macroscopeOverviewTabPanelInstance.listViewErrors );
			this.msDisplayRedirectsAudit = new MacroscopeDisplayRedirectsAudit ( this, this.macroscopeOverviewTabPanelInstance.listViewRedirectsAudit );
			this.msDisplayTitles = new MacroscopeDisplayTitles ( this, this.macroscopeOverviewTabPanelInstance.listViewPageTitles );
			this.msDisplayDescriptions = new MacroscopeDisplayDescriptions ( this, this.macroscopeOverviewTabPanelInstance.listViewPageDescriptions );
			this.msDisplayKeywords = new MacroscopeDisplayKeywords ( this, this.macroscopeOverviewTabPanelInstance.listViewPageKeywords );
			this.msDisplayHeadings = new MacroscopeDisplayHeadings ( this, this.macroscopeOverviewTabPanelInstance.listViewPageHeadings );
			this.msDisplayRobots = new MacroscopeDisplayRobots ( this, this.macroscopeOverviewTabPanelInstance.listViewRobots );
			this.msDisplaySitemaps = new MacroscopeDisplaySitemaps ( this, this.macroscopeOverviewTabPanelInstance.listViewSitemaps );
			this.msDisplayEmailAddresses = new MacroscopeDisplayEmailAddresses ( this, this.macroscopeOverviewTabPanelInstance.listViewEmailAddresses );
			this.msDisplayTelephoneNumbers = new MacroscopeDisplayTelephoneNumbers ( this, this.macroscopeOverviewTabPanelInstance.listViewTelephoneNumbers );
			this.msDisplayHostnames = new MacroscopeDisplayHostnames ( this, this.macroscopeOverviewTabPanelInstance.listViewHostnames );
			this.msDisplayHistory = new MacroscopeDisplayHistory ( this, this.macroscopeOverviewTabPanelInstance.listViewHistory );

			// Appearance
			
			this.macroscopeOverviewTabPanelInstance.Dock = DockStyle.Fill;

			// Events
			
			this.macroscopeOverviewTabPanelInstance.tabControlMain.Click += this.CallbackTabControlDisplaySelectedIndexChanged;
			
			this.macroscopeOverviewTabPanelInstance.listViewStructure.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
			this.macroscopeOverviewTabPanelInstance.treeViewHierarchy.NodeMouseClick += this.CallbackHierarchyNodeMouseClick;
			this.macroscopeOverviewTabPanelInstance.listViewCanonicalAnalysis.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
			this.macroscopeOverviewTabPanelInstance.listViewHrefLang.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;			
			this.macroscopeOverviewTabPanelInstance.listViewErrors.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
			this.macroscopeOverviewTabPanelInstance.listViewPageTitles.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;			
			this.macroscopeOverviewTabPanelInstance.listViewPageDescriptions.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;			
			this.macroscopeOverviewTabPanelInstance.listViewPageHeadings.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;			
			this.macroscopeOverviewTabPanelInstance.listViewRobots.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;			
			this.macroscopeOverviewTabPanelInstance.listViewEmailAddresses.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;
			this.macroscopeOverviewTabPanelInstance.listViewTelephoneNumbers.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;			
			this.macroscopeOverviewTabPanelInstance.listViewHistory.Click += this.CallbackListViewShowDocumentDetailsOnUrlClick;

			// Context Menu Events
			
			this.macroscopeOverviewTabPanelInstance.toolStripMenuItemOpenInBrowser.Click += this.CallbackOpenInBrowserClick;
			this.macroscopeOverviewTabPanelInstance.toolStripMenuItemAddHostToAllowedHosts.Click += this.CallbackAddToAllowedHosts;
			this.macroscopeOverviewTabPanelInstance.toolStripMenuItemRemoveFromAllowedHosts.Click += this.CallbackRemoveFromAllowedHosts;
			this.macroscopeOverviewTabPanelInstance.toolStripMenuItemResetEntry.Click += this.CallbackRetryFetchClick;

		}
		
		/**************************************************************************/
		
		void ConfigureDocumentDetailsInstance ()
		{
			this.macroscopeDocumentDetailsInstance.Dock = DockStyle.Fill;
		}

		/**************************************************************************/
				
		void ConfigureSiteStructurePanelInstance ()
		{
			this.msSiteStructureOverview = new MacroscopeDisplayStructureOverview ( this, this.macroscopeSiteStructurePanelInstance.treeViewSiteOverview );
			this.macroscopeSiteStructurePanelInstance.Dock = DockStyle.Fill;
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

			this.StopTabPageTimer();
			this.StopSiteOverviewTimer();
			this.StopStatusBarTimer();

			if( this.JobMaster != null )
			{
				this.JobMaster.ClearAllQueues();
			}

			this.JobMaster = null;

			this.SemaphoreTabPages.Dispose();
			
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

		void CallbackEditPreferencesClick ( object sender, EventArgs e )
		{
			DebugMsg( "CallbackEditPreferencesClick Called" );
			MacroscopePrefsForm fPreferencesForm = new MacroscopePrefsForm ();
			MacroscopePrefsControl PrefsControl = fPreferencesForm.macroscopePrefsControlInstance;

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
				PrefsControl.checkBoxFetchXml.Checked = MacroscopePreferencesManager.GetFetchXml();
				PrefsControl.checkBoxFetchBinaries.Checked = MacroscopePreferencesManager.GetFetchBinaries();

				// Analysis Options
				PrefsControl.checkBoxCheckHreflangs.Checked = MacroscopePreferencesManager.GetCheckHreflangs();
			
				// SEO Options
				PrefsControl.numericUpDownTitleMinLen.Value = MacroscopePreferencesManager.GetTitleMinLen();
				PrefsControl.numericUpDownTitleMaxLen.Value = MacroscopePreferencesManager.GetTitleMaxLen();
				PrefsControl.numericUpDownTitleMinWords.Value = MacroscopePreferencesManager.GetTitleMinWords();
				PrefsControl.numericUpDownTitleMaxWords.Value = MacroscopePreferencesManager.GetTitleMaxWords();
				PrefsControl.numericUpDownDescriptionMinLen.Value = MacroscopePreferencesManager.GetDescriptionMinLen();
				PrefsControl.numericUpDownDescriptionMaxLen.Value = MacroscopePreferencesManager.GetDescriptionMaxLen();
				PrefsControl.numericUpDownDescriptionMinWords.Value = MacroscopePreferencesManager.GetDescriptionMinWords();
				PrefsControl.numericUpDownDescriptionMaxWords.Value = MacroscopePreferencesManager.GetDescriptionMaxWords();
				PrefsControl.numericUpDownMaxHeadingDepth.Value = MacroscopePreferencesManager.GetMaxHeadingDepth();

			}

			DialogResult fPreferencsResult = fPreferencesForm.ShowDialog();
			
			DebugMsg( string.Format( "CallbackEditPreferencesClick: {0}", fPreferencsResult ) );

			if( fPreferencsResult == DialogResult.OK )
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
				MacroscopePreferencesManager.SetFetchXml( PrefsControl.checkBoxFetchXml.Checked );
				MacroscopePreferencesManager.SetFetchBinaries( PrefsControl.checkBoxFetchBinaries.Checked );

				// Analysis Options
				
				MacroscopePreferencesManager.SetCheckHreflangs( PrefsControl.checkBoxCheckHreflangs.Checked );
			
				// SEO Options
				
				MacroscopePreferencesManager.SetTitleMinLen( ( int )PrefsControl.numericUpDownTitleMinLen.Value );
				MacroscopePreferencesManager.SetTitleMaxLen( ( int )PrefsControl.numericUpDownTitleMaxLen.Value );
				MacroscopePreferencesManager.SetTitleMinWords( ( int )PrefsControl.numericUpDownTitleMinWords.Value );
				MacroscopePreferencesManager.SetTitleMaxWords( ( int )PrefsControl.numericUpDownTitleMaxWords.Value );
				MacroscopePreferencesManager.SetDescriptionMinLen( ( int )PrefsControl.numericUpDownDescriptionMinLen.Value );
				MacroscopePreferencesManager.SetDescriptionMaxLen( ( int )PrefsControl.numericUpDownDescriptionMaxLen.Value );
				MacroscopePreferencesManager.SetDescriptionMinWords( ( int )PrefsControl.numericUpDownDescriptionMinWords.Value );
				MacroscopePreferencesManager.SetDescriptionMaxWords( ( int )PrefsControl.numericUpDownDescriptionMaxWords.Value );
				MacroscopePreferencesManager.SetMaxHeadingDepth( ( ushort )PrefsControl.numericUpDownMaxHeadingDepth.Value );

				// Tidy Up
				
				MacroscopePreferencesManager.SavePreferences();
				MacroscopePreferencesManager.ConfigureHttpProxy();
				
			}

		}

		/** Help Menu *************************************************************/
		
		void CallbackHelpAboutClick ( object sender, EventArgs e )
		{
			DebugMsg( "CallbackHelpAboutClick Called" );
			MacroscopeAboutForm fAboutForm = new MacroscopeAboutForm ();
			fAboutForm.ShowDialog();
		}

		/** DIALOGUE BOXES ********************************************************/
		
		void DialogueBoxWarning ( string sTitle, string sMessage )
		{
			MessageBox.Show(
				sMessage,
				sTitle,
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning,
				MessageBoxDefaultButton.Button1
			);
		}
		
		void DialogueBoxError ( string sTitle, string sMessage )
		{
			MessageBox.Show(
				sMessage,
				sTitle,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error,
				MessageBoxDefaultButton.Button1
			);
		}

		void DialogueBoxStartUrlInvalid ()
		{
			DialogueBoxError( "Error", "Please enter a valid URL" );
		}

		/** MAIN CONTROL STRIP CALLBACKS ******************************************/
				
		void CallbackStartUrlTextChanged ( object sender, EventArgs e )
		{
			
			string sStartUrl = this.GetUrl();
			this.StartUrlDirty = true;

			if( MacroscopeUrlTools.ValidateUrl( sStartUrl ) )
			{
				MacroscopePreferencesManager.SetStartUrl( sStartUrl );
			}
			
		}
		
		void CallbackStartUrlKeyUp ( object sender, KeyEventArgs e )
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

		void CallbackScanStart ( object sender, EventArgs e )
		{
			this.CallackScanStartExecute();
		}

		/**************************************************************************/
				
		void CallackScanStartExecute ()
		{
			
			string sStartUrl = this.GetUrl();
						
			if( MacroscopeUrlTools.ValidateUrl( sStartUrl ) )
			{

				this.ScanningControlsStart( true );

				if( this.StartUrlDirty )
				{
					this.JobMaster.ClearAllQueues();
					this.JobMaster = new MacroscopeJobMaster ( MacroscopeConstants.RunTimeMode.LIVE, this );
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
		
		void CallackScanStartUrlListFileExecute ( string sPath )
		{

			this.JobMaster.ClearAllQueues();
			this.JobMaster = new MacroscopeJobMaster ( MacroscopeConstants.RunTimeMode.LISTFILE, this );
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

				this.ClearDisplay();
				
			}

		}
		
		/**************************************************************************/
		
		public void CallbackScanComplete ()
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

		void StartTabPageTimer ()
		{
			this.TimerTabPages = new System.Timers.Timer ( 4000 );
			this.TimerTabPages.Elapsed += CallbackTabPageTimer;
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
			
			this.SemaphoreTabPages.WaitOne();

			TabControl tcDisplay = this.macroscopeOverviewTabPanelInstance.tabControlMain;
			string sTabPageName = tcDisplay.TabPages[ tcDisplay.SelectedIndex ].Name;

			if( this.JobMaster.PeekUpdateDisplayQueue() )
			{
				this.UpdateTabPage( sTabPageName );
				this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayQueue );
			}

			this.SemaphoreTabPages.Release( 1 );

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
						this.JobMaster.GetDocCollection(),
						this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayStructure )
					);
					break;

				case "tabPageHierarchy":
					this.msDisplayHierarchy.RefreshData(
						this.JobMaster.GetDocCollection(),
						this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayHierarchy )
					);
					break;

				case "tabPageCanonicalAnalysis":
					this.msDisplayCanonical.RefreshData(
						this.JobMaster.GetDocCollection(),
						this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayCanonicalAnalysis )
					);
					break;
									
				case "tabPageHrefLangAnalysis":
					this.msDisplayHrefLang.RefreshData( this.JobMaster.GetDocCollection(), JobMaster.GetLocales() );
					break;
							
				case "tabPageErrors":
					this.msDisplayErrors.RefreshData( this.JobMaster.GetDocCollection() );
					break;
					
				case "tabPageRedirectsAudit":
					this.msDisplayRedirectsAudit.RefreshData( this.JobMaster.GetDocCollection() );
					break;
									
				case "tabPageUriAnalysis":
					break;
									
				case "tabPagePageTitles":
					this.msDisplayTitles.RefreshData(
						this.JobMaster.GetDocCollection(),
						this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageTitles )
					);
					break;
									
				case "tabPagePageDescriptions":
					this.msDisplayDescriptions.RefreshData(
						this.JobMaster.GetDocCollection(),
						this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageDescriptions )
					);
					break;
									
				case "tabPagePageKeywords":
					this.msDisplayKeywords.RefreshData(
						this.JobMaster.GetDocCollection(),
						this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageKeywords )
					);
					break;
									
				case "tabPagePageHeadings":
					this.msDisplayHeadings.RefreshData(
						this.JobMaster.GetDocCollection(),
						this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplayPageHeadings )
					);
					break;

				case "tabPageRobots":
					this.msDisplayRobots.RefreshData(
						this.JobMaster
					);
					break;
					
				case "tabPageSitemaps":
					this.msDisplaySitemaps.RefreshData(
						this.JobMaster.GetDocCollection(),
						this.JobMaster.DrainDisplayQueueAsList( MacroscopeConstants.NamedQueueDisplaySitemaps )
					);
					break;
									
				case "tabPageEmailAddresses":
					this.msDisplayEmailAddresses.RefreshData( this.JobMaster.GetDocCollection() );
					break;
									
				case "tabPageTelephoneNumbers":
					this.msDisplayTelephoneNumbers.RefreshData( this.JobMaster.GetDocCollection() );
					break;

				case "tabPageHostnames":
					this.msDisplayHostnames.RefreshData( this.JobMaster.GetDocCollection() );
					break;
						
				case "tabPageHistory":
					this.msDisplayHistory.RefreshData( this.JobMaster.GetHistory() );
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

		/** SITE OVERVIEW PANEL ***************************************************/

		void StartSiteOverviewTimer ()
		{
			this.TimerSiteOverview = new System.Timers.Timer ( 4000 );
			this.TimerSiteOverview.Elapsed += this.CallbackSiteOverviewTimer;
			this.TimerSiteOverview.AutoReset = true;
			this.TimerSiteOverview.Enabled = true;
			this.TimerSiteOverview.Start();
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
			this.UpdateSiteOverview();
		}

		void UpdateSiteOverview ()
		{
			this.msSiteStructureOverview.RefreshData( this.JobMaster.GetDocCollection() );
		}

		/** Whole Display *********************************************************/

		public void ClearDisplay ()
		{
			this.msDisplayStructure.ClearData();
			this.msDisplayHierarchy.ClearData();
			this.msDisplayCanonical.ClearData();
			this.msDisplayHrefLang.ClearData();			
			this.msDisplayTitles.ClearData();
			this.msDisplayEmailAddresses.ClearData();
			this.msDisplayTelephoneNumbers.ClearData();
			this.msDisplayHistory.ClearData();
			this.macroscopeDocumentDetailsInstance.ClearData();
		}

		/** Scanning Controls *****************************************************/

		void ScanningControlsEnable ( Boolean bState )
		{

			this.reportsToolStripMenuItem.Enabled = true;

			this.textBoxStartUrl.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;


			this.toolStripButtonRetryBrokenLinks.Enabled = true;

		}

		void ScanningControlsStart ( Boolean bState )
		{

			this.reportsToolStripMenuItem.Enabled = false;

			this.textBoxStartUrl.Enabled = false;
			this.ButtonStart.Enabled = false;
			this.ButtonStop.Enabled = true;
			this.ButtonReset.Enabled = false;

			this.toolStripButtonRetryBrokenLinks.Enabled = false;

		}

		void ScanningControlsStopping ( Boolean bState )
		{

			this.reportsToolStripMenuItem.Enabled = false;

			this.textBoxStartUrl.Enabled = false;
			this.ButtonStart.Enabled = false;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;

			this.toolStripButtonRetryBrokenLinks.Enabled = false;

		}

		void ScanningControlsStopped ( Boolean bState )
		{
			this.reportsToolStripMenuItem.Enabled = true;

			this.textBoxStartUrl.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = true;

			this.toolStripButtonRetryBrokenLinks.Enabled = true;

		}

		void ScanningControlsReset ( Boolean bState )
		{
			this.reportsToolStripMenuItem.Enabled = true;

			this.textBoxStartUrl.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;

			this.toolStripButtonRetryBrokenLinks.Enabled = true;

		}
		
		void ScanningControlsComplete ( Boolean bState )
		{

			this.reportsToolStripMenuItem.Enabled = true;

			this.textBoxStartUrl.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = true;

			this.toolStripButtonRetryBrokenLinks.Enabled = true;

		}

		/** MAIN SCANNING THREAD **************************************************/

		void ScanningThread ()
		{
			DebugMsg( "Scanning Thread: Started." );
			this.JobMaster.SetStartUrl( this.GetUrl() );
			this.JobMaster.Execute();
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

		/** Status Bar ************************************************************/

		void StartStatusBarTimer ()
		{
			this.TimerStatusBar = new System.Timers.Timer ( 1000 );
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

		/**************************************************************************/

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
				msExcelReport.WriteXslx( this.JobMaster, sPath );
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
				msExcelReport.WriteXslx( this.JobMaster, sPath );
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
