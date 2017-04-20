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
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  public class MacroscopeUrlUtils : Macroscope
  {

    /**************************************************************************/

    static MacroscopeUrlUtils ()
    {
      SuppressStaticDebugMsg = true;
    }

    public MacroscopeUrlUtils ()
    {
      SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public static Boolean ValidateUrl ( string Url )
    {

      Boolean IsValid = false;

      if( Url.StartsWith( "http", StringComparison.Ordinal ) )
      {
      
        try
        {

          Uri CheckUri = new Uri ( Url, UriKind.Absolute );

          if( CheckUri != null )
          {
            IsValid = true;
          }

        }
        catch( UriFormatException ex )
        {

          DebugMsg( string.Format( "ValidateUrl: {0}", ex.Message ), true );

        }
      
      }
         
      return( IsValid );

    }

    /**************************************************************************/

    public static string SanitizeUrl ( string Url )
    {

      string Sanitized;
      DebugMsg( string.Format( "SanitizeUrl 1: {0}", Url ), true );

      Sanitized = Uri.EscapeUriString( Url );

      DebugMsg( string.Format( "SanitizeUrl 2: {0}", Sanitized ), true );

      return( Sanitized );
    }

    /**************************************************************************/

    public static string MakeUrlAbsolute ( string BaseUrl, string Url )
    {

      string UrlFixed;
      Uri BaseUri = null;
      string BaseUriPort = "";
      Uri NewUri = null;

      Regex reHTTP = new Regex ( "^https?:" );
      Regex reDoubleSlash = new Regex ( "^//" );
      Regex reSlash = new Regex ( "^/" );
      Regex reQuery = new Regex ( "^\\?" );
      Regex reHash = new Regex ( "^#" );
      Regex reUnsupportedScheme = new Regex ( "^[^:]+:" );

      BaseUrl = HtmlEntity.DeEntitize( BaseUrl );
      BaseUrl = Uri.UnescapeDataString( BaseUrl );
      
      Url = HtmlEntity.DeEntitize( Url );
      Url = Uri.UnescapeDataString( Url );

      try
      {
        
        BaseUri = new Uri ( BaseUrl, UriKind.Absolute );
        
        if( BaseUri.Port > 0 )
        {
          BaseUriPort = string.Format( ":{0}", BaseUri.Port );
        }
        
      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "MakeUrlAbsolute: {0}", ex.Message ), true );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "MakeUrlAbsolute: {0}", ex.Message ), true );
      }

      if( BaseUri == null )
      {
        throw new MacroscopeUriFormatException ( "Malformed Base URI" );
      }

      if( !Regex.IsMatch( Url, "^(https?:|/|#)" ) )
      {
        DebugMsg( string.Format( "STRANGE URL: 1: {0}", BaseUrl ), true );
        DebugMsg( string.Format( "STRANGE URL: 2: {0}", Url ), true );

      }

      if( !reHTTP.IsMatch( Url ) )
      {
        Boolean IsSuspect = false;
        if(
          ( !reDoubleSlash.IsMatch( Url ) )
          && ( !reSlash.IsMatch( Url ) )
          && ( !reQuery.IsMatch( Url ) )
          && ( !reHash.IsMatch( Url ) ) )
        {
          if( reUnsupportedScheme.IsMatch( Url ) )
          {
            IsSuspect = true;
          }
        }
        if( IsSuspect )
        {
          DebugMsg( string.Format( "STRANGE URL: IS SUSPECT: {0}", Url ), true );
          return( null );
        }
      }

      if( reDoubleSlash.IsMatch( Url ) )
      {

        try
        {
          NewUri = new Uri (
            string.Format(
              "{0}:{1}",
              BaseUri.Scheme,
              Url
            ),
            UriKind.Absolute
          );

        }
        catch( InvalidOperationException ex )
        {
          DebugMsg( ex.Message, true );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( ex.Message, true );
        }

      }
      else
      if( reSlash.IsMatch( Url ) )
      {

        try
        {
          NewUri = new Uri (
            string.Format(
              "{0}://{1}{2}{3}",
              BaseUri.Scheme,
              BaseUri.Host,
              BaseUriPort,
              Url
            ),
            UriKind.Absolute
          );

        }
        catch( InvalidOperationException ex )
        {
          DebugMsg( ex.Message, true );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( ex.Message, true );
        }

      }
      else
      if( reQuery.IsMatch( Url ) )
      {

        try
        {
          NewUri = new Uri (
            string.Format(
              "{0}://{1}{2}{3}{4}",
              BaseUri.Scheme,
              BaseUri.Host,
              BaseUriPort,
              BaseUri.AbsolutePath,
              Url
            ),
            UriKind.Absolute
          );

        }
        catch( InvalidOperationException ex )
        {
          DebugMsg( ex.Message, true );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( ex.Message, true );
        }

      }
      else
      if( reHash.IsMatch( Url ) )
      {

        string NewUrl = Url;
        Regex reHashRemove = new Regex ( "#.*$", RegexOptions.Singleline );
        NewUrl = reHashRemove.Replace( NewUrl, "" );

        try
        {
          NewUri = new Uri (
            string.Format(
              "{0}://{1}{2}{3}",
              BaseUri.Scheme,
              BaseUri.Host,
              BaseUriPort,
              NewUrl
            ),
            UriKind.Absolute
          );

        }
        catch( InvalidOperationException ex )
        {
          DebugMsg( ex.Message, true );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( ex.Message, true );
        }

      }
      else
      if( reHTTP.IsMatch( Url ) )
      {

        try
        {
          NewUri = new Uri ( Url, UriKind.Absolute );
        }
        catch( InvalidOperationException ex )
        {
          DebugMsg( ex.Message, true );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( ex.Message, true );
        }

      }
      else
      if( reUnsupportedScheme.IsMatch( Url ) )
      {

        ; // NO-OP, for now.

      }
      else
      {


        DebugMsg( string.Format( "RELATIVE URL 1: {0}", Url ), true );

        string BasePath = Regex.Replace( BaseUri.AbsolutePath, "/[^/]+$", "/" );
        string NewPath = string.Join( "", BasePath, Url );

        DebugMsg( string.Format( "RELATIVE URL 2: {0}", BasePath ), true );
        DebugMsg( string.Format( "RELATIVE URL 3: {0}", NewPath ), true );


        try
        {
          NewUri = new Uri (
            string.Format(
              "{0}://{1}{2}{3}",
              BaseUri.Scheme,
              BaseUri.Host,
              BaseUriPort,
              NewPath
            ),
            UriKind.Absolute
          );

        }
        catch( InvalidOperationException ex )
        {
          DebugMsg( ex.Message, true );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( ex.Message, true );
        }

      }

      if( NewUri != null )
      {
        UrlFixed = NewUri.ToString();
      }
      else
      {
        UrlFixed = Url;
      }

      UrlFixed = SanitizeUrl( UrlFixed );

      return( UrlFixed );

    }

    /**************************************************************************/

    public static Boolean VerifySameHost ( string BaseUrL, string Url )
    {
      Boolean Success = false;
      Uri BaseUri = null;
      Uri NewUri = null;

      try
      {
        BaseUri = new Uri ( BaseUrL, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", BaseUrL ), true );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", BaseUrL ), true );
      }
      catch( Exception ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", BaseUrL ), true );
      }

      try
      {
        NewUri = new Uri ( Url, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", Url ), true );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", Url ), true );
      }
      catch( Exception ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", Url ), true );
      }

      try
      {
        if( ( BaseUri != null ) && ( NewUri != null ) && ( BaseUri.Host.ToString() == NewUri.Host.ToString() ) )
        {
          Success = true;
        }
      }
      catch( UriFormatException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", Url ), true );
      }

      return( Success );
    }

    /**************************************************************************/

    public static int FindUrlDepth ( string Url )
    {

      int Depth = 0;
      Uri DocumentURI = null;

      try
      {
        DocumentURI = new Uri ( Url, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( ex.Message, true );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( ex.Message, true );
      }

      if( DocumentURI != null )
      {
        string sPath = DocumentURI.AbsolutePath;
        Depth = sPath.Split( '/' ).Length - 1;
      }

      return( Depth );

    }

    /**************************************************************************/

    public static string CleanUrlCss ( string sProperty )
    {

      string sCleaned = null;

      
      DebugMsg( string.Format( "CleanUrlCss: sProperty: {0}", sProperty ), true );
      
      if( Regex.IsMatch( sProperty, "url\\([^()]+\\)" ) )
      {

        DebugMsg( string.Format( "CleanUrlCss: HAS URL: {0}", sProperty ), true );
      
        MatchCollection reMatches = Regex.Matches(
                                      sProperty,
                                      "url\\(\\s*[\"']?(.+?)[\"']?\\s*\\)",
                                      RegexOptions.IgnoreCase
                                    );

        DebugMsg( string.Format( "CleanUrlCss: reMatches: {0}", reMatches.Count ), true );

        foreach( Match match in reMatches )
        {

          if( match.Groups[ 1 ].Value.Length > 0 )
          {
            sCleaned = match.Groups[ 1 ].Value;
            break;
          }

        }

        if( sCleaned.ToLower() == "none" )
        {
          sCleaned = null;
        }
       
      }
      else
      {

        sCleaned = null;

      }

      DebugMsg( string.Format( "CleanUrlCss: sCleaned: {0}", sCleaned ), true );
      
      return( sCleaned );

    }

    /**************************************************************************/

    public static string GetHostnameFromUrl ( string Url )
    {

      Uri DocumentUri = null;
      string Hostname = null;
      
      try
      {
        DocumentUri = new Uri ( Url, UriKind.Absolute );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "GetHostnameFromUrl: {0}", ex.Message ), true );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "GetHostnameFromUrl: {0}", ex.Message ), true );
      }

      if( DocumentUri != null )
      {
        Hostname = DocumentUri.Host;
      }

      return( Hostname );

    }
    
    public static string GetHostnameAndPortFromUrl ( string Url )
    {

      Uri DocumentUri = null;
      string HostnameAndPort = null;
      
      try
      {
        DocumentUri = new Uri ( Url, UriKind.Absolute );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "GetHostnameAndPortFromUrl: {0}", ex.Message ), true );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "GetHostnameAndPortFromUrl: {0}", ex.Message ), true );
      }

      if( DocumentUri != null )
      {
        HostnameAndPort = string.Join( ":", DocumentUri.Host, DocumentUri.Port );
      }

      return( HostnameAndPort );

    }
    
    /**************************************************************************/
    
    public static string StripQueryString ( string Url )
    {

      Uri UriBase = null;
      Uri UriNew = null;
      string NewUrl = null;

      try
      {
        UriBase = new Uri ( Url, UriKind.Absolute );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "StripQueryString: {0}", ex.Message ), true );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "StripQueryString: {0}", ex.Message ), true );
      }

      if( UriBase != null )
      {

        try
        {
          UriNew = new Uri (
            string.Format(
              "{0}://{1}{2}",
              UriBase.Scheme,
              UriBase.Host,
              UriBase.AbsolutePath
            ),
            UriKind.Absolute
          );

        }
        catch( InvalidOperationException ex )
        {
          DebugMsg( ex.Message, true );
        }
        catch( UriFormatException ex )
        {
          DebugMsg( ex.Message, true );
        }
      
        if( UriBase != null )
        {
          NewUrl = UriNew.ToString();
        }
        else
        {
          NewUrl = Url;
        }

      }
      else
      {
        NewUrl = Url;
      }

      return( NewUrl );

    }
      
    /**************************************************************************/

  }

}
