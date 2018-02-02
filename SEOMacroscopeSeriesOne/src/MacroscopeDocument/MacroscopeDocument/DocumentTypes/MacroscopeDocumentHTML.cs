/*

	This file is part of SEOMacroscope.

	Copyright 2018 Jason Holland.

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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using HtmlAgilityPack;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private void ConfigureHtmlPageRequestHeadersCallback ( HttpRequestMessage Request )
    {
      this.AuthenticateRequest( Request: Request );
    }

    /** -------------------------------------------------------------------- **/

    private async Task ProcessHtmlPage ()
    {

      Stopwatch TimeDuration = new Stopwatch();
      long FinalDuration;

      TimeDuration.Start();

      try
      {
        await this._ProcessHtmlPage();
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "ProcessHtmlPage: {0}", ex.Message ) );
      }

      TimeDuration.Stop();
      FinalDuration = TimeDuration.ElapsedMilliseconds;

      if ( FinalDuration > 0 )
      {
        this.Duration = FinalDuration;
      }
      else
      {
        this.Duration = 0;
      }

    }

    /** -------------------------------------------------------------------- **/

    private async Task _ProcessHtmlPage ()
    {

      HtmlDocument HtmlDoc = null;

      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse ClientResponse = null;
      string ResponseErrorCondition = null;

      try
      {

        ClientResponse = await Client.Get(
          this.GetUri(),
          this.ConfigureHtmlPageRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback,
          MacroscopeHttpTwoClient.DecodeResponseContentAs.STRING
        );

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "_ProcessHtmlPage :: MacroscopeDocumentException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.SetStatusCode( HttpStatusCode.BadRequest );
        this.AddRemark( "_ProcessHtmlPage", ex.Message );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "_ProcessHtmlPage :: Exception: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
        this.SetStatusCode( HttpStatusCode.BadRequest );
        this.AddRemark( "_ProcessHtmlPage", ex.Message );
      }

      if ( ClientResponse != null )
      {

        string RawData = "";

        this.ProcessResponseHttpHeaders( Response: ClientResponse );

        /** Get Response Body ---------------------------------------------- **/

        try
        {

          this.DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );

          /*
          Encoding UseEncoding = Encoding.UTF8;

          if( this.GetCharacterEncoding() != null )
          {
            UseEncoding = this.GetCharacterEncoding();
          }
          else
          {
            UseEncoding = this.HtmlSniffCharset();
          }
          */

          RawData = ClientResponse.GetContentAsString();
          this.SetContentLength( Length: RawData.Length ); // May need to find bytes length
          this.SetChecksum( RawData );

          this.SetWasDownloaded( true );

        }
        catch ( Exception ex )
        {

          this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );

          this.SetStatusCode( HttpStatusCode.Ambiguous );

          RawData = "";
          this.SetContentLength( Length: 0 );

        }

        /** ---------------------------------------------------------------- **/

        if ( !string.IsNullOrEmpty( RawData ) )
        {

          HtmlDoc = this.ParseRawDataIntoHtmlDocument( RawData: RawData );

        }

        /** ---------------------------------------------------------------- **/

        if ( !string.IsNullOrEmpty( RawData ) )
        {

          if (
            MacroscopePreferencesManager.GetCustomFiltersEnable()
            && MacroscopePreferencesManager.GetCustomFiltersApplyToHtml() )
          {

            MacroscopeCustomFilters CustomFilter = this.DocCollection.GetJobMaster().GetCustomFilter();

            if ( ( CustomFilter != null ) && ( CustomFilter.IsEnabled() ) )
            {
              this.ProcessHtmlCustomFiltered( CustomFilter: CustomFilter, HtmlText: RawData );
            }

          }

        }

        /** ---------------------------------------------------------------- **/

        if ( !string.IsNullOrEmpty( RawData ) )
        {

          if (
            MacroscopePreferencesManager.GetDataExtractorsEnable()
            && MacroscopePreferencesManager.GetDataExtractorsApplyToHtml() )
          {

            this.ProcessHtmlDataExtractors( HtmlText: RawData );

          }

        }

        /** ---------------------------------------------------------------- **/

        if ( HtmlDoc != null )
        {

          { // Probe Base HREF 
            this.ProcessHtmlBaseHref( HtmlDoc: HtmlDoc );
          }

          { // Probe Locale

            MacroscopeLocaleTools msLocale = new MacroscopeLocaleTools();

            this.Locale = msLocale.ProbeLocale( HtmlDoc: HtmlDoc );

            if ( this.Locale != null )
            {
              this.SetHreflang( HrefLangLocale: this.Locale, Url: this.DocUrl );
            }

            if ( this.Locale != null )
            {

              string LanguageCode = this.Locale;

              if ( LanguageCode.ToLower().Equals( "x-default" ) )
              {
                LanguageCode = "x-default";
              }
              else
              {
                LanguageCode = Regex.Replace( LanguageCode, @"^([^\-]+)([\-][^\-]+)$", "$1" );
              }

              this.SetIsoLanguageCode( LanguageCode: LanguageCode );

            }

          }

          { // Probe Character Set
            try
            {
              if ( HtmlDoc.DeclaredEncoding != null )
              {
                this.SetCharacterSet( HtmlDoc.DeclaredEncoding.EncodingName );
              }
              else
              {
                this.SetCharacterSet( "" );
              }
            }
            catch ( Exception ex )
            {
              this.DebugMsg( string.Format( ex.Message ) );
              this.SetCharacterSet( "" );
            }
          }

          { // Title

            HtmlNode TitleNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/title" );

            if ( TitleNode != null )
            {
              this.SetTitle( TitleNode.InnerText, MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES );
              this.DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
            }
            else
            {
              this.DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
            }

          }

          { // Description
            HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='description']" );
            if ( nNode != null )
            {
              this.Description = nNode.GetAttributeValue( "content", null );
              this.DebugMsg( string.Format( "DESCRIPTION: {0}", this.Description ) );
            }
            else
            {
              this.Description = null;
              this.DebugMsg( string.Format( "DESCRIPTION: {0}", "MISSING" ) );
            }
          }

          { // Keywords
            HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@name='keywords']" );
            if ( nNode != null )
            {
              this.Keywords = nNode.GetAttributeValue( "content", null );
              this.DebugMsg( string.Format( "KEYWORDS: {0}", this.Keywords ) );
            }
            else
            {
              this.Keywords = null;
              this.DebugMsg( string.Format( "KEYWORDS: {0}", "MISSING" ) );
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

            this.AnalyzeHrefLangAlternates( HtmlDoc: HtmlDoc );

          }

          { // Process META Tags
            // https://moz.com/blog/meta-referrer-tag

            HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//head/meta" );

            if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
            {

              foreach ( HtmlNode MetaNode in NodeCollection )
              {

                string MetaName = MetaNode.GetAttributeValue( "name", null );
                string MetaContent = MetaNode.GetAttributeValue( "content", null );

                if ( ( !string.IsNullOrEmpty( MetaName ) ) && ( !string.IsNullOrEmpty( MetaContent ) ) )
                {

                  this.DebugMsg( string.Format( "META: {0} :: {1}", MetaName, MetaContent ) );

                  lock ( this.MetaHeaders )
                  {
                    if ( this.MetaHeaders.ContainsKey( MetaName ) )
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

          { // Process Document Text

            HtmlDocument HtmlDocDocumentText = this.ParseRawDataIntoHtmlDocument( RawData: RawData );

            if ( HtmlDocDocumentText != null )
            {

              string Text = this.ProcessHtmlDocumentText( HtmlDoc: HtmlDocDocumentText );

              if ( !string.IsNullOrEmpty( Text ) )
              {
                this.SetDocumentText( Text: Text );
              }
              else
              {
                this.SetDocumentText( Text: "" );
              }

            }
            else
            {
              this.SetDocumentText( Text: "" );
            }

          }

          { // Process Body Text

            HtmlDocument HtmlDocBodyText = this.ParseRawDataIntoHtmlDocument( RawData: RawData );

            if ( HtmlDocBodyText != null )
            {

              string Text = this.ProcessHtmlBodyText( HtmlDoc: HtmlDocBodyText );

              if ( Text != null )
              {
                this.SetBodyText( Text: Text );
              }
              else
              {
                this.SetBodyText( Text: "" );
              }

            }
            else
            {
              this.SetBodyText( Text: "" );
            }

          }

        }

        /** ---------------------------------------------------------------- **/

      }
      else
      {
        this.AddRemark( "DOWNLOAD_FAILED_HTML", "Failed to download HTML." );
      }

      if ( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

    }

    /**************************************************************************/

    private HtmlDocument ParseRawDataIntoHtmlDocument ( string RawData )
    {

      HtmlDocument HtmlDoc = null;

      if ( !string.IsNullOrEmpty( RawData ) )
      {

        HtmlDoc = new HtmlDocument();

        HtmlDoc.LoadHtml( RawData );

      }

      return ( HtmlDoc );

    }

    /**************************************************************************/

    private void ProcessHtmlBaseHref ( HtmlDocument HtmlDoc )
    {

      HtmlNode BaseHrefNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/base[@href]" );

      if ( BaseHrefNode != null )
      {

        this.SetBaseHref( Url: BaseHrefNode.GetAttributeValue( "href", "" ) );

        this.DebugMsg( string.Format( "BASE HREF: {0}", this.BaseHref ) );

      }
      else
      {

        this.UnsetBaseHref();

        this.DebugMsg( string.Format( "BASE HREF: {0}", "MISSING" ) );

      }

    }

    /**************************************************************************/

    private void ProcessHtmlCanonical ( HtmlDocument HtmlDoc )
    {

      HtmlNode CanonicalNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/link[@rel='canonical']" );

      if ( CanonicalNode != null )
      {

        this.Canonical = CanonicalNode.GetAttributeValue( "href", "" );

        this.DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );

        if ( MacroscopePreferencesManager.GetFollowCanonicalLinks() )
        {
          string LinkUrlAbs;

          LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
            BaseHref: this.BaseHref,
            BaseUrl: this.DocUrl,
            Url: this.Canonical
          );

          MacroscopeLink Outlink;

          Outlink = this.AddHtmlOutlink(
            AbsoluteUrl: LinkUrlAbs,
            LinkType: MacroscopeConstants.InOutLinkType.CANONICAL,
            Follow: true
          );

          if ( Outlink != null )
          {
            Outlink.SetRawTargetUrl( this.Canonical );
          }

        }

      }
      else
      {

        this.Canonical = "";
        this.DebugMsg( string.Format( "CANONICAL: {0}", "MISSING" ) );

      }

    }

    /**************************************************************************/

    private void ProcessHtmlHyperlinksOut ( HtmlDocument HtmlDoc )
    {

      HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

      if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
      {

        foreach ( HtmlNode LinkNode in NodeCollection )
        {

          MacroscopeHyperlinkOut HyperlinkOut = null;
          string LinkUrl = LinkNode.GetAttributeValue( "href", null );
          string LinkUrlAbsolute = null;
          string LinkTitle = LinkNode.GetAttributeValue( "title", "" );

          this.DebugMsg( string.Format( "ProcessHtmlHyperlinksOut: {0}", this.GetUrl() ) );

          if ( LinkUrl != null )
          {

            LinkUrlAbsolute = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
              BaseHref: this.GetBaseHref(),
              BaseUrl: this.DocUrl,
              Url: LinkUrl
            );

            if ( LinkUrlAbsolute != null )
            {

              this.SetProcessHyperlinksInForUrl( Url: LinkUrlAbsolute );

              HyperlinkOut = this.HyperlinksOut.Add(
                LinkType: MacroscopeConstants.HyperlinkType.TEXT,
                UrlTarget: LinkUrlAbsolute
              );

              HyperlinkOut.SetRawTargetUrl( TargetUrl: LinkUrl );

              if ( LinkTitle.Length > 0 )
              {

                try
                {
                  LinkTitle = HtmlEntity.DeEntitize( LinkTitle );
                }
                catch ( Exception ex )
                {
                  this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", this.GetUrl() ) );
                  this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", LinkTitle ) );
                  this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", ex.Message ) );
                }

                if ( !string.IsNullOrEmpty( LinkTitle ) )
                  HyperlinkOut.SetLinkTitle( LinkTitle );

              }

              { // FOLLOW / NOFOLLOW

                string Rel = LinkNode.GetAttributeValue( "rel", null );

                if ( !string.IsNullOrEmpty( Rel ) )
                {
                  if ( Rel.ToLower().Equals( "nofollow" ) )
                    HyperlinkOut.UnsetDoFollow();
                }

              }

              { // LINK TARGET

                string Target = LinkNode.GetAttributeValue( "target", null );

                if ( !string.IsNullOrEmpty( Target ) )
                {
                  HyperlinkOut.SetLinkTarget( Target );
                }

              }

              { // TEXT LINK

                string LinkText = LinkNode.InnerText;

                if ( !string.IsNullOrEmpty( LinkText ) )
                {

                  try
                  {
                    LinkText = HtmlEntity.DeEntitize( LinkText );
                  }
                  catch ( Exception ex )
                  {
                    this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", this.GetUrl() ) );
                    this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", LinkText ) );
                    this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", ex.Message ) );
                  }

                  if ( !string.IsNullOrEmpty( LinkText ) )
                    HyperlinkOut.SetLinkText( LinkText );

                }

              }

              { // IMAGE LINK

                HtmlNode ImageNode = LinkNode.SelectSingleNode( "descendant::img" );

                if ( ImageNode != null )
                {

                  this.DebugMsg( string.Format( "ImageNode: {0}", this.GetUrl() ) );
                  this.DebugMsg( string.Format( "ImageNode: SRC: {0}", ImageNode.GetAttributeValue( "src", "UNKNOWN" ) ) );
                  this.DebugMsg( string.Format( "ImageNode: {0}", ImageNode ) );

                  HyperlinkOut.SetHyperlinkType( MacroscopeConstants.HyperlinkType.IMAGE );
                  string LinkAltText = LinkNode.GetAttributeValue( "alt", "" );

                  this.DebugMsg( string.Format( "ImageNode: LinkAltText: {0}", LinkAltText ) );

                  if ( !string.IsNullOrEmpty( LinkAltText ) )
                  {

                    try
                    {
                      LinkAltText = HtmlEntity.DeEntitize( LinkAltText );
                    }
                    catch ( Exception ex )
                    {
                      this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", this.GetUrl() ) );
                      this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", LinkAltText ) );
                      this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", ex.Message ) );
                    }

                    if ( !string.IsNullOrEmpty( LinkAltText ) )
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

    // TODO: Check for unimplemented hyperlink types:

    private void ProcessHtmlOutlinks ( HtmlDocument HtmlDoc )
    {

      if ( this.GetIsExternal() )
      {
        return;
      }

      { // A HREF links ------------------------------------------------------//

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = null;
            string LinkUrlAbs = null;
            string LinkTitle = null;
            string LinkAltText = null;
            MacroscopeLink Outlink = null;

            LinkUrl = LinkNode.GetAttributeValue( "href", null );

            LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
              BaseHref: this.GetBaseHref(),
              BaseUrl: this.DocUrl,
              Url: LinkUrl
            );

            LinkTitle = LinkNode.GetAttributeValue( "title", "" );

            LinkAltText = LinkNode.GetAttributeValue( "alt", "" );

            this.DebugMsg( string.Format( "ProcessHtmlOutlinks: HREF :    LinkUrl : {0}", LinkUrl ) );
            this.DebugMsg( string.Format( "ProcessHtmlOutlinks: HREF : LinkUrlAbs : {0}", LinkUrlAbs ) );

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.AHREF,
                Follow: true
              );

              if ( Outlink != null )
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

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = null;
            string Content = LinkNode.GetAttributeValue( "content", null );

            MatchCollection reMatches = Regex.Matches( Content, @"^\s*[0-9]+;\s*url=([^\s]+)\s*$" );

            for ( int i = 0 ; i < reMatches.Count ; i++ )
            {
              if ( reMatches[ i ].Groups[ 0 ].Value.Length > 0 )
              {
                LinkUrl = reMatches[ i ].Groups[ 0 ].Value;
                break;
              }
            }

            if ( ( LinkUrl != null ) && ( LinkUrl.Length > 0 ) )
            {

              string LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LinkUrl );
              MacroscopeLink Outlink = null;

              if ( LinkUrlAbs != null )
              {

                Outlink = this.AddHtmlOutlink(
                  AbsoluteUrl: LinkUrlAbs,
                  LinkType: MacroscopeConstants.InOutLinkType.META,
                  Follow: true
                );

                if ( Outlink != null )
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

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "href", null );
            string LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LinkUrl );
            MacroscopeConstants.InOutLinkType LinkType = MacroscopeConstants.InOutLinkType.LINK;
            bool Follow = true;
            MacroscopeLink Outlink = null;

            if ( !string.IsNullOrEmpty( LinkNode.GetAttributeValue( "hreflang", null ) ) )
            {

              LinkType = MacroscopeConstants.InOutLinkType.HREFLANG;

              if ( MacroscopePreferencesManager.GetFollowHrefLangLinks() )
              {
                Follow = true;
                this.DocCollection.GetAllowedHosts().AddFromUrl( Url: LinkUrlAbs );
              }
              else
              {
                Follow = false;
              }

            }

            switch ( LinkNode.GetAttributeValue( "rel", "" ).ToLower() )
            {
              case "stylesheet":
                LinkType = MacroscopeConstants.InOutLinkType.STYLESHEET;
                break;
              case "alternate":
                LinkType = MacroscopeConstants.InOutLinkType.ALTERNATE;
                break;
              case "author":
                LinkType = MacroscopeConstants.InOutLinkType.AUTHOR;
                break;
              default:
                break;
            }

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: LinkType,
                Follow: Follow
              );

              if ( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // FRAME element links -----------------------------------------------//
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/frame

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//frame[@src]" );

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = null;
            string LinkUrlAbs = null;
            MacroscopeLink Outlink = null;

            LinkUrl = LinkNode.GetAttributeValue( "src", null );
            LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
              BaseHref: this.GetBaseHref(),
              BaseUrl: this.DocUrl,
              Url: LinkUrl
            );

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.FRAME,
                Follow: true
              );

              if ( Outlink != null )
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

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = null;
            string LinkUrlAbs = null;
            MacroscopeLink Outlink = null;

            LinkUrl = LinkNode.GetAttributeValue( "src", null );
            LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
              BaseHref: this.GetBaseHref(),
              BaseUrl: this.DocUrl,
              Url: LinkUrl
            );

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.IFRAME,
                Follow: true
              );

              if ( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // MAP HREF links

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//map/area[@href]" );

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = null;
            string LinkUrlAbs = null;
            MacroscopeLink Outlink = null;

            LinkUrl = LinkNode.GetAttributeValue( "href", null );
            LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
              BaseHref: this.GetBaseHref(),
              BaseUrl: this.DocUrl,
              Url: LinkUrl
            );

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.MAP,
                Follow: true
              );

              if ( Outlink != null )
              {
                Outlink.SetRawTargetUrl( LinkUrl );
              }

            }

          }

        }

      } // -------------------------------------------------------------------//

      { // IMG element links -------------------------------------------------// 

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//img[@src]" );

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = null;
            string LinkUrlAbs = null;
            string LinkTitle = null;
            string LinkAltText = null;
            MacroscopeLink Outlink = null;

            LinkUrl = LinkNode.GetAttributeValue( "src", null );
            LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LinkUrl );
            LinkTitle = LinkNode.GetAttributeValue( "title", "" );
            LinkAltText = LinkNode.GetAttributeValue( "alt", "" );

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                Follow: true
              );

              if ( Outlink != null )
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

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LinkUrl );
            MacroscopeLink Outlink = null;

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.SCRIPT,
                Follow: true
              );

              if ( Outlink != null )
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

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LinkUrl );
            MacroscopeLink Outlink = null;

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.AUDIO,
                Follow: true
              );

              if ( Outlink != null )
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

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LinkUrl );
            MacroscopeLink Outlink = null;

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.VIDEO,
                Follow: true
              );

              if ( Outlink != null )
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

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "src", null );
            string LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LinkUrl );
            MacroscopeLink Outlink = null;

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.EMBED,
                Follow: true
              );

              if ( Outlink != null )
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

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          foreach ( HtmlNode LinkNode in NodeCollection )
          {

            string LinkUrl = LinkNode.GetAttributeValue( "data", null );
            string LinkUrlAbs = MacroscopeHttpUrlUtils.MakeUrlAbsolute( BaseHref: this.GetBaseHref(), BaseUrl: this.DocUrl, Url: LinkUrl );
            MacroscopeLink Outlink = null;

            if ( LinkUrlAbs != null )
            {

              Outlink = this.AddHtmlOutlink(
                AbsoluteUrl: LinkUrlAbs,
                LinkType: MacroscopeConstants.InOutLinkType.OBJECT,
                Follow: true
              );

              if ( Outlink != null )
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

      if ( HtmlDoc != null )
      {

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//style" );

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          this.DebugMsg( string.Format( "ProcessHtmlInlineCssLinks: {0}", NodeCollection.Count ) );

          foreach ( HtmlNode Node in NodeCollection )
          {

            string CssText = Node.InnerText;

            if ( !string.IsNullOrEmpty( CssText ) )
            {

              ExCSS.Parser ExCssParser = null;
              ExCSS.StyleSheet ExCssStylesheet = null;

              this.DebugMsg( string.Format( "ProcessHtmlInlineCssLinks: {0}", CssText ) );

              try
              {
                ExCssParser = new ExCSS.Parser();
                ExCssStylesheet = ExCssParser.Parse( CssText );
                this.ProcessCssOutlinks( ExCssStylesheet );
              }
              catch ( Exception ex )
              {
                this.DebugMsg( string.Format( "ProcessHtmlInlineCssLinks: {0}", ex.Message ) );
                this.AddRemark( "ProcessHtmlInlineCssLinks", ex.Message );
              }

            }

          }

        }

      }

      return;

    }

    /** -------------------------------------------------------------------- **/

    private void ProcessHtmlAttributeCssLinks ( HtmlDocument HtmlDoc )
    {

      if ( HtmlDoc != null )
      {

        HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//*[@style]" );

        if ( ( NodeCollection != null ) && ( NodeCollection.Count > 0 ) )
        {

          this.DebugMsg( string.Format( "ProcessHtmlAttributeCssLinks: {0}", NodeCollection.Count ) );

          foreach ( HtmlNode Node in NodeCollection )
          {

            string CssText = Node.GetAttributeValue( "style", "" );

            if ( !string.IsNullOrEmpty( CssText ) )
            {

              ExCSS.Parser ExCssParser = null;
              ExCSS.StyleSheet ExCssStylesheet = null;

              this.DebugMsg( string.Format( "ProcessHtmlAttributeCssLinks: {0}", CssText ) );

              try
              {
                ExCssParser = new ExCSS.Parser();
                ExCssStylesheet = ExCssParser.Parse( CssText );
                this.ProcessCssOutlinks( ExCssStylesheet );
              }
              catch ( Exception ex )
              {
                this.DebugMsg( string.Format( "ProcessHtmlAttributeCssLinks: {0}", ex.Message ) );
                this.AddRemark( "ProcessHtmlAttributeCssLinks", ex.Message );
              }

            }

          }

        }

      }

      return;

    }

    /**************************************************************************/

    private MacroscopeLink AddHtmlOutlink (
      string AbsoluteUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      bool Follow
    )
    {

      MacroscopeLink OutLink = null;
      bool Proceed = true;

      if ( !MacroscopePreferencesManager.GetCheckExternalLinks() )
      {

        if ( this.DocCollection != null )
        {

          MacroscopeAllowedHosts AllowedHosts = this.DocCollection.GetAllowedHosts();

          if ( AllowedHosts != null )
          {

            if ( !AllowedHosts.IsAllowedFromUrl( Url: AbsoluteUrl ) )
            {
              Proceed = false;
            }

          }

        }

      }

      switch ( LinkType )
      {
        case MacroscopeConstants.InOutLinkType.CANONICAL:
          if ( !MacroscopePreferencesManager.GetFollowCanonicalLinks() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.ALTERNATE:
          if ( !MacroscopePreferencesManager.GetFollowAlternateLinks() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.STYLESHEET:
          if ( !MacroscopePreferencesManager.GetFetchStylesheets() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.SCRIPT:
          if ( !MacroscopePreferencesManager.GetFetchJavascripts() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.IMAGE:
          if ( !MacroscopePreferencesManager.GetFetchImages() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.AUDIO:
          if ( !MacroscopePreferencesManager.GetFetchAudio() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.VIDEO:
          if ( !MacroscopePreferencesManager.GetFetchVideo() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.EMBED:
          if ( !MacroscopePreferencesManager.GetFetchBinaries() )
          {
            Proceed = false;
          }
          break;
        case MacroscopeConstants.InOutLinkType.OBJECT:
          if ( !MacroscopePreferencesManager.GetFetchBinaries() )
          {
            Proceed = false;
          }
          break;
      }

      if ( Proceed )
      {

        OutLink = new MacroscopeLink(
          SourceUrl: this.GetUrl(),
          TargetUrl: AbsoluteUrl,
          LinkType: LinkType,
          Follow: Follow
        );

        this.Outlinks.Add( OutLink );

      }

      return ( OutLink );

    }

    /**************************************************************************/

    /// Reference: https://support.google.com/webmasters/answer/189077

    private void ExtractHrefLangAlternates ( HtmlDocument HtmlDoc )
    {

      HtmlNodeCollection NodeList = HtmlDoc.DocumentNode.SelectNodes( "//link[@rel='alternate']" );

      if ( NodeList != null )
      {

        foreach ( HtmlNode LinkNode in NodeList )
        {

          MacroscopeHrefLang HrefLangAlternate = null;
          string Rel = LinkNode.GetAttributeValue( "rel", "" );
          string HrefLangLocale = LinkNode.GetAttributeValue( "hreflang", "" );
          string Href = LinkNode.GetAttributeValue( "href", "" );

          if ( string.IsNullOrEmpty( HrefLangLocale ) || string.IsNullOrWhiteSpace( HrefLangLocale ) )
          {
            continue;
          }
          else
          {

            if ( this.DocUrl == Href )
            {
              HrefLangLocale = this.Locale;
            }

            this.DebugMsg( string.Format( "HREFLANG: {0}, {1}", HrefLangLocale, Href ) );

            if ( MacroscopePreferencesManager.GetCheckHreflangs() )
            {

              if ( this.DocCollection != null )
              {

                MacroscopeJobMaster JobMaster = null;
                MacroscopeIncludeExcludeUrls IncludeExcludeUrls = null;

                JobMaster = this.DocCollection.GetJobMaster();
                IncludeExcludeUrls = JobMaster.GetIncludeExcludeUrls();

                if ( IncludeExcludeUrls != null )
                {

                  IncludeExcludeUrls.AddExplicitIncludeUrl( Url: Href );

                  this.DocCollection.GetAllowedHosts().AddFromUrl( Url: Href );

                  this.DocCollection.GetJobMaster().AddUrlQueueItem( Url: Href );

                }

              }

            }

            HrefLangAlternate = new MacroscopeHrefLang( JobMaster: this.DocCollection.GetJobMaster(), Locale: HrefLangLocale, Url: Href );

            if ( HrefLangAlternate != null )
            {
              this.HrefLang[ HrefLangLocale ] = HrefLangAlternate;
            }

          }

        }

      }

    }

    /**************************************************************************/

    private void AnalyzeHrefLangAlternates ( HtmlDocument HtmlDoc )
    {

      HtmlNodeCollection NodeList = HtmlDoc.DocumentNode.SelectNodes( "//link[@rel='alternate']" );
      string DocumentLocale = this.GetLocale();
      bool SelfReferentialLocalePresent = false;

      if ( NodeList != null )
      {

        foreach ( HtmlNode LinkNode in NodeList )
        {

          string HrefLangLocale = LinkNode.GetAttributeValue( "hreflang", "" );

          if ( string.IsNullOrEmpty( HrefLangLocale ) || string.IsNullOrWhiteSpace( HrefLangLocale ) )
          {
            continue;
          }
          else
          {

            this.DebugMsg( string.Format( "AnalyzeHrefLangAlternates: {0}, {1}", DocumentLocale, HrefLangLocale ) );

            if ( HrefLangLocale == DocumentLocale )
            {
              SelfReferentialLocalePresent = true;
              break;
            }

          }

        }

      }

      if ( !SelfReferentialLocalePresent )
      {
        this.AddRemark( "MISSING_HREFLANG", @"A self-referential HrefLang element appears to be missing from this page." );
      }

    }

    /**************************************************************************/

    private void ExtractHtmlHeadings ( HtmlDocument HtmlDoc )
    {

      for ( ushort HeadingLevel = 1 ; HeadingLevel <= 6 ; HeadingLevel++ )
      {

        HtmlNodeCollection NodeCollection;

        NodeCollection = HtmlDoc.DocumentNode.SelectNodes(
          string.Format( "//h{0}", HeadingLevel )
        );

        if ( NodeCollection != null )
        {

          foreach ( HtmlNode Node in NodeCollection )
          {

            string HeadingText = Node.InnerText;

            if ( HeadingText != null )
            {
              this.AddHeading( HeadingLevel, HeadingText );
            }

          }

        }

      }

    }

    /** Process Document Text *************************************************/

    private string ProcessHtmlDocumentText ( HtmlDocument HtmlDoc )
    {

      List<string> ExtractedText = new List<string>( 16 );
      string TextProcessed = "";

      if ( HtmlDoc != null )
      {

        this.StripDisregardedNodes( HtmlDoc: HtmlDoc );

        this.StripNonTextNodes( HtmlDoc: HtmlDoc );

        ExtractedText = this.GetNodeText( Node: HtmlDoc.DocumentNode );

        TextProcessed = string.Join( "", ExtractedText );

        //TextProcessed = Regex.Replace( TextProcessed, "<!--.*?-->", "", RegexOptions.Singleline );

      }

      return ( TextProcessed );

    }

    /** Process Body Text *****************************************************/

    private string ProcessHtmlBodyText ( HtmlDocument HtmlDoc )
    {

      HtmlNode BodyNode = HtmlDoc.DocumentNode.SelectSingleNode( "//body" );
      List<string> ExtractedText = new List<string>( 16 );
      string TextProcessed = "";

      if ( BodyNode != null )
      {

        this.StripNonTextNodes( HtmlDoc: HtmlDoc );

        ExtractedText = this.GetNodeText( Node: BodyNode );

        TextProcessed = string.Join( "", ExtractedText );

        //TextProcessed = Regex.Replace( TextProcessed, "<!--.*?-->", "", RegexOptions.Singleline );

      }

      return ( TextProcessed );

    }

    /** Recursively Extract Text From HTML Node *******************************/

    public List<string> GetNodeText ( HtmlNode Node )
    {

      List<string> ExtractedText = new List<string>( 16 );

      HtmlNodeCollection NodeCollection = Node.SelectNodes( "//text()" );

      if ( NodeCollection != null )
      {

        foreach ( HtmlNode NodeText in NodeCollection )
        {

          string NodeTextString = NodeText.InnerText;

          if ( !string.IsNullOrEmpty( NodeTextString ) )
          {

            NodeTextString = Regex.Replace( NodeTextString, "<[^<>]+?>", "" );

            if ( !string.IsNullOrEmpty( NodeTextString ) )
            {

              ExtractedText.Add( NodeTextString );
            }

          }

        }

      }

      return ( ExtractedText );

    }

    /**************************************************************************/

    private void StripNodes ( HtmlNodeCollection NodeCollection )
    {
      if ( NodeCollection != null )
      {
        List<HtmlNode> NodesToRemove = new List<HtmlNode>();
        foreach ( HtmlNode Node in NodeCollection )
        {
          NodesToRemove.Add( Node );
        }
        for ( int i = 0 ; i < NodesToRemove.Count ; i++ )
        {
          NodesToRemove[ i ].Remove();
        }
      }
      return;
    }

    /**************************************************************************/

    private void StripDisregardedNodes ( HtmlDocument HtmlDoc )
    {

      if ( MacroscopePreferencesManager.GetDisregardHtml5ElementNav() )
      {
        this.StripNodes( NodeCollection: HtmlDoc.DocumentNode.SelectNodes( "(//nav)" ) );
      }

      if ( MacroscopePreferencesManager.GetDisregardHtml5ElementHeader() )
      {
        this.StripNodes( NodeCollection: HtmlDoc.DocumentNode.SelectNodes( "(//header)" ) );
      }

      if ( MacroscopePreferencesManager.GetDisregardHtml5ElementFooter() )
      {
        this.StripNodes( NodeCollection: HtmlDoc.DocumentNode.SelectNodes( "(//footer)" ) );
      }

      return;

    }

    /**************************************************************************/

    private void StripNonTextNodes ( HtmlDocument HtmlDoc )
    {
      this.StripNodes( NodeCollection: HtmlDoc.DocumentNode.SelectNodes( "(//script|//style|//comment())" ) );
      return;
    }

    /** Extract Email Addresses ***********************************************/

    private void ExtractHtmlEmailAddresses ( HtmlDocument HtmlDoc )
    {

      HtmlNodeCollection NodeCollection = HtmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

      if ( NodeCollection != null )
      {

        foreach ( HtmlNode LinkNode in NodeCollection )
        {

          string LinkUrl = LinkNode.GetAttributeValue( "href", null );

          if ( LinkUrl != null )
          {

            if ( Regex.IsMatch( LinkUrl, "^mailto:" ) )
            {

              MatchCollection reMatches = Regex.Matches( LinkUrl, "^mailto:([^?]+)" );

              foreach ( Match reMatch in reMatches )
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

      if ( NodeCollection != null )
      {

        foreach ( HtmlNode LinkNode in NodeCollection )
        {

          string LinkUrl = LinkNode.GetAttributeValue( "href", null );

          if ( LinkUrl != null )
          {

            if ( Regex.IsMatch( LinkUrl, "^tel:" ) )
            {

              MatchCollection reMatches = Regex.Matches( LinkUrl, "^tel:(.+)" );

              foreach ( Match reMatch in reMatches )
              {
                this.AddTelephoneNumber( TelephoneNumber: reMatch.Groups[ 1 ].Value );
              }

            }

          }

        }

      }

    }

    /** Process Custom Filtered ***********************************************/

    private void ProcessHtmlCustomFiltered (
      MacroscopeCustomFilters CustomFilter,
      string HtmlText
    )
    {

      Dictionary<string, MacroscopeConstants.TextPresence> Analyzed;

      Analyzed = CustomFilter.AnalyzeText( Text: HtmlText );

      foreach ( string Key in Analyzed.Keys )
      {
        this.SetCustomFiltered( Text: Key, Presence: Analyzed[ Key ] );
      }

    }

    /** Process Data Extractors ***********************************************/

    private void ProcessHtmlDataExtractors ( string HtmlText )
    {

      MacroscopeJobMaster JobMaster = this.DocCollection.GetJobMaster();

      {

        MacroscopeDataExtractorCssSelectors DataExtractor = JobMaster.GetDataExtractorCssSelectors();

        if ( ( DataExtractor != null ) && ( DataExtractor.IsEnabled() ) )
        {
          this.ProcessHtmlDataExtractorCssSelectors(
            DataExtractor: DataExtractor,
            HtmlText: HtmlText
          );
        }

      }

      {

        MacroscopeDataExtractorRegexes DataExtractor = JobMaster.GetDataExtractorRegexes();

        if ( ( DataExtractor != null ) && ( DataExtractor.IsEnabled() ) )
        {
          this.ProcessHtmlDataExtractorRegexes(
            DataExtractor: DataExtractor,
            HtmlText: HtmlText
          );
        }

      }

      {

        MacroscopeDataExtractorXpaths DataExtractor = JobMaster.GetDataExtractorXpaths();

        if ( ( DataExtractor != null ) && ( DataExtractor.IsEnabled() ) )
        {
          this.ProcessHtmlDataExtractorXpaths(
            DataExtractor: DataExtractor,
            HtmlText: HtmlText
          );
        }

      }

    }

    /** -------------------------------------------------------------------- **/

    private void ProcessHtmlDataExtractorCssSelectors (
      MacroscopeDataExtractorCssSelectors DataExtractor,
      string HtmlText
    )
    {

      List<KeyValuePair<string, string>> Analyzed = DataExtractor.AnalyzeHtml( Html: HtmlText );

      foreach ( KeyValuePair<string, string> Pair in Analyzed )
      {
        this.SetDataExtractedCssSelectors(
          Label: Pair.Key,
          Text: Pair.Value
        );
      }

    }

    /** -------------------------------------------------------------------- **/

    private void ProcessHtmlDataExtractorRegexes (
      MacroscopeDataExtractorRegexes DataExtractor,
      string HtmlText
    )
    {

      List<KeyValuePair<string, string>> Analyzed = DataExtractor.AnalyzeText( Text: HtmlText );

      foreach ( KeyValuePair<string, string> Pair in Analyzed )
      {
        this.SetDataExtractedRegexes(
          Label: Pair.Key,
          Text: Pair.Value
        );
      }

    }

    /** -------------------------------------------------------------------- **/

    private void ProcessHtmlDataExtractorXpaths (
      MacroscopeDataExtractorXpaths DataExtractor,
      string HtmlText
    )
    {

      List<KeyValuePair<string, string>> Analyzed = null;

      Analyzed = DataExtractor.AnalyzeHtml( Html: HtmlText );

      if ( Analyzed != null )
      {

        foreach ( KeyValuePair<string, string> Pair in Analyzed )
        {
          this.SetDataExtractedXpaths(
            Label: Pair.Key,
            Text: Pair.Value
          );
        }

      }

    }

    /** Sniff Charset *********************************************************/

#if DEBUG

    private async Task<Encoding> HtmlSniffCharset ()
    {

      // TODO: Make this optional in preferences

      // TODO: Implement add more encodings detectors

      Encoding EncSniffed = Encoding.UTF8;


      string HtmlData = await this.FetchHtmlFile( HtmlUri: this.GetUri() );
      byte[] HtmlBytes = Encoding.ASCII.GetBytes( HtmlData );

      if ( ( HtmlBytes.Length > 2 ) && HtmlBytes[ 0 ].Equals( 0xFE ) && HtmlBytes[ 1 ].Equals( 0xFF ) ) // UTF-8 BOM: Big Endian: FE FF
      {
        EncSniffed = Encoding.UTF8;
      }
      else
      if ( ( HtmlBytes.Length > 2 ) && HtmlBytes[ 0 ].Equals( 0xFF ) && HtmlBytes[ 1 ].Equals( 0xFE ) ) // UTF-8 BOM: Little Endian: FF FE
      {
        EncSniffed = Encoding.UTF8;
      }
      else
      {

        if ( Regex.IsMatch( HtmlData.ToLower(), @"<meta[^<>]+charset=""[^""]*utf-8[^""]*""[^<>]*>" ) )
        {
          EncSniffed = Encoding.UTF8;
        }
        else
        if ( Regex.IsMatch( HtmlData.ToLower(), @"<meta[^<>]+content=""[^""]*text/html;\s*charset=utf-8[^""]*""[^<>]*>" ) )
        {
          EncSniffed = Encoding.UTF8;
        }
        else
        {
          EncSniffed = Encoding.UTF8;
        }

      }


      return ( EncSniffed );

    }

#endif

    /** Fetch HTML File *******************************************************/

    private async Task<string> FetchHtmlFile ( Uri HtmlUri )
    {

      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      bool Proceed = false;
      string HtmlData = "";
      string RawData = "";

      try
      {

        Response = await Client.Get(
          HtmlUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback,
          MacroscopeHttpTwoClient.DecodeResponseContentAs.STRING
        );

        if ( Response != null )
        {
          Proceed = true;
        }

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", HtmlUri.ToString() ) );
        this.AddRemark( "FETCHHTMLFILE", string.Format( "There was an error downloading the HTML content: {0}", ex.Message ) );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "Exception: {0}", HtmlUri.ToString() ) );
        this.AddRemark( "FETCHHTMLFILE", string.Format( "There was an error downloading the HTML content: {0}", ex.Message ) );
      }

      if ( ( Proceed ) && ( Response != null ) )
      {

        try
        {
          RawData = Response.GetContentAsString();
        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
          RawData = "";
        }

        if ( !string.IsNullOrEmpty( RawData ) )
        {
          HtmlData = RawData;
        }

      }

      return ( HtmlData );

    }

    /**************************************************************************/

  }

}
