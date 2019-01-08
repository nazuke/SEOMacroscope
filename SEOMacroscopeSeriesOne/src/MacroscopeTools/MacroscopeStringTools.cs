/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeStringTools.
  /// </summary>

  public class MacroscopeStringTools : Macroscope
  {

    /**************************************************************************/

    static MacroscopeStringTools ()
    {
      SuppressStaticDebugMsg = true;
    }

    public MacroscopeStringTools ()
    {
    }

    /**************************************************************************/

    public static string ReverseString ( string Input )
    {

      string Output = "";

      for( int i = ( Input.Length - 1 ) ; i >= 0 ; i-- )
      {
        Output += Input[ i ];
      }

      return ( Output );

    }

    /**************************************************************************/

    public static string[] ReverseStringArray ( string[] Input )
    {

      string[] Output = new string[ Input.Length ];

      for( int i = 0 ; i < Input.Length ; i++ )
      {
        Output[ i ] = MacroscopeStringTools.ReverseString( Input[ i ] );
      }

      return ( Output );

    }

    /**************************************************************************/

    public static string CleanDocumentText ( MacroscopeDocument msDoc )
    {

      string CleanedText = msDoc.GetDocumentTextRaw();

      if( !string.IsNullOrEmpty( CleanedText ) )
      {

        try
        {
          CleanedText = HtmlEntity.DeEntitize( CleanedText );
        }
        catch( System.Collections.Generic.KeyNotFoundException ex )
        {
          DebugMsgStatic( string.Format( "CleanDocumentText: {0}", ex.Message ) );
          msDoc.AddRemark( "CleanDocumentText", "Possibly contains invalid HTML Entities." );
        }
        catch( Exception ex )
        {
          DebugMsgStatic( string.Format( "CleanDocumentText: {0}", ex.Message ) );
          msDoc.AddRemark( "CleanDocumentText", "Possibly contains invalid HTML Entities." );
        }

        CleanedText = CleanText( Text: CleanedText );

      }

      return ( CleanedText );

    }

    /** -------------------------------------------------------------------- **/

    public static string CleanHtmlText ( string Text )
    {

      string CleanedText = Text;

      if( !string.IsNullOrEmpty( CleanedText ) )
      {

        try
        {
          CleanedText = HtmlEntity.DeEntitize( CleanedText );
        }
        catch( Exception ex )
        {
          DebugMsgStatic( string.Format( "CleanBodyText: {0}", ex.Message ) );
        }

        CleanedText = CleanText( Text: CleanedText );

      }

      return ( CleanedText );

    }

    /**************************************************************************/

    public static string StripHtmlDocTypeAndCommentsFromText ( string Text )
    {

      string CleanedText = "";

      if( !string.IsNullOrEmpty( Text ) )
      {

        CleanedText = Text;

        CleanedText = Regex.Replace( CleanedText, @"<!.+?>", " ", RegexOptions.Singleline );
        CleanedText = Regex.Replace( CleanedText, @"<!--.+?-->", " ", RegexOptions.Singleline );

        CleanedText = CleanedText.Trim();

      }

      return ( CleanedText );

    }

    /**************************************************************************/

    public static string CleanText ( string Text )
    {

      string CleanedText = "";

      if( !string.IsNullOrEmpty( Text ) )
      {

        CleanedText = Text;

        CleanedText = Regex.Replace( CleanedText, @"<!.+?>", " ", RegexOptions.Singleline ); // Strip <!DOCTYPE>
        CleanedText = Regex.Replace( CleanedText, @"<!--.+?-->", " ", RegexOptions.Singleline ); // Strip HTML/XML comments
        CleanedText = Regex.Replace( CleanedText, @"<[^<>]+?>", " ", RegexOptions.Singleline ); // Strip HTML/XML tags

        CleanedText = Regex.Replace( CleanedText, @"(?<![\w\d])([^\w\d\p{Sc}]+)", " ", RegexOptions.Singleline );
        CleanedText = Regex.Replace( CleanedText, @"([^\w\d\p{Sc}]+)(?![\w\d])", " ", RegexOptions.Singleline );

        CleanedText = Regex.Replace( CleanedText, @"([\p{P}\p{Sc}]+)(?![\w\d])", " ", RegexOptions.Singleline ); // Strip punctuation
        CleanedText = Regex.Replace( CleanedText, @"[\s]+", " ", RegexOptions.Singleline ); // Compact white space

        CleanedText = CleanedText.Trim();

      }

      return ( CleanedText );

    }

    /*

    public static string CleanText ( string Text )
    {

      string CleanedText = "";

      if( !string.IsNullOrEmpty( Text ) )
      {
        
        CleanedText = Text;
        
        CleanedText = Regex.Replace( CleanedText, @"<!.+?>", " ", RegexOptions.Singleline );
        CleanedText = Regex.Replace( CleanedText, @"<!--.+?-->", " ", RegexOptions.Singleline );
        CleanedText = Regex.Replace( CleanedText, @"[\s]+", " ", RegexOptions.Singleline );
        CleanedText = Regex.Replace( CleanedText, @"(?<![\w\d])([^\p{L}\p{N}\p{Sc}]+)", " ", RegexOptions.Singleline );
        CleanedText = Regex.Replace( CleanedText, @"([^\p{L}\p{N}\p{Sc}]+)(?![\w\d])", " ", RegexOptions.Singleline );
        CleanedText = Regex.Replace( CleanedText, @"([\p{P}\p{Sc}]+)(?![\w\d])", " ", RegexOptions.Singleline );
        CleanedText = Regex.Replace( CleanedText, @"[\s]+", " ", RegexOptions.Singleline );
        
        CleanedText = CleanedText.Trim();

      }
      
      return( CleanedText );

    }

    */

    /**************************************************************************/

    public static string CompactWhiteSpace ( string Text )
    {

      string NewText = Text;

      if( !string.IsNullOrEmpty( NewText ) )
      {

        try
        {
          NewText = HtmlEntity.DeEntitize( NewText );
        }
        catch( Exception ex )
        {
          DebugMsgStatic( string.Format( "CompactWhiteSpace: {0}", ex.Message ) );
        }

        NewText = Regex.Replace( NewText, @"[\s]+", " ", RegexOptions.Singleline );
        NewText = Regex.Replace( NewText, @"[\s]+$", "", RegexOptions.Singleline );
        NewText = Regex.Replace( NewText, @"[\r\n]+", Environment.NewLine, RegexOptions.Singleline );

      }

      return ( NewText );

    }

    /**************************************************************************/

    public static string CleanWhiteSpace ( string Text )
    {

      string NewText = Text;

      if( !string.IsNullOrEmpty( NewText ) )
      {

        try
        {
          NewText = HtmlEntity.DeEntitize( NewText );
        }
        catch( Exception ex )
        {
          DebugMsgStatic( string.Format( "CompactWhiteSpace: {0}", ex.Message ) );
        }

        NewText = Regex.Replace( NewText, @"[\r\n]+", " ", RegexOptions.Singleline );
        NewText = Regex.Replace( NewText, @"[\s]+", " ", RegexOptions.Singleline );
        NewText = Regex.Replace( NewText, @"^[\s]+", "", RegexOptions.Singleline );
        NewText = Regex.Replace( NewText, @"[\s]+$", "", RegexOptions.Singleline );

      }

      return ( NewText );

    }

    /**************************************************************************/

    public static string StripNewLines ( string Text )
    {

      string NewText = Text;

      if( !string.IsNullOrEmpty( Text ) )
      {
        NewText = NewText.Replace( "\r", "" );
        NewText = NewText.Replace( "\n", "" );
      }

      return ( NewText );

    }

    /**************************************************************************/

  }

}
