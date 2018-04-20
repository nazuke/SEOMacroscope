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
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace SEOMacroscope
{

  public class MacroscopeHttpTwoClientResponse : Macroscope
  {

    /**************************************************************************/

    private HttpResponseMessage Response;
    private HttpContent ResponseContent;
    private SortedDictionary<string, List<string>> ConsolidatedHttpHeaders;
    private byte[] ContentAsBytes;

    /**************************************************************************/

    public MacroscopeHttpTwoClientResponse ()
    {
      this.SuppressDebugMsg = true;
      this.ConsolidatedHttpHeaders = new SortedDictionary<string, List<string>>();
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

    }

    /** -------------------------------------------------------------------- **/

    public HttpResponseMessage GetResponse ()
    {
      return ( this.Response );
    }

    /**************************************************************************/

    public MediaTypeHeaderValue GetMimeType ()
    {

      MediaTypeHeaderValue MimeType = this.Response.Content.Headers.ContentType;

      return ( MimeType );

    }

    /**************************************************************************/

    public void AddConsolidatedHttpHeader ( string Name, string Value )
    {
      lock( this.ConsolidatedHttpHeaders )
      {
        if( this.ConsolidatedHttpHeaders.ContainsKey( Name ) )
        {
          this.ConsolidatedHttpHeaders[ Name ].Add( Value );
        }
        else
        {
          this.ConsolidatedHttpHeaders.Add( Name, new List<string>() { Value } );
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<string> IterateConsolidatedHttpHeaders ()
    {
      lock( this.ConsolidatedHttpHeaders )
      {
        foreach( string Name in this.ConsolidatedHttpHeaders.Keys )
        {
          yield return ( Name );
        }
      }
    }

    /** -------------------------------------------------------------------- **/

    public IEnumerable<string> IterateConsolidatedHttpHeaderValues ( string Name )
    {
      lock( this.ConsolidatedHttpHeaders[ Name ] )
      {
        foreach( string Value in this.ConsolidatedHttpHeaders[ Name ] )
        {
          yield return ( Value );
        }
      }
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

    /** -------------------------------------------------------------------- **/

    public HttpContent GetResponseContent ()
    {
      return ( this.ResponseContent );
    }

    /**************************************************************************/

    public long GetContentLength ()
    {
      return ( this.ContentAsBytes.LongLength );
    }

    /**************************************************************************/

    public void SetContentAsBytes ( byte[] RequestContentAsBytes )
    {
      this.ContentAsBytes = RequestContentAsBytes;
    }

    /** -------------------------------------------------------------------- **/

    public byte[] GetContentAsBytes ()
    {
      return ( this.ContentAsBytes );
    }

    /**************************************************************************/

    public string GetContentAsString ()
    {
      return ( Encoding.UTF8.GetString( this.ContentAsBytes ) );
    }

    /** -------------------------------------------------------------------- **/

    public string GetContentAsString ( Encoding WithEncoding )
    {
      return ( WithEncoding.GetString( this.ContentAsBytes ) );
    }

    /**************************************************************************/

  }

}
