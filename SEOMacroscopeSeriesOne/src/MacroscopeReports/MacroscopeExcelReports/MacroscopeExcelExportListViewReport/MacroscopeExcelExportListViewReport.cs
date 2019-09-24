﻿/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Windows.Forms;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelExportListViewReport : MacroscopeExcelReports
  {

    /**************************************************************************/

    private string WorksheetName;
    
    private ListView TargetListView;

    /**************************************************************************/

    public MacroscopeExcelExportListViewReport (
      string SelectedWorksheetName,
      ListView SelectedListView
    ) : base()
    {

      this.WorksheetName = SelectedWorksheetName;

      this.TargetListView = SelectedListView;

    }

    /**************************************************************************/

    public void WriteXslx ( MacroscopeJobMaster JobMaster, string OutputFilename )
    {

      XLWorkbook wb = new XLWorkbook ();

      DebugMsg( string.Format( "EXCEL OutputFilename: {0}", OutputFilename ) );

      lock( this.TargetListViewLock )
      {

        this.BuildWorksheetListView( JobMaster, wb, WorksheetName );
        
      }

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
