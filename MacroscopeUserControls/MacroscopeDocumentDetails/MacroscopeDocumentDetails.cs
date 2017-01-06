using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

	public partial class MacroscopeDocumentDetails : MacroscopeUserControl
	{

		/**************************************************************************/
		
		public MacroscopeDocumentDetails ()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//

		}

		/**************************************************************************/
		
		void MacroscopeDocumentDetailsLoad ( object sender, EventArgs e )
		{
			debug_msg( string.Format( "MacroscopeDocumentDetailsLoad: {0}", "initialize" ) );
		}

		/**************************************************************************/
				
	}

}
