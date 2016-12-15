using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Threading;

namespace SEOMacroscope
{

	public class MacroscopeDocument
	{

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
		public string date_modified;

		public string canonical;
		public Hashtable hreflang;

		public Hashtable inlinks;
		public Hashtable outlinks;

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
			hreflang = new Hashtable ();
			inlinks = new Hashtable ();
			outlinks = new Hashtable ();
			depth = MacroscopeURLTools.find_url_depth( url );
		}
		
		/**************************************************************************/

		public string get_url()
		{
			return( this.url );
		}

		/**************************************************************************/
		
		public int get_status_code()
		{
			return( this.status_code );
		}
				
		/**************************************************************************/
				
		public Hashtable add_inlink( string sURL )
		{
			if (this.inlinks.ContainsKey( sURL )) {
				int count = (int)this.inlinks[ sURL ] + 1;
				this.inlinks[ sURL ] = count;
			} else {
				this.inlinks.Add( sURL, 1 );
			}
			return( this.inlinks );
		}

		/**************************************************************************/

		public Hashtable get_inlinks()
		{
			return( this.inlinks );
		}

		/**************************************************************************/

		public Hashtable get_outlinks()
		{
			return( this.outlinks );
		}

		/**************************************************************************/

		public Boolean execute()
		{

			if (this.is_redirect_page()) {
				debug_msg( string.Format( "IS REDIRECT: {0}", this.url ), 2 );
			} 

			if (this.is_html_page()) {

				debug_msg( string.Format( "IS HTML PAGE: {0}", this.url ), 2 );
				this.process_html_page();

			} else if (this.is_binary_page()) {

				debug_msg( string.Format( "IS BINARY PAGE: {0}", this.url ), 2 );
				this.process_binary_page();

			} else {

				debug_msg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.url ), 2 );

			}
			
			return( true );

		}

		/**************************************************************************/

		Boolean is_redirect_page()
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
				res = (HttpWebResponse)req.GetResponse();

				debug_msg( string.Format( "Status: {0}", res.StatusCode ), 2 );

				if (res.StatusCode == HttpStatusCode.Moved) {
					bIsRedirect = true;
				} else if (res.StatusCode == HttpStatusCode.MovedPermanently) {
					bIsRedirect = true;
				}
			
				if (bIsRedirect) {
					this.is_redirect = true;
					this.url = res.GetResponseHeader( "Location" );
					this.url_redirect_from = sOriginalURL;
				}
				res.Close();

			} catch (WebException ex) {
				debug_msg( string.Format( "is_redirect :: WebException: {0}", ex.Message ), 2 );
			}

			return( bIsRedirect );
		}

		/**************************************************************************/

		
		
		
		
		/**************************************************************************/

		Boolean is_html_page()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIsHTML = false;
			Regex reIsHTML = new Regex ("^text/html", RegexOptions.IgnoreCase);
			try {
				req = WebRequest.CreateHttp( this.url );
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
				debug_msg( string.Format( "Status: {0}", res.StatusCode ), 2 );
				debug_msg( string.Format( "ContentType: {0}", res.ContentType.ToString() ), 2 );
				if (reIsHTML.IsMatch( res.ContentType.ToString() )) {
					bIsHTML = true;
				}
				res.Close();
			} catch (WebException ex) {
				debug_msg( string.Format( "is_html_page :: WebException: {0}", ex.Message ), 2 );
			}
			return( bIsHTML );
		}


		/**************************************************************************/
		
		Boolean process_html_page()
		{
							
			/*
				HTTP HEADER: Content-Type
				HTTP HEADER: Date
			*/
		
			HttpWebRequest req = null;
			HttpWebResponse res = null;

			try {
				req = WebRequest.CreateHttp( this.url );
				req.Method = "GET";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
			} catch (WebException ex) {
				debug_msg( string.Format( "process_html_page :: WebException: {0}", ex.Message ), 3 );
				debug_msg( string.Format( "process_html_page :: WebException: {0}", this.url ), 3 );
			}

			if (res != null) {
				
				string sRawData = "";
							
				// Status Code
				this.status_code = process_status_code( res.StatusCode );
				debug_msg( string.Format( "Status: {0}", this.status_code ), 2 );

				// Probe HTTP Headers
				foreach (string sHeader in res.Headers) {
					debug_msg( string.Format( "HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader( sHeader ) ), 3 );
				}

				// Stash HTTP Headers
				this.mime_type = res.ContentType;
				this.content_length = res.ContentLength;
				debug_msg( string.Format( "Content-Type: {0}", this.mime_type ), 3 );			
				debug_msg( string.Format( "Content-Length: {0}", this.content_length.ToString() ), 3 );

				// Get Response Body
				debug_msg( string.Format( "MIME TYPE: {0}", this.mime_type ), 3 );
				Stream sStream = res.GetResponseStream();
				StreamReader srRead = new StreamReader (sStream, Encoding.UTF8); // Assume UTF-8
				sRawData = srRead.ReadToEnd();
				//debug_msg( string.Format( "sRawData: {0}", sRawData ), 3 );

				if (sRawData.Length > 0) {
					this.htmlDoc = new HtmlDocument ();
					this.htmlDoc.LoadHtml( sRawData );
					debug_msg( string.Format( "htmlDoc: {0}", this.htmlDoc ), 3 );
				} else {
					debug_msg( string.Format( "sRawData: {0}", "EMPTY" ), 3 );
				}

				if (this.htmlDoc != null) {

					{ // Title
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode( "/html/head/title" );
						if (nNode != null) {
							this.title = nNode.InnerText;
							debug_msg( string.Format( "TITLE: {0}", this.title ), 3 );
						} else {
							debug_msg( string.Format( "TITLE: {0}", "MISSING" ), 3 );
						}
					}

					{ // Description
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='description']" );
						if (nNode != null) {
							this.description = nNode.GetAttributeValue( "content", null );
							debug_msg( string.Format( "DESCRIPTION: {0}", this.description ), 3 );
						} else {
							this.description = null;		
							debug_msg( string.Format( "DESCRIPTION: {0}", "MISSING" ), 3 );
						}
					}
						
					{ // Keywords
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='keywords']" );
						if (nNode != null) {
							this.keywords = nNode.GetAttributeValue( "content", null );
							debug_msg( string.Format( "KEYWORDS: {0}", this.keywords ), 3 );
						} else {
							this.keywords = null;		
							debug_msg( string.Format( "KEYWORDS: {0}", "MISSING" ), 3 );
						}
					}

					{ // Outlinks
						HtmlNodeCollection nOutlinks = this.htmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
						if (nOutlinks != null) {
							foreach (HtmlNode nLink in nOutlinks) {
								string sLinkURL = nLink.GetAttributeValue( "href", null );
								string sLinkURLAbs = MacroscopeURLTools.make_url_absolute( this.url, sLinkURL );

								debug_msg( string.Format( "sLinkURL: {0}", sLinkURL.ToString() ), 4 );
								debug_msg( string.Format( "Outlink: {0}", sLinkURLAbs.ToString() ), 4 );

								if (this.outlinks.ContainsKey( sLinkURL )) {
									this.outlinks.Remove( sLinkURL );
									this.outlinks.Add( sLinkURL, sLinkURLAbs );
								} else {
									this.outlinks.Add( sLinkURL, sLinkURLAbs );
								}

							}
						}
					}

				}
				
				res.Close();

			} else {
				this.status_code = 500;
			}

			return( true );
		}

		/**************************************************************************/

		Boolean is_binary_page()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIsBinary = false;
			try {
				req = WebRequest.CreateHttp( this.url );
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
				debug_msg( string.Format( "Status: {0}", res.StatusCode ), 2 );
				debug_msg( string.Format( "ContentType: {0}", res.ContentType.ToString() ), 2 );
				bIsBinary = true;
				res.Close();
			} catch (WebException ex) {
				debug_msg( string.Format( "is_binary_page :: WebException: {0}", ex.Message ), 2 );
			}
			return( bIsBinary );
		}

		/**************************************************************************/
		
		Boolean process_binary_page()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;

			try {
				req = WebRequest.CreateHttp( this.url );
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
			} catch (WebException ex) {
				debug_msg( string.Format( "process_html_page :: WebException: {0}", ex.Message ), 3 );
				debug_msg( string.Format( "process_html_page :: WebException: {0}", this.url ), 3 );
			}

			if (res != null) {
											
				// Status Code
				this.status_code = process_status_code( res.StatusCode );
				debug_msg( string.Format( "Status: {0}", this.status_code ), 2 );

				// Probe HTTP Headers
				foreach (string sHeader in res.Headers) {
					debug_msg( string.Format( "HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader( sHeader ) ), 3 );
				}

				// Stash HTTP Headers
				this.mime_type = res.ContentType;
				this.content_length = res.ContentLength;
				debug_msg( string.Format( "Content-Type: {0}", this.mime_type ), 3 );			
				debug_msg( string.Format( "Content-Length: {0}", this.content_length.ToString() ), 3 );

				{ // Title
					MatchCollection reMatches = Regex.Matches( this.url, "/([^/]+)$" );
					string sTitle = null;
					foreach (Match match in reMatches) {
						if (match.Groups[ 0 ].Value.Length > 0) {
							sTitle = match.Groups[ 0 ].Value.ToString();
							break;
						}
					}
					if (sTitle != null) {
						this.title = sTitle;
						debug_msg( string.Format( "TITLE: {0}", this.title ), 3 );
					} else {
						debug_msg( string.Format( "TITLE: {0}", "MISSING" ), 3 );
					}
				}

				res.Close();

			} else {
				this.status_code = 500;
			}

			return( true );
		}

		/**************************************************************************/

		int process_status_code( HttpStatusCode status )
		{
			int iStatus = 0;
			switch (status) {
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
