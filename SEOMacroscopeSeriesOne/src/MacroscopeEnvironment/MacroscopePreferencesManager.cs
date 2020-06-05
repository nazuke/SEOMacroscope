/*

	This file is part of SEOMacroscope.

	Copyright 2020 Jason Holland.

	The GitHub repository may be found at:

		https://github.com/nazuke/SEOMacroscope

	SEOMacroscope is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	SEOMacroscope is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Windows;
using System.Collections.Generic;

namespace SEOMacroscope
{

  public static class MacroscopePreferencesManager
  {

    /**************************************************************************/

    static MacroscopePreferences Preferences;

    // Application Version
    //static string AppVersion;

    /** Crawl History ------------------------------------------------------ **/

    static StringCollection CrawlHistory;
    static int CrawlHistorySize = 20;

    /** Application Settings ----------------------------------------------- **/

    static bool AutomaticallyCheckForUpdates;

    /** Display Options ---------------------------------------------------- **/

    static bool PauseDisplayDuringScan;
    static bool ShowProgressDialogues;

    /** WebProxy Options --------------------------------------------------- **/

    static int ProxyType = 0;

    /** Global Server Certificate Validation ------------------------------- **/

    static bool ServerCertificateValidation;

    /** Spidering Control -------------------------------------------------- **/

    static string StartUrl;
    static int MaxThreads;
    static int MaxFetchesPerWorker;
    static int Depth;
    static int PageLimit;
    static int RequestTimeout;
    static int MaxRetries;
    static int CrawlDelay;

    static bool CrawlStrictUrlCheck;

    static bool IgnoreQueries;
    static bool IgnoreHashFragments;

    static bool CheckExternalLinks;
    static bool FetchExternalLinks;

    static bool FollowRobotsProtocol;
    static bool FollowSitemapLinks;
    static bool ProbeHumansText;
    static bool ProbeParentFolderUrls;
    static bool ProbeHead404sWithGet;

    static bool CheckRedirects;
    static bool FollowRedirects;
    static bool FollowNoFollow;
    static bool FollowCanonicalLinks;
    static bool FollowAlternateLinks;
    static bool FollowHrefLangLinks;
    static bool FollowListLinks;
    static bool DowncaseLinks;

    static bool FetchStylesheets;
    static bool FetchJavascripts;
    static bool FetchImages;
    static bool FetchAudio;
    static bool FetchVideo;
    static bool FetchXml;
    static bool FetchBinaries;

    static bool ScanSitesInList;

    /** Per-Job Spidering Options ------------------------------------------ **/

    static bool CrawlParentDirectories;
    static bool CrawlChildDirectories;

    /** Analysis Options --------------------------------------------------- **/

    static bool ResolveAddresses;

    static bool CheckHreflangs;

    static bool ProcessAudio;
    static bool ProcessBinaries;
    static bool ProcessImages;
    static bool ProcessJavascripts;
    static bool ProcessPdfs;
    static bool ProcessStylesheets;
    static bool ProcessVideo;
    static bool ProcessXml;

    static int RedirectChainsMaxHops;

    static bool WarnAboutInsecureLinks;

    static bool EnableTextIndexing;
    static bool CaseSensitiveTextIndexing;

    static bool DetectQrCodeInImage;

    /** SEO Options -------------------------------------------------------- **/

    static int TitleMinLen;
    static int TitleMaxLen;
    static int TitleMinWords;
    static int TitleMaxWords;
    static int TitleMaxPixelWidth;

    static int DescriptionMinLen;
    static int DescriptionMaxLen;
    static int DescriptionMinWords;
    static int DescriptionMaxWords;

    static ushort MaxHeadingDepth;

    static bool AnalyzeKeywordsInText;
    static bool AnalyzeTextReadability;
    static int AnalyzeTextReadabilityEnglishAlgorithm;

    static bool EnableLevenshteinDeduplication;
    static int LevenshteinAnalysisLevel;
    static int MaxLevenshteinSizeDifference;
    static int MaxLevenshteinDistance;

    static bool DetectLanguage;

    static bool AnalyzeClickPaths;

    /** Custom Filters Options --------------------------------------------- **/

    static bool CustomFiltersEnable;
    static int CustomFiltersMaxItems;

    static bool CustomFiltersApplyToHtml;
    static bool CustomFiltersApplyToCss;
    static bool CustomFiltersApplyToJavascripts;
    static bool CustomFiltersApplyToText;
    static bool CustomFiltersApplyToXml;

    /** Extractor Options -------------------------------------------------- **/

    static bool DataExtractorsEnable;
    static bool DataExtractorsCleanWhiteSpace;

    static int DataExtractorsMaxItemsCssSelectors;
    static int DataExtractorsMaxItemsRegexes;
    static int DataExtractorsMaxItemsXpaths;

    static bool DataExtractorsApplyToHtml;
    static bool DataExtractorsApplyToCss;
    static bool DataExtractorsApplyToJavascripts;
    static bool DataExtractorsApplyToText;
    static bool DataExtractorsApplyToXml;
    static bool DataExtractorsApplyToPdf;

    /** Export Options ----------------------------------------------------- **/

    static bool SitemapIncludeLinkedPdfs;

    /** Disregard Html5 Elements Settings ---------------------------------- **/

    static bool DisregardHtml5ElementNav;
    static bool DisregardHtml5ElementHeader;
    static bool DisregardHtml5ElementFooter;

    /** Ignore Errors Settings --------------------------------------------- **/

    static bool IgnoreErrors410; // Page GONE
    static bool IgnoreErrors451; // Unavailable for legal reason

    /**************************************************************************/

    static MacroscopePreferencesManager ()
    {

      Preferences = new MacroscopePreferences();

      //CheckAppVersion();

      SetDefaultValues();

      if( Preferences != null )
      {

        if( Preferences.FirstRun == true )
        {

          SetDefaultValues();
          Preferences.FirstRun = false;
          Preferences.Save();

        }
        else
        {

          CrawlHistory = Preferences.CrawlHistory;

          AutomaticallyCheckForUpdates = Preferences.AutomaticallyCheckForUpdates;

          PauseDisplayDuringScan = Preferences.PauseDisplayDuringScan;
          ShowProgressDialogues = Preferences.ShowProgressDialogues;

          ProxyType = Preferences.ProxyType;

          ServerCertificateValidation = Preferences.ServerCertificateValidation;

          StartUrl = Preferences.StartUrl;

          MaxThreads = Preferences.MaxThreads;
          MaxFetchesPerWorker = Preferences.MaxFetchesPerWorker;

          Depth = Preferences.Depth;
          PageLimit = Preferences.PageLimit;
          RequestTimeout = Preferences.RequestTimeout;
          MaxRetries = Preferences.MaxRetries;
          CrawlDelay = Preferences.CrawlDelay;

          CrawlStrictUrlCheck = Preferences.CrawlStrictUrlCheck;

          IgnoreQueries = Preferences.IgnoreQueries;
          IgnoreHashFragments = Preferences.IgnoreHashFragments;

          CheckExternalLinks = Preferences.CheckExternalLinks;
          FetchExternalLinks = Preferences.FetchExternalLinks;

          ResolveAddresses = Preferences.ResolveAddresses;

          CheckHreflangs = Preferences.CheckHreflangs;
          RedirectChainsMaxHops = Preferences.RedirectChainsMaxHops;
          ScanSitesInList = Preferences.ScanSitesInList;
          WarnAboutInsecureLinks = Preferences.WarnAboutInsecureLinks;

          EnableTextIndexing = Preferences.EnableTextIndexing;
          CaseSensitiveTextIndexing = Preferences.CaseSensitiveTextIndexing;

          DetectQrCodeInImage = Preferences.DetectQrCodeInImage;

          EnableLevenshteinDeduplication = Preferences.EnableLevenshteinDeduplication;
          LevenshteinAnalysisLevel = Preferences.LevenshteinAnalysisLevel;
          MaxLevenshteinSizeDifference = Preferences.MaxLevenshteinSizeDifference;
          MaxLevenshteinDistance = Preferences.MaxLevenshteinDistance;

          FollowRobotsProtocol = Preferences.FollowRobotsProtocol;
          FollowSitemapLinks = Preferences.FollowSitemapLinks;
          ProbeHumansText = Preferences.ProbeHumansText;
          ProbeParentFolderUrls = Preferences.ProbeParentFolderUrls;
          ProbeHead404sWithGet = Preferences.ProbeHead404sWithGet;

          CheckRedirects = Preferences.CheckRedirects;
          FollowRedirects = Preferences.FollowRedirects;
          FollowNoFollow = Preferences.FollowNoFollow;
          FollowCanonicalLinks = Preferences.FollowCanonicalLinks;
          FollowAlternateLinks = Preferences.FollowAlternateLinks;
          FollowHrefLangLinks = Preferences.FollowHrefLangLinks;
          FollowListLinks = Preferences.FollowListLinks;
          DowncaseLinks = Preferences.DowncaseLinks;

          FetchStylesheets = Preferences.FetchStylesheets;
          FetchJavascripts = Preferences.FetchJavascripts;
          FetchImages = Preferences.FetchImages;
          FetchAudio = Preferences.FetchAudio;
          FetchVideo = Preferences.FetchVideo;
          FetchXml = Preferences.FetchXml;
          FetchBinaries = Preferences.FetchBinaries;

          ProcessAudio = Preferences.ProcessAudio;
          ProcessBinaries = Preferences.ProcessBinaries;
          ProcessImages = Preferences.ProcessImages;
          ProcessJavascripts = Preferences.ProcessJavascripts;
          ProcessPdfs = Preferences.ProcessPdfs;
          ProcessStylesheets = Preferences.ProcessStylesheets;
          ProcessVideo = Preferences.ProcessVideo;
          ProcessXml = Preferences.ProcessXml;

          CrawlParentDirectories = Preferences.CrawlParentDirectories;
          CrawlChildDirectories = Preferences.CrawlChildDirectories;

          TitleMinLen = Preferences.TitleMinLen;
          TitleMaxLen = Preferences.TitleMaxLen;
          TitleMinWords = Preferences.TitleMinWords;
          TitleMaxWords = Preferences.TitleMaxWords;
          TitleMaxPixelWidth = Preferences.TitleMaxPixelWidth;

          DescriptionMinLen = Preferences.DescriptionMinLen;
          DescriptionMaxLen = Preferences.DescriptionMaxLen;
          DescriptionMinWords = Preferences.DescriptionMinWords;
          DescriptionMaxWords = Preferences.DescriptionMaxWords;
          MaxHeadingDepth = Preferences.MaxHeadingDepth;
          AnalyzeKeywordsInText = Preferences.AnalyzeKeywordsInText;
          AnalyzeTextReadability = Preferences.AnalyzeTextReadability;
          AnalyzeTextReadabilityEnglishAlgorithm = Preferences.AnalyzeTextReadabilityEnglishAlgorithm;

          DetectLanguage = Preferences.DetectLanguage;

          AnalyzeClickPaths = Preferences.AnalyzeClickPaths;

          CustomFiltersEnable = Preferences.CustomFiltersEnable;
          CustomFiltersMaxItems = Preferences.CustomFiltersMaxItems;
          CustomFiltersApplyToHtml = Preferences.CustomFiltersApplyToHtml;
          CustomFiltersApplyToCss = Preferences.CustomFiltersApplyToCss;
          CustomFiltersApplyToJavascripts = Preferences.CustomFiltersApplyToJavascripts;
          CustomFiltersApplyToText = Preferences.CustomFiltersApplyToText;
          CustomFiltersApplyToXml = Preferences.CustomFiltersApplyToXml;

          DataExtractorsEnable = Preferences.DataExtractorsEnable;
          DataExtractorsCleanWhiteSpace = Preferences.DataExtractorsCleanWhiteSpace;
          DataExtractorsMaxItemsCssSelectors = Preferences.DataExtractorsMaxItemsCssSelectors;
          DataExtractorsMaxItemsRegexes = Preferences.DataExtractorsMaxItemsRegexes;
          DataExtractorsMaxItemsXpaths = Preferences.DataExtractorsMaxItemsXpaths;
          DataExtractorsApplyToHtml = Preferences.DataExtractorsApplyToHtml;
          DataExtractorsApplyToCss = Preferences.DataExtractorsApplyToCss;
          DataExtractorsApplyToJavascripts = Preferences.DataExtractorsApplyToJavascripts;
          DataExtractorsApplyToText = Preferences.DataExtractorsApplyToText;
          DataExtractorsApplyToPdf = Preferences.DataExtractorsApplyToPdf;
          DataExtractorsApplyToXml = Preferences.DataExtractorsApplyToXml;

          SitemapIncludeLinkedPdfs = Preferences.SitemapIncludeLinkedPdfs;

          DisregardHtml5ElementNav = Preferences.DisregardHtml5ElementNav;
          DisregardHtml5ElementHeader = Preferences.DisregardHtml5ElementHeader;
          DisregardHtml5ElementFooter = Preferences.DisregardHtml5ElementFooter;

          IgnoreErrors410 = Preferences.IgnoreErrors410;
          IgnoreErrors451 = Preferences.IgnoreErrors451;

        }

      }

      SanitizeValues();

      ConfigureServerCertificateValidation();

      DebugMsg( string.Format( "MacroscopePreferencesManager StartUrl: \"{0}\"", StartUrl ) );
      DebugMsg( string.Format( "MacroscopePreferencesManager Depth: {0}", Depth ) );
      DebugMsg( string.Format( "MacroscopePreferencesManager PageLimit: {0}", PageLimit ) );

    }

    /**************************************************************************/

    private static void CheckAppVersion ()
    {

      string SavedAppVersion = Preferences.AppVersion;
      bool DoReset = false;

      if( string.IsNullOrEmpty( SavedAppVersion ) )
      {
        DoReset = true;
      }

      if( DoReset )
      {
        SetDefaultValues();
        SavePreferences();
      }

    }

    /**************************************************************************/

    public static void SetDefaultValues ()
    {

      /** Crawl History ------------------------------------------------------ **/

      CrawlHistory = new StringCollection();

      /** Application Settings --------------------------------------------- **/

      AutomaticallyCheckForUpdates = true;

      /** Display Options -------------------------------------------------- **/

      PauseDisplayDuringScan = false;
      ShowProgressDialogues = true;

      /** WebProxy Options ------------------------------------------------- **/

      ProxyType = 0;

      /** Global Server Certificate Validation ----------------------------- **/

      SetServerCertificateValidation( true );

      /** Spidering Control ------------------------------------------------ **/

      StartUrl = "";
      MaxThreads = 2;
      MaxFetchesPerWorker = 256;
      Depth = -1;
      PageLimit = -1;
      RequestTimeout = 10;
      MaxRetries = 0;
      CrawlDelay = 0;

      CrawlStrictUrlCheck = false;

      IgnoreQueries = false;
      IgnoreHashFragments = true;

      CheckExternalLinks = true;
      FetchExternalLinks = false;

      FollowRobotsProtocol = true;
      FollowSitemapLinks = true;
      ProbeHumansText = false;
      ProbeParentFolderUrls = false;
      ProbeHead404sWithGet = false;

      CheckRedirects = true;
      FollowRedirects = false;
      FollowNoFollow = true;
      FollowCanonicalLinks = true;
      FollowAlternateLinks = true;
      FollowHrefLangLinks = false;
      FollowListLinks = false;
      DowncaseLinks = false;

      FetchStylesheets = true;
      FetchJavascripts = true;
      FetchImages = true;
      FetchAudio = true;
      FetchVideo = true;
      FetchXml = true;
      FetchBinaries = true;

      ProcessAudio = true;
      ProcessBinaries = true;
      ProcessImages = true;
      ProcessJavascripts = true;
      ProcessPdfs = false;
      ProcessStylesheets = true;
      ProcessVideo = true;
      ProcessXml = true;

      /** Per-Job Spidering Options ---------------------------------------- **/

      CrawlParentDirectories = true;
      CrawlChildDirectories = true;

      /** Analysis Options ------------------------------------------------- **/

      ResolveAddresses = false;
      CheckHreflangs = false;
      RedirectChainsMaxHops = 10;
      ScanSitesInList = false;
      WarnAboutInsecureLinks = true;

      EnableTextIndexing = false;
      CaseSensitiveTextIndexing = false;

      DetectQrCodeInImage = false;

      EnableLevenshteinDeduplication = false;
      LevenshteinAnalysisLevel = 1; // 1 | 2
      MaxLevenshteinSizeDifference = 64;
      MaxLevenshteinDistance = 16;

      DetectLanguage = true;

      AnalyzeClickPaths = false;

      /** SEO Options ------------------------------------------------------ **/

      TitleMinLen = 10;
      TitleMaxLen = 70;
      TitleMinWords = 3;
      TitleMaxWords = 10;
      TitleMaxPixelWidth = 512;

      DescriptionMinLen = 10;
      DescriptionMaxLen = 150;
      DescriptionMinWords = 3;
      DescriptionMaxWords = 20;

      MaxHeadingDepth = 2;

      AnalyzeKeywordsInText = false;
      AnalyzeTextReadability = false;
      AnalyzeTextReadabilityEnglishAlgorithm = 0;

      /** Custom Filter Options -------------------------------------------- **/

      CustomFiltersEnable = false;

      CustomFiltersMaxItems = 5;

      CustomFiltersApplyToHtml = true;
      CustomFiltersApplyToCss = true;
      CustomFiltersApplyToJavascripts = true;
      CustomFiltersApplyToText = true;
      CustomFiltersApplyToXml = true;

      /** Extractor Options ------------------------------------------------ **/

      DataExtractorsEnable = false;
      DataExtractorsCleanWhiteSpace = true;

      DataExtractorsMaxItemsCssSelectors = 5;
      DataExtractorsMaxItemsRegexes = 5;
      DataExtractorsMaxItemsXpaths = 5;

      DataExtractorsApplyToHtml = true;
      DataExtractorsApplyToCss = true;
      DataExtractorsApplyToJavascripts = true;
      DataExtractorsApplyToText = true;
      DataExtractorsApplyToPdf = false;
      DataExtractorsApplyToXml = true;

      /** Export Options --------------------------------------------------- **/

      SitemapIncludeLinkedPdfs = false;

      /** Ignore Errors Settings ------------------------------------------- **/

      DisregardHtml5ElementNav = true;
      DisregardHtml5ElementHeader = true;
      DisregardHtml5ElementFooter = true;

      /** Ignore Errors Settings ------------------------------------------- **/

      IgnoreErrors410 = true;
      IgnoreErrors451 = true;

      /** ------------------------------------------------------------------ **/

    }

    /**************************************************************************/

    static void SanitizeValues ()
    {

      if( CrawlHistory == null )
      {
        CrawlHistory = new StringCollection();
      }

      if( StartUrl.Length > 0 )
      {
        StartUrl = Regex.Replace( StartUrl, @"^\s+", "" );
        StartUrl = Regex.Replace( StartUrl, @"\s+$", "" );
      }

      if( Depth < 0 )
      {
        Depth = -1;
      }

      if( PageLimit < 0 )
      {
        PageLimit = -1;
      }

      if( RequestTimeout <= 10 )
      {
        RequestTimeout = 10;
      }

      if( RequestTimeout >= 50 )
      {
        RequestTimeout = 50;
      }

      if( MaxRetries < 0 )
      {
        MaxRetries = 0;
      }

      if( MaxRetries > 10 )
      {
        MaxRetries = 10;
      }

      if( CrawlDelay < 0 )
      {
        CrawlDelay = 0;
      }

      if( CrawlDelay > 60 )
      {
        CrawlDelay = 60;
      }

      /** Custom Filter Options -------------------------------------------- **/

      if( CustomFiltersMaxItems < 1 )
      {
        CustomFiltersMaxItems = 1;
      }

      if( CustomFiltersMaxItems > 100 )
      {
        CustomFiltersMaxItems = 100;
      }

      /** Data Extractor Options ------------------------------------------- **/

      if( DataExtractorsMaxItemsCssSelectors < 1 )
      {
        DataExtractorsMaxItemsCssSelectors = 1;
      }

      if( DataExtractorsMaxItemsCssSelectors > 100 )
      {
        DataExtractorsMaxItemsCssSelectors = 100;
      }

      if( DataExtractorsMaxItemsRegexes < 1 )
      {
        DataExtractorsMaxItemsRegexes = 1;
      }

      if( DataExtractorsMaxItemsRegexes > 100 )
      {
        DataExtractorsMaxItemsRegexes = 100;
      }

      if( DataExtractorsMaxItemsXpaths < 1 )
      {
        DataExtractorsMaxItemsXpaths = 1;
      }

      if( DataExtractorsMaxItemsXpaths > 100 )
      {
        DataExtractorsMaxItemsXpaths = 100;
      }

      /** ------------------------------------------------------------------ **/

      SavePreferences();

    }

    /**************************************************************************/

    public static void SavePreferences ()
    {

      if( Preferences != null )
      {

        Preferences.CrawlHistory = CrawlHistory;

        Preferences.AutomaticallyCheckForUpdates = AutomaticallyCheckForUpdates;

        Preferences.PauseDisplayDuringScan = PauseDisplayDuringScan;
        Preferences.ShowProgressDialogues = ShowProgressDialogues;

        Preferences.ProxyType = ProxyType;

        Preferences.StartUrl = StartUrl;

        Preferences.MaxThreads = MaxThreads;
        Preferences.MaxFetchesPerWorker = MaxFetchesPerWorker;

        Preferences.Depth = Depth;
        Preferences.PageLimit = PageLimit;
        Preferences.RequestTimeout = RequestTimeout;
        Preferences.MaxRetries = MaxRetries;
        Preferences.CrawlDelay = CrawlDelay;

        Preferences.CrawlStrictUrlCheck = CrawlStrictUrlCheck;

        Preferences.IgnoreQueries = IgnoreQueries;
        Preferences.IgnoreHashFragments = IgnoreHashFragments;

        Preferences.CheckExternalLinks = CheckExternalLinks;
        Preferences.FetchExternalLinks = FetchExternalLinks;

        Preferences.ResolveAddresses = ResolveAddresses;

        Preferences.CheckHreflangs = CheckHreflangs;
        Preferences.RedirectChainsMaxHops = RedirectChainsMaxHops;
        Preferences.ScanSitesInList = ScanSitesInList;
        Preferences.WarnAboutInsecureLinks = WarnAboutInsecureLinks;

        Preferences.EnableTextIndexing = EnableTextIndexing;
        Preferences.CaseSensitiveTextIndexing = CaseSensitiveTextIndexing;

        Preferences.DetectQrCodeInImage = DetectQrCodeInImage;

        Preferences.EnableLevenshteinDeduplication = EnableLevenshteinDeduplication;
        Preferences.LevenshteinAnalysisLevel = LevenshteinAnalysisLevel;
        Preferences.MaxLevenshteinSizeDifference = MaxLevenshteinSizeDifference;
        Preferences.MaxLevenshteinDistance = MaxLevenshteinDistance;

        Preferences.FollowRobotsProtocol = FollowRobotsProtocol;
        Preferences.FollowSitemapLinks = FollowSitemapLinks;
        Preferences.ProbeHumansText = ProbeHumansText;
        Preferences.ProbeParentFolderUrls = ProbeParentFolderUrls;
        Preferences.ProbeHead404sWithGet = ProbeHead404sWithGet;

        Preferences.CheckRedirects = CheckRedirects;
        Preferences.FollowRedirects = FollowRedirects;

        Preferences.FollowNoFollow = FollowNoFollow;
        Preferences.FollowCanonicalLinks = FollowCanonicalLinks;
        Preferences.FollowHrefLangLinks = FollowHrefLangLinks;
        Preferences.FollowListLinks = FollowListLinks;
        Preferences.DowncaseLinks = DowncaseLinks;

        Preferences.FetchStylesheets = FetchStylesheets;
        Preferences.FetchJavascripts = FetchJavascripts;
        Preferences.FetchImages = FetchImages;
        Preferences.FetchAudio = FetchAudio;
        Preferences.FetchVideo = FetchVideo;
        Preferences.FetchXml = FetchXml;
        Preferences.FetchBinaries = FetchBinaries;

        Preferences.ProcessAudio = ProcessAudio;
        Preferences.ProcessBinaries = ProcessBinaries;
        Preferences.ProcessImages = ProcessImages;
        Preferences.ProcessJavascripts = ProcessJavascripts;
        Preferences.ProcessPdfs = ProcessPdfs;
        Preferences.ProcessStylesheets = ProcessStylesheets;
        Preferences.ProcessVideo = ProcessVideo;
        Preferences.ProcessXml = ProcessXml;

        Preferences.CrawlParentDirectories = CrawlParentDirectories;
        Preferences.CrawlChildDirectories = CrawlChildDirectories;

        Preferences.TitleMinLen = TitleMinLen;
        Preferences.TitleMaxLen = TitleMaxLen;
        Preferences.TitleMinWords = TitleMinWords;
        Preferences.TitleMaxWords = TitleMaxWords;
        Preferences.TitleMaxPixelWidth = TitleMaxPixelWidth;

        Preferences.DescriptionMinLen = DescriptionMinLen;
        Preferences.DescriptionMaxLen = DescriptionMaxLen;
        Preferences.DescriptionMinWords = DescriptionMinWords;
        Preferences.DescriptionMaxWords = DescriptionMaxWords;

        Preferences.MaxHeadingDepth = MaxHeadingDepth;

        Preferences.AnalyzeKeywordsInText = AnalyzeKeywordsInText;
        Preferences.AnalyzeTextReadability = AnalyzeTextReadability;
        Preferences.AnalyzeTextReadabilityEnglishAlgorithm = AnalyzeTextReadabilityEnglishAlgorithm;

        Preferences.DetectLanguage = DetectLanguage;

        Preferences.AnalyzeClickPaths = AnalyzeClickPaths;

        Preferences.CustomFiltersEnable = CustomFiltersEnable;
        Preferences.CustomFiltersMaxItems = CustomFiltersMaxItems;
        Preferences.CustomFiltersApplyToHtml = CustomFiltersApplyToHtml;
        Preferences.CustomFiltersApplyToCss = CustomFiltersApplyToCss;
        Preferences.CustomFiltersApplyToJavascripts = CustomFiltersApplyToJavascripts;
        Preferences.CustomFiltersApplyToText = CustomFiltersApplyToText;
        Preferences.CustomFiltersApplyToXml = CustomFiltersApplyToXml;

        Preferences.DataExtractorsEnable = DataExtractorsEnable;
        Preferences.DataExtractorsCleanWhiteSpace = DataExtractorsCleanWhiteSpace;
        Preferences.DataExtractorsMaxItemsCssSelectors = DataExtractorsMaxItemsCssSelectors;
        Preferences.DataExtractorsMaxItemsRegexes = DataExtractorsMaxItemsRegexes;
        Preferences.DataExtractorsMaxItemsXpaths = DataExtractorsMaxItemsXpaths;
        Preferences.DataExtractorsApplyToHtml = DataExtractorsApplyToHtml;
        Preferences.DataExtractorsApplyToCss = DataExtractorsApplyToCss;
        Preferences.DataExtractorsApplyToJavascripts = DataExtractorsApplyToJavascripts;
        Preferences.DataExtractorsApplyToText = DataExtractorsApplyToText;
        Preferences.DataExtractorsApplyToPdf = DataExtractorsApplyToPdf;
        Preferences.DataExtractorsApplyToXml = DataExtractorsApplyToXml;

        Preferences.SitemapIncludeLinkedPdfs = SitemapIncludeLinkedPdfs;

        Preferences.DisregardHtml5ElementNav = DisregardHtml5ElementNav;
        Preferences.DisregardHtml5ElementHeader = DisregardHtml5ElementHeader;
        Preferences.DisregardHtml5ElementFooter = DisregardHtml5ElementFooter;

        Preferences.IgnoreErrors410 = IgnoreErrors410;
        Preferences.IgnoreErrors451 = IgnoreErrors451;

        Preferences.Save();

      }

    }

    /** Application Settings **************************************************/

    public static bool GetAutomaticallyCheckForUpdates ()
    {
      return ( AutomaticallyCheckForUpdates );
    }

    public static void SetAutomaticallyCheckForUpdates ( bool State )
    {
      AutomaticallyCheckForUpdates = State;
    }

    /** Display Options *******************************************************/

    public static bool GetPauseDisplayDuringScan ()
    {
      return ( PauseDisplayDuringScan );
    }

    public static void SetPauseDisplayDuringScan ( bool State )
    {
      PauseDisplayDuringScan = State;
    }

    public static bool GetShowProgressDialogues ()
    {
      return ( ShowProgressDialogues );
    }

    public static void SetShowProgressDialogues ( bool State )
    {
      ShowProgressDialogues = State;
    }

    /** HTTP Proxy ************************************************************/

    public static void SetProxyType ( int Value )
    {
      ProxyType = Value;
    }

    public static int GetProxyType ()
    {
      return ( ProxyType );
    }

    /** Global Server Certificate Validation **********************************/

    public static bool GetServerCertificateValidation ()
    {
      return ( ServerCertificateValidation );
    }

    /** -------------------------------------------------------------------- **/

    public static void SetServerCertificateValidation ( bool Value )
    {

      ServerCertificateValidation = Value;

      ConfigureServerCertificateValidation();

    }

    /** -------------------------------------------------------------------- **/

    private static void ConfigureServerCertificateValidation ()
    {
      if( ServerCertificateValidation )
      {
        ServicePointManager.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
      }
      else
      {
        if( ServicePointManager.ServerCertificateValidationCallback != null )
        {
          ServicePointManager.ServerCertificateValidationCallback -= ServerCertificateValidationCallback;
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    private static bool ServerCertificateValidationCallback (
      object sender,
      System.Security.Cryptography.X509Certificates.X509Certificate certificate,
      System.Security.Cryptography.X509Certificates.X509Chain chain,
      System.Net.Security.SslPolicyErrors sslPolicyErrors
    )
    {
      return ( true );
    }

    /** Set Starting URL ******************************************************/

    public static void SetStartUrl ( string Url )
    {
      StartUrl = Url;
    }

    /** -------------------------------------------------------------------- **/

    public static string GetStartUrl ()
    {
      return ( StartUrl );
    }

    /** Crawl History *********************************************************/

    public static void CrawlHistoryPush ( string Url )
    {

      if( !CrawlHistory.Contains( Url ) )
      {

        if( CrawlHistory.Count >= CrawlHistorySize )
        {
          CrawlHistoryPop();
        }

        CrawlHistory.Add( value: Url );

        SavePreferences();

      }

    }

    /** -------------------------------------------------------------------- **/

    public static void CrawlHistoryPop ()
    {
      if( CrawlHistory.Count > 0 )
      {
        CrawlHistory.RemoveAt( 0 );
      }
    }

    /** -------------------------------------------------------------------- **/

    public static List<string> GetCrawlHistory ()
    {

      List<string> CrawlHistoryCopy = new List<string>();

      foreach( string Url in CrawlHistory )
      {
        CrawlHistoryCopy.Add( Url );
      }

      return ( CrawlHistoryCopy );

    }

    /** -------------------------------------------------------------------- **/

    public static void ClearCrawlHistory ()
    {
      CrawlHistory.Clear();
    }

    /**************************************************************************/

    public static int GetMaxThreads ()
    {
      return ( MaxThreads );
    }

    public static void SetMaxThreads ( int Max )
    {
      MaxThreads = Max;
    }

    /**************************************************************************/

    public static int GetMaxFetchesPerWorker ()
    {
      return ( MaxFetchesPerWorker );
    }

    public static void SetMaxFetchesPerWorker ( int Max )
    {
      MaxFetchesPerWorker = Max;
    }

    /**************************************************************************/

    public static int GetDepth ()
    {
      return ( Depth );
    }

    public static void SetDepth ( int Max )
    {
      Depth = Max;
    }

    /**************************************************************************/

    public static int GetPageLimit ()
    {
      return ( PageLimit );
    }

    public static void SetPageLimit ( int Max )
    {
      PageLimit = Max;
    }

    /** Request Timeout *******************************************************/

    public static int GetRequestTimeout ()
    {
      return ( RequestTimeout );
    }

    public static void SetRequestTimeout ( int Seconds )
    {
      RequestTimeout = Seconds;
    }

    /** Maximum Retries *******************************************************/

    public static int GetMaxRetries ()
    {
      return ( MaxRetries );
    }

    public static void SetMaxRetries ( int Retries )
    {
      MaxRetries = Retries;
    }

    /** Crawl Delay ***********************************************************/

    public static int GetCrawlDelay ()
    {
      return ( CrawlDelay );
    }

    public static void SetCrawlDelay ( int Seconds )
    {
      CrawlDelay = Seconds;
    }

    /** Strict URL Check ******************************************************/

    public static bool GetCrawlStrictUrlCheck ()
    {
      return ( CrawlStrictUrlCheck );
    }

    public static void SetCrawlStrictUrlCheck ( bool State )
    {
      CrawlStrictUrlCheck = State;
    }

    /** Ignore Queries ********************************************************/

    public static bool GetIgnoreQueries ()
    {
      return ( IgnoreQueries );
    }

    public static void SetIgnoreQueries ( bool State )
    {
      IgnoreQueries = State;
    }

    /** Ignore Hash Fragments *************************************************/

    public static bool GetIgnoreHashFragments ()
    {
      return ( IgnoreHashFragments );
    }

    public static void SetIgnoreHashFragments ( bool State )
    {
      IgnoreHashFragments = State;
    }

    /** Domain Spidering Controls *********************************************/

    public static bool GetCheckExternalLinks ()
    {
      return ( CheckExternalLinks );
    }

    public static void SetCheckExternalLinks ( bool State )
    {
      CheckExternalLinks = State;
    }

    /** -------------------------------------------------------------------- **/

    public static bool GetFetchExternalLinks ()
    {
      return ( FetchExternalLinks );
    }

    public static void SetFetchExternalLinks ( bool State )
    {
      FetchExternalLinks = State;
    }

    /**************************************************************************/

    public static bool GetScanSitesInList ()
    {
      return ( ScanSitesInList );
    }

    public static void SetScanSitesInList ( bool State )
    {
      ScanSitesInList = State;
    }

    /**************************************************************************/

    public static bool GetResolveAddresses ()
    {
      return ( ResolveAddresses );
    }

    public static void SetResolveAddresses ( bool State )
    {
      ResolveAddresses = State;
    }

    /**************************************************************************/

    public static bool GetCheckHreflangs ()
    {
      return ( CheckHreflangs );
    }

    public static void SetCheckHreflangs ( bool State )
    {
      CheckHreflangs = State;
    }

    /**************************************************************************/

    public static int GetRedirectChainsMaxHops ()
    {
      return ( RedirectChainsMaxHops );
    }

    public static void SetRedirectChainsMaxHops ( int Max )
    {
      RedirectChainsMaxHops = Max;
    }

    /**************************************************************************/

    public static bool GetWarnAboutInsecureLinks ()
    {
      return ( WarnAboutInsecureLinks );
    }

    public static void SetWarnAboutInsecureLinks ( bool State )
    {
      WarnAboutInsecureLinks = State;
    }

    /**************************************************************************/

    public static bool GetEnableTextIndexing ()
    {
      return ( EnableTextIndexing );
    }

    public static void SetEnableTextIndexing ( bool State )
    {
      EnableTextIndexing = State;
    }

    /** -------------------------------------------------------------------- **/

    public static bool GetCaseSensitiveTextIndexing ()
    {
      return ( CaseSensitiveTextIndexing );
    }

    public static void SetCaseSensitiveTextIndexing ( bool State )
    {
      CaseSensitiveTextIndexing = State;
    }

    /** QR Codes **************************************************************/

    public static bool GetDetectQrCodeInImage ()
    {
      return ( DetectQrCodeInImage );
    }

    public static void SetDetectQrCodeInImage ( bool State )
    {
      DetectQrCodeInImage = State;
    }

    /** Levenshtein Deduplication *********************************************/

    public static bool GetEnableLevenshteinDeduplication ()
    {
      return ( EnableLevenshteinDeduplication );
    }

    public static void SetEnableLevenshteinDeduplication ( bool State )
    {
      EnableLevenshteinDeduplication = State;
    }

    /** -------------------------------------------------------------------- **/

    public static int GetLevenshteinAnalysisLevel ()
    {
      return ( LevenshteinAnalysisLevel );
    }

    public static void SetLevenshteinAnalysisLevel ( int Level )
    {
      LevenshteinAnalysisLevel = Level;
    }

    /** -------------------------------------------------------------------- **/

    public static int GetMaxLevenshteinSizeDifference ()
    {
      return ( MaxLevenshteinSizeDifference );
    }

    public static void SetMaxLevenshteinSizeDifference ( int Max )
    {
      MaxLevenshteinSizeDifference = Max;
    }

    /** -------------------------------------------------------------------- **/

    public static int GetMaxLevenshteinDistance ()
    {
      return ( MaxLevenshteinDistance );
    }

    public static void SetMaxLevenshteinDistance ( int Max )
    {
      MaxLevenshteinDistance = Max;
    }

    /** Robots ****************************************************************/

    public static bool GetFollowRobotsProtocol ()
    {
      return ( FollowRobotsProtocol );
    }

    public static void SetFollowRobotsProtocol ( bool State )
    {
      FollowRobotsProtocol = State;
    }

    /** Sitemap Links *********************************************************/

    public static bool GetFollowSitemapLinks ()
    {
      return ( FollowSitemapLinks );
    }

    public static void SetFollowSitemapLinks ( bool State )
    {
      FollowSitemapLinks = State;
    }

    /** Humans ****************************************************************/

    public static bool GetProbeHumansText ()
    {
      return ( ProbeHumansText );
    }

    public static void SetProbeHumansText ( bool State )
    {
      ProbeHumansText = State;
    }

    /** Probe Parent Folder URLs **********************************************/

    public static bool GetProbeParentFolderUrls ()
    {
      return ( ProbeParentFolderUrls );
    }

    public static void SetProbeParentFolderUrls ( bool State )
    {
      ProbeParentFolderUrls = State;
    }

    /** Probe HEAD 404s with GET **********************************************/

    public static bool GetProbeHead404sWithGet ()
    {
      return ( ProbeHead404sWithGet );
    }

    public static void SetProbeHead404sWithGet ( bool State )
    {
      ProbeHead404sWithGet = State;
    }

    /**************************************************************************/

    public static bool GetCheckRedirects ()
    {
      return ( CheckRedirects );
    }

    public static void SetCheckRedirects ( bool State )
    {
      CheckRedirects = State;
    }

    /** -------------------------------------------------------------------- **/

    public static bool GetFollowRedirects ()
    {
      return ( FollowRedirects );
    }

    public static void SetFollowRedirects ( bool State )
    {
      FollowRedirects = State;
    }

    /**************************************************************************/

    public static bool GetFollowNoFollow ()
    {
      return ( FollowNoFollow );
    }

    public static void SetFollowNoFollow ( bool State )
    {
      FollowNoFollow = State;
    }

    /**************************************************************************/

    public static bool GetFollowCanonicalLinks ()
    {
      return ( FollowCanonicalLinks );
    }

    public static void SetFollowCanonicalLinks ( bool State )
    {
      FollowCanonicalLinks = State;
    }

    /**************************************************************************/

    public static bool GetFollowAlternateLinks ()
    {
      return ( FollowAlternateLinks );
    }

    public static void SetFollowAlternateLinks ( bool State )
    {
      FollowAlternateLinks = State;
    }

    /**************************************************************************/

    public static bool GetFollowHrefLangLinks ()
    {
      return ( FollowHrefLangLinks );
    }

    public static void SetFollowHrefLangLinks ( bool State )
    {
      FollowHrefLangLinks = State;
    }

    /**************************************************************************/

    public static bool GetFollowListLinks ()
    {
      return ( FollowListLinks );
    }

    public static void SetFollowListLinks ( bool State )
    {
      FollowListLinks = State;
    }

    /**************************************************************************/

    public static bool GetDowncaseLinks ()
    {
      return ( DowncaseLinks );
    }

    public static void SetDowncaseLinks ( bool State )
    {
      DowncaseLinks = State;
    }

    /** CRAWL DOCUMENT TYPES **************************************************/

    public static bool GetFetchStylesheets ()
    {
      return ( FetchStylesheets );
    }

    public static void SetFetchStylesheets ( bool State )
    {
      FetchStylesheets = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetFetchJavascripts ()
    {
      return ( FetchJavascripts );
    }

    public static void SetFetchJavascripts ( bool State )
    {
      FetchJavascripts = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetFetchImages ()
    {
      return ( FetchImages );
    }

    public static void SetFetchImages ( bool State )
    {
      FetchImages = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetFetchAudio ()
    {
      return ( FetchAudio );
    }

    public static void SetFetchAudio ( bool State )
    {
      FetchAudio = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetFetchVideo ()
    {
      return ( FetchVideo );
    }

    public static void SetFetchVideo ( bool State )
    {
      FetchVideo = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetFetchXml ()
    {
      return ( FetchXml );
    }

    public static void SetFetchXml ( bool State )
    {
      FetchXml = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetFetchBinaries ()
    {
      return ( FetchBinaries );
    }

    public static void SetFetchBinaries ( bool State )
    {
      FetchBinaries = State;
    }

    /** PROCESS DOCUMENT TYPES ************************************************/

    public static bool GetProcessAudio ()
    {
      return ( ProcessAudio );
    }

    public static void SetProcessAudio ( bool State )
    {
      ProcessAudio = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetProcessBinaries ()
    {
      return ( ProcessBinaries );
    }

    public static void SetProcessBinaries ( bool State )
    {
      ProcessBinaries = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetProcessImages ()
    {
      return ( ProcessImages );
    }

    public static void SetProcessImages ( bool State )
    {
      ProcessImages = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetProcessJavascripts ()
    {
      return ( ProcessJavascripts );
    }

    public static void SetProcessJavascripts ( bool State )
    {
      ProcessJavascripts = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetProcessPdfs ()
    {
      return ( ProcessPdfs );
    }

    public static void SetProcessPdfs ( bool State )
    {
      ProcessPdfs = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetProcessStylesheets ()
    {
      return ( ProcessStylesheets );
    }

    public static void SetProcessStylesheets ( bool State )
    {
      ProcessStylesheets = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetProcessVideo ()
    {
      return ( ProcessVideo );
    }

    public static void SetProcessVideo ( bool State )
    {
      ProcessVideo = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetProcessXml ()
    {
      return ( ProcessXml );
    }

    public static void SetProcessXml ( bool State )
    {
      ProcessXml = State;
    }

    /** Per-Job Spidering Options *********************************************/

    public static bool GetCrawlParentDirectories ()
    {
      return ( CrawlParentDirectories );
    }

    /** -------------------------------------------------------------------  **/

    public static void SetCrawlParentDirectories ( bool State )
    {
      CrawlParentDirectories = State;
    }

    /** -------------------------------------------------------------------  **/

    public static bool GetCrawlChildDirectories ()
    {
      return ( CrawlChildDirectories );
    }

    /** -------------------------------------------------------------------  **/

    public static void SetCrawlChildDirectories ( bool State )
    {
      CrawlChildDirectories = State;
    }

    /** SEO Options ***********************************************************/

    public static int GetTitleMinLen ()
    {
      return ( TitleMinLen );
    }

    public static void SetTitleMinLen ( int Length )
    {
      TitleMinLen = Length;
    }

    public static int GetTitleMaxLen ()
    {
      return ( TitleMaxLen );
    }

    public static void SetTitleMaxLen ( int Length )
    {
      TitleMaxLen = Length;
    }

    public static int GetTitleMinWords ()
    {
      return ( TitleMinWords );
    }

    public static void SetTitleMinWords ( int Min )
    {
      TitleMinWords = Min;
    }

    public static int GetTitleMaxWords ()
    {
      return ( TitleMaxWords );
    }

    public static void SetTitleMaxWords ( int Max )
    {
      TitleMaxWords = Max;
    }

    public static int GetTitleMaxPixelWidth ()
    {
      return ( TitleMaxPixelWidth );
    }

    public static void SetTitleMaxPixelWidth ( int Max )
    {
      TitleMaxPixelWidth = Max;
    }

    /* ---------------------------------------------------------------------- */

    public static int GetDescriptionMinLen ()
    {
      return ( DescriptionMinLen );
    }

    public static void SetDescriptionMinLen ( int Length )
    {
      DescriptionMinLen = Length;
    }

    public static int GetDescriptionMaxLen ()
    {
      return ( DescriptionMaxLen );
    }

    public static void SetDescriptionMaxLen ( int Length )
    {
      DescriptionMaxLen = Length;
    }

    public static int GetDescriptionMinWords ()
    {
      return ( DescriptionMinWords );
    }

    public static void SetDescriptionMinWords ( int Min )
    {
      DescriptionMinWords = Min;
    }

    public static int GetDescriptionMaxWords ()
    {
      return ( DescriptionMaxWords );
    }

    public static void SetDescriptionMaxWords ( int Max )
    {
      DescriptionMaxWords = Max;
    }

    public static ushort GetMaxHeadingDepth ()
    {
      return ( MaxHeadingDepth );
    }

    public static void SetMaxHeadingDepth ( ushort Depth )
    {
      MaxHeadingDepth = Depth;
    }

    public static bool GetAnalyzeKeywordsInText ()
    {
      return ( AnalyzeKeywordsInText );
    }

    public static void SetAnalyzeKeywordsInText ( bool State )
    {
      AnalyzeKeywordsInText = State;
    }

    /* Readability Options -------------------------------------------------- */

    public static bool GetAnalyzeTextReadability ()
    {
      return ( AnalyzeTextReadability );
    }

    public static void SetAnalyzeTextReadability ( bool State )
    {
      AnalyzeTextReadability = State;
    }

    public static MacroscopeAnalyzeReadability.AnalyzeReadabilityEnglishAlgorithm GetAnalyzeTextReadabilityEnglishAlgorithm ()
    {
      return ( (MacroscopeAnalyzeReadability.AnalyzeReadabilityEnglishAlgorithm) AnalyzeTextReadabilityEnglishAlgorithm );
    }

    public static void SetAnalyzeTextReadabilityEnglishAlgorithm ( MacroscopeAnalyzeReadability.AnalyzeReadabilityEnglishAlgorithm Selected )
    {
      AnalyzeTextReadabilityEnglishAlgorithm = (int) Selected;
    }

    /* ---------------------------------------------------------------------- */

    public static bool GetDetectLanguage ()
    {
      return ( DetectLanguage );
    }

    public static void SetDetectLanguage ( bool Enabled )
    {
      DetectLanguage = Enabled;
    }

    /* ---------------------------------------------------------------------- */

    public static bool GetAnalyzeClickPaths ()
    {
      return ( AnalyzeClickPaths );
    }

    public static void SetAnalyzeClickPaths ( bool Enabled )
    {
      AnalyzeClickPaths = Enabled;
    }

    /** Custom Filter Options *************************************************/

    public static bool GetCustomFiltersEnable ()
    {
      return ( CustomFiltersEnable );
    }

    public static void SetCustomFiltersEnable ( bool State )
    {
      CustomFiltersEnable = State;
    }

    /* ---------------------------------------------------------------------- */

    public static int GetCustomFiltersMaxItems ()
    {
      return ( CustomFiltersMaxItems );
    }

    public static void SetCustomFiltersMaxItems ( int Max )
    {
      CustomFiltersMaxItems = Max;
    }

    /* ---------------------------------------------------------------------- */

    public static bool GetCustomFiltersApplyToHtml ()
    {
      return ( CustomFiltersApplyToHtml );
    }

    public static void SetCustomFiltersApplyToHtml ( bool State )
    {
      CustomFiltersApplyToHtml = State;
    }

    public static bool GetCustomFiltersApplyToCss ()
    {
      return ( CustomFiltersApplyToCss );
    }

    public static void SetCustomFiltersApplyToCss ( bool State )
    {
      CustomFiltersApplyToCss = State;
    }

    public static bool GetCustomFiltersApplyToJavascripts ()
    {
      return ( CustomFiltersApplyToJavascripts );
    }

    public static void SetCustomFiltersApplyToJavascripts ( bool State )
    {
      CustomFiltersApplyToJavascripts = State;
    }

    public static bool GetCustomFiltersApplyToText ()
    {
      return ( CustomFiltersApplyToText );
    }

    public static void SetCustomFiltersApplyToText ( bool State )
    {
      CustomFiltersApplyToText = State;
    }

    public static bool GetCustomFiltersApplyToXml ()
    {
      return ( CustomFiltersApplyToXml );
    }

    public static void SetCustomFiltersApplyToXml ( bool State )
    {
      CustomFiltersApplyToXml = State;
    }

    /** Data Extractor Options ************************************************/

    public static bool GetDataExtractorsEnable ()
    {
      return ( DataExtractorsEnable );
    }

    public static void SetDataExtractorsEnable ( bool State )
    {
      DataExtractorsEnable = State;
    }

    /* ---------------------------------------------------------------------- */

    public static bool GetDataExtractorsCleanWhiteSpace ()
    {
      return ( DataExtractorsCleanWhiteSpace );
    }

    public static void SetDataExtractorsCleanWhiteSpace ( bool State )
    {
      DataExtractorsCleanWhiteSpace = State;
    }

    /* ---------------------------------------------------------------------- */

    public static int GetDataExtractorsMaxItemsCssSelectors ()
    {
      return ( DataExtractorsMaxItemsCssSelectors );
    }

    public static void SetDataExtractorsMaxItemsCssSelectors ( int Max )
    {
      DataExtractorsMaxItemsCssSelectors = Max;
    }

    /* ---------------------------------------------------------------------- */

    public static int GetDataExtractorsMaxItemsRegexes ()
    {
      return ( DataExtractorsMaxItemsRegexes );
    }

    public static void SetDataExtractorsMaxItemsRegexes ( int Max )
    {
      DataExtractorsMaxItemsRegexes = Max;
    }

    /* ---------------------------------------------------------------------- */

    public static int GetDataExtractorsMaxItemsXpaths ()
    {
      return ( DataExtractorsMaxItemsXpaths );
    }

    public static void SetDataExtractorsMaxItemsXpaths ( int Max )
    {
      DataExtractorsMaxItemsXpaths = Max;
    }

    /* ---------------------------------------------------------------------- */

    public static bool GetDataExtractorsApplyToHtml ()
    {
      return ( DataExtractorsApplyToHtml );
    }

    public static void SetDataExtractorsApplyToHtml ( bool State )
    {
      DataExtractorsApplyToHtml = State;
    }

    public static bool GetDataExtractorsApplyToCss ()
    {
      return ( DataExtractorsApplyToCss );
    }

    public static void SetDataExtractorsApplyToCss ( bool State )
    {
      DataExtractorsApplyToCss = State;
    }

    public static bool GetDataExtractorsApplyToJavascripts ()
    {
      return ( DataExtractorsApplyToJavascripts );
    }

    public static void SetDataExtractorsApplyToJavascripts ( bool State )
    {
      DataExtractorsApplyToJavascripts = State;
    }

    public static bool GetDataExtractorsApplyToText ()
    {
      return ( DataExtractorsApplyToText );
    }

    public static void SetDataExtractorsApplyToText ( bool State )
    {
      DataExtractorsApplyToText = State;
    }

    public static bool GetDataExtractorsApplyToPdf ()
    {
      return ( DataExtractorsApplyToPdf );
    }

    public static void SetDataExtractorsApplyToPdf ( bool State )
    {
      DataExtractorsApplyToPdf = State;
    }

    public static bool GetDataExtractorsApplyToXml ()
    {
      return ( DataExtractorsApplyToXml );
    }

    public static void SetDataExtractorsApplyToXml ( bool State )
    {
      DataExtractorsApplyToXml = State;
    }

    /** Export Options ********************************************************/

    public static bool GetSitemapIncludeLinkedPdfs ()
    {
      return ( SitemapIncludeLinkedPdfs );
    }

    public static void SetSitemapIncludeLinkedPdfs ( bool State )
    {
      SitemapIncludeLinkedPdfs = State;
    }

    /** Disregard Html5 Elements Settings *************************************/

    public static bool GetDisregardHtml5ElementNav ()
    {
      return ( DisregardHtml5ElementNav );
    }

    public static void SetDisregardHtml5ElementNav ( bool State )
    {
      DisregardHtml5ElementNav = State;
    }

    /** -------------------------------------------------------------------- **/

    public static bool GetDisregardHtml5ElementHeader ()
    {
      return ( DisregardHtml5ElementHeader );
    }

    public static void SetDisregardHtml5ElementHeader ( bool State )
    {
      DisregardHtml5ElementHeader = State;
    }

    /** -------------------------------------------------------------------- **/

    public static bool GetDisregardHtml5ElementFooter ()
    {
      return ( DisregardHtml5ElementFooter );
    }

    public static void SetDisregardHtml5ElementFooter ( bool State )
    {
      DisregardHtml5ElementFooter = State;
    }

    /** Ignore Errors Settings ************************************************/

    public static bool GetIgnoreErrors410 ()
    {
      return ( IgnoreErrors410 );
    }

    public static void SetIgnoreErrors410 ( bool State )
    {
      IgnoreErrors410 = State;
    }

    /** -------------------------------------------------------------------- **/

    public static bool GetIgnoreErrors451 ()
    {
      return ( IgnoreErrors451 );
    }

    public static void SetIgnoreErrors451 ( bool State )
    {
      IgnoreErrors451 = State;
    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    static void DebugMsg ( String Msg )
    {
      System.Diagnostics.Debug.WriteLine( Msg );
    }

    /**************************************************************************/

  }

}
