using System;
using System.Collections;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeJobWorker : Macroscope
	{

		/**************************************************************************/

		readonly MacroscopeJobMaster msJobMaster;
		
		/**************************************************************************/

		public MacroscopeJobWorker ( MacroscopeJobMaster msJobMasterNew )
		{
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

			if( ! this.msJobMaster.RobotsGet().ApplyRobotRule( sURL ) ) {
				debug_msg( string.Format( "Disallowed by robots.txt: {0}", sURL ), 1 );
				return;
			}

			this.msJobMaster.HistoryAdd( sURL );

			if( DocCollection.Contains( sURL ) ) {
				return;
			} else {
				DocCollection.Add( sURL, msDoc );
			}

			if( this.msJobMaster.Depth > 0 ) {
				if( msDoc.Depth > this.msJobMaster.Depth ) {
					//debug_msg( string.Format( "TOO DEEP: {0}", msDoc.depth ), 3 );
					DocCollection.Remove( sURL );
					return;
				}
			}

			if( this.msJobMaster.ProbeHrefLangs ) {
				msDoc.ProbeHrefLangs = true;
			}

			if( msDoc.Execute() ) {
			
				this.msJobMaster.PageLimitCount++;

				{
					string sLocale = msDoc.Locale;
					Hashtable htHrefLangs = ( Hashtable )msDoc.GetHrefLangs();
					if( sLocale != null ) {
						this.msJobMaster.AddLocales( sLocale );
					}
					foreach( string sKeyLocale in htHrefLangs.Keys ) {
						this.msJobMaster.AddLocales( sKeyLocale );
					}
				}

				Hashtable htOutlinks = msDoc.GetOutlinks();

				foreach( string sOutlinkKey in htOutlinks.Keys ) {
					
					string sOutlinkURL = ( string )htOutlinks[ sOutlinkKey ];

					if( sOutlinkURL != null ) {

						Boolean bProceed = true;

						if( this.msJobMaster.PageLimit < 0 ) {

							bProceed = true;

						} else if( this.msJobMaster.PageLimit > -1 ) {
						
							if( this.msJobMaster.PageLimitCount >= this.msJobMaster.PageLimit ) {
								debug_msg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", this.msJobMaster.PageLimit, this.msJobMaster.PageLimitCount ), 2 );
								bProceed = false;
							}
							
						}
						
						if( bProceed ) {
							
							if( MacroscopeURLTools.VerifySameHost( this.msJobMaster.StartUrl, sOutlinkURL ) ) {
								this.msJobMaster.UrlQueueAdd( sOutlinkURL );
							} else {
								//debug_msg( string.Format( "FOREIGN HOST: {0}", sOutlinkURL ), 2 );
							}
							
						} else {
							break;
						}

					}

				}

			} else {
				debug_msg( string.Format( "EXECUTE FAILED: {0}", sURL ), 2 );
			}

		}

		/**************************************************************************/

		/*
		public void debug_msg ( String sMsg )
		{
		}

		public void debug_msg ( String sMsg, int iOffset )
		{
		}
		*/
		
		/**************************************************************************/
	}
	
}
