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
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeHttpTwoClient : Macroscope
  {

    /**************************************************************************/

    [Test]
    public async Task TestHttpProtocolProbe ()
    {
      MacroscopeHttpTwoClient Client = new MacroscopeHttpTwoClient();
      List<string> UrlList = new List<string>();

      UrlList.Add( "https://nazuke.github.io/robots.txt" );

      foreach( string Url in UrlList)
      {

        this.DebugMsg( string.Format( "Url: {0}", Url ) );

        MacroscopeHttpTwoClientResponse ClientResponse = await Client.Get( Url: Url, ConfigureRequestHeaders: this.ConfigureRequestHeaders );

        Assert.Greater( ClientResponse.GetContentAsString().Length, 0 );

        this.DebugMsg( ClientResponse.GetContentAsString() );

      }

    }

    /**************************************************************************/

    private void ConfigureRequestHeaders ( HttpRequestMessage Request )
    {
      this.DebugMsg( "ConfigureRequestHeaders Called" );
    }

    /**************************************************************************/

  }

}
