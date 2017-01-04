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
			
			this.textBoxURL.Text = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" ).ToString();

			msJobMaster = new MacroscopeJobMaster ( this );


						
			this.ScanningControlsEnable( true );

		}
		
		/**************************************************************************/

		public DataGridView GetDisplayStructure ()
		{
			return( this.dataGridStructure );
		}

		/**************************************************************************/

		public DataGridView GetDisplayHrefLang ()
		{
			return( this.dataGridHrefLang );
		}

		/**************************************************************************/
		
		public DataGridView GetDisplayEmailAddresses ()
		{
			return( this.dataGridEmailAddresses );
		}
		
		/**************************************************************************/
		public DataGridView GetDisplayTelephoneNumbers ()
		{
			return( this.dataGridTelephoneNumbers );
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
				if( this.msJobMaster.IsWorkersPaused() ) {
					this.msJobMaster.WorkersUnpause();
				}
			}
		}

		/**************************************************************************/

		void CallbackScanReset ( object sender, EventArgs e )
		{

			if( this.msJobMaster.WorkersStopped() ) {

				this.ScanningControlsReset( true );
							
				this.dataGridStructure.DataSource = null;
				this.dataGridHrefLang.DataSource = null;
				this.dataGridEmailAddresses.DataSource = null;
				this.dataGridTelephoneNumbers.DataSource = null;

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
							this.ScanningControlsReset( true );
						}
					)
				);
			} else {
				this.ScanningControlsReset( true );
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

		/**************************************************************************/

		void ScanningThread ()
		{
			debug_msg( "Scanning Thread: Started." );
			this.msJobMaster.StartUrl = this.GetURL();
			this.msJobMaster.Execute();
			debug_msg( "Scanning Thread: Done." );
		}

		/**************************************************************************/

		public void UpdateDisplayStructure ( MacroscopeJobMaster msJobMaster )
		{

			this.msDisplayStructure.RefreshData( msJobMaster.GetDocCollection() );

			this.msDisplayHrefLang.RefreshData( msJobMaster.GetDocCollection(), msJobMaster.GetLocales() );

			this.msDisplayEmailAddresses.RefreshData( msJobMaster.GetDocCollection() );
						
			this.msDisplayTelephoneNumbers.RefreshData( msJobMaster.GetDocCollection() );
						
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
			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate {
							this.toolStripUrlCount.Text = string.Format( "URLs in Queue: {0}", this.msJobMaster.UrlQueueCount().ToString() );
							this.toolStripThreads.Text = string.Format( "Threads: {0}", this.msJobMaster.RunningThreadsCount().ToString() );
						}
					)
				);
			} else {
				this.toolStripUrlCount.Text = string.Format( "URLs in Queue: {0}", this.msJobMaster.UrlQueueCount().ToString() );
				this.toolStripThreads.Text = string.Format( "Threads: {0}", this.msJobMaster.RunningThreadsCount().ToString() );
			}
		}

		/**************************************************************************/
		
		void CallbackDataBindingComplete ( object sender, DataGridViewBindingCompleteEventArgs e )
		{
			DataGridView dgvGrid = ( DataGridView )sender;
			dgvGrid.AutoResizeColumns();
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
