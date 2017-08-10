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
using System.Threading;
using Fastenshtein;

namespace SEOMacroscope
{
  
  /// <summary>
  /// Description of MacroscopeLevenshteinAnalysis.
  /// </summary>

  public class MacroscopeLevenshteinAnalysis : Macroscope
  {
	  
    /**************************************************************************/

    private const long EstimateMemoryAllocation = 8192;
    private IMacroscopeAnalysisPercentageDone PercentageDone;
    
    private readonly MacroscopeDocument msDocOriginal;
    private string MonstrousText;
    private Levenshtein Monster;
    private int ComparisonSizeDifference;
    private int ComparisonThreshold;
    private Dictionary<string,Boolean> CrossCheck;
    
    /**************************************************************************/
	      
    public MacroscopeLevenshteinAnalysis (
      MacroscopeDocument msDoc,
      int SizeDifference,
      int Threshold,
      Dictionary<string,Boolean> CrossCheckList,
      IMacroscopeAnalysisPercentageDone IPercentageDone
    )
    {
      
      this.SuppressDebugMsg = true;
      
      this.msDocOriginal = msDoc;
      this.MonstrousText = msDoc.GetDocumentTextRaw().ToLower();
      this.Monster = new Levenshtein ( MonstrousText );
      this.ComparisonSizeDifference = SizeDifference;
      this.ComparisonThreshold = Threshold;
      
      this.CrossCheck = CrossCheckList;
      
      this.PercentageDone = IPercentageDone;
      
    }

    public MacroscopeLevenshteinAnalysis (
      MacroscopeDocument msDoc,
      int SizeDifference,
      int Threshold,
      Dictionary<string,Boolean> CrossCheckList
    )
    {
      
      this.SuppressDebugMsg = true;
      
      this.msDocOriginal = msDoc;
      this.MonstrousText = msDoc.GetDocumentTextRaw().ToLower();
      this.Monster = new Levenshtein ( MonstrousText );
      this.ComparisonSizeDifference = SizeDifference;
      this.ComparisonThreshold = Threshold;
      
      this.CrossCheck = CrossCheckList;
            
      this.PercentageDone = null;
      
    }

    /**************************************************************************/

    public Dictionary<MacroscopeDocument,int> AnalyzeDocCollection (
      MacroscopeDocumentCollection DocCollection
    )
    {

      if( this.Monster.GetType() != typeof( Levenshtein ) )
      {
        throw new Exception ( "MacroscopeLevenshteinAnalysis not initialized" );
      }
      
      Dictionary<MacroscopeDocument,int> DocList = new Dictionary<MacroscopeDocument,int> ( DocCollection.CountDocuments() );
      decimal DocListCount = ( decimal )DocCollection.CountDocuments();
      decimal Count = 0;
      Boolean Proceed = false;

      try
      {

        long MemoryEstimateBytes = 0;
        int RequiredMegabytes = 0;
        long DocumentCount = 0;

        foreach( MacroscopeDocument msDocCheck in DocCollection.IterateDocuments() )
        {
          if( ( !msDocCheck.GetIsExternal() ) && ( !msDocCheck.GetIsRedirect() ) )
          {
            DocumentCount++;
          }
        }

        MemoryEstimateBytes = 512 * DocumentCount;
        RequiredMegabytes = ( int )( MemoryEstimateBytes / ( long )1024 );

        if( this.MemoryGate( RequiredMegabytes: RequiredMegabytes ) )
        {
          Proceed = true;
        }
        else
        {
          Proceed = false;
        }
     
      }
      catch( MacroscopeInsufficientMemoryException ex )
      {
        DebugMsg( string.Format( "MacroscopeInsufficientMemoryException: {0}", ex.Message ) );
        GC.Collect();
        Thread.Yield();
      }

      if( !Proceed )
      {
        return( DocList );
      }

      foreach( MacroscopeDocument msDocCompare in DocCollection.IterateDocuments() )
      {

        string BodyText = msDocCompare.GetDocumentTextRaw().ToLower();
        Boolean DoCheck = false;
        
        Count++;

        if( ( this.PercentageDone != null ) && ( DocListCount > 0 ) )
        {
          this.PercentageDone.PercentageDone( ( ( ( decimal )100 / DocListCount ) * Count ), msDocCompare.GetUrl() );
        }
        
        if( CrossCheckDocuments( msDocCompare: msDocCompare ) )
        {
          continue;
        }
        
        if( msDocCompare.GetIsExternal() )
        {
          continue;
        }
        
        if( msDocCompare.GetIsRedirect() )
        {
          continue;
        }
        
        if( !msDocCompare.GetIsHtml() )
        {
          continue;
        }
        else
        if( msDocCompare.GetUrl() == this.msDocOriginal.GetUrl() )
        {
          continue;
        }
        else
        if( BodyText.Length == 0 )
        {
          continue;
        }

        if( msDocOriginal.GetChecksum() == msDocCompare.GetChecksum() )
        {
          DocList.Add( msDocCompare, 0 );
          continue;
        }

        //DebugMsg( string.Format( "msDocOriginal: {0}", this.msDocOriginal.GetUrl() ) );
        //DebugMsg( string.Format( "this.MonstrousText.Length: {0}", this.MonstrousText.Length ) );
        //DebugMsg( string.Format( "msDocCompare: {0}", msDocCompare.GetUrl() ) );
        //DebugMsg( string.Format( "BodyText.Length: {0}", BodyText.Length ) );        

        //DebugMsg( string.Format( "this.ComparisonThreshold: {0}", this.ComparisonThreshold ) );        

        if( BodyText.Length > this.MonstrousText.Length )
        {
          
          int iLen = BodyText.Length - this.MonstrousText.Length;
          
          //DebugMsg( string.Format( "iLen 1: {0}", iLen ) );
          
          if( iLen <= this.ComparisonSizeDifference )
          {
            DoCheck = true;
          }
          
        }
        else
        {
          
          int iLen = this.MonstrousText.Length - BodyText.Length;
          
          //DebugMsg( string.Format( "iLen 2: {0}", iLen ) );
          
          if( iLen <= this.ComparisonSizeDifference )
          {
            DoCheck = true;
          }
          
        }

        if( DoCheck )
        {
          
          int Distance = this.Monster.Distance( BodyText );
          
          //DebugMsg( string.Format( "Distance: {0}", Distance ) );
          
          if( Distance <= this.ComparisonThreshold )
          {
            DocList.Add( msDocCompare, Distance );
          }
          
        }
        else
        {
          
          //DebugMsg( string.Format( "DoCheck: {0}", DoCheck ) );
          
        }

        Thread.Yield();
        
      }

      return( DocList );

    }

    /**************************************************************************/

    private Boolean CrossCheckDocuments ( MacroscopeDocument msDocCompare )
    {

      Boolean CrossChecked = false;

      string Key1 = string.Join( "::", this.msDocOriginal.GetChecksum(), msDocCompare.GetChecksum() );
      string Key2 = string.Join( "::", msDocCompare.GetChecksum(), this.msDocOriginal.GetChecksum() );

      lock( this.CrossCheck )
      {

        if( this.CrossCheck.ContainsKey( Key1 ) )
        {
          CrossChecked = true;
        }
        else
        {
          this.CrossCheck.Add( Key1, true );
        }

        if( this.CrossCheck.ContainsKey( Key2 ) )
        {
          CrossChecked = true;
        }
        else
        {
          this.CrossCheck.Add( Key2, true );
        }

      }

      return( CrossChecked );

    }

    /**************************************************************************/

    public static Dictionary<string,Boolean> GetCrossCheckList ( int Capacity )
    {
      Dictionary<string,Boolean> CrossCheck = new Dictionary<string,Boolean> ( Capacity );
      return( CrossCheck );
    }

    /**************************************************************************/
  
  }
	
}
