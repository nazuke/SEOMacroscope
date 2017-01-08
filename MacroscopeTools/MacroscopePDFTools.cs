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
