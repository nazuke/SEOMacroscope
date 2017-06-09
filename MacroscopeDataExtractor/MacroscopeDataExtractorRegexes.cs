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

    private List<MacroscopeConstants.ActiveInactive> ExtractActiveInactive;
    private List<KeyValuePair<string, Regex>> ExtractRegexes;

    /**************************************************************************/

    public MacroscopeDataExtractorRegexes ( int Size )
    {

      this.Disable();
      
      this.Max = Size;
      
      this.ExtractActiveInactive = new List<MacroscopeConstants.ActiveInactive> ( this.Max );

      this.ExtractRegexes = new List<KeyValuePair<string, Regex>> ( this.Max );

      for( int Slot = 0 ; Slot < this.Max ; Slot++ )
      {
        
        this.ExtractActiveInactive.Add( MacroscopeConstants.ActiveInactive.INACTIVE );
        
        this.ExtractRegexes.Add( new KeyValuePair<string, Regex> () );

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

    public void SetActiveInactive ( int Slot, MacroscopeConstants.ActiveInactive State )
    {
      this.ExtractActiveInactive[ Slot ] = State;
    }
    
    public MacroscopeConstants.ActiveInactive GetActiveInactive ( int Slot )
    {
      return( this.ExtractActiveInactive[ Slot ] );
    }

    /**************************************************************************/

    public void SetPattern (
      int Slot,
      string RegexLabel,
      string RegexString
    )
    {
      
      if( SyntaxCheckRegex( RegexString: RegexString ) )
      {
        
        Regex RegexPattern = new Regex ( RegexString, RegexOptions.Singleline );
        KeyValuePair<string, Regex> RegexSlot = new KeyValuePair<string, Regex> ( RegexLabel, RegexPattern );

        this.ExtractRegexes[ Slot ] = RegexSlot;
        
        this.SetEnabled();
        
      }
      else
      {

        throw( new FormatException ( "Invalid regular expression" ) );
        
      }

    }

    /**************************************************************************/

    public KeyValuePair<string, Regex> GetPattern ( int Slot )
    {

      return( this.ExtractRegexes[ Slot ] );

    }
    
    /**************************************************************************/
        
    public string GetPatternLabel ( int Slot )
    {

      return( this.ExtractRegexes[ Slot ].Key );

    }
    
    /**************************************************************************/
        
    public Regex GetPatternRegex ( int Slot )
    {

      return( this.ExtractRegexes[ Slot ].Value );

    }

    /**************************************************************************/

    public List<KeyValuePair<string, string>> AnalyzeText ( string Text )
    {

      List<KeyValuePair<string, string>> ResultList = new List<KeyValuePair<string, string>> ( 8 );

      if( this.IsEnabled() )
      {
        
        for( int Slot = 0 ; Slot < this.Max ; Slot++ )
        {
          
          MatchCollection PatternMatches;
          string SlotRegex;
                
          this.DebugMsg(
            string.Format(
              "SLOT KEY: {0} => {1} => {2}",
              Slot,
              this.ExtractRegexes[ Slot ].Key,
              this.ExtractRegexes[ Slot ].Value
            )
          );
          
          if( string.IsNullOrEmpty( this.ExtractRegexes[ Slot ].Key ) )
          {
            continue;
          }
        
          SlotRegex = this.ExtractRegexes[ Slot ].Value.ToString();
                
          PatternMatches = Regex.Matches( 
            Text,
            SlotRegex,
            RegexOptions.Singleline
          );

          foreach( Match match in PatternMatches )
          {
            
            string FoundString = match.Value;

            if( !string.IsNullOrEmpty( FoundString ) )
            {

              KeyValuePair<string, string> MatchedItem;
              
              MatchedItem = new KeyValuePair<string, string> ( 
                this.ExtractRegexes[ Slot ].Key,
                FoundString
              );
              
              ResultList.Add( MatchedItem );
              
            }
            
          }

        }
        
      }

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
