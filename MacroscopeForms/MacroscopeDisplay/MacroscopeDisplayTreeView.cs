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
using System.Windows.Forms;

namespace SEOMacroscope
{

  abstract public class MacroscopeDisplayTreeView : Macroscope
  {

    /**************************************************************************/

    public MacroscopeMainForm MainForm;

    public TreeView tvTreeView;

    protected Boolean TreeViewConfigured = false;
        
    /**************************************************************************/

    protected MacroscopeDisplayTreeView ( MacroscopeMainForm MainFormNew, TreeView tvTreeViewNew )
    {
      MainForm = MainFormNew;
      tvTreeView = tvTreeViewNew;
    }

    /**************************************************************************/

    abstract protected void ConfigureTreeView ();
        
    /**************************************************************************/
            
    public void RefreshData ( MacroscopeDocumentCollection DocCollection )
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.RenderTreeView( DocCollection );
            }
          )
        );
      }
      else
      {
        this.RenderTreeView( DocCollection );
      }
    }

    /** Render Entire DocCollection *******************************************/

    public void RenderTreeView ( MacroscopeDocumentCollection DocCollection )
    {
      DebugMsg( string.Format( "RenderListView: {0}", "BASE" ) );
      foreach( string sUrl in DocCollection.DocumentKeys() )
      {
        MacroscopeDocument msDoc = DocCollection.GetDocument( sUrl );
        this.RenderTreeView( msDoc, sUrl );
      }
    }

    /** Render One ************************************************************/

    abstract protected void RenderTreeView ( MacroscopeDocument msDoc, string sUrl );

    /**************************************************************************/

  }

}
