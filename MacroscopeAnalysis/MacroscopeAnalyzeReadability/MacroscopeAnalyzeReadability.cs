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

namespace SEOMacroscope
{

  /// <summary>
  /// Analyze the readability of the text in a document.
  /// </summary>

  public class MacroscopeAnalyzeReadability : Macroscope
  {

    /**************************************************************************/

    public enum AnalyzeReadabilityType
    {
      UNKNOWN = 0,
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
      
      switch( IsoLanguageCode )
      {
        case "en":
          Analyzer = new MacroscopeAnalyzeReadabilityEnglish ();
          break;
        default:
          break;
      }

      return( Analyzer );
            
    }

    /**************************************************************************/

  }

}
