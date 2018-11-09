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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeIntenseKeywordAnalysis.
  /// </summary>

  public class MacroscopeIntenseKeywordAnalysis : MacroscopeAnalysis
  {

    /**************************************************************************/

    // Keyword Term / MacroscopeDocumentList
    Dictionary<string, MacroscopeDocumentList> DocList;

    /**************************************************************************/

    public MacroscopeIntenseKeywordAnalysis () : base()
    {
      this.SuppressDebugMsg = false;
      this.DocList = null;
    }

    public MacroscopeIntenseKeywordAnalysis ( Dictionary<string, MacroscopeDocumentList> DocList ) : base()
    {
      this.SuppressDebugMsg = false;
      this.DocList = DocList;
    }

    /**************************************************************************/

    public void Analyze ( MacroscopeDocument msDoc )
    {

      string Keywords = msDoc.GetKeywords();
      string BodyText = msDoc.GetDocumentTextCleaned();
      List<string> KeywordsList = new List<string>();

      foreach( string Keyword in Keywords.Split( ',' ) )
      {
        KeywordsList.Add( Keyword );
      }

      this.DebugMsg( Keywords );
      this.DebugMsg( BodyText );




      return;

    }



    /**************************************************************************/

  }

}
