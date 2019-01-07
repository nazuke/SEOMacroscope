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
  /// Description of MacroscopeDataExtractor.cs.
  /// </summary>

  [Serializable()]
  public class MacroscopeDataExtractor : Macroscope
  {

    /**************************************************************************/

    protected bool Enabled;

    protected int Max;

    protected List<MacroscopeConstants.ActiveInactive> ExtractActiveInactive;

    /**************************************************************************/

    public MacroscopeDataExtractor ( int Size )
    {

      this.SuppressDebugMsg = true;

      this.Disable();

      this.SetSize( NewSize: Size );

      this.ExtractActiveInactive = new List<MacroscopeConstants.ActiveInactive>( this.GetSize() );

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

    public void SetSize ( int NewSize )
    {
      this.Max = NewSize;
    }

    public int GetSize ()
    {
      return ( this.Max );
    }

    /**************************************************************************/

    public void SetActiveInactive ( int Slot, MacroscopeConstants.ActiveInactive State )
    {
      this.ExtractActiveInactive[ Slot ] = State;
    }

    public MacroscopeConstants.ActiveInactive GetActiveInactive ( int Slot )
    {
      return ( this.ExtractActiveInactive[ Slot ] );
    }

    /**************************************************************************/

    protected string CleanWhiteSpace ( string Text )
    {

      string CleanedText = Text;

      if ( !string.IsNullOrEmpty( Text ) )
      {

        CleanedText = Regex.Replace( CleanedText, @"[\s]+", " ", RegexOptions.Singleline );

        CleanedText = CleanedText.Trim();

      }

      return ( CleanedText );

    }

    /**************************************************************************/

    public bool CanApplyDataExtractorsToDocument ( MacroscopeDocument msDoc )
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
          || msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.PDF )
          || msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.TEXT )
          || msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.XML ) ) )
        {
          CanApply = false;
        }
        else
        {

          switch ( msDoc.GetDocumentType() )
          {
            case MacroscopeConstants.DocumentType.HTML:
              if ( !MacroscopePreferencesManager.GetDataExtractorsApplyToHtml() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.CSS:
              if ( !MacroscopePreferencesManager.GetDataExtractorsApplyToCss() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.JAVASCRIPT:
              if( !MacroscopePreferencesManager.GetDataExtractorsApplyToJavascripts() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.PDF:
              if( !MacroscopePreferencesManager.GetDataExtractorsApplyToPdf() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.TEXT:
              if ( !MacroscopePreferencesManager.GetDataExtractorsApplyToText() )
              {
                CanApply = false;
              }
              break;
            case MacroscopeConstants.DocumentType.XML:
              if ( !MacroscopePreferencesManager.GetDataExtractorsApplyToXml() )
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
