using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RobotsTxt;

namespace SEOMacroscope
{

	public class MacroscopeJob : Macroscope
	{

		/** BEGIN: Configuration **/
		
		public string start_url { get; set; }
		public int depth { get; set; }
		public int page_limit { get; set; }
		int page_limit_count;
		public Boolean same_site { get; set; }
		public Boolean probe_hreflangs { get; set; }
		
		/** END: Configuration **/

		int pages_found;
		
		Hashtable history;
		Hashtable doc_collection;
		Hashtable locales;
		
		MacroscopeJobThread msJobThread;
		MacroscopeRobots robots;
				
		/**************************************************************************/

		public MacroscopeJob ( MacroscopeJobThread msJobThreadNew )
		{
			depth = 10;
			page_limit = -1;
			page_limit_count = 0;
			same_site = true;
			pages_found = 0;
			history = new Hashtable ( 4096 );
			doc_collection = new Hashtable ( 4096 );
			locales = new Hashtable ( 32 );
			msJobThread = msJobThreadNew;
			robots = new MacroscopeRobots ();
		}

		/**************************************************************************/

		public Boolean run()
		{

			debug_msg( "Run", 1 );

			debug_msg( string.Format( "Start URL: {0}", this.start_url ), 1 );

			this.page_limit_count = 0;
			this.recurse( start_url, start_url, 0 );

			debug_msg( string.Format( "Pages Found: {0}", this.pages_found ), 1 );

			debug_msg( "Done", 1 );

			return( true );
		}
		
		/**************************************************************************/
		
		public Hashtable get_doc_collection()
		{
			return( this.doc_collection );
		}
		
		/**************************************************************************/
		
		public Hashtable get_locales()
		{
			return( this.locales );
		}

		/**************************************************************************/

		public Boolean recurse( string sParentURL, string sURL, int iDepth )
		{
			MacroscopeDocument msDoc = new MacroscopeDocument ( sURL );

			if( !robots.apply_robot_rule( sURL ) ) {
				debug_msg( string.Format( "Disallowed by robots.txt: {0}", sURL ), 1 );
				return( false );
			}

			if( this.doc_collection.ContainsKey( sURL ) ) {

				debug_msg( string.Format( "ADDING INLINK FOR: {0}", sURL ), 2 );
				debug_msg( string.Format( "PARENT: {0}", sParentURL ), 3 );
								
				msDoc = ( MacroscopeDocument )this.doc_collection[ sURL ];
				if( msDoc != null ) {
					msDoc.add_inlink( sParentURL );
				}
				return( true );

			} else {
				this.doc_collection.Add( sURL, msDoc );
				msDoc.add_inlink( sParentURL );
			}
			
			if( msDoc.depth > this.depth ) {
				//debug_msg( string.Format( "TOO DEEP: {0}", msDoc.depth ), 3 );
				this.doc_collection.Remove( sURL );
				return( true );
			}

			if( this.probe_hreflangs ) {
				msDoc.probe_hreflangs = true;
			}

			if( msDoc.execute() ) {
			
				this.page_limit_count++;

				{
					string sLocale = msDoc.locale;
					Hashtable htHrefLangs = ( Hashtable )msDoc.get_hreflangs();
					if( sLocale != null ) {
						if( !this.locales.ContainsKey( sLocale ) ) {
							this.locales[ sLocale ] = sLocale;
						}
					}
					foreach( string sKeyLocale in htHrefLangs.Keys ) {
						if( !this.locales.ContainsKey( sKeyLocale ) ) {
							this.locales[ sKeyLocale ] = sKeyLocale;
						}
					}
				}

				Hashtable htOutlinks = msDoc.get_outlinks();

				foreach( string sOutlinkKey in htOutlinks.Keys ) {
					string sOutlinkURL = ( string )htOutlinks[ sOutlinkKey ];
					//debug_msg( string.Format( "Outlink: {0}", sOutlinkURL ), 2 );

					if( sOutlinkURL != null ) {

						Boolean bProceed = true;

						if( this.page_limit < 0 ) {
							bProceed = true;
						} else if( this.page_limit > -1 ) {
							if( this.page_limit_count >= this.page_limit ) {
								debug_msg( string.Format( "PAGE LIMIT REACHED: {0} :: {1}", this.page_limit, this.page_limit_count ), 2 );
								bProceed = false;
							}
						}

						if( bProceed ) {
							if( MacroscopeURLTools.verify_same_host( this.start_url, sOutlinkURL ) ) {

								if( this.history.ContainsKey( sOutlinkURL ) ) {
									//debug_msg( string.Format( "ALREADY SEEN: {0}", sOutlinkURL ), 2 );
								} else {
									debug_msg( string.Format( "RECURSING INTO: {0}", sOutlinkURL ), 2 );
									this.pages_found++;
									this.history.Add( sOutlinkURL, true );

									this.msJobThread.Update();

									this.recurse( sURL, sOutlinkURL, iDepth + 1 );
								}

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

			return( true );
		}

		/**************************************************************************/

		public Boolean list_results()
		{

			debug_msg( "Results", 1 );

			foreach( string sKey in this.doc_collection.Keys ) {

				debug_msg( string.Format( "Doc Key: {0}", sKey ), 2 );

				MacroscopeDocument msDoc = ( MacroscopeDocument )this.doc_collection[ sKey ];

				debug_msg( string.Format( "URL: {0}", msDoc.get_url() ), 3 );
				debug_msg( string.Format( "Status: {0}", msDoc.status_code.ToString() ), 3 );

				if( msDoc.mime_type != null ) {
					debug_msg( string.Format( "MIME Type: {0}", msDoc.mime_type.ToString() ), 3 );
				} else {
					debug_msg( string.Format( "MIME Type: {0}", "ERROR" ), 3 );
				}

				if( msDoc.canonical != null ) {
					debug_msg( string.Format( "Canonical: {0}", msDoc.canonical.ToString() ), 3 );
				} else {
					debug_msg( string.Format( "Canonical: {0}", "ERROR" ), 3 );
				}

				if( msDoc.title != null ) {
					debug_msg( string.Format( "Title: {0}", msDoc.title.ToString() ), 3 );
				} else {
					debug_msg( string.Format( "Title: {0}", "ERROR" ), 3 );
				}

				Hashtable htInlinks = ( Hashtable )msDoc.get_inlinks();
				
				foreach( string sKeyURL in htInlinks.Keys ) {
					debug_msg( string.Format( "Inlink: {0} :: {1}", htInlinks[ sKeyURL ], sKeyURL ), 4 );
				}
				
				Hashtable htHrefLangs = ( Hashtable )msDoc.get_hreflangs();
				
				foreach( string sKeyLocale in htHrefLangs.Keys ) {
					MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[ sKeyLocale ];
					debug_msg( string.Format( "HrefLang: {0}", msHrefLang.get_locale() ), 4 );
					debug_msg( string.Format( "URL: {0}", msHrefLang.get_url() ), 5 );
					debug_msg( string.Format( "Available: {0}", msHrefLang.is_available() ), 5 );
				}

			}

			return( true );
		}

		/**************************************************************************/

	}

}
