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
		
		Boolean IsJavascriptPage()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIs = false;
			Regex reIs = new Regex ( "^(application/javascript|text/javascript)", RegexOptions.IgnoreCase );
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
//				debug_msg( string.Format( "IsJavascriptPage :: UriFormatException: {0}", ex.Message ), 2 );
			} catch( WebException ex ) {
				debug_msg( string.Format( "IsJavascriptPage :: WebException: {0}", ex.Message ), 2 );
			}
			return( bIs );
		}

		/**************************************************************************/
		
		Boolean process_javascript_page()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;

			try {
				req = WebRequest.CreateHttp( this.Url );
				req.Method = "HEAD";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				res = ( HttpWebResponse )req.GetResponse();
			} catch( WebException ex ) {
				debug_msg( string.Format( "process_javascript_page :: WebException: {0}", ex.Message ), 3 );
				debug_msg( string.Format( "process_javascript_page :: WebException: {0}", this.Url ), 3 );
			}

			if( res != null ) {
											
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

	}

}
