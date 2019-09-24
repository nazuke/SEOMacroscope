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
  public class TestMacroscopeAnalyzeTextLanguage : Macroscope
  {

    /**************************************************************************/

    [Test]
    public void TestCorrectAnalyzeLanguage ()
    {

      Dictionary<string,string> Texts = new Dictionary<string, string> ();

      Texts.Add( "de", "Der schnelle braune Fuchs springt über den faulen Hund." );
      Texts.Add( "en", "The quick brown fox jumps over the lazy dog." );
      Texts.Add( "es", "El zorro marrón rápido salta sobre el perro perezoso." );
      Texts.Add( "fr", "Le renard brun rapide saute sur le chien paresseux." );
      Texts.Add( "it", "La volpe marrone veloce salta sul cane pigro." );
      Texts.Add( "ja", "色は匂へど散りぬるを我が世誰ぞ常ならん有為の奥山今日越えて浅き夢見じ酔ひもせず。" );
      Texts.Add( "pt", "A rápida raposa marrom salta sobre o cão preguiçoso." );
      Texts.Add( "sv", "Den snabba brunräven hoppar över den lata hunden." );
      Texts.Add( "zh-cn", "敏捷的棕色狐狸跳过了懒狗。" );
      Texts.Add( "zh-tw", "敏捷的棕色狐狸跳過了懶狗。" );

      MacroscopeAnalyzeTextLanguage AnalyzeTextLanguage;
      
      foreach( string LanguageCode in Texts.Keys )
      {

        AnalyzeTextLanguage = new MacroscopeAnalyzeTextLanguage ( IsoLanguageCode: LanguageCode );

        string ProbableLanguage = AnalyzeTextLanguage.AnalyzeLanguage( Text: Texts[ LanguageCode ] );

        Assert.AreEqual(
          LanguageCode,
          ProbableLanguage,
          string.Format(
            "Wrong language detected for: {0} :: {1} :: {2}",
            Texts[ LanguageCode ],
            ProbableLanguage,
            LanguageCode
          )
        );

      }

    }
    
    /**************************************************************************/
    
    [Test]
    public void TestWrongAnalyzeLanguage ()
    {

      Dictionary<string,string> Texts = new Dictionary<string, string> ();

      Texts.Add( "zh-tw", "Der schnelle braune Fuchs springt über den faulen Hund." );
      Texts.Add( "zh-cn", "The quick brown fox jumps over the lazy dog." );
      Texts.Add( "en", "El zorro marrón rápido salta sobre el perro perezoso." );
      Texts.Add( "ja", "Le renard brun rapide saute sur le chien paresseux." );
      Texts.Add( "de", "La volpe marrone veloce salta sul cane pigro." );
      Texts.Add( "es", "色は匂へど散りぬるを我が世誰ぞ常ならん有為の奥山今日越えて浅き夢見じ酔ひもせず。" );
      Texts.Add( "fr", "A rápida raposa marrom salta sobre o cão preguiçoso." );
      Texts.Add( "it", "Den snabba brunräven hoppar över den lata hunden." );
      Texts.Add( "pt", "敏捷的棕色狐狸跳过了懒狗。" );
      Texts.Add( "sv", "敏捷的棕色狐狸跳過了懶狗。" );

      MacroscopeAnalyzeTextLanguage AnalyzeTextLanguage;

      foreach( string LanguageCode in Texts.Keys )
      {

        AnalyzeTextLanguage = new MacroscopeAnalyzeTextLanguage ();

        string ProbableLanguage = AnalyzeTextLanguage.AnalyzeLanguage( Text: Texts[ LanguageCode ] );

        Assert.AreNotEqual(
          LanguageCode,
          ProbableLanguage,
          string.Format(
            "Wrong language detected for: {0} :: {1} :: {2}",
            ProbableLanguage,
            LanguageCode,
            Texts[ LanguageCode ]
          )
        );

      }

    }
    
    /**************************************************************************/

    [Test]
    public void TestDetectJapaneseLanguage ()
    {

      List<KeyValuePair<string,string>> Texts = new List<KeyValuePair<string,string>> ();

      Texts.Add( new KeyValuePair<string, string> ( "ja", "色は匂へど散りぬるを我が世誰ぞ常ならん有為の奥山今日越えて浅き夢見じ酔ひもせず。" ) );
      Texts.Add( new KeyValuePair<string, string> ( "ja", "クイックブラウンキツネは怠惰な犬の上を飛ぶ。" ) );
      Texts.Add( new KeyValuePair<string, string> ( "ja", "君は日本の食べ物を好きか" ) );

      MacroscopeAnalyzeTextLanguage AnalyzeTextLanguage;
      
      foreach( KeyValuePair<string, string> Item in Texts )
      {

        string LanguageCode = Item.Key;
        string LanguageText = Item.Value;
        
        AnalyzeTextLanguage = new MacroscopeAnalyzeTextLanguage ( IsoLanguageCode: LanguageCode );

        string ProbableLanguage = AnalyzeTextLanguage.AnalyzeLanguage( Text: LanguageText );

        Assert.AreEqual(
          LanguageCode,
          ProbableLanguage,
          string.Format(
            "Wrong language detected for: {0} :: {1} :: {2}",
            LanguageText,
            ProbableLanguage,
            LanguageCode
          )
        );

      }

    }
    
    /**************************************************************************/
        
  }
  
}
