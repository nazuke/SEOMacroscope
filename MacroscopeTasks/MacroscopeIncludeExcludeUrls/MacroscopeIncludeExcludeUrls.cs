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
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeIncludeExcludeUrls.
  /// </summary>

  public class MacroscopeIncludeExcludeUrls : Macroscope
  {

    /**************************************************************************/

    List<string> IncludeUrlPatternsList;
    List<string> ExcludeUrlPatternsList;

    /**************************************************************************/

    public MacroscopeIncludeExcludeUrls ()
    {

      IncludeUrlPatternsList = new List<string> ( 128 );
      ExcludeUrlPatternsList = new List<string> ( 128 );

    }

    /** Include URL Patterns **************************************************/

    public void LoadIncludeUrlPatterns ( string IncludeUrlPatternsText )
    {

      this.IncludeUrlPatternsList.Clear();

      foreach( string Line in Regex.Split( IncludeUrlPatternsText, "\r\n", RegexOptions.Singleline ) )
      {
        DebugMsg( string.Format( "LoadIncludeUrlPatterns: {0}", Line ) );
        if( Line.Length > 0 )
        {
          this.IncludeUrlPatternsList.Add( Line );
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    public string FetchIncludeUrlPatterns ()
    {
      string Text = string.Join( "\r\n", this.IncludeUrlPatternsList );
      return( Text );
    }

    /** -------------------------------------------------------------------- **/

    public Boolean UseIncludeUrlPatterns ()
    {
      Boolean IncludePatterns = false;

      int Count = this.IncludeUrlPatternsList.Count;

      if( Count > 0 )
      {
        IncludePatterns = true;
      }
      return( IncludePatterns );
    }

    /** -------------------------------------------------------------------- **/

    public Boolean MatchesIncludeUrlPattern ( string Url )
    {
      Boolean PatternMatches = false;

      // TODO: Implement this.

      for( int i = 0 ; i < this.IncludeUrlPatternsList.Count ; i++ )
      {
        if( Url.IndexOf( this.IncludeUrlPatternsList[ i ], StringComparison.Ordinal ) >= 0 )
        {
          DebugMsg( string.Format( "MatchesIncludeUrlPattern: MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
          PatternMatches = true;
          break;
        }
        else
        {
          DebugMsg( string.Format( "MatchesIncludeUrlPattern: NO MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
        }
      }

      return( PatternMatches );
    }

    /** Exclude URL Patterns **************************************************/

    public void LoadExcludeUrlPatterns ( string ExcludeUrlPatternsText )
    {

      this.ExcludeUrlPatternsList.Clear();

      foreach( string Line in Regex.Split( ExcludeUrlPatternsText, "\r\n", RegexOptions.Singleline ) )
      {
        DebugMsg( string.Format( "LoadExcludeUrlPatterns: {0}", Line ) );
        if( Line.Length > 0 )
        {
          this.ExcludeUrlPatternsList.Add( Line );
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    public string FetchExcludeUrlPatterns ()
    {
      string Text = string.Join( "\r\n", this.ExcludeUrlPatternsList );
      return( Text );
    }

    /** -------------------------------------------------------------------- **/

    public Boolean UseExcludeUrlPatterns ()
    {
      Boolean ExcludePatterns = false;
      if( this.ExcludeUrlPatternsList.Count > 0 )
      {
        ExcludePatterns = true;
      }
      return( ExcludePatterns );
    }

    /** -------------------------------------------------------------------- **/

    public Boolean MatchesExcludeUrlPattern ( string Url )
    {
      Boolean PatternMatches = false;

      // TODO: Implement this.

      int iPatterns = this.ExcludeUrlPatternsList.Count;

      DebugMsg( string.Format( "iPatterns: COUNT: {0}", iPatterns ) );

      for( int i = 0 ; i < iPatterns ; i++ )
      {
        if( Url.IndexOf( this.ExcludeUrlPatternsList[ i ], StringComparison.Ordinal ) >= 0 )
        {
          DebugMsg( string.Format( "MatchesIncludeUrlPattern: MATCH: {0}", i ) );
          DebugMsg( string.Format( "MatchesIncludeUrlPattern: MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
          PatternMatches = true;
          break;
        }
        else
        {
          DebugMsg( string.Format( "MatchesIncludeUrlPattern: NO MATCH: {0}", i ) );
          DebugMsg( string.Format( "MatchesIncludeUrlPattern: NO MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
        }
      }

      return( PatternMatches );
    }

    /**************************************************************************/

  }

}
