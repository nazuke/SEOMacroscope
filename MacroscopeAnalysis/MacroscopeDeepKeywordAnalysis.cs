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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDeepKeywordAnalysis.
  /// </summary>

  public class MacroscopeDeepKeywordAnalysis : Macroscope
  {

    /**************************************************************************/

    Dictionary<string,string> DocList;
    
    /**************************************************************************/

    public MacroscopeDeepKeywordAnalysis ()
    {
      this.SuppressDebugMsg = true;
      this.DocList = null;
    }
    
    public MacroscopeDeepKeywordAnalysis ( Dictionary<string,string> DocList )
    {
      this.SuppressDebugMsg = true;
      this.DocList = DocList;
    }

    /**************************************************************************/

    public void Analyze (
      string Text,
      Dictionary<string,int> Terms,
      int Words
    )
    {
      if( Words == 1 )
      {
        this.AnalyzeTerm(
          Text: Text,
          Terms: Terms
        );
      }
      else
      if( Words > 1 )
      {
        this.AnalyzePhrase(
          Text: Text,
          Terms: Terms,
          Words: Words
        );
      }
    }

    /** Analyze 1 Word ********************************************************/

    private void AnalyzeTerm (
      string Text,
      Dictionary<string,int> Terms
    )
    {
      if( Text.Length > 0 )
      {
        string [] Chunks = Text.Split( ' ' );
        if( Chunks.Length > 0 )
        {
          for( int i = 0 ; i < Chunks.Length ; i++ )
          {
            string sTerm = Chunks[ i ];
            if( sTerm.Length > 0 )
            {
              if( Terms.ContainsKey( sTerm ) )
              {
                Terms[ sTerm ] += 1;
              }
              else
              {
                Terms.Add( sTerm, 1 );
              }
            }
          }
        }
      }
    }

    /** Analyze Multi-Word Phrases ********************************************/

    private void AnalyzePhrase (
      string Text,
      Dictionary<string,int> Terms,
      int Words
    )
    {

      if( Text.Length > 0 )
      {

        string [] Chunks = Text.Split( ' ' );
        
        if( Chunks.Length > 0 )
        {
          
          for( int i = 0 ; i < Chunks.Length ; i++ )
          {

            string sTerm = Chunks[ i ];
            int iEnd = ( i + Words );

            if( ( Chunks.Length - iEnd ) >= Words )
            {
            
              for( int j = i + 1 ; ( j < iEnd ) && ( j < Chunks.Length ) ; j++ )
              {
                string sSubTerm = Chunks[ j ];
                sTerm = string.Join( " ", sTerm, sSubTerm );
              }

              DebugMsg( string.Format( "RANGE: {0} :: {1} :: {2}", i, iEnd, sTerm ) );

              if( sTerm.Length > 0 )
              {

                DebugMsg( string.Format( "sTerm: {0}", sTerm ) );

                if( Terms.ContainsKey( sTerm ) )
                {
                  Terms[ sTerm ] += 1;
                }
                else
                {
                  Terms.Add( sTerm, 1 );
                }

              }
              
            }

          }
          
        }
        
      }

    }

    /**************************************************************************/

  }

}
