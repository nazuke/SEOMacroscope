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
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Annot.DA;
using iText.Layout.Element;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  public class MacroscopePdfTools : Macroscope, IDisposable
  {
    /**************************************************************************/

    private PdfDocument Pdf = null;
    private bool HasError = false;
    private string ErrorMessage = null;
    private Dictionary<string, string> Metadata = null;

    /**************************************************************************/

    public MacroscopePdfTools ( byte[] PdfData )
    {

      this.HasError = false;
      this.ErrorMessage = null;
      this.Metadata = new Dictionary<string, string>( 4 );

      try
      {
        using( MemoryStream MemStream = new MemoryStream( PdfData ) )
        {
          this.Pdf = new PdfDocument( new PdfReader( MemStream ) );
          this.LoadMetadata();
        }
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        this.HasError = true;
        this.ErrorMessage = ex.Message;
      }

    }

    /** Self Destruct Sequence ************************************************/

    public void Dispose ()
    {
      Dispose( true );
    }

    /** -------------------------------------------------------------------- **/

    protected virtual void Dispose ( bool disposing )
    {
      if( this.Pdf != null )
      {
        if( !this.Pdf.IsClosed() )
        {
          this.Pdf.Close();
        }
      }
    }

    /**************************************************************************/

    private void LoadMetadata ()
    {

      this.Metadata.Add( "title", "" );
      this.Metadata.Add( "author", "" );
      this.Metadata.Add( "description", "" );
      this.Metadata.Add( "keywords", "" );

      if( this.Pdf != null )
      {

        PdfDocumentInfo pdfInfo = this.Pdf.GetDocumentInfo();

        this.Metadata[ "title" ] = pdfInfo.GetTitle();
        this.Metadata[ "author" ] = pdfInfo.GetAuthor();
        this.Metadata[ "description" ] = pdfInfo.GetSubject();
        this.Metadata[ "keywords" ] = pdfInfo.GetKeywords();

      }

      return;

    }

    /**************************************************************************/

    public string GetTitle ()
    {
      string Text = null;
      if( this.Metadata.ContainsKey( "title" ) )
      {
        Text = this.Metadata[ "title" ];
      }
      return ( Text );
    }

    /**************************************************************************/

    public string GetAuthor ()
    {
      string Text = null;
      if( this.Metadata.ContainsKey( "author" ) )
      {
        Text = this.Metadata[ "author" ];
      }
      return ( Text );
    }

    /**************************************************************************/

    public string GetDescription ()
    {
      string Text = null;
      if( this.Metadata.ContainsKey( "description" ) )
      {
        Text = this.Metadata[ "description" ];
      }
      return ( Text );
    }

    /**************************************************************************/

    public string GetKeywords ()
    {
      string Text = null;
      if( this.Metadata.ContainsKey( "keywords" ) )
      {
        Text = this.Metadata[ "keywords" ];
      }
      return ( Text );
    }

    /**************************************************************************/

    public string GetTextAsString ()
    {
      List<string> Texts = this.GetTextAsList();
      string Text = string.Join( Environment.NewLine, Texts );
      return ( Text );
    }

    /**************************************************************************/

    public List<string> GetTextAsList ()
    {

      List<string> Texts = new List<string>( this.Pdf.GetNumberOfPages() );

      for( int i = 1 ; i <= this.Pdf.GetNumberOfPages() ; i++ )
      {
        PdfPage Page = this.Pdf.GetPage( i );
        string PageText = PdfTextExtractor.GetTextFromPage( Page );
        Texts.Add( PageText );
      }

      return ( Texts );

    }

    /**************************************************************************/

    public List<string> GetOutLinks ()
    {

      // TDOO: Test this with PDF with embedded links, and complete.

      List<string> OutLinks = new List<string>( this.Pdf.GetNumberOfPages() );

      /*
      for ( int i = 1 ; i <= this.Pdf.GetNumberOfPages() ; i++ )
      {

        PdfPage Page = this.Pdf.GetPage( i );

        if ( Page.GetAnnotations().Count > 0 )
        {

          IList<PdfAnnotation> Annotations = Page.GetAnnotations();

          foreach ( PdfAnnotation Annotation in Annotations )
          {

            if ( Annotation.GetType().Equals( PdfName.Link ) )
            {
              string Url = Annotation.ToString();
              OutLinks.Add( Url );
            }


          }

        }

      }
      */

      return ( OutLinks );

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

    public async Task<bool> IsPdfUrl ( MacroscopeJobMaster JobMaster, string Url )
    {

      bool Result = false;
      Uri TargetUri = new Uri( Url );
      string MimeType = await MacroscopeHttpUrlUtils.GetMimeTypeOfUrl( JobMaster: JobMaster, TargetUri: TargetUri );

      if( !string.IsNullOrEmpty( MimeType ) )
      {
        if( Regex.IsMatch( MimeType, "^application/pdf$", RegexOptions.IgnoreCase ) )
        {
          Result = true;
        }
      }

      return ( Result );

    }

    /**************************************************************************/

  }

}
