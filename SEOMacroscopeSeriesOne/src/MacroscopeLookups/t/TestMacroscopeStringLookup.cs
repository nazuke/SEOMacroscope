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
  public class TestMacroscopeStringLookup : Macroscope
  {

    /**************************************************************************/

    public TestMacroscopeStringLookup ()
    {
    }

    /**************************************************************************/

    [Test]
    public void TestStringLookup ()
    {

      Faker fake = new Faker();
      int max = 10000;
      List<string> Texts = new List<string>( max );
      List<string> DistinctTexts = new List<string>( max );
      Dictionary<string, bool> RandomizedTexts = new Dictionary<string, bool>( max );

      for( int i = 0 ; i < max ; i++ )
      {
        Texts.Add( fake.Lorem.Sentence() );
      }

      DistinctTexts = Texts.Distinct().ToList();

      foreach( string Text in DistinctTexts )
      {
        RandomizedTexts.Add( Text, true );
      }

      for( int i = 0 ; i < DistinctTexts.Count() ; i++ )
      {
        string Text = DistinctTexts[ i ];
        ulong value = MacroscopeStringLookup.Lookup( Text: Text );
      }

      foreach( string Text in RandomizedTexts.Keys )
      {

        ulong value = MacroscopeStringLookup.Lookup( Text: Text );
        ulong found = 0;

        for( int i = 0 ; i < DistinctTexts.Count() ; i++ )
        {
          if( DistinctTexts[ i ] == Text )
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
