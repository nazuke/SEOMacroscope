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
		MacroscopeDisplayCanonical msDisplayCanonical;
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
			msDisplayCanonical = new MacroscopeDisplayCanonical ( this );
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
				
		public ListView GetDisplayCanonicalAnalysis ()
		{
			return( this.listViewCanonicalAnalysis );
		}

		/**************************************************************************/

		public ListView GetDisplayHrefLang ()
		{
			return( this.listViewHrefLang );
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
			
			if( this.tScanningThread != null ) {
				if( this.tScanningThread.IsAlive ) {
					this.tScanningThread.Abort();
				}
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

			this.ScanningControlsStopping( true );
			
			this.msJobMaster.WorkersStop();

			while( this.msJobMaster.RunningThreadsCount() > 0 ) {
				debug_msg( "CallbackScanStop: WAITING" );
				Thread.Sleep( 100 );
			}
			
			this.ScanningControlsStopped( true );
	
		}

		/**************************************************************************/

		void CallbackScanReset ( object sender, EventArgs e )
		{

			if( this.msJobMaster.WorkersStopped() ) {

				this.ScanningControlsReset( true );
				
				this.msJobMaster = new MacroscopeJobMaster ( this );

				this.ClearDisplay();
				
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

		void CallbackCanonicalAnalysisClick ( object sender, EventArgs e )
		{
			debug_msg( "EVENT: CallbackCanonicalAnalysisClick" );

			this.msDisplayCanonical.RefreshData( this.msJobMaster.DocCollectionGet() );

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

		void CallbackHrefLangAnalysisClick ( object sender, EventArgs e )
		{
			debug_msg( "EVENT: CallbackHrefLangAnalysisClick" );

			this.msDisplayHrefLang.RefreshData( this.msJobMaster.DocCollectionGet(), msJobMaster.LocalesGet() );

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

		void ScanningControlsEnable ( Boolean bState )
		{
			this.textBoxURL.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;
		}

		void ScanningControlsStart ( Boolean bState )
		{
			this.textBoxURL.Enabled = false;
			this.ButtonStart.Enabled = false;
			this.ButtonStop.Enabled = true;
			this.ButtonReset.Enabled = false;
		}

		void ScanningControlsStopping ( Boolean bState )
		{
			this.textBoxURL.Enabled = false;
			this.ButtonStart.Enabled = false;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;
		}

		void ScanningControlsStopped ( Boolean bState )
		{
			this.textBoxURL.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = true;
		}

		void ScanningControlsPause ( Boolean bState )
		{
			this.textBoxURL.Enabled = false;
			this.ButtonStart.Enabled = false;
			this.ButtonStop.Enabled = true;
			this.ButtonReset.Enabled = false;
		}

		void ScanningControlsResume ( Boolean bState )
		{
			this.textBoxURL.Enabled = false;
			this.ButtonStart.Enabled = false;
			this.ButtonStop.Enabled = true;
			this.ButtonReset.Enabled = false;
		}

		void ScanningControlsReset ( Boolean bState )
		{
			this.textBoxURL.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = false;
		}
		
		void ScanningControlsComplete ( Boolean bState )
		{
			this.textBoxURL.Enabled = true;
			this.ButtonStart.Enabled = true;
			this.ButtonStop.Enabled = false;
			this.ButtonReset.Enabled = true;
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

		public void ClearDisplay ()
		{

			this.msDisplayStructure.ClearData();
			this.msDisplayCanonical.ClearData();
			this.msDisplayHrefLang.ClearData();
			this.msDisplayEmailAddresses.ClearData();
			this.msDisplayTelephoneNumbers.ClearData();
			this.msDisplayHistory.ClearData();

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
		
		public void UpdateDisplay ()
		{

			this.msDisplayStructure.RefreshData( this.msJobMaster.DocCollectionGet() );

			this.msDisplayHrefLang.RefreshData( this.msJobMaster.DocCollectionGet(), msJobMaster.LocalesGet() );

			this.msDisplayEmailAddresses.RefreshData( this.msJobMaster.DocCollectionGet() );
						
			this.msDisplayTelephoneNumbers.RefreshData( this.msJobMaster.DocCollectionGet() );
						
			this.msDisplayHistory.RefreshData( this.msJobMaster.HistoryGet() );
						
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

			this.msDisplayCanonical.RefreshDataSingle( msJobMaster.DocCollectionGet().Get( sURL ), sURL );
						
			//this.msDisplayHrefLang.RefreshData( this.msJobMaster.DocCollectionGet(), msJobMaster.LocalesGet() );

			this.msDisplayEmailAddresses.RefreshDataSingle( msJobMaster.DocCollectionGet().Get( sURL ), sURL );
						
			this.msDisplayTelephoneNumbers.RefreshDataSingle( msJobMaster.DocCollectionGet().Get( sURL ), sURL );

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

		public void UpdateDisplayHrefLang ()
		{

			this.msDisplayHrefLang.RefreshData( this.msJobMaster.DocCollectionGet(), this.msJobMaster.LocalesGet() );

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

					string sURL = lvItem.SubItems[0].Text.ToString();
					
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

		
		void ToolStripDropDownButton1Click ( object sender, EventArgs e )
		{
	
		}
		void JPEGToolStripMenuItemClick ( object sender, EventArgs e )
		{
	
		}
		void TabPageCanonicalsAuditClick ( object sender, EventArgs e )
		{
	
		}

		

		


		/**************************************************************************/
			
	}
	
}
