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
	/// Description of MacroscopeSearchIndexer.
	/// </summary>
	
	public class MacroscopeSearchIndexer : Macroscope
	{
	
		/**************************************************************************/

		// sUrl, MacroscopeDocument
		Dictionary<string,MacroscopeDocument> DocumentIndex;

		// sUrl, sInvertedIndex ( sKeyword, Boolean )
		Dictionary<string,Dictionary<string,Boolean>> ForwardIndex;

		// sUrl, DocumentIndex
		Dictionary<string,Dictionary<string,MacroscopeDocument>> InvertedIndex;

		/**************************************************************************/

		public MacroscopeSearchIndexer ()
		{

			SuppressDebugMsg = false;
						
			DocumentIndex = new Dictionary<string, MacroscopeDocument> ( 4096 );

			ForwardIndex = new Dictionary<string,Dictionary<string,Boolean>> ( 4096 );

			InvertedIndex = new Dictionary<string, Dictionary<string,MacroscopeDocument>> ( 4096 );

		}
		
		/**************************************************************************/

		public void AddDocumentToIndex ( MacroscopeDocument msDoc )
		{
			string sUrl = msDoc.GetUrl();

			this.RemoveDocumentFromIndex( sUrl );
			
			this.ProcessText( msDoc );

		}

		/**************************************************************************/

		void ProcessText ( MacroscopeDocument msDoc )
		{
			
			List<string> TextBlocks = new List<string> ( 16 );
			List<string> Words = new List<string> ( 256 );
			string sTextBlock = "";

			TextBlocks.Add( msDoc.GetTitle() );
			TextBlocks.Add( msDoc.GetDescription() );
			TextBlocks.Add( msDoc.GetKeywords() );
			TextBlocks.Add( msDoc.GetBodyText() );

			DebugMsg( string.Format( "ProcessText: {0}", TextBlocks.Count ) );

			if( TextBlocks.Count > 0 )
			{
				for( int i = 0 ; i < TextBlocks.Count ; i++ )
				{
					string [] WordsChunk = TextBlocks[ i ].Split( ' ' );
					if( WordsChunk.Length > 0 )
					{
						for( int j = 0 ; j < WordsChunk.Length ; j++ )
						{
							if( WordsChunk[ j ].Length > 0 )
							{
								if( !Words.Contains( WordsChunk[ j ] ) )
								{
									Words.Add( WordsChunk[ j ] );
								}
							}
						}
					}
				}
			}
			
			
			sTextBlock = string.Join( ", ", Words );
			

			DebugMsg( string.Format( "ProcessText: sTextBlock :: {0}", sTextBlock ) );
				
			
			
			
			DebugMsg( "" );
			
			
		}

		/**************************************************************************/

		void RemoveDocumentFromIndex ( string sUrl )
		{

			/*
			if( this.ForwardIndex.ContainsKey( sUrl ) )
			{
			}
			*/

			/*
			if( this.DocumentIndex.ContainsKey( sUrl ) )
			{

				//this.DocumentIndex.Remove( sUrl );
			}
			*/

		}

		/**************************************************************************/
				
	}
	
}
