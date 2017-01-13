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
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace SEOMacroscope
{

	public static class MacroscopePreferencesManager
	{

		/**************************************************************************/

		static MacroscopePreferences Preferences;
		static WebProxy wpProxy = null;
		
		static string HomeDirectory;
		static string PrefsDirectory;

		// WebProxy Options
		static string HttpProxyHost;
		static int HttpProxyPort;
		//static string HttpProxyUsername;
		//static string HttpProxyPassword;

		static string HttpsProxyHost;
		static int HttpsProxyPort;
		//static string HttpsProxyUsername;
		//static string HttpsProxyPassword;

		// Spidering Control
		static string StartUrl;
		static int Depth;
		static int PageLimit;
		static Boolean SameSite;
		static Boolean FollowRobotsProtocol;
		static Boolean FollowNoFollow;
		static Boolean FetchStylesheets;
		static Boolean FetchJavascripts;
		static Boolean FetchImages;
		static Boolean FetchPdfs;
		static Boolean FetchBinaries;

		// Analysis Options
		static Boolean ProbeHreflangs;
		
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
					HttpsProxyHost = Preferences.HttpsProxyHost;
					HttpsProxyPort = Preferences.HttpsProxyPort;

					StartUrl = Preferences.StartUrl;
			
					Depth = Preferences.Depth;
					PageLimit = Preferences.PageLimit;

					SameSite = Preferences.SameSite;
					ProbeHreflangs = Preferences.ProbeHreflangs;
			
					FollowRobotsProtocol = Preferences.FollowRobotsProtocol;
					FollowNoFollow = Preferences.FollowNoFollow;

					FetchStylesheets = Preferences.FetchStylesheets;
					FetchJavascripts = Preferences.FetchJavascripts;
					FetchImages = Preferences.FetchImages;
					FetchPdfs = Preferences.FetchPdfs;

				}

			}

			SanitizeValues();

			ConfigureHttpProxy();
			
			debug_msg( string.Format( "MacroscopePreferencesManager StartUrl: \"{0}\"", StartUrl ) );
			debug_msg( string.Format( "MacroscopePreferencesManager Depth: {0}", Depth ) );
			debug_msg( string.Format( "MacroscopePreferencesManager PageLimit: {0}", PageLimit ) );

		}

		/**************************************************************************/

		static void SetDefaultValues ()
		{

			// WebProxy Options
			HttpProxyHost = "";
			HttpProxyPort = 0;
			HttpsProxyHost = "";
			HttpsProxyPort = 0;

			// Spidering Control
			StartUrl = "";
			Depth = -1;
			PageLimit = -1;
			SameSite = true;
			FollowRobotsProtocol = true;
			FollowNoFollow = true;
			FetchStylesheets = true;
			FetchJavascripts = true;
			FetchImages = true;
			FetchPdfs = false;

			// Analysis Options
			ProbeHreflangs = true;
			
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

		static void ConfigureHttpProxy ()
		{
			
			string sHttpProxyHost;
			int iHttpProxyPort;

			if( HttpProxyHost.Length > 0 ) {

				sHttpProxyHost = HttpProxyHost;

				if( HttpProxyPort > 0 ) {
					iHttpProxyPort = HttpProxyPort;
				} else {
					iHttpProxyPort = 80;
				}

				wpProxy = new WebProxy ( sHttpProxyHost, iHttpProxyPort );

			}

		}

		/**************************************************************************/
		
		public static void LoadPreferences ()
		{
			
			{
				string sHomeDir = null;
				if( Environment.OSVersion.Platform == PlatformID.Unix ) {
					sHomeDir = Environment.GetEnvironmentVariable( "HOME" );
				} else {
					sHomeDir = Environment.ExpandEnvironmentVariables( "%HOMEDRIVE%%HOMEPATH%" );
				}
				if( sHomeDir == null ) {
					throw new Exception ( "Cannot determine home directory." );
				} else {
					HomeDirectory = sHomeDir;
				}
				debug_msg( string.Format( "HomeDirectory: {0}", HomeDirectory ) );
			}
			
			{
				PrefsDirectory = string.Join( Path.DirectorySeparatorChar.ToString(), HomeDirectory, ".seomacroscope" );
				debug_msg( string.Format( "PrefsDirectory: {0}", PrefsDirectory ) );
				if( Directory.Exists( PrefsDirectory ) ) {
					debug_msg( string.Format( "PrefsDirectory Exists: {0}", PrefsDirectory ) );
				} else {
					debug_msg( string.Format( "PrefsDirectory Not Exists: {0}", PrefsDirectory ) );
					try {
						DirectoryInfo diPrefs = Directory.CreateDirectory( PrefsDirectory );
						if( Directory.Exists( PrefsDirectory ) ) {
							diPrefs.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
						}
					} catch( IOException ) {
						throw new Exception ( "Cannot create preferences directory." );
					}
				}
			}

		}
				
		/**************************************************************************/
		
		public static void SavePreferences ()
		{

			if( Preferences != null ) {
				
				Preferences.HttpProxyHost = HttpProxyHost;
				Preferences.HttpProxyPort = HttpProxyPort;
				Preferences.HttpsProxyHost = HttpsProxyHost;
				Preferences.HttpsProxyPort = HttpsProxyPort;

				Preferences.StartUrl = StartUrl;
			
				Preferences.Depth = Depth;
				Preferences.PageLimit = PageLimit;

				Preferences.SameSite = SameSite;
				Preferences.ProbeHreflangs = ProbeHreflangs;
			
				Preferences.FollowRobotsProtocol = FollowRobotsProtocol;
				Preferences.FollowNoFollow = FollowNoFollow;

				Preferences.FetchStylesheets = FetchStylesheets;
				Preferences.FetchJavascripts = FetchJavascripts;
				Preferences.FetchImages = FetchImages;
				Preferences.FetchPdfs = FetchPdfs;
				
				Preferences.Save();

			}

		}
		
		/**************************************************************************/
				
		public static string GetHomeDirectory ()
		{
			return( HomeDirectory );
		}

		/**************************************************************************/

		public static WebProxy GetHttpProxy ()
		{
			return( wpProxy );
		}

		/**************************************************************************/
				
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

		public static int GetDepth ()
		{
			return( Depth );
		}
		
		/**************************************************************************/
		
		public static int GetPageLimit ()
		{
			return( PageLimit );
		}
		
		/**************************************************************************/
		
		public static Boolean GetSameSite ()
		{
			return( SameSite );
		}
		
		/**************************************************************************/
		
		public static Boolean GetProbeHreflangs ()
		{
			return( ProbeHreflangs );
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
		
		public static Boolean GetFollowNoFollow ()
		{
			return( FollowNoFollow );
		}

		public static void SetFollowNoFollow ( Boolean bState )
		{
			FollowNoFollow = bState;
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

		/**************************************************************************/
		
		static void debug_msg ( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		static void debug_msg ( String sMsg, int iOffset )
		{
			String sMsgPadded = new String ( ' ', iOffset * 2 ) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}

		/**************************************************************************/

	}

}
