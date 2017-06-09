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
using System.Diagnostics;
using System.Collections.Generic;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDataExtractorRegexes
  {

    /**************************************************************************/

    [Test]
    public void TestExtractors ()
    {

      Macroscope ms = new Macroscope ();
      
      MacroscopeDataExtractorRegexes DataExtractor = new MacroscopeDataExtractorRegexes ( Size: 5 );

      List<string> Texts = new List<string> ();

      Texts.Add( "The quick brown fox jumps over the lazy dog." );

      DataExtractor.SetPattern( 0, "Label: The", "[^\\w]*[tT]he\\s" );
      DataExtractor.SetPattern( 1, "Label: over", "[^\\w]*[oO]ver\\s" );
      DataExtractor.SetPattern( 2, "Label: fox", "[^\\w]*[fF]ox\\s" );
      DataExtractor.SetPattern( 3, "Label: dog", "[^\\w]*[dD]og\\s?" );
      DataExtractor.SetPattern( 4, "Label: brown", "[^\\w]*[bB]rown\\s" );

      foreach( string ContainsText in Texts )
      {

        List<KeyValuePair<string, string>> AnalyzedList = DataExtractor.AnalyzeText( Text: ContainsText );

        Assert.IsNotNull( AnalyzedList );    

        Assert.AreEqual(
          AnalyzedList.Count,
          6, // Should match 6 times
          string.Format( "Wrong number of matches: {0}", AnalyzedList.Count )
        );
        
        foreach( KeyValuePair<string, string> AnalyzedItem in AnalyzedList )
        {

          ms.DebugMsg( string.Format( "ITEM: {0} => {1}", AnalyzedItem.Key, AnalyzedItem.Value ) );
        
        }
        
      }

    }

    /**************************************************************************/

  }

}
