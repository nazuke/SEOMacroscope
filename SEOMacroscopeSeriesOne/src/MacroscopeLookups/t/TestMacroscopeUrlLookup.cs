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
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using NUnit.Framework;
using Bogus;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeUrlLookup : Macroscope
  {

    /**************************************************************************/

    public TestMacroscopeUrlLookup ()
    {
    }

    /**************************************************************************/

    [Test]
    public void TestUrlLookup ()
    {

      Faker fake = new Faker();
      int max = 10000;
      List<string> Urls = new List<string>( max );
      List<string> DistinctUrls = new List<string>( max );
      Dictionary<string, bool> RandomizedUrls = new Dictionary<string, bool>( max );

      for( int i = 0 ; i < max ; i++ )
      {
        Urls.Add( fake.Internet.UrlWithPath() );
      }

      DistinctUrls = Urls.Distinct().ToList();

      foreach( string Url in DistinctUrls )
      {
        RandomizedUrls.Add( Url, true );
      }

      for( int i = 0 ; i < DistinctUrls.Count() ; i++ )
      {
        string Url = DistinctUrls[ i ];
        ulong value = MacroscopeUrlLookup.Lookup( Url: Url );
      }

      foreach( string Url in RandomizedUrls.Keys )
      {

        ulong value = MacroscopeUrlLookup.Lookup( Url: Url );
        ulong found = 0;

        for( int i = 0 ; i < DistinctUrls.Count() ; i++ )
        {
          if( DistinctUrls[ i ] == Url )
          {
            found = (ulong) i + 1;
            break;
          }
        }

        Assert.AreEqual( value, found );

      }

    }

    /**************************************************************************/

  }

}
