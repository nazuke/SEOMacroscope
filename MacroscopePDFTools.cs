using System;
using System.Collections;
using PdfSharp;

namespace SEOMacroscope
{

	public class MacroscopePDFTools : Macroscope
	{

		/**************************************************************************/
				
		public MacroscopePDFTools ()
		{
		}

		/**************************************************************************/

		public Hashtable extract_metadata()
		{
			Hashtable htMetadata = new Hashtable ( 32 );
			
			debug_msg( string.Format( "PDF title: {0}", "" ) );
			
			return( htMetadata );

		}
		
		/**************************************************************************/

	}

}
