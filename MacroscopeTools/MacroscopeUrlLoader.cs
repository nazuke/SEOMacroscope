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
using System.IO;
using System.Net;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeUrlLoader.
  /// </summary>

  public class MacroscopeUrlLoader : Macroscope
  {

    /**************************************************************************/

    public MacroscopeUrlLoader ()
    {
    }

    /**************************************************************************/

    public MemoryStream LoadMemoryStreamFromUrl ( string Url, List<string> Expects )
    {

      // TODO: List of expected mime types

      MemoryStream StreamLoader = null;

      return( StreamLoader );

    }

    /**************************************************************************/

    public MemoryStream LoadMemoryStreamFromUrl ( string Url )
    {

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      MemoryStream msStream = null;
      
      try
      {

        req = WebRequest.CreateHttp( Url );
        req.Method = "GET";
        req.Timeout = 1000;
        req.KeepAlive = false;
        req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        
        MacroscopePreferencesManager.EnableHttpProxy( req );

        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "LoadFromUrl :: UriFormatException: {0}", ex.Message ) );
      }
      catch( WebException ex )
      {

        this.DebugMsg( string.Format( "LoadFromUrl :: WebException: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "LoadFromUrl :: WebException: {0}", Url ) );

      }

      if( res != null )
      {

        try
        {

          Stream sStream = res.GetResponseStream();
          List<byte> aRawDataList = new List<byte> ();
          Byte [] aRawData = new Byte[0];

          do
          {
            int buf = sStream.ReadByte();
            if( buf > -1 )
            {
              aRawDataList.Add( ( byte )buf );
            }
            else
            {
              break;
            }
          } while( sStream.CanRead );

          aRawData = aRawDataList.ToArray();


          if( aRawData.Length > 0 )
          {
            msStream = new MemoryStream ( aRawData );
          }

        }
        catch( WebException ex )
        {

          this.DebugMsg( string.Format( "LoadFromUrl :: WebException: {0}", ex.Message ) );

        }

        res.Close();
        
        res.Dispose();

      }

      return( msStream );

    }

    /**************************************************************************/

  }

}
