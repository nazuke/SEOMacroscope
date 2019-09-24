/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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

  public partial class MacroscopeCsvPageMetadataReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageKeywords (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();

      {
        ws.WriteField( "URL" );
        ws.WriteField( "Occurrences" );
        ws.WriteField( "Keywords" );
        ws.WriteField( "Keywords Length" );
        ws.WriteField( "Number of Keywords" );
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

          string Keywords = msDoc.GetKeywords();
          int Occurrences = 0;
          int KeywordsLength = msDoc.GetKeywordsLength();
          int KeywordsNumber = msDoc.GetKeywordsCount();

          if( KeywordsLength > 0 )
          {
            Occurrences = DocCollection.GetStatsKeywordsCount( msDoc );
          }

          this.InsertAndFormatUrlCell( ws, msDoc );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( Occurrences.ToString() ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( Keywords ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( KeywordsLength.ToString() ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( KeywordsNumber.ToString() ) );

          ws.NextRecord();
                  
        }

      }

    }

    /**************************************************************************/

  }

}
