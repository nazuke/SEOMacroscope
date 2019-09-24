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

namespace SEOMacroscope
{
	partial class MacroscopeBarChart
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Data.DataView dataView1;
		private System.Windows.Forms.DataVisualization.Charting.Chart barChartPanel;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			this.dataView1 = new System.Data.DataView();
			this.barChartPanel = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)(this.dataView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barChartPanel)).BeginInit();
			this.SuspendLayout();
			// 
			// barChartPanel
			// 
			chartArea1.Name = "ChartAreaMain";
			this.barChartPanel.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.barChartPanel.Legends.Add(legend1);
			this.barChartPanel.Location = new System.Drawing.Point(10, 10);
			this.barChartPanel.Name = "barChartPanel";
			this.barChartPanel.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
			this.barChartPanel.Size = new System.Drawing.Size(250, 150);
			this.barChartPanel.TabIndex = 0;
			this.barChartPanel.Text = "chart1";
			// 
			// MacroscopeBarChart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.barChartPanel);
			this.Name = "MacroscopeBarChart";
			this.Size = new System.Drawing.Size(300, 200);
			((System.ComponentModel.ISupportInitialize)(this.dataView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barChartPanel)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
