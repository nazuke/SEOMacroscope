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
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDocument : Macroscope, IMacroscopeTaskController
  {

    /**************************************************************************/

    private Dictionary<string, string> HtmlDocs;

    /**************************************************************************/

    public TestMacroscopeDocument ()
    {

      StreamReader Reader;
      List<string> DocKeys = new List<string>( 16 );

      DocKeys.Add( "SEOMacroscope.src.MacroscopeDocument.t.HtmlDocs.TestHtmlDocument001.html" );

      this.HtmlDocs = new Dictionary<string, string>();

      foreach( string Filename in DocKeys )
      {

        Reader = new StreamReader(
          Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
        );

        this.HtmlDocs.Add( Filename, Reader.ReadToEnd() );

        Reader.Close();

      }

    }

    /**************************************************************************/

    [Test]
    public async Task TestHtmlDocument ()
    {

      MacroscopeJobMaster JobMaster;
      MacroscopeDocumentCollection DocCollection;

      List<string> UrlList = new List<string>();

      UrlList.Add( "https://nazuke.github.io/" );

      JobMaster = new MacroscopeJobMaster(
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );

      foreach( string Url in UrlList )
      {

        MacroscopeDocument msDoc = DocCollection.CreateDocument( Url: Url );

        Assert.IsNotNull( msDoc, string.Format( "FAIL: {0}", Url ) );

        bool ExecuteResult = await msDoc.Execute();

        Assert.IsTrue( ExecuteResult, string.Format( "FAIL: {0}", "Execute()" ) );

        Assert.AreEqual( Url, msDoc.GetUrl(), string.Format( "FAIL: {0}", Url ) );

        Assert.IsTrue( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ), string.Format( "FAIL: {0}", Url ) );

      }

    }

    /**************************************************************************/

    [Test]
    public async Task TestTextDocument ()
    {

      MacroscopeJobMaster JobMaster;
      MacroscopeDocumentCollection DocCollection;

      List<string> UrlList = new List<string>();

      UrlList.Add( "https://nazuke.github.io/robots.txt" );

      JobMaster = new MacroscopeJobMaster(
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );

      foreach( string Url in UrlList )
      {

        MacroscopeDocument msDoc = DocCollection.CreateDocument( Url: Url );

        Assert.IsNotNull( msDoc, string.Format( "FAIL: {0}", Url ) );

        bool ExecuteResult = await msDoc.Execute();

        Assert.IsTrue( ExecuteResult, string.Format( "FAIL: {0}", "Execute()" ) );

        Assert.AreEqual( Url, msDoc.GetUrl(), string.Format( "FAIL: {0}", Url ) );

        Assert.IsTrue( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.TEXT ), string.Format( "FAIL: {0}", Url ) );

        /** Content Property Assertions ------------------------------------ **/

        Assert.AreEqual( "text/plain", msDoc.GetMimeType() );
        Assert.Greater( msDoc.GetContentLength(), 0 );

      }

    }

    /**************************************************************************/

    [Test]
    public async Task TestDetectLanguage ()
    {

      MacroscopeJobMaster JobMaster;
      MacroscopeDocumentCollection DocCollection;
      List<string> UrlList = new List<string>();
      UrlList.Add( "https://nazuke.github.io/SEOMacroscope/" );
      MacroscopePreferencesManager.SetDefaultValues();
      MacroscopePreferencesManager.SetDetectLanguage( Enabled: true );
      MacroscopePreferencesManager.SetRequestTimeout( Seconds: 10 );

      JobMaster = new MacroscopeJobMaster(
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );

      for( int i = 0 ; i < 10 ; i++ )
      {

        foreach( string Url in UrlList )
        {

          MacroscopeDocument msDoc = DocCollection.CreateDocument( Url: Url );

          Assert.IsNotNull( msDoc, string.Format( "FAIL: {0}", Url ) );

          bool ExecuteResult = await msDoc.Execute();

          Assert.IsTrue( ExecuteResult, string.Format( "FAIL: {0}", "Execute()" ) );

          Assert.IsTrue( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ), string.Format( "FAIL: {0}", Url ) );

          Assert.IsNotNull( msDoc.GetTitle(), string.Format( "FAIL: {0}", msDoc.GetTitle() ) );

          Assert.IsNotEmpty( msDoc.GetTitle(), string.Format( "FAIL: {0}", msDoc.GetTitle() ) );

          string LanguageTitle = msDoc.GetTitleLanguage();
          string LanguageDescription = msDoc.GetDescriptionLanguage();
          string LanguageBodyText = msDoc.GetDocumentTextLanguage();

          Assert.AreEqual( "en", LanguageTitle, string.Format( "FAIL: {0} :: {1}", "LanguageTitle", LanguageTitle ) );

          Assert.AreEqual( "en", LanguageDescription, string.Format( "FAIL: {0} :: {1}", "LanguageDescription", LanguageDescription ) );

          Assert.AreEqual( "en", LanguageBodyText, string.Format( "FAIL: {0} :: {1}", "LanguageBodyText", LanguageBodyText ) );

        }

      }

    }

    /**************************************************************************/

    [Test]
    public void TestGetNodeText ()
    {
      foreach( string HtmlDocKey in this.HtmlDocs.Keys )
      {
        MacroscopeDocument msDoc = new MacroscopeDocument( Url: "https://nazuke.github.io/" );
        string Html = this.HtmlDocs[ HtmlDocKey ];
        HtmlDocument HtmlDoc = new HtmlDocument();
        HtmlDoc.LoadHtml( html: Html );
        List<string> CleanedText = msDoc.GetNodeText( Node: HtmlDoc.DocumentNode );
        Assert.IsNotEmpty( CleanedText, "CleanedText is empty" );
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
