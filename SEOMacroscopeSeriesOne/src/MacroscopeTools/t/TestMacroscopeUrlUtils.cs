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
  public class TestMacroscopeUrlUtils
  {

    /**************************************************************************/

    [Test]
    public void TestMakeUrlAbsoluteUrls ()
    {

      Dictionary<string,string> UrlTable = new Dictionary<string,string> ();

      UrlTable.Add(
        @"path/to/images/picture.gif",
        @"http://www.host.com/path/to/page/path/to/images/picture.gif"
      );

      UrlTable.Add(
        @"../path/to/images/picture.gif",
        @"http://www.host.com/path/to/path/to/images/picture.gif"
      );

      UrlTable.Add(
        @"../../path/to/images/picture.gif",
        @"http://www.host.com/path/path/to/images/picture.gif"
      );
      
      const string BaseUrl = "http://www.host.com/path/to/page/";
      const string Filename = "index.html";
      string Url = string.Join( "", BaseUrl, Filename );

      foreach( string RelativeUrl in UrlTable.Keys )
      {
        string sAbsoluteUrl = MacroscopeUrlUtils.MakeUrlAbsolute( Url, RelativeUrl );
        Assert.AreEqual( UrlTable[ RelativeUrl ], sAbsoluteUrl, "DO NOT MATCH" );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestMakeUrlAbsoluteUrlsWithBaseHref ()
    {

      /*
        List Items:
          Base HREF
          Base URL
          Page URL
          Absolute URL
      */

      List<List<string>> TestList = new List<List<string>> ();

      TestList.Add( new List<string> () );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/BASEHREF/index.html" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/path/to/page/" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/path/to/page/to/pages/index.html" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/path/to/page/to/pages/index.html" );

      TestList.Add( new List<string> () );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/BASEHREF/index.html" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/path/to/page/" );
      TestList[ TestList.Count - 1 ].Add( "path/to/pages/index.html" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/BASEHREF/path/to/pages/index.html" );
            
      TestList.Add( new List<string> () );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/BASEHREF/index.html" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/path/to/page/" );
      TestList[ TestList.Count - 1 ].Add( "../path/to/pages/index.html" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/path/to/pages/index.html" );
            
      TestList.Add( new List<string> () );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/BASEHREF/index.html" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/path/to/page/" );
      TestList[ TestList.Count - 1 ].Add( "../../path/to/pages/index.html" );
      TestList[ TestList.Count - 1 ].Add( "http://www.host.com/path/to/pages/index.html" );

      foreach( List<string> UrlSet in TestList )
      {

        string BaseHref = UrlSet[ 0 ];
        string BaseUrl = UrlSet[ 1 ];
        string PageUrl = UrlSet[ 2 ];
        string AbsoluteUrl = UrlSet[ 3 ];

        string ResolvedUrl;

        ResolvedUrl = MacroscopeUrlUtils.MakeUrlAbsolute(
          BaseHref: BaseHref,
          BaseUrl: BaseUrl,
          Url: PageUrl
        );
       
        Assert.AreEqual( AbsoluteUrl, ResolvedUrl, "DO NOT MATCH" );
      
      }

    }

    /**************************************************************************/

    [Test]
    public void TestValidateUrls ()
    {

      Dictionary<string,bool> UrlList = new Dictionary<string,bool> ();
        
      UrlList.Add(
        "http://www.host.com/",
        true
      );
      
      UrlList.Add(
        "http://www.host.com/index.html",
        true
      );
      
      UrlList.Add(
        "http://www.host.com/path/path/to/images/picture.gif",
        true
      );
      
      UrlList.Add(
        "http://www.host.com/??",
        true
      );
      
      UrlList.Add(
        "http://www.host.com/ ",
        true
      );
      
      UrlList.Add(
        "http://   www.host.com/",
        false
      );

      foreach( string Url in UrlList.Keys )
      {
        bool IsValid = MacroscopeUrlUtils.ValidateUrl( Url );
        Assert.AreEqual( UrlList[ Url ], IsValid, string.Format( "NOT VALID: {0}", Url ) );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestCleanUrlCss ()
    {

      Dictionary<string,string> PropertiesTable = new Dictionary<string,string> ();

      PropertiesTable.Add(
        "background-image:none;",          
        null
      );
      
      PropertiesTable.Add(
        "background: #0b7bee url(none) no-repeat center center/cover;",
        null
      );
      
      PropertiesTable.Add(
        "background: #0b7bee url(images/video-bg.jpg) no-repeat center center/cover;",
        "images/video-bg.jpg"
      );
      
      PropertiesTable.Add(
        "background: #0b7bee url(\"images/video-bg.jpg\") no-repeat center center/cover;",
        "images/video-bg.jpg"
      );
      
      PropertiesTable.Add(
        "src: url(\"fonts/company/latin-e-bold-eot.eot\");",
        "fonts/company/latin-e-bold-eot.eot"
      );
      
      PropertiesTable.Add( 
        "src: url(\"fonts/company/latin-e-bold-eot.eot?#iefix\") format(\"embedded-opentype\"),url(\"fonts/company/latin-e-bold-woff.woff\") format(\"woff\"),url(\"fonts/company/latin-e-bold-ttf.ttf\") format(\"truetype\");",
        "fonts/company/latin-e-bold-eot.eot?#iefix"
      );
      
      PropertiesTable.Add( 
        "background: #ffffff url(images/services/features-background.png) no-repeat left bottom;",
        "images/services/features-background.png"
      );
      
      PropertiesTable.Add( 
        "background: transparent url(\"images/home/mouse.png\") no-repeat 90% top;",
        "images/home/mouse.png"
      );
      
      PropertiesTable.Add( 
        "background: #0b7bee url(images/services/features-background_hover.png) no-repeat left bottom;",
        "images/services/features-background_hover.png"
      );
      
      PropertiesTable.Add( 
        "background-image: url(\"images/global/page-head-trans.png\");",
        "images/global/page-head-trans.png"
      );
      
      PropertiesTable.Add( 
        "background-image: url(\"images/heroes/hero.jpg\");",
        "images/heroes/hero.jpg"
      );

      foreach( string PropertyKey in PropertiesTable.Keys )
      {
        string Cleaned = MacroscopeUrlUtils.CleanUrlCss( PropertyKey );
        Assert.AreEqual( PropertiesTable[ PropertyKey ], Cleaned, string.Format( "NOT VALID: {0}", Cleaned ) );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestStripQueryString ()
    {

      Dictionary<string,string> UrlList = new Dictionary<string,string> ();

      UrlList.Add( "http://www.host.com/#aberdeen-angus", "http://www.host.com/#aberdeen-angus" );

      UrlList.Add( "http://www.host.com/product/list/#boris", "http://www.host.com/product/list/#boris" );

      UrlList.Add( "http://www.host.com/product/list/index.html#boris", "http://www.host.com/product/list/index.html#boris" );

      UrlList.Add( "http://www.host.com/?key1=value1&key2=value2&key3=value3#boris", "http://www.host.com/#boris" );

      UrlList.Add( "http://www.host.com/?key1=value1&key2=value2&key3=value3#gonzo", "http://www.host.com/#gonzo" );

      UrlList.Add( "http://www.host.com/index.html?key1=value1&key2=value2&key3=value3#gonzo", "http://www.host.com/index.html#gonzo" );

      foreach( string Url in UrlList.Keys )
      {
        string UrlResult = MacroscopeUrlUtils.StripQueryString( Url );
        Assert.AreEqual( UrlList[ Url ], UrlResult, string.Format( "NOT VALID: {0}", Url ) );
      }

    }
    
    /**************************************************************************/

    [Test]
    public void TestStripHashFragment ()
    {

      Dictionary<string,string> UrlList = new Dictionary<string,string> ();

      UrlList.Add( "http://www.host.com/#aberdeen-angus", "http://www.host.com/" );

      UrlList.Add( "http://www.host.com/product/list/#boris", "http://www.host.com/product/list/" );

      UrlList.Add( "http://www.host.com/product/list/index.html#boris", "http://www.host.com/product/list/index.html" );

      UrlList.Add( "http://www.host.com/?key1=value1&key2=value2&key3=value3", "http://www.host.com/?key1=value1&key2=value2&key3=value3" );

      UrlList.Add( "http://www.host.com/?key1=value1&key2=value2&key3=value3#gonzo", "http://www.host.com/?key1=value1&key2=value2&key3=value3" );

      UrlList.Add( "http://www.host.com/index.html?key1=value1&key2=value2&key3=value3#gonzo", "http://www.host.com/index.html?key1=value1&key2=value2&key3=value3" );

      foreach( string Url in UrlList.Keys )
      {
        string UrlResult = MacroscopeUrlUtils.StripHashFragment( Url );
        Assert.AreEqual( UrlList[ Url ], UrlResult, string.Format( "NOT VALID: {0}", Url ) );
      }

    }

    /**************************************************************************/

  }

}
