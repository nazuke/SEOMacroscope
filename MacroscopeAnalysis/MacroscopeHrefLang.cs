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
using System.Net;

namespace SEOMacroscope
{

  /// <summary>
  /// Analyze the HrefLang attributes of an HTML document.
  /// </summary>

  public class MacroscopeHrefLang : Macroscope
  {

    /**************************************************************************/

    string Locale;
    string Url;
    //DateTime DateModified;
    Boolean Available;

    /**************************************************************************/

    public MacroscopeHrefLang ( string Locale, string Url )
    {

      Boolean bCheckHrefLang = MacroscopePreferencesManager.GetCheckHreflangs();

      this.Locale = Locale;
      this.Url = Url;

      if( bCheckHrefLang )
      {
        this.Available = Check();
      }
      else
      {
        this.Available = false;
      }

    }

    /**************************************************************************/

    public string GetLocale ()
    {
      return( this.Locale );
    }

    /**************************************************************************/

    public string GetUrl ()
    {
      return( this.Url );
    }

    /**************************************************************************/

    public Boolean IsAvailable ()
    {
      return( this.Available );
    }

    /**************************************************************************/

    Boolean Check ()
    {

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      Boolean bAvailable = false;

      try
      {

        req = WebRequest.CreateHttp( this.Url );
        req.Method = "HEAD";
        req.Timeout = 10000;
        req.KeepAlive = false;
        MacroscopePreferencesManager.EnableHttpProxy( req );

        res = ( HttpWebResponse )req.GetResponse();

        DebugMsg( string.Format( "MacroscopeHrefLang Status: {0}", res.StatusCode ) );

        if( res.StatusCode == HttpStatusCode.OK )
        {
          bAvailable = true;
        }

        res.Close();

      }
      catch( WebException ex )
      {
        DebugMsg( string.Format( "MacroscopeHrefLang WebException: {0}", ex.Message ) );
      }

      return( bAvailable );

    }

    /**************************************************************************/

  }

}
