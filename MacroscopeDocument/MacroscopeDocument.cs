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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// MacroscopeDocument is a representation of the document found at a crawled URL.
  /// </summary>

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private Boolean IsDirty;

    private string Url;
    private int Timeout;

    private string Checksum;
    private string Etag;

    private Boolean IsExternal;

    private Boolean IsRedirect;
    private string UrlRedirectFrom;
    private string UrlRedirectTo;

    private string Scheme;
    private string Hostname;
    private int Port;
    private string Path;
    private string Fragment;
    private string QueryString;

    private string RawHttpStatusLine;
    private string RawHttpHeaders;

    private Boolean HypertextStrictTransportPolicy;
    private Boolean IsSecureUrl;
    private List<string> InSecureLinks;
    
    private int StatusCode;
    private string ErrorCondition;
    private long ContentLength;

    private string MimeType;
    private MacroscopeConstants.DocumentType DocumentType;

    private Boolean IsCompressed;
    private string CompressionMethod;
    private string ContentEncoding;

    private string Locale;
    private Encoding CharSet;
    private string CharacterSet;

    private long Duration;
    private Boolean WasDownloaded;

    private DateTime DateServer;
    private DateTime DateModified;

    private string Canonical;
    private Dictionary<string,MacroscopeHrefLang> HrefLang;

    // Outbound links to pages and linked assets to follow
    private Dictionary<string,MacroscopeOutlink> Outlinks;

    // Inbound links from other pages in the scanned collection
    private MacroscopeHyperlinksIn HyperlinksIn;

    // Outbound hypertext links
    private MacroscopeHyperlinksOut HyperlinksOut;

    private Dictionary<string,string> EmailAddresses;
    private Dictionary<string,string> TelephoneNumbers;

    private MacroscopeAnalyzePageTitles AnalyzePageTitles;

    private string Title;
    private int TitlePixelWidth;
    private string Description;
    private string Keywords;

    private Dictionary<ushort,List<string>> Headings;

    private string BodyText;
    private List<Dictionary<string,int>> DeepKeywordAnalysis;

    private int Depth;

    // Delegate Functions
    private delegate void TimeDuration(Action ProcessMethod);

    /**************************************************************************/

    public MacroscopeDocument ( string sUrl )
    {

      this.SuppressDebugMsg = false;

      this.IsDirty = true;

      this.Url = sUrl;
      this.Timeout = MacroscopePreferencesManager.GetRequestTimeout() * 1000;

      this.Checksum = "";
      this.Etag = "";

      this.IsExternal = false;

      this.IsRedirect = false;
      this.UrlRedirectFrom = "";
      this.UrlRedirectTo = "";

      this.RawHttpStatusLine = "";
      this.RawHttpHeaders = "";

      this.Scheme = "";
      this.Hostname = "";
      this.Port = 80;
      this.Path = "";
      this.Fragment = "";
      this.QueryString = "";

      this.HypertextStrictTransportPolicy = false;
      this.IsSecureUrl = false;
      this.InSecureLinks = new List<string> ( 128 );
    
      this.StatusCode = 0;
      this.ErrorCondition = "";
      this.ContentLength = 0;

      this.MimeType = "";
      this.DocumentType = MacroscopeConstants.DocumentType.BINARY;

      this.ContentEncoding = "";

      this.Locale = null;
      this.CharSet = null;
      this.CharacterSet = null;

      this.Duration = 0;
      this.WasDownloaded = false;

      this.DateServer = new DateTime ();
      this.DateModified = new DateTime ();

      this.Canonical = "";
      this.HrefLang = new Dictionary<string,MacroscopeHrefLang> ( 1024 );

      this.Outlinks = new Dictionary<string,MacroscopeOutlink> ( 128 );
      this.HyperlinksIn = new MacroscopeHyperlinksIn ();
      this.HyperlinksOut = new MacroscopeHyperlinksOut ();

      this.EmailAddresses = new Dictionary<string,string> ( 256 );
      this.TelephoneNumbers = new Dictionary<string,string> ( 256 );

      this.AnalyzePageTitles = new MacroscopeAnalyzePageTitles ();

      this.Title = "";
      this.TitlePixelWidth = 0;
      this.Description = "";
      this.Keywords = "";

      this.Headings = new Dictionary<ushort,List<string>> () {
        {
          1,
          new List<string> ( 16 )
        }, {
          2,
          new List<string> ( 16 )
        },
        {
          3,
          new List<string> ( 16 )
        }, {
          4,
          new List<string> ( 16 )
        },
        {
          5,
          new List<string> ( 16 )
        }, {
          6,
          new List<string> ( 16 )
        }
      };

      this.BodyText = "";
      
      this.DeepKeywordAnalysis = new List<Dictionary<string,int>> ( 4 );
      for( int i = 0 ; i <= 3 ; i++ )
      {
        this.DeepKeywordAnalysis.Add( new Dictionary<string,int> ( 256 ) );
      }

      this.Depth = MacroscopeUrlTools.FindUrlDepth( Url );

    }

    /** Delegates *************************************************************/

    private TimeDuration GetTimeDurationDelegate ()
    {

      TimeDuration fTimeDuration = delegate( Action ProcessMethod )
      {
        Stopwatch swDuration = new Stopwatch ();
        long lDuration;
        swDuration.Start();
        try
        {
          ProcessMethod();
        }
        catch( MacroscopeDocumentException ex )
        {
          DebugMsg( string.Format( "fTimeDuration: {0}", ex.Message ) );
        }
        swDuration.Stop();
        lDuration = swDuration.ElapsedMilliseconds;
        if( lDuration > 0 )
        {
          this.Duration = lDuration;
        }
        else
        {
          this.Duration = 0;
        }
        DebugMsg( string.Format( "DURATION: {0} :: {1}", lDuration, this.Duration ) );
      };

      return( fTimeDuration );

    }

    /** Dirty Flag ************************************************************/

    public void SetIsDirty ()
    {
      this.IsDirty = true;
    }

    void ClearIsDirty ()
    {
      this.IsDirty = false;
    }

    public Boolean GetIsDirty ()
    {
      return( this.IsDirty );
    }

    /** Host Details **********************************************************/

    public string GetUrl ()
    {
      return( this.Url );
    }

    public string GetScheme ()
    {
      return( this.Scheme );
    }

    public string GetHostname ()
    {
      return( this.Hostname );
    }

    public int GetPort ()
    {
      return( this.Port );
    }

    public string GetPath ()
    {
      return( this.Path );
    }

    public string GetFragment ()
    {
      return( this.Fragment );
    }

    public string GetQueryString ()
    {
      return( this.QueryString );
    }

    /** Secure URLs ***********************************************************/

    public void SetIsSecureUrl ( Boolean bState )
    {
      this.IsSecureUrl = bState;
    }

    public Boolean GetIsSecureUrl ()
    {
      return( this.IsSecureUrl );
    }

    public void AddInsecureLink ( string sUrl )
    {
      this.InSecureLinks.Add( sUrl );
    }
    
    public List<string> GetInsecureLinks ()
    {
      List<string> DocList = new List<string> ( 128 );
      lock( this.InSecureLinks )
      {
        int iCount = this.InSecureLinks.Count;
        if( iCount > 0 )
        {
          for( int i = 0 ; i < iCount ; i++ )
          {
            DocList.Add( this.InSecureLinks[ i ] );
          }
        }
      }
      return( DocList );
    }

    /** Checksum Value ********************************************************/

    public void SetChecksum ( string ChecksumValue )
    {
      this.Checksum = this.GenerateChecksum( ChecksumValue );
    }

    public string GetChecksum ()
    {
      return( this.Checksum );
    }

    private string GenerateChecksum ( string sData )
    {
      MD5 Md5Digest = MD5.Create();
      byte [] BytesIn = Encoding.UTF8.GetBytes( sData );
      byte [] Hashed = Md5Digest.ComputeHash( BytesIn );
      StringBuilder sbString = new StringBuilder ();
      for( int i = 0 ; i < Hashed.Length ; i++ )
      {
        sbString.Append( Hashed[ i ].ToString( "X2" ) );
      }
      string sChecksum = sbString.ToString();
      return( sChecksum );
    }

    /** Etag Value ************************************************************/
    
    public void SetEtag ( string EtagValue )
    {
      this.Etag = EtagValue;
      this.Checksum = EtagValue;
    }

    public string GetEtag ()
    {
      return( this.Etag );
    }

    /** Is External Flag ******************************************************/

    public void SetIsExternal ( Boolean bState )
    {
      this.IsExternal = bState;
    }

    public Boolean GetIsExternal ()
    {
      return( this.IsExternal );
    }

    /** Is Redirect Flag ******************************************************/

    public Boolean GetIsRedirect ()
    {
      return( this.IsRedirect );
    }

    public string GetUrlRedirectFrom ()
    {
      return( this.UrlRedirectFrom );
    }

    public string GetUrlRedirectTo ()
    {
      return( this.UrlRedirectTo );
    }

    /**************************************************************************/

    public void SetStatusCode ( HttpStatusCode Status )
    {
      this.StatusCode = ( int )Status;
    }

    public int GetStatusCode ()
    {
      return( this.StatusCode );
    }

    /**************************************************************************/

    public void SetErrorCondition ( string sErrorCondtion )
    {
      this.ErrorCondition = sErrorCondtion;
    }
    
    public string GetErrorCondition ()
    {
      return( this.ErrorCondition );
    }

    /** HTTP Headers **********************************************************/

    public string GetHttpStatusLineAsText ()
    {
      return( this.RawHttpStatusLine );
    }

    public string GetHttpHeadersAsText ()
    {
      return( this.RawHttpHeaders );
    }

    /**************************************************************************/

    public string GetMimeType ()
    {
      string sMimeType = null;
      if( this.MimeType == null )
      {
        sMimeType = "";
      }
      else
      {
        MatchCollection matches = Regex.Matches( this.MimeType, "^([^\\s;/]+)/([^\\s;/]+)" );
        foreach( Match match in matches )
        {
          sMimeType = String.Format( "{0}/{1}", match.Groups[ 1 ].Value, match.Groups[ 2 ].Value );
        }
        if( sMimeType == null )
        {
          sMimeType = this.MimeType;
        }
      }
      return( sMimeType );
    }

    /** Document Type Methods *************************************************/

    public MacroscopeConstants.DocumentType GetDocumentType ()
    {
      return( this.DocumentType );
    }

    public void SetIsBinary ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.BINARY;
    }

    public Boolean GetIsBinary ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.BINARY )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsHtml ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.HTML;
    }

    public Boolean GetIsHtml ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.HTML )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsCss ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.CSS;
    }

    public Boolean GetIsCss ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.CSS )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsJavascript ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.JAVASCRIPT;
    }


    public Boolean GetIsJavascript ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.JAVASCRIPT )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsImage ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.IMAGE;
    }

    public Boolean GetIsImage ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.IMAGE )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsPdf ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.PDF;
    }

    public Boolean GetIsPdf ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.PDF )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsAudio ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.AUDIO;
    }

    public Boolean GetIsAudio ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.AUDIO )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsVideo ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.VIDEO;
    }

    public Boolean GetIsVideo ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.VIDEO )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsXml ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.XML;
    }

    public Boolean GetIsXml ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.XML )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    public void SetIsSitemapXml ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.SITEMAPXML;
    }

    public Boolean GetIsSitemapXml ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.SITEMAPXML )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }

    /** Compression ***********************************************************/

    public Boolean GetIsCompressed ()
    {
      return( this.IsCompressed );
    }

    public string GetCompressionMethod ()
    {
      return( this.CompressionMethod );
    }

    /** Content Length ********************************************************/

    public long GetContentLength ()
    {
      return( this.ContentLength );
    }

    /** Language/Locale *******************************************************/

    public string GetLang ()
    {
      return( this.Locale );
    }

    public string GetLocale ()
    {
      return( this.Locale );
    }

    /** Character Set *********************************************************/

    public string GetCharacterSet ()
    {
      return( this.CharacterSet );
    }

    public void SetCharacterSet ( string sCharSet )
    {
      this.CharacterSet = sCharSet;
    }

    /** Canonical *************************************************************/

    public string GetCanonical ()
    {
      return( this.Canonical );
    }

    public void SetCanonical ( string sCanonical )
    {
      this.Canonical = sCanonical;
    }

    /** Dates *****************************************************************/

    public string GetDateServer ()
    {
      return( this.DateServer.ToShortDateString() );
    }

    public string GetDateModified ()
    {
      return( this.DateModified.ToShortDateString() );
    }

    /** Outlinks **************************************************************/

    public Dictionary<string,MacroscopeOutlink> GetOutlinks ()
    {
      return( this.Outlinks );
    }

    public IEnumerable IterateOutlinks ()
    {
      lock( this.Outlinks )
      {
        foreach( string sUrl in this.Outlinks.Keys )
        {
          yield return sUrl;
        }
      }
    }

    public MacroscopeOutlink GetOutlink ( string sUrl )
    {
      MacroscopeOutlink Outlink = null;
      if( this.Outlinks.ContainsKey( sUrl ) )
      {
        Outlink = this.Outlinks[ sUrl ];
      }
      return( Outlink );
    }

    public int CountOutlinks ()
    {
      int iCount = this.GetOutlinks().Count;
      return( iCount );
    }

    private void AddDocumentOutlink (
      string sRawUrl,
      string sAbsoluteUrl,
      MacroscopeConstants.OutlinkType sType,
      Boolean bFollow
    )
    {

      MacroscopeOutlink OutLink = new MacroscopeOutlink ( sRawUrl, sAbsoluteUrl, sType, bFollow );

      if( this.Outlinks.ContainsKey( sRawUrl ) )
      {
        this.Outlinks.Remove( sRawUrl );
        this.Outlinks.Add( sRawUrl, OutLink );
      }
      else
      {
        this.Outlinks.Add( sRawUrl, OutLink );
      }

    }

    /**************************************************************************/

    public MacroscopeHyperlinksIn GetHyperlinksIn ()
    {
      return( this.HyperlinksIn );
    }

    /** Hyperlinks In *********************************************************/

    public void AddHyperlinkIn (
      MacroscopeConstants.HyperlinkType hlType,
      string sMethod,
      string sUrlOrigin,
      string sUrlTarget,
      string sLinkText,
      string sAltText
    )
    {
      this.HyperlinksIn.Add(
        hlType,
        sMethod,
        sUrlOrigin,
        sUrlTarget,
        sLinkText,
        sAltText
      );
    }

    public void ClearHyperlinksIn ()
    {
      this.HyperlinksIn.Clear();
    }

    public int CountHyperlinksIn ()
    {
      return( this.HyperlinksIn.Count() );
    }

    /** Hyperlinks Out ********************************************************/

    public MacroscopeHyperlinksOut GetHyperlinksOut ()
    {
      return( this.HyperlinksOut );
    }

    public int CountHyperlinksOut ()
    {
      int iCount = this.HyperlinksOut.Count();
      return( iCount );
    }

    /** Email Addresses *******************************************************/

    public void AddEmailAddress ( string sString )
    {
      DebugMsg( string.Format( "AddEmailAddress: {0}", sString ) );
      if( this.EmailAddresses.ContainsKey( sString ) )
      {
        this.EmailAddresses[ sString ] = this.GetUrl();
      }
      else
      {
        this.EmailAddresses.Add( sString, this.GetUrl() );
      }
    }

    public Dictionary<string,string> GetEmailAddresses ()
    {
      return( this.EmailAddresses );
    }

    /** Telephone Numbers *****************************************************/

    public void AddTelephoneNumber ( string sString )
    {
      DebugMsg( string.Format( "AddTelephoneNumber: {0}", sString ) );
      if( this.TelephoneNumbers.ContainsKey( sString ) )
      {
        this.TelephoneNumbers[ sString ] = this.GetUrl();
      }
      else
      {
        this.TelephoneNumbers.Add( sString, this.GetUrl() );
      }
    }

    public Dictionary<string,string> GetTelephoneNumbers ()
    {
      return( this.TelephoneNumbers );
    }

    /** Title *****************************************************************/

    public string GetTitle ()
    {
      string sValue;
      if( this.Title != null )
      {
        sValue = this.Title;
      }
      else
      {
        sValue = "";
      }
      return( sValue );
    }

    public int GetTitleLength ()
    {
      return( this.GetTitle().Length );
    }

    public int GetTitlePixelWidth ()
    {
      return( this.TitlePixelWidth );
    }

    /** Description ***********************************************************/

    public string GetDescription ()
    {
      string sValue;
      if( this.Description != null )
      {
        sValue = this.Description;
      }
      else
      {
        sValue = "";
      }
      return( sValue );
    }

    public int GetDescriptionLength ()
    {
      return( this.GetDescription().Length );
    }

    /** Keywords **************************************************************/

    public string GetKeywords ()
    {
      string sValue;
      if( this.Keywords != null )
      {
        sValue = this.Keywords;
      }
      else
      {
        sValue = "";
      }
      return( sValue );
    }

    public int GetKeywordsLength ()
    {
      return( this.GetKeywords().Length );
    }

    public int GetKeywordsCount ()
    {
      int uiCount = 0;
      string [] aKeywords = Regex.Split( this.GetKeywords(), "[\\s,]+" );
      uiCount = aKeywords.GetLength( 0 );
      return( uiCount );
    }

    /** HrefLang **************************************************************/

    private void SetHreflang ( string sLocale, string sUrl )
    {
      MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang ( sLocale, sUrl );
      this.HrefLang[ sLocale ] = msHrefLang;
    }

    public Dictionary<string,MacroscopeHrefLang> GetHrefLangs ()
    {
      return( this.HrefLang );
    }

    /** Headings **************************************************************/

    public void AddHeading ( ushort iLevel, string sString )
    {
      if( this.Headings.ContainsKey( iLevel ) )
      {
        List<string> lHeadings = this.Headings[ iLevel ];
        lHeadings.Add( sString );
      }
    }

    public List<string> GetHeadings ( ushort iLevel )
    {
      List<string> lHeadings = new List<string> ();
      if( this.Headings.ContainsKey( iLevel ) )
      {
        lHeadings = this.Headings[ iLevel ];
      }
      return( lHeadings );
    }

    /** Body Text *************************************************************/

    public void SetBodyText ( string sText )
    {
      if( ( sText != null ) && ( sText.Length > 0 ) )
      {
        sText = MacroscopeStringTools.CleanBodyText( sText );
        this.BodyText = sText;
      }
      else
      {
        this.BodyText = "";
      }
    }

    public string GetBodyText ()
    {
      return( this.BodyText );
    }

    /** Deep Keyword Analysis *************************************************/

    private void ExecuteDeepKeywordAnalysis ()
    {
      
      Boolean bProceed = false;

      if( this.GetIsHtml() )
      {
        bProceed = true;
      }
      else
      if( this.GetIsPdf() )
      {
        bProceed = true;
      }
      
      if( bProceed )
      {

        MacroscopeDeepKeywordAnalysis AnalyzeKeywords = new MacroscopeDeepKeywordAnalysis ();
      
        lock( this.DeepKeywordAnalysis )
        {
          for( int Words = 0 ; Words <= 3 ; Words++ )
          {
            lock( this.DeepKeywordAnalysis[ Words ] )
            {
              this.DeepKeywordAnalysis[ Words ].Clear();
              AnalyzeKeywords.Analyze(
                Text: this.GetBodyText(), 
                Terms: this.DeepKeywordAnalysis[ Words ],
                Words: Words + 1
              );
            }
          }
        }
      
      }
      
    }

    public Dictionary<string,int> GetDeepKeywordAnalysisAsDictonary ( int Words )
    {
      int iWordsOffset = Words - 1;
      Dictionary<string,int> Terms = new Dictionary<string,int> ( this.DeepKeywordAnalysis.Count );
      lock( this.DeepKeywordAnalysis )
      {
        foreach( string sTerm in this.DeepKeywordAnalysis[iWordsOffset].Keys )
        {
          Terms.Add( sTerm, this.DeepKeywordAnalysis[ iWordsOffset ][ sTerm ] );
        }
      }
      return( Terms );
    }
    
    /** Durations *************************************************************/

    public void SetDuration ( long lDuration )
    {
      this.Duration = lDuration;
    }

    public long GetDuration ()
    {
      return( this.Duration );
    }

    public decimal GetDurationInSeconds ()
    {
      decimal dur = ( decimal )( ( decimal )this.GetDuration() / ( decimal )1000 );
      return( dur );
    }

    public string GetDurationInSecondsFormatted ()
    {
      decimal dDuration = this.GetDurationInSeconds();
      string sDuration = dDuration.ToString( "0.00" );
      return( sDuration );
    }

    public void SetWasDownloaded ( Boolean bState )
    {
      this.WasDownloaded = bState;
    }

    public Boolean GetWasDownloaded ()
    {
      return( this.WasDownloaded );
    }

    /** Page Depth ************************************************************/

    public int GetDepth ()
    {
      return( this.Depth );
    }

    /** Executor **************************************************************/

    public Boolean Execute ()
    {

      // TODO: Change this, so that the initial HEAD request only runs once,
      // TODO: and determines of the page is a redirect, as well as the mime type.

      TimeDuration fTimeDuration = this.GetTimeDurationDelegate();
      Boolean bDownloadDocument = true;

      this.ClearIsDirty();

      try
      {
        this.ProcessUrlElements();
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "ProcessUrlElements: {0}", ex.Message ) );
      }

      fTimeDuration( this.ExecuteHeadRequest );

      if( this.GetStatusCode() == ( int )HttpStatusCode.RequestTimeout )
      {
        return( false );
      }
      else
      if( this.GetStatusCode() == ( int )HttpStatusCode.GatewayTimeout )
      {
        return( false );
      }

      if( this.GetIsRedirect() )
      {
        DebugMsg( string.Format( "REDIRECT DETECTED: {0}", this.Url ) );
        return( true );
      }

      if( !MacroscopePreferencesManager.GetCheckExternalLinks() )
      {
        if( this.GetIsExternal() )
        {
          bDownloadDocument = false;
        }
      }

      if( bDownloadDocument )
      {

        if( this.GetIsHtml() )
        {
          DebugMsg( string.Format( "IS HTML PAGE: {0}", this.Url ) );
          fTimeDuration( this.ProcessHtmlPage );

        }
        else
        if( this.GetIsCss() )
        {
          DebugMsg( string.Format( "IS CSS PAGE: {0}", this.Url ) );
          if( MacroscopePreferencesManager.GetFetchStylesheets() )
          {
            fTimeDuration( this.ProcessCssPage );
          }

        }
        else
        if( this.GetIsImage() )
        {
          DebugMsg( string.Format( "IS IMAGE PAGE: {0}", this.Url ) );
          if( MacroscopePreferencesManager.GetFetchImages() )
          {
            fTimeDuration( this.ProcessImagePage );
          }

        }
        else
        if( this.GetIsJavascript() )
        {
          DebugMsg( string.Format( "IS JAVASCRIPT PAGE: {0}", this.Url ) );
          if( MacroscopePreferencesManager.GetFetchJavascripts() )
          {
            fTimeDuration( this.ProcessJavascriptPage );
          }

        }
        else
        if( this.GetIsPdf() )
        {
          DebugMsg( string.Format( "IS PDF PAGE: {0}", this.Url ) );
          if( MacroscopePreferencesManager.GetFetchPdfs() )
          {
            fTimeDuration( this.ProcessPdfPage );
          }

        }
        else
        if( this.GetIsXml() )
        {
          DebugMsg( string.Format( "IS XML PAGE: {0}", this.Url ) );
          if( MacroscopePreferencesManager.GetFetchXml() )
          {
            fTimeDuration( this.ProcessXmlPage );
          }

        }
        else
        if( this.GetIsVideo() )
        {
          DebugMsg( string.Format( "IS VIDEO PAGE: {0}", this.Url ) );
          if( MacroscopePreferencesManager.GetFetchVideo() )
          {
            fTimeDuration( this.ProcessVideoPage );
          }
        }
        else
        if( this.GetIsBinary() )
        {
          DebugMsg( string.Format( "IS BINARY PAGE: {0}", this.Url ) );
          if( MacroscopePreferencesManager.GetFetchBinaries() )
          {
            fTimeDuration( this.ProcessBinaryPage );
          }

        }
        else
        {
          DebugMsg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.Url ) );
        }

      }
      else
      {
        DebugMsg( string.Format( "SKIPPING DOWNLOAD:: {0}", this.Url ) );
      }

      if( this.Title.Length > 0 )
      {
        this.TitlePixelWidth = AnalyzePageTitles.CalcTitleWidth( this.Title );
      }

      if( MacroscopePreferencesManager.GetWarnAboutInsecureLinks() )
      {
        MacroscopeInsecureLinks InsecureLinks = new MacroscopeInsecureLinks ();
        InsecureLinks.Analyze( this );
      }

      if( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
      {
        this.ExecuteDeepKeywordAnalysis();
      }

      return( true );

    }

    /** Execute Head Request **************************************************/

    private void ExecuteHeadRequest ()
    {

      HttpWebRequest req = WebRequest.CreateHttp( this.Url );
      HttpWebResponse res = null;
      Boolean bIsRedirect = false;
      string sOriginalUrl = this.Url;
      string sErrorCondition = null;

      req.Method = "HEAD";
      req.Timeout = this.Timeout;
      req.KeepAlive = false;
      req.AllowAutoRedirect = false;
      req.UserAgent = this.UserAgent();
      
      MacroscopePreferencesManager.EnableHttpProxy( req );

      try
      {

        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( TimeoutException ex )
      {

        DebugMsg( string.Format( "ExecuteHeadRequest :: TimeoutException: {0}", ex.Message ) );

        sErrorCondition = ex.Message;

      }
      catch( WebException ex )
      {

        DebugMsg( string.Format( "ExecuteHeadRequest :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ExecuteHeadRequest :: WebException: {0}", ex.Status ) );
        DebugMsg( string.Format( "ExecuteHeadRequest :: WebException: {0}", ( int )ex.Status ) );

        sErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        DebugMsg( string.Format( "Status: {0}", res.StatusCode ) );

        this.SetErrorCondition( res.StatusDescription );

        foreach( string sKey in res.Headers )
        {
          DebugMsg( string.Format( "HEADERS: {0} => {1}", sKey, res.GetResponseHeader( sKey ) ) );
        }

        this.ProcessHttpHeaders( req, res );

        try
        {

          switch( res.StatusCode )
          {

          // 200 Range

            case HttpStatusCode.OK:
              bIsRedirect = false;
              break;

          // 300 Range

            case HttpStatusCode.Moved:
              this.SetErrorCondition( HttpStatusCode.Moved.ToString() );
              bIsRedirect = true;
              break;

            case HttpStatusCode.SeeOther:
              this.SetErrorCondition( HttpStatusCode.SeeOther.ToString() );
              bIsRedirect = true;
              break;

            case HttpStatusCode.Redirect:
              this.SetErrorCondition( HttpStatusCode.Redirect.ToString() );
              bIsRedirect = true;
              break;

          // 400 Range

            case HttpStatusCode.BadRequest:
              this.SetErrorCondition( HttpStatusCode.BadRequest.ToString() );
              bIsRedirect = false;
              break;

            case HttpStatusCode.Forbidden:
              this.SetErrorCondition( HttpStatusCode.Forbidden.ToString() );
              bIsRedirect = false;
              break;

            case HttpStatusCode.NotFound:
              this.SetErrorCondition( HttpStatusCode.NotFound.ToString() );
              bIsRedirect = false;
              break;

            case HttpStatusCode.Gone:
              this.SetErrorCondition( HttpStatusCode.Gone.ToString() );
              bIsRedirect = false;
              break;

          // Unhandled

            default:
              throw new MacroscopeDocumentException ( "Unhandled HttpStatusCode Type" );

          }

        }
        catch( MacroscopeDocumentException ex )
        {
          DebugMsg( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
        }

        if( bIsRedirect )
        {

          this.IsRedirect = true;
          string sLocation = res.GetResponseHeader( "Location" );
          string sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, sLocation );

          if( sLinkUrlAbs != null )
          {
            this.UrlRedirectFrom = sOriginalUrl;
            this.UrlRedirectTo = sLinkUrlAbs;
            this.AddDocumentOutlink(
              sLinkUrlAbs,
              sLinkUrlAbs,
              MacroscopeConstants.OutlinkType.REDIRECT,
              true
            );
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

    private void ProcessErrorCondition ( string ErrorCond )
    {

      if( ErrorCond != null )
      {

        switch( ErrorCond.ToLower() )
        {
          case "timeout":
            this.StatusCode = ( int )HttpStatusCode.RequestTimeout;
            break;
          default:
            this.StatusCode = ( int )HttpStatusCode.Ambiguous;
            break;
        }

        this.ErrorCondition = ErrorCond;

      }
      else
      {
        this.ErrorCondition = "";
      }

    }

    /**************************************************************************/

    private void ProcessHttpHeaders ( HttpWebRequest req, HttpWebResponse res )
    {

      // Status Code
      this.StatusCode = ( int )res.StatusCode;
      this.SetErrorCondition( res.StatusDescription );

      // Raw HTTP Headers
      this.RawHttpStatusLine = string.Join(
        " ",
        string.Join( "/", "HTTP", res.ProtocolVersion ),
        ( ( int )res.StatusCode ).ToString(),
        res.StatusDescription,
        "\r\n"
      );
      this.RawHttpHeaders = res.Headers.ToString();

      // Common HTTP Headers
      {
        this.MimeType = res.ContentType;
        this.ContentLength = res.ContentLength;
      }

      // Probe HTTP Headers
      foreach( string sHeader in res.Headers )
      {

        DebugMsg( string.Format( "HTTP HEADER: {0} :: {1}", sHeader, res.GetResponseHeader( sHeader ) ) );

        if( sHeader.ToLower().Equals( "date" ) )
        {
          this.DateServer = DateTime.Parse( res.GetResponseHeader( sHeader ) );
        }

        if( sHeader.ToLower().Equals( "last-modified" ) )
        {
          this.DateModified = DateTime.Parse( res.GetResponseHeader( sHeader ) );
        }

        if( sHeader.ToLower().Equals( "content-encoding" ) )
        {
          this.IsCompressed = true;
          this.CompressionMethod = res.GetResponseHeader( sHeader );
        }

        // Process HTST Policy
        // https://www.owasp.org/index.php/HTTP_Strict_Transport_Security_Cheat_Sheet
        // Strict-Transport-Security: max-age=31536000; includeSubDomains; preload
        if( sHeader.ToLower().Equals( "strict-transport-security" ) )
        {
          this.HypertextStrictTransportPolicy = true;
          // TODO: implement includeSubDomains
        }

        // Canonical HTTP Header
        // Link: <http://www.example.com/downloads/white-paper.pdf>; rel="canonical"
        if( sHeader.ToLower().Equals( "link" ) )
        {

          string sUrl = null;
          string sRel = null;
          string sRaw = res.GetResponseHeader( sHeader );

          MatchCollection matches = Regex.Matches( sRaw, "<([^<>]+)>\\s*;\\srel=\"([^\"]+)\"" );

          foreach( Match match in matches )
          {
            sUrl = match.Groups[ 1 ].Value;
            sRel = match.Groups[ 2 ].Value;
          }

          if( ( sRel != null ) && ( sRel.ToLower() == "canonical" ) )
          {

            if( ( sUrl != null ) && ( sUrl.Length > 0 ) )
            {
              this.SetCanonical( sUrl );
            }
          }

        }

        // Probe Character Set
        // TODO: implement this
        if( sHeader.ToLower().Equals( "content-type" ) )
        {
          string sCharSet = "";
          this.CharSet = null;
        }

        // Process Etag
        if( sHeader.ToLower().Equals( "etag" ) )
        {
          string ETag = res.GetResponseHeader( sHeader );
          if( ( ETag != null ) && ( ETag.Length > 0 ) )
          {
            ETag = Regex.Replace( ETag, "[\"'\\s]+", "", RegexOptions.Singleline );
          }
          else
          {
            ETag = "";
          }
          this.SetEtag( ETag );
        }

      }

      // Process Dates
      {
        if( this.DateServer.Date == new DateTime ().Date )
        {
          this.DateServer = new DateTime ();
        }
        if( this.DateModified.Date == new DateTime ().Date )
        {
          this.DateModified = this.DateServer;
        }
      }

      // Process MIME Type
      {

        Regex reIsHtml = new Regex ( "^(text/html|application/xhtml+xml)", RegexOptions.IgnoreCase );
        Regex reIsCss = new Regex ( "^text/css", RegexOptions.IgnoreCase );
        Regex reIsJavascript = new Regex ( "^(application/javascript|text/javascript)", RegexOptions.IgnoreCase );
        Regex reIsImage = new Regex ( "^image/(gif|png|jpeg|bmp|webp)", RegexOptions.IgnoreCase );
        Regex reIsPdf = new Regex ( "^application/pdf", RegexOptions.IgnoreCase );
        Regex reIsAudio = new Regex ( "^audio/[a-z0-9]+", RegexOptions.IgnoreCase );
        Regex reIsVideo = new Regex ( "^video/[a-z0-9]+", RegexOptions.IgnoreCase );
        Regex reIsXml = new Regex ( "^(application|text)/(atom\\+xml|xml)", RegexOptions.IgnoreCase );

        if( reIsHtml.IsMatch( res.ContentType.ToString() ) )
        {
          this.SetIsHtml();
        }
        else
        if( reIsCss.IsMatch( res.ContentType.ToString() ) )
        {
          this.SetIsCss();
        }
        else
        if( reIsJavascript.IsMatch( res.ContentType.ToString() ) )
        {
          this.SetIsJavascript();
        }
        else
        if( reIsImage.IsMatch( res.ContentType.ToString() ) )
        {
          this.SetIsImage();
        }
        else
        if( reIsPdf.IsMatch( res.ContentType.ToString() ) )
        {
          this.SetIsPdf();
        }
        else
        if( reIsAudio.IsMatch( res.ContentType.ToString() ) )
        {
          this.SetIsAudio();
        }
        else
        if( reIsVideo.IsMatch( res.ContentType.ToString() ) )
        {
          this.SetIsVideo();
        }
        else
        if( reIsXml.IsMatch( res.ContentType.ToString() ) )
        {
          this.SetIsXml();
        }
        else
        {
          this.SetIsBinary();
        }

      }

    }

    /**************************************************************************/

    private void ProcessUrlElements ()
    {

      Uri uUri = new Uri ( this.GetUrl(), UriKind.Absolute );
      this.Scheme = uUri.Scheme;
      this.Hostname = uUri.Host;
      this.Port = uUri.Port;
      this.Path = uUri.AbsolutePath;
      this.Fragment = uUri.Fragment;
      this.QueryString = uUri.Query;

      if( this.Scheme.ToLower().Equals( "https" ) )
      {
        this.SetIsSecureUrl( true );
      }

    }

    /**************************************************************************/

  }

}
