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
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;


namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeCustomFilters.
  /// </summary>

  [Serializable()]
  public class MacroscopeCustomFilters : Macroscope
  {

    /**************************************************************************/

    private bool Enabled;

    private int Max;

    private List<KeyValuePair<string, MacroscopeConstants.Contains>> Contains;

    /**************************************************************************/

    public MacroscopeCustomFilters ( int Size )
    {

      this.Disable();

      this.Max = Size;

      this.Contains = new List<KeyValuePair<string, MacroscopeConstants.Contains>>( Size );

      for ( int Slot = 0 ; Slot < this.Max ; Slot++ )
      {

        KeyValuePair<string, MacroscopeConstants.Contains> Pair;

        Pair = new KeyValuePair<string, MacroscopeConstants.Contains>(
          Slot.ToString(),
          MacroscopeConstants.Contains.MUST_HAVE_STRING
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

    public bool IsEnabled ()
    {
      return ( this.Enabled );
    }

    /**************************************************************************/

    public int GetSize ()
    {
      return ( this.Max );
    }

    /**************************************************************************/

    public void SetPattern (
      int Slot,
      string Text,
      MacroscopeConstants.Contains ContainsSetting
    )
    {

      KeyValuePair<string, MacroscopeConstants.Contains> Pair;

      Pair = new KeyValuePair<string, MacroscopeConstants.Contains>( Text, ContainsSetting );

      this.Contains[ Slot ] = Pair;

      this.SetEnabled();

    }

    /**************************************************************************/

    public KeyValuePair<string, MacroscopeConstants.Contains> GetPattern (
      int Slot
    )
    {

      return ( this.Contains[ Slot ] );

    }

    /**************************************************************************/

    public Dictionary<string, MacroscopeConstants.TextPresence> AnalyzeText ( string Text )
    {

      Dictionary<string, MacroscopeConstants.TextPresence> Analyzed = null;

      if ( !this.IsEnabled() )
      {
        return ( Analyzed );
      }

      lock ( this.Contains )
      {

        Analyzed = new Dictionary<string, MacroscopeConstants.TextPresence>( this.Max );

        for ( int Slot = 0 ; Slot < this.Max ; Slot++ )
        {

          string PatternText = this.Contains[ Slot ].Key;

          if ( string.IsNullOrEmpty( PatternText ) )
          {
            continue;
          }

          switch ( this.Contains[ Slot ].Value )
          {

            case MacroscopeConstants.Contains.MUST_HAVE_STRING:
              if ( Text.Contains( PatternText ) )
              {
                Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.CONTAINS_STRING );
              }
              else
              {
                Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.MUST_CONTAIN_STRING );
              }
              break;

            case MacroscopeConstants.Contains.MUST_NOT_HAVE_STRING:
              if ( Text.Contains( PatternText ) )
              {
                Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.SHOULD_NOT_CONTAIN_STRING );
              }
              else
              {
                Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.NOT_CONTAINS_STRING );
              }
              break;

            case MacroscopeConstants.Contains.MUST_HAVE_REGEX:
              if ( Regex.IsMatch( Text, PatternText ) )
              {
                Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.CONTAINS_REGEX );
              }
              else
              {
                Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.MUST_CONTAIN_REGEX );
              }
              break;

            case MacroscopeConstants.Contains.MUST_NOT_HAVE_REGEX:
              if ( Regex.IsMatch( Text, PatternText ) )
              {
                Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.SHOULD_NOT_CONTAIN_REGEX );
              }
              else
              {
                Analyzed.Add( PatternText, MacroscopeConstants.TextPresence.NOT_CONTAINS_REGEX );
              }
              break;

            default:
              throw new Exception( "Undefined MacroscopeConstants.Contains" );
          }

        }

      }

      return ( Analyzed );

    }

    /**************************************************************************/

    public bool CanApplyCustomFiltersToDocument ( MacroscopeDocument msDoc )
    {

      bool CanApply = true;

      if (
        ( msDoc == null )
        || ( msDoc.GetIsRedirect() )
        || ( msDoc.GetStatusCode() != HttpStatusCode.OK )
        || ( !msDoc.GetIsInternal() ) )
      {
        CanApply = false;
      }
      else
      {

        if (
          !( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML )
          || msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.CSS )
          || msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.JAVASCRIPT )
          || msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.TEXT )
          || msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.XML ) )
          )
        {
          CanApply = false;
        }
        else
        {

          switch ( msDoc.GetDocumentType() )
          {
            case MacroscopeConstants.DocumentType.HTML:
              if ( !MacroscopePreferencesManager.GetCustomFiltersApplyToHtml() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.CSS:
              if ( !MacroscopePreferencesManager.GetCustomFiltersApplyToCss() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.JAVASCRIPT:
              if ( !MacroscopePreferencesManager.GetCustomFiltersApplyToJavascripts() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.TEXT:
              if ( !MacroscopePreferencesManager.GetCustomFiltersApplyToText() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.XML:
              if ( !MacroscopePreferencesManager.GetCustomFiltersApplyToXml() )
              {
                CanApply = false;
              }
              break;
            default:
              break;
          }

        }

      }

      return ( CanApply );

    }

    /**************************************************************************/

  }

}
