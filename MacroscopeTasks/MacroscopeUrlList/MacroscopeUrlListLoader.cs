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
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeUrlListLoader.
	/// </summary>

	public class MacroscopeUrlListLoader : Macroscope
	{

		/**************************************************************************/

		MacroscopeJobMaster JobMaster;
		string Path;
		List<string> UrlList;

		/**************************************************************************/

		public MacroscopeUrlListLoader ( MacroscopeJobMaster JobMaster, string Path )
		{
			this.JobMaster = JobMaster;
			this.Path = Path;
			this.UrlList = new List<string> ();
		}

		/**************************************************************************/

		public Boolean Execute ()
		{
			Boolean bSuccess = false;
			MacroscopeAllowedHosts AllowedHosts = this.JobMaster.GetAllowedHosts();

			this.CleanseList();

			if( this.UrlList.Count > 0 )
			{

			  this.JobMaster.SetRunTimeMode( RunTimeMode: MacroscopeConstants.RunTimeMode.LISTFILE );

				for( int i = 0 ; i < this.UrlList.Count ; i++ )
				{
					string sUrl = this.UrlList[ i ];
					AllowedHosts.AddFromUrl( sUrl );
					this.JobMaster.AddUrlQueueItem( sUrl );
				}

				bSuccess = true;
			}

			return( bSuccess );
		}

		/**************************************************************************/

		Boolean CleanseList ()
		{

			Boolean bSuccess = false;
			string [] saUrls = null;

			try
			{
				saUrls = File.ReadAllLines( this.Path );
			}
			catch( FileLoadException ex )
			{
				DebugMsg( string.Format( "FileLoadException: {0}", ex.Message ) );
			}

			if( ( saUrls != null ) && ( saUrls.Length > 0 ) )
			{
				for( int i = 0 ; i < saUrls.Length ; i++ )
				{
					string sUrl = saUrls[ i ];
					sUrl = Regex.Replace( sUrl, "^\\s+", "" );
					sUrl = Regex.Replace( sUrl, "\\s+$", "" );
					if( sUrl.Length > 0 )
					{
						if( Uri.IsWellFormedUriString( sUrl, UriKind.Absolute ) )
						{
							DebugMsg( string.Format( "CleanseList Adding: {0}", sUrl ) );
							this.UrlList.Add( sUrl );
						}
					}
				}
				bSuccess = true;
			}

			return( bSuccess );
		}

		/**************************************************************************/

		public string GetUrlListItem ( int Item )
		{
			string sUrl = null;
			try
			{
				if( this.UrlList[ Item ].Length > 0 )
				{
					sUrl = this.UrlList[ Item ];
				}
			}
			catch( Exception ex )
			{
				DebugMsg( string.Format( "GetUrlListItem: {0}", ex.Message ) );
			}
			return( sUrl );
		}

		/**************************************************************************/

	}

}
