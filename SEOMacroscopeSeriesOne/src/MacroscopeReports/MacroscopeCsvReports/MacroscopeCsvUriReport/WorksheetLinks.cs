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

  public partial class MacroscopeCsvUriReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    private void BuildWorksheetPageLinks (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();

      {

        ws.WriteField( "URL" );
        ws.WriteField( "Link Type" );
        ws.WriteField( "Source URL" );
        ws.WriteField( "Target URL" );
        ws.WriteField( "Follow" );
        ws.WriteField( "Alt Text" );
        ws.WriteField( "Raw Source URL" );
        ws.WriteField( "Raw Target URL" );

        ws.NextRecord();
        
      }

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url: Url );

        foreach( MacroscopeLink Link in msDoc.IterateOutlinks() )
        {

          string LinkType = Link.GetLinkType().ToString();
            
          string SourceUrl = Link.GetSourceUrl();
          string TargetUrl = Link.GetTargetUrl();

          string AltText = Link.GetAltText();
        
          string RawSourceUrl = Link.GetRawSourceUrl();
          string RawTargetUrl = Link.GetRawTargetUrl();

          string DoFollow = "No Follow";

          if( Link.GetDoFollow() )
          {
            DoFollow = "Follow";
          }

          if( string.IsNullOrEmpty( AltText ) )
          {
            AltText = "";
          }

          if( string.IsNullOrEmpty( RawSourceUrl ) )
          {
            RawSourceUrl = "";
          }

          if( string.IsNullOrEmpty( RawTargetUrl ) )
          {
            RawTargetUrl = "";
          }

          this.InsertAndFormatUrlCell( ws, msDoc );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( LinkType ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( SourceUrl ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( TargetUrl ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( DoFollow ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( AltText ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( RawSourceUrl ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( RawTargetUrl ) );

          ws.NextRecord();
                  
        }

      }

    }

    /**************************************************************************/

  }

}
