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
      string ResponseErrorCondition = null;
      Boolean IsAuthenticating = false;
      
      try
      {

        req = WebRequest.CreateHttp( this.DocUrl );
        req.Method = "GET";
        req.Timeout = this.Timeout;
        req.KeepAlive = false;
        
        this.PrepareRequestHttpHeaders( req: req );

        IsAuthenticating = this.AuthenticateRequest( req );
                                      
        MacroscopePreferencesManager.EnableHttpProxy( req );

        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "ProcessXmlPage :: UriFormatException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( WebException ex )
      {

        DebugMsg( string.Format( "ProcessXmlPage :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ProcessXmlPage :: WebException: {0}", this.DocUrl ) );
        DebugMsg( string.Format( "ProcessXmlPage :: WebExceptionStatus: {0}", ex.Status ) );
        ResponseErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        string RawData = "";

        this.ProcessResponseHttpHeaders( req, res );

        if( IsAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        // Get Response Body
        try
        {
          
          DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );

          Stream ResponseStream = res.GetResponseStream();
          StreamReader ResponseStreamReader = new StreamReader ( ResponseStream, Encoding.UTF8 ); // Assume UTF-8
          RawData = ResponseStreamReader.ReadToEnd();
          
          this.ContentLength = RawData.Length; // May need to find bytes length
          
          this.SetWasDownloaded( true );
          
          this.SetChecksum( RawData );
        
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

          RawData = "";
          this.ContentLength = 0;
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
          this.SetStatusCode( HttpStatusCode.BadRequest );
          RawData = "";
          this.ContentLength = 0;
        }

        if( !string.IsNullOrEmpty( RawData ) )
        {
          
          XmlDoc = new XmlDocument ();
          
          try
          {
            XmlDoc.LoadXml( RawData );
          }
          catch( XmlException ex )
          {
            DebugMsg( string.Format( "XmlException: {0}", ex.Message ) );
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
          }
          
          DebugMsg( string.Format( "XmlDoc: {0}", XmlDoc ) );
        
        }
        else
        {
          DebugMsg( string.Format( "RawData: {0}", "EMPTY" ) );
        }

        /** Custom Filters ------------------------------------------------- **/

        if( !string.IsNullOrEmpty( RawData ) )
        {

          if(
            MacroscopePreferencesManager.GetCustomFiltersEnable()
            && MacroscopePreferencesManager.GetCustomFiltersApplyToXml() )
          {
          
            MacroscopeCustomFilters CustomFilter = this.DocCollection.GetJobMaster().GetCustomFilter();

            if( ( CustomFilter != null ) && ( CustomFilter.IsEnabled() ) )
            {
              this.ProcessGenericCustomFiltered(
                CustomFilter: CustomFilter, 
                GenericText: RawData
              );
            }

          }
          
        }

        /** Data Extractors ------------------------------------------------ **/

        if( !string.IsNullOrEmpty( RawData ) )
        {

          if(
            MacroscopePreferencesManager.GetDataExtractorsEnable()
            && MacroscopePreferencesManager.GetDataExtractorsApplyToXml() )
          {
            this.ProcessGenericDataExtractors( GenericText: RawData );
          }

        }

        /** ---------------------------------------------------------------- **/

        if( ( XmlDoc != null ) & ( XmlDoc.DocumentElement != null ) )
        {
          if( this.DetectSitemapXmlDocument( XmlDoc ) )
          {
            DebugMsg( string.Format( "ProcessXmlPage: {0} :: {1}", "SITEMAP DETECTED", this.GetUrl() ) );
            this.SetIsSitemapXml();
            this.ProcessSitemapXmlOutlinks( XmlDoc );
          }
        }

        res.Close();
        
        res.Dispose();

      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

    }

    /**************************************************************************/

    Boolean DetectSitemapXmlDocument ( XmlDocument XmlDoc )
    {
      
      // Reference: https://www.sitemaps.org/protocol.html
      
      Boolean IsSitemapXml = false;
      
      try
      {
        
        string XmlnsValue = XmlDoc.DocumentElement.GetAttribute( "xmlns" );

        DebugMsg( string.Format( "DetectSitemapXmlDocument sXmlns: {0} :: {1}", XmlnsValue, this.GetUrl() ) );

        if( XmlnsValue != null )
        {
          if( XmlnsValue == MacroscopeConstants.SitemapXmlNamespace )
          {
            DebugMsg( string.Format( "DetectSitemapXmlDocument: {0}", XmlnsValue ) );
            IsSitemapXml = true;
          }
        }
      
      }
      catch( XmlException ex )
      {
        DebugMsg( string.Format( "DetectSitemapXmlDocument: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "DetectSitemapXmlDocument: {0}", ex.Message ) );
      }
      
      return( IsSitemapXml );
    
    }

    /**************************************************************************/

    private void ProcessSitemapXmlOutlinks ( XmlDocument XmlDoc )
    {

      XmlNodeList OutlinksList = XmlDoc.GetElementsByTagName( "loc", MacroscopeConstants.SitemapXmlNamespace );

      DebugMsg( string.Format( "ProcessSitemapXmlOutlinks nlOutlinks: {0}", OutlinksList.Count ) );

      if( OutlinksList != null )
      {

        foreach( XmlNode LinkNode in OutlinksList )
        {

          string LinkUrl = null;

          try
          {
            LinkUrl = LinkNode.InnerText;
            DebugMsg( string.Format( "ProcessSitemapXmlOutlinks sLinkUrl: {0}", LinkUrl ) );
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "ProcessSitemapXmlOutlinks: {0}", ex.Message ) );
          }

          if( LinkUrl != null )
          {
            
            MacroscopeLink Outlink = this.AddSitemapXmlOutlink(
                                       AbsoluteUrl: LinkUrl,
                                       LinkType: MacroscopeConstants.InOutLinkType.SITEMAPXML,
                                       Follow: true
                                     );
            if( Outlink != null )
            {
              Outlink.SetRawTargetUrl( LinkUrl );
            }
            
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

      MacroscopeLink OutLink = null;
      Boolean Proceed = true;
            
      if( !MacroscopePreferencesManager.GetCheckExternalLinks() )
      {
        MacroscopeAllowedHosts AllowedHosts = this.DocCollection.GetAllowedHosts();
        if( AllowedHosts != null )
        {
          if( !AllowedHosts.IsAllowedFromUrl( Url: AbsoluteUrl ) )
          {
            Proceed = false;
          }
        }
      }

      switch( LinkType )
      {
        case MacroscopeConstants.InOutLinkType.SITEMAPXML:
          if( !MacroscopePreferencesManager.GetFetchXml() )
          {
            Proceed = false;
          }
          break;
      }
      
      if( Proceed )
      {

        OutLink = new MacroscopeLink (
          SourceUrl: this.GetUrl(),
          TargetUrl: AbsoluteUrl,
          LinkType: LinkType,
          Follow: Follow
        );

        this.Outlinks.Add( OutLink );
      
      }
      
      return( OutLink );
            
    }

    /**************************************************************************/

  }

}
