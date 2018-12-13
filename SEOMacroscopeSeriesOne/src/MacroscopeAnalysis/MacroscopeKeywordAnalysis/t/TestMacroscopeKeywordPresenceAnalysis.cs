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
  public class TestMacroscopeKeywordPresenceAnalysis : Macroscope, IMacroscopeTaskController
  {

    /**************************************************************************/

    private Dictionary<string, string> HtmlDocsMalformedKeywords;
    private Dictionary<string, string> HtmlDocsGoodTitle;
    private Dictionary<string, string> HtmlDocsBadTitle;
    private Dictionary<string, string> HtmlDocsGoodDescription;
    private Dictionary<string, string> HtmlDocsBadDescription;
    private Dictionary<string, string> HtmlDocsGoodBody;
    private Dictionary<string, string> HtmlDocsBadBody;
    
    /**************************************************************************/

    public TestMacroscopeKeywordPresenceAnalysis ()
    {

      StreamReader Reader;
      List<string> DocKeysMalformedKeywords = new List<string>( 2 );
      List<string> DocKeysGoodTitle = new List<string>( 2 );
      List<string> DocKeysBadTitle = new List<string>( 2 );
      List<string> DocKeysGoodDescription = new List<string>( 2 );
      List<string> DocKeysBadDescription = new List<string>( 2 );
      List<string> DocKeysGoodBody = new List<string>( 2 );
      List<string> DocKeysBadBody = new List<string>( 2 );

      DocKeysMalformedKeywords.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestMalformedKeywords.html" );

      DocKeysGoodTitle.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestGoodTitleKeywords.html" );
      DocKeysBadTitle.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestBadTitleKeywords.html" );

      DocKeysGoodDescription.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestGoodDescriptionKeywords.html" );
      DocKeysBadDescription.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestBadDescriptionKeywords.html" );

      DocKeysGoodBody.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestGoodBodyKeywords.html" );
      DocKeysBadBody.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeKeywordAnalysis.t.HtmlDocs.TestBadBodyKeywords.html" );

      this.HtmlDocsMalformedKeywords = new Dictionary<string, string>();
      this.HtmlDocsGoodTitle = new Dictionary<string, string>();
      this.HtmlDocsBadTitle = new Dictionary<string, string>();
      this.HtmlDocsGoodDescription = new Dictionary<string, string>();
      this.HtmlDocsBadDescription = new Dictionary<string, string>();
      this.HtmlDocsGoodBody = new Dictionary<string, string>();
      this.HtmlDocsBadBody = new Dictionary<string, string>();

      foreach( string Filename in DocKeysMalformedKeywords )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.HtmlDocsMalformedKeywords.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in DocKeysGoodTitle )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.HtmlDocsGoodTitle.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in DocKeysBadTitle )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.HtmlDocsBadTitle.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in DocKeysGoodDescription )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.HtmlDocsGoodDescription.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in DocKeysBadDescription )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.HtmlDocsBadDescription.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in DocKeysGoodBody )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.HtmlDocsGoodBody.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in DocKeysBadBody )
      {
        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );
        this.HtmlDocsBadBody.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

    }

    /** MALFORMED KEYWORDS ****************************************************/

    [Test]
    public void TestMalformedKeywords ()
    {

      foreach( string HtmlDocKey in this.HtmlDocsMalformedKeywords.Keys )
      {

        bool Passes = false;
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.HtmlDocsMalformedKeywords[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();

        msDoc.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );

        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );

        string Keywords = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='keywords']" ).GetAttributeValue( name: "content", def: "" );
        string TitleText = HtmlDoc.DocumentNode.SelectSingleNode( "//title" ).InnerText;
        string BodyText = string.Join( " ", CleanedText.ToArray() );

        Assert.IsNotEmpty( Keywords, "Keywords is empty" );

        msDoc.SetKeywords( Keywords );
        msDoc.SetTitle( TitleText: TitleText, ProcessingMode: MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES );
        msDoc.SetDocumentText( Text: BodyText );

        MacroscopeKeywordPresenceAnalysis Analyzer = new MacroscopeKeywordPresenceAnalysis();

        List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence = Analyzer.AnalyzeKeywordPresence( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {
          if( Pair.Value == MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MALFORMED_KEYWORDS_METATAG )
          {
            Passes = true;
          }
        }

        Assert.IsTrue( Passes );

      }

    }

    /** TITLE *****************************************************************/

    [Test]
    public void TestGoodTitleKeywords ()
    {

      foreach( string HtmlDocKey in this.HtmlDocsGoodTitle.Keys )
      {

        bool Passes = false;
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.HtmlDocsGoodTitle[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();

        msDoc.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );

        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );

        string Keywords = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='keywords']" ).GetAttributeValue( name: "content", def: "" );
        string TitleText = HtmlDoc.DocumentNode.SelectSingleNode( "//title" ).InnerText;
        string BodyText = string.Join( " ", CleanedText.ToArray() );

        Assert.IsNotEmpty( Keywords, "Keywords is empty" );

        msDoc.SetKeywords( Keywords );
        msDoc.SetTitle( TitleText: TitleText, ProcessingMode: MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES );
        msDoc.SetDocumentText( Text: BodyText );

        MacroscopeKeywordPresenceAnalysis Analyzer = new MacroscopeKeywordPresenceAnalysis();

        List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence = Analyzer.AnalyzeKeywordPresence( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {
          if( Pair.Value == MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_TITLE )
          {
            Passes = true;
          }
        }

        Assert.IsTrue( Passes );

      }

    }

    /** -------------------------------------------------------------------- **/

    [Test]
    public void TestBadTitleKeywords ()
    {

      foreach( string HtmlDocKey in this.HtmlDocsBadTitle.Keys )
      {

        bool Passes = false;
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.HtmlDocsBadTitle[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();

        msDoc.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );

        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );

        string Keywords = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='keywords']" ).GetAttributeValue( name: "content", def: "" );
        string TitleText = HtmlDoc.DocumentNode.SelectSingleNode( "//title" ).InnerText;
        string BodyText = string.Join( " ", CleanedText.ToArray() );

        Assert.IsNotEmpty( Keywords, "Keywords is empty" );

        msDoc.SetKeywords( Keywords );
        msDoc.SetTitle( TitleText: TitleText, ProcessingMode: MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES );
        msDoc.SetDocumentText( Text: BodyText );

        MacroscopeKeywordPresenceAnalysis Analyzer = new MacroscopeKeywordPresenceAnalysis();

        List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence = Analyzer.AnalyzeKeywordPresence( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {
          if( Pair.Value == MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_TITLE )
          {
            Passes = true;
          }
        }

        Assert.IsTrue( Passes );

      }

    }

    /** DESCRIPTION ***********************************************************/

    [Test]
    public void TestGoodDescriptionKeywords ()
    {

      foreach( string HtmlDocKey in this.HtmlDocsGoodDescription.Keys )
      {

        bool Passes = false;
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.HtmlDocsGoodDescription[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();

        msDoc.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );

        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );

        string Keywords = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='keywords']" ).GetAttributeValue( name: "content", def: "" );
        string DescriptionText = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='description']" ).GetAttributeValue( name: "content", def: "" );
        string BodyText = string.Join( " ", CleanedText.ToArray() );

        Assert.IsNotEmpty( Keywords, "Keywords is empty" );

        msDoc.SetKeywords( Keywords );
        msDoc.SetDescription( DescriptionText: DescriptionText, ProcessingMode: MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES );
        msDoc.SetDocumentText( Text: BodyText );

        MacroscopeKeywordPresenceAnalysis Analyzer = new MacroscopeKeywordPresenceAnalysis();

        List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence = Analyzer.AnalyzeKeywordPresence( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {
          if( Pair.Value == MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_DESCRIPTION )
          {
            Passes = true;
          }
        }

        Assert.IsTrue( Passes );

      }

    }

    /** -------------------------------------------------------------------- **/

    [Test]
    public void TestBadDescriptionKeywords ()
    {

      foreach( string HtmlDocKey in this.HtmlDocsBadDescription.Keys )
      {

        bool Passes = false;
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.HtmlDocsBadDescription[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();

        msDoc.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );

        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );

        string Keywords = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='keywords']" ).GetAttributeValue( name: "content", def: "" );
        string DescriptionText = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='description']" ).GetAttributeValue( name: "content", def: "" );
        string BodyText = string.Join( " ", CleanedText.ToArray() );

        Assert.IsNotEmpty( Keywords, "Keywords is empty" );

        msDoc.SetKeywords( Keywords );
        msDoc.SetDescription( DescriptionText: DescriptionText, ProcessingMode: MacroscopeConstants.TextProcessingMode.DECODE_HTML_ENTITIES );
        msDoc.SetDocumentText( Text: BodyText );

        MacroscopeKeywordPresenceAnalysis Analyzer = new MacroscopeKeywordPresenceAnalysis();

        List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence = Analyzer.AnalyzeKeywordPresence( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {
          if( Pair.Value == MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_DESCRIPTION )
          {
            Passes = true;
          }
        }

        Assert.IsTrue( Passes );

      }

    }

    /** BODY ******************************************************************/

    [Test]
    public void TestGoodBodyKeywords ()
    {

      foreach( string HtmlDocKey in this.HtmlDocsGoodBody.Keys )
      {

        bool Passes = false;
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.HtmlDocsGoodBody[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();

        msDoc.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );

        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );

        string Keywords = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='keywords']" ).GetAttributeValue( name: "content", def: "" );
        string BodyText = string.Join( " ", CleanedText.ToArray() );

        Assert.IsNotEmpty( Keywords, "Keywords is empty" );

        msDoc.SetKeywords( Keywords );

        msDoc.SetDocumentText( Text: BodyText );

        MacroscopeKeywordPresenceAnalysis Analyzer = new MacroscopeKeywordPresenceAnalysis();

        List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence = Analyzer.AnalyzeKeywordPresence( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {
          if( Pair.Value == MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.PRESENT_IN_BODY )
          {
            Passes = true;
          }
        }

        Assert.IsTrue( Passes );

      }

    }

    /** -------------------------------------------------------------------- **/

    [Test]
    public void TestBadBodyKeywords ()
    {

      foreach( string HtmlDocKey in this.HtmlDocsBadBody.Keys )
      {

        bool Passes = false;
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.HtmlDocsBadBody[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();

        msDoc.SetDocumentType( Type: MacroscopeConstants.DocumentType.HTML );

        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );

        string Keywords = HtmlDoc.DocumentNode.SelectSingleNode( "//meta[@name='keywords']" ).GetAttributeValue( name: "content", def: "" );
        string BodyText = string.Join( " ", CleanedText.ToArray() );

        Assert.IsNotEmpty( Keywords, "Keywords is empty" );

        msDoc.SetKeywords( Keywords );

        msDoc.SetDocumentText( Text: BodyText );

        MacroscopeKeywordPresenceAnalysis Analyzer = new MacroscopeKeywordPresenceAnalysis();

        List<KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS>> KeywordPresence = Analyzer.AnalyzeKeywordPresence( msDoc: msDoc );

        foreach( KeyValuePair<string, MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS> Pair in KeywordPresence )
        {
          if( Pair.Value == MacroscopeKeywordPresenceAnalysis.KEYWORD_STATUS.MISSING_IN_BODY )
          {
            Passes = true;
          }
        }

        Assert.IsTrue( Passes );

      }

    }

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
