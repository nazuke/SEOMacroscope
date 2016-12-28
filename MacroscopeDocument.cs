using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Threading;

namespace SEOMacroscope
{

	public partial class MacroscopeDocument : Macroscope
	{

		/**************************************************************************/

		/** BEGIN: Configuration **/
		public Boolean probe_hreflangs { get; set; }
		/** END: Configuration **/
		
		string url;
		int timeout;

		Boolean is_redirect;
		string url_redirect_from;

		HtmlDocument htmlDoc;
		
		public MacroscopeDocument parent;

		public string scheme;
		public string hostname;
		public int port;
		public string path;
		public string fragment;
		public string query_string;

		public int status_code;
		public long content_length;
		public string mime_type;
		public string content_encoding;
		public string locale;
		public DateTime date_server;
		public DateTime date_modified;

		public string canonical;
		public Hashtable hreflang;

		// Outbound links to pages and linked assets to follow
		public Hashtable outlinks;

		// Inbound links from other pages in the scanned collection
		public Hashtable inhyperlinks;

		// Outbound hypertext links
		public Hashtable outhyperlinks;
		
		
		public string title;
		public string description;
		public string keywords;

		public ArrayList headings1;
		public ArrayList headings2;

		public int depth;
		
		/**************************************************************************/

		public MacroscopeDocument ( string sURL )
		{
			url = sURL;
			timeout = 10000;
			is_redirect = false;
			status_code = 0;
			mime_type = null;
			locale = "null";
			date_server = new DateTime ();
			date_modified = new DateTime ();
			hreflang = new Hashtable ();
			outlinks = new Hashtable ();
			inhyperlinks = new Hashtable ();
			outhyperlinks = new Hashtable ();
			depth = MacroscopeURLTools.find_url_depth( url );
		}
		
		/**************************************************************************/

		public string get_url()
		{
			return( this.url );
		}

		/**************************************************************************/

		public string get_is_redirect()
		{
			return( this.is_redirect.ToString() );
		}

		/**************************************************************************/
		
		public int get_status_code()
		{
			return( this.status_code );
		}

		/**************************************************************************/
		
		public string get_mime_type()
		{
			string sMimeType = null;
			if( this.mime_type == null ) {
				sMimeType = "";
			} else {
				MatchCollection matches = Regex.Matches( this.mime_type, "^([^\\s;/]+)/([^\\s;/]+)" );
				foreach( Match match in matches ) {
					sMimeType = String.Format( "{0}/{1}", match.Groups[ 1 ].Value, match.Groups[ 2 ].Value );
				}
				if( sMimeType == null ) {
					sMimeType = this.mime_type;
				}
			}
			return( sMimeType );
		}
		
		/**************************************************************************/
		
		public string get_lang()
		{
			return( this.locale );
		}
		
		/**************************************************************************/
		
		public string get_locale()
		{
			return( this.locale );
		}

		/**************************************************************************/
		
		public string get_canonical()
		{
			return( this.canonical );
		}

		/**************************************************************************/
		
		public string get_date_server()
		{
			return( this.date_server.ToShortDateString() );
		}
		
		/**************************************************************************/
		
		public string get_date_modified()
		{
			return( this.date_modified.ToShortDateString() );
		}
				
		/**************************************************************************/

		public Hashtable get_outlinks()
		{
			return( this.outlinks );
		}
		
		/**************************************************************************/
		
		public int count_outlinks()
		{
			int iCount = this.get_outlinks().Count;
			return( iCount );
		}

		/**************************************************************************/
				
		public Hashtable add_inhyperlink( string sURL )
		{
			if( this.inhyperlinks.ContainsKey( sURL ) ) {
				int count = ( int )this.inhyperlinks[ sURL ] + 1;
				this.inhyperlinks[ sURL ] = count;
			} else {
				this.inhyperlinks.Add( sURL, 1 );
			}
			return( this.inhyperlinks );
		}

		/**************************************************************************/

		public Hashtable get_inhyperlinks()
		{
			return( this.inhyperlinks );
		}

		/**************************************************************************/

		public Hashtable get_outhyperlinks()
		{
			return( this.outhyperlinks );
		}
		
		/**************************************************************************/

		public int count_inhyperlinks()
		{
			int iCount = this.get_inhyperlinks().Count;
			return( iCount );
		}
		
		/**************************************************************************/
		
		public int count_outhyperlinks()
		{
			int iCount = this.get_outhyperlinks().Count;
			return( iCount );
		}

		/**************************************************************************/
		
		public string get_title()
		{
			string sValue;
			if( this.title != null ) {
				sValue = this.title;
			} else {
				sValue = "";
			}
			return( sValue );
		}

		/**************************************************************************/
		
		public string get_description()
		{
			string sValue;
			if( this.description != null ) {
				sValue = this.description;
			} else {
				sValue = "";
			}
			return( sValue );
		}

		/**************************************************************************/

		void SetHreflang( string sLocale, string sURL )
		{
			MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang ( false, sLocale, sURL );
			this.hreflang[ sLocale ] = msHrefLang;
		}

		/**************************************************************************/

		public Hashtable GetHreflangs()
		{
			return( this.hreflang );
		}

		/**************************************************************************/

		public Boolean Execute()
		{

			if( this.IsRedirectPage() ) {
				debug_msg( string.Format( "IS REDIRECT: {0}", this.url ), 2 );
				this.is_redirect = true;
			} 

			if( this.IsHtmlPage() ) {
				debug_msg( string.Format( "IS HTML PAGE: {0}", this.url ), 2 );
				this.ProcessHtmlPage();

			} else if( this.IsCssPage() ) {
				debug_msg( string.Format( "IS CSS PAGE: {0}", this.url ), 2 );
				this.ProcessCssPage();

			} else if( this.IsImagePage() ) {
				debug_msg( string.Format( "IS IMAGE PAGE: {0}", this.url ), 2 );
				this.process_image_page();
				
			} else if( this.IsJavascriptPage() ) {
				debug_msg( string.Format( "IS JAVASCRIPT PAGE: {0}", this.url ), 2 );
				this.process_javascript_page();

			} else if( this.IsPdfPage() ) {
				debug_msg( string.Format( "IS PDF PAGE: {0}", this.url ), 2 );
				this.ProcessPdfPage();

			} else if( this.IsBinaryPage() ) {
				debug_msg( string.Format( "IS BINARY PAGE: {0}", this.url ), 2 );
				this.ProcessBinaryPage();

			} else {
				debug_msg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.url ), 2 );
			}
			
			return( true );

		}

		/**************************************************************************/

		Boolean IsRedirectPage()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIsRedirect = false;
			string sOriginalURL = this.url;

			try {

				req = WebRequest.CreateHttp( this.url );
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				req.AllowAutoRedirect = false;
				res = ( HttpWebResponse )req.GetResponse();

				debug_msg( string.Format( "Status: {0}", res.StatusCode ), 2 );

				if( res.StatusCode == HttpStatusCode.Moved ) {
					bIsRedirect = true;
				} else if( res.StatusCode == HttpStatusCode.MovedPermanently ) {
					bIsRedirect = true;
				}
			
				if( bIsRedirect ) {
					this.is_redirect = true;
					this.url = res.GetResponseHeader( "Location" );
					this.url_redirect_from = sOriginalURL;



					this.url = MacroscopeURLTools.make_url_absolute( this.url, this.url_redirect_from );









				}
				res.Close();

			} catch( WebException ex ) {
				debug_msg( string.Format( "is_redirect :: WebException: {0}", ex.Message ), 2 );
			}

			return( bIsRedirect );
		}

		/**************************************************************************/

		int ProcessStatusCode( HttpStatusCode status )
		{
			int iStatus = 0;
			switch( status ) {
				case HttpStatusCode.OK:
					iStatus = 200;
					break;
				case HttpStatusCode.MovedPermanently:
					iStatus = 301;
					break;
				case HttpStatusCode.NotFound:
					iStatus = 404;
					break;
				case HttpStatusCode.Gone:
					iStatus = 410;
					break;
				case HttpStatusCode.InternalServerError:
					iStatus = 500;
					break;
			}
			return( iStatus );
		}

		/**************************************************************************/

	}

}
