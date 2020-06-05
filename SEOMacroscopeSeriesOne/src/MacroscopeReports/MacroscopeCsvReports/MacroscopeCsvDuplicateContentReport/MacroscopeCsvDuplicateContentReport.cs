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
using System.Globalization;
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvDuplicateContentReport : MacroscopeCsvReports, IMacroscopeAnalysisPercentageDone
  {

    /**************************************************************************/

    public enum OutputWorksheet
    {
      TITLES = 0,
      CHECKSUMS = 1,
      ETAGS = 2,
      PAGES = 3
    }

    IMacroscopeProgressForm ProgressForm;

    /**************************************************************************/

    public MacroscopeCsvDuplicateContentReport ()
    {
      this.ProgressForm = null;
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeCsvDuplicateContentReport ( IMacroscopeProgressForm ProgressFormDialogue )
    {
      this.ProgressForm = ProgressFormDialogue;
    }

    /**************************************************************************/

    public void WriteCsv (
      MacroscopeJobMaster JobMaster,
      MacroscopeCsvDuplicateContentReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      if ( this.ProgressForm != null )
      {

        decimal MajorPercentageDivider = 1;

        try
        {

          using ( StreamWriter writer = File.CreateText( OutputFilename ) )
          {

            CsvWriter ws = new CsvWriter( writer, CultureInfo.InvariantCulture );

            switch ( SelectedOutputWorksheet )
            {

              case MacroscopeCsvDuplicateContentReport.OutputWorksheet.TITLES:

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

                this.BuildWorksheetPageDuplicateTitles( JobMaster, ws );

                break;

              case MacroscopeCsvDuplicateContentReport.OutputWorksheet.CHECKSUMS:

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

                this.BuildWorksheetPageDuplicateChecksums( JobMaster, ws );

                break;

              case MacroscopeCsvDuplicateContentReport.OutputWorksheet.ETAGS:

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

                this.BuildWorksheetPageDuplicateEtags( JobMaster, ws );

                break;

              case MacroscopeCsvDuplicateContentReport.OutputWorksheet.PAGES:

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

                this.BuildWorksheetPageDuplicatePages( JobMaster, ws );

                break;

              default:
                break;
            }

          }

        }
        catch ( CsvHelperException )
        {
          MacroscopeSaveCsvFileException CannotSaveCsvFileException;
          CannotSaveCsvFileException = new MacroscopeSaveCsvFileException(
            string.Format( "Cannot write to CSV file at {0}", OutputFilename )
          );
          throw CannotSaveCsvFileException;
        }
        catch ( IOException )
        {
          MacroscopeSaveCsvFileException CannotSaveCsvFileException;
          CannotSaveCsvFileException = new MacroscopeSaveCsvFileException(
            string.Format( "Cannot write to CSV file at {0}", OutputFilename )
          );
          throw CannotSaveCsvFileException;
        }

      }
      else
      {

        try
        {

          using ( StreamWriter writer = File.CreateText( OutputFilename ) )
          {
            CsvWriter ws = new CsvWriter( writer, CultureInfo.InvariantCulture );
            switch ( SelectedOutputWorksheet )
            {
              case MacroscopeCsvDuplicateContentReport.OutputWorksheet.TITLES:
                this.BuildWorksheetPageDuplicateTitles( JobMaster, ws );
                break;
              case MacroscopeCsvDuplicateContentReport.OutputWorksheet.CHECKSUMS:
                this.BuildWorksheetPageDuplicateChecksums( JobMaster, ws );
                break;
              case MacroscopeCsvDuplicateContentReport.OutputWorksheet.ETAGS:
                this.BuildWorksheetPageDuplicateEtags( JobMaster, ws );
                break;
              case MacroscopeCsvDuplicateContentReport.OutputWorksheet.PAGES:
                this.BuildWorksheetPageDuplicatePages( JobMaster, ws );
                break;
              default:
                break;
            }
          }

        }
        catch ( CsvHelperException )
        {
          MacroscopeSaveCsvFileException CannotSaveCsvFileException;
          CannotSaveCsvFileException = new MacroscopeSaveCsvFileException(
            string.Format( "Cannot write to CSV file at {0}", OutputFilename )
          );
          throw CannotSaveCsvFileException;
        }
        catch ( IOException )
        {
          MacroscopeSaveCsvFileException CannotSaveCsvFileException;
          CannotSaveCsvFileException = new MacroscopeSaveCsvFileException(
            string.Format( "Cannot write to CSV file at {0}", OutputFilename )
          );
          throw CannotSaveCsvFileException;
        }

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
