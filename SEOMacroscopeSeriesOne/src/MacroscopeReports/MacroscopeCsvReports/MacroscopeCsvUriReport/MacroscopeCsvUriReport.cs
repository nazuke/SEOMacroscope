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
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvUriReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    public enum OutputWorksheet
    {
      LINKS = 0,
      HYPERLINKS = 1,
      URIS = 2,
      ORPHANS = 3
    }

    /**************************************************************************/

    public MacroscopeCsvUriReport ()
    {
    }

    /**************************************************************************/

    public void WriteCsv (
      MacroscopeJobMaster JobMaster,
      MacroscopeCsvUriReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      try
      {

        using ( StreamWriter writer = File.CreateText( OutputFilename ) )
        {

          CsvWriter ws = new CsvWriter( writer );

          switch ( SelectedOutputWorksheet )
          {
            case MacroscopeCsvUriReport.OutputWorksheet.LINKS:
              this.BuildWorksheetPageLinks( JobMaster, ws );
              break;
            case MacroscopeCsvUriReport.OutputWorksheet.HYPERLINKS:
              this.BuildWorksheetPageHyperlinks( JobMaster, ws );
              break;
            case MacroscopeCsvUriReport.OutputWorksheet.URIS:
              this.BuildWorksheetPageUriAnalysis( JobMaster, ws );
              break;
            case MacroscopeCsvUriReport.OutputWorksheet.ORPHANS:
              this.BuildWorksheetPageOrphanedPages( JobMaster, ws );
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

    /**************************************************************************/

  }

}
