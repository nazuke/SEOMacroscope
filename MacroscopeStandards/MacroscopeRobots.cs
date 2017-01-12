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
using System.Collections;
using System.Collections.Generic;
using System.Net;
using RobotsTxt;

namespace SEOMacroscope
{

	
	public class MacroscopeRobots : Macroscope
	{
	
		Hashtable htRobots;
		
		/**************************************************************************/

		public MacroscopeRobots ()
		{
			htRobots = new Hashtable ( 32 );
		}

		/**************************************************************************/

		public Boolean ApplyRobotRule ( string sURL )
		{
			Boolean bAllowed = false;

			if( !MacroscopePreferencesManager.GetFollowRobotsProtocol() ) {
				debug_msg( string.Format( "ROBOTS Disabled: {0}", sURL ), 2 );
				return( true );
			}

			Uri uBase = new Uri ( sURL, UriKind.Absolute );
			Uri uNew = null;
			string sRobotsTxtURL = null;
			
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
			   	
				sRobotsTxtURL = uNew.ToString();
				
			} catch( InvalidOperationException ex ) {
				debug_msg( string.Format( "ApplyRobotRule: {0}", ex.Message ) );
			} catch( UriFormatException ex ) {
				debug_msg( string.Format( "ApplyRobotRule: {0}", ex.Message ) );
			}
			
			if( sRobotsTxtURL != null ) {
				
				Robots robot = null;
				
				if( this.htRobots.ContainsKey( sRobotsTxtURL ) ) {
					robot = ( Robots )this.htRobots[ sRobotsTxtURL ];
				} else {
					try {
						using( WebClient wc = new WebClient () ) {
							String sRobotsText = wc.DownloadString( sRobotsTxtURL );
							robot = new Robots ( sRobotsText );
							this.htRobots.Add( sRobotsTxtURL, robot );
						}
					} catch( Exception ex ) {
						debug_msg( string.Format( "ApplyRobotRule: {0}", ex.Message ) );
					}					
				}
				
				if( robot != null ) {
					if( uBase != null ) {
						if( robot.IsPathAllowed( "*", uBase.AbsolutePath ) ) {
							bAllowed = true;
						} else {
							debug_msg( string.Format( "ROBOTS Disallowed: {0}", sURL ), 2 );
							debug_msg( string.Format( "ROBOTS AbsolutePath: {0}", uBase.AbsolutePath ), 2 );
						}
					}

				}
		
			}
			
			return( bAllowed );
		}

		/**************************************************************************/

	}
	
}
