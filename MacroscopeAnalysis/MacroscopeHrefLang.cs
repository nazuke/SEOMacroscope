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

	public class MacroscopeHrefLang : Macroscope
	{

		/**************************************************************************/

		Boolean EnableProbe;
		string Locale;
		string Url;
		//DateTime DateModified;
		Boolean Available;
		
		/**************************************************************************/

		public MacroscopeHrefLang ( Boolean bProbe, string sLocale, string sURL )
		{
			EnableProbe = bProbe;
			Locale = sLocale;	
			Url = sURL;
			if( bProbe ) {
				Available = Probe();
			} else {
				Available = false;
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

		Boolean Probe ()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bAvailable = false;

			try {

				req = WebRequest.CreateHttp( this.Url );
				req.Method = "HEAD";
				req.Timeout = 10000;
				req.KeepAlive = false;
				MacroscopePreferencesManager.EnableHttpProxy( req );

				res = ( HttpWebResponse )req.GetResponse();

				debug_msg( string.Format( "MacroscopeHrefLang Status: {0}", res.StatusCode ) );

				if( res.StatusCode == HttpStatusCode.OK ) {
					bAvailable = true;
				}

				res.Close();

			} catch( WebException ex ) {
				debug_msg( string.Format( "MacroscopeHrefLang WebException: {0}", ex.Message ) );
			}

			return( bAvailable );

		}

		/**************************************************************************/

	}

}
