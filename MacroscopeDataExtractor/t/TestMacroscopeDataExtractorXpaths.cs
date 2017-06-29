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
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDataExtractorXpaths : Macroscope
  {

    /**************************************************************************/

    private Dictionary<string,string> HtmlDocs;

    /**************************************************************************/
    
    public TestMacroscopeDataExtractorXpaths ()
    {

      StreamReader Reader;
      List<string> HtmlDocKeys = new List<string> ( 16 );

      this.HtmlDocs = new Dictionary<string,string> ();

      this.HtmlDocs.Add( "HtmlDoc001", null );
      this.HtmlDocs.Add( "HtmlDoc002", null );
      this.HtmlDocs.Add( "HtmlDoc003", null );
      this.HtmlDocs.Add( "HtmlDoc004", null );
      this.HtmlDocs.Add( "HtmlDoc005", null );

      foreach( string HtmlDocKey in this.HtmlDocs.Keys )
      {
        HtmlDocKeys.Add( HtmlDocKey );
      }

      foreach( string HtmlDocKey in HtmlDocKeys )
      {
        
        Reader = new StreamReader (
          Assembly.GetExecutingAssembly().GetManifestResourceStream(
            HtmlDocKey
          )
        );

        this.HtmlDocs[ HtmlDocKey ] = Reader.ReadToEnd();

        Reader.Close();

        Reader.Dispose();

      }

    }

    /**************************************************************************/

    [Test]
    public void TestTitlesOuterHtml ()
    {

      Dictionary<string,string> AssetDic = new Dictionary<string, string> () {
        {
          "HtmlDoc001",
          "<title>HtmlDoc001</title>"
        },
        {
          "HtmlDoc002",
          "<title>HtmlDoc002</title>"
        },
        {
          "HtmlDoc003",
          "<title>HtmlDoc003</title>"
        },
        {
          "HtmlDoc004",
          "<title>HtmlDoc004</title>"
        },
        {
          "HtmlDoc005",
          "<title>HtmlDoc005</title>"
        }
      };

      MacroscopeDataExtractorXpaths DataExtractor = new MacroscopeDataExtractorXpaths ( Size: 1 );

      DataExtractor.SetXpath(
        Slot: 0,
        XpathLabel: "TestTitlesOuterHtml",
        XpathString: "//title",
        ExtractorType: MacroscopeConstants.DataExtractorType.OUTERHTML
      );

      DataExtractor.SetActiveInactive(
        Slot: 0,
        State: MacroscopeConstants.ActiveInactive.ACTIVE
      );
      
      foreach( string HtmlDocKey in this.HtmlDocs.Keys )
      {
           
        string Html = this.HtmlDocs[ HtmlDocKey ];

        List<KeyValuePair<string, string>> ResultList = DataExtractor.AnalyzeHtml( Html: Html );

        DebugMsg( string.Format( "HtmlDocKey: {0} :: Value: {1}", HtmlDocKey, ResultList[ 0 ].Value ) );

        Assert.IsNotEmpty( ResultList, "WHOOPS!" );

        Assert.AreEqual( AssetDic[ HtmlDocKey ], ResultList[ 0 ].Value );

      }

    }
    
    /**************************************************************************/

    [Test]
    public void TestTitlesInnerHtml ()
    {

      Dictionary<string,string> AssetDic = new Dictionary<string, string> () {
        {
          "HtmlDoc001",
          "HtmlDoc001"
        },
        {
          "HtmlDoc002",
          "HtmlDoc002"
        },
        {
          "HtmlDoc003",
          "HtmlDoc003"
        },
        {
          "HtmlDoc004",
          "HtmlDoc004"
        },
        {
          "HtmlDoc005",
          "HtmlDoc005"
        }
      };

      MacroscopeDataExtractorXpaths DataExtractor = new MacroscopeDataExtractorXpaths ( Size: 1 );

      DataExtractor.SetXpath(
        Slot: 0,
        XpathLabel: "TestTitlesInnerHtml",
        XpathString: "//title",
        ExtractorType: MacroscopeConstants.DataExtractorType.INNERHTML
      );

      DataExtractor.SetActiveInactive(
        Slot: 0,
        State: MacroscopeConstants.ActiveInactive.ACTIVE
      );
      
      foreach( string HtmlDocKey in this.HtmlDocs.Keys )
      {
           
        string Html = this.HtmlDocs[ HtmlDocKey ];

        List<KeyValuePair<string, string>> ResultList = DataExtractor.AnalyzeHtml( Html: Html );

        DebugMsg( string.Format( "HtmlDocKey: {0} :: Value: {1}", HtmlDocKey, ResultList[ 0 ].Value ) );

        Assert.IsNotEmpty( ResultList, "WHOOPS!" );

        Assert.AreEqual( AssetDic[ HtmlDocKey ], ResultList[ 0 ].Value );

      }

    }
    
    /**************************************************************************/

    [Test]
    public void TestTitlesInnerText ()
    {

      Dictionary<string,string> AssetDic = new Dictionary<string, string> () {
        {
          "HtmlDoc001",
          "HtmlDoc001"
        },
        {
          "HtmlDoc002",
          "HtmlDoc002"
        },
        {
          "HtmlDoc003",
          "HtmlDoc003"
        },
        {
          "HtmlDoc004",
          "HtmlDoc004"
        },
        {
          "HtmlDoc005",
          "HtmlDoc005"
        }
      };

      MacroscopeDataExtractorXpaths DataExtractor = new MacroscopeDataExtractorXpaths ( Size: 1 );

      DataExtractor.SetXpath(
        Slot: 0,
        XpathLabel: "TestTitlesInnerText",
        XpathString: "//title",
        ExtractorType: MacroscopeConstants.DataExtractorType.INNERTEXT
      );

      DataExtractor.SetActiveInactive(
        Slot: 0,
        State: MacroscopeConstants.ActiveInactive.ACTIVE
      );
      
      foreach( string HtmlDocKey in this.HtmlDocs.Keys )
      {
           
        string Html = this.HtmlDocs[ HtmlDocKey ];

        List<KeyValuePair<string, string>> ResultList = DataExtractor.AnalyzeHtml( Html: Html );

        DebugMsg( string.Format( "HtmlDocKey: {0} :: Value: {1}", HtmlDocKey, ResultList[ 0 ].Value ) );

        Assert.IsNotEmpty( ResultList, "WHOOPS!" );

        Assert.AreEqual( AssetDic[ HtmlDocKey ], ResultList[ 0 ].Value );

      }

    }

    /**************************************************************************/
    
  }
	
}
