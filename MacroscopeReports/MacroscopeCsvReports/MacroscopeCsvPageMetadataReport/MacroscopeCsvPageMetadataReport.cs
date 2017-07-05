/*

  This file is part of SEOMacroscope.

  Copyright 2017 Jason Holland.

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

  public partial class MacroscopeCsvPageMetadataReport : MacroscopeCsvReports
  {
  
    /**************************************************************************/

    public enum OutputWorksheet
    {
      TITLES = 0,
      DESCRIPTIONS = 1,
      KEYWORDS = 2
    }
      
    /**************************************************************************/
      
    public MacroscopeCsvPageMetadataReport ()
    {
    }

    /**************************************************************************/

    public void WriteCsv (
      MacroscopeJobMaster JobMaster,
      MacroscopeCsvPageMetadataReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {
      
      try
      {
              
        using( StreamWriter writer = File.CreateText( OutputFilename ) )
        {
        
          CsvWriter ws = new CsvWriter ( writer );
        
          switch( SelectedOutputWorksheet )
          {
            case MacroscopeCsvPageMetadataReport.OutputWorksheet.TITLES:
              this.BuildWorksheetPageTitles( JobMaster, ws );
              break;
            case MacroscopeCsvPageMetadataReport.OutputWorksheet.DESCRIPTIONS:
              this.BuildWorksheetPageDescriptions( JobMaster, ws );
              break;
            case MacroscopeCsvPageMetadataReport.OutputWorksheet.KEYWORDS:
              this.BuildWorksheetPageKeywords( JobMaster, ws );
              break;
            default:
              this.BuildWorksheetPageTitles( JobMaster, ws );
              break;
          }

        }

      }
      catch( CsvWriterException )
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
