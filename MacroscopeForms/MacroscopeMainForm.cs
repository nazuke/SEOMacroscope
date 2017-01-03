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

			this.ScanningEnableControls();

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
			MacroscopePreferences.SavePreferences();
			this.ScanningDisableControls();
			this.tScanningThread = new Thread ( new ThreadStart ( ScanningThread ) );
			this.tScanningThread.Start();
		}

		/**************************************************************************/

		void CallbackScanPause ( object sender, EventArgs e )
		{
			if( this.tScanningThread.IsAlive ) {
				;
				this.ScanningEnableControls();
			}
		}

		/**************************************************************************/
				
		void CallbackScanReset ( object sender, EventArgs e )
		{
			if( this.tScanningThread.IsAlive ) {
				this.tScanningThread.Abort();
			}
			this.ScanningEnableControls();
			
			this.dataGridStructure.DataSource = null;
			this.dataGridHrefLang.DataSource = null;
			this.dataGridEmailAddresses.DataSource = null;
			this.dataGridTelephoneNumbers.DataSource = null;
			
		}
		
		/**************************************************************************/
		
		public void CallbackScanComplete ()
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

		void ScanningDisableControls ()
		{
			this.textBoxURL.Enabled = false;
			this.buttonStart.Enabled = false;
			this.buttonPause.Enabled = true;
			this.buttonReset.Enabled = false;
		}

		/**************************************************************************/

		void ScanningEnableControls ()
		{
			this.textBoxURL.Enabled = true;
			this.buttonStart.Enabled = true;
			this.buttonPause.Enabled = false;
			this.buttonReset.Enabled = true;
		}
		
		/**************************************************************************/

		void ScanningThread ()
		{
			debug_msg( "Scanning Thread: Started." );
			MacroscopeJobMaster msJobMaster = new MacroscopeJobMaster ( this );
			msJobMaster.StartUrl = this.GetURL();
			msJobMaster.Execute();
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
		void PropertyGrid1Click ( object sender, EventArgs e )
		{
	
		}


		/**************************************************************************/
			
	}
	
}
