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

	public class MacroscopeDocument : Macroscope
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

		public Hashtable inlinks;
		public Hashtable outlinks;

		public string title;
		public string description;
		public string keywords;

		public ArrayList headings1;
		public ArrayList headings2;

		public int depth;
		
		/**************************************************************************/

		public MacroscopeDocument(string sURL)
		{
			url = sURL;
			timeout = 10000;
			is_redirect = false;
			status_code = 0;
			mime_type = null;
			locale = "null";
			date_server = new DateTime();
			date_modified = new DateTime();
			hreflang = new Hashtable();
			inlinks = new Hashtable();
			outlinks = new Hashtable();
			depth = MacroscopeURLTools.find_url_depth(url);
		}
		
		/**************************************************************************/

		public string get_url()
		{
			return(this.url);
		}

		/**************************************************************************/

		public string get_is_redirect()
		{
			return(this.is_redirect.ToString());
		}

		/**************************************************************************/
		
		public int get_status_code()
		{
			return(this.status_code);
		}
		
		/**************************************************************************/
		
		public string get_date_server()
		{
			return(this.date_server.ToShortDateString());
		}
		
						/**************************************************************************/
		
		public string get_date_modified()
		{
			return(this.date_modified.ToShortDateString());
		}
				
		/**************************************************************************/
				
		public Hashtable add_inlink(string sURL)
		{
			if (this.inlinks.ContainsKey(sURL)) {
				int count = (int)this.inlinks[sURL] + 1;
				this.inlinks[sURL] = count;
			} else {
				this.inlinks.Add(sURL, 1);
			}
			return(this.inlinks);
		}

		/**************************************************************************/

		public Hashtable get_inlinks()
		{
			return(this.inlinks);
		}

		/**************************************************************************/

		public Hashtable get_outlinks()
		{
			return(this.outlinks);
		}
		
		/**************************************************************************/

		void set_hreflang(string sLocale, string sURL)
		{
			MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang(false, sLocale, sURL);
			this.hreflang[sLocale] = msHrefLang;
		}

		/**************************************************************************/

		public Hashtable get_hreflangs()
		{
			return(this.hreflang);
		}

		/**************************************************************************/

		public Boolean execute()
		{

			if (this.is_redirect_page()) {
				debug_msg(string.Format("IS REDIRECT: {0}", this.url), 2);
				this.is_redirect = true;
			} 

			if (this.is_html_page()) {
				debug_msg(string.Format("IS HTML PAGE: {0}", this.url), 2);
				this.process_html_page();

			} else if (this.is_pdf_page()) {
				debug_msg(string.Format("IS PDF PAGE: {0}", this.url), 2);
				this.process_pdf_page();

			} else if (this.is_binary_page()) {
				debug_msg(string.Format("IS BINARY PAGE: {0}", this.url), 2);
				this.process_binary_page();

			} else {
				debug_msg(string.Format("UNKNOWN PAGE TYPE: {0}", this.url), 2);
			}
			
			return(true);

		}

		/**************************************************************************/

		Boolean is_redirect_page()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIsRedirect = false;
			string sOriginalURL = this.url;

			try {

				req = WebRequest.CreateHttp(this.url);
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				req.AllowAutoRedirect = false;
				res = (HttpWebResponse)req.GetResponse();

				debug_msg(string.Format("Status: {0}", res.StatusCode), 2);

				if (res.StatusCode == HttpStatusCode.Moved) {
					bIsRedirect = true;
				} else if (res.StatusCode == HttpStatusCode.MovedPermanently) {
					bIsRedirect = true;
				}
			
				if (bIsRedirect) {
					this.is_redirect = true;
					this.url = res.GetResponseHeader("Location");
					this.url_redirect_from = sOriginalURL;
				}
				res.Close();

			} catch (WebException ex) {
				debug_msg(string.Format("is_redirect :: WebException: {0}", ex.Message), 2);
			}

			return(bIsRedirect);
		}

		/**************************************************************************/

		Boolean is_html_page()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIs = false;
			Regex reIs = new Regex("^text/html", RegexOptions.IgnoreCase);
			try {
				req = WebRequest.CreateHttp(this.url);
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
				debug_msg(string.Format("Status: {0}", res.StatusCode), 2);
				debug_msg(string.Format("ContentType: {0}", res.ContentType.ToString()), 2);
				if (reIs.IsMatch(res.ContentType.ToString())) {
					bIs = true;
				}
				res.Close();
			} catch (WebException ex) {
				debug_msg(string.Format("is_html_page :: WebException: {0}", ex.Message), 2);
			}
			return(bIs);
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
				req = WebRequest.CreateHttp(this.url);
				req.Method = "GET";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
			} catch (WebException ex) {
				debug_msg(string.Format("process_html_page :: WebException: {0}", ex.Message), 3);
				debug_msg(string.Format("process_html_page :: WebException: {0}", this.url), 3);
			}

			if (res != null) {
				
				string sRawData = "";
							
				// Status Code
				this.status_code = process_status_code(res.StatusCode);
				debug_msg(string.Format("Status: {0}", this.status_code), 2);

				// Probe HTTP Headers
				foreach (string sHeader in res.Headers) {
					debug_msg(string.Format("HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader(sHeader)), 3);
					if (sHeader.Equals("Date")) {
						this.date_server = DateTime.Parse(res.GetResponseHeader(sHeader));
					}
				}

				// Stash HTTP Headers
				this.mime_type = res.ContentType;
				this.content_length = res.ContentLength;
				debug_msg(string.Format("Content-Type: {0}", this.mime_type), 3);
				debug_msg(string.Format("Content-Length: {0}", this.content_length.ToString()), 3);

				// Get Response Body
				debug_msg(string.Format("MIME TYPE: {0}", this.mime_type), 3);
				Stream sStream = res.GetResponseStream();
				StreamReader srRead = new StreamReader(sStream, Encoding.UTF8); // Assume UTF-8
				sRawData = srRead.ReadToEnd();
				this.content_length = sRawData.Length; // May need to find bytes length
				//debug_msg( string.Format( "sRawData: {0}", sRawData ), 3 );

				if (sRawData.Length > 0) {
					this.htmlDoc = new HtmlDocument();
					this.htmlDoc.LoadHtml(sRawData);
					debug_msg(string.Format("htmlDoc: {0}", this.htmlDoc), 3);
				} else {
					debug_msg(string.Format("sRawData: {0}", "EMPTY"), 3);
				}

				if (this.htmlDoc != null) {

					{ // Probe Locale
						MacroscopeLocaleTools msLocale = new MacroscopeLocaleTools();
						this.locale = msLocale.probe_locale(this.htmlDoc);
						this.set_hreflang(this.locale, this.url);
					}

					{ // Canonical
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode("/html/head/link[@rel='canonical']");
						if (nNode != null) {
							this.canonical = nNode.GetAttributeValue("href", null);
							debug_msg(string.Format("CANONICAL: {0}", this.canonical), 3);
						} else {
							this.canonical = null;		
							debug_msg(string.Format("CANONICAL: {0}", "MISSING"), 3);
						}
					}

					{ // Title
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode("/html/head/title");
						if (nNode != null) {
							this.title = nNode.InnerText;
							debug_msg(string.Format("TITLE: {0}", this.title), 3);
						} else {
							debug_msg(string.Format("TITLE: {0}", "MISSING"), 3);
						}
					}

					{ // Description
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode("/html/head/meta[@name='description']");
						if (nNode != null) {
							this.description = nNode.GetAttributeValue("content", null);
							debug_msg(string.Format("DESCRIPTION: {0}", this.description), 3);
						} else {
							this.description = null;		
							debug_msg(string.Format("DESCRIPTION: {0}", "MISSING"), 3);
						}
					}
						
					{ // Keywords
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode("/html/head/meta[@name='keywords']");
						if (nNode != null) {
							this.keywords = nNode.GetAttributeValue("content", null);
							debug_msg(string.Format("KEYWORDS: {0}", this.keywords), 3);
						} else {
							this.keywords = null;		
							debug_msg(string.Format("KEYWORDS: {0}", "MISSING"), 3);
						}
					}

					{ // Outlinks
						HtmlNodeCollection nOutlinks = this.htmlDoc.DocumentNode.SelectNodes("//a[@href]");
						if (nOutlinks != null) {

							foreach (HtmlNode nLink in nOutlinks) {

								string sLinkURL = nLink.GetAttributeValue("href", null);
								string sLinkURLAbs = MacroscopeURLTools.make_url_absolute(this.url, sLinkURL);

								//debug_msg( string.Format( "sLinkURL: {0}", sLinkURL.ToString() ), 4 );
								//debug_msg( string.Format( "Outlink: {0}", sLinkURLAbs.ToString() ), 4 );

								if (this.outlinks.ContainsKey(sLinkURL)) {
									this.outlinks.Remove(sLinkURL);
									this.outlinks.Add(sLinkURL, sLinkURLAbs);
								} else {
									this.outlinks.Add(sLinkURL, sLinkURLAbs);
								}

							}
							
						}
						
					}

					{ // HREFLANG Alternatives
						this.probe_hreflang_alternates();
					}

				}
				
				res.Close();

			} else {
				this.status_code = 500;
			}

			return(true);
		}

		/**************************************************************************/

		Boolean is_pdf_page()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIs = false;
			Regex reIs = new Regex("^application/pdf", RegexOptions.IgnoreCase);
			try {
				req = WebRequest.CreateHttp(this.url);
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
				debug_msg(string.Format("Status: {0}", res.StatusCode), 2);
				debug_msg(string.Format("ContentType: {0}", res.ContentType.ToString()), 2);
				if (reIs.IsMatch(res.ContentType.ToString())) {
					bIs = true;
				}
				res.Close();
			} catch (WebException ex) {
				debug_msg(string.Format("is_pdf_page :: WebException: {0}", ex.Message), 2);
			}
			return(bIs);
		}

		/**************************************************************************/
		
		Boolean process_pdf_page()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;

			try {
				req = WebRequest.CreateHttp(this.url);
				req.Method = "GET";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
			} catch (WebException ex) {
				debug_msg(string.Format("process_html_page :: WebException: {0}", ex.Message), 3);
				debug_msg(string.Format("process_html_page :: WebException: {0}", this.url), 3);
			}

			if (res != null) {

				MacroscopePDFTools pdfTools;
				
				{ // Get Response Body
					Stream sStream = res.GetResponseStream();
					List<byte> aRawDataList = new List<byte>();
					byte[] aRawData;
					do {
						int buf = sStream.ReadByte();
						if (buf > -1) {
							aRawDataList.Add((byte)buf);
						} else {
							break;
						}
					} while(sStream.CanRead);
					aRawData = aRawDataList.ToArray();
					this.content_length = aRawData.Length;
					pdfTools = new MacroscopePDFTools(aRawData);
				}

				// Status Code
				this.status_code = process_status_code(res.StatusCode);
				debug_msg(string.Format("Status: {0}", this.status_code), 2);

				{ // Probe Locale
					this.locale = "en"; // Implement locale probing
					this.set_hreflang(this.locale, this.url);
				}
				
				{ // Canonical
					this.canonical = this.url;
					debug_msg(string.Format("CANONICAL: {0}", this.canonical), 3);
				}
				
				// Probe HTTP Headers
				foreach (string sHeader in res.Headers) {
					debug_msg(string.Format("HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader(sHeader)), 3);
				}

				// Stash HTTP Headers
				this.mime_type = res.ContentType;
				this.content_length = res.ContentLength;
				debug_msg(string.Format("Content-Type: {0}", this.mime_type), 3);			
				debug_msg(string.Format("Content-Length: {0}", this.content_length.ToString()), 3);

				{ // Title
					if (pdfTools != null) {
						string sTitle = pdfTools.get_title();
						if (sTitle != null) {
							this.title = sTitle;
							debug_msg(string.Format("TITLE: {0}", this.title), 3);
						} else {
							debug_msg(string.Format("TITLE: {0}", "MISSING"), 3);
						}
					}
				}
				
				res.Close();

			} else {
				this.status_code = 500;
			}

			return(true);
		}

		/**************************************************************************/
		
		Boolean is_binary_page()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIs = false;
			try {
				req = WebRequest.CreateHttp(this.url);
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
				debug_msg(string.Format("Status: {0}", res.StatusCode), 2);
				debug_msg(string.Format("ContentType: {0}", res.ContentType.ToString()), 2);
				bIs = true;
				res.Close();
			} catch (WebException ex) {
				debug_msg(string.Format("is_binary_page :: WebException: {0}", ex.Message), 2);
			}
			return(bIs);
		}

		/**************************************************************************/
		
		Boolean process_binary_page()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;

			try {
				req = WebRequest.CreateHttp(this.url);
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
			} catch (WebException ex) {
				debug_msg(string.Format("process_html_page :: WebException: {0}", ex.Message), 3);
				debug_msg(string.Format("process_html_page :: WebException: {0}", this.url), 3);
			}

			if (res != null) {
											
				// Status Code
				this.status_code = process_status_code(res.StatusCode);
				debug_msg(string.Format("Status: {0}", this.status_code), 2);

				// Probe HTTP Headers
				foreach (string sHeader in res.Headers) {
					debug_msg(string.Format("HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader(sHeader)), 3);
				}

				// Stash HTTP Headers
				this.mime_type = res.ContentType;
				this.content_length = res.ContentLength;
				debug_msg(string.Format("Content-Type: {0}", this.mime_type), 3);			
				debug_msg(string.Format("Content-Length: {0}", this.content_length.ToString()), 3);

				{ // Title
					MatchCollection reMatches = Regex.Matches(this.url, "/([^/]+)$");
					string sTitle = null;
					foreach (Match match in reMatches) {
						if (match.Groups[0].Value.Length > 0) {
							sTitle = match.Groups[0].Value.ToString();
							break;
						}
					}
					if (sTitle != null) {
						this.title = sTitle;
						debug_msg(string.Format("TITLE: {0}", this.title), 3);
					} else {
						debug_msg(string.Format("TITLE: {0}", "MISSING"), 3);
					}
				}

				res.Close();

			} else {
				this.status_code = 500;
			}

			return(true);
		}

		/**************************************************************************/

		int process_status_code(HttpStatusCode status)
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
			return(iStatus);
		}

		/**************************************************************************/

		void probe_hreflang_alternates()
		{
			HtmlNodeCollection nlNodeList = this.htmlDoc.DocumentNode.SelectNodes("//link[@rel='alternate']");
			if (nlNodeList != null) {
				foreach (HtmlNode nNode in nlNodeList) {
					string sRel = nNode.GetAttributeValue("rel", "");
					string sLocale = nNode.GetAttributeValue("hreflang", "");
					string sHref = nNode.GetAttributeValue("href", "");
					if (sLocale == "") {
						continue;
					} else {
						if (this.url == sHref) {
							sLocale = this.locale;
						}
						debug_msg(string.Format("HREFLANG: {0}, {1}", sLocale, sHref), 3);
						MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang(this.probe_hreflangs, sLocale, sHref);
						this.hreflang[sLocale] = msHrefLang;
					}
				}
			}
		}

		/**************************************************************************/

	}

}
