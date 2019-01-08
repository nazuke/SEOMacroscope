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
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeKeywordPresenceAnalysis.
  /// </summary>

  [Serializable()]
  public class MacroscopeKeywordPresenceAnalysis : MacroscopeAnalysis
  {

    /**************************************************************************/

    public enum KEYWORD_STATUS
    {
      KEYWORDS_METATAG_EMPTY = 0,
      MALFORMED_KEYWORDS_METATAG = 1,
      PRESENT_IN_TITLE = 2,
      MISSING_IN_TITLE = 3,
      PRESENT_IN_DESCRIPTION = 4,
      MISSING_IN_DESCRIPTION = 5,
      PRESENT_IN_BODY = 6,
      MISSING_IN_BODY = 7
    }

    /**************************************************************************/

    public MacroscopeKeywordPresenceAnalysis () : base()
    {
      this.SuppressDebugMsg = false;
    }

    /**************************************************************************/

    public List<KeyValuePair<string, KEYWORD_STATUS>> AnalyzeKeywordPresence ( MacroscopeDocument msDoc )
    {

      string Keywords = msDoc.GetKeywords().ToLower();
      string TitleText = msDoc.GetTitle().ToLower();
      string DescriptionText = msDoc.GetDescription().ToLower();
      string BodyText = msDoc.GetDocumentTextCleaned().ToLower();
      List<string> KeywordsList = new List<string>();
      List<KeyValuePair<string, KEYWORD_STATUS>> KeywordPresence = new List<KeyValuePair<string, KEYWORD_STATUS>>();
      bool KeywordsMetatagFilled = false;

      foreach( string Keyword in Keywords.Split( ',' ) )
      {

        string KeywordCleaned = MacroscopeStringTools.CleanWhiteSpace( Keyword );

        if( KeywordCleaned.Length > 0 )
        {
          KeywordsList.Add( KeywordCleaned );
          KeywordsMetatagFilled = true;
        }

      }

      if( KeywordsMetatagFilled )
      {

        foreach( string Keyword in KeywordsList )
        {

          try
          {

            string kw = this.GetPatternForLanguage( msDoc: msDoc, Keyword: Keyword );

            if( Regex.IsMatch( TitleText, kw ) )
            {
              KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.PRESENT_IN_TITLE ) );
            }
            else
            {
              KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.MISSING_IN_TITLE ) );
            }

            if( Regex.IsMatch( DescriptionText, kw ) )
            {
              KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.PRESENT_IN_DESCRIPTION ) );
            }
            else
            {
              KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.MISSING_IN_DESCRIPTION ) );
            }

            if( Regex.IsMatch( BodyText, kw ) )
            {
              KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.PRESENT_IN_BODY ) );
            }
            else
            {
              KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.MISSING_IN_BODY ) );
            }

          }
          catch( Exception ex )
          {
            this.DebugMsg( ex.Message );
            KeywordPresence.Add( new KeyValuePair<string, KEYWORD_STATUS>( Keyword, KEYWORD_STATUS.MALFORMED_KEYWORDS_METATAG ) );
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
