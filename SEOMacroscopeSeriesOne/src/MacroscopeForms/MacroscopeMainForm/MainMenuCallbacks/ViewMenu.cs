/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Initialize View Menu **************************************************/

    private void InitializeViewMenu ()
    {

      TabControl OverviewTabControl = this.macroscopeOverviewTabPanelInstance.tabControlMain;
      List<string> TabPagesSorted = new List<string>( OverviewTabControl.TabPages.Count );

      foreach( TabPage OverviewTabPage in OverviewTabControl.TabPages )
      {
        TabPagesSorted.Add( OverviewTabPage.Name );
      }

      TabPagesSorted.Sort();

      foreach( string TabName in TabPagesSorted )
      {

        TabPage OverviewTabPage = OverviewTabControl.TabPages[ TabName ];
        ToolStripMenuItem NewToolStripMenuItem;
        string TabLabel = OverviewTabPage.Text;

        NewToolStripMenuItem = new ToolStripMenuItem( text: TabLabel );
        NewToolStripMenuItem.Tag = TabName;
        NewToolStripMenuItem.Click += CallbackViewMenuItemClick;

        this.viewToolStripMenuItem.DropDownItems.Add( NewToolStripMenuItem );

      }

    }

    /** ----------------------------------------------------------------------- **/

    private void CallbackViewMenuItemClick ( object sender, EventArgs e )
    {

      ToolStripMenuItem ClickedToolStripMenuItem = (ToolStripMenuItem) sender;

      string TabName = ClickedToolStripMenuItem.Tag.ToString();

      this.SelectTabPage( TabName: TabName );

      this.UpdateTabPage( TabName: TabName );

    }

    /**************************************************************************/

  }

}
