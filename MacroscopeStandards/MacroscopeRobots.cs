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
using RobotsTxt;

namespace SEOMacroscope
{


  public class MacroscopeRobots : Macroscope
  {

    /**************************************************************************/

    private Dictionary<string,Robots> RobotSquad;
    
    private Dictionary<Uri,Boolean> BadRobots;

    /**************************************************************************/

    public MacroscopeRobots ()
    {

      this.SuppressDebugMsg = true;

      this.RobotSquad = new Dictionary<string,Robots> ( 8 );

      this.BadRobots = new Dictionary<Uri,Boolean> ( 8 );

    }

    /** ROBOT RULES ***********************************************************/

    public Boolean ApplyRobotRule ( string Url )
    {

      Boolean Allowed = false;

      if( !MacroscopePreferencesManager.GetFollowRobotsProtocol() )
      {
        DebugMsg( string.Format( "ROBOTS Disabled: {0}", Url ) );
        return( true );
      }
      else
      {

        Robots robot = this.FetchRobot( Url: Url );
        Uri BaseUri = null;

        try
        {
          BaseUri = new Uri ( Url, UriKind.Absolute );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( string.Format( "ApplyRobotRule: {0}", ex.Message ) );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "ApplyRobotRule: {0}", ex.Message ) );
        }

        if( ( robot != null ) && ( BaseUri != null ) )
        {

          if( robot.IsPathAllowed( "*", BaseUri.AbsolutePath ) )
          {
            Allowed = true;
          }
          else
          {
            DebugMsg( string.Format( "ROBOTS Disallowed: {0}", Url ) );
            DebugMsg( string.Format( "ROBOTS AbsolutePath: {0}", BaseUri.AbsolutePath ) );
          }

        }

      }

      return( Allowed );

    }

    /** Sitemaps **************************************************************/

    public List<string> GetSitemapsAsList ( string Url )
    {

      List<string> SitemapsList = new List<string> ();
      Robots robot = this.FetchRobot( Url: Url );

      if( ( robot != null ) && ( robot.Sitemaps != null ) )
      {

        foreach( Sitemap SitemapEntry in robot.Sitemaps )
        {

          string SitemapUrl = SitemapEntry.Url.ToString();
          SitemapsList.Add( SitemapUrl );

          DebugMsg( string.Format( "ROBOTS SitemapUrl: {0}", SitemapUrl ) );

        }

      }

      return( SitemapsList );

    }

    /** Crawl Delay ***********************************************************/

    public int GetCrawlDelay ( string Url )
    {

      int Delay = 0;
      Robots robot = this.FetchRobot( Url: Url );

      if( robot != null )
      {

        long CrawlDelayTime = robot.CrawlDelay( this.UserAgent() );

        if( CrawlDelayTime == 0 )
        {
          CrawlDelayTime = robot.CrawlDelay( "*" );
        }

        if( CrawlDelayTime > 0 )
        {
          Delay = ( int )( CrawlDelayTime / 1000 );
        }

        DebugMsg( string.Format( "ROBOTS CrawlDelayTime: {0}", CrawlDelayTime ) );
        DebugMsg( string.Format( "ROBOTS Delay: {0}", Delay ) );

      }

      return( Delay );

    }

    /** Generate Robot URL ****************************************************/

    public static string GenerateRobotUrl ( string Url )
    {

      string RobotUrl = null;
      
      if( MacroscopePreferencesManager.GetFollowRobotsProtocol() )
      {
        
        DebugMsg( string.Format( "ROBOTS Disabled: {0}", Url ), true );

        Uri BaseUri = null;
        string BaseUriPort = "";
        Uri RobotsUri = null;
        string RobotsTxtUrl = null;

        try
        {

          BaseUri = new Uri ( Url, UriKind.Absolute );

          if( BaseUri.Port > 0 )
          {
            BaseUriPort = string.Format( ":{0}", BaseUri.Port );
          }
                  
          RobotsUri = new Uri (
            string.Format(
              "{0}://{1}{2}{3}",
              BaseUri.Scheme,
              BaseUri.Host,
              BaseUriPort,
              "/robots.txt"
            ),
            UriKind.Absolute
          );

          RobotsTxtUrl = RobotsUri.ToString();

        }
        catch( InvalidOperationException ex )
        {
          DebugMsg( string.Format( "GenerateRobotUrl: {0}", ex.Message ), true );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( string.Format( "GenerateRobotUrl: {0}", ex.Message ), true );
        }

        if( !string.IsNullOrEmpty( RobotsTxtUrl ) )
        {
          RobotUrl = RobotsTxtUrl;
        }
    
      }
    
      return( RobotUrl );
      
    }

    /** Fetch Robot ***********************************************************/

    public Robots FetchRobot ( string Url )
    {

      Robots robot = null;

      if( !MacroscopePreferencesManager.GetFollowRobotsProtocol() )
      {
        DebugMsg( string.Format( "ROBOTS Disabled: {0}", Url ) );
        return( robot );
      }

      Uri BaseUri = null;
      Uri RobotsUri = null;
      string RobotsTxtUrl = null;

      try
      {
        
        BaseUri = new Uri ( Url, UriKind.Absolute );

        string BaseUriPort = "";
        
        if( BaseUri.Port > 0 )
        {
          BaseUriPort = string.Format( ":{0}", BaseUri.Port );
        }     

        RobotsUri = new Uri (
          string.Format(
            "{0}://{1}{2}{3}",
            BaseUri.Scheme,
            BaseUri.Host,
            BaseUriPort,
            "/robots.txt"
          ),
          UriKind.Absolute
        );

        RobotsTxtUrl = RobotsUri.ToString();

      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( string.Format( "FetchRobot: {0}", ex.Message ) );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "FetchRobot: {0}", ex.Message ) );
      }

      /*
      lock( this.BadRobots )
      {
        if( !this.BadRobots.ContainsKey( RobotsUri ) )
        {
          return( robot );
        }
      }
      */
     
      if( !string.IsNullOrEmpty( RobotsTxtUrl ) )
      {

        lock( this.RobotSquad )
        {

          if( this.RobotSquad.ContainsKey( RobotsTxtUrl ) )
          {
            robot = this.RobotSquad[ RobotsTxtUrl ];
          }
          else
          {

            String RobotsText = this.FetchRobotTextFile( RobotsUri: RobotsUri );

            if( RobotsText != null )
            {
              robot = new Robots ( content: RobotsText );
              this.RobotSquad.Add( RobotsTxtUrl, robot );
            }

          }

        }

      }

      return( robot );

    }

    /** Fetch Robots Text *****************************************************/

    private string FetchRobotTextFile ( Uri RobotsUri )
    {

      Boolean Proceed = false;
      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string RobotText = "";
      string RawData = "";

      if( !MacroscopeDnsTools.CheckValidHostname( Url: RobotsUri.ToString() ) )
      {
        DebugMsg( string.Format( "FetchRobotTextFile :: CheckValidHostname: {0}", "NOT OK" ) );
        return( RobotText );
      }

      try
      {

        req = WebRequest.CreateHttp( RobotsUri );
        req.Method = "GET";
        req.Timeout = MacroscopePreferencesManager.GetRequestTimeout() * 1000;
        req.KeepAlive = false;
        req.UserAgent = this.UserAgent();
        req.Host = RobotsUri.Host;
        req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        
        MacroscopePreferencesManager.EnableHttpProxy( req );
				
        this.PrepareRequestHttpHeaders( RobotsUri: RobotsUri, req: req );
        
        res = ( HttpWebResponse )req.GetResponse();

        Proceed = true;

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "UriFormatException: {0}", ex.Message ) );
        DebugMsg( string.Format( "Exception: {0}", RobotsUri.ToString() ) );
      }
      catch( WebException ex )
      {
        DebugMsg( string.Format( "WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "WebException: {0}", RobotsUri.ToString() ) );
        DebugMsg( string.Format( "WebExceptionStatus: {0}", ex.Status ) );
      }
      catch( NotSupportedException ex )
      {
        DebugMsg( string.Format( "NotSupportedException: {0}", ex.Message ) );
        DebugMsg( string.Format( "NotSupportedException: {0}", RobotsUri.ToString() ) );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        DebugMsg( string.Format( "Exception: {0}", RobotsUri.ToString() ) );
      }

      if( ( Proceed ) && ( res != null ) )
      {

        try
        {
          Stream ResponseStream = res.GetResponseStream();
          StreamReader ReadStream = new StreamReader ( ResponseStream );
          RawData = ReadStream.ReadToEnd();
        }
        catch( WebException ex )
        {
          DebugMsg( string.Format( "FetchRobotTextFile: WebException: {0}", ex.Message ) );
          RawData = "";
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "FetchRobotTextFile: Exception: {0}", ex.Message ) );
          RawData = "";
        }

        res.Close();
        
        res.Dispose();
      
      }
      else
      {

        lock( this.BadRobots )
        {
          if( !this.BadRobots.ContainsKey( RobotsUri ) )
          {
            this.BadRobots.Add( RobotsUri, true );
            RobotText = "";
          }
        }

      }

      if( !string.IsNullOrEmpty( RawData ) )
      {
        RobotText = RawData;
      }

      return( RobotText );
      
    }

    /** HTTP Headers **********************************************************/

    // https://en.wikipedia.org/wiki/List_of_HTTP_header_fields
    
    private void PrepareRequestHttpHeaders ( Uri RobotsUri, HttpWebRequest req )
    {

      string HostAndPort = RobotsUri.Host;

      if( RobotsUri.Port > 0 )
      {
        HostAndPort = string.Join( ":", RobotsUri.Host, RobotsUri.Port.ToString() );
      }

      req.Host = HostAndPort;

      req.UserAgent = this.UserAgent();

      req.Accept = "*/*";
      
      //req.Headers.Add( "Host", HostAndPort );
      
      req.Headers.Add( "Accept-Charset", "utf-8, us-ascii" );

      req.Headers.Add( "Accept-Encoding", "gzip, deflate" );

      req.Headers.Add( "Accept-Language", "*" );

      return;
      
    }

    /**************************************************************************/
    
  }

}
