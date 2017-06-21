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
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelLanguagesReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private void BuildWorksheetHrefLangMatrixUnspecified (
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
          LocaleCols[ LocaleKey ] = iCol;
          ws.Cell( iRow, iCol ).Value = LocaleKey;
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
        Boolean Proceed = false;

        foreach( string LocaleKey in LocalesTable.Keys )
        {
          if( !string.IsNullOrEmpty( LocaleKey ) )
          {
            if( !HrefLangsTable.ContainsKey( LocaleKey ) )
            {
              Proceed = true;
            }
          }
        }

        if( Proceed )
        {

          if( !string.IsNullOrEmpty( msDoc.GetLocale() ) )
          {
            continue;
          }

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
            ws.Cell( iRow, LocaleCols[ LocaleCol ] ).Value = msDoc.GetUrl();
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

                MacroscopeHrefLang msHrefLang = HrefLangsTable[ LocaleKey ];
                string Value = msHrefLang.GetUrl();

                ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Value = Value;

                if( JobMaster.GetAllowedHosts().IsInternalUrl( Value ) )
                {
                  ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Style.Font.SetFontColor( XLColor.Green );
                }
                else
                {
                  ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Style.Font.SetFontColor( XLColor.Blue );
                }

              }
              else
              {
                ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Style.Font.SetFontColor( XLColor.Red );
                ws.Cell( iRow, LocaleCols[ LocaleKey ] ).Value = "NOT SPECIFIED";
              }
              
            }
            
          }

          iRow++;
        
        
        }

      }

      {
        var rangeData = ws.Range( 1, 1, iRow - 1, iColMax - 1 );
        var excelTable = rangeData.CreateTable();
      }

    }

    /**************************************************************************/

  }

}
