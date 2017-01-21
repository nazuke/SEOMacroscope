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
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Net;
using HtmlAgilityPack;

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
			string sErrorCondition = null;
						
			try {
				
				req = WebRequest.CreateHttp( this.Url );
				req.Method = "HEAD";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				MacroscopePreferencesManager.EnableHttpProxy( req );

				try {
					res = ( HttpWebResponse )req.GetResponse();
				} catch( WebException ex ) {
					DebugMsg( string.Format( "IsHtmlPage :: WebException: {0}", ex.Message ) );
					DebugMsg( string.Format( "IsHtmlPage :: WebExceptionStatus: {0}", ex.Status ) );
					sErrorCondition = ex.Status.ToString();
				}

				if( res != null ) {
					this.ProcessHttpHeaders( req, res );

					DebugMsg( string.Format( "Status: {0}", res.StatusCode ) );
					DebugMsg( string.Format( "ContentType: {0}", res.ContentType.ToString() ) );

					if( reIs.IsMatch( res.ContentType.ToString() ) ) {
						bIs = true;
						this.IsHtml = true;
					}

					res.Close();

				}

//			} catch( UriFormatException ex ) {
//				DebugMsg( string.Format( "IsHtmlPage :: UriFormatException: {0}", ex.Message ) );
			
			} catch( WebException ex ) {
				DebugMsg( string.Format( "IsHtmlPage :: WebException: {0}", ex.Message ) );
				DebugMsg( string.Format( "IsHtmlPage :: WebExceptionStatus: {0}", ex.Status ) );
				sErrorCondition = ex.Status.ToString();
			}

			if( sErrorCondition != null ) {
				this.StatusCode = 500;
				this.ErrorCondition = sErrorCondition;
			}

			return( bIs );
		}

		/**************************************************************************/
		
		void ProcessHtmlPage ()
		{
							
			/*
				HTTP HEADER: Content-Type
				HTTP HEADER: Date
			*/
		
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			string sErrorCondition = null;
			
			try {
				
				req = WebRequest.CreateHttp( this.Url );
				req.Method = "GET";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				MacroscopePreferencesManager.EnableHttpProxy( req );
				res = ( HttpWebResponse )req.GetResponse();

			} catch( WebException ex ) {
				DebugMsg( string.Format( "ProcessHtmlPage :: WebException: {0}", ex.Message ) );
				DebugMsg( string.Format( "ProcessHtmlPage :: WebException: {0}", this.Url ) );
				DebugMsg( string.Format( "IsRedirectPage :: WebExceptionStatus: {0}", ex.Status ) );
				sErrorCondition = ex.Status.ToString();
			}

			if( res != null ) {
				
				string sRawData = "";
							
				this.ProcessHttpHeaders( req, res );

				// Get Response Body
				try {
					DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );
					Stream sStream = res.GetResponseStream();
					StreamReader srRead = new StreamReader ( sStream, Encoding.UTF8 ); // Assume UTF-8
					sRawData = srRead.ReadToEnd();
					this.ContentLength = sRawData.Length; // May need to find bytes length
					//DebugMsg( string.Format( "sRawData: {0}", sRawData ) );
				} catch( WebException ex ) {
					DebugMsg( string.Format( "WebException", ex.Message ) );
					this.StatusCode = 500;
					sRawData = "";
					this.ContentLength = 0;
				}
				
				if( sRawData.Length > 0 ) {
					this.HtmlDoc = new HtmlDocument ();
					this.HtmlDoc.LoadHtml( sRawData );
					DebugMsg( string.Format( "htmlDoc: {0}", this.HtmlDoc ) );
				} else {
					DebugMsg( string.Format( "sRawData: {0}", "EMPTY" ) );
				}

				if( this.HtmlDoc != null ) {

					{ // Probe Locale
						MacroscopeLocaleTools msLocale = new MacroscopeLocaleTools ();
						this.Locale = msLocale.ProbeLocale( this.HtmlDoc );
						this.SetHreflang( this.Locale, this.Url );
					}

					{ // Canonical
						HtmlNode nNode = this.HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/link[@rel='canonical']" );
						if( nNode != null ) {
							this.Canonical = nNode.GetAttributeValue( "href", "" );
							DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
						} else {
							this.Canonical = "";		
							DebugMsg( string.Format( "CANONICAL: {0}", "MISSING" ) );
						}
					}

					{ // Title
						HtmlNode nNode = this.HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/title" );
						if( nNode != null ) {
							this.Title = nNode.InnerText;
							DebugMsg( string.Format( "TITLE: {0}", this.Title ) );
						} else {
							DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
						}
					}

					{ // Description
						HtmlNode nNode = this.HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='description']" );
						if( nNode != null ) {
							this.Description = nNode.GetAttributeValue( "content", null );
							DebugMsg( string.Format( "DESCRIPTION: {0}", this.Description ) );
						} else {
							this.Description = null;		
							DebugMsg( string.Format( "DESCRIPTION: {0}", "MISSING" ) );
						}
					}
						
					{ // Keywords
						HtmlNode nNode = this.HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='keywords']" );
						if( nNode != null ) {
							this.Keywords = nNode.GetAttributeValue( "content", null );
							DebugMsg( string.Format( "KEYWORDS: {0}", this.Keywords ) );
						} else {
							this.Keywords = null;		
							DebugMsg( string.Format( "KEYWORDS: {0}", "MISSING" ) );
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

			if( sErrorCondition != null ) {
				this.StatusCode = 500;
				this.ErrorCondition = sErrorCondition;
			}
			
		}

		/**************************************************************************/

		void ProcessHtmlHyperlinksOut ()
		{

			// TODO: Add image links
				
			HtmlNodeCollection nOutlinks = this.HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

			if( nOutlinks != null ) {

				foreach( HtmlNode nLink in nOutlinks ) {

					string sLinkURL = nLink.GetAttributeValue( "href", null );
					string sLinkURLAbs = MacroscopeURLTools.MakeUrlAbsolute( this.Url, sLinkURL );

					MacroscopeHyperlinkOut hlHyperlinkOut = this.HyperlinksOut.Add( this.Url, sLinkURLAbs );

					{
						string sFollow = nLink.GetAttributeValue( "rel", null );
						if( sFollow != null ) {
							if( sFollow.ToLower().Equals( "nofollow" ) ) {
								hlHyperlinkOut.SetFollow( false );
							}
						} 
					}

					{
						string sLinkText = nLink.InnerText;
						if( sLinkText != null ) {
							if( sLinkText.Length > 0 ) {
								hlHyperlinkOut.SetLinkText( sLinkText );
							}
						}
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
		
		void ProbeHrefLangAlternates ()
		{

			HtmlNodeCollection nlNodeList = this.HtmlDoc.DocumentNode.SelectNodes( "//link[@rel='alternate']" );

			if( nlNodeList != null ) {

				foreach( HtmlNode nNode in nlNodeList ) {

					MacroscopeHrefLang msHrefLang;
					string sRel = nNode.GetAttributeValue( "rel", "" );
					string sLocale = nNode.GetAttributeValue( "hreflang", "" );
					string sHref = nNode.GetAttributeValue( "href", "" );

					if( sLocale == "" ) {
						continue;
					} else {

						if( this.Url == sHref ) {
							sLocale = this.Locale;
						}
						
						DebugMsg( string.Format( "HREFLANG: {0}, {1}", sLocale, sHref ) );
						
						msHrefLang = new MacroscopeHrefLang ( sLocale, sHref );
						
						this.HrefLang[sLocale] = msHrefLang;

					}

				}

			}

		}
		
		/**************************************************************************/

		void ExtractHtmlHeadings ()
		{
			for( ushort iLevel = 1; iLevel <= 6; iLevel++ ) {
				HtmlNodeCollection nNodes = this.HtmlDoc.DocumentNode.SelectNodes( string.Format( "//h{0}", iLevel ) );
				if( nNodes != null ) {
					foreach( HtmlNode nNode in nNodes ) {
						string sText = nNode.InnerText;
						if( sText != null ) {
							this.AddHeading( iLevel, sText );
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
					if( sLinkURL != null ) {
						if( Regex.IsMatch( sLinkURL, "^mailto:" ) ) {
							MatchCollection reMatches = Regex.Matches( sLinkURL, "^mailto:([^?]+)" );
							foreach( Match reMatch in reMatches ) {
								this.AddEmailAddress( reMatch.Groups[1].Value.ToString() );
							}
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
					if( sLinkURL != null ) {
						if( Regex.IsMatch( sLinkURL, "^tel:" ) ) {
							MatchCollection reMatches = Regex.Matches( sLinkURL, "^tel:(.+)" );
							foreach( Match reMatch in reMatches ) {
								this.AddTelephoneNumber( reMatch.Groups[1].Value.ToString() );
							}
						}
					}
				}
			}
		}

		/**************************************************************************/

	}

}
