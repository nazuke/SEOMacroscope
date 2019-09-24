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

namespace SEOMacroscope
{

  public class MacroscopeHumans : Macroscope
  {

    /** http://humanstxt.org/ *************************************************/

    public MacroscopeHumans ()
    {
      this.SuppressDebugMsg = true;
    }

    /** Generate Humans URL ***************************************************/

    public static string GenerateHumansUrl ( string Url )
    {

      string HumansUrl = null;
      Uri BaseUri = null;
      string BaseUriPort = "";
      Uri HumansUri = null;
      string HumansTxtUrl = null;

      DebugMsgStatic( string.Format( "HUMANS Disabled: {0}", Url ) );

      try
      {

        BaseUri = new Uri( Url, UriKind.Absolute );

        if ( BaseUri.Port > 0 )
        {
          BaseUriPort = string.Format( ":{0}", BaseUri.Port );
        }

        HumansUri = new Uri(
          string.Format(
            "{0}://{1}{2}{3}",
            BaseUri.Scheme,
            BaseUri.Host,
            BaseUriPort,
            "/humans.txt"
          ),
          UriKind.Absolute
        );

        HumansTxtUrl = HumansUri.ToString();

      }
      catch ( InvalidOperationException ex )
      {
        DebugMsgStatic( string.Format( "GenerateHumansUrl: {0}", ex.Message ) );
      }
      catch ( UriFormatException ex )
      {
        DebugMsgStatic( string.Format( "GenerateHumansUrl: {0}", ex.Message ) );
      }

      if ( !string.IsNullOrEmpty( HumansTxtUrl ) )
      {
        HumansUrl = HumansTxtUrl;
      }


      return ( HumansUrl );

    }

    /**************************************************************************/

  }

}
