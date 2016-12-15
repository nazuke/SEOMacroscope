using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using RobotsTxt;

namespace SEOMacroscope
{

	
	public class MacroscopeRobots
	{
	
		Hashtable htRobots;
		
		/**************************************************************************/

		public MacroscopeRobots ()
		{
			htRobots = new Hashtable (32);
		}

		/**************************************************************************/

		public Boolean apply_robot_rule( string sURL )
		{
			Boolean bAllowed = false;
			
			Uri uBase = new Uri (sURL, UriKind.Absolute);
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
				
			} catch (InvalidOperationException ex) {
				debug_msg( ex.Message );
			} catch (UriFormatException ex) {
				debug_msg( ex.Message );
			}
			
			if (sRobotsTxtURL != null) {
				
				Robots robot = null;
				
				if (this.htRobots.ContainsKey( sRobotsTxtURL )) {
					robot = (Robots)this.htRobots[ sRobotsTxtURL ];
				} else {
					try {
						using (WebClient wc = new WebClient ()) {
							String sRobotsText = wc.DownloadString( sRobotsTxtURL );
							robot = new Robots (sRobotsText);
							this.htRobots.Add( sRobotsTxtURL, robot );
						}
					} catch (Exception ex) {
						debug_msg( ex.Message );
					}					
				}
				
				if (robot != null) {
					if (uBase != null) {
						if (robot.IsPathAllowed( "*", uBase.AbsolutePath )) {
							bAllowed = true;
						} else {
							debug_msg( string.Format( "ROBOTS Disallowed: {0}", sURL ), 2 );
						}
					}

				}
		
			}
			
			return( bAllowed );
		}

		/**************************************************************************/
		
		void debug_msg( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		void debug_msg( String sMsg, int iOffset )
		{
			String sMsgPadded = new String (' ', iOffset * 2) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}

		/**************************************************************************/

		
	}
	
}
