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
using System.Collections.Generic;
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvCustomFilterReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetCustomFilter (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();

      Dictionary<string,int> FilterColsTable = new Dictionary<string,int> ( CustomFilter.GetSize() );
      
      const int FilterColOffset = 3;

      {

        ws.WriteField( MacroscopeConstants.Url );
        ws.WriteField( MacroscopeConstants.StatusCode );
        ws.WriteField( MacroscopeConstants.Status );
        ws.WriteField( MacroscopeConstants.ContentType );

        for( int Slot = 0 ; Slot < CustomFilter.GetSize() ; Slot++ )
        {

          string FilterPattern = CustomFilter.GetPattern( Slot ).Key;

          if( FilterColsTable.ContainsKey( FilterPattern ) || string.IsNullOrEmpty( FilterPattern ) )
          {

            FilterColsTable.Add( string.Format( "EMPTY{0}", Slot + 1 ), Slot + FilterColOffset );

            ws.WriteField( string.Format( "EMPTY{0}", Slot + 1 ) );

          }
          else
          {

            FilterColsTable.Add( FilterPattern, Slot + FilterColOffset );

            ws.WriteField( FilterPattern );

          }

        }
        
        ws.NextRecord();
        
      }

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        string DocUrl = msDoc.GetUrl();
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
        string MimeType = msDoc.GetMimeType();
        
        if( !this.CustomFilter.CanApplyCustomFiltersToDocument( msDoc: msDoc ) )
        {
          continue;
        }

        this.InsertAndFormatUrlCell( ws, msDoc );

        this.InsertAndFormatStatusCodeCell( ws, msDoc );

        this.InsertAndFormatContentCell( ws, this.FormatIfMissing( msDoc.GetStatusCode().ToString() ) );

        this.InsertAndFormatContentCell( ws, this.FormatIfMissing( MimeType ) );

        for( int Slot = 0 ; Slot < this.CustomFilter.GetSize() ; Slot++ )
        {

          string FilterPattern = this.CustomFilter.GetPattern( Slot: Slot ).Key;
          KeyValuePair<string, MacroscopeConstants.TextPresence> Pair = msDoc.GetCustomFilteredItem( Text: FilterPattern );

          if( ( Pair.Key != null ) && ( Pair.Value != MacroscopeConstants.TextPresence.UNDEFINED ) )
          {

            string CustomFilterItemValue = MacroscopeConstants.TextPresenceLabels[ Pair.Value ];

            this.InsertAndFormatContentCell( ws, CustomFilterItemValue );

          }
          else
          {

            this.InsertAndFormatContentCell( ws, "" );

          }

          ws.NextRecord();
                  
        }

      }

    }

    /**************************************************************************/

  }

}
