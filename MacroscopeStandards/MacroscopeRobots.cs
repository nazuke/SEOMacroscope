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
using System.Net;
using RobotsTxt;

namespace SEOMacroscope
{

	
	public class MacroscopeRobots : Macroscope
	{

		/**************************************************************************/

		Dictionary<string,Robots> dicRobots;

		/**************************************************************************/

		public MacroscopeRobots ()
		{
			dicRobots = new Dictionary<string,Robots>(32);
		}

		/**************************************************************************/

		public Boolean ApplyRobotRule ( string sUrl )
		{
			Boolean bAllowed = false;

			if( !MacroscopePreferencesManager.GetFollowRobotsProtocol() ) {
				DebugMsg( string.Format( "ROBOTS Disabled: {0}", sUrl ) );
				return( true );
			}

			Uri uBase = new Uri ( sUrl, UriKind.Absolute );
			Uri uNew = null;
			string sRobotsTxtUrl = null;
			
			try {
				uNew = new Uri (
					string.Format(
						"{0}://{1}{2}",
						uBase.Scheme,
						uBase.Host,
						"/robots.txt"
					),
					UriKind.Absolute
				);
			   	
				sRobotsTxtUrl = uNew.ToString();
				
			} catch( InvalidOperationException ex ) {
				DebugMsg( string.Format( "ApplyRobotRule: {0}", ex.Message ) );
			} catch( UriFormatException ex ) {
				DebugMsg( string.Format( "ApplyRobotRule: {0}", ex.Message ) );
			}
			
			if( sRobotsTxtUrl != null ) {
				
				Robots robot = null;
				
				if( this.dicRobots.ContainsKey( sRobotsTxtUrl ) ) {
					robot = this.dicRobots[ sRobotsTxtUrl ];
				} else {
					try {
						using( WebClient wc = new WebClient () ) {
							String sRobotsText = wc.DownloadString( sRobotsTxtUrl );
							robot = new Robots ( sRobotsText );
							this.dicRobots.Add( sRobotsTxtUrl, robot );
						}
					} catch( Exception ex ) {
						DebugMsg( string.Format( "ApplyRobotRule: {0}", ex.Message ) );
					}					
				}
				
				if( robot != null ) {
					if( uBase != null ) {
						if( robot.IsPathAllowed( "*", uBase.AbsolutePath ) ) {
							bAllowed = true;
						} else {
							DebugMsg( string.Format( "ROBOTS Disallowed: {0}", sUrl ) );
							DebugMsg( string.Format( "ROBOTS AbsolutePath: {0}", uBase.AbsolutePath ) );
						}
					}

				}
		
			}
			
			return( bAllowed );
		}

		/**************************************************************************/

	}
	
}
