/*
 * Created by SharpDevelop.
 * User: jholland
 * Date: 8/22/2017
 * Time: 12:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
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
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
			series1.ChartArea = "ChartAreaMain";
			series1.Legend = "Legend1";
			series1.Name = "SeriesMain";
			this.barChartPanel.Series.Add(series1);
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
