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

namespace SEOMacroscope
{

	public class MacroscopeJobWorker : Macroscope
	{

		/**************************************************************************/

		readonly MacroscopeJobMaster msJobMaster;
		
		/**************************************************************************/

		public MacroscopeJobWorker ( MacroscopeJobMaster msJobMasterNew )
		{
			SuppressDebugMsg = true;
			msJobMaster = msJobMasterNew;
		}

		/**************************************************************************/
		
		public void Execute ( string sURL )
		{
			//Thread.Sleep( 100 );
			this.Fetch( sURL );
			msJobMaster.WorkersNotifyDone( sURL );
		}

		/**************************************************************************/

		void Fetch ( string sURL )
		{

			MacroscopeDocument msDoc = new MacroscopeDocument ( sURL );
			MacroscopeDocumentCollection DocCollection = this.msJobMaster.DocCollectionGet();
			MacroscopeAllowedHosts msAllowedHosts = this.msJobMaster.GetAllowedHosts();

			if( !this.msJobMaster.RobotsGet().ApplyRobotRule( sURL ) ) {
				DebugMsg( string.Format( "Disallowed by robots.txt: {0}", sURL ) );
				return;
			}

			this.msJobMaster.HistoryAdd( sURL );

			if( DocCollection.Contains( sURL ) ) {
				return;
			} else {
				DocCollection.Add( sURL, msDoc );
			}

			if( this.msJobMaster.GetDepth() > 0 ) {
				if( msDoc.GetDepth() > this.msJobMaster.GetDepth() ) {
					//DebugMsg( string.Format( "TOO DEEP: {0}", msDoc.depth ), 3 );
					DocCollection.Remove( sURL );
					return;
				}
			}

			if( this.msJobMaster.GetProbeHrefLangs() ) {
				msDoc.ProbeHrefLangs = true;
			}

			if( msDoc.Execute() ) {
			
				this.msJobMaster.IncPageLimitCount();

				if( msDoc.GetIsRedirect() ) {
					DebugMsg( string.Format( "Redirect Discovered: {0}", msDoc.GetUrlRedirectTo() ) );
					this.msJobMaster.UrlQueueAdd( msDoc.GetUrlRedirectTo() );
				}

				{
					string sLocale = msDoc.GetLocale();
					Dictionary<string,MacroscopeHrefLang> htHrefLangs = msDoc.GetHrefLangs();
					if( sLocale != null ) {
						this.msJobMaster.AddLocales( sLocale );
					}
					foreach( string sKeyLocale in htHrefLangs.Keys ) {
						this.msJobMaster.AddLocales( sKeyLocale );
						{
							string sHrefLangUrl = htHrefLangs[ sKeyLocale ].GetUrl();
							if( ( sHrefLangUrl != null ) && ( sHrefLangUrl.Length > 0 ) ) {
								if( !this.msJobMaster.HistorySeen( sHrefLangUrl ) ) {
									msAllowedHosts.AddFromUrl( sHrefLangUrl );
									this.msJobMaster.UrlQueueAdd( sHrefLangUrl );
								}
							}
						}
					}
				}

				if( MacroscopePreferencesManager.GetFollowCanonicalLinks() ) {
					string sCanonicalUrl = msDoc.GetCanonical();
					if( ( sCanonicalUrl != null ) && ( sCanonicalUrl.Length > 0 ) ) {
						if( !this.msJobMaster.HistorySeen( sCanonicalUrl ) ) {
							msAllowedHosts.AddFromUrl( sCanonicalUrl );
							this.msJobMaster.UrlQueueAdd( sCanonicalUrl );
						}
					}
				}

				Dictionary<string,string> htOutlinks = msDoc.GetOutlinks();

				foreach( string sOutlinkKey in htOutlinks.Keys ) {
					
					string sOutlinkURL = htOutlinks[ sOutlinkKey ];

					if( sOutlinkURL != null ) {

						Boolean bProceed = true;

						if( this.msJobMaster.GetPageLimit() < 0 ) {

							bProceed = true;

						} else if( this.msJobMaster.GetPageLimit() > -1 ) {
						
							if( this.msJobMaster.GetPageLimitCount() >= this.msJobMaster.GetPageLimit() ) {
								DebugMsg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", this.msJobMaster.GetPageLimit(), this.msJobMaster.GetPageLimitCount() ) );
								bProceed = false;
							}
							
						}
						
						if( bProceed ) {
							
							if( !this.msJobMaster.HistorySeen( sOutlinkURL ) ) {
								if( msAllowedHosts.IsAllowedFromUrl( sOutlinkURL ) ) {
									this.msJobMaster.UrlQueueAdd( sOutlinkURL );
								} else {
									DebugMsg( string.Format( "FOREIGN HOST: {0}", sOutlinkURL ) );
								}
							}
							
						} else {
							break;
						}

					}

				}

			} else {
				DebugMsg( string.Format( "EXECUTE FAILED: {0}", sURL ) );
			}

		}

		/**************************************************************************/

	}
	
}
