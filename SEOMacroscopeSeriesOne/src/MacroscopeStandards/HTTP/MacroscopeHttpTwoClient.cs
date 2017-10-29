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
using System.Net.Http;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  public class MacroscopeHttpTwoClient : Macroscope
  {

    // TODO: finish this class

    /**************************************************************************/

    private static HttpClient Client;

    /**************************************************************************/

    static MacroscopeHttpTwoClient ()
    {

      Client = new HttpClient( new WinHttpHandler() );

    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeHttpTwoClient ()
    {

      this.SuppressDebugMsg = false;

    }

    /**************************************************************************/

    public HttpResponseMessage Head ()
    {

      // TODO: Implement this

      HttpResponseMessage Response = null;



      return( Response );

    }

    /**************************************************************************/

    public async Task<MacroscopeHttpTwoClientResponse> Get ( string Url, Action<HttpRequestMessage> ConfigureRequestHeaders )
    {

      Uri DocumentUri = new Uri( Url );
      MacroscopeHttpTwoClientResponse ClientResponse = new MacroscopeHttpTwoClientResponse();

      using( HttpRequestMessage Request = new HttpRequestMessage( HttpMethod.Get, DocumentUri ) )
      {

        Request.Version = new Version( 2, 0 );
        Request.Headers.Host = DocumentUri.Host;
        Request.Headers.Add( "User-Agent", this.UserAgent() );

        ConfigureRequestHeaders( Request );

        try
        {

          using( HttpResponseMessage Response = await Client.SendAsync( Request ) )
          {
            using( HttpContent ResponseContent = Response.Content )
            {
              
              // TODO: add options to get string and/or bytes[] here:

              ClientResponse.SetContentAsString( ResponseContent.ReadAsStringAsync().Result);

            }

          }
        }
        catch( TimeoutException ex )
        {
          this.DebugMsg( ex.Message );
        }

      }

      return ( ClientResponse );

      /*
      req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

      MacroscopePreferencesManager.EnableHttpProxy( req );

      this.PrepareRequestHttpHeaders( RobotsUri: RobotsUri, req: req );

      */

    }

    /**************************************************************************/

    public HttpResponseMessage Post ()
    {

      // TODO: Implement this

      HttpResponseMessage Response = null;



      return ( Response );

    }

    /**************************************************************************/

  }

}
