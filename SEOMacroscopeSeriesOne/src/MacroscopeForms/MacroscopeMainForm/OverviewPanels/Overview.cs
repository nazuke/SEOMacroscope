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

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /**************************************************************************/

    public void ReconfigureStructureOverviewControls ()
    {

      if( MacroscopePreferencesManager.GetEnableTextIndexing() )
      {
        this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = true;
      }
      else
      {
        this.macroscopeOverviewTabPanelInstance.toolStripStructureSearchTextBoxSearch.Enabled = false;
      }

    }

    /**************************************************************************/

    public void SetStructureOverviewControlsToScanning ()
    {

      this.SelectTabPage( TabName: MacroscopeConstants.tabPageStructureOverview );

      this.EnableTabPage( TabName: MacroscopeConstants.tabPageStructureOverview );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageStructureLinkCounts );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageHierarchy );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageRobots );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageSitemaps );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageSitemapErrors );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageCanonicalAnalysis );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageHrefLangAnalysis );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageErrors );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageHostnames );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageRedirectsAudit );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageLinks );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageHyperlinks );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageUriAnalysis );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPagePageTitles );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPagePageDescriptions );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPagePageKeywords );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPagePageHeadings );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPagePageText );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageStylesheets );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageJavascripts );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageImages );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageAudios );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageVideos );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageEmailAddresses );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageTelephoneNumbers );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageCustomFilters );

      this.DisableTabPage( TabName: MacroscopeConstants.tabPageDataExtractors );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageCssSelectors );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageRegexes );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageXpaths );

      this.DisableTabPage( TabName: MacroscopeConstants.tabPageRemarks );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageUriQueue );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageHistory );
      this.DisableTabPage( TabName: MacroscopeConstants.tabPageSearch );

    }

    /** -------------------------------------------------------------------- **/

    public void SetStructureOverviewControlsToEnabled ()
    {

      this.SelectTabPage( TabName: MacroscopeConstants.tabPageStructureOverview );

      this.EnableTabPage( TabName: MacroscopeConstants.tabPageStructureOverview );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageStructureLinkCounts );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageHierarchy );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageRobots );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageSitemaps );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageSitemapErrors );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageCanonicalAnalysis );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageHrefLangAnalysis );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageErrors );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageHostnames );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageRedirectsAudit );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageLinks );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageHyperlinks );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageUriAnalysis );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPagePageTitles );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPagePageDescriptions );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPagePageKeywords );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPagePageHeadings );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPagePageText );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageStylesheets );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageJavascripts );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageImages );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageAudios );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageVideos );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageEmailAddresses );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageTelephoneNumbers );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageCustomFilters );

      this.EnableTabPage( TabName: MacroscopeConstants.tabPageDataExtractors );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageCssSelectors );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageRegexes );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageXpaths );

      this.EnableTabPage( TabName: MacroscopeConstants.tabPageRemarks );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageUriQueue );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageHistory );
      this.EnableTabPage( TabName: MacroscopeConstants.tabPageSearch );

    }

    /**************************************************************************/

  }

}
