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

			if( !MacroscopePreferences.GetFollowRobotsProtocol() ) {
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
				debug_msg( ex.Message );
			} catch( UriFormatException ex ) {
				debug_msg( ex.Message );
			}
			
			if( sRobotsTxtURL != null ) {
				
				Robots robot = null;
				
				if( this.htRobots.ContainsKey( sRobotsTxtURL ) ) {
					robot = ( Robots )this.htRobots[ sRobotsTxtURL ];
				} else {
					try {
						using( WebClient wc = new WebClient () ) {
							String sRobotsText = wc.DownloadString( sRobotsTxtURL );

							debug_msg( string.Format( "ROBOTS sRobotsText: {0}", sRobotsText ), 2 );

							robot = new Robots ( sRobotsText );
							this.htRobots.Add( sRobotsTxtURL, robot );
						}
					} catch( Exception ex ) {
						debug_msg( ex.Message );
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
