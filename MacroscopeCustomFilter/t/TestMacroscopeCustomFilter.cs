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
using System.Diagnostics;
using System.Collections.Generic;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeCustomFilter
  {

    /**************************************************************************/

    [Test]
    public void TestContainsText ()
    {

      MacroscopeCustomFilter CustomFilter = new MacroscopeCustomFilter ( Size: 5 );

      List<string> Texts = new List<string> ();
      List<string> ContainsTexts = new List<string> ();
            
      //Texts.Add( "Der schnelle braune Fuchs springt über den faulen Hund." );
      Texts.Add( "The quick brown fox jumps over the lazy dog." );
      //Texts.Add( "El zorro marrón rápido salta sobre el perro perezoso." );
      //Texts.Add( "Le renard brun rapide saute sur le chien paresseux." );
      //Texts.Add( "La volpe marrone veloce salta sul cane pigro." );
      //Texts.Add( "色は匂へど散りぬるを我が世誰ぞ常ならん有為の奥山今日越えて浅き夢見じ酔ひもせず。" );
      //Texts.Add( "A rápida raposa marrom salta sobre o cão preguiçoso." );
      //Texts.Add( "Den snabba brunräven hoppar över den lata hunden." );
      //Texts.Add( "敏捷的棕色狐狸跳过了懒狗。" );
      //Texts.Add( "敏捷的棕色狐狸跳過了懶狗。" );

      ContainsTexts.Add( "fox" );

      CustomFilter.AddPattern( 0, "The", MacroscopeConstants.Contains.CONTAINS );
      CustomFilter.AddPattern( 1, "over", MacroscopeConstants.Contains.CONTAINS );
      CustomFilter.AddPattern( 2, "fox", MacroscopeConstants.Contains.CONTAINS );
      CustomFilter.AddPattern( 3, "dog", MacroscopeConstants.Contains.CONTAINS );
      CustomFilter.AddPattern( 4, "brown", MacroscopeConstants.Contains.CONTAINS );

      foreach( string ContainsText in ContainsTexts )
      {

        Dictionary<string, MacroscopeConstants.TextPresence> Analyzed = CustomFilter.AnalyzeText( Text: ContainsText );

        Assert.IsNotNull( Analyzed );
        
        foreach( string AnalyzedKey in Analyzed.Keys )
        {

          Assert.AreEqual(
            Analyzed[ AnalyzedKey ],
            MacroscopeConstants.TextPresence.CONTAINS
          );
        
        }
        
      }

    }

    /**************************************************************************/

  }

}
