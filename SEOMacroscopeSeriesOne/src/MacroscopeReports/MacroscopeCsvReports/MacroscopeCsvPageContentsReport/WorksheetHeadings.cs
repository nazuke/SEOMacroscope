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
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvPageContentsReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageHeadings (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();

      {

        ws.WriteField( "URL" );
        ws.WriteField( "Occurences" );
        ws.WriteField( "Order" );

        for( int i = 1 ; i <= 6 ; i++ )
        {
          ws.WriteField( string.Format( "H{0}", i ) );
        }

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

        if( msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
        {
          Proceed = true;
        }

        if( Proceed )
        {

          for( ushort HeadingLevel = 1 ; HeadingLevel <= MacroscopePreferencesManager.GetMaxHeadingDepth() ; HeadingLevel++ )
          {

            List<string> HeadingsList = msDoc.GetHeadings( HeadingLevel );

            for( int Order = 0 ; Order < HeadingsList.Count ; Order++ )
            {

              int Occurences = DocCollection.GetStatsHeadingsCount( HeadingLevel: HeadingLevel, Text: HeadingsList[ Order ] );

              this.InsertAndFormatUrlCell( ws, msDoc );

              this.InsertAndFormatContentCell( ws, Occurences.ToString() );

              this.InsertAndFormatContentCell( ws, this.FormatIfMissing( ( Order + 1 ).ToString() ) );

              this.InsertAndFormatContentCell( ws, this.FormatIfMissing( HeadingsList[ Order ] ) );

              ws.NextRecord();

            }

          }

        }

      }

    }

    /**************************************************************************/

  }

}
