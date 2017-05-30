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
      string ResponseErrorCondition = null;
      Boolean IsAuthenticating = false;
      
      try
      {

        req = WebRequest.CreateHttp( this.DocUrl );
        req.Method = "GET";
        req.Timeout = this.Timeout;
        req.KeepAlive = false;
        
        this.PrepareRequestHttpHeaders( req: req );
                
        IsAuthenticating = this.AuthenticateRequest( req );
                      
        MacroscopePreferencesManager.EnableHttpProxy( req );

        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "ProcessHtmlPage :: UriFormatException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( TimeoutException ex )
      {

        DebugMsg( string.Format( "ProcessHtmlPage :: TimeoutException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;

      }
      catch( WebException ex )
      {

        DebugMsg( string.Format( "ProcessHtmlPage :: WebException: {0}", ex.Message ) );
        res = ( HttpWebResponse )ex.Response;
        ResponseErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        string RawData = "";

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

          Stream ResponseStream = res.GetResponseStream();
          StreamReader ResponseStreamReader = new StreamReader ( ResponseStream, encUseEncoding );
          RawData = ResponseStreamReader.ReadToEnd();
          this.ContentLength = RawData.Length; // May need to find bytes length
          this.SetChecksum( RawData );
          
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
          HtmlDoc = new HtmlDocument ();
          HtmlDoc.LoadHtml( RawData );
          DebugMsg( string.Format( "htmlDoc: {0}", HtmlDoc ) );
        }
        else
        {
          DebugMsg( string.Format( "RawData: {0}", "EMPTY" ) );
        }

        if( !string.IsNullOrEmpty( RawData ) )
        {
          
          MacroscopeCustomFilter CustomFilter = this.DocCollection.GetJobMaster().GetCustomFilter();

          if( ( CustomFilter != null ) && ( CustomFilter.IsEnabled() ) )
          {
            this.ProcessHtmlCustomFiltered( CustomFilter: CustomFilter, HtmlText: RawData );           
          }

        }

        if( HtmlDoc != null )
        {

          { // Probe Locale
            
            MacroscopeLocaleTools msLocale = new MacroscopeLocaleTools ();

            this.Locale = msLocale.ProbeLocale( HtmlDoc );

            if( this.Locale != null )
            {
              this.SetHreflang( HrefLangLocale: this.Locale, Url: this.DocUrl );
            }

            if( this.Locale != null )
            {
              
              string LanguageCode = this.Locale;
              
              if( LanguageCode.ToLower().Equals( "x-default" ) )
              {
                LanguageCode = "x-default";
              }
              else
              {
                LanguageCode = Regex.Replace( LanguageCode, "^([^\\-]+)([\\-][^\\-]+)$", "$1" );
              }
              
              this.SetIsoLanguageCode( LanguageCode: LanguageCode );
              
            }

          }

          { // Probe Character Set
            try
            {
              if( HtmlDoc.DeclaredEncoding != null )
              {
                this.SetCharacterSet( HtmlDoc.DeclaredEncoding.EncodingName );
              }
              else
              {
                this.SetCharacterSet( "" );
              }
            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( ex.Message ) );
              this.SetCharacterSet( "" );
            }
          }

          { // Title

            HtmlNode TitleNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/title" );

            if( TitleNode != null )
            {
              this.SetTitle( TitleNode.InnerText, MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES );
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
            this.ProcessHtmlCanonical( HtmlDoc: HtmlDoc );
          }

          { // Outlinks

            this.ProcessHtmlHyperlinksOut( HtmlDoc: HtmlDoc );

            this.ProcessHtmlOutlinks( HtmlDoc: HtmlDoc );

          }

          { // Process Inline CSS Links
            this.ProcessHtmlInlineCssLinks( HtmlDoc: HtmlDoc );
            this.ProcessHtmlAttributeCssLinks( HtmlDoc: HtmlDoc );
          }

          { // Extract interesting document elements
            this.ExtractHtmlHeadings( HtmlDoc: HtmlDoc );
          }

          { // Special Links
            this.ExtractHtmlEmailAddresses( HtmlDoc: HtmlDoc );
            this.ExtractHtmlTelephoneNumbers( HtmlDoc: HtmlDoc );
          }

          { // HREFLANG Alternatives
            this.ExtractHrefLangAlternates( HtmlDoc: HtmlDoc );
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
            string Text = this.ProcessHtmlBodyText( HtmlDoc: HtmlDoc );
            if( Text != null )
            {
              this.SetBodyText( Text );
            }
            else
            {
              this.SetBodyText( "" );
            }
          }

        }

        res.Close();
        
        res.Dispose();

      }
      else
      {
        this.AddRemark( "Failed to download HTML." );
      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

    }

    /**************************************************************************/

    private void ProcessHtmlCanonical ( HtmlDocument HtmlDoc )
    {

      HtmlNode CanonicalNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/link[@rel='canonical']" );

      if( CanonicalNode != null )
      {

        this.Canonical = CanonicalNode.GetAttributeValue( "href", "" );
        
        DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
        
        if( MacroscopePreferencesManager.GetFollowCanonicalLinks() )
        {
          
          string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, this.Canonical );
          
          MacroscopeLink Outlink = this.AddHtmlOutlink(
                                     AbsoluteUrl: LinkUrlAbs,
                                     LinkType: MacroscopeConstants.InOutLinkType.CANONICAL,
                                     Follow: true
                                   );
          if( Outlink != null )
          {
            Outlink.SetRawTargetUrl( this.Canonical );
          }
          
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
                    
          DebugMsg( string.Format( "ProcessHtmlHyperlinksOut: {0}", this.GetUrl() ) );

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

                if( !string.IsNullOrEmpty( Rel ) )
                {
                  if( Rel.ToLower().Equals( "nofollow" ) )
                    HyperlinkOut.UnsetDoFollow();
                }

              }

              { // LINK TARGET

                string Target = LinkNode.GetAttributeValue( "target", null );

                if( !string.IsNullOrEmpty( Target ) )
                {
                  HyperlinkOut.SetLinkTarget( Target );
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

      if( this.GetIsExternal() )
      {
        return;
      }

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
            MacroscopeLink Outlink = null;
            
            DebugMsg( string.Format( "ProcessHtmlOutlinks: HREF :    LinkUrl : {0}", LinkUrl ) );
            DebugMsg( string.Format( "ProcessHtmlOutlinks: HREF : LinkUrlAbs : {0}", LinkUrlAbs ) );

            if( LinkUrlAbs != null )
            {
              
              Outlink = this.AddHtmlOutlink( 
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
              MacroscopeLink Outlink = null;
            
              if( LinkUrlAbs != null )
              {
                
                Outlink = this.AddHtmlOutlink(
                  AbsoluteUrl: LinkUrlAbs,
                  LinkType: MacroscopeConstants.InOutLinkType.META,
                  Follow: true
                );
                
                if( Outlink != null )
                {
                  Outlink.SetRawTargetUrl( LinkUrl );
                }
                
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
            Boolean Follow = true;
            MacroscopeLink Outlink = null;

            if( LinkNode.GetAttributeValue( "hreflang", null ) != null )
            {

              LinkType = MacroscopeConstants.InOutLinkType.HREFLANG;

              if( MacroscopePreferencesManager.GetFollowHrefLangLinks() )
              {
                Follow = true;
                this.DocCollection.GetAllowedHosts().AddFromUrl( Url: LinkUrlAbs );
              }
              else
              {
                Follow = false;
              }

            }

            if(
              ( LinkNode.GetAttributeValue( "rel", null ) != null )
              && ( LinkNode.GetAttributeValue( "rel", "" ).ToLower() == "stylesheet" ) )
            {
              LinkType = MacroscopeConstants.InOutLinkType.STYLESHEET;
            }
            else
            if(
              ( LinkNode.GetAttributeValue( "rel", null ) != null )
              && ( LinkNode.GetAttributeValue( "rel", "" ).ToLower() == "alternate" ) )
            {
              LinkType = MacroscopeConstants.InOutLinkType.ALTERNATE;
            }

            if( LinkUrlAbs != null )
            {
              
              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: LinkType,
                Follow: Follow
              );
              
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }
              
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
            MacroscopeLink Outlink = null;
              
            if( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.IFRAME,
                Follow: true
              );
              
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }
              
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
            MacroscopeLink Outlink = null;
              
            if( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.MAP,
                Follow: true
              );
              
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }
              
            }

          }

        }

      } // -------------------------------------------------------------------//

      { // IMG element links -------------------------------------------------// 
       
        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//img[@src]" );

        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, LinkUrl );
            string LinkTitle = LinkNode.GetAttributeValue( "title", "" );
            string LinkAltText = LinkNode.GetAttributeValue( "alt", "" );
            MacroscopeLink Outlink = null;
              
            if( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
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
            MacroscopeLink Outlink = null;
              
            if( LinkUrlAbs != null )
            {
              
              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.SCRIPT,
                Follow: true
              );
              
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }
              
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
            MacroscopeLink Outlink = null;
              
            if( LinkUrlAbs != null )
            {
              
              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.AUDIO,
                Follow: true
              );
              
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }
              
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
            MacroscopeLink Outlink = null;
              
            if( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.VIDEO,
                Follow: true
              );
              
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }
         
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
            MacroscopeLink Outlink = null;
              
            if( LinkUrlAbs != null )
            {
              
              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.EMBED,
                Follow: true
              );
              
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }
              
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
            MacroscopeLink Outlink = null;
              
            if( LinkUrlAbs != null )
            {
              
              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.OBJECT,
                Follow: true
              );
              
              if( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }
              
            }

          }

        }

      } // -------------------------------------------------------------------//

    }

    /** Process Inline CSS Links **********************************************/

    private void ProcessHtmlInlineCssLinks ( HtmlDocument HtmlDoc )
    {

      if( HtmlDoc != null )
      {
        
        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//style" );
      
        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {
          
          DebugMsg( string.Format( "ProcessHtmlInlineCssLinks: {0}", NodeCollection.Count ) );
                        
          foreach( HtmlNode Node in NodeCollection )
          {

            string CssText = Node.InnerText;
                          
            if( !string.IsNullOrEmpty( CssText ) )
            {
              
              DebugMsg( string.Format( "ProcessHtmlInlineCssLinks: {0}", CssText ) );

              ExCSS.Parser ExCssParser = new ExCSS.Parser ();
              ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( CssText );

              this.ProcessCssOutlinks( ExCssStylesheet );

            }

          }

        }

      }

    }

    /** -------------------------------------------------------------------- **/
    
    private void ProcessHtmlAttributeCssLinks ( HtmlDocument HtmlDoc )
    {

      if( HtmlDoc != null )
      {
        
        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//*[@style]" );
      
        if( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {
          
          DebugMsg( string.Format( "ProcessHtmlAttributeCssLinks: {0}", NodeCollection.Count ) );
                    
          foreach( HtmlNode Node in NodeCollection )
          {

            string CssText = Node.GetAttributeValue( "style", "" );
            
            if( !string.IsNullOrEmpty( CssText ) )
            {
              
              DebugMsg( string.Format( "ProcessHtmlAttributeCssLinks: {0}", CssText ) );

              ExCSS.Parser ExCssParser = new ExCSS.Parser ();
              ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( CssText );

              this.ProcessCssOutlinks( ExCssStylesheet );

            }

          }

        }

      }

    }

    /**************************************************************************/

    private MacroscopeLink AddHtmlOutlink (
      string AbsoluteUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      Boolean Follow
    )
    {
      
      MacroscopeLink OutLink = null;
      Boolean Proceed = true;

      if( !MacroscopePreferencesManager.GetCheckExternalLinks() )
      {
        
        if( this.DocCollection != null )
        {  
    
          MacroscopeAllowedHosts AllowedHosts = this.DocCollection.GetAllowedHosts();
        
          if( AllowedHosts != null )
          {
        
            if( !AllowedHosts.IsAllowedFromUrl( Url: AbsoluteUrl ) )
            {
              Proceed = false;
            }
          
          }
        
        }

      }

      switch( LinkType )
      {
        case MacroscopeConstants.InOutLinkType.STYLESHEET:
          if( !MacroscopePreferencesManager.GetFetchStylesheets() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.SCRIPT:
          if( !MacroscopePreferencesManager.GetFetchJavascripts() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.IMAGE:
          if( !MacroscopePreferencesManager.GetFetchImages() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.AUDIO:
          if( !MacroscopePreferencesManager.GetFetchAudio() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.VIDEO:
          if( !MacroscopePreferencesManager.GetFetchVideo() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.EMBED:
          if( !MacroscopePreferencesManager.GetFetchBinaries() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.OBJECT:
          if( !MacroscopePreferencesManager.GetFetchBinaries() )
          {
            Proceed = false;
          }
          break;
      }

      //Proceed = true; // TODO: REMOVE THIS
      
      if( Proceed )
      {

        OutLink = new MacroscopeLink (
          SourceUrl: this.GetUrl(),
          TargetUrl: AbsoluteUrl,
          LinkType: LinkType,
          Follow: Follow
        );

        this.Outlinks.Add( OutLink );

      }

      return( OutLink );

    }

    /**************************************************************************/

    /// Reference: https://support.google.com/webmasters/answer/189077

    private void ExtractHrefLangAlternates ( HtmlDocument HtmlDoc )
    {

      HtmlNodeCollection NodeList = HtmlDoc.DocumentNode.SelectNodes( "//link[@rel='alternate']" );

      if( NodeList != null )
      {

        foreach( HtmlNode LinkNode in NodeList )
        {

          MacroscopeHrefLang msHrefLang;
          string Rel = LinkNode.GetAttributeValue( "rel", "" );
          string HrefLangLocale = LinkNode.GetAttributeValue( "hreflang", "" );
          string Href = LinkNode.GetAttributeValue( "href", "" );

          if( HrefLangLocale == "" )
          {
            continue;
          }
          else
          {

            if( this.DocUrl == Href )
            {
              HrefLangLocale = this.Locale;
            }

            DebugMsg( string.Format( "HREFLANG: {0}, {1}", HrefLangLocale, Href ) );

            msHrefLang = new MacroscopeHrefLang ( HrefLangLocale, Href );

            this.HrefLang[ HrefLangLocale ] = msHrefLang;

          }

        }

      }

    }

    /**************************************************************************/

    private void ExtractHtmlHeadings ( HtmlDocument HtmlDoc )
    {

      for( ushort HeadingLevel = 1 ; HeadingLevel <= 6 ; HeadingLevel++ )
      {

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes(
                                              string.Format( "//h{0}", HeadingLevel )
                                            );

        if( NodeCollection != null )
        {

          foreach( HtmlNode Node in NodeCollection )
          {

            string HeadingText = Node.InnerText;

            if( HeadingText != null )
            {
              this.AddHeading( HeadingLevel, HeadingText );
            }
            
          }
          
        }
        
      }
      
    }

    /** Process Body Text *****************************************************/

    string ProcessHtmlBodyText ( HtmlDocument HtmlDoc )
    {

      List<HtmlNode> NodesToRemove = new List<HtmlNode> ();
      string BodyTextProcessed = "";

      if( HtmlDoc != null )
      {
        
        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "(//script|//style)" );
      
        if( NodeCollection != null )
        {
          
          foreach( HtmlNode Node in NodeCollection )
          {
            NodesToRemove.Add( Node );
          }

          for( int i = 0 ; i < NodesToRemove.Count ; i++ )
          {
            NodesToRemove[ i ].Remove();
          }
                  
        }

        BodyTextProcessed = HtmlDoc.DocumentNode.InnerText;
        BodyTextProcessed = Regex.Replace( BodyTextProcessed, "<!--.*?-->", "", RegexOptions.Singleline );
        
      }
      
      return( BodyTextProcessed );
    }

    /** Extract Email Addresses ***********************************************/

    private void ExtractHtmlEmailAddresses ( HtmlDocument HtmlDoc )
    {

      HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

      if( NodeCollection != null )
      {

        foreach( HtmlNode LinkNode in NodeCollection )
        {

          string LinkUrl = LinkNode.GetAttributeValue( "href", null );

          if( LinkUrl != null )
          {

            if( Regex.IsMatch( LinkUrl, "^mailto:" ) )
            {

              MatchCollection reMatches = Regex.Matches( LinkUrl, "^mailto:([^?]+)" );

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

      HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

      if( NodeCollection != null )
      {

        foreach( HtmlNode LinkNode in NodeCollection )
        {

          string LinkUrl = LinkNode.GetAttributeValue( "href", null );

          if( LinkUrl != null )
          {

            if( Regex.IsMatch( LinkUrl, "^tel:" ) )
            {

              MatchCollection reMatches = Regex.Matches( LinkUrl, "^tel:(.+)" );

              foreach( Match reMatch in reMatches )
              {
                this.AddTelephoneNumber( TelephoneNumber: reMatch.Groups[ 1 ].Value );
              }
              
            }
            
          }
          
        }
        
      }
      
    }

    /** Process Custom Filtered *********************************************/

    private void ProcessHtmlCustomFiltered (
      MacroscopeCustomFilter CustomFilter,
      string HtmlText
    )
    {

      Dictionary<string, MacroscopeConstants.TextPresence> Analyzed = CustomFilter.AnalyzeText( Text: HtmlText );

      foreach( string Key in Analyzed.Keys )
      {
        this.SetCustomFiltered( Text: Key, Presence: Analyzed[ Key ] );
      }

      return;
      
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
