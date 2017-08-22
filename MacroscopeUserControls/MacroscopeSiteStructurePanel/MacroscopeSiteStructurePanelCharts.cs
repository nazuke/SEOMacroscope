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

  public partial class MacroscopeSiteStructurePanelCharts : UserControl
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

    private void ClearSiteSummary ()
    {
      this.barChartSiteSummary.Clear();
    }
          
    /** -------------------------------------------------------------------- **/
        
    private void UpdateSiteSummary ()
    {

      this.barChartSiteSummary.SetTitle( "Site Summary" );

      SortedDictionary<string,double> DataPoints = new SortedDictionary<string,double> ();

      DataPoints.Add( "Item 1", 3 );
      DataPoints.Add( "Item 2", 10 );
      DataPoints.Add( "Item 3", 5 );

      this.barChartSiteSummary.Update( DataPoints: DataPoints );

    }

    /** -------------------------------------------------------------------- **/




    /**************************************************************************/

  }

}
