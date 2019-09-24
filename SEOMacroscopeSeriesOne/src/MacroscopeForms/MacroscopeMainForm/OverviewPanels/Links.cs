/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

  The GitHub repository may be found at:

    https://github.com/nazuke/SEOMacroscope

  SEOMacroscope is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  SEOMacroscope is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** LINKS PANEL TOOL STRIP CALLBACKS **************************************/

    private void CallbackButtonLinksShowAll ( object sender, EventArgs e )
    {
      
      this.msDisplayLinks.ClearData();
      
      this.msDisplayLinks.RefreshData(
        this.JobMaster.GetDocCollection()
      );
      
    }

    /**************************************************************************/
        
    private void CallbackSearchTextBoxLinksSearchSourceUrlKeyUp ( object sender, KeyEventArgs e )
    {
      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;
      switch( e.KeyCode )
      {
        case Keys.Return:
          string UrlFragment = SearchTextBox.Text;
          DebugMsg( string.Format( "CallbackSearchTextBoxLinksSearchSourceUrlKeyUp: {0}", UrlFragment ) );
          if( UrlFragment.Length > 0 )
          {
            SearchTextBox.Text = UrlFragment;
            this.msDisplayLinks.ClearData();
            this.msDisplayLinks.RefreshDataSearchSourceUrls(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlFragment: UrlFragment
            );
          }
          break;
      }
    }

    /**************************************************************************/
    
    private void CallbackSearchTextBoxLinksSearchTargetUrlKeyUp ( object sender, KeyEventArgs e )
    {
      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;
      switch( e.KeyCode )
      {
        case Keys.Return:
          string UrlFragment = SearchTextBox.Text;
          DebugMsg( string.Format( "CallbackSearchTextBoxLinksSearchTargetUrlKeyUp: {0}", UrlFragment ) );
          if( UrlFragment.Length > 0 )
          {
            SearchTextBox.Text = UrlFragment;
            this.msDisplayLinks.ClearData();
            this.msDisplayLinks.RefreshDataSearchTargetUrls(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlFragment: UrlFragment
            );
          }
          break;
      }
    }

    /**************************************************************************/

  }

}
