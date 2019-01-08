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

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeAnalyzePageTitles
  {

    /**************************************************************************/

    const string Sample = "The quick brown fox jumps over the lazy dog!";

    /**************************************************************************/

    [Test]
    public void TestCalcTitleWidthWide ()
    {

      MacroscopeAnalyzePageTitles AnalyzePageTitles = new MacroscopeAnalyzePageTitles ();

      int Width = AnalyzePageTitles.CalcTitleWidth( Sample );

      Assert.Greater( Width, 0, "Width too small", 1 );

    }

    /**************************************************************************/

    [Test]
    public void TestCalcTitleWidthNarrow ()
    {

      MacroscopeAnalyzePageTitles AnalyzePageTitles = new MacroscopeAnalyzePageTitles ();

      int Width = AnalyzePageTitles.CalcTitleWidth( "Bongo" );

      Assert.Greater( Width, 0, "Width too small", 1 );

    }

    /**************************************************************************/

    [Test]
    public void TestCalcTitleWidthMassive ()
    {

      MacroscopeAnalyzePageTitles AnalyzePageTitles = new MacroscopeAnalyzePageTitles ();

      string Massive = "";

      for( int i = 1 ; i <= 100 ; i++ )
      {
        Massive += Sample;
      }

      int Width = AnalyzePageTitles.CalcTitleWidth( Massive );

      Assert.Greater( Width, 0, "Width too small", 1 );

    }

    /**************************************************************************/

    [Test]
    public void TestCalcTitleWidthZero ()
    {

      MacroscopeAnalyzePageTitles AnalyzePageTitles = new MacroscopeAnalyzePageTitles ();

      int Width = AnalyzePageTitles.CalcTitleWidth( "" );

      Assert.AreEqual( Width, 0, "Width not equal to zero", 1 );

    }

    /**************************************************************************/

  }

}
