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
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeHttpUrlUtils : Macroscope
  {

    /**************************************************************************/

    [Test]
    public void TestFindUrlDepth ()
    {

      Dictionary<string, int> UrlList = new Dictionary<string, int>();

      UrlList.Add( "https://nazuke.github.io/", 0 );
      UrlList.Add( "https://nazuke.github.io/0.html", 0 );
      UrlList.Add( "https://nazuke.github.io/0/1.html", 1 );
      UrlList.Add( "https://nazuke.github.io/0/1/2.html", 2 );
      UrlList.Add( "https://nazuke.github.io/0/1/2/", 2 );
      UrlList.Add( "https://nazuke.github.io/0/1/2/3.html", 3 );
      UrlList.Add( "https://nazuke.github.io/0/1/2/3.html/", 3 );
      UrlList.Add( "https://nazuke.github.io/0/1/2/3/4.html?key=value", 4 );

      foreach( KeyValuePair<string, int> UrlPair in UrlList )
      {
        this.DebugMsg( string.Format( "{0}: {1}", UrlPair.Value, UrlPair.Key ) );
        int Depth = MacroscopeHttpUrlUtils.FindUrlDepth( Url: UrlPair.Key );
        Assert.AreEqual( UrlPair.Value, Depth );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestIsWithinParentDirectory ()
    {
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/index.html";
      List<string> TargetUrls = new List<string>();
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/image" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/page.jsp" );
      TargetUrls.Add( "http://www.companyname.com/path/to/file.pdf" );
      TargetUrls.Add( "http://www.companyname.com/path/" );
      TargetUrls.Add( "http://www.companyname.com/path/index.php" );
      TargetUrls.Add( "http://www.companyname.com/" );
      foreach( string TargetUrl in TargetUrls )
      {
        Assert.IsTrue( MacroscopeHttpUrlUtils.IsWithinParentDirectory( StartUrl: StartUrl, Url: TargetUrl ), string.Format( "FAIL: {0}", TargetUrl ) );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestIsNotWithinParentDirectory ()
    {
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/index.html";
      List<string> TargetUrls = new List<string>();
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/sub-folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/sub-folder/sub-folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/images/some-image.jpg" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/folder/media/image" );
      foreach( string TargetUrl in TargetUrls )
      {
        Assert.IsFalse( MacroscopeHttpUrlUtils.IsWithinParentDirectory( StartUrl: StartUrl, Url: TargetUrl ), string.Format( "FAIL: {0}", TargetUrl ) );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestIsWithinChildDirectory ()
    {
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/index.html";
      List<string> TargetUrls = new List<string>();
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/sub-folder/sub-folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/sub-folder/image" );
      foreach( string TargetUrl in TargetUrls )
      {
        Assert.IsTrue( MacroscopeHttpUrlUtils.IsWithinChildDirectory( StartUrl: StartUrl, Url: TargetUrl ), string.Format( "FAIL: {0}", TargetUrl ) );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestIsNotWithinChildDirectory ()
    {
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/sub-folder/sub-folder/index.html";
      List<string> TargetUrls = new List<string>();
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/folder/image" );
      TargetUrls.Add( "http://www.companyname.com/folder/image" );
      foreach( string TargetUrl in TargetUrls )
      {
        Assert.IsFalse( MacroscopeHttpUrlUtils.IsWithinChildDirectory( StartUrl: StartUrl, Url: TargetUrl ), string.Format( "FAIL: {0}", TargetUrl ) );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestParentFolderUrls ()
    {

      Dictionary<string, int> UrlList = new Dictionary<string, int>();

      UrlList.Add( "https://nazuke.github.io/", 0 );
      UrlList.Add( "https://nazuke.github.io/0.html", 0 );
      UrlList.Add( "https://nazuke.github.io/0/1.html", 1 );
      UrlList.Add( "https://nazuke.github.io/0/1/2.html", 2 );
      UrlList.Add( "https://nazuke.github.io/0/1/2/", 3 );
      UrlList.Add( "https://nazuke.github.io/0/1/2/3.html", 3 );
      UrlList.Add( "https://nazuke.github.io/0/1/2/3.html/", 4 );
      UrlList.Add( "https://nazuke.github.io/0/1/2/3/4.html?key=value", 4 );

      foreach( KeyValuePair<string, int> UrlPair in UrlList )
      {
        List<string> ParentFolderUrls = MacroscopeHttpUrlUtils.GetParentFolderUrls( Url: UrlPair.Key  );
        Assert.AreEqual( UrlPair.Value, ParentFolderUrls.Count );
      }

    }

    /**************************************************************************/

  }

}
