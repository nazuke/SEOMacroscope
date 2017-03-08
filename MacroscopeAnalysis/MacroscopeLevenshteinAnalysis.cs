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
using Fastenshtein;

namespace SEOMacroscope
{
  
  /// <summary>
  /// Description of MacroscopeLevenshteinAnalysis.
  /// </summary>
  /// 
  
  public class MacroscopeLevenshteinAnalysis : Macroscope
  {
	  
    /**************************************************************************/

    MacroscopeDocument msDocOriginal;
    string MonstrousText;
    Levenshtein Monster = null;
    int ComparisonThreshold;
    
    /**************************************************************************/
	      
    public MacroscopeLevenshteinAnalysis ( MacroscopeDocument msDoc, int Threshold )
    {
      
      this.SuppressDebugMsg = false;
      
      this.msDocOriginal = msDoc;
      this.MonstrousText = msDoc.GetBodyText();
      this.Monster = new Levenshtein ( MonstrousText );
      this.ComparisonThreshold = Threshold;
      
    }

    /**************************************************************************/

    public Dictionary<MacroscopeDocument,int> AnalyzeDocCollection ( MacroscopeDocumentCollection DocCollection )
    {

      if( Monster == null )
      {
        throw new Exception ( "MacroscopeLevenshteinAnalysis not initialized" );
      }
      
      Dictionary<MacroscopeDocument,int> DocList = new Dictionary<MacroscopeDocument,int> ( DocCollection.CountDocuments() );

      foreach( MacroscopeDocument msDocCompare in DocCollection.IterateDocuments() )
      {

        if( msDocCompare.GetUrl() == this.msDocOriginal.GetUrl() )
        {
          continue;
        }

        string BodyText = msDocCompare.GetBodyText();
        Boolean DoCheck = false;

        DebugMsg( string.Format( "msDocOriginal: {0}", this.msDocOriginal.GetUrl() ) );
        DebugMsg( string.Format( "this.MonstrousText.Length: {0}", this.MonstrousText.Length ) );
        DebugMsg( string.Format( "msDocCompare: {0}", msDocCompare.GetUrl() ) );
        DebugMsg( string.Format( "BodyText.Length: {0}", BodyText.Length ) );        

        DebugMsg( string.Format( "this.ComparisonThreshold: {0}", this.ComparisonThreshold ) );        

        if( BodyText.Length > this.MonstrousText.Length )
        {
          int iLen = BodyText.Length - this.MonstrousText.Length;
          DebugMsg( string.Format( "iLen 1: {0}", iLen ) );        
          if( iLen <= this.ComparisonThreshold )
          {
            DoCheck = true;
          }
        }
        else
        {
          int iLen = this.MonstrousText.Length - BodyText.Length;
          DebugMsg( string.Format( "iLen 2: {0}", iLen ) );        
          if( iLen <= this.ComparisonThreshold )
          {
            DoCheck = true;
          }
        }

        if( DoCheck )
        {
          int Distance = Monster.Distance( BodyText );

          DebugMsg( string.Format( "Distance: {0}", Distance ) );        


          DocList.Add( msDocCompare, Distance );
        }
        else
        {
          DebugMsg( string.Format( "DoCheck: {0}", DoCheck ) );
        }

      }

      return( DocList );

    }

    /**************************************************************************/
  
  }
	
}
