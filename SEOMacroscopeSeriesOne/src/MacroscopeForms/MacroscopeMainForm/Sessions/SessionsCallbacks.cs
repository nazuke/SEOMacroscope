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
using System.Windows.Forms;

namespace SEOMacroscope
{

  /*
   * Reference:
   *  https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/walkthrough-persisting-an-object-in-visual-studio
   */

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Sessions Save Dialogue Boxes ******************************************/

    private void CallbackSaveSessionToFile ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "SEO Macroscope session files (*.seomacroscope)|*.seomacroscope";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "seomacroscope";
      Dialog.AddExtension = true;
      Dialog.FileName = "Session.seomacroscope";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Pathname = Dialog.FileName;

        Cursor.Current = Cursors.WaitCursor;

        try
        {

          MacroscopeSessionSaver SessionSaver = new MacroscopeSessionSaver();
          MacroscopeJobMaster JobMaster = this.GetJobMaster();
          SessionSaver.Save( JobMaster: JobMaster, Pathname: Pathname );

          this.DialogueBoxFeedback( "SEO Macroscope session", "The SEO Macroscope session file was saved successfully." );

        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }
      else
      {
        this.DialogueBoxError( "Error", "Could not open SEO Macroscope session file." );
      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackLoadSessionFromFile ( object sender, EventArgs e )
    {

      OpenFileDialog Dialog = new OpenFileDialog();

      Dialog.Filter = "SEO Macroscope session files (*.seomacroscope)|*.seomacroscope";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "seomacroscope";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Pathname = Dialog.FileName;

        if( File.Exists( Pathname ) )
        {

          Cursor.Current = Cursors.WaitCursor;

          try
          {

            MacroscopeSessionLoader SessionLoader = new MacroscopeSessionLoader();
            MacroscopeJobMaster JobMaster = SessionLoader.Load( Pathname: Pathname );

            if( JobMaster != null )
            {

              this.ScanReset( JobRunTimeMode: JobMaster.GetRunTimeMode() );

              JobMaster.InitializeAfterDeserialization( NewTaskController: this );
              this.SetJobMaster( NewJobMaster: JobMaster );

              this.SetIncludeExcludeUrls( NewIncludeExcludeUrls: JobMaster.GetIncludeExcludeUrls() );
              this.SetCustomFilters( NewCustomFilter: JobMaster.GetCustomFilter() );
              this.SetDataExtractors( NewDataExtractorCssSelectors: JobMaster.GetDataExtractorCssSelectors(), NewDataExtractorRegexes: JobMaster.GetDataExtractorRegexes(), NewDataExtractorXpaths: JobMaster.GetDataExtractorXpaths() );

              this.StartUrlString = JobMaster.GetStartUrl();
              this.textBoxStartUrl.Text = this.StartUrlString;
              this.StartUrlDirty = false;

              this.SelectTabPage( TabName: MacroscopeConstants.tabPageStructureOverview );
              this.msDisplayStructure.RefreshData( DocCollection: this.JobMaster.GetDocCollection() );

              this.ScanningControlsStopped();

              this.DialogueBoxFeedback( "SEO Macroscope session", "The SEO Macroscope session file was loaded successfully." );

            }
            else
            {
              this.DialogueBoxError( "Error", "There was an error loading the SEO Macroscope session file." );
            }

          }
          catch( Exception ex )
          {
            this.DialogueBoxError( "Error", ex.Message );
          }
          finally
          {
            Cursor.Current = Cursors.Default;
          }

        }
        else
        {
          this.DialogueBoxError( "Error", "The SEO Macroscope session file specified could not be found" );
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

    }

    /**************************************************************************/

  }

}
