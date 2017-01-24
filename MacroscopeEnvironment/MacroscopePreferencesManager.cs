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
		
		static Boolean SameSite;
		
		static Boolean FollowRobotsProtocol;
		static Boolean FollowRedirects;
		static Boolean FollowNoFollow;
		static Boolean FollowCanonicalLinks;
		static Boolean FollowHrefLangLinks;

		static Boolean FetchStylesheets;
		static Boolean FetchJavascripts;
		static Boolean FetchImages;
		static Boolean FetchPdfs;
		static Boolean FetchBinaries;

		// Analysis Options
		static Boolean CheckHreflangs;
		
		// SEO Options
		static int TitleMinLen;
		static int TitleMaxLen;
		static int TitleMinWords;
		static int TitleMaxWords;
		static int DescriptionMinLen;
		static int DescriptionMaxLen;
		static int DescriptionMinWords;
		static int DescriptionMaxWords;

		/**************************************************************************/
		
		static MacroscopePreferencesManager ()
		{

			Preferences = new MacroscopePreferences ();

			SetDefaultValues();

			if( Preferences != null ) {

				if( Preferences.FirstRun == true ) {

					SetDefaultValues();
					Preferences.FirstRun = false;
					Preferences.Save();
				
				} else {

					HttpProxyHost = Preferences.HttpProxyHost;
					HttpProxyPort = Preferences.HttpProxyPort;

					StartUrl = Preferences.StartUrl;
			
					MaxThreads = Preferences.MaxThreads;
					MaxFetchesPerWorker = Preferences.MaxFetchesPerWorker;
					
					Depth = Preferences.Depth;
					PageLimit = Preferences.PageLimit;

					SameSite = Preferences.SameSite;
					CheckHreflangs = Preferences.CheckHreflangs;
			
					FollowRobotsProtocol = Preferences.FollowRobotsProtocol;
					FollowRedirects = Preferences.FollowRedirects;			
					FollowNoFollow = Preferences.FollowNoFollow;
					FollowCanonicalLinks = Preferences.FollowCanonicalLinks;			
					FollowHrefLangLinks = Preferences.FollowHrefLangLinks;
	
					FetchStylesheets = Preferences.FetchStylesheets;
					FetchJavascripts = Preferences.FetchJavascripts;
					FetchImages = Preferences.FetchImages;
					FetchPdfs = Preferences.FetchPdfs;

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
			
			SameSite = true;
			
			FollowRobotsProtocol = true;
			FollowRedirects = false;
			FollowNoFollow = true;
			FollowCanonicalLinks = true;			
			FollowHrefLangLinks = false;

			FetchStylesheets = true;
			FetchJavascripts = true;
			FetchImages = true;
			FetchPdfs = false;

			// Analysis Options
			CheckHreflangs = true;
			
			// SEO Options
			TitleMinLen = 10;
			TitleMaxLen = 70;
			TitleMinWords = 3;
			TitleMaxWords = 10;
			DescriptionMinLen = 10;
			DescriptionMaxLen = 100;
			DescriptionMinWords = 3;
			DescriptionMaxWords = 10;

		}

		/**************************************************************************/
				
		static void SanitizeValues ()
		{

			if( StartUrl.Length > 0 ) {
				StartUrl = Regex.Replace( StartUrl, "^\\s+", "" );
				StartUrl = Regex.Replace( StartUrl, "\\s+$", "" );
			}

			if( Depth <= 0 ) {
				Depth = -1;
			}

			if( PageLimit <= 0 ) {
				PageLimit = -1;
			}

			SavePreferences();
			
		}

		/**************************************************************************/
		
		public static void SavePreferences ()
		{

			if( Preferences != null ) {
				
				Preferences.HttpProxyHost = HttpProxyHost;
				Preferences.HttpProxyPort = HttpProxyPort;

				Preferences.StartUrl = StartUrl;
			
				Preferences.MaxThreads = MaxThreads;
				Preferences.MaxFetchesPerWorker = MaxFetchesPerWorker;

				Preferences.Depth = Depth;
				Preferences.PageLimit = PageLimit;

				Preferences.SameSite = SameSite;
				Preferences.CheckHreflangs = CheckHreflangs;
			
				Preferences.FollowRobotsProtocol = FollowRobotsProtocol;
				Preferences.FollowRedirects = FollowRedirects;
				Preferences.FollowNoFollow = FollowNoFollow;
				Preferences.FollowCanonicalLinks = FollowCanonicalLinks;			
				Preferences.FollowHrefLangLinks = FollowHrefLangLinks;

				Preferences.FetchStylesheets = FetchStylesheets;
				Preferences.FetchJavascripts = FetchJavascripts;
				Preferences.FetchImages = FetchImages;
				Preferences.FetchPdfs = FetchPdfs;
				
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

			if( HttpProxyHost.Length > 0 ) {

				sHttpProxyHost = HttpProxyHost;

				if( HttpProxyPort >= 0 ) {
					iHttpProxyPort = HttpProxyPort;
				} else {
					iHttpProxyPort = 80;
				}

				DebugMsg( string.Format( "ConfigureHttpProxy: {0}:{1}", HttpProxyHost, HttpProxyPort ) );
				
				wpProxy = new WebProxy ( sHttpProxyHost, iHttpProxyPort );

			} else {
				
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
			if( wpProxy != null ) {
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

		/**************************************************************************/
		
		public static Boolean GetSameSite ()
		{
			return( SameSite );
		}
		
		public static void SetSameSite ( Boolean bValue )
		{
			SameSite = bValue;
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
		
		public static Boolean GetFollowRobotsProtocol ()
		{
			return( FollowRobotsProtocol );
		}

		public static void SetFollowRobotsProtocol ( Boolean bState )
		{
			FollowRobotsProtocol = bState;
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

		/**************************************************************************/
		
		[Conditional( "DEVMODE" )]
		static void DebugMsg ( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		/**************************************************************************/

	}

}
