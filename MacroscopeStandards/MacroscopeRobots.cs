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

    Dictionary<string,Robots> RobotsDic;

    /**************************************************************************/

    public MacroscopeRobots ()
    {
      RobotsDic = new Dictionary<string,Robots> ( 32 );
    }

    /** ROBOT RULES ***********************************************************/

    public Boolean ApplyRobotRule ( string sUrl )
    {

      Boolean bAllowed = false;

      if( !MacroscopePreferencesManager.GetFollowRobotsProtocol() )
      {
        DebugMsg( string.Format( "ROBOTS Disabled: {0}", sUrl ) );
        return( true );
      }
      else
      {

        Robots robot = this.FetchRobot( sUrl );
        Uri uBase = new Uri ( sUrl, UriKind.Absolute );

        if( ( robot != null ) && ( uBase != null ) )
        {

          if( robot.IsPathAllowed( "*", uBase.AbsolutePath ) )
          {
            bAllowed = true;
          }
          else
          {
            DebugMsg( string.Format( "ROBOTS Disallowed: {0}", sUrl ) );
            DebugMsg( string.Format( "ROBOTS AbsolutePath: {0}", uBase.AbsolutePath ) );
          }

        }

      }

      return( bAllowed );

    }

    /** Sitemaps **************************************************************/

    public List<string> GetSitemapsAsList ( string sUrl )
    {

      List<string> SitemapsList = new List<string> ();
      Robots robot = this.FetchRobot( sUrl );

      if( robot != null )
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

      int iDelay = 0;
      Robots robot = this.FetchRobot( Url );

      if( robot != null )
      {

        long iGetCrawlDelay = robot.CrawlDelay( "*" );

        if( iGetCrawlDelay > 0 )
        {
          iDelay = ( int )( iGetCrawlDelay / 1000 );
        }

        DebugMsg( string.Format( "ROBOTS iGetCrawlDelay: {0}", iGetCrawlDelay ) );
        DebugMsg( string.Format( "ROBOTS iDelay: {0}", iDelay ) );

      }

      return( iDelay );

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

      Uri uBase = new Uri ( Url, UriKind.Absolute );
      Uri uRobotsUri = null;
      string sRobotsTxtUrl = null;

      try
      {
        uRobotsUri = new Uri (
          string.Format(
            "{0}://{1}{2}",
            uBase.Scheme,
            uBase.Host,
            "/robots.txt"
          ),
          UriKind.Absolute
        );

        sRobotsTxtUrl = uRobotsUri.ToString();

      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( string.Format( "FetchRobot: {0}", ex.Message ) );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "FetchRobot: {0}", ex.Message ) );
      }

      if( sRobotsTxtUrl != null )
      {

        lock( this.RobotsDic )
        {

          if( this.RobotsDic.ContainsKey( sRobotsTxtUrl ) )
          {
            robot = this.RobotsDic[ sRobotsTxtUrl ];
          }
          else
          {

            String sRobotsText = this.FetchRobotTextFile( RobotsUri: uRobotsUri );

            if( sRobotsText.Length > 0 )
            {
              robot = new Robots ( sRobotsText );
              this.RobotsDic.Add( sRobotsTxtUrl, robot );
            }

          }

        }

      }

      return( robot );

    }

    /** Fetch Robots Text *****************************************************/

    string FetchRobotTextFile ( Uri RobotsUri )
    {
      Boolean bProceed = false;
      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string RobotText = "";
      string sRawData = "";

      if( !MacroscopeDnsTools.CheckValidHostname( Url: RobotsUri.ToString() ) )
      {
        DebugMsg( string.Format( "FetchRobotTextFile :: CheckValidHostname: {0}", "NOT OK" ) );
        return( RobotText );
      }

      try
      {

        req = WebRequest.CreateHttp( RobotsUri );
        req.Method = "GET";
        req.Timeout = 30000; // 30 seconds
        req.KeepAlive = false;
        req.UserAgent = this.UserAgent();
        req.Host = RobotsUri.Host;

        MacroscopePreferencesManager.EnableHttpProxy( req );
				
        res = ( HttpWebResponse )req.GetResponse();
				
        bProceed = true;

      }
      catch( WebException ex )
      {
        DebugMsg( string.Format( "FetchRobotTextFile :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "FetchRobotTextFile :: WebException: {0}", RobotsUri.ToString() ) );
        DebugMsg( string.Format( "FetchRobotTextFile :: WebExceptionStatus: {0}", ex.Status ) );
      }
      catch( NotSupportedException ex )
      {
        DebugMsg( string.Format( "FetchRobotTextFile :: NotSupportedException: {0}", ex.Message ) );
        DebugMsg( string.Format( "FetchRobotTextFile :: NotSupportedException: {0}", RobotsUri.ToString() ) );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "FetchRobotTextFile :: Exception: {0}", ex.Message ) );
        DebugMsg( string.Format( "FetchRobotTextFile :: Exception: {0}", RobotsUri.ToString() ) );
      }

      if( ( bProceed ) && ( res != null ) )
      {
        try
        {
          Stream sStream = res.GetResponseStream();
          StreamReader srRead = new StreamReader ( sStream );
          sRawData = srRead.ReadToEnd();
        }
        catch( WebException ex )
        {
          DebugMsg( string.Format( "FetchRobotTextFile: WebException", ex.Message ) );
          sRawData = "";
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "FetchRobotTextFile: Exception", ex.Message ) );
          sRawData = "";
        }
      }

      if( sRawData.Length > 0 )
      {
        RobotText = sRawData;
      }

      return( RobotText );
    }

    /**************************************************************************/

  }

}
