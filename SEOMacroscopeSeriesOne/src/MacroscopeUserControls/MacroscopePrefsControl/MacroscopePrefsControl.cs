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
  /// Description of MacroscopePrefsControl.
  /// </summary>

  public partial class MacroscopePrefsControl : UserControl
  {

    /**************************************************************************/

    public MacroscopePrefsControl ()
    {
      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.
    }

    /**************************************************************************/
        
    public void SetPrefsFormControlFields ()
    {

      { // Configure Display Options
        this.checkBoxPauseDisplayDuringScan.Checked = MacroscopePreferencesManager.GetPauseDisplayDuringScan();
        this.checkBoxShowProgressDialogues.Checked = MacroscopePreferencesManager.GetShowProgressDialogues();
      }

      { //Configure Form Fields

        /** Spidering Control ---------------------------------------------- **/

        this.numericUpDownDepth.Minimum = -1;
        this.numericUpDownDepth.Maximum = 10000;

        this.numericUpDownPageLimit.Minimum = -1;
        this.numericUpDownPageLimit.Maximum = 10000;

        this.numericUpDownCrawlDelay.Minimum = 0;
        this.numericUpDownCrawlDelay.Maximum = 60;

        this.numericUpDownMaxRetries.Minimum = 0;
        this.numericUpDownMaxRetries.Maximum = 10;

      }

      {

        /** WebProxy Options ----------------------------------------------- **/

        this.textBoxHttpProxyHost.Text = MacroscopePreferencesManager.GetHttpProxyHost();
        this.numericUpDownHttpProxyPort.Value = MacroscopePreferencesManager.GetHttpProxyPort();

        /** Server Certificate Options --------------------------------------- **/

        this.checkBoxServerCertificateValidation.Checked = MacroscopePreferencesManager.GetServerCertificateValidation();
        
        /** Spidering Control ---------------------------------------------- **/

        this.numericUpDownMaxThreads.Value = MacroscopePreferencesManager.GetMaxThreads();
        this.numericUpDownDepth.Value = MacroscopePreferencesManager.GetDepth();
        this.numericUpDownPageLimit.Value = MacroscopePreferencesManager.GetPageLimit();
        this.numericUpDownCrawlDelay.Value = MacroscopePreferencesManager.GetCrawlDelay();
        this.numericUpDownRequestTimeout.Value = ( Decimal )MacroscopePreferencesManager.GetRequestTimeout();
        this.numericUpDownMaxRetries.Value = ( Decimal )MacroscopePreferencesManager.GetMaxRetries();

        this.checkBoxCrawlStrictUrlCheck.Checked = MacroscopePreferencesManager.GetCrawlStrictUrlCheck();

        this.checkBoxCheckExternalLinks.Checked = MacroscopePreferencesManager.GetCheckExternalLinks();
        this.checkBoxFetchExternalLinks.Checked = MacroscopePreferencesManager.GetFetchExternalLinks();
        
        this.checkBoxFollowRobotsProtocol.Checked = MacroscopePreferencesManager.GetFollowRobotsProtocol();
        this.checkBoxFollowSitemapLinks.Checked = MacroscopePreferencesManager.GetFollowSitemapLinks();
        
        this.checkBoxCheckRedirects.Checked = MacroscopePreferencesManager.GetCheckRedirects();
        this.checkBoxFollowRedirects.Checked = MacroscopePreferencesManager.GetFollowRedirects();
        
        this.checkBoxFollowNoFollow.Checked = MacroscopePreferencesManager.GetFollowNoFollow();
        this.checkBoxIgnoreQueries.Checked = MacroscopePreferencesManager.GetIgnoreQueries();
        this.checkBoxIgnoreHashFragments.Checked = MacroscopePreferencesManager.GetIgnoreHashFragments();
        this.checkBoxFollowCanonicalLinks.Checked = MacroscopePreferencesManager.GetFollowCanonicalLinks();
        this.checkBoxFollowAlternateLinks.Checked = MacroscopePreferencesManager.GetFollowAlternateLinks();
        this.checkBoxFollowHrefLangLinks.Checked = MacroscopePreferencesManager.GetFollowHrefLangLinks();

        this.checkBoxFetchStylesheets.Checked = MacroscopePreferencesManager.GetFetchStylesheets();
        this.checkBoxFetchJavascripts.Checked = MacroscopePreferencesManager.GetFetchJavascripts();
        this.checkBoxFetchImages.Checked = MacroscopePreferencesManager.GetFetchImages();
        this.checkBoxFetchAudio.Checked = MacroscopePreferencesManager.GetFetchAudio();
        this.checkBoxFetchVideo.Checked = MacroscopePreferencesManager.GetFetchVideo();
        this.checkBoxFetchXml.Checked = MacroscopePreferencesManager.GetFetchXml();
        this.checkBoxFetchBinaries.Checked = MacroscopePreferencesManager.GetFetchBinaries();

        /** Analysis Options ----------------------------------------------- **/
        
        this.checkBoxResolveAddresses.Checked = MacroscopePreferencesManager.GetResolveAddresses();

        this.checkBoxCheckHreflangs.Checked = MacroscopePreferencesManager.GetCheckHreflangs();
        this.checkBoxDetectLanguage.Checked = MacroscopePreferencesManager.GetDetectLanguage();

        this.checkBoxProcessStylesheets.Checked = MacroscopePreferencesManager.GetProcessStylesheets();
        this.checkBoxProcessJavascripts.Checked = MacroscopePreferencesManager.GetProcessJavascripts();
        this.checkBoxProcessImages.Checked = MacroscopePreferencesManager.GetProcessImages();
        this.checkBoxProcessPdfs.Checked = MacroscopePreferencesManager.GetProcessPdfs();
        this.checkBoxProcessAudio.Checked = MacroscopePreferencesManager.GetProcessAudio();
        this.checkBoxProcessVideo.Checked = MacroscopePreferencesManager.GetProcessVideo();
        this.checkBoxProcessXml.Checked = MacroscopePreferencesManager.GetProcessXml();
        this.checkBoxProcessBinaries.Checked = MacroscopePreferencesManager.GetProcessBinaries();

        this.checkBoxScanSitesInList.Checked = MacroscopePreferencesManager.GetScanSitesInList();
        this.checkBoxWarnAboutInsecureLinks.Checked = MacroscopePreferencesManager.GetWarnAboutInsecureLinks();

        this.checkBoxEnableTextIndexing.Checked = MacroscopePreferencesManager.GetEnableTextIndexing();
        this.checkBoxCaseSensitiveTextIndexing.Checked = MacroscopePreferencesManager.GetCaseSensitiveTextIndexing();

        /** SEO Options ---------------------------------------------------- **/

        this.numericUpDownTitleMinLen.Value = MacroscopePreferencesManager.GetTitleMinLen();
        this.numericUpDownTitleMaxLen.Value = MacroscopePreferencesManager.GetTitleMaxLen();
        this.numericUpDownTitleMinWords.Value = MacroscopePreferencesManager.GetTitleMinWords();
        this.numericUpDownTitleMaxWords.Value = MacroscopePreferencesManager.GetTitleMaxWords();
        this.numericUpDownTitleMaxPixelWidth.Value = MacroscopePreferencesManager.GetTitleMaxPixelWidth();

        this.numericUpDownDescriptionMinLen.Value = MacroscopePreferencesManager.GetDescriptionMinLen();
        this.numericUpDownDescriptionMaxLen.Value = MacroscopePreferencesManager.GetDescriptionMaxLen();
        this.numericUpDownDescriptionMinWords.Value = MacroscopePreferencesManager.GetDescriptionMinWords();
        this.numericUpDownDescriptionMaxWords.Value = MacroscopePreferencesManager.GetDescriptionMaxWords();

        this.numericUpDownMaxHeadingDepth.Value = MacroscopePreferencesManager.GetMaxHeadingDepth();

        this.checkBoxAnalyzeKeywordsInText.Checked = MacroscopePreferencesManager.GetAnalyzeKeywordsInText();
        this.checkBoxAnalyzeTextReadability.Checked = MacroscopePreferencesManager.GetAnalyzeTextReadability();
        this.comboBoxAnalyzeTextReadabilityEnglishAlgorithm.SelectedIndex = ( int )MacroscopePreferencesManager.GetAnalyzeTextReadabilityEnglishAlgorithm();

        this.checkBoxEnableLevenshteinDeduplication.Checked = MacroscopePreferencesManager.GetEnableLevenshteinDeduplication();
        this.numericUpDownMaxLevenshteinSizeDifference.Value = MacroscopePreferencesManager.GetMaxLevenshteinSizeDifference();
        this.numericUpDownMaxLevenshteinDistance.Value = MacroscopePreferencesManager.GetMaxLevenshteinDistance();
        
        this.checkBoxAnalyzeClickPaths.Checked = MacroscopePreferencesManager.GetAnalyzeClickPaths();
              
#if DEBUG
        this.groupBoxPageNavigationAnalysis.Visible = true;
#else
        this.groupBoxPageNavigationAnalysis.Visible = false;
#endif
        
        /** Custom Filter Options ------------------------------------------ **/

        this.checkBoxCustomFiltersEnable.Checked = MacroscopePreferencesManager.GetCustomFiltersEnable();
        this.numericUpDownCustomFiltersMaxItems.Value = MacroscopePreferencesManager.GetCustomFiltersMaxItems();

        this.checkBoxCustomFiltersApplyToHtml.Checked = MacroscopePreferencesManager.GetCustomFiltersApplyToHtml();
        this.checkBoxCustomFiltersApplyToCss.Checked = MacroscopePreferencesManager.GetCustomFiltersApplyToCss();
        this.checkBoxCustomFiltersApplyToJavascripts.Checked = MacroscopePreferencesManager.GetCustomFiltersApplyToJavascripts();
        this.checkBoxCustomFiltersApplyToText.Checked = MacroscopePreferencesManager.GetCustomFiltersApplyToText();
        this.checkBoxCustomFiltersApplyToXml.Checked = MacroscopePreferencesManager.GetCustomFiltersApplyToXml();

        /** Extractor Options ---------------------------------------------- **/

        this.checkBoxDataExtractorsEnable.Checked = MacroscopePreferencesManager.GetDataExtractorsEnable();
        this.checkBoxDataExtractorsCleanWhiteSpace.Checked = MacroscopePreferencesManager.GetDataExtractorsCleanWhiteSpace();

        this.numericUpDownDataExtractorsMaxItemsCssSelectors.Value = MacroscopePreferencesManager.GetDataExtractorsMaxItemsCssSelectors();
        this.numericUpDownDataExtractorsMaxItemsRegexes.Value = MacroscopePreferencesManager.GetDataExtractorsMaxItemsRegexes();
        this.numericUpDownDataExtractorsMaxItemsXpaths.Value = MacroscopePreferencesManager.GetDataExtractorsMaxItemsXpaths();

        this.checkBoxDataExtractorsApplyToHtml.Checked = MacroscopePreferencesManager.GetDataExtractorsApplyToHtml();
        this.checkBoxDataExtractorsApplyToCss.Checked = MacroscopePreferencesManager.GetDataExtractorsApplyToCss();
        this.checkBoxDataExtractorsApplyToJavascripts.Checked = MacroscopePreferencesManager.GetDataExtractorsApplyToJavascripts();
        this.checkBoxDataExtractorsApplyToText.Checked = MacroscopePreferencesManager.GetDataExtractorsApplyToText();
        this.checkBoxDataExtractorsApplyToXml.Checked = MacroscopePreferencesManager.GetDataExtractorsApplyToXml();

        /** Export Options ------------------------------------------------- **/
        
        this.checkBoxSitemapIncludeLinkedPdfs.Checked = MacroscopePreferencesManager.GetSitemapIncludeLinkedPdfs();

        /** Advanced Settings ---------------------------------------------- **/
        
        this.checkBoxEnableMemoryGuard.Checked = MacroscopePreferencesManager.GetEnableMemoryGuard();

      }

    }

    /**************************************************************************/
        
    public void SavePrefsFormControlFields ()
    {

      /** Configure Display Options ---------------------------------------- **/

      MacroscopePreferencesManager.SetPauseDisplayDuringScan( this.checkBoxPauseDisplayDuringScan.Checked );
      MacroscopePreferencesManager.SetShowProgressDialogues( this.checkBoxShowProgressDialogues.Checked );

      /** WebProxy Options ------------------------------------------------- **/

      MacroscopePreferencesManager.SetHttpProxyHost( this.textBoxHttpProxyHost.Text );
      MacroscopePreferencesManager.SetHttpProxyPort( ( int )this.numericUpDownHttpProxyPort.Value );
      
      /** Server Certificate Options --------------------------------------- **/

      MacroscopePreferencesManager.SetServerCertificateValidation( this.checkBoxServerCertificateValidation.Checked );

      /** Spidering Control ------------------------------------------------ **/

      MacroscopePreferencesManager.SetMaxThreads( ( int )this.numericUpDownMaxThreads.Value );
      MacroscopePreferencesManager.SetDepth( ( int )this.numericUpDownDepth.Value );
      MacroscopePreferencesManager.SetPageLimit( ( int )this.numericUpDownPageLimit.Value );
      MacroscopePreferencesManager.SetCrawlDelay( ( int )this.numericUpDownCrawlDelay.Value );
      MacroscopePreferencesManager.SetRequestTimeout( ( int )this.numericUpDownRequestTimeout.Value );
      MacroscopePreferencesManager.SetMaxRetries( ( int )this.numericUpDownMaxRetries.Value );

      MacroscopePreferencesManager.SetCrawlStrictUrlCheck( this.checkBoxCrawlStrictUrlCheck.Checked );
              
      MacroscopePreferencesManager.SetCheckExternalLinks( this.checkBoxCheckExternalLinks.Checked );
      MacroscopePreferencesManager.SetFetchExternalLinks( this.checkBoxFetchExternalLinks.Checked );

      MacroscopePreferencesManager.SetFollowRobotsProtocol( this.checkBoxFollowRobotsProtocol.Checked );
      MacroscopePreferencesManager.SetFollowSitemapLinks( this.checkBoxFollowSitemapLinks.Checked );

      MacroscopePreferencesManager.SetCheckRedirects( this.checkBoxCheckRedirects.Checked );
      MacroscopePreferencesManager.SetFollowRedirects( this.checkBoxFollowRedirects.Checked );

      MacroscopePreferencesManager.SetFollowNoFollow( this.checkBoxFollowNoFollow.Checked );
      MacroscopePreferencesManager.SetIgnoreQueries( this.checkBoxIgnoreQueries.Checked );
      MacroscopePreferencesManager.SetIgnoreHashFragments( this.checkBoxIgnoreHashFragments.Checked );

      MacroscopePreferencesManager.SetFollowCanonicalLinks( this.checkBoxFollowCanonicalLinks.Checked );
      MacroscopePreferencesManager.SetFollowAlternateLinks( this.checkBoxFollowAlternateLinks.Checked );
      
      MacroscopePreferencesManager.SetFollowHrefLangLinks( this.checkBoxFollowHrefLangLinks.Checked );

      MacroscopePreferencesManager.SetFetchStylesheets( this.checkBoxFetchStylesheets.Checked );
      MacroscopePreferencesManager.SetFetchJavascripts( this.checkBoxFetchJavascripts.Checked );
      MacroscopePreferencesManager.SetFetchImages( this.checkBoxFetchImages.Checked );
      MacroscopePreferencesManager.SetFetchAudio( this.checkBoxFetchAudio.Checked );
      MacroscopePreferencesManager.SetFetchVideo( this.checkBoxFetchVideo.Checked );
      MacroscopePreferencesManager.SetFetchXml( this.checkBoxFetchXml.Checked );
      MacroscopePreferencesManager.SetFetchBinaries( this.checkBoxFetchBinaries.Checked );

      /** Analysis Options ------------------------------------------------- **/

      MacroscopePreferencesManager.SetResolveAddresses( this.checkBoxResolveAddresses.Checked );

      MacroscopePreferencesManager.SetCheckHreflangs( this.checkBoxCheckHreflangs.Checked );
      MacroscopePreferencesManager.SetDetectLanguage( Enabled: this.checkBoxDetectLanguage.Checked );

      MacroscopePreferencesManager.SetProcessStylesheets( this.checkBoxProcessStylesheets.Checked );
      MacroscopePreferencesManager.SetProcessJavascripts( this.checkBoxProcessJavascripts.Checked );
      MacroscopePreferencesManager.SetProcessImages( this.checkBoxProcessImages.Checked );
      MacroscopePreferencesManager.SetProcessPdfs( this.checkBoxProcessPdfs.Checked );
      MacroscopePreferencesManager.SetProcessAudio( this.checkBoxProcessAudio.Checked );
      MacroscopePreferencesManager.SetProcessVideo( this.checkBoxProcessVideo.Checked );
      MacroscopePreferencesManager.SetProcessXml( this.checkBoxProcessXml.Checked );
      MacroscopePreferencesManager.SetProcessBinaries( this.checkBoxProcessBinaries.Checked );

      MacroscopePreferencesManager.SetScanSitesInList( this.checkBoxScanSitesInList.Checked );
      MacroscopePreferencesManager.SetWarnAboutInsecureLinks( this.checkBoxWarnAboutInsecureLinks.Checked );

      MacroscopePreferencesManager.SetEnableTextIndexing( this.checkBoxEnableTextIndexing.Checked );
      MacroscopePreferencesManager.SetCaseSensitiveTextIndexing( this.checkBoxCaseSensitiveTextIndexing.Checked );
        
      /** SEO Options ------------------------------------------------------ **/
      
      MacroscopePreferencesManager.SetTitleMinLen( ( int )this.numericUpDownTitleMinLen.Value );
      MacroscopePreferencesManager.SetTitleMaxLen( ( int )this.numericUpDownTitleMaxLen.Value );
      MacroscopePreferencesManager.SetTitleMinWords( ( int )this.numericUpDownTitleMinWords.Value );
      MacroscopePreferencesManager.SetTitleMaxWords( ( int )this.numericUpDownTitleMaxWords.Value );
      MacroscopePreferencesManager.SetTitleMaxPixelWidth( ( int )this.numericUpDownTitleMaxPixelWidth.Value );

      MacroscopePreferencesManager.SetDescriptionMinLen( ( int )this.numericUpDownDescriptionMinLen.Value );
      MacroscopePreferencesManager.SetDescriptionMaxLen( ( int )this.numericUpDownDescriptionMaxLen.Value );
      MacroscopePreferencesManager.SetDescriptionMinWords( ( int )this.numericUpDownDescriptionMinWords.Value );
      MacroscopePreferencesManager.SetDescriptionMaxWords( ( int )this.numericUpDownDescriptionMaxWords.Value );

      MacroscopePreferencesManager.SetMaxHeadingDepth( ( ushort )this.numericUpDownMaxHeadingDepth.Value );

      MacroscopePreferencesManager.SetAnalyzeKeywordsInText( this.checkBoxAnalyzeKeywordsInText.Checked );
      MacroscopePreferencesManager.SetAnalyzeTextReadability( this.checkBoxAnalyzeTextReadability.Checked );
      MacroscopePreferencesManager.SetAnalyzeTextReadabilityEnglishAlgorithm( ( MacroscopeAnalyzeReadability.AnalyzeReadabilityEnglishAlgorithm )this.comboBoxAnalyzeTextReadabilityEnglishAlgorithm.SelectedIndex );

      MacroscopePreferencesManager.SetEnableLevenshteinDeduplication( this.checkBoxEnableLevenshteinDeduplication.Checked );
      MacroscopePreferencesManager.SetMaxLevenshteinSizeDifference( ( int )this.numericUpDownMaxLevenshteinSizeDifference.Value );
      MacroscopePreferencesManager.SetMaxLevenshteinDistance( ( int )this.numericUpDownMaxLevenshteinDistance.Value );

      MacroscopePreferencesManager.SetAnalyzeClickPaths( this.checkBoxAnalyzeClickPaths.Checked );
      
      /** Custom Filter Options -------------------------------------------- **/

      MacroscopePreferencesManager.SetCustomFiltersEnable( this.checkBoxCustomFiltersEnable.Checked );

      MacroscopePreferencesManager.SetCustomFiltersMaxItems( ( int )this.numericUpDownCustomFiltersMaxItems.Value );

      MacroscopePreferencesManager.SetCustomFiltersApplyToHtml( this.checkBoxCustomFiltersApplyToHtml.Checked );
      MacroscopePreferencesManager.SetCustomFiltersApplyToCss( this.checkBoxCustomFiltersApplyToCss.Checked );
      MacroscopePreferencesManager.SetCustomFiltersApplyToJavascripts( this.checkBoxCustomFiltersApplyToJavascripts.Checked );
      MacroscopePreferencesManager.SetCustomFiltersApplyToText( this.checkBoxCustomFiltersApplyToText.Checked );
      MacroscopePreferencesManager.SetCustomFiltersApplyToXml( this.checkBoxCustomFiltersApplyToXml.Checked );
      
      
      /** Extractor Options ------------------------------------------------ **/

      MacroscopePreferencesManager.SetDataExtractorsEnable( this.checkBoxDataExtractorsEnable.Checked );
      MacroscopePreferencesManager.SetDataExtractorsCleanWhiteSpace( this.checkBoxDataExtractorsCleanWhiteSpace.Checked );

      MacroscopePreferencesManager.SetDataExtractorsMaxItemsCssSelectors( ( int )this.numericUpDownDataExtractorsMaxItemsCssSelectors.Value );
      MacroscopePreferencesManager.SetDataExtractorsMaxItemsRegexes( ( int )this.numericUpDownDataExtractorsMaxItemsRegexes.Value );
      MacroscopePreferencesManager.SetDataExtractorsMaxItemsXpaths( ( int )this.numericUpDownDataExtractorsMaxItemsXpaths.Value );

      MacroscopePreferencesManager.SetDataExtractorsApplyToHtml( this.checkBoxDataExtractorsApplyToHtml.Checked );
      MacroscopePreferencesManager.SetDataExtractorsApplyToCss( this.checkBoxDataExtractorsApplyToCss.Checked );
      MacroscopePreferencesManager.SetDataExtractorsApplyToJavascripts( this.checkBoxDataExtractorsApplyToJavascripts.Checked );
      MacroscopePreferencesManager.SetDataExtractorsApplyToText( this.checkBoxDataExtractorsApplyToText.Checked );
      MacroscopePreferencesManager.SetDataExtractorsApplyToXml( this.checkBoxDataExtractorsApplyToXml.Checked );
      
      /** Export Options --------------------------------------------------- **/
              
      MacroscopePreferencesManager.SetSitemapIncludeLinkedPdfs( this.checkBoxSitemapIncludeLinkedPdfs.Checked );

      /** Advanced Settings ---------------------------------------------- **/

      MacroscopePreferencesManager.SetEnableMemoryGuard( this.checkBoxEnableMemoryGuard.Checked );
        
      /** Tidy Up ---------------------------------------------------------- **/
      
      MacroscopePreferencesManager.SavePreferences();
      MacroscopePreferencesManager.ConfigureHttpProxy();

    }

    /**************************************************************************/

  }

}
