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
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopePrefsForm.
  /// </summary>

  public partial class MacroscopePrefsForm : Form
  {

    /**************************************************************************/

    public MacroscopePrefsForm ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.Shown += this.CallbackPrefsFormShown;
		            
    }

    /**************************************************************************/

    private void CallbackPrefsFormShown ( object sender, EventArgs e )
    {

      MacroscopePrefsControl PrefsControl = this.macroscopePrefsControlInstance;
		
      this.buttonPrefsDefault.Click += this.SetPrefsFormControlFieldToDefaults;
      
      this.SetPrefsFormControlFields( PrefsControl: PrefsControl );

    }

    /**************************************************************************/

    private void SetPrefsFormControlFieldToDefaults ( object sender, EventArgs e )
    {

      Button DefaultsButton = ( Button )sender;
      MacroscopePrefsControl PrefsControl = this.macroscopePrefsControlInstance;

      MacroscopePreferencesManager.SetDefaultValues();

      this.SetPrefsFormControlFields( PrefsControl );

    }

    /**************************************************************************/
        
    private void SetPrefsFormControlFields ( MacroscopePrefsControl PrefsControl )
    {

      { // Configure Display Options
        PrefsControl.checkBoxPauseDisplayDuringScan.Checked = MacroscopePreferencesManager.GetPauseDisplayDuringScan();
        PrefsControl.checkBoxShowProgressDialogues.Checked = MacroscopePreferencesManager.GetShowProgressDialogues();
      }

      { //Configure Form Fields

        // Spidering Control

        PrefsControl.numericUpDownDepth.Minimum = -1;
        PrefsControl.numericUpDownDepth.Maximum = 10000;

        PrefsControl.numericUpDownPageLimit.Minimum = -1;
        PrefsControl.numericUpDownPageLimit.Maximum = 10000;

        PrefsControl.numericUpDownCrawlDelay.Minimum = 0;
        PrefsControl.numericUpDownCrawlDelay.Maximum = 60;

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
        PrefsControl.numericUpDownCrawlDelay.Value = MacroscopePreferencesManager.GetCrawlDelay();
        PrefsControl.numericUpDownRequestTimeout.Value = ( Decimal )MacroscopePreferencesManager.GetRequestTimeout();
        PrefsControl.numericUpDownMaxRetries.Value = ( Decimal )MacroscopePreferencesManager.GetMaxRetries();

        PrefsControl.checkBoxCheckExternalLinks.Checked = MacroscopePreferencesManager.GetCheckExternalLinks();
        PrefsControl.checkBoxFollowRobotsProtocol.Checked = MacroscopePreferencesManager.GetFollowRobotsProtocol();
        PrefsControl.checkBoxFollowSitemapLinks.Checked = MacroscopePreferencesManager.GetFollowSitemapLinks();
        PrefsControl.checkBoxFollowRedirects.Checked = MacroscopePreferencesManager.GetFollowRedirects();
        PrefsControl.checkBoxFollowNoFollow.Checked = MacroscopePreferencesManager.GetFollowNoFollow();
        PrefsControl.checkBoxIgnoreQueries.Checked = MacroscopePreferencesManager.GetIgnoreQueries();
        PrefsControl.checkBoxFollowCanonicalLinks.Checked = MacroscopePreferencesManager.GetFollowCanonicalLinks();
        PrefsControl.checkBoxFollowHrefLangLinks.Checked = MacroscopePreferencesManager.GetFollowHrefLangLinks();

        PrefsControl.checkBoxFetchStylesheets.Checked = MacroscopePreferencesManager.GetFetchStylesheets();
        PrefsControl.checkBoxFetchJavascripts.Checked = MacroscopePreferencesManager.GetFetchJavascripts();
        PrefsControl.checkBoxFetchImages.Checked = MacroscopePreferencesManager.GetFetchImages();
        PrefsControl.checkBoxFetchAudio.Checked = MacroscopePreferencesManager.GetFetchAudio();
        PrefsControl.checkBoxFetchVideo.Checked = MacroscopePreferencesManager.GetFetchVideo();
        PrefsControl.checkBoxFetchXml.Checked = MacroscopePreferencesManager.GetFetchXml();
        PrefsControl.checkBoxFetchBinaries.Checked = MacroscopePreferencesManager.GetFetchBinaries();

        PrefsControl.checkBoxProcessStylesheets.Checked = MacroscopePreferencesManager.GetProcessStylesheets();
        PrefsControl.checkBoxProcessJavascripts.Checked = MacroscopePreferencesManager.GetProcessJavascripts();
        PrefsControl.checkBoxProcessImages.Checked = MacroscopePreferencesManager.GetProcessImages();
        PrefsControl.checkBoxProcessPdfs.Checked = MacroscopePreferencesManager.GetProcessPdfs();
        PrefsControl.checkBoxProcessAudio.Checked = MacroscopePreferencesManager.GetProcessAudio();
        PrefsControl.checkBoxProcessVideo.Checked = MacroscopePreferencesManager.GetProcessVideo();
        PrefsControl.checkBoxProcessXml.Checked = MacroscopePreferencesManager.GetProcessXml();
        PrefsControl.checkBoxProcessBinaries.Checked = MacroscopePreferencesManager.GetProcessBinaries();

        // Analysis Options

        PrefsControl.checkBoxCheckHreflangs.Checked = MacroscopePreferencesManager.GetCheckHreflangs();
        PrefsControl.checkBoxDetectLanguage.Checked = MacroscopePreferencesManager.GetDetectLanguage();
             
        PrefsControl.checkBoxScanSitesInList.Checked = MacroscopePreferencesManager.GetScanSitesInList();
        PrefsControl.checkBoxWarnAboutInsecureLinks.Checked = MacroscopePreferencesManager.GetWarnAboutInsecureLinks();

        PrefsControl.checkBoxEnableLevenshteinDeduplication.Checked = MacroscopePreferencesManager.GetEnableLevenshteinDeduplication();
        PrefsControl.numericUpDownMaxLevenshteinSizeDifference.Value = MacroscopePreferencesManager.GetMaxLevenshteinSizeDifference();
        PrefsControl.numericUpDownMaxLevenshteinDistance.Value = MacroscopePreferencesManager.GetMaxLevenshteinDistance();

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

        // Export Options
        
        PrefsControl.checkBoxSitemapIncludeLinkedPdfs.Checked = MacroscopePreferencesManager.GetSitemapIncludeLinkedPdfs();

      }

    }

    /**************************************************************************/
        
    public void SavePrefsFormControlFields ()
    {

      MacroscopePrefsControl PrefsControl = this.macroscopePrefsControlInstance;

      // Configure Display Options
      
      MacroscopePreferencesManager.SetPauseDisplayDuringScan( PrefsControl.checkBoxPauseDisplayDuringScan.Checked );
      MacroscopePreferencesManager.SetShowProgressDialogues( PrefsControl.checkBoxShowProgressDialogues.Checked );

      // WebProxy Options

      MacroscopePreferencesManager.SetHttpProxyHost( PrefsControl.textBoxHttpProxyHost.Text );
      MacroscopePreferencesManager.SetHttpProxyPort( ( int )PrefsControl.numericUpDownHttpProxyPort.Value );

      // Spidering Control

      MacroscopePreferencesManager.SetMaxThreads( ( int )PrefsControl.numericUpDownMaxThreads.Value );
      MacroscopePreferencesManager.SetDepth( ( int )PrefsControl.numericUpDownDepth.Value );
      MacroscopePreferencesManager.SetPageLimit( ( int )PrefsControl.numericUpDownPageLimit.Value );
      MacroscopePreferencesManager.SetCrawlDelay( ( int )PrefsControl.numericUpDownCrawlDelay.Value );
      MacroscopePreferencesManager.SetRequestTimeout( ( int )PrefsControl.numericUpDownRequestTimeout.Value );
      MacroscopePreferencesManager.SetMaxRetries( ( int )PrefsControl.numericUpDownMaxRetries.Value );

      MacroscopePreferencesManager.SetCheckExternalLinks( PrefsControl.checkBoxCheckExternalLinks.Checked );
      MacroscopePreferencesManager.SetFollowRobotsProtocol( PrefsControl.checkBoxFollowRobotsProtocol.Checked );
      MacroscopePreferencesManager.SetFollowSitemapLinks( PrefsControl.checkBoxFollowSitemapLinks.Checked );
      MacroscopePreferencesManager.SetFollowRedirects( PrefsControl.checkBoxFollowRedirects.Checked );
      MacroscopePreferencesManager.SetFollowNoFollow( PrefsControl.checkBoxFollowNoFollow.Checked );
      MacroscopePreferencesManager.SetIgnoreQueries( PrefsControl.checkBoxIgnoreQueries.Checked );
      MacroscopePreferencesManager.SetFollowCanonicalLinks( PrefsControl.checkBoxFollowCanonicalLinks.Checked );
      MacroscopePreferencesManager.SetFollowHrefLangLinks( PrefsControl.checkBoxFollowHrefLangLinks.Checked );

      MacroscopePreferencesManager.SetFetchStylesheets( PrefsControl.checkBoxFetchStylesheets.Checked );
      MacroscopePreferencesManager.SetFetchJavascripts( PrefsControl.checkBoxFetchJavascripts.Checked );
      MacroscopePreferencesManager.SetFetchImages( PrefsControl.checkBoxFetchImages.Checked );
      MacroscopePreferencesManager.SetFetchAudio( PrefsControl.checkBoxFetchAudio.Checked );
      MacroscopePreferencesManager.SetFetchVideo( PrefsControl.checkBoxFetchVideo.Checked );
      MacroscopePreferencesManager.SetFetchXml( PrefsControl.checkBoxFetchXml.Checked );
      MacroscopePreferencesManager.SetFetchBinaries( PrefsControl.checkBoxFetchBinaries.Checked );

      MacroscopePreferencesManager.SetProcessStylesheets( PrefsControl.checkBoxProcessStylesheets.Checked );
      MacroscopePreferencesManager.SetProcessJavascripts( PrefsControl.checkBoxProcessJavascripts.Checked );
      MacroscopePreferencesManager.SetProcessImages( PrefsControl.checkBoxProcessImages.Checked );
      MacroscopePreferencesManager.SetProcessPdfs( PrefsControl.checkBoxProcessPdfs.Checked );
      MacroscopePreferencesManager.SetProcessAudio( PrefsControl.checkBoxProcessAudio.Checked );
      MacroscopePreferencesManager.SetProcessVideo( PrefsControl.checkBoxProcessVideo.Checked );
      MacroscopePreferencesManager.SetProcessXml( PrefsControl.checkBoxProcessXml.Checked );
      MacroscopePreferencesManager.SetProcessBinaries( PrefsControl.checkBoxProcessBinaries.Checked );

      // Analysis Options

      MacroscopePreferencesManager.SetCheckHreflangs( PrefsControl.checkBoxCheckHreflangs.Checked );
      MacroscopePreferencesManager.SetDetectLanguage( Detect: PrefsControl.checkBoxDetectLanguage.Checked );

      MacroscopePreferencesManager.SetScanSitesInList( PrefsControl.checkBoxScanSitesInList.Checked );
      MacroscopePreferencesManager.SetWarnAboutInsecureLinks( PrefsControl.checkBoxWarnAboutInsecureLinks.Checked );

      MacroscopePreferencesManager.SetEnableLevenshteinDeduplication( PrefsControl.checkBoxEnableLevenshteinDeduplication.Checked );
      MacroscopePreferencesManager.SetMaxLevenshteinSizeDifference( ( int )PrefsControl.numericUpDownMaxLevenshteinSizeDifference.Value );
      MacroscopePreferencesManager.SetMaxLevenshteinDistance( ( int )PrefsControl.numericUpDownMaxLevenshteinDistance.Value );

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

      // Export Options
        
      MacroscopePreferencesManager.SetSitemapIncludeLinkedPdfs( PrefsControl.checkBoxSitemapIncludeLinkedPdfs.Checked );

      // Tidy Up

      MacroscopePreferencesManager.SavePreferences();
      MacroscopePreferencesManager.ConfigureHttpProxy();

    }

    /**************************************************************************/

    private static void DebugMsg ( String sMsg )
    {
      System.Diagnostics.Debug.WriteLine( sMsg );
    }

    /**************************************************************************/

  }

}
