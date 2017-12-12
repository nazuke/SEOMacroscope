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
using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeHttpProtocolProbe : Macroscope
  {

    /**************************************************************************/

    //[Test] DISABLED - Does not work below Windows 10
    public async Task TestHttpProtocolProbe ()
    {

      MacroscopeHttpProtocolProbe HttpProtocolProbe;
      MacroscopeHttpProtocolProbe.HttpProtocolVersion HttpProtocolVersion;
      Dictionary<string, MacroscopeHttpProtocolProbe.HttpProtocolVersion> UrlList;

      UrlList = new Dictionary<string, MacroscopeHttpProtocolProbe.HttpProtocolVersion>();

      UrlList.Add( "https://nazuke.github.io/", MacroscopeHttpProtocolProbe.HttpProtocolVersion.HTTP_TWO );
      UrlList.Add( "https://http2.akamai.com/demo", MacroscopeHttpProtocolProbe.HttpProtocolVersion.HTTP_TWO );
      UrlList.Add( "https://http1.akamai.com/demo/h2_demo_frame.html", MacroscopeHttpProtocolProbe.HttpProtocolVersion.HTTP_ONE_POINT_ONE );
      UrlList.Add( "https://http2.akamai.com/demo/h2_demo_frame.html", MacroscopeHttpProtocolProbe.HttpProtocolVersion.HTTP_TWO );
     
      HttpProtocolProbe = new MacroscopeHttpProtocolProbe();

      foreach( string Url in UrlList.Keys )
      {

        HttpProtocolVersion = await HttpProtocolProbe.Probe( Url: Url );

        this.DebugMsg( string.Format( "HttpProtocolVersion: {0}", HttpProtocolVersion ) );

        Assert.AreEqual( UrlList[ Url ], HttpProtocolVersion );

      }

    }

    /**************************************************************************/

  }

}
