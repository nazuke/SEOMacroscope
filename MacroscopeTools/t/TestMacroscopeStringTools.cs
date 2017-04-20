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
  public class TestMacroscopeStringTools
  {

    /**************************************************************************/

    [Test]
    public void TestCleanText ()
    {

      Dictionary<string,string> StringsTable = new Dictionary<string,string> () {
        {
          "!\"#$%&'()=~|-^\\/,.<>;:@`{}[]*+The quick brown fox jumps over the lazy dog's!\"#$%&'()=~|-^\\/,.<>;:@`{}[]*+",
          "The quick brown fox jumps over the lazy dog's"
        },
        {
          "The quick/slow, and the fast-stop and the second-best things we don't know about.",
          "The quick/slow and the fast-stop and the second-best things we don't know about"
        },
        {
          "The markets opened at $100.00 today.",
          "The markets opened at $100.00 today"
        }
      };

      foreach( string StringKey in StringsTable.Keys )
      {

        string Cleaned = MacroscopeStringTools.CleanText( Text: StringKey );

        Assert.AreEqual( StringsTable[ StringKey ], Cleaned, string.Format( "NOT VALID: {0}", Cleaned ) );

      }

    }

    /**************************************************************************/

  }

}
