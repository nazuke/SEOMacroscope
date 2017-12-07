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
using System.Net;
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
      int Row,
      int Col,
      MacroscopeDocument msDoc
    )
    {

      ws.Cell( Row, Col ).Value = msDoc.GetUrl();

      if( msDoc.GetIsInternal() )
      {
        ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Green );
      }
      else
      {
        ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Gray );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatUrlCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      string Url
    )
    {

      ws.Cell( Row, Col ).Value = Url;

    }

    /**************************************************************************/

    public void InsertAndFormatStatusCodeCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      MacroscopeDocument msDoc
    )
    {

      string Value = ( (int) msDoc.GetStatusCode() ).ToString();

      if( string.IsNullOrEmpty( Value ) )
      {
        Value = "0";
      }

      ws.Cell( Row, Col ).Value = Value;

      {
        if( Regex.IsMatch( Value, "^[2]" ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Green );
        }
        else
        if( Regex.IsMatch( Value, "^[3]" ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Goldenrod );
        }
        else
        if( Regex.IsMatch( Value, "^[45]" ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Red );
        }
        else
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Blue );
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatStatusCodeCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      int StatusCode
    )
    {

      ws.Cell( Row, Col ).Value = StatusCode.ToString();

      {
        if( ( StatusCode >= 200 ) && ( StatusCode <= 299 ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Green );
        }
        else
        if( ( StatusCode >= 300 ) && ( StatusCode <= 399 ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Goldenrod );
        }
        else
        if( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Red );
        }
        else
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Blue );
        }
      }

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatStatusCodeCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      HttpStatusCode StatusCode
    )
    {

      ws.Cell( Row, Col ).Value = StatusCode.ToString();

      {
        if( ( (int) StatusCode >= 200 ) && ( (int) StatusCode <= 299 ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Green );
        }
        else
        if( ( (int) StatusCode >= 300 ) && ( (int) StatusCode <= 399 ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Goldenrod );
        }
        else
        if( ( (int) StatusCode >= 400 ) && ( (int) StatusCode <= 599 ) )
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Red );
        }
        else
        {
          ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Blue );
        }
      }

    }

    /**************************************************************************/

    public void InsertAndFormatRedirectCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      MacroscopeDocument msDoc
    )
    {

      string Value = msDoc.GetIsRedirect().ToString();

      ws.Cell( Row, Col ).Value = Value;

      if( Value.ToLower() == "true" )
      {
        ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Red );
      }
      else
      {
        ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Gray );
      }

    }
    
    /**************************************************************************/

    public void InsertAndFormatRobotsCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      MacroscopeDocument msDoc
    )
    {

      Boolean Value = msDoc.GetAllowedByRobots();

      ws.Cell( Row, Col ).Value = msDoc.GetAllowedByRobotsAsString();

      if( Value )
      {
        ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Green );
      }
      else
      {
        ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Red );
      }

    }

    /**************************************************************************/

    public void InsertAndFormatContentCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      string Value
    )
    {

      ws.Cell( Row, Col ).Value = Value;

      if( Value == "MISSING" )
      {
        ws.Cell( Row, Col ).Style.Font.SetFontColor( XLColor.Red );
      }

      this.SetContentCellType( ws, Row, Col, XLCellValues.Text );

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatContentCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      int Value
    )
    {

      ws.Cell( Row, Col ).Value = Value;

      this.SetContentCellType( ws, Row, Col, XLCellValues.Number );

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatContentCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      decimal Value
    )
    {

      ws.Cell( Row, Col ).Value = Value;

      this.SetContentCellType( ws, Row, Col, XLCellValues.Number );

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatDateCell (
      IXLWorksheet ws,
      int Row,
      int Col,
      string Value
    )
    {

      ws.Cell( Row, Col ).Value = Value;

      this.SetContentCellType( ws, Row, Col, XLCellValues.DateTime );

    }

    /**************************************************************************/

    private void SetContentCellType (
      IXLWorksheet ws,
      int Row,
      int Col,
      XLCellValues CellType
    )
    {

      try
      {
        ws.Cell( Row, Col ).SetDataType( dataType: CellType );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

    }

    /**************************************************************************/

    public string FormatIfMissing ( string Value )
    {

      string FormattedValue;

      if( Value == null )
      {
        FormattedValue = "MISSING";
      }
      else
      if( Value.Length == 0 )
      {
        FormattedValue = "MISSING";
      }
      else
      {
        FormattedValue = Value;
      }

      return ( FormattedValue );

    }

    /**************************************************************************/

  }

}
