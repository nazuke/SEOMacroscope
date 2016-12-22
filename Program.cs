using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SEOMacroscope
{

	internal sealed class Program
	{

		static Boolean bExiting; // Set to true to indicate in Exiting mode
		
		public static void Exit () {
			bExiting = true;
			// do cleanup
			debug_msg( "Exiting" );
			Application.Exit();
		}

		#if BUILD_TEXT_VERSION
		
		public static void Main( string[] args )
		{
			debug_msg( "SEO Macroscope" );
			MacroscopeJob msJob = new MacroscopeJob ();

			string sPathExcelHrefLangs = Environment.GetEnvironmentVariable( "TEMP" ).ToString();

			msJob.start_url = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" ).ToString();

			msJob.depth = 5;
			msJob.page_limit = 10;
			msJob.probe_hreflangs = false;

			msJob.run();

			msJob.list_results();

			MacroscopeExcelReports msExcelReports = new MacroscopeExcelReports();

			msExcelReports.write_xslx_file_overview(
				msJob,
				string.Join(
					"",
					sPathExcelHrefLangs,
					System.IO.Path.DirectorySeparatorChar,
					"excel_overview"
				)
			);

			msExcelReports.write_xslx_file_hreflang(
				msJob,
				string.Join(
					"",
					sPathExcelHrefLangs,
					System.IO.Path.DirectorySeparatorChar,
					"excel_hreflang"
				)
			);

		}

		#else

		[STAThread]
		private static void Main( string[] args )
		{
			bExiting = false;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new MacroscopeMainForm () );
		}

		#endif

		static void debug_msg( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		static void debug_msg( String sMsg, int iOffset )
		{
			String sMsgPadded = new String ( ' ', iOffset * 2 ) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}

	}

}
