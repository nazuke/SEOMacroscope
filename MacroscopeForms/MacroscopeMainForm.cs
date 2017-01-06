using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

	public partial class MacroscopeMainForm : Form
	{
		
		/**************************************************************************/

		MacroscopeDisplayStructure msDisplayStructure;
		MacroscopeDisplayHrefLang msDisplayHrefLang;
		MacroscopeDisplayEmailAddresses msDisplayEmailAddresses;
		MacroscopeDisplayTelephoneNumbers msDisplayTelephoneNumbers;
		MacroscopeDisplayHistory msDisplayHistory;
		
		Thread tScanningThread;
		MacroscopeJobMaster msJobMaster;
			
		/**************************************************************************/

		public MacroscopeMainForm ()
		{
			
			InitializeComponent();// The InitializeComponent() call is required for Windows Forms designer support.

			MacroscopePreferences.LoadPreferences();
						
			msDisplayStructure = new MacroscopeDisplayStructure ( this );
			msDisplayHrefLang = new MacroscopeDisplayHrefLang ( this );
			msDisplayEmailAddresses = new MacroscopeDisplayEmailAddresses ( this );
			msDisplayTelephoneNumbers = new MacroscopeDisplayTelephoneNumbers ( this );
			msDisplayHistory = new MacroscopeDisplayHistory ( this );
						
			this.textBoxURL.Text = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" ).ToString();

			msJobMaster = new MacroscopeJobMaster ( this );

			this.ScanningControlsEnable( true );

		}
		
		/**************************************************************************/

		public ListView GetDisplayStructure ()
		{
			return( this.listViewStructure );
		}

		/**************************************************************************/

		public DataGridView GetDisplayHrefLang ()
		{
			return( this.dataGridHrefLang );
		}

		/**************************************************************************/
		
		public ListView GetDisplayEmailAddresses ()
		{
			return( this.listViewEmailAddresses );
		}
		
		/**************************************************************************/

		public ListView GetDisplayTelephoneNumbers ()
		{
			return( this.listViewTelephoneNumbers );
		}

		/**************************************************************************/

		public ListView GetDisplayHistory ()
		{
			return( this.listViewHistory );
		}

		/**************************************************************************/
				
		public string GetURL ()
		{
			return( this.textBoxURL.Text );
		}

		/**************************************************************************/

		public void SetURL ( string sURL )
		{
			this.textBoxURL.Text = sURL;
		}

		/**************************************************************************/
				
		void CallbackFileExit ( object sender, EventArgs e )
		{
			
			if( this.tScanningThread.IsAlive ) {
				this.tScanningThread.Abort();
			}

			Program.Exit();

		}

		/**************************************************************************/
				
		void CallbackScanStart ( object sender, EventArgs e )
		{
			this.ScanningControlsStart( true );

			MacroscopePreferences.SavePreferences();

			this.tScanningThread = new Thread ( new ThreadStart ( this.ScanningThread ) );
			this.tScanningThread.Start();

		}

		/**************************************************************************/
		
		void CallbackScanStop ( object sender, EventArgs e )
		{

			this.ScanningControlsStop( true );
						
			this.msJobMaster.WorkersPause();
			this.msJobMaster.WorkersStop();

			/*
			while( !this.msJobMaster.WorkersStopped() ) {
				debug_msg( "CallbackScanStop: WAITING" );
				Thread.Sleep( 100 );
			}
			*/
			
		}

		/**************************************************************************/

		void CallbackScanPause ( object sender, EventArgs e )
		{
			
			this.ScanningControlsPause( true );
			
			if( this.tScanningThread.IsAlive ) {

				while( !this.msJobMaster.WorkersPause() ) {
					Thread.Sleep( 100 );
				}

				if( this.msJobMaster.WorkersPause() ) {
					;
				}

			}

		}
		
		/**************************************************************************/

		void CallbackScanResume ( object sender, EventArgs e )
		{
			this.ScanningControlsResume( true );
			if( this.tScanningThread.IsAlive ) {
				if( this.msJobMaster.WorkersArePaused() ) {
					this.msJobMaster.WorkersUnpause();
				}
			}
		}

		/**************************************************************************/

		void CallbackScanReset ( object sender, EventArgs e )
		{

			if( this.msJobMaster.WorkersStopped() ) {

				this.ScanningControlsReset( true );
							
				this.dataGridHrefLang.DataSource = null;

				this.msJobMaster.HistoryClear();
				
				this.msJobMaster = new MacroscopeJobMaster ( this );

			}
			
		}
		
		/**************************************************************************/
		
		public void CallbackScanComplete ()
		{
			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate {
							this.UpdateStatusBar();
							this.ScanningControlsComplete( true );
						}
					)
				);
			} else {
				this.UpdateStatusBar();
				this.ScanningControlsComplete( true );
			}
		}
		
		/**************************************************************************/

		void CallbackDataError ( object sender, DataGridViewDataErrorEventArgs e )
		{
			debug_msg( "EVENT: DataError" );
		}
		
		/**************************************************************************/
		
		void CallbackRowsAdded ( object sender, DataGridViewRowsAddedEventArgs e )
		{
			this.Update();
		}

		/**************************************************************************/

		void ScanningControlsEnable ( Boolean bState )
		{
			this.textBoxURL.Enabled = true;
			this.buttonStart.Enabled = true;
			this.buttonStop.Enabled = false;
			this.buttonPause.Enabled = false;
			this.buttonResume.Enabled = false;
			this.buttonReset.Enabled = false;
		}

		void ScanningControlsStart ( Boolean bState )
		{
			this.textBoxURL.Enabled = false;
			this.buttonStart.Enabled = false;
			this.buttonStop.Enabled = true;
			this.buttonPause.Enabled = true;
			this.buttonResume.Enabled = false;
			this.buttonReset.Enabled = false;
		}

		void ScanningControlsStop ( Boolean bState )
		{
			this.textBoxURL.Enabled = true;
			this.buttonStart.Enabled = true;
			this.buttonStop.Enabled = false;
			this.buttonPause.Enabled = false;
			this.buttonResume.Enabled = false;
			this.buttonReset.Enabled = true;
		}

		void ScanningControlsPause ( Boolean bState )
		{
			this.textBoxURL.Enabled = false;
			this.buttonStart.Enabled = false;
			this.buttonStop.Enabled = true;
			this.buttonPause.Enabled = false;
			this.buttonResume.Enabled = true;
			this.buttonReset.Enabled = false;
		}

		void ScanningControlsResume ( Boolean bState )
		{
			this.textBoxURL.Enabled = false;
			this.buttonStart.Enabled = false;
			this.buttonStop.Enabled = true;
			this.buttonPause.Enabled = true;
			this.buttonResume.Enabled = false;
			this.buttonReset.Enabled = false;
		}

		void ScanningControlsReset ( Boolean bState )
		{
			this.textBoxURL.Enabled = true;
			this.buttonStart.Enabled = true;
			this.buttonStop.Enabled = false;
			this.buttonPause.Enabled = false;
			this.buttonResume.Enabled = false;
			this.buttonReset.Enabled = false;
		}
		
		void ScanningControlsComplete ( Boolean bState )
		{
			this.textBoxURL.Enabled = true;
			this.buttonStart.Enabled = true;
			this.buttonStop.Enabled = false;
			this.buttonPause.Enabled = false;
			this.buttonResume.Enabled = false;
			this.buttonReset.Enabled = true;
		}

		/**************************************************************************/

		void ScanningThread ()
		{
			debug_msg( "Scanning Thread: Started." );
			this.msJobMaster.StartUrl = this.GetURL();
			this.msJobMaster.Execute();
			debug_msg( "Scanning Thread: Done." );
		}

		/**************************************************************************/

		public void UpdateDisplay ( MacroscopeJobMaster msJobMaster )
		{

			this.msDisplayStructure.RefreshData( msJobMaster.DocCollectionGet() );

			this.msDisplayHrefLang.RefreshData( msJobMaster.DocCollectionGet(), msJobMaster.LocalesGet() );

			this.msDisplayEmailAddresses.RefreshData( msJobMaster.DocCollectionGet() );
						
			this.msDisplayTelephoneNumbers.RefreshData( msJobMaster.DocCollectionGet() );
						
			this.msDisplayHistory.RefreshData( msJobMaster.HistoryGet() );
						
			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate {
							this.Refresh();
							this.Update();
						}
					)
				);
			} else {
				this.Refresh();
				this.Update();
			}

		}

		/**************************************************************************/

		public void UpdateDisplaySingle ( MacroscopeJobMaster msJobMaster, string sURL )
		{

			this.msDisplayStructure.RefreshDataSingle( msJobMaster.DocCollectionGet().Get( sURL ), sURL );

			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate {
							this.Refresh();
							this.Update();
						}
					)
				);
			} else {
				this.Refresh();
				this.Update();
			}

		}

		/**************************************************************************/

		public void UpdateDisplayHrefLang ( MacroscopeJobMaster msJobMaster )
		{

			this.msDisplayHrefLang.RefreshData( msJobMaster.DocCollectionGet(), msJobMaster.LocalesGet() );

			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate {
							this.Refresh();
							this.Update();
						}
					)
				);
			} else {
				this.Refresh();
				this.Update();
			}

		}

		/**************************************************************************/

		public void UpdateStatusBar ()
		{

			string sThreads = string.Format( "Threads: {0}", this.msJobMaster.RunningThreadsCount().ToString() );
			string sUrlCount = string.Format( "URLs in Queue: {0}", this.msJobMaster.UrlQueueCount().ToString() );
			string sUrlsFound = string.Format( "URLs Found: {0}", this.msJobMaster.DocCollectionGet().Count() );

			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate {
							this.toolStripThreads.Text = sThreads;
							this.toolStripUrlCount.Text = sUrlCount;
							this.toolStripFound.Text = sUrlsFound;
						}
					)
				);
			} else {
				this.toolStripThreads.Text = sThreads;
				this.toolStripUrlCount.Text = sUrlCount;
				this.toolStripFound.Text = sUrlsFound;
			}
			
		}

		/**************************************************************************/
		
		void CallbackDataBindingComplete ( object sender, DataGridViewBindingCompleteEventArgs e )
		{
			DataGridView dgvGrid = ( DataGridView )sender;
			dgvGrid.AutoResizeColumns();
		}

		/**************************************************************************/

		void CallbackListViewClick ( object sender, EventArgs e )
		{

			debug_msg( string.Format( "CallbackListViewClick: {0}", "" ) );
			
			ListView lvListView = ( ListView )sender;
						
			debug_msg( string.Format( "CallbackListViewClick: {0}", lvListView.SelectedItems.ToString() ) );
			
			lock( lvListView ) {

				foreach( ListViewItem lvItem in lvListView.SelectedItems ) {

					string sURL = lvItem.SubItems[ 0 ].Text.ToString();
					
					debug_msg( string.Format( "CallbackListViewClick: {0}", sURL ) );
					this.macroscopeDocumentDetailsMain.UpdateDisplay( this.msJobMaster, sURL );

				}

			}

			debug_msg( string.Format( "" ) );
			
		}

		/**************************************************************************/
		
		void CallbackSaveOverviewExcelReport ( object sender, EventArgs e )
		{
			SaveFileDialog Dialog = new SaveFileDialog ();
			Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
			Dialog.FilterIndex = 2;
			Dialog.RestoreDirectory = true;
			Dialog.DefaultExt = "xlsx";
			Dialog.AddExtension = true;
			if( Dialog.ShowDialog() == DialogResult.OK ) {
				string sPath = Dialog.FileName;
				MacroscopeExcelOverviewReport msExcelReport = new MacroscopeExcelOverviewReport ();
				msExcelReport.WriteXslx( this.msJobMaster, sPath );
			}
			Dialog.Dispose();
		}
		
		/**************************************************************************/
				
		void CallbackSaveHrefLangExcelReport ( object sender, EventArgs e )
		{
			SaveFileDialog Dialog = new SaveFileDialog ();
			Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
			Dialog.FilterIndex = 2;
			Dialog.RestoreDirectory = true;
			Dialog.DefaultExt = "xlsx";
			Dialog.AddExtension = true;
			if( Dialog.ShowDialog() == DialogResult.OK ) {
				string sPath = Dialog.FileName;
				MacroscopeExcelHrefLangReport msExcelReport = new MacroscopeExcelHrefLangReport ();
				msExcelReport.WriteXslx( this.msJobMaster, sPath );
			}
			Dialog.Dispose();
		}

		/**************************************************************************/
	
		static void debug_msg ( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		static void debug_msg ( String sMsg, int iOffset )
		{
			String sMsgPadded = new String ( ' ', iOffset * 2 ) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}

		

		


		/**************************************************************************/
			
	}
	
}
