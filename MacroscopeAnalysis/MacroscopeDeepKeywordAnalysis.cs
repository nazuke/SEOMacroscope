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

    // Keyword Term / MacroscopeDocumentList
    Dictionary<string,MacroscopeDocumentList> DocList;

    /**************************************************************************/

    public MacroscopeDeepKeywordAnalysis ()
    {
      this.SuppressDebugMsg = true;
      this.DocList = null;
    }
    
    public MacroscopeDeepKeywordAnalysis ( Dictionary<string,MacroscopeDocumentList> DocList )
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

    public void Analyze (
      MacroscopeDocument msDoc,
      string Text,
      Dictionary<string,int> Terms,
      int Words
    )
    {

      Dictionary<string,int> TermsList = null;

      if( Words == 1 )
      {
        TermsList = this.AnalyzeTerm(
          Text: Text,
          Terms: Terms
        );
      }
      else
      if( Words > 1 )
      {
        TermsList = this.AnalyzePhrase(
          Text: Text,
          Terms: Terms,
          Words: Words
        );
      }

      if( ( this.DocList != null ) && ( TermsList != null ) )
      {

        lock( this.DocList )
        {

          foreach( string KeywordTerm in TermsList.Keys )
          {

            MacroscopeDocumentList DocumentList;
            
            if( this.DocList.ContainsKey( KeywordTerm ) )
            {
              DocumentList = this.DocList[ KeywordTerm ];
            }
            else
            {
              DocumentList = new MacroscopeDocumentList ();
              this.DocList.Add( KeywordTerm, DocumentList );
            }
            
            DocumentList.AddDocument( msDoc );

          }

        }

      }

    }

    /** Analyze 1 Word ********************************************************/

    private Dictionary<string,int> AnalyzeTerm (
      string Text,
      Dictionary<string,int> Terms
    )
    {

      Dictionary<string,int> TermsList = new Dictionary<string,int> ();

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

              if( TermsList.ContainsKey( sTerm ) )
              {
                TermsList[ sTerm ] += 1;
              }
              else
              {
                TermsList.Add( sTerm, 1 );
              }

            }

          }

        }

      }

      return( TermsList );

    }

    /** Analyze Multi-Word Phrases ********************************************/

    private Dictionary<string,int> AnalyzePhrase (
      string Text,
      Dictionary<string,int> Terms,
      int Words
    )
    {

      Dictionary<string,int> TermsList = new Dictionary<string,int> ();
            
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

                if( TermsList.ContainsKey( sTerm ) )
                {
                  TermsList[ sTerm ] += 1;
                }
                else
                {
                  TermsList.Add( sTerm, 1 );
                }
              
              }
              
            }

          }
          
        }
        
      }

      return( TermsList );

    }

    /**************************************************************************/

  }

}
