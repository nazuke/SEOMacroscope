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

		Boolean IsHtmlPage ()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIs = false;
			Regex reIs = new Regex ( "^text/html", RegexOptions.IgnoreCase );
			try {
				req = WebRequest.CreateHttp( this.Url );
				req.Method = "HEAD";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				res = ( HttpWebResponse )req.GetResponse();
				debug_msg( string.Format( "Status: {0}", res.StatusCode ), 2 );
				debug_msg( string.Format( "ContentType: {0}", res.ContentType.ToString() ), 2 );
				if( reIs.IsMatch( res.ContentType.ToString() ) ) {
					bIs = true;
					this.IsHtml = true;
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
		
		Boolean ProcessHtmlPage ()
		{
							
			/*
				HTTP HEADER: Content-Type
				HTTP HEADER: Date
			*/
		
			HttpWebRequest req = null;
			HttpWebResponse res = null;

			try {
				req = WebRequest.CreateHttp( this.Url );
				req.Method = "GET";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				res = ( HttpWebResponse )req.GetResponse();
			} catch( WebException ex ) {
				debug_msg( string.Format( "ProcessHtmlPage :: WebException: {0}", ex.Message ), 3 );
				debug_msg( string.Format( "ProcessHtmlPage :: WebException: {0}", this.Url ), 3 );
			}

			if( res != null ) {
				
				string sRawData = "";
							
				// Status Code
				this.StatusCode = this.ProcessStatusCode( res.StatusCode );
				debug_msg( string.Format( "Status: {0}", this.StatusCode ), 2 );

				// Probe HTTP Headers
				foreach( string sHeader in res.Headers ) {
					debug_msg( string.Format( "HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader( sHeader ) ), 3 );
					if( sHeader.Equals( "Date" ) ) {
						this.DateServer = DateTime.Parse( res.GetResponseHeader( sHeader ) );
					}
				}

				// Stash HTTP Headers
				this.MimeType = res.ContentType;
				this.ContentLength = res.ContentLength;
				debug_msg( string.Format( "Content-Type: {0}", this.MimeType ), 3 );
				debug_msg( string.Format( "Content-Length: {0}", this.ContentLength.ToString() ), 3 );

				// Get Response Body
				debug_msg( string.Format( "MIME TYPE: {0}", this.MimeType ), 3 );
				Stream sStream = res.GetResponseStream();
				StreamReader srRead = new StreamReader ( sStream, Encoding.UTF8 ); // Assume UTF-8
				sRawData = srRead.ReadToEnd();
				this.ContentLength = sRawData.Length; // May need to find bytes length
				//debug_msg( string.Format( "sRawData: {0}", sRawData ), 3 );

				if( sRawData.Length > 0 ) {
					this.HtmlDoc = new HtmlDocument ();
					this.HtmlDoc.LoadHtml( sRawData );
					debug_msg( string.Format( "htmlDoc: {0}", this.HtmlDoc ), 3 );
				} else {
					debug_msg( string.Format( "sRawData: {0}", "EMPTY" ), 3 );
				}

				if( this.HtmlDoc != null ) {

					{ // Probe Locale
						MacroscopeLocaleTools msLocale = new MacroscopeLocaleTools ();
						this.Locale = msLocale.probe_locale( this.HtmlDoc );
						this.SetHreflang( this.Locale, this.Url );
					}

					{ // Canonical
						HtmlNode nNode = this.HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/link[@rel='canonical']" );
						if( nNode != null ) {
							this.Canonical = nNode.GetAttributeValue( "href", null );
							debug_msg( string.Format( "CANONICAL: {0}", this.Canonical ), 3 );
						} else {
							this.Canonical = null;		
							debug_msg( string.Format( "CANONICAL: {0}", "MISSING" ), 3 );
						}
					}

					{ // Title
						HtmlNode nNode = this.HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/title" );
						if( nNode != null ) {
							this.Title = nNode.InnerText;
							debug_msg( string.Format( "TITLE: {0}", this.Title ), 3 );
						} else {
							debug_msg( string.Format( "TITLE: {0}", "MISSING" ), 3 );
						}
					}

					{ // Description
						HtmlNode nNode = this.HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='description']" );
						if( nNode != null ) {
							this.Description = nNode.GetAttributeValue( "content", null );
							debug_msg( string.Format( "DESCRIPTION: {0}", this.Description ), 3 );
						} else {
							this.Description = null;		
							debug_msg( string.Format( "DESCRIPTION: {0}", "MISSING" ), 3 );
						}
					}
						
					{ // Keywords
						HtmlNode nNode = this.HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='keywords']" );
						if( nNode != null ) {
							this.Keywords = nNode.GetAttributeValue( "content", null );
							debug_msg( string.Format( "KEYWORDS: {0}", this.Keywords ), 3 );
						} else {
							this.Keywords = null;		
							debug_msg( string.Format( "KEYWORDS: {0}", "MISSING" ), 3 );
						}
					}

					{ // Outlinks
						this.ProcessHtmlHyperlinksOut();
						this.ProcessHtmlOutlinks();
					}	
					
					{ // Extract interesting document elements
						this.ExtractHtmlHeadings();
					}
						
					{ // Special Links
						this.ExtractHtmlEmailAddresses();
						this.ExtractHtmlTelephoneNumbers();
					}

					{ // HREFLANG Alternatives
						this.ProbeHrefLangAlternates();
					}

				}
				
				res.Close();

			} else {
				this.StatusCode = 500;
			}

			return( true );
		}

		/**************************************************************************/

		void ProcessHtmlHyperlinksOut ()
		{

			HtmlNodeCollection nOutlinks = this.HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

			if( nOutlinks != null ) {

				foreach( HtmlNode nLink in nOutlinks ) {

					string sLinkURL = nLink.GetAttributeValue( "href", null );
					string sLinkURLAbs = MacroscopeURLTools.MakeUrlAbsolute( this.Url, sLinkURL );

					if( this.HyperlinksOut.ContainsKey( sLinkURL ) ) {
						this.HyperlinksOut.Remove( sLinkURL );
						this.HyperlinksOut.Add( sLinkURL, sLinkURLAbs );
					} else {
						this.HyperlinksOut.Add( sLinkURL, sLinkURLAbs );
					}

				}
							
			}
						
		}
				
		/**************************************************************************/

		void ProcessHtmlOutlinks ()
		{

			{ // Normal A HREF links
				HtmlNodeCollection nOutlinks = this.HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkURL = nLink.GetAttributeValue( "href", null );
						string sLinkURLAbs = MacroscopeURLTools.MakeUrlAbsolute( this.Url, sLinkURL );
						if( this.Outlinks.ContainsKey( sLinkURL ) ) {
							this.Outlinks.Remove( sLinkURL );
							this.Outlinks.Add( sLinkURL, sLinkURLAbs );
						} else {
							this.Outlinks.Add( sLinkURL, sLinkURLAbs );
						}
					}
				}
			}

			{ // LINK element links
				HtmlNodeCollection nOutlinks = this.HtmlDoc.DocumentNode.SelectNodes( "//link[@href]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkURL = nLink.GetAttributeValue( "href", null );
						string sLinkURLAbs = MacroscopeURLTools.MakeUrlAbsolute( this.Url, sLinkURL );
						if( this.Outlinks.ContainsKey( sLinkURL ) ) {
							this.Outlinks.Remove( sLinkURL );
							this.Outlinks.Add( sLinkURL, sLinkURLAbs );
						} else {
							this.Outlinks.Add( sLinkURL, sLinkURLAbs );
						}
					}
				}
			}

			{ // Image element links
				HtmlNodeCollection nOutlinks = this.HtmlDoc.DocumentNode.SelectNodes( "//img[@src]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkURL = nLink.GetAttributeValue( "src", null );
						string sLinkURLAbs = MacroscopeURLTools.MakeUrlAbsolute( this.Url, sLinkURL );
						if( this.Outlinks.ContainsKey( sLinkURL ) ) {
							this.Outlinks.Remove( sLinkURL );
							this.Outlinks.Add( sLinkURL, sLinkURLAbs );
						} else {
							this.Outlinks.Add( sLinkURL, sLinkURLAbs );
						}
					}
				}
			}

			{ // Script element links
				HtmlNodeCollection nOutlinks = this.HtmlDoc.DocumentNode.SelectNodes( "//script[@src]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkURL = nLink.GetAttributeValue( "src", null );
						string sLinkURLAbs = MacroscopeURLTools.MakeUrlAbsolute( this.Url, sLinkURL );
						if( this.Outlinks.ContainsKey( sLinkURL ) ) {
							this.Outlinks.Remove( sLinkURL );
							this.Outlinks.Add( sLinkURL, sLinkURLAbs );
						} else {
							this.Outlinks.Add( sLinkURL, sLinkURLAbs );
						}
					}
				}
			}
			
		}

		/**************************************************************************/

		void ExtractHtmlHeadings ()
		{
			
			{
				HtmlNodeCollection nNodes = this.HtmlDoc.DocumentNode.SelectNodes( "//h1" );
				if( nNodes != null ) {
					foreach( HtmlNode nNode in nNodes ) {
						string sText = nNode.InnerText;
						if( sText != null ) {
							this.AddHeading1( sText );
						}
					}			
				}
			}
			
			{
				HtmlNodeCollection nNodes = this.HtmlDoc.DocumentNode.SelectNodes( "//h2" );
				if( nNodes != null ) {
					foreach( HtmlNode nNode in nNodes ) {
						string sText = nNode.InnerText;
						if( sText != null ) {
							this.AddHeading2( sText );
						}
					}			
				}
			}
			
		}

		/**************************************************************************/
				
		void ExtractHtmlEmailAddresses ()
		{
			HtmlNodeCollection nNodes = this.HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
			if( nNodes != null ) {
				foreach( HtmlNode nLink in nNodes ) {
					string sLinkURL = nLink.GetAttributeValue( "href", null );
					if( Regex.IsMatch( sLinkURL, "^mailto:" ) ) {
						MatchCollection reMatches = Regex.Matches( sLinkURL, "^mailto:([^?]+)" );
						foreach( Match reMatch in reMatches ) {
							this.AddEmailAddress( reMatch.Groups[1].Value.ToString() );
						}
					}
				}			
			}
		}
			
		/**************************************************************************/

		void ExtractHtmlTelephoneNumbers ()
		{
			HtmlNodeCollection nNodes = this.HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
			if( nNodes != null ) {
				foreach( HtmlNode nLink in nNodes ) {
					string sLinkURL = nLink.GetAttributeValue( "href", null );
					if( Regex.IsMatch( sLinkURL, "^tel:" ) ) {
						MatchCollection reMatches = Regex.Matches( sLinkURL, "^tel:(.+)" );
						foreach( Match reMatch in reMatches ) {
							this.AddTelephoneNumber( reMatch.Groups[1].Value.ToString() );
						}
					}
				}			
			}
		}
		
		/**************************************************************************/
		void ProbeHrefLangAlternates ()
		{
			HtmlNodeCollection nlNodeList = this.HtmlDoc.DocumentNode.SelectNodes( "//link[@rel='alternate']" );
			if( nlNodeList != null ) {
				foreach( HtmlNode nNode in nlNodeList ) {
					string sRel = nNode.GetAttributeValue( "rel", "" );
					string sLocale = nNode.GetAttributeValue( "hreflang", "" );
					string sHref = nNode.GetAttributeValue( "href", "" );
					if( sLocale == "" ) {
						continue;
					} else {
						if( this.Url == sHref ) {
							sLocale = this.Locale;
						}
						debug_msg( string.Format( "HREFLANG: {0}, {1}", sLocale, sHref ), 3 );
						MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang ( this.probe_hreflangs, sLocale, sHref );
						this.HrefLang[sLocale] = msHrefLang;
					}
				}
			}
		}

		/**************************************************************************/

	}

}
