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
using System.Diagnostics;
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

		public void CopyListViewTextToClipboard ( ListView lvListView )
		{

			string sTextToCopy = "";

			foreach( ListViewItem lvItem in lvListView.SelectedItems ) {
				sTextToCopy += lvItem.Text;
				for( int i = 1; i < lvItem.SubItems.Count; i++ ) {
					sTextToCopy += "\t" + lvItem.SubItems[ i ].Text;
				}
				sTextToCopy += "\n";
			}

			try {
				this.CopyTextToClipboard( sTextToCopy );
			} catch( Exception ex ) {
				MessageBox.Show( ex.Message );
			}

		}

		/**************************************************************************/

		public void CopyTextToClipboard ( string sText )
		{
			Clipboard.SetText( sText );
		}

		/**************************************************************************/
		
		[Conditional( "DEVMODE" )]
		public void DebugMsg ( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		/**************************************************************************/
	
	}
	
}
