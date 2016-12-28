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
			msJob.run();

			this.msMainForm.CallbackScanComplete();

		}

		/**************************************************************************/
		
		public void Update()
		{
			this.msMainForm.UpdateDisplayStructure( this.msJob );
		}

		/**************************************************************************/
				
	}
	
}
