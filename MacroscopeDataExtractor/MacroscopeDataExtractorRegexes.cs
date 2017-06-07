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
using System.Text;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDataExtractorRegexes.
  /// </summary>

  public class MacroscopeDataExtractorRegexes : Macroscope
  {

    /**************************************************************************/

    private Boolean Enabled;

    private int Max;

    private List<KeyValuePair<string, Regex>> ExtractRegexes;

    /**************************************************************************/

    public MacroscopeDataExtractorRegexes ( int Size )
    {

      this.Disable();
      
      this.Max = Size;
      
      this.ExtractRegexes = new List<KeyValuePair<string, Regex>> ( this.Max );

      for( int Slot = 0 ; Slot < this.Max ; Slot++ )
      {

        this.ExtractRegexes[ Slot ] = new KeyValuePair<string, Regex> ();

      }

    }

    /**************************************************************************/
    
    public void Disable ()
    {
      this.Enabled = false;
    }
    
    public void SetEnabled ()
    {
      this.Enabled = true;
    }
    
    public Boolean IsEnabled ()
    {
      return( this.Enabled );
    }
    
    /**************************************************************************/

    public int GetSize ()
    {
      return( this.Max );
    }

    /**************************************************************************/

    public void SetPattern (
      int Slot
    )
    {

      //return( this.Contains[ Slot ] );

    }

    /**************************************************************************/

    public KeyValuePair<string, Regex> GetPattern ( int Slot )
    {

      return( this.ExtractRegexes[ Slot ] );

    }

    /**************************************************************************/

    public List<string> AnalyzeText ( string Text )
    {

      List<string> ResultList = new List<string> ( 8 );








      return( ResultList );

    }

    /**************************************************************************/
    
    public static Boolean SyntaxCheckRegex ( string RegexString )
    {
      
      Boolean IsValid = false;

      try
      {
        
        Regex rgx = new Regex ( RegexString, RegexOptions.Singleline );

        IsValid = true;

      }
      catch( Exception ex )
      {

        DebugMsg( ex.Message, true );

        IsValid = false;
        
      }

      return( IsValid );

    }
        
    /**************************************************************************/
  
  }

}
