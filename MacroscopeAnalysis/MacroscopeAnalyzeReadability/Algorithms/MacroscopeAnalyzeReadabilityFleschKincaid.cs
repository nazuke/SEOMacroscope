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
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Analyze the readability of the English text in a document with the Flesch-Kincaid method.
  /// </summary>

  public class MacroscopeAnalyzeReadabilityFleschKincaid : Macroscope,IMacroscopeAnalyzeReadability
  {

    /**
        
        Flesch-Kincaid:

        http://www.mang.canterbury.ac.nz/writing_guide/writing/flesch.shtml
        
        http://www.phonicsontheweb.com/syllables.php

     **/
   
    /**************************************************************************/

    private const double FKAverageSentenceLengthConstant = 1.015;
    private const double FKAverageWordLengthConstant = 84.6;

    private static List<double> WordsPerSentenceLookupTable;
    private static List<double> SyllablesPerWordLookupTable;

    private MacroscopeAnalyzeReadability.AnalyzeReadabilityType Type;

    private char [] SentenceDelimiters;

    private double Grade;

    /**************************************************************************/

    static MacroscopeAnalyzeReadabilityFleschKincaid ()
    {

      WordsPerSentenceLookupTable = new List<double> ( 101 );
      SyllablesPerWordLookupTable = new List<double> ( 101 );

      double WordsPerSentence = 50;

      for( double i = 0 ; i <= 100 ; i++ )
      {

        double Computed = ( ( WordsPerSentence / 100 ) * i ) - 10;

        WordsPerSentenceLookupTable.Add( Computed );
        WordsPerSentence--;

      }

    }
    
    /**************************************************************************/

    public MacroscopeAnalyzeReadabilityFleschKincaid ()
    {
      
      this.SuppressDebugMsg = false;
      
      this.Type = MacroscopeAnalyzeReadability.AnalyzeReadabilityType.FLESCH_KINCAID;
      
      this.SentenceDelimiters = new char[1];
      this.SentenceDelimiters[ 0 ] = '.';

      this.Grade = 0;

      foreach( double WordsPerSentenceLookupValue in WordsPerSentenceLookupTable )
      {
        this.DebugMsg( string.Format( "WordsPerSentenceLookupValue: \"{0}\"", WordsPerSentenceLookupValue ) );
      }
      
      
      return;
      
    }

    /**************************************************************************/

    public  MacroscopeAnalyzeReadability.AnalyzeReadabilityType GetAnalyzeReadabilityType ()
    {
      return( this.Type );
    }

    /**************************************************************************/
        
    public double AnalyzeReadability ( MacroscopeDocument msDoc )
    {
      
      double ReadabilityGrade = 0;
      string SampleText = msDoc.GetBodyText();

      if( !string.IsNullOrEmpty( SampleText ) )
      {
        ReadabilityGrade = this.AnalyzeReadability( SampleText: SampleText );
      }
      
      this.Grade = ReadabilityGrade;

      return( ReadabilityGrade );
      
    }

    /** -------------------------------------------------------------------- **/

    public double AnalyzeReadability ( string SampleText )
    {
      
      double ReadabilityGrade = 0;
      double WordCount;
      double SyllableCount;
      double SentenceCount;
      double AverageSyllablesPerWord;
      double AverageWordsPerSentence;

      SampleText = Regex.Replace( SampleText, @"\s+", " ", RegexOptions.Singleline );
      SampleText = SampleText.Trim();

      WordCount = this.CountWords( SampleText: SampleText );
      this.DebugMsg( string.Format( "WordCount: \"{0}\"", WordCount ) );

      SyllableCount = this.CountSyllables( SampleText: SampleText );
      this.DebugMsg( string.Format( "SyllableCount: \"{0}\"", SyllableCount ) );

      SentenceCount = this.CountSentences( SampleText: SampleText );
      this.DebugMsg( string.Format( "SentenceCount: \"{0}\"", SentenceCount ) );

      AverageSyllablesPerWord = SyllableCount / WordCount;
      this.DebugMsg( string.Format( "AverageSyllablesPerWord: \"{0}\"", AverageSyllablesPerWord ) );

      AverageWordsPerSentence = WordCount / SentenceCount;
      this.DebugMsg( string.Format( "AverageWordsPerSentence: \"{0}\"", AverageWordsPerSentence ) );
      
      
      
      
      
      

      
      
      
      
      
      
      

      this.Grade = ReadabilityGrade;
            
      return( ReadabilityGrade );
            
    }

    /**************************************************************************/

    public double CountWords ( string SampleText )
    {

      double Count = 0;
      string [] Words;

      Words = Regex.Split(
        SampleText.ToLower(),
        @"([\s\p{Ps}\p{Pe}]+|[\p{P}]+(?![\w]))",
        RegexOptions.Singleline
      );

      foreach( string Word in Words )
      {
        if( !string.IsNullOrEmpty( Word ) )
        {
          string StrippedWord = Regex.Replace( Word, @"[\p{P}]+", "", RegexOptions.Singleline );
          if( !string.IsNullOrWhiteSpace( StrippedWord ) )
          {
            Count++;
          }
        }
      }

      return( Count );

    }

    /**************************************************************************/
    
    public double CountSentences ( string SampleText )
    {
      
      double Count = 0;
      string [] Sentences;

      Sentences = SampleText.ToLower().Split(
        this.SentenceDelimiters,
        StringSplitOptions.RemoveEmptyEntries
      );

      foreach( string Sentence in Sentences )
      {

        if( !string.IsNullOrEmpty( Sentence ) )
        {
          Count++;
        }

      }

      return( Count );

    }

    /**************************************************************************/

    public double CountSyllables ( string SampleText )
    {
      
      double Syllables = 0;
      string [] Sentences;

      Sentences = SampleText.ToLower().Split(
        this.SentenceDelimiters,
        StringSplitOptions.RemoveEmptyEntries
      );

      foreach( string Sentence in Sentences )
      {

        string [] Words = Regex.Split( Sentence, @"[^\w]+", RegexOptions.Singleline );

        foreach( string Word in Words )
        {
          
          string Subtracted = this.SubtractSilentVowel( WordText: Word );
          
          string Flattened = this.FlattenDipthongs( WordText: Subtracted );

          double VowelCount = this.CountVowels( WordText: Flattened );

          Syllables += VowelCount;
          
        }
      
      }

      return( Syllables );
      
    }

    /**************************************************************************/

    public string SubtractSilentVowel ( string WordText )
    {

      string Subtracted = WordText;
      
      if( !string.IsNullOrEmpty( Subtracted ) )
      {

        if( Regex.IsMatch( Subtracted, "[^aiueoy][e]$", RegexOptions.Singleline ) )
        {

          if( this.CountVowels( WordText: Subtracted ) > 1 )
          {
            Subtracted = Subtracted.Remove( Subtracted.Length - 1, 1 );
          }
          
        }

      }

      return( Subtracted );
      
    }

    /**************************************************************************/

    public string FlattenDipthongs ( string WordText )
    {
      
      string Flattened = WordText;

      if( !string.IsNullOrEmpty( WordText ) )
      {

        Flattened = Regex.Replace(
          input: Flattened,
          pattern: "([aiueoy])[aiueoy]", 
          replacement: "${1}",
          options: RegexOptions.Singleline
        );

      }
      
      return( Flattened );
      
    }
    
    /**************************************************************************/

    public double CountVowels ( string WordText )
    {
      
      double Count = 0;

      string [] Vowels = Regex.Split( WordText, "[^aiueoy]", RegexOptions.Singleline );

      foreach( string Letter in Vowels )
      {

        if( !string.IsNullOrEmpty( Letter ) )
        {
          Count++;
        }

      }

      return( Count );
      
    }

    /**************************************************************************/
    
    public string GradeToString ()
    {
      
      string GradeString = "SIMPLE";
      
      if( ( this.Grade >= 0 ) && ( this.Grade <= 9 ) )
      {
        GradeString = "SIMPLE";
      }
      else
      if( ( this.Grade >= 10 ) && ( this.Grade <= 12 ) )
      {
        GradeString = "LOW COMPLEXITY"; 
      }
      else
      if( ( this.Grade >= 13 ) && ( this.Grade <= 16 ) )
      {
        GradeString = "MEDIUM COMPLEXITY"; 
      }
      else
      if( ( this.Grade >= 17 ) && ( this.Grade <= 21 ) )
      {
        GradeString = "HIGH COMPLEXITY"; 
      }
      else
      {
        GradeString = "N/A";
      }
      
      return( GradeString );
      
    }
    
    /**************************************************************************/

  }

}
