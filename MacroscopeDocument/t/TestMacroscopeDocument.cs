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
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    [Test]
    public void TestHtmlDocument ()
    {

      List<string> UrlList = new List<string> () {
        {
          "https://nazuke.github.io/SEOMacroscope/"
        }
      };

      foreach( string Url in UrlList )
      {

        MacroscopeDocument msDoc = new MacroscopeDocument ( Url: Url );
        
        Assert.IsNotNull( msDoc, string.Format( "FAIL: {0}", Url ) );

        Assert.IsTrue( msDoc.Execute(), string.Format( "FAIL: {0}", "Execute()" ) );
          
        Assert.AreEqual( Url, msDoc.GetUrl(), string.Format( "FAIL: {0}", Url ) );
      
        Assert.IsTrue( msDoc.GetIsHtml(), string.Format( "FAIL: {0}", Url ) );

      }

    }
    
    /**************************************************************************/

    [Test]
    public void TestDetectLanguage ()
    {

      List<string> UrlList = new List<string> () {
        {
          "https://nazuke.github.io/SEOMacroscope/"
        }
      };

      MacroscopePreferencesManager.SetDetectLanguage( Detect: true );
      MacroscopePreferencesManager.SetRequestTimeout( Seconds: 10 );
                
      for( int i = 0 ; i < 10 ; i++ )
      {
        
        foreach( string Url in UrlList )
        {

          MacroscopeDocument msDoc = new MacroscopeDocument ( Url: Url );
        
          Assert.IsNotNull( msDoc, string.Format( "FAIL: {0}", Url ) );

          Assert.IsTrue( msDoc.Execute(), string.Format( "FAIL: {0}", "Execute()" ) );

          Assert.IsTrue( msDoc.GetIsHtml(), string.Format( "FAIL: {0}", Url ) );

          Assert.IsNotNullOrEmpty( msDoc.GetTitle(), string.Format( "FAIL: {0}", msDoc.GetTitle() ) );

          string LanguageTitle = msDoc.GetTitleLanguage();
          string LanguageDescription = msDoc.GetDescriptionLanguage();
          string LanguageBodyText = msDoc.GetBodyTextLanguage();

          Assert.AreEqual( "eng", LanguageTitle, string.Format( "FAIL: {0} :: {1}", "LanguageTitle", LanguageTitle ) );

          Assert.AreEqual( "eng", LanguageDescription, string.Format( "FAIL: {0} :: {1}", "LanguageDescription", LanguageDescription ) );

          Assert.AreEqual( "eng", LanguageBodyText, string.Format( "FAIL: {0} :: {1}", "LanguageBodyText", LanguageBodyText ) );

        }
      
      }

    }

    /**************************************************************************/

  }

}
