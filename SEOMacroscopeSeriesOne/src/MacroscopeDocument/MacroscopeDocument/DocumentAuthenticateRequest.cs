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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDocument.
  /// </summary>

  [Serializable()]
  public partial class MacroscopeDocument : Macroscope
  {

    // TODO: Verify that authentication works properly

    /** Authenticate Request **************************************************/

    private void AuthenticateRequest ( HttpRequestMessage Request )
    {

      // Reference: https://en.wikipedia.org/wiki/Basic_access_authentication#Protocol

      bool IsAuthenticating = false;
      byte[] UsernamePassword;
      string UsernamePasswordB64Encoded;

      if( this.GetAuthenticationCredential() != null )
      {

        UsernamePassword = Encoding.UTF8.GetBytes(
          string.Join(
            ":",
            this.GetAuthenticationCredential().GetUsername(),
            this.GetAuthenticationCredential().GetPassword()
          )
        );

        UsernamePasswordB64Encoded = System.Convert.ToBase64String( UsernamePassword );

        if( Request != null )
        {
          Request.Headers.Add( HttpRequestHeader.Authorization.ToString(), string.Join( " ", "Basic", UsernamePasswordB64Encoded ) );
          IsAuthenticating = true;
        }

      }

      if ( IsAuthenticating )
      {
        this.VerifyOrPurgeCredential();
      }

      return;

    }

    /** -------------------------------------------------------------------- **/

    private void VerifyOrPurgeCredential ()
    {

      if( this.GetAuthenticationCredential() != null )
      {

        this.DebugMsg( string.Format( "VerifyCredential: {0}", this.GetStatusCode() ) );

        if( this.GetStatusCode() == HttpStatusCode.Unauthorized )
        {

          MacroscopeCredential Credential = this.GetAuthenticationCredential();

          Credential.GetCredentialsHttp().RemoveCredential(
            Credential.GetDomain(),
            Credential.GetRealm()
          );

        }

      }

    }

    /**************************************************************************/

  }

}
