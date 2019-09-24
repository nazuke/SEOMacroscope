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

    /** Edit Menu *************************************************************/

    private void CallbackEditPreferencesClick ( object sender, EventArgs e )
    {

      MacroscopePrefsForm PreferencesForm = new MacroscopePrefsForm ();
      DialogResult PreferencesResult;

      int CustomFiltersMaxItems = MacroscopePreferencesManager.GetCustomFiltersMaxItems();

      int DataExtractorsMaxItemsCssSelectors = MacroscopePreferencesManager.GetDataExtractorsMaxItemsCssSelectors();
      int DataExtractorsMaxItemsRegexes = MacroscopePreferencesManager.GetDataExtractorsMaxItemsRegexes();
      int DataExtractorsMaxItemsXpaths = MacroscopePreferencesManager.GetDataExtractorsMaxItemsXpaths();

      PreferencesResult = PreferencesForm.ShowDialog();

      if( PreferencesResult == DialogResult.OK )
      {

        PreferencesForm.SavePrefsFormControlFields();

        /** Custom Filters ------------------------------------------------- **/

        this.InitializeCustomFilters();

        /** Data Extractors ------------------------------------------------ **/

        {
          
          bool ReconfigureInitializeCssSelectors = false;
          bool ReconfigureDataExtractorMaxItemsRegexes = false;
          bool ReconfigureDataExtractorMaxItemsXpaths = false;

          if( DataExtractorsMaxItemsCssSelectors != MacroscopePreferencesManager.GetDataExtractorsMaxItemsCssSelectors() )
          {
            ReconfigureInitializeCssSelectors = true;
          }

          if( DataExtractorsMaxItemsRegexes != MacroscopePreferencesManager.GetDataExtractorsMaxItemsRegexes() )
          {
            ReconfigureDataExtractorMaxItemsRegexes = true;
          }
        
          if( DataExtractorsMaxItemsXpaths != MacroscopePreferencesManager.GetDataExtractorsMaxItemsXpaths() )
          {
            ReconfigureDataExtractorMaxItemsXpaths = true;
          }

          this.InitializeDataExtractors(
            InitializeCssSelectors: ReconfigureInitializeCssSelectors,
            InitializeRegexes: ReconfigureDataExtractorMaxItemsRegexes,
            InitializeXpaths: ReconfigureDataExtractorMaxItemsXpaths
          );
        
        }

        /** Reports Menu --------------------------------------------------- **/
        
        this.ReconfigureReportsMenu();
              
        /** ---------------------------------------------------------------- **/

        /** Structure Overview Controls ------------------------------------ **/
        
        this.ReconfigureStructureOverviewControls();
              
        /** ---------------------------------------------------------------- **/

        /** Search Controls ------------------------------------------------ **/
        
        this.ReconfigureSearchCollectionControls();
              
        /** ---------------------------------------------------------------- **/

      }

      if( PreferencesForm != null )
      {
        PreferencesForm.Dispose();
      }

    }
        

    /**************************************************************************/

  }

}
