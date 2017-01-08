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
		public Boolean ProbeHrefLangs { get; set; }
		/** END: Configuration **/
		
		string Url;
		int Timeout;

		Boolean IsRedirect;
		string UrlRedirectFrom;

		HtmlDocument HtmlDoc;
		
		public MacroscopeDocument parent;

		public string Scheme;
		public string Hostname;
		public int Port;
		public string Path;
		public string Fragment;
		public string QueryString;

		public int StatusCode;
		public long ContentLength;
		public string MimeType;
		public Boolean IsHtml;
		public string ContentEncoding;
		public string Locale;

		public DateTime DateServer;
		public DateTime DateModified;

		public string Canonical;
		public Hashtable HrefLang;

		// Outbound links to pages and linked assets to follow
		public Hashtable Outlinks;

		// Inbound links from other pages in the scanned collection
		public Hashtable HyperlinksIn;

		// Outbound hypertext links
		public Hashtable HyperlinksOut;

		public Hashtable EmailAddresses;
		public Hashtable TelephoneNumbers;
		
		public string Title;
		public string Description;
		public string Keywords;

		public ArrayList Headings1;
		public ArrayList Headings2;

		public int Depth;
		
		/**************************************************************************/

		public MacroscopeDocument ( string sURL )
		{

			Url = sURL;
			Timeout = 10000;
			IsRedirect = false;
			UrlRedirectFrom = "";
			
			StatusCode = 0;
			ContentLength = 0;
			
			MimeType = "";
			IsHtml = false;
			ContentEncoding = "";
			Locale = "unknown";

			DateServer = new DateTime ();
			DateModified = new DateTime ();

			Canonical = "";
			HrefLang = new Hashtable ();

			Outlinks = new Hashtable ();
			HyperlinksIn = new Hashtable ();
			HyperlinksOut = new Hashtable ();

			EmailAddresses = new Hashtable ();
			TelephoneNumbers = new Hashtable ();

			Title = "";
			Description = "";
			Keywords = "";
			
			Headings1 = new ArrayList ( 16 );
			Headings2 = new ArrayList ( 16 );

			Depth = MacroscopeURLTools.FindUrlDepth( Url );
			
		}
		
		/**************************************************************************/

		public string GetUrl ()
		{
			return( this.Url );
		}

		/**************************************************************************/

		public string GetIsRedirect ()
		{
			return( this.IsRedirect.ToString() );
		}

		/**************************************************************************/
		
		public int GetStatusCode ()
		{
			return( this.StatusCode );
		}

		/**************************************************************************/
		
		public string GetMimeType ()
		{
			string sMimeType = null;
			if( this.MimeType == null ) {
				sMimeType = "";
			} else {
				MatchCollection matches = Regex.Matches( this.MimeType, "^([^\\s;/]+)/([^\\s;/]+)" );
				foreach( Match match in matches ) {
					sMimeType = String.Format( "{0}/{1}", match.Groups[1].Value, match.Groups[2].Value );
				}
				if( sMimeType == null ) {
					sMimeType = this.MimeType;
				}
			}
			return( sMimeType );
		}

		/**************************************************************************/
		
		public Boolean GetIsHtml ()
		{
			return( this.IsHtml );
		}

		/**************************************************************************/
		
		public string GetLang ()
		{
			return( this.Locale );
		}
		
		/**************************************************************************/
		
		public string GetLocale ()
		{
			return( this.Locale );
		}

		/**************************************************************************/
		
		public string GetCanonical ()
		{
			return( this.Canonical );
		}

		/**************************************************************************/
		
		public string GetDateServer ()
		{
			return( this.DateServer.ToShortDateString() );
		}
		
		/**************************************************************************/
		
		public string GetDateModified ()
		{
			return( this.DateModified.ToShortDateString() );
		}
				
		/**************************************************************************/

		public Hashtable GetOutlinks ()
		{
			return( this.Outlinks );
		}
		
		/**************************************************************************/
		
		public int CountOutlinks ()
		{
			int iCount = this.GetOutlinks().Count;
			return( iCount );
		}

		/**************************************************************************/
				
		public Hashtable AddHyperlinkIn ( string sURL )
		{
			if( this.HyperlinksIn.ContainsKey( sURL ) ) {
				int count = ( int )this.HyperlinksIn[sURL] + 1;
				this.HyperlinksIn[sURL] = count;
			} else {
				this.HyperlinksIn.Add( sURL, 1 );
			}
			return( this.HyperlinksIn );
		}

		/**************************************************************************/

		public Hashtable GetHyperlinksIn ()
		{
			return( this.HyperlinksIn );
		}

		/**************************************************************************/

		public Hashtable GetHyperlinksOut ()
		{
			return( this.HyperlinksOut );
		}
		
		/**************************************************************************/

		public int CountHyperlinksIn ()
		{
			int iCount = this.GetHyperlinksIn().Count;
			return( iCount );
		}
		
		/**************************************************************************/
		
		public int CountHyperlinksOut ()
		{
			int iCount = this.GetHyperlinksOut().Count;
			return( iCount );
		}

		/**************************************************************************/

		public Hashtable AddEmailAddress ( string sString )
		{
			debug_msg( string.Format( "AddEmailAddress: {0}", sString ) );
			if( this.EmailAddresses.ContainsKey( sString ) ) {
				this.EmailAddresses[sString] = this.GetUrl();
			} else {
				this.EmailAddresses.Add( sString, this.GetUrl() );
			}
			return( this.EmailAddresses );
		}

		/**************************************************************************/

		public Hashtable GetEmailAddresses ()
		{
			return( this.EmailAddresses );
		}

		/**************************************************************************/

		public Hashtable AddTelephoneNumber ( string sString )
		{
			debug_msg( string.Format( "AddTelephoneNumber: {0}", sString ) );
			if( this.TelephoneNumbers.ContainsKey( sString ) ) {
				this.TelephoneNumbers[sString] = this.GetUrl();
			} else {
				this.TelephoneNumbers.Add( sString, this.GetUrl() );
			}
			return( this.TelephoneNumbers );
		}

		/**************************************************************************/

		public Hashtable GetTelephoneNumbers ()
		{
			return( this.TelephoneNumbers );
		}

		/**************************************************************************/
		
		public string GetTitle ()
		{
			string sValue;
			if( this.Title != null ) {
				sValue = this.Title;
			} else {
				sValue = "";
			}
			return( sValue );
		}
		
		/**************************************************************************/
		
		public int GetTitleLength ()
		{
			return( this.GetTitle().Length );
		}

		/**************************************************************************/
		
		public string GetDescription ()
		{
			string sValue;
			if( this.Description != null ) {
				sValue = this.Description;
			} else {
				sValue = "";
			}
			return( sValue );
		}
		
		/**************************************************************************/
				
		public int GetDescriptionLength ()
		{
			return( this.GetDescription().Length );
		}
				
		/**************************************************************************/
				
		public string GetKeywords ()
		{
			string sValue;
			if( this.Keywords != null ) {
				sValue = this.Keywords;
			} else {
				sValue = "";
			}
			return( sValue );
		}

		/**************************************************************************/
				
		public int GetKeywordsLength ()
		{
			return( this.GetKeywords().Length );
		}
				
		/**************************************************************************/
				
		public int GetKeywordsCount ()
		{
			int uiCount = 0;
			string[] aKeywords = Regex.Split( this.GetKeywords(), "[\\s,]+" );
			uiCount = aKeywords.GetLength( 0 );
			return( uiCount );
		}
				
		/**************************************************************************/

		void SetHreflang ( string sLocale, string sURL )
		{
			MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang ( false, sLocale, sURL );
			this.HrefLang[sLocale] = msHrefLang;
		}

		/**************************************************************************/

		public Hashtable GetHrefLangs ()
		{
			return( this.HrefLang );
		}

		/**************************************************************************/

		public void AddHeading1 ( string sString )
		{
			this.Headings1.Add( sString );
		}
		
		/**************************************************************************/
				
		public ArrayList GetHeadings1 ()
		{
			return( this.Headings1 );
		}

		/**************************************************************************/

		public void AddHeading2 ( string sString )
		{
			this.Headings2.Add( sString );
		}
		
		/**************************************************************************/
				
		public ArrayList GetHeadings2 ()
		{
			return( this.Headings2 );
		}

		/**************************************************************************/

		public Boolean Execute ()
		{

			if( this.IsRedirectPage() ) {
				debug_msg( string.Format( "IS REDIRECT: {0}", this.Url ), 2 );
				this.IsRedirect = true;
			} 

			if( this.IsHtmlPage() ) {
				debug_msg( string.Format( "IS HTML PAGE: {0}", this.Url ), 2 );
				this.ProcessHtmlPage();

			} else if( this.IsCssPage() ) {
				debug_msg( string.Format( "IS CSS PAGE: {0}", this.Url ), 2 );
				if( MacroscopePreferences.GetFetchStylesheets() ) {
					this.ProcessCssPage();
				}

			} else if( this.IsImagePage() ) {
				debug_msg( string.Format( "IS IMAGE PAGE: {0}", this.Url ), 2 );
				if( MacroscopePreferences.GetFetchImages() ) {
					this.ProcessImagePage();
				}
				
			} else if( this.IsJavascriptPage() ) {
				debug_msg( string.Format( "IS JAVASCRIPT PAGE: {0}", this.Url ), 2 );
				if( MacroscopePreferences.GetFetchJavascripts() ) {
					this.process_javascript_page();
				}

			} else if( this.IsPdfPage() ) {
				debug_msg( string.Format( "IS PDF PAGE: {0}", this.Url ), 2 );
				if( MacroscopePreferences.GetFetchPdfs() ) {
					this.ProcessPdfPage();
				}

			} else if( this.IsBinaryPage() ) {
				debug_msg( string.Format( "IS BINARY PAGE: {0}", this.Url ), 2 );
				if( MacroscopePreferences.GetFetchBinaries() ) {
					this.ProcessBinaryPage();
				}

			} else {
				debug_msg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.Url ), 2 );
			}
			
			return( true );

		}

		/**************************************************************************/

		Boolean IsRedirectPage ()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIsRedirect = false;
			string sOriginalURL = this.Url;

			try {

				req = WebRequest.CreateHttp( this.Url );
				req.Method = "HEAD";
				req.Timeout = this.Timeout;
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
					this.IsRedirect = true;
					this.Url = res.GetResponseHeader( "Location" );
					this.UrlRedirectFrom = sOriginalURL;


					//this.url = MacroscopeURLTools.make_url_absolute( this.url, this.UrlRedirectFrom );


				}
				res.Close();

			} catch( WebException ex ) {
				debug_msg( string.Format( "is_redirect :: WebException: {0}", ex.Message ), 2 );
			}

			return( bIsRedirect );
		}

		
		/**************************************************************************/
		
		
		void ProcessHttpHeaders ( HttpWebRequest req, HttpWebResponse res )
		{
			
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

		}
		
		/**************************************************************************/
		
		public List<KeyValuePair<string,string>> DetailDocumentDetails ()
		{

			List<KeyValuePair<string,string>> slDetails = new List<KeyValuePair<string,string>> ();

			slDetails.Add( new KeyValuePair<string,string> ( "URL", this.GetUrl() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Status Code", this.GetStatusCode().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Content Type", this.GetMimeType() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Content Length", this.ContentLength.ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Encoding", this.ContentEncoding ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Date Server", this.GetDateServer() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Date Modified", this.GetDateModified() ) );
				
			slDetails.Add( new KeyValuePair<string,string> ( "Canonical", this.GetCanonical() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Redirect", this.GetIsRedirect() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Redirected From", this.UrlRedirectFrom ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Links In Count", this.CountHyperlinksIn().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Links Out Count", this.CountHyperlinksOut().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "HrefLang Count", this.GetHrefLangs().Count.ToString() ) );
				
			slDetails.Add( new KeyValuePair<string,string> ( "Title", this.GetTitle() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Title Length", this.GetTitleLength().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Description", this.GetDescription() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Description Length", this.GetDescriptionLength().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Keywords", this.GetKeywords() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Keywords Length", this.GetKeywordsLength().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Keywords Count", this.GetKeywordsCount().ToString() ) );

			{
				string sHeading;
				if( this.GetHeadings1().Count > 0 ) {
					sHeading = this.GetHeadings1()[0].ToString();
				} else {
					sHeading = "";
				}
				slDetails.Add( new KeyValuePair<string,string> ( "H1", sHeading ) );
				slDetails.Add( new KeyValuePair<string,string> ( "H1 Length", sHeading.Length.ToString() ) );
			}

			{
				string sHeading;
				if( this.GetHeadings2().Count > 0 ) {
					sHeading = this.GetHeadings2()[0].ToString();
				} else {
					sHeading = "";
				}
				slDetails.Add( new KeyValuePair<string,string> ( "H2", sHeading ) );
				slDetails.Add( new KeyValuePair<string,string> ( "H2 Length", sHeading.Length.ToString() ) );
			}
				
			slDetails.Add( new KeyValuePair<string,string> ( "Page Depth", this.Depth.ToString() ) );

			return( slDetails );

		}

		/**************************************************************************/

		int ProcessStatusCode ( HttpStatusCode status )
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
