﻿/*
	
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
		
		public string ProbeLocale ( HtmlDocument doc )
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
		
		public void debug_msg ( String sMsg )
		{
		}

		public void debug_msg ( String sMsg, int iOffset )
		{
		}
		
		/**************************************************************************/
	
	}

}