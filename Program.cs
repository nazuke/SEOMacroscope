using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SEOMacroscope
{

	internal sealed class Program
	{

		#if BUILD_TEXT_VERSION
		
		public static void Main( string[] args )
		{
			debug_msg( "SEO Macroscope" );
			MacroscopeJob msJob = new MacroscopeJob ();
			msJob.start_url = Environment.GetEnvironmentVariable( "seomacroscope_scan_url" ).ToString();
			msJob.depth = 2;
			msJob.probe_hreflangs = false;
			msJob.run();
		}

		#else

		[STAThread]
		private static void Main( string[] args )
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new MainForm () );
		}

		#endif
		
		static void debug_msg( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		static void debug_msg( String sMsg, int iOffset )
		{
			String sMsgPadded = new String (' ', iOffset * 2) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}

	}

}
