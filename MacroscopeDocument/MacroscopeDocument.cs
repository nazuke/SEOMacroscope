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
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SEOMacroscope
{

  /// <summary>
  /// MacroscopeDocument is a representation of the document found at a crawled URL.
  /// </summary>

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private MacroscopeDocumentCollection DocCollection;

    private Boolean IsDirty;

    private MacroscopeConstants.FetchStatus FetchStatus;

    private DateTime CrawledDate;
    
    private string DocUrl;
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
    private string QueryString;
    private string Fragment;
    
    private MacroscopeConstants.AuthenticationType AuthenticationType;
    private string AuthenticationRealm;
    private MacroscopeCredential AuthenticationCredential;

    private string RawHttpRequestHeaders;
    private string RawHttpStatusLine;
    private string RawHttpHeaders;

    private Boolean HypertextStrictTransportPolicy;
    private Boolean IsSecureUrl;
    private List<string> InSecureLinks;
    
    private HttpStatusCode StatusCode;
    private string ErrorCondition;
    private long ContentLength;

    private string MimeType;
    private MacroscopeConstants.DocumentType DocumentType;

    private Boolean IsCompressed;
    private string CompressionMethod;
    private string ContentEncoding;

    private string Locale;
    private string IsoLanguageCode;
    private Encoding CharSet;
    private string CharacterSet;

    private long Duration;
    private Boolean WasDownloaded;

    private DateTime DateServer;
    private DateTime DateModified;
    private DateTime DateExpires;

    private string Canonical;
    private Dictionary<string,MacroscopeHrefLang> HrefLang;

    private Dictionary<string,string> MetaHeaders;
    
    // Inbound links
    private Boolean ProcessInlinks;
    
    // Outbound links to pages and linked assets to follow
    //private Dictionary<string,MacroscopeLink> Outlinks;
    private MacroscopeLinkList Outlinks;

    // Inbound hypertext links
    private Boolean ProcessHyperlinksIn;

    // Outbound hypertext links
    private MacroscopeHyperlinksOut HyperlinksOut;

    private Dictionary<string,string> EmailAddresses;
    private Dictionary<string,string> TelephoneNumbers;

    private MacroscopeAnalyzePageTitles AnalyzePageTitles;

    private string Title;
    private string TitleLanguage;
    private int TitlePixelWidth;
    private string Description;
    private string DescriptionLanguage;
    private string Keywords;
    private string AltText;
    
    private Dictionary<ushort,List<string>> Headings;

    private string BodyText;
    private string BodyTextLanguage;
    private List<Dictionary<string,int>> DeepKeywordAnalysis;
    private int WordCount;

    private int Depth;

    private List<string> Remarks;

    private Dictionary<string, MacroscopeConstants.TextPresence> CustomFiltered;

    // Delegate Functions
    private delegate void TimeDuration( Action ProcessMethod );

    /**************************************************************************/

    public MacroscopeDocument (
      string Url
    )
    {
      this.InitializeDocument( Url );
    }
    
    public MacroscopeDocument (
      MacroscopeDocumentCollection DocumentCollection,
      string Url
    )
    {
      this.InitializeDocument( Url );
      this.SetDocumentCollection( DocumentCollection );
    }

    public MacroscopeDocument (
      MacroscopeCredential Credential,
      string Url
    )
    {
      this.InitializeDocument( Url );
      this.AuthenticationCredential = Credential;
    }
    
    public MacroscopeDocument (
      MacroscopeDocumentCollection DocumentCollection,
      MacroscopeCredential Credential,
      string Url
    )
    {
      this.InitializeDocument( Url );
      this.SetDocumentCollection( DocumentCollection );
      this.AuthenticationCredential = Credential;
    }

    /** Self Destruct Sequence ************************************************/
    
    ~MacroscopeDocument ()
    {

      this.DocCollection = null;

      this.BodyText = null;

      this.DeepKeywordAnalysis = null;
      
      return;
      
    }

    /** -------------------------------------------------------------------- **/

    private void InitializeDocument ( string Url )
    {

      this.SuppressDebugMsg = true;

      DocCollection = null;
      
      this.IsDirty = true;

      this.ClearFetchStatus();
      
      this.CrawledDate = DateTime.UtcNow;
          
      this.DocUrl = Url;
      this.Timeout = MacroscopePreferencesManager.GetRequestTimeout() * 1000;

      this.Checksum = "";
      this.Etag = "";

      this.IsExternal = false;

      this.IsRedirect = false;
      this.UrlRedirectFrom = "";
      this.UrlRedirectTo = "";

      this.RawHttpRequestHeaders = "";
      this.RawHttpStatusLine = "";
      this.RawHttpHeaders = "";

      this.Scheme = "";
      this.Hostname = "";
      this.Port = 80;
      this.Path = "";
      this.Fragment = "";
      this.QueryString = "";

      AuthenticationType = MacroscopeConstants.AuthenticationType.NONE;
      AuthenticationRealm = null;
      AuthenticationCredential = null;
      
      this.HypertextStrictTransportPolicy = false;
      this.IsSecureUrl = false;
      this.InSecureLinks = new List<string> ( 128 );
    
      this.StatusCode = 0;
      this.ErrorCondition = "";
      this.ContentLength = 0;

      this.MimeType = MacroscopeConstants.DefaultMimeType;
      this.DocumentType = MacroscopeConstants.DocumentType.BINARY;

      this.ContentEncoding = "";

      this.Locale = null;
      this.IsoLanguageCode = null;
      this.CharSet = null;
      this.CharacterSet = null;

      this.Duration = 0;
      this.WasDownloaded = false;

      this.DateServer = new DateTime ();
      this.DateModified = new DateTime ();
      this.DateExpires = new DateTime ();

      this.Canonical = "";
      this.HrefLang = new Dictionary<string,MacroscopeHrefLang> ( 1024 );

      this.MetaHeaders = new Dictionary<string,string> ( 8 );
      
      this.ProcessInlinks = false;
      this.Outlinks = new MacroscopeLinkList ();

      this.ProcessHyperlinksIn = false;
      this.HyperlinksOut = new MacroscopeHyperlinksOut ();

      this.EmailAddresses = new Dictionary<string,string> ( 256 );
      this.TelephoneNumbers = new Dictionary<string,string> ( 256 );

      this.AnalyzePageTitles = new MacroscopeAnalyzePageTitles ();

      this.Title = "";
      this.TitleLanguage = "";
      this.TitlePixelWidth = 0;
      this.Description = "";
      this.DescriptionLanguage = "";
      this.Keywords = "";
      this.AltText = "";
      
      this.Headings = new Dictionary<ushort,List<string>> ( 7 );

      for( ushort HeadingLevel = 1 ; HeadingLevel <= 6 ; HeadingLevel++ )
      {
        this.Headings.Add( HeadingLevel, new List<string> ( 16 ) );
      }

      this.BodyText = "";
      this.BodyTextLanguage = "";
      
      this.DeepKeywordAnalysis = new List<Dictionary<string,int>> ( 4 );
      for( int i = 0 ; i <= 3 ; i++ )
      {
        this.DeepKeywordAnalysis.Add( new Dictionary<string,int> ( 256 ) );
      }

      this.WordCount = 0;
      
      this.Depth = MacroscopeUrlUtils.FindUrlDepth( Url );

      this.Remarks = new List<string> ();

      this.CustomFiltered = new Dictionary<string, MacroscopeConstants.TextPresence> ( 5 );

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

    /** DocumentCollection ************************************************************/
    
    private void SetDocumentCollection ( MacroscopeDocumentCollection DocumentCollection )
    {
      this.DocCollection = DocumentCollection;
    }

    /** Dirty Flag ************************************************************/

    public void SetIsDirty ()
    {
      this.IsDirty = true;
    }

    private void ClearIsDirty ()
    {
      this.IsDirty = false;
    }

    public Boolean GetIsDirty ()
    {
      return( this.IsDirty );
    }

    /** Fetch Status ************************************************************/

    public void SetFetchStatus ( MacroscopeConstants.FetchStatus NewFetchStatus )
    {
      this.FetchStatus = NewFetchStatus;
    }

    public void ClearFetchStatus ()
    {
      this.FetchStatus = MacroscopeConstants.FetchStatus.VOID;
    }

    public MacroscopeConstants.FetchStatus GetFetchStatus ()
    {
      return( this.FetchStatus );
    }

    /** Host Details **********************************************************/

    public string GetUrl ()
    {
      return( this.DocUrl );
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

    /** Authentication ********************************************************/

    public void SetAuthenticationType ( MacroscopeConstants.AuthenticationType AuthenticationType )
    {
      this.AuthenticationType = AuthenticationType;
    }
    
    public MacroscopeConstants.AuthenticationType GetAuthenticationType ()
    {
      return( this.AuthenticationType );
    }

    public void SetAuthenticationRealm ( string AuthenticationRealm )
    {
      this.AuthenticationRealm = AuthenticationRealm;
    }
    
    public string GetAuthenticationRealm ()
    {
      return( this.AuthenticationRealm );
    }

    private MacroscopeCredential GetAuthenticationCredential ()
    {
      return( this.AuthenticationCredential );
    }

    /** Secure URLs ***********************************************************/

    public void SetIsSecureUrl ( Boolean State )
    {
      this.IsSecureUrl = State;
    }

    public Boolean GetIsSecureUrl ()
    {
      return( this.IsSecureUrl );
    }

    public void AddInsecureLink ( string Url )
    {
      this.InSecureLinks.Add( Url );
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

    private string GenerateChecksum ( string RawData )
    {
      HashAlgorithm Digest = HashAlgorithm.Create( "SHA256" );
      byte [] BytesIn = Encoding.UTF8.GetBytes( RawData );
      byte [] Hashed = Digest.ComputeHash( BytesIn );
      StringBuilder sbString = new StringBuilder ();
      for( int i = 0 ; i < Hashed.Length ; i++ )
      {
        sbString.Append( Hashed[ i ].ToString( "X2" ) );
      }
      string ChecksumValue = sbString.ToString();
      return( ChecksumValue );
    }

    /** Etag Value ************************************************************/
    
    public void SetEtag ( string EtagValue )
    {
      this.Etag = EtagValue;
    }

    public string GetEtag ()
    {
      return( this.Etag );
    }

    /** Is External Flag ******************************************************/

    public void SetIsInternal ( Boolean State )
    {
      if( State )
      {
        this.IsExternal = false;
      }
      else
      {
        this.IsExternal = true;
      }
    }

    public Boolean GetIsInternal ()
    {
      if( this.IsExternal )
      {
        return( false );
      }
      else
      {
        return( true );
      }
    }

    /** -------------------------------------------------------------------- **/
    
    public void SetIsExternal ( Boolean State )
    {
      this.IsExternal = State;
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

    public void SetUrlRedirectFrom ( string Url )
    {
      this.UrlRedirectFrom = Url;
    }

    public string GetUrlRedirectFrom ()
    {
      return( this.UrlRedirectFrom );
    }

    public void SetUrlRedirectTo ( string  Url )
    {
      this.UrlRedirectTo = Url;
    }

    public string GetUrlRedirectTo ()
    {
      return( this.UrlRedirectTo );
    }

    /**************************************************************************/

    public void SetStatusCode ( HttpStatusCode Status )
    {
      this.StatusCode = Status;
    }

    public HttpStatusCode GetStatusCode ()
    {
      return( this.StatusCode );
    }

    /**************************************************************************/

    public void SetErrorCondition ( string ErrorCondtionValue )
    {
      this.ErrorCondition = ErrorCondtionValue;
    }
    
    public string GetErrorCondition ()
    {
      return( this.ErrorCondition );
    }

    /** HTTP Headers **********************************************************/

    public string GetHttpRequestHeadersAsText ()
    {
      return( this.RawHttpRequestHeaders );
    }

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
      string DocumentMimeType = null;
      if( this.MimeType == null )
      {
        DocumentMimeType = MacroscopeConstants.DefaultMimeType;
      }
      else
      {
        MatchCollection matches = Regex.Matches( this.MimeType, "^([^\\s;/]+)/([^\\s;/]+)" );
        foreach( Match match in matches )
        {
          DocumentMimeType = String.Format( "{0}/{1}", match.Groups[ 1 ].Value, match.Groups[ 2 ].Value );
        }
        if( DocumentMimeType == null )
        {
          DocumentMimeType = this.MimeType;
        }
      }
      return( DocumentMimeType );
    }

    /** Document Type Methods *************************************************/

    public MacroscopeConstants.DocumentType GetDocumentType ()
    {
      return( this.DocumentType );
    }

    /** -------------------------------------------------------------------- **/
        
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

    /** -------------------------------------------------------------------- **/
        
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

    /** -------------------------------------------------------------------- **/
        
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

    /** -------------------------------------------------------------------- **/
        
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
    
    /** -------------------------------------------------------------------- **/

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
    
    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/
        
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

    /** -------------------------------------------------------------------- **/
        
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

    /** -------------------------------------------------------------------- **/
        
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

    /** -------------------------------------------------------------------- **/
        
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
    
    /** -------------------------------------------------------------------- **/
        
    public void SetIsText ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.TEXT;
    }

    public Boolean GetIsText ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.TEXT )
      {
        return( true );
      }
      else
      {
        return( false );
      }
    }
    
    /** -------------------------------------------------------------------- **/

    public void SetIsSitemapText ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.SITEMAPTEXT;
    }

    public Boolean GetIsSitemapText ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.SITEMAPTEXT )
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

    public void SetLocale ( string NewLocale )
    {
      this.Locale = NewLocale;
    }

    public string GetLocale ()
    {
      return( this.Locale );
    }
            
    public void SetIsoLanguageCode ( string LanguageCode )
    {
      this.IsoLanguageCode = LanguageCode;
    }
    
    public string GetIsoLanguageCode ()
    {
      return( this.IsoLanguageCode );
    }

    /** Character Set *********************************************************/

    public string GetCharacterSet ()
    {
      return( this.CharacterSet );
    }

    public void SetCharacterSet ( string NewCharSet )
    {
      this.CharacterSet = NewCharSet;
    }

    /** Canonical *************************************************************/

    public string GetCanonical ()
    {
      return( this.Canonical );
    }

    public void SetCanonical ( string NewCanonical )
    {
      this.Canonical = NewCanonical;
    }

    /** Dates *****************************************************************/

    public string GetCrawledDate ()
    {
      return( this.CrawledDate.ToUniversalTime().ToString( "r" ) );
    }

    public string GetDateServer ()
    {
      return( this.DateServer.ToUniversalTime().ToString( "r" ) );
    }

    public string GetDateModified ()
    {
      return( this.DateModified.ToUniversalTime().ToString( "r" ) );
    }

    public string GetDateExpires ()
    {
      return( this.DateExpires.ToUniversalTime().ToString( "r" ) );
    }

    /** Inlinks ***************************************************************/
    
    public void SetProcessInlinks ()
    {
      this.ProcessInlinks = true;
    }
       
    public void UnsetProcessInlinks ()
    {
      this.ProcessInlinks = false;
    }
    
    public Boolean GetProcessInlinks ()
    {
      return( this.ProcessInlinks );
    }

    public MacroscopeLinkList GetLinksIn ()
    {
      MacroscopeLinkList DocumentLinksIn = this.DocCollection.GetDocumentInlinks( this.GetUrl() );
      return( DocumentLinksIn );
    }

    public int CountInlinks ()
    {
      
      MacroscopeLinkList LinksIn = this.GetLinksIn();
      int Count = 0;
      
      if( LinksIn != null )
      {
        Count = LinksIn.Count();
      }
      
      return( Count );
      
    }
        
    /** Outlinks **************************************************************/

    public MacroscopeLinkList GetOutlinks ()
    {
      return( this.Outlinks );
    }

    public IEnumerable<MacroscopeLink> IterateOutlinks ()
    {
      lock( this.Outlinks )
      {
        foreach( MacroscopeLink Link in this.Outlinks.IterateLinks() )
        {
          yield return Link;
        }
      }
    }

    public int CountOutlinks ()
    {
      int iCount = this.Outlinks.Count();
      return( iCount );
    }

    private MacroscopeLink AddDocumentOutlink (
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

    /** Hyperlinks In *********************************************************/

    public MacroscopeHyperlinksIn GetHyperlinksIn ()
    {
      MacroscopeHyperlinksIn DocumentHyperlinksIn = this.DocCollection.GetDocumentHyperlinksIn( this.GetUrl() );
      return( DocumentHyperlinksIn );
    }

    public int CountHyperlinksIn ()
    {

      MacroscopeHyperlinksIn DocumentHyperlinksIn = this.DocCollection.GetDocumentHyperlinksIn( this.GetUrl() );
      int Count = 0;
      
      if( DocumentHyperlinksIn != null )
      {
        
        Count = DocumentHyperlinksIn.Count();
      }

      return( Count );
    }

    /** Hyperlinks Out ********************************************************/

    public void SetProcessHyperlinksIn ()
    {
      this.ProcessHyperlinksIn = true;
    }
       
    public void UnsetProcessHyperlinksIn ()
    {
      this.ProcessHyperlinksIn = false;
    }
    
    public Boolean GetProcessHyperlinksIn ()
    {
      return( this.ProcessHyperlinksIn );
    }

    public MacroscopeHyperlinksOut GetHyperlinksOut ()
    {
      return( this.HyperlinksOut );
    }

    private void SetProcessHyperlinksInForUrl ( string Url )
    {

      if( this.DocCollection != null )
      {
        
        MacroscopeDocument msDoc = this.DocCollection.GetDocument( Url: Url );
      
        if( msDoc != null )
        {
          msDoc.SetProcessInlinks();
          msDoc.SetProcessHyperlinksIn();
        }
      
      }

    }

    public int CountHyperlinksOut ()
    {
      int iCount = this.HyperlinksOut.Count();
      return( iCount );
    }

    /** Email Addresses *******************************************************/

    public void AddEmailAddress ( string EmailAddress )
    {
      if( this.EmailAddresses.ContainsKey( EmailAddress ) )
      {
        this.EmailAddresses[ EmailAddress ] = this.GetUrl();
      }
      else
      {
        this.EmailAddresses.Add( EmailAddress, this.GetUrl() );
      }
    }

    public Dictionary<string,string> GetEmailAddresses ()
    {
      return( this.EmailAddresses );
    }

    /** Telephone Numbers *****************************************************/

    public void AddTelephoneNumber ( string TelephoneNumber )
    {
      if( this.TelephoneNumbers.ContainsKey( TelephoneNumber ) )
      {
        this.TelephoneNumbers[ TelephoneNumber ] = this.GetUrl();
      }
      else
      {
        this.TelephoneNumbers.Add( TelephoneNumber, this.GetUrl() );
      }
    }

    public Dictionary<string,string> GetTelephoneNumbers ()
    {
      return( this.TelephoneNumbers );
    }

    /** Title *****************************************************************/

    public void SetTitle (
      string TitleText,
      MacroscopeConstants.TextProcessingMode ProcessingMode
    )
    {

      string Value = TitleText;

      if( ProcessingMode == MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES )
      {
        Value = HtmlEntity.DeEntitize( TitleText );
      }

      Value = Regex.Replace( Value, "[\\s]+", " ", RegexOptions.Singleline );
      Value = Value.Trim();

      this.Title = Value;

    }

    public string GetTitle ()
    {
      string TitleValue;
      if( this.Title != null )
      {
        TitleValue = this.Title;
      }
      else
      {
        TitleValue = "";
      }
      return( TitleValue );
    }

    public int GetTitleLength ()
    {
      return( this.Title.Length );
    }

    public void SetTitlePixelWidth ( int Width )
    {
      this.TitlePixelWidth = Width;
    }

    public int GetTitlePixelWidth ()
    {
      return( this.TitlePixelWidth );
    }

    /** -------------------------------------------------------------------- **/

    public void DetectTitleLanguage ()
    {
      if( this.GetIsHtml() || this.GetIsPdf() )
      {
        this.TitleLanguage = this.ProbeTextLanguage( Text: this.Title );
      }
    }

    public string GetTitleLanguage ()
    {
      return( this.TitleLanguage );
    }

    /** Description ***********************************************************/

    public string GetDescription ()
    {
      string DescriptionValue;
      if( this.Description != null )
      {
        DescriptionValue = this.Description;
      }
      else
      {
        DescriptionValue = "";
      }
      return( DescriptionValue );
    }

    public int GetDescriptionLength ()
    {
      return( this.GetDescription().Length );
    }

    /** -------------------------------------------------------------------- **/

    public void DetectDescriptionLanguage ()
    {
      if( this.GetIsHtml() || this.GetIsPdf() )
      {
        this.DescriptionLanguage = this.ProbeTextLanguage( Text: this.Description );
      }
    }

    public string GetDescriptionLanguage ()
    {
      return( this.DescriptionLanguage );
    }
    
    /** Keywords **************************************************************/

    public string GetKeywords ()
    {

      string KeywordsValue;

      if( this.Keywords != null )
      {
        KeywordsValue = this.Keywords;
      }
      else
      {
        KeywordsValue = "";
      }

      return( KeywordsValue );

    }

    /** -------------------------------------------------------------------- **/

    public int GetKeywordsLength ()
    {
      
      int Length = 0;

      if( this.Keywords != null )
      {
        Length = this.Keywords.Length;
      }
      
      return( Length );

    }

    /** -------------------------------------------------------------------- **/
    
    public int GetKeywordsCount ()
    {

      int Count = 0;
      
      if(
        ( this.Keywords != null )
        && ( this.Keywords.Length > 0 ) )
      {
        string [] KeywordsList = Regex.Split( this.Keywords, "[\\s,]+" );
        Count = KeywordsList.GetLength( 0 );
      }

      return( Count );

    }

    /** AltText ***************************************************************/

    public void SetAltText ( string AltText, MacroscopeConstants.TextProcessingMode ProcessingMode )
    {

      string Value = AltText;

      if( ProcessingMode == MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES )
      {
        Value = HtmlEntity.DeEntitize( AltText );
      }

      this.AltText = Value;

    }

    /** -------------------------------------------------------------------- **/

    public string GetAltText ()
    {
      string Value;
      if( this.AltText != null )
      {
        Value = this.AltText;
      }
      else
      {
        Value = "";
      }
      return( Value );
    }

    /** -------------------------------------------------------------------- **/

    public int GetAltTextLength ()
    {
      return( this.AltText.Length );
    }

    /** HrefLang **************************************************************/

    private void SetHreflang ( string HrefLangLocale, string Url )
    {
      string LocaleProcessed = HrefLangLocale.ToLower();
      MacroscopeHrefLang msHrefLang = new MacroscopeHrefLang ( LocaleProcessed, Url );
      this.HrefLang[ LocaleProcessed ] = msHrefLang;
    }

    public Dictionary<string,MacroscopeHrefLang> GetHrefLangs ()
    {
      return( this.HrefLang );
    }

    /** META Headers **********************************************************/

    public IEnumerable<KeyValuePair<string,string>> IterateMetaTags ()
    {
      lock( this.MetaHeaders )
      {
        foreach( string Key  in this.MetaHeaders.Keys )
        {
          KeyValuePair<string,string> KP = new KeyValuePair<string,string> ( Key, this.MetaHeaders[ Key ] );
          yield return KP;
        }
      }
    }

    public string GetMetaTag ( string Key )
    {
      string Value = "";
      if( this.MetaHeaders.ContainsKey( Key ) )
      {
        Value = this.MetaHeaders[ Key ];
      }
      return( Value );
    }

    /** Headings **************************************************************/

    public void AddHeading ( ushort HeadingLevel, string HeadingText )
    {
      if( this.Headings.ContainsKey( HeadingLevel ) )
      {
        List<string> lHeadings = this.Headings[ HeadingLevel ];
        lHeadings.Add( HeadingText );
      }
    }

    /** -------------------------------------------------------------------- **/
    
    public List<string> GetHeadings ( ushort HeadingLevel )
    {
      List<string> HeadingList = new List<string> ();
      if( this.Headings.ContainsKey( HeadingLevel ) )
      {
        HeadingList = this.Headings[ HeadingLevel ];
      }
      return( HeadingList );
    }

    /** Body Text *************************************************************/

    public void SetBodyText ( string Text )
    {
      if( !string.IsNullOrEmpty( Text ) )
      {

        this.BodyText = Text;
        Text = MacroscopeStringTools.CleanBodyText( msDoc: this );
        this.BodyText = Text;
        
        if( !string.IsNullOrEmpty( this.BodyText ) )
        {
          this.SetWordCount();
        }

      }
      else
      {
        this.BodyText = "";
      }
    }

    /** -------------------------------------------------------------------- **/
        
    public string GetBodyText ()
    {
      return( this.BodyText );
    }
    
    /** -------------------------------------------------------------------- **/
        
    public int GetBodyTextLength ()
    {
      return( this.BodyText.Length );
    }

    /** -------------------------------------------------------------------- **/

    public void DetectBodyTextLanguage ()
    {
      if( this.GetIsHtml() || this.GetIsPdf() )
      {
        this.BodyTextLanguage = this.ProbeTextLanguage( Text: this.BodyText );
      }
    }

    /** -------------------------------------------------------------------- **/
        
    public string GetBodyTextLanguage ()
    {
      return( this.BodyTextLanguage );
    }

    /** -------------------------------------------------------------------- **/

    private void SetWordCount ()
    {
          
      this.WordCount = 0;

      try
      {
        string [] Words = Regex.Split( this.BodyText, "\\s", RegexOptions.Singleline );
        this.WordCount = Words.Length;
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "SetWordCount: {0}", ex.Message ) );
      }

    }

    public int GetWordCount ()
    {
      return( this.WordCount );
    }

    /** Deep Keyword Analysis *************************************************/

    private void ExecuteDeepKeywordAnalysis ()
    {
      
      Boolean Proceed = false;

      if( this.GetIsHtml() )
      {
        Proceed = true;
      }
      else
      if( this.GetIsPdf() )
      {
        Proceed = true;
      }
      
      if( Proceed )
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

    /** -------------------------------------------------------------------- **/
        
    public Dictionary<string,int> GetDeepKeywordAnalysisAsDictonary ( int Words )
    {
      int WordsOffset = Words - 1;
      Dictionary<string,int> Terms = new Dictionary<string,int> ( this.DeepKeywordAnalysis.Count );
      lock( this.DeepKeywordAnalysis )
      {
        foreach( string Term in this.DeepKeywordAnalysis[WordsOffset].Keys )
        {
          Terms.Add( Term, this.DeepKeywordAnalysis[ WordsOffset ][ Term ] );
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
      decimal DurationSeconds = ( decimal )( ( decimal )this.GetDuration() / ( decimal )1000 );
      return( DurationSeconds );
    }

    public string GetDurationInSecondsFormatted ()
    {
      decimal DurationSeconds = this.GetDurationInSeconds();
      string DurationText = DurationSeconds.ToString( "0.00" );
      return( DurationText );
    }

    public void SetWasDownloaded ( Boolean State )
    {
      this.WasDownloaded = State;
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

    /** Remarks ***************************************************************/

    public void AddRemark ( string Observation )
    {
      lock( this.Remarks )
      {
        this.Remarks.Add( Observation );
      }
    }

    /** -------------------------------------------------------------------- **/
        
    public IEnumerable<string> IterateRemarks ()
    {
      lock( this.Remarks )
      {
        foreach( string Observation in this.Remarks )
        {
          yield return Observation;
        }
      }
    }

    /** Custom Filtered Values ************************************************/

    public void SetCustomFiltered ( string Text, MacroscopeConstants.TextPresence Presence )
    {

     
      if( this.CustomFiltered.ContainsKey( Text ) )
      {
        this.CustomFiltered[ Text ] = Presence;
        
      }
      else
      {
        this.CustomFiltered.Add( Text, Presence );
        
      }

    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<string> IterateCustomFiltered ()
    {
      lock( this.CustomFiltered )
      {
        foreach( string Text in this.CustomFiltered.Keys )
        {
          yield return Text;
        }
      }
    }
        
    /** -------------------------------------------------------------------- **/ 

    public Dictionary<string, MacroscopeConstants.TextPresence> GetCustomFiltered ()
    {

      Dictionary<string, MacroscopeConstants.TextPresence> ListCopy = new Dictionary<string, MacroscopeConstants.TextPresence> ( this.CustomFiltered.Count );

      lock( this.CustomFiltered )
      {

        foreach( string Key in this.CustomFiltered.Keys )
        {
          ListCopy.Add( Key, this.CustomFiltered[ Key ] );
        }

      }

      return( ListCopy );

    }
    
    /** -------------------------------------------------------------------- **/

    public KeyValuePair<string, MacroscopeConstants.TextPresence> GetCustomFilteredItem ( string Text )
    {

      KeyValuePair<string, MacroscopeConstants.TextPresence> Pair;

      Pair = new KeyValuePair<string, MacroscopeConstants.TextPresence> (
        null,
        MacroscopeConstants.TextPresence.UNDEFINED
      );
      
      try
      {
        
        Pair = new KeyValuePair<string, MacroscopeConstants.TextPresence> (
          Text,
          this.CustomFiltered[ Text ]
        );

      }
      catch( KeyNotFoundException ex )
      {
        DebugMsg( string.Format( "GetCustomFilteredItem: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "GetCustomFilteredItem: {0}", ex.Message ) );
      }

      return( Pair );

    }

    /** Executor **************************************************************/

    public Boolean Execute ()
    {

      TimeDuration fTimeDuration = this.GetTimeDurationDelegate();
      Boolean DoDownloadDocument = true;

      this.ClearIsDirty();

      this.SetProcessInlinks();
      this.SetProcessHyperlinksIn();
            
      try
      {
        this.ProcessUrlElements();
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "ProcessUrlElements: {0}", ex.Message ) );
      }

      fTimeDuration( this.ExecuteHeadRequest );

      if( this.GetStatusCode() == HttpStatusCode.RequestTimeout )
      {
        return( false );
      }
      else
      if( this.GetStatusCode() == HttpStatusCode.GatewayTimeout )
      {
        return( false );
      }

      if( this.GetIsRedirect() )
      {
        DebugMsg( string.Format( "REDIRECT DETECTED: {0}", this.DocUrl ) );
        return( true );
      }

      if( !MacroscopePreferencesManager.GetCheckExternalLinks() )
      {
        if( this.GetIsExternal() )
        {
          DoDownloadDocument = false;
        }
      }

      if( this.DocCollection == null )
      {
        this.SetFetchStatus( NewFetchStatus: MacroscopeConstants.FetchStatus.OK );
      }
      else
      if( this.GetFetchStatus() != MacroscopeConstants.FetchStatus.OK )
      {
        DoDownloadDocument = false;
      }
      
      if( DoDownloadDocument )
      {

        this.CrawledDate = DateTime.UtcNow;
              
        if( this.GetIsHtml() )
        {
          
          DebugMsg( string.Format( "IS HTML PAGE: {0}", this.DocUrl ) );
          
          fTimeDuration( this.ProcessHtmlPage );

        }
        else
        if( this.GetIsCss() )
        {
          
          DebugMsg( string.Format( "IS CSS PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessStylesheets() )
          {
            fTimeDuration( this.ProcessCssPage );
          }

        }
        else
        if( this.GetIsImage() )
        {
          
          DebugMsg( string.Format( "IS IMAGE PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessImages() )
          {
            fTimeDuration( this.ProcessImagePage );
          }

        }
        else
        if( this.GetIsJavascript() )
        {
          
          DebugMsg( string.Format( "IS JAVASCRIPT PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessJavascripts() )
          {
            fTimeDuration( this.ProcessJavascriptPage );
          }

        }
        else
        if( this.GetIsPdf() )
        {
          
          DebugMsg( string.Format( "IS PDF PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessPdfs() )
          {
            fTimeDuration( this.ProcessPdfPage );
          }

        }
        else
        if( this.GetIsXml() )
        {
          
          DebugMsg( string.Format( "IS XML PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessXml() )
          {
            fTimeDuration( this.ProcessXmlPage );
          }

        }
        else
        if( this.GetIsText() )
        {
          
          DebugMsg( string.Format( "IS TEXT PAGE: {0}", this.DocUrl ) );

          fTimeDuration( this.ProcessTextPage );

        }
        else
        if( this.GetIsAudio() )
        {
          
          DebugMsg( string.Format( "IS AUDIO PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessAudio() )
          {
            fTimeDuration( this.ProcessAudioPage );
          }
          
        }
        else
        if( this.GetIsVideo() )
        {
          
          DebugMsg( string.Format( "IS VIDEO PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessVideo() )
          {
            fTimeDuration( this.ProcessVideoPage );
          }
          
        }
        else
        if( this.GetIsBinary() )
        {
          DebugMsg( string.Format( "IS BINARY PAGE: {0}", this.DocUrl ) );
          if( MacroscopePreferencesManager.GetProcessBinaries() )
          {
            fTimeDuration( this.ProcessBinaryPage );
          }

        }
        else
        {
          DebugMsg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.DocUrl ) );
        }

      }
      else
      {
        DebugMsg( string.Format( "SKIPPING DOWNLOAD:: {0}", this.DocUrl ) );
      }

      if( this.GetTitleLength() > 0 )
      {

        this.SetTitlePixelWidth( AnalyzePageTitles.CalcTitleWidth( this.GetTitle() ) );

        if( MacroscopePreferencesManager.GetDetectLanguage() )
        {
          this.DetectTitleLanguage();
        }

      }

      if( this.GetDescriptionLength() > 0 )
      {
        if( MacroscopePreferencesManager.GetDetectLanguage() )
        {
          this.DetectDescriptionLanguage();
        }
      }

      if( this.GetBodyTextLength() > 0 )
      {
        if( MacroscopePreferencesManager.GetDetectLanguage() )
        {
          this.DetectBodyTextLanguage();
        }
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

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string OriginalUrl = this.DocUrl;
      string ResponseErrorCondition = null;
      Boolean IsAuthenticating = false;

      this.SetProcessInlinks();
      this.SetProcessHyperlinksIn();

      try
      {

        req = WebRequest.CreateHttp( this.DocUrl );
        
        req.Method = "HEAD";
        req.Timeout = this.Timeout;
        req.KeepAlive = false;
        req.AllowAutoRedirect = false;

        this.PrepareRequestHttpHeaders( req: req );

        IsAuthenticating = this.AuthenticateRequest( req );
                            
        MacroscopePreferencesManager.EnableHttpProxy( req );

        this.CrawledDate = DateTime.UtcNow;

        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "ExecuteHeadRequest :: UriFormatException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( TimeoutException ex )
      {
        DebugMsg( string.Format( "ExecuteHeadRequest :: TimeoutException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( WebException ex )
      {
        DebugMsg( string.Format( "ExecuteHeadRequest :: WebException: {0}", ex.Message ) );
        res = ( HttpWebResponse )ex.Response;
        ResponseErrorCondition = ex.Status.ToString();
      }

      DebugMsg( string.Format( "ResponseErrorCondition: {0}", ResponseErrorCondition ) );

      if( res != null )
      {

        DebugMsg( string.Format( "StatusCode: {0}", res.StatusCode ) );
              
        this.SetErrorCondition( res.StatusDescription );

        foreach( string HttpHeaderKey in res.Headers )
        {
          DebugMsg( string.Format( "HEADERS: {0} => {1}", HttpHeaderKey, res.GetResponseHeader( HttpHeaderKey ) ) );
        }

        this.ProcessResponseHttpHeaders( req, res );

        if( IsAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        if( this.IsRedirect )
        {

          this.IsRedirect = true;
          string Location = res.GetResponseHeader( "Location" );
          Location = Uri.UnescapeDataString( Location );
          string LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute( this.DocUrl, Location );

          if( !string.IsNullOrEmpty( LinkUrlAbs ) )
          {
            
            this.SetUrlRedirectFrom( Url: OriginalUrl );

            this.SetUrlRedirectTo( Url: LinkUrlAbs );
            
            MacroscopeLink OutLink = this.AddDocumentOutlink(
                                       AbsoluteUrl: LinkUrlAbs,
                                       LinkType: MacroscopeConstants.InOutLinkType.REDIRECT,
                                       Follow: true
                                     );
            
            OutLink.SetRawTargetUrl( res.GetResponseHeader( "Location" ) );
          
          }

        }

        res.Close();
        
        res.Dispose();

      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }
      
      /*
      if( res != null )
      {
        res.Dispose();
      }
      */

    }

    /**************************************************************************/

    private Boolean AuthenticateRequest ( HttpWebRequest req )
    {
      
      // Reference: https://en.wikipedia.org/wiki/Basic_access_authentication#Protocol

      Boolean IsAuthenticating = false;

      if( this.GetAuthenticationCredential() != null )
      {
       
        byte [] UsernamePassword = Encoding.UTF8.GetBytes(
                                     string.Join(
                                       ":",
                                       this.GetAuthenticationCredential().GetUsername(),
                                       this.GetAuthenticationCredential().GetPassword()
                                     )
                                   );

        string UsernamePasswordB64Encoded = System.Convert.ToBase64String( UsernamePassword );

        if( req != null )
        {
          
          req.PreAuthenticate = true;

          req.Headers.Add(
            HttpRequestHeader.Authorization,
            string.Join( " ", "Basic", UsernamePasswordB64Encoded )
          );
        
          IsAuthenticating = true;
        
        }
        
      }

      return( IsAuthenticating );
      
    }

    /** -------------------------------------------------------------------- **/

    private void VerifyOrPurgeCredential ()
    {

      if( this.GetAuthenticationCredential() != null )
      {
        DebugMsg( string.Format( "VerifyCredential: {0}", this.GetStatusCode() ) );
        
        if( this.GetStatusCode() == HttpStatusCode.Unauthorized )
        {
          
          MacroscopeCredential Credential = this.GetAuthenticationCredential();

          Credential.GetCredentialsHttp().RemoveCredential(
            Credential.GetDomain(),
            Credential.GetRealm()
          );

        }

      }

    }

    /**************************************************************************/

    private void ProcessErrorCondition ( string ResponseErrorCondition )
    {

      if( ResponseErrorCondition != null )
      {

        switch( ResponseErrorCondition.ToLower() )
        {
          case "timeout":
            this.SetStatusCode( HttpStatusCode.RequestTimeout );
            break;
          default:
            break;
        }

        this.ErrorCondition = ResponseErrorCondition;

      }
      else
      {
        this.ErrorCondition = "";
      }

    }

    /** HTTP Headers **********************************************************/

    // https://en.wikipedia.org/wiki/List_of_HTTP_header_fields
    
    private void PrepareRequestHttpHeaders ( HttpWebRequest req )
    {

      req.Host = this.GetHostname();

      req.UserAgent = this.UserAgent();

      req.Accept = "*/*";
      
      req.Headers.Add( "Accept-Charset", "utf-8, us-ascii" );

      // TODO: Add support for compressed responses
      //req.Headers.Add( "Accept-Encoding", "gzip, deflate" );

      req.Headers.Add( "Accept-Language", "*" );

      this.RawHttpRequestHeaders = req.Headers.ToString();

    }

    private void ProcessResponseHttpHeaders ( HttpWebRequest req, HttpWebResponse res )
    {

      Boolean IsRedirectUrl = false;
            
      // Status Code
      this.SetStatusCode( res.StatusCode );
      this.SetErrorCondition( res.StatusDescription );

      try
      {

        switch( this.GetStatusCode() )
        {

        // 200 Range

          case HttpStatusCode.OK:
            IsRedirectUrl = false;
            break;

        // 300 Range

          case HttpStatusCode.Moved:
            this.SetErrorCondition( HttpStatusCode.Moved.ToString() );
            IsRedirectUrl = true;
            break;

          case HttpStatusCode.SeeOther:
            this.SetErrorCondition( HttpStatusCode.SeeOther.ToString() );
            IsRedirectUrl = true;
            break;

          case HttpStatusCode.Redirect:
            this.SetErrorCondition( HttpStatusCode.Redirect.ToString() );
            IsRedirectUrl = true;
            break;

        // 400 Range

          case HttpStatusCode.BadRequest:
            this.SetErrorCondition( HttpStatusCode.BadRequest.ToString() );
            IsRedirectUrl = false;
            break;
              
          case HttpStatusCode.Unauthorized:
            this.SetErrorCondition( HttpStatusCode.Unauthorized.ToString() );
            IsRedirectUrl = false;
            break;
            
          case HttpStatusCode.PaymentRequired:
            this.SetErrorCondition( HttpStatusCode.PaymentRequired.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.Forbidden:
            this.SetErrorCondition( HttpStatusCode.Forbidden.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.NotFound:
            this.SetErrorCondition( HttpStatusCode.NotFound.ToString() );
            IsRedirectUrl = false;
            break;
              
          case HttpStatusCode.MethodNotAllowed:
            this.SetErrorCondition( HttpStatusCode.MethodNotAllowed.ToString() );
            IsRedirectUrl = false;
            break;

          case HttpStatusCode.Gone:
            this.SetErrorCondition( HttpStatusCode.Gone.ToString() );
            IsRedirectUrl = false;
            break;
              
          case HttpStatusCode.RequestUriTooLong:
            this.SetErrorCondition( HttpStatusCode.RequestUriTooLong.ToString() );
            IsRedirectUrl = false;
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

      if( IsRedirectUrl )
      {
        this.IsRedirect = true;
      }

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

        if( res.ContentType != null )
        {
          this.MimeType = res.ContentType;
        }
        else
        {
          this.MimeType = MacroscopeConstants.DefaultMimeType;
        }

        this.ContentLength = res.ContentLength;
      }

      // Probe HTTP Headers
      foreach( string HttpHeaderName in res.Headers )
      {

        //DebugMsg( string.Format( "HTTP HEADER: {0} :: {1}", HttpHeaderName, res.GetResponseHeader( sHeader ) ) );

        if( HttpHeaderName.ToLower().Equals( "www-authenticate" ) )
        {
          
          // EXAMPLE: WWW-Authenticate: Basic realm="Access to the staging site"

          string NewAuthenticationType = "";    
          string NewAuthenticationRealm = "";
          string NewAuthenticationValue = res.GetResponseHeader( HttpHeaderName );

          MatchCollection matches = Regex.Matches( NewAuthenticationValue, "^\\s*(Basic)\\s+realm=\"([^\"]+)\"", RegexOptions.IgnoreCase );

          DebugMsg( string.Format( "www-authenticate: \"{0}\"", NewAuthenticationValue ) );

          foreach( Match match in matches )
          {
            NewAuthenticationType = match.Groups[ 1 ].Value;
            NewAuthenticationRealm = match.Groups[ 2 ].Value;
          }

          DebugMsg( string.Format( "www-authenticate: \"{0}\" :: \"{1}\"", NewAuthenticationType, NewAuthenticationRealm ) );

          if( NewAuthenticationType.ToLower() == "basic" )
          {
            this.SetAuthenticationType( MacroscopeConstants.AuthenticationType.BASIC );
          }
          else
          {
            this.SetAuthenticationType( MacroscopeConstants.AuthenticationType.UNSUPPORTED );
          }

          this.SetAuthenticationRealm( NewAuthenticationRealm );

        }

        if( HttpHeaderName.ToLower().Equals( "date" ) )
        {
          string DateString = res.GetResponseHeader( HttpHeaderName );
          this.DateServer = this.ParseHttpDate( HeaderField: HttpHeaderName, DateString: DateString );
        }

        if( HttpHeaderName.ToLower().Equals( "last-modified" ) )
        {
          string DateString = res.GetResponseHeader( HttpHeaderName );
          this.DateModified = this.ParseHttpDate( HeaderField: HttpHeaderName, DateString: DateString );
        }

        if( HttpHeaderName.ToLower().Equals( "expires" ) )
        {
          string DateString = res.GetResponseHeader( HttpHeaderName );
          this.DateExpires = this.ParseHttpDate( HeaderField: HttpHeaderName, DateString: DateString );
        }

        if( HttpHeaderName.ToLower().Equals( "content-encoding" ) )
        {
          this.IsCompressed = true;
          this.CompressionMethod = res.GetResponseHeader( HttpHeaderName );
        }

        // Process HTST Policy
        // https://www.owasp.org/index.php/HTTP_Strict_Transport_Security_Cheat_Sheet
        // Strict-Transport-Security: max-age=31536000; includeSubDomains; preload
        if( HttpHeaderName.ToLower().Equals( "strict-transport-security" ) )
        {
          this.HypertextStrictTransportPolicy = true;
          // TODO: implement includeSubDomains
        }

        // Canonical HTTP Header
        // Link: <http://www.example.com/downloads/white-paper.pdf>; rel="canonical"
        if( HttpHeaderName.ToLower().Equals( "link" ) )
        {

          string Url = null;
          string Rel = null;
          string Raw = res.GetResponseHeader( HttpHeaderName );

          MatchCollection matches = Regex.Matches( Raw, "<([^<>]+)>\\s*;\\srel=\"([^\"]+)\"" );

          foreach( Match match in matches )
          {
            Url = match.Groups[ 1 ].Value;
            Rel = match.Groups[ 2 ].Value;
          }

          if( ( Rel != null ) && ( Rel.ToLower() == "canonical" ) )
          {

            if( ( Url != null ) && ( Url.Length > 0 ) )
            {
              this.SetCanonical( Url );
            }
          }

        }

        // Probe Character Set
        // TODO: implement this
        if( HttpHeaderName.ToLower().Equals( "content-type" ) )
        {
          //string NewCharSet = "";
          this.CharSet = null;
        }

        // Process Etag
        if( HttpHeaderName.ToLower().Equals( "etag" ) )
        {
          string ETag = res.GetResponseHeader( HttpHeaderName );
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
          this.DateServer = DateTime.UtcNow;
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
        Regex reIsImage = new Regex ( "^image/(gif|png|jpeg|bmp|webp|vnd.microsoft.icon|x-icon)", RegexOptions.IgnoreCase );
        Regex reIsPdf = new Regex ( "^application/pdf", RegexOptions.IgnoreCase );
        Regex reIsAudio = new Regex ( "^audio/[a-z0-9]+", RegexOptions.IgnoreCase );
        Regex reIsVideo = new Regex ( "^video/[a-z0-9]+", RegexOptions.IgnoreCase );
        Regex reIsXml = new Regex ( "^(application|text)/(atom\\+xml|xml)", RegexOptions.IgnoreCase );
        Regex reIsText = new Regex ( "^(text)/(plain)", RegexOptions.IgnoreCase );
        
        if( reIsHtml.IsMatch( this.MimeType ) )
        {
          this.SetIsHtml();
        }
        else
        if( reIsCss.IsMatch( this.MimeType ) )
        {
          this.SetIsCss();
        }
        else
        if( reIsJavascript.IsMatch( this.MimeType ) )
        {
          this.SetIsJavascript();
        }
        else
        if( reIsImage.IsMatch( this.MimeType ) )
        {
          this.SetIsImage();
        }
        else
        if( reIsPdf.IsMatch( this.MimeType ) )
        {
          this.SetIsPdf();
        }
        else
        if( reIsAudio.IsMatch( this.MimeType ) )
        {
          this.SetIsAudio();
        }
        else
        if( reIsVideo.IsMatch( this.MimeType ) )
        {
          this.SetIsVideo();
        }
        else
        if( reIsXml.IsMatch( this.MimeType ) )
        {
          this.SetIsXml();
        }
        else
        if( reIsText.IsMatch( this.MimeType ) )
        {
          this.SetIsText();
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

      Uri DocumentUri = null;

      try
      {
        DocumentUri = new Uri ( this.GetUrl(), UriKind.Absolute );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "ProcessUrlElements: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "ProcessUrlElements: {0}", ex.Message ) );
      }

      if( DocumentUri != null )
      {

        this.Scheme = DocumentUri.Scheme;
        this.Hostname = DocumentUri.Host;
        this.Port = DocumentUri.Port;
        this.Path = DocumentUri.AbsolutePath;
        this.Fragment = DocumentUri.Fragment;
        this.QueryString = DocumentUri.Query;

        if( this.Scheme.ToLower().Equals( "https" ) )
        {
          this.SetIsSecureUrl( true );
        }
      
      }
      else
      {
        this.AddRemark( "Malformed URL" );
      }

    }

    /**************************************************************************/

    private DateTime ParseHttpDate ( string HeaderField, string DateString )
    {

      DateTime ParsedDate = DateTime.UtcNow;

      try
      {
        ParsedDate = DateTime.Parse( DateString );
      }
      catch( FormatException ex )
      {
        DebugMsg( string.Format( "ParseHttpDate: {0}", ex.Message ) );
        this.AddRemark( Observation: string.Format( "Bad HTTP Date in \"{0}\": \"{1}\"", HeaderField, DateString ) );
      }

      return( ParsedDate );

    }

    /** Language Detection ****************************************************/

    
    public string ProbeTextLanguage ( string Text )
    {

      string LanguagedDetected = null;

      MacroscopeAnalyzeTextLanguage AnalyzeTextLanguage;

      if( !string.IsNullOrEmpty( Text ) )
      {

        string LanguageCode = this.GetIsoLanguageCode();

        AnalyzeTextLanguage = new MacroscopeAnalyzeTextLanguage ( IsoLanguageCode: LanguageCode );

        LanguagedDetected = AnalyzeTextLanguage.AnalyzeLanguage( Text: Text );

        AnalyzeTextLanguage = null;

      }
     
      return( LanguagedDetected );

    }

    /**************************************************************************/

  }

}
