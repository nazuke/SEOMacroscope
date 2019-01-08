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
using CsvHelper;

namespace SEOMacroscope
{

  public partial class MacroscopeCsvBrokenLinksReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    public enum OutputWorksheet
    {
      BROKEN_LINKS = 0,
      GOOD_LINKS = 1,
      REDIRECTED_LINKS = 2
    }

    /**************************************************************************/

    public MacroscopeCsvBrokenLinksReport ()
    {
    }

    /**************************************************************************/

    public void WriteCsv (
      MacroscopeJobMaster JobMaster,
      MacroscopeCsvBrokenLinksReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      try
      {

        using( StreamWriter writer = File.CreateText( OutputFilename ) )
        {

          CsvWriter ws = new CsvWriter( writer );

          switch( SelectedOutputWorksheet )
          {
            case MacroscopeCsvBrokenLinksReport.OutputWorksheet.BROKEN_LINKS:
              this.BuildWorksheetPageBrokenLinks( JobMaster, ws );
              break;
            case MacroscopeCsvBrokenLinksReport.OutputWorksheet.GOOD_LINKS:
              this.BuildWorksheetPageGoodLinks( JobMaster, ws );
              break;
            case MacroscopeCsvBrokenLinksReport.OutputWorksheet.REDIRECTED_LINKS:
              this.BuildWorksheetPageRedirectedLinks( JobMaster, ws );
              break;
            default:
              break;
          }

        }

      }
      catch( CsvHelperException )
      {
        MacroscopeSaveCsvFileException CannotSaveCsvFileException;
        CannotSaveCsvFileException = new MacroscopeSaveCsvFileException(
          string.Format( "Cannot write to CSV file at {0}", OutputFilename )
        );
        throw CannotSaveCsvFileException;
      }
      catch( IOException )
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
