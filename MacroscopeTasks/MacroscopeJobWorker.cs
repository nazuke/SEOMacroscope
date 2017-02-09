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
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeJobWorker : Macroscope
	{

		/**************************************************************************/

		MacroscopeJobMaster JobMaster;
		MacroscopeDocumentCollection DocCollection;
		MacroscopeAllowedHosts AllowedHosts;
		MacroscopeIncludeExcludeUrls IncludeExcludeUrls;
		
		/**************************************************************************/

		public MacroscopeJobWorker ( MacroscopeJobMaster JobMasterNew )
		{
			
			SuppressDebugMsg = false;
			
			JobMaster = JobMasterNew;
			
			DocCollection = JobMaster.GetDocCollection();
			AllowedHosts = JobMaster.GetAllowedHosts();

			IncludeExcludeUrls = JobMaster.GetIncludeExcludeUrls();

		}

		/**************************************************************************/

		public void Execute ()
		{
					
			int iMaxFetches = MacroscopePreferencesManager.GetMaxFetchesPerWorker();
			
			while( iMaxFetches > 0 )
			{

				if( JobMaster.GetThreadsStop() )
				{

					DebugMsg( string.Format( "JobMaster.GetThreadsStop: {0}", JobMaster.GetThreadsStop() ) );
					break;
					
				}
				else
				{
				
					string sUrl = this.JobMaster.GetUrlQueueItem();

					if( sUrl != null )
					{
						if( !this.CheckIncludeExcludeUrl( sUrl ) )
						{
							sUrl = null;
						}
					}

					if( sUrl != null )
					{

						DebugMsg( string.Format( "Execute: {0}", sUrl ) );

						int iTries = MacroscopePreferencesManager.GetMaxRetries();

						do
						{
							if( this.Fetch( sUrl ) )
							{
								JobMaster.NotifyWorkersFetched( sUrl );
								break;
							}
							else
							{
								DebugMsg( string.Format( "Fetch Failed: {0} :: {1}", iTries, sUrl ) );
							}
							iTries--;
						} while( iTries > 0 );

						if( MacroscopePreferencesManager.GetFollowRobotsProtocol() )
						{
							int iCrawlDelay = this.JobMaster.GetCrawlDelay();
							if( iCrawlDelay > 0 )
							{
								DebugMsg( string.Format( "Sleeping for {0} seconds...", iCrawlDelay ) );
								Thread.Sleep( iCrawlDelay * 1000 );
							}
						}
						
					}
				
				}

				iMaxFetches--;

			}

			JobMaster.NotifyWorkersDone();
		
		}

		/** Check Include/Exclude URL *********************************************/

		Boolean CheckIncludeExcludeUrl ( string sUrl )
		{
			
			Boolean bSuccess = true;

			if( this.IncludeExcludeUrls.UseIncludeUrlPatterns() )
			{
				if( this.IncludeExcludeUrls.MatchesIncludeUrlPattern( sUrl ) )
				{
					DebugMsg( string.Format( "CheckIncludeExcludeUrl: MATCHES INCLUDE URL: {0}", sUrl ) );
				}
				else
				{
					DebugMsg( string.Format( "CheckIncludeExcludeUrl: DOES NOT MATCH INCLUDE URL: {0}", sUrl ) );
					bSuccess = false;
				}
			}

			if( this.IncludeExcludeUrls.UseExcludeUrlPatterns() )
			{
				if( this.IncludeExcludeUrls.MatchesExcludeUrlPattern( sUrl ) )
				{
					DebugMsg( string.Format( "CheckIncludeExcludeUrl: MATCHES EXCLUDE URL: {0}", sUrl ) );
					bSuccess = false;
				}
				else
				{
					DebugMsg( string.Format( "CheckIncludeExcludeUrl: DOES NOT MATCH EXCLUDE URL: {0}", sUrl ) );
				}
			}

			return( bSuccess );
			
		}

		/**************************************************************************/

		Boolean Fetch ( string sUrl )
		{

			MacroscopeDocument msDoc = new MacroscopeDocument ( sUrl );
			Boolean bResult = false;

			if( !MacroscopeDnsTools.CheckValidHostname( sUrl ) )
			{
				DebugMsg( string.Format( "Fetch :: CheckValidHostname: {0}", "NOT OK" ) );
				msDoc.SetStatusCode( HttpStatusCode.BadGateway );
				return( bResult );
			}
			
			if( !this.JobMaster.GetRobots().ApplyRobotRule( sUrl ) )
			{
				DebugMsg( string.Format( "Disallowed by robots.txt: {0}", sUrl ) );
				this.JobMaster.AddToBlockedByRobots( sUrl );
				return( bResult );
			}
			else
			{
				this.JobMaster.RemoveFromBlockedByRobots( sUrl );
			}

			this.JobMaster.AddHistoryItem( sUrl );

			if( this.AllowedHosts.IsExternalUrl( sUrl ) )
			{
				DebugMsg( string.Format( "IsExternalUrl: {0}", sUrl ) );
				msDoc.SetIsExternal( true );
			}

			if( this.DocCollection.ContainsDocument( sUrl ) )
			{
				if( !this.DocCollection.GetDocument( sUrl ).GetIsDirty() )
				{
					return( bResult );
				}
			}

			if( this.JobMaster.GetDepth() > 0 )
			{
				int Depth = MacroscopeUrlTools.FindUrlDepth( sUrl );
				if( Depth > this.JobMaster.GetDepth() )
				{
					DebugMsg( string.Format( "TOO DEEP: {0}", Depth ) );
					return( bResult );
				}
			}

			if( msDoc.Execute() )
			{
				
				this.DocCollection.AddDocument( sUrl, msDoc );

				this.JobMaster.IncPageLimitCount();

				if( msDoc.GetIsRedirect() )
				{

					DebugMsg( string.Format( "REDIRECTION DETECTED GetUrl: {0}", msDoc.GetUrl() ) );
					DebugMsg( string.Format( "REDIRECTION DETECTED From: {0}", msDoc.GetUrlRedirectFrom() ) );
					DebugMsg( string.Format( "REDIRECTION DETECTED To: {0}", msDoc.GetUrlRedirectTo() ) );

					if( MacroscopePreferencesManager.GetFollowRedirects() )
					{
						string sHostname = msDoc.GetHostname();
						string sHostnameFrom = MacroscopeAllowedHosts.ParseHostnameFromUrl( msDoc.GetUrlRedirectFrom() );
						string sHostnameTo = MacroscopeAllowedHosts.ParseHostnameFromUrl( msDoc.GetUrlRedirectTo() );
					}

					this.JobMaster.AddUrlQueueItem( msDoc.GetUrlRedirectTo() );

				}
				else
				{

					this.ProcessHrefLangLanguages( msDoc ); // Process Languages from HrefLang

					this.ProcessOutlinks( msDoc ); // Process Outlinks from document

				}
				
				bResult = true;
				
			}
			else
			{
				DebugMsg( string.Format( "EXECUTE FAILED: {0}", sUrl ) );
			}

			return( bResult );
			
		}

		/**************************************************************************/
		
		void ProcessHrefLangLanguages ( MacroscopeDocument msDoc )
		{

			string sLocale = msDoc.GetLocale();
			Dictionary<string,MacroscopeHrefLang> dicHrefLangs = msDoc.GetHrefLangs();

			if( sLocale != null )
			{
				this.JobMaster.AddLocales( sLocale );
			}

			foreach( string sKeyLocale in dicHrefLangs.Keys )
			{
				this.JobMaster.AddLocales( sKeyLocale );
			}

		}

		/**************************************************************************/

		void ProcessOutlinks ( MacroscopeDocument msDoc )
		{

			if(
				( this.JobMaster.GetRuntimeMode() == MacroscopeConstants.RunTimeMode.LISTFILE )
				|| ( this.JobMaster.GetRuntimeMode() == MacroscopeConstants.RunTimeMode.LISTTEXT )
				|| ( this.JobMaster.GetRuntimeMode() == MacroscopeConstants.RunTimeMode.SITEMAP ) )
			{
				DebugMsg( string.Format( "ProcessOutlinks LISTMODE: {0}", this.JobMaster.GetRuntimeMode() ) );
				return;
			}

			foreach( string sUrl in msDoc.IterateOutlinks() )
			{

				MacroscopeOutlink Outlink = msDoc.GetOutlink( sUrl );
				Boolean bProceed = true;

				if( !Outlink.Follow )
				{
					continue;
				}

				if( Outlink.AbsoluteUrl == null )
				{
					continue;
				}

				if( this.JobMaster.SeenHistoryItem( Outlink.AbsoluteUrl ) )
				{
					continue;
				}

				/*
				if( !this.AllowedHosts.IsAllowedFromUrl( Outlink.AbsoluteUrl ) ) {
					//DebugMsg( string.Format( "FOREIGN HOST: {0}", Outlink.AbsoluteUrl ) );
					continue;
				}
				*/
				
				if( this.JobMaster.GetPageLimit() > -1 )
				{
					if( this.JobMaster.GetPageLimitCount() >= this.JobMaster.GetPageLimit() )
					{
						DebugMsg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", this.JobMaster.GetPageLimit(), this.JobMaster.GetPageLimitCount() ) );
						bProceed = false;
					}
				}

				if( bProceed )
				{

					this.JobMaster.AddUrlQueueItem( Outlink.AbsoluteUrl );

					this.JobMaster.AddToProgress( Outlink.AbsoluteUrl );

				}

			}
			
		}

		/**************************************************************************/

	}
	
}
