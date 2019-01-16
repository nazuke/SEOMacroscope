/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SEOMacroscope
{

  public class MacroscopeHttpUrlUtils : Macroscope
  {

    /**************************************************************************/

    static MacroscopeHttpUrlUtils ()
    {
      SuppressStaticDebugMsg = true;
    }

    public MacroscopeHttpUrlUtils ()
    {
      SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public static bool ValidateUrl ( string Url )
    {

      bool IsValid = false;

      if( Url.StartsWith( "http", StringComparison.Ordinal ) )
      {

        try
        {

          Uri CheckUri = new Uri( Url, UriKind.Absolute );

          if( CheckUri != null )
          {
            IsValid = true;
          }

        }
        catch( UriFormatException ex )
        {

          DebugMsgStatic( string.Format( "ValidateUrl: {0}", ex.Message ) );

        }

      }

      return ( IsValid );

    }

    /**************************************************************************/

    public static string SanitizeUrl ( string Url )
    {

      string Unsanitary;
      string Sanitized;

      DebugMsgStatic( string.Format( "SanitizeUrl 1: {0}", Url ) );

      Unsanitary = Uri.UnescapeDataString( Url );
      Sanitized = Uri.EscapeUriString( Unsanitary );

      DebugMsgStatic( string.Format( "SanitizeUrl 2: {0}", Sanitized ) );

      return ( Sanitized );

    }

    /**************************************************************************/

    /*

      Reference: https://www.w3.org/TR/html5/document-metadata.html#the-base-element

    */

    public static string MakeUrlAbsolute (
      string BaseHref,
      string BaseUrl,
      string Url
    )
    {

      string AbsoluteBaseHref;
      string UrlFixed;

      if( !string.IsNullOrEmpty( value: BaseHref ) )
      {

        AbsoluteBaseHref = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
          BaseUrl: BaseUrl,
          Url: BaseHref
        );

        DebugMsgStatic( string.Format( "BASEHREF: {0}", BaseHref ) );
        DebugMsgStatic( string.Format( "ABSOLUTEBASEHREF: {0}", AbsoluteBaseHref ) );

        UrlFixed = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
          BaseUrl: AbsoluteBaseHref,
          Url: Url
        );

        DebugMsgStatic( string.Format( "URL: {0}", Url ) );
        DebugMsgStatic( string.Format( "URLFIXED: {0}", UrlFixed ) );

      }
      else
      {

        UrlFixed = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
          BaseUrl: BaseUrl,
          Url: Url
        );

      }

      return ( UrlFixed );

    }

    /**************************************************************************/

    public static string MakeUrlAbsolute (
      string BaseUrl,
      string Url
    )
    {

      string UrlFixed;
      Uri BaseUri = null;
      string BaseUriPort = "";
      Uri NewUri = null;

      Regex reHTTP = new Regex( "^https?:" );
      Regex reDoubleSlash = new Regex( "^//" );
      Regex reSlash = new Regex( "^/" );
      Regex reQuery = new Regex( "^\\?" );
      Regex reHash = new Regex( "^#" );
      Regex reUnsupportedScheme = new Regex( "^[^:]+:" );

      BaseUrl = HtmlEntity.DeEntitize( BaseUrl );
      BaseUrl = Uri.UnescapeDataString( BaseUrl );

      Url = HtmlEntity.DeEntitize( Url );
      Url = Uri.UnescapeDataString( Url );

      try
      {

        BaseUri = new Uri( BaseUrl, UriKind.Absolute );

        if( BaseUri.Port > 0 )
        {
          BaseUriPort = string.Format( ":{0}", BaseUri.Port );
        }

      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "MakeUrlAbsolute: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "MakeUrlAbsolute: {0}", ex.Message ) );
      }

      if( BaseUri == null )
      {
        throw new MacroscopeUriFormatException( "Malformed Base URI" );
      }

      if( !Regex.IsMatch( Url, "^(https?:|/|#)" ) )
      {
        DebugMsgStatic( string.Format( "STRANGE URL: 1: {0}", BaseUrl ) );
        DebugMsgStatic( string.Format( "STRANGE URL: 2: {0}", Url ) );

      }

      if( !reHTTP.IsMatch( Url ) )
      {
        bool IsSuspect = false;
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
          DebugMsgStatic( string.Format( "STRANGE URL: IS SUSPECT: {0}", Url ) );
          return ( null );
        }
      }

      if( reDoubleSlash.IsMatch( Url ) )
      {

        try
        {
          NewUri = new Uri(
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
          DebugMsgStatic( ex.Message );
        }
        catch( UriFormatException ex )
        {
          DebugMsgStatic( ex.Message );
        }

      }
      else
      if( reSlash.IsMatch( Url ) )
      {

        try
        {
          NewUri = new Uri(
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
          DebugMsgStatic( ex.Message );
        }
        catch( UriFormatException ex )
        {
          DebugMsgStatic( ex.Message );
        }

      }
      else
      if( reQuery.IsMatch( Url ) )
      {

        try
        {
          NewUri = new Uri(
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
          DebugMsgStatic( ex.Message );
        }
        catch( UriFormatException ex )
        {
          DebugMsgStatic( ex.Message );
        }

      }
      else
      if( reHash.IsMatch( Url ) )
      {

        string NewUrl = Url;
        Regex reHashRemove = new Regex( "#.*$", RegexOptions.Singleline );
        NewUrl = reHashRemove.Replace( NewUrl, "" );

        try
        {
          NewUri = new Uri(
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
          DebugMsgStatic( ex.Message );
        }
        catch( UriFormatException ex )
        {
          DebugMsgStatic( ex.Message );
        }

      }
      else
      if( reHTTP.IsMatch( Url ) )
      {

        try
        {
          NewUri = new Uri( Url, UriKind.Absolute );
        }
        catch( InvalidOperationException ex )
        {
          DebugMsgStatic( ex.Message );
        }
        catch( UriFormatException ex )
        {
          DebugMsgStatic( ex.Message );
        }

      }
      else
      if( reUnsupportedScheme.IsMatch( Url ) )
      {

        ; // NO-OP, for now.

      }
      else
      {

        DebugMsgStatic( string.Format( "RELATIVE URL 1: {0}", Url ) );

        string BasePath = Regex.Replace( BaseUri.AbsolutePath, "/[^/]+$", "/" );
        string NewPath = string.Join( "", BasePath, Url );

        DebugMsgStatic( string.Format( "RELATIVE URL 2: {0}", BasePath ) );
        DebugMsgStatic( string.Format( "RELATIVE URL 3: {0}", NewPath ) );

        try
        {
          NewUri = new Uri(
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
          DebugMsgStatic( ex.Message );
        }
        catch( UriFormatException ex )
        {
          DebugMsgStatic( ex.Message );
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

      return ( UrlFixed );

    }

    /**************************************************************************/

    public static bool VerifySameHost ( string BaseUrl, string Url )
    {
      bool Success = false;
      Uri BaseUri = null;
      Uri NewUri = null;

      try
      {
        BaseUri = new Uri( BaseUrl, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsgStatic( ex.Message );
        DebugMsgStatic( string.Format( "FAILED TO VERIFY: {0}", BaseUrl ) );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( ex.Message );
        DebugMsgStatic( string.Format( "FAILED TO VERIFY: {0}", BaseUrl ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( ex.Message );
        DebugMsgStatic( string.Format( "FAILED TO VERIFY: {0}", BaseUrl ) );
      }

      try
      {
        NewUri = new Uri( Url, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsgStatic( ex.Message );
        DebugMsgStatic( string.Format( "FAILED TO VERIFY: {0}", Url ) );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( ex.Message );
        DebugMsgStatic( string.Format( "FAILED TO VERIFY: {0}", Url ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( ex.Message );
        DebugMsgStatic( string.Format( "FAILED TO VERIFY: {0}", Url ) );
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
        DebugMsgStatic( ex.Message );
        DebugMsgStatic( string.Format( "FAILED TO VERIFY: {0}", Url ) );
      }

      return ( Success );
    }

    /**************************************************************************/

    public static bool CompareUrls (
      string UrlLeft,
      string UrlRight,
      bool CheckScheme = true,
      bool CheckHost = true,
      bool CheckPort = false,
      bool CheckPath = true,
      bool CheckQuery = false
      )
    {

      Regex reGoodUrl = new Regex( "^(https?|mailto):", RegexOptions.IgnoreCase );
      bool AreMatch = false;
      bool Proceed = true;
      Uri UriLeft = null;
      Uri UriRight = null;

      if( UrlLeft == UrlRight )
      {
        return ( true );
      }

      try
      {
        if( reGoodUrl.IsMatch( UrlLeft ) )
        {
          UriLeft = new Uri( UrlLeft, UriKind.Absolute );
        }
        if( reGoodUrl.IsMatch( UrlRight ) )
        {
          UriRight = new Uri( UrlRight, UriKind.Absolute );
        }
      }
      catch( InvalidOperationException ex )
      {
        DebugMsgStatic( ex.Message );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( ex.Message );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( ex.Message );
      }

      if( ( UriLeft != null ) && ( UriRight != null ) )
      {

        if( CheckScheme )
        {
          if( UriLeft.Scheme != UriRight.Scheme )
          {
            Proceed = false;
          }
        }

        if( CheckHost )
        {
          if( UriLeft.Host != UriRight.Host )
          {
            Proceed = false;
          }
        }

        if( CheckPort )
        {
          if( UriLeft.Port != UriRight.Port )
          {
            Proceed = false;
          }
        }

        if( CheckPath )
        {
          if( UriLeft.AbsolutePath != UriRight.AbsolutePath )
          {
            Proceed = false;
          }
        }

        if( CheckQuery )
        {
          if( UriLeft.Query != UriRight.Query )
          {
            Proceed = false;
          }
        }

      }

      if( Proceed )
      {
        AreMatch = true;
      }

      return ( AreMatch );

    }

    /**************************************************************************/

    public static int FindUrlDepth ( string Url )
    {

      int Depth = -1;
      Uri DocumentURI = null;

      try
      {
        DocumentURI = new Uri( Url, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsgStatic( ex.Message );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( ex.Message );
      }

      if( DocumentURI != null )
      {

        string Path = DocumentURI.AbsolutePath;
        string[] PathElements = Path.Split( '/' );

        for( int i = 0 ; i < PathElements.Length ; i++ )
        {
          if( !string.IsNullOrEmpty( PathElements[ i ] ) )
          {
            Depth++;
          }
        }

        if( Depth < 0 )
        {
          Depth = 0;
        }

      }

      return ( Depth );

    }

    /**************************************************************************/

    public static string DetermineStartingDirectory ( string Url )
    {

      Uri StartUri = null;
      string Path = "/";
      string StartUriPort = "";
      string StartingUrl = null;

      try
      {

        StartUri = new Uri( Url );

        if( StartUri.Port > 0 )
        {
          StartUriPort = string.Format( ":{0}", StartUri.Port );
        }

        Path = StartUri.AbsolutePath;

      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "DetermineStartingDirectory: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "DetermineStartingDirectory: {0}", ex.Message ) );
      }


      if( StartUri != null )
      {

        Path = Regex.Replace( Path, "/[^/]*$", "/", RegexOptions.IgnoreCase );

        if( Path.Length == 0 )
        {
          Path = "/";
        }

        StartingUrl = string.Join(
            "",
            StartUri.Scheme,
            "://",
            StartUri.Host,
            StartUriPort,
            Path
        );

      }

      return ( StartingUrl );

    }

    /**************************************************************************/

    public static bool IsWithinParentDirectory ( string StartUrl, string Url )
    {

      bool IsWithin = false;
      Uri CurrentUri = null;
      string CurrentUriPort = "";

      try
      {

        CurrentUri = new Uri( Url );

        if( CurrentUri.Port > 0 )
        {
          CurrentUriPort = string.Format( ":{0}", CurrentUri.Port );
        }

      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "IsWithinParentDirectory: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "IsWithinParentDirectory: {0}", ex.Message ) );
      }

      if( CurrentUri != null )
      {

        if(
          ( CurrentUri.Scheme.ToLower() == "http" )
          || ( CurrentUri.Scheme.ToLower() == "https" ) )
        {

          string StartingUrl = MacroscopeHttpUrlUtils.DetermineStartingDirectory( Url: StartUrl );
          string Path = CurrentUri.AbsolutePath;
          string CurrentUriString;
          int ParentStartingDirectoryLength;
          int CurrentUriStringLength;

          Path = Regex.Replace( Path, "/[^/]*$", "/", RegexOptions.IgnoreCase );

          if( Path.Length == 0 )
          {
            Path = "/";
          }

          CurrentUriString = string.Join(
            "",
            CurrentUri.Scheme,
            "://",
            CurrentUri.Host,
            CurrentUriPort,
            Path
          );

          ParentStartingDirectoryLength = StartingUrl.Length;
          CurrentUriStringLength = CurrentUriString.Length;

          if( ParentStartingDirectoryLength >= CurrentUriStringLength )
          {
            if( StartingUrl.StartsWith( CurrentUriString, StringComparison.Ordinal ) )
            {
              IsWithin = true;
            }

          }

        }

      }

      return ( IsWithin );

    }

    /** -------------------------------------------------------------------- **/

    public static bool IsWithinChildDirectory ( string StartUrl, string Url )
    {

      bool IsWithin = false;
      Uri CurrentUri = null;
      string CurrentUriPort = "";

      try
      {

        CurrentUri = new Uri( Url );

        if( CurrentUri.Port > 0 )
        {
          CurrentUriPort = string.Format( ":{0}", CurrentUri.Port );
        }

      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "UriFormatException: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "Exception: {0}", ex.Message ) );
      }

      if( CurrentUri != null )
      {

        if(
          ( CurrentUri.Scheme.ToLower() == "http" )
          || ( CurrentUri.Scheme.ToLower() == "https" ) )
        {

          string StartingUrl = MacroscopeHttpUrlUtils.DetermineStartingDirectory( Url: StartUrl );
          string Path = CurrentUri.AbsolutePath;
          string CurrentUriString;
          int ChildStartingDirectoryLength;
          int CurrentUriStringLength;

          Path = Regex.Replace( Path, "/[^/]*$", "/", RegexOptions.IgnoreCase );

          if( Path.Length == 0 )
          {
            Path = "/";
          }

          CurrentUriString = string.Join(
            "",
            CurrentUri.Scheme,
            "://",
            CurrentUri.Host,
            CurrentUriPort,
            Path
          );

          ChildStartingDirectoryLength = StartingUrl.Length;
          CurrentUriStringLength = CurrentUriString.Length;

          if( CurrentUriStringLength >= ChildStartingDirectoryLength )
          {

            if( CurrentUriString.StartsWith( StartingUrl, StringComparison.Ordinal ) )
            {
              IsWithin = true;
            }

          }

        }

      }

      return ( IsWithin );

    }

    /**************************************************************************/

    public static string CleanUrlCss ( string CssProperty )
    {

      string CleanedUrl = null;


      DebugMsgStatic( string.Format( "CleanUrlCss: sProperty: {0}", CssProperty ) );

      if( Regex.IsMatch( CssProperty, @"url\([^()]+\)" ) )
      {
        MatchCollection reMatches;

        DebugMsgStatic( string.Format( "CleanUrlCss: HAS URL: {0}", CssProperty ) );

        reMatches = Regex.Matches( CssProperty, "url\\(\\s*[\"']?(.+?)[\"']?\\s*\\)", RegexOptions.IgnoreCase );

        DebugMsgStatic( string.Format( "CleanUrlCss: reMatches: {0}", reMatches.Count ) );

        foreach( Match match in reMatches )
        {

          if( match.Groups[ 1 ].Value.Length > 0 )
          {
            CleanedUrl = match.Groups[ 1 ].Value;
            break;
          }

        }

        if( CleanedUrl.ToLower() == "none" )
        {
          CleanedUrl = null;
        }

      }
      else
      {

        CleanedUrl = null;

      }

      DebugMsgStatic( string.Format( "CleanUrlCss: CleanedUrl: {0}", CleanedUrl ) );

      return ( CleanedUrl );

    }

    /**************************************************************************/

    public static string GetHostnameFromUrl ( string Url )
    {

      Uri DocumentUri = null;
      string Hostname = null;

      try
      {
        DocumentUri = new Uri( Url, UriKind.Absolute );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "GetHostnameFromUrl: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "GetHostnameFromUrl: {0}", ex.Message ) );
      }

      if( DocumentUri != null )
      {
        Hostname = DocumentUri.Host;
      }

      return ( Hostname );

    }

    /** -------------------------------------------------------------------- **/

    public static string GetHostnameAndPortFromUrl ( string Url )
    {

      Uri DocumentUri = null;
      string HostnameAndPort = null;

      try
      {
        DocumentUri = new Uri( Url, UriKind.Absolute );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "GetHostnameAndPortFromUrl: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "GetHostnameAndPortFromUrl: {0}", ex.Message ) );
      }

      if( DocumentUri != null )
      {
        if( DocumentUri.Port > 0 )
        {
          HostnameAndPort = string.Join( ":", DocumentUri.Host, DocumentUri.Port );
        }
        else
        {
          HostnameAndPort = DocumentUri.Host;
        }
      }

      return ( HostnameAndPort );

    }

    /** Strip Query String ****************************************************/

    public static string StripQueryString ( string Url )
    {

      Uri UriBase = null;
      Uri UriNew = null;
      string NewUrl = null;

      try
      {
        UriBase = new Uri( Url, UriKind.Absolute );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "StripQueryString: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "StripQueryString: {0}", ex.Message ) );
      }

      if( UriBase != null )
      {

        string UriBasePort = "";

        if( UriBase.Port > 0 )
        {
          UriBasePort = string.Format( ":{0}", UriBase.Port );
        }

        try
        {
          UriNew = new Uri(
            string.Format(
              "{0}://{1}{2}{3}{4}",
              UriBase.Scheme,
              UriBase.Host,
              UriBasePort,
              UriBase.AbsolutePath,
              UriBase.Fragment
            ),
            UriKind.Absolute
          );

        }
        catch( InvalidOperationException ex )
        {
          DebugMsgStatic( ex.Message );
        }
        catch( UriFormatException ex )
        {
          DebugMsgStatic( ex.Message );
        }
        catch( Exception ex )
        {
          DebugMsgStatic( ex.Message );
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

      NewUrl = SanitizeUrl( Url: NewUrl );

      return ( NewUrl );

    }

    /** Strip Hash Fragment ***************************************************/

    public static string StripHashFragment ( string Url )
    {

      Uri UriBase = null;
      Uri UriNew = null;
      string NewUrl = null;

      try
      {
        UriBase = new Uri( Url, UriKind.Absolute );
      }
      catch( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "StripHashFragment: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "StripHashFragment: {0}", ex.Message ) );
      }

      if( UriBase != null )
      {

        string UriBasePort = "";

        if( UriBase.Port > 0 )
        {
          UriBasePort = string.Format( ":{0}", UriBase.Port );
        }

        try
        {
          UriNew = new Uri(
            string.Format(
              "{0}://{1}{2}{3}{4}",
              UriBase.Scheme,
              UriBase.Host,
              UriBasePort,
              UriBase.AbsolutePath,
              UriBase.Query
            ),
            UriKind.Absolute
          );
        }
        catch( InvalidOperationException ex )
        {
          DebugMsgStatic( ex.Message );
        }
        catch( UriFormatException ex )
        {
          DebugMsgStatic( ex.Message );
        }
        catch( Exception ex )
        {
          DebugMsgStatic( ex.Message );
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

      NewUrl = SanitizeUrl( Url: NewUrl );

      return ( NewUrl );

    }

    /** URL Parent Folders ****************************************************/

    public static List<string> GetParentFolderUrls ( string Url )
    {

      Uri DocumentURI = null;
      List<string> UrlList = new List<string>();

      try
      {
        DocumentURI = new Uri( Url, UriKind.Absolute );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( ex.Message );
      }

      if( DocumentURI != null )
      {

        string UrlStripped = Regex.Replace( Url.ToString(), "[^/]+$", "" );

        while( Regex.IsMatch( UrlStripped, "^https?://[^/]+/([^/]+/)+$" ) )
        {

          UrlList.Add( UrlStripped );

          UrlStripped = Regex.Replace( UrlStripped, "/[^/]+/$", "/" );

        }

      }

      return ( UrlList );

    }

    /**************************************************************************/

    public static async Task<string> GetMimeTypeOfUrl ( MacroscopeJobMaster JobMaster, Uri TargetUri )
    {

      MacroscopeHttpTwoClient Client = JobMaster.GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      string MimeType = null;

      try
      {

        Response = await Client.Head( TargetUri, ConfigureHeadRequestHeadersCallback, PostProcessRequestHttpHeadersCallback );

        if( Response != null )
        {
          MimeType = Response.GetMimeType().ToString();
        }

      }
      catch( MacroscopeDocumentException ex )
      {
        DebugMsgStatic( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
        DebugMsgStatic( string.Format( "MacroscopeDocumentException: {0}", TargetUri.ToString() ) );
      }
      catch( Exception ex )
      {
        DebugMsgStatic( string.Format( "Exception: {0}", ex.Message ) );
        DebugMsgStatic( string.Format( "Exception: {0}", TargetUri.ToString() ) );
      }

      return ( MimeType );

    }

    /** -------------------------------------------------------------------- **/

    private static void ConfigureHeadRequestHeadersCallback ( HttpRequestMessage Request )
    {
    }

    /** -------------------------------------------------------------------- **/

    private static void PostProcessRequestHttpHeadersCallback ( HttpRequestMessage Request, HttpRequestHeaders DefaultRequestHeaders )
    {
    }

    /**************************************************************************/

    public static string DowncaseUrl ( string Url )
    {

      Uri OriginalUri = new Uri( Url, UriKind.Absolute );
      string UriBasePort = "";
      Uri DowncasedUri = null;
      string DowncasedUriString = null;

      if( OriginalUri.Port > 0 )
      {
        UriBasePort = string.Format( ":{0}", OriginalUri.Port );
      }

      DowncasedUri = new Uri(
        string.Format(
          "{0}://{1}{2}{3}{4}",
          OriginalUri.Scheme,
          OriginalUri.Host,
          UriBasePort,
          OriginalUri.AbsolutePath.ToLower(),
          OriginalUri.Query
        ),
        UriKind.Absolute
      );

      DowncasedUriString = DowncasedUri.ToString();

      return ( DowncasedUriString );

    }

    /**************************************************************************/

  }

}
