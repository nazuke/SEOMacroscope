using System;

namespace SEOMacroscope
{

	public class MacroscopeJobThread : Macroscope
	{

		/**************************************************************************/

		MacroscopeMainForm msMainForm;
		MacroscopeJob msJob;
			
		/**************************************************************************/

		public MacroscopeJobThread ( MacroscopeMainForm msMainFormNew )
		{
			msMainForm = msMainFormNew;
			msJob = new MacroscopeJob ( this );
		}

		/**************************************************************************/
		
		public void Run()
		{
		
			debug_msg( "SEO Macroscope" );

			//string sPathExcelHrefLangs = Environment.GetEnvironmentVariable( "TEMP" ).ToString();

			//msJob.start_url = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" ).ToString();

			msJob.start_url = msMainForm.GetURL();
			
			msJob.depth = 5;
			msJob.page_limit = 1000;
			msJob.probe_hreflangs = false;

			msJob.run();

		}

		/**************************************************************************/
		
		public void Update()
		{
			this.msMainForm.UpdateDisplayStructure( this.msJob );
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
