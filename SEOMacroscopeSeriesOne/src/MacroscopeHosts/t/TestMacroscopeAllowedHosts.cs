/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

  The GitHub repository may be found at:

    https://github.com/nazuke/SEOMacroscope

  SEOMacroscope is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  SEOMacroscope is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeAllowedHosts : Macroscope
  {

    /**************************************************************************/

    [Test]
    public void TestCount ()
    {

      MacroscopeAllowedHosts AllowedHosts = new MacroscopeAllowedHosts ();
      List<string> TestUrls = new List<string> ();

      TestUrls.Add( "https://nazuke.github.io/SEOMacroscope/" );
      TestUrls.Add( "https://bogus.bogus.com/some/path/index.html" );
      TestUrls.Add( "https://www.google.com/" );
      
      foreach( string Url in TestUrls )
      {
        AllowedHosts.AddFromUrl( Url: Url );
      }

      Assert.AreEqual(
        TestUrls.Count,
        AllowedHosts.Count(),
        string.Format( "FAIL: {0} :: {1}", TestUrls.Count, AllowedHosts.Count() )
      );

    }

    /**************************************************************************/

    [Test]
    public void TestRemoveFromUrl ()
    {

      MacroscopeAllowedHosts AllowedHosts = new MacroscopeAllowedHosts ();
      List<string> TestUrls = new List<string> ();

      TestUrls.Add( "https://nazuke.github.io/SEOMacroscope/" );
      TestUrls.Add( "https://bogus.bogus.com/some/path/index.html" );
      TestUrls.Add( "https://www.google.com/" );
      
      foreach( string Url in TestUrls )
      {
        AllowedHosts.AddFromUrl( Url: Url );
      }

      Assert.AreEqual(
        TestUrls.Count,
        AllowedHosts.Count(),
        string.Format( "FAIL: {0} :: {1}", TestUrls.Count, AllowedHosts.Count() )
      );

      this.DebugMsg( TestUrls[ 1 ] );
      this.DebugMsg( AllowedHosts.Count().ToString() );
      
      AllowedHosts.RemoveFromUrl( Url: TestUrls[ 1 ] );

      this.DebugMsg( AllowedHosts.Count().ToString() );

            
      Assert.AreEqual(
        TestUrls.Count - 1,
        AllowedHosts.Count(),
        string.Format( "FAIL: {0} :: {1}", TestUrls.Count - 1, AllowedHosts.Count() )
      );
      
    }

    /**************************************************************************/

  }

}
