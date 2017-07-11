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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private void ProcessTextPage ()
    {

      List<string> TextDoc = new List<string> ();
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
        DebugMsg( string.Format( "ProcessTextPage :: UriFormatException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( WebException ex )
      {

        DebugMsg( string.Format( "ProcessTextPage :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ProcessTextPage :: WebException: {0}", this.DocUrl ) );
        DebugMsg( string.Format( "ProcessTextPage :: WebExceptionStatus: {0}", ex.Status ) );
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

        /** ---------------------------------------------------------------- **/
        
        if( !string.IsNullOrEmpty( RawData ) )
        {
          
          string [] Lines = Regex.Split( RawData, "[\\r\\n]+" );
          TextDoc = Lines.ToList();

          DebugMsg( string.Format( "TextDoc: {0}", TextDoc.Count ) );

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
            && MacroscopePreferencesManager.GetCustomFiltersApplyToText() )
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
            && MacroscopePreferencesManager.GetDataExtractorsApplyToText() )
          {
            this.ProcessGenericDataExtractors( GenericText: RawData );
          }

        }

        /** Process Text Document ------------------------------------------ **/

        if( ( TextDoc != null ) && ( TextDoc.Count > 0 ) )
        {

          if( this.GetPath().EndsWith( "robots.txt", StringComparison.InvariantCultureIgnoreCase ) )
          {
            this.ProcessRobotsTextOutlinks( TextDoc: TextDoc );
          }

          if( this.DetectSitemapTextDocument( TextDoc: TextDoc ) )
          {
            DebugMsg( string.Format( "ProcessTextPage: {0} :: {1}", "SITEMAP DETECTED", this.GetUrl() ) );
            this.SetIsSitemapText();
            this.ProcessSitemapTextOutlinks( TextDoc );
          }

        }
       
        /** ---------------------------------------------------------------- **/
        
        res.Close();

        res.Dispose();
        
      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

    }

    /**************************************************************************/

    Boolean DetectSitemapTextDocument ( List<string> TextDoc )
    {
      
      Boolean IsSitemap = false;

      foreach( string Url in TextDoc )
      {

        string UrlProcessing = Regex.Replace( Url, "\\s+", "" );

        if( !string.IsNullOrEmpty( UrlProcessing ) )
        {

          try
          {

            Uri SitemapUri = new Uri ( UrlProcessing );

            if( SitemapUri != null )
            {
              if( ( SitemapUri.Scheme == "http" ) || ( SitemapUri.Scheme == "https" ) )
              {
                IsSitemap = true;
              }
              else
              {
                IsSitemap = false;
                break;
              }
            }
            
          }
          catch( UriFormatException ex )
          {
            DebugMsg( string.Format( "DetectSitemapTextDocument: UriFormatException: {0}", ex.Message ) );
            DebugMsg( string.Format( "DetectSitemapTextDocument: UriFormatException: {0}", this.GetUrl() ) );
            IsSitemap = false;
            break;
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "DetectSitemapTextDocument: Exception: {0}", ex.Message ) );
            DebugMsg( string.Format( "DetectSitemapTextDocument: Exception: {0}", this.GetUrl() ) );
            IsSitemap = false;
            break;
          }

        }

      }

      DebugMsg( string.Format( "DetectSitemapTextDocument: IsSitemap: {0} :: {1}", IsSitemap, this.GetUrl() ) );
                  
      return( IsSitemap );
      
    }

    /** Text Sitemap Out Links ************************************************/

    private void ProcessSitemapTextOutlinks ( List<string> TextDoc )
    {

      if( this.GetIsExternal() )
      {
        return;
      }
            
      foreach( string Url in TextDoc )
      {

        string UrlProcessing = Regex.Replace( Url, "\\s+", "" );
        string UrlCleaned = null;

        if( !string.IsNullOrEmpty( UrlProcessing ) )
        {

          try
          {
            Uri SitemapUri = new Uri ( UrlProcessing );
            if( SitemapUri != null )
            {
              UrlCleaned = UrlProcessing;
            }
          }
          catch( UriFormatException ex )
          {
            DebugMsg( string.Format( "ProcessSitemapTextOutlinks: {0}", ex.Message ) );
            UrlCleaned = null;
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "ProcessSitemapTextOutlinks: {0}", ex.Message ) );
            UrlCleaned = null;
          }

          if( UrlCleaned != null )
          {
            
            MacroscopeLink Outlink = this.AddSitemapTextOutlink(
                                       AbsoluteUrl: UrlCleaned,
                                       LinkType: MacroscopeConstants.InOutLinkType.SITEMAPTEXT,
                                       Follow: true
                                     );

            if( Outlink != null )
            {
              Outlink.SetRawTargetUrl( UrlCleaned );
            }
            
          }
        
        }

      }

    }

    /** Robots.txt Out Links **************************************************/

    private void ProcessRobotsTextOutlinks ( List<string> TextDoc )
    {

      if( this.GetIsExternal() )
      {
        return;
      }
            
      foreach( string Url in TextDoc )
      {

        if( Regex.IsMatch( Url, "^Sitemap:\\s*[^\\s]+", RegexOptions.IgnoreCase ) )
        {

          string UrlProcessing = Regex.Replace( Url, "^(Sitemap:\\s*)", "", RegexOptions.IgnoreCase );
          string UrlCleaned = null;

          UrlProcessing = UrlProcessing.Trim();
          
          if( !string.IsNullOrEmpty( UrlProcessing ) )
          {

            try
            {
              Uri SitemapUri = new Uri ( UrlProcessing );
              if( SitemapUri != null )
              {
                UrlCleaned = UrlProcessing;
              }
            }
            catch( UriFormatException ex )
            {
              DebugMsg( string.Format( "ProcessRobotsTextOutlinks: {0}", ex.Message ) );
              UrlCleaned = null;
            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "ProcessRobotsTextOutlinks: {0}", ex.Message ) );
              UrlCleaned = null;
            }

            if( UrlCleaned != null )
            {

              MacroscopeLink Outlink = this.AddSitemapTextOutlink(
                                         AbsoluteUrl: UrlCleaned,
                                         LinkType: MacroscopeConstants.InOutLinkType.ROBOTSTEXT,
                                         Follow: true
                                       );
            
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( UrlCleaned );
              }

            }
        
          }

        }
        
      }

    }

    /**************************************************************************/

    private MacroscopeLink AddSitemapTextOutlink (
      string AbsoluteUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      Boolean Follow
    )
    {

      MacroscopeLink OutLink = null;
        
      if( !MacroscopePreferencesManager.GetCheckExternalLinks() )
      {
        MacroscopeAllowedHosts AllowedHosts = this.DocCollection.GetAllowedHosts();
        if( AllowedHosts != null )
        {
          if( !AllowedHosts.IsAllowedFromUrl( Url: AbsoluteUrl ) )
          {
            return( OutLink );
          }
        }
      }

      OutLink = new MacroscopeLink (
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
