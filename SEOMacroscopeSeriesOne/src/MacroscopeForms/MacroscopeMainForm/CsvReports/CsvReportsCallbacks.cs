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
using System.Collections.Generic;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Report Save Dialogue Boxes ********************************************/

    private const int CsvReportMegabytesRamRequired = 64;

    /**************************************************************************/

    private void CallbackExportListViewToCsvReport ( object sender, EventArgs e )
    {

      KeyValuePair<string, ListView> SelectedListView;

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Report.csv";

      this.Enabled = false;

      SelectedListView = this.GetTabPageListView();

      if( SelectedListView.Value != null )
      {

        if( Dialog.ShowDialog() == DialogResult.OK )
        {

          string Path = Dialog.FileName;
          MacroscopeCsvExportListViewReport CsvReport;

          CsvReport = new MacroscopeCsvExportListViewReport( SelectedListView: SelectedListView.Value );

          try
          {
            CsvReport.WriteCsv( this.JobMaster, Path );
          }
          catch( MacroscopeInsufficientMemoryException ex )
          {
            this.DialogueBoxError( "Error saving CSV Report", ex.Message );
          }
          catch( MacroscopeSaveCsvFileException ex )
          {
            this.DialogueBoxError( "Error saving CSV Report", ex.Message );
          }
          catch( Exception ex )
          {
            this.DialogueBoxError( "Error saving CSV Report", ex.Message );
          }

        }

      }
      else
      {
        this.DialogueBoxError( "Error saving CSV Report", "Cannot export this view type" );
      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /**************************************************************************/

    private void CallbackSaveOverviewCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Overview.csv";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvOverviewReport CsvReport = new MacroscopeCsvOverviewReport();

        try
        {
          CsvReport.WriteCsv( this.JobMaster, Path );
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Overview CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Overview CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Overview CSV Report", ex.Message );
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveErrorsCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Errors.csv";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvErrorsReport CsvReport = new MacroscopeCsvErrorsReport();

        try
        {
          CsvReport.WriteCsv( this.JobMaster, Path );
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Errors CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Errors CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Errors CSV Report", ex.Message );
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveRobotsCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Robots.csv";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvRobotsReport CsvReport = new MacroscopeCsvRobotsReport();

        try
        {
          CsvReport.WriteCsv( this.JobMaster, Path );
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Robots CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Robots CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Robots CSV Report", ex.Message );
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveSitemapErrorsCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Sitemap-Errors.csv";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvSitemapErrorsReport CsvReport = new MacroscopeCsvSitemapErrorsReport();

        try
        {
          CsvReport.WriteCsv( this.JobMaster, Path );
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Errors CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Errors CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Errors CSV Report", ex.Message );
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** Broken Links Report ------------------------------------------------ **/

    private void CallbackSaveBrokenLinksCsvReportBrokenLinks ( object sender, EventArgs e )
    {
      this.CallbackSaveBrokenLinksCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvBrokenLinksReport.OutputWorksheet.BROKEN_LINKS,
        OutputFilename: "Macroscope-Broken-Links-Broken-Links.csv"
      );
    }

    private void CallbackSaveBrokenLinksCsvReportGoodLinks ( object sender, EventArgs e )
    {
      this.CallbackSaveBrokenLinksCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvBrokenLinksReport.OutputWorksheet.GOOD_LINKS,
        OutputFilename: "Macroscope-Broken-Links-Good-Links.csv"
      );
    }

    private void CallbackSaveBrokenLinksCsvReportRedirectedLinks ( object sender, EventArgs e )
    {
      this.CallbackSaveBrokenLinksCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvBrokenLinksReport.OutputWorksheet.REDIRECTED_LINKS,
        OutputFilename: "Macroscope-Broken-Links-Redirected-Links.csv"
      );
    }

    private void CallbackSaveBrokenLinksCsvReport (
      object sender,
      EventArgs e,
      MacroscopeCsvBrokenLinksReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvBrokenLinksReport CsvReport = new MacroscopeCsvBrokenLinksReport();

        try
        {

          Cursor.Current = Cursors.WaitCursor;

          CsvReport.WriteCsv(
            JobMaster: this.JobMaster,
            SelectedOutputWorksheet: SelectedOutputWorksheet,
            OutputFilename: Path
          );

          Cursor.Current = Cursors.Default;

        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Broken Links CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Broken Links CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Broken Links CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** Page Metadata Report **************************************************/

    private void CallbackSavePageMetadataCsvReportTitles ( object sender, EventArgs e )
    {
      this.CallbackSavePageMetadataCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvPageMetadataReport.OutputWorksheet.TITLES,
        OutputFilename: "Macroscope-Page-Metadata-Titles.csv"
      );
    }

    private void CallbackSavePageMetadataCsvReportDescriptions ( object sender, EventArgs e )
    {
      this.CallbackSavePageMetadataCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvPageMetadataReport.OutputWorksheet.DESCRIPTIONS,
        OutputFilename: "Macroscope-Page-Metadata-Descriptions.csv"
      );
    }

    private void CallbackSavePageMetadataCsvReportKeywords ( object sender, EventArgs e )
    {
      this.CallbackSavePageMetadataCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvPageMetadataReport.OutputWorksheet.KEYWORDS,
        OutputFilename: "Macroscope-Page-Metadata-Keywords.csv"
      );
    }

    private void CallbackSavePageMetadataCsvReport (
      object sender,
      EventArgs e,
      MacroscopeCsvPageMetadataReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvPageMetadataReport CsvReport = new MacroscopeCsvPageMetadataReport();

        try
        {

          Cursor.Current = Cursors.WaitCursor;

          CsvReport.WriteCsv(
            JobMaster: this.JobMaster,
            SelectedOutputWorksheet: SelectedOutputWorksheet,
            OutputFilename: Path
          );

          Cursor.Current = Cursors.Default;

        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Page Metadata CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Page Metadata CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Page Metadata CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** Page Contents Report **************************************************/

    private void CallbackSavePageContentsCsvReportHeadings ( object sender, EventArgs e )
    {
      this.CallbackSavePageContentsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvPageContentsReport.OutputWorksheet.HEADINGS,
        OutputFilename: "Macroscope-Page-Contents-Headings.csv"
      );
    }

    private void CallbackSavePageContentsCsvReportPageText ( object sender, EventArgs e )
    {
      this.CallbackSavePageContentsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvPageContentsReport.OutputWorksheet.PAGETEXT,
        OutputFilename: "Macroscope-Page-Contents-Page-Text.csv"
      );
    }

    private void CallbackSavePageContentsCsvReport (
      object sender,
      EventArgs e,
      MacroscopeCsvPageContentsReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvPageContentsReport CsvReport = new MacroscopeCsvPageContentsReport();

        try
        {

          Cursor.Current = Cursors.WaitCursor;

          CsvReport.WriteCsv(
            JobMaster: this.JobMaster,
            SelectedOutputWorksheet: SelectedOutputWorksheet,
            OutputFilename: Path
          );

          Cursor.Current = Cursors.Default;

        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Page Contents CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Page Contents CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Page Contents CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveUriAnalysisCsvReportLinks ( object sender, EventArgs e )
    {
      this.CallbackSaveUriAnalysisCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvUriReport.OutputWorksheet.LINKS,
        OutputFilename: "Macroscope-URI-Analysis-Links.csv"
      );
    }

    private void CallbackSaveUriAnalysisCsvReportHyperlinks ( object sender, EventArgs e )
    {
      this.CallbackSaveUriAnalysisCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvUriReport.OutputWorksheet.HYPERLINKS,
        OutputFilename: "Macroscope-URI-Analysis-Hyperlinks.csv"
      );
    }

    private void CallbackSaveUriAnalysisCsvReportUris ( object sender, EventArgs e )
    {
      this.CallbackSaveUriAnalysisCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvUriReport.OutputWorksheet.URIS,
        OutputFilename: "Macroscope-URI-Analysis-URIs.csv"
      );
    }

    private void CallbackSaveUriAnalysisCsvReportOrphanedPages ( object sender, EventArgs e )
    {
      this.CallbackSaveUriAnalysisCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvUriReport.OutputWorksheet.ORPHANS,
        OutputFilename: "Macroscope-URI-Analysis-Orphaned-Pages.csv"
      );
    }

    private void CallbackSaveUriAnalysisCsvReport (
      object sender,
      EventArgs e,
      MacroscopeCsvUriReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvUriReport CsvReport = new MacroscopeCsvUriReport();

        try
        {

          Cursor.Current = Cursors.WaitCursor;

          CsvReport.WriteCsv(
            JobMaster: this.JobMaster,
            SelectedOutputWorksheet: SelectedOutputWorksheet,
            OutputFilename: Path
          );

          Cursor.Current = Cursors.Default;

        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving URI Analysis CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving URI Analysis CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving URI Analysis CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveRedirectsCsvReportRedirectsAudit ( object sender, EventArgs e )
    {
      this.CallbackSaveRedirectsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvRedirectsReport.OutputWorksheet.REDIRECTS_AUDIT,
        OutputFilename: "Macroscope-Redirects-Audit.csv"
      );
    }

    private void CallbackSaveRedirectsCsvReportRedirectChains ( object sender, EventArgs e )
    {
      this.CallbackSaveRedirectsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvRedirectsReport.OutputWorksheet.REDIRECT_CHAINS,
        OutputFilename: "Macroscope-Redirect-Chains.csv"
      );
    }

    private void CallbackSaveRedirectsCsvReport (
      object sender,
      EventArgs e,
      MacroscopeCsvRedirectsReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvRedirectsReport CsvReport = new MacroscopeCsvRedirectsReport();

        try
        {
          Cursor.Current = Cursors.WaitCursor;
          CsvReport.WriteCsv( this.JobMaster, SelectedOutputWorksheet, Path );
          Cursor.Current = Cursors.Default;
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Redirects CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Redirects CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Redirects CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveDuplicateContentCsvReportTitles ( object sender, EventArgs e )
    {
      this.CallbackSaveDuplicateContentCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvDuplicateContentReport.OutputWorksheet.TITLES,
        OutputFilename: "Macroscope-Duplicate-Content-Titles.csv"
      );
    }

    private void CallbackSaveDuplicateContentCsvReportChecksums ( object sender, EventArgs e )
    {
      this.CallbackSaveDuplicateContentCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvDuplicateContentReport.OutputWorksheet.CHECKSUMS,
        OutputFilename: "Macroscope-Duplicate-Content-Checksums.csv"
      );
    }

    private void CallbackSaveDuplicateContentCsvReportEtags ( object sender, EventArgs e )
    {
      this.CallbackSaveDuplicateContentCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvDuplicateContentReport.OutputWorksheet.ETAGS,
        OutputFilename: "Macroscope-Duplicate-Content-Etags.csv"
      );
    }

    private void CallbackSaveDuplicateContentCsvReportPages ( object sender, EventArgs e )
    {
      this.CallbackSaveDuplicateContentCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvDuplicateContentReport.OutputWorksheet.PAGES,
        OutputFilename: "Macroscope-Duplicate-Content-Pages.csv"
      );
    }

    private void CallbackSaveDuplicateContentCsvReport (
      object sender,
      EventArgs e,
      MacroscopeCsvDuplicateContentReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeTriplePercentageProgressForm ProgressForm;
        MacroscopeCsvDuplicateContentReport CsvReport;

        ProgressForm = new MacroscopeTriplePercentageProgressForm( MainForm: this );

        CsvReport = new MacroscopeCsvDuplicateContentReport(
          ProgressFormDialogue: ProgressForm
        );

        try
        {

          Cursor.Current = Cursors.WaitCursor;

          CsvReport.WriteCsv(
            JobMaster: this.JobMaster,
            SelectedOutputWorksheet: SelectedOutputWorksheet,
            OutputFilename: Path
          );

          Cursor.Current = Cursors.Default;

        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Duplicate Content CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Duplicate Content CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Duplicate Content CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** Contact Details ---------------------------------------------------- **/

    private void CallbackSaveContactDetailsCsvReportEmail ( object sender, EventArgs e )
    {
      this.CallbackSaveContactDetailsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvContactDetailsReport.OutputWorksheet.EMAIL,
        OutputFilename: "Macroscope-Contact-Details-Telephone.csv"
      );
    }

    private void CallbackSaveContactDetailsCsvReportTelephone ( object sender, EventArgs e )
    {
      this.CallbackSaveContactDetailsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvContactDetailsReport.OutputWorksheet.TELEPHONE,
        OutputFilename: "Macroscope-Contact-Details-Email.csv"
      );
    }

    private void CallbackSaveContactDetailsCsvReport (
      object sender,
      EventArgs e,
      MacroscopeCsvContactDetailsReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvContactDetailsReport CsvReport = new MacroscopeCsvContactDetailsReport();

        try
        {

          Cursor.Current = Cursors.WaitCursor;

          CsvReport.WriteCsv(
            JobMaster: this.JobMaster,
            SelectedOutputWorksheet: SelectedOutputWorksheet,
            OutputFilename: Path
          );

          Cursor.Current = Cursors.Default;

        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Contact Details CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Contact Details CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Contact Details CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** Remarks ------------------------------------------------------------ **/

    private void CallbackSaveRemarksCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Remarks.csv";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MaroscopeCsvRemarksReport CsvReport = new MaroscopeCsvRemarksReport();

        try
        {
          Cursor.Current = Cursors.WaitCursor;
          CsvReport.WriteCsv( this.JobMaster, Path );
          Cursor.Current = Cursors.Default;
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Remarks CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Remarks CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Remarks CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveCustomFilterCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog();

      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Custom-Filters.csv";

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvCustomFilterReport CsvReport = new MacroscopeCsvCustomFilterReport( NewCustomFilter: this.CustomFilter );

        try
        {
          Cursor.Current = Cursors.WaitCursor;
          CsvReport.WriteCsv( this.JobMaster, Path );
          Cursor.Current = Cursors.Default;
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Custom Filters CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Custom Filters CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Custom Filters CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /** Data Exctractors ******************************************************/

    private void CallbackSaveDataExtractorsCsvReportCssSelectors ( object sender, EventArgs e )
    {
      this.CallbackSaveDataExtractorsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvDataExtractorReport.OutputWorksheet.CSS_SELECTORS,
        OutputFilename: "Macroscope-Data-Extractors-CSS-Selectors.csv"
      );
    }

    private void CallbackSaveDataExtractorsCsvReportRegexes ( object sender, EventArgs e )
    {
      this.CallbackSaveDataExtractorsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvDataExtractorReport.OutputWorksheet.REGEXES,
        OutputFilename: "Macroscope-Data-Extractors-Regexes.csv"
      );
    }

    private void CallbackSaveDataExtractorsCsvReportXpaths ( object sender, EventArgs e )
    {
      this.CallbackSaveDataExtractorsCsvReport(
        sender: sender,
        e: e,
        SelectedOutputWorksheet: MacroscopeCsvDataExtractorReport.OutputWorksheet.XPATHS,
        OutputFilename: "Macroscope-Data-Extractors-XPaths.csv"
      );
    }

    private void CallbackSaveDataExtractorsCsvReport (
      object sender,
      EventArgs e,
      MacroscopeCsvDataExtractorReport.OutputWorksheet SelectedOutputWorksheet,
      string OutputFilename
    )
    {

      SaveFileDialog Dialog = new SaveFileDialog();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      this.Enabled = false;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvDataExtractorReport CsvReport;

        CsvReport = new MacroscopeCsvDataExtractorReport(
          NewDataExtractorCssSelectors: this.DataExtractorCssSelectors,
          NewDataExtractorRegexes: this.DataExtractorRegexes,
          NewDataExtractorXpaths: this.DataExtractorXpaths
        );

        try
        {

          Cursor.Current = Cursors.WaitCursor;

          CsvReport.WriteCsv(
            JobMaster: this.JobMaster,
            SelectedOutputWorksheet: SelectedOutputWorksheet,
            OutputFilename: Path
          );

          Cursor.Current = Cursors.Default;

        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Page Metadata CSV Report", ex.Message );
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Page Metadata CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Page Metadata CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

      this.Enabled = true;

    }

    /**************************************************************************/

  }

}
