/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Threading;
using System.Threading.Tasks;

using HtmlAgilityPack;

namespace SEOMacroscope
{

  /// <summary>
  /// MacroscopeDocument is a representation of the document found at a crawled URL.
  /// </summary>

  public partial class MacroscopeDocument : Macroscope, IDisposable
  {

    /**************************************************************************/

    private MacroscopeDocumentCollection DocCollection;

    private bool IsDirty;

    private MacroscopeConstants.FetchStatus FetchStatus;

    private DateTime CrawledDate;

    private string DocUrl;

    private string Checksum;
    private string Etag;

    private bool AllowedByRobots;

    private bool IsExternal;
    private bool Forced;

    private bool IsRedirect;
    private string UrlRedirectFrom;
    private string UrlRedirectFromRaw;
    private string UrlRedirectTo;
    private string UrlRedirectToRaw;

    private string ServerName; // HTTP Server Identifier

    private string BaseHref;

    private string Scheme;
    private string Hostname;
    private int Port;
    private string Path;
    private string QueryString;
    private string Fragment;

    private List<IPAddress> ServerAddresses;

    private MacroscopeConstants.AuthenticationType AuthenticationType;
    private string AuthenticationRealm;
    private MacroscopeCredential AuthenticationCredential;

    private string RawHttpRequestHeaders;
    private object RawHttpRequestHeadersLocker = new object();
    private string RawHttpResponseStatusLine;
    private string RawHttpResponseHeaders;

    private bool HypertextStrictTransportPolicy;
    private bool IsSecureUrl;
    private List<string> InSecureLinks;

    private HttpStatusCode StatusCode;
    private string ErrorCondition;
    private long? ContentLength;

    private string MimeType;
    private MacroscopeConstants.DocumentType DocumentType;

    private bool IsCompressed;
    private string CompressionMethod;
    private string ContentEncoding;

    private string Locale;
    private string IsoLanguageCode;
    private Encoding CharacterEncoding;
    private string CharacterSet;

    private long Duration;
    private bool WasDownloaded;

    private DateTime DateServer;
    private DateTime DateModified;
    private DateTime DateExpires;

    private string Canonical;
    private Dictionary<string, MacroscopeHrefLang> HrefLang;

    private string LinkShortLink;
    private string LinkFirst;
    private string LinkPrev;
    private string LinkNext;
    private string LinkLast;

    private Dictionary<string, string> MetaHeaders;

    // Inbound links
    private bool ProcessInlinks;

    // Outbound links to pages and linked assets to follow
    //private Dictionary<string,MacroscopeLink> Outlinks;
    private MacroscopeLinkList Outlinks;

    // Inbound hypertext links
    private bool ProcessHyperlinksIn;

    // Outbound hypertext links
    private MacroscopeHyperlinksOut HyperlinksOut;

    private Dictionary<string, string> EmailAddresses;
    private Dictionary<string, string> TelephoneNumbers;

    private MacroscopeAnalyzePageTitles AnalyzePageTitles;

    private string Title;
    private string TitleLanguage;
    private int TitlePixelWidth;
    private string Author;
    private string Description;
    private string DescriptionLanguage;
    private string Keywords;
    private string AltText;

    private Dictionary<ushort, List<string>> Headings;

    private string DocumentTextRaw;
    private string DocumentTextCleaned;
    private string DocumentTextLanguage;

    private string BodyTextRaw;

    private MacroscopeLevenshteinFingerprint LevenshteinFingerprint;
    private SortedDictionary<MacroscopeDocument, int> LevenshteinNearDuplicates;

    private List<Dictionary<string, int>> DeepKeywordAnalysis;

    private double ReadabilityGrade;
    private MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod ReadabilityGradeMethod;
    private string ReadabilityGradeDescription;

    private int WordCount;

    private int Depth;

    private Dictionary<string, string> Remarks;

    private Dictionary<string, MacroscopeConstants.TextPresence> CustomFiltered;

    // Label, list of values found
    private Dictionary<string, List<string>> DataExtractedCssSelectors;
    private Dictionary<string, List<string>> DataExtractedRegexes;
    private Dictionary<string, List<string>> DataExtractedXpaths;

    /** Constructors **********************************************************/

    public MacroscopeDocument (
      string Url
    )
    {
      this.InitializeDocument( Url );
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDocument (
      MacroscopeDocumentCollection DocumentCollection,
      string Url
    )
    {
      this.InitializeDocument( Url );
      this.SetDocumentCollection( DocumentCollection );
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDocument (
      MacroscopeCredential Credential,
      string Url
    )
    {
      this.InitializeDocument( Url );
      this.AuthenticationCredential = Credential;
    }

    /** -------------------------------------------------------------------- **/

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

    /** -------------------------------------------------------------------- **/

    private void InitializeDocument ( string Url )
    {

      this.SuppressDebugMsg = true;

      this.DocCollection = null;

      this.IsDirty = true;

      this.ClearFetchStatus();

      this.CrawledDate = DateTime.UtcNow;

      this.DocUrl = Url;

      this.Checksum = "";
      this.Etag = "";

      this.AllowedByRobots = true;

      this.IsExternal = false;
      this.Forced = false;

      this.IsRedirect = false;
      this.UrlRedirectFrom = "";
      this.UrlRedirectTo = "";

      this.RawHttpRequestHeaders = "";
      this.RawHttpResponseStatusLine = "";
      this.RawHttpResponseHeaders = "";

      this.ServerName = "";
      this.ServerAddresses = new List<IPAddress>( 1 );

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
      this.InSecureLinks = new List<string>( 128 );

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

      this.DateServer = new DateTime();
      this.DateModified = new DateTime();
      this.DateExpires = new DateTime();

      this.Canonical = "";
      this.HrefLang = new Dictionary<string, MacroscopeHrefLang>( 1024 );

      this.LinkShortLink = "";
      this.LinkFirst = "";
      this.LinkPrev = "";
      this.LinkNext = "";
      this.LinkLast = "";

      this.MetaHeaders = new Dictionary<string, string>( 8 );

      this.ProcessInlinks = false;
      this.Outlinks = new MacroscopeLinkList();

      this.ProcessHyperlinksIn = false;
      this.HyperlinksOut = new MacroscopeHyperlinksOut();

      this.EmailAddresses = new Dictionary<string, string>( 256 );
      this.TelephoneNumbers = new Dictionary<string, string>( 256 );

      this.AnalyzePageTitles = new MacroscopeAnalyzePageTitles();

      this.Title = "";
      this.TitleLanguage = "";
      this.TitlePixelWidth = 0;
      this.Author = "";
      this.Description = "";
      this.DescriptionLanguage = "";
      this.Keywords = "";
      this.AltText = "";

      this.Headings = new Dictionary<ushort, List<string>>( 7 );

      for( ushort HeadingLevel = 1 ; HeadingLevel <= 6 ; HeadingLevel++ )
      {
        this.Headings.Add( HeadingLevel, new List<string>( 16 ) );
      }

      this.DocumentTextRaw = "";
      this.DocumentTextCleaned = "";
      this.DocumentTextLanguage = "";

      this.BodyTextRaw = "";

      this.LevenshteinFingerprint = new MacroscopeLevenshteinFingerprint( msDoc: this );
      this.ClearLevenshteinNearDuplicates();

      this.DeepKeywordAnalysis = new List<Dictionary<string, int>>( 4 );
      for( int i = 0 ; i <= 3 ; i++ )
      {
        this.DeepKeywordAnalysis.Add( new Dictionary<string, int>( 256 ) );
      }

      this.ReadabilityGrade = 0;
      this.ReadabilityGradeMethod = MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod.UNKNOWN;
      this.ReadabilityGradeDescription = "";

      this.WordCount = 0;

      this.Depth = MacroscopeHttpUrlUtils.FindUrlDepth( Url: Url );

      this.Remarks = new Dictionary<string, string>();

      this.CustomFiltered = new Dictionary<string, MacroscopeConstants.TextPresence>( 5 );

      this.DataExtractedCssSelectors = new Dictionary<string, List<string>>( 8 );
      this.DataExtractedRegexes = new Dictionary<string, List<string>>( 8 );
      this.DataExtractedXpaths = new Dictionary<string, List<string>>( 8 );

    }

    /** Self Destruct Sequence ************************************************/

    public void Dispose ()
    {
      Dispose( true );
    }

    protected virtual void Dispose ( bool disposing )
    {
      if( this.AnalyzePageTitles != null )
      {
        this.AnalyzePageTitles.Dispose();
      }
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

    public bool GetIsDirty ()
    {
      return ( this.IsDirty );
    }

    /** Fetch Status **********************************************************/

    public void SetFetchStatus ( MacroscopeConstants.FetchStatus Status )
    {
      this.FetchStatus = Status;
    }

    /** -------------------------------------------------------------------- **/

    public void ClearFetchStatus ()
    {
      this.FetchStatus = MacroscopeConstants.FetchStatus.VOID;
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeConstants.FetchStatus GetFetchStatus ()
    {
      return ( this.FetchStatus );
    }

    /** Server Details ********************************************************/

    public void SetServerName ( string NewServerName )
    {
      this.ServerName = NewServerName;
    }

    /** -------------------------------------------------------------------- **/

    public string GetServerName ()
    {
      return ( this.ServerName );
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
      return ( this.BaseHref );
    }

    /** Host Details **********************************************************/

    public Uri GetUri ()
    {
      return ( new Uri( this.DocUrl, UriKind.Absolute ) );
    }

    /** -------------------------------------------------------------------- **/

    public string GetUrl ()
    {
      return ( this.DocUrl );
    }

    /** -------------------------------------------------------------------- **/

    public string GetScheme ()
    {
      return ( this.Scheme );
    }

    /** -------------------------------------------------------------------- **/

    public string GetHostname ()
    {
      string Host = this.Hostname;
      return ( Host );
    }

    /** -------------------------------------------------------------------- **/

    public string GetHostAndPort ()
    {

      string HostAndPort = this.Hostname;

      if( ( this.Port > 0 ) && ( this.Port != 80 ) )
      {
        HostAndPort = string.Join( ":", this.Hostname, this.Port.ToString() );
      }

      return ( HostAndPort );

    }

    /** -------------------------------------------------------------------- **/

    public int GetPort ()
    {
      return ( this.Port );
    }

    /** -------------------------------------------------------------------- **/

    public string GetPath ()
    {
      return ( this.Path );
    }

    /** -------------------------------------------------------------------- **/

    public string GetFragment ()
    {
      return ( this.Fragment );
    }

    /** -------------------------------------------------------------------- **/

    public string GetQueryString ()
    {
      return ( this.QueryString );
    }

    /** Host Addresses ********************************************************/

    public IPHostEntry SetHostAddresses ()
    {

      IPHostEntry HostEntry = null;

      if( !string.IsNullOrEmpty( this.GetHostname() ) )
      {

        HostEntry = Dns.GetHostEntry( this.GetHostname() );

        this.SetHostAddresses( HostEntry: HostEntry );

      }

      return ( HostEntry );

    }

    /** -------------------------------------------------------------------- **/

    public IPHostEntry SetHostAddresses ( IPHostEntry HostEntry )
    {

      lock( this.ServerAddresses )
      {

        foreach( IPAddress Address in HostEntry.AddressList )
        {
          this.ServerAddresses.Add( Address );
        }

      }

      return ( HostEntry );

    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<IPAddress> IterateHostAddresses ()
    {
      lock( this.ServerAddresses )
      {
        foreach( IPAddress Address in this.ServerAddresses )
        {
          yield return Address;
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public List<IPAddress> GetHostAddresses ()
    {

      List<IPAddress> AddressList = new List<IPAddress>( this.ServerAddresses.Count );

      lock( this.ServerAddresses )
      {

        foreach( IPAddress Address in this.ServerAddresses )
        {
          AddressList.Add( Address );

        }

      }

      return ( AddressList );

    }

    /** -------------------------------------------------------------------- **/

    public string GetHostAddressesAsCsv ()
    {

      List<IPAddress> AddressList = new List<IPAddress>( this.ServerAddresses.Count );
      List<string> AddressesListCsv = new List<string>( this.ServerAddresses.Count );
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

      return ( AddressesCsv );

    }

    /** Authentication ********************************************************/

    public void SetAuthenticationType ( MacroscopeConstants.AuthenticationType AuthenticationType )
    {
      this.AuthenticationType = AuthenticationType;
    }

    public MacroscopeConstants.AuthenticationType GetAuthenticationType ()
    {
      return ( this.AuthenticationType );
    }

    public void SetAuthenticationRealm ( string AuthenticationRealm )
    {
      this.AuthenticationRealm = AuthenticationRealm;
    }

    public string GetAuthenticationRealm ()
    {
      return ( this.AuthenticationRealm );
    }

    private MacroscopeCredential GetAuthenticationCredential ()
    {
      return ( this.AuthenticationCredential );
    }

    /** Secure URLs ***********************************************************/

    public void SetIsSecureUrl ( bool State )
    {
      this.IsSecureUrl = State;
    }

    public bool GetIsSecureUrl ()
    {
      return ( this.IsSecureUrl );
    }

    public void AddInsecureLink ( string Url )
    {
      this.InSecureLinks.Add( Url );
    }

    public List<string> GetInsecureLinks ()
    {
      List<string> DocList = new List<string>( 128 );
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
      return ( DocList );
    }

    /** Checksum Value ********************************************************/

    public void SetChecksum ( string ChecksumValue )
    {
      this.Checksum = this.GenerateChecksum( ChecksumValue );
    }

    public string GetChecksum ()
    {
      return ( this.Checksum );
    }

    private string GenerateChecksum ( string RawData )
    {
      HashAlgorithm Digest = HashAlgorithm.Create( "SHA256" );
      byte[] BytesIn = Encoding.UTF8.GetBytes( RawData );
      byte[] Hashed = Digest.ComputeHash( BytesIn );
      StringBuilder sbString = new StringBuilder();
      for( int i = 0 ; i < Hashed.Length ; i++ )
      {
        sbString.Append( Hashed[ i ].ToString( "X2" ) );
      }
      string ChecksumValue = sbString.ToString();
      return ( ChecksumValue );
    }

    /** Etag Value ************************************************************/

    public void SetEtag ( string EtagValue )
    {
      this.Etag = EtagValue;
    }

    public string GetEtag ()
    {
      return ( this.Etag );
    }

    /** Robots ****************************************************************/

    public void SetAllowedByRobots ( bool Value )
    {
      this.AllowedByRobots = Value;
    }

    public bool GetAllowedByRobots ()
    {
      return ( this.AllowedByRobots );
    }

    public string GetAllowedByRobotsAsString ()
    {
      string Value = "";
      if( this.AllowedByRobots )
      {
        Value = "ALLOWED";
      }
      else
      {
        Value = "DISALLOWED";
      }
      return ( Value );
    }

    /** Is External Flag ******************************************************/

    public void SetIsInternal ( bool State )
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

    public bool GetIsInternal ()
    {
      if( this.IsExternal )
      {
        return ( false );
      }
      else
      {
        return ( true );
      }
    }

    /** -------------------------------------------------------------------- **/

    public void SetIsExternal ( bool State )
    {
      this.IsExternal = State;
    }

    public bool GetIsExternal ()
    {
      return ( this.IsExternal );
    }

    /** Forced Flag ***********************************************************/

    public void SetForced ()
    {
      this.Forced = true;
    }

    /** -------------------------------------------------------------------- **/

    public void ClearForced ()
    {
      this.Forced = false;
    }

    /** -------------------------------------------------------------------- **/

    public bool GetForced ()
    {
      return ( this.Forced );
    }

    /** Is Redirect Flag ******************************************************/

    public void SetIsRedirect ()
    {
      this.IsRedirect = true;
    }

    /** -------------------------------------------------------------------- **/

    public void SetIsNotRedirect ()
    {
      this.IsRedirect = false;
    }

    /** -------------------------------------------------------------------- **/

    public bool GetIsRedirect ()
    {
      return ( this.IsRedirect );
    }

    /** -------------------------------------------------------------------- **/

    public void SetUrlRedirectFrom ( string Url )
    {
      this.UrlRedirectFrom = Url;
      this.UrlRedirectFromRaw = Url;
    }

    /** -------------------------------------------------------------------- **/

    public string GetUrlRedirectFrom ()
    {
      return ( this.UrlRedirectFrom );
    }

    /** -------------------------------------------------------------------- **/

    public string GetUrlRedirectFromRaw ()
    {
      return ( this.UrlRedirectFromRaw );
    }

    /** -------------------------------------------------------------------- **/

    public void SetUrlRedirectTo ( string Url )
    {

      this.UrlRedirectToRaw = Url;

      if( !string.IsNullOrEmpty( Url ) )
      {

        string UrlUnescaped = Uri.UnescapeDataString( stringToUnescape: Url );

        string UrlAbsolute = MacroscopeHttpUrlUtils.MakeUrlAbsolute(
          BaseHref: this.GetBaseHref(),
          BaseUrl: this.GetUrl(),
          Url: UrlUnescaped
         );

        if( !string.IsNullOrEmpty( UrlAbsolute ) )
        {
          this.UrlRedirectTo = UrlAbsolute;
        }

      }
      else
      {
        this.UrlRedirectTo = "";
      }

    }

    /** -------------------------------------------------------------------- **/

    public string GetUrlRedirectTo ()
    {
      return ( this.UrlRedirectTo );
    }

    /** -------------------------------------------------------------------- **/

    public string GetUrlRedirectToRaw ()
    {
      return ( this.UrlRedirectToRaw );
    }

    /**************************************************************************/

    public void SetStatusCode ( HttpStatusCode Status )
    {
      this.StatusCode = Status;
      return;
    }

    public HttpStatusCode GetStatusCode ()
    {
      return ( this.StatusCode );
    }

    /**************************************************************************/

    public void SetErrorCondition ( string Value )
    {
      this.ErrorCondition = Value;
    }

    public string GetErrorCondition ()
    {

      string Value = "";

      if( this.ErrorCondition != null )
      {
        Value = this.ErrorCondition;
      }

      return ( Value );

    }

    /** HTTP Headers **********************************************************/

    public void SetHttpRequestHeaders ( string Text )
    {
      lock( this.RawHttpRequestHeadersLocker )
      {
        this.RawHttpRequestHeaders = Text;
      }
    }

    public string GetHttpRequestHeadersAsText ()
    {
      return ( this.RawHttpRequestHeaders );
    }

    /** -------------------------------------------------------------------- **/

    public void SetHttpResponseStatusLine ( MacroscopeHttpTwoClientResponse Response )
    {

      this.RawHttpResponseStatusLine = string.Join(
        " ",
        string.Join( "/", "HTTP", Response.GetResponse().Version ),
        ( (int) Response.GetResponse().StatusCode ).ToString(),
        Response.GetResponse().ReasonPhrase,
        Environment.NewLine
      );

    }

    public string GetHttpResponseStatusLineAsText ()
    {
      return ( this.RawHttpResponseStatusLine );
    }

    /** -------------------------------------------------------------------- **/

    public void SetHttpResponseHeaders ( MacroscopeHttpTwoClientResponse Response )
    {
      this.RawHttpResponseHeaders = Response.GetResponse().Headers.ToString();
    }

    public string GetHttpResponseHeadersAsText ()
    {
      return ( this.RawHttpResponseHeaders );
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
      return ( DocumentMimeType );
    }

    /** Document Type Methods *************************************************/

    public void SetDocumentType ( MacroscopeConstants.DocumentType Type )
    {
      this.DocumentType = Type;
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeConstants.DocumentType GetDocumentType ()
    {
      return ( this.DocumentType );
    }

    /** -------------------------------------------------------------------- **/

    public bool IsDocumentType ( MacroscopeConstants.DocumentType Type )
    {
      if( this.DocumentType == Type )
      {
        return ( true );
      }
      return ( false );
    }

    /** -------------------------------------------------------------------- **/

    public void SetIsSkipped ()
    {
      this.SetDocumentType( Type: MacroscopeConstants.DocumentType.SKIPPED );
    }

    /** Compression ***********************************************************/

    public bool GetIsCompressed ()
    {
      return ( this.IsCompressed );
    }

    public string GetCompressionMethod ()
    {
      return ( this.CompressionMethod );
    }

    /** Content Length ********************************************************/

    public void SetContentLength ( long Length )
    {
      this.ContentLength = Length;
    }

    public void SetContentLength ( int Length )
    {
      this.ContentLength = (long) Length;
    }

    /** -------------------------------------------------------------------- **/

    public long? GetContentLength ()
    {
      return ( this.ContentLength );
    }

    /** Language/Locale *******************************************************/

    public void SetLocale ( string NewLocale )
    {
      this.Locale = NewLocale;
    }

    /** -------------------------------------------------------------------- **/

    public string GetLocale ()
    {
      return ( this.Locale );
    }

    /** -------------------------------------------------------------------- **/

    public void SetIsoLanguageCode ( string LanguageCode )
    {
      this.IsoLanguageCode = LanguageCode;
    }

    /** -------------------------------------------------------------------- **/

    public string GetIsoLanguageCode ()
    {
      return ( this.IsoLanguageCode );
    }

    /** Character Encoding ****************************************************/

    public Encoding GetCharacterEncoding ()
    {
      return ( this.CharacterEncoding );
    }

    /** -------------------------------------------------------------------- **/

    public void SetCharacterEncoding ( Encoding NewEncoding )
    {
      this.CharacterEncoding = NewEncoding;
    }

    /** Character Set *********************************************************/

    public string GetCharacterSet ()
    {
      return ( this.CharacterSet );
    }

    /** -------------------------------------------------------------------- **/

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
      return ( this.Canonical );
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
      return ( this.LinkShortLink );
    }

    /** -------------------------------------------------------------------- **/

    public void SetLinkFirst ( string Url )
    {
      this.LinkFirst = Url;
    }

    public string GetLinkFirst ()
    {
      return ( this.LinkFirst );
    }

    /** -------------------------------------------------------------------- **/

    public void SetLinkPrev ( string Url )
    {
      this.LinkPrev = Url;
    }

    public string GetLinkPrev ()
    {
      return ( this.LinkPrev );
    }

    /** -------------------------------------------------------------------- **/

    public void SetLinkNext ( string Url )
    {
      this.LinkNext = Url;
    }

    public string GetLinkNext ()
    {
      return ( this.LinkNext );
    }

    /** -------------------------------------------------------------------- **/

    public void SetLinkLast ( string Url )
    {
      this.LinkLast = Url;
    }

    public string GetLinkLast ()
    {
      return ( this.LinkLast );
    }

    /** Dates *****************************************************************/

    public string GetCrawledDate ()
    {
      return ( this.CrawledDate.ToUniversalTime().ToString( "r" ) );
    }

    /** -------------------------------------------------------------------- **/

    public string GetDateServer ()
    {
      return ( this.DateServer.ToUniversalTime().ToString( "r" ) );
    }

    /** -------------------------------------------------------------------- **/

    public string GetDateModified ()
    {
      return ( this.DateModified.ToUniversalTime().ToString( "r" ) );
    }

    /** -------------------------------------------------------------------- **/

    public string GetDateModifiedForSitemapXml ()
    {
      return ( this.DateModified.ToUniversalTime().ToString( "yyyy-MM-dd" ) );
    }

    /** -------------------------------------------------------------------- **/

    public string GetDateExpires ()
    {
      return ( this.DateExpires.ToUniversalTime().ToString( "r" ) );
    }

    /** Inlinks ***************************************************************/

    public void SetProcessInlinks ()
    {
      this.ProcessInlinks = true;
    }

    /** -------------------------------------------------------------------- **/

    public void UnsetProcessInlinks ()
    {
      this.ProcessInlinks = false;
    }

    /** -------------------------------------------------------------------- **/

    public bool GetProcessInlinks ()
    {
      return ( this.ProcessInlinks );
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeLinkList GetLinksIn ()
    {
      MacroscopeLinkList DocumentLinksIn = this.DocCollection.GetDocumentInlinks( this.GetUrl() );
      return ( DocumentLinksIn );
    }

    /** -------------------------------------------------------------------- **/

    public int CountInlinks ()
    {

      MacroscopeLinkList LinksIn = this.GetLinksIn();
      int Count = 0;

      if( LinksIn != null )
      {
        Count = LinksIn.Count();
      }

      return ( Count );

    }

    /** Outlinks **************************************************************/

    public MacroscopeLinkList GetOutlinks ()
    {
      return ( this.Outlinks );
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
      int Count = this.Outlinks.Count();
      return ( Count );
    }

    /** -------------------------------------------------------------------- **/

    private MacroscopeLink AddDocumentOutlink (
      string AbsoluteUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      bool Follow
    )
    {

      MacroscopeLink OutLink = null;

      OutLink = new MacroscopeLink(
        SourceUrl: this.GetUrl(),
        TargetUrl: AbsoluteUrl,
        LinkType: LinkType,
        Follow: Follow
      );

      this.Outlinks.Add( OutLink );

      return ( OutLink );

    }

    /** Hyperlinks In *********************************************************/

    public MacroscopeHyperlinksIn GetHyperlinksIn ()
    {

      MacroscopeHyperlinksIn DocumentHyperlinksIn = this.DocCollection.GetDocumentHyperlinksIn( this.GetUrl() );

      return ( DocumentHyperlinksIn );

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

      return ( Count );
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

    public bool GetProcessHyperlinksIn ()
    {
      return ( this.ProcessHyperlinksIn );
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeHyperlinksOut GetHyperlinksOut ()
    {
      return ( this.HyperlinksOut );
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

        MacroscopeDocument msDoc = this.DocCollection.GetDocumentByUrl( Url: Url );

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

      return ( this.HyperlinksOut.Count() );

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

    public Dictionary<string, string> GetEmailAddresses ()
    {
      return ( this.EmailAddresses );
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

    public Dictionary<string, string> GetTelephoneNumbers ()
    {
      return ( this.TelephoneNumbers );
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
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", this.GetUrl() ) );
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", TitleText ) );
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", ex.Message ) );
        }

      }

      Value = Regex.Replace( Value, @"[\s]+", " ", RegexOptions.Singleline );
      Value = Value.Trim();

      this.Title = Value;

    }

    /** -------------------------------------------------------------------- **/

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
      return ( TitleValue );
    }

    /** -------------------------------------------------------------------- **/

    public int GetTitleLength ()
    {
      return ( this.Title.Length );
    }

    /** -------------------------------------------------------------------- **/

    public void SetTitlePixelWidth ( int Width )
    {
      this.TitlePixelWidth = Width;
    }

    /** -------------------------------------------------------------------- **/

    public int GetTitlePixelWidth ()
    {
      return ( this.TitlePixelWidth );
    }

    /** -------------------------------------------------------------------- **/

    public void DetectTitleLanguage ()
    {

      bool Proceed = false;

      switch( this.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Proceed = true;
          break;
        default:
          break;
      }

      if( Proceed )
      {
        this.TitleLanguage = this.ProbeTextLanguage( Text: this.Title );
      }
    }

    /** -------------------------------------------------------------------- **/

    public string GetTitleLanguage ()
    {
      return ( this.TitleLanguage );
    }

    /** Author ***********************************************************/

    public void SetAuthor (
      string AuthorText,
      MacroscopeConstants.TextProcessingMode ProcessingMode
    )
    {

      string Value = AuthorText;

      if( ProcessingMode == MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES )
      {

        try
        {
          Value = HtmlEntity.DeEntitize( AuthorText );
        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", this.GetUrl() ) );
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", AuthorText ) );
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", ex.Message ) );
        }

      }

      Value = Regex.Replace( Value, @"[\s]+", " ", RegexOptions.Singleline );
      Value = Value.Trim();

      this.Author = Value;

    }

    /** -------------------------------------------------------------------- **/

    public string GetAuthor ()
    {
      string AuthorValue;
      if( this.Author != null )
      {
        AuthorValue = this.Author;
      }
      else
      {
        AuthorValue = "";
      }
      return ( AuthorValue );
    }

    /** Description ***********************************************************/

    public void SetDescription (
      string DescriptionText,
      MacroscopeConstants.TextProcessingMode ProcessingMode
    )
    {

      string Value = DescriptionText;

      if( ProcessingMode == MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES )
      {

        try
        {
          Value = HtmlEntity.DeEntitize( DescriptionText );
        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", this.GetUrl() ) );
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", DescriptionText ) );
          this.DebugMsg( string.Format( "HtmlEntity.DeEntitize: {0}", ex.Message ) );
        }

      }

      Value = Regex.Replace( Value, @"[\s]+", " ", RegexOptions.Singleline );
      Value = Value.Trim();

      this.Description = Value;

    }

    /** -------------------------------------------------------------------- **/

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
      return ( DescriptionValue );
    }

    /** -------------------------------------------------------------------- **/

    public int GetDescriptionLength ()
    {
      return ( this.GetDescription().Length );
    }

    /** -------------------------------------------------------------------- **/

    public void DetectDescriptionLanguage ()
    {

      bool Proceed = false;

      switch( this.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Proceed = true;
          break;
        default:
          break;
      }

      if( Proceed )
      {
        this.DescriptionLanguage = this.ProbeTextLanguage( Text: this.Description );
      }

    }

    public string GetDescriptionLanguage ()
    {
      return ( this.DescriptionLanguage );
    }

    /** Keywords **************************************************************/

    public void SetKeywords ( string KeywordsText )
    {

      if( ( !string.IsNullOrEmpty( KeywordsText ) ) && ( !string.IsNullOrWhiteSpace( KeywordsText ) ) )
      {
        this.Keywords = HtmlEntity.DeEntitize( KeywordsText );
      }
      else
      {
        this.Keywords = "";
      }

    }

    /** -------------------------------------------------------------------- **/

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

      return ( KeywordsValue );

    }

    /** -------------------------------------------------------------------- **/

    public int GetKeywordsLength ()
    {

      int Length = 0;

      if( this.Keywords != null )
      {
        Length = this.Keywords.Length;
      }

      return ( Length );

    }

    /** -------------------------------------------------------------------- **/

    public int GetKeywordsCount ()
    {

      int Count = 0;

      if(
        ( this.Keywords != null )
        && ( this.Keywords.Length > 0 ) )
      {
        string[] KeywordsList = Regex.Split( this.Keywords, @"[\s,]+" );
        Count = KeywordsList.GetLength( 0 );
      }

      return ( Count );

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
      return ( Value );
    }

    /** -------------------------------------------------------------------- **/

    public int GetAltTextLength ()
    {
      return ( this.AltText.Length );
    }

    /** HrefLang **************************************************************/

    private void SetHreflang ( string HrefLangLocale, string Url )
    {

      string LocaleProcessed = HrefLangLocale.ToLower();
      MacroscopeHrefLang HrefLangAlternate = new MacroscopeHrefLang( JobMaster: this.DocCollection.GetJobMaster(), Locale: LocaleProcessed, Url: Url );

      this.HrefLang[ LocaleProcessed ] = HrefLangAlternate;

    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, MacroscopeHrefLang> GetHrefLangs ()
    {
      return ( this.HrefLang );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string, MacroscopeHrefLang>> IterateHrefLangs ()
    {
      lock( this.HrefLang )
      {
        foreach( string Key in this.HrefLang.Keys )
        {
          KeyValuePair<string, MacroscopeHrefLang> KP = new KeyValuePair<string, MacroscopeHrefLang>( Key, this.HrefLang[ Key ] );
          yield return KP;
        }
      }
    }

    /** META Headers **********************************************************/

    public IEnumerable<KeyValuePair<string, string>> IterateMetaTags ()
    {
      lock( this.MetaHeaders )
      {
        foreach( string Key in this.MetaHeaders.Keys )
        {
          KeyValuePair<string, string> KP = new KeyValuePair<string, string>( Key, this.MetaHeaders[ Key ] );
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
      return ( Value );
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
      List<string> HeadingList = new List<string>();
      if( this.Headings.ContainsKey( HeadingLevel ) )
      {
        HeadingList = this.Headings[ HeadingLevel ];
      }
      return ( HeadingList );
    }

    /** Entire Document Text **************************************************/

    public void SetDocumentText ( string Text )
    {

      if( !string.IsNullOrEmpty( Text ) )
      {

        if( this.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) || this.IsDocumentType( Type: MacroscopeConstants.DocumentType.PDF ) )
        {

          this.DocumentTextRaw = MacroscopeStringTools.CompactWhiteSpace( Text: Text );
          this.DocumentTextRaw = MacroscopeStringTools.StripHtmlDocTypeAndCommentsFromText( Text: this.DocumentTextRaw );

          this.DocumentTextCleaned = MacroscopeStringTools.CleanDocumentText( msDoc: this );

          if( !string.IsNullOrEmpty( this.DocumentTextCleaned ) )
          {
            this.SetWordCount();
          }

        }

        if( this.IsDocumentType( Type: MacroscopeConstants.DocumentType.TEXT )
          || this.IsDocumentType( Type: MacroscopeConstants.DocumentType.SITEMAPTEXT )
          || this.IsDocumentType( Type: MacroscopeConstants.DocumentType.XML )
          || this.IsDocumentType( Type: MacroscopeConstants.DocumentType.SITEMAPXML )
          )
        {
          this.DocumentTextRaw = Text;
          this.DocumentTextCleaned = Text;
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
      return ( this.DocumentTextRaw );
    }

    public string GetDocumentTextCleaned ()
    {
      return ( this.DocumentTextCleaned );
    }

    /** -------------------------------------------------------------------- **/

    public int GetDocumentTextRawLength ()
    {
      return ( this.DocumentTextRaw.Length );
    }

    public int GetDocumentTextCleanedLength ()
    {
      return ( this.DocumentTextCleaned.Length );
    }

    /** -------------------------------------------------------------------- **/

    public void DetectDocumentTextLanguage ()
    {

      bool Proceed = false;

      switch( this.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Proceed = true;
          break;
        default:
          break;
      }

      if( Proceed )
      {
        this.DocumentTextLanguage = this.ProbeTextLanguage( Text: this.DocumentTextCleaned );
      }

    }

    /** -------------------------------------------------------------------- **/

    public string GetDocumentTextLanguage ()
    {
      return ( this.DocumentTextLanguage );
    }

    /** Word Count ************************************************************/

    private void SetWordCount ()
    {
      if( this.GetStatusCode() == HttpStatusCode.OK )
      {
        this.WordCount = MacroscopeAnalyzeWordCount.CountWords( Text: this.DocumentTextCleaned );
      }
    }

    /** -------------------------------------------------------------------- **/

    public int GetWordCount ()
    {
      return ( this.WordCount );
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
      return ( this.BodyTextRaw );
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
          this.LevenshteinNearDuplicates.Add( msDocKey, this.LevenshteinNearDuplicates[ msDocKey ] );
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

      bool Proceed = false;

      switch( this.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Proceed = true;
          break;
        default:
          break;
      }

      if( this.GetStatusCode() != HttpStatusCode.OK )
      {
        Proceed = false;
      }

      if( Proceed )
      {

        MacroscopeDeepKeywordAnalysis AnalyzeKeywords = new MacroscopeDeepKeywordAnalysis();

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

    public Dictionary<string, int> GetDeepKeywordAnalysisAsDictonary ( int Words )
    {
      int WordsOffset = Words - 1;
      Dictionary<string, int> Terms = new Dictionary<string, int>( this.DeepKeywordAnalysis.Count );
      lock( this.DeepKeywordAnalysis )
      {
        foreach( string Term in this.DeepKeywordAnalysis[ WordsOffset ].Keys )
        {
          Terms.Add( Term, this.DeepKeywordAnalysis[ WordsOffset ][ Term ] );
        }
      }
      return ( Terms );
    }

    /** Text Readability ******************************************************/

    public void CalculateReadabilityGrade ()
    {

      bool Proceed = false;
      double Grade = 0;
      string GradeDescription = "";
      MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod GradeMethod = MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod.UNKNOWN;
      IMacroscopeAnalyzeReadability AnalyzeReadability = null;

      if( this.GetIsRedirect() )
      {
        return;
      }

      switch( this.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Proceed = true;
          break;
        default:
          break;
      }

      if( Proceed )
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
      return ( this.ReadabilityGrade );
    }

    public MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod GetReadabilityGradeMethod ()
    {
      return ( this.ReadabilityGradeMethod );
    }

    public string GetReadabilityGradeDescription ()
    {
      return ( this.ReadabilityGradeDescription );
    }

    /** Durations *************************************************************/

    public void SetDuration ( long lDuration )
    {
      this.Duration = lDuration;
    }

    public long GetDuration ()
    {
      return ( this.Duration );
    }

    public decimal GetDurationInSeconds ()
    {
      decimal DurationSeconds = (decimal) ( (decimal) this.GetDuration() / (decimal) 1000 );
      return ( DurationSeconds );
    }

    public string GetDurationInSecondsFormatted ()
    {
      decimal DurationSeconds = this.GetDurationInSeconds();
      string DurationText = DurationSeconds.ToString( "0.00" );
      return ( DurationText );
    }

    public void SetWasDownloaded ( bool State )
    {
      this.WasDownloaded = State;
    }

    public bool GetWasDownloaded ()
    {
      return ( this.WasDownloaded );
    }

    /** Page Depth ************************************************************/

    public int GetDepth ()
    {
      return ( this.Depth );
    }

    /** Remarks ***************************************************************/

    public void AddRemark ( string RemarkName, string Observation )
    {
      lock( this.Remarks )
      {
        if( this.Remarks.ContainsKey( RemarkName ) )
        {
          this.Remarks[ RemarkName ] = Observation;
        }
        else
        {
          this.Remarks.Add( RemarkName, Observation );
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public void RemoveRemark ( string RemarkName )
    {
      lock( this.Remarks )
      {
        if( this.Remarks.ContainsKey( RemarkName ) )
        {
          this.Remarks.Remove( RemarkName );
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string, string>> IterateRemarks ()
    {
      lock( this.Remarks )
      {
        foreach( string RemarkName in this.Remarks.Keys )
        {
          KeyValuePair<string, string> RemarkPair = new KeyValuePair<string, string>();
          yield return RemarkPair;
        }
      }
    }

    /** Custom Filtered Values ************************************************/

    public void SetCustomFiltered ( string Text, MacroscopeConstants.TextPresence Presence )
    {
      lock( this.CustomFiltered )
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

    public Dictionary<string, MacroscopeConstants.TextPresence> GetCustomFiltered ()
    {

      Dictionary<string, MacroscopeConstants.TextPresence> ListCopy = new Dictionary<string, MacroscopeConstants.TextPresence>( this.CustomFiltered.Count );

      lock( this.CustomFiltered )
      {

        foreach( string Key in this.CustomFiltered.Keys )
        {
          ListCopy.Add( Key, this.CustomFiltered[ Key ] );
        }

      }

      return ( ListCopy );

    }

    /** -------------------------------------------------------------------- **/

    public KeyValuePair<string, MacroscopeConstants.TextPresence> GetCustomFilteredItem ( string Text )
    {

      KeyValuePair<string, MacroscopeConstants.TextPresence> Pair;

      Pair = new KeyValuePair<string, MacroscopeConstants.TextPresence>(
        null,
        MacroscopeConstants.TextPresence.UNDEFINED
      );

      try
      {

        Pair = new KeyValuePair<string, MacroscopeConstants.TextPresence>(
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

      return ( Pair );

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
        Items = new List<string>( 8 );
        this.DataExtractedCssSelectors.Add( Label, Items );
      }
      Items.Add( Text );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string, string>> IterateDataExtractedCssSelectors ()
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
                KeyValuePair<string, string> Pair = new KeyValuePair<string, string>( Label, Text );
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
        Items = new List<string>( 8 );
        this.DataExtractedRegexes.Add( Label, Items );
      }
      Items.Add( Text );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string, string>> IterateDataExtractedRegexes ()
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
                KeyValuePair<string, string> Pair = new KeyValuePair<string, string>( Label, Text );
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
        Items = new List<string>( 8 );
        this.DataExtractedXpaths.Add( Label, Items );
      }
      Items.Add( Text );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<KeyValuePair<string, string>> IterateDataExtractedXpaths ()
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
                KeyValuePair<string, string> Pair = new KeyValuePair<string, string>( Label, Text );
                yield return Pair;
              }
            }
          }
        }
      }
    }

    /** Executor **************************************************************/

    public async Task<bool> Execute ()
    {

      bool DoDownloadDocument = true;

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

      await this.ExecuteHeadRequest();

      switch( this.GetStatusCode()  )
      {
        case HttpStatusCode.RequestTimeout:
          return ( false );
        case HttpStatusCode.GatewayTimeout:
          return ( false );
        default:
          break;
      }

      if( this.GetIsRedirect() )
      {
        this.DebugMsg( string.Format( "REDIRECT DETECTED: {0}", this.DocUrl ) );
        return ( true );
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

      /*
       * Force GET request for web servers that return 404 on HEAD requests:
       */
      if( this.GetStatusCode() == HttpStatusCode.NotFound )
      {
        if( MacroscopePreferencesManager.GetProbeHead404sWithGet() )
        {
          DoDownloadDocument = true;
        }
      }

      if( DoDownloadDocument )
      {

        this.CrawledDate = DateTime.UtcNow;

        switch( this.GetDocumentType() )
        {
          case MacroscopeConstants.DocumentType.HTML:
            this.DebugMsg( string.Format( "IS HTML PAGE: {0}", this.DocUrl ) );
            await this.ProcessHtmlPage();
            break;
          case MacroscopeConstants.DocumentType.CSS:
            this.DebugMsg( string.Format( "IS CSS PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessStylesheets() )
            {
              await this.ProcessCssPage();
            }
            break;
          case MacroscopeConstants.DocumentType.IMAGE:
            this.DebugMsg( string.Format( "IS IMAGE PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessImages() )
            {
              await this.ProcessImagePage();
            }
            break;
          case MacroscopeConstants.DocumentType.JAVASCRIPT:
            this.DebugMsg( string.Format( "IS JAVASCRIPT PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessJavascripts() )
            {
              await this.ProcessJavascriptPage();
            }
            break;
          case MacroscopeConstants.DocumentType.PDF:
            this.DebugMsg( string.Format( "IS PDF PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessPdfs() )
            {
              await this.ProcessPdfPage();
            }
            break;
          case MacroscopeConstants.DocumentType.XML:
            this.DebugMsg( string.Format( "IS XML PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessXml() )
            {
              await this.ProcessXmlPage();
            }
            break;
          case MacroscopeConstants.DocumentType.SITEMAPXML:
            this.DebugMsg( string.Format( "IS SITEMAP XML PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessXml() )
            {
              await this.ProcessXmlPage();
            }
            break;
          case MacroscopeConstants.DocumentType.SITEMAPTEXT:
            this.DebugMsg( string.Format( "IS SITEMAP TEXT PAGE: {0}", this.DocUrl ) );
            await this.ProcessTextPage();
            break;
          case MacroscopeConstants.DocumentType.TEXT:
            this.DebugMsg( string.Format( "IS TEXT PAGE: {0}", this.DocUrl ) );
            await this.ProcessTextPage();
            break;
          case MacroscopeConstants.DocumentType.AUDIO:
            this.DebugMsg( string.Format( "IS AUDIO PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessAudio() )
            {
              await this.ProcessAudioPage();
            }
            break;
          case MacroscopeConstants.DocumentType.VIDEO:
            this.DebugMsg( string.Format( "IS VIDEO PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessVideo() )
            {
              await this.ProcessVideoPage();
            }
            break;
          case MacroscopeConstants.DocumentType.BINARY:
            this.DebugMsg( string.Format( "IS BINARY PAGE: {0}", this.DocUrl ) );
            if( MacroscopePreferencesManager.GetProcessBinaries() )
            {
              await this.ProcessBinaryPage();
            }
            break;
          default:
            this.DebugMsg( string.Format( "UNKNOWN PAGE TYPE: {0}", this.DocUrl ) );
            break;
        }

      }
      else
      {

        this.DebugMsg( string.Format( "SKIPPING DOWNLOAD:: {0}", this.DocUrl ) );

        this.SetIsSkipped();

        this.CrawledDate = DateTime.UtcNow;

        this.DebugMsg( string.Format( "IS SKIPPED PAGE: {0}", this.DocUrl ) );

        await this.ProcessSkippedPage();

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
        MacroscopeInsecureLinks InsecureLinks = new MacroscopeInsecureLinks();
        InsecureLinks.Analyze( this );
      }

      if( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
      {
        this.ExecuteDeepKeywordAnalysis();
      }

      return ( true );

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
        DocumentUri = new Uri( this.GetUrl(), UriKind.Absolute );
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
        this.AddRemark( "MALFORMED_URL", "Malformed URL" );
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

        AnalyzeTextLanguage = new MacroscopeAnalyzeTextLanguage( IsoLanguageCode: LanguageCode );

        LanguagedDetected = AnalyzeTextLanguage.AnalyzeLanguage( Text: Text );

        AnalyzeTextLanguage = null;

      }

      return ( LanguagedDetected );

    }

    /**************************************************************************/

  }

}
