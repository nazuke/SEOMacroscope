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
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeCheckForUpdate : Macroscope
  {

    /**************************************************************************/

    [Test]
    public void TestParseVersionNumber ()
    {

      MacroscopeCheckForUpdate CheckForUpdate = new MacroscopeCheckForUpdate();
      Dictionary<string, int[]> Versions = new Dictionary<string, int[]>();

      Versions.Add( "1.2.3.4", new int[] { 1, 2, 3, 4 } );
      Versions.Add( "888.888.888.888", new int[] { 888, 888, 888, 888 } );
      Versions.Add( "123.456.789.101", new int[] { 123, 456, 789, 101 } );

      foreach( KeyValuePair<string, int[]> Version in Versions )
      {
        int[] Parsed = CheckForUpdate.ParseVersionNumber( VersionString: Version.Key );
        Assert.AreEqual( Version.Value, Parsed );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestIsVersionNewer ()
    {

      MacroscopeCheckForUpdate CheckForUpdate = new MacroscopeCheckForUpdate();
      string CurrentVersion = "1.2.3.4";
      Dictionary<string, bool> Versions = new Dictionary<string, bool>();

      Versions.Add( CurrentVersion, false );
      Versions.Add( "xxxx", false );
      Versions.Add( "0.88.3.99", false );
      Versions.Add( "1.3.3.4", true );
      Versions.Add( "1.1.3.4", false );
      Versions.Add( "10.99.17.69", true );

      foreach( KeyValuePair<string, bool> Version in Versions )
      {
        bool result = CheckForUpdate.IsVersionNewer( CurrentVersion: CurrentVersion, CompareVersion: Version.Key );
        Assert.AreEqual( Version.Value, result );
      }

    }

    /**************************************************************************/

  }

}
