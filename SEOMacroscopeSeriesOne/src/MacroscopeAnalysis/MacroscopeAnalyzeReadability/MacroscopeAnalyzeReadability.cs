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

namespace SEOMacroscope
{

  /// <summary>
  /// Analyze the readability of the text in a document.
  /// </summary>

  public class MacroscopeAnalyzeReadability : Macroscope
  {

    /**************************************************************************/

    public enum AnalyzeReadabilityMethod
    {
      UNKNOWN = 0,
      FLESCH_KINCAID = 1,
      SMOG = 2
    }
    
    public enum AnalyzeReadabilityEnglishAlgorithm
    {
      FLESCH_KINCAID = 0,
      SMOG = 1
    }
    
    /**************************************************************************/

    public MacroscopeAnalyzeReadability ()
    {
      
      this.SuppressDebugMsg = true;

    }

    /**************************************************************************/

    public static IMacroscopeAnalyzeReadability AnalyzerFactory ( MacroscopeDocument msDoc )
    {
      
      IMacroscopeAnalyzeReadability Analyzer = null;
      string IsoLanguageCode = msDoc.GetIsoLanguageCode();

      if( !string.IsNullOrEmpty( IsoLanguageCode ) )
      {
        Analyzer = MacroscopeAnalyzeReadability.AnalyzerFactory( IsoLanguageCode: IsoLanguageCode );
      }

      return( Analyzer );
      
    }

    /** -------------------------------------------------------------------- **/

    public static IMacroscopeAnalyzeReadability AnalyzerFactory ( string IsoLanguageCode )
    {

      IMacroscopeAnalyzeReadability Analyzer = null;

      if( IsoLanguageCode.Equals( "x-default" ) )
      {
        IsoLanguageCode = "en";
      }

      switch( IsoLanguageCode )
      {
        case "en":
          switch( MacroscopePreferencesManager.GetAnalyzeTextReadabilityEnglishAlgorithm() )
          {
            case MacroscopeAnalyzeReadability.AnalyzeReadabilityEnglishAlgorithm.FLESCH_KINCAID:
              Analyzer = new MacroscopeAnalyzeReadabilityFleschKincaid ();
              break;
            case MacroscopeAnalyzeReadability.AnalyzeReadabilityEnglishAlgorithm.SMOG:
              Analyzer = new MacroscopeAnalyzeReadabilitySmog ();
              break;
            default:
              break;
          }
          break;
        default:
          break;
      }

      return( Analyzer );
            
    }

    /**************************************************************************/

    public static string FormatAnalyzeReadabilityMethod (
      MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod ReadabilityMethod 
    )
    {
      
      string Formatted = "Unknown";
       
      switch( ReadabilityMethod )
      {
        case MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod.FLESCH_KINCAID:
          Formatted = "Flesch-Kincaid Reading Ease";
          break;
        case MacroscopeAnalyzeReadability.AnalyzeReadabilityMethod.SMOG:
          Formatted = "SMOG Index";
          break;
        default:
          break;
      }
      
      return( Formatted );
      
    }

    /**************************************************************************/

  }

}
