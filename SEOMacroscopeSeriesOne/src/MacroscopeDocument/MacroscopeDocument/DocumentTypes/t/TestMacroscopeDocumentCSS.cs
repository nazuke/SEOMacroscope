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
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using Alba.CsCss.Gfx;
using Alba.CsCss.Style;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDocumentCSS : Macroscope
  {

    /**************************************************************************/

    private Dictionary<string, string> CssGoodDocs;
    private Dictionary<string, string> CssBadDocs;

    /**************************************************************************/

    public TestMacroscopeDocumentCSS ()
    {

      StreamReader Reader;
      List<string> CssGoodDocKeys = new List<string>( 16 );
      List<string> CssBadDocKeys = new List<string>( 16 );

      this.CssGoodDocs = new Dictionary<string, string>();
      this.CssBadDocs = new Dictionary<string, string>();

      CssGoodDocKeys.Add( "SEOMacroscope.src.MacroscopeDocument.MacroscopeDocument.DocumentTypes.t.CssDocs.TestCssDocumentGood001.css" );

      CssBadDocKeys.Add( "SEOMacroscope.src.MacroscopeDocument.MacroscopeDocument.DocumentTypes.t.CssDocs.TestCssDocumentBad001.css" );

      foreach( string Filename in CssGoodDocKeys )
      {
        Reader = new StreamReader(
         stream: Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
       );
        this.CssGoodDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in CssBadDocKeys )
      {
        Reader = new StreamReader(
         stream: Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
       );
        this.CssBadDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

    }

    /**************************************************************************/

    [Test]
    public void TestSimpleCssParsing ()
    {

      CssLoader Parser = new CssLoader();

      foreach( string Filename in CssGoodDocs.Keys )
      {

        string CssData = CssGoodDocs[ Filename ];
        CssStyleSheet Stylesheet = Parser.ParseSheet( CssData, new Uri( "https://nazuke.github.io/dummy.css" ), new Uri( "https://nazuke.github.io/dummy.css" ) );

        Assert.IsNotNull( Stylesheet, string.Format( "FAIL: {0}", Filename ) );

      }

    }

    /**************************************************************************/

    [Test]
    public void TestBadCss ()
    {

      CssLoader Parser = new CssLoader();

      foreach( string Filename in CssBadDocs.Keys )
      {

        string CssData = CssBadDocs[ Filename ];
        CssStyleSheet Stylesheet = Parser.ParseSheet( CssData, new Uri( "https://nazuke.github.io/dummy.css" ), new Uri( "https://nazuke.github.io/dummy.css" ) );

        Assert.IsNotNull( Stylesheet, string.Format( "FAIL: {0}", Filename ) );

      }

    }


    /**************************************************************************/

    /*
    [Test]
    public void TestExCssExample ()
    {
      var parser = new StylesheetParser();
      var stylesheet = parser.Parse( ".someClass{color: red; background-image: url('/images/logo.png')" );
      var rule = stylesheet.Rules.First();
      var selector = rule.SelectorText; // Yields .someClass
      var color = rule.Style.Color;
      var image = rule.Style.BackgroundImage; // url('/images/logo.png')
    }
    */

    /**************************************************************************/

  }

}
