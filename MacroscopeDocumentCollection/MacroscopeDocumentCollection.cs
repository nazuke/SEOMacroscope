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
using System.Net;
using System.Text.RegularExpressions;
using System.Timers;
using System.Threading;

namespace SEOMacroscope
{

  public sealed class MacroscopeDocumentCollection : Macroscope
  {

    /**************************************************************************/

    private Dictionary<string,MacroscopeDocument> DocCollection;

    private MacroscopeJobMaster JobMaster;
    private MacroscopeNamedQueue NamedQueue;
    private MacroscopeSearchIndex SearchIndex;
    private MacroscopeDeepKeywordAnalysis AnalyzeKeywords;

    private Dictionary<string,MacroscopeLinkList> StructInlinks;
    private Dictionary<string,MacroscopeHyperlinksIn> StructHyperlinksIn;

    private Dictionary<string,Boolean> StatsHistory;
    private Dictionary<string,int> StatsHostnames;
    private Dictionary<string,int> StatsTitles;
    private Dictionary<string,int> StatsDescriptions;
    private Dictionary<string,int> StatsKeywords;
    private List<Dictionary<string,int>> StatsHeadings;
    private Dictionary<string,int> StatsWarnings;
    private Dictionary<string,int> StatsErrors;
    private Dictionary<string,int> StatsChecksums;

    private Dictionary<string,int> StatsLanguagesPages;
    private Dictionary<string,int> StatsLanguagesTitles;
    private Dictionary<string,int> StatsLanguagesDescriptions;
    private Dictionary<string,int> StatsLanguagesBodyTexts;
    
    private Dictionary<string,decimal> StatsDurations;
    
    private Dictionary<string,MacroscopeDocumentList> StatsDeepKeywordAnalysisDocs;
    private List<Dictionary<string,int>> StatsDeepKeywordAnalysis;

    private Dictionary<string,int> StatsReadabilityGrades;
    private Dictionary<string,int> StatsReadabilityGradeDescriptions;

    private int StatsUrlsInternal;
    private int StatsUrlsExternal;
    private int StatsUrlsSitemaps;

    private Dictionary<string,IPHostEntry> DnsCache;

    private static object LockerDocCollection = new object ();
    private static object LockerRecalc = new object ();
    
    private System.Timers.Timer TimerRecalc;

    /**************************************************************************/

    public MacroscopeDocumentCollection ( MacroscopeJobMaster JobMaster )
    {

      this.SuppressDebugMsg = true;

      this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZING..." );

      this.DocCollection = new Dictionary<string,MacroscopeDocument> ( 4096 );

      this.JobMaster = JobMaster;

      this.NamedQueue = new MacroscopeNamedQueue ();
      this.NamedQueue.CreateNamedQueue( MacroscopeConstants.RecalculateDocCollection );

      this.SearchIndex = new MacroscopeSearchIndex ();

      this.StructInlinks = new Dictionary<string,MacroscopeLinkList> ( 1024 );
      this.StructHyperlinksIn = new Dictionary<string,MacroscopeHyperlinksIn> ( 1024 );
      
      this.StatsHistory = new Dictionary<string,Boolean> ( 1024 );
      this.StatsHostnames = new Dictionary<string,int> ( 16 );
      this.StatsTitles = new Dictionary<string,int> ( 256 );
      this.StatsDescriptions = new Dictionary<string,int> ( 256 );
      this.StatsKeywords = new Dictionary<string,int> ( 256 );

      this.StatsHeadings = new List<Dictionary<string,int>> ( 7 );
      for( ushort i = 1 ; i <= 6 ; i++ )
      {
        this.StatsHeadings.Add( new Dictionary<string,int> ( 256 ) );
      }

      this.StatsWarnings = new  Dictionary<string,int> ( 32 );
      this.StatsErrors = new  Dictionary<string,int> ( 32 );
      this.StatsChecksums = new  Dictionary<string,int> ( 1024 );

      this.StatsLanguagesPages = new Dictionary<string,int> ( 8 );
      this.StatsLanguagesTitles = new Dictionary<string,int> ( 8 );
      this.StatsLanguagesDescriptions = new Dictionary<string,int> ( 8 );
      this.StatsLanguagesBodyTexts = new Dictionary<string,int> ( 8 );

      this.StatsDurations = new Dictionary<string,decimal> ( 1024 );

      this.StatsDeepKeywordAnalysisDocs = new Dictionary<string,MacroscopeDocumentList> ( 1024 );
      this.StatsDeepKeywordAnalysis = new  List<Dictionary<string,int>> ( 4 );
      for( int i = 0 ; i < 4 ; i++ )
      {
        this.StatsDeepKeywordAnalysis.Add( new Dictionary<string,int> ( 1024 ) );
      }

      this.AnalyzeKeywords = new MacroscopeDeepKeywordAnalysis ( DocList: this.StatsDeepKeywordAnalysisDocs );
           
      this.StatsReadabilityGrades = new  Dictionary<string,int> ( 16 );
      this.StatsReadabilityGradeDescriptions = new  Dictionary<string,int> ( 16 );

      this.StatsUrlsInternal = 0;
      this.StatsUrlsExternal = 0;
      this.StatsUrlsSitemaps = 0;

      this.DnsCache = new Dictionary<string,IPHostEntry> ( 16 );
          
      this.StartRecalcTimer();

      this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZED." );

    }

    /** Self Destruct Sequence ************************************************/

    ~MacroscopeDocumentCollection ()
    {
      
      this.DebugMsg( "MacroscopeDocumentCollection DESTRUCTOR CALLED" );
      
      this.StopRecalcTimer();

      this.DocCollection = null;

      this.JobMaster = null;
      this.NamedQueue = null;
      this.SearchIndex = null;
      this.AnalyzeKeywords = null;

      this.StructInlinks = null;
      this.StructHyperlinksIn = null;

      this.StatsHistory = null;
      this.StatsHostnames = null;
      this.StatsTitles = null;
      this.StatsDescriptions = null;
      this.StatsKeywords = null;
      this.StatsHeadings = null;
      this.StatsWarnings = null;
      this.StatsErrors = null;
      this.StatsChecksums = null;
    
      this.StatsLanguagesPages = null;
      this.StatsLanguagesTitles = null;
      this.StatsLanguagesDescriptions = null;
      this.StatsLanguagesBodyTexts = null;

      this.StatsDurations = null;
    
      this.StatsDeepKeywordAnalysisDocs = null;
      this.StatsDeepKeywordAnalysis = null;

      //this.AnalyzeTextLanguage = null;

      this.DnsCache = null;

    }

    /** Job Master Methods ****************************************************/
    
    public MacroscopeJobMaster GetJobMaster ()
    {
      return( this.JobMaster );
    }

    /** Allowed Hosts Methods *************************************************/

    public MacroscopeAllowedHosts GetAllowedHosts ()
    {
      return( this.JobMaster.GetAllowedHosts() );
    }

    /** Document Collection Methods *******************************************/

    public Boolean ContainsDocument ( string Url )
    {

      Boolean DocumentPresent = false;

      if( this.DocCollection.ContainsKey( Url ) )
      {
        DocumentPresent = true;
      }

      return( DocumentPresent );

    }

    /** Create Document Methods ***********************************************/
    
    public MacroscopeDocument CreateDocument ( string Url )
    {

      MacroscopeDocument msDoc;

      msDoc = new MacroscopeDocument (
        DocumentCollection: this,
        Url: Url
      );

      return( msDoc );

    }

    public MacroscopeDocument CreateDocument ( MacroscopeCredential Credential, string Url )
    {

      MacroscopeDocument msDoc;

      msDoc = new MacroscopeDocument (
        DocumentCollection: this,
        Url: Url,
        Credential: Credential
      );
      
      return( msDoc );

    }

    /** Document Stats ********************************************************/

    public int CountDocuments ()
    {
      return( this.DocCollection.Count );
    }

    public int CountUrlsInternal ()
    {
      return( this.StatsUrlsInternal );
    }

    public int CountUrlsExternal ()
    {
      return( this.StatsUrlsExternal );
    }

    public int CountUrlsSitemaps ()
    {
      return( this.StatsUrlsSitemaps );
    }

    /**************************************************************************/

    public void AddDocument ( MacroscopeDocument msDoc )
    {
      this.AddDocument( msDoc.GetUrl(), msDoc );
    }

    // TODO: There may be a bug here, whereby two or more error pages are added multiple times.
    public void AddDocument ( string Url, MacroscopeDocument msDoc )
    {

      if( Monitor.TryEnter( LockerDocCollection ) )
      {

        try
        {

          if( this.DocCollection.ContainsKey( Url ) )
          {
            this.DocCollection.Remove( Url );
            this.DocCollection.Add( Url, msDoc );
          }
          else
          {
            try
            {
              this.DocCollection.Add( Url, msDoc );
            }
            catch( ArgumentException ex )
            {
              this.DebugMsg( string.Format( "AddDocument: {0}", ex.Message ) );
            }
            catch( Exception ex )
            {
              this.DebugMsg( string.Format( "AddDocument: {0}", ex.Message ) );
            }
          
          }
        
        }
        finally
        {
          Monitor.Exit( LockerDocCollection );
        }

      }

    }

    /**************************************************************************/

    public Boolean DocumentExists ( string Url )
    {

      Boolean DocumentPresent = false;

      if( this.DocCollection.ContainsKey( Url ) )
      {
        DocumentPresent = true;
      }

      return( DocumentPresent );

    }

    /**************************************************************************/

    public MacroscopeDocument GetDocument ( string Url )
    {

      MacroscopeDocument msDoc = null;

      if(
        ( !string.IsNullOrEmpty( Url ) )
        && this.DocCollection.ContainsKey( Url ) )
      {
        msDoc = this.DocCollection[ Url ];
      }
      else
      {
        //throw new MacroscopeDocumentCollectionException ( string.Format( "Document not in collection: {0}", Url ) );
      }

      return( msDoc );

    }

    /**************************************************************************/

    public void RemoveDocument ( string Url )
    {

      if( Monitor.TryEnter( LockerDocCollection ) )
      {

        try
        {

          if( this.DocCollection.ContainsKey( Url ) )
          {
            this.DocCollection.Remove( Url );
          }
        
        }
        finally
        {
          Monitor.Exit( LockerDocCollection );
        }

      }
      
    }

    /**************************************************************************/

    public IEnumerable<MacroscopeDocument> IterateDocuments ()
    {

      if( Monitor.TryEnter( LockerDocCollection ) )
      {

        try
        {
          if( this.DocCollection.Count > 0 )
          {

            foreach( string Url in this.DocCollection.Keys )
            {
              MacroscopeDocument msDoc = this.DocCollection[ Url ];
              if( msDoc != null )
              {
                yield return this.DocCollection[ Url ];
              }
            }
          
          }
        }
        finally
        {
          Monitor.Exit( LockerDocCollection );
        }

      }

    }

    /**************************************************************************/

    public List<string> DocumentKeys ()
    {

      List<string> lKeys = new List<string> ();

      if( Monitor.TryEnter( LockerDocCollection ) )
      {

        try
        {

          if( this.DocCollection.Count > 0 )
          {

            foreach( string Url in this.DocCollection.Keys )
            {
              lKeys.Add( Url );
            }
          
          }
        
        }
        finally
        {
          Monitor.Exit( LockerDocCollection );
        }

      }

      return( lKeys );
      
    }

    /** Inlinks ***************************************************************/

    public MacroscopeLinkList GetDocumentInlinks ( string Url )
    {
      
      MacroscopeLinkList Inlinks = null;
      
      lock( this.StructInlinks )
      {
        
        if( this.StructInlinks.ContainsKey( Url ) )
        {
          Inlinks = this.StructInlinks[ Url ];
        }
        
      }
      
      return( Inlinks );
      
    }

    public IEnumerable<string> IterateInlinks ()
    {
      
      lock( this.StructInlinks )
      {
        
        foreach( string Url in this.StructInlinks.Keys )
        {
          yield return Url;
        }
        
      }
      
    }

    /** HyperlinksIn **********************************************************/

    public MacroscopeHyperlinksIn GetDocumentHyperlinksIn ( string Url )
    {
    
      MacroscopeHyperlinksIn HyperlinksIn = null;
      
      lock( this.StructHyperlinksIn )
      {

        if( this.StructHyperlinksIn.ContainsKey( Url ) )
        {
          HyperlinksIn = this.StructHyperlinksIn[ Url ];
        }

      }
      
      return( HyperlinksIn );
      
    }

    public IEnumerable<string> IterateHyperlinksIn ()
    {
      
      lock( this.StructHyperlinksIn )
      {
        
        foreach( string Url in this.StructHyperlinksIn.Keys )
        {
          yield return Url;
        }
        
      }
      
    }

    /** Recalculate Stats Across DocCollection ********************************/

    private void StartRecalcTimer ()
    {
      this.DebugMsg( string.Format( "StartRecalcTimer: {0}", "STARTING..." ) );
      this.TimerRecalc = new System.Timers.Timer ( 2000 );
      this.TimerRecalc.Elapsed += this.WorkerRecalculateDocCollection;
      this.TimerRecalc.AutoReset = true;
      this.TimerRecalc.Enabled = true;
      this.TimerRecalc.Start();
      this.DebugMsg( string.Format( "StartRecalcTimer: {0}", "STARTED." ) );
    }

    private void StopRecalcTimer ()
    {
      try
      {
        this.TimerRecalc.Stop();
        this.TimerRecalc.Dispose();
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "StopRecalcTimer: {0}", ex.Message ) );
      }
    }

    private void WorkerRecalculateDocCollection ( Object self, ElapsedEventArgs e )
    {

      try
      {

        Boolean DrainQueue = this.DrainWorkerRecalculateDocCollectionQueue();

        if( DrainQueue )
        {
          this.TimerRecalc.Interval = 2000;
          this.RecalculateDocCollection();
        }
        else
        {
          this.TimerRecalc.Interval = 10000;
        }

      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "WorkerRecalculateDocCollection: {0}", ex.Message ) );
      }

    }

    /**************************************************************************/

    public void AddWorkerRecalculateDocCollectionQueue ()
    {
      
      this.NamedQueue.AddToNamedQueue( MacroscopeConstants.RecalculateDocCollection, "c" );

    }

    /**************************************************************************/

    public Boolean DrainWorkerRecalculateDocCollectionQueue ()
    {
      Boolean Result = false;
      try
      {
        if( this.NamedQueue.PeekNamedQueue( MacroscopeConstants.RecalculateDocCollection ) )
        {
          Result = true;
          this.NamedQueue.DrainNamedQueueItemsAsList( MacroscopeConstants.RecalculateDocCollection );
        }
      }
      catch( InvalidOperationException ex )
      {
        this.DebugMsg( string.Format( "DrainWorkerRecalculateDocCollectionQueue: {0}", ex.Message ) );
      }
      return( Result );
    }

    /**************************************************************************/

    public void RecalculateDocCollection ()
    {

      lock( LockerRecalc )
      {

        if( Monitor.TryEnter( LockerDocCollection ) )
        {

          try
          {

            MacroscopeAllowedHosts AllowedHosts = this.JobMaster.GetAllowedHosts();

            this.StatsUrlsInternal = 0;
            this.StatsUrlsExternal = 0;
            this.StatsUrlsSitemaps = 0;

            foreach( string UrlTarget in this.DocCollection.Keys )
            {

              MacroscopeDocument msDoc = this.GetDocument( Url: UrlTarget );

              try
              {
                this.RecalculateInlinks( msDoc: msDoc );
              }
              catch( Exception ex )
              {
                this.DebugMsg( string.Format( "RecalculateInlinks: {0}", ex.Message ) );
              }
         
              try
              {
                this.RecalculateHyperlinksIn( msDoc: msDoc );
              }
              catch( Exception ex )
              {
                this.DebugMsg( string.Format( "RecalculateHyperlinksIn: {0}", ex.Message ) );
              }
          
              if( this.StatsHistory.ContainsKey( UrlTarget ) )
              {

                this.DebugMsg( string.Format( "RecalculateDocCollection Already Seen: {0}", UrlTarget ) );

              }
              else
              {

                this.DebugMsg( string.Format( "RecalculateDocCollection Adding: {0}", UrlTarget ) );

                this.StatsHistory.Add( UrlTarget, true );

                this.RecalculateStatsHostnames( msDoc: msDoc );

                this.RecalculateStatsTitles( msDoc: msDoc );

                this.RecalculateStatsDescriptions( msDoc: msDoc );

                this.RecalculateStatsKeywords( msDoc: msDoc );

                this.RecalculateStatsHeadings( msDoc: msDoc );

                this.RecalculateStatsWarnings( msDoc: msDoc );

                this.RecalculateStatsErrors( msDoc: msDoc );

                this.RecalculateStatsChecksums( msDoc: msDoc );

                this.RecalculateStatsLanguages( msDoc: msDoc );
                
                this.RecalculateStatsDurations( msDoc: msDoc );
            
                if( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
                {
                  this.RecalculateStatsDeepKeywordAnalysis( msDoc: msDoc );
                }
            
                if( MacroscopePreferencesManager.GetAnalyzeTextReadability() )
                {
                  this.RecalculateStatsReadabilityGrades( msDoc: msDoc );
                  this.RecalculateStatsReadabilityGradeDescriptions( msDoc: msDoc );
                }

                this.AddDocumentToSearchIndex( msDoc: msDoc );

                if( MacroscopePreferencesManager.GetResolveAddresses() )
                {
                  this.DnsLookup( msDoc: msDoc );
                }

              }

              if( AllowedHosts.IsAllowed( msDoc.GetHostname() ) )
              {
                this.StatsUrlsInternal++;
              }
              else
              {
                this.StatsUrlsExternal++;
              }

              if( msDoc.GetIsSitemapXml() )
              {
                this.StatsUrlsSitemaps++;
              }

              Thread.Yield();

            }

          }
          finally
          {
            Monitor.Exit( LockerDocCollection );
          }

          GC.Collect();
          
        }

      }

    }

    /** Inlinks ***************************************************************/

    private void RecalculateInlinks ( MacroscopeDocument msDoc )
    {

      Boolean ProcessInlinks = msDoc.GetProcessInlinks();
      string DocumentUrl = msDoc.GetUrl();
      
      DebugMsg( string.Format( "RecalculateInlinks: {0} :: {1}", ProcessInlinks, DocumentUrl ) );
      
      if( msDoc.GetProcessInlinks() )
      {

        DebugMsg( string.Format( "RecalculateInlinks: PROCESSING: {0}", DocumentUrl ) );
        
        msDoc.UnsetProcessInlinks();

        foreach( MacroscopeLink Link in msDoc.IterateOutlinks() )
        {

          string Url = Link.GetTargetUrl();
          MacroscopeLinkList Inlinks = null;
          
          if( Url.Equals( DocumentUrl ) )
          {
            DebugMsg( string.Format( "RecalculateInlinks: SELF: {0}", Url ) );
            continue;
          }

          if( this.StructInlinks.ContainsKey( Url ) )
          {
            Inlinks = this.StructInlinks[ Url ];
          }
          else
          {
            Inlinks = new MacroscopeLinkList ();
            this.StructInlinks.Add( Url, Inlinks );
          }

          if( Inlinks != null )
          {
            Inlinks.Add( Link: Link );
          }
          else
          {
            DebugMsg( string.Format( "RecalculateInlinks: NULL: {0}", DocumentUrl ) );
          }

        }

      }
      else
      {
        DebugMsg( string.Format( "RecalculateInlinks: ALREADY PROCESSED: {0}", DocumentUrl ) );
      }

    }
   
    /** Hyperlinks In *********************************************************/

    private void RecalculateHyperlinksIn ( MacroscopeDocument msDoc )
    {

      string DocumentUrl = msDoc.GetUrl();
            
      DebugMsg( string.Format( "RecalculateHyperlinksIn: {0} :: {1}", msDoc.GetProcessHyperlinksIn(), DocumentUrl ) );

      if( msDoc.GetProcessHyperlinksIn() )
      {

        MacroscopeHyperlinksOut HyperlinksOut = msDoc.GetHyperlinksOut();

        DebugMsg( string.Format( "RecalculateHyperlinksIn: PROCESSING: {0}", DocumentUrl ) );
        
        msDoc.UnsetProcessInlinks();
        msDoc.UnsetProcessHyperlinksIn();

        foreach( MacroscopeHyperlinkOut HyperlinkOut in HyperlinksOut.IterateLinks() )
        {
                  
          string Url = HyperlinkOut.GetTargetUrl();
          MacroscopeHyperlinksIn HyperlinksIn = null;

          DebugMsg( string.Format( "RecalculateHyperlinksIn: URL SOURCE: {0}", DocumentUrl ) );
          DebugMsg( string.Format( "RecalculateHyperlinksIn: URL TARGET: {0}", Url ) );

          if( Url.Equals( DocumentUrl ) )
          {
            DebugMsg( string.Format( "RecalculateHyperlinksIn: SELF: {0}", Url ) );
            continue;
          }
          
          if( this.StructHyperlinksIn.ContainsKey( Url ) )
          {
            HyperlinksIn = this.StructHyperlinksIn[ Url ];
          }
          else
          {
            HyperlinksIn = new MacroscopeHyperlinksIn ();
            this.StructHyperlinksIn.Add( Url, HyperlinksIn );
          }

          if( HyperlinksIn != null )
          {

            HyperlinksIn.Add(
              LinkType: HyperlinkOut.GetHyperlinkType(),
              Method: HyperlinkOut.GetMethod(),
              SourceUrl: DocumentUrl,
              TargetUrl: Url,
              LinkText: HyperlinkOut.GetLinkText(),
              LinkTitle: HyperlinkOut.GetLinkTitle(),
              AltText: HyperlinkOut.GetAltText(),
              ExtantGuid: HyperlinkOut.GetGuid()
            );

          }
          else
          {
            DebugMsg( string.Format( "RecalculateHyperlinksIn: NULL: {0}", DocumentUrl ) );
          }

        }

      }
      else
      {
        
        DebugMsg( string.Format( "RecalculateHyperlinksIn: ALREADY PROCESSED: {0}", DocumentUrl ) );
        
      }

    }

    /** Hostnames *************************************************************/

    private void ClearStatsHostnames ()
    {
      this.StatsHostnames.Clear();
    }

    /** -------------------------------------------------------------------- **/
        
    public Dictionary<string,int> GetStatsHostnamesWithCount ()
    {

      Dictionary<string,int> HostnamesList = null;

      lock( this.StatsHostnames )
      {

        HostnamesList = new Dictionary<string,int> ( this.StatsHostnames.Count );
        
        foreach( string Hostname in this.StatsHostnames.Keys )
        {
          HostnamesList.Add( Hostname, this.StatsHostnames[ Hostname ] );
        }
        
      }
      
      return( HostnamesList );
      
    }

    /** -------------------------------------------------------------------- **/
        
    public int GetStatsHostnamesCount ( string Hostname )
    {

      int Value = 0;

      if( this.StatsHostnames.ContainsKey( Hostname ) )
      {
        Value = this.StatsHostnames[ Hostname ];
      }

      return( Value );

    }

    /** -------------------------------------------------------------------- **/
        
    private void RecalculateStatsHostnames ( MacroscopeDocument msDoc )
    {
      string Url = msDoc.GetUrl();
      string Text = msDoc.GetHostname();

      if( !string.IsNullOrEmpty( Text ) )
      {

        Text = Text.ToLower();

        if( this.StatsHostnames.ContainsKey( Text ) )
        {
          lock( this.StatsHostnames )
          {
            this.StatsHostnames[ Text ] = this.StatsHostnames[ Text ] + 1;
          }
        }
        else
        {
          lock( this.StatsHostnames )
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
      
      if( !string.IsNullOrEmpty( Text ) )
      {

        string Hashed = Macroscope.GetStringDigest( Text: Text.ToLower() );

        if( this.StatsTitles.ContainsKey( Hashed ) )
        {

          Value = this.StatsTitles[ Hashed ];

        }
        else
        {

          DebugMsg( string.Format( "GetStatsTitleCount: {0}", Value ) );

          this.RecalculateStatsTitles( msDoc );

          if( this.StatsTitles.ContainsKey( Hashed ) )
          {
            Value = this.StatsTitles[ Hashed ];
          }

        }
      
      }

      return( Value );

    }

    /** -------------------------------------------------------------------- **/
        
    private void RecalculateStatsTitles ( MacroscopeDocument msDoc )
    {

      Boolean Proceed = false;
      int Value = -1;
      
      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      if( msDoc.GetIsHtml() )
      {
        Proceed = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        Proceed = true;
      }

      if( Proceed )
      {

        string Text = msDoc.GetTitle();
        
        if( !string.IsNullOrEmpty( Text ) )
        {
        
          string Hashed = Macroscope.GetStringDigest( Text: Text.ToLower() );
        
          lock( this.StatsTitles )
          {
          
            if( this.StatsTitles.ContainsKey( Hashed ) )
            {
            
              Value = this.StatsTitles[ Hashed ];
            
              this.StatsTitles[ Hashed ] = Value + 1;
            
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

      if( !string.IsNullOrEmpty( Text ) )
      {

        string Hashed = Macroscope.GetStringDigest( Text: Text.ToLower() );
              
        if( this.StatsDescriptions.ContainsKey( Hashed ) )
        {
          
          Value = this.StatsDescriptions[ Hashed ];
          
        }
        else
        {

          DebugMsg( string.Format( "GetStatsDescriptionCount: {0}", Value ) );

          this.RecalculateStatsDescriptions( msDoc );

          if( this.StatsDescriptions.ContainsKey( Hashed ) )
          {
            Value = this.StatsDescriptions[ Hashed ];
          }

        }
        
      }
      
      return( Value );

    }

    /** -------------------------------------------------------------------- **/
        
    private void RecalculateStatsDescriptions ( MacroscopeDocument msDoc )
    {

      Boolean Process;
      int Value = -1;
      
      if( msDoc.GetIsHtml() )
      {
        Process = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        Process = true;
      }
      else
      {
        Process = false;
      }

      if( Process )
      {

        string Text = msDoc.GetDescription();

        if( !string.IsNullOrEmpty( Text ) )
        {

          string Hashed = Macroscope.GetStringDigest( Text: Text.ToLower() );

          lock( this.StatsDescriptions )
          {
            
            if( this.StatsDescriptions.ContainsKey( Hashed ) )
            {
              
              Value = this.StatsDescriptions[ Hashed ] + 1;
              
              this.StatsDescriptions[ Hashed ] = Value;

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

      if( !string.IsNullOrEmpty( Text ) )
      {

        string Hashed = Macroscope.GetStringDigest( Text: Text.ToLower() );
        
        if( this.StatsKeywords.ContainsKey( Hashed ) )
        {

          Value = this.StatsKeywords[ Hashed ];

        }
        else
        {

          DebugMsg( string.Format( "GetStatsKeywordsCount: {0}", Value ) );

          this.RecalculateStatsKeywords( msDoc );

          if( this.StatsKeywords.ContainsKey( Hashed ) )
          {
            Value = this.StatsKeywords[ Hashed ];
          }

        }
        
      }

      return( Value );

    }

    /** -------------------------------------------------------------------- **/
        
    private void RecalculateStatsKeywords ( MacroscopeDocument msDoc )
    {

      Boolean Proceed = false;
      int Value = -1;
      
      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      if( msDoc.GetIsHtml() )
      {
        Proceed = true;
      }

      if( Proceed )
      {

        string Text = msDoc.GetKeywords();

        if( !string.IsNullOrEmpty( Text ) )
        {

          string Hashed = Macroscope.GetStringDigest( Text: Text.ToLower() );
        
          lock( this.StatsKeywords )
          {

            if( this.StatsKeywords.ContainsKey( Hashed ) )
            {

              Value = this.StatsKeywords[ Hashed ] + 1;
              
              this.StatsKeywords[ Hashed ] = Value;

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
      lock( this.StatsWarnings )
      {
        this.StatsWarnings.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/
        
    public Dictionary<string,int> GetStatsWarningsCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsWarnings.Count );
      lock( this.StatsWarnings )
      {
        foreach( string Key in this.StatsWarnings.Keys )
        {
          dicStats.Add( Key, this.StatsWarnings[ Key ] );
        }
      }
      return( dicStats );
    }

    /** -------------------------------------------------------------------- **/
    
    private void RecalculateStatsWarnings ( MacroscopeDocument msDoc )
    {

      string ErrorCondition = msDoc.GetErrorCondition();
      HttpStatusCode StatusCode = msDoc.GetStatusCode();

      if( ( ( int )StatusCode >= 300 ) && ( ( int )StatusCode <= 399 ) )
      {

        string Text = string.Format( "{0} ({1})", StatusCode, ErrorCondition );

        DebugMsg( string.Format( "RecalculateStatsWarnings: {0}", Text ) );

        lock( this.StatsWarnings )
        {

          if( this.StatsWarnings.ContainsKey( Text ) )
          {
            this.StatsWarnings[ Text ] = this.StatsWarnings[ Text ] + 1;
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
      lock( this.StatsErrors )
      {
        this.StatsErrors.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/
        
    public Dictionary<string,int> GetStatsErrorsCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsErrors.Count );
      lock( this.StatsErrors )
      {
        foreach( string Key in this.StatsErrors.Keys )
        {
          dicStats.Add( Key, this.StatsErrors[ Key ] );
        }
      }
      return( dicStats );
    }

    /** -------------------------------------------------------------------- **/
        
    private void RecalculateStatsErrors ( MacroscopeDocument msDoc )
    {

      string ErrorCondition = msDoc.GetErrorCondition();
      HttpStatusCode StatusCode = msDoc.GetStatusCode();

      if( ( ( int )StatusCode >= 400 ) && ( ( int )StatusCode <= 599 ) )
      {

        string Text = string.Format( "{0} ({1})", StatusCode, ErrorCondition );

        DebugMsg( string.Format( "RecalculateStatsErrors: {0}", Text ) );

        lock( this.StatsErrors )
        {

          if( this.StatsErrors.ContainsKey( Text ) )
          {
            this.StatsErrors[ Text ] = this.StatsErrors[ Text ] + 1;
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
      lock( this.StatsChecksums )
      {
        this.StatsChecksums.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/
        
    public int GetStatsChecksumCount ( string Checksum )
    {
      int Count = 0;
      lock( this.StatsChecksums )
      {
        if( this.StatsChecksums.ContainsKey( Checksum ) )
        {
          Count = this.StatsChecksums[ Checksum ];
        }
      }
      return( Count );
    }

    /** -------------------------------------------------------------------- **/
        
    public Dictionary<string,int> GetStatsChecksumsCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsChecksums.Count );
      lock( this.StatsChecksums )
      {
        foreach( string Key in this.StatsChecksums.Keys )
        {
          dicStats.Add( Key, this.StatsChecksums[ Key ] );
        }
      }
      return( dicStats );
    }

    /** -------------------------------------------------------------------- **/
        
    private void RecalculateStatsChecksums ( MacroscopeDocument msDoc )
    {

      string ChecksumValue = msDoc.GetChecksum();

      if( ChecksumValue.Length > 0 )
      {

        DebugMsg( string.Format( "RecalculateStatsChecksums: {0}", ChecksumValue ) );

        lock( this.StatsChecksums )
        {

          if( this.StatsChecksums.ContainsKey( ChecksumValue ) )
          {
            this.StatsChecksums[ ChecksumValue ] = this.StatsChecksums[ ChecksumValue ] + 1;
          }
          else
          {
            this.StatsChecksums.Add( ChecksumValue, 1 );
          }

        }

      }

    }

    /** Languages Stats *******************************************************/

    private void ClearStatsLanguages ()
    {

      lock( this.StatsLanguagesPages )
      {
        this.StatsLanguagesPages.Clear();
      }

      lock( this.StatsLanguagesTitles )
      {
        this.StatsLanguagesTitles.Clear();
      }

      lock( this.StatsLanguagesDescriptions )
      {
        this.StatsLanguagesDescriptions.Clear();
      }

      lock( this.StatsLanguagesBodyTexts )
      {
        this.StatsLanguagesBodyTexts.Clear();
      }
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string,int> GetStatsLanguagesPagesCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsLanguagesPages.Count );
      lock( this.StatsLanguagesPages )
      {
        foreach( string Key in this.StatsLanguagesPages.Keys )
        {
          dicStats.Add( Key, this.StatsLanguagesPages[ Key ] );
        }
      }
      return( dicStats );
    }

    public Dictionary<string,int> GetStatsLanguagesTitlesCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsLanguagesTitles.Count );
      lock( this.StatsLanguagesTitles )
      {
        foreach( string Key in this.StatsLanguagesTitles.Keys )
        {
          dicStats.Add( Key, this.StatsLanguagesTitles[ Key ] );
        }
      }
      return( dicStats );
    }
        
    public Dictionary<string,int> GetStatsLanguagesDescriptionsCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsLanguagesDescriptions.Count );
      lock( this.StatsLanguagesDescriptions )
      {
        foreach( string Key in this.StatsLanguagesDescriptions.Keys )
        {
          dicStats.Add( Key, this.StatsLanguagesDescriptions[ Key ] );
        }
      }
      return( dicStats );
    }
        
    public Dictionary<string,int> GetStatsLanguagesBodyTextsCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsLanguagesBodyTexts.Count );
      lock( this.StatsLanguagesBodyTexts )
      {
        foreach( string Key in this.StatsLanguagesBodyTexts.Keys )
        {
          dicStats.Add( Key, this.StatsLanguagesBodyTexts[ Key ] );
        }
      }
      return( dicStats );
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsLanguages ( MacroscopeDocument msDoc )
    {

      string PageLanguage = msDoc.GetIsoLanguageCode();
      string TitleLanguage = msDoc.GetTitleLanguage();
      string DescriptionLanguage = msDoc.GetDescriptionLanguage();
      string BodyTextLanguage = msDoc.GetDocumentTextLanguage();

      if( string.IsNullOrEmpty( PageLanguage ) )
      {
        PageLanguage = "unknown";
      }
      
      if( string.IsNullOrEmpty( TitleLanguage ) )
      {
        TitleLanguage = "unknown";
      }
      
      if( string.IsNullOrEmpty( DescriptionLanguage ) )
      {
        DescriptionLanguage = "unknown";
      }
      
      if( string.IsNullOrEmpty( BodyTextLanguage ) )
      {
        BodyTextLanguage = "unknown";
      }

      if( !string.IsNullOrEmpty( PageLanguage ) )
      {

        DebugMsg( string.Format( "RecalculateStatsLanguages: {0}", PageLanguage ) );

        lock( this.StatsLanguagesPages )
        {

          if( this.StatsLanguagesPages.ContainsKey( PageLanguage ) )
          {
            this.StatsLanguagesPages[ PageLanguage ] = this.StatsLanguagesPages[ PageLanguage ] + 1;
          }
          else
          {
            this.StatsLanguagesPages.Add( PageLanguage, 1 );
          }

        }

      }
      
      if( !string.IsNullOrEmpty( TitleLanguage ) )
      {

        DebugMsg( string.Format( "RecalculateStatsLanguages: {0}", TitleLanguage ) );

        lock( this.StatsLanguagesTitles )
        {

          if( this.StatsLanguagesTitles.ContainsKey( TitleLanguage ) )
          {
            this.StatsLanguagesTitles[ TitleLanguage ] = this.StatsLanguagesTitles[ TitleLanguage ] + 1;
          }
          else
          {
            this.StatsLanguagesTitles.Add( TitleLanguage, 1 );
          }

        }

      }
      
      if( !string.IsNullOrEmpty( DescriptionLanguage ) )
      {

        DebugMsg( string.Format( "RecalculateStatsLanguages: {0}", DescriptionLanguage ) );

        lock( this.StatsLanguagesDescriptions )
        {

          if( this.StatsLanguagesDescriptions.ContainsKey( DescriptionLanguage ) )
          {
            this.StatsLanguagesDescriptions[ DescriptionLanguage ] = this.StatsLanguagesDescriptions[ DescriptionLanguage ] + 1;
          }
          else
          {
            this.StatsLanguagesDescriptions.Add( DescriptionLanguage, 1 );
          }

        }

      }
      
      if( !string.IsNullOrEmpty( BodyTextLanguage ) )
      {

        DebugMsg( string.Format( "RecalculateStatsLanguages: {0}", BodyTextLanguage ) );

        lock( this.StatsLanguagesBodyTexts )
        {

          if( this.StatsLanguagesBodyTexts.ContainsKey( BodyTextLanguage ) )
          {
            this.StatsLanguagesBodyTexts[ BodyTextLanguage ] = this.StatsLanguagesBodyTexts[ BodyTextLanguage ] + 1;
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
      lock( this.StatsDurations )
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
      
      if( this.StatsDurations.Count > 0 )
      {
        
        lock( this.StatsDurations )
        {
          foreach( string Url in this.StatsDurations.Keys )
          {
            Count++;
            Maximus += this.StatsDurations[ Url ];
          }
        }
      
        if( Count > 0 )
        {
          Average = Maximus / Count;
        }
        
      }
      
      return( Average );
    }

    /** -------------------------------------------------------------------- **/

    public decimal GetStatsDurationsFastest ()
    {
      decimal Fastest = -1; 
      if( this.StatsDurations.Count > 0 )
      {   
        lock( this.StatsDurations )
        {
          foreach( string Url in this.StatsDurations.Keys )
          {
            if( Fastest == -1 )
            {
              Fastest = this.StatsDurations[ Url ];
            }
            else
            {
              if( this.StatsDurations[ Url ] <= Fastest )
              {
                Fastest = this.StatsDurations[ Url ];
              }
            }
          }
        }
      } 
      return( Fastest );
    }

    /** -------------------------------------------------------------------- **/

    public decimal GetStatsDurationsSlowest ()
    {
      decimal Slowest = -1; 
      if( this.StatsDurations.Count > 0 )
      {   
        lock( this.StatsDurations )
        {
          foreach( string Url in this.StatsDurations.Keys )
          {
            if( Slowest == -1 )
            {
              Slowest = this.StatsDurations[ Url ];
            }
            else
            {
              if( this.StatsDurations[ Url ] >= Slowest )
              {
                Slowest = this.StatsDurations[ Url ];
              }
            }
          }
        }
      } 
      return( Slowest );
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsDurations ( MacroscopeDocument msDoc )
    {
      string Url = msDoc.GetUrl();
      lock( this.StatsDurations )
      {
        if( this.StatsDurations.ContainsKey( Url ) )
        {
          this.StatsDurations[ Url ] = msDoc.GetDurationInSeconds();
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
      lock( this.StatsDeepKeywordAnalysis )
      {
        for( int i = 0 ; i < 4 ; i++ )
        {
          lock( this.StatsDeepKeywordAnalysis[i] )
          {
            this.StatsDeepKeywordAnalysis[ i ].Clear();
          }
        }
        lock( this.StatsDeepKeywordAnalysisDocs )
        {
          this.StatsDeepKeywordAnalysisDocs.Clear();
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    private void RecalculateStatsDeepKeywordAnalysis ( MacroscopeDocument msDoc )
    {

      Boolean Proceed = false;

      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      if( msDoc.GetIsHtml() )
      {
        Proceed = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        Proceed = true;
      }
      
      if( Proceed )
      {
        
        string DocLang = msDoc.GetIsoLanguageCode();
        
        if( DocLang != null )
        {
          if( Regex.IsMatch( msDoc.GetIsoLanguageCode(), "^(x-default|en|fr|de|it|es|po)", RegexOptions.IgnoreCase ) )
          {
            lock( this.StatsDeepKeywordAnalysis )
            {
              for( int i = 0 ; i < 4 ; i++ )
              {
                this.AnalyzeKeywords.Analyze(
                  msDoc: msDoc,
                  Text: msDoc.GetDocumentTextCleaned(),
                  Terms: this.StatsDeepKeywordAnalysis[ i ],
                  Words: i + 1
                );
              }
            }
          }
        }
      
      }
      
    }

    /** -------------------------------------------------------------------- **/

    public Dictionary<string,int> GetDeepKeywordAnalysisAsDictonary ( int Words )
    {
      
      int WordsOffset = Words - 1;
      Dictionary<string,int> Terms = new Dictionary<string,int> ( this.StatsDeepKeywordAnalysis[ WordsOffset ].Count );

      lock( this.StatsDeepKeywordAnalysis[WordsOffset] )
      {
        foreach( string Term in this.StatsDeepKeywordAnalysis[WordsOffset].Keys )
        {
          Terms.Add( Term, this.StatsDeepKeywordAnalysis[ WordsOffset ][ Term ] );
        }
      }
      
      return( Terms );
      
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeDocumentList GetDeepKeywordAnalysDocumentList ( string KeywordTerm )
    {

      MacroscopeDocumentList DocumentList = null;      

      if( this.StatsDeepKeywordAnalysisDocs.ContainsKey( KeywordTerm ) )
      {
        DocumentList = this.StatsDeepKeywordAnalysisDocs[ KeywordTerm ];
      }

      return( DocumentList );

    }

    /** Readability Analysis **************************************************/

    private void ClearStatsReadabilityGrades ()
    {
      lock( this.StatsReadabilityGrades )
      {
        lock( this.StatsReadabilityGradeDescriptions )
        {
          this.StatsReadabilityGrades.Clear();
          this.StatsReadabilityGradeDescriptions.Clear();
        }
      }
    }

    /** -------------------------------------------------------------------- **/
        
    public SortedDictionary<string,int> GetStatsReadabilityGradesCount ()
    {
      SortedDictionary<string,int> dicStats = new SortedDictionary<string, int> ();
      lock( this.StatsReadabilityGrades )
      {
        foreach( string Key in this.StatsReadabilityGrades.Keys )
        {
          dicStats.Add( Key, this.StatsReadabilityGrades[ Key ] );
        }
      }
      return( dicStats );
    }
    
    /** -------------------------------------------------------------------- **/
        
    public SortedDictionary<string,int> GetStatsReadabilityGradeStringsCount ()
    {
      SortedDictionary<string,int> dicStats = new SortedDictionary<string, int> ();
      lock( this.StatsReadabilityGradeDescriptions )
      {
        foreach( string Key in this.StatsReadabilityGradeDescriptions.Keys )
        {
          dicStats.Add( Key, this.StatsReadabilityGradeDescriptions[ Key ] );
        }
      }
      return( dicStats );
    }
    
    /** -------------------------------------------------------------------- **/
            
    private void RecalculateStatsReadabilityGrades ( MacroscopeDocument msDoc )
    {

      Boolean Proceed = false;

      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      if( msDoc.GetIsHtml() || msDoc.GetIsPdf() )
      {
        Proceed = true;
      }

      if( Proceed )
      {

        lock( this.StatsReadabilityGrades )
        {
          string Grade;
          
          Grade = string.Format(
            "{0} / {1}",
            MacroscopeAnalyzeReadability.FormatAnalyzeReadabilityMethod(
              ReadabilityMethod: msDoc.GetReadabilityGradeMethod()
            ),
            msDoc.GetReadabilityGrade().ToString( "00.00" )
          );

          if( this.StatsReadabilityGrades.ContainsKey( Grade ) )
          {
            this.StatsReadabilityGrades[ Grade ] = this.StatsReadabilityGrades[ Grade ] + 1;
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

      Boolean Proceed = false;

      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      if( msDoc.GetIsHtml() || msDoc.GetIsPdf() )
      {
        Proceed = true;
      }

      if( Proceed )
      {

        lock( this.StatsReadabilityGradeDescriptions )
        {
          string Grade;
          
          Grade = string.Format(
            "{0} / {1}",
            MacroscopeAnalyzeReadability.FormatAnalyzeReadabilityMethod(
              ReadabilityMethod: msDoc.GetReadabilityGradeMethod()
            ),
            msDoc.GetReadabilityGradeDescription()
          );

          if( this.StatsReadabilityGradeDescriptions.ContainsKey( Grade ) )
          {
            this.StatsReadabilityGradeDescriptions[ Grade ] = this.StatsReadabilityGradeDescriptions[ Grade ] + 1;
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
      lock( this.StatsHeadings )
      {
        for( ushort HeadingLevel = 1 ; HeadingLevel <= 6 ; HeadingLevel++ )
        {
          lock( this.StatsHeadings[HeadingLevel] )
          {
            this.StatsHeadings[ HeadingLevel ].Clear();
          }
        }
        lock( this.StatsHeadings )
        {
          this.StatsHeadings.Clear();
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public int GetStatsHeadingsCount ( ushort HeadingLevel, string Text )
    {

      int Count = 0;
      string Hashed = Macroscope.GetStringDigest( Text: Text.ToLower() );
      
      if( this.StatsHeadings[ HeadingLevel ].ContainsKey( Hashed ) )
      {
        Count = this.StatsHeadings[ HeadingLevel ][ Hashed ];
      }

      return( Count );

    }
        
    /** -------------------------------------------------------------------- **/
            
    private void RecalculateStatsHeadings ( MacroscopeDocument msDoc )
    {

      Boolean Proceed = false;

      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      if( msDoc.GetIsHtml() )
      {
        Proceed = true;
      }

      if( Proceed )
      {

        lock( this.StatsHeadings )
        {
          
          for( ushort HeadingLevel = 1 ; HeadingLevel <= MacroscopePreferencesManager.GetMaxHeadingDepth() ; HeadingLevel++ )
          {

            List<string> Headings = msDoc.GetHeadings( HeadingLevel: HeadingLevel );

            foreach( string Text in Headings )
            {

              string Hashed = Macroscope.GetStringDigest( Text: Text.ToLower() );
      
              if( this.StatsHeadings[ HeadingLevel ].ContainsKey( Hashed ) )
              {
                this.StatsHeadings[ HeadingLevel ][ Hashed ] = this.StatsHeadings[ HeadingLevel ][ Hashed ] + 1;
              }
              else
              {
                this.StatsHeadings[ HeadingLevel ].Add( Hashed, 1 );
              }

            }
        
          }
        
        }
      
      }
      
    }

    /** Search Index **********************************************************/

    public MacroscopeSearchIndex GetSearchIndex ()
    {
      return( this.SearchIndex );
    }

    /** -------------------------------------------------------------------- **/
        
    private void AddDocumentToSearchIndex ( MacroscopeDocument msDoc )
    {
      
      if( MacroscopePreferencesManager.GetEnableTextIndexing() )
      {

        Boolean Proceed = false;
      
        if( msDoc.GetIsHtml() )
        {
          Proceed = true;
        }
        else
        if( msDoc.GetIsPdf() )
        {
          Proceed = true;
        }
      
        if( Proceed )
        {
          this.SearchIndex.AddDocumentToIndex( msDoc );
        }

      }
      
    }

    /** DNS Lookup ************************************************************/

    public void DnsLookup ( MacroscopeDocument msDoc )
    {

      IPHostEntry HostEntry = null;
      string ServerName = msDoc.GetServerName();

      if( this.DnsCache.ContainsKey( ServerName ) )
      {

        msDoc.SetServerAddresses( HostEntry: this.DnsCache[ ServerName ] );

      }
      else
      {

        HostEntry = msDoc.SetServerAddresses();

        lock( this.DnsCache )
        {
          
          if( !this.DnsCache.ContainsKey( ServerName ) )
          {
            this.DnsCache.Add( ServerName, HostEntry );
          }

        }

      }

    }

    /**************************************************************************/

  }

}
