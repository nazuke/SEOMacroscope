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
using System.Security.Cryptography;
using System.Text;

namespace SEOMacroscope
{
  
  /// <summary>
  /// MacroscopeCredentialsHttp manages the credential requests queue, and the collection of credentials entered for this crawl session.
  /// </summary>
	
  public class MacroscopeCredentialsHttp
  {
	
    /**************************************************************************/

    private Queue<MacroscopeCredentialRequest> CredentialRequests;

    private Dictionary<string,MacroscopeCredential> Credentials;
    private Dictionary<string,string> Memo;

    /**************************************************************************/

    public MacroscopeCredentialsHttp ()
    {
      this.CredentialRequests = new Queue<MacroscopeCredentialRequest> ( 16 );
      this.Credentials = new Dictionary<string,MacroscopeCredential> ( 16 );
      this.Memo = new Dictionary<string,string> ( 16 );
    }

    /**************************************************************************/

    public void ClearAll ()
    {
      this.ClearCredentialRequests();
      this.ClearCredentials();
    }

    /**************************************************************************/

    private string GenerateKey ( string Domain, string Realm )
    {

      string sKey = null;
      string sQuickKey = string.Join( "::", Domain, Realm );

      if( this.Memo.ContainsKey( sQuickKey ) )
      {
        sKey = this.Memo[ sQuickKey ];
      }
      else
      {

        HashAlgorithm Digest = HashAlgorithm.Create( "SHA256" );
        byte [] BytesIn = Encoding.UTF8.GetBytes( sQuickKey );
        byte [] Hashed = Digest.ComputeHash( BytesIn );
        StringBuilder sbString = new StringBuilder ();

        for( int i = 0 ; i < Hashed.Length ; i++ )
        {
          sbString.Append( Hashed[ i ].ToString( "X2" ) );
        }

        sKey = sbString.ToString();
        this.Memo[ sQuickKey ] = sKey;

      }

      return( sKey );
    }

    public string TestGenerateKey ( string Domain, string Realm )
    {
      return( this.GenerateKey( Domain, Realm ) );
    }

    /** Credential Requests ***************************************************/

    public void ClearCredentialRequests ()
    {
      lock( this.CredentialRequests )
      {
        this.CredentialRequests.Clear();
      }
    }

    /* ---------------------------------------------------------------------- */

    public void EnqueueCredentialRequest ( string Domain, string Realm, string Url )
    {

      lock( this.CredentialRequests )
      {

        MacroscopeCredentialRequest CredentialRequest = new MacroscopeCredentialRequest ( Domain, Realm, Url );

        this.CredentialRequests.Enqueue( CredentialRequest );

      }

    }

    /* ---------------------------------------------------------------------- */

    public Boolean PeekCredentialRequest ()
    {

      Boolean bResult = false;

      lock( this.CredentialRequests )
      {

        try
        {
          if( this.CredentialRequests.Peek() != null )
          {
            bResult = true;
          }
        }
        catch( Exception ex )
        {
          ;
        }

      }

      return( bResult );

    }

    /* ---------------------------------------------------------------------- */

    public MacroscopeCredentialRequest DequeueCredentialRequest ()
    {

      MacroscopeCredentialRequest CredentialRequest = null;

      lock( this.CredentialRequests )
      {

        try
        {
          if( this.CredentialRequests.Peek() != null )
          {
            CredentialRequest = this.CredentialRequests.Dequeue();
          }
        }
        catch( Exception ex )
        {
          ;
        }

      }

      return( CredentialRequest );

    }

    /** Credentials ***********************************************************/

    public void ClearCredentials ()
    {
      lock( this.Credentials )
      {
        this.Credentials.Clear();
        lock( this.Memo )
        {
          this.Memo.Clear();
        }
      }
    }

    /* ---------------------------------------------------------------------- */
    
    
    public void AddCredential ( string Domain, string Realm, string Username, string Password )
    {

      string sKey = this.GenerateKey( Domain, Realm );
      
      lock( this.Credentials )
      {

        if( this.Credentials.ContainsKey( sKey ) )
        {
          this.Credentials.Remove( sKey );
        }

        MacroscopeCredential Credential = new MacroscopeCredential (
                                            CredentialsHttp: this,
                                            Domain: Domain,
                                            Realm: Realm,
                                            Username: Username,
                                            Password: Password
                                          );

        this.Credentials.Add( sKey, Credential );

      }

    }

    /* ---------------------------------------------------------------------- */

    public Boolean CredentialExists ( string Domain, string Realm )
    {

      string sKey = this.GenerateKey( Domain, Realm );
      Boolean bResult = false;
      
      lock( this.Credentials )
      {

        if( this.Credentials.ContainsKey( sKey ) )
        {
          bResult = true;
        }

      }
      
      return( bResult );
      
    }

    /* ---------------------------------------------------------------------- */
    
    public void RemoveCredential ( string Domain, string Realm )
    {

      string sKey = this.GenerateKey( Domain, Realm );
            
      lock( this.Credentials )
      {

        if( this.Credentials.ContainsKey( sKey ) )
        {
          this.Credentials.Remove( sKey );
        }

      }
      
    }

    /* ---------------------------------------------------------------------- */
        
    public MacroscopeCredential GetCredential ( string Domain, string Realm )
    {

      string sKey = this.GenerateKey( Domain, Realm );
      MacroscopeCredential Credential = null;

      lock( this.Credentials )
      {

        if( this.Credentials.ContainsKey( sKey ) )
        {
          Credential = this.Credentials[ sKey ];
        }

      }
      
      return( Credential );

    }
    
    /**************************************************************************/

  }

}
