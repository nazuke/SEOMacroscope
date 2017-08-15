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
    DateTime DateModified;
    DateTime DateServer;
    Boolean Available;

    /**************************************************************************/

    public MacroscopeHrefLang ( string Locale, string Url )
    {

      Boolean CheckHrefLang = MacroscopePreferencesManager.GetCheckHreflangs();

      this.SuppressDebugMsg = true;
      
      this.Locale = Locale;
      this.Url = Url;

      if( CheckHrefLang )
      {
        this.Available = this.Check();
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

    public DateTime GetDateModified ()
    {
      return( this.DateModified );
    }

    /** -------------------------------------------------------------------- **/

    public DateTime GetDateServer ()
    {
      return( this.DateServer );
    }

    /**************************************************************************/

    public Boolean IsAvailable ()
    {
      return( this.Available );
    }

    /**************************************************************************/

    private Boolean Check ()
    {
      
      // TODO: Increase level of detail here. 

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      Boolean IsAvailableCheck = false;

      try
      {

        req = WebRequest.CreateHttp( this.Url );
        req.Method = "HEAD";
        req.Timeout = 10000;
        req.KeepAlive = false;
        req.Host = MacroscopeUrlUtils.GetHostnameFromUrl( this.Url );
        req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        
        MacroscopePreferencesManager.EnableHttpProxy( req );

        using( res = ( HttpWebResponse )req.GetResponse() )
        {

          DebugMsg( string.Format( "MacroscopeHrefLang Status: {0}", res.StatusCode ) );

          if( res.StatusCode == HttpStatusCode.OK )
          {

            IsAvailableCheck = true;

            this.ProcessResponseHttpHeaders( req: req, res: res );

          }
          else
          {
            IsAvailableCheck = false;
          }

          res.Close();
        
        }

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "MacroscopeHrefLang UriFormatException: {0}", ex.Message ) );
      }
      catch( WebException ex )
      {
        DebugMsg( string.Format( "MacroscopeHrefLang WebException: {0}", ex.Message ) );
      }

      return( IsAvailableCheck );

    }

    /**************************************************************************/

    private void ProcessResponseHttpHeaders ( HttpWebRequest req, HttpWebResponse res )
    {

      foreach( string HttpHeaderName in res.Headers )
      {

        if( HttpHeaderName.ToLower().Equals( "date" ) )
        {
          string DateString = res.GetResponseHeader( HttpHeaderName );
          this.DateServer = MacroscopeDateTools.ParseHttpDate( HeaderField: HttpHeaderName, DateString: DateString );
        }

        if( HttpHeaderName.ToLower().Equals( "last-modified" ) )
        {
          string DateString = res.GetResponseHeader( HttpHeaderName );
          this.DateModified = MacroscopeDateTools.ParseHttpDate( HeaderField: HttpHeaderName, DateString: DateString );
        }

      }

      if( this.DateServer.Date == new DateTime ().Date )
      {
        this.DateServer = DateTime.UtcNow;
      }

      if( this.DateModified.Date == new DateTime ().Date )
      {
        this.DateModified = this.DateServer;
      }

    }

    /**************************************************************************/

  }

}
