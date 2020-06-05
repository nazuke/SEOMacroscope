/*

	This file is part of SEOMacroscope.

	Copyright 2020 Jason Holland.

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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeIncludeExcludeUrls.
  /// </summary>

  [Serializable()]
  public class MacroscopeIncludeExcludeUrls : Macroscope
  {

    /**************************************************************************/

    private List<string> ExplicitIncludeUrlPatternsList;
    private List<string> ExplicitExcludeUrlPatternsList;

    private List<string> IncludeUrlPatternsList;
    private List<string> ExcludeUrlPatternsList;

    /**************************************************************************/

    public MacroscopeIncludeExcludeUrls ()
    {

      this.SuppressDebugMsg = true;

      this.ExplicitIncludeUrlPatternsList = new List<string>( 8 );
      this.ExplicitExcludeUrlPatternsList = new List<string>( 8 );

      this.IncludeUrlPatternsList = new List<string>( 32 );
      this.ExcludeUrlPatternsList = new List<string>( 32 );

    }

    /** Explicit Include URL Patterns *****************************************/

    public void ClearExplicitIncludeUrls ()
    {

      lock( this.ExplicitIncludeUrlPatternsList )
      {
        this.ExplicitIncludeUrlPatternsList.Clear();
      }

    }

    /** -------------------------------------------------------------------- **/

    public void AddExplicitIncludeUrl ( string Url )
    {

      Url = Url.Trim();

      if( !string.IsNullOrEmpty( Url ) )
      {

        if( !this.ExplicitIncludeUrlPatternsList.Contains( Url ) )
        {
          this.ExplicitIncludeUrlPatternsList.Add( Url );
        }

      }

    }

    /** Explicit Exclude URL Patterns *****************************************/

    public void ClearExplicitExcludeUrls ()
    {

      lock( this.ExplicitExcludeUrlPatternsList )
      {
        this.ExplicitExcludeUrlPatternsList.Clear();
      }

    }

    /** -------------------------------------------------------------------- **/

    public void AddExplicitExcludeUrl ( string Url )
    {

      Url = Url.Trim();

      if( !string.IsNullOrEmpty( Url ) )
      {

        if( !this.ExplicitExcludeUrlPatternsList.Contains( Url ) )
        {
          this.ExplicitExcludeUrlPatternsList.Add( Url );
        }

      }

    }

    /** Include URL Patterns **************************************************/

    public void LoadIncludeUrlPatterns ( string IncludeUrlPatternsText )
    {

      this.IncludeUrlPatternsList.Clear();

      foreach( string Url in Regex.Split( IncludeUrlPatternsText, Environment.NewLine, RegexOptions.Singleline ) )
      {

        string TrimmedUrl = Url.Trim();

        if( !string.IsNullOrEmpty( TrimmedUrl ) )
        {
          this.IncludeUrlPatternsList.Add( TrimmedUrl );
        }

      }

    }

    /** -------------------------------------------------------------------- **/

    public void AddIncludeUrlPattern ( string Url )
    {

      Url = Url.Trim();

      if( !string.IsNullOrEmpty( Url ) )
      {
        this.IncludeUrlPatternsList.Add( Url );
      }

    }

    /** -------------------------------------------------------------------- **/

    public string FetchIncludeUrlPatterns ()
    {

      string Text = string.Join( Environment.NewLine, this.IncludeUrlPatternsList );

      return ( Text );

    }

    /** -------------------------------------------------------------------- **/

    public bool UseIncludeUrlPatterns ()
    {

      bool IncludePatterns = false;

      if( this.IncludeUrlPatternsList.Count > 0 )
      {
        IncludePatterns = true;
      }

      return ( IncludePatterns );

    }

    /** -------------------------------------------------------------------- **/

    public bool MatchesIncludeUrlPattern ( string Url )
    {

      bool PatternMatches = false;

      for( int i = 0 ; i < this.ExplicitIncludeUrlPatternsList.Count ; i++ )
      {

        if( Url.Equals( this.ExplicitIncludeUrlPatternsList[ i ] ) )
        {
          PatternMatches = true;
          break;
        }

      }

      if( !PatternMatches )
      {

        for( int i = 0 ; i < this.IncludeUrlPatternsList.Count ; i++ )
        {

          try
          {

            if( Regex.IsMatch( Url, this.IncludeUrlPatternsList[ i ] ) )
            {
              PatternMatches = true;
              break;
            }

          }
          catch( Exception ex )
          {
            this.DebugMsg( string.Format( "MatchesIncludeUrlPattern: {0}", ex.Message ) );
          }

        }

      }

      return ( PatternMatches );

    }

    /** Exclude URL Patterns **************************************************/

    public void LoadExcludeUrlPatterns ( string ExcludeUrlPatternsText )
    {

      this.ExcludeUrlPatternsList.Clear();

      foreach( string Url in Regex.Split( ExcludeUrlPatternsText, Environment.NewLine, RegexOptions.Singleline ) )
      {

        string TrimmedUrl = Url.Trim();

        if( !string.IsNullOrEmpty( TrimmedUrl ) )
        {
          this.ExcludeUrlPatternsList.Add( TrimmedUrl );
        }

      }

    }

    /** -------------------------------------------------------------------- **/

    public void AddExcludeUrlPattern ( string Url )
    {

      Url = Url.Trim();

      if( !string.IsNullOrEmpty( Url ) )
      {
        this.ExcludeUrlPatternsList.Add( Url );
      }

    }

    /** -------------------------------------------------------------------- **/

    public string FetchExcludeUrlPatterns ()
    {

      string Text = string.Join( Environment.NewLine, this.ExcludeUrlPatternsList );

      return ( Text );

    }

    /** -------------------------------------------------------------------- **/

    public bool UseExcludeUrlPatterns ()
    {

      bool ExcludePatterns = false;

      if( this.ExcludeUrlPatternsList.Count > 0 )
      {
        ExcludePatterns = true;
      }

      return ( ExcludePatterns );

    }

    /** -------------------------------------------------------------------- **/

    public bool MatchesExcludeUrlPattern ( string Url )
    {

      bool PatternMatches = false;

      if( this.ExplicitExcludeUrlPatternsList.Count > 0 )
      {

        for( int i = 0 ; i < this.ExplicitExcludeUrlPatternsList.Count ; i++ )
        {

          if( Url.Equals( this.ExplicitExcludeUrlPatternsList[ i ] ) )
          {
            PatternMatches = true;
            break;
          }

        }

      }

      if( !PatternMatches )
      {

        if( this.ExcludeUrlPatternsList.Count > 0 )
        {

          for( int i = 0 ; i < this.ExcludeUrlPatternsList.Count ; i++ )
          {

            try
            {

              if( Regex.IsMatch( Url, this.ExcludeUrlPatternsList[ i ] ) )
              {
                PatternMatches = true;
                break;
              }

            }
            catch( Exception ex )
            {
              this.DebugMsg( string.Format( "MatchesExcludeUrlPattern: {0}", ex.Message ) );
            }

          }

        }

      }

      return ( PatternMatches );

    }

    /**************************************************************************/

  }

}
