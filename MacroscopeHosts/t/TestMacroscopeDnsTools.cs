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
  public class TestMacroscopeDnsTools : Macroscope
  {

    /**************************************************************************/

    [Test]
    public void TestCheckValidHostname ()
    {

      SortedDictionary<string,Boolean> TestUrls = new SortedDictionary<string, bool> ();

      TestUrls.Add( "https://nazuke.github.io/SEOMacroscope/", true );
      TestUrls.Add( "https://bogus.bogus.com/some/path/index.html", false );
      TestUrls.Add( "https://www.google.com/", true );
      
      foreach( string Url in TestUrls.Keys )
      {
        Assert.AreEqual(
          TestUrls[ Url ],
          MacroscopeDnsTools.CheckValidHostname( Url: Url ),
          string.Format( "FAIL: {0}", Url )
        );
      }

    }

    /**************************************************************************/

  }

}
