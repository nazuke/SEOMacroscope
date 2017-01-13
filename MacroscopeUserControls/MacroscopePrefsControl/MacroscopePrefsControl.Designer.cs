/*
 * Created by SharpDevelop.
 * User: jholland
 * Date: 1/12/2017
 * Time: 18:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SEOMacroscope
{
	partial class MacroscopePrefsControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TabControl tabControlPreferences;
		private System.Windows.Forms.TabPage tabPageSpideringControl;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxFetchBinaries;
		private System.Windows.Forms.CheckBox checkBoxFetchPdfs;
		private System.Windows.Forms.CheckBox checkBoxFetchImages;
		private System.Windows.Forms.CheckBox checkBoxFetchJavascripts;
		private System.Windows.Forms.CheckBox checkBoxFetchStylesheets;
		private System.Windows.Forms.TabPage tabPageAnalysisOptions;
		private System.Windows.Forms.TabPage tabPageNetworkSettings;
		private System.Windows.Forms.TabPage tabPageSeo;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		
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
			this.tabControlPreferences = new System.Windows.Forms.TabControl();
			this.tabPageSpideringControl = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxFetchBinaries = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchPdfs = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchImages = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchJavascripts = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchStylesheets = new System.Windows.Forms.CheckBox();
			this.tabPageAnalysisOptions = new System.Windows.Forms.TabPage();
			this.tabPageSeo = new System.Windows.Forms.TabPage();
			this.tabPageNetworkSettings = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.tabControlPreferences.SuspendLayout();
			this.tabPageSpideringControl.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPageSeo.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlPreferences
			// 
			this.tabControlPreferences.Controls.Add(this.tabPageSpideringControl);
			this.tabControlPreferences.Controls.Add(this.tabPageAnalysisOptions);
			this.tabControlPreferences.Controls.Add(this.tabPageSeo);
			this.tabControlPreferences.Controls.Add(this.tabPageNetworkSettings);
			this.tabControlPreferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlPreferences.Location = new System.Drawing.Point(0, 0);
			this.tabControlPreferences.Name = "tabControlPreferences";
			this.tabControlPreferences.SelectedIndex = 0;
			this.tabControlPreferences.Size = new System.Drawing.Size(600, 600);
			this.tabControlPreferences.TabIndex = 2;
			// 
			// tabPageSpideringControl
			// 
			this.tabPageSpideringControl.AutoScroll = true;
			this.tabPageSpideringControl.Controls.Add(this.groupBox1);
			this.tabPageSpideringControl.Location = new System.Drawing.Point(4, 22);
			this.tabPageSpideringControl.Name = "tabPageSpideringControl";
			this.tabPageSpideringControl.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSpideringControl.Size = new System.Drawing.Size(592, 574);
			this.tabPageSpideringControl.TabIndex = 0;
			this.tabPageSpideringControl.Text = "Spidering Control";
			this.tabPageSpideringControl.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBoxFetchBinaries);
			this.groupBox1.Controls.Add(this.checkBoxFetchPdfs);
			this.groupBox1.Controls.Add(this.checkBoxFetchImages);
			this.groupBox1.Controls.Add(this.checkBoxFetchJavascripts);
			this.groupBox1.Controls.Add(this.checkBoxFetchStylesheets);
			this.groupBox1.Location = new System.Drawing.Point(10, 10);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(570, 180);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Spider Document Types";
			// 
			// checkBoxFetchBinaries
			// 
			this.checkBoxFetchBinaries.Location = new System.Drawing.Point(20, 140);
			this.checkBoxFetchBinaries.Name = "checkBoxFetchBinaries";
			this.checkBoxFetchBinaries.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchBinaries.TabIndex = 4;
			this.checkBoxFetchBinaries.Text = "Binary Files";
			this.checkBoxFetchBinaries.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchPdfs
			// 
			this.checkBoxFetchPdfs.Location = new System.Drawing.Point(20, 110);
			this.checkBoxFetchPdfs.Name = "checkBoxFetchPdfs";
			this.checkBoxFetchPdfs.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchPdfs.TabIndex = 3;
			this.checkBoxFetchPdfs.Text = "PDFs";
			this.checkBoxFetchPdfs.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchImages
			// 
			this.checkBoxFetchImages.Location = new System.Drawing.Point(20, 80);
			this.checkBoxFetchImages.Name = "checkBoxFetchImages";
			this.checkBoxFetchImages.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchImages.TabIndex = 2;
			this.checkBoxFetchImages.Text = "Images";
			this.checkBoxFetchImages.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchJavascripts
			// 
			this.checkBoxFetchJavascripts.Location = new System.Drawing.Point(20, 50);
			this.checkBoxFetchJavascripts.Name = "checkBoxFetchJavascripts";
			this.checkBoxFetchJavascripts.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchJavascripts.TabIndex = 1;
			this.checkBoxFetchJavascripts.Text = "Javascripts";
			this.checkBoxFetchJavascripts.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchStylesheets
			// 
			this.checkBoxFetchStylesheets.Location = new System.Drawing.Point(20, 20);
			this.checkBoxFetchStylesheets.Name = "checkBoxFetchStylesheets";
			this.checkBoxFetchStylesheets.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchStylesheets.TabIndex = 0;
			this.checkBoxFetchStylesheets.Text = "CSS Stylesheets";
			this.checkBoxFetchStylesheets.UseVisualStyleBackColor = true;
			// 
			// tabPageAnalysisOptions
			// 
			this.tabPageAnalysisOptions.Location = new System.Drawing.Point(4, 22);
			this.tabPageAnalysisOptions.Name = "tabPageAnalysisOptions";
			this.tabPageAnalysisOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAnalysisOptions.Size = new System.Drawing.Size(592, 574);
			this.tabPageAnalysisOptions.TabIndex = 2;
			this.tabPageAnalysisOptions.Text = "Analysis Options";
			this.tabPageAnalysisOptions.UseVisualStyleBackColor = true;
			// 
			// tabPageSeo
			// 
			this.tabPageSeo.AutoScroll = true;
			this.tabPageSeo.Controls.Add(this.groupBox3);
			this.tabPageSeo.Controls.Add(this.groupBox2);
			this.tabPageSeo.Location = new System.Drawing.Point(4, 22);
			this.tabPageSeo.Name = "tabPageSeo";
			this.tabPageSeo.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSeo.Size = new System.Drawing.Size(592, 574);
			this.tabPageSeo.TabIndex = 3;
			this.tabPageSeo.Text = "SEO Options";
			this.tabPageSeo.UseVisualStyleBackColor = true;
			// 
			// tabPageNetworkSettings
			// 
			this.tabPageNetworkSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageNetworkSettings.Name = "tabPageNetworkSettings";
			this.tabPageNetworkSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageNetworkSettings.Size = new System.Drawing.Size(592, 574);
			this.tabPageNetworkSettings.TabIndex = 1;
			this.tabPageNetworkSettings.Text = "Network Settings";
			this.tabPageNetworkSettings.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(10, 10);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(570, 172);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Page Titles";
			// 
			// groupBox3
			// 
			this.groupBox3.Location = new System.Drawing.Point(10, 188);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(570, 172);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Page Descriptions";
			// 
			// MacroscopePrefsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControlPreferences);
			this.Name = "MacroscopePrefsControl";
			this.Size = new System.Drawing.Size(600, 600);
			this.tabControlPreferences.ResumeLayout(false);
			this.tabPageSpideringControl.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPageSeo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
