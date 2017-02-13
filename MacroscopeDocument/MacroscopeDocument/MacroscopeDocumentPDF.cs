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
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SEOMacroscope
{

	public partial class MacroscopeDocument : Macroscope
	{

		/**************************************************************************/

		void ProcessPdfPage ()
		{

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

				DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ex.Message ) );
				DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ex.Status ) );
				DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ( int )ex.Status ) );

				sErrorCondition = ex.Status.ToString();

			}

			if( res != null ) {

				MacroscopePdfTools pdfTools;

				this.ProcessHttpHeaders( req, res );

				{ // Probe Locale
					this.Locale = "en"; // Implement locale probing
					this.SetHreflang( this.Locale, this.Url );
				}

				{ // Canonical
					this.Canonical = this.Url;
					DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
				}

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
						pdfTools = new MacroscopePdfTools ( aRawData );

					} catch( WebException ex ) {

						DebugMsg( string.Format( "WebException", ex.Message ) );
						pdfTools = null;
						this.ContentLength = 0;

					} catch( Exception ex ) {

						DebugMsg( string.Format( "Exception", ex.Message ) );
						this.StatusCode = ( int )HttpStatusCode.BadRequest;
						pdfTools = null;
						this.ContentLength = 0;

					}
				}

				{ // Title
					if( pdfTools != null ) {
						string sTitle = pdfTools.GetTitle();
						if( sTitle != null ) {
							this.Title = sTitle;
							DebugMsg( string.Format( "TITLE: {0}", this.Title ) );
						} else {
							DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
						}
					}
				}

				res.Close();

			}

			if( sErrorCondition != null ) {
				this.ProcessErrorCondition( sErrorCondition );
			}

		}

		/**************************************************************************/

	}

}
