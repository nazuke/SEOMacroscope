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
using System.Security.Cryptography;
using System.Text;

namespace SEOMacroscope
{
  
  /// <summary>
  /// MacroscopeCredentialsHttp manages the credential requests queue, and the collection of credentials entered for this crawl session.
  /// </summary>
	
  public class MacroscopeCredentialsHttp : Macroscope
  {
	
    /**************************************************************************/

    private Queue<MacroscopeCredentialRequest> CredentialRequests;

    private Dictionary<string,MacroscopeCredential> Credentials;
    private Dictionary<string,string> Memo;

    /**************************************************************************/

    public MacroscopeCredentialsHttp ()
    {
      this.SuppressDebugMsg = true;
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

      string Key = null;
      string QuickKey = string.Join( "::", Domain, Realm );

      if( this.Memo.ContainsKey( QuickKey ) )
      {
        Key = this.Memo[ QuickKey ];
      }
      else
      {

        HashAlgorithm Digest = HashAlgorithm.Create( "SHA256" );
        byte [] BytesIn = Encoding.UTF8.GetBytes( QuickKey );
        byte [] Hashed = Digest.ComputeHash( BytesIn );
        StringBuilder Buf = new StringBuilder ();

        for( int i = 0 ; i < Hashed.Length ; i++ )
        {
          Buf.Append( Hashed[ i ].ToString( "X2" ) );
        }

        Key = Buf.ToString();
        this.Memo[ QuickKey ] = Key;

      }

      return( Key );
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

    public bool PeekCredentialRequest ()
    {

      bool Result = false;

      lock( this.CredentialRequests )
      {

        try
        {
          
          if( ( this.CredentialRequests.Count > 0 )
              && ( this.CredentialRequests.Peek() != null ) )
          {
            Result = true;
          }
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "PeekCredentialRequest: {0}", ex.Message ) );
        }

      }

      return( Result );

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
          DebugMsg( string.Format( "DequeueCredentialRequest: {0}", ex.Message ) );
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

      string Key = this.GenerateKey( Domain, Realm );
      
      lock( this.Credentials )
      {

        if( this.Credentials.ContainsKey( Key ) )
        {
          this.Credentials.Remove( Key );
        }

        MacroscopeCredential Credential = new MacroscopeCredential (
                                            CredentialsHttp: this,
                                            Domain: Domain,
                                            Realm: Realm,
                                            Username: Username,
                                            Password: Password
                                          );

        this.Credentials.Add( Key, Credential );

      }

    }

    /* ---------------------------------------------------------------------- */

    public bool CredentialExists ( string Domain, string Realm )
    {

      string Key = this.GenerateKey( Domain, Realm );
      bool Result = false;
      
      lock( this.Credentials )
      {

        if( this.Credentials.ContainsKey( Key ) )
        {
          Result = true;
        }

      }
      
      return( Result );
      
    }

    /* ---------------------------------------------------------------------- */
    
    public void RemoveCredential ( string Domain, string Realm )
    {

      string Key = this.GenerateKey( Domain, Realm );
            
      lock( this.Credentials )
      {

        if( this.Credentials.ContainsKey( Key ) )
        {
          this.Credentials.Remove( Key );
        }

      }
      
    }

    /* ---------------------------------------------------------------------- */
        
    public MacroscopeCredential GetCredential ( string Domain, string Realm )
    {

      string Key = this.GenerateKey( Domain, Realm );
      MacroscopeCredential Credential = null;

      lock( this.Credentials )
      {

        if( this.Credentials.ContainsKey( Key ) )
        {
          Credential = this.Credentials[ Key ];
        }

      }
      
      return( Credential );

    }
    
    /**************************************************************************/

  }

}
