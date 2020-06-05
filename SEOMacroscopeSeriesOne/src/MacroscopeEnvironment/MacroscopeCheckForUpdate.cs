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
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  public class MacroscopeCheckForUpdate : Macroscope
  {

    /**************************************************************************/

    public MacroscopeCheckForUpdate ()
    {
      this.SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public async Task<bool> PhoneHome ()
    {

      bool NewVersionAvailable = false;
      MacroscopeHttpUrlLoader UrlLoader = new MacroscopeHttpUrlLoader();
      MacroscopeHttpTwoClient Client = new MacroscopeHttpTwoClient();
      Uri TargetUri = new Uri( MacroscopeConstants.CheckForUpdateUrl );
      byte[] Data = await UrlLoader.LoadImmediateDataFromUrl( Client: Client, TargetUri: TargetUri );
      string PublishedVersion = System.Text.Encoding.UTF8.GetString( Data );
      string CurrentVersion = Macroscope.GetVersion();
      bool CheckResult = this.IsVersionNewer( CurrentVersion: CurrentVersion, CompareVersion: PublishedVersion );

      if( CheckResult )
      {
        NewVersionAvailable = true;
      }

      return ( NewVersionAvailable );

    }

    /**************************************************************************/

    public bool IsVersionNewer ( string CurrentVersion, string CompareVersion )
    {

      bool IsNewer = false;

      try
      {

        int[] ParsedCurrentVersion = this.ParseVersionNumber( VersionString: CurrentVersion );
        int[] ParsedCompareVersion = this.ParseVersionNumber( VersionString: CompareVersion );

        if( ParsedCompareVersion[ 0 ] > ParsedCurrentVersion[ 0 ] )
        {
          IsNewer = true;
        }
        else if( ParsedCompareVersion[ 0 ] == ParsedCurrentVersion[ 0 ] )
        {

          if( ParsedCompareVersion[ 1 ] > ParsedCurrentVersion[ 1 ] )
          {
            IsNewer = true;
          }
          else if( ParsedCompareVersion[ 1 ] == ParsedCurrentVersion[ 1 ] )
          {
            if( ParsedCompareVersion[ 2 ] > ParsedCurrentVersion[ 2 ] )
            {
              IsNewer = true;
            }
            else if( ParsedCompareVersion[ 2 ] == ParsedCurrentVersion[ 2 ] )
            {
              if( ParsedCompareVersion[ 3 ] > ParsedCurrentVersion[ 3 ] )
              {
                IsNewer = true;
              }
            }
          }

        }

      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "IsVersionNewer: {0}", ex.Message ) );
      }

      return ( IsNewer );

    }

    /**************************************************************************/

    public int[] ParseVersionNumber ( string VersionString )
    {

      int[] VersionElements = new int[ 4 ];
      MatchCollection matches;

      matches = Regex.Matches( VersionString, @"^([0-9]+)\.([0-9]+)\.([0-9]+)\.([0-9]+)$" );

      foreach( Match MatchedElement in matches )
      {

        for( int i = 0 ; i < 4 ; i++ )
        {

          if( !Int32.TryParse( MatchedElement.Groups[ i + 1 ].Value, out VersionElements[ i ] ) )
          {
            throw new Exception( "Invalid version string." );
          }
        }

      }

      return ( VersionElements );

    }

    /**************************************************************************/

  }

}
