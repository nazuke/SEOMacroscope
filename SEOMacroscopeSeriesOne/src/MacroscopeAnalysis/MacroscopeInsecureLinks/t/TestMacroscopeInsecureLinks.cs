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
  public class TestMacroscopeInsecureLinks : Macroscope
  {

    // TODO: Implement tests

    /**************************************************************************/

    private Dictionary<string, string> HtmlDocs;

    /**************************************************************************/

    public TestMacroscopeInsecureLinks ()
    {

      StreamReader Reader;
      List<string> DocKeys = new List<string>( 16 );

      DocKeys.Add( "SEOMacroscope.src.MacroscopeAnalysis.MacroscopeInsecureLinks.t.HtmlDocs.MacroscopeInsecureLinks001.html" );

      this.HtmlDocs = new Dictionary<string, string>();

      foreach ( string Filename in DocKeys )
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
    public void TestAnalyze ()
    {

      MacroscopeInsecureLinks InsecureLinks = new MacroscopeInsecureLinks();

      MacroscopeDocument msDoc = new MacroscopeDocument( "https://nazuke.github.io/" );

      InsecureLinks.Analyze( msDoc: msDoc );

      List<string> InsecureList = msDoc.GetInsecureLinks();

      // TODO: Finish this
      //Assert.Greater( InsecureList.Count, 0 );

    }

    /**************************************************************************/

  }

}
