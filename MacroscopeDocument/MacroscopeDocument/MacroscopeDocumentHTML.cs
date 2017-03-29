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
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Net;
using HtmlAgilityPack;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private void ProcessHtmlPage ()
    {

      HtmlDocument HtmlDoc = null;
      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string sErrorCondition = null;
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

        DebugMsg( string.Format( "ExecuteHeadRequest :: WebException: {0}", ex.Message ) );
        res = ( HttpWebResponse )ex.Response;
        sErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        string sRawData = "";

        this.ProcessResponseHttpHeaders( req, res );

        // Get Response Body
        try
        {

          DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );

          Encoding encUseEncoding = Encoding.UTF8;
          if( this.CharSet != null )
          {
            encUseEncoding = this.CharSet;
          }
          else
          {
            encUseEncoding = this.HtmlSniffCharset();
          }

          Stream sStream = res.GetResponseStream();
          StreamReader srRead = new StreamReader ( sStream, encUseEncoding );
          sRawData = srRead.ReadToEnd();
          this.ContentLength = sRawData.Length; // May need to find bytes length
          this.SetChecksum( sRawData );
          
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
          sRawData = "";
          this.ContentLength = 0;

        }

        if( sRawData.Length > 0 )
        {
          HtmlDoc = new HtmlDocument ();
          HtmlDoc.LoadHtml( sRawData );
          DebugMsg( string.Format( "htmlDoc: {0}", HtmlDoc ) );
        }
        else
        {
          DebugMsg( string.Format( "sRawData: {0}", "EMPTY" ) );
        }

        if( HtmlDoc != null )
        {

          { // Probe Locale
            
            MacroscopeLocaleTools msLocale = new MacroscopeLocaleTools ();
            this.Locale = msLocale.ProbeLocale( HtmlDoc );
            if( this.Locale != null )
            {
              this.SetHreflang( this.Locale, this.DocUrl );
            }
          }

          { // Probe Character Set
            try
            {
              this.SetCharacterSet( HtmlDoc.DeclaredEncoding.EncodingName );
            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( ex.Message ) );
              this.SetCharacterSet( "" );
            }
          }

          { // Title
            HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/title" );
            if( nNode != null )
            {
              this.SetTitle( nNode.InnerText, MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES );
              DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
            }
            else
            {
              DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
            }
          }

          { // Description
            HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='description']" );
            if( nNode != null )
            {
              this.Description = nNode.GetAttributeValue( "content", null );
              DebugMsg( string.Format( "DESCRIPTION: {0}", this.Description ) );
            }
            else
            {
              this.Description = null;
              DebugMsg( string.Format( "DESCRIPTION: {0}", "MISSING" ) );
            }
          }

          { // Keywords
            HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='keywords']" );
            if( nNode != null )
            {
              this.Keywords = nNode.GetAttributeValue( "content", null );
              DebugMsg( string.Format( "KEYWORDS: {0}", this.Keywords ) );
            }
            else
            {
              this.Keywords = null;
              DebugMsg( string.Format( "KEYWORDS: {0}", "MISSING" ) );
            }
          }

          { // Canonical
            this.ProcessHtmlCanonical( HtmlDoc );
          }

          { // Outlinks
            this.ProcessHtmlHyperlinksOut( HtmlDoc );
            this.ProcessHtmlOutlinks( HtmlDoc );
          }

          { // Extract interesting document elements
            this.ExtractHtmlHeadings( HtmlDoc );
          }

          { // Special Links
            this.ExtractHtmlEmailAddresses( HtmlDoc );
            this.ExtractHtmlTelephoneNumbers( HtmlDoc );
          }

          { // HREFLANG Alternatives
            this.ExtractHrefLangAlternates( HtmlDoc );
          }

          { // Process Body Text
            string sText = this.ProcessHtmlBodyText( sRawData );
            if( sText != null )
            {
              this.SetBodyText( sText );
            }
            else
            {
              this.SetBodyText( "" );
            }
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

    private void ProcessHtmlCanonical ( HtmlDocument HtmlDoc )
    {

      HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/link[@rel='canonical']" );

      if( nNode != null )
      {

        this.Canonical = nNode.GetAttributeValue( "href", "" );
        
        DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
        
        if( MacroscopePreferencesManager.GetFollowCanonicalLinks() )
        {
          
          string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, this.Canonical );
          
          MacroscopeLink Outlink = this.AddHtmlOutlink(
                                     AbsoluteUrl: sLinkUrlAbs,
                                     LinkType: MacroscopeConstants.InOutLinkType.CANONICAL,
                                     Follow: true
                                   );
          
          Outlink.SetRawTargetUrl( this.Canonical );
          
        }

      }
      else
      {

        this.Canonical = "";
        DebugMsg( string.Format( "CANONICAL: {0}", "MISSING" ) );

      }

    }

    /**************************************************************************/

    private void ProcessHtmlHyperlinksOut ( HtmlDocument HtmlDoc )
    {

      HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

      if( nOutlinks != null )
      {

        foreach( HtmlNode nLink in nOutlinks )
        {

          MacroscopeHyperlinkOut HyperlinkOut = null;
          string sLinkUrl = nLink.GetAttributeValue( "href", null );
          string sLinkUrlAbsolute = null;
          string sLinkTitle = nLink.GetAttributeValue( "title", "" );
          
          if( sLinkUrl != null )
          {

            sLinkUrlAbsolute = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

            if( sLinkUrlAbsolute != null )
            {

              this.SetProcessHyperlinksInForUrl( Url: sLinkUrlAbsolute );

              HyperlinkOut = this.HyperlinksOut.Add(
                LinkType: MacroscopeConstants.HyperlinkType.TEXT,
                UrlTarget: sLinkUrlAbsolute
              );

              if( sLinkTitle.Length > 0 )
              {
                sLinkTitle = HtmlEntity.DeEntitize( sLinkTitle );
                if( sLinkTitle.Length > 0 )
                  HyperlinkOut.SetLinkTitle( sLinkTitle );
              }

              { // FOLLOW / NOFOLLOW

                string sRel = nLink.GetAttributeValue( "rel", null );

                if( sRel != null )
                {
                  if( sRel.ToLower().Contains( "nofollow" ) )
                    HyperlinkOut.UnsetDoFollow();
                }

              }

              { // TEXT LINK

                string sLinkText = nLink.InnerText;

                if( ( sLinkText != null ) && ( sLinkText.Length > 0 ) )
                {
                  sLinkText = HtmlEntity.DeEntitize( sLinkText );
                  if( sLinkText.Length > 0 )
                    HyperlinkOut.SetLinkText( sLinkText );
                }
            
              }

              { // IMAGE LINK

                HtmlNode nImage = nLink.SelectSingleNode( "descendant::img" );

                if( nImage != null )
                {

                  DebugMsg( string.Format( "nImage: {0}", this.GetUrl() ) );
                  DebugMsg( string.Format( "nImage: SRC: {0}", nImage.GetAttributeValue( "src", "UNKNOWN" ) ) );
                  DebugMsg( string.Format( "nImage: {0}", nImage ) );

                  HyperlinkOut.SetHyperlinkType( MacroscopeConstants.HyperlinkType.IMAGE );
                  string sAltText = nLink.GetAttributeValue( "alt", "" );
              
                  DebugMsg( string.Format( "nImage: sAltText: {0}", sAltText ) );
                            
                  if( ( sAltText != null ) && ( sAltText.Length > 0 ) )
                  {
                    sAltText = HtmlEntity.DeEntitize( sAltText );
                    HyperlinkOut.SetAltText( sAltText );
                  }
              
                }

              }
            
            }
          
          }

        }

      }

    }

    /**************************************************************************/

    private void ProcessHtmlOutlinks ( HtmlDocument HtmlDoc )
    {

      { // A HREF links ------------------------------------------------------//

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "href", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );
            string sTitle = nLink.GetAttributeValue( "title", "" );
            string sAltText = nLink.GetAttributeValue( "alt", "" );

            if( sLinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink( 
                                         sLinkUrlAbs,
                                         MacroscopeConstants.InOutLinkType.AHREF,
                                         true
                                       );
              
              if( Outlink != null )
              {
                Outlink.SetTitle( Title: sTitle );
                Outlink.SetAltText( AltText: sAltText );
                Outlink.SetRawTargetUrl( sLinkUrl );
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // META elements -----------------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//meta[@content]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = null;
            string sContent = nLink.GetAttributeValue( "content", null );

            MatchCollection reMatches = Regex.Matches( sContent, "^\\s*[0-9]+;\\s*url=([^\\s]+)\\s*$" );

            for( int i = 0 ; i < reMatches.Count ; i++ )
            {
              if( reMatches[ i ].Groups[ 0 ].Value.Length > 0 )
              {
                sLinkUrl = reMatches[ i ].Groups[ 0 ].Value;
                break;
              }
            }

            if( ( sLinkUrl != null ) && ( sLinkUrl.Length > 0 ) )
            {

              string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

              DebugMsg( string.Format( "META: 1 :: {0}", sLinkUrl ) );
              DebugMsg( string.Format( "META: 2 :: {0}", sLinkUrlAbs ) );

              if( sLinkUrlAbs != null )
              {
                
                MacroscopeLink Outlink = this.AddHtmlOutlink(
                                           AbsoluteUrl: sLinkUrlAbs,
                                           LinkType: MacroscopeConstants.InOutLinkType.META,
                                           Follow: true
                                         );
                
                Outlink.SetRawTargetUrl( sLinkUrl );
                
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // LINK element links

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//link[@href]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "href", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );
            MacroscopeConstants.InOutLinkType LinkType = MacroscopeConstants.InOutLinkType.LINK;
            Boolean bFollow = true;

            if( nLink.GetAttributeValue( "hreflang", null ) != null )
            {
              LinkType = MacroscopeConstants.InOutLinkType.HREFLANG;
              if( !MacroscopePreferencesManager.GetFollowHrefLangLinks() )
              {
                bFollow = false;
              }
            }

            if(
              ( nLink.GetAttributeValue( "rel", null ) != null )
              && ( nLink.GetAttributeValue( "rel", "" ).ToLower() == "stylesheet" ) )
            {
              LinkType = MacroscopeConstants.InOutLinkType.STYLESHEET;
            }

            if( sLinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: LinkType,
                                         Follow: bFollow
                                       );
              
              Outlink.SetRawTargetUrl( sLinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // IFRAME element links -----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/iframe

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//iframe[@src]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {
            string sLinkUrl = nLink.GetAttributeValue( "src", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

            DebugMsg( string.Format( "IFRAME: 1 :: {0}", sLinkUrl ) );
            DebugMsg( string.Format( "IFRAME: 2 :: {0}", sLinkUrlAbs ) );

            if( sLinkUrlAbs != null )
            {

              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.IFRAME,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( sLinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // MAP HREF links

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//map/area[@href]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "href", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

            if( sLinkUrlAbs != null )
            {

              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.MAP,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( sLinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // IMG element links -----------------------------------------------//

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//img[@src]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "src", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );
            string sTitle = nLink.GetAttributeValue( "title", "" );
            string sAltText = nLink.GetAttributeValue( "alt", "" );
            
            DebugMsg( string.Format( "IMAGE: 1 :: {0}", sLinkUrl ) );
            DebugMsg( string.Format( "IMAGE: 2 :: {0}", sLinkUrlAbs ) );

            if( sLinkUrlAbs != null )
            {

              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                                         Follow: true
                                       );

              if( Outlink != null )
              {
                Outlink.SetTitle( Title: sTitle );
                Outlink.SetAltText( AltText: sAltText );
                Outlink.SetRawTargetUrl( sLinkUrl );
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // SCRIPT element links ----------------------------------------------//

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//script[@src]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "src", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

            if( sLinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.SCRIPT,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( sLinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // AUDIO element links -----------------------------------------------//
        // https://developer.mozilla.org/en/docs/Web/HTML/Element/audio

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "(//audio[@src]|//audio/source[@src]|//audio/track[@src])" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "src", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

            DebugMsg( string.Format( "AUDIO: 1 :: {0}", sLinkUrl ) );
            DebugMsg( string.Format( "AUDIO: 2 :: {0}", sLinkUrlAbs ) );

            if( sLinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.AUDIO,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( sLinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // VIDEO element links -----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/video

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "(//video[@src]|//video/source[@src]|//video/track[@src])" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "src", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

            DebugMsg( string.Format( "VIDEO: 1 :: {0}", sLinkUrl ) );
            DebugMsg( string.Format( "VIDEO: 2 :: {0}", sLinkUrlAbs ) );

            if( sLinkUrlAbs != null )
            {

              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.VIDEO,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( sLinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // EMBED element links -----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/embed

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//embed[@src]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "src", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

            DebugMsg( string.Format( "EMBED: 1 :: {0}", sLinkUrl ) );
            DebugMsg( string.Format( "EMBED: 2 :: {0}", sLinkUrlAbs ) );

            if( sLinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.EMBED,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( sLinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // OBJECT element links ----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/object

        HtmlNodeCollection nOutlinks = HtmlDoc.DocumentNode.SelectNodes( "//object[@data]" );

        if( nOutlinks != null )
        {

          foreach( HtmlNode nLink in nOutlinks )
          {

            string sLinkUrl = nLink.GetAttributeValue( "data", null );
            string sLinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, sLinkUrl );

            DebugMsg( string.Format( "OBJECT: 1 :: {0}", sLinkUrl ) );
            DebugMsg( string.Format( "OBJECT: 2 :: {0}", sLinkUrlAbs ) );

            if( sLinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: sLinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.OBJECT,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( sLinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

    }

    /**************************************************************************/

    private MacroscopeLink AddHtmlOutlink (
      string AbsoluteUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      Boolean Follow
    )
    {

      MacroscopeLink OutLink = new MacroscopeLink (
                                 TargetUrl: AbsoluteUrl,
                                 SourceUrl: this.GetUrl(),
                                 LinkType: LinkType,
                                 Follow: Follow
                               );

      this.Outlinks.Add( OutLink );

      return( OutLink );

    }

    /**************************************************************************/

    /// Reference: https://support.google.com/webmasters/answer/189077

    private void ExtractHrefLangAlternates ( HtmlDocument HtmlDoc )
    {

      HtmlNodeCollection nlNodeList = HtmlDoc.DocumentNode.SelectNodes( "//link[@rel='alternate']" );

      if( nlNodeList != null )
      {

        foreach( HtmlNode nNode in nlNodeList )
        {

          MacroscopeHrefLang msHrefLang;
          string sRel = nNode.GetAttributeValue( "rel", "" );
          string sLocale = nNode.GetAttributeValue( "hreflang", "" );
          string sHref = nNode.GetAttributeValue( "href", "" );

          if( sLocale == "" )
          {
            continue;
          }
          else
          {

            if( this.DocUrl == sHref )
            {
              sLocale = this.Locale;
            }

            DebugMsg( string.Format( "HREFLANG: {0}, {1}", sLocale, sHref ) );

            msHrefLang = new MacroscopeHrefLang ( sLocale, sHref );

            this.HrefLang[ sLocale ] = msHrefLang;

          }

        }

      }

    }

    /**************************************************************************/

    private void ExtractHtmlHeadings ( HtmlDocument HtmlDoc )
    {
      for( ushort iLevel = 1 ; iLevel <= 6 ; iLevel++ )
      {
        HtmlNodeCollection nNodes = HtmlDoc.DocumentNode.SelectNodes( string.Format( "//h{0}", iLevel ) );
        if( nNodes != null )
        {
          foreach( HtmlNode nNode in nNodes )
          {
            string sText = nNode.InnerText;
            if( sText != null )
            {
              this.AddHeading( iLevel, sText );
            }
          }
        }
      }
    }

    /**************************************************************************/

    string ProcessHtmlBodyText ( string sHtml )
    {

      HtmlDocument HtmlDoc = new HtmlDocument ();
      List<HtmlNode> NodesToRemove = new List<HtmlNode> ();
      string sText = "";

      HtmlDoc.LoadHtml( sHtml );

      if( HtmlDoc != null )
      {
        
        HtmlNodeCollection nNodeCollection = HtmlDoc.DocumentNode.SelectNodes( "(//script|//style)" );
      
        if( nNodeCollection != null )
        {
          
          foreach( HtmlNode nNode in nNodeCollection )
          {
            NodesToRemove.Add( nNode );
          }

          for( int i = 0 ; i < NodesToRemove.Count ; i++ )
          {
            NodesToRemove[ i ].Remove();
          }
                  
        }

        sText = HtmlDoc.DocumentNode.InnerText;
        sText = Regex.Replace( sText, "<!--.*?-->", "", RegexOptions.Singleline );
        
      }
      
      return( sText );
    }

    /** Extract Email Addresses ***********************************************/

    private void ExtractHtmlEmailAddresses ( HtmlDocument HtmlDoc )
    {
      HtmlNodeCollection nNodes = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
      if( nNodes != null )
      {
        foreach( HtmlNode nLink in nNodes )
        {
          string sLinkUrl = nLink.GetAttributeValue( "href", null );
          if( sLinkUrl != null )
          {
            if( Regex.IsMatch( sLinkUrl, "^mailto:" ) )
            {
              MatchCollection reMatches = Regex.Matches( sLinkUrl, "^mailto:([^?]+)" );
              foreach( Match reMatch in reMatches )
              {
                this.AddEmailAddress( EmailAddress: reMatch.Groups[ 1 ].Value );
              }
            }
          }
        }
      }
    }

    /** Extract Telephone Numbers *********************************************/

    private void ExtractHtmlTelephoneNumbers ( HtmlDocument HtmlDoc )
    {
      HtmlNodeCollection nNodes = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );
      if( nNodes != null )
      {
        foreach( HtmlNode nLink in nNodes )
        {
          string sLinkUrl = nLink.GetAttributeValue( "href", null );
          if( sLinkUrl != null )
          {
            if( Regex.IsMatch( sLinkUrl, "^tel:" ) )
            {
              MatchCollection reMatches = Regex.Matches( sLinkUrl, "^tel:(.+)" );
              foreach( Match reMatch in reMatches )
              {
                this.AddTelephoneNumber( TelephoneNumber: reMatch.Groups[ 1 ].Value );
              }
            }
          }
        }
      }
    }

    /** Sniff Charset *********************************************************/

    Encoding HtmlSniffCharset ()
    {

      Encoding encSniffed = Encoding.UTF8;

      // TODO: Implement code to download HTML, and examine the content-type meta tag.

      return( encSniffed );

    }

    /**************************************************************************/

  }

}
