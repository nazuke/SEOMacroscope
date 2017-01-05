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
using ExCSS;

namespace SEOMacroscope
{

	public partial class MacroscopeDocument : Macroscope
	{

		/**************************************************************************/
		
		Boolean IsCssPage ()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIs = false;
			Regex reIs = new Regex ( "^text/css", RegexOptions.IgnoreCase );
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
				}
				res.Close();
//			} catch( UriFormatException ex ) {
//				debug_msg( string.Format( "IsCssPage :: UriFormatException: {0}", ex.Message ), 2 );
			} catch( WebException ex ) {
				debug_msg( string.Format( "IsCssPage :: WebException: {0}", ex.Message ), 2 );
			}
			return( bIs );
		}

		/**************************************************************************/
		
		Boolean ProcessCssPage ()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;

			
			debug_msg( string.Format( "ProcessCssPage: {0}", "" ), 0 );
			
			try {
				req = WebRequest.CreateHttp( this.Url );
				req.Method = "GET";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				res = ( HttpWebResponse )req.GetResponse();
			} catch( WebException ex ) {
				debug_msg( string.Format( "ProcessCssPage :: WebException: {0}", ex.Message ), 3 );
				debug_msg( string.Format( "ProcessCssPage :: WebException: {0}", this.Url ), 3 );
			}

			if( res != null ) {
				
				string sRawData = "";


				// Status Code
				this.StatusCode = this.ProcessStatusCode( res.StatusCode );
				debug_msg( string.Format( "Status: {0}", this.StatusCode ), 2 );
			
				// Probe HTTP Headers
				foreach( string sHeader in res.Headers ) {
					debug_msg( string.Format( "HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader( sHeader ) ), 3 );
				}

				// Stash HTTP Headers
				this.MimeType = res.ContentType;
				this.ContentLength = res.ContentLength;
				debug_msg( string.Format( "Content-Type: {0}", this.MimeType ), 3 );			
				debug_msg( string.Format( "Content-Length: {0}", this.ContentLength.ToString() ), 3 );

				// Get Response Body
				try {
					debug_msg( string.Format( "MIME TYPE: {0}", this.MimeType ), 3 );
					Stream sStream = res.GetResponseStream();
					StreamReader srRead = new StreamReader ( sStream, Encoding.UTF8 ); // Assume UTF-8
					sRawData = srRead.ReadToEnd();
					this.ContentLength = sRawData.Length; // May need to find bytes length
					//debug_msg( string.Format( "sRawData: {0}", sRawData ), 3 );
				} catch( WebException ex ) {
					this.StatusCode = 500;
					sRawData = "";
					this.ContentLength = 0;
				}

				if( sRawData.Length > 0 ) {
					ExCSS.Parser ExCssParser = new  ExCSS.Parser ();
					ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( sRawData );




					this.ProcessCssHyperlinksOut( ExCssStylesheet );

				}

				{ // Title
					MatchCollection reMatches = Regex.Matches( this.Url, "/([^/]+)$" );
					string sTitle = null;
					foreach( Match match in reMatches ) {
						if( match.Groups[ 1 ].Value.Length > 0 ) {
							sTitle = match.Groups[ 1 ].Value.ToString();
							break;
						}
					}
					if( sTitle != null ) {
						this.Title = sTitle;
						debug_msg( string.Format( "TITLE: {0}", this.Title ), 3 );
					} else {
						debug_msg( string.Format( "TITLE: {0}", "MISSING" ), 3 );
					}
				}

				res.Close();

			} else {
				this.StatusCode = 500;
			}

			return( true );
			
		}

		/**************************************************************************/

		void ProcessCssHyperlinksOut ( ExCSS.StyleSheet ExCssStylesheet )
		{

			foreach( var rRule in ExCssStylesheet.StyleRules ) {
						
				int iRule = ExCssStylesheet.StyleRules.IndexOf( rRule );

				foreach( Property pProp in ExCssStylesheet.StyleRules[ iRule ].Declarations.Properties ) {

					if( pProp.Name.Equals( "background-image" ) ) {

						string sBackgroundImageUrl = pProp.Term.ToString();
						string sLinkURLAbs;

						sBackgroundImageUrl = MacroscopeURLTools.CleanUrlCss( sBackgroundImageUrl );
						sLinkURLAbs = MacroscopeURLTools.MakeUrlAbsolute( this.Url, sBackgroundImageUrl );

						debug_msg("");
						debug_msg( string.Format( "sBackgroundImageUrl: {0}", sBackgroundImageUrl ) );
						debug_msg( string.Format( "sBackgroundImageUrl this.Url: {0}", this.Url ) );
						debug_msg( string.Format( "sBackgroundImageUrl sLinkURLAbs: {0}", sLinkURLAbs ) );
						debug_msg("");

						if( this.HyperlinksOut.ContainsKey( sBackgroundImageUrl ) ) {
							this.HyperlinksOut.Remove( sBackgroundImageUrl );
							this.HyperlinksOut.Add( sBackgroundImageUrl, sLinkURLAbs );
						} else {
							this.HyperlinksOut.Add( sBackgroundImageUrl, sLinkURLAbs );
						}

					}

				}

			}

		}

		/**************************************************************************/

	}

}
