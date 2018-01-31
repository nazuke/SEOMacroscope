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
using System.IO;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelUriReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    public MacroscopeExcelUriReport ()
    {
    }

    /**************************************************************************/

    public void WriteXslx ( MacroscopeJobMaster JobMaster, string OutputFilename )
    {

      XLWorkbook wb = new XLWorkbook ();

      this.BuildWorksheetPageLinks( JobMaster, wb, "Links" );
      this.BuildWorksheetPageHyperlinks( JobMaster, wb, "Hyperlinks" );
      this.BuildWorksheetPageUriAnalysis( JobMaster, wb, "URI Analysis" );
      this.BuildWorksheetPageOrphanedPages( JobMaster, wb, "Orphaned Pages" );

      try
      {
        wb.SaveAs( OutputFilename );
      }
      catch( IOException )
      {
        MacroscopeSaveExcelFileException CannotSaveExcelFileException;
        CannotSaveExcelFileException = new MacroscopeSaveExcelFileException (
          string.Format( "Cannot write to Excel file at {0}", OutputFilename )
        );
        throw CannotSaveExcelFileException;
      }

    }

    /**************************************************************************/

  }

}
