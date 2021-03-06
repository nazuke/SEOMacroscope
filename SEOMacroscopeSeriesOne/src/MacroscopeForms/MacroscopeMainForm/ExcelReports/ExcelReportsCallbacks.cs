﻿/*

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
using System.Collections.Generic;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Report Save Dialogue Boxes ********************************************/

    private const int ExcelReportMegabytesRamRequired = 64;

    /**************************************************************************/

    private void CallbackExportListViewToExcelReport ( object sender, EventArgs e )
    {

      KeyValuePair<string, ListView> SelectedListView;

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Report.xlsx";

      this.Enabled = false;

      SelectedListView = this.GetTabPageListView();

      if( SelectedListView.Value != null )
      {

        if( Dialog.ShowDialog() == DialogResult.OK )
        {

          string Path = Dialog.FileName;
          MacroscopeExcelExportListViewReport msExcelReport;

          msExcelReport = new MacroscopeExcelExportListViewReport(
            SelectedWorksheetName: SelectedListView.Key,
            SelectedListView: SelectedListView.Value
          );

          Cursor.Current = Cursors.WaitCursor;

          try
          {
            msExcelReport.WriteXslx( this.JobMaster, Path );
          }
          catch( MacroscopeSaveExcelFileException ex )
          {
            this.DialogueBoxError( "Error saving Excel Report", ex.Message );
          }
          catch( Exception ex )
          {
            this.DialogueBoxError( "Error saving Excel Report", ex.Message );
          }
          finally
          {
            Cursor.Current = Cursors.Default;
          }

        }

      }
      else
      {
        this.DialogueBoxError( "Error saving Excel Report", "Cannot export this view type" );
      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveOverviewExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Overview.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelOverviewReport msExcelReport = new MacroscopeExcelOverviewReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Overview Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Overview Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveErrorsExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Errors.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelErrorsReport msExcelReport = new MacroscopeExcelErrorsReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Errors Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Errors Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveBrokenLinksExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Broken-Links.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelBrokenLinksReport msExcelReport = new MacroscopeExcelBrokenLinksReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Broken Links Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Broken Links Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveRobotsExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Robots.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelRobotsReport msExcelReport = new MacroscopeExcelRobotsReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Robots Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Robots Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveSitemapErrorsExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Sitemap-Errors.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelSitemapsReport msExcelReport = new MacroscopeExcelSitemapsReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Errors Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Errors Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveLanguagesExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Languages.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelLanguagesReport msExcelReport = new MacroscopeExcelLanguagesReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving HrefLang Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving HrefLang Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSavePageMetadataExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Page-Metadata.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelPageMetadataReport msExcelReport = new MacroscopeExcelPageMetadataReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Page Metadata Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Page Metadata Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSavePageContentsExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Page-Contents.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelPageContentsReport msExcelReport = new MacroscopeExcelPageContentsReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Page Contents Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Page Contents Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveUriAnalysisExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-URI-Analysis.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelUriReport msExcelReport = new MacroscopeExcelUriReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving URI Analysis Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving URI Analysis Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveRedirectsExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Redirects.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelRedirectsReport msExcelReport = new MacroscopeExcelRedirectsReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Redirects Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Redirects Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveKeywordAnalysisExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Keyword-Analysis.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeDoublePercentageProgressForm ProgressForm;
        MacroscopeExcelKeywordAnalysisReport msExcelReport;

        ProgressForm = new MacroscopeDoublePercentageProgressForm( MainForm: this );
        msExcelReport = new MacroscopeExcelKeywordAnalysisReport( ProgressFormDialogue: ProgressForm );

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Keyword Analysis Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Keyword Analysis Excel Report", ex.Message );
        }
        finally
        {
          ProgressForm.DoClose();
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveDuplicateContentExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Duplicate-Content.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        this.Enabled = false;

        string Path;
        MacroscopeTriplePercentageProgressForm ProgressForm;
        MacroscopeExcelDuplicateContent msExcelReport;

        Path = Dialog.FileName;
        ProgressForm = new MacroscopeTriplePercentageProgressForm( MainForm: this );
        msExcelReport = new MacroscopeExcelDuplicateContent( ProgressFormDialogue: ProgressForm );

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( JobMaster: this.JobMaster, OutputFilename: Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Duplicate Content Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Duplicate Content Excel Report", ex.Message );
        }
        finally
        {
          ProgressForm.DoClose();
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveContactDetailsExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Contact-Details.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelContactDetailsReport msExcelReport = new MacroscopeExcelContactDetailsReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Contact Details Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Contact Details Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveRemarksExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Remarks.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MaroscopeExcelRemarksReport msExcelReport = new MaroscopeExcelRemarksReport();

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Remarks Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Remarks Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveCustomFilterExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Custom-Filters.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelCustomFilterReport msExcelReport = new MacroscopeExcelCustomFilterReport( NewCustomFilter: this.CustomFilter );

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Custom Filters Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Custom Filters Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveDataExtractorsExcelReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xlsx";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Data-Extractors.xlsx";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelDataExtractorReport msExcelReport;

        msExcelReport = new MacroscopeExcelDataExtractorReport(
          NewDataExtractorCssSelectors: this.DataExtractorCssSelectors,
          NewDataExtractorRegexes: this.DataExtractorRegexes,
          NewDataExtractorXpaths: this.DataExtractorXpaths
        );

        Cursor.Current = Cursors.WaitCursor;

        try
        {
          msExcelReport.WriteXslx( this.JobMaster, Path );
        }
        catch( MacroscopeSaveExcelFileException ex )
        {
          this.DialogueBoxError( "Error saving Data Extractors Excel Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Data Extractors Excel Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      if( Dialog != null )
      {
        Dialog.Dispose();
      }

      this.Enabled = true;

    }

    /**************************************************************************/

  }

}
