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
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelLanguagesReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetHrefLangMatrix (
      MacroscopeJobMaster JobMaster,
      XLWorkbook wb,
      string WorksheetLabel
    )
    {
      var ws = wb.Worksheets.Add( WorksheetLabel );

      int iRow = 1;
      int iCol = 1;
      int iColMax = 1;

      Dictionary<string,string> LocalesTable = JobMaster.GetLocales();
      MacroscopeDocumentCollection DocCollection = JobMaster.GetDocCollection();
      Dictionary<string,int> LocaleCols = new Dictionary<string, int> ();

      {

        ws.Cell( iRow, iCol ).Value = "URL";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Status Code";
        iCol++;
        
        ws.Cell( iRow, iCol ).Value = "Site Locale";
        iCol++;

        ws.Cell( iRow, iCol ).Value = "Title";
        iCol++;

        foreach( string LocaleKey in LocalesTable.Keys )
        {

          DebugMsg( string.Format( "EXCEL Locale: {0}", LocaleKey ) );

          string LocaleLabel = LocaleKey.ToUpper();
          string DateServerLabel = string.Format( "{0} Date Server", LocaleKey.ToUpper() );
          string DateModifiedLabel = string.Format( "{0} Date Modified", LocaleKey.ToUpper() );

          LocaleCols[ LocaleKey ] = iCol;

          ws.Cell( iRow, iCol ).Value = LocaleLabel;
          iCol++;

          ws.Cell( iRow, iCol ).Value = DateServerLabel;
          iCol++;

          ws.Cell( iRow, iCol ).Value = DateModifiedLabel;
          iCol++;

        }

        for( int i = 1 ; i <= iCol ; i++ )
        {
          ws.Cell( iRow, i ).Style.Font.SetBold();
        }

      }

      iColMax = iCol;

      iRow++;

      foreach( string Key in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Key );
        Dictionary<string,MacroscopeHrefLang> HrefLangsTable = msDoc.GetHrefLangs();

        string SiteLocale = this.FormatIfMissing( msDoc.GetLocale() );
        string Title = this.FormatIfMissing( msDoc.GetTitle() );
        string LocaleCol = msDoc.GetLocale();

        iCol = 1;

        this.InsertAndFormatUrlCell( ws, iRow, iCol, msDoc );
        iCol++;

        this.InsertAndFormatStatusCodeCell( ws, iRow, iCol, msDoc );
        iCol++;

        ws.Cell( iRow, iCol ).Value = SiteLocale;
        if( SiteLocale == "MISSING" )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
        }
        iCol++;
          
        ws.Cell( iRow, iCol ).Value = Title;
        if( Title == "MISSING" )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( XLColor.Red );
        }
        iCol++;
          
        if( LocaleCol != null )
        {
          this.InsertAndFormatUrlCell( ws, iRow, LocaleCols[ LocaleCol ], msDoc.GetUrl() );
        }
        else
        {
          ;
        }
            
        foreach( string LocaleKey in LocalesTable.Keys )
        {
            
          if( !string.IsNullOrEmpty( LocaleKey ) )
          {
            
            if( HrefLangsTable.ContainsKey( LocaleKey ) )
            {

              MacroscopeHrefLang HrefLangAlternate = HrefLangsTable[ LocaleKey ];

              string HrefLangUrl = HrefLangAlternate.GetUrl();
              DateTime HrefLangDateServer = HrefLangAlternate.GetDateServer();
              DateTime HrefLangDateModified = HrefLangAlternate.GetDateModified();

              this.InsertAndFormatUrlCell( ws, iRow, LocaleCols[ LocaleKey ], HrefLangUrl );

              if( JobMaster.GetAllowedHosts().IsInternalUrl( HrefLangUrl ) )
              {
                ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Style.Font.SetFontColor( XLColor.Green );
              }
              else
              {
                ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Style.Font.SetFontColor( XLColor.Blue );
              }

              this.InsertAndFormatDateCell( ws, iRow, LocaleCols[ LocaleKey ] + 1, HrefLangDateServer.ToString() );

              this.InsertAndFormatDateCell( ws, iRow, LocaleCols[ LocaleKey ] + 2, HrefLangDateModified.ToString() );

            }
            else
            {
              ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Style.Font.SetFontColor( XLColor.Red );
              ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Value = "NOT SPECIFIED";
              ws.Cell( iRow, LocaleCols[ LocaleKey ] + 1 ).Value = "";
              ws.Cell( iRow, LocaleCols[ LocaleKey ] + 2 ).Value = "";
            }
              
          }
            
        }

        iRow++;

      }

      {
        var rangeData = ws.Range( 1, 1, iRow - 1, iColMax - 1 );
        var excelTable = rangeData.CreateTable();
      }

    }

    /**************************************************************************/

  }

}
