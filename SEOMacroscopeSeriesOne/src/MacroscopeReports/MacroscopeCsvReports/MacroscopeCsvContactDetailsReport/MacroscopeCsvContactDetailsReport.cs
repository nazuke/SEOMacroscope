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

  public partial class MacroscopeCsvContactDetailsReport : MacroscopeCsvReports
  {

    /**************************************************************************/

    public enum OutputWorksheet
    {
      TELEPHONE = 0,
      EMAIL = 1
    }
        
    /**************************************************************************/
            
    public MacroscopeCsvContactDetailsReport ()
    {
    }

    /**************************************************************************/

    public void WriteCsv (
      MacroscopeJobMaster JobMaster,
      MacroscopeCsvContactDetailsReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      try
      {
              
        using( StreamWriter writer = File.CreateText( OutputFilename ) )
        {
        
          CsvWriter ws = new CsvWriter ( writer, CultureInfo.InvariantCulture );
        
          switch( SelectedOutputWorksheet )
          {
            case MacroscopeCsvContactDetailsReport.OutputWorksheet.TELEPHONE:
              this.BuildWorksheetEmailAddresses( JobMaster, ws );
              break;
            case MacroscopeCsvContactDetailsReport.OutputWorksheet.EMAIL:
              this.BuildWorksheetTelephoneNumbers( JobMaster, ws );
              break;
            default:
              break;
          }

        }

      }
      catch( CsvHelperException )
      {
        MacroscopeSaveCsvFileException CannotSaveCsvFileException;
        CannotSaveCsvFileException = new MacroscopeSaveCsvFileException (
          string.Format( "Cannot write to CSV file at {0}", OutputFilename )
        );
        throw CannotSaveCsvFileException;
      }
      catch( IOException )
      {
        MacroscopeSaveCsvFileException CannotSaveCsvFileException;
        CannotSaveCsvFileException = new MacroscopeSaveCsvFileException (
          string.Format( "Cannot write to CSV file at {0}", OutputFilename )
        );
        throw CannotSaveCsvFileException;
      }

    }

    /**************************************************************************/

  }

}
