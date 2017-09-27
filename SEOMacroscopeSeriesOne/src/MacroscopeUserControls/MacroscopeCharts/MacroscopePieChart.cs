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
using System.Windows.Forms.DataVisualization.Charting;


namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeBarChart.
  /// </summary>

  public partial class MacroscopePieChart : UserControl
  {

    /**************************************************************************/

    Chart PieChart;

    /**************************************************************************/

    public MacroscopePieChart ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.PieChart = this.pieChartPanel;

      this.PieChart.Dock = DockStyle.Fill;
      
      this.Clear();

    }

    /**************************************************************************/

    public void SetTitle ( string Title )
    {
      this.PieChart.Text = Title;
    }

    /**************************************************************************/

    public void Clear ()
    {
      this.PieChart.Series.Clear();
    }

    /**************************************************************************/

    public void Update ( SortedDictionary<string,double> DataPoints )
    {

      const string SeriesName = "Readbility";
      Series DataSeries = new Series ();

      DataSeries.Name = SeriesName;
      DataSeries.ChartType = SeriesChartType.Pie;

      this.PieChart.Series.Clear();
      this.PieChart.Series.Add( item: DataSeries );

      foreach( string DataPointKey in DataPoints.Keys )
      {

        double Value = DataPoints[ DataPointKey ];
        
        DataPoint DataPointItem = DataSeries.Points.Add( Value );

        DataPointItem.AxisLabel = string.Format( "{0:0.00}", Value );

        DataPointItem.LegendText = string.Format( "{0}: {1:0.00}", DataPointKey, Value );

      }

      this.PieChart.Invalidate();
      
    }

    /**************************************************************************/

  }

}
