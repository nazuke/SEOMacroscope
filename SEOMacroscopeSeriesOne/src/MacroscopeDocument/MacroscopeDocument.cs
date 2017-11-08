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
using System.Net.Http;
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

    private string ServerName;
    private List<IPAddress> ServerAddresses;

    private string BaseHref;

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
    private string RawHttpResponseStatusLine;
    private string RawHttpResponseHeaders;

    private Boolean HypertextStrictTransportPolicy;
    private Boolean IsSecureUrl;
    private List<string> InSecureLinks;
    
    private HttpStatusCode StatusCode;
    private string ErrorCondition;
    private long? ContentLength;

    private string MimeType;
    private MacroscopeConstants.DocumentType DocumentType;

    private Boolean IsCompressed;
    private string CompressionMethod;
    private string ContentEncoding;

    private string Locale;
    private string IsoLanguageCode;
    private Encoding CharacterEncoding;
    private string CharacterSet;

    private long Duration;
    private Boolean WasDownloaded;

    private DateTime DateServer;
    private DateTime DateModified;
    private DateTime DateExpires;

    private string Canonical;
    private Dictionary<string,MacroscopeHrefLang> HrefLang;
    
    private string LinkShortLink;
    private string LinkFirst;
    private string LinkPrev;
    private string LinkNext;
    private string LinkLast;

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

    private string DocumentTextRaw;
    private string DocumentTextCleaned;
    private string DocumentTextLanguage;

    private string BodyTextRaw;

    private MacroscopeLevenshteinFingerprint LevenshteinFingerprint;
    private SortedDictionary<MacroscopeDocument, int> LevenshteinNearDuplicates;

    private List<Dictionary<string,int>> DeepKeywordAnalysis;

    private double ReadabilityGrade;
    private MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod ReadabilityGradeMethod;
    private string ReadabilityGradeDescription;
    
    private int WordCount;

    private int Depth;

    private List<string> Remarks;

    private Dictionary<string, MacroscopeConstants.TextPresence> CustomFiltered;


    // Label, list of values found
    private Dictionary<string, List<string>> DataExtractedCssSelectors;
    private Dictionary<string, List<string>> DataExtractedRegexes;
    private Dictionary<string, List<string>> DataExtractedXpaths;


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

    /** Self-Destruct Sequence ************************************************/
    
    ~MacroscopeDocument ()
    {

      this.DocCollection = null;

      this.DocumentTextRaw = null;
      this.DocumentTextCleaned = null;

      this.DeepKeywordAnalysis = null;

    }

    /** -------------------------------------------------------------------- **/

    private void InitializeDocument ( string Url )
    {

      this.SuppressDebugMsg = false;

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
      this.RawHttpResponseStatusLine = "";
      this.RawHttpResponseHeaders = "";

      this.ServerName = "";
      this.ServerAddresses = new List<IPAddress> ( 1 );

      this.BaseHref = "";

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

      this.IsCompressed = false;
      this.CompressionMethod = "";
      this.ContentEncoding = "";

      this.Locale = null;
      this.IsoLanguageCode = null;
      this.CharacterEncoding = Encoding.UTF8;
      this.CharacterSet = null;

      this.Duration = 0;
      this.WasDownloaded = false;

      this.DateServer = new DateTime ();
      this.DateModified = new DateTime ();
      this.DateExpires = new DateTime ();

      this.Canonical = "";
      this.HrefLang = new Dictionary<string,MacroscopeHrefLang> ( 1024 );

      this.LinkShortLink = "";
      this.LinkFirst = "";
      this.LinkPrev = "";
      this.LinkNext = "";
      this.LinkLast = "";
    
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

      this.DocumentTextRaw = "";
      this.DocumentTextCleaned = "";
      this.DocumentTextLanguage = "";
      
      this.BodyTextRaw = "";

      this.LevenshteinFingerprint = new MacroscopeLevenshteinFingerprint( msDoc: this );
      this.ClearLevenshteinNearDuplicates();

      this.DeepKeywordAnalysis = new List<Dictionary<string,int>> ( 4 );
      for( int i = 0 ; i <= 3 ; i++ )
      {
        this.DeepKeywordAnalysis.Add( new Dictionary<string,int> ( 256 ) );
      }

      this.ReadabilityGrade = 0;
      this.ReadabilityGradeMethod = MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod.UNKNOWN;
      this.ReadabilityGradeDescription = "";
      
      this.WordCount = 0;
      
      this.Depth = MacroscopeUrlUtils.FindUrlDepth( Url );

      this.Remarks = new List<string> ();

      this.CustomFiltered = new Dictionary<string, MacroscopeConstants.TextPresence> ( 5 );

      this.DataExtractedCssSelectors = new Dictionary<string, List<string>> ( 8 );
      this.DataExtractedRegexes = new Dictionary<string, List<string>> ( 8 );
      this.DataExtractedXpaths = new Dictionary<string, List<string>> ( 8 );

    }

    /** Delegates *************************************************************/

    private TimeDuration GetTimeDurationDelegate ()
    {

      TimeDuration DelegateTimeDuration = delegate( Action ProcessMethod )
      {

        Stopwatch DelegateStopWatch = new Stopwatch ();
        long FinalDuration;

        DelegateStopWatch.Start();

        try
        {
          ProcessMethod();
        }
        catch( MacroscopeDocumentException ex )
        {
          this.DebugMsg( string.Format( "GetTimeDurationDelegate: {0}", ex.Message ) );
        }

        DelegateStopWatch.Stop();

        FinalDuration = DelegateStopWatch.ElapsedMilliseconds;

        if( FinalDuration > 0 )
        {
          this.Duration = FinalDuration;
        }
        else
        {
          this.Duration = 0;
        }

        this.DebugMsg( string.Format( "DURATION: {0} :: {1}", FinalDuration, this.Duration ) );

      };

      return( DelegateTimeDuration );

    }

    /** DocumentCollection ****************************************************/
    
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

    /** Fetch Status **********************************************************/

    public void SetFetchStatus ( MacroscopeConstants.FetchStatus Status )
    {
      this.FetchStatus = Status;
    }

    public void ClearFetchStatus ()
    {
      this.FetchStatus = MacroscopeConstants.FetchStatus.VOID;
    }

    public MacroscopeConstants.FetchStatus GetFetchStatus ()
    {
      return( this.FetchStatus );
    }

    /** Server Details ********************************************************/

    public void SetServerName ( string NewServerName )
    {
      this.ServerName = NewServerName;
    }

    public string GetServerName ()
    {
      return( this.ServerName );
    }

    /** -------------------------------------------------------------------- **/

    public IPHostEntry SetServerAddresses ()
    {

      IPHostEntry HostEntry = null;

      if( !string.IsNullOrEmpty( this.ServerName ) )
      {

        HostEntry = Dns.GetHostEntry( this.ServerName );

        this.SetServerAddresses( HostEntry: HostEntry );

      }

      return( HostEntry );

    }

    public IPHostEntry SetServerAddresses ( IPHostEntry HostEntry )
    {

      lock( this.ServerAddresses )
      {

        foreach( IPAddress Address in HostEntry.AddressList )
        {
          this.ServerAddresses.Add( Address );
        }

      }

      return( HostEntry );

    }

    public IEnumerable<IPAddress> IterateServerAddresses ()
    {
      lock( this.ServerAddresses )
      {
        foreach( IPAddress Address in this.ServerAddresses )
        {
          yield return Address;
        }
      }
    }

        
    public List<IPAddress> GetServerAddresses ()
    {

      List<IPAddress> AddressList = new List<IPAddress> ( this.ServerAddresses.Count );

      lock( this.ServerAddresses )
      {

        foreach( IPAddress Address in this.ServerAddresses )
        {
          AddressList.Add( Address );
          
        }

      }

      return( AddressList );

    }

    public string GetServerAddressesAsCsv ()
    {

      List<IPAddress> AddressList = new List<IPAddress> ( this.ServerAddresses.Count );
      List<string> AddressesListCsv = new List<string> ( this.ServerAddresses.Count );
      string AddressesCsv = "";

      lock( this.ServerAddresses )
      {

        foreach( IPAddress Address in this.ServerAddresses )
        {
          AddressesListCsv.Add( Address.ToString() );
        }

      }

      if( AddressesListCsv.Count > 0 )
      {
        AddressesCsv = string.Join( ", ", AddressesListCsv );
      }

      return( AddressesCsv );

    }

    /** Base HREF *************************************************************/

    public void UnsetBaseHref ()
    {
      this.BaseHref = "";
    }
    
    public void SetBaseHref ( string Url )
    {
      this.BaseHref = Url;
    }

    public string GetBaseHref ()
    {
      return( this.BaseHref );
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
      string Host = this.Hostname;
      return( Host );
    }

    public string GetHostAndPort ()
    {

      string HostAndPort = this.Hostname;

      if( ( this.Port > 0 ) && ( this.Port != 80 ) )
      {
        HostAndPort = string.Join( ":", this.Hostname, this.Port.ToString() );
      }

      return( HostAndPort );

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

    public void SetErrorCondition ( string Value )
    {
      this.ErrorCondition = Value;
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

    /** -------------------------------------------------------------------- **/

      // TODO: Deprecate this:
    public void SetHttpResponseStatusLine ( HttpWebResponse Response )
    {

      this.RawHttpResponseStatusLine = string.Join(
        " ",
        string.Join( "/", "HTTP", Response.ProtocolVersion ),
        ( (int) Response.StatusCode ).ToString(),
        Response.StatusDescription,
        Environment.NewLine
      );

    }

    public void SetHttpResponseStatusLine ( MacroscopeHttpTwoClientResponse Response )
    {

      this.RawHttpResponseStatusLine = string.Join(
        " ",
        string.Join( "/", "HTTP", Response.GetResponse().Version ),
        ( (int) Response.GetResponse().StatusCode).ToString(),
        Response.GetResponse().ReasonPhrase,
        Environment.NewLine
      );

    }

    public string GetHttpResponseStatusLineAsText ()
    {
      return( this.RawHttpResponseStatusLine );
    }

    /** -------------------------------------------------------------------- **/

    // TODO: Deprecate this:
    public void SetHttpResponseHeaders ( HttpWebResponse Response )
    {
      this.RawHttpResponseHeaders = Response.Headers.ToString();
    }

    public void SetHttpResponseHeaders ( MacroscopeHttpTwoClientResponse Response )
    {
      this.RawHttpResponseHeaders = Response.GetResponse().Headers.ToString();
    }

    public string GetHttpResponseHeadersAsText ()
    {
      return( this.RawHttpResponseHeaders );
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
        MatchCollection matches = Regex.Matches( this.MimeType, @"^([^\s;/]+)/([^\s;/]+)" );
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
        
    public void SetIsSkipped ()
    {
      this.DocumentType = MacroscopeConstants.DocumentType.SKIPPED;
    }

    public Boolean GetIsSkipped ()
    {
      if( this.DocumentType == MacroscopeConstants.DocumentType.SKIPPED )
      {
        return( true );
      }
      else
      {
        return( false );
      }
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

      Boolean IsHtml = false;

      if( this.DocumentType == MacroscopeConstants.DocumentType.HTML )
      {
        IsHtml = true;
      }
      else
      {
        IsHtml = false;
      }

      return( IsHtml );

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

    public long? GetContentLength ()
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

    /** Character Encoding ****************************************************/

    public Encoding GetCharacterEncoding ()
    {
      return( this.CharacterEncoding );
    }

    public void SetCharacterEncoding ( Encoding NewEncoding )
    {
      this.CharacterEncoding = NewEncoding;
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

    public void SetCanonical ( string Url )
    {
      this.Canonical = Url;
    }

    public string GetCanonical ()
    {
      return( this.Canonical );
    }

    /** HTTP Link Header URLs *************************************************/

    // https://webmasters.googleblog.com/2011/09/pagination-with-relnext-and-relprev.html

    /** -------------------------------------------------------------------- **/       

    public void SetLinkShortLink ( string Url )
    {
      this.LinkShortLink = Url;
    }

    public string GetLinkShortLink ()
    {
      return( this.LinkShortLink );
    }

    /** -------------------------------------------------------------------- **/

    public void SetLinkFirst ( string Url )
    {
      this.LinkFirst = Url;
    }

    public string GetLinkFirst ()
    {
      return( this.LinkFirst );
    }
    
    /** -------------------------------------------------------------------- **/

    public void SetLinkPrev ( string Url )
    {
      this.LinkPrev = Url;
    }

    public string GetLinkPrev ()
    {
      return( this.LinkPrev );
    }
    
    /** -------------------------------------------------------------------- **/    

    public void SetLinkNext ( string Url )
    {
      this.LinkNext = Url;
    }

    public string GetLinkNext ()
    {
      return( this.LinkNext );
    }
    
    /** -------------------------------------------------------------------- **/    

    public void SetLinkLast ( string Url )
    {
      this.LinkLast = Url;
    }

    public string GetLinkLast ()
    {
      return( this.LinkLast );
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

    public string GetDateModifiedForSitemapXml ()
    {
      return( this.DateModified.ToUniversalTime().ToString( "yyyy-MM-dd" ) );
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

    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/

    public int CountOutlinks ()
    {
      int iCount = this.Outlinks.Count();
      return( iCount );
    }

    /** -------------------------------------------------------------------- **/

    private MacroscopeLink AddDocumentOutlink (
      string AbsoluteUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      Boolean Follow
    )
    {

      MacroscopeLink OutLink;
      
      OutLink = new MacroscopeLink ( 
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

    /** -------------------------------------------------------------------- **/

    public IEnumerable<MacroscopeHyperlinkIn> IterateHyperlinksIn ()
    {

      MacroscopeHyperlinksIn DocumentHyperlinksIn = this.DocCollection.GetDocumentHyperlinksIn( this.GetUrl() );

      lock( DocumentHyperlinksIn )
      {

        foreach( MacroscopeHyperlinkIn Link in DocumentHyperlinksIn.IterateLinks() )
        {
          yield return Link;
        }

      }
      
    }

    /** -------------------------------------------------------------------- **/

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
       
    /** -------------------------------------------------------------------- **/

    public void UnsetProcessHyperlinksIn ()
    {
      this.ProcessHyperlinksIn = false;
    }
    
    /** -------------------------------------------------------------------- **/

    public Boolean GetProcessHyperlinksIn ()
    {
      return( this.ProcessHyperlinksIn );
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeHyperlinksOut GetHyperlinksOut ()
    {
      return( this.HyperlinksOut );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<MacroscopeHyperlinkOut> IterateHyperlinksOut ()
    {
      lock( this.HyperlinksOut )
      {
        foreach( MacroscopeHyperlinkOut Link in this.HyperlinksOut.IterateLinks() )
        {
          yield return Link;
        }
      }
    }

    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/

    public int CountHyperlinksOut ()
    {

      return( this.HyperlinksOut.Count() );

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

        try
        {
          Value = HtmlEntity.DeEntitize( TitleText );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", this.GetUrl() ) );
          DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", TitleText ) );
          DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", ex.Message ) );
        }

      }

      Value = Regex.Replace( Value, @"[\s]+", " ", RegexOptions.Singleline );
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
        string [] KeywordsList = Regex.Split( this.Keywords, @"[\s,]+" );
        Count = KeywordsList.GetLength( 0 );
      }

      return( Count );

    }

    /** AltText ***************************************************************/

    public void SetAltText (
      string AltText,
      MacroscopeConstants.TextProcessingMode ProcessingMode
      )
    {

      string Value = AltText;

      if( ProcessingMode == MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES )
      {

        if( !string.IsNullOrEmpty( AltText ) )
        {

          try
          {
            Value = HtmlEntity.DeEntitize( AltText );
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", this.GetUrl() ) );
            DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", AltText ) );
            DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", ex.Message ) );
          }

        }

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

      MacroscopeHrefLang HrefLangAlternate = new MacroscopeHrefLang ( LocaleProcessed, Url );

      this.HrefLang[ LocaleProcessed ] = HrefLangAlternate;

    }

    /** -------------------------------------------------------------------- **/
    
    public Dictionary<string,MacroscopeHrefLang> GetHrefLangs ()
    {
      return( this.HrefLang );
    }
    
    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string,MacroscopeHrefLang>> IterateHrefLangs ()
    {
      lock( this.HrefLang )
      {
        foreach( string Key  in this.HrefLang.Keys )
        {
          KeyValuePair<string,MacroscopeHrefLang> KP = new KeyValuePair<string,MacroscopeHrefLang> ( Key, this.HrefLang[ Key ] );
          yield return KP;
        }
      }
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

    /** Entire Document Text **************************************************/

    public void SetDocumentText ( string Text )
    {

      if( !string.IsNullOrEmpty( Text ) )
      {

        this.DocumentTextRaw = MacroscopeStringTools.CompactWhiteSpace( Text: Text );
        this.DocumentTextRaw = MacroscopeStringTools.StripHtmlDocTypeAndCommentsFromText( Text: this.DocumentTextRaw );
        
        this.DocumentTextCleaned = MacroscopeStringTools.CleanDocumentText( msDoc: this );
        
        if( !string.IsNullOrEmpty( this.DocumentTextCleaned ) )
        {
          this.SetWordCount();
        }
        
        if( MacroscopePreferencesManager.GetAnalyzeTextReadability() )
        {
          this.CalculateReadabilityGrade();
        }
        
      }
      else
      {
        this.DocumentTextRaw = "";
        this.DocumentTextCleaned = "";
      }
          
    }

    /** -------------------------------------------------------------------- **/
        
    public string GetDocumentTextRaw ()
    {
      return( this.DocumentTextRaw );
    }
    
    public string GetDocumentTextCleaned ()
    {
      return( this.DocumentTextCleaned );
    }
    
    /** -------------------------------------------------------------------- **/
        
    public int GetDocumentTextRawLength ()
    {
      return( this.DocumentTextRaw.Length );
    }

    public int GetDocumentTextCleanedLength ()
    {
      return( this.DocumentTextCleaned.Length );
    }

    /** -------------------------------------------------------------------- **/

    public void DetectDocumentTextLanguage ()
    {
      if( this.GetIsHtml() || this.GetIsPdf() )
      {
        this.DocumentTextLanguage = this.ProbeTextLanguage( Text: this.DocumentTextCleaned );
      }
    }

    /** -------------------------------------------------------------------- **/
        
    public string GetDocumentTextLanguage ()
    {
      return( this.DocumentTextLanguage );
    }

    /** -------------------------------------------------------------------- **/

    private void SetWordCount ()
    {
          
      this.WordCount = 0;

      try
      {
        string [] Words = Regex.Split( this.DocumentTextCleaned, @"\s+", RegexOptions.Singleline );
        this.WordCount = Words.Length;
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "SetWordCount: {0}", ex.Message ) );
      }

    }

    public int GetWordCount ()
    {
      return( this.WordCount );
    }

    /** Body Text *************************************************************/

    public void SetBodyText ( string Text )
    {

      string TextCopy = Text;
      
      if( !string.IsNullOrEmpty( TextCopy ) )
      {

        TextCopy = MacroscopeStringTools.StripHtmlDocTypeAndCommentsFromText( Text: TextCopy );

        TextCopy = MacroscopeStringTools.CompactWhiteSpace( Text: Text );

      }
      else
      {
        TextCopy = "";
      }

      this.BodyTextRaw = TextCopy;

      if( !string.IsNullOrEmpty( this.BodyTextRaw ) )
      {

        if( MacroscopePreferencesManager.GetAnalyzeTextReadability() )
        {
          this.CalculateReadabilityGrade();
        }
      
      }

      this.SetLevenshteinFingerprint();

    }

    /** -------------------------------------------------------------------- **/
        
    public string GetBodyTextRaw ()
    {
      return( this.BodyTextRaw );
    }

    /** Levenshtein Fingerprint ***********************************************/

    private void SetLevenshteinFingerprint ()
    {
      this.LevenshteinFingerprint.Analyze();
    }

    /** -------------------------------------------------------------------- **/

    public string GetLevenshteinFingerprint ()
    {
      return ( this.LevenshteinFingerprint.GetFingerprint() );
    }

    /** -------------------------------------------------------------------- **/

    public void ClearLevenshteinNearDuplicates ()
    {
      if( this.LevenshteinNearDuplicates != null )
      {
        lock( this.LevenshteinNearDuplicates )
        {
          this.LevenshteinNearDuplicates = new SortedDictionary<MacroscopeDocument, int>();
        }
      }
      else
      {
        this.LevenshteinNearDuplicates = new SortedDictionary<MacroscopeDocument, int>();
      }
    }

    /** -------------------------------------------------------------------- **/

    public SortedDictionary<MacroscopeDocument, int> GetLevenshteinNearDuplicates ()
    {
      SortedDictionary<MacroscopeDocument, int> Copy = new SortedDictionary<MacroscopeDocument, int>();
      lock( this.LevenshteinNearDuplicates )
      {
        foreach( MacroscopeDocument msDocKey in this.LevenshteinNearDuplicates.Keys )
        {
          this.LevenshteinNearDuplicates.Add( msDocKey, this.LevenshteinNearDuplicates[msDocKey] );
        }
      }
      return ( Copy );
    }

    /** -------------------------------------------------------------------- **/

    public void AddLevenshteinNearDuplicates ( MacroscopeDocument msDocNearDuplicate, int EditDistance )
    {
      lock( this.LevenshteinNearDuplicates )
      {
        if( !this.LevenshteinNearDuplicates.ContainsKey( msDocNearDuplicate ) )
        {
          this.LevenshteinNearDuplicates.Add( msDocNearDuplicate, EditDistance );
        }
      }
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
                Text: this.GetDocumentTextCleaned(), 
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
    
    /** Text Readability ******************************************************/

    public void CalculateReadabilityGrade ()
    {
      
      double Grade = 0;
      string GradeDescription = "";
      MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod GradeMethod = MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod.UNKNOWN;
      IMacroscopeAnalyzeReadability AnalyzeReadability = null;
      
      if( this.GetIsRedirect() )
      {
        return;
      }
            
      if( this.GetIsHtml() || this.GetIsPdf() )
      {
        switch( this.GetIsoLanguageCode() )
        {
          case "x-default":
            AnalyzeReadability = MacroscopeAnalyzeReadability.AnalyzerFactory( msDoc: this );
            break;
          case "en":
            AnalyzeReadability = MacroscopeAnalyzeReadability.AnalyzerFactory( msDoc: this );
            break;
          default:
            break;
        }
      }

      if( AnalyzeReadability != null )
      {
        Grade = AnalyzeReadability.AnalyzeReadability( msDoc: this );
        GradeMethod = AnalyzeReadability.GetAnalyzeReadabilityMethod();
        GradeDescription = AnalyzeReadability.GradeToString();
      }

      this.ReadabilityGrade = Grade;
      this.ReadabilityGradeMethod = GradeMethod;
      this.ReadabilityGradeDescription = GradeDescription;

    }

    public double GetReadabilityGrade ()
    {
      return( this.ReadabilityGrade );
    }
    
    public MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod GetReadabilityGradeMethod ()
    {
      return( this.ReadabilityGradeMethod );
    }

    public string GetReadabilityGradeDescription ()
    {
      return( this.ReadabilityGradeDescription );
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
        this.DebugMsg( string.Format( "GetCustomFilteredItem: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "GetCustomFilteredItem: {0}", ex.Message ) );
      }

      return( Pair );

    }

    /** Data Extractors *******************************************************/

    /** CSS Selectors ------------------------------------------------------ **/

    public void SetDataExtractedCssSelectors ( string Label, string Text )
    {
      List<string> Items = null;
      if( this.DataExtractedCssSelectors.ContainsKey( Label ) )
      {
        Items = this.DataExtractedCssSelectors[ Label ];
      }
      else
      {
        Items = new List<string> ( 8 );
        this.DataExtractedCssSelectors.Add( Label, Items );
      }
      Items.Add( Text );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string,string>> IterateDataExtractedCssSelectors ()
    {
      lock( this.DataExtractedCssSelectors )
      {
        foreach( string Label in this.DataExtractedCssSelectors.Keys )
        {
          if( this.DataExtractedCssSelectors.ContainsKey( Label ) )
          {
            List<string> Items = this.DataExtractedCssSelectors[ Label ];
            lock( Items )
            {
              foreach( string Text in Items )
              {
                KeyValuePair<string,string> Pair = new KeyValuePair<string, string> ( Label, Text );
                yield return Pair;
              }
            }
          }
        }
      }
    }

    /** Regexes ------------------------------------------------------------ **/

    public void SetDataExtractedRegexes ( string Label, string Text )
    {
      List<string> Items = null;
      if( this.DataExtractedRegexes.ContainsKey( Label ) )
      {
        Items = this.DataExtractedRegexes[ Label ];
      }
      else
      {
        Items = new List<string> ( 8 );
        this.DataExtractedRegexes.Add( Label, Items );
      }
      Items.Add( Text );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string,string>> IterateDataExtractedRegexes ()
    {
      lock( this.DataExtractedRegexes )
      {
        foreach( string Label in this.DataExtractedRegexes.Keys )
        {
          if( this.DataExtractedRegexes.ContainsKey( Label ) )
          {
            List<string> Items = this.DataExtractedRegexes[ Label ];
            lock( Items )
            {
              foreach( string Text in Items )
              {
                KeyValuePair<string,string> Pair = new KeyValuePair<string, string> ( Label, Text );
                yield return Pair;
              }
            }
          }
        }
      }
    }

    /** XPaths ------------------------------------------------------------- **/

    public void SetDataExtractedXpaths ( string Label, string Text )
    {
      List<string> Items = null;
      if( this.DataExtractedXpaths.ContainsKey( Label ) )
      {
        Items = this.DataExtractedXpaths[ Label ];
      }
      else
      {
        Items = new List<string> ( 8 );
        this.DataExtractedXpaths.Add( Label, Items );
      }
      Items.Add( Text );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string,string>> IterateDataExtractedXpaths ()
    {
      lock( this.DataExtractedXpaths )
      {
        foreach( string Label in this.DataExtractedXpaths.Keys )
        {
          if( this.DataExtractedXpaths.ContainsKey( Label ) )
          {
            List<string> Items = this.DataExtractedXpaths[ Label ];
            lock( Items )
            {
              foreach( string Text in Items )
              {
                KeyValuePair<string,string> Pair = new KeyValuePair<string, string> ( Label, Text );
                yield return Pair;
              }
            }
          }
        }
      }
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
        this.DebugMsg( string.Format( "ProcessUrlElements: {0}", ex.Message ) );
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
        this.DebugMsg( string.Format( "REDIRECT DETECTED: {0}", this.DocUrl ) );
        return( true );
      }

      if( !MacroscopePreferencesManager.GetFetchExternalLinks() )
      {
        if( this.GetIsExternal() )
        {
          DoDownloadDocument = false;
        }
      }

      if( this.DocCollection == null )
      {
        this.SetFetchStatus( Status: MacroscopeConstants.FetchStatus.OK );
      }
      else
      if( this.GetFetchStatus() == MacroscopeConstants.FetchStatus.VOID )
      {
        DoDownloadDocument = true;
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
          
          this.DebugMsg( string.Format( "IS HTML PAGE: {0}", this.DocUrl ) );
          
          fTimeDuration( this.ProcessHtmlPage );

        }
        else
        if( this.GetIsCss() )
        {
          
          this.DebugMsg( string.Format( "IS CSS PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessStylesheets() )
          {
            fTimeDuration( this.ProcessCssPage );
          }

        }
        else
        if( this.GetIsImage() )
        {
          
          this.DebugMsg( string.Format( "IS IMAGE PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessImages() )
          {
            fTimeDuration( this.ProcessImagePage );
          }

        }
        else
        if( this.GetIsJavascript() )
        {
          
          this.DebugMsg( string.Format( "IS JAVASCRIPT PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessJavascripts() )
          {
            fTimeDuration( this.ProcessJavascriptPage );
          }

        }
        else
        if( this.GetIsPdf() )
        {
          
          this.DebugMsg( string.Format( "IS PDF PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessPdfs() )
          {
            fTimeDuration( this.ProcessPdfPage );
          }

        }
        else
        if( this.GetIsXml() )
        {
          
          this.DebugMsg( string.Format( "IS XML PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessXml() )
          {
            fTimeDuration( this.ProcessXmlPage );
          }

        }
        else
        if( this.GetIsText() )
        {
          
          this.DebugMsg( string.Format( "IS TEXT PAGE: {0}", this.DocUrl ) );

          fTimeDuration( this.ProcessTextPage );

        }
        else
        if( this.GetIsAudio() )
        {
          
          this.DebugMsg( string.Format( "IS AUDIO PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessAudio() )
          {
            fTimeDuration( this.ProcessAudioPage );
          }
          
        }
        else
        if( this.GetIsVideo() )
        {
          
          this.DebugMsg( string.Format( "IS VIDEO PAGE: {0}", this.DocUrl ) );
          
          if( MacroscopePreferencesManager.GetProcessVideo() )
          {
            fTimeDuration( this.ProcessVideoPage );
          }
          
        }
        else
        if( this.GetIsBinary() )
        {
          this.DebugMsg( string.Format( "IS BINARY PAGE: {0}", this.DocUrl ) );
          if( MacroscopePreferencesManager.GetProcessBinaries() )
          {
            fTimeDuration( this.ProcessBinaryPage );
          }

        }
        else
        {
          this.DebugMsg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.DocUrl ) );
        }

      }
      else
      {

        this.DebugMsg( string.Format( "SKIPPING DOWNLOAD:: {0}", this.DocUrl ) );

        this.SetIsSkipped();

        this.CrawledDate = DateTime.UtcNow;

        this.DebugMsg( string.Format( "IS SKIPPED PAGE: {0}", this.DocUrl ) );
          
        fTimeDuration( this.ProcessSkippedPage );

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

      if( this.GetDocumentTextRawLength() > 0 )
      {
        if( MacroscopePreferencesManager.GetDetectLanguage() )
        {
          this.DetectDocumentTextLanguage();
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
        this.DebugMsg( string.Format( "ProcessUrlElements: {0}", ex.Message ) );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "ProcessUrlElements: {0}", ex.Message ) );
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
