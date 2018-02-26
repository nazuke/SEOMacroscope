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
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvPageMetadataReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageTitles (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();

      {
        ws.WriteField( "URL" );
        ws.WriteField( "Page Language" );
        ws.WriteField( "Detected Language" );
        ws.WriteField( "Occurrences" );
        ws.WriteField( "Title" );
        ws.WriteField( "Title Length" );
        ws.WriteField( "Pixel Width" );
        ws.NextRecord();
      }

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        bool Proceed = false;

        if( msDoc.GetIsExternal() )
        {
          continue;
        }
        
        if( msDoc.GetIsRedirect() )
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

          string Title = msDoc.GetTitle();
          string PageLanguage = msDoc.GetIsoLanguageCode();
          string DetectedLanguage = msDoc.GetTitleLanguage();
          int Occurrences = 0;
          int TitleLength = msDoc.GetTitleLength();
          int TitlePixelWidth = msDoc.GetTitlePixelWidth();

          if( TitleLength > 0 )
          {
            Occurrences = DocCollection.GetStatsTitleCount( msDoc: msDoc );
          }

          this.InsertAndFormatUrlCell( ws, msDoc );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( PageLanguage ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( DetectedLanguage ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( Occurrences.ToString() ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( Title ) );
          
          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( TitleLength.ToString() ) );
          
          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( TitlePixelWidth.ToString() ) );

          ws.NextRecord();
          
        }

      }

    }

    /**************************************************************************/

  }

}
