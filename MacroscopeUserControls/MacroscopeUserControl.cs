using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

	public partial class MacroscopeUserControl : UserControl
	{

		/**************************************************************************/
						
		public MacroscopeUserControl ()
		{
		}

		/**************************************************************************/
		
		public void debug_msg ( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		public void debug_msg ( String sMsg, int iOffset )
		{
			String sMsgPadded = new String ( ' ', iOffset * 2 ) + sMsg;
			System.Diagnostics.Debug.WriteLine( sMsgPadded );
		}

		/**************************************************************************/
	
	}
	
}
