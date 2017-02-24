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
using System.Diagnostics;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeAllowedHosts.
  /// </summary>

  public class MacroscopeAllowedHosts : Macroscope
  {

    /**************************************************************************/

    Dictionary<string,Boolean> Hostnames;

    MacroscopeDomainWrangler DomainWrangler;

    /**************************************************************************/

    public MacroscopeAllowedHosts ()
    {

      this.Hostnames = new Dictionary<string,Boolean> ( 32 );

      this.DomainWrangler = new MacroscopeDomainWrangler ();

    }

    /**************************************************************************/

    public void Add ( string Hostname )
    {
      if( !this.Hostnames.ContainsKey( Hostname ) )
      {
        this.Hostnames.Add( Hostname, true );
      }
      else
      {
        this.Hostnames[ Hostname ] = true;
      }
    }

    /**************************************************************************/

    public void AddFromUrl ( string Url )
    {
      Uri uFromUrl = new Uri ( Url, UriKind.Absolute );
      this.Add( uFromUrl.Host );
    }

    /**************************************************************************/

    public void Remove ( string Hostname )
    {
      if( !this.Hostnames.ContainsKey( Hostname ) )
      {
        this.Hostnames.Remove( Hostname );
      }
    }

    /**************************************************************************/

    public void RemoveFromUrl ( string Url )
    {
      Uri uFromUrl = new Uri ( Url, UriKind.Absolute );
      this.Remove( uFromUrl.Host );
    }

    /**************************************************************************/

    public void Allow ( string Hostname )
    {
      if( this.Hostnames.ContainsKey( Hostname ) )
      {
        this.Hostnames[ Hostname ] = true;
      }
      else
      {
        this.Hostnames.Add( Hostname, true );
      }
    }

    /**************************************************************************/

    public void Disallow ( string Hostname )
    {
      if( this.Hostnames.ContainsKey( Hostname ) )
      {
        this.Hostnames[ Hostname ] = false;
      }
      else
      {
        this.Hostnames.Add( Hostname, false );
      }
    }

    /**************************************************************************/

    public Boolean IsAllowed ( string Hostname )
    {
      Boolean bIsAllowed = false;
      if( this.Hostnames.ContainsKey( Hostname ) )
      {
        bIsAllowed = this.Hostnames[ Hostname ];
      }
      return( bIsAllowed );
    }

    /**************************************************************************/

    public Boolean IsAllowedFromUrl ( string Url )
    {
      Uri uFromUrl = new Uri ( Url, UriKind.Absolute );
      return( this.IsAllowed( uFromUrl.Host ) );
    }

    /**************************************************************************/

    public Boolean IsInternalUrl ( string Url )
    {
      Uri uUrl = new Uri ( Url, UriKind.Absolute );
      Boolean bIsInternal = false;
      if( this.IsAllowed( uUrl.Host ) )
      {
        bIsInternal = true;
      }
      return( bIsInternal );
    }

    /**************************************************************************/

    public Boolean IsExternalUrl ( string Url )
    {
      Uri uUrl = new Uri ( Url, UriKind.Absolute );
      Boolean bIsExternal = false;
      if( !this.IsAllowed( uUrl.Host ) )
      {
        bIsExternal = true;
      }
      return( bIsExternal );
    }

    /**************************************************************************/

    public static string ParseHostnameFromUrl ( string Url )
    {
      Uri uUrl = new Uri ( Url, UriKind.Absolute );
      string sHostname = uUrl.Host;
      return( sHostname );
    }

    /**************************************************************************/

    public Boolean IsWithinAllowedDomain ( string Hostname )
    {
      // TODO: This does not work.
      Boolean bAllowed = false;
      lock( this.Hostnames )
      {
        foreach( string sAllowedHostname in this.Hostnames.Keys )
        {
          if( this.DomainWrangler.IsWithinSameDomain( Hostname, sAllowedHostname ) )
          {
            bAllowed = true;
            break;
          }
        }
      }
      return( bAllowed );
    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    public void DumpAllowedHosts ()
    {
      lock( this.Hostnames )
      {
        foreach( string sUrl in this.Hostnames.Keys )
        {
          DebugMsg( string.Format( "ALLOWED_HOST: {0}", sUrl ) );
        }
      }
    }

    /**************************************************************************/

  }

}
