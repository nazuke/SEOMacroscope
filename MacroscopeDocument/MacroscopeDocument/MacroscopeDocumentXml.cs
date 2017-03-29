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

    private void ProcessXmlPage ()
    {

      XmlDocument XmlDoc = null;
      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string sErrorCondition = null;
      Boolean bAuthenticating = false;
      
      try
      {

        req = WebRequest.CreateHttp( this.DocUrl );
        req.Method = "GET";
        req.Timeout = this.Timeout;
        req.KeepAlive = false;
        
        this.PrepareRequestHttpHeaders( req: req );

        bAuthenticating = this.AuthenticateRequest( req );
                                      
        MacroscopePreferencesManager.EnableHttpProxy( req );

        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( WebException ex )
      {

        DebugMsg( string.Format( "ProcessXmlPage :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ProcessXmlPage :: WebException: {0}", this.DocUrl ) );
        DebugMsg( string.Format( "ProcessXmlPage :: WebExceptionStatus: {0}", ex.Status ) );
        sErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        string sRawData = "";

        this.ProcessResponseHttpHeaders( req, res );

        if( bAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        // Get Response Body
        try
        {
          
          DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );

          Stream sStream = res.GetResponseStream();
          StreamReader srRead = new StreamReader ( sStream, Encoding.UTF8 ); // Assume UTF-8
          sRawData = srRead.ReadToEnd();
          
          this.ContentLength = sRawData.Length; // May need to find bytes length
          
          this.SetWasDownloaded( true );
        
        }
        catch( WebException ex )
        {
          DebugMsg( string.Format( "WebException: {0}", ex.Message ) );

          if( ex.Response != null )
          {
            this.SetStatusCode( ( ( HttpWebResponse )ex.Response ).StatusCode );
          }
          else
          {
            this.SetStatusCode( ( HttpStatusCode )ex.Status );
          }

          sRawData = "";
          this.ContentLength = 0;
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
          this.SetStatusCode( HttpStatusCode.BadRequest );
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
          if( this.DetectSitemapXmlDocument( XmlDoc ) )
          {
            DebugMsg( string.Format( "ProcessXmlPage: {0} :: {1}", "SITEMAP DETECTED", this.GetUrl() ) );
            this.SetIsSitemapXml();
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

    Boolean DetectSitemapXmlDocument ( XmlDocument XmlDoc )
    {
      // Reference: https://www.sitemaps.org/protocol.html
      Boolean bIsSitemapXml = false;
      string sXmlns = XmlDoc.DocumentElement.GetAttribute( "xmlns" );

      DebugMsg( string.Format( "DetectSitemapXmlDocument sXmlns: {0} :: {1}", sXmlns, this.GetUrl() ) );

      if( sXmlns != null )
      {
        if( sXmlns == MacroscopeConstants.SitemapXmlNamespace )
        {
          DebugMsg( string.Format( "DetectSitemapXmlDocument: {0}", sXmlns ) );
          bIsSitemapXml = true;
        }
      }
      return( bIsSitemapXml );
    }

    /**************************************************************************/

    private void ProcessSitemapXmlOutlinks ( XmlDocument XmlDoc )
    {

      XmlNodeList nlOutlinks = XmlDoc.GetElementsByTagName( "loc", MacroscopeConstants.SitemapXmlNamespace );

      DebugMsg( string.Format( "ProcessSitemapXmlOutlinks nlOutlinks: {0}", nlOutlinks.Count ) );

      if( nlOutlinks != null )
      {

        foreach( XmlNode nLoc in nlOutlinks )
        {

          string sLinkUrl = null;

          try
          {
            sLinkUrl = nLoc.InnerText;
            DebugMsg( string.Format( "ProcessSitemapXmlOutlinks sLinkUrl: {0}", sLinkUrl ) );
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "ProcessSitemapXmlOutlinks: {0}", ex.Message ) );
          }

          if( sLinkUrl != null )
          {
            
            MacroscopeLink Outlink = this.AddSitemapXmlOutlink(
                                       AbsoluteUrl: sLinkUrl,
                                       LinkType: MacroscopeConstants.InOutLinkType.SITEMAPXML,
                                       Follow: true
                                     );
            
            Outlink.SetRawTargetUrl( sLinkUrl );
            
          }

        }

      }

    }

    /**************************************************************************/

    private MacroscopeLink AddSitemapXmlOutlink (
      string AbsoluteUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      Boolean Follow
    )
    {

      MacroscopeLink OutLink = new MacroscopeLink (
                                 SourceUrl: this.GetUrl(),
                                 TargetUrl: AbsoluteUrl,
                                 LinkType: LinkType,
                                 Follow: Follow
                               );

      this.Outlinks.Add( OutLink );
      
      return( OutLink );
            
    }

    /**************************************************************************/

  }

}
