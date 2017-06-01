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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeCustomFilters.
  /// </summary>

  public class MacroscopeCustomFilters : Macroscope
  {

    /**************************************************************************/

    private Boolean Enabled;

    private int Max;
    
    private List<KeyValuePair<string, MacroscopeConstants.Contains>> Contains;

    /**************************************************************************/
    
    public MacroscopeCustomFilters ( int Size )
    {

      this.Disable();
      
      this.Max = Size;
      
      this.Contains = new List<KeyValuePair<string, MacroscopeConstants.Contains>> ( Size );

      for( int Slot = 0 ; Slot < this.Max ; Slot++ )
      {

        KeyValuePair<string, MacroscopeConstants.Contains> Pair;

        Pair = new KeyValuePair<string, MacroscopeConstants.Contains> (
          Slot.ToString(),
          MacroscopeConstants.Contains.MUSTHAVE
        );

        this.Contains.Add( Pair );

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
      int Slot,
      string Text,
      MacroscopeConstants.Contains ContainsSetting
    )
    {
      
      KeyValuePair<string, MacroscopeConstants.Contains> Pair;

      Pair = new KeyValuePair<string, MacroscopeConstants.Contains> ( Text, ContainsSetting );

      this.Contains[ Slot ] = Pair;

      this.SetEnabled();
      
    }

    /**************************************************************************/

    public KeyValuePair<string, MacroscopeConstants.Contains> GetPattern (
      int Slot
    )
    {

      return( this.Contains[ Slot ] );

    }

    /**************************************************************************/

    public Dictionary<string, MacroscopeConstants.TextPresence> AnalyzeText ( string Text )
    {

      if( !this.IsEnabled() )
      {
        return( null );
      }

      Dictionary<string, MacroscopeConstants.TextPresence> Analyzed = new Dictionary<string, MacroscopeConstants.TextPresence> ( this.Max );

      lock( this.Contains )
      {

        for( int Slot = 0 ; Slot < this.Max ; Slot++ )
        {

          string PatternText = this.Contains[ Slot ].Key;

          if( string.IsNullOrEmpty( PatternText ) )
          {
            continue;
          }

          if( this.Contains[ Slot ].Value == MacroscopeConstants.Contains.MUSTHAVE )
          {

            if( Text.Contains( PatternText ) )
            {
              Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.CONTAINS );
            }
            else
            {
              Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.NOTCONTAINS );
            }

          }
          else
          if( this.Contains[ Slot ].Value == MacroscopeConstants.Contains.MUSTNOTHAVE )
          {
            
            if( Text.Contains( PatternText ) )
            {
              Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.SHOULDNOTCONTAIN );
            }
            else
            {
              Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.NOTCONTAINS );
            }

          }
          else
          {
            throw new Exception ( "Undefined MacroscopeConstants.Contains" );
          }

        }

      }

      return( Analyzed );
      
    }

    /**************************************************************************/
    
  }

}
