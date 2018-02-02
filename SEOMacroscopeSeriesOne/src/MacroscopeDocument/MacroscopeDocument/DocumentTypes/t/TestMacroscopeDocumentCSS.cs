﻿/*

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
using ExCSS;
using NUnit.Framework;

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

      foreach ( string Filename in CssGoodDocKeys )
      {
        Reader = new StreamReader(
         stream: Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
       );
        this.CssGoodDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach ( string Filename in CssBadDocKeys )
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
      ExCSS.Parser ExCssParser = new ExCSS.Parser();
      foreach ( string Filename in CssGoodDocs.Keys )
      {
        string CssData = CssGoodDocs[ Filename ];
        ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( CssData );
        Assert.IsNotNull( ExCssStylesheet, string.Format( "FAIL: {0}", Filename ) );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestBadCss ()
    {
      ExCSS.Parser ExCssParser = new ExCSS.Parser();
      foreach ( string Filename in CssBadDocs.Keys )
      {
        string CssData = CssBadDocs[ Filename ];
        ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( CssData );
        Assert.IsNotNull( ExCssStylesheet, string.Format( "FAIL: {0}", Filename ) );
      }
    }

    /**************************************************************************/

  }

}