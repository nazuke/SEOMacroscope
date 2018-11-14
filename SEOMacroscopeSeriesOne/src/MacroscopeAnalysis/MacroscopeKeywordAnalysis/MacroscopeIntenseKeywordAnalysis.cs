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
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeIntenseKeywordAnalysis.
  /// </summary>

  public class MacroscopeIntenseKeywordAnalysis : MacroscopeAnalysis
  {

    /**************************************************************************/

    public enum KEYWORD_STATUS
    {
      KEYWORDS_METATAG_EMPTY = 0,
      PRESENT_IN_BODY_TEXT = 1,
      MISSING_IN_BODY_TEXT = 2
    }

    /**************************************************************************/

    public MacroscopeIntenseKeywordAnalysis () : base()
    {
      this.SuppressDebugMsg = false;
    }

    /**************************************************************************/

    public List<KeyValuePair<string, KEYWORD_STATUS>> AnalyzeKeywordPresence ( MacroscopeDocument msDoc )
    {

      string Keywords = msDoc.GetKeywords().ToLower();
      string BodyText = msDoc.GetDocumentTextCleaned().ToLower();
      List<string> KeywordsList = new List<string>();
      List<KeyValuePair<string, KEYWORD_STATUS>> KeywordPresence = new List<KeyValuePair<string, KEYWORD_STATUS>>();
      bool KeywordsMetatagEmpty = false;

      foreach( string Keyword in Keywords.Split( ',' ) )
      {
        string KeywordCleaned = MacroscopeStringTools.CleanWhiteSpace( Keyword );
        KeywordsList.Add( KeywordCleaned );
        KeywordsMetatagEmpty = true;
      }

      if( KeywordsMetatagEmpty )
      {

        foreach( string Keyword in KeywordsList )
        {

          string kw = this.GetPatternForLanguage( msDoc: msDoc, Keyword: Keyword );

          if( Regex.IsMatch( BodyText, kw ) )
          {
            KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.PRESENT_IN_BODY_TEXT ) );
          }
          else
          {
            KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.MISSING_IN_BODY_TEXT ) );
          }

        }

      }
      else
      {

        KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( "", KEYWORD_STATUS.KEYWORDS_METATAG_EMPTY ) );

      }

      return ( KeywordPresence );

    }

    /**************************************************************************/

    private string GetPatternForLanguage ( MacroscopeDocument msDoc, string Keyword )
    {

      string Pattern = "\\s" + Keyword + "\\s";
      string LangCode = msDoc.GetIsoLanguageCode();

      if( LangCode != null )
      {

        if( LangCode.ToLower().StartsWith( "ja" ) )
        {
          Pattern = Keyword;
        }
        else if( LangCode.ToLower().StartsWith( "zh" ) )
        {
          Pattern = Keyword;
        }

      }
      return ( Pattern );

    }

    /**************************************************************************/

  }

}
