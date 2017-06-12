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

    List<string> ExplicitIncludeUrlPatternsList;
    List<string> ExplicitExcludeUrlPatternsList;

    List<string> IncludeUrlPatternsList;
    List<string> ExcludeUrlPatternsList;

    /**************************************************************************/

    public MacroscopeIncludeExcludeUrls ()
    {

      this.SuppressDebugMsg = true;
      
      this.ExplicitIncludeUrlPatternsList = new List<string> ( 8 );
      this.ExplicitExcludeUrlPatternsList = new List<string> ( 8 );

      this.IncludeUrlPatternsList = new List<string> ( 32 );
      this.ExcludeUrlPatternsList = new List<string> ( 32 );

    }

    /** Include URL Patterns **************************************************/

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

    /** Include URL Patterns **************************************************/

    public void LoadIncludeUrlPatterns ( string IncludeUrlPatternsText )
    {

      this.IncludeUrlPatternsList.Clear();

      foreach( string Url in Regex.Split( IncludeUrlPatternsText, "\r\n", RegexOptions.Singleline ) )
      {
        
        DebugMsg( string.Format( "LoadIncludeUrlPatterns: {0}", Url ) );
        
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
      
      string Text = string.Join( "\r\n", this.IncludeUrlPatternsList );
      
      return( Text );
    
    }

    /** -------------------------------------------------------------------- **/

    public Boolean UseIncludeUrlPatterns ()
    {
      
      Boolean IncludePatterns = false;

      if( this.IncludeUrlPatternsList.Count > 0 )
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

      for( int i = 0 ; i < this.ExplicitIncludeUrlPatternsList.Count ; i++ )
      {
        
        if( Url.Equals( this.ExplicitIncludeUrlPatternsList[ i ] ) )
        {
          DebugMsg( string.Format( "ExplicitIncludeUrlPatternsList: MATCH: {0} :: {1}", this.ExplicitIncludeUrlPatternsList[ i ], Url ) );
          PatternMatches = true;
          break;
        }
        else
        {
          DebugMsg( string.Format( "ExplicitIncludeUrlPatternsList: NO MATCH: {0} :: {1}", this.ExplicitIncludeUrlPatternsList[ i ], Url ) );
        }

      }

      if( !PatternMatches )
      {

        for( int i = 0 ; i < this.IncludeUrlPatternsList.Count ; i++ )
        {
        
          if( Url.IndexOf( this.IncludeUrlPatternsList[ i ], StringComparison.Ordinal ) >= 0 )
          {
            DebugMsg( string.Format( "IncludeUrlPatternsList: MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
            PatternMatches = true;
            break;
          }
          else
          {
            DebugMsg( string.Format( "IncludeUrlPatternsList: NO MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
          }

        }

      }
            
      return( PatternMatches );
    }

    /** Exclude URL Patterns **************************************************/

    public void LoadExcludeUrlPatterns ( string ExcludeUrlPatternsText )
    {

      this.ExcludeUrlPatternsList.Clear();

      foreach( string Url in Regex.Split( ExcludeUrlPatternsText, "\r\n", RegexOptions.Singleline ) )
      {

        DebugMsg( string.Format( "LoadExcludeUrlPatterns: {0}", Url ) );

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

      for( int i = 0 ; i < this.ExplicitExcludeUrlPatternsList.Count ; i++ )
      {
        
        if( Url.Equals( this.ExplicitExcludeUrlPatternsList[ i ] ) )
        {
          DebugMsg( string.Format( "ExplicitExcludeUrlPatternsList: MATCH: {0}", i ) );
          DebugMsg( string.Format( "ExplicitExcludeUrlPatternsList: MATCH: {0} :: {1}", this.ExplicitExcludeUrlPatternsList[ i ], Url ) );
          PatternMatches = true;
          break;
        }
        else
        {
          DebugMsg( string.Format( "ExplicitExcludeUrlPatternsList: NO MATCH: {0}", i ) );
          DebugMsg( string.Format( "ExplicitExcludeUrlPatternsList: NO MATCH: {0} :: {1}", this.ExplicitExcludeUrlPatternsList[ i ], Url ) );
        }
        
      }

      if( !PatternMatches )
      {

        for( int i = 0 ; i < this.ExcludeUrlPatternsList.Count ; i++ )
        {
        
          if( Url.IndexOf( this.ExcludeUrlPatternsList[ i ], StringComparison.Ordinal ) >= 0 )
          {
            DebugMsg( string.Format( "ExcludeUrlPatternsList: MATCH: {0}", i ) );
            DebugMsg( string.Format( "ExcludeUrlPatternsList: MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
            PatternMatches = true;
            break;
          }
          else
          {
            DebugMsg( string.Format( "ExcludeUrlPatternsList: NO MATCH: {0}", i ) );
            DebugMsg( string.Format( "ExcludeUrlPatternsList: NO MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
          }
        
        }
      
      }

      return( PatternMatches );
      
    }

    /**************************************************************************/

  }

}
