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
using NTextCat;
using System.Linq;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeAnalyzeTextLanguage.
  /// </summary>

  public class MacroscopeAnalyzeTextLanguage : Macroscope
  {

    /**************************************************************************/

    RankedLanguageIdentifier NTextCatIdentifier;
    
    /**************************************************************************/

    public MacroscopeAnalyzeTextLanguage ()
    {

      this.SuppressDebugMsg = true;
      
      RankedLanguageIdentifierFactory NTextCatFactory = new RankedLanguageIdentifierFactory ();
      
      this.NTextCatIdentifier = NTextCatFactory.Load( "Core14.profile.xml" );

    }

    /**************************************************************************/
        
    ~MacroscopeAnalyzeTextLanguage ()
    {
      
      this.NTextCatIdentifier = null;
      
    }

    /**************************************************************************/

    public string AnalyzeLanguage ( string Text )
    {
      
      // http://ntextcat.codeplex.com/wikipage?title=NTextCat.Lib%20Samples&referringTitle=Home

      string LanguageDetected = null;

      var PossibleLanguages = this.NTextCatIdentifier.Identify( Text );

      var ProbableLanguage = PossibleLanguages.FirstOrDefault();

      if( ProbableLanguage != null )
      {
        LanguageDetected = ProbableLanguage.Item1.Iso639_3;
      }

      DebugMsg( string.Format( "LanguageDetected: {0}", LanguageDetected ) );

      return( LanguageDetected );

    }

    /**************************************************************************/

  }

}
