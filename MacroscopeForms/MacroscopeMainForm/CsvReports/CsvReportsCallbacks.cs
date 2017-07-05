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
using System.Windows.Forms;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Report Save Dialogue Boxes ********************************************/

    private const int CsvReportMegabytesRamRequired = 64;

    /**************************************************************************/

    private void CallbackSaveOverviewCsvReport ( object sender, EventArgs e )
    {
      
      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Overview.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvOverviewReport CsvReport = new MacroscopeCsvOverviewReport ();

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            CsvReport.WriteCsv( this.JobMaster, Path );
          }
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
                
    }

    /** -------------------------------------------------------------------- **/  

    private void CallbackSaveErrorsCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Errors.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelErrorsReport CsvReport = new MacroscopeExcelErrorsReport ();

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            CsvReport.WriteXslx( this.JobMaster, Path );
          }
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

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveBrokenLinksCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Broken-Links.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelBrokenLinksReport CsvReport = new MacroscopeExcelBrokenLinksReport ();

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            CsvReport.WriteXslx( this.JobMaster, Path );
          }
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

      }

      Dialog.Dispose();

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveLanguagesCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Languages.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelLanguagesReport CsvReport = new MacroscopeExcelLanguagesReport ();

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            CsvReport.WriteXslx( this.JobMaster, Path );
            Cursor.Current = Cursors.Default;
          }
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving HrefLang CSV Report", ex.Message );       
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving HrefLang CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving HrefLang CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }

      }

      Dialog.Dispose();

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

      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvPageMetadataReport CsvReport = new MacroscopeCsvPageMetadataReport ();

        try
        {
          
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            
            Cursor.Current = Cursors.WaitCursor;
            
            CsvReport.WriteCsv(
              JobMaster: this.JobMaster,
              SelectedOutputWorksheet: SelectedOutputWorksheet,
              OutputFilename: Path
            );
            
            Cursor.Current = Cursors.Default;
          
          }
          
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

      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvPageContentsReport CsvReport = new MacroscopeCsvPageContentsReport ();

        try
        {
          
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            
            Cursor.Current = Cursors.WaitCursor;
            
            CsvReport.WriteCsv(
              JobMaster: this.JobMaster,
              SelectedOutputWorksheet: SelectedOutputWorksheet,
              OutputFilename: Path
            );
            
            Cursor.Current = Cursors.Default;
          
          }
          
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

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveUriAnalysisCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-URI-Analysis.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelUriReport CsvReport = new MacroscopeExcelUriReport ();

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            CsvReport.WriteXslx( this.JobMaster, Path );
            Cursor.Current = Cursors.Default;
          }
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

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveRedirectsCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();
      
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Redirects.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelRedirectsReport CsvReport = new MacroscopeExcelRedirectsReport ();

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            CsvReport.WriteXslx( this.JobMaster, Path );
            Cursor.Current = Cursors.Default;
          }
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

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveKeywordAnalysisCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();

      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Keyword-Analysis.csv";
      
      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;

        MacroscopeTriplePercentageProgressForm ProgressForm = new MacroscopeTriplePercentageProgressForm ( MainForm: this );

        MacroscopeExcelKeywordAnalysisReport CsvReport = new MacroscopeExcelKeywordAnalysisReport (
                                                           ProgressFormDialogue: ProgressForm
                                                         );
        
        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            CsvReport.WriteXslx( this.JobMaster, Path );
            Cursor.Current = Cursors.Default;
          }
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Keyword Analysis CSV Report", ex.Message );       
        }
        catch( MacroscopeSaveCsvFileException ex )
        {
          this.DialogueBoxError( "Error saving Keyword Analysis CSV Report", ex.Message );
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Keyword Analysis CSV Report", ex.Message );
        }
        finally
        {
          Cursor.Current = Cursors.Default;
        }
    
      }
      
      Dialog.Dispose();
    
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveDuplicateContentCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();
      
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Duplicate-Content.csv";
      
      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;

        MacroscopeTriplePercentageProgressForm ProgressForm = new MacroscopeTriplePercentageProgressForm ( MainForm: this );

        MacroscopeExcelDuplicateContent CsvReport = new MacroscopeExcelDuplicateContent (
                                                      ProgressFormDialogue: ProgressForm
                                                    );
        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            CsvReport.WriteXslx( JobMaster: this.JobMaster, OutputFilename: Path );
            Cursor.Current = Cursors.Default;
          }
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
          ProgressForm.DoClose();
          Cursor.Current = Cursors.Default;
        }
      
      }
      
      Dialog.Dispose();
    
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveContactDetailsCsvReport ( object sender, EventArgs e )
    {
    
      SaveFileDialog Dialog = new SaveFileDialog ();
      
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Contact-Details.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeExcelContactDetailsReport CsvReport = new MacroscopeExcelContactDetailsReport ();

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            CsvReport.WriteXslx( this.JobMaster, Path );
            Cursor.Current = Cursors.Default;
          }
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

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveRemarksCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();
      
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Remarks.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MaroscopeExcelRemarksReport CsvReport = new MaroscopeExcelRemarksReport ();

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            CsvReport.WriteXslx( this.JobMaster, Path );
            Cursor.Current = Cursors.Default;
          }
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

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveCustomFilterCsvReport ( object sender, EventArgs e )
    {

      SaveFileDialog Dialog = new SaveFileDialog ();
      
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Custom-Filters.csv";

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvCustomFilterReport CsvReport = new MacroscopeCsvCustomFilterReport ( NewCustomFilter: this.CustomFilter );

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            CsvReport.WriteCsv( this.JobMaster, Path );
            Cursor.Current = Cursors.Default;
          }
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

      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "csv";
      Dialog.AddExtension = true;
      Dialog.FileName = OutputFilename;

      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Path = Dialog.FileName;
        MacroscopeCsvDataExtractorReport CsvReport;

        CsvReport = new MacroscopeCsvDataExtractorReport (
          NewDataExtractorCssSelectors: this.DataExtractorCssSelectors,
          NewDataExtractorRegexes: this.DataExtractorRegexes,
          NewDataExtractorXpaths: this.DataExtractorXpaths
        );

        try
        {
          
          if( Macroscope.MemoryGuard( RequiredMegabytes: CsvReportMegabytesRamRequired ) )
          {
            
            Cursor.Current = Cursors.WaitCursor;
            
            CsvReport.WriteCsv(
              JobMaster: this.JobMaster,
              SelectedOutputWorksheet: SelectedOutputWorksheet,
              OutputFilename: Path
            );
            
            Cursor.Current = Cursors.Default;
          
          }
          
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

    }

    /**************************************************************************/

  }

}
