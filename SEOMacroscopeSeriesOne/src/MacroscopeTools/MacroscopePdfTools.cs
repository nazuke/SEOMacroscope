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
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  public class MacroscopePdfTools : Macroscope
  {
    /**************************************************************************/

    private PdfDocument Pdf;
    private bool HasError;
    private string ErrorMessage;

    /**************************************************************************/

    public MacroscopePdfTools ( byte[] PdfData )
    {

      this.HasError = false;
      this.ErrorMessage = null;

      try
      {

        using ( MemoryStream ms = new MemoryStream( PdfData ) )
        {
          Pdf = PdfReader.Open( ms, PdfDocumentOpenMode.InformationOnly );
        }

      }
      catch ( PdfReaderException ex )
      {

        DebugMsg( string.Format( "PDF Exception: {0}", ex.Message ) );
        this.HasError = true;
        this.ErrorMessage = ex.Message;

      }
      catch ( PdfSharpException ex )
      {

        DebugMsg( string.Format( "PDF Exception: {0}", ex.Message ) );
        this.HasError = true;
        this.ErrorMessage = ex.Message;

      }
      catch ( Exception ex )
      {

        DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        this.HasError = true;
        this.ErrorMessage = ex.Message;

      }

    }

    /**************************************************************************/

    public Dictionary<string, string> GetMetadata ()
    {

      Dictionary<string, string> dicMetadata = new Dictionary<string, string>( 32 );

      if ( Pdf != null )
      {
        PdfDocumentInformation pdfInfo = Pdf.Info;
        dicMetadata.Add( "title", pdfInfo.Title );
      }

      return ( dicMetadata );

    }

    /**************************************************************************/

    public string GetTitle ()
    {

      Dictionary<string, string> dicMetadata = this.GetMetadata();
      string Title = "";

      if ( dicMetadata.ContainsKey( "title" ) )
      {
        Title = dicMetadata[ "title" ];
      }

      return ( Title );

    }

    /**************************************************************************/

    public bool GetHasError ()
    {
      return ( this.HasError );
    }

    /**************************************************************************/

    public string GetErrorMessage ()
    {
      return ( this.ErrorMessage );
    }

    /**************************************************************************/

    // TODO: Fix this so that it is HTTP/2 compliant
    public bool IsPdfUrl ( string Url )
    {

      bool Result = false;
      string MimeType = MacroscopeUrlUtils.GetMimeTypeOfUrl( Url: Url );

      if ( !string.IsNullOrEmpty( MimeType ) )
      {
        if ( Regex.IsMatch( MimeType, "^application/pdf$", RegexOptions.IgnoreCase ) )
        {
          Result = true;
        }
      }

      return ( Result );

    }

    /**************************************************************************/

  }

}
