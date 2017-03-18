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
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using ExCSS;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private void ProcessCssPage ()
    {

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string sErrorCondition = null;
      Boolean bAuthenticating = false;
      
      DebugMsg( string.Format( "ProcessCssPage: {0}", "" ) );

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

        DebugMsg( string.Format( "ProcessCssPage :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ProcessCssPage :: WebException: {0}", ex.Status ) );
        DebugMsg( string.Format( "ProcessCssPage :: WebException: {0}", ( int )ex.Status ) );

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
          StreamReader srRead;

          if( this.CharSet != null )
          {
            srRead = new StreamReader ( sStream, this.CharSet );
          }
          else
          {
            srRead = new StreamReader ( sStream );
          }
         
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
          this.ContentLength = 0;

        }

        if( sRawData.Length > 0 )
        {

          ExCSS.Parser ExCssParser = new  ExCSS.Parser ();
          ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( sRawData );

          this.ProcessCssHyperlinksOut( ExCssStylesheet );

        }
        else
        {
          
          DebugMsg( string.Format( "ProcessCssPage: ERROR: {0}", this.GetUrl() ) );
          
        }
        
        { // Title
          MatchCollection reMatches = Regex.Matches( this.DocUrl, "/([^/]+)$" );
          string sTitle = null;
          foreach( Match match in reMatches )
          {
            if( match.Groups[ 1 ].Value.Length > 0 )
            {
              sTitle = match.Groups[ 1 ].Value.ToString();
              break;
            }
          }
          if( sTitle != null )
          {
            this.SetTitle( sTitle, MacroscopeConstants.TextProcessingMode.NO_PROCESSING );
            DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
          }
          else
          {
            DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
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

    private void ProcessCssHyperlinksOut ( ExCSS.StyleSheet ExCssStylesheet )
    {

      foreach( var rRule in ExCssStylesheet.StyleRules )
      {

        int iRule = ExCssStylesheet.StyleRules.IndexOf( rRule );

        foreach( Property pProp in ExCssStylesheet.StyleRules[ iRule ].Declarations.Properties )
        {
          
          string sBackgroundImageUrl;
          string sLinkUrlAbs;

          switch( pProp.Name.ToLower() )
          {

            case "background-image":
              sBackgroundImageUrl = pProp.Term.ToString();
              sLinkUrlAbs = this.ProcessCssBackImageUrl( sBackgroundImageUrl );
              DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background-image): {0}", sBackgroundImageUrl ) );
              DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background-image): {0}", sLinkUrlAbs ) );
              if( sLinkUrlAbs != null )
              {
                // TODO: Verify that this actually works:
                this.HyperlinksOut.Add(
                  LinkType: MacroscopeConstants.HyperlinkType.CSS,
                  UrlTarget: sLinkUrlAbs
                );
                this.AddCssOutlink(
                  AbsoluteUrl: sLinkUrlAbs,
                  LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                  Follow: true
                );
              }
              break;

            case "background":
              sBackgroundImageUrl = pProp.Term.ToString();
              sLinkUrlAbs = this.ProcessCssBackImageUrl( sBackgroundImageUrl );
              DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background): {0}", sBackgroundImageUrl ) );
              DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background): {0}", sLinkUrlAbs ) );
              if( sLinkUrlAbs != null )
              {     
                // TODO: Verify that this actually works:
                this.HyperlinksOut.Add( 
                  LinkType: MacroscopeConstants.HyperlinkType.CSS,
                  UrlTarget: sLinkUrlAbs
                );
                this.AddCssOutlink(
                  AbsoluteUrl: sLinkUrlAbs,
                  LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                  Follow: true
                );
              }
              break;

            default:
              break;

          }

        }

      }

    }

    /**************************************************************************/

    private string ProcessCssBackImageUrl ( string sBackgroundImageUrl )
    {

      string sLinkUrlAbs = null;
      string sLinkUrlCleaned = MacroscopeUrlUtils.CleanUrlCss( sBackgroundImageUrl );

      if( sLinkUrlCleaned != null )
      {

        sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrlCleaned );

        DebugMsg( string.Format( "ProcessCssBackImageUrl: {0}", sLinkUrlCleaned ) );
        DebugMsg( string.Format( "ProcessCssBackImageUrl: this.Url: {0}", this.DocUrl ) );
        DebugMsg( string.Format( "ProcessCssBackImageUrl: sLinkUrlAbs: {0}", sLinkUrlAbs ) );

      }

      return( sLinkUrlAbs );
      
    }

    /**************************************************************************/

    private MacroscopeLink AddCssOutlink (
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

      if( this.Outlinks.ContainsKey( AbsoluteUrl ) )
      {
        this.Outlinks.Remove( AbsoluteUrl );
        this.Outlinks.Add( AbsoluteUrl, OutLink );
      }
      else
      {
        this.Outlinks.Add( AbsoluteUrl, OutLink );
      }

      return( OutLink );
            
    }

    /**************************************************************************/
    
  }

}
