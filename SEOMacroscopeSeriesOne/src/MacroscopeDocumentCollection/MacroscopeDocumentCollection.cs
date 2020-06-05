/*

	This file is part of SEOMacroscope.

	Copyright 2020 Jason Holland.

	The GitHub repository may be found at:

		https://github.com/nazuke/SEOMacroscope

	SEOMacroscope is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	SEOMacroscope is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Text.RegularExpressions;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  [Serializable()]
  public class MacroscopeDocumentCollection : Macroscope, IDisposable
  {

    // TODO: Implement scheduled Levenshtein analysis in this class

    /**************************************************************************/

    private ConcurrentDictionary<ulong, MacroscopeDocument> DocCollection;

    private MacroscopeJobMaster JobMaster;
    private MacroscopeNamedQueue<bool> NamedQueue;
    private MacroscopeSearchIndex SearchIndex;
    private MacroscopeDeepKeywordAnalysis DeepAnalyzeKeywords;
    private MacroscopeKeywordPresenceAnalysis AnalyzeKeywordsPresence;

    private string StartUrl;

    private Dictionary<string, MacroscopeLinkList> StructInlinks;
    private Dictionary<string, MacroscopeHyperlinksIn> StructHyperlinksIn;
    private Dictionary<string, List<decimal>> StructHyperlinksRatio;

    private Dictionary<ulong, bool> StatsHistory;
    private Dictionary<string, int> StatsHostnames;

    private Dictionary<string, bool> StatsCanonicals;

    private Dictionary<ulong, int> StatsTitles;
    private Dictionary<ulong, int> StatsDescriptions;
    private Dictionary<ulong, int> StatsKeywords;

    private List<Dictionary<ulong, int>> StatsHeadings;

    private Dictionary<string, int> StatsWarnings;
    private Dictionary<string, int> StatsErrors;
    private Dictionary<string, int> StatsChecksums;

    private Dictionary<string, int> StatsDocumentTypesInternal;
    private Dictionary<string, int> StatsDocumentTypesExternal;

    private Dictionary<string, int> StatsLanguagesPages;
    private Dictionary<string, int> StatsLanguagesTitles;
    private Dictionary<string, int> StatsLanguagesDescriptions;
    private Dictionary<string, int> StatsLanguagesBodyTexts;

    private Dictionary<string, decimal> StatsDurations;

    private Dictionary<string, MacroscopeDocumentList> StatsDeepKeywordAnalysisDocs;
    private List<Dictionary<string, int>> StatsDeepKeywordAnalysis;

    private Dictionary<ulong, List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>>> StatsKeywordPresenceAnalysis;

    private Dictionary<string, int> StatsReadabilityGrades;
    private Dictionary<string, int> StatsReadabilityGradeDescriptions;

    private ulong StatsUrlsInternal;
    private ulong StatsUrlsExternal;
    private ulong StatsUrlsSitemaps;

    private Dictionary<string, IPHostEntry> DnsCache;

    private MacroscopeClickPathAnalysis ClickPathAnalysis;

    private Dictionary<string, Dictionary<string, Dictionary<string, string>>> StatsSitemapErrors;
    private List<MacroscopeDocumentList> DocumentsInSitemaps;

    private MacroscopeDocumentList OrphanedDocumentList;

    private List<List<MacroscopeRedirectChainDocStruct>> AnalyzedRedirectChains;

    private static object LockerRecalc = new object();

    [field: NonSerialized()]
    private System.Timers.Timer TimerRecalc;

    /**************************************************************************/

    public MacroscopeDocumentCollection ( MacroscopeJobMaster JobMaster )
    {

      this.SuppressDebugMsg = true;

      this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZING..." );

      this.DocCollection = new ConcurrentDictionary<ulong, MacroscopeDocument>();

      this.JobMaster = JobMaster;

      this.NamedQueue = new MacroscopeNamedQueue<bool>();
      this.NamedQueue.CreateNamedQueue( MacroscopeConstants.RecalculateDocCollection );

      this.SearchIndex = new MacroscopeSearchIndex();

      this.StartUrl = null;

      this.StructInlinks = new Dictionary<string, MacroscopeLinkList>( 1024 );
      this.StructHyperlinksIn = new Dictionary<string, MacroscopeHyperlinksIn>( 1024 );
      this.StructHyperlinksRatio = new Dictionary<string, List<decimal>>( 1024 );

      this.StatsHistory = new Dictionary<ulong, bool>( 1024 );
      this.StatsHostnames = new Dictionary<string, int>( 16 );

      this.StatsCanonicals = new Dictionary<string, bool>( 1024 );

      this.StatsTitles = new Dictionary<ulong, int>( 256 );
      this.StatsDescriptions = new Dictionary<ulong, int>( 256 );
      this.StatsKeywords = new Dictionary<ulong, int>( 256 );

      this.StatsHeadings = new List<Dictionary<ulong, int>>( 7 );
      for ( ushort i = 1; i <= 6; i++ )
      {
        this.StatsHeadings.Add( new Dictionary<ulong, int>( 256 ) );
      }

      this.StatsWarnings = new Dictionary<string, int>( 32 );
      this.StatsErrors = new Dictionary<string, int>( 32 );
      this.StatsChecksums = new Dictionary<string, int>( 1024 );

      this.StatsDocumentTypesInternal = new Dictionary<string, int>( 8 );
      this.StatsDocumentTypesExternal = new Dictionary<string, int>( 8 );

      this.StatsLanguagesPages = new Dictionary<string, int>( 8 );
      this.StatsLanguagesTitles = new Dictionary<string, int>( 8 );
      this.StatsLanguagesDescriptions = new Dictionary<string, int>( 8 );
      this.StatsLanguagesBodyTexts = new Dictionary<string, int>( 8 );

      this.StatsDurations = new Dictionary<string, decimal>( 1024 );

      this.StatsDeepKeywordAnalysisDocs = new Dictionary<string, MacroscopeDocumentList>( 1024 );
      this.StatsDeepKeywordAnalysis = new List<Dictionary<string, int>>( 4 );
      for ( int i = 0; i < 4; i++ )
      {
        this.StatsDeepKeywordAnalysis.Add( new Dictionary<string, int>( 1024 ) );
      }

      this.DeepAnalyzeKeywords = new MacroscopeDeepKeywordAnalysis( DocList: this.StatsDeepKeywordAnalysisDocs );

      this.AnalyzeKeywordsPresence = new MacroscopeKeywordPresenceAnalysis();
      this.StatsKeywordPresenceAnalysis = new Dictionary<ulong, List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>>>();

      this.StatsReadabilityGrades = new Dictionary<string, int>( 16 );
      this.StatsReadabilityGradeDescriptions = new Dictionary<string, int>( 16 );

      this.StatsUrlsInternal = 0;
      this.StatsUrlsExternal = 0;
      this.StatsUrlsSitemaps = 0;

      this.DnsCache = new Dictionary<string, IPHostEntry>( 16 );

      this.ClickPathAnalysis = new MacroscopeClickPathAnalysis( DocumentCollection: this );

      this.StatsSitemapErrors = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
      this.DocumentsInSitemaps = new List<MacroscopeDocumentList>( 2 );

      this.OrphanedDocumentList = new MacroscopeDocumentList();

      this.AnalyzedRedirectChains = new List<List<MacroscopeRedirectChainDocStruct>>();

      this.StartRecalcTimer();

      this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZED." );

    }

    /** Self Destruct Sequence ************************************************/

    public void Dispose ()
    {
      Dispose( true );
    }

    /** -------------------------------------------------------------------- **/

    protected virtual void Dispose ( bool disposing )
    {
      if ( this.TimerRecalc != null )
      {
        this.TimerRecalc.Dispose();
      }
    }

    /** Job Master Methods ****************************************************/

    public MacroscopeJobMaster GetJobMaster ()
    {
      return ( this.JobMaster );
    }

    /** Start URL *************************************************************/

    public void SetStartUrl ( string Url )
    {
      this.StartUrl = Url;
    }

    /** -------------------------------------------------------------------- **/

    public string GetStartUrl ()
    {
      return ( this.StartUrl );
    }

    /** Gather List from all OutLinks in DocCollection ************************/

    public List<string> FetchUrlsFromOutLinks ()
    {
      List<string> UrlsFromOutLinks = new List<string>( this.DocCollection.Count * 10 );
      lock ( this.DocCollection )
      {
        foreach ( ulong DocKey in this.DocCollection.Keys )
        {
          MacroscopeDocument msDoc = this.DocCollection[DocKey];
          foreach ( MacroscopeLink OutLink in msDoc.IterateOutlinks() )
          {
            string OutLinkUrl = OutLink.GetTargetUrl();
            if ( !UrlsFromOutLinks.Contains( OutLinkUrl ) )
            {
              UrlsFromOutLinks.Add( OutLinkUrl );
            }
          }
        }
      }
      return ( UrlsFromOutLinks );
    }

    /** Allowed Hosts Methods *************************************************/

    public MacroscopeAllowedHosts GetAllowedHosts ()
    {
      return ( this.JobMaster.GetAllowedHosts() );
    }

    /** Document Collection Methods *******************************************/

    public bool ContainsDocument ( MacroscopeDocument msDoc )
    {
      return ( this.ContainsDocument( Url: msDoc.GetUrl() ) );
    }

    /** -------------------------------------------------------------------- **/

    public bool ContainsDocument ( string Url )
    {

      bool DocumentPresent = false;
      ulong DocKey = UrlToDigest( Url: Url );

      if ( this.DocCollection.ContainsKey( DocKey ) )
      {
        DocumentPresent = true;
      }

      return ( DocumentPresent );

    }

    /** Create Document Methods ***********************************************/

    public MacroscopeDocument CreateDocument ( string Url )
    {

      MacroscopeDocument msDoc;

      msDoc = new MacroscopeDocument(
        DocumentCollection: this,
        Url: Url
      );

      this.AddDocument( msDoc: msDoc );

      return ( msDoc );

    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDocument CreateDocument ( MacroscopeCredential Credential, string Url )
    {

      MacroscopeDocument msDoc;

      msDoc = new MacroscopeDocument(
        DocumentCollection: this,
        Url: Url,
        Credential: Credential
      );

      this.AddDocument( msDoc: msDoc );

      return ( msDoc );

    }

    /** Document Stats ********************************************************/

    public int CountDocuments ()
    {
      return ( this.DocCollection.Count );
    }

    /** -------------------------------------------------------------------- **/

    public ulong CountUrlsInternal ()
    {
      return ( this.StatsUrlsInternal );
    }

    /** -------------------------------------------------------------------- **/

    public ulong CountUrlsExternal ()
    {
      return ( this.StatsUrlsExternal );
    }

    /** -------------------------------------------------------------------- **/

    public ulong CountUrlsSitemaps ()
    {
      return ( this.StatsUrlsSitemaps );
    }

    /**************************************************************************/

    public bool AddDocument ( MacroscopeDocument msDoc )
    {

      bool DocumentAdded = false;
      ulong DocKey = UrlToDigest( Url: msDoc.GetUrl() );

      lock ( this.DocCollection )
      {

        try
        {

          if ( this.ContainsDocument( msDoc: msDoc ) )
          {
            this.RemoveDocument( msDoc.GetUrl() );
            DocumentAdded = this.DocCollection.TryAdd( DocKey, msDoc );
          }
          else
          {
            try
            {
              DocumentAdded = this.DocCollection.TryAdd( DocKey, msDoc );
            }
            catch ( ArgumentException ex )
            {
              this.DebugMsg( string.Format( "AddDocument: {0}", ex.Message ) );
            }
            catch ( Exception ex )
            {
              this.DebugMsg( string.Format( "AddDocument: {0}", ex.Message ) );
            }
          }

        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "AddDocument: {0}", ex.Message ) );
        }

      }

      return ( DocumentAdded );

    }

    /**************************************************************************/

    public MacroscopeDocument GetDocumentByDocKey ( ulong DocKey )
    {

      MacroscopeDocument msDoc = null;

      try
      {

        if ( this.DocCollection.ContainsKey( DocKey ) )
        {
          msDoc = this.DocCollection[DocKey];
        }

      }
      catch ( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      return ( msDoc );

    }

    /**************************************************************************/

    public MacroscopeDocument GetDocumentByUrl ( string Url )
    {

      MacroscopeDocument msDoc = null;
      ulong DocKey = UrlToDigest( Url: Url );

      try
      {

        if ( this.DocCollection.ContainsKey( DocKey ) )
        {
          msDoc = this.DocCollection[DocKey];
        }

      }
      catch ( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      return ( msDoc );

    }

    /**************************************************************************/

    // TODO: Implement this:
    public MacroscopeDocument GetDocumentByETag ( string ETag )
    {

      MacroscopeDocument msDoc = null;

      /*
      try
      {

        if( !string.IsNullOrEmpty( Url ) )
        {
          if( this.DocCollection.ContainsKey( Url ) )
          {
            msDoc = this.DocCollection[ Url ];
          }
        }

      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }
      */

      return ( msDoc );

    }

    /**************************************************************************/

    public bool RemoveDocument ( string Url )
    {

      bool DocumentRemoved = false;
      ulong DocKey = UrlToDigest( Url: Url );

      lock ( this.DocCollection )
      {
        if ( this.DocCollection.ContainsKey( DocKey ) )
        {
          MacroscopeDocument Discard;
          DocumentRemoved = this.DocCollection.TryRemove( DocKey, out Discard );
        }
      }

      return ( DocumentRemoved );

    }

    /**************************************************************************/

    public IEnumerable<MacroscopeDocument> IterateDocuments ()
    {

      lock ( this.DocCollection )
      {

        if ( this.DocCollection.Count > 0 )
        {

          foreach ( ulong DocKey in this.DocCollection.Keys )
          {
            MacroscopeDocument msDoc = this.DocCollection[DocKey];
            if ( msDoc != null )
            {
              yield return this.DocCollection[DocKey];
            }
          }

        }

      }

    }

    /**************************************************************************/

    public List<ulong> DocumentKeys ()
    {

      List<ulong> lKeys = new List<ulong>();

      lock ( this.DocCollection )
      {

        if ( this.DocCollection.Count > 0 )
        {

          foreach ( ulong DocKey in this.DocCollection.Keys )
          {
            lKeys.Add( DocKey );
          }

        }

      }

      return ( lKeys );

    }

    /**************************************************************************/

    public List<string> DocumentUrls ()
    {

      List<string> lKeys = new List<string>();

      lock ( this.DocCollection )
      {

        if ( this.DocCollection.Count > 0 )
        {

          foreach ( ulong DocKey in this.DocCollection.Keys )
          {
            try
            {
              lKeys.Add( this.GetDocumentByDocKey( DocKey: DocKey ).GetUrl() );
            }
            catch ( Exception ex )
            {
              this.DebugMsg( ex.Message );
            }
          }

        }

      }

      return ( lKeys );

    }

    /** Inlinks ***************************************************************/

    public MacroscopeLinkList GetDocumentInlinks ( string Url )
    {

      MacroscopeLinkList Inlinks = null;

      lock ( this.StructInlinks )
      {

        if ( this.StructInlinks.ContainsKey( Url ) )
        {
          Inlinks = this.StructInlinks[Url];
        }

      }

      return ( Inlinks );

    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<string> IterateInlinks ()
    {

      lock ( this.StructInlinks )
      {

        foreach ( string Url in this.StructInlinks.Keys )
        {
          yield return Url;
        }

      }

    }

    /** HyperlinksIn **********************************************************/

    public MacroscopeHyperlinksIn GetDocumentHyperlinksIn ( string Url )
    {

      MacroscopeHyperlinksIn HyperlinksIn = null;

      lock ( this.StructHyperlinksIn )
      {

        if ( this.StructHyperlinksIn.ContainsKey( Url ) )
        {
          HyperlinksIn = this.StructHyperlinksIn[Url];
        }

      }

      return ( HyperlinksIn );

    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<string> IterateHyperlinksIn ()
    {

      lock ( this.StructHyperlinksIn )
      {

        foreach ( string Url in this.StructHyperlinksIn.Keys )
        {
          yield return Url;
        }

      }

    }

    /** Hyperlinks Ratio ******************************************************/

    public List<decimal> GetDocumentHyperlinksRatio ( string Url )
    {
      List<decimal> Ratio = new List<decimal>( 2 );
      Ratio.Add( 0 );
      Ratio.Add( 0 );
      lock ( this.StructHyperlinksRatio )
      {
        if ( this.StructHyperlinksRatio.ContainsKey( Url ) )
        {
          Ratio[0] = this.StructHyperlinksRatio[Url][0];
          Ratio[1] = this.StructHyperlinksRatio[Url][1];
        }
      }
      return ( Ratio );
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<List<decimal>> IterateHyperlinksRatio ()
    {

      lock ( this.StructHyperlinksRatio )
      {

        foreach ( string Url in this.StructHyperlinksRatio.Keys )
        {
          yield return this.StructHyperlinksRatio[Url];
        }

      }

    }

    /** Recalculate Stats Across DocCollection ********************************/

    private void StartRecalcTimer ()
    {
      this.DebugMsg( string.Format( "StartRecalcTimer: {0}", "STARTING..." ) );
      this.TimerRecalc = new System.Timers.Timer( 2000 );
      this.TimerRecalc.Elapsed += this.WorkerRecalculateDocCollection;
      this.TimerRecalc.AutoReset = true;
      this.TimerRecalc.Enabled = true;
      this.TimerRecalc.Start();
      this.DebugMsg( string.Format( "StartRecalcTimer: {0}", "STARTED." ) );
    }

    /** -------------------------------------------------------------------- **/

    private void StopRecalcTimer ()
    {
      try
      {
        this.TimerRecalc.Stop();
        if ( this.TimerRecalc != null )
        {
          this.TimerRecalc.Dispose();
        }
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "StopRecalcTimer: {0}", ex.Message ) );
      }
    }

    /** -------------------------------------------------------------------- **/

    private void WorkerRecalculateDocCollection ( object self, ElapsedEventArgs e )
    {

      bool DrainQueue = this.DrainWorkerRecalculateDocCollectionQueue();

      try
      {

        this.TimerRecalc.Stop();

        if ( DrainQueue )
        {
          this.TimerRecalc.Interval = 2000;
          this.RecalculateDocCollection();
        }
        else
        {
          this.TimerRecalc.Interval = 10000;
        }

        this.TimerRecalc.Start();

      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "WorkerRecalculateDocCollection: {0}", ex.Message ) );
      }

    }

    /**************************************************************************/

    public void AddWorkerRecalculateDocCollectionQueue ()
    {

      this.NamedQueue.AddToNamedQueue( MacroscopeConstants.RecalculateDocCollection, true );

    }

    /**************************************************************************/

    public bool DrainWorkerRecalculateDocCollectionQueue ()
    {
      bool Result = false;
      try
      {
        if ( this.NamedQueue.PeekNamedQueue( MacroscopeConstants.RecalculateDocCollection ) )
        {
          Result = true;
          this.NamedQueue.DrainNamedQueueItemsAsList( MacroscopeConstants.RecalculateDocCollection );
        }
      }
      catch ( InvalidOperationException ex )
      {
        this.DebugMsg( string.Format( "DrainWorkerRecalculateDocCollectionQueue: {0}", ex.Message ) );
      }
      return ( Result );
    }

    /**************************************************************************/

    public void RecalculateDocCollection ()
    {

      lock ( LockerRecalc )
      {

        lock ( this.DocCollection )
        {

          MacroscopeAllowedHosts AllowedHosts = this.JobMaster.GetAllowedHosts();

          this.StatsUrlsInternal = 0;
          this.StatsUrlsExternal = 0;
          this.StatsUrlsSitemaps = 0;

          foreach ( MacroscopeDocument msDoc in this.IterateDocuments() )
          {

            try
            {
              this.RecalculateInlinks( msDoc: msDoc );
            }
            catch ( Exception ex )
            {
              this.DebugMsg( string.Format( "RecalculateInlinks: {0}", ex.Message ) );
            }

            try
            {
              this.RecalculateHyperlinksIn( msDoc: msDoc );
            }
            catch ( Exception ex )
            {
              this.DebugMsg( string.Format( "RecalculateHyperlinksIn: {0}", ex.Message ) );
            }

            try
            {
              this.RecalculateHyperlinksRatio( msDoc: msDoc );
            }
            catch ( Exception ex )
            {
              this.DebugMsg( string.Format( "RecalculateHyperlinksRatio: {0}", ex.Message ) );
            }

            if ( this.StatsHistory.ContainsKey( UrlToDigest( Url: msDoc.GetUrl() ) ) )
            {
              this.DebugMsg( string.Format( "RecalculateDocCollection Already Seen: {0}", msDoc.GetUrl() ) );
            }
            else
            {

              this.DebugMsg( string.Format( "RecalculateDocCollection Adding: {0}", msDoc.GetUrl() ) );

              if ( msDoc.GetStatusCode() != 0 )
              {
                this.StatsHistory.Add( UrlToDigest( Url: msDoc.GetUrl() ), true );
              }

              this.RecalculateStatsCanonicals( msDoc: msDoc );

              this.RecalculateStatsHostnames( msDoc: msDoc );

              this.RecalculateStatsTitles( msDoc: msDoc );

              this.RecalculateStatsDescriptions( msDoc: msDoc );

              this.RecalculateStatsKeywords( msDoc: msDoc );

              this.RecalculateStatsHeadings( msDoc: msDoc );

              this.RecalculateStatsWarnings( msDoc: msDoc );

              this.RecalculateStatsErrors( msDoc: msDoc );

              this.RecalculateStatsChecksums( msDoc: msDoc );

              this.RecalculateStatsDocumentTypes( msDoc: msDoc );

              this.RecalculateStatsLanguages( msDoc: msDoc );

              this.RecalculateStatsDurations( msDoc: msDoc );

              if ( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
              {
                this.RecalculateStatsDeepKeywordAnalysis( msDoc: msDoc );
              }

              this.RecalculateStatsKeywordPresenceAnalysis( msDoc: msDoc );

              if ( MacroscopePreferencesManager.GetAnalyzeTextReadability() )
              {
                this.RecalculateStatsReadabilityGrades( msDoc: msDoc );
                this.RecalculateStatsReadabilityGradeDescriptions( msDoc: msDoc );
              }

              this.AddDocumentToSearchIndex( msDoc: msDoc );

              if ( MacroscopePreferencesManager.GetResolveAddresses() )
              {
                this.DnsLookup( msDoc: msDoc );
              }

              this.RecalculateMacroscopeRedirectChains( msDoc: msDoc );

            }

            this.RecalculateSitemapErrors( msDoc: msDoc );

            if ( AllowedHosts.IsAllowed( msDoc.GetHostname() ) )
            {
              this.StatsUrlsInternal++;
            }
            else
            {
              this.StatsUrlsExternal++;
            }

            if ( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.SITEMAPXML ) )
            {
              this.StatsUrlsSitemaps++;
            }

          }

        }

      }

    }

    /** -------------------------------------------------------------------- **/

    public void RecalculateDocCollectionFinal ()
    {

      lock ( LockerRecalc )
      {

        lock ( this.DocCollection )
        {

          MacroscopeAllowedHosts AllowedHosts = this.JobMaster.GetAllowedHosts();

          this.RecalculateAnalyzeInSitemaps();

          this.RecalculateOrphanedDocumentList();

          foreach ( MacroscopeDocument msDoc in this.IterateDocuments() )
          {
            this.RecalculateStatsKeywordPresenceAnalysis( msDoc: msDoc );
          }

        }

      }

    }

    /** Inlinks ***************************************************************/

    private void RecalculateInlinks ( MacroscopeDocument msDoc )
    {

      bool ProcessInlinks = msDoc.GetProcessInlinks();
      string DocumentUrl = msDoc.GetUrl();

      this.DebugMsg( string.Format( "RecalculateInlinks: {0} :: {1}", ProcessInlinks, DocumentUrl ) );

      if ( ProcessInlinks )
      {

        this.DebugMsg( string.Format( "RecalculateInlinks: PROCESSING: {0}", DocumentUrl ) );

        msDoc.UnsetProcessInlinks();

        foreach ( MacroscopeLink Link in msDoc.IterateOutlinks() )
        {

          string Url = Link.GetTargetUrl();
          MacroscopeLinkList Inlinks = null;

          if ( Url.Equals( DocumentUrl ) )
          {
            this.DebugMsg( string.Format( "RecalculateInlinks: SELF: {0}", Url ) );
            continue;
          }

          if ( this.StructInlinks.ContainsKey( Url ) )
          {
            Inlinks = this.StructInlinks[Url];
          }
          else
          {
            Inlinks = new MacroscopeLinkList();
            this.StructInlinks.Add( Url, Inlinks );
          }

          if ( Inlinks != null )
          {
            Inlinks.Add( Link: Link );
          }
          else
          {
            this.DebugMsg( string.Format( "RecalculateInlinks: NULL: {0}", DocumentUrl ) );
          }

        }

      }
      else
      {
        this.DebugMsg( string.Format( "RecalculateInlinks: ALREADY PROCESSED: {0}", DocumentUrl ) );
      }

      return;

    }

    /** Hyperlinks In *********************************************************/

    private void RecalculateHyperlinksIn ( MacroscopeDocument msDoc )
    {

      string DocumentUrl = msDoc.GetUrl();

      this.DebugMsg( string.Format( "RecalculateHyperlinksIn: {0} :: {1}", msDoc.GetProcessHyperlinksIn(), DocumentUrl ) );

      if ( msDoc.GetProcessHyperlinksIn() )
      {

        MacroscopeHyperlinksOut HyperlinksOut = msDoc.GetHyperlinksOut();

        this.DebugMsg( string.Format( "RecalculateHyperlinksIn: PROCESSING: {0}", DocumentUrl ) );

        msDoc.UnsetProcessInlinks();
        msDoc.UnsetProcessHyperlinksIn();

        foreach ( MacroscopeHyperlinkOut HyperlinkOut in HyperlinksOut.IterateLinks() )
        {

          string Url = HyperlinkOut.GetTargetUrl();
          MacroscopeHyperlinksIn HyperlinksIn = null;

          this.DebugMsg( string.Format( "RecalculateHyperlinksIn: URL SOURCE: {0}", DocumentUrl ) );
          this.DebugMsg( string.Format( "RecalculateHyperlinksIn: URL TARGET: {0}", Url ) );

          if ( Url.Equals( DocumentUrl ) )
          {
            this.DebugMsg( string.Format( "RecalculateHyperlinksIn: SELF: {0}", Url ) );
            continue;
          }

          if ( this.StructHyperlinksIn.ContainsKey( Url ) )
          {
            HyperlinksIn = this.StructHyperlinksIn[Url];
          }
          else
          {
            HyperlinksIn = new MacroscopeHyperlinksIn();
            this.StructHyperlinksIn.Add( Url, HyperlinksIn );
          }

          if ( HyperlinksIn != null )
          {

            HyperlinksIn.Add(
              LinkType: HyperlinkOut.GetHyperlinkType(),
              Method: HyperlinkOut.GetMethod(),
              SourceUrl: DocumentUrl,
              TargetUrl: Url,
              AnchorText: HyperlinkOut.GetAnchorText(),
              Title: HyperlinkOut.GetTitle(),
              AltText: HyperlinkOut.GetAltText(),
              ExtantGuid: HyperlinkOut.GetGuid()
            );

          }
          else
          {
            this.DebugMsg( string.Format( "RecalculateHyperlinksIn: NULL: {0}", DocumentUrl ) );
          }

        }

      }
      else
      {

        this.DebugMsg( string.Format( "RecalculateHyperlinksIn: ALREADY PROCESSED: {0}", DocumentUrl ) );

      }

    }

    /** Hyperlinks Ratio ******************************************************/

    private void RecalculateHyperlinksRatio ( MacroscopeDocument msDoc )
    {

      string Url = msDoc.GetUrl();
      decimal TotalLinksIn = (decimal) msDoc.CountHyperlinksIn();
      decimal TotalLinksOut = (decimal) msDoc.CountHyperlinksOut();
      decimal RatioLinksIn = 0;
      decimal RatioLinksOut = 0;

      lock ( this.StructHyperlinksRatio )
      {

        if ( !this.StructHyperlinksRatio.ContainsKey( Url ) )
        {
          this.StructHyperlinksRatio[Url] = new List<decimal>( 2 );
          this.StructHyperlinksRatio[Url].Add( 0 );
          this.StructHyperlinksRatio[Url].Add( 0 );
        }

        if ( TotalLinksIn > 0 )
        {
          RatioLinksIn = ( (decimal) 100 / ( TotalLinksIn + TotalLinksOut ) ) * TotalLinksIn;
        }

        if ( TotalLinksOut > 0 )
        {
          RatioLinksOut = ( (decimal) 100 / ( TotalLinksIn + TotalLinksOut ) ) * TotalLinksOut;
        }

        this.StructHyperlinksRatio[Url][0] = RatioLinksIn;
        this.StructHyperlinksRatio[Url][1] = RatioLinksOut;

      }

    }

    /** Canonicals *************************************************************/

    private void ClearStatsCanonicals ()
    {
      this.StatsCanonicals.Clear();
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<bool, int> GetStatsCanonicalsCount ()
    {

      Dictionary<bool, int> CanonicalsList = null;

      lock ( this.StatsCanonicals )
      {

        CanonicalsList = new Dictionary<bool, int>( this.StatsCanonicals.Count );

        foreach ( string Url in this.StatsCanonicals.Keys )
        {

          if ( CanonicalsList.ContainsKey( this.StatsCanonicals[Url] ) )
          {
            CanonicalsList[this.StatsCanonicals[Url]] = CanonicalsList[this.StatsCanonicals[Url]] + 1;
          }
          else
          {
            CanonicalsList.Add( this.StatsCanonicals[Url], 1 );
          }

        }

      }

      return ( CanonicalsList );

    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsCanonicals ( MacroscopeDocument msDoc )
    {

      string Url = msDoc.GetUrl();
      string Canonical = msDoc.GetCanonical();
      bool Value = false;

      if ( !string.IsNullOrEmpty( Canonical ) )
      {
        Value = true;
      }

      lock ( this.StatsCanonicals )
      {

        if ( this.StatsCanonicals.ContainsKey( Url ) )
        {
          this.StatsCanonicals[Url] = Value;
        }
        else
        {
          this.StatsCanonicals.Add( Url, Value );
        }

      }

    }

    /** Hostnames *************************************************************/

    private void ClearStatsHostnames ()
    {
      this.StatsHostnames.Clear();
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsHostnamesWithCount ()
    {

      Dictionary<string, int> HostnamesList = null;

      lock ( this.StatsHostnames )
      {

        HostnamesList = new Dictionary<string, int>( this.StatsHostnames.Count );

        foreach ( string Hostname in this.StatsHostnames.Keys )
        {
          HostnamesList.Add( Hostname, this.StatsHostnames[Hostname] );
        }

      }

      return ( HostnamesList );

    }

    /** -------------------------------------------------------------------- **/

    public int GetStatsHostnamesCount ( string Hostname )
    {

      int Value = 0;

      if ( this.StatsHostnames.ContainsKey( Hostname ) )
      {
        Value = this.StatsHostnames[Hostname];
      }

      return ( Value );

    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsHostnames ( MacroscopeDocument msDoc )
    {
      string Url = msDoc.GetUrl();
      string Text = msDoc.GetHostname();

      if ( !string.IsNullOrEmpty( Text ) )
      {

        Text = Text.ToLower();

        if ( this.StatsHostnames.ContainsKey( Text ) )
        {
          lock ( this.StatsHostnames )
          {
            this.StatsHostnames[Text] = this.StatsHostnames[Text] + 1;
          }
        }
        else
        {
          lock ( this.StatsHostnames )
          {
            this.StatsHostnames.Add( Text, 1 );
          }
        }

      }

    }

    /** Titles ****************************************************************/

    private void ClearStatsTitles ()
    {
      this.StatsTitles.Clear();
    }

    /** -------------------------------------------------------------------- **/

    public int GetStatsTitleCount ( MacroscopeDocument msDoc )
    {

      int Value = 0;
      string Text = msDoc.GetTitle();

      if ( !string.IsNullOrEmpty( Text ) )
      {

        ulong Hashed = Macroscope.StringToDigest( Text: Text.ToLower() );

        if ( this.StatsTitles.ContainsKey( Hashed ) )
        {

          Value = this.StatsTitles[Hashed];

        }
        else
        {

          this.DebugMsg( string.Format( "GetStatsTitleCount: {0}", Value ) );

          this.RecalculateStatsTitles( msDoc );

          if ( this.StatsTitles.ContainsKey( Hashed ) )
          {
            Value = this.StatsTitles[Hashed];
          }

        }

      }

      return ( Value );

    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsTitles ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;
      int Value = -1;

      if ( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
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

      if ( Proceed )
      {

        string Text = msDoc.GetTitle();

        if ( !string.IsNullOrEmpty( Text ) )
        {

          ulong Hashed = Macroscope.StringToDigest( Text: Text.ToLower() );

          lock ( this.StatsTitles )
          {

            if ( this.StatsTitles.ContainsKey( Hashed ) )
            {

              Value = this.StatsTitles[Hashed];

              this.StatsTitles[Hashed] = Value + 1;

            }
            else
            {

              Value = 1;

              this.StatsTitles.Add( Hashed, Value );

            }

          }

        }

      }

    }

    /** Descriptions **********************************************************/

    private void ClearStatsDescriptions ()
    {
      this.StatsDescriptions.Clear();
    }

    /** -------------------------------------------------------------------- **/

    public int GetStatsDescriptionCount ( MacroscopeDocument msDoc )
    {

      int Value = 0;
      string Text = msDoc.GetDescription();

      if ( !string.IsNullOrEmpty( Text ) )
      {

        ulong Hashed = Macroscope.StringToDigest( Text: Text.ToLower() );

        if ( this.StatsDescriptions.ContainsKey( Hashed ) )
        {

          Value = this.StatsDescriptions[Hashed];

        }
        else
        {

          this.DebugMsg( string.Format( "GetStatsDescriptionCount: {0}", Value ) );

          this.RecalculateStatsDescriptions( msDoc );

          if ( this.StatsDescriptions.ContainsKey( Hashed ) )
          {
            Value = this.StatsDescriptions[Hashed];
          }

        }

      }

      return ( Value );

    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsDescriptions ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;
      int Value = -1;

      switch ( msDoc.GetDocumentType() )
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

      if ( Proceed )
      {

        string Text = msDoc.GetDescription();

        if ( !string.IsNullOrEmpty( Text ) )
        {

          ulong Hashed = Macroscope.StringToDigest( Text: Text.ToLower() );

          lock ( this.StatsDescriptions )
          {

            if ( this.StatsDescriptions.ContainsKey( Hashed ) )
            {

              Value = this.StatsDescriptions[Hashed] + 1;

              this.StatsDescriptions[Hashed] = Value;

            }
            else
            {

              Value = 1;

              this.StatsDescriptions.Add( Hashed, Value );

            }

          }

        }

      }

    }

    /** Keywords **************************************************************/

    private void ClearStatsKeywords ()
    {
      this.StatsKeywords.Clear();
    }

    /** -------------------------------------------------------------------- **/

    public int GetStatsKeywordsCount ( MacroscopeDocument msDoc )
    {

      int Value = 0;
      string Text = msDoc.GetKeywords();

      if ( !string.IsNullOrEmpty( Text ) )
      {

        ulong Hashed = Macroscope.StringToDigest( Text: Text.ToLower() );

        if ( this.StatsKeywords.ContainsKey( Hashed ) )
        {

          Value = this.StatsKeywords[Hashed];

        }
        else
        {

          this.DebugMsg( string.Format( "GetStatsKeywordsCount: {0}", Value ) );

          this.RecalculateStatsKeywords( msDoc );

          if ( this.StatsKeywords.ContainsKey( Hashed ) )
          {
            Value = this.StatsKeywords[Hashed];
          }

        }

      }

      return ( Value );

    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsKeywords ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;
      int Value = -1;

      if ( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
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

      if ( Proceed )
      {

        string Text = msDoc.GetKeywords();

        if ( !string.IsNullOrEmpty( Text ) )
        {

          ulong Hashed = Macroscope.StringToDigest( Text: Text.ToLower() );

          lock ( this.StatsKeywords )
          {

            if ( this.StatsKeywords.ContainsKey( Hashed ) )
            {

              Value = this.StatsKeywords[Hashed] + 1;

              this.StatsKeywords[Hashed] = Value;

            }
            else
            {

              Value = 1;

              this.StatsKeywords.Add( Hashed, Value );

            }

          }

        }

      }

    }

    /** Warnings ****************************************************************/

    private void ClearStatsWarnings ()
    {
      lock ( this.StatsWarnings )
      {
        this.StatsWarnings.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsWarningsCount ()
    {
      Dictionary<string, int> dicStats = new Dictionary<string, int>( this.StatsWarnings.Count );
      lock ( this.StatsWarnings )
      {
        foreach ( string Key in this.StatsWarnings.Keys )
        {
          dicStats.Add( Key, this.StatsWarnings[Key] );
        }
      }
      return ( dicStats );
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsWarnings ( MacroscopeDocument msDoc )
    {

      string ErrorCondition = msDoc.GetErrorCondition();
      HttpStatusCode StatusCode = msDoc.GetStatusCode();

      if ( ( (int) StatusCode >= 300 ) && ( (int) StatusCode <= 399 ) )
      {

        string Text = string.Format( "{0} ({1})", StatusCode, ErrorCondition );

        this.DebugMsg( string.Format( "RecalculateStatsWarnings: {0}", Text ) );

        lock ( this.StatsWarnings )
        {

          if ( this.StatsWarnings.ContainsKey( Text ) )
          {
            this.StatsWarnings[Text] = this.StatsWarnings[Text] + 1;
          }
          else
          {
            this.StatsWarnings.Add( Text, 1 );
          }

        }

      }

    }

    /** Errors ****************************************************************/

    private void ClearStatsErrors ()
    {
      lock ( this.StatsErrors )
      {
        this.StatsErrors.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsErrorsCount ()
    {
      Dictionary<string, int> dicStats = new Dictionary<string, int>( this.StatsErrors.Count );
      lock ( this.StatsErrors )
      {
        foreach ( string Key in this.StatsErrors.Keys )
        {
          dicStats.Add( Key, this.StatsErrors[Key] );
        }
      }
      return ( dicStats );
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsErrors ( MacroscopeDocument msDoc )
    {

      string ErrorCondition = msDoc.GetErrorCondition();
      HttpStatusCode StatusCode = msDoc.GetStatusCode();

      if ( ( (int) StatusCode >= 400 ) && ( (int) StatusCode <= 599 ) )
      {

        string Text = string.Format( "{0} ({1})", StatusCode, ErrorCondition );

        this.DebugMsg( string.Format( "RecalculateStatsErrors: {0}", Text ) );

        lock ( this.StatsErrors )
        {

          if ( this.StatsErrors.ContainsKey( Text ) )
          {
            this.StatsErrors[Text] = this.StatsErrors[Text] + 1;
          }
          else
          {
            this.StatsErrors.Add( Text, 1 );
          }

        }

      }

    }

    /** Checksums *************************************************************/

    private void ClearStatsChecksums ()
    {
      lock ( this.StatsChecksums )
      {
        this.StatsChecksums.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    public int GetStatsChecksumCount ( string Checksum )
    {
      int Count = 0;
      lock ( this.StatsChecksums )
      {
        if ( this.StatsChecksums.ContainsKey( Checksum ) )
        {
          Count = this.StatsChecksums[Checksum];
        }
      }
      return ( Count );
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsChecksumsCount ()
    {
      Dictionary<string, int> dicStats = new Dictionary<string, int>( this.StatsChecksums.Count );
      lock ( this.StatsChecksums )
      {
        foreach ( string Key in this.StatsChecksums.Keys )
        {
          dicStats.Add( Key, this.StatsChecksums[Key] );
        }
      }
      return ( dicStats );
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsChecksums ( MacroscopeDocument msDoc )
    {

      string ChecksumValue = msDoc.GetChecksum();

      if ( ChecksumValue.Length > 0 )
      {

        this.DebugMsg( string.Format( "RecalculateStatsChecksums: {0}", ChecksumValue ) );

        lock ( this.StatsChecksums )
        {

          if ( this.StatsChecksums.ContainsKey( ChecksumValue ) )
          {
            this.StatsChecksums[ChecksumValue] = this.StatsChecksums[ChecksumValue] + 1;
          }
          else
          {
            this.StatsChecksums.Add( ChecksumValue, 1 );
          }

        }

      }

    }

    /** Document Types Stats **************************************************/

    private void ClearDocumentTypes ()
    {
      lock ( this.StatsDocumentTypesInternal )
      {
        this.StatsDocumentTypesInternal.Clear();
      }
      lock ( this.StatsDocumentTypesExternal )
      {
        this.StatsDocumentTypesExternal.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsDocumentTypesInternalCount ()
    {
      Dictionary<string, int> Copy = new Dictionary<string, int>( this.StatsDocumentTypesInternal.Count );
      lock ( this.StatsDocumentTypesInternal )
      {
        foreach ( string Key in this.StatsDocumentTypesInternal.Keys )
        {
          Copy.Add( Key, this.StatsDocumentTypesInternal[Key] );
        }
      }
      return ( Copy );
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsDocumentTypesExternalCount ()
    {
      Dictionary<string, int> Copy = new Dictionary<string, int>( this.StatsDocumentTypesExternal.Count );
      lock ( this.StatsDocumentTypesExternal )
      {
        foreach ( string Key in this.StatsDocumentTypesExternal.Keys )
        {
          Copy.Add( Key, this.StatsDocumentTypesExternal[Key] );
        }
      }
      return ( Copy );
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsDocumentTypes ( MacroscopeDocument msDoc )
    {
      if ( msDoc.GetIsInternal() )
      {
        lock ( this.StatsDocumentTypesInternal )
        {
          this.RecalculateStatsDocumentTypes( DocumentTypes: this.StatsDocumentTypesInternal, msDoc: msDoc );
        }
      }
      else
      {
        lock ( this.StatsDocumentTypesExternal )
        {
          this.RecalculateStatsDocumentTypes( DocumentTypes: this.StatsDocumentTypesExternal, msDoc: msDoc );
        }
      }
      return;
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsDocumentTypes ( Dictionary<string, int> DocumentTypes, MacroscopeDocument msDoc )
    {

      string Type = "Other";

      switch ( msDoc.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Type = "HTML";
          break;
        case MacroscopeConstants.DocumentType.IMAGE:
          Type = "Image";
          break;
        case MacroscopeConstants.DocumentType.CSS:
          Type = "CSS";
          break;
        case MacroscopeConstants.DocumentType.JAVASCRIPT:
          Type = "JavaScript";
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Type = "PDF";
          break;
        case MacroscopeConstants.DocumentType.TEXT:
          Type = "Text";
          break;
        case MacroscopeConstants.DocumentType.AUDIO:
          Type = "Audio";
          break;
        case MacroscopeConstants.DocumentType.VIDEO:
          Type = "Video";
          break;
        case MacroscopeConstants.DocumentType.XML:
          Type = "XML";
          break;
        case MacroscopeConstants.DocumentType.SITEMAPXML:
          Type = "Sitemap XML";
          break;
        case MacroscopeConstants.DocumentType.SITEMAPTEXT:
          Type = "Sitemap Text";
          break;
        case MacroscopeConstants.DocumentType.BINARY:
          Type = "Binary File";
          break;
        case MacroscopeConstants.DocumentType.SKIPPED:
          Type = "Skipped";
          break;
        default:
          break;
      }

      if ( DocumentTypes.ContainsKey( Type ) )
      {
        DocumentTypes[Type] += 1;
      }
      else
      {
        DocumentTypes.Add( Type, 1 );
      }

      return;

    }

    /** Languages Stats *******************************************************/

    private void ClearStatsLanguages ()
    {

      lock ( this.StatsLanguagesPages )
      {
        this.StatsLanguagesPages.Clear();
      }

      lock ( this.StatsLanguagesTitles )
      {
        this.StatsLanguagesTitles.Clear();
      }

      lock ( this.StatsLanguagesDescriptions )
      {
        this.StatsLanguagesDescriptions.Clear();
      }

      lock ( this.StatsLanguagesBodyTexts )
      {
        this.StatsLanguagesBodyTexts.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsLanguagesPagesCount ()
    {
      Dictionary<string, int> dicStats = new Dictionary<string, int>( this.StatsLanguagesPages.Count );
      lock ( this.StatsLanguagesPages )
      {
        foreach ( string Key in this.StatsLanguagesPages.Keys )
        {
          dicStats.Add( Key, this.StatsLanguagesPages[Key] );
        }
      }
      return ( dicStats );
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsLanguagesTitlesCount ()
    {
      Dictionary<string, int> dicStats = new Dictionary<string, int>( this.StatsLanguagesTitles.Count );
      lock ( this.StatsLanguagesTitles )
      {
        foreach ( string Key in this.StatsLanguagesTitles.Keys )
        {
          dicStats.Add( Key, this.StatsLanguagesTitles[Key] );
        }
      }
      return ( dicStats );
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsLanguagesDescriptionsCount ()
    {
      Dictionary<string, int> dicStats = new Dictionary<string, int>( this.StatsLanguagesDescriptions.Count );
      lock ( this.StatsLanguagesDescriptions )
      {
        foreach ( string Key in this.StatsLanguagesDescriptions.Keys )
        {
          dicStats.Add( Key, this.StatsLanguagesDescriptions[Key] );
        }
      }
      return ( dicStats );
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetStatsLanguagesBodyTextsCount ()
    {
      Dictionary<string, int> dicStats = new Dictionary<string, int>( this.StatsLanguagesBodyTexts.Count );
      lock ( this.StatsLanguagesBodyTexts )
      {
        foreach ( string Key in this.StatsLanguagesBodyTexts.Keys )
        {
          dicStats.Add( Key, this.StatsLanguagesBodyTexts[Key] );
        }
      }
      return ( dicStats );
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsLanguages ( MacroscopeDocument msDoc )
    {

      string PageLanguage = msDoc.GetIsoLanguageCode();
      string TitleLanguage = msDoc.GetTitleLanguage();
      string DescriptionLanguage = msDoc.GetDescriptionLanguage();
      string BodyTextLanguage = msDoc.GetDocumentTextLanguage();

      if ( string.IsNullOrEmpty( PageLanguage ) )
      {
        PageLanguage = "unknown";
      }

      if ( string.IsNullOrEmpty( TitleLanguage ) )
      {
        TitleLanguage = "unknown";
      }

      if ( string.IsNullOrEmpty( DescriptionLanguage ) )
      {
        DescriptionLanguage = "unknown";
      }

      if ( string.IsNullOrEmpty( BodyTextLanguage ) )
      {
        BodyTextLanguage = "unknown";
      }

      if ( !string.IsNullOrEmpty( PageLanguage ) )
      {

        this.DebugMsg( string.Format( "RecalculateStatsLanguages: {0}", PageLanguage ) );

        lock ( this.StatsLanguagesPages )
        {

          if ( this.StatsLanguagesPages.ContainsKey( PageLanguage ) )
          {
            this.StatsLanguagesPages[PageLanguage] = this.StatsLanguagesPages[PageLanguage] + 1;
          }
          else
          {
            this.StatsLanguagesPages.Add( PageLanguage, 1 );
          }

        }

      }

      if ( !string.IsNullOrEmpty( TitleLanguage ) )
      {

        this.DebugMsg( string.Format( "RecalculateStatsLanguages: {0}", TitleLanguage ) );

        lock ( this.StatsLanguagesTitles )
        {

          if ( this.StatsLanguagesTitles.ContainsKey( TitleLanguage ) )
          {
            this.StatsLanguagesTitles[TitleLanguage] = this.StatsLanguagesTitles[TitleLanguage] + 1;
          }
          else
          {
            this.StatsLanguagesTitles.Add( TitleLanguage, 1 );
          }

        }

      }

      if ( !string.IsNullOrEmpty( DescriptionLanguage ) )
      {

        this.DebugMsg( string.Format( "RecalculateStatsLanguages: {0}", DescriptionLanguage ) );

        lock ( this.StatsLanguagesDescriptions )
        {

          if ( this.StatsLanguagesDescriptions.ContainsKey( DescriptionLanguage ) )
          {
            this.StatsLanguagesDescriptions[DescriptionLanguage] = this.StatsLanguagesDescriptions[DescriptionLanguage] + 1;
          }
          else
          {
            this.StatsLanguagesDescriptions.Add( DescriptionLanguage, 1 );
          }

        }

      }

      if ( !string.IsNullOrEmpty( BodyTextLanguage ) )
      {

        this.DebugMsg( string.Format( "RecalculateStatsLanguages: {0}", BodyTextLanguage ) );

        lock ( this.StatsLanguagesBodyTexts )
        {

          if ( this.StatsLanguagesBodyTexts.ContainsKey( BodyTextLanguage ) )
          {
            this.StatsLanguagesBodyTexts[BodyTextLanguage] = this.StatsLanguagesBodyTexts[BodyTextLanguage] + 1;
          }
          else
          {
            this.StatsLanguagesBodyTexts.Add( BodyTextLanguage, 1 );
          }

        }

      }

    }

    /** Average Duration ******************************************************/

    private void ClearStatsDurations ()
    {
      lock ( this.StatsDurations )
      {
        this.StatsDurations.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    public decimal GetStatsDurationAverage ()
    {

      decimal Average = 0;
      decimal Maximus = 0;
      int Count = 0;

      if ( this.StatsDurations.Count > 0 )
      {

        lock ( this.StatsDurations )
        {
          foreach ( string Url in this.StatsDurations.Keys )
          {
            Count++;
            Maximus += this.StatsDurations[Url];
          }
        }

        if ( Count > 0 )
        {
          Average = Maximus / Count;
        }

      }

      return ( Average );
    }

    /** -------------------------------------------------------------------- **/

    public decimal GetStatsDurationsFastest ()
    {
      decimal Fastest = -1;
      if ( this.StatsDurations.Count > 0 )
      {
        lock ( this.StatsDurations )
        {
          foreach ( string Url in this.StatsDurations.Keys )
          {
            if ( Fastest == -1 )
            {
              Fastest = this.StatsDurations[Url];
            }
            else
            {
              if ( this.StatsDurations[Url] <= Fastest )
              {
                Fastest = this.StatsDurations[Url];
              }
            }
          }
        }
      }
      return ( Fastest );
    }

    /** -------------------------------------------------------------------- **/

    public decimal GetStatsDurationsSlowest ()
    {
      decimal Slowest = -1;
      if ( this.StatsDurations.Count > 0 )
      {
        lock ( this.StatsDurations )
        {
          foreach ( string Url in this.StatsDurations.Keys )
          {
            if ( Slowest == -1 )
            {
              Slowest = this.StatsDurations[Url];
            }
            else
            {
              if ( this.StatsDurations[Url] >= Slowest )
              {
                Slowest = this.StatsDurations[Url];
              }
            }
          }
        }
      }
      return ( Slowest );
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsDurations ( MacroscopeDocument msDoc )
    {
      string Url = msDoc.GetUrl();
      lock ( this.StatsDurations )
      {
        if ( this.StatsDurations.ContainsKey( Url ) )
        {
          this.StatsDurations[Url] = msDoc.GetDurationInSeconds();
        }
        else
        {
          this.StatsDurations.Add( Url, msDoc.GetDurationInSeconds() );
        }
      }
      ;
    }

    /** Deep Keyword Analysis *************************************************/

    private void ClearStatsDeepKeywordAnalysis ()
    {
      lock ( this.StatsDeepKeywordAnalysis )
      {
        for ( int i = 0; i < 4; i++ )
        {
          lock ( this.StatsDeepKeywordAnalysis[i] )
          {
            this.StatsDeepKeywordAnalysis[i].Clear();
          }
        }
        lock ( this.StatsDeepKeywordAnalysisDocs )
        {
          this.StatsDeepKeywordAnalysisDocs.Clear();
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsDeepKeywordAnalysis ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;

      if ( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
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

      if ( Proceed )
      {

        string DocLang = msDoc.GetIsoLanguageCode();

        if ( DocLang != null )
        {
          if ( Regex.IsMatch( msDoc.GetIsoLanguageCode(), "^(x-default|en|fr|de|it|es|po)", RegexOptions.IgnoreCase ) )
          {
            lock ( this.StatsDeepKeywordAnalysis )
            {
              for ( int i = 0; i < 4; i++ )
              {
                this.DeepAnalyzeKeywords.Analyze(
                  msDoc: msDoc,
                  Text: msDoc.GetDocumentTextCleaned(),
                  Terms: this.StatsDeepKeywordAnalysis[i],
                  Words: i + 1
                );
              }
            }
          }
        }

      }

    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string, int> GetDeepKeywordAnalysisAsDictonary ( int Words )
    {

      int WordsOffset = Words - 1;
      Dictionary<string, int> Terms = new Dictionary<string, int>( this.StatsDeepKeywordAnalysis[WordsOffset].Count );

      lock ( this.StatsDeepKeywordAnalysis[WordsOffset] )
      {
        foreach ( string Term in this.StatsDeepKeywordAnalysis[WordsOffset].Keys )
        {
          Terms.Add( Term, this.StatsDeepKeywordAnalysis[WordsOffset][Term] );
        }
      }

      return ( Terms );

    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDocumentList GetDeepKeywordAnalysDocumentList ( string KeywordTerm )
    {

      MacroscopeDocumentList DocumentList = null;

      if ( this.StatsDeepKeywordAnalysisDocs.ContainsKey( KeywordTerm ) )
      {
        DocumentList = this.StatsDeepKeywordAnalysisDocs[KeywordTerm];
      }

      return ( DocumentList );

    }

    /** Keyword Presence Analysis *********************************************/

    private void ClearStatsKeywordPresenceAnalysis ()
    {
      lock ( this.StatsKeywordPresenceAnalysis )
      {
        this.StatsKeywordPresenceAnalysis.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsKeywordPresenceAnalysis ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;
      List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence;

      if ( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        default:
          break;
      }

      if ( Proceed )
      {

        ulong DocKey = UrlToDigest( Url: msDoc.GetUrl() );

        KeywordPresence = this.AnalyzeKeywordsPresence.AnalyzeKeywordPresence( msDoc: msDoc );

        if ( KeywordPresence.Count > 0 )
        {

          if ( this.StatsKeywordPresenceAnalysis.ContainsKey( DocKey ) )
          {
            this.StatsKeywordPresenceAnalysis[DocKey] = KeywordPresence;
          }
          else
          {
            this.StatsKeywordPresenceAnalysis.Add( DocKey, KeywordPresence );
          }

        }
        else
        {

          if ( this.StatsKeywordPresenceAnalysis.ContainsKey( DocKey ) )
          {
            this.StatsKeywordPresenceAnalysis[DocKey] = null;
          }
          else
          {
            this.StatsKeywordPresenceAnalysis.Add( DocKey, null );
          }

        }

      }

    }

    /** -------------------------------------------------------------------- **/

    public List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> GetKeywordPresenceAnalysis ( MacroscopeDocument msDoc )
    {

      ulong DocKey = UrlToDigest( Url: msDoc.GetUrl() );
      List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence = null;

      if ( this.StatsKeywordPresenceAnalysis.ContainsKey( DocKey ) )
      {
        KeywordPresence = this.StatsKeywordPresenceAnalysis[DocKey];
      }

      return ( KeywordPresence );

    }

    /** Readability Analysis **************************************************/

    private void ClearStatsReadabilityGrades ()
    {
      lock ( this.StatsReadabilityGrades )
      {
        lock ( this.StatsReadabilityGradeDescriptions )
        {
          this.StatsReadabilityGrades.Clear();
          this.StatsReadabilityGradeDescriptions.Clear();
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public SortedDictionary<string, int> GetStatsReadabilityGradesCount ()
    {

      SortedDictionary<string, int> dicStats = new SortedDictionary<string, int>();

      lock ( this.StatsReadabilityGrades )
      {

        foreach ( string Key in this.StatsReadabilityGrades.Keys )
        {
          dicStats.Add( Key, this.StatsReadabilityGrades[Key] );
        }

      }

      return ( dicStats );

    }

    /** -------------------------------------------------------------------- **/

    public SortedDictionary<string, int> GetStatsReadabilityGradeStringsCount ()
    {

      SortedDictionary<string, int> dicStats = new SortedDictionary<string, int>();

      lock ( this.StatsReadabilityGradeDescriptions )
      {

        foreach ( string Key in this.StatsReadabilityGradeDescriptions.Keys )
        {
          dicStats.Add( Key, this.StatsReadabilityGradeDescriptions[Key] );
        }

      }

      return ( dicStats );

    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsReadabilityGrades ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;

      if ( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
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

      if ( Proceed )
      {

        lock ( this.StatsReadabilityGrades )
        {
          string Grade;

          Grade = string.Format(
            "{0} / {1}",
            MacroscopeAnalyzeReadability.FormatAnalyzeReadabilityMethod(
              ReadabilityMethod: msDoc.GetReadabilityGradeMethod()
            ),
            msDoc.GetReadabilityGrade().ToString( "00.00" )
          );

          if ( this.StatsReadabilityGrades.ContainsKey( Grade ) )
          {
            this.StatsReadabilityGrades[Grade] = this.StatsReadabilityGrades[Grade] + 1;
          }
          else
          {
            this.StatsReadabilityGrades.Add( Grade, 1 );
          }

        }

      }

    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsReadabilityGradeDescriptions ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;

      if ( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
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

      if ( Proceed )
      {

        lock ( this.StatsReadabilityGradeDescriptions )
        {
          string Grade;

          Grade = string.Format(
            "{0} / {1}",
            MacroscopeAnalyzeReadability.FormatAnalyzeReadabilityMethod(
              ReadabilityMethod: msDoc.GetReadabilityGradeMethod()
            ),
            msDoc.GetReadabilityGradeDescription()
          );

          if ( this.StatsReadabilityGradeDescriptions.ContainsKey( Grade ) )
          {
            this.StatsReadabilityGradeDescriptions[Grade] = this.StatsReadabilityGradeDescriptions[Grade] + 1;
          }
          else
          {
            this.StatsReadabilityGradeDescriptions.Add( Grade, 1 );
          }

        }

      }

    }

    /** Headings Analysis *****************************************************/

    private void ClearStatsHeadings ()
    {
      lock ( this.StatsHeadings )
      {
        for ( ushort HeadingLevel = 1; HeadingLevel <= 6; HeadingLevel++ )
        {
          lock ( this.StatsHeadings[HeadingLevel] )
          {
            this.StatsHeadings[HeadingLevel].Clear();
          }
        }
        lock ( this.StatsHeadings )
        {
          this.StatsHeadings.Clear();
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public int GetStatsHeadingsCount ( ushort HeadingLevel, string Text )
    {

      int Count = 0;
      ulong Hashed = Macroscope.StringToDigest( Text: Text.ToLower() );

      if ( this.StatsHeadings[HeadingLevel].ContainsKey( Hashed ) )
      {
        Count = this.StatsHeadings[HeadingLevel][Hashed];
      }

      return ( Count );

    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsHeadings ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;

      if ( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        default:
          break;
      }

      if ( Proceed )
      {

        lock ( this.StatsHeadings )
        {

          for ( ushort HeadingLevel = 1; HeadingLevel <= MacroscopePreferencesManager.GetMaxHeadingDepth(); HeadingLevel++ )
          {

            List<string> Headings = msDoc.GetHeadings( HeadingLevel: HeadingLevel );

            foreach ( string Text in Headings )
            {

              ulong Hashed = Macroscope.StringToDigest( Text: Text.ToLower() );

              if ( this.StatsHeadings[HeadingLevel].ContainsKey( Hashed ) )
              {
                this.StatsHeadings[HeadingLevel][Hashed] = this.StatsHeadings[HeadingLevel][Hashed] + 1;
              }
              else
              {
                this.StatsHeadings[HeadingLevel].Add( Hashed, 1 );
              }

            }

          }

        }

      }

    }

    /** Calculate Home Page Link Chains ***************************************/

    public void RecalculateClickPaths ()
    {

      if ( MacroscopePreferencesManager.GetAnalyzeClickPaths() )
      {

        // TODO: Implement this:

        MacroscopeDocument msDoc = this.GetDocumentByUrl( Url: this.GetStartUrl() );

        if ( msDoc != null )
        {

          if ( msDoc.GetIsInternal() && msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
          {
#if DEBUG
            this.ClickPathAnalysis.Analyze( RootDoc: msDoc );
#endif
          }

        }

      }

    }

    /** Sitemap Errors ********************************************************/

    private void RecalculateSitemapErrors ( MacroscopeDocument msDoc )
    {

      bool Proceed = false;
      string Url = msDoc.GetUrl();

      switch ( msDoc.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.SITEMAPXML:
          Proceed = true;
          break;
        case MacroscopeConstants.DocumentType.SITEMAPTEXT:
          Proceed = true;
          break;
        default:
          break;
      }

      if ( msDoc.GetIsExternal() )
      {
        Proceed = false;
      }

      if ( !Proceed )
      {
        return;
      }

      foreach ( MacroscopeLink Outlink in msDoc.IterateOutlinks() )
      {

        string TargetUrl = Outlink.GetTargetUrl();
        MacroscopeDocument msDocLinked = this.GetDocumentByUrl( Url: TargetUrl );
        bool InsertDoc = false;

        if ( ( msDocLinked != null ) && msDocLinked.GetIsInternal() )
        {
          int StatusCode = (int) msDocLinked.GetStatusCode();
          if ( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
          {
            InsertDoc = true;
          }
          if ( !msDocLinked.GetAllowedByRobots() )
          {
            InsertDoc = true;
          }
        }

        if ( InsertDoc )
        {

          string LinkedUrl = msDocLinked.GetUrl();

          Dictionary<string, Dictionary<string, string>> DocumentList; // URL => PropertyList
          Dictionary<string, string> PropertyList = new Dictionary<string, string>();

          lock ( this.StatsSitemapErrors )
          {

            if ( this.StatsSitemapErrors.ContainsKey( Url ) )
            {
              DocumentList = this.StatsSitemapErrors[Url];
            }
            else
            {
              DocumentList = new Dictionary<string, Dictionary<string, string>>();
              this.StatsSitemapErrors.Add( Url, DocumentList );
            }

            lock ( this.StatsSitemapErrors[Url] )
            {

              if ( DocumentList.ContainsKey( LinkedUrl ) )
              {
                lock ( DocumentList[LinkedUrl] )
                {
                  DocumentList.Remove( LinkedUrl );
                }
              }

              lock ( DocumentList )
              {
                DocumentList.Add( LinkedUrl, PropertyList );
                PropertyList.Add( "sitemap_url", msDoc.GetUrl() );
                PropertyList.Add( "status_code", msDocLinked.GetStatusCode().ToString() );
                PropertyList.Add( "robots", msDocLinked.GetAllowedByRobotsAsString() );
                PropertyList.Add( "target_url", TargetUrl );
              }

            }

          }

        }

      }

      return;

    }

    /** -------------------------------------------------------------------- **/

    public List<Dictionary<string, string>> GetSitemapErrorsAsTable ()
    {

      List<Dictionary<string, string>> CompiledTable = new List<Dictionary<string, string>>();

      lock ( this.StatsSitemapErrors )
      {

        foreach ( string SitemapUrl in this.StatsSitemapErrors.Keys )
        {

          if ( this.StatsSitemapErrors.ContainsKey( SitemapUrl ) )
          {

            Dictionary<string, Dictionary<string, string>> DocumentList = this.StatsSitemapErrors[SitemapUrl];

            foreach ( string DocumentUrl in DocumentList.Keys )
            {

              Dictionary<string, string> PropertyList = DocumentList[DocumentUrl];
              Dictionary<string, string> Entry = new Dictionary<string, string>();

              Entry.Add( "sitemap_url", PropertyList["sitemap_url"] );
              Entry.Add( "status_code", PropertyList["status_code"] );
              Entry.Add( "robots", PropertyList["robots"] );
              Entry.Add( "target_url", PropertyList["target_url"] );

              CompiledTable.Add( Entry );

            }

          }

        }

      }

      return ( CompiledTable );

    }

    /** Analyze Documents in Sitemaps *****************************************/

    private void RecalculateAnalyzeInSitemaps ()
    {
      MacroscopeAnalyzeSitemapUrls Analyzer = new MacroscopeAnalyzeSitemapUrls();
      this.DocumentsInSitemaps = Analyzer.AnalyzeInSitemaps( DocCollection: this );
      return;
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDocumentList GetDocumentsNotInSitemaps ()
    {
      MacroscopeDocumentList DocumentList = new MacroscopeDocumentList();
      if ( this.DocumentsInSitemaps.Count == 2 )
      {
        DocumentList = this.DocumentsInSitemaps[0];
      }
      return ( DocumentList );
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDocumentList GetDocumentsInSitemaps ()
    {
      MacroscopeDocumentList DocumentList = new MacroscopeDocumentList();
      if ( this.DocumentsInSitemaps.Count == 2 )
      {
        DocumentList = this.DocumentsInSitemaps[1];
      }
      return ( DocumentList );
    }

    /** Analyze Orphaned Documents in Collection ******************************/

    private void RecalculateOrphanedDocumentList ()
    {
      MacroscopeAnalyzeOrphanedPages Analyzer = new MacroscopeAnalyzeOrphanedPages();
      this.OrphanedDocumentList = Analyzer.AnalyzeOrphanedDocumentsInCollection( DocCollection: this );
      return;
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDocumentList GetOrphanedDocumentList ()
    {
      return ( this.OrphanedDocumentList );
    }

    /** Analyze Redirect Chains in Collection *********************************/

    private async void RecalculateMacroscopeRedirectChains ( MacroscopeDocument msDoc )
    {

      if ( msDoc.GetFetchStatus() != MacroscopeConstants.FetchStatus.OK )
      {
        return;
      }

      if ( msDoc.GetStatusCode() == 0 )
      {
        return;
      }

      if ( msDoc.GetIsRedirect() )
      {

        MacroscopeRedirectChainAnalysis Analyzer = new MacroscopeRedirectChainAnalysis( Client: this.GetJobMaster().GetHttpClient() );
        List<MacroscopeRedirectChainDocStruct> AnalyzedRedirectChain = new List<MacroscopeRedirectChainDocStruct>();

        try
        {

          var CheckStatusCode = msDoc.GetStatusCode();
          var CheckStartUrl = msDoc.GetUrl();
          var CheckRedirectUrl = msDoc.GetUrlRedirectTo();

          AnalyzedRedirectChain = await Analyzer.AnalyzeRedirectChains(
            StatusCode: CheckStatusCode,
            StartUrl: CheckStartUrl,
            RedirectUrl: CheckRedirectUrl
          );

        }
        catch ( Exception ex )
        {
          this.DebugMsg( ex.Message );
        }

        try
        {
          if ( AnalyzedRedirectChain.Count > 0 )
          {
            this.AnalyzedRedirectChains.Add( AnalyzedRedirectChain );
          }
        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "RecalculateMacroscopeRedirectChains: {0}", ex.Message ) );
        }

      }
      else
      {
        this.DebugMsg( string.Format( "DOC: {0}", msDoc.GetUrl() ) );
      }

      return;

    }

    /** -------------------------------------------------------------------- **/

    public List<List<MacroscopeRedirectChainDocStruct>> GetMacroscopeRedirectChains ()
    {
      return ( this.AnalyzedRedirectChains );
    }

    /** Search Index **********************************************************/

    public MacroscopeSearchIndex GetSearchIndex ()
    {
      return ( this.SearchIndex );
    }

    /** -------------------------------------------------------------------- **/

    private void AddDocumentToSearchIndex ( MacroscopeDocument msDoc )
    {

      if ( MacroscopePreferencesManager.GetEnableTextIndexing() )
      {

        bool Proceed = false;

        switch ( msDoc.GetDocumentType() )
        {
          case MacroscopeConstants.DocumentType.HTML:
            Proceed = true;
            break;
          case MacroscopeConstants.DocumentType.TEXT:
            Proceed = true;
            break;
          case MacroscopeConstants.DocumentType.PDF:
            Proceed = true;
            break;
          default:
            break;
        }

        if ( Proceed )
        {
          this.SearchIndex.AddDocumentToIndex( msDoc );
        }

      }

    }

    /** DNS Lookup ************************************************************/

    public void DnsLookup ( MacroscopeDocument msDoc )
    {

      IPHostEntry HostEntry = null;
      string Hostname = msDoc.GetHostname();

      if ( this.DnsCache.ContainsKey( Hostname ) )
      {

        msDoc.SetHostAddresses( HostEntry: this.DnsCache[Hostname] );

      }
      else
      {

        HostEntry = msDoc.SetHostAddresses();

        lock ( this.DnsCache )
        {

          if ( !this.DnsCache.ContainsKey( Hostname ) )
          {
            this.DnsCache.Add( Hostname, HostEntry );
          }

        }

      }

    }

    /**************************************************************************/

  }

}
