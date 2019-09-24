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
using System.Collections.Generic;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** SEARCH COLLECTION PANEL CALLBACKS *************************************/

    public void ReconfigureSearchCollectionControls ()
    {

      if( MacroscopePreferencesManager.GetEnableTextIndexing() )
      {
        this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = true;
        this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = true;
      }
      else
      {
        this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionButtonClear.Enabled = false;
        this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionTextBoxSearch.Enabled = false;
      }

    }

    /**************************************************************************/

    private void CallbackSearchCollectionButtonClear ( object sender, EventArgs e )
    {
      this.msDisplaySearchCollection.ClearData();
      this.macroscopeOverviewTabPanelInstance.toolStripSearchCollectionDocumentsNumber.Text = string.Format( "Documents: {0}", 0 );
    }

    /**************************************************************************/
        
    private void CallbackSearchCollectionTextBoxSearchKeyUp ( object sender, KeyEventArgs e )
    {

      ToolStripTextBox SearchTextBox = ( ToolStripTextBox )sender;

      DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp: {0}", "CALLED" ) );

      switch( e.KeyCode )
      {

        case Keys.Return:

          DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp: {0}", "RETURN" ) );

          MacroscopeSearchIndex SearchIndex = this.JobMaster.GetDocCollection().GetSearchIndex();

          string SearchText = MacroscopeStringTools.CleanHtmlText( Text: SearchTextBox.Text );

          if( SearchText.Length > 0 )
          {

            List<MacroscopeDocument> DocList = null;

            SearchTextBox.Text = SearchText;

            DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp sText: {0}", SearchText ) );

            DocList = SearchIndex.ExecuteSearchForDocuments(
              MacroscopeSearchIndex.SearchMode.AND,
              SearchText.Split( ' ' )
            );

            this.msDisplaySearchCollection.ClearData();

            DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp DocList: {0}", DocList.Count ) );

            this.msDisplaySearchCollection.RefreshData( DocList );

          }

          break;

        case Keys.Escape:

          DebugMsg( string.Format( "CallbackSearchCollectionTextBoxSearchKeyUp: {0}", "ESCAPE" ) );
          SearchTextBox.Text = "";

          break;

        default:
          break;

      }

    }

    /**************************************************************************/

  }

}
