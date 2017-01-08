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
				
				if( res != null ) {
					this.ProcessHttpHeaders( req, res );
				}
				
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

				this.ProcessHttpHeaders( req, res );

				// Get Response Body
				try {
					debug_msg( string.Format( "MIME TYPE: {0}", this.MimeType ), 3 );
					Stream sStream = res.GetResponseStream();
					StreamReader srRead = new StreamReader ( sStream, Encoding.UTF8 ); // Assume UTF-8
					sRawData = srRead.ReadToEnd();
					this.ContentLength = sRawData.Length; // May need to find bytes length
					//debug_msg( string.Format( "sRawData: {0}", sRawData ), 3 );
				} catch( WebException ex ) {
					debug_msg( string.Format( "WebException", ex.Message ), 3 );
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
						if( match.Groups[1].Value.Length > 0 ) {
							sTitle = match.Groups[1].Value.ToString();
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

						debug_msg( "" );
						debug_msg( string.Format( "sBackgroundImageUrl: {0}", sBackgroundImageUrl ) );
						debug_msg( string.Format( "sBackgroundImageUrl this.Url: {0}", this.Url ) );
						debug_msg( string.Format( "sBackgroundImageUrl sLinkURLAbs: {0}", sLinkURLAbs ) );
						debug_msg( "" );

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
