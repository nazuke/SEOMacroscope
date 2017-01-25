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

namespace SEOMacroscope
{

	public class MacroscopeJobWorker : Macroscope
	{

		/**************************************************************************/
				
		public override Boolean SuppressDebugMsg { get; protected set; }
				
		/**************************************************************************/

		MacroscopeJobMaster JobMaster;
		MacroscopeDocumentCollection DocCollection;
		MacroscopeAllowedHosts AllowedHosts;
		
		Boolean CheckExternalLinks;
			
		/**************************************************************************/

		public MacroscopeJobWorker ( MacroscopeJobMaster JobMasterNew )
		{
			
			SuppressDebugMsg = false;
			
			JobMaster = JobMasterNew;
			
			DocCollection = JobMaster.GetDocCollection();
			AllowedHosts = JobMaster.GetAllowedHosts();

			CheckExternalLinks = MacroscopePreferencesManager.GetCheckExternalLinks();

		}

		/**************************************************************************/

		public void Execute ()
		{
					
			int iMaxFetches = MacroscopePreferencesManager.GetMaxFetchesPerWorker();
			
			while( iMaxFetches > 0 ) {

				if( JobMaster.GetThreadsStop() ) {

					DebugMsg( string.Format( "JobMaster.GetThreadsStop: {0}", JobMaster.GetThreadsStop() ) );
					break;
					
				} else {
				
					string sUrl = this.JobMaster.GetUrlQueueItem();
				
					if( sUrl != null ) {

						DebugMsg( string.Format( "Execute: {0}", sUrl ) );
					
						if( this.Fetch( sUrl ) ) {
							JobMaster.NotifyWorkersFetched( sUrl );
						}
					
					}
				
				}
				
				iMaxFetches--;

			}

			JobMaster.NotifyWorkersDone();
		
		}

		/**************************************************************************/

		Boolean Fetch ( string sUrl )
		{

			MacroscopeDocument msDoc = new MacroscopeDocument ( sUrl );
			Boolean bResult = false;
			
			//this.AllowedHosts.DumpAllowedHosts();

			if( !this.JobMaster.GetRobots().ApplyRobotRule( sUrl ) ) {
				DebugMsg( string.Format( "Disallowed by robots.txt: {0}", sUrl ) );
				return( bResult );
			}

			this.JobMaster.AddHistoryItem( sUrl );

			/*
			if( this.CheckExternalLinks ) {
				if( !this.AllowedHosts.IsAllowedFromUrl( sUrl ) ) {
					DebugMsg( string.Format( "Disallowed by SameSite: {0}", sUrl ) );
					return( bResult );
				}
			}
			*/

			// TODO: Add HEAD checking here
			if( this.CheckExternalLinks ) {
				if( !this.AllowedHosts.IsExternalUrl( sUrl ) ) {
					DebugMsg( string.Format( "IsExternalUrl: {0}", sUrl ) );
					return( bResult );
				}
				
			}

			if( this.DocCollection.ContainsDocument( sUrl ) ) {
				if( !this.DocCollection.GetDocument( sUrl ).GetIsDirty() ) {
					return( bResult );
				}
			}

			if( this.JobMaster.GetDepth() > 0 ) {
				int Depth = MacroscopeUrlTools.FindUrlDepth( sUrl );
				if( Depth > this.JobMaster.GetDepth() ) {
					DebugMsg( string.Format( "TOO DEEP: {0}", Depth ) );
					return( bResult );
				}
			}

			if( msDoc.Execute() ) {
				
				this.DocCollection.AddDocument( sUrl, msDoc );

				this.JobMaster.IncPageLimitCount();

				if( msDoc.GetIsRedirect() ) {

					DebugMsg( string.Format( "Redirect Discovered: {0}", msDoc.GetUrlRedirectTo() ) );
					this.JobMaster.AddUrlQueueItem( msDoc.GetUrlRedirectTo() );

				} else {

					this.ProcessHrefLangLanguages( msDoc ); // Process Languages from HrefLang

					this.ProcessOutlinks( msDoc ); // Process Outlinks from document

				}
				
				bResult = true;
				
			} else {
				DebugMsg( string.Format( "EXECUTE FAILED: {0}", sUrl ) );
			}
			
			return( bResult );
			
		}

		/**************************************************************************/
		
		void ProcessHrefLangLanguages ( MacroscopeDocument msDoc )
		{

			string sLocale = msDoc.GetLocale();
			Dictionary<string,MacroscopeHrefLang> dicHrefLangs = msDoc.GetHrefLangs();

			if( sLocale != null ) {
				this.JobMaster.AddLocales( sLocale );
			}

			foreach( string sKeyLocale in dicHrefLangs.Keys ) {
				this.JobMaster.AddLocales( sKeyLocale );
			}

		}

		/**************************************************************************/

		void ProcessOutlinks ( MacroscopeDocument msDoc )
		{

			// TODO: add this.SameSite check

			foreach( string sUrl in msDoc.IterateOutlinks() ) {

				MacroscopeOutlink Outlink = msDoc.GetOutlink( sUrl );
				Boolean bProceed = true;

				if( !Outlink.Follow ) {
					continue;
				}

				if( Outlink.AbsoluteUrl == null ) {
					continue;
				}

				if( this.JobMaster.SeenHistoryItem( Outlink.AbsoluteUrl ) ) {
					continue;
				}

				if( !this.AllowedHosts.IsAllowedFromUrl( Outlink.AbsoluteUrl ) ) {
					//DebugMsg( string.Format( "FOREIGN HOST: {0}", Outlink.AbsoluteUrl ) );
					continue;
				}

				if( this.JobMaster.GetPageLimit() > -1 ) {
					if( this.JobMaster.GetPageLimitCount() >= this.JobMaster.GetPageLimit() ) {
						DebugMsg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", this.JobMaster.GetPageLimit(), this.JobMaster.GetPageLimitCount() ) );
						bProceed = false;
					}
				}

				if( bProceed ) {
					this.JobMaster.AddUrlQueueItem( Outlink.AbsoluteUrl );
				}

			}
			
		}

		/**************************************************************************/

	}
	
}
