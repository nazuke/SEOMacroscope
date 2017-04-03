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

          { // Process META Tags
            // https://moz.com/blog/meta-referrer-tag

            HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//head/meta" );

            if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
            {

              foreach( HtmlNode MetaNode in NodeCollection )
              {

                string MetaName = MetaNode.GetAttributeValue( "name", null );        
                string MetaContent = MetaNode.GetAttributeValue( "content", null );

                if( ( !string.IsNullOrEmpty( MetaName ) ) && ( !string.IsNullOrEmpty( MetaContent ) ) )
                {

                  DebugMsg( string.Format( "META: {0} :: {1}", MetaName, MetaContent ) );

                  lock( this.MetaHeaders )
                  {
                    if( this.MetaHeaders.ContainsKey( MetaName ) )
                    {
                      this.MetaHeaders[ MetaName ] = MetaContent;
                    }
                    else
                    {
                      this.MetaHeaders.Add( MetaName, MetaContent );
                    }
                  }

                }

              }

            }

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

      HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

      if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
      {

        foreach( HtmlNode LinkNode in NodeCollection )
        {

          MacroscopeHyperlinkOut HyperlinkOut = null;
          string LinkUrl = LinkNode.GetAttributeValue( "href", null );
          string LinkUrlAbsolute = null;
          string LinkTitle = LinkNode.GetAttributeValue( "title", "" );
          
          DebugMsg( string.Format( "ImageNode: {0}", this.GetUrl() ) );
          
          
          if( LinkUrl != null )
          {

            LinkUrlAbsolute = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

            if( LinkUrlAbsolute != null )
            {

              this.SetProcessHyperlinksInForUrl( Url: LinkUrlAbsolute );

              HyperlinkOut = this.HyperlinksOut.Add(
                LinkType: MacroscopeConstants.HyperlinkType.TEXT,
                UrlTarget: LinkUrlAbsolute
              );

              if( LinkTitle.Length > 0 )
              {
                LinkTitle = HtmlEntity.DeEntitize( LinkTitle );
                if( LinkTitle.Length > 0 )
                  HyperlinkOut.SetLinkTitle( LinkTitle );
              }

              { // FOLLOW / NOFOLLOW

                string Rel = LinkNode.GetAttributeValue( "rel", null );

                if( Rel != null )
                {
                  if( Rel.ToLower().Contains( "nofollow" ) )
                    HyperlinkOut.UnsetDoFollow();
                }

              }

              { // TEXT LINK

                string LinkText = LinkNode.InnerText;

                if( ( LinkText != null ) && ( LinkText.Length > 0 ) )
                {
                  LinkText = HtmlEntity.DeEntitize( LinkText );
                  if( LinkText.Length > 0 )
                    HyperlinkOut.SetLinkText( LinkText );
                }
            
              }

              { // IMAGE LINK

                HtmlNode ImageNode = LinkNode.SelectSingleNode( "descendant::img" );

                if( ImageNode != null )
                {

                  DebugMsg( string.Format( "ImageNode: {0}", this.GetUrl() ) );
                  DebugMsg( string.Format( "ImageNode: SRC: {0}", ImageNode.GetAttributeValue( "src", "UNKNOWN" ) ) );
                  DebugMsg( string.Format( "ImageNode: {0}", ImageNode ) );

                  HyperlinkOut.SetHyperlinkType( MacroscopeConstants.HyperlinkType.IMAGE );
                  string LinkAltText = LinkNode.GetAttributeValue( "alt", "" );
              
                  DebugMsg( string.Format( "ImageNode: LinkAltText: {0}", LinkAltText ) );
                            
                  if( ( LinkAltText != null ) && ( LinkAltText.Length > 0 ) )
                  {
                    LinkAltText = HtmlEntity.DeEntitize( LinkAltText );
                    HyperlinkOut.SetAltText( LinkAltText );
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

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "href", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );
            string LinkTitle = LinkNode.GetAttributeValue( "title", "" );
            string LinkAltText = LinkNode.GetAttributeValue( "alt", "" );

            DebugMsg( string.Format( "ProcessHtmlOutlinks: HREF :    LinkUrl : {0}", LinkUrl ) );
            DebugMsg( string.Format( "ProcessHtmlOutlinks: HREF : LinkUrlAbs : {0}", LinkUrlAbs ) );

            if( LinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink( 
                                         LinkUrlAbs,
                                         MacroscopeConstants.InOutLinkType.AHREF,
                                         true
                                       );
              
              if( Outlink != null )
              {
                Outlink.SetTitle( Title: LinkTitle );
                Outlink.SetAltText( AltText: LinkAltText );
                Outlink.SetRawTargetUrl( LinkUrl );
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // META elements -----------------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/meta

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//meta[@content]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = null;
            string Content = LinkNode.GetAttributeValue( "content", null );

            MatchCollection reMatches = Regex.Matches( Content, "^\\s*[0-9]+;\\s*url=([^\\s]+)\\s*$" );

            for( int i = 0 ; i < reMatches.Count ; i++ )
            {
              if( reMatches[ i ].Groups[ 0 ].Value.Length > 0 )
              {
                LinkUrl = reMatches[ i ].Groups[ 0 ].Value;
                break;
              }
            }

            if( ( LinkUrl != null ) && ( LinkUrl.Length > 0 ) )
            {

              string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

              if( LinkUrlAbs != null )
              {
                
                MacroscopeLink Outlink = this.AddHtmlOutlink(
                                           AbsoluteUrl: LinkUrlAbs,
                                           LinkType: MacroscopeConstants.InOutLinkType.META,
                                           Follow: true
                                         );
                
                Outlink.SetRawTargetUrl( LinkUrl );
                
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // LINK element links

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//link[@href]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "href", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );
            MacroscopeConstants.InOutLinkType LinkType = MacroscopeConstants.InOutLinkType.LINK;
            Boolean bFollow = true;

            if( LinkNode.GetAttributeValue( "hreflang", null ) != null )
            {
              LinkType = MacroscopeConstants.InOutLinkType.HREFLANG;
              if( !MacroscopePreferencesManager.GetFollowHrefLangLinks() )
              {
                bFollow = false;
              }
            }

            if(
              ( LinkNode.GetAttributeValue( "rel", null ) != null )
              && ( LinkNode.GetAttributeValue( "rel", "" ).ToLower() == "stylesheet" ) )
            {
              LinkType = MacroscopeConstants.InOutLinkType.STYLESHEET;
            }

            if( LinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: LinkType,
                                         Follow: bFollow
                                       );
              
              Outlink.SetRawTargetUrl( LinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // IFRAME element links -----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/iframe

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//iframe[@src]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {
            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

            if( LinkUrlAbs != null )
            {

              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.IFRAME,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( LinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // MAP HREF links

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//map/area[@href]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "href", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

            if( LinkUrlAbs != null )
            {

              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.MAP,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( LinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // IMG element links -----------------------------------------------//

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//img[@src]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );
            string LinkTitle = LinkNode.GetAttributeValue( "title", "" );
            string LinkAltText = LinkNode.GetAttributeValue( "alt", "" );
            
            if( LinkUrlAbs != null )
            {

              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                                         Follow: true
                                       );

              if( Outlink != null )
              {
                Outlink.SetTitle( Title: LinkTitle );
                Outlink.SetAltText( AltText: LinkAltText );
                Outlink.SetRawTargetUrl( TargetUrl: LinkUrl );
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // SCRIPT element links ----------------------------------------------//

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//script[@src]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

            if( LinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.SCRIPT,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( LinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // AUDIO element links -----------------------------------------------//
        // https://developer.mozilla.org/en/docs/Web/HTML/Element/audio

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "(//audio[@src]|//audio/source[@src]|//audio/track[@src])" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

            if( LinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.AUDIO,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( LinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // VIDEO element links -----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/video

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "(//video[@src]|//video/source[@src]|//video/track[@src])" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

            if( LinkUrlAbs != null )
            {

              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.VIDEO,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( LinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // EMBED element links -----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/embed

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//embed[@src]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

            if( LinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.EMBED,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( LinkUrl );
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // OBJECT element links ----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/object

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//object[@data]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "data", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );

            if( LinkUrlAbs != null )
            {
              
              MacroscopeLink Outlink = this.AddHtmlOutlink(
                                         AbsoluteUrl: LinkUrlAbs,
                                         LinkType: MacroscopeConstants.InOutLinkType.OBJECT,
                                         Follow: true
                                       );
              
              Outlink.SetRawTargetUrl( LinkUrl );
              
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
                                 SourceUrl: this.GetUrl(),
                                 TargetUrl: AbsoluteUrl,
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
