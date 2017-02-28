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

namespace SEOMacroscope
{
  
  /// <summary>
  /// Description of MacroscopeCredential.
  /// </summary>
  
  public class MacroscopeCredential
  {
  
    /**************************************************************************/

    private MacroscopeCredentialsHttp CredentialsHttp;
    private string Domain;
    private string Realm;
    private string Username;
    private string Password;

    /**************************************************************************/

    public MacroscopeCredential (
      MacroscopeCredentialsHttp CredentialsHttp,
      string Domain,
      string Realm,
      string Username,
      string Password
    )
    {
      this.CredentialsHttp = CredentialsHttp;
      this.Domain = Domain;
      this.Realm = Realm;
      this.Username = Username;
      this.Password = Password;
    }

    /**************************************************************************/

    public MacroscopeCredentialsHttp GetCredentialsHttp ()
    {
      return( this.CredentialsHttp );
    }
    
    /**************************************************************************/

    public string GetDomain ()
    {
      return( this.Domain );
    }

    /**************************************************************************/

    public string GetRealm ()
    {
      return( this.Realm );
    }

    /**************************************************************************/

    public string GetUsername ()
    {
      return( this.Username );
    }

    /**************************************************************************/

    public string GetPassword ()
    {
      return( this.Password );
    }

    /**************************************************************************/

  }

}
