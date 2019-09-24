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
      this.pieChartResponseTimes.Dock = DockStyle.Fill;
      this.pieChartLanguagesSpecified.Dock = DockStyle.Fill;
      this.pieChartReadability.Dock = DockStyle.Fill;
      
      this.ConfigureSiteSummary();
      this.ConfigureResponseTimes();
      this.ConfigureLanguagesSpecified();
      this.ConfigureReadability();
      
    }

    /**************************************************************************/

    private void ConfigureSiteSummary ()
    {
      this.barChartSiteSummary.SetTitle( "Site Summary" );
    }

    /** -------------------------------------------------------------------- **/  
   
    private void ConfigureResponseTimes ()
    {
      this.pieChartResponseTimes.SetTitle( "Response Times" );
    }

    /** -------------------------------------------------------------------- **/
    
    private void ConfigureLanguagesSpecified ()
    {
      this.pieChartLanguagesSpecified.SetTitle( "Languages Specified" );
    }

    /** -------------------------------------------------------------------- **/
    
    private void ConfigureReadability ()
    {
      this.pieChartReadability.SetTitle( "Readability" );
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
    
    public void ClearAll ()
    {
      this.ClearSiteSummary();
      this.ClearResponseTimes();
      this.ClearLanguagesSpecified();
      this.ClearReadability();
    }
          
    /**************************************************************************/

    public void ClearSiteSummary ()
    {
      this.barChartSiteSummary.Clear();
    }
          
    /** -------------------------------------------------------------------- **/

    public void UpdateSiteSummary ( SortedDictionary<string,double> DataPoints )
    {
      this.barChartSiteSummary.Update( DataPoints: DataPoints );
    }
    
    /**************************************************************************/

    public void ClearResponseTimes ()
    {
      this.pieChartResponseTimes.Clear();
    }
          
    /** -------------------------------------------------------------------- **/

    public void UpdateResponseTimes ( SortedDictionary<string,double> DataPoints )
    {
      this.pieChartResponseTimes.Update( DataPoints: DataPoints );
    }

    /**************************************************************************/

    public void ClearLanguagesSpecified ()
    {
      this.pieChartLanguagesSpecified.Clear();
    }
          
    /** -------------------------------------------------------------------- **/

    public void UpdateLanguagesSpecified ( SortedDictionary<string,double> DataPoints )
    {
      this.pieChartLanguagesSpecified.Update( DataPoints: DataPoints );
    }

    /**************************************************************************/

    public void ClearReadability ()
    {
      this.pieChartReadability.Clear();
    }
          
    /** -------------------------------------------------------------------- **/

    public void UpdateReadability ( SortedDictionary<string,double> DataPoints )
    {
      this.pieChartReadability.Update( DataPoints: DataPoints );
    }

    /**************************************************************************/

  }

}
