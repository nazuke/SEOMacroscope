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
  /// Description of MacroscopeSearchIndex.
  /// </summary>

  public class MacroscopeSearchIndex : Macroscope
  {

    /**************************************************************************/

    public enum SearchMode
    {
      OR = 1,
      AND = 2
    }

    // Url, InvertedIndex ( Keyword, bool )
    private Dictionary<string,Dictionary<string,bool>> ForwardIndex;

    // Url, DocumentIndex
    private Dictionary<string,Dictionary<string,MacroscopeDocument>> InvertedIndex;

    /**************************************************************************/

    public MacroscopeSearchIndex ()
    {

      this.SuppressDebugMsg = true;

      this.ForwardIndex = new Dictionary<string,Dictionary<string,bool>> ( 4096 );

      this.InvertedIndex = new Dictionary<string, Dictionary<string,MacroscopeDocument>> ( 4096 );

    }

    /**************************************************************************/

    public void AddDocumentToIndex ( MacroscopeDocument msDoc )
    {

      this.RemoveDocumentFromIndex( msDoc: msDoc );

      this.ProcessText( msDoc: msDoc );

    }

    /**************************************************************************/

    private void ProcessText ( MacroscopeDocument msDoc )
    {

      List<string> TextBlocks = new List<string> ( 16 );
      List<string> Terms = new List<string> ( 256 );
      bool CaseSensitive = MacroscopePreferencesManager.GetCaseSensitiveTextIndexing();

      TextBlocks.Add( msDoc.GetTitle() );
      TextBlocks.Add( msDoc.GetDescription() );
      TextBlocks.Add( msDoc.GetKeywords() );
      TextBlocks.Add( msDoc.GetDocumentTextCleaned() );

      DebugMsg( string.Format( "ProcessText: TextBlocks.Count: {0}", TextBlocks.Count ) );

      if( TextBlocks.Count > 0 )
      {
        for( int i = 0 ; i < TextBlocks.Count ; i++ )
        {
          string [] Chunk = TextBlocks[ i ].Split( ' ' );
          if( Chunk.Length > 0 )
          {
            for( int j = 0 ; j < Chunk.Length ; j++ )
            {
              if( Chunk[ j ].Length > 0 )
              {
                if( !Terms.Contains( Chunk[ j ] ) )
                {
                  Terms.Add( Chunk[ j ] );
                }
              }
            }
          }
        }
      }

      DebugMsg( string.Format( "ProcessText: Words :: {0}", Terms.Count ) );

      for( int i = 0 ; i < Terms.Count ; i++ )
      {

        Dictionary<string,MacroscopeDocument> DocumentReference;

        string Term = Terms[ i ];

        if( !CaseSensitive )
        {
          Term = Term.ToLower();
        }

        DebugMsg( string.Format( "ProcessText: Term :: {0}", Term ) );
        
        if( InvertedIndex.ContainsKey( Term ) )
        {
          DocumentReference = this.InvertedIndex[ Term ];
        }
        else
        {
          DocumentReference = new Dictionary<string,MacroscopeDocument> ();
          this.InvertedIndex.Add( Term, DocumentReference );
        }

        if( !DocumentReference.ContainsKey( msDoc.GetUrl() ) )
        {
          DocumentReference.Add( msDoc.GetUrl(), msDoc );
        }

      }

    }

    /**************************************************************************/

    private void RemoveDocumentFromIndex ( MacroscopeDocument msDoc )
    {

      string Url = msDoc.GetUrl();

      if( this.ForwardIndex.ContainsKey( key: Url ) )
      {

        lock( this.ForwardIndex )
        {

          foreach( string Term in this.ForwardIndex[Url].Keys )
          {

            if( this.InvertedIndex.ContainsKey( key: Term ) )
            {
                  
              lock( this.InvertedIndex )
              {
                this.InvertedIndex.Remove( key: Term );
              }

            }

          }

          this.ForwardIndex.Remove( key: Url );

        }

      }

    }

    /** SEARCH INDEX **********************************************************/

    public List<MacroscopeDocument> ExecuteSearchForDocuments (
      MacroscopeSearchIndex.SearchMode SMode,
      string [] Terms
    )
    {

      List<MacroscopeDocument> DocList = null;
      bool CaseSensitive = MacroscopePreferencesManager.GetCaseSensitiveTextIndexing();

      for( int i = 0 ; i < Terms.Length ; i++ )
      {

        if( !CaseSensitive )
        {
          Terms[ i ] = Terms[ i ].ToLower();
        }
      
      }

      switch( SMode )
      {
        case MacroscopeSearchIndex.SearchMode.OR:
          DocList = this.ExecuteSearchForDocumentsOR( Terms );
          break;
        case MacroscopeSearchIndex.SearchMode.AND:
          DocList = this.ExecuteSearchForDocumentsAND( Terms );
          break;
      }

      return( DocList );

    }

    /** SEARCH INDEX: OR METHOD ***********************************************/

    public List<MacroscopeDocument> ExecuteSearchForDocumentsOR ( string [] Terms )
    {

      List<MacroscopeDocument> DocList = new List<MacroscopeDocument> ();

      for( int i = 0 ; i < Terms.Length ; i++ )
      {

        if( InvertedIndex.ContainsKey( Terms[ i ] ) )
        {

          foreach( string Url in InvertedIndex[Terms[i]].Keys )
          {
            DocList.Add( InvertedIndex[ Terms[ i ] ][ Url ] );
          }

        }

      }

      return( DocList );

    }

    /** SEARCH INDEX: AND METHOD **********************************************/

    public List<MacroscopeDocument> ExecuteSearchForDocumentsAND ( string [] Terms )
    {

      List<MacroscopeDocument> DocList = new List<MacroscopeDocument> ();

      Dictionary<MacroscopeDocument,int> DocListGather = new Dictionary<MacroscopeDocument,int> ();

      for( int i = 0 ; i < Terms.Length ; i++ )
      {

        if( InvertedIndex.ContainsKey( Terms[ i ] ) )
        {

          foreach( string Url in InvertedIndex[Terms[i]].Keys )
          {

            MacroscopeDocument msDoc = InvertedIndex[ Terms[ i ] ][ Url ];
            if( DocListGather.ContainsKey( msDoc ) )
            {
              DocListGather[ msDoc ] = DocListGather[ msDoc ] + 1;
            }
            else
            {
              DocListGather.Add( msDoc, 1 );
            }

          }

        }

      }

      foreach( MacroscopeDocument msDoc in DocListGather.Keys )
      {
        if( DocListGather[ msDoc ] == Terms.Length )
        {
          DocList.Add( msDoc );
        }
      }

      return( DocList );

    }

    /**************************************************************************/

  }

}
