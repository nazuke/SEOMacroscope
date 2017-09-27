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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeStringTools : Macroscope
  {

    /**************************************************************************/

    private Dictionary<string,string> HtmlDocs;

    /**************************************************************************/
    
    public TestMacroscopeStringTools ()
    {

      StreamReader Reader;
      List<string> HtmlDocKeys = new List<string> ( 16 );

      this.HtmlDocs = new Dictionary<string,string> ();

      this.HtmlDocs.Add( "StringToolsHtmlDoc001", null );

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
    public void TestCleanText ()
    {

      Dictionary<string,string> StringsTable = new Dictionary<string,string> ();

      StringsTable.Add(
        key: "!\"#$%&'()=~|-^\\/,.<>;:@`{}[]*+The quick brown fox jumps over the lazy dog's!\"#$%&'()=~|-^\\/,.<>;:@`{}[]*+",
        value: "The quick brown fox jumps over the lazy dog's"
      );

      StringsTable.Add(
        key: "The quick/slow, and the fast-stop and the second-best things we don't know about.",
        value: "The quick/slow and the fast-stop and the second-best things we don't know about"
      );
      
      StringsTable.Add(
        key: "The markets opened at $100.00 today.",
        value: "The markets opened at $100.00 today"
      );

      foreach( string StringKey in StringsTable.Keys )
      {

        string Cleaned = MacroscopeStringTools.CleanText( Text: StringKey );

        Assert.AreEqual( StringsTable[ StringKey ], Cleaned, string.Format( "NOT VALID: {0}", Cleaned ) );

      }

    }

    /**************************************************************************/

    [Test]
    public void TestCleanTextWithHtmlDoc ()
    {

      Dictionary<string,string> AssetDic = new Dictionary<string, string> ();
      
      AssetDic.Add( 
        key: "StringToolsHtmlDoc001",
        value: "First Heading"
      );

      foreach( string HtmlDocKey in this.HtmlDocs.Keys )
      {

        string Html = this.HtmlDocs[ HtmlDocKey ];

        string Cleaned = MacroscopeStringTools.CleanText( Text: Html );

        DebugMsg( string.Format( "HtmlDocKey: {0} :: Value: ||{1}||", HtmlDocKey, Cleaned ) );

        //Assert.IsNotEmpty( ResultList, "WHOOPS!" );

        //Assert.AreEqual( AssetDic[ HtmlDocKey ], ResultList[ 0 ].Value );

      }

    }

    /**************************************************************************/

  }

}
