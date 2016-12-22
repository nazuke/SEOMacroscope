using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace SEOMacroscope
{

	public class MacroscopeHrefLang : Macroscope
	{

		/**************************************************************************/

		Boolean enable_probe;
		string locale;
		string url;
		DateTime date_modified;
		Boolean available;
		
		/**************************************************************************/

		public MacroscopeHrefLang ( Boolean bProbe, string sLocale, string sURL )
		{
			enable_probe = bProbe;
			locale = sLocale;	
			url = sURL;
			if( bProbe ) {
				available = probe();
			} else {
				available = false;
			}
		}

		/**************************************************************************/

		public string get_locale()
		{
			return( this.locale );
		}

		/**************************************************************************/

		public string get_url()
		{
			return( this.url );
		}

		/**************************************************************************/
		
		public Boolean is_available()
		{
			return( this.available );
		}

		/**************************************************************************/

		Boolean probe()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bAvailable = false;

			try {

				req = WebRequest.CreateHttp( this.url );
				req.Method = "HEAD";
				req.Timeout = 10000;
				req.KeepAlive = false;
				res = ( HttpWebResponse )req.GetResponse();

				debug_msg( string.Format( "HrefLang Status: {0}", res.StatusCode ), 2 );

				if( res.StatusCode == HttpStatusCode.OK ) {
					bAvailable = true;
				}

				res.Close();

			} catch( WebException ex ) {
				debug_msg( string.Format( "is_html_page :: WebException: {0}", ex.Message ), 2 );
			}

			return( bAvailable );

		}

		/**************************************************************************/

	}

}
