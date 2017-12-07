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
using System.Net;
using CsvHelper;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeCsvReports.
  /// </summary>

  public class MacroscopeCsvReports : Macroscope
  {

    /**************************************************************************/

    public MacroscopeCsvReports ()
    {
    }

    /**************************************************************************/

    public void InsertAndFormatUrlCell (
      CsvWriter ws,
      MacroscopeDocument msDoc
    )
    {

      ws.WriteField( msDoc.GetUrl() );

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatUrlCell (
      CsvWriter ws,
      string Url
    )
    {

      ws.WriteField( Url );

    }

    /**************************************************************************/

    public void InsertAndFormatStatusCodeCell (
      CsvWriter ws,
      MacroscopeDocument msDoc
    )
    {

      string Value = ( (int) msDoc.GetStatusCode() ).ToString();

      if( string.IsNullOrEmpty( Value ) )
      {
        Value = "0";
      }

      ws.WriteField( Value );

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatStatusCodeCell (
      CsvWriter ws,
      int StatusCode
    )
    {

      string Value = StatusCode.ToString();

      ws.WriteField( Value );

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatStatusCodeCell (
      CsvWriter ws,
      HttpStatusCode StatusCode
    )
    {

      string Value = StatusCode.ToString();

      ws.WriteField( Value );

    }

    /**************************************************************************/

    public void InsertAndFormatRedirectCell (
      CsvWriter ws,
      MacroscopeDocument msDoc
    )
    {

      string Value = msDoc.GetIsRedirect().ToString();

      ws.WriteField( Value );

    }

    /**************************************************************************/

    public void InsertAndFormatRobotsCell (
      CsvWriter ws,
      MacroscopeDocument msDoc
    )
    {

      string Value = msDoc.GetAllowedByRobotsAsString();

      ws.WriteField( Value );

    }

    /**************************************************************************/

    public void InsertAndFormatContentCell (
      CsvWriter ws,
      string Value
    )
    {

      ws.WriteField( Value );

    }

    /** -------------------------------------------------------------------- **/

    public void InsertAndFormatContentCell (
      CsvWriter ws,
      int Value
    )
    {

      ws.WriteField( Value );

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
