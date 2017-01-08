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
using System.Collections;
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

		public void InsertAndFormatContentCell ( IXLWorksheet ws, int iRow, int iCol, string sValue )
		{
			ws.Cell( iRow, iCol ).Value = sValue;
			if( sValue == "MISSING" ) {
				ws.Cell( iRow, iCol ).Style.Font.SetFontColor( ClosedXML.Excel.XLColor.Red );
			}
		}
		
		/**************************************************************************/
		
		public string FormatIfMissing ( string sString )
		{
			string sFormatted;
			if( sString == null ) {
				sFormatted = "MISSING";
			} else if( sString.Length == 0 ) {
				sFormatted = "MISSING";
			} else {
				sFormatted = sString;
			}
			return( sFormatted );
		}
		
		/**************************************************************************/
		
	}
	
}
