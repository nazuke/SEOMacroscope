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
using System.Threading;

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
		
			foreach( string sLine in Regex.Split( IncludeUrlPatternsText, "\r\n", RegexOptions.Singleline ) )
			{
				DebugMsg( string.Format( "LoadIncludeUrlPatterns: {0}", sLine ) );
				if( sLine.Length > 0 )
				{
					this.IncludeUrlPatternsList.Add( sLine );
				}
			}

		}

		public string FetchIncludeUrlPatterns ()
		{
			string sText = string.Join( "\r\n", this.IncludeUrlPatternsList );
			return( sText );
		}

		public Boolean UseIncludeUrlPatterns ()
		{
			Boolean bUse = false;

			int Count = this.IncludeUrlPatternsList.Count;
			
			if( Count > 0 )
			{
				bUse = true;
			}
			return( bUse );
		}

		public Boolean MatchesIncludeUrlPattern ( string Url )
		{
			Boolean bMatch = false;

			// TODO: Implement this.

			for( int i = 0 ; i < this.IncludeUrlPatternsList.Count ; i++ )
			{
				if( Url.IndexOf( this.IncludeUrlPatternsList[ i ] ) >= 0 )
				{
					DebugMsg( string.Format( "MatchesIncludeUrlPattern: MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
					bMatch = true;
					break;
				}
				else
				{
					DebugMsg( string.Format( "MatchesIncludeUrlPattern: NO MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
				}
			}

			return( bMatch );
		}

		/** Exclude URL Patterns **************************************************/

		public void LoadExcludeUrlPatterns ( string ExcludeUrlPatternsText )
		{
			
			this.ExcludeUrlPatternsList.Clear();

			foreach( string sLine in Regex.Split( ExcludeUrlPatternsText, "\r\n", RegexOptions.Singleline ) )
			{
				DebugMsg( string.Format( "LoadExcludeUrlPatterns: {0}", sLine ) );
				if( sLine.Length > 0 )
				{
					this.ExcludeUrlPatternsList.Add( sLine );
				}
			}

		}
		
		public string FetchExcludeUrlPatterns ()
		{
			string sText = string.Join( "\r\n", this.ExcludeUrlPatternsList );
			return( sText );
		}

		public Boolean UseExcludeUrlPatterns ()
		{
			Boolean bUse = false;
			if( this.ExcludeUrlPatternsList.Count > 0 )
			{
				bUse = true;
			}
			return( bUse );
		}

		public Boolean MatchesExcludeUrlPattern ( string Url )
		{
			Boolean bMatch = false;

			// TODO: Implement this.

			int iPatterns = this.ExcludeUrlPatternsList.Count;

			DebugMsg( string.Format( "iPatterns: COUNT: {0}", iPatterns ) );

			for( int i = 0 ; i < iPatterns ; i++ )
			{
				if( Url.IndexOf( this.ExcludeUrlPatternsList[ i ] ) >= 0 )
				{
					DebugMsg( string.Format( "MatchesIncludeUrlPattern: MATCH: {0}", i ) );
					DebugMsg( string.Format( "MatchesIncludeUrlPattern: MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
					bMatch = true;
					break;
				}
				else
				{
					DebugMsg( string.Format( "MatchesIncludeUrlPattern: NO MATCH: {0}", i ) );
					DebugMsg( string.Format( "MatchesIncludeUrlPattern: NO MATCH: {0} :: {1}", this.IncludeUrlPatternsList[ i ], Url ) );
				}
			}
						
			return( bMatch );
		}
		
		/**************************************************************************/
		
	}

}
