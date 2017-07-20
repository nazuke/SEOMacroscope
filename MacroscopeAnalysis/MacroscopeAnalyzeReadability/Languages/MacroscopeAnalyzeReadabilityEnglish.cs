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
  /// Analyze the readability of the English text in a document.
  /// </summary>

  public class MacroscopeAnalyzeReadabilityEnglish : Macroscope,IMacroscopeAnalyzeReadability
  {

    /**
        
        Readability test:
        
        https://en.wikipedia.org/wiki/SMOG
        http://www.phonicsontheweb.com/syllables.php
        
        http://www.learningandwork.org.uk/resource/readability/
        
    **/
   
    /**************************************************************************/

    private MacroscopeAnalyzeReadability.AnalyzeReadabilityType Type;
    
    private char [] SentenceDelimiters;
    
    private SortedDictionary<double,double> SqrtLookupTable;

    private double Grade;
    
    /**************************************************************************/

    public MacroscopeAnalyzeReadabilityEnglish ()
    {
      
      this.SuppressDebugMsg = true;
      
      this.Type = MacroscopeAnalyzeReadability.AnalyzeReadabilityType.SMOG;
      
      this.SentenceDelimiters = new char[3];
      this.SentenceDelimiters[ 0 ] = '.';
      this.SentenceDelimiters[ 1 ] = '\r';
      this.SentenceDelimiters[ 2 ] = '\n';

      /*
        Value: 1 4 9 16 25 36 49 64 81 100 121 144 169
         SQRT: 1 2 3  4  5  6  7  8  9  10  11  12  13
      */
     
      this.SqrtLookupTable = new SortedDictionary<double,double> ();

      this.SqrtLookupTable.Add( 1, 1 );
      this.SqrtLookupTable.Add( 4, 2 );
      this.SqrtLookupTable.Add( 9, 3 );
      this.SqrtLookupTable.Add( 16, 4 );
      this.SqrtLookupTable.Add( 25, 5 );
      this.SqrtLookupTable.Add( 36, 6 );
      this.SqrtLookupTable.Add( 49, 7 );
      this.SqrtLookupTable.Add( 64, 8 );
      this.SqrtLookupTable.Add( 81, 9 );
      this.SqrtLookupTable.Add( 100, 10 );
      this.SqrtLookupTable.Add( 121, 11 );
      this.SqrtLookupTable.Add( 144, 12 );
      this.SqrtLookupTable.Add( 169, 13 );
     
      this.Grade = 0;
      
    }

    /**************************************************************************/

    public  MacroscopeAnalyzeReadability.AnalyzeReadabilityType GetAnalyzeReadabilityType ()
    {
      return( this.Type );
    }

    /**************************************************************************/
        
    public double AnalyzeReadability ( MacroscopeDocument msDoc )
    {
      
      double SmogGrade = 0;
      string SampleText = msDoc.GetBodyText();

      if( !string.IsNullOrEmpty( SampleText ) )
      {
        SmogGrade = this.AnalyzeReadability( SampleText: SampleText );
      }
      
      this.Grade = SmogGrade;
      
      return( SmogGrade );
      
    }

    /** -------------------------------------------------------------------- **/

    public double AnalyzeReadability ( string SampleText )
    {
      
      double SmogGrade = 0;

      SampleText = Regex.Replace( SampleText, "\\s+", " ", RegexOptions.Singleline );
      SampleText = SampleText.Trim();

      double SentenceCount = this.CountSentences( SampleText: SampleText );

      this.DebugMsg( string.Format( "SentenceCount: \"{0}\"", SentenceCount ) );
      
      
      if( SentenceCount > 30 )
      {

        double SmogGradeCalculated = 0;

        for( int Range = 0 ; Range <= 2 ; Range++ )
        {
          SmogGradeCalculated += this.CalculateSmogGrade( SampleText: SampleText, Range: Range );
        }
        
        SmogGrade = ( SmogGradeCalculated / ( double )3 );

      }
      else
      {
        
        SmogGrade = this.CalculateSmogGrade( SampleText: SampleText, Range: 0 );
        
      }
      
      this.DebugMsg( string.Format( "AnalyzeSmogGrade: \"{0}\"", SmogGrade ) );
            
      this.Grade = SmogGrade;
            
      return( SmogGrade );
            
    }

    /**************************************************************************/

    public double CalculateSmogGrade ( string SampleText, int Range )
    {

      double SmogGrade = 0;
      //double Summed = 0;
      string SentencesText;
      // double Sentences;
      List<KeyValuePair<string,double>> PolySyllables;
      double PolySyllableCount = 0;
      double SquareRoot = 0;
      
      SentencesText = this.SampleSentences( SampleText: SampleText, Range: Range );

      PolySyllables = this.CountPolySyllables( SampleText: SentencesText );

      this.DebugMsg( string.Format( "PolySyllables.Count: \"{0}\"", PolySyllables.Count ) );

      PolySyllableCount = PolySyllables.Count * 3;

      this.DebugMsg( string.Format( "PolySyllableCount: \"{0}\"", PolySyllableCount ) );

      foreach( double BaseValue in this.SqrtLookupTable.Keys )
      {

        this.DebugMsg( string.Format( "BaseValue: {0} => {1}", BaseValue, this.SqrtLookupTable[ BaseValue ] ) );

        if( PolySyllableCount >= BaseValue )
        {
          SquareRoot = this.SqrtLookupTable[ BaseValue ];
        }
        else
        {
          break;
        }

      }

      this.DebugMsg( string.Format( "SquareRoot: \"{0}\"", SquareRoot ) );

      SmogGrade = SquareRoot + 8;

      this.DebugMsg( string.Format( "CalculateSmogGrade: \"{0}\"", SmogGrade ) );

      return( SmogGrade );
      
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

    public string SampleSentences ( string SampleText, int Range )
    {

      string SampledSentencesText = SampleText;
      string [] Sentences;
      List<string> SentenceList = new List<string> ( 10 );
      List<string> SampledList = new List<string> ( 10 );

      Sentences = SampleText.ToLower().Split(
        this.SentenceDelimiters,
        StringSplitOptions.RemoveEmptyEntries
      );

      foreach( string Sentence in Sentences )
      {
        if( !string.IsNullOrEmpty( Sentence ) )
        {
          SentenceList.Add( Sentence );
        }
      }

      if( this.CountSentences( SampleText: SampledSentencesText ) > 30 )
      {

        const int StartBegin = 0;
        int StartMiddle = ( SentenceList.Count / 2 ) - 5;
        int StartEnd = SentenceList.Count - 10;

        switch( Range )
        {
          case 0:
            SampledList.AddRange( SentenceList.GetRange( StartBegin, 10 ) );
            break;
          case 1:
            SampledList.AddRange( SentenceList.GetRange( StartMiddle, 10 ) );
            break;
          case 2:
            SampledList.AddRange( SentenceList.GetRange( StartEnd, 10 ) );
            break;
          default:
            SampledList.AddRange( SentenceList.GetRange( StartMiddle, 10 ) );
            break;
        }
        
        SampledSentencesText = String.Join( ".", SampledList );

      }
      else
      {

        int NumberOfSentences = ( int )this.CountSentences( SampleText: SampledSentencesText );
        
        SampledList.AddRange( SentenceList.GetRange( 0, NumberOfSentences ) );

        SampledSentencesText = String.Join( ".", SampledList );

      }

      this.DebugMsg( string.Format( "SampledList 1: \"{0}\"", SampledList.Count ) );

      return( SampledSentencesText );

    }


    /**************************************************************************/

    public List<KeyValuePair<string,double>> CountPolySyllables ( string SampleText )
    {
      
      List<KeyValuePair<string,double>> PolySyllables = new List<KeyValuePair<string,double>> ( 10 );
      string [] Sentences;

      Sentences = SampleText.ToLower().Split(
        this.SentenceDelimiters,
        StringSplitOptions.RemoveEmptyEntries
      );

      foreach( string Sentence in Sentences )
      {

        //this.DebugMsg( string.Format( "Sentence: \"{0}\"", Sentence ) );

        string [] Words = Regex.Split( Sentence, "[^\\w]+", RegexOptions.Singleline );

        foreach( string Word in Words )
        {
          
          //this.DebugMsg( string.Format( "Word: \"{0}\"", Word ) ); 

          string Subtracted = this.SubtractSilentVowel( WordText: Word );
          //this.DebugMsg( string.Format( "Subtracted: \"{0}\"", Subtracted ) ); 
          
          string Flattened = this.FlattenDipthongs( WordText: Subtracted );
          //this.DebugMsg( string.Format( "Flattened: \"{0}\"", Flattened ) ); 

          double VowelCount = this.CountVowels( WordText: Flattened );
          //this.DebugMsg( string.Format( "VowelCount: \"{0}\"", VowelCount ) ); 

          if( VowelCount > 2 )
          {
            
            //this.DebugMsg( string.Format( "POLYSYLLABLE: \"{0}\" => {1}", Word, VowelCount ) );             
            
            PolySyllables.Add( new KeyValuePair<string, double> ( Word, VowelCount ) );
          }
          else
          {
            //this.DebugMsg( string.Format( "NOT POLYSYLLABLE: \"{0}\" => {1}", Word, VowelCount ) ); 
          }
          
        }
      
      }

      return( PolySyllables );
      
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

        //this.DebugMsg( string.Format( "Sentence: \"{0}\"", Sentence ) );

        string [] Words = Regex.Split( Sentence, "[^\\w]+", RegexOptions.Singleline );

        foreach( string Word in Words )
        {
          
          //this.DebugMsg( string.Format( "Word: \"{0}\"", Word ) ); 

          string Subtracted = this.SubtractSilentVowel( WordText: Word );
          //this.DebugMsg( string.Format( "Subtracted: \"{0}\"", Subtracted ) ); 
          
          string Flattened = this.FlattenDipthongs( WordText: Subtracted );
          //this.DebugMsg( string.Format( "Flattened: \"{0}\"", Flattened ) ); 

          double VowelCount = this.CountVowels( WordText: Flattened );
          //this.DebugMsg( string.Format( "VowelCount: \"{0}\"", VowelCount ) ); 

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
