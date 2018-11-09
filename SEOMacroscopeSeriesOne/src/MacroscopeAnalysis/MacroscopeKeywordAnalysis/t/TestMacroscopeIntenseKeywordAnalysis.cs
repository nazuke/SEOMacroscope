/*

	This file is part of SEOMacroscope.

	Copyright 2018 Jason Holland.

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
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeIntenseKeywordAnalysis : Macroscope, IMacroscopeTaskController
  {

    /**************************************************************************/

    private Dictionary<string, string> GoodHtmlDocs;
    private Dictionary<string, string> BadHtmlDocs;

    /**************************************************************************/

    public TestMacroscopeIntenseKeywordAnalysis ()
    {

      StreamReader Reader;
      List<string> GoodDocKeys = new List<string>( 2 );
      List<string> BadDocKeys  = new List<string>( 2 );

      GoodDocKeys.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestGoodKeywords.html" );
      BadDocKeys.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestBadKeywords.html" );

      this.GoodHtmlDocs = new Dictionary<string, string>();
      this.BadHtmlDocs = new Dictionary<string, string>();

      foreach( string Filename in GoodDocKeys )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.GoodHtmlDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in BadDocKeys )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.BadHtmlDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

    }

    /**************************************************************************/

    [Test]
    public void TestGoodKeywords ()
    {
      foreach( string HtmlDocKey in this.GoodHtmlDocs.Keys )
      {
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.GoodHtmlDocs[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();

        msDoc.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );

        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );

        string Keywords = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='keywords']" ).GetAttributeValue( name: "content", def: "" );
        string BodyText = string.Join( " ", CleanedText.ToArray() );

        Assert.IsNotEmpty( Keywords, "Keywords is empty" );

        msDoc.SetKeywords( Keywords );

        msDoc.SetDocumentText( Text: BodyText );

        MacroscopeIntenseKeywordAnalysis Analyzer = new MacroscopeIntenseKeywordAnalysis();

        Analyzer.Analyze( msDoc: msDoc );

        //Assert.IsNotEmpty( CleanedText, "CleanedText is empty" );

      }
    }

    /**************************************************************************/
    /*
    [Test]
    public void TestBadKeywords ()
    {
      foreach( string HtmlDocKey in this.BadHtmlDocs.Keys )
      {
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.BadHtmlDocs[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();
        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );
        Assert.IsNotEmpty( CleanedText, "CleanedText is empty" );
      }
    }
    */
    /**************************************************************************/

    public void ICallbackScanComplete ()
    {
    }

    public MacroscopeCredentialsHttp IGetCredentialsHttp ()
    {
      MacroscopeCredentialsHttp CredentialsHttp = new MacroscopeCredentialsHttp();
      return ( CredentialsHttp );
    }

    public void ICallbackOutOfMemory ()
    {
    }

    /**************************************************************************/

  }

}
