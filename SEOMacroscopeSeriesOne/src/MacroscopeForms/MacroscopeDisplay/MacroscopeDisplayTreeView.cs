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

  abstract public class MacroscopeDisplayTreeView : Macroscope
  {

    /**************************************************************************/

    public MacroscopeMainForm MainForm;

    public TreeView tvTreeView;

    protected bool TreeViewConfigured = false;
        
    /**************************************************************************/

    protected MacroscopeDisplayTreeView ( MacroscopeMainForm MainForm, TreeView tvTreeView )
    {
      this.MainForm = MainForm;
      this.tvTreeView = tvTreeView;
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
              Cursor.Current = Cursors.WaitCursor;
              this.RenderTreeView( DocCollection );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderTreeView( DocCollection );
        Cursor.Current = Cursors.Default;
      }
    }

    /** Render Entire DocCollection *******************************************/

    public void RenderTreeView ( MacroscopeDocumentCollection DocCollection )
    {
      
      if( DocCollection.CountDocuments() == 0 )
      {
        return;
      }

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = ( decimal )DocCollection.CountDocuments();
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
      
        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );
              
      }

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        Application.DoEvents();

        if( msDoc == null )
        {
          continue;
        }
              
        string Url = msDoc.GetUrl();

        this.RenderTreeView( msDoc, Url );

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        { 

          Count++;
          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );
        
        }
                
      }
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }
      
      ProgressForm.Dispose();

    }

    /** Render One ************************************************************/

    abstract protected void RenderTreeView ( MacroscopeDocument msDoc, string Url );

    /**************************************************************************/

  }

}
