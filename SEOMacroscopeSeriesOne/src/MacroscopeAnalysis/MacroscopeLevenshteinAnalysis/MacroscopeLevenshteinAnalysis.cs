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
using System.Threading;
using Fastenshtein;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeLevenshteinAnalysis.
  /// </summary>

  // TODO: Perform deeper analysis on similarly detected documents.

  public class MacroscopeLevenshteinAnalysis : MacroscopeAnalysis
  {

    /**************************************************************************/

    private const long EstimateMemoryAllocation = 8192;
    private IMacroscopeAnalysisPercentageDone PercentageDone;

    private readonly MacroscopeDocument msDocOriginal;
    private string Fingerprint;
    private Levenshtein AnalyzerFingerprint;
    private int ComparisonSizeDifference;
    private int ComparisonThreshold;
    private Dictionary<string, bool> CrossCheck;

    /**************************************************************************/

    public MacroscopeLevenshteinAnalysis (
      MacroscopeDocument msDoc,
      int SizeDifference,
      int Threshold,
      Dictionary<string, bool> CrossCheckList,
      IMacroscopeAnalysisPercentageDone IPercentageDone
    ) : base()
    {

      this.msDocOriginal = msDoc;
      this.Fingerprint = msDoc.GetLevenshteinFingerprint();
      this.AnalyzerFingerprint = new Levenshtein( Fingerprint );
      this.ComparisonSizeDifference = SizeDifference;
      this.ComparisonThreshold = Threshold;
      this.CrossCheck = CrossCheckList;
      this.PercentageDone = IPercentageDone;

    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeLevenshteinAnalysis (
      MacroscopeDocument msDoc,
      int SizeDifference,
      int Threshold,
      Dictionary<string, bool> CrossCheckList
    ) : base()
    {

      this.msDocOriginal = msDoc;
      this.Fingerprint = msDoc.GetLevenshteinFingerprint();
      this.AnalyzerFingerprint = new Levenshtein( Fingerprint );
      this.ComparisonSizeDifference = SizeDifference;
      this.ComparisonThreshold = Threshold;
      this.CrossCheck = CrossCheckList;
      this.PercentageDone = null;

    }

    /**************************************************************************/

    public Dictionary<MacroscopeDocument, int> ReanalyzeDocCollection ( MacroscopeDocumentCollection DocCollection )
    {

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {
        msDoc.ClearLevenshteinNearDuplicates();
      }

      return ( this.AnalyzeDocCollection( DocCollection: DocCollection ) );

    }

    /**************************************************************************/

    public Dictionary<MacroscopeDocument, int> AnalyzeDocCollection ( MacroscopeDocumentCollection DocCollection )
    {

      Dictionary<MacroscopeDocument, int> DocList;
      decimal DocListCount;
      decimal Count;
      bool Proceed;

      if ( this.AnalyzerFingerprint.GetType() != typeof( Levenshtein ) )
      {
        throw new Exception( "MacroscopeLevenshteinAnalysis not initialized" );
      }

      DocList = new Dictionary<MacroscopeDocument, int>( DocCollection.CountDocuments() );
      DocListCount = (decimal) DocCollection.CountDocuments();
      Count = 0;
      Proceed = false;

      try
      {

        long MemoryEstimateBytes = 0;
        int RequiredMegabytes = 0;
        long DocumentCount = 0;

        foreach ( MacroscopeDocument msDocCheck in DocCollection.IterateDocuments() )
        {
          if ( ( !msDocCheck.GetIsExternal() ) && ( !msDocCheck.GetIsRedirect() ) )
          {
            DocumentCount++;
          }
        }

        MemoryEstimateBytes = 512 * DocumentCount;
        RequiredMegabytes = (int) ( MemoryEstimateBytes / (long) 1024 );

        if ( this.MemoryGate( RequiredMegabytes: RequiredMegabytes ) )
        {
          Proceed = true;
        }
        else
        {
          Proceed = false;
        }

      }
      catch ( MacroscopeInsufficientMemoryException ex )
      {
        this.DebugMsg( string.Format( "MacroscopeInsufficientMemoryException: {0}", ex.Message ) );
        Thread.Yield();
      }

      if ( !Proceed )
      {
        return ( DocList );
      }

      foreach ( MacroscopeDocument msDocCompare in DocCollection.IterateDocuments() )
      {

        string CompareFingerprint = msDocCompare.GetLevenshteinFingerprint();
        bool DoCheck = false;

        Count++;

        if ( ( this.PercentageDone != null ) && ( DocListCount > 0 ) )
        {
          this.PercentageDone.PercentageDone( ( ( (decimal) 100 / DocListCount ) * Count ), msDocCompare.GetUrl() );
        }

        if ( this.CrossCheckDocuments( msDocCompare: msDocCompare ) )
        {
          continue;
        }

        if ( msDocCompare.GetIsExternal() )
        {
          continue;
        }

        if ( msDocCompare.GetIsRedirect() )
        {
          continue;
        }

        if ( !this.AllowedDocType( msDoc: msDocCompare ) )
        {
          continue;
        }
        else
        if ( msDocCompare.GetUrl() == this.msDocOriginal.GetUrl() )
        {
          continue;
        }
        else
        if ( CompareFingerprint.Length == 0 )
        {
          continue;
        }

        if ( msDocOriginal.GetChecksum() == msDocCompare.GetChecksum() )
        {
          DocList.Add( msDocCompare, 0 );
          continue;
        }

        //this.DebugMsg( string.Format( "msDocOriginal: {0}", this.msDocOriginal.GetUrl() ) );
        //this.DebugMsg( string.Format( "this.Fingerprint.Length: {0}", this.Fingerprint.Length ) );
        //this.DebugMsg( string.Format( "msDocCompare: {0}", msDocCompare.GetUrl() ) );
        //this.DebugMsg( string.Format( "CompareFingerprint.Length: {0}", CompareFingerprint.Length ) );        

        //this.DebugMsg( string.Format( "this.ComparisonThreshold: {0}", this.ComparisonThreshold ) );        

        if ( CompareFingerprint.Length > this.Fingerprint.Length )
        {
          int Len = CompareFingerprint.Length - this.Fingerprint.Length;
          if ( Len <= this.ComparisonSizeDifference )
          {
            DoCheck = true;
          }
        }
        else
        {
          int Len = this.Fingerprint.Length - CompareFingerprint.Length;
          if ( Len <= this.ComparisonSizeDifference )
          {
            DoCheck = true;
          }
        }

        if ( DoCheck )
        {

          int DistanceFingerprint = this.AnalyzerFingerprint.Distance( CompareFingerprint );

          if ( DistanceFingerprint <= this.ComparisonThreshold )
          {

            switch ( MacroscopePreferencesManager.GetLevenshteinAnalysisLevel() )
            {
              case 1:
                DocList.Add( msDocCompare, DistanceFingerprint );
                break;
              case 2:
                string DocumentText = this.msDocOriginal.GetDocumentTextRaw().ToLower();
                string CompareDocumentText = msDocCompare.GetDocumentTextRaw().ToLower();
                Levenshtein AnalyzerText = new Levenshtein( value: DocumentText );
                int DistanceDocumentText = AnalyzerText.Distance( value: CompareDocumentText );
                if ( DistanceDocumentText <= this.ComparisonThreshold )
                {
                  DocList.Add( msDocCompare, DistanceDocumentText );
                }
                break;
              default:
                throw new Exception( "Invalid Levenshtein Analysis Level" );
            }

          }

        }

        Thread.Yield();

      }

      return ( DocList );

    }

    /**************************************************************************/

    private bool CrossCheckDocuments ( MacroscopeDocument msDocCompare )
    {

      bool CrossChecked = false;

      string Key1 = string.Join( "::", this.msDocOriginal.GetChecksum(), msDocCompare.GetChecksum() );
      string Key2 = string.Join( "::", msDocCompare.GetChecksum(), this.msDocOriginal.GetChecksum() );

      lock ( this.CrossCheck )
      {

        if ( this.CrossCheck.ContainsKey( Key1 ) )
        {
          CrossChecked = true;
        }
        else
        {
          this.CrossCheck.Add( Key1, true );
        }

        if ( this.CrossCheck.ContainsKey( Key2 ) )
        {
          CrossChecked = true;
        }
        else
        {
          this.CrossCheck.Add( Key2, true );
        }

      }

      return ( CrossChecked );

    }

    /**************************************************************************/

    public static Dictionary<string, bool> GetCrossCheckList ( int Capacity )
    {
      Dictionary<string, bool> CrossCheck = new Dictionary<string, bool>( Capacity );
      return ( CrossCheck );
    }

    /**************************************************************************/

    public bool AllowedDocType ( MacroscopeDocument msDoc )
    {

      bool Allowed = false;

      switch ( msDoc.GetDocumentType( ) )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Allowed = true;
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Allowed = true;
          break;
        default:
          break;
      }

      return ( Allowed );

    }

    /**************************************************************************/

  }

}
