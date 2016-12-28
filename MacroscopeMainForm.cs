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
				
		Thread tScanningThread;
		
		/**************************************************************************/

		public MacroscopeMainForm ()
		{
			InitializeComponent();// The InitializeComponent() call is required for Windows Forms designer support.

			msDisplayStructure = new MacroscopeDisplayStructure ( this );
			msDisplayHrefLang = new MacroscopeDisplayHrefLang ( this );
			
			this.textBoxURL.Text = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" ).ToString();

			//tScanningThread = new Thread ( new ThreadStart ( ScanningThread ) );
			
			this.ScanningEnableControls();

		}
		
		/**************************************************************************/

		public DataGridView GetDisplayStructure()
		{
			return( this.dataGridStructure );
		}

		/**************************************************************************/

		public DataGridView GetDisplayHrefLang()
		{
			return( this.dataGridHrefLang );
		}

		/**************************************************************************/
				
		public string GetURL()
		{
			return( this.textBoxURL.Text );
		}

		/**************************************************************************/

		public void SetURL( string sURL )
		{
			this.textBoxURL.Text = sURL;
		}

		/**************************************************************************/
				
		void CallbackFileExit( object sender, EventArgs e )
		{
			
			if( this.tScanningThread.IsAlive ) {
				this.tScanningThread.Abort();
			}

			Program.Exit();

		}

		/**************************************************************************/
				
		void CallbackScanStart( object sender, EventArgs e )
		{
			this.ScanningDisableControls();
			this.tScanningThread = new Thread ( new ThreadStart ( ScanningThread ) );
			this.tScanningThread.Start();
		}

		/**************************************************************************/

		void CallbackScanPause( object sender, EventArgs e )
		{
			if( this.tScanningThread.IsAlive ) {
				;
				this.ScanningEnableControls();
			}
		}

		/**************************************************************************/
				
		void CallbackScanReset( object sender, EventArgs e )
		{
			if( this.tScanningThread.IsAlive ) {
				this.tScanningThread.Abort();
			}
			this.ScanningEnableControls();
		}
		
		/**************************************************************************/
		
		public void CallbackScanComplete()
		{
			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate {
							this.ScanningEnableControls();
						}
					)
				);
			}
		}
		
		/**************************************************************************/

		void CallbackDataError( object sender, DataGridViewDataErrorEventArgs e )
		{
			debug_msg( "EVENT: DataError" );
		}
		
		/**************************************************************************/
		
		void CallbackRowsAdded( object sender, DataGridViewRowsAddedEventArgs e )
		{
			this.Update();
		}
		
		/**************************************************************************/

		void ScanningDisableControls()
		{
			this.textBoxURL.Enabled = false;
			this.buttonStart.Enabled = false;
			this.buttonPause.Enabled = true;
			this.buttonReset.Enabled = false;
		}

		/**************************************************************************/

		void ScanningEnableControls()
		{
			this.textBoxURL.Enabled = true;
			this.buttonStart.Enabled = true;
			this.buttonPause.Enabled = false;
			this.buttonReset.Enabled = true;
		}
		
		/**************************************************************************/

		void ScanningThread()
		{
			debug_msg( "Scanning Thread: Started." );
			MacroscopeJobThread msJob = new MacroscopeJobThread ( this );
			msJob.Run();
			debug_msg( "Scanning Thread: Done." );
		}

		/**************************************************************************/

		public void UpdateDisplayStructure( MacroscopeJob msJob )
		{

			this.msDisplayStructure.RefreshData( msJob.get_doc_collection() );

			this.msDisplayHrefLang.RefreshData( msJob.get_doc_collection(), msJob.get_locales() );

			if( this.InvokeRequired ) {
				this.Invoke(
					new MethodInvoker (
						delegate {
							this.Refresh();
							this.Update();
						}
					)
				);
			}

		}

		/**************************************************************************/
		
		void CallbackDataBindingComplete( object sender, DataGridViewBindingCompleteEventArgs e )
		{
			DataGridView dgvGrid = ( DataGridView )sender;
			dgvGrid.AutoResizeColumns();
		}
				
		/**************************************************************************/
	
		static void debug_msg( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		static void debug_msg( String sMsg, int iOffset )
		{
			String sMsgPadded = new String ( ' ', iOffset * 2 ) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}


		/**************************************************************************/
			
	}
	
}
