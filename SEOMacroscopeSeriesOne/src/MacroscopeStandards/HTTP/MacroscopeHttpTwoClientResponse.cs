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
using System.Collections.Generic;

namespace SEOMacroscope
{

  public class MacroscopeHttpTwoClientResponse : Macroscope
  {

    /**************************************************************************/

    private HttpResponseMessage Response;
    private HttpContent ResponseContent;
    private byte[] ContentAsBytes;
    private string ContentAsString;

    /**************************************************************************/

    public MacroscopeHttpTwoClientResponse ()
    {
      this.SuppressDebugMsg = false;
    }

    /**************************************************************************/

    public void SetResponse ( HttpResponseMessage RequestResponse )
    {

      this.Response = RequestResponse;

      foreach( KeyValuePair<string, IEnumerable<string>> Item in this.Response.Headers )
      {
        foreach( string Value in Item.Value )
        {
          this.DebugMsg( string.Format( "SETRESPONSE: {0} => {1}", Item.Key, Value ) );
        }
      }

      return;

    }

    public HttpResponseMessage GetResponse ()
    {
      return ( this.Response );
    }

    /**************************************************************************/

    public void SetResponseContent ( HttpContent RequestResponseContent )
    {

      this.ResponseContent = RequestResponseContent;

      foreach( KeyValuePair<string, IEnumerable<string>> Item in this.ResponseContent.Headers )
      {
        foreach( string Value in Item.Value )
        {
          this.DebugMsg( string.Format( "SETRESPONSECONTENT: {0} => {1}", Item.Key, Value ) );
        }
      }

      return;

    }

    public HttpContent GetResponseContent ()
    {
      return ( this.ResponseContent );
    }

    /**************************************************************************/

    public void SetContentAsBytes ( byte[] RequestContentAsBytes )
    {
      this.ContentAsBytes = RequestContentAsBytes;
    }

    public byte[] GetContentAsBytes ()
    {
      return ( this.ContentAsBytes );
    }

    /**************************************************************************/

    public void SetContentAsString ( string RequestContentAsString )
    {
      this.ContentAsString = RequestContentAsString;
    }

    public string GetContentAsString ()
    {
      return ( this.ContentAsString );
    }

    /**************************************************************************/

  }

}
