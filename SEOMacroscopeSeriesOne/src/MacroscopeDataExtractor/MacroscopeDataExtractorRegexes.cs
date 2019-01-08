/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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

  [Serializable()]
  public class MacroscopeDataExtractorRegexes : MacroscopeDataExtractor
  {

    /**************************************************************************/

    private List<KeyValuePair<string, Regex>> ExtractRegexes;

    /**************************************************************************/

    public MacroscopeDataExtractorRegexes ( int Size )
      : base( Size: Size )
    {
      
      this.SuppressDebugMsg = true;
     
      this.ExtractRegexes = new List<KeyValuePair<string, Regex>> ( this.GetSize() );

      for( int Slot = 0 ; Slot < this.GetSize() ; Slot++ )
      {
        
        this.ExtractActiveInactive.Add( MacroscopeConstants.ActiveInactive.INACTIVE );
        
        this.ExtractRegexes.Add( new KeyValuePair<string, Regex> ( string.Format( "Regex {0}", Slot + 1 ), null ) );

      }

    }
   
    /**************************************************************************/

    public void SetRegex (
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

        Regex RegexPattern = new Regex ( "", RegexOptions.Singleline );
        KeyValuePair<string, Regex> RegexSlot = new KeyValuePair<string, Regex> ( string.Format( "Regex {0}", Slot + 1 ), RegexPattern );

        this.ExtractRegexes[ Slot ] = RegexSlot;
        
        throw( new FormatException ( "Invalid regular expression" ) );
        
      }

    }

    /**************************************************************************/
        
    public string GetLabel ( int Slot )
    {

      return( this.ExtractRegexes[ Slot ].Key );

    }
    
    /**************************************************************************/
        
    public Regex GetRegex ( int Slot )
    {

      return( this.ExtractRegexes[ Slot ].Value );

    }

    /**************************************************************************/

    public List<KeyValuePair<string, string>> AnalyzeText ( string Text )
    {

      List<KeyValuePair<string, string>> ResultList = new List<KeyValuePair<string, string>> ( 8 );

      if( this.IsEnabled() )
      {
        
        for( int Slot = 0 ; Slot < this.GetSize() ; Slot++ )
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

          for( int i = 0 ; i < PatternMatches.Count ; i++ )
          {

            Match match = PatternMatches[ i ];

            for( int j = 1 ; j < match.Groups.Count ; j++ )
            {

              Group captured = match.Groups[ j ];
            
              this.DebugMsg( string.Format( "captured: {0} => \"{1}\"", captured.Index, captured.Value ) );

              string FoundString = captured.Value;

              if( !string.IsNullOrEmpty( FoundString ) )
              {

                KeyValuePair<string, string> MatchedItem;
              
                if( MacroscopePreferencesManager.GetDataExtractorsCleanWhiteSpace() )
                {
                  FoundString = this.CleanWhiteSpace( Text: FoundString );
                }
                                  
                MatchedItem = new KeyValuePair<string, string> ( 
                  this.ExtractRegexes[ Slot ].Key,
                  FoundString
                );
              
                ResultList.Add( MatchedItem );

              }
            
            }
            
          }

        }
        
      }

      return( ResultList );

    }

    /**************************************************************************/
    
    public static bool SyntaxCheckRegex ( string RegexString )
    {
      
      bool IsValid = false;

      try
      {
        
        Regex rgx = new Regex ( RegexString, RegexOptions.Singleline );

        IsValid = true;

      }
      catch( Exception ex )
      {

        DebugMsgStatic( ex.Message );

        IsValid = false;
        
      }

      return( IsValid );

    }
        
    /**************************************************************************/
  
  }

}
