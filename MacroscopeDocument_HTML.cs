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

		Boolean IsHtmlPage()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIs = false;
			Regex reIs = new Regex ( "^text/html", RegexOptions.IgnoreCase );
			try {
				req = WebRequest.CreateHttp( this.url );
				req.Method = "HEAD";
				req.Timeout = this.timeout;
				req.KeepAlive = false;
				res = ( HttpWebResponse )req.GetResponse();
				debug_msg( string.Format( "Status: {0}", res.StatusCode ), 2 );
				debug_msg( string.Format( "ContentType: {0}", res.ContentType.ToString() ), 2 );
				if( reIs.IsMatch( res.ContentType.ToString() ) ) {
					bIs = true;
				}
				res.Close();
//			} catch( UriFormatException ex ) {
//				debug_msg( string.Format( "IsHtmlPage :: UriFormatException: {0}", ex.Message ), 2 );
			} catch( WebException ex ) {
				debug_msg( string.Format( "IsHtmlPage :: WebException: {0}", ex.Message ), 2 );
			}
			return( bIs );
		}

		/**************************************************************************/
		
		Boolean ProcessHtmlPage()
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
				res = ( HttpWebResponse )req.GetResponse();
			} catch( WebException ex ) {
				debug_msg( string.Format( "ProcessHtmlPage :: WebException: {0}", ex.Message ), 3 );
				debug_msg( string.Format( "ProcessHtmlPage :: WebException: {0}", this.url ), 3 );
			}

			if( res != null ) {
				
				string sRawData = "";
							
				// Status Code
				this.status_code = this.ProcessStatusCode( res.StatusCode );
				debug_msg( string.Format( "Status: {0}", this.status_code ), 2 );

				// Probe HTTP Headers
				foreach( string sHeader in res.Headers ) {
					debug_msg( string.Format( "HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader( sHeader ) ), 3 );
					if( sHeader.Equals( "Date" ) ) {
						this.date_server = DateTime.Parse( res.GetResponseHeader( sHeader ) );
					}
				}

				// Stash HTTP Headers
				this.mime_type = res.ContentType;
				this.content_length = res.ContentLength;
				debug_msg( string.Format( "Content-Type: {0}", this.mime_type ), 3 );
				debug_msg( string.Format( "Content-Length: {0}", this.content_length.ToString() ), 3 );

				// Get Response Body
				debug_msg( string.Format( "MIME TYPE: {0}", this.mime_type ), 3 );
				Stream sStream = res.GetResponseStream();
				StreamReader srRead = new StreamReader ( sStream, Encoding.UTF8 ); // Assume UTF-8
				sRawData = srRead.ReadToEnd();
				this.content_length = sRawData.Length; // May need to find bytes length
				//debug_msg( string.Format( "sRawData: {0}", sRawData ), 3 );

				if( sRawData.Length > 0 ) {
					this.htmlDoc = new HtmlDocument ();
					this.htmlDoc.LoadHtml( sRawData );
					debug_msg( string.Format( "htmlDoc: {0}", this.htmlDoc ), 3 );
				} else {
					debug_msg( string.Format( "sRawData: {0}", "EMPTY" ), 3 );
				}

				if( this.htmlDoc != null ) {

					{ // Probe Locale
						MacroscopeLocaleTools msLocale = new MacroscopeLocaleTools ();
						this.locale = msLocale.probe_locale( this.htmlDoc );
						this.SetHreflang( this.locale, this.url );
					}

					{ // Canonical
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode( "/html/head/link[@rel='canonical']" );
						if( nNode != null ) {
							this.canonical = nNode.GetAttributeValue( "href", null );
							debug_msg( string.Format( "CANONICAL: {0}", this.canonical ), 3 );
						} else {
							this.canonical = null;		
							debug_msg( string.Format( "CANONICAL: {0}", "MISSING" ), 3 );
						}
					}

					{ // Title
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode( "/html/head/title" );
						if( nNode != null ) {
							this.title = nNode.InnerText;
							debug_msg( string.Format( "TITLE: {0}", this.title ), 3 );
						} else {
							debug_msg( string.Format( "TITLE: {0}", "MISSING" ), 3 );
						}
					}

					{ // Description
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='description']" );
						if( nNode != null ) {
							this.description = nNode.GetAttributeValue( "content", null );
							debug_msg( string.Format( "DESCRIPTION: {0}", this.description ), 3 );
						} else {
							this.description = null;		
							debug_msg( string.Format( "DESCRIPTION: {0}", "MISSING" ), 3 );
						}
					}
						
					{ // Keywords
						HtmlNode nNode = this.htmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='keywords']" );
						if( nNode != null ) {
							this.keywords = nNode.GetAttributeValue( "content", null );
							debug_msg( string.Format( "KEYWORDS: {0}", this.keywords ), 3 );
						} else {
							this.keywords = null;		
							debug_msg( string.Format( "KEYWORDS: {0}", "MISSING" ), 3 );
						}
					}

					{ // Outlinks
						this.process_html_outhyperlinks();
						this.process_html_outlinks();
					}

					{ // HREFLANG Alternatives
						this.probe_hreflang_alternates();
					}

				}
				
				res.Close();

			} else {
				this.status_code = 500;
			}

			return( true );
		}

		/**************************************************************************/

		void process_html_outhyperlinks()
		{

			HtmlNodeCollection nOutlinks = this.htmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

			if( nOutlinks != null ) {

				foreach( HtmlNode nLink in nOutlinks ) {

					string sLinkURL = nLink.GetAttributeValue( "href", null );
					string sLinkURLAbs = MacroscopeURLTools.make_url_absolute( this.url, sLinkURL );

					if( this.outhyperlinks.ContainsKey( sLinkURL ) ) {
						this.outhyperlinks.Remove( sLinkURL );
						this.outhyperlinks.Add( sLinkURL, sLinkURLAbs );
					} else {
						this.outhyperlinks.Add( sLinkURL, sLinkURLAbs );
					}

				}
							
			}
						
		}
				
		/**************************************************************************/

		void process_html_outlinks()
		{

			{ // Normal A HREF links
				HtmlNodeCollection nOutlinks = this.htmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkURL = nLink.GetAttributeValue( "href", null );
						string sLinkURLAbs = MacroscopeURLTools.make_url_absolute( this.url, sLinkURL );
						if( this.outlinks.ContainsKey( sLinkURL ) ) {
							this.outlinks.Remove( sLinkURL );
							this.outlinks.Add( sLinkURL, sLinkURLAbs );
						} else {
							this.outlinks.Add( sLinkURL, sLinkURLAbs );
						}
					}
				}
			}

			{ // LINK element links
				HtmlNodeCollection nOutlinks = this.htmlDoc.DocumentNode.SelectNodes( "//link[@href]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkURL = nLink.GetAttributeValue( "href", null );
						string sLinkURLAbs = MacroscopeURLTools.make_url_absolute( this.url, sLinkURL );
						if( this.outlinks.ContainsKey( sLinkURL ) ) {
							this.outlinks.Remove( sLinkURL );
							this.outlinks.Add( sLinkURL, sLinkURLAbs );
						} else {
							this.outlinks.Add( sLinkURL, sLinkURLAbs );
						}
					}
				}
			}

			{ // Image element links
				HtmlNodeCollection nOutlinks = this.htmlDoc.DocumentNode.SelectNodes( "//img[@src]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkURL = nLink.GetAttributeValue( "src", null );
						string sLinkURLAbs = MacroscopeURLTools.make_url_absolute( this.url, sLinkURL );
						if( this.outlinks.ContainsKey( sLinkURL ) ) {
							this.outlinks.Remove( sLinkURL );
							this.outlinks.Add( sLinkURL, sLinkURLAbs );
						} else {
							this.outlinks.Add( sLinkURL, sLinkURLAbs );
						}
					}
				}
			}

			{ // Script element links
				HtmlNodeCollection nOutlinks = this.htmlDoc.DocumentNode.SelectNodes( "//script[@src]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkURL = nLink.GetAttributeValue( "src", null );
						string sLinkURLAbs = MacroscopeURLTools.make_url_absolute( this.url, sLinkURL );
						if( this.outlinks.ContainsKey( sLinkURL ) ) {
							this.outlinks.Remove( sLinkURL );
							this.outlinks.Add( sLinkURL, sLinkURLAbs );
						} else {
							this.outlinks.Add( sLinkURL, sLinkURLAbs );
						}
					}
				}
			}
			
		}

		/**************************************************************************/

		void probe_hreflang_alternates()
		{
			HtmlNodeCollection nlNodeList = this.htmlDoc.DocumentNode.SelectNodes( "//link[@rel='alternate']" );
			if( nlNodeList != null ) {
				foreach( HtmlNode nNode in nlNodeList ) {
					string sRel = nNode.GetAttributeValue( "rel", "" );
					string sLocale = nNode.GetAttributeValue( "hreflang", "" );
					string sHref = nNode.GetAttributeValue( "href", "" );
					if( sLocale == "" ) {
						continue;
					} else {
						if( this.url == sHref ) {
							sLocale = this.locale;
						}
						debug_msg( string.Format( "HREFLANG: {0}, {1}", sLocale, sHref ), 3 );
						MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang ( this.probe_hreflangs, sLocale, sHref );
						this.hreflang[ sLocale ] = msHrefLang;
					}
				}
			}
		}

		/**************************************************************************/

	}

}
