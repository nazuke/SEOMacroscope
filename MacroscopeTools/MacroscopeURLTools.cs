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
using System.Web;

namespace SEOMacroscope
{

  public class MacroscopeUrlTools : Macroscope
  {

    /**************************************************************************/

    static MacroscopeUrlTools ()
    {
      SuppressStaticDebugMsg = true;
    }

    public MacroscopeUrlTools ()
    {
      SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public static Boolean ValidateUrl ( string sUrl )
    {

      Boolean bIsValid = false;

      try
      {

        Uri uUri = new Uri ( sUrl, UriKind.Absolute );

        if( uUri != null )
        {
          bIsValid = true;
        }

      }
      catch( UriFormatException ex )
      {

        DebugMsg( string.Format( "ValidateUrl: {0}", ex.Message ), true );

      }

      return( bIsValid );

    }

    /**************************************************************************/

    public static string SanitizeUrl ( string sUrl )
    {

      string sSanitized;
      DebugMsg( string.Format( "SanitizeUrl 1: {0}", sUrl ), true );

      sSanitized = Uri.EscapeUriString( sUrl );

      DebugMsg( string.Format( "SanitizeUrl 2: {0}", sSanitized ), true );

      return( sSanitized );
    }

    /**************************************************************************/

    public static string MakeUrlAbsolute ( string sBaseUrl, string sUrl )
    {

      string sUrlFixed;
      Uri uBase = new Uri ( sBaseUrl, UriKind.Absolute );
      Uri uNew = null;

      Regex reHTTP = new Regex ( "^https?:" );
      Regex reDoubleSlash = new Regex ( "^//" );
      Regex reSlash = new Regex ( "^/" );
      Regex reQuery = new Regex ( "^\\?" );
      Regex reHash = new Regex ( "^#" );
      Regex reUnsupportedScheme = new Regex ( "^[^:]+:" );

      sUrl = HtmlEntity.DeEntitize( sUrl );

      if( !Regex.IsMatch( sUrl, "^(https?:|/|#)" ) )
      {
        DebugMsg( string.Format( "STRANGE URL: 1: {0}", sBaseUrl ), true );
        DebugMsg( string.Format( "STRANGE URL: 2: {0}", sUrl ), true );

      }

      if( !reHTTP.IsMatch( sUrl ) )
      {
        Boolean bSuspect = false;
        if(
          ( !reDoubleSlash.IsMatch( sUrl ) )
          && ( !reSlash.IsMatch( sUrl ) )
          && ( !reQuery.IsMatch( sUrl ) )
          && ( !reHash.IsMatch( sUrl ) ) )
        {
          if( reUnsupportedScheme.IsMatch( sUrl ) )
          {
            bSuspect = true;
          }
        }
        if( bSuspect )
        {
          DebugMsg( string.Format( "STRANGE URL: IS SUSPECT: {0}", sUrl ), true );
          return( null );
        }
      }

      if( reDoubleSlash.IsMatch( sUrl ) )
      {

        try
        {
          uNew = new Uri (
            string.Format(
              "{0}:{1}",
              uBase.Scheme,
              sUrl
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
      if( reSlash.IsMatch( sUrl ) )
      {

        try
        {
          uNew = new Uri (
            string.Format(
              "{0}://{1}{2}",
              uBase.Scheme,
              uBase.Host,
              sUrl
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
      if( reQuery.IsMatch( sUrl ) )
      {

        try
        {
          uNew = new Uri (
            string.Format(
              "{0}://{1}{2}",
              uBase.Scheme,
              uBase.Host,
              sUrl
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
      if( reHash.IsMatch( sUrl ) )
      {

        string sNewUrl = sUrl;
        Regex reHashRemove = new Regex ( "#.*$", RegexOptions.Singleline );
        sNewUrl = reHashRemove.Replace( sNewUrl, "" );

        try
        {
          uNew = new Uri (
            string.Format(
              "{0}://{1}{2}",
              uBase.Scheme,
              uBase.Host,
              sNewUrl
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
      if( reHTTP.IsMatch( sUrl ) )
      {

        try
        {
          uNew = new Uri ( sUrl, UriKind.Absolute );
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
      if( reUnsupportedScheme.IsMatch( sUrl ) )
      {

        ; // NO-OP, for now.

      }
      else
      {


        DebugMsg( string.Format( "RELATIVE URL 1: {0}", sUrl ), true );

        string sBasePath = Regex.Replace( uBase.AbsolutePath, "/[^/]+$", "/" );
        string sNewPath = string.Join( "", sBasePath, sUrl );

        DebugMsg( string.Format( "RELATIVE URL 2: {0}", sBasePath ), true );
        DebugMsg( string.Format( "RELATIVE URL 3: {0}", sNewPath ), true );


        try
        {
          uNew = new Uri (
            string.Format(
              "{0}://{1}{2}",
              uBase.Scheme,
              uBase.Host,
              sNewPath
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

      if( uNew != null )
      {
        sUrlFixed = uNew.ToString();
      }
      else
      {
        sUrlFixed = sUrl;
      }

      sUrlFixed = SanitizeUrl( sUrlFixed );

      return( sUrlFixed );

    }

    /**************************************************************************/

    public static Boolean VerifySameHost ( string sBaseUrL, string sUrl )
    {
      Boolean bSuccess = false;
      Uri uBase = null;
      Uri uNew = null;

      try
      {
        uBase = new Uri ( sBaseUrL, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", sBaseUrL ), true );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", sBaseUrL ), true );
      }
      catch( Exception ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", sBaseUrL ), true );
      }

      try
      {
        uNew = new Uri ( sUrl, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", sUrl ), true );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", sUrl ), true );
      }
      catch( Exception ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", sUrl ), true );
      }

      try
      {
        if( ( uBase != null ) && ( uNew != null ) && ( uBase.Host.ToString() == uNew.Host.ToString() ) )
        {
          bSuccess = true;
        }
      }
      catch( UriFormatException ex )
      {
        DebugMsg( ex.Message, true );
        DebugMsg( string.Format( "FAILED TO VERIFY: {0}", sUrl ), true );
      }

      return( bSuccess );
    }

    /**************************************************************************/

    public static int FindUrlDepth ( string sUrl )
    {

      int iDepth = 0;
      Uri uURI = null;

      try
      {
        uURI = new Uri ( sUrl, UriKind.Absolute );
      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( ex.Message, true );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( ex.Message, true );
      }

      if( uURI != null )
      {
        string sPath = uURI.AbsolutePath;
        iDepth = sPath.Split( '/' ).Length - 1;
      }

      return( iDepth );

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
  }

}
