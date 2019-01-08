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
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDataExtractorCssSelectors : Macroscope
  {

    /**************************************************************************/

    private Dictionary<string, string> HtmlDocs;

    /**************************************************************************/

    public TestMacroscopeDataExtractorCssSelectors ()
    {

      StreamReader Reader;
      List<string> DocKeys = new List<string>( 16 );

      this.HtmlDocs = new Dictionary<string, string>();

      DocKeys.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc001.html" );
      DocKeys.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc002.html" );
      DocKeys.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc003.html" );
      DocKeys.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc004.html" );
      DocKeys.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc005.html" );

      foreach ( string Filename in DocKeys )
      {

        Reader = new StreamReader( Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename ) );

        this.HtmlDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();

      }

    }

    /**************************************************************************/

    [Test]
    public void TestHeadingsLevel1 ()
    {

      Dictionary<string, string> AssetDic = new Dictionary<string, string>();

      AssetDic.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc001.html", "First Heading" );
      AssetDic.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc002.html", "First Heading" );
      AssetDic.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc003.html", "First Heading" );
      AssetDic.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc004.html", "First Heading" );
      AssetDic.Add( "SEOMacroscope.src.MacroscopeDataExtractor.t.HtmlDocs.HtmlDoc005.html", "First Heading" );

      MacroscopeDataExtractorCssSelectors DataExtractor = new MacroscopeDataExtractorCssSelectors( Size: 1 );

      DataExtractor.SetCssSelector(
        Slot: 0,
        CssSelectorLabel: "TestHeadingsLevel1",
        CssSelectorString: "h1.heading",
        ExtractorType: MacroscopeConstants.DataExtractorType.INNERTEXT
      );

      DataExtractor.SetActiveInactive(
        Slot: 0,
        State: MacroscopeConstants.ActiveInactive.ACTIVE
      );

      foreach ( string HtmlDocKey in this.HtmlDocs.Keys )
      {

        string Html = this.HtmlDocs[ HtmlDocKey ];

        List<KeyValuePair<string, string>> ResultList = DataExtractor.AnalyzeHtml( Html: Html );

        DebugMsg( string.Format( "HtmlDocKey: {0} :: Value: {1}", HtmlDocKey, ResultList[ 0 ].Value ) );

        Assert.IsNotEmpty( ResultList, "WHOOPS!" );

        Assert.AreEqual( AssetDic[ HtmlDocKey ], ResultList[ 0 ].Value );

      }

    }

    /**************************************************************************/

  }

}
