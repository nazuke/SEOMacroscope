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
