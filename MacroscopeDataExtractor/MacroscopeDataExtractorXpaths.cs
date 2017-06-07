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
using System.Xml.XPath;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDataExtractorXpaths.cs.
  /// </summary>

  public class MacroscopeDataExtractorXpaths : Macroscope
  {

    /**************************************************************************/

    private Boolean Enabled;

    private int Max;

    //private List<KeyValuePair<string, XPathExpression>> ExtractXpaths;
    
    
    
    

    
    
    
    
    

    
    
    
    /**************************************************************************/
    
    public MacroscopeDataExtractorXpaths ( int Size )
    {

      this.Disable();
      
      this.Max = Size;
      


      for( int Slot = 0 ; Slot < this.Max ; Slot++ )
      {



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

    public void GetPattern (
      int Slot
    )
    {

      //return( this.Contains[ Slot ] );

    }

    /**************************************************************************/

    public void AnalyzeText ( string Text )
    {



      
    }

    /**************************************************************************/
    
    public static Boolean SyntaxCheckXpath ( string XpathString )
    {
      
      Boolean IsValid = false;

      try
      {
        
        XPathExpression expression = XPathExpression.Compile( XpathString );

        IsValid = true;

      }
      catch( XPathException ex )
      {

        DebugMsg( ex.Message, true );

        IsValid = false;
        
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
