/*
 * Created by SharpDevelop.
 * User: jholland
 * Date: 8/22/2017
 * Time: 15:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SEOMacroscope
{
	partial class MacroscopeSiteStructurePanelCharts
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		public System.Windows.Forms.TabControl tabControlCharts;
		private System.Windows.Forms.TabPage tabPageChartsSiteSummary;
		private System.Windows.Forms.TabPage tabPageChartsLanguagesDetected;
		private System.Windows.Forms.TabPage tabPageChartsReadability;
		public SEOMacroscope.MacroscopeBarChart barChartSiteSummary;
		private SEOMacroscope.MacroscopeBarChart barChartLanguagesDetected;
		public SEOMacroscope.MacroscopeBarChart barChartReadability;
		
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
			this.tabControlCharts = new System.Windows.Forms.TabControl();
			this.tabPageChartsSiteSummary = new System.Windows.Forms.TabPage();
			this.tabPageChartsLanguagesDetected = new System.Windows.Forms.TabPage();
			this.tabPageChartsReadability = new System.Windows.Forms.TabPage();
			this.barChartSiteSummary = new SEOMacroscope.MacroscopeBarChart();
			this.barChartLanguagesDetected = new SEOMacroscope.MacroscopeBarChart();
			this.barChartReadability = new SEOMacroscope.MacroscopeBarChart();
			this.tabControlCharts.SuspendLayout();
			this.tabPageChartsSiteSummary.SuspendLayout();
			this.tabPageChartsLanguagesDetected.SuspendLayout();
			this.tabPageChartsReadability.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlCharts
			// 
			this.tabControlCharts.Controls.Add(this.tabPageChartsSiteSummary);
			this.tabControlCharts.Controls.Add(this.tabPageChartsLanguagesDetected);
			this.tabControlCharts.Controls.Add(this.tabPageChartsReadability);
			this.tabControlCharts.Location = new System.Drawing.Point(10, 10);
			this.tabControlCharts.Name = "tabControlCharts";
			this.tabControlCharts.SelectedIndex = 0;
			this.tabControlCharts.Size = new System.Drawing.Size(300, 300);
			this.tabControlCharts.TabIndex = 1;
			// 
			// tabPageChartsSiteSummary
			// 
			this.tabPageChartsSiteSummary.BackColor = System.Drawing.Color.LightGray;
			this.tabPageChartsSiteSummary.CausesValidation = false;
			this.tabPageChartsSiteSummary.Controls.Add(this.barChartSiteSummary);
			this.tabPageChartsSiteSummary.Location = new System.Drawing.Point(4, 22);
			this.tabPageChartsSiteSummary.Name = "tabPageChartsSiteSummary";
			this.tabPageChartsSiteSummary.Size = new System.Drawing.Size(292, 274);
			this.tabPageChartsSiteSummary.TabIndex = 0;
			this.tabPageChartsSiteSummary.Text = "Site Summary";
			// 
			// tabPageChartsLanguagesDetected
			// 
			this.tabPageChartsLanguagesDetected.BackColor = System.Drawing.Color.LightGray;
			this.tabPageChartsLanguagesDetected.CausesValidation = false;
			this.tabPageChartsLanguagesDetected.Controls.Add(this.barChartLanguagesDetected);
			this.tabPageChartsLanguagesDetected.Location = new System.Drawing.Point(4, 22);
			this.tabPageChartsLanguagesDetected.Name = "tabPageChartsLanguagesDetected";
			this.tabPageChartsLanguagesDetected.Size = new System.Drawing.Size(292, 274);
			this.tabPageChartsLanguagesDetected.TabIndex = 1;
			this.tabPageChartsLanguagesDetected.Text = "Language Detected";
			// 
			// tabPageChartsReadability
			// 
			this.tabPageChartsReadability.BackColor = System.Drawing.Color.LightGray;
			this.tabPageChartsReadability.CausesValidation = false;
			this.tabPageChartsReadability.Controls.Add(this.barChartReadability);
			this.tabPageChartsReadability.Location = new System.Drawing.Point(4, 22);
			this.tabPageChartsReadability.Name = "tabPageChartsReadability";
			this.tabPageChartsReadability.Size = new System.Drawing.Size(292, 274);
			this.tabPageChartsReadability.TabIndex = 2;
			this.tabPageChartsReadability.Text = "Readability";
			// 
			// barChartSiteSummary
			// 
			this.barChartSiteSummary.Location = new System.Drawing.Point(10, 10);
			this.barChartSiteSummary.Name = "barChartSiteSummary";
			this.barChartSiteSummary.Size = new System.Drawing.Size(200, 200);
			this.barChartSiteSummary.TabIndex = 0;
			// 
			// barChartLanguagesDetected
			// 
			this.barChartLanguagesDetected.Location = new System.Drawing.Point(10, 10);
			this.barChartLanguagesDetected.Name = "barChartLanguagesDetected";
			this.barChartLanguagesDetected.Size = new System.Drawing.Size(200, 200);
			this.barChartLanguagesDetected.TabIndex = 1;
			// 
			// barChartReadability
			// 
			this.barChartReadability.Location = new System.Drawing.Point(10, 10);
			this.barChartReadability.Name = "barChartReadability";
			this.barChartReadability.Size = new System.Drawing.Size(200, 200);
			this.barChartReadability.TabIndex = 1;
			// 
			// MacroscopeSiteStructurePanelCharts
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControlCharts);
			this.Name = "MacroscopeSiteStructurePanelCharts";
			this.Size = new System.Drawing.Size(350, 350);
			this.tabControlCharts.ResumeLayout(false);
			this.tabPageChartsSiteSummary.ResumeLayout(false);
			this.tabPageChartsLanguagesDetected.ResumeLayout(false);
			this.tabPageChartsReadability.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
