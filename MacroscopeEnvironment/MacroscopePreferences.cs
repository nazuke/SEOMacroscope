using System;
using System.IO;

namespace SEOMacroscope
{

	public static class MacroscopePreferences
	{

		/**************************************************************************/
		
		static string HomeDirectory;
		static string PrefsDirectory;

		static uint Depth;
		static int PageLimit;
		static Boolean SameSite;
		static Boolean ProbeHreflangs;

		/**************************************************************************/
		
		static MacroscopePreferences()
		{
			Depth = 10;
			PageLimit = -1;
			SameSite = true;
			ProbeHreflangs = false;
		}
		
		/**************************************************************************/
		
		public static void LoadPreferences()
		{
			
			{
				string sHomeDir = null;
				if (Environment.OSVersion.Platform == PlatformID.Unix) {
					sHomeDir = Environment.GetEnvironmentVariable("HOME");
				} else {
					sHomeDir = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
				}
				if (sHomeDir == null) {
					throw new Exception("Cannot determine home directory.");
				} else {
					HomeDirectory = sHomeDir;
				}
				debug_msg(string.Format("HomeDirectory: {0}", HomeDirectory));
			}
			
			{
				PrefsDirectory = string.Join(Path.DirectorySeparatorChar.ToString(), HomeDirectory, ".seomacroscope");
				debug_msg(string.Format("PrefsDirectory: {0}", PrefsDirectory));
				if (Directory.Exists(PrefsDirectory)) {
					debug_msg(string.Format("PrefsDirectory Exists: {0}", PrefsDirectory));
				} else {
					debug_msg(string.Format("PrefsDirectory Not Exists: {0}", PrefsDirectory));
					try {
						DirectoryInfo diPrefs = Directory.CreateDirectory(PrefsDirectory);
						if (Directory.Exists(PrefsDirectory)) {
							diPrefs.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
						}
					} catch (IOException) {
						throw new Exception("Cannot create preferences directory.");
					}
				}
			}

		}
				
		/**************************************************************************/
		
		public static void SavePreferences()
		{
				
		}
		
		/**************************************************************************/
				
		public static string GetHomeDirectory()
		{
			return(HomeDirectory);
		}

		/**************************************************************************/
		
		public static uint GetDepth()
		{
			return(Depth);
		}
		
		/**************************************************************************/
		
		public static int GetPageLimit()
		{
			return(PageLimit);
		}
		
		/**************************************************************************/
		
		public static Boolean GetSameSite()
		{
			return(SameSite);
		}
		
		/**************************************************************************/
		
		public static Boolean GetProbeHreflangs()
		{
			return(ProbeHreflangs);
		}

		/**************************************************************************/
		
		static void debug_msg(String sMsg)
		{
			System.Diagnostics.Debug.WriteLine(sMsg);
		}

		static void debug_msg(String sMsg, int iOffset)
		{
			String sMsgPadded = new String(' ', iOffset * 2) + sMsg;
			System.Diagnostics.Debug.WriteLine(sMsgPadded);
		}

		/**************************************************************************/

	}

}
