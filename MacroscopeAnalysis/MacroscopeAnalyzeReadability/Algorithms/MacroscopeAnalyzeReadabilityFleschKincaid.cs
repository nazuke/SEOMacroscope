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

    private static char [] SentenceDelimiters;

    private const int FKLookupTableSize = 130;
    private static List<double> WordsPerSentenceLookupTable;
    private static List<double> SyllablesPerWordLookupTable;

    private MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod Type;

    private double Grade;

    /**************************************************************************/

    static MacroscopeAnalyzeReadabilityFleschKincaid ()
    {

      {
        SentenceDelimiters = new char[3];
        SentenceDelimiters[ 0 ] = '.';
        SentenceDelimiters[ 1 ] = '?';
        SentenceDelimiters[ 2 ] = '!';
      }
      
      {
        WordsPerSentenceLookupTable = new List<double> ( FKLookupTableSize + 1 );
        for( double i = 0 ; i <= FKLookupTableSize ; i++ )
        {
          double Computed = ( ( ( double )65 / ( double )FKLookupTableSize ) * i ) - ( double )25;
          WordsPerSentenceLookupTable.Add( Computed );
        }
        WordsPerSentenceLookupTable.Reverse();
      }

      {
        SyllablesPerWordLookupTable = new List<double> ( FKLookupTableSize + 1 );
        for( double i = 0 ; i <= FKLookupTableSize ; i++ )
        {
          double Computed = ( ( ( double )0.6 / ( double )FKLookupTableSize ) * i ) + ( double )1.2;
          SyllablesPerWordLookupTable.Add( Computed );
        }
        SyllablesPerWordLookupTable.Reverse();
      }

    }
    
    /**************************************************************************/

    public MacroscopeAnalyzeReadabilityFleschKincaid ()
    {
      
      this.SuppressDebugMsg = true;
      
      this.Type = MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod.FLESCH_KINCAID;

      this.Grade = 0;

    }

    /**************************************************************************/

    public MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod GetAnalyzeReadabilityMethod ()
    {
      return( this.Type );
    }

    /**************************************************************************/
        
    public double AnalyzeReadability ( MacroscopeDocument msDoc )
    {
      
      double ReadabilityGrade = 0;
      string SampleText = msDoc.GetBodyTextRaw();

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
      
      double WordCount;
      double SyllableCount;
      double SentenceCount;
      double AverageSyllablesPerWord;
      double AverageWordsPerSentence;

      int WordsPerSentenceScore = 0;
      int SyllablesPerWordScore = 0;
      double ReadabilityScoreAngle = 0;
      
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

      WordsPerSentenceScore = this.LookupWordsPerSentenceScore( AverageWordsPerSentence: AverageWordsPerSentence );
      this.DebugMsg( string.Format( "WordsPerSentenceScore: \"{0}\"", WordsPerSentenceScore ) );

      SyllablesPerWordScore = this.LookupSyllablesPerWordScore( AverageSyllablesPerWord: AverageSyllablesPerWord );
      this.DebugMsg( string.Format( "SyllablesPerWordScore: \"{0}\"", SyllablesPerWordScore ) );

      ReadabilityScoreAngle = this.LookupReadabilityScoreAngle(
        WordsPerSentenceScore: WordsPerSentenceScore,
        SyllablesPerWordScore: SyllablesPerWordScore
      );
      this.DebugMsg( string.Format( "ReadabilityScoreAngle: \"{0}\"", ReadabilityScoreAngle ) );

      this.Grade = ReadabilityScoreAngle;
            
      return( this.Grade );
            
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
        SentenceDelimiters,
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
        SentenceDelimiters,
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

    public int LookupWordsPerSentenceScore ( double AverageWordsPerSentence )
    {
    
      int WordsPerSentenceScore = 0;

      for( int i = 0 ; i <= FKLookupTableSize ; i++ )
      {

        if( AverageWordsPerSentence <= WordsPerSentenceLookupTable[ i ] )
        {
          WordsPerSentenceScore = i;
        }
        else
        {
          break;
        }
      }

      return( WordsPerSentenceScore );

    }

    /**************************************************************************/

    public int LookupSyllablesPerWordScore ( double AverageSyllablesPerWord )
    {
    
      int SyllablesPerWordScore = 0;

      for( int i = 0 ; i <= FKLookupTableSize ; i++ )
      {

        if( AverageSyllablesPerWord <= SyllablesPerWordLookupTable[ i ] )
        {
          SyllablesPerWordScore = i;
        }
        else
        {
          break;
        }
      }

      return( SyllablesPerWordScore );

    }

    /**************************************************************************/

    public double LookupReadabilityScoreAngle (
      double WordsPerSentenceScore,
      double SyllablesPerWordScore )
    {
              
      double ReadabilityScoreAngle = 0;
      double Larger = 0;
      double Smaller = 0;
      double Difference = 0;
      
      if( WordsPerSentenceScore > SyllablesPerWordScore )
      {
        Larger = WordsPerSentenceScore;
        Smaller = SyllablesPerWordScore;
      }
      else
      {
        Larger = SyllablesPerWordScore;
        Smaller = WordsPerSentenceScore;
      }

      Difference = ( Larger - Smaller ) / ( double )2;

      ReadabilityScoreAngle = Larger - Difference;

      return( ReadabilityScoreAngle );

    }

    /**************************************************************************/

    public string GradeToString ()
    {
      
      string GradeString = "SIMPLE";

      if( this.Grade >= 90 )
      {
        GradeString = "VERY EASY";
      }
      else
      if( this.Grade >= 80 )
      {
        GradeString = "EASY";
      }
      else
      if( this.Grade >= 70 )
      {
        GradeString = "FAIRLY EASY";
      }
      else
      if( this.Grade >= 60 )
      {
        GradeString = "PLAIN ENGLISH";
      }
      else
      if( this.Grade >= 50 )
      {
        GradeString = "FAIRLY DIFFICULT";
      }
      else
      if( this.Grade >= 40 )
      {
        GradeString = "DIFFICULT";
      }
      else
      if( this.Grade >= 30 )
      {
        GradeString = "DIFFICULT";
      }
      else
      if( this.Grade >= 20 )
      {
        GradeString = "DIFFICULT";
      }
      else
      if( this.Grade >= 10 )
      {
        GradeString = "VERY DIFFICULT";
      }
      else
      if( this.Grade >= 0 )
      {
        GradeString = "VERY DIFFICULT";
      }
      else
      {
        GradeString = "INCOMPREHENSIBLE";
      }

      return( GradeString );
      
    }
    
    /**************************************************************************/

  }

}
