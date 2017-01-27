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
		
		void ProcessHtmlPage ()
		{

			HtmlDocument HtmlDoc = null;
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
					sRawData = "";
					this.ContentLength = 0;

				} catch( Exception ex ) {

					DebugMsg( string.Format( "Exception", ex.Message ) );
					this.StatusCode = ( int )HttpStatusCode.BadRequest;
					sRawData = "";
					this.ContentLength = 0;

				}
				
				if( sRawData.Length > 0 ) {
					HtmlDoc = new HtmlDocument ();
					HtmlDoc.LoadHtml( sRawData );
					DebugMsg( string.Format( "htmlDoc: {0}", HtmlDoc ) );
				} else {
					DebugMsg( string.Format( "sRawData: {0}", "EMPTY" ) );
				}

				if( HtmlDoc != null ) {

					{ // Probe Locale
						MacroscopeLocaleTools msLocale = new MacroscopeLocaleTools ();
						this.Locale = msLocale.ProbeLocale( HtmlDoc );
						this.SetHreflang( this.Locale, this.Url );
					}

					{ // Title
						HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/title" );
						if( nNode != null ) {
							this.Title = nNode.InnerText;
							DebugMsg( string.Format( "TITLE: {0}", this.Title ) );
						} else {
							DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
						}
					}

					{ // Description
						HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='description']" );
						if( nNode != null ) {
							this.Description = nNode.GetAttributeValue( "content", null );
							DebugMsg( string.Format( "DESCRIPTION: {0}", this.Description ) );
						} else {
							this.Description = null;		
							DebugMsg( string.Format( "DESCRIPTION: {0}", "MISSING" ) );
						}
					}
						
					{ // Keywords
						HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='keywords']" );
						if( nNode != null ) {
							this.Keywords = nNode.GetAttributeValue( "content", null );
							DebugMsg( string.Format( "KEYWORDS: {0}", this.Keywords ) );
						} else {
							this.Keywords = null;		
							DebugMsg( string.Format( "KEYWORDS: {0}", "MISSING" ) );
						}
					}

					{ // Canonical
						this.ProcessHtmlCanonical( HtmlDoc );
					}
					
					{ // Outlinks
						this.ProcessHtmlHyperlinksOut( HtmlDoc );
						this.ProcessHtmlOutlinks( HtmlDoc );
					}	
					
					{ // Extract interesting document elements
						this.ExtractHtmlHeadings( HtmlDoc );
					}
						
					{ // Special Links
						this.ExtractHtmlEmailAddresses( HtmlDoc );
						this.ExtractHtmlTelephoneNumbers( HtmlDoc );
					}

					{ // HREFLANG Alternatives
						this.ExtractHrefLangAlternates( HtmlDoc );
					}

				}
				
				res.Close();

			}

			if( sErrorCondition != null ) {
				this.ProcessErrorCondition( sErrorCondition );
			}
			
		}

		/**************************************************************************/

		void ProcessHtmlCanonical ( HtmlDocument HtmlDoc )
		{

			HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/link[@rel='canonical']" );

			if( nNode != null ) {

				this.Canonical = nNode.GetAttributeValue( "href", "" );
				DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
				if( MacroscopePreferencesManager.GetFollowCanonicalLinks() ) {
					string sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, this.Canonical );
					this.AddHtmlOutlink( this.Canonical, sLinkUrlAbs, MacroscopeConstants.LINK_CANONICAL, true );
				}

			} else {

				this.Canonical = "";
				DebugMsg( string.Format( "CANONICAL: {0}", "MISSING" ) );

			}

		}
		
		/**************************************************************************/

		void ProcessHtmlHyperlinksOut ( HtmlDocument HtmlDoc )
		{

			// TODO: Add image links
				
			HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

			if( nOutlinks != null ) {

				foreach( HtmlNode nLink in nOutlinks ) {

					string sLinkUrl = nLink.GetAttributeValue( "href", null );
					
					//sLinkUrl = MacroscopeUrlTools.SanitizeUrl( sLinkUrl );

					string sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, sLinkUrl );

					MacroscopeHyperlinkOut hlHyperlinkOut = this.HyperlinksOut.Add( this.Url, sLinkUrlAbs );

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

		void ProcessHtmlOutlinks ( HtmlDocument HtmlDoc )
		{

			{ // Normal A HREF links
				HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkUrl = nLink.GetAttributeValue( "href", null );
						string sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, sLinkUrl );
						this.AddHtmlOutlink( sLinkUrl, sLinkUrlAbs, MacroscopeConstants.LINK_AHREF, true );
					}
				}
			}

			{ // LINK element links

				HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//link[@href]" );

				if( nOutlinks != null ) {

					foreach( HtmlNode nLink in nOutlinks ) {

						string sLinkUrl = nLink.GetAttributeValue( "href", null );
						string sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, sLinkUrl );
						string sType = MacroscopeConstants.LINK_LINK;
						Boolean bFollow = true;

						if( nLink.GetAttributeValue( "hreflang", null ) != null ) {
							sType = MacroscopeConstants.LINK_HREFLANG;
							if( !MacroscopePreferencesManager.GetFollowHrefLangLinks() ) {
								bFollow = false;
							}
						}
						
						this.AddHtmlOutlink( sLinkUrl, sLinkUrlAbs, sType, bFollow );
						
					}
					
				}
				
			}

			{ // Image element links
				HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//img[@src]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkUrl = nLink.GetAttributeValue( "src", null );
						string sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, sLinkUrl );
						this.AddHtmlOutlink( sLinkUrl, sLinkUrlAbs, MacroscopeConstants.LINK_IMAGE, true );
					}
				}
			}

			{ // Script element links
				HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//script[@src]" );
				if( nOutlinks != null ) {
					foreach( HtmlNode nLink in nOutlinks ) {
						string sLinkUrl = nLink.GetAttributeValue( "src", null );
						string sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, sLinkUrl );
						this.AddHtmlOutlink( sLinkUrl, sLinkUrlAbs, MacroscopeConstants.LINK_SCRIPT, true );
					}
				}
			}
			
		}

		/**************************************************************************/

		void AddHtmlOutlink ( string sRawUrl, string sAbsoluteUrl, string sType, Boolean bFollow )
		{

			MacroscopeOutlink OutLink = new MacroscopeOutlink ( sRawUrl, sAbsoluteUrl, sType, bFollow );

			if( this.Outlinks.ContainsKey( sRawUrl ) ) {
				this.Outlinks.Remove( sRawUrl );
				this.Outlinks.Add( sRawUrl, OutLink );
			} else {
				this.Outlinks.Add( sRawUrl, OutLink );
			}

		}

		/**************************************************************************/
		
		void ExtractHrefLangAlternates ( HtmlDocument HtmlDoc )
		{

			HtmlNodeCollection nlNodeList = HtmlDoc.DocumentNode.SelectNodes( "//link[@rel='alternate']" );

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
						
						this.HrefLang[ sLocale ] = msHrefLang;

					}

				}

			}

		}
		
		/**************************************************************************/

		void ExtractHtmlHeadings ( HtmlDocument HtmlDoc )
		{
			for( ushort iLevel = 1; iLevel <= 6; iLevel++ ) {
				HtmlNodeCollection nNodes = HtmlDoc.DocumentNode.SelectNodes( string.Format( "//h{0}", iLevel ) );
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
				
		void ExtractHtmlEmailAddresses ( HtmlDocument HtmlDoc )
		{
			HtmlNodeCollection nNodes = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
			if( nNodes != null ) {
				foreach( HtmlNode nLink in nNodes ) {
					string sLinkUrl = nLink.GetAttributeValue( "href", null );
					if( sLinkUrl != null ) {
						if( Regex.IsMatch( sLinkUrl, "^mailto:" ) ) {
							MatchCollection reMatches = Regex.Matches( sLinkUrl, "^mailto:([^?]+)" );
							foreach( Match reMatch in reMatches ) {
								this.AddEmailAddress( reMatch.Groups[ 1 ].Value.ToString() );
							}
						}
					}
				}
			}
		}
			
		/**************************************************************************/

		void ExtractHtmlTelephoneNumbers ( HtmlDocument HtmlDoc )
		{
			HtmlNodeCollection nNodes = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
			if( nNodes != null ) {
				foreach( HtmlNode nLink in nNodes ) {
					string sLinkUrl = nLink.GetAttributeValue( "href", null );
					if( sLinkUrl != null ) {
						if( Regex.IsMatch( sLinkUrl, "^tel:" ) ) {
							MatchCollection reMatches = Regex.Matches( sLinkUrl, "^tel:(.+)" );
							foreach( Match reMatch in reMatches ) {
								this.AddTelephoneNumber( reMatch.Groups[ 1 ].Value.ToString() );
							}
						}
					}
				}
			}
		}

		/**************************************************************************/

	}

}
