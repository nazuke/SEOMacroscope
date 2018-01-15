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

    Dictionary<string, bool> Hostnames;

    /**************************************************************************/

    public MacroscopeAllowedHosts ()
    {
      this.SuppressDebugMsg = true;
      this.Hostnames = new Dictionary<string, bool>( 32 );
    }

    /**************************************************************************/

    public void Add ( string Hostname )
    {
      if ( !this.Hostnames.ContainsKey( Hostname ) )
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

      Uri FromUrl = null;

      try
      {
        FromUrl = new Uri( Url, UriKind.Absolute );
      }
      catch ( UriFormatException ex )
      {
        DebugMsg( string.Format( "AddFromUrl: {0}", ex.Message ) );
      }
      catch ( Exception ex )
      {
        DebugMsg( string.Format( "AddFromUrl: {0}", ex.Message ) );
      }

      if ( FromUrl != null )
      {
        this.Add( FromUrl.Host );
      }

    }

    /**************************************************************************/

    public void Remove ( string Hostname )
    {
      if ( this.Hostnames.ContainsKey( Hostname ) )
      {
        lock ( this.Hostnames )
        {
          this.Hostnames.Remove( key: Hostname );
        }
      }
    }

    /**************************************************************************/

    public void RemoveFromUrl ( string Url )
    {

      Uri FromUrl = null;

      try
      {
        FromUrl = new Uri( Url, UriKind.Absolute );
      }
      catch ( UriFormatException ex )
      {
        DebugMsg( string.Format( "RemoveFromUrl: {0}", ex.Message ) );
      }
      catch ( Exception ex )
      {
        DebugMsg( string.Format( "RemoveFromUrl: {0}", ex.Message ) );
      }

      if ( FromUrl != null )
      {
        this.Remove( Hostname: FromUrl.Host );
      }

    }

    /**************************************************************************/

    public int Count ()
    {
      return ( this.Hostnames.Count );
    }

    /**************************************************************************/

    public Dictionary<string, bool> ListHostnames ()
    {

      Dictionary<string, bool> HostnamesCopy = new Dictionary<string, bool>( this.Hostnames.Count );

      lock ( this.Hostnames )
      {

        foreach ( string Url in this.Hostnames.Keys )
        {
          HostnamesCopy.Add( Url, this.Hostnames[ Url ] );
        }

      }

      return ( HostnamesCopy );

    }

    /**************************************************************************/

    public IEnumerable<KeyValuePair<string, bool>> IterateHostnames ()
    {

      lock ( this.Hostnames )
      {

        foreach ( string Url in this.Hostnames.Keys )
        {

          yield return new KeyValuePair<string, bool>( Url, this.Hostnames[ Url ] );

        }

      }

    }

    /**************************************************************************/

    public void Allow ( string Hostname )
    {
      if ( this.Hostnames.ContainsKey( Hostname ) )
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
      if ( this.Hostnames.ContainsKey( Hostname ) )
      {
        this.Hostnames[ Hostname ] = false;
      }
      else
      {
        this.Hostnames.Add( Hostname, false );
      }
    }

    /**************************************************************************/

    public bool IsAllowed ( string Hostname )
    {

      bool HostIsAllowed = false;

      if ( this.Hostnames.ContainsKey( Hostname ) )
      {
        HostIsAllowed = this.Hostnames[ Hostname ];
      }

      return ( HostIsAllowed );

    }

    /**************************************************************************/

    public bool IsAllowedFromUrl ( string Url )
    {

      Uri FromUri = null;
      bool Allowed = false;

      try
      {
        FromUri = new Uri( Url, UriKind.Absolute );
      }
      catch ( UriFormatException ex )
      {
        DebugMsg( string.Format( "IsAllowedFromUrl 1: {0}", ex.Message ) );
      }
      catch ( Exception ex )
      {
        DebugMsg( string.Format( "IsAllowedFromUrl 2: {0}", ex.Message ) );
      }

      if ( FromUri != null )
      {
        Allowed = this.IsAllowed( FromUri.Host );
      }

      return ( Allowed );
    }

    /**************************************************************************/

    public bool IsInternalUrl ( string Url )
    {

      bool IsInternal = false;

      if ( Url != null )
      {

        Uri DocumentUrl = null;

        try
        {
          DocumentUrl = new Uri( Url, UriKind.Absolute );
        }
        catch ( UriFormatException ex )
        {
          DebugMsg( string.Format( "IsInternalUrl: {0}", ex.Message ) );
        }
        catch ( Exception ex )
        {
          DebugMsg( string.Format( "IsInternalUrl: {0}", ex.Message ) );
        }

        if ( DocumentUrl != null )
        {
          if ( this.IsAllowed( DocumentUrl.Host ) )
          {
            IsInternal = true;
          }
        }

      }

      return ( IsInternal );

    }

    /**************************************************************************/

    public bool IsExternalUrl ( string Url )
    {

      bool IsExternal = false;

      if ( Url != null )
      {

        Uri DocumentUrl = null;

        try
        {
          DocumentUrl = new Uri( Url, UriKind.Absolute );
        }
        catch ( UriFormatException ex )
        {
          DebugMsg( string.Format( "IsExternalUrl: {0}", ex.Message ) );
        }
        catch ( Exception ex )
        {
          DebugMsg( string.Format( "IsExternalUrl: {0}", ex.Message ) );
        }

        if ( DocumentUrl != null )
        {
          if ( !this.IsAllowed( Hostname: DocumentUrl.Host ) )
          {
            IsExternal = true;
          }
        }

      }

      return ( IsExternal );

    }

    /**************************************************************************/

    public static string ParseHostnameFromUrl ( string Url )
    {

      string Hostname = null;
      Uri DocumentUrl = null;

      if ( !string.IsNullOrEmpty( Url ) )
      {

        try
        {
          DocumentUrl = new Uri( Url, UriKind.Absolute );
        }
        catch ( UriFormatException ex )
        {
          DebugMsg( string.Format( "ParseHostnameFromUrl: {0}", ex.Message ), true );
        }
        catch ( Exception ex )
        {
          DebugMsg( string.Format( "ParseHostnameFromUrl: {0}", ex.Message ), true );
        }

        if ( DocumentUrl != null )
        {
          Hostname = DocumentUrl.Host;
        }

      }

      return ( Hostname );

    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    public void DumpAllowedHosts ()
    {
      lock ( this.Hostnames )
      {
        foreach ( string Url in this.Hostnames.Keys )
        {
          DebugMsg( string.Format( "ALLOWED_HOST: {0}", Url ) );
        }
      }
    }

    /**************************************************************************/

  }

}
