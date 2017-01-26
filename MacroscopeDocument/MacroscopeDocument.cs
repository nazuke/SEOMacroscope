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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeDocument.
	/// </summary>

	public partial class MacroscopeDocument : Macroscope
	{

		/**************************************************************************/
		
		public override Boolean SuppressDebugMsg { get; protected set; }
			
		/**************************************************************************/
		
		Boolean IsDirty;
		
		string Url;
		int Timeout;

		Boolean IsExternal;
		
		Boolean IsRedirect;
		string UrlRedirectFrom;
		string UrlRedirectTo;

		string Scheme;
		string Hostname;
		int Port;
		string Path;
		string Fragment;
		string QueryString;
		
		Boolean HypertextStrictTransportPolicy;

		int StatusCode;
		string ErrorCondition;
		long ContentLength;
		string MimeType;
		Boolean IsHtml;
		Boolean IsImage;
		Boolean IsPdf;
		Boolean IsCompressed;
		string CompressionMethod;
		string ContentEncoding;
		string Locale;
		
		long Duration;

		DateTime DateServer;
		DateTime DateModified;

		string Canonical;
		Dictionary<string,MacroscopeHrefLang> HrefLang;

		// Outbound links to pages and linked assets to follow
		Dictionary<string,MacroscopeOutlink> Outlinks;

		// Inbound links from other pages in the scanned collection
		MacroscopeHyperlinksIn HyperlinksIn;

		// Outbound hypertext links
		MacroscopeHyperlinksOut HyperlinksOut;

		Dictionary<string,string> EmailAddresses;
		Dictionary<string,string> TelephoneNumbers;
		
		string Title;
		string Description;
		string Keywords;

		Dictionary<ushort,ArrayList> Headings;

		int Depth;
		
		// Delegate Functions
		delegate void TimeDuration( Action ProcessMethod );

		/**************************************************************************/

		public MacroscopeDocument ( string sUrl )
		{

			SuppressDebugMsg = false;
			
			IsDirty = true;
			
			Url = sUrl;
			Timeout = 10000;
			
			IsExternal = false;
			
			IsRedirect = false;
			UrlRedirectFrom = "";			
			UrlRedirectTo = "";
			
			Scheme = "";
			Hostname = "";
			Port = 80;
			Path = "";
			Fragment = "";
			QueryString = "";
			HypertextStrictTransportPolicy = false;
		
			StatusCode = 0;
			ErrorCondition = "";
			ContentLength = 0;
			
			MimeType = "";
			IsHtml = false;
			ContentEncoding = "";
			Locale = null;

			DateServer = new DateTime ();
			DateModified = new DateTime ();

			Canonical = "";
			HrefLang = new Dictionary<string,MacroscopeHrefLang> ( 1024 );

			Outlinks = new Dictionary<string,MacroscopeOutlink> ( 128 );
			HyperlinksIn = new MacroscopeHyperlinksIn ();
			HyperlinksOut = new MacroscopeHyperlinksOut ();

			EmailAddresses = new Dictionary<string,string> ( 256 );
			TelephoneNumbers = new Dictionary<string,string> ( 256 );

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

			Depth = MacroscopeUrlTools.FindUrlDepth( Url );
			
		}

		/** Dirty Flag ************************************************************/

		public void SetIsDirty ()
		{
			this.IsDirty = true;
		}
		
		void ClearIsDirty ()
		{
			this.IsDirty = false;
		}

		public Boolean GetIsDirty ()
		{
			return( this.IsDirty );
		}

		/** Host Details **********************************************************/

		public string GetUrl ()
		{
			return( this.Url );
		}

		public string GetScheme ()
		{
			return( this.Scheme );
		}

		public string GetHostname ()
		{
			return( this.Hostname );
		}

		public int GetPort ()
		{
			return( this.Port );
		}

		public string GetPath ()
		{
			return( this.Path );
		}

		public string GetFragment ()
		{
			return( this.Fragment );
		}

		public string GetQueryString ()
		{
			return( this.QueryString );
		}

		/** Is External Flag ******************************************************/

		public void SetIsExternal ( Boolean bState )
		{
			this.IsExternal = bState;
		}

		public Boolean GetIsExternal ()
		{
			return( this.IsExternal );
		}

		/** Is Redirect Flag ******************************************************/

		public Boolean GetIsRedirect ()
		{
			return( this.IsRedirect );
		}

		public string GetUrlRedirectFrom ()
		{
			return( this.UrlRedirectFrom );
		}

		public string GetUrlRedirectTo ()
		{
			return( this.UrlRedirectTo );
		}

		/**************************************************************************/
		
		public int GetStatusCode ()
		{
			return( this.StatusCode );
		}
		
		/**************************************************************************/

		public string GetErrorCondition ()
		{
			return( this.ErrorCondition );
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

		/** Document Type Getters *************************************************/
		
		public Boolean GetIsHtml ()
		{
			return( this.IsHtml );
		}
		
		public Boolean GetIsImage ()
		{
			return( this.IsImage );
		}
		
		public Boolean GetIsPdf ()
		{
			return( this.IsPdf );
		}

		/**************************************************************************/

		public Boolean GetIsCompressed ()
		{
			return( this.IsCompressed );
		}

		public string GetCompressionMethod ()
		{
			return( this.CompressionMethod );
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

		/** Canonical *************************************************************/
		
		public string GetCanonical ()
		{
			return( this.Canonical );
		}
		
		public void SetCanonical ( string sCanonical )
		{
			this.Canonical = sCanonical;
		}

		/** Dates *****************************************************************/
		
		public string GetDateServer ()
		{
			return( this.DateServer.ToShortDateString() );
		}
		
		public string GetDateModified ()
		{
			return( this.DateModified.ToShortDateString() );
		}

		/** Outlinks **************************************************************/

		public Dictionary<string,MacroscopeOutlink> GetOutlinks ()
		{
			return( this.Outlinks );
		}

		public IEnumerable IterateOutlinks ()
		{
			lock( this.Outlinks ) {
				foreach( string sUrl in this.Outlinks.Keys ) {
					yield return sUrl;
				}
			}
		}

		public MacroscopeOutlink GetOutlink ( string sUrl )
		{
			MacroscopeOutlink Outlink = null;
			if( this.Outlinks.ContainsKey( sUrl ) ) {
				Outlink = this.Outlinks[ sUrl ];
			}
			return( Outlink );
		}

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

		/** Hyperlinks In *********************************************************/

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

		public void ClearHyperlinksIn ()
		{
			this.HyperlinksIn.Clear();
		}

		public int CountHyperlinksIn ()
		{
			return( this.HyperlinksIn.Count() );
		}

		/** Hyperlinks Out ********************************************************/

		public MacroscopeHyperlinksOut GetHyperlinksOut ()
		{
			return( this.HyperlinksOut );
		}
		
		public int CountHyperlinksOut ()
		{
			int iCount = this.HyperlinksOut.Count();
			return( iCount );
		}

		/** Email Addresses *******************************************************/

		public void AddEmailAddress ( string sString )
		{
			DebugMsg( string.Format( "AddEmailAddress: {0}", sString ) );
			if( this.EmailAddresses.ContainsKey( sString ) ) {
				this.EmailAddresses[ sString ] = this.GetUrl();
			} else {
				this.EmailAddresses.Add( sString, this.GetUrl() );
			}
		}

		public Dictionary<string,string> GetEmailAddresses ()
		{
			return( this.EmailAddresses );
		}

		/** Telephone Numbers *****************************************************/

		public void AddTelephoneNumber ( string sString )
		{
			DebugMsg( string.Format( "AddTelephoneNumber: {0}", sString ) );
			if( this.TelephoneNumbers.ContainsKey( sString ) ) {
				this.TelephoneNumbers[ sString ] = this.GetUrl();
			} else {
				this.TelephoneNumbers.Add( sString, this.GetUrl() );
			}
		}

		public Dictionary<string,string> GetTelephoneNumbers ()
		{
			return( this.TelephoneNumbers );
		}

		/** Title *****************************************************************/
		
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
		
		public int GetTitleLength ()
		{
			return( this.GetTitle().Length );
		}

		/** Description ***********************************************************/
		
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
		
		public int GetDescriptionLength ()
		{
			return( this.GetDescription().Length );
		}
				
		/** Keywords **************************************************************/
				
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

		public int GetKeywordsLength ()
		{
			return( this.GetKeywords().Length );
		}

		public int GetKeywordsCount ()
		{
			int uiCount = 0;
			string[] aKeywords = Regex.Split( this.GetKeywords(), "[\\s,]+" );
			uiCount = aKeywords.GetLength( 0 );
			return( uiCount );
		}
				
		/** HrefLang **************************************************************/

		void SetHreflang ( string sLocale, string sUrl )
		{
			MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang ( sLocale, sUrl );
			this.HrefLang[ sLocale ] = msHrefLang;
		}

		public Dictionary<string,MacroscopeHrefLang> GetHrefLangs ()
		{
			return( this.HrefLang );
		}

		/** Headings **************************************************************/

		public void AddHeading ( ushort iLevel, string sString )
		{
			if( this.Headings.ContainsKey( iLevel ) ) {
				ArrayList alHeadings = this.Headings[ iLevel ];
				alHeadings.Add( sString );
			}
		}

		public ArrayList GetHeadings ( ushort iLevel )
		{
			ArrayList alHeadings = new ArrayList ();
			if( this.Headings.ContainsKey( iLevel ) ) {
				alHeadings = this.Headings[ iLevel ];
			}
			return( alHeadings );
		}

		/** Durations *************************************************************/

		public long GetDuration ()
		{
			//DebugMsg( string.Format( "GetDuration: {0}", this.Duration ), 0 );
			return( this.Duration );
		}

		public void SetDuration ( long lDuration )
		{
			this.Duration = lDuration;
		}

		public decimal GetDurationInSeconds ()
		{
			decimal dDuration = ( decimal )( ( decimal )this.GetDuration() / ( decimal )1000 );
			//DebugMsg( string.Format( "GetDurationInSeconds: {0}", dDuration ), 0 );
			return( dDuration );
		}

		public string GetDurationInSecondsFormatted ()
		{
			decimal dDuration = this.GetDurationInSeconds();
			string sDuration = dDuration.ToString( "0.00" );
			//DebugMsg( string.Format( "GetDurationInSecondsFormatted: {0}", sDuration ), 0 );
			return( sDuration );
		}

		/** Page Depth ************************************************************/

		public int GetDepth ()
		{
			return( this.Depth );
		}

		/**************************************************************************/

		public Boolean Execute ()
		{
			
			// TODO: Change this, so that the initial HEAD request only runs once,
			// TODO: and determines of the page is a redirect, as well as the mime type.

			TimeDuration fTimeDuration = this.GetTimeDurationDelegate();
			Boolean bDownloadDocument = true;

			this.ClearIsDirty();

			try {
				this.ProcessUrlElements();
			} catch( Exception ex ) {
				DebugMsg( string.Format( "ProcessUrlElements: {0}", ex.Message ) );
			}

			if( this.IsRedirectPage() ) {
				DebugMsg( string.Format( "IS REDIRECT: {0}", this.Url ) );
				return( true );
			} 

			if( !MacroscopePreferencesManager.GetCheckExternalLinks() ) {
				if( this.GetIsExternal() ) {
					bDownloadDocument = false;
				}
			}

			if( this.IsHtmlPage() ) {
				DebugMsg( string.Format( "IS HTML PAGE: {0}", this.Url ) );
				if( bDownloadDocument ) {
					fTimeDuration( this.ProcessHtmlPage );
				}

			} else if( this.IsCssPage() ) {
				DebugMsg( string.Format( "IS CSS PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchStylesheets() ) {
					if( bDownloadDocument ) {
						fTimeDuration( this.ProcessCssPage );
					}
				}

			} else if( this.IsImagePage() ) {
				DebugMsg( string.Format( "IS IMAGE PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchImages() ) {
					if( bDownloadDocument ) {
						fTimeDuration( this.ProcessImagePage );
					}
				}
				
			} else if( this.IsJavascriptPage() ) {
				DebugMsg( string.Format( "IS JAVASCRIPT PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchJavascripts() ) {
					if( bDownloadDocument ) {
						fTimeDuration( this.ProcessJavascriptPage );
					}
				}

			} else if( this.IsPdfPage() ) {
				DebugMsg( string.Format( "IS PDF PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchPdfs() ) {
					if( bDownloadDocument ) {
						fTimeDuration( this.ProcessPdfPage );
					}
				}

			} else if( this.IsBinaryPage() ) {
				DebugMsg( string.Format( "IS BINARY PAGE: {0}", this.Url ) );
				if( MacroscopePreferencesManager.GetFetchBinaries() ) {
					if( bDownloadDocument ) {
						fTimeDuration( this.ProcessBinaryPage );
					}
				}

			} else {
				DebugMsg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.Url ) );
			}

			return( true );

		}

		/**************************************************************************/
		
		TimeDuration GetTimeDurationDelegate ()
		{

			TimeDuration fTimeDuration = delegate( Action ProcessMethod )
			{
				Stopwatch swDuration = new Stopwatch ();
				long lDuration;
				swDuration.Start();
				try {
					ProcessMethod();
				} catch( MacroscopeDocumentException ex ) {
					DebugMsg( string.Format( "fTimeDuration: {0}", ex.Message ) );
				}
				swDuration.Stop();
				lDuration = swDuration.ElapsedMilliseconds;
				if( lDuration > 0 ) {
					this.Duration = lDuration;
				} else {
					this.Duration = 0;
				}
				//DebugMsg( string.Format( "DURATION: {0} :: {1}", lDuration, this.Duration ) );
			};

			return( fTimeDuration );

		}

		/**************************************************************************/

		Boolean IsRedirectPage ()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIsRedirect = false;
			string sOriginalUrl = this.Url;
			string sErrorCondition = null;

			try {

				req = WebRequest.CreateHttp( this.Url );
				req.Method = "HEAD";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				req.AllowAutoRedirect = false;
				MacroscopePreferencesManager.EnableHttpProxy( req );

				try {
					res = ( HttpWebResponse )req.GetResponse();
				} catch( WebException ex ) {
					DebugMsg( string.Format( "IsRedirectPage :: WebException: {0}", ex.Message ) );
					DebugMsg( string.Format( "IsRedirectPage :: WebExceptionStatus: {0}", ex.Status ) );
					sErrorCondition = ex.Status.ToString();
				}

				if( res != null ) {
				
					DebugMsg( string.Format( "Status: {0}", res.StatusCode ) );

					foreach( string sKey in res.Headers ) {
						DebugMsg( string.Format( "HEADERS: {0} => {1}", sKey, res.GetResponseHeader( sKey ) ) );
					}

					try {

						switch( res.StatusCode ) {

						// 200 Range

							case HttpStatusCode.OK:
								bIsRedirect = false;
								break;

						// 300 Range

							case HttpStatusCode.Moved:
								bIsRedirect = true;
								break;

							case HttpStatusCode.SeeOther:
								bIsRedirect = true;
								break;
						
							case HttpStatusCode.Redirect:
								bIsRedirect = true;
								break;

						// 400 Range

							case HttpStatusCode.Forbidden:
								bIsRedirect = false;
								break;

							case HttpStatusCode.NotFound:
								bIsRedirect = false;
								break;

							case HttpStatusCode.Gone:
								bIsRedirect = false;
								break;
								
						// Unhandled

							default:
								throw new MacroscopeDocumentException ( "Unhandled HttpStatusCode Type" );
								
						}

					} catch( MacroscopeDocumentException ex ) {
						DebugMsg( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
					}

					if( bIsRedirect ) {

						this.IsRedirect = true;
						string sLocation = res.GetResponseHeader( "Location" );
						string sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, sLocation );
						this.UrlRedirectFrom = sOriginalUrl;
						this.UrlRedirectTo = sLinkUrlAbs;
						this.AddDocumentOutlink( sLinkUrlAbs, sLinkUrlAbs, MacroscopeConstants.LINK_REDIRECT, true );

					}
					
					res.Close();

				}
				
			} catch( WebException ex ) {
				DebugMsg( string.Format( "IsRedirectPage :: WebException: {0}", ex.Message ) );
				DebugMsg( string.Format( "IsRedirectPage :: WebExceptionStatus: {0}", ex.Status ) );
				sErrorCondition = ex.Status.ToString();
			}

			if( sErrorCondition != null ) {
				this.StatusCode = 500;
				this.ErrorCondition = sErrorCondition;
			}

			return( bIsRedirect );
		}

		/**************************************************************************/

		void ProcessHttpHeaders ( HttpWebRequest req, HttpWebResponse res )
		{
			
			// Status Code
			this.StatusCode = ( int )res.StatusCode;

			// Common HTTP Headers
			{
				this.MimeType = res.ContentType;
				this.ContentLength = res.ContentLength;
			}

			// Probe HTTP Headers
			foreach( string sHeader in res.Headers ) {

				DebugMsg( string.Format( "HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader( sHeader ) ) );

				if( sHeader.ToLower().Equals( "date" ) ) {
					this.DateServer = DateTime.Parse( res.GetResponseHeader( sHeader ) );
				}

				if( sHeader.ToLower().Equals( "last-modified" ) ) {
					this.DateModified = DateTime.Parse( res.GetResponseHeader( sHeader ) );
				}

				if( sHeader.ToLower().Equals( "content-encoding" ) ) {
					this.IsCompressed = true;
					this.CompressionMethod = res.GetResponseHeader( sHeader );
				}

				// Process HTST Policy
				// https://www.owasp.org/index.php/HTTP_Strict_Transport_Security_Cheat_Sheet
				// Strict-Transport-Security: max-age=31536000; includeSubDomains; preload
				if( sHeader.ToLower().Equals( "strict-transport-security" ) ) {
					this.HypertextStrictTransportPolicy = true;
					// TODO: implement includeSubDomains
				}

				// Canonical HTTP Header
				// Link: <http://www.example.com/downloads/white-paper.pdf>; rel="canonical"
				if( sHeader.ToLower().Equals( "link" ) ) {

					string sUrl = null;
					string sRel = null;
					string sRaw = res.GetResponseHeader( sHeader );

					MatchCollection matches = Regex.Matches( sRaw, "<([^<>]+)>\\s*;\\srel=\"([^\"]+)\"" );

					foreach( Match match in matches ) {
						sUrl = match.Groups[ 1 ].Value;
						sRel = match.Groups[ 2 ].Value;
					}
									
					if( ( sRel != null ) && ( sRel.ToLower() == "canonical" ) ) {
						
						if( ( sUrl != null ) && ( sUrl.Length > 0 ) ) {
							this.SetCanonical( sUrl );
						}
					}

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

		}

		/**************************************************************************/

		void ProcessUrlElements ()
		{
			Uri uUri = new Uri ( this.GetUrl(), UriKind.Absolute );
			this.Scheme = uUri.Scheme;
			this.Hostname = uUri.Host;
			this.Port = uUri.Port;
			this.Path = uUri.AbsolutePath;
			this.Fragment = uUri.Fragment;
			this.QueryString = uUri.Query;
		}

		/** Outlinks **************************************************************/

		void AddDocumentOutlink ( string sRawUrl, string sAbsoluteUrl, string sType, Boolean bFollow )
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
		
		public List<KeyValuePair<string,string>> DetailDocumentDetails ()
		{

			List<KeyValuePair<string,string>> slDetails = new List<KeyValuePair<string,string>> ();

			slDetails.Add( new KeyValuePair<string,string> ( "URL", this.GetUrl() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Status Code", this.GetStatusCode().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Duration (seconds)", this.GetDurationInSecondsFormatted() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "HTST Policy Enabled", this.HypertextStrictTransportPolicy.ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Content Type", this.GetMimeType() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Content Length", this.ContentLength.ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Encoding", this.ContentEncoding ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Compressed", this.GetIsCompressed().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Compression Method", this.GetCompressionMethod() ) );
									
			slDetails.Add( new KeyValuePair<string,string> ( "Date", this.GetDateServer() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Date Modified", this.GetDateModified() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Language", this.GetLang() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Canonical", this.GetCanonical() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Redirect", this.GetIsRedirect().ToString() ) );
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
			
			slDetails.Add( new KeyValuePair<string,string> ( "Error Condition", this.GetErrorCondition() ) );

			return( slDetails );

		}

		/**************************************************************************/

	}

}
