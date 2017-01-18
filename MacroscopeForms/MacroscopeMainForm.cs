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
using System.Timers;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace SEOMacroscope
{

	public partial class MacroscopeMainForm : Form
	{
		
		/**************************************************************************/

		Thread ThreadScanner;

		MacroscopeJobMaster msJobMaster;

		Boolean StartUrlDirty;

		MacroscopeDisplayStructure msDisplayStructure;
		MacroscopeDisplayCanonical msDisplayCanonical;
		MacroscopeDisplayHrefLang msDisplayHrefLang;
		MacroscopeDisplayTitles msDisplayTitles;
		MacroscopeDisplayEmailAddresses msDisplayEmailAddresses;
		MacroscopeDisplayTelephoneNumbers msDisplayTelephoneNumbers;
		//MacroscopeDisplayHostnames msDisplayHostnames;
		MacroscopeDisplayQueue msDisplayQueue;
		MacroscopeDisplayHistory msDisplayHistory;

		System.Timers.Timer TimerTabPages;
			
		/**************************************************************************/

		public MacroscopeMainForm ()
		{
			
			InitializeComponent();// The InitializeComponent() call is required for Windows Forms designer support.

			msJobMaster = new MacroscopeJobMaster ( this );

			StartUrlDirty = false;
			
			msDisplayStructure = new MacroscopeDisplayStructure ( this );
			msDisplayCanonical = new MacroscopeDisplayCanonical ( this );
			msDisplayHrefLang = new MacroscopeDisplayHrefLang ( this );
			msDisplayTitles = new MacroscopeDisplayTitles ( this );
			msDisplayEmailAddresses = new MacroscopeDisplayEmailAddresses ( this );
			msDisplayTelephoneNumbers = new MacroscopeDisplayTelephoneNumbers ( this );
			//msDisplayHostnames = new MacroscopeDisplayHostnames(this) ;
			msDisplayQueue = new MacroscopeDisplayQueue ( this );
			msDisplayHistory = new MacroscopeDisplayHistory ( this );

			SetURL( MacroscopePreferencesManager.GetStartUrl() );

			#if DEBUG
			textBoxStartUrl.Text = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" );
			#endif

			ScanningControlsEnable( true );

			StartTabPageTimer();
			
		}
		
		/**************************************************************************/

		~MacroscopeMainForm ()
		{
			DebugMsg( "MacroscopeMainForm DESTRUCTOR CALLED" );
			
			this.Cleanup();
		}
		
		/**************************************************************************/

		void Cleanup ()
		{
			
			DebugMsg( "MacroscopeMainForm Cleanup CALLED" );
						
			MacroscopePreferencesManager.SavePreferences();
						
			if( this.ThreadScanner != null ) {
				DebugMsg( "Cleaning up ThreadScanner" );
				this.ThreadScanner.Abort();
			}

			if( this.msJobMaster != null ) {
				this.msJobMaster.ShutdownWorkerUpdateDisplay();
				this.msJobMaster = null;
			}

		}

		/**************************************************************************/
				
		public MacroscopeJobMaster GetJobMaster ()
		{
			return( msJobMaster );
		}

		/** Displays **************************************************************/

		public ListView GetDisplayStructure ()
		{
			return( this.listViewStructure );
		}

		public ListView GetDisplayCanonicalAnalysis ()
		{
			return( this.listViewCanonicalAnalysis );
		}

		public ListView GetDisplayHrefLang ()
		{
			return( this.listViewHrefLang );
		}

		public ListView GetDisplayTitles ()
		{
			return( this.listViewPageTitles );
		}

		public ListView GetDisplayEmailAddresses ()
		{
			return( this.listViewEmailAddresses );
		}
		
		public ListView GetDisplayTelephoneNumbers ()
		{
			return( this.listViewTelephoneNumbers );
		}

		public ListView GetDisplayHostnames ()
		{
			return( this.listViewHostnames );
		}
		
		public ListView GetDisplayQueue ()
		{
			return( this.listViewQueue );
		}
		
		public ListView GetDisplayHistory ()
		{
			return( this.listViewHistory );
		}

		/**************************************************************************/
				
		public string GetURL ()
		{
			return( this.textBoxStartUrl.Text );
		}

		/**************************************************************************/

		public void SetURL ( string sURL )
		{
			this.textBoxStartUrl.Text = sURL;
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

			{

				// WebProxy Options
				PrefsControl.textBoxHttpProxyHost.Text = MacroscopePreferencesManager.GetHttpProxyHost();
				PrefsControl.numericUpDownHttpProxyPort.Value = MacroscopePreferencesManager.GetHttpProxyPort();

				// Spidering Control
				PrefsControl.numericUpDownMaxThreads.Value = MacroscopePreferencesManager.GetMaxThreads();
				PrefsControl.numericUpDownDepth.Value = MacroscopePreferencesManager.GetDepth();
				PrefsControl.numericUpDownPageLimit.Value = MacroscopePreferencesManager.GetPageLimit();
				PrefsControl.checkBoxSameSite.Checked = MacroscopePreferencesManager.GetSameSite();
				PrefsControl.checkBoxFollowRobotsProtocol.Checked = MacroscopePreferencesManager.GetFollowRobotsProtocol();
				PrefsControl.checkBoxFollowRedirects.Checked = MacroscopePreferencesManager.GetFollowRedirects();
				PrefsControl.checkBoxFollowNoFollow.Checked = MacroscopePreferencesManager.GetFollowNoFollow();
				PrefsControl.checkBoxFollowCanonicalLinks.Checked = MacroscopePreferencesManager.GetFollowCanonicalLinks();
				PrefsControl.checkBoxFollowHrefLangLinks.Checked = MacroscopePreferencesManager.GetFollowHrefLangLinks();
				PrefsControl.checkBoxFetchStylesheets.Checked = MacroscopePreferencesManager.GetFetchStylesheets();
				PrefsControl.checkBoxFetchJavascripts.Checked = MacroscopePreferencesManager.GetFetchImages();
				PrefsControl.checkBoxFetchImages.Checked = MacroscopePreferencesManager.GetFetchImages();
				PrefsControl.checkBoxFetchPdfs.Checked = MacroscopePreferencesManager.GetFetchPdfs();
				PrefsControl.checkBoxFetchBinaries.Checked = MacroscopePreferencesManager.GetFetchBinaries();

				// Analysis Options
				PrefsControl.checkBoxProbeHreflangs.Checked = MacroscopePreferencesManager.GetProbeHreflangs();
			
				// SEO Options
				PrefsControl.numericUpDownTitleMinLen.Value = MacroscopePreferencesManager.GetTitleMinLen();
				PrefsControl.numericUpDownTitleMaxLen.Value = MacroscopePreferencesManager.GetTitleMaxLen();
				PrefsControl.numericUpDownTitleMinWords.Value = MacroscopePreferencesManager.GetTitleMinWords();
				PrefsControl.numericUpDownTitleMaxWords.Value = MacroscopePreferencesManager.GetTitleMaxWords();
				PrefsControl.numericUpDownDescriptionMinLen.Value = MacroscopePreferencesManager.GetDescriptionMinLen();
				PrefsControl.numericUpDownDescriptionMaxLen.Value = MacroscopePreferencesManager.GetDescriptionMaxLen();
				PrefsControl.numericUpDownDescriptionMinWords.Value = MacroscopePreferencesManager.GetDescriptionMinWords();
				PrefsControl.numericUpDownDescriptionMaxWords.Value = MacroscopePreferencesManager.GetDescriptionMaxWords();

			}

			DialogResult fPreferencsResult = fPreferencesForm.ShowDialog();
			
			DebugMsg( string.Format( "CallbackEditPreferencesClick: {0}", fPreferencsResult ) );

			if( fPreferencsResult == DialogResult.OK ) {

				// WebProxy Options
				MacroscopePreferencesManager.SetHttpProxyHost( PrefsControl.textBoxHttpProxyHost.Text );
				MacroscopePreferencesManager.SetHttpProxyPort( ( int )PrefsControl.numericUpDownHttpProxyPort.Value );

				// Spidering Control
				MacroscopePreferencesManager.SetMaxThreads( ( int )PrefsControl.numericUpDownMaxThreads.Value );
				MacroscopePreferencesManager.SetDepth( ( int )PrefsControl.numericUpDownDepth.Value );
				MacroscopePreferencesManager.SetPageLimit( ( int )PrefsControl.numericUpDownPageLimit.Value );
				MacroscopePreferencesManager.SetSameSite( PrefsControl.checkBoxSameSite.Checked );
				MacroscopePreferencesManager.SetFollowRobotsProtocol( PrefsControl.checkBoxFollowRobotsProtocol.Checked );
				MacroscopePreferencesManager.SetFollowRedirects( PrefsControl.checkBoxFollowRedirects.Checked );
				MacroscopePreferencesManager.SetFollowNoFollow( PrefsControl.checkBoxFollowNoFollow.Checked );
				MacroscopePreferencesManager.SetFollowCanonicalLinks( PrefsControl.checkBoxFollowCanonicalLinks.Checked );
				MacroscopePreferencesManager.SetFollowHrefLangLinks( PrefsControl.checkBoxFollowHrefLangLinks.Checked );
				MacroscopePreferencesManager.SetFetchStylesheets( PrefsControl.checkBoxFetchStylesheets.Checked );
				MacroscopePreferencesManager.SetFetchImages( PrefsControl.checkBoxFetchJavascripts.Checked );
				MacroscopePreferencesManager.SetFetchImages( PrefsControl.checkBoxFetchImages.Checked );
				MacroscopePreferencesManager.SetFetchPdfs( PrefsControl.checkBoxFetchPdfs.Checked );
				MacroscopePreferencesManager.SetFetchBinaries( PrefsControl.checkBoxFetchBinaries.Checked );

				// Analysis Options
				MacroscopePreferencesManager.SetProbeHreflangs( PrefsControl.checkBoxProbeHreflangs.Checked );
			
				// SEO Options
				MacroscopePreferencesManager.SetTitleMinLen( ( int )PrefsControl.numericUpDownTitleMinLen.Value );
				MacroscopePreferencesManager.SetTitleMaxLen( ( int )PrefsControl.numericUpDownTitleMaxLen.Value );
				MacroscopePreferencesManager.SetTitleMinWords( ( int )PrefsControl.numericUpDownTitleMinWords.Value );
				MacroscopePreferencesManager.SetTitleMaxWords( ( int )PrefsControl.numericUpDownTitleMaxWords.Value );
				MacroscopePreferencesManager.SetDescriptionMinLen( ( int )PrefsControl.numericUpDownDescriptionMinLen.Value );
				MacroscopePreferencesManager.SetDescriptionMaxLen( ( int )PrefsControl.numericUpDownDescriptionMaxLen.Value );
				MacroscopePreferencesManager.SetDescriptionMinWords( ( int )PrefsControl.numericUpDownDescriptionMinWords.Value );
				MacroscopePreferencesManager.SetDescriptionMaxWords( ( int )PrefsControl.numericUpDownDescriptionMaxWords.Value );

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

			string sStartUrl = this.GetURL();
			StartUrlDirty = true;

			if( MacroscopeURLTools.ValidateUrl( sStartUrl ) ) {
				MacroscopePreferencesManager.SetStartUrl( sStartUrl );
			}

		}

		/**************************************************************************/
				
		void CallbackScanStart ( object sender, EventArgs e )
		{

			string sStartUrl = this.GetURL();
						
			if( MacroscopeURLTools.ValidateUrl( sStartUrl ) ) {

				this.ScanningControlsStart( true );

				if( StartUrlDirty ) {
					this.msJobMaster.ShutdownWorkerUpdateDisplay();
					this.msJobMaster = new MacroscopeJobMaster ( this );
					this.ClearDisplay();
					StartUrlDirty = false;
				}

				MacroscopePreferencesManager.SetStartUrl( sStartUrl );

				MacroscopePreferencesManager.SavePreferences();

				this.ThreadScanner = new Thread ( new ThreadStart ( this.ScanningThread ) );
				this.ThreadScanner.Start();

			} else {
				
				DialogueBoxStartUrlInvalid();
				
			}

		}

		/**************************************************************************/
		
		void CallbackScanStop ( object sender, EventArgs e )
		{

			this.ScanningControlsStopping( true );
			
			this.msJobMaster.StopWorkers();

			while( this.msJobMaster.CountRunningThreads() > 0 ) {
				DebugMsg( "CallbackScanStop: WAITING" );
				Thread.Sleep( 100 );
			}
			
			this.ScanningControlsStopped( true );
	
		}

		/**************************************************************************/

		void CallbackScanReset ( object sender, EventArgs e )
		{

			if( this.msJobMaster.WorkersStopped() ) {

				this.ScanningControlsReset( true );

				this.msJobMaster.ShutdownWorkerUpdateDisplay();

				this.msJobMaster = new MacroscopeJobMaster ( this );

				this.ClearDisplay();
				
			}
			
		}
		
		/**************************************************************************/
		
		public void CallbackScanComplete ()
		{
			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate
						{
							this.UpdateStatusBar();
							this.ScanningControlsComplete( true );
						}
					)
				);
			} else {
				this.UpdateStatusBar();
				this.ScanningControlsComplete( true );
			}
		}

		/** TAB PAGES *************************************************************/

		void StartTabPageTimer ()
		{
			this.TimerTabPages = new System.Timers.Timer ( 5000 );
			this.TimerTabPages.Elapsed += this.CallbackTabPageTimer;
			this.TimerTabPages.AutoReset = true;
			this.TimerTabPages.Enabled = true;
			this.TimerTabPages.Start();
		}

		void StopTabPageTimer ()
		{
			this.TimerTabPages.Stop();
			this.TimerTabPages.Dispose();
		}
		
		void CallbackTabPageTimer ( Object self, ElapsedEventArgs e )
		{

			TabControl tcDisplay = this.tabControlMain;

			DebugMsg( string.Format( "CallbackTabPageTimer: {0}", "CALLED" ) );

			this.UpdateStatusBar();
			
			if( this.msJobMaster.CountRunningThreads() > 1 ) {

				this.Invoke(
					new MethodInvoker (
						delegate
						{
						
							DebugMsg( string.Format( "CallbackTabPageTimer tcDisplay: {0}", tcDisplay.TabPages[ tcDisplay.SelectedIndex ].Name ) );
					
							// TODO: Finish this structure

							switch( tcDisplay.TabPages[ tcDisplay.SelectedIndex ].Name ) {

								case "tabPageStructureOverview":
									//this.msJobMaster.WorkerUpdateDisplay();
									this.msDisplayStructure.RefreshData( this.msJobMaster.GetDocCollection() );
									break;

								case "tabPageHierarchy":
									break;

								case "tabPageCanonicalAnalysis":
									this.msDisplayCanonical.RefreshData( this.msJobMaster.GetDocCollection() );
									break;
									
								case "tabPageHrefLangAnalysis":
									this.msDisplayHrefLang.RefreshData( this.msJobMaster.GetDocCollection(), msJobMaster.GetLocales() );
									break;
									
								case "tabPageRedirectsAudit":
									break;
									
								case "tabPageUriAnalysis":
									break;
									
								case "tabPagePageTitles":
									this.msDisplayTitles.RefreshData( this.msJobMaster.GetDocCollection() );
									break;
									
								case "tabPagePageDescription":
									break;
									
								case "tabPagePageKeywords":
									break;
									
								case "tabPagePageHeadings":
									break;
									
								case "tabPageEmailAddresses":
									this.msDisplayEmailAddresses.RefreshData( this.msJobMaster.GetDocCollection() );
									break;
									
								case "tabPageTelephoneNumbers":
									this.msDisplayTelephoneNumbers.RefreshData( this.msJobMaster.GetDocCollection() );
									break;

								case "tabPageQueue":
									this.msDisplayQueue.RefreshData( this.msJobMaster.GetQueue() );
									break;
								
								case "tabPageHistory":
									this.msDisplayHistory.RefreshData( this.msJobMaster.GetHistory() );
									break;
								
								default:
									break;
								
							}

						}
					)
				);

			} else {
				DebugMsg( string.Format( "CallbackTabPageTimer: {0}", "STOPPED" ) );
			}

		}

		void CallbackTabControlDisplaySelectedIndexChanged ( object sender, EventArgs e )
		{
			TabControl tcDisplay = this.tabControlMain;
			DebugMsg( string.Format( "CallbackTabControlDisplaySelectedIndexChanged: {0}", tcDisplay.TabPages[ tcDisplay.SelectedIndex ] ) );
		}

		void CallbackListViewStructureOverviewClick ( object sender, EventArgs e )
		{
			ListView lvListView = ( ListView )sender;
			lock( lvListView ) {
				foreach( ListViewItem lvItem in lvListView.SelectedItems ) {
					string sURL = lvItem.SubItems[ 0 ].Text.ToString();
					this.macroscopeDocumentDetailsMain.UpdateDisplay( this.msJobMaster, sURL );
				}
			}
		}

		/** Scanning Controls *****************************************************/

		void ScanningControlsEnable ( Boolean bState )
		{
			this.reportsToolStripMenuItem.Enabled = true;
			this.textBoxStartUrl.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;
		}

		void ScanningControlsStart ( Boolean bState )
		{
			this.reportsToolStripMenuItem.Enabled = false;
			this.textBoxStartUrl.Enabled = false;
			this.ButtonStart.Enabled = false;
			this.ButtonStop.Enabled = true;
			this.ButtonReset.Enabled = false;
		}

		void ScanningControlsStopping ( Boolean bState )
		{
			this.reportsToolStripMenuItem.Enabled = false;
			this.textBoxStartUrl.Enabled = false;
			this.ButtonStart.Enabled = false;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;
		}

		void ScanningControlsStopped ( Boolean bState )
		{
			this.reportsToolStripMenuItem.Enabled = true;
			this.textBoxStartUrl.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = true;
		}

		void ScanningControlsReset ( Boolean bState )
		{
			this.reportsToolStripMenuItem.Enabled = true;
			this.textBoxStartUrl.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;
		}
		
		void ScanningControlsComplete ( Boolean bState )
		{
			this.reportsToolStripMenuItem.Enabled = true;
			this.textBoxStartUrl.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = true;
		}

		/**************************************************************************/

		void ScanningThread ()
		{
			DebugMsg( "Scanning Thread: Started." );
			this.msJobMaster.SetStartUrl( this.GetURL() );
			this.msJobMaster.Execute();
			DebugMsg( "Scanning Thread: Done." );
		}

		/**************************************************************************/

		public void ClearDisplay ()
		{

			this.msDisplayStructure.ClearData();
			
			this.msDisplayCanonical.ClearData();
			
			this.msDisplayHrefLang.ClearData();
			
			this.msDisplayEmailAddresses.ClearData();
			
			this.msDisplayTelephoneNumbers.ClearData();
			
			this.msDisplayHistory.ClearData();

			this.macroscopeDocumentDetailsMain.ClearData();

		}

		/**************************************************************************/

		public void UpdateDisplaySingle ( string sURL )
		{

			if( this.msJobMaster != null ) {
				
				this.msDisplayStructure.RefreshDataSingle( this.msJobMaster.GetDocCollection().Get( sURL ), sURL );

				this.msDisplayCanonical.RefreshDataSingle( this.msJobMaster.GetDocCollection().Get( sURL ), sURL );
						
				this.msDisplayEmailAddresses.RefreshDataSingle( this.msJobMaster.GetDocCollection().Get( sURL ), sURL );
						
				this.msDisplayTelephoneNumbers.RefreshDataSingle( this.msJobMaster.GetDocCollection().Get( sURL ), sURL );

			}
			
		}

		/**************************************************************************/

		public void UpdateDisplayHrefLang ()
		{

			DebugMsg( string.Format( "UpdateDisplayHrefLang: {0}", "CALLED" ) );

			if( this.msJobMaster != null ) {
			
				this.msDisplayHrefLang.RefreshData( this.msJobMaster.GetDocCollection(), this.msJobMaster.GetLocales() );

				if( this.InvokeRequired ) {
					this.Invoke(
						new MethodInvoker (
							delegate
							{
								this.Refresh();
								this.Update();
							}
						)
					);
				} else {
					this.Refresh();
					this.Update();
				}

			}
			
		}

		/**************************************************************************/

		public void UpdateStatusBar ()
		{
			if( this.msJobMaster != null ) {

				string sThreads = string.Format( "Threads: {0}", this.msJobMaster.CountRunningThreads() );
				string sUrlCount = string.Format( "URLs in Queue: {0}", this.msJobMaster.CountUrlQueue() );
				string sUrlsFound = string.Format( "URLs Found: {0}", this.msJobMaster.GetDocCollection().Count() );

				if( this.InvokeRequired ) {
					this.Invoke(
						new MethodInvoker (
							delegate
							{
								this.toolStripThreads.Text = sThreads;
								this.toolStripUrlCount.Text = sUrlCount;
								this.toolStripFound.Text = sUrlsFound;
							}
						)
					);
				} else {
					this.toolStripThreads.Text = sThreads;
					this.toolStripUrlCount.Text = sUrlCount;
					this.toolStripFound.Text = sUrlsFound;
				}
			
			}
			
		}

		/**************************************************************************/

		void CallbackRetryBrokenLinksClick ( object sender, EventArgs e )
		{
			DebugMsg( string.Format( "CallbackRetryBrokenLinksClick: {0}", "CALLED" ) );
		}

		/**************************************************************************/

		void CopyTextToClipboard ( string sText )
		{
			Clipboard.SetText( sText );
		}

		/**************************************************************************/
		
		void CallbackSaveOverviewExcelReport ( object sender, EventArgs e )
		{
			SaveFileDialog Dialog = new SaveFileDialog ();
			Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
			Dialog.FilterIndex = 2;
			Dialog.RestoreDirectory = true;
			Dialog.DefaultExt = "xlsx";
			Dialog.AddExtension = true;
			if( Dialog.ShowDialog() == DialogResult.OK ) {
				string sPath = Dialog.FileName;
				MacroscopeExcelOverviewReport msExcelReport = new MacroscopeExcelOverviewReport ();
				msExcelReport.WriteXslx( this.msJobMaster, sPath );
			}
			Dialog.Dispose();
		}
		
		/**************************************************************************/
				
		void CallbackSaveHrefLangExcelReport ( object sender, EventArgs e )
		{
			SaveFileDialog Dialog = new SaveFileDialog ();
			Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
			Dialog.FilterIndex = 2;
			Dialog.RestoreDirectory = true;
			Dialog.DefaultExt = "xlsx";
			Dialog.AddExtension = true;
			if( Dialog.ShowDialog() == DialogResult.OK ) {
				string sPath = Dialog.FileName;
				MacroscopeExcelHrefLangReport msExcelReport = new MacroscopeExcelHrefLangReport ();
				msExcelReport.WriteXslx( this.msJobMaster, sPath );
			}
			Dialog.Dispose();
		}

		/**************************************************************************/
	
		static void DebugMsg ( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		/**************************************************************************/
			
	}
	
}
