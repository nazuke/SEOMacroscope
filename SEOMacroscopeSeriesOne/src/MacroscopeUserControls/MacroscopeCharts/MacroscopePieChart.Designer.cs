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
	partial class MacroscopePieChart
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataVisualization.Charting.Chart pieChartPanel;
		
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
			this.pieChartPanel = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)(this.pieChartPanel)).BeginInit();
			this.SuspendLayout();
			// 
			// pieChartPanel
			// 
			chartArea1.Name = "ChartAreaMain";
			this.pieChartPanel.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.pieChartPanel.Legends.Add(legend1);
			this.pieChartPanel.Location = new System.Drawing.Point(10, 10);
			this.pieChartPanel.Name = "pieChartPanel";
			this.pieChartPanel.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
			this.pieChartPanel.Size = new System.Drawing.Size(250, 150);
			this.pieChartPanel.TabIndex = 1;
			this.pieChartPanel.Text = "chart1";
			// 
			// MacroscopePieChart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pieChartPanel);
			this.Name = "MacroscopePieChart";
			this.Size = new System.Drawing.Size(300, 200);
			((System.ComponentModel.ISupportInitialize)(this.pieChartPanel)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
