using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RobotsTxt;

namespace SEOMacroscope
{

	public class MacroscopeJob
	{

		/** BEGIN: Configuration **/
		
		public string start_url { get; set; }
		public int depth { get; set; }
		public Boolean same_site { get; set; }
		public Boolean probe_hreflangs { get; set; }
		
		/** END: Configuration **/

		int pages_found;
		
		Hashtable history;
				
		Hashtable doc_collection;

		MacroscopeRobots robots;
				
		/**************************************************************************/

		public MacroscopeJob ()
		{
			depth = 10;
			same_site = true;
			pages_found = 0;
			history = new Hashtable (4096);
			doc_collection = new Hashtable (4096);
			robots = new MacroscopeRobots ();
		}

		/**************************************************************************/

		public Boolean run()
		{

			debug_msg( "Run", 1 );

			debug_msg( string.Format( "Start URL: {0}", this.start_url ), 1 );

			this.recurse( start_url, start_url, 0 );

			this.list_results();
			
			debug_msg( string.Format( "Pages Found: {0}", this.pages_found ), 1 );

			debug_msg( "Done", 1 );

			return( true );
		}

		/**************************************************************************/

		public Boolean recurse( string sParentURL, string sURL, int iDepth )
		{
			MacroscopeDocument msDoc = new MacroscopeDocument (sURL);

			if (!robots.apply_robot_rule( sURL )) {
				debug_msg( string.Format( "Disallowed by robots.txt: {0}", sURL ), 1 );
				return( false );
			}

			if (this.doc_collection.ContainsKey( sURL )) {

				debug_msg( string.Format( "ADDING INLINK FOR: {0}", sURL ), 2 );
				debug_msg( string.Format( "PARENT: {0}", sParentURL ), 3 );
								
				msDoc = (MacroscopeDocument)this.doc_collection[ sURL ];
				if (msDoc != null) {
					msDoc.add_inlink( sParentURL );
				}
				return( true );

			} else {
				this.doc_collection.Add( sURL, msDoc );
				msDoc.add_inlink( sParentURL );
			}
			
			if (msDoc.depth > this.depth) {
				//debug_msg( string.Format( "TOO DEEP: {0}", msDoc.depth ), 3 );
				this.doc_collection.Remove( sURL );
				return( true );
			}

			if (msDoc.execute()) {
			
				Hashtable htOutlinks = msDoc.get_outlinks();

				foreach (string sOutlinkKey in htOutlinks.Keys) {
					string sOutlinkURL = (string)htOutlinks[ sOutlinkKey ];
					//debug_msg( string.Format( "Outlink: {0}", sOutlinkURL ), 2 );

					if (sOutlinkURL != null) {

						if (MacroscopeURLTools.verify_same_host( this.start_url, sOutlinkURL )) {

							if (this.history.ContainsKey( sOutlinkURL )) {
								//debug_msg( string.Format( "ALREADY SEEN: {0}", sOutlinkURL ), 2 );
							} else {
								debug_msg( string.Format( "RECURSING INTO: {0}", sOutlinkURL ), 2 );
								this.pages_found++;
								this.history.Add( sOutlinkURL, true );
								this.recurse( sURL, sOutlinkURL, iDepth + 1 );
							}

						} else {
							//debug_msg( string.Format( "FOREIGN HOST: {0}", sOutlinkURL ), 2 );
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

			foreach (string sKey in this.doc_collection.Keys) {

				debug_msg( string.Format( "Doc Key: {0}", sKey ), 2 );

				MacroscopeDocument msDoc = (MacroscopeDocument)this.doc_collection[ sKey ];

				debug_msg( string.Format( "URL: {0}", msDoc.get_url() ), 3 );
				debug_msg( string.Format( "Status: {0}", msDoc.status_code.ToString() ), 3 );

				if (msDoc.mime_type != null) {
					debug_msg( string.Format( "MIME Type: {0}", msDoc.mime_type.ToString() ), 3 );
				} else {
					debug_msg( string.Format( "MIME Type: {0}", "ERROR" ), 3 );
				}

				if (msDoc.title != null) {
					debug_msg( string.Format( "Title: {0}", msDoc.title.ToString() ), 3 );
				} else {
					debug_msg( string.Format( "Title: {0}", "ERROR" ), 3 );
				}

				Hashtable htInlinks = (Hashtable)msDoc.get_inlinks();
				
				foreach (string sKeyInlink in htInlinks.Keys) {
					debug_msg( string.Format( "Inlink: {0} :: {1}", htInlinks[ sKeyInlink ], sKeyInlink ), 4 );
				}

			}

			return( true );
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
