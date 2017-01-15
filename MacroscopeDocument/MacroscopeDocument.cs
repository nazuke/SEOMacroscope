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
using System.Threading;
using System.Diagnostics;

using HtmlAgilityPack;

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeDocument.
	/// </summary>

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
		public Boolean IsCompressed;
		public string CompressionMethod;
		public string ContentEncoding;
		public string Locale;
		
		public long Duration;

		public DateTime DateServer;
		public DateTime DateModified;

		public string Canonical;
		public Hashtable HrefLang;

		// Outbound links to pages and linked assets to follow
		public Hashtable Outlinks;

		// Inbound links from other pages in the scanned collection
		//public Hashtable HyperlinksIn;
		public MacroscopeHyperlinksIn HyperlinksIn;

		// Outbound hypertext links
		public MacroscopeHyperlinksOut HyperlinksOut;

		public Hashtable EmailAddresses;
		public Hashtable TelephoneNumbers;
		
		public string Title;
		public string Description;
		public string Keywords;

		public Dictionary<ushort,ArrayList> Headings;

		public int Depth;
		
		// Delegate Functions
		delegate void TimeDuration( Action ProcessMethod );

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
			Locale = null;

			DateServer = new DateTime ();
			DateModified = new DateTime ();

			Canonical = "";
			HrefLang = new Hashtable ();

			Outlinks = new Hashtable ();
			HyperlinksIn = new MacroscopeHyperlinksIn ();
			HyperlinksOut = new MacroscopeHyperlinksOut ();

			EmailAddresses = new Hashtable ();
			TelephoneNumbers = new Hashtable ();

			Title = "";
			Description = "";
			Keywords = "";

			Headings = new Dictionary<ushort,ArrayList> ()
			{
				{
					1,
					new ArrayList ( 16 )
				},
				{
					2,
					new ArrayList ( 16 )
				},
				{
					3,
					new ArrayList ( 16 )
				},
				{
					4,
					new ArrayList ( 16 )
				},
				{
					5,
					new ArrayList ( 16 )
				},
				{
					6,
					new ArrayList ( 16 )
				}
			};

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
					sMimeType = String.Format( "{0}/{1}", match.Groups[ 1 ].Value, match.Groups[ 2 ].Value );
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

		public MacroscopeHyperlinksIn GetHyperlinksIn ()
		{
			return( this.HyperlinksIn );
		}

		/**************************************************************************/

		public void AddHyperlinkIn (
			string sType,
			string sMethod,
			int iLinkClass,
			string sUrlOrigin,
			string sUrlTarget,
			string sLinkText,
			string sAltText
		)
		{
			this.HyperlinksIn.Add( sType, sMethod, iLinkClass, sUrlOrigin, sUrlTarget, sLinkText, sAltText );
		}

		/**************************************************************************/

		public void ClearHyperlinksIn ()
		{
			this.HyperlinksIn.Clear();
		}

		/**************************************************************************/

		public int CountHyperlinksIn ()
		{
			return( this.HyperlinksIn.Count() );
		}

		/**************************************************************************/

		public MacroscopeHyperlinksOut GetHyperlinksOut ()
		{
			return( this.HyperlinksOut );
		}

		/**************************************************************************/
		
		public int CountHyperlinksOut ()
		{
			int iCount = this.HyperlinksOut.Count();
			return( iCount );
		}

		/**************************************************************************/

		public Hashtable AddEmailAddress ( string sString )
		{
			debug_msg( string.Format( "AddEmailAddress: {0}", sString ) );
			if( this.EmailAddresses.ContainsKey( sString ) ) {
				this.EmailAddresses[ sString ] = this.GetUrl();
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
				this.TelephoneNumbers[ sString ] = this.GetUrl();
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
			this.HrefLang[ sLocale ] = msHrefLang;
		}

		/**************************************************************************/

		public Hashtable GetHrefLangs ()
		{
			return( this.HrefLang );
		}

		/**************************************************************************/

		public void AddHeading ( ushort iLevel, string sString )
		{
			if( this.Headings.ContainsKey( iLevel ) ) {
				ArrayList alHeadings = this.Headings[ iLevel ];
				alHeadings.Add( sString );
			}
		}

		/**************************************************************************/
				
		public ArrayList GetHeadings ( ushort iLevel )
		{
			ArrayList alHeadings = new ArrayList ();
			if( this.Headings.ContainsKey( iLevel ) ) {
				alHeadings = this.Headings[ iLevel ];
			}
			return( alHeadings );
		}

		/**************************************************************************/

		public void SetDuration ( long lDuration )
		{
			this.Duration = lDuration;
		}
		
		/**************************************************************************/
				
		public long GetDuration ()
		{
			//debug_msg( string.Format( "GetDuration: {0}", this.Duration ), 0 );
			return( this.Duration );
		}

		public decimal GetDurationInSeconds ()
		{
			decimal dDuration = ( decimal )( ( decimal )this.GetDuration() / ( decimal )1000 );
			//debug_msg( string.Format( "GetDurationInSeconds: {0}", dDuration ), 0 );
			return( dDuration );
		}

		public string GetDurationInSecondsFormatted ()
		{
			decimal dDuration = this.GetDurationInSeconds();
			string sDuration = dDuration.ToString( "0.00" );
			//debug_msg( string.Format( "GetDurationInSecondsFormatted: {0}", sDuration ), 0 );
			return( sDuration );
		}

		/**************************************************************************/

		public Boolean Execute ()
		{

			// TODO: validate this.Url
			
			if( this.IsRedirectPage() ) {
				debug_msg( string.Format( "IS REDIRECT: {0}", this.Url ) );
				this.IsRedirect = true;
			} 

			TimeDuration fTimeDuration = delegate( Action ProcessMethod )
			{
				Stopwatch swDuration = new Stopwatch ();
				long lDuration;
				swDuration.Start();
				try {
					ProcessMethod();
				} catch( MacroscopeDocumentException ex ) {
					debug_msg( string.Format( "fTimeDuration: {0}", ex.Message ) );
				}
				swDuration.Stop();
				lDuration = swDuration.ElapsedMilliseconds;
				if( lDuration > 0 ) {
					this.Duration = lDuration;
				} else {
					this.Duration = 0;
				}
				debug_msg( string.Format( "DURATION: {0} :: {1}", lDuration, this.Duration ) );
			};

			if( this.IsHtmlPage() ) {
				debug_msg( string.Format( "IS HTML PAGE: {0}", this.Url ) );
				fTimeDuration( this.ProcessHtmlPage );

			} else if( this.IsCssPage() ) {
				debug_msg( string.Format( "IS CSS PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchStylesheets() ) {
					fTimeDuration( this.ProcessCssPage );
				}

			} else if( this.IsImagePage() ) {
				debug_msg( string.Format( "IS IMAGE PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchImages() ) {
					fTimeDuration( this.ProcessImagePage );
				}
				
			} else if( this.IsJavascriptPage() ) {
				debug_msg( string.Format( "IS JAVASCRIPT PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchJavascripts() ) {
					fTimeDuration( this.ProcessJavascriptPage );
				}

			} else if( this.IsPdfPage() ) {
				debug_msg( string.Format( "IS PDF PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchPdfs() ) {
					fTimeDuration( this.ProcessPdfPage );
				}

			} else if( this.IsBinaryPage() ) {
				debug_msg( string.Format( "IS BINARY PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchBinaries() ) {
					fTimeDuration( this.ProcessBinaryPage );
				}

			} else {
				debug_msg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.Url ) );
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
				MacroscopePreferencesManager.EnableHttpProxy( req );
				res = ( HttpWebResponse )req.GetResponse();

				debug_msg( string.Format( "Status: {0}", res.StatusCode ) );

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
				debug_msg( string.Format( "is_redirect :: WebException: {0}", ex.Message ) );
			}

			return( bIsRedirect );
		}

		/**************************************************************************/

		void ProcessHttpHeaders ( HttpWebRequest req, HttpWebResponse res )
		{
			
			// Status Code

			this.StatusCode = this.ProcessStatusCode( res.StatusCode );

			debug_msg( string.Format( "Status: {0}", this.StatusCode ) );

			// Probe HTTP Headers

			foreach( string sHeader in res.Headers ) {

				debug_msg( string.Format( "HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader( sHeader ) ) );

				if( sHeader.ToLower().Equals( "date" ) ) {
					this.DateServer = DateTime.Parse( res.GetResponseHeader( sHeader ) );
				}

				if( sHeader.ToLower().Equals( "last-modified" ) ) {
					this.DateModified = DateTime.Parse( res.GetResponseHeader( sHeader ) );
				}

			}

			// Process Dates
			{
			
				if( this.DateServer.Date == new DateTime ().Date ) {
					this.DateServer = new DateTime ();
				}
			
				if( this.DateModified.Date == new DateTime ().Date ) {
					this.DateModified = this.DateServer;
				}
				
			}
			
			// Stash HTTP Headers

			this.MimeType = res.ContentType;
			this.ContentLength = res.ContentLength;

			debug_msg( string.Format( "Content-Type: {0}", this.MimeType ) );
			debug_msg( string.Format( "Content-Length: {0}", this.ContentLength.ToString() ) );

			
			// TODO: Link: <http://www.example.com/downloads/white-paper.pdf>; rel="canonical"
			
			
			
			
		}
		
		/**************************************************************************/
		
		public List<KeyValuePair<string,string>> DetailDocumentDetails ()
		{

			List<KeyValuePair<string,string>> slDetails = new List<KeyValuePair<string,string>> ();

			slDetails.Add( new KeyValuePair<string,string> ( "URL", this.GetUrl() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Status Code", this.GetStatusCode().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Duration (seconds)", this.GetDurationInSecondsFormatted() ) );
						
			slDetails.Add( new KeyValuePair<string,string> ( "Content Type", this.GetMimeType() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Content Length", this.ContentLength.ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Encoding", this.ContentEncoding ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Date", this.GetDateServer() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Date Modified", this.GetDateModified() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Language", this.GetLang() ) );

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

			for( ushort iLevel = 1; iLevel <= 6; iLevel++ ) {
				string sHeading;
				if( this.GetHeadings( iLevel ).Count > 0 ) {
					sHeading = this.GetHeadings( iLevel )[ 0 ].ToString();
				} else {
					sHeading = null;
				}
				if( sHeading != null ) {
					slDetails.Add( new KeyValuePair<string,string> ( string.Format( "H{0}", iLevel ), sHeading ) );
					slDetails.Add( new KeyValuePair<string,string> ( string.Format( "H{0} Length", iLevel ), sHeading.Length.ToString() ) );
				}
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
