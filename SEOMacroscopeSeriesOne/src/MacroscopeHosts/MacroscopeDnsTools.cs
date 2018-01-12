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
using System.Net;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDnsTools.
  /// </summary>

  public class MacroscopeDnsTools : Macroscope
  {

    /**************************************************************************/

    public MacroscopeDnsTools ()
    {
      this.SuppressDebugMsg = true;
    }

    /** Check Hostnames *******************************************************/

    public static bool CheckValidHostname ( string Url )
    {

      bool Success = false;
      string Hostname = null;
      Uri RobotUri = null;

      try
      {
        RobotUri = new Uri ( Url, UriKind.Absolute );
        Hostname = RobotUri.Host;
      }
      catch( InvalidOperationException ex )
      {
        DebugMsg( string.Format( "CheckValidHostname: {0}", ex.Message ), true );
      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "CheckValidHostname: {0}", ex.Message ), true );
      }

      if( Hostname != null )
      {
        IPHostEntry ip = null;
        try
        {
          ip = Dns.GetHostEntry( Hostname );
        }
        catch( System.Net.Sockets.SocketException ex )
        {
          DebugMsg( string.Format( "CheckValidHostname SocketException: {0}", ex.Message ), true );
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "CheckValidHostname Exception: {0}", ex.Message ), true );
        }
        if( ip != null )
        {
          Success = true;
        }
      }

      return( Success );

    }

    /**************************************************************************/

  }

}
