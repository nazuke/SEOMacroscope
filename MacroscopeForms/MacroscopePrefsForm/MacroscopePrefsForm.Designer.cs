/*
 * Created by SharpDevelop.
 * User: jason
 * Date: 2017/01/12
 * Time: 21:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SEOMacroscope
{
	partial class MacroscopePrefsForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TabControl tabControlPreferences;
		private System.Windows.Forms.TabPage tabPageSpideringControl;
		private System.Windows.Forms.TabPage tabPageNetworkSettings;
		private System.Windows.Forms.TabPage tabPageAnalysisOptions;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxFetchStylesheets;
		private System.Windows.Forms.CheckBox checkBoxFetchPdfs;
		private System.Windows.Forms.CheckBox checkBoxFetchImages;
		private System.Windows.Forms.CheckBox checkBoxFetchJavascripts;
		private System.Windows.Forms.CheckBox checkBoxFetchBinaries;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.tabControlPreferences = new System.Windows.Forms.TabControl();
			this.tabPageSpideringControl = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxFetchStylesheets = new System.Windows.Forms.CheckBox();
			this.tabPageAnalysisOptions = new System.Windows.Forms.TabPage();
			this.tabPageNetworkSettings = new System.Windows.Forms.TabPage();
			this.checkBoxFetchJavascripts = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchImages = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchPdfs = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchBinaries = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tabControlPreferences.SuspendLayout();
			this.tabPageSpideringControl.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tabControlPreferences, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 461);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.button1);
			this.flowLayoutPanel1.Controls.Add(this.button2);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 404);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(578, 54);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(490, 13);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button2.Location = new System.Drawing.Point(409, 13);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "OK";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// tabControlPreferences
			// 
			this.tabControlPreferences.Controls.Add(this.tabPageSpideringControl);
			this.tabControlPreferences.Controls.Add(this.tabPageAnalysisOptions);
			this.tabControlPreferences.Controls.Add(this.tabPageNetworkSettings);
			this.tabControlPreferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlPreferences.Location = new System.Drawing.Point(3, 3);
			this.tabControlPreferences.Name = "tabControlPreferences";
			this.tabControlPreferences.SelectedIndex = 0;
			this.tabControlPreferences.Size = new System.Drawing.Size(578, 395);
			this.tabControlPreferences.TabIndex = 1;
			// 
			// tabPageSpideringControl
			// 
			this.tabPageSpideringControl.Controls.Add(this.groupBox1);
			this.tabPageSpideringControl.Location = new System.Drawing.Point(4, 22);
			this.tabPageSpideringControl.Name = "tabPageSpideringControl";
			this.tabPageSpideringControl.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSpideringControl.Size = new System.Drawing.Size(570, 369);
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
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(180, 180);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Spider Document Types";
			this.groupBox1.Enter += new System.EventHandler(this.GroupBox1Enter);
			// 
			// checkBoxFetchStylesheets
			// 
			this.checkBoxFetchStylesheets.Location = new System.Drawing.Point(6, 19);
			this.checkBoxFetchStylesheets.Name = "checkBoxFetchStylesheets";
			this.checkBoxFetchStylesheets.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchStylesheets.TabIndex = 0;
			this.checkBoxFetchStylesheets.Text = "CSS Stylesheets";
			this.checkBoxFetchStylesheets.UseVisualStyleBackColor = true;
			this.checkBoxFetchStylesheets.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// tabPageAnalysisOptions
			// 
			this.tabPageAnalysisOptions.Location = new System.Drawing.Point(4, 22);
			this.tabPageAnalysisOptions.Name = "tabPageAnalysisOptions";
			this.tabPageAnalysisOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAnalysisOptions.Size = new System.Drawing.Size(570, 369);
			this.tabPageAnalysisOptions.TabIndex = 2;
			this.tabPageAnalysisOptions.Text = "Analysis Options";
			this.tabPageAnalysisOptions.UseVisualStyleBackColor = true;
			// 
			// tabPageNetworkSettings
			// 
			this.tabPageNetworkSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageNetworkSettings.Name = "tabPageNetworkSettings";
			this.tabPageNetworkSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageNetworkSettings.Size = new System.Drawing.Size(570, 369);
			this.tabPageNetworkSettings.TabIndex = 1;
			this.tabPageNetworkSettings.Text = "Network Settings";
			this.tabPageNetworkSettings.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchJavascripts
			// 
			this.checkBoxFetchJavascripts.Location = new System.Drawing.Point(6, 49);
			this.checkBoxFetchJavascripts.Name = "checkBoxFetchJavascripts";
			this.checkBoxFetchJavascripts.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchJavascripts.TabIndex = 1;
			this.checkBoxFetchJavascripts.Text = "Javascripts";
			this.checkBoxFetchJavascripts.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchImages
			// 
			this.checkBoxFetchImages.Location = new System.Drawing.Point(6, 79);
			this.checkBoxFetchImages.Name = "checkBoxFetchImages";
			this.checkBoxFetchImages.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchImages.TabIndex = 2;
			this.checkBoxFetchImages.Text = "Images";
			this.checkBoxFetchImages.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchPdfs
			// 
			this.checkBoxFetchPdfs.Location = new System.Drawing.Point(6, 109);
			this.checkBoxFetchPdfs.Name = "checkBoxFetchPdfs";
			this.checkBoxFetchPdfs.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchPdfs.TabIndex = 3;
			this.checkBoxFetchPdfs.Text = "PDFs";
			this.checkBoxFetchPdfs.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchBinaries
			// 
			this.checkBoxFetchBinaries.Location = new System.Drawing.Point(6, 139);
			this.checkBoxFetchBinaries.Name = "checkBoxFetchBinaries";
			this.checkBoxFetchBinaries.Size = new System.Drawing.Size(104, 24);
			this.checkBoxFetchBinaries.TabIndex = 4;
			this.checkBoxFetchBinaries.Text = "Binary Files";
			this.checkBoxFetchBinaries.UseVisualStyleBackColor = true;
			// 
			// MacroscopePrefsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 461);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MacroscopePrefsForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SEO Macroscope Preferences";
			this.TopMost = true;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.tabControlPreferences.ResumeLayout(false);
			this.tabPageSpideringControl.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
