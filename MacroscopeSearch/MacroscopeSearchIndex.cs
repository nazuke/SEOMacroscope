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

		// sUrl, MacroscopeDocument
		Dictionary<string,MacroscopeDocument> DocumentIndex;

		// sUrl, sInvertedIndex ( sKeyword, Boolean )
		Dictionary<string,Dictionary<string,Boolean>> ForwardIndex;

		// sUrl, DocumentIndex
		Dictionary<string,Dictionary<string,MacroscopeDocument>> InvertedIndex;

		/**************************************************************************/

		public MacroscopeSearchIndex ()
		{

			SuppressDebugMsg = false;
						
			DocumentIndex = new Dictionary<string, MacroscopeDocument> ( 4096 );

			ForwardIndex = new Dictionary<string,Dictionary<string,Boolean>> ( 4096 );

			InvertedIndex = new Dictionary<string, Dictionary<string,MacroscopeDocument>> ( 4096 );

		}
		
		/**************************************************************************/

		public void AddDocumentToIndex ( MacroscopeDocument msDoc )
		{
			
			this.RemoveDocumentFromIndex( msDoc );
			
			this.ProcessText( msDoc );

		}

		/**************************************************************************/

		void ProcessText ( MacroscopeDocument msDoc )
		{
			
			List<string> TextBlocks = new List<string> ( 16 );
			List<string> Terms = new List<string> ( 256 );

			TextBlocks.Add( msDoc.GetTitle() );
			TextBlocks.Add( msDoc.GetDescription() );
			TextBlocks.Add( msDoc.GetKeywords() );
			TextBlocks.Add( msDoc.GetBodyText() );

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

				string sTerm = Terms[ i ];
				
				Dictionary<string,MacroscopeDocument> DocumentReference;

				if( InvertedIndex.ContainsKey( sTerm ) )
				{
					DocumentReference = this.InvertedIndex[ sTerm ];
				}
				else
				{
					DocumentReference = new Dictionary<string,MacroscopeDocument> ();
					this.InvertedIndex.Add( sTerm, DocumentReference );
				}

				if( !DocumentReference.ContainsKey( msDoc.GetUrl() ) )
				{
					DocumentReference.Add( msDoc.GetUrl(), msDoc );
				}

			}
									
		}

		/**************************************************************************/

		void RemoveDocumentFromIndex ( MacroscopeDocument msDoc )
		{
		}

		/**************************************************************************/

		// TODO: Finish this

		public List<MacroscopeDocument> ExecuteSearchForDocuments ( string [] Terms )
		{
			List<MacroscopeDocument> DocList = new List<MacroscopeDocument> ();

			for( int i = 0 ; i < Terms.Length ; i++ ) {
				
				if( InvertedIndex.ContainsKey( Terms[i] ) ) {

				}

			}

			return( DocList );
		}
		
		/**************************************************************************/
				
	}
	
}
