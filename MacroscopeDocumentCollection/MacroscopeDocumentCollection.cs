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
      
    private Dictionary<string,Boolean> StatsHistory;
    private Dictionary<string,int> StatsHostnames;
    private Dictionary<string,int> StatsTitles;
    private Dictionary<string,int> StatsDescriptions;
    private Dictionary<string,int> StatsKeywords;
    private Dictionary<string,int> StatsWarnings;
    private Dictionary<string,int> StatsErrors;
    private Dictionary<string,int> StatsChecksums;
    private Dictionary<string,int> StatsDeepKeywordAnalysis;

    private int StatsUrlsInternal;
    private int StatsUrlsExternal;
    private int StatsUrlsSitemaps;

    private Semaphore SemaphoreRecalc;
    private System.Timers.Timer TimerRecalc;

    /**************************************************************************/

    public MacroscopeDocumentCollection ( MacroscopeJobMaster JobMasterNew )
    {

      this.SuppressDebugMsg = true;

      this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZING..." );

      this.DocCollection = new Dictionary<string,MacroscopeDocument> ( 4096 );

      this.JobMaster = JobMasterNew;

      this.NamedQueue = new MacroscopeNamedQueue ();
      this.NamedQueue.CreateNamedQueue( MacroscopeConstants.RecalculateDocCollection );

      this.SearchIndex = new MacroscopeSearchIndex ();

      this.AnalyzeKeywords = new MacroscopeDeepKeywordAnalysis ();
          
      this.StatsHistory = new Dictionary<string,Boolean> ( 1024 );
      this.StatsHostnames = new Dictionary<string,int> ( 16 );
      this.StatsTitles = new Dictionary<string,int> ( 1024 );
      this.StatsDescriptions = new Dictionary<string,int> ( 1024 );
      this.StatsKeywords = new Dictionary<string,int> ( 1024 );
      this.StatsWarnings = new  Dictionary<string,int> ( 32 );
      this.StatsErrors = new  Dictionary<string,int> ( 32 );
      this.StatsChecksums = new  Dictionary<string,int> ( 1024 );
      this.StatsDeepKeywordAnalysis = new  Dictionary<string,int> ( 1024 );

      this.StatsUrlsInternal = 0;
      this.StatsUrlsExternal = 0;
      this.StatsUrlsSitemaps = 0;

      this.SemaphoreRecalc = new Semaphore ( 0, 1 );
      this.StartRecalcTimer();

      this.DebugMsg( "MacroscopeDocumentCollection: INITIALIZED." );

    }

    /**************************************************************************/

    ~MacroscopeDocumentCollection ()
    {
      this.DebugMsg( "MacroscopeDocumentCollection DESTRUCTOR CALLED" );
      this.StopRecalcTimer();
    }

    /** Document Collection Methods *******************************************/

    public Boolean ContainsDocument ( string sKey )
    {
      Boolean sResult = false;
      if( this.DocCollection.ContainsKey( sKey ) )
      {
        sResult = true;
      }
      return( sResult );
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

    // TODO: There may be a bug here, whereby two or more error pages are added multiple times.

    public void AddDocument ( string sKey, MacroscopeDocument msDoc )
    {
      lock( this.DocCollection )
      {
        if( this.DocCollection.ContainsKey( sKey ) )
        {
          this.DocCollection.Remove( sKey );
          this.DocCollection.Add( sKey, msDoc );
        }
        else
        {
          try
          {
            this.DocCollection.Add( sKey, msDoc );
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
    }

    /**************************************************************************/

    public Boolean DocumentExists ( string sKey )
    {
      Boolean bExists = false;
      if( this.DocCollection.ContainsKey( sKey ) )
      {
        bExists = true;
      }
      return( bExists );
    }

    /**************************************************************************/

    public MacroscopeDocument GetDocument ( string sKey )
    {
      MacroscopeDocument msDoc = null;
      if( this.DocCollection.ContainsKey( sKey ) )
      {
        msDoc = ( MacroscopeDocument )this.DocCollection[ sKey ];
      }
      return( msDoc );
    }

    /**************************************************************************/

    public void RemoveDocument ( string sKey )
    {
      if( this.DocCollection.ContainsKey( sKey ) )
      {
        lock( this.DocCollection )
        {
          this.DocCollection.Remove( sKey );
        }
      }
    }

    /**************************************************************************/

    public IEnumerable IterateDocuments ()
    {
      lock( this.DocCollection )
      {
        foreach( string sUrl in this.DocumentKeys() )
        {
          yield return this.DocCollection[ sUrl ];
        }
      }
    }

    /**************************************************************************/

    public List<string> DocumentKeys ()
    {
      List<string> lKeys = new List<string> ();
      lock( this.DocCollection )
      {
        foreach( string sKey in this.DocCollection.Keys )
        {
          lKeys.Add( sKey );
        }
      }
      return( lKeys );
    }

    /** Recalculate Stats Across DocCollection ********************************/

    void StartRecalcTimer ()
    {
      this.DebugMsg( string.Format( "StartRecalcTimer: {0}", "STARTING..." ) );
      SemaphoreRecalc.Release( 1 );
      this.DebugMsg( string.Format( "StartRecalcTimer SemaphoreRecalc: {0}", "RELEASED" ) );
      this.TimerRecalc = new System.Timers.Timer ( 2000 );
      this.TimerRecalc.Elapsed += this.WorkerRecalculateDocCollection;
      this.TimerRecalc.AutoReset = true;
      this.TimerRecalc.Enabled = true;
      this.TimerRecalc.Start();
      this.DebugMsg( string.Format( "StartRecalcTimer: {0}", "STARTED." ) );
    }

    void StopRecalcTimer ()
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

    void WorkerRecalculateDocCollection ( Object self, ElapsedEventArgs e )
    {
      //DebugMsg( string.Format( "WorkerRecalculateDocCollection: {0}", "CALLED" ) );
      try
      {
        Boolean bDrainQueue = this.DrainWorkerRecalculateDocCollectionQueue();
        //DebugMsg( string.Format( "bDrainQueue: {0}", bDrainQueue ) );
        if( bDrainQueue )
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
      this.NamedQueue.AddToNamedQueue( MacroscopeConstants.RecalculateDocCollection, "calc" );
    }

    /**************************************************************************/

    public Boolean DrainWorkerRecalculateDocCollectionQueue ()
    {
      Boolean bResult = false;
      try
      {
        if( this.NamedQueue.PeekNamedQueue( MacroscopeConstants.RecalculateDocCollection ) )
        {
          bResult = true;
          this.NamedQueue.DrainNamedQueueItemsAsList( MacroscopeConstants.RecalculateDocCollection );
        }
      }
      catch( InvalidOperationException ex )
      {
        this.DebugMsg( string.Format( "DrainWorkerRecalculateDocCollectionQueue: {0}", ex.Message ) );
      }
      return( bResult );
    }

    /**************************************************************************/

    public void RecalculateDocCollection ()
    {

      this.DebugMsg( string.Format( "RecalculateDocCollection: CALLED" ) );

      SemaphoreRecalc.WaitOne();

      lock( this.DocCollection )
      {

        MacroscopeAllowedHosts AllowedHosts = this.JobMaster.GetAllowedHosts();

        this.StatsUrlsInternal = 0;
        this.StatsUrlsExternal = 0;
        this.StatsUrlsSitemaps = 0;

        foreach( string sUrlTarget in this.DocCollection.Keys )
        {

          MacroscopeDocument msDoc = this.GetDocument( sUrlTarget );

          this.RecalculateLinksIn( sUrlTarget, msDoc );

          if( this.StatsHistory.ContainsKey( sUrlTarget ) )
          {

            this.DebugMsg( string.Format( "RecalculateDocCollection Already Seen: {0}", sUrlTarget ) );

          }
          else
          {

            this.DebugMsg( string.Format( "RecalculateDocCollection Adding: {0}", sUrlTarget ) );

            this.StatsHistory.Add( sUrlTarget, true );

            this.RecalculateStatsHostnames( msDoc );

            this.RecalculateStatsTitles( msDoc );

            this.RecalculateStatsDescriptions( msDoc );

            this.RecalculateStatsKeywords( msDoc );

            this.RecalculateStatsWarnings( msDoc );

            this.RecalculateStatsErrors( msDoc );

            this.RecalculateStatsChecksums( msDoc );

            if( MacroscopePreferencesManager.GetAnalyzeKeywordsInText() )
            {
              this.RecalculateStatsDeepKeywordAnalysis( msDoc );
            }
            
            this.AddDocumentToSearchIndex( msDoc );

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

        }

      }

      SemaphoreRecalc.Release();

    }

    /** Hyperlinks In *********************************************************/

    void RecalculateLinksIn ( string sUrlTarget, MacroscopeDocument msDoc )
    {

      msDoc.ClearHyperlinksIn();

      foreach( string sUrlOrigin in this.DocCollection.Keys )
      {

        foreach( MacroscopeHyperlinkOut HyperlinkOut in msDoc.GetHyperlinksOut().IterateLinks( sUrlTarget ) )
        {

          if( sUrlTarget == HyperlinkOut.GetUrlTarget() )
          {

            msDoc.AddHyperlinkIn(
              HyperlinkOut.GetHyperlinkType(),
              HyperlinkOut.GetMethod(),
              sUrlOrigin,
              sUrlTarget,
              HyperlinkOut.GetLinkText(),
              HyperlinkOut.GetAltText()
            );

          }

        }

      }

    }

    /** Hostnames *************************************************************/

    void ClearStatsHostnames ()
    {
      this.StatsHostnames.Clear();
    }

    public Dictionary<string,int> GetStatsHostnamesWithCount ()
    {
      Dictionary<string,int> dicHostnames = new Dictionary<string,int> ( this.StatsHostnames.Count );
      lock( this.StatsHostnames )
      {
        foreach( string sHostname in this.StatsHostnames.Keys )
        {
          dicHostnames.Add( sHostname, this.StatsHostnames[ sHostname ] );
        }
      }
      return( dicHostnames );
    }

    public int GetStatsHostnamesCount ( string sText )
    {
      int iValue = 0;
      if( this.StatsHostnames.ContainsKey( sText ) )
      {
        iValue = this.StatsHostnames[ sText ];
      }
      return( iValue );
    }

    void RecalculateStatsHostnames ( MacroscopeDocument msDoc )
    {
      string sUrl = msDoc.GetUrl();
      string sText = msDoc.GetHostname();

      if( ( sText != null ) && ( sText.Length > 0 ) )
      {

        sText = sText.ToLower();

        if( this.StatsHostnames.ContainsKey( sText ) )
        {
          lock( this.StatsHostnames )
          {
            this.StatsHostnames[ sText ] = this.StatsHostnames[ sText ] + 1;
          }
        }
        else
        {
          lock( this.StatsHostnames )
          {
            this.StatsHostnames.Add( sText, 1 );
          }
        }

      }

    }

    /** Titles ****************************************************************/

    void ClearStatsTitles ()
    {
      this.StatsTitles.Clear();
    }

    public int GetStatsTitleCount ( string sText )
    {
      int iValue = 0;
      string sHashed = sText.GetHashCode().ToString();
      if( this.StatsTitles.ContainsKey( sHashed ) )
      {
        iValue = this.StatsTitles[ sHashed ];
      }
      return( iValue );
    }

    void RecalculateStatsTitles ( MacroscopeDocument msDoc )
    {

      Boolean bProcess;

      if( msDoc.GetIsHtml() )
      {
        bProcess = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        bProcess = true;
      }
      else
      {
        bProcess = false;
      }

      if( bProcess )
      {

        string sUrl = msDoc.GetUrl();
        string sText = msDoc.GetTitle();
        string sHashed = sText.GetHashCode().ToString();

        if( this.StatsTitles.ContainsKey( sHashed ) )
        {
          lock( this.StatsTitles )
          {
            this.StatsTitles[ sHashed ] = this.StatsTitles[ sHashed ] + 1;
          }
        }
        else
        {
          lock( this.StatsTitles )
          {
            this.StatsTitles.Add( sHashed, 1 );
          }
        }

      }

    }

    /** Descriptions **********************************************************/

    void ClearStatsDescriptions ()
    {
      this.StatsDescriptions.Clear();
    }

    public int GetStatsDescriptionCount ( string sText )
    {
      int iValue = 0;
      string sHashed = sText.GetHashCode().ToString();
      if( this.StatsDescriptions.ContainsKey( sHashed ) )
      {
        iValue = this.StatsDescriptions[ sHashed ];
      }
      return( iValue );
    }

    void RecalculateStatsDescriptions ( MacroscopeDocument msDoc )
    {

      Boolean bProcess;

      if( msDoc.GetIsHtml() )
      {
        bProcess = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        bProcess = true;
      }
      else
      {
        bProcess = false;
      }

      if( bProcess )
      {

        string sUrl = msDoc.GetUrl();
        string sText = msDoc.GetDescription();
        string sHashed = sText.GetHashCode().ToString();

        if( this.StatsDescriptions.ContainsKey( sHashed ) )
        {
          lock( this.StatsDescriptions )
          {
            this.StatsDescriptions[ sHashed ] = this.StatsDescriptions[ sHashed ] + 1;
          }
        }
        else
        {
          lock( this.StatsDescriptions )
          {
            this.StatsDescriptions.Add( sHashed, 1 );
          }
        }

      }

    }

    /** Keywords **************************************************************/

    void ClearStatsKeywords ()
    {
      this.StatsKeywords.Clear();
    }

    public int GetStatsKeywordsCount ( string sText )
    {
      int iValue = 0;
      string sHashed = sText.GetHashCode().ToString();
      if( this.StatsKeywords.ContainsKey( sHashed ) )
      {
        iValue = this.StatsKeywords[ sHashed ];
      }
      return( iValue );
    }

    void RecalculateStatsKeywords ( MacroscopeDocument msDoc )
    {

      Boolean bProcess;

      if( msDoc.GetIsHtml() )
      {
        bProcess = true;
      }
      else
      {
        bProcess = false;
      }

      if( bProcess )
      {

        string sUrl = msDoc.GetUrl();
        string sText = msDoc.GetKeywords();
        string sHashed = sText.GetHashCode().ToString();

        if( this.StatsKeywords.ContainsKey( sHashed ) )
        {
          lock( this.StatsKeywords )
          {
            this.StatsKeywords[ sHashed ] = this.StatsKeywords[ sHashed ] + 1;
          }
        }
        else
        {
          lock( this.StatsKeywords )
          {
            this.StatsKeywords.Add( sHashed, 1 );
          }
        }

      }

    }

    /** Warnings ****************************************************************/

    void ClearStatsWarnings ()
    {
      lock( this.StatsWarnings )
      {
        this.StatsWarnings.Clear();
      }
    }

    public Dictionary<string,int> GetStatsWarningsCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsWarnings.Count );
      lock( this.StatsWarnings )
      {
        foreach( string sKey in this.StatsWarnings.Keys )
        {
          dicStats.Add( sKey, this.StatsWarnings[ sKey ] );
        }
      }
      return( dicStats );
    }

    void RecalculateStatsWarnings ( MacroscopeDocument msDoc )
    {

      string sErrorCondition = msDoc.GetErrorCondition();
      int iStatusCode = msDoc.GetStatusCode();

      if( ( iStatusCode >= 300 ) && ( iStatusCode <= 399 ) )
      {

        string sText = string.Format( "{0} ({1})", iStatusCode, sErrorCondition );

        DebugMsg( string.Format( "RecalculateStatsWarnings: {0}", sText ) );

        lock( this.StatsWarnings )
        {

          if( this.StatsWarnings.ContainsKey( sText ) )
          {
            this.StatsWarnings[ sText ] = this.StatsWarnings[ sText ] + 1;
          }
          else
          {
            this.StatsWarnings.Add( sText, 1 );
          }

        }

      }

    }

    /** Errors ****************************************************************/

    void ClearStatsErrors ()
    {
      lock( this.StatsErrors )
      {
        this.StatsErrors.Clear();
      }
    }

    public Dictionary<string,int> GetStatsErrorsCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsErrors.Count );
      lock( this.StatsErrors )
      {
        foreach( string sKey in this.StatsErrors.Keys )
        {
          dicStats.Add( sKey, this.StatsErrors[ sKey ] );
        }
      }
      return( dicStats );
    }

    void RecalculateStatsErrors ( MacroscopeDocument msDoc )
    {

      string sErrorCondition = msDoc.GetErrorCondition();
      int iStatusCode = msDoc.GetStatusCode();

      if( ( iStatusCode >= 400 ) && ( iStatusCode <= 599 ) )
      {

        string sText = string.Format( "{0} ({1})", iStatusCode, sErrorCondition );

        DebugMsg( string.Format( "RecalculateStatsErrors: {0}", sText ) );

        lock( this.StatsErrors )
        {

          if( this.StatsErrors.ContainsKey( sText ) )
          {
            this.StatsErrors[ sText ] = this.StatsErrors[ sText ] + 1;
          }
          else
          {
            this.StatsErrors.Add( sText, 1 );
          }

        }

      }

    }

    /** Checksums *************************************************************/

    void ClearStatsChecksums ()
    {
      lock( this.StatsChecksums )
      {
        this.StatsChecksums.Clear();
      }
    }

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

    public Dictionary<string,int> GetStatsChecksumsCount ()
    {
      Dictionary<string,int> dicStats = new Dictionary<string,int> ( this.StatsChecksums.Count );
      lock( this.StatsChecksums )
      {
        foreach( string sKey in this.StatsChecksums.Keys )
        {
          dicStats.Add( sKey, this.StatsChecksums[ sKey ] );
        }
      }
      return( dicStats );
    }

    void RecalculateStatsChecksums ( MacroscopeDocument msDoc )
    {

      string sChecksum = msDoc.GetChecksum();

      if( sChecksum.Length > 0 )
      {

        DebugMsg( string.Format( "RecalculateStatsChecksums: {0}", sChecksum ) );

        lock( this.StatsChecksums )
        {

          if( this.StatsChecksums.ContainsKey( sChecksum ) )
          {
            this.StatsChecksums[ sChecksum ] = this.StatsChecksums[ sChecksum ] + 1;
          }
          else
          {
            this.StatsChecksums.Add( sChecksum, 1 );
          }

        }

      }

    }

    /** Deep Keyword Analysis *************************************************/

    private void ClearStatsDeepKeywordAnalysis ()
    {
      lock( this.StatsDeepKeywordAnalysis )
      {
        this.StatsDeepKeywordAnalysis.Clear();
      }
    }

    private void RecalculateStatsDeepKeywordAnalysis ( MacroscopeDocument msDoc )
    {
      //lock( this.StatsDeepKeywordAnalysis )
      //{
      this.AnalyzeKeywords.Analyze( msDoc.GetBodyText(), this.StatsDeepKeywordAnalysis );
      //}
      DebugMsg( "" );
    }

    public Dictionary<string,int> GetDeepKeywordAnalysisAsDictonary ()
    {
      Dictionary<string,int> Terms = new Dictionary<string,int> ( this.StatsDeepKeywordAnalysis.Count );
      lock( this.StatsDeepKeywordAnalysis )
      {
        foreach( string sTerm in this.StatsDeepKeywordAnalysis.Keys )
        {
          Terms.Add( sTerm, this.StatsDeepKeywordAnalysis[ sTerm ] );
        }
      }
      return( Terms );
    }

    /** Search Index **********************************************************/

    public MacroscopeSearchIndex GetSearchIndex ()
    {
      return( this.SearchIndex );
    }

    void AddDocumentToSearchIndex ( MacroscopeDocument msDoc )
    {
      if( msDoc.GetIsHtml() )
      {
        this.SearchIndex.AddDocumentToIndex( msDoc );
      }
    }

    /**************************************************************************/

  }

}
