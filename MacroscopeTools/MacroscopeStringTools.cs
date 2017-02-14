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
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeStringTools.
	/// </summary>

	public class MacroscopeStringTools : Macroscope
	{

		/**************************************************************************/

		public MacroscopeStringTools ()
		{
		}

		/**************************************************************************/

		public static string ReverseString ( string sInput )
		{
			string sOutput = "";
			for( int i = ( sInput.Length - 1 ) ; i >= 0 ; i-- )
			{
				sOutput += sInput[ i ];
			}
			return( sOutput );
		}

		/**************************************************************************/

		public static string[] ReverseStringArray ( string [] sInput )
		{
			string [] sOutput = new string[sInput.Length];
			for( int i = 0 ; i < sInput.Length ; i++ )
			{
				sOutput[ i ] = MacroscopeStringTools.ReverseString( sInput[ i ] );
			}
			return( sOutput );
		}

		/**************************************************************************/

		public static string CleanBodyText ( string sText )
		{

			string sCleaned = "";

			if( ( sText != null ) && ( sText.Length > 0 ) )
			{
				sCleaned = sText.ToLower();
				sCleaned = Regex.Replace( sCleaned, "[\\s]+", " ", RegexOptions.Singleline );
				sCleaned = Regex.Replace( sCleaned, "[^\\w\\d]+", " ", RegexOptions.Singleline );
			}

			return( sCleaned );

		}

		/**************************************************************************/

	}

}
