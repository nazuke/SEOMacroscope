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

namespace SEOMacroscope
{

	public static class MacroscopePreferences
	{

		/**************************************************************************/
		
		static string HomeDirectory;
		static string PrefsDirectory;

		static int Depth;
		static int PageLimit;

		static Boolean SameSite;
		static Boolean ProbeHreflangs;

		static Boolean FollowRobotsProtocol;
		static Boolean FollowNoFollow;

		static Boolean FetchStylesheets;
		static Boolean FetchJavascripts;
		static Boolean FetchImages;
		static Boolean FetchPdfs;
		static Boolean FetchBinaries;

		/**************************************************************************/
		
		static MacroscopePreferences ()
		{

			Depth = -1;
			PageLimit = -1;

			SameSite = true;
			ProbeHreflangs = false;
			
			FollowRobotsProtocol = true;
			FollowNoFollow = true;

			FetchStylesheets = true;
			FetchJavascripts = false;
			FetchImages = false;
			FetchPdfs = false;

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
		}
		
		/**************************************************************************/
				
		public static string GetHomeDirectory ()
		{
			return( HomeDirectory );
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
