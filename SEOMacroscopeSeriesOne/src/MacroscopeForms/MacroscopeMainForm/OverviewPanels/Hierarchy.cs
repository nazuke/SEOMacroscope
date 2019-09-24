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

    /** HIERARCHY PANEL CALLBACKS *********************************************/

    private void CallbackHierarchyNodeMouseClick ( object sender, TreeNodeMouseClickEventArgs e )
    {

      string Url = null;

      try
      {
        if( e.Node.Tag != null )
        {
          Url = e.Node.Tag.ToString();
        }
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "CallbackHierarchyNodeMouseClick: {0}", ex.Message ) );
      }

      if( Url != null )
      {
        this.macroscopeDocumentDetailsInstance.UpdateDisplay( this.JobMaster, Url );
      }
      else
      {
        this.macroscopeDocumentDetailsInstance.ClearData();
      }

    }

    /**************************************************************************/

  }

}
