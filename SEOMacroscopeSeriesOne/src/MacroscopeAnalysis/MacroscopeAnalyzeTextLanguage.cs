/*

  This file is part of SEOMacroscope.

  Copyright 2018 Jason Holland.

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
using System.Linq;
using LanguageDetection;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeAnalyzeTextLanguage.
  /// </summary>

  public class MacroscopeAnalyzeTextLanguage : Macroscope
  {

    /*
      
      http://unicode.org/iso15924/iso15924-codes.html

    */
    
    /**************************************************************************/

    LanguageDetector DetectLanguage;

    /**************************************************************************/

    public MacroscopeAnalyzeTextLanguage ()
    {

      this.SuppressDebugMsg = true;

      this.DetectLanguage = new LanguageDetector ();

      this.DetectLanguage.AddLanguages(
        "en", // English        
        "es", // Spanish
        "de", // German
        "fr", // French
        "it", // Italian
        "ja", // Japanese        
        "no", // Norwegian
        "pt", // Portuguese
        "sv", // Swedish
        "zh-cn", // Chinese Simplified
        "zh-tw" // Chinese Traditional
      );
     
    }

    /** -------------------------------------------------------------------- **/
    
    public MacroscopeAnalyzeTextLanguage ( string IsoLanguageCode )
    {

      this.SuppressDebugMsg = true;

      this.DetectLanguage = new LanguageDetector ();
      
      this.DetectLanguage.RandomSeed = 666;
      this.DetectLanguage.ProbabilityThreshold = ( double )0.5;
      this.DetectLanguage.MaxTextLength = 1024 * 8;

      if( string.IsNullOrEmpty( IsoLanguageCode ) )
      {

        this.DetectLanguage.AddAllLanguages();

      }
      else
      {

        if( IsoLanguageCode.ToLower().Equals( "x-default" ) )
        {

          this.DetectLanguage.AddAllLanguages();

        }
        else
        {

          this.DetectLanguage.AddLanguages( "en" );

          if( !IsoLanguageCode.ToLower().Equals( "en" ) )
          {
            
            try
            {
              this.DetectLanguage.AddLanguages( IsoLanguageCode.ToLower() );
            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeAnalyzeTextLanguage: {0}", ex.Message ) );
            }
            
          }

        }

      }

    }

    /**************************************************************************/
        
    ~MacroscopeAnalyzeTextLanguage ()
    {
      this.DetectLanguage = null;
    }

    /**************************************************************************/

    public string AnalyzeLanguage ( string Text )
    {

      string LanguageDetected = null;

      try
      {
        LanguageDetected = this.DetectLanguage.Detect( text: Text );
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "AnalyzeLanguage: {0}", ex.Message ) );
      }

      return( LanguageDetected );

    }

    /**************************************************************************/

  }

}
