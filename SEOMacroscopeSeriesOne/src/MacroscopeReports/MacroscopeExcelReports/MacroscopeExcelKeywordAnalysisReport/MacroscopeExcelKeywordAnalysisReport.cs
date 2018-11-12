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
using System.Collections.Generic;
using ClosedXML.Excel;

namespace SEOMacroscope
{

  public partial class MacroscopeExcelKeywordAnalysisReport : MacroscopeExcelReports, IMacroscopeAnalysisPercentageDone
  {

    /**************************************************************************/

    IMacroscopeProgressForm ProgressForm;

    /**************************************************************************/

    public MacroscopeExcelKeywordAnalysisReport ()
    {
      this.ProgressForm = null;
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeExcelKeywordAnalysisReport ( IMacroscopeProgressForm ProgressFormDialogue )
    {
      this.ProgressForm = ProgressFormDialogue;
    }

    /**************************************************************************/

    public void WriteXslx ( MacroscopeJobMaster JobMaster, string OutputFilename )
    {

      XLWorkbook Workbook = new XLWorkbook();
      Dictionary<string, int> DicTerms = null;

      if ( this.ProgressForm != null )
      {

        const decimal MajorPercentageDivider = 4;

        for ( int i = 0 ; i <= 3 ; i++ )
        {

          if ( !this.ProgressForm.Cancelled() )
          {

            this.ProgressForm.UpdatePercentages(
            Title: "Processing Keywords",
            Message: "Processing keyword terms collection:",
            MajorPercentage: ( (decimal) 100 / MajorPercentageDivider ) * (decimal) i + 1,
            ProgressLabelMajor: "",
            MinorPercentage: 0,
            ProgressLabelMinor: "",
            SubMinorPercentage: 0,
            ProgressLabelSubMinor: ""
          );

            DicTerms = JobMaster.GetDocCollection().GetDeepKeywordAnalysisAsDictonary( Words: i + 1 );
            this.BuildWorksheetKeywordTerms( JobMaster, Workbook, string.Format( "{0} Word Term", i + 1 ), DicTerms );

          }

        }

        this.BuildWorksheetKeywordsPresence( JobMaster, Workbook, "Keywords Presence", JobMaster.GetDocCollection() );

        if( !this.ProgressForm.Cancelled() )
        {
          this.SaveOutputFile( Workbook: Workbook, OutputFilename: OutputFilename );
        }

      }
      else
      {

        for ( int i = 0 ; i <= 3 ; i++ )
        {
          DicTerms = JobMaster.GetDocCollection().GetDeepKeywordAnalysisAsDictonary( Words: i + 1 );
          this.BuildWorksheetKeywordTerms( JobMaster, Workbook, string.Format( "{0} Word Term", i + 1 ), DicTerms );
        }

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
