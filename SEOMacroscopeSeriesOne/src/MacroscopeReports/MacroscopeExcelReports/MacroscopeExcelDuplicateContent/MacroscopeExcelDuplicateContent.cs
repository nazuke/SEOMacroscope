/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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

  public partial class MacroscopeExcelDuplicateContent : MacroscopeExcelReports, IMacroscopeAnalysisPercentageDone
  {

    /**************************************************************************/

    IMacroscopeProgressForm ProgressForm;

    /**************************************************************************/

    public MacroscopeExcelDuplicateContent ()
    {
      this.ProgressForm = null;
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeExcelDuplicateContent ( IMacroscopeProgressForm ProgressFormDialogue )
    {
      this.ProgressForm = ProgressFormDialogue;
    }

    /**************************************************************************/

    public void WriteXslx ( MacroscopeJobMaster JobMaster, string OutputFilename )
    {

      XLWorkbook Workbook = new XLWorkbook();

      if( this.ProgressForm != null )
      {

        decimal MajorPercentageDivider = 3;

        if( MacroscopePreferencesManager.GetEnableLevenshteinDeduplication() )
        {
          MajorPercentageDivider = 4;
        }

        if( ( this.ProgressForm != null ) && ( !this.ProgressForm.Cancelled() ) )
        {

          this.ProgressForm.UpdatePercentages(
            Title: "Processing Titles",
            Message: "Identifying duplicate titles in collection:",
            MajorPercentage: ( (decimal) 100 / MajorPercentageDivider ) * (decimal) 1,
            ProgressLabelMajor: "Documents Processed",
            MinorPercentage: 0,
            ProgressLabelMinor: "",
            SubMinorPercentage: 0,
            ProgressLabelSubMinor: ""
          );

          this.BuildWorksheetPageDuplicateTitles( JobMaster, Workbook, "Duplicate Titles" );

        }

        if( !this.ProgressForm.Cancelled() )
        {

          this.ProgressForm.UpdatePercentages(
            Title: "Processing Checksums",
            Message: "Identifying duplicate checksums in collection:",
            MajorPercentage: ( (decimal) 100 / MajorPercentageDivider ) * (decimal) 2,
            ProgressLabelMajor: "Documents Processed",
            MinorPercentage: 0,
            ProgressLabelMinor: "",
            SubMinorPercentage: 0,
            ProgressLabelSubMinor: ""
          );

          this.BuildWorksheetPageDuplicateChecksums( JobMaster, Workbook, "Duplicate Checksums" );

        }

        if( !this.ProgressForm.Cancelled() )
        {

          this.ProgressForm.UpdatePercentages(
            Title: "Processing ETags",
            Message: "Identifying duplicate ETags in collection:",
            MajorPercentage: ( (decimal) 100 / MajorPercentageDivider ) * (decimal) 3,
            ProgressLabelMajor: "Documents Processed",
            MinorPercentage: 0,
            ProgressLabelMinor: "",
            SubMinorPercentage: 0,
            ProgressLabelSubMinor: ""
          );

          this.BuildWorksheetPageDuplicateEtags( JobMaster, Workbook, "Duplicate ETags" );

        }

        if( !this.ProgressForm.Cancelled() )
        {

          if( MacroscopePreferencesManager.GetEnableLevenshteinDeduplication() )
          {

            this.ProgressForm.UpdatePercentages(
              Title: "Applying Levenshtein Distance",
              Message: "Identifying duplicate documents via Levenshtein Distance in collection:",
              MajorPercentage: ( (decimal) 100 / MajorPercentageDivider ) * (decimal) 4,
              ProgressLabelMajor: "Documents Processed: 0",
              MinorPercentage: 0,
              ProgressLabelMinor: "",
              SubMinorPercentage: 0,
              ProgressLabelSubMinor: ""
            );

            this.BuildWorksheetPageDuplicatePages( JobMaster, Workbook, "Duplicate Content" );

          }

        }
        if( !this.ProgressForm.Cancelled() )
        {
          this.SaveOutputFile( Workbook: Workbook, OutputFilename: OutputFilename );
        }

      }
      else
      {

        this.BuildWorksheetPageDuplicateTitles( JobMaster, Workbook, "Duplicate Titles" );
        this.BuildWorksheetPageDuplicateChecksums( JobMaster, Workbook, "Duplicate Checksums" );
        this.BuildWorksheetPageDuplicateEtags( JobMaster, Workbook, "Duplicate ETags" );
        this.BuildWorksheetPageDuplicatePages( JobMaster, Workbook, "Duplicate Content" );

        this.SaveOutputFile( Workbook: Workbook, OutputFilename: OutputFilename );

      }

    }

    /**************************************************************************/

    private void SaveOutputFile ( XLWorkbook Workbook, string OutputFilename )
    {

      try
      {
        Workbook.SaveAs( OutputFilename );
      }
      catch( IOException )
      {
        MacroscopeSaveExcelFileException CannotSaveExcelFileException;
        CannotSaveExcelFileException = new MacroscopeSaveExcelFileException(
          string.Format( "Cannot write to Excel file at {0}", OutputFilename )
        );
        throw CannotSaveExcelFileException;
      }

    }

    /**************************************************************************/

    public void PercentageDone ( decimal Percent )
    {
    }

    public void PercentageDone ( decimal Percent, string Message )
    {

      this.ProgressForm.UpdatePercentages(
        Title: null,
        Message: null,
        MajorPercentage: -1,
        ProgressLabelMajor: null,
        MinorPercentage: -1,
        ProgressLabelMinor: null,
        SubMinorPercentage: Percent,
        ProgressLabelSubMinor: Message
      );

    }

    /**************************************************************************/

  }

}
