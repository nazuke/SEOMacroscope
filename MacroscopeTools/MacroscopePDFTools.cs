using System;
using System.Collections;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace SEOMacroscope
{

	public class MacroscopePDFTools : Macroscope
	{
		/**************************************************************************/

		PdfDocument Pdf;

		/**************************************************************************/
				
		public MacroscopePDFTools ( byte[] aPDF )
		{
			try {
				using( MemoryStream ms = new MemoryStream ( aPDF ) ) {
					Pdf = PdfReader.Open( ms, PdfDocumentOpenMode.InformationOnly );
				}
			} catch( PdfReaderException ex ) {
				debug_msg( string.Format( "PDF Exception: {0}", ex.Message ), 4 );
			} catch( PdfSharpException ex ) {
				debug_msg( string.Format( "PDF Exception: {0}", ex.Message ), 4 );
			}
		}

		/**************************************************************************/

		public Hashtable GetMetadata()
		{
			Hashtable htMetadata = new Hashtable ( 32 );
			if( Pdf != null ) {
				PdfDocumentInformation pdfInfo = Pdf.Info;
				htMetadata[ "title" ] = pdfInfo.Title;
			}
			return( htMetadata );
		}
		
		/**************************************************************************/

		public string GetTitle()
		{
			Hashtable htMetadata = this.GetMetadata();
			string sTitle = "";
			if( htMetadata.ContainsKey( "title" ) ) {
				sTitle = ( string )htMetadata[ "title" ].ToString();
			}
			return( sTitle );
		}
		
		/**************************************************************************/

	}

}
