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
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvPageContentsReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageText (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();

      {

        ws.WriteField( "URL" );
        ws.WriteField( "Page Locale" );
        ws.WriteField( "Page Language" );
        ws.WriteField( "Detected Language" );
        ws.WriteField( "Word Count" );

        ws.NextRecord();

      }

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        Boolean Proceed = false;

        if( msDoc.GetIsExternal() )
        {
          continue;
        }
        
        if( msDoc.GetIsRedirect() )
        {
          continue;
        }
            
        if( msDoc.GetIsHtml() )
        {
          Proceed = true;
        }
        else
        if( msDoc.GetIsPdf() )
        {
          Proceed = true;
        }

        if( Proceed )
        {

          string PageLocale = msDoc.GetLocale();
          string PageLanguage = msDoc.GetIsoLanguageCode();
          string DetectedLanguage = msDoc.GetBodyTextLanguage();
          int WordCount = msDoc.GetWordCount();

          if( string.IsNullOrEmpty( PageLocale ) )
          {
            PageLocale = "";
          }
        
          if( string.IsNullOrEmpty( PageLanguage ) )
          {
            PageLanguage = "";
          }
        
          if( string.IsNullOrEmpty( DetectedLanguage ) )
          {
            DetectedLanguage = "";
          }

          this.InsertAndFormatUrlCell( ws, msDoc );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( PageLocale ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( PageLanguage ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( DetectedLanguage ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( WordCount.ToString() ) );

          ws.NextRecord();

        }

      }

      return;
      
    }

    /**************************************************************************/

  }

}
