/*

  This file is part of SEOMacroscope.

  Copyright 2020 Jason Holland.

  The GitHub repository may be found at:

    https://github.com/nazuke/SEOMacroscope

  SEOMacroscope is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  SEOMacroscope is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.IO;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelRobotsReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    public MacroscopeExcelRobotsReport ()
    {
    }

    /**************************************************************************/

    public void WriteXslx ( MacroscopeJobMaster JobMaster, string OutputFilename )
    {

      XLWorkbook Workbook = new XLWorkbook();

      this.BuildWorksheetBlockedByRobotsInternal( JobMaster, Workbook, "Internal URLs Blocked by Robots" );
      this.BuildWorksheetBlockedByRobotsExternal( JobMaster, Workbook, "External URLs Blocked by Robots" );

      try
      {
        Workbook.SaveAs( OutputFilename );
      }
      catch ( IOException )
      {
        MacroscopeSaveExcelFileException CannotSaveExcelFileException;
        CannotSaveExcelFileException = new MacroscopeSaveExcelFileException(
          string.Format( "Cannot write to Excel file at {0}", OutputFilename )
        );
        throw CannotSaveExcelFileException;
      }

    }

    /**************************************************************************/

  }

}
