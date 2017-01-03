using System;
using System.Collections;
using System.Collections.Generic;
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

		public string GetLocale()
		{
			return( this.Locale );
		}

		/**************************************************************************/

		public string GetUrl()
		{
			return( this.Url );
		}

		/**************************************************************************/
		
		public Boolean IsAvailable()
		{
			return( this.Available );
		}

		/**************************************************************************/

		Boolean Probe()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;
			Boolean bAvailable = false;

			try {

				req = WebRequest.CreateHttp( this.Url );
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
				debug_msg( string.Format( "IsHtmlPage :: WebException: {0}", ex.Message ), 2 );
			}

			return( bAvailable );

		}

		/**************************************************************************/

	}

}
