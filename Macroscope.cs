using System;

namespace SEOMacroscope
{

	public class Macroscope
	{

		/**************************************************************************/

		public Macroscope()
		{
		}

		/**************************************************************************/
		
		public void debug_msg( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		public void debug_msg( String sMsg, int iOffset )
		{
			String sMsgPadded = new String ( ' ', iOffset * 2 ) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}

		/**************************************************************************/

	}

}
