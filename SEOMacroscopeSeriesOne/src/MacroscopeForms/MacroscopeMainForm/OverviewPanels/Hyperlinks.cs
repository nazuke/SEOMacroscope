/*

  This file is part of SEOMacroscope.

  Copyright 2018 Jason Holland.

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
using System.Windows.Forms;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** HYPERLINKS PANEL TOOL STRIP CALLBACKS **************************************/

    private void CallbackButtonHyperlinksShowAll ( object sender, EventArgs e )
    {

      this.msDisplayHyperlinks.ClearData();

      this.msDisplayHyperlinks.RefreshData(
        DocCollection: this.JobMaster.GetDocCollection()
      );

    }

    /**************************************************************************/
        
    private void CallbackSearchTextBoxHyperlinksSearchSourceUrlKeyUp ( object sender, KeyEventArgs e )
    {

      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;

      switch( e.KeyCode )
      {

        case Keys.Return:

          string UrlFragment = SearchTextBox.Text;

          DebugMsg( string.Format( "CallbackSearchTextBoxHyperlinksSearchSourceUrlKeyUp: {0}", UrlFragment ) );

          if( UrlFragment.Length > 0 )
          {
            SearchTextBox.Text = UrlFragment;
            this.msDisplayHyperlinks.ClearData();
            this.msDisplayHyperlinks.RefreshDataSearchSourceUrls(
              DocCollection: this.JobMaster.GetDocCollection(),
              UrlFragment: UrlFragment
            );
          }

          break;

      }

    }

    /**************************************************************************/
        
    private void CallbackSearchTextBoxHyperlinksSearchTargetUrlKeyUp ( object sender, KeyEventArgs e )
    {

      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;

      switch( e.KeyCode )
      {

        case Keys.Return:

          string UrlFragment = SearchTextBox.Text;

          DebugMsg( string.Format( "CallbackSearchTextBoxHyperlinksSearchTargetUrlKeyUp: {0}", UrlFragment ) );

          if( UrlFragment.Length > 0 )
          {
            SearchTextBox.Text = UrlFragment;
            this.msDisplayHyperlinks.ClearData();
            this.msDisplayHyperlinks.RefreshDataSearchTargetUrls(
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
