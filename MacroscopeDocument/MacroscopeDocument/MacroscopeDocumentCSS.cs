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
      string ResponseErrorCondition = null;
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

          Stream ResponseStream = res.GetResponseStream();
          StreamReader ResponseStreamReader;

          if( this.CharSet != null )
          {
            ResponseStreamReader = new StreamReader ( ResponseStream, this.CharSet );
          }
          else
          {
            ResponseStreamReader = new StreamReader ( ResponseStream );
          }
         
          RawData = ResponseStreamReader.ReadToEnd();

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
          this.ContentLength = 0;

        }

        if( RawData.Length > 0 )
        {

          ExCSS.Parser ExCssParser = new ExCSS.Parser ();
          ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( RawData );

          this.ProcessCssHyperlinksOut( ExCssStylesheet );

        }
        else
        {
          
          DebugMsg( string.Format( "ProcessCssPage: ERROR: {0}", this.GetUrl() ) );
          
        }
        
        { // Title
          MatchCollection reMatches = Regex.Matches( this.DocUrl, "/([^/]+)$" );
          string DocumentTitle = null;
          foreach( Match match in reMatches )
          {
            if( match.Groups[ 1 ].Value.Length > 0 )
            {
              DocumentTitle = match.Groups[ 1 ].Value.ToString();
              break;
            }
          }
          if( DocumentTitle != null )
          {
            this.SetTitle( DocumentTitle, MacroscopeConstants.TextProcessingMode.NO_PROCESSING );
            DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
          }
          else
          {
            DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
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

    private void ProcessCssHyperlinksOut ( ExCSS.StyleSheet ExCssStylesheet )
    {

      if( this.GetIsExternal() )
      {
        return;
      }
            
      foreach( var CssRule in ExCssStylesheet.StyleRules )
      {

        int iRule = ExCssStylesheet.StyleRules.IndexOf( CssRule );

        foreach( Property pProp in ExCssStylesheet.StyleRules[ iRule ].Declarations.Properties )
        {
          
          string BackgroundImageUrl;
          string LinkUrlAbs;

          switch( pProp.Name.ToLower() )
          {

            case "background-image":

              if( pProp.Term != null )
              {
                
                BackgroundImageUrl = pProp.Term.ToString();
                LinkUrlAbs = this.ProcessCssBackImageUrl( BackgroundImageUrl );

                DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background-image): {0}", BackgroundImageUrl ) );
                DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background-image): {0}", LinkUrlAbs ) );

                if( LinkUrlAbs != null )
                {

                  // TODO: Verify that this actually works:

                  this.HyperlinksOut.Add(
                    LinkType: MacroscopeConstants.HyperlinkType.CSS,
                    UrlTarget: LinkUrlAbs
                  );

                  MacroscopeLink Outlink = this.AddCssOutlink(
                                             AbsoluteUrl: LinkUrlAbs,
                                             LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                                             Follow: true
                                           );
                
                  Outlink.SetRawTargetUrl( BackgroundImageUrl );

                }
              
              }

              break;

            case "background":

              if( pProp.Term != null )
              {
                
                BackgroundImageUrl = pProp.Term.ToString();
                LinkUrlAbs = this.ProcessCssBackImageUrl( BackgroundImageUrl );

                DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background): {0}", BackgroundImageUrl ) );
                DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background): {0}", LinkUrlAbs ) );

                if( LinkUrlAbs != null )
                {     

                  // TODO: Verify that this actually works:

                  this.HyperlinksOut.Add(
                    LinkType: MacroscopeConstants.HyperlinkType.CSS,
                    UrlTarget: LinkUrlAbs
                  );

                  MacroscopeLink Outlink = this.AddCssOutlink(
                                             AbsoluteUrl: LinkUrlAbs,
                                             LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                                             Follow: true
                                           );
                
                  Outlink.SetRawTargetUrl( BackgroundImageUrl );
                
                }
              
              }
              
              break;

            default:
              break;

          }

        }

      }

    }

    /**************************************************************************/

    private string ProcessCssBackImageUrl ( string BackgroundImageUrl )
    {

      string LinkUrlAbs = null;
      string LinkUrlCleaned = MacroscopeUrlUtils.CleanUrlCss( BackgroundImageUrl );

      if( LinkUrlCleaned != null )
      {

        LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrlCleaned );

        DebugMsg( string.Format( "ProcessCssBackImageUrl: {0}", LinkUrlCleaned ) );
        DebugMsg( string.Format( "ProcessCssBackImageUrl: this.DocUrl: {0}", this.DocUrl ) );
        DebugMsg( string.Format( "ProcessCssBackImageUrl: LinkUrlAbs: {0}", LinkUrlAbs ) );

      }

      return( LinkUrlAbs );
      
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

      this.Outlinks.Add( OutLink );

      return( OutLink );
            
    }

    /**************************************************************************/
    
  }

}
