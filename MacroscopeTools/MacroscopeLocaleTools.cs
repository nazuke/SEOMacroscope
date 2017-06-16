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
using HtmlAgilityPack;

namespace SEOMacroscope
{

  public class MacroscopeLocaleTools : Macroscope
  {

    /**************************************************************************/

    public MacroscopeLocaleTools ()
    {
      this.SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public string ProbeLocale ( HtmlDocument HtmlDoc )
    {

      string DocumentLocale = null;

      if( DocumentLocale == null )
      {
        HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html[@lang]" );
        if( nNode != null )
        {
          DocumentLocale = nNode.GetAttributeValue( "lang", null );
          DebugMsg( string.Format( "HTML@LANG: {0}", DocumentLocale ) );
        }
        else
        {
          DocumentLocale = null;
          DebugMsg( string.Format( "HTML@LANG: {0}", "MISSING" ) );
        }
      }

      if( DocumentLocale == null )
      {
        HtmlNode nNode = HtmlDoc.DocumentNode.SelectSingleNode( "/html/head/meta[@http-equiv='Content-Language']" );
        if( nNode != null )
        {
          DocumentLocale = nNode.GetAttributeValue( "content", null );
          DebugMsg( string.Format( "HTML@LANG: {0}", DocumentLocale ) );
        }
        else
        {
          DocumentLocale = null;
          DebugMsg( string.Format( "HTML@LANG: {0}", "MISSING" ) );
        }
      }

      if( DocumentLocale == null )
      {
        DocumentLocale = "x-default";
      }
      
      return( DocumentLocale.ToLower() );

    }

    /**************************************************************************/

  }

}
