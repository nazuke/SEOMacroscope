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

        DebugMsg( string.Format( "ProcessTextPage :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ProcessTextPage :: WebException: {0}", this.DocUrl ) );
        DebugMsg( string.Format( "ProcessTextPage :: WebExceptionStatus: {0}", ex.Status ) );
        ResponseErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        string RawData = "";

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
          RawData = srRead.ReadToEnd();
          
          this.ContentLength = RawData.Length; // May need to find bytes length
          
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

        if( RawData.Length > 0 )
        {
          
          string [] Lines = Regex.Split( RawData, "[\\r\\n]+" );
          TextDoc = Lines.ToList();

          DebugMsg( string.Format( "TextDoc: {0}", TextDoc.Count ) );
        }
        else
        {
          DebugMsg( string.Format( "RawData: {0}", "EMPTY" ) );
        }

        if( ( TextDoc != null ) && ( TextDoc.Count > 0 ) )
        {
          if( this.DetectSitemapTextDocument( TextDoc ) )
          {
            DebugMsg( string.Format( "ProcessTextPage: {0} :: {1}", "SITEMAP DETECTED", this.GetUrl() ) );
            this.SetIsSitemapText();
            this.ProcessSitemapTextOutlinks( TextDoc );
          }
        }
       
        res.Close();

      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

    }

    /**************************************************************************/

    Boolean DetectSitemapTextDocument ( List<string> TextDoc )
    {
      
      Boolean IsSitemapText = true;

      foreach( string Url in TextDoc )
      {

        string UrlProcessing = Regex.Replace( Url, "^\\s*(.+?)\\s*$", "" );

        if( !string.IsNullOrEmpty( UrlProcessing ) )
        {
        
          try
          {
            Uri SitemapUri = new Uri ( UrlProcessing );
          }
          catch
          {
            IsSitemapText = false;
          }

          if( !IsSitemapText )
          {
            break;
          }
          
        }

      }

      return( IsSitemapText );
      
    }

    /**************************************************************************/

    private void ProcessSitemapTextOutlinks ( List<string> TextDoc )
    {

      foreach( string Url in TextDoc )
      {

        string UrlProcessing = Regex.Replace( Url, "^\\s*(.+?)\\s*$", "" );
        string UrlCleaned = null;

        if( !string.IsNullOrEmpty( UrlProcessing ) )
        {
        
          DebugMsg( string.Format( "ProcessSitemapTextOutlinks UrlProcessing: {0}", UrlProcessing ) );
                      
          try
          {
            Uri SitemapUri = new Uri ( UrlProcessing );
            if( SitemapUri != null )
            {
              UrlCleaned = UrlProcessing;
            }
            DebugMsg( string.Format( "ProcessSitemapTextOutlinks UrlCleaned: {0}", UrlCleaned ) );
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "ProcessSitemapTextOutlinks: {0}", ex.Message ) );
          }

          if( UrlCleaned != null )
          {
            
            MacroscopeLink Outlink = this.AddSitemapTextOutlink(
                                       AbsoluteUrl: UrlCleaned,
                                       LinkType: MacroscopeConstants.InOutLinkType.SITEMAPTEXT,
                                       Follow: true
                                     );
            
            Outlink.SetRawTargetUrl( UrlCleaned );
            
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
