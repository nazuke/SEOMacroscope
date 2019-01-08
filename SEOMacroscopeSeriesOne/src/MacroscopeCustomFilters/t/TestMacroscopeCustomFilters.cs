/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeCustomFilter : Macroscope
  {

    /**************************************************************************/

    [Test]
    public void TestContainsText ()
    {

      MacroscopeCustomFilters CustomFilter = new MacroscopeCustomFilters ( Size: 5 );

      List<string> Texts = new List<string> ();

      Texts.Add( "The quick brown fox jumps over the lazy dog." );

      CustomFilter.SetPattern( 0, "The", MacroscopeConstants.Contains.MUST_HAVE_STRING );
      CustomFilter.SetPattern( 1, "over", MacroscopeConstants.Contains.MUST_HAVE_STRING );
      CustomFilter.SetPattern( 2, "fox", MacroscopeConstants.Contains.MUST_HAVE_STRING );
      CustomFilter.SetPattern( 3, "dog", MacroscopeConstants.Contains.MUST_HAVE_STRING );
      CustomFilter.SetPattern( 4, "brown", MacroscopeConstants.Contains.MUST_HAVE_STRING );

      foreach( string ContainsText in Texts )
      {

        Dictionary<string, MacroscopeConstants.TextPresence> Analyzed = CustomFilter.AnalyzeText( Text: ContainsText );

        Assert.IsNotNull( Analyzed );
        
        foreach( string AnalyzedKey in Analyzed.Keys )
        {

          Assert.AreEqual( 
            MacroscopeConstants.TextPresence.CONTAINS_STRING,
            Analyzed[ AnalyzedKey ],
            string.Format(
              "Wrong TextPresence for: {0} :: {1}",
              AnalyzedKey,
              Analyzed[ AnalyzedKey ]
            )
          );
        
        }
        
      }

    }

    /**************************************************************************/

    [Test]
    public void TestDoesNotContainText ()
    {

      MacroscopeCustomFilters CustomFilter = new MacroscopeCustomFilters ( Size: 5 );

      List<string> Texts = new List<string> ();

      Texts.Add( "The quick brown fox jumps over the lazy dog." );

      CustomFilter.SetPattern( 0, "Mad", MacroscopeConstants.Contains.MUST_NOT_HAVE_STRING );
      CustomFilter.SetPattern( 1, "car", MacroscopeConstants.Contains.MUST_NOT_HAVE_STRING );
      CustomFilter.SetPattern( 2, "nugget", MacroscopeConstants.Contains.MUST_NOT_HAVE_STRING );
      CustomFilter.SetPattern( 3, "quickly", MacroscopeConstants.Contains.MUST_NOT_HAVE_STRING );
      CustomFilter.SetPattern( 4, "doggy", MacroscopeConstants.Contains.MUST_NOT_HAVE_STRING );

      foreach( string ContainsText in Texts )
      {

        Dictionary<string, MacroscopeConstants.TextPresence> Analyzed = CustomFilter.AnalyzeText( Text: ContainsText );

        Assert.IsNotNull( Analyzed );
        
        foreach( string AnalyzedKey in Analyzed.Keys )
        {

          Assert.AreEqual( 
            MacroscopeConstants.TextPresence.NOT_CONTAINS_STRING,
            Analyzed[ AnalyzedKey ],
            string.Format(
              "Wrong TextPresence for: {0} :: {1}",
              AnalyzedKey,
              Analyzed[ AnalyzedKey ]
            )
          );
        
        }
        
      }

    }

    /**************************************************************************/

    [Test]
    public void TestContainsRegex ()
    {

      MacroscopeCustomFilters CustomFilter = new MacroscopeCustomFilters( Size: 5 );

      List<string> Texts = new List<string>();

      Texts.Add( "The quick brown fox jumps over the lazy dog." );

      CustomFilter.SetPattern( 0, "Th[e]", MacroscopeConstants.Contains.MUST_HAVE_REGEX );
      CustomFilter.SetPattern( 1, "[Oo]ver", MacroscopeConstants.Contains.MUST_HAVE_REGEX );
      CustomFilter.SetPattern( 2, "[a-z]{2}x", MacroscopeConstants.Contains.MUST_HAVE_REGEX );
      CustomFilter.SetPattern( 3, "(dog|DOG)", MacroscopeConstants.Contains.MUST_HAVE_REGEX );
      CustomFilter.SetPattern( 4, "^.+brown.+$", MacroscopeConstants.Contains.MUST_HAVE_REGEX );

      foreach ( string ContainsText in Texts )
      {

        Dictionary<string, MacroscopeConstants.TextPresence> Analyzed = CustomFilter.AnalyzeText( Text: ContainsText );

        Assert.IsNotNull( Analyzed );

        foreach ( string AnalyzedKey in Analyzed.Keys )
        {

          Assert.AreEqual(
            MacroscopeConstants.TextPresence.CONTAINS_REGEX,
            Analyzed[ AnalyzedKey ],
            string.Format(
              "Wrong TextPresence for: {0} :: {1}",
              AnalyzedKey,
              Analyzed[ AnalyzedKey ]
            )
          );

        }

      }

    }

    /**************************************************************************/

    [Test]
    public void TestDoesNotContainRegex ()
    {

      MacroscopeCustomFilters CustomFilter = new MacroscopeCustomFilters( Size: 5 );

      List<string> Texts = new List<string>();

      Texts.Add( "The quick brown fox jumps over the lazy dog." );

      CustomFilter.SetPattern( 0, "Mad", MacroscopeConstants.Contains.MUST_NOT_HAVE_REGEX );
      CustomFilter.SetPattern( 1, "car", MacroscopeConstants.Contains.MUST_NOT_HAVE_REGEX );
      CustomFilter.SetPattern( 2, "nugget", MacroscopeConstants.Contains.MUST_NOT_HAVE_REGEX );
      CustomFilter.SetPattern( 3, "quickly", MacroscopeConstants.Contains.MUST_NOT_HAVE_REGEX );
      CustomFilter.SetPattern( 4, "doggy", MacroscopeConstants.Contains.MUST_NOT_HAVE_REGEX );

      foreach ( string ContainsText in Texts )
      {

        Dictionary<string, MacroscopeConstants.TextPresence> Analyzed = CustomFilter.AnalyzeText( Text: ContainsText );

        Assert.IsNotNull( Analyzed );

        foreach ( string AnalyzedKey in Analyzed.Keys )
        {

          Assert.AreEqual(
            MacroscopeConstants.TextPresence.NOT_CONTAINS_REGEX,
            Analyzed[ AnalyzedKey ],
            string.Format(
              "Wrong TextPresence for: {0} :: {1}",
              AnalyzedKey,
              Analyzed[ AnalyzedKey ]
            )
          );

        }

      }

    }

    /**************************************************************************/

  }

}
