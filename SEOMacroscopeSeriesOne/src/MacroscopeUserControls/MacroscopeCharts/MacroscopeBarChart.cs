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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeBarChart.
  /// </summary>

  public partial class MacroscopeBarChart : UserControl
  {

    /**************************************************************************/

    Chart BarChart;

    /**************************************************************************/

    public MacroscopeBarChart ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.BarChart = this.barChartPanel;

      this.BarChart.Dock = DockStyle.Fill;
      
      this.Clear();

    }

    /**************************************************************************/

    public void SetTitle ( string Title )
    {
      this.BarChart.Text = Title;
    }

    /**************************************************************************/

    public void Clear ()
    {
      this.BarChart.Series.Clear();
    }

    /**************************************************************************/

    public void Update ( SortedDictionary<string,double> DataPoints )
    {

      double Max = 10;
      double Count = 1;
      
      this.BarChart.Series.Clear();
      
      foreach( string DataPointKey in DataPoints.Keys )
      {

        string SeriesName = DataPointKey;
        DataPoint DataPointItem = new DataPoint ();
        ChartArea Area;
  
        this.BarChart.Series.Add( name: SeriesName );

        this.BarChart.Series[ SeriesName ].ChartType = SeriesChartType.Column;

        this.BarChart.Series[ SeriesName ].LegendText = string.Format(
          "{0}: {1:0.00}",
          SeriesName,
          DataPoints[ DataPointKey ]
        );
        
        DataPointItem.AxisLabel = SeriesName;
        DataPointItem.SetValueXY( Count, DataPoints[ DataPointKey ] );      

        this.BarChart.Series[ SeriesName ].Points.Add( item: DataPointItem );

        if( DataPoints[ DataPointKey ] > Max )
        {
          Max = DataPoints[ DataPointKey ];
        }

        Area = this.BarChart.ChartAreas[ 0 ];
        Area.AxisY.Maximum = Max + 10;
        Area.AxisY.Minimum = 0;
        
        Count++;
        
      }

      this.BarChart.Invalidate();
            
    }

    /**************************************************************************/

  }

}
