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
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace SEOMacroscope
{

	public partial class MacroscopeDocument : Macroscope
	{

		/**************************************************************************/
		
		void ProcessSitemapXmlPage ()
		{

			XmlDocument XmlDoc = null;
			HttpWebRequest req = null;
			HttpWebResponse res = null;
			string sErrorCondition = null;
			
			try
			{

				req = WebRequest.CreateHttp( this.Url );
				req.Method = "GET";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				MacroscopePreferencesManager.EnableHttpProxy( req );
				res = ( HttpWebResponse )req.GetResponse();

			}
			catch( WebException ex )
			{

				DebugMsg( string.Format( "ProcessSitemapXmlPage :: WebException: {0}", ex.Message ) );
				DebugMsg( string.Format( "ProcessSitemapXmlPage :: WebException: {0}", this.Url ) );
				DebugMsg( string.Format( "ProcessSitemapXmlPage :: WebExceptionStatus: {0}", ex.Status ) );
				sErrorCondition = ex.Status.ToString();

			}

			if( res != null )
			{
				
				string sRawData = "";
							
				this.ProcessHttpHeaders( req, res );

				// Get Response Body
				try
				{
					DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );
					Stream sStream = res.GetResponseStream();
					StreamReader srRead = new StreamReader ( sStream, Encoding.UTF8 ); // Assume UTF-8
					sRawData = srRead.ReadToEnd();
					this.ContentLength = sRawData.Length; // May need to find bytes length
					DebugMsg( string.Format( "ProcessSitemapXmlPage :: sRawData: {0}", sRawData ) );
				}
				catch( WebException ex )
				{
					DebugMsg( string.Format( "WebException", ex.Message ) );
					sRawData = "";
					this.ContentLength = 0;
				}
				catch( Exception ex )
				{
					DebugMsg( string.Format( "Exception", ex.Message ) );
					this.StatusCode = ( int )HttpStatusCode.BadRequest;
					sRawData = "";
					this.ContentLength = 0;
				}
				
				if( sRawData.Length > 0 )
				{
					XmlDoc = new XmlDocument ();
					XmlDoc.LoadXml( sRawData );
					DebugMsg( string.Format( "XmlDoc: {0}", XmlDoc ) );
				}
				else
				{
					DebugMsg( string.Format( "sRawData: {0}", "EMPTY" ) );
				}

				if( XmlDoc != null )
				{

					{ // Outlinks
						this.ProcessSitemapXmlOutlinks( XmlDoc );
					}	

				}
				
				res.Close();

			}

			if( sErrorCondition != null )
			{
				this.ProcessErrorCondition( sErrorCondition );
			}
			
		}

		/**************************************************************************/

		void ProcessSitemapXmlOutlinks ( XmlDocument XmlDoc )
		{

			XmlNodeList nOutlinks = XmlDoc.SelectNodes( "//url/loc" );

			if( nOutlinks != null )
			{

				foreach( XmlNode nLoc in nOutlinks )
				{
					
					string sLinkUrl = null;
					
					try
					{
						sLinkUrl = nLoc.Attributes.GetNamedItem( "href" ).InnerText;
					}
					catch( Exception ex )
					{
						DebugMsg( string.Format( "ProcessSitemapXmlOutlinks: {0}", ex.Message ) );
					}
					
					if( sLinkUrl != null )
					{
						this.AddSitemapXmlOutlink( sLinkUrl, sLinkUrl, MacroscopeConstants.OutlinkType.SITEMAPXML, true );
					}

				}

			}

		}

		/**************************************************************************/

		void AddSitemapXmlOutlink ( string sRawUrl, string sAbsoluteUrl, MacroscopeConstants.OutlinkType sType, Boolean bFollow )
		{

			MacroscopeOutlink OutLink = new MacroscopeOutlink ( sRawUrl, sAbsoluteUrl, sType, bFollow );

			if( this.Outlinks.ContainsKey( sRawUrl ) )
			{
				this.Outlinks.Remove( sRawUrl );
				this.Outlinks.Add( sRawUrl, OutLink );
			}
			else
			{
				this.Outlinks.Add( sRawUrl, OutLink );
			}

		}

		/**************************************************************************/

	}

}
