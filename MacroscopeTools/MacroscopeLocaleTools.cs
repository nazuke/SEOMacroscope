using System;
using HtmlAgilityPack;

namespace SEOMacroscope
{

	public class MacroscopeLocaleTools : Macroscope
	{

		/**************************************************************************/

		public MacroscopeLocaleTools ()
		{
		}

		/**************************************************************************/
		
		public string ProbeLocale( HtmlDocument doc )
		{

			string sLocale = null;

			if( sLocale == null ) {
				HtmlNode nNode = doc.DocumentNode.SelectSingleNode( "/html[@lang]" );
				if( nNode != null ) {
					sLocale = nNode.GetAttributeValue( "lang", null );
					debug_msg( string.Format( "HTML@LANG: {0}", sLocale ), 3 );
				} else {
					sLocale = null;		
					debug_msg( string.Format( "HTML@LANG: {0}", "MISSING" ), 3 );
				}
			}

			if( sLocale == null ) {
				HtmlNode nNode = doc.DocumentNode.SelectSingleNode( "/html/head/meta[@http-equiv='Content-Language']" );
				if( nNode != null ) {
					sLocale = nNode.GetAttributeValue( "content", null );
					debug_msg( string.Format( "HTML@LANG: {0}", sLocale ), 3 );
				} else {
					sLocale = null;		
					debug_msg( string.Format( "HTML@LANG: {0}", "MISSING" ), 3 );
				}
			}

			return( sLocale );

		}

		/**************************************************************************/
	
	}

}
