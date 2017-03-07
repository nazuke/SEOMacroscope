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
using System.Text.RegularExpressions;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public class MacroscopeExcelReports : Macroscope
  {

    /**************************************************************************/

    public MacroscopeExcelReports ()
    {
    }

    /**************************************************************************/

    public void InsertAndFormatUrlCell (
      IXLWorksheet ws,
      int iRow,
      int iCol,
      MacroscopeDocument msDoc
    )
    {

      ws.Cell( iRow, iCol ).Value = msDoc.GetUrl();

      if( !msDoc.GetIsExternal() )
      {
        ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Green );
      }
      else
      {
        ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Gray );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatUrlCell (
      IXLWorksheet ws,
      int iRow,
      int iCol,
      string Url
    )
    {

      ws.Cell( iRow, iCol ).Value = Url;

    }

    /**************************************************************************/

    public void InsertAndFormatStatusCodeCell (
      IXLWorksheet ws,
      int iRow,
      int iCol,
      MacroscopeDocument msDoc
    )
    {

      string sValue = ( ( int )msDoc.GetStatusCode() ).ToString();
      
      if( sValue == null )
      {
        sValue = "0";
      }
      
      ws.Cell( iRow, iCol ).Value = sValue;

      {
        if( Regex.IsMatch( sValue, "^[2]" ) )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Green );
        }
        else
        if( Regex.IsMatch( sValue, "^[3]" ) )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Goldenrod );
        }
        else
        if( Regex.IsMatch( sValue, "^[45]" ) )
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
        }
        else
        {
          ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Blue );
        }
      }

    }

    /**************************************************************************/

    public void InsertAndFormatRedirectCell (
      IXLWorksheet ws,
      int iRow,
      int iCol,
      MacroscopeDocument msDoc
    )
    {

      string sValue = msDoc.GetIsRedirect().ToString();
      
      ws.Cell( iRow, iCol ).Value = sValue;

      if( sValue.ToLower() == "true" )
      {
        ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
      }
      else
      {
        ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Gray );
      }

    }

    /**************************************************************************/

    public void InsertAndFormatContentCell ( IXLWorksheet ws, int iRow, int iCol, string sValue )
    {
      ws.Cell( iRow, iCol ).Value = sValue;
      if( sValue == "MISSING" )
      {
        ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
      }
    }

    /**************************************************************************/

    public string FormatIfMissing ( string sString )
    {
      string sFormatted;
      if( sString == null )
      {
        sFormatted = "MISSING";
      }
      else
      if( sString.Length == 0 )
      {
        sFormatted = "MISSING";
      }
      else
      {
        sFormatted = sString;
      }
      return( sFormatted );
    }

    /**************************************************************************/

  }

}
