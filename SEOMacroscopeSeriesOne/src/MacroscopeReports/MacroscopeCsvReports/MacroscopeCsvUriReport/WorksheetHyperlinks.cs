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

    private void BuildWorksheetPageHyperlinks (
      MacroscopeJobMaster JobMaster,
      CsvWriter ws
    )
    {

      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      MacroscopeAllowedHosts AllowedHosts = JobMaster.GetAllowedHosts();

      {

        ws.WriteField( "Source URL" );
        ws.WriteField( "Target URL" );
        ws.WriteField( "Follow" );
        ws.WriteField( "Target" );
        ws.WriteField( "Link Text" );
        ws.WriteField( "Title Text" );
        ws.WriteField( "Alt Text" );
        ws.WriteField( "Raw Target URL" );

        ws.NextRecord();

      }

      foreach( string Url in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url: Url );
        MacroscopeHyperlinksOut HyperlinksOut = msDoc.GetHyperlinksOut();

        foreach( MacroscopeHyperlinkOut HyperlinkOut in HyperlinksOut.IterateLinks() )
        {

          string HyperlinkOutUrl = HyperlinkOut.GetTargetUrl();
          string DoFollow = "No Follow";
          string LinkTarget = HyperlinkOut.GetLinkTarget();
          string LinkText = HyperlinkOut.GetLinkText();
          string LinkTitle = HyperlinkOut.GetLinkTitle();      
          string AltText = HyperlinkOut.GetAltText();       
          
          string RawTargetUrl = HyperlinkOut.GetRawTargetUrl();       

          if( string.IsNullOrEmpty( HyperlinkOutUrl ) )
          {
            HyperlinkOutUrl = "";
          }

          if( HyperlinkOut.GetDoFollow() )
          {
            DoFollow = "Follow";
          }

          this.InsertAndFormatUrlCell( ws, msDoc );

          this.InsertAndFormatUrlCell( ws, HyperlinkOutUrl );

          this.InsertAndFormatContentCell( ws, DoFollow );

          this.InsertAndFormatContentCell( ws, LinkTarget );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( LinkText ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( LinkTitle ) );

          this.InsertAndFormatContentCell( ws, this.FormatIfMissing( AltText ) );

          this.InsertAndFormatContentCell( ws, RawTargetUrl );

          ws.NextRecord();
                  
        }

      }

    }

    /**************************************************************************/

  }

}
