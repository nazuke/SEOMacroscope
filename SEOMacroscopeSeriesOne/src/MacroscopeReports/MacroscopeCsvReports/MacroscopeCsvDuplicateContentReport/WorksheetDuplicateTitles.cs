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

  public partial class MacroscopeCsvDuplicateContentReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageDuplicateTitles (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      decimal Count = 0;
      decimal DocCount = 0;
      
      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();
      
      DocCount = ( decimal )DocCollection.CountDocuments();

      {

        ws.WriteField( "URL" );
        ws.WriteField( "Occurrences" );
        ws.WriteField( "Title" );

        ws.NextRecord();
                
      }

      foreach ( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        bool Proceed = false;

        if( DocCount > 0 )
        {
          Count++;
          this.ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: -1,
            ProgressLabelMajor: string.Format( "Documents Processed: {0}", Count ),
            MinorPercentage: ( ( decimal )100 / DocCount ) * Count,
            ProgressLabelMinor: msDoc.GetUrl(),
            SubMinorPercentage: -1,
            ProgressLabelSubMinor: null
          );
        }
        
        if( AllowedHosts.IsInternalUrl( Url: msDoc.GetUrl() ) )
        {

          switch ( msDoc.GetDocumentType() )
          {
            case MacroscopeConstants.DocumentType.HTML:
              Proceed = true;
              break;
            case MacroscopeConstants.DocumentType.PDF:
              Proceed = true;
              break;
            default:
              Proceed = false;
              break;
          }

        }
          
        if( Proceed )
        {

          string Title = msDoc.GetTitle();
          int Occurrences = DocCollection.GetStatsTitleCount( msDoc: msDoc );
            
          if( Occurrences > 1 )
          {
            
            this.InsertAndFormatUrlCell( ws, msDoc );

            this.InsertAndFormatContentCell( ws, Occurrences );

            this.InsertAndFormatContentCell( ws, this.FormatIfMissing( Title ) );

            ws.NextRecord();

          }
          
        }

      }

    }

    /**************************************************************************/

  }

}
