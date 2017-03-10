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
      string sErrorCondition = null;
      Boolean bAuthenticating = false;
      
      try
      {

        req = WebRequest.CreateHttp( this.DocUrl );
        req.Method = "GET";
        req.Timeout = this.Timeout;
        req.KeepAlive = false;
                
        this.PrepareRequestHttpHeaders( req: req );
                
        bAuthenticating = this.AuthenticateRequest( req );
                                      
        MacroscopePreferencesManager.EnableHttpProxy( req );

        res = ( HttpWebResponse )req.GetResponse();

      }
      catch( WebException ex )
      {

        DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ex.Message ) );
        DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ex.Status ) );
        DebugMsg( string.Format( "ProcessPdfPage :: WebException: {0}", ( int )ex.Status ) );

        sErrorCondition = ex.Status.ToString();

      }

      if( res != null )
      {

        MacroscopePdfTools pdfTools;

        this.ProcessResponseHttpHeaders( req, res );

        if( bAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        { // Probe Locale
          this.Locale = "en"; // Implement locale probing
          this.SetHreflang( this.Locale, this.DocUrl );
        }

        { // Canonical
          this.Canonical = this.DocUrl;
          DebugMsg( string.Format( "CANONICAL: {0}", this.Canonical ) );
        }

        { // Get Response Body
          try
          {

            Stream sStream = res.GetResponseStream();
            List<byte> aRawDataList = new List<byte> ();
            byte [] aRawData;
            do
            {
              int buf = sStream.ReadByte();
              if( buf > -1 )
              {
                aRawDataList.Add( ( byte )buf );
              }
              else
              {
                break;
              }
            } while( sStream.CanRead );
            aRawData = aRawDataList.ToArray();
            this.ContentLength = aRawData.Length;

            pdfTools = new MacroscopePdfTools ( aRawData );
						
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

      if( sErrorCondition != null )
      {
        this.ProcessErrorCondition( sErrorCondition );
      }

    }

    /**************************************************************************/

  }

}
