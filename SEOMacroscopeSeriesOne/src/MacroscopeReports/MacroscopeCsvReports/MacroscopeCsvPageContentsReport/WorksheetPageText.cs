/*

  This file is part of SEOMacroscope.

  Copyright 2020 Jason Holland.

  The GitHub repository may be found at:

    https://github.com/nazuke/SEOMacroscope

  SEOMacroscope is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  SEOMacroscope is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

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
        ws.WriteField( "Readability Method" );
        ws.WriteField( "Readability Grade" );
        ws.WriteField( "Readability Grade Description" );

        ws.NextRecord();

      }

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        bool Proceed = false;

        if ( msDoc.GetIsExternal() )
        {
          continue;
        }

        if ( msDoc.GetIsRedirect() )
        {
          continue;
        }

        switch ( msDoc.GetDocumentType() )
        {
          case MacroscopeConstants.DocumentType.HTML:
            Proceed = true;
            break;
          case MacroscopeConstants.DocumentType.PDF:
            Proceed = true;
            break;
          default:
            break;
        }

        if ( Proceed )
        {

          string PageLocale = msDoc.GetLocale();
          string PageLanguage = msDoc.GetIsoLanguageCode();
          string DetectedLanguage = msDoc.GetDocumentTextLanguage();
          int WordCount = msDoc.GetWordCount();
          string ReadabilityGradeType = MacroscopeAnalyzeReadability.FormatAnalyzeReadabilityMethod( ReadabilityMethod: msDoc.GetReadabilityGradeMethod() );
          string ReadabilityGrade = msDoc.GetReadabilityGrade().ToString( "00.00" );
          string ReadabilityGradeDescription = msDoc.GetReadabilityGradeDescription();

          if ( string.IsNullOrEmpty( PageLocale ) )
          {
            PageLocale = "";
          }

          if ( string.IsNullOrEmpty( PageLanguage ) )
          {
            PageLanguage = "";
          }

          if ( string.IsNullOrEmpty( DetectedLanguage ) )
          {
            DetectedLanguage = "";
          }

          this.InsertAndFormatUrlCell( ws, msDoc );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( PageLocale ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( PageLanguage ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( DetectedLanguage ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( WordCount.ToString() ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( ReadabilityGradeType ) );
          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( ReadabilityGrade ) );
          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( ReadabilityGradeDescription ) );

          ws.NextRecord();

        }

      }

    }

    /**************************************************************************/

  }

}
