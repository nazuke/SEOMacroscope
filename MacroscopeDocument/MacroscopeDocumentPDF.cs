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

		Boolean IsPdfPage ()
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bIs = false;
			Regex reIs = new Regex ( "^application/pdf", RegexOptions.IgnoreCase );
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
//				debug_msg( string.Format( "IsPdfPage :: UriFormatException: {0}", ex.Message ), 2 );
			} catch( WebException ex ) {
				debug_msg( string.Format( "IsPdfPage :: WebException: {0}", ex.Message ), 2 );
			}
			return( bIs );
		}

		/**************************************************************************/
		
		Boolean ProcessPdfPage ()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;

			try {
				req = WebRequest.CreateHttp( this.Url );
				req.Method = "GET";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				res = ( HttpWebResponse )req.GetResponse();
			} catch( WebException ex ) {
				debug_msg( string.Format( "ProcessPdfPage :: WebException: {0}", ex.Message ), 3 );
				debug_msg( string.Format( "ProcessPdfPage :: WebException: {0}", this.Url ), 3 );
			}

			if( res != null ) {

				MacroscopePDFTools pdfTools;
				
				{ // Get Response Body
					try {
						Stream sStream = res.GetResponseStream();
						List<byte> aRawDataList = new List<byte> ();
						byte[] aRawData;
						do {
							int buf = sStream.ReadByte();
							if( buf > -1 ) {
								aRawDataList.Add( ( byte )buf );
							} else {
								break;
							}
						} while( sStream.CanRead );
						aRawData = aRawDataList.ToArray();
						this.ContentLength = aRawData.Length;
						pdfTools = new MacroscopePDFTools ( aRawData );
					} catch( WebException ex ) {
						pdfTools = null;
						this.StatusCode = 500;
						this.ContentLength = 0;
					}
				}

				// Status Code
				this.StatusCode = this.ProcessStatusCode( res.StatusCode );
				debug_msg( string.Format( "Status: {0}", this.StatusCode ), 2 );

				{ // Probe Locale
					this.Locale = "en"; // Implement locale probing
					this.SetHreflang( this.Locale, this.Url );
				}
				
				{ // Canonical
					this.Canonical = this.Url;
					debug_msg( string.Format( "CANONICAL: {0}", this.Canonical ), 3 );
				}
				
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
					if( pdfTools != null ) {
						string sTitle = pdfTools.GetTitle();
						if( sTitle != null ) {
							this.Title = sTitle;
							debug_msg( string.Format( "TITLE: {0}", this.Title ), 3 );
						} else {
							debug_msg( string.Format( "TITLE: {0}", "MISSING" ), 3 );
						}
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
