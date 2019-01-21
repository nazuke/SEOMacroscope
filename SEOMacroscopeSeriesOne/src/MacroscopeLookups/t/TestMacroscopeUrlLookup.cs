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

    int MaxUrls = 1000;
    int MaxLoops = 100;
    private List<string> Urls;
    List<string> DistinctUrls;
    Dictionary<string, bool> RandomizedUrls;

    /**************************************************************************/

    public TestMacroscopeUrlLookup ()
    {

      MacroscopeUrlLookup.Clear();

      Faker fake = new Faker();
      this.Urls = new List<string>();
      this.RandomizedUrls = new Dictionary<string, bool>();

      for( int i = 0 ; i < this.MaxUrls ; i++ )
      {
        Urls.Add( fake.Internet.UrlWithPath() );
      }

      this.DistinctUrls = Urls.Distinct().ToList();

      foreach( string Url in this.DistinctUrls )
      {
        this.RandomizedUrls.Add( Url, true );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestUrlLookup ()
    {

      foreach( string Url in this.DistinctUrls )
      {
        MacroscopeUrlLookup.Lookup( Url: Url );
      }

      foreach( string Url in this.RandomizedUrls.Keys )
      {

        ulong value = MacroscopeUrlLookup.Lookup( Url: Url );
        ulong found = 0;
        bool not_found = true;

        for( int k = 0 ; k < this.DistinctUrls.Count() ; k++ )
        {
          if( this.DistinctUrls[ k ] == Url )
          {
            found = (ulong) k;
            not_found = false;
            break;
          }
          else
          {
            not_found = true;
          }
        }

        Assert.IsFalse( not_found );
        Assert.AreEqual( value, found );

      }

    }

    /**************************************************************************/

    [Test]
    public void TestUrlLookupMultiple ()
    {

      for( int repeat = 0 ; repeat < this.MaxLoops ; repeat++ )
      {

        foreach( string Url in this.DistinctUrls )
        {
          MacroscopeUrlLookup.Lookup( Url: Url );
        }

        foreach( string Url in this.RandomizedUrls.Keys )
        {

          ulong value = MacroscopeUrlLookup.Lookup( Url: Url );
          ulong found = 0;
          bool not_found = true;

          for( int k = 0 ; k < this.DistinctUrls.Count() ; k++ )
          {
            if( this.DistinctUrls[ k ] == Url )
            {
              found = (ulong) k;
              not_found = false;
              break;
            }
            else
            {
              not_found = true;
            }
          }

          Assert.IsFalse( not_found );
          Assert.AreEqual( value, found );

        }

      }

    }

    /**************************************************************************/

    [Test]
    public void TestUrlLookupWithClear ()
    {

      for( int repeat = 0 ; repeat < this.MaxLoops ; repeat++ )
      {

        MacroscopeUrlLookup.Clear();

        foreach( string Url in this.DistinctUrls )
        {
          MacroscopeUrlLookup.Lookup( Url: Url );
        }

        foreach( string Url in this.RandomizedUrls.Keys )
        {

          ulong value = MacroscopeUrlLookup.Lookup( Url: Url );
          ulong found = 0;
          bool not_found = true;

          for( int k = 0 ; k < this.DistinctUrls.Count() ; k++ )
          {
            if( this.DistinctUrls[ k ] == Url )
            {
              found = (ulong) k;
              not_found = false;
              break;
            }
            else
            {
              not_found = true;
            }
          }

          Assert.IsFalse( not_found );
          Assert.AreEqual( value, found );

        }

      }

    }

    /**************************************************************************/

  }

}
