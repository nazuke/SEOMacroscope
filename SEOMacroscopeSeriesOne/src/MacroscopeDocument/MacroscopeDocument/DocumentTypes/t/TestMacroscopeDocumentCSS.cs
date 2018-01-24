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
using ExCSS;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDocumentCSS : Macroscope
  {

    /**************************************************************************/

    private Dictionary<string, string> CssDocs;

    /**************************************************************************/

    public TestMacroscopeDocumentCSS ()
    {

      StreamReader Reader;
      List<string> CssDocKeys = new List<string>( 16 );

      this.CssDocs = new Dictionary<string, string>();

      CssDocKeys.Add( "SEOMacroscope.src.MacroscopeDocument.MacroscopeDocument.DocumentTypes.t.CssDocs.TestCssDocument001.css" );

      foreach ( string Filename in CssDocKeys )
      {
        Reader = new StreamReader(
         stream: Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
       );
        this.CssDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

    }

    /**************************************************************************/

    [Test]
    public void TestSimpleCssParsing ()
    {
      ExCSS.Parser ExCssParser = new ExCSS.Parser();
      foreach ( string Filename in CssDocs.Keys )
      {
        string CssData = CssDocs[ Filename ];
        ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( CssData );
        Assert.IsNotNull( ExCssStylesheet, string.Format( "FAIL: {0}", Filename ) );
      }
    }

    /**************************************************************************/

  }

}
