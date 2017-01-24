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

		readonly MacroscopeJobMaster msJobMaster;
		
		/**************************************************************************/

		public MacroscopeJobWorker ( MacroscopeJobMaster msJobMasterNew )
		{
			SuppressDebugMsg = true;
			msJobMaster = msJobMasterNew;
		}

		/**************************************************************************/
		
		public void Execute ( string sUrl )
		{
			DebugMsg( string.Format( "Execute: {0}", sUrl ) );
			this.Fetch( sUrl );
			msJobMaster.NotifyWorkersDone( sUrl );
		}

		/**************************************************************************/

		void Fetch ( string sUrl )
		{

			MacroscopeDocument msDoc = new MacroscopeDocument ( sUrl );
			MacroscopeDocumentCollection DocCollection = this.msJobMaster.GetDocCollection();
			MacroscopeAllowedHosts AllowedHosts = this.msJobMaster.GetAllowedHosts();

			AllowedHosts.DumpAllowedHosts();

			if( !this.msJobMaster.GetRobots().ApplyRobotRule( sUrl ) ) {
				DebugMsg( string.Format( "Disallowed by robots.txt: {0}", sUrl ) );
				return;
			}

			this.msJobMaster.AddHistory( sUrl );

			if( MacroscopePreferencesManager.GetSameSite() ) {
				if( !AllowedHosts.IsAllowedFromUrl( sUrl ) ) {
					DebugMsg( string.Format( "Disallowed by SameSite.txt: {0}", sUrl ) );
					return;
				}
			}

			if( DocCollection.ContainsDocument( sUrl ) ) {
				return;
			}

			if( this.msJobMaster.GetDepth() > 0 ) {
				int Depth = MacroscopeUrlTools.FindUrlDepth( sUrl );
				if( Depth > this.msJobMaster.GetDepth() ) {
					DebugMsg( string.Format( "TOO DEEP: {0}", Depth ) );
					return;
				}
			}

			if( msDoc.Execute() ) {
				
				DocCollection.AddDocument( sUrl, msDoc );

				this.msJobMaster.IncPageLimitCount();

				if( msDoc.GetIsRedirect() ) {

					DebugMsg( string.Format( "Redirect Discovered: {0}", msDoc.GetUrlRedirectTo() ) );
					this.msJobMaster.AddUrlQueueItem( msDoc.GetUrlRedirectTo() );

				} else {

					this.ProcessHrefLangLanguages( msDoc ); // Process Languages from HrefLang

					this.ProcessOutlinks( msDoc ); // Process Outlinks from document

				}

			} else {
				DebugMsg( string.Format( "EXECUTE FAILED: {0}", sUrl ) );
			}

		}

		/**************************************************************************/
		
		void ProcessHrefLangLanguages ( MacroscopeDocument msDoc )
		{

			string sLocale = msDoc.GetLocale();
			Dictionary<string,MacroscopeHrefLang> dicHrefLangs = msDoc.GetHrefLangs();

			if( sLocale != null ) {
				this.msJobMaster.AddLocales( sLocale );
			}

			foreach( string sKeyLocale in dicHrefLangs.Keys ) {
				this.msJobMaster.AddLocales( sKeyLocale );
			}

		}

		/**************************************************************************/

		void ProcessOutlinks ( MacroscopeDocument msDoc )
		{

			MacroscopeAllowedHosts AllowedHosts = this.msJobMaster.GetAllowedHosts();
						
			foreach( string sUrl in msDoc.IterateOutlinks() ) {

				MacroscopeOutlink Outlink = msDoc.GetOutlink( sUrl );
				Boolean bProceed = true;

				if( !Outlink.Follow ) {
					continue;
				}

				if( Outlink.AbsoluteUrl == null ) {
					continue;
				}

				if( this.msJobMaster.SeenHistory( Outlink.AbsoluteUrl ) ) {
					continue;
				}

				if( !AllowedHosts.IsAllowedFromUrl( Outlink.AbsoluteUrl ) ) {
					DebugMsg( string.Format( "FOREIGN HOST: {0}", Outlink.AbsoluteUrl ) );
					continue;
				}

				if( this.msJobMaster.GetPageLimit() > -1 ) {
					if( this.msJobMaster.GetPageLimitCount() >= this.msJobMaster.GetPageLimit() ) {
						DebugMsg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", this.msJobMaster.GetPageLimit(), this.msJobMaster.GetPageLimitCount() ) );
						bProceed = false;
					}
				}

				if( bProceed ) {
					this.msJobMaster.AddUrlQueueItem( Outlink.AbsoluteUrl );
				}

			}
			
		}

		/**************************************************************************/

	}
	
}
