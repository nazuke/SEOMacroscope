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
using System.IO;
using System.Net;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private void ProcessPdfPage ()
    {

      HttpWebRequest req = null;
      HttpWebResponse res = null;
      string ResponseErrorCondition = null;
      Boolean Authenticating = false;
      
      try
      {

        req = WebRequest.CreateHttp( this.DocUrl );
        req.Method = "GET";
        req.Timeout = this.Timeout;
        req.KeepAlive = false;
                
        this.PrepareRequestHttpHeaders( req: req );
                
        Authenticating = this.AuthenticateRequest( req );
                                      
        MacroscopePreferencesManager.EnableHttpProxy( req );

        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( WebException ex )
      {

        DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ex.Status ) );
        DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ( int )ex.Status ) );

        ResponseErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        MacroscopePdfTools pdfTools;

        this.ProcessResponseHttpHeaders( req, res );

        if( Authenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        { // Probe Locale
          this.Locale = "en"; // Implement locale probing
          this.SetHreflang( HrefLangLocale: this.Locale, Url: this.DocUrl );
        }

        { // Canonical
          this.Canonical = this.DocUrl;
          DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
        }

        { // Get Response Body
          try
          {

            Stream ResponseStream = res.GetResponseStream();
            List<byte> RawDataList = new List<byte> ();
            byte [] RawData;
            do
            {
              int buf = ResponseStream.ReadByte();
              if( buf > -1 )
              {
                RawDataList.Add( ( byte )buf );
              }
              else
              {
                break;
              }
            } while( ResponseStream.CanRead );
            RawData = RawDataList.ToArray();
            this.ContentLength = RawData.Length;

            pdfTools = new MacroscopePdfTools ( RawData );
						
            this.SetWasDownloaded( true );

          }
          catch( WebException ex )
          {

            DebugMsg( string.Format( "WebException: {0}", ex.Message ) );

            if( ex.Response != null )
            {
              this.SetStatusCode( ( ( HttpWebResponse )ex.Response ).StatusCode );
            }
            else
            {
              this.SetStatusCode( ( HttpStatusCode )ex.Status );
            }

            pdfTools = null;
            this.ContentLength = 0;

          }
          catch( Exception ex )
          {

            DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
            this.SetStatusCode( HttpStatusCode.BadRequest );
            pdfTools = null;
            this.ContentLength = 0;

          }
        }

        { // Title
          if( pdfTools != null )
          {
            string sTitle = pdfTools.GetTitle();
            if( sTitle != null )
            {
              this.SetTitle( sTitle, MacroscopeConstants.TextProcessingMode.NO_PROCESSING );
              DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
            }
            else
            {
              DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
            }
          }
        }

        res.Close();

      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }

    }

    /**************************************************************************/

  }

}
