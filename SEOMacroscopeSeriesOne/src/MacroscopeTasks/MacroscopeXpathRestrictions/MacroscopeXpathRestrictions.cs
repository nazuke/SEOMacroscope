/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

	The GitHub repository may be found at:

		https://github.com/nazuke/SEOMacroscope

	SEOMacroscope is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	SEOMacroscope is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeXpathRestrictions.
  /// </summary>

  public class MacroscopeXpathRestrictions : Macroscope
  {

    /**************************************************************************/

    private List<string> IncludeXpathsList;

    /**************************************************************************/

    public MacroscopeXpathRestrictions ()
    {

      this.IncludeXpathsList = new List<string> ( 8 );

    }

    /** Include XPath Patterns ************************************************/

    public void LoadIncludeXpathPatterns ( string IncludeXpathsText )
    {

      this.IncludeXpathsList.Clear();

      foreach( string Url in Regex.Split( IncludeXpathsText, Environment.NewLine, RegexOptions.Singleline ) )
      {
        
        DebugMsg( string.Format( "LoadIncludeXpathPatterns: {0}", Url ) );
        
        string TrimmedUrl = Url.Trim();
        
        if( !string.IsNullOrEmpty( TrimmedUrl ) )
        {
          this.IncludeXpathsList.Add( TrimmedUrl );
        }
      
      }

    }

    /** -------------------------------------------------------------------- **/

    public string FetchIncludeUrlPatterns ()
    {
      
      string Text = string.Join( Environment.NewLine, this.IncludeXpathsList );
      
      return( Text );
    
    }

    /** -------------------------------------------------------------------- **/

    public bool UseIncludeUrlPatterns ()
    {
      
      bool IncludePatterns = false;

      if( this.IncludeXpathsList.Count > 0 )
      {
        IncludePatterns = true;
      }
      
      return( IncludePatterns );
      
    }

    /** -------------------------------------------------------------------- **/

    public bool MatchesXpath( HtmlDocument HtmlDoc )
    {

      bool XpathMatches = false;

      return( XpathMatches );
      
    }

    /**************************************************************************/

  }

}
