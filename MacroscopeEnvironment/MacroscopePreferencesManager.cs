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
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net;

namespace SEOMacroscope
{

	public static class MacroscopePreferencesManager
	{

		/**************************************************************************/

		static MacroscopePreferences Preferences;
		static WebProxy wpProxy = null;

		// WebProxy Options
		static string HttpProxyHost;
		static int HttpProxyPort;
		//static string HttpProxyUsername;
		//static string HttpProxyPassword;

		// Spidering Control
		static string StartUrl;
		static int MaxThreads;
		static int MaxFetchesPerWorker;
		static int Depth;
		static int PageLimit;
		static int RequestTimeout;
		static int MaxRetries;
		
		static Boolean CheckExternalLinks;
		
		static Boolean FollowRobotsProtocol;
		static Boolean FollowSitemapLinks;
		
		static Boolean FollowRedirects;
		static Boolean FollowNoFollow;
		static Boolean FollowCanonicalLinks;
		static Boolean FollowHrefLangLinks;
		static Boolean FollowListLinks;

		static Boolean FetchStylesheets;
		static Boolean FetchJavascripts;
		static Boolean FetchImages;
		static Boolean FetchPdfs;
		static Boolean FetchAudio;
		static Boolean FetchVideo;
		static Boolean FetchXml;
		static Boolean FetchBinaries;

		// Analysis Options
		static Boolean CheckHreflangs;
		static Boolean ScanSitesInList;
		static Boolean WarnAboutInsecureLinks;
		
		// SEO Options

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
		
		static Boolean AnalyzeKeywordsInText;

		/**************************************************************************/
		
		static MacroscopePreferencesManager ()
		{

			Preferences = new MacroscopePreferences ();

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

					HttpProxyHost = Preferences.HttpProxyHost;
					HttpProxyPort = Preferences.HttpProxyPort;

					StartUrl = Preferences.StartUrl;
			
					MaxThreads = Preferences.MaxThreads;
					MaxFetchesPerWorker = Preferences.MaxFetchesPerWorker;
					
					Depth = Preferences.Depth;
					PageLimit = Preferences.PageLimit;
					RequestTimeout = Preferences.RequestTimeout;
					MaxRetries = Preferences.MaxRetries;

					CheckExternalLinks = Preferences.CheckExternalLinks;
				
					CheckHreflangs = Preferences.CheckHreflangs;
					ScanSitesInList = Preferences.ScanSitesInList;
					WarnAboutInsecureLinks = Preferences.WarnAboutInsecureLinks;

					FollowRobotsProtocol = Preferences.FollowRobotsProtocol;
					FollowSitemapLinks = Preferences.FollowSitemapLinks;
					
					FollowRedirects = Preferences.FollowRedirects;			
					FollowNoFollow = Preferences.FollowNoFollow;
					FollowCanonicalLinks = Preferences.FollowCanonicalLinks;			
					FollowHrefLangLinks = Preferences.FollowHrefLangLinks;
					FollowListLinks = Preferences.FollowListLinks;
					
					FetchStylesheets = Preferences.FetchStylesheets;
					FetchJavascripts = Preferences.FetchJavascripts;
					FetchImages = Preferences.FetchImages;
					FetchPdfs = Preferences.FetchPdfs;
					FetchAudio = Preferences.FetchAudio;
					FetchVideo = Preferences.FetchVideo;
					FetchXml = Preferences.FetchXml;

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

				}

			}

			SanitizeValues();

			ConfigureHttpProxy();
			
			DebugMsg( string.Format( "MacroscopePreferencesManager StartUrl: \"{0}\"", StartUrl ) );
			DebugMsg( string.Format( "MacroscopePreferencesManager Depth: {0}", Depth ) );
			DebugMsg( string.Format( "MacroscopePreferencesManager PageLimit: {0}", PageLimit ) );

		}

		/**************************************************************************/

		static void SetDefaultValues ()
		{

			// WebProxy Options
			
			HttpProxyHost = "";
			HttpProxyPort = 0;

			// Spidering Control
			
			StartUrl = "";
			MaxThreads = 16;
			MaxFetchesPerWorker = 32;
			Depth = -1;
			PageLimit = -1;
			RequestTimeout = 30;
			MaxRetries = 0;
			
			CheckExternalLinks = false;

			FollowRobotsProtocol = true;
			FollowSitemapLinks = true;
			
			FollowRedirects = false;
			FollowNoFollow = true;
			FollowCanonicalLinks = true;			
			FollowHrefLangLinks = false;
			FollowListLinks = false;
						
			FetchStylesheets = true;
			FetchJavascripts = true;
			FetchImages = true;
			FetchPdfs = false;
			FetchAudio = false;
			FetchVideo = false;
			FetchXml = true;

			// Analysis Options
			
			CheckHreflangs = true;
			ScanSitesInList = false;
			WarnAboutInsecureLinks = false;

			// SEO Options
			
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

		}

		/**************************************************************************/
				
		static void SanitizeValues ()
		{

			if( StartUrl.Length > 0 )
			{
				StartUrl = Regex.Replace( StartUrl, "^\\s+", "" );
				StartUrl = Regex.Replace( StartUrl, "\\s+$", "" );
			}

			if( Depth <= 0 )
			{
				Depth = -1;
			}

			if( PageLimit <= 0 )
			{
				PageLimit = -1;
			}

			if( RequestTimeout <= 10 )
			{
				RequestTimeout = 10;
			}
			else
			if( RequestTimeout >= 50 )
			{
				RequestTimeout = 50;
			}

			if( MaxRetries <= 0 )
			{
				MaxRetries = 0;
			}
			else
			if( MaxRetries > 10 )
			{
				MaxRetries = 10;
			}
			
			SavePreferences();
			
		}

		/**************************************************************************/
		
		public static void SavePreferences ()
		{

			if( Preferences != null )
			{
				
				Preferences.HttpProxyHost = HttpProxyHost;
				Preferences.HttpProxyPort = HttpProxyPort;

				Preferences.StartUrl = StartUrl;
			
				Preferences.MaxThreads = MaxThreads;
				Preferences.MaxFetchesPerWorker = MaxFetchesPerWorker;

				Preferences.Depth = Depth;
				Preferences.PageLimit = PageLimit;
				Preferences.RequestTimeout = RequestTimeout;
				Preferences.MaxRetries = MaxRetries;

				Preferences.CheckExternalLinks = CheckExternalLinks;
				
				Preferences.CheckHreflangs = CheckHreflangs;
				Preferences.ScanSitesInList = ScanSitesInList;
				Preferences.WarnAboutInsecureLinks = WarnAboutInsecureLinks;

				Preferences.FollowRobotsProtocol = FollowRobotsProtocol;
				Preferences.FollowSitemapLinks = FollowSitemapLinks;
				
				Preferences.FollowRedirects = FollowRedirects;
				Preferences.FollowNoFollow = FollowNoFollow;
				Preferences.FollowCanonicalLinks = FollowCanonicalLinks;			
				Preferences.FollowHrefLangLinks = FollowHrefLangLinks;
				Preferences.FollowListLinks = FollowListLinks;
								
				Preferences.FetchStylesheets = FetchStylesheets;
				Preferences.FetchJavascripts = FetchJavascripts;
				Preferences.FetchImages = FetchImages;
				Preferences.FetchPdfs = FetchPdfs;
				Preferences.FetchAudio = FetchAudio;
				Preferences.FetchVideo = FetchVideo;
				Preferences.FetchXml = FetchXml;

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

				Preferences.Save();

			}

		}

		/** HTTP Proxy ************************************************************/

		public static string GetHttpProxyHost ()
		{
			return( HttpProxyHost );
		}
		
		public static void SetHttpProxyHost ( string sValue )
		{
			HttpProxyHost = sValue;
		}

		public static int GetHttpProxyPort ()
		{
			return( HttpProxyPort );
		}
		
		public static void SetHttpProxyPort ( int iValue )
		{
			HttpProxyPort = iValue;
		}

		public static void ConfigureHttpProxy ()
		{
			
			string sHttpProxyHost;
			int iHttpProxyPort;

			if( HttpProxyHost.Length > 0 )
			{

				sHttpProxyHost = HttpProxyHost;

				if( HttpProxyPort >= 0 )
				{
					iHttpProxyPort = HttpProxyPort;
				}
				else
				{
					iHttpProxyPort = 80;
				}

				DebugMsg( string.Format( "ConfigureHttpProxy: {0}:{1}", HttpProxyHost, HttpProxyPort ) );
				
				wpProxy = new WebProxy ( sHttpProxyHost, iHttpProxyPort );

			}
			else
			{
				
				DebugMsg( string.Format( "ConfigureHttpProxy: NOT USED" ) );
				
				wpProxy = null;
				
			}

		}

		public static WebProxy GetHttpProxy ()
		{
			return( wpProxy );
		}
	
		public static void EnableHttpProxy ( WebRequest req )
		{
			if( wpProxy != null )
			{
				req.Proxy = wpProxy;
			}
		}
				
		/**************************************************************************/

		public static string GetStartUrl ()
		{
			return( StartUrl );
		}
		
		public static void SetStartUrl ( string sStartUrl )
		{
			StartUrl = sStartUrl;
		}
		
		/**************************************************************************/
				
		public static int GetMaxThreads ()
		{
			return( MaxThreads );
		}
		
		public static void SetMaxThreads ( int iMaxThreads )
		{
			MaxThreads = iMaxThreads;
		}

		/**************************************************************************/
				
		public static int GetMaxFetchesPerWorker ()
		{
			return( MaxFetchesPerWorker );
		}
		
		public static void SetMaxFetchesPerWorker ( int iMaxFetchesPerWorker )
		{
			MaxFetchesPerWorker = iMaxFetchesPerWorker;
		}

		/**************************************************************************/

		public static int GetDepth ()
		{
			return( Depth );
		}
		
		public static void SetDepth ( int iValue )
		{
			Depth = iValue;
		}
		
		/**************************************************************************/
		
		public static int GetPageLimit ()
		{
			return( PageLimit );
		}
		
		public static void SetPageLimit ( int iValue )
		{
			PageLimit = iValue;
		}

		/** Request Timeout *******************************************************/

		public static int GetRequestTimeout ()
		{
			return( RequestTimeout );
		}
		
		public static void SetRequestTimeout ( int iValue )
		{
			RequestTimeout = iValue;
		}

		/** Maximum Retries *******************************************************/

		public static int GetMaxRetries ()
		{
			return( MaxRetries );
		}
		
		public static void SetMaxRetries ( int iValue )
		{
			MaxRetries = iValue;
		}

		/** Domain Spidering Controls *********************************************/

		public static void SetCheckExternalLinks ( Boolean bValue )
		{
			CheckExternalLinks = bValue;
		}

		public static Boolean GetCheckExternalLinks ()
		{
			return( CheckExternalLinks );
		}

		/**************************************************************************/
		
		public static Boolean GetCheckHreflangs ()
		{
			return( CheckHreflangs );
		}
		
		public static void SetCheckHreflangs ( Boolean bValue )
		{
			CheckHreflangs = bValue;
		}

		/**************************************************************************/
		
		public static Boolean GetScanSitesInList ()
		{
			return( ScanSitesInList );
		}
		
		public static void SetScanSitesInList ( Boolean bValue )
		{
			ScanSitesInList = bValue;
		}

		/**************************************************************************/
				
		public static Boolean GetWarnAboutInsecureLinks ()
		{
			return( WarnAboutInsecureLinks );
		}
		
		public static void SetWarnAboutInsecureLinks ( Boolean bValue )
		{
			WarnAboutInsecureLinks = bValue;
		}

		/**************************************************************************/
		
		public static Boolean GetFollowRobotsProtocol ()
		{
			return( FollowRobotsProtocol );
		}

		public static void SetFollowRobotsProtocol ( Boolean bState )
		{
			FollowRobotsProtocol = bState;
		}

		/**************************************************************************/
		
		public static Boolean GetFollowSitemapLinks ()
		{
			return( FollowSitemapLinks );
		}

		public static void SetFollowSitemapLinks ( Boolean bState )
		{
			FollowSitemapLinks = bState;
		}

		/**************************************************************************/
		public static Boolean GetFollowRedirects ()
		{
			return( FollowRedirects );
		}

		public static void SetFollowRedirects ( Boolean bState )
		{
			FollowRedirects = bState;
		}
		
		/**************************************************************************/

		public static Boolean GetFollowNoFollow ()
		{
			return( FollowNoFollow );
		}

		public static void SetFollowNoFollow ( Boolean bState )
		{
			FollowNoFollow = bState;
		}

		/**************************************************************************/
		
		public static Boolean GetFollowCanonicalLinks ()
		{
			return( FollowCanonicalLinks );
		}

		public static void SetFollowCanonicalLinks ( Boolean bState )
		{
			FollowCanonicalLinks = bState;
		}
		
		/**************************************************************************/

		public static Boolean GetFollowHrefLangLinks ()
		{
			return( FollowHrefLangLinks );
		}

		public static void SetFollowHrefLangLinks ( Boolean bState )
		{
			FollowHrefLangLinks = bState;
		}

		/**************************************************************************/
				
		public static Boolean GetFollowListLinks ()
		{
			return( FollowListLinks );
		}

		public static void SetFollowListLinks ( Boolean bState )
		{
			FollowListLinks = bState;
		}

		/**************************************************************************/
		
		public static Boolean GetFetchStylesheets ()
		{
			return( FetchStylesheets );
		}
		
		public static void SetFetchStylesheets ( Boolean bState )
		{
			FetchStylesheets = bState;
		}

		/**************************************************************************/
				
		public static Boolean GetFetchJavascripts ()
		{
			return( FetchJavascripts );
		}
		
		public static void SetFetchJavascripts ( Boolean bState )
		{
			FetchJavascripts = bState;
		}

		/**************************************************************************/
				
		public static Boolean GetFetchImages ()
		{
			return( FetchImages );
		}
		
		public static void SetFetchImages ( Boolean bState )
		{
			FetchImages = bState;
		}

		/**************************************************************************/
				
		public static Boolean GetFetchPdfs ()
		{
			return( FetchPdfs );
		}
		
		public static void SetFetchPdfs ( Boolean bState )
		{
			FetchPdfs = bState;
		}
		
		/**************************************************************************/
		
		public static Boolean GetFetchAudio ()
		{
			return( FetchAudio );
		}
		
		public static void SetFetchAudio ( Boolean bState )
		{
			FetchAudio = bState;
		}

		/**************************************************************************/
		
		public static Boolean GetFetchVideo ()
		{
			return( FetchVideo );
		}
		
		public static void SetFetchVideo ( Boolean bState )
		{
			FetchVideo = bState;
		}

		/**************************************************************************/
		
		public static Boolean GetFetchXml ()
		{
			return( FetchXml );
		}
		
		public static void SetFetchXml ( Boolean bState )
		{
			FetchXml = bState;
		}

		/**************************************************************************/
				
		public static Boolean GetFetchBinaries ()
		{
			return( FetchBinaries );
		}
		
		public static void SetFetchBinaries ( Boolean bState )
		{
			FetchBinaries = bState;
		}

		/** SEO Options ***********************************************************/

		public static int GetTitleMinLen ()
		{
			return( TitleMinLen );
		}
		
		public static void SetTitleMinLen ( int iValue )
		{
			TitleMinLen = iValue;
		}

		public static int GetTitleMaxLen ()
		{
			return( TitleMaxLen );
		}
		
		public static void SetTitleMaxLen ( int iValue )
		{
			TitleMaxLen = iValue;
		}

		public static int GetTitleMinWords ()
		{
			return( TitleMinWords );
		}
		
		public static void SetTitleMinWords ( int iValue )
		{
			TitleMinWords = iValue;
		}

		public static int GetTitleMaxWords ()
		{
			return( TitleMaxWords );
		}
		
		public static void SetTitleMaxWords ( int iValue )
		{
			TitleMaxWords = iValue;
		}

		public static int GetTitleMaxPixelWidth ()
		{
			return( TitleMaxPixelWidth );
		}
		
		public static void SetTitleMaxPixelWidth ( int iValue )
		{
			TitleMaxPixelWidth = iValue;
		}

		/* ---------------------------------------------------------------------- */
		
		public static int GetDescriptionMinLen ()
		{
			return( DescriptionMinLen );
		}
		
		public static void SetDescriptionMinLen ( int iValue )
		{
			DescriptionMinLen = iValue;
		}

		public static int GetDescriptionMaxLen ()
		{
			return( DescriptionMaxLen );
		}
		
		public static void SetDescriptionMaxLen ( int iValue )
		{
			DescriptionMaxLen = iValue;
		}

		public static int GetDescriptionMinWords ()
		{
			return( DescriptionMinWords );
		}
		
		public static void SetDescriptionMinWords ( int iValue )
		{
			DescriptionMinWords = iValue;
		}

		public static int GetDescriptionMaxWords ()
		{
			return( DescriptionMaxWords );
		}
		
		public static void SetDescriptionMaxWords ( int iValue )
		{
			DescriptionMaxWords = iValue;
		}

		public static ushort GetMaxHeadingDepth ()
		{
			return( MaxHeadingDepth );
		}
		
		public static void SetMaxHeadingDepth ( ushort iValue )
		{
			MaxHeadingDepth = iValue;
		}

		public static Boolean GetAnalyzeKeywordsInText ()
		{
			return( AnalyzeKeywordsInText );
		}
		
		public static void SetAnalyzeKeywordsInText ( Boolean bState )
		{
			AnalyzeKeywordsInText = bState;
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
