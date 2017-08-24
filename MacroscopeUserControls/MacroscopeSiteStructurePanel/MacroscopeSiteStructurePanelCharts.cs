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
using System.Collections.Generic;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeSiteStructurePanelCharts.
  /// </summary>

  public partial class MacroscopeSiteStructurePanelCharts : MacroscopeUserControl
  {

    /**************************************************************************/

    public MacroscopeSiteStructurePanelCharts ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.
			
      this.tabControlCharts.Dock = DockStyle.Fill;
      
      this.barChartSiteSummary.Dock = DockStyle.Fill;
      this.barChartLanguagesDetected.Dock = DockStyle.Fill;
      this.barChartReadability.Dock = DockStyle.Fill;
      
      this.ConfigureSiteSummary();
      this.ConfigureLanguagesDetected();
      this.ConfigureReadability();
      
      
    }

    /**************************************************************************/

    private void ConfigureSiteSummary ()
    {
      this.barChartSiteSummary.SetTitle( "Site Summary" );
    }

    /** -------------------------------------------------------------------- **/
    
    private void ConfigureLanguagesDetected ()
    {
      this.barChartLanguagesDetected.SetTitle( "Languages Detected" );
    }

    /** -------------------------------------------------------------------- **/
    
    private void ConfigureReadability ()
    {
      this.barChartReadability.SetTitle( "Readability" );
    }

    /**************************************************************************/

    public void SelectTabPage ( string TabName )
    {

      TabControl TabControlPanel = this.tabControlCharts;

      try
      {

        int ChosenTabIndex = TabControlPanel.TabPages.IndexOfKey( key: TabName );

        TabControlPanel.SelectTab( index: ChosenTabIndex );

      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "SelectTabPage: {0}", ex.Message ) );
      }
      
    }

    /**************************************************************************/

    private void ClearSiteSummary ()
    {
      this.barChartSiteSummary.Clear();
    }
          
    /** -------------------------------------------------------------------- **/

    public void UpdateSiteSummary ( SortedDictionary<string,double> DataPoints )
    {
      this.barChartSiteSummary.Update( DataPoints: DataPoints );
    }

    /**************************************************************************/

    private void ClearLanguagesDetected ()
    {
      this.barChartLanguagesDetected.Clear();
    }
          
    /** -------------------------------------------------------------------- **/

    public void UpdateLanguagesDetected ( SortedDictionary<string,double> DataPoints )
    {
      this.barChartLanguagesDetected.Update( DataPoints: DataPoints );
    }

    /**************************************************************************/

    private void ClearReadability ()
    {
      this.barChartReadability.Clear();
    }
          
    /** -------------------------------------------------------------------- **/

    public void UpdateReadability ( SortedDictionary<string,double> DataPoints )
    {
      this.barChartReadability.Update( DataPoints: DataPoints );
    }

    /**************************************************************************/

  }

}
