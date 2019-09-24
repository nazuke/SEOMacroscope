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

    int MaxTexts = 1000;
    int MaxLoops = 100;
    private List<string> Texts;
    List<string> DistinctTexts;
    Dictionary<string, bool> RandomizedTexts;

    /**************************************************************************/

    public TestMacroscopeStringLookup ()
    {

      MacroscopeStringLookup.Clear();

      Faker fake = new Faker();
      this.Texts = new List<string>();
      this.RandomizedTexts = new Dictionary<string, bool>();

      for( int i = 0 ; i < this.MaxTexts ; i++ )
      {
        Texts.Add( fake.Lorem.Sentence() );
      }

      this.DistinctTexts = Texts.Distinct().ToList();

      foreach( string Text in this.DistinctTexts )
      {
        this.RandomizedTexts.Add( Text, true );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestStringLookup ()
    {

      foreach( string Text in this.DistinctTexts )
      {
        MacroscopeStringLookup.Lookup( Text: Text );
      }

      foreach( string Text in this.RandomizedTexts.Keys )
      {

        ulong value = MacroscopeStringLookup.Lookup( Text: Text );
        ulong found = 0;
        bool not_found = true;

        for( int k = 0 ; k < this.DistinctTexts.Count() ; k++ )
        {
          if( this.DistinctTexts[ k ] == Text )
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
    public void TestStringLookupMultiple ()
    {

      for( int repeat = 0 ; repeat < this.MaxLoops ; repeat++ )
      {

        foreach( string Text in this.DistinctTexts )
        {
          MacroscopeStringLookup.Lookup( Text: Text );
        }

        foreach( string Text in this.RandomizedTexts.Keys )
        {

          ulong value = MacroscopeStringLookup.Lookup( Text: Text );
          ulong found = 0;
          bool not_found = true;

          for( int k = 0 ; k < this.DistinctTexts.Count() ; k++ )
          {
            if( this.DistinctTexts[ k ] == Text )
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
    public void TestStringLookupWithClear ()
    {

      for( int repeat = 0 ; repeat < this.MaxLoops ; repeat++ )
      {

        MacroscopeStringLookup.Clear();

        foreach( string Text in this.DistinctTexts )
        {
          MacroscopeStringLookup.Lookup( Text: Text );
        }

        foreach( string Text in this.RandomizedTexts.Keys )
        {

          ulong value = MacroscopeStringLookup.Lookup( Text: Text );
          ulong found = 0;
          bool not_found = true;

          for( int k = 0 ; k < this.DistinctTexts.Count() ; k++ )
          {
            if( this.DistinctTexts[ k ] == Text )
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
