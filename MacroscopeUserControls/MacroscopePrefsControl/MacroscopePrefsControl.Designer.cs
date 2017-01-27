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
		public System.Windows.Forms.CheckBox checkBoxFetchBinaries;
		public System.Windows.Forms.CheckBox checkBoxFetchPdfs;
		public System.Windows.Forms.CheckBox checkBoxFetchImages;
		public System.Windows.Forms.CheckBox checkBoxFetchJavascripts;
		public System.Windows.Forms.CheckBox checkBoxFetchStylesheets;
		private System.Windows.Forms.TabPage tabPageAnalysisOptions;
		private System.Windows.Forms.TabPage tabPageNetworkSettings;
		private System.Windows.Forms.TabPage tabPageSeo;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		public System.Windows.Forms.NumericUpDown numericUpDownTitleMaxLen;
		public System.Windows.Forms.NumericUpDown numericUpDownTitleMinLen;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.NumericUpDown numericUpDownTitleMaxWords;
		public System.Windows.Forms.NumericUpDown numericUpDownTitleMinWords;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		public System.Windows.Forms.NumericUpDown numericUpDownDescriptionMaxWords;
		public System.Windows.Forms.NumericUpDown numericUpDownDescriptionMinWords;
		public System.Windows.Forms.NumericUpDown numericUpDownDescriptionMaxLen;
		public System.Windows.Forms.NumericUpDown numericUpDownDescriptionMinLen;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		public System.Windows.Forms.NumericUpDown numericUpDownPageLimit;
		public System.Windows.Forms.NumericUpDown numericUpDownDepth;
		private System.Windows.Forms.GroupBox groupBox5;
		public System.Windows.Forms.CheckBox checkBoxFollowRobotsProtocol;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.GroupBox groupBox6;
		public System.Windows.Forms.CheckBox checkBoxCheckHreflangs;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
		private System.Windows.Forms.GroupBox groupBox8;
		public System.Windows.Forms.NumericUpDown numericUpDownHttpProxyPort;
		public System.Windows.Forms.TextBox textBoxHttpProxyHost;
		private System.Windows.Forms.Label label13;
		public System.Windows.Forms.NumericUpDown numericUpDownMaxThreads;
		public System.Windows.Forms.CheckBox checkBoxFollowNoFollow;
		public System.Windows.Forms.CheckBox checkBoxFollowRedirects;
		public System.Windows.Forms.CheckBox checkBoxFollowCanonicalLinks;
		public System.Windows.Forms.CheckBox checkBoxFollowHrefLangLinks;
		public System.Windows.Forms.CheckBox checkBoxCheckExternalLinks;
		private System.Windows.Forms.GroupBox groupBox7;
		public System.Windows.Forms.NumericUpDown numericUpDown;
		
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
			System.Windows.Forms.Label label9;
			System.Windows.Forms.Label label10;
			this.tabControlPreferences = new System.Windows.Forms.TabControl();
			this.tabPageSpideringControl = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.checkBoxFollowHrefLangLinks = new System.Windows.Forms.CheckBox();
			this.checkBoxFollowCanonicalLinks = new System.Windows.Forms.CheckBox();
			this.checkBoxFollowRedirects = new System.Windows.Forms.CheckBox();
			this.checkBoxFollowNoFollow = new System.Windows.Forms.CheckBox();
			this.checkBoxFollowRobotsProtocol = new System.Windows.Forms.CheckBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.checkBoxCheckExternalLinks = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.numericUpDownMaxThreads = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.numericUpDownPageLimit = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownDepth = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxFetchBinaries = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchPdfs = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchImages = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchJavascripts = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchStylesheets = new System.Windows.Forms.CheckBox();
			this.tabPageAnalysisOptions = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkBoxCheckHreflangs = new System.Windows.Forms.CheckBox();
			this.tabPageSeo = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownTitleMaxWords = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownTitleMinWords = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownTitleMaxLen = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownTitleMinLen = new System.Windows.Forms.NumericUpDown();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.numericUpDownDescriptionMaxWords = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownDescriptionMinWords = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownDescriptionMaxLen = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownDescriptionMinLen = new System.Windows.Forms.NumericUpDown();
			this.tabPageNetworkSettings = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.textBoxHttpProxyHost = new System.Windows.Forms.TextBox();
			this.numericUpDownHttpProxyPort = new System.Windows.Forms.NumericUpDown();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			this.tabControlPreferences.SuspendLayout();
			this.tabPageSpideringControl.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxThreads)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPageLimit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDepth)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.tabPageAnalysisOptions.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.tabPageSeo.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxWords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMinWords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxLen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMinLen)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMaxWords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMinWords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMaxLen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMinLen)).BeginInit();
			this.tabPageNetworkSettings.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			this.groupBox8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHttpProxyPort)).BeginInit();
			this.SuspendLayout();
			// 
			// label9
			// 
			label9.Location = new System.Drawing.Point(223, 23);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(210, 23);
			label9.TabIndex = 0;
			label9.Text = "HTTP Proxy Hostname";
			// 
			// label10
			// 
			label10.Location = new System.Drawing.Point(223, 52);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(200, 23);
			label10.TabIndex = 4;
			label10.Text = "HTTP Proxy Port";
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
			this.tabControlPreferences.TabIndex = 0;
			// 
			// tabPageSpideringControl
			// 
			this.tabPageSpideringControl.AutoScroll = true;
			this.tabPageSpideringControl.Controls.Add(this.flowLayoutPanel1);
			this.tabPageSpideringControl.Location = new System.Drawing.Point(4, 22);
			this.tabPageSpideringControl.Name = "tabPageSpideringControl";
			this.tabPageSpideringControl.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSpideringControl.Size = new System.Drawing.Size(592, 574);
			this.tabPageSpideringControl.TabIndex = 0;
			this.tabPageSpideringControl.Text = "Spidering Control";
			this.tabPageSpideringControl.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.groupBox5);
			this.flowLayoutPanel1.Controls.Add(this.groupBox7);
			this.flowLayoutPanel1.Controls.Add(this.groupBox4);
			this.flowLayoutPanel1.Controls.Add(this.groupBox1);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(586, 568);
			this.flowLayoutPanel1.TabIndex = 6;
			// 
			// groupBox5
			// 
			this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox5.Controls.Add(this.checkBoxFollowHrefLangLinks);
			this.groupBox5.Controls.Add(this.checkBoxFollowCanonicalLinks);
			this.groupBox5.Controls.Add(this.checkBoxFollowRedirects);
			this.groupBox5.Controls.Add(this.checkBoxFollowNoFollow);
			this.groupBox5.Controls.Add(this.checkBoxFollowRobotsProtocol);
			this.groupBox5.Location = new System.Drawing.Point(10, 10);
			this.groupBox5.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(500, 120);
			this.groupBox5.TabIndex = 1;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Standards";
			// 
			// checkBoxFollowHrefLangLinks
			// 
			this.checkBoxFollowHrefLangLinks.Location = new System.Drawing.Point(220, 50);
			this.checkBoxFollowHrefLangLinks.Name = "checkBoxFollowHrefLangLinks";
			this.checkBoxFollowHrefLangLinks.Size = new System.Drawing.Size(160, 24);
			this.checkBoxFollowHrefLangLinks.TabIndex = 5;
			this.checkBoxFollowHrefLangLinks.Text = "Follow HREFLANG links";
			this.checkBoxFollowHrefLangLinks.UseVisualStyleBackColor = true;
			// 
			// checkBoxFollowCanonicalLinks
			// 
			this.checkBoxFollowCanonicalLinks.Location = new System.Drawing.Point(220, 20);
			this.checkBoxFollowCanonicalLinks.Name = "checkBoxFollowCanonicalLinks";
			this.checkBoxFollowCanonicalLinks.Size = new System.Drawing.Size(160, 24);
			this.checkBoxFollowCanonicalLinks.TabIndex = 4;
			this.checkBoxFollowCanonicalLinks.Text = "Follow canonical links";
			this.checkBoxFollowCanonicalLinks.UseVisualStyleBackColor = true;
			// 
			// checkBoxFollowRedirects
			// 
			this.checkBoxFollowRedirects.Location = new System.Drawing.Point(20, 50);
			this.checkBoxFollowRedirects.Name = "checkBoxFollowRedirects";
			this.checkBoxFollowRedirects.Size = new System.Drawing.Size(160, 24);
			this.checkBoxFollowRedirects.TabIndex = 2;
			this.checkBoxFollowRedirects.Text = "Follow redirects";
			this.checkBoxFollowRedirects.UseVisualStyleBackColor = true;
			// 
			// checkBoxFollowNoFollow
			// 
			this.checkBoxFollowNoFollow.Location = new System.Drawing.Point(20, 80);
			this.checkBoxFollowNoFollow.Name = "checkBoxFollowNoFollow";
			this.checkBoxFollowNoFollow.Size = new System.Drawing.Size(160, 24);
			this.checkBoxFollowNoFollow.TabIndex = 3;
			this.checkBoxFollowNoFollow.Text = "Follow rel=\"nofollow\" links";
			this.checkBoxFollowNoFollow.UseVisualStyleBackColor = true;
			// 
			// checkBoxFollowRobotsProtocol
			// 
			this.checkBoxFollowRobotsProtocol.Location = new System.Drawing.Point(20, 20);
			this.checkBoxFollowRobotsProtocol.Name = "checkBoxFollowRobotsProtocol";
			this.checkBoxFollowRobotsProtocol.Size = new System.Drawing.Size(160, 24);
			this.checkBoxFollowRobotsProtocol.TabIndex = 1;
			this.checkBoxFollowRobotsProtocol.Text = "Follow robots protocol";
			this.checkBoxFollowRobotsProtocol.UseVisualStyleBackColor = true;
			// 
			// groupBox7
			// 
			this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox7.Controls.Add(this.checkBoxCheckExternalLinks);
			this.groupBox7.Location = new System.Drawing.Point(10, 140);
			this.groupBox7.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(500, 60);
			this.groupBox7.TabIndex = 9;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Domain Restrictions";
			// 
			// checkBoxCheckExternalLinks
			// 
			this.checkBoxCheckExternalLinks.Location = new System.Drawing.Point(20, 20);
			this.checkBoxCheckExternalLinks.Name = "checkBoxCheckExternalLinks";
			this.checkBoxCheckExternalLinks.Size = new System.Drawing.Size(200, 24);
			this.checkBoxCheckExternalLinks.TabIndex = 7;
			this.checkBoxCheckExternalLinks.Text = "Check external links";
			this.checkBoxCheckExternalLinks.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.numericUpDownMaxThreads);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.numericUpDownPageLimit);
			this.groupBox4.Controls.Add(this.numericUpDownDepth);
			this.groupBox4.Location = new System.Drawing.Point(10, 210);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(500, 120);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Spidering Limits";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(146, 22);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(200, 23);
			this.label13.TabIndex = 9;
			this.label13.Text = "Maximum worker threads";
			// 
			// numericUpDownMaxThreads
			// 
			this.numericUpDownMaxThreads.Location = new System.Drawing.Point(20, 20);
			this.numericUpDownMaxThreads.Maximum = new decimal(new int[] {
			64,
			0,
			0,
			0});
			this.numericUpDownMaxThreads.Minimum = new decimal(new int[] {
			4,
			0,
			0,
			0});
			this.numericUpDownMaxThreads.Name = "numericUpDownMaxThreads";
			this.numericUpDownMaxThreads.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownMaxThreads.TabIndex = 6;
			this.numericUpDownMaxThreads.Value = new decimal(new int[] {
			4,
			0,
			0,
			0});
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(146, 82);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(200, 23);
			this.label11.TabIndex = 5;
			this.label11.Text = "Maximum pages fetched";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(146, 52);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(200, 23);
			this.label12.TabIndex = 4;
			this.label12.Text = "Maximum page depth";
			// 
			// numericUpDownPageLimit
			// 
			this.numericUpDownPageLimit.Location = new System.Drawing.Point(20, 80);
			this.numericUpDownPageLimit.Maximum = new decimal(new int[] {
			1000000000,
			0,
			0,
			0});
			this.numericUpDownPageLimit.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownPageLimit.Name = "numericUpDownPageLimit";
			this.numericUpDownPageLimit.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownPageLimit.TabIndex = 9;
			this.numericUpDownPageLimit.Value = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			// 
			// numericUpDownDepth
			// 
			this.numericUpDownDepth.Location = new System.Drawing.Point(20, 50);
			this.numericUpDownDepth.Maximum = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			this.numericUpDownDepth.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownDepth.Name = "numericUpDownDepth";
			this.numericUpDownDepth.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownDepth.TabIndex = 8;
			this.numericUpDownDepth.Value = new decimal(new int[] {
			100,
			0,
			0,
			0});
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox1.Controls.Add(this.checkBoxFetchBinaries);
			this.groupBox1.Controls.Add(this.checkBoxFetchPdfs);
			this.groupBox1.Controls.Add(this.checkBoxFetchImages);
			this.groupBox1.Controls.Add(this.checkBoxFetchJavascripts);
			this.groupBox1.Controls.Add(this.checkBoxFetchStylesheets);
			this.groupBox1.Location = new System.Drawing.Point(10, 340);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(500, 120);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Fetch Document Types";
			// 
			// checkBoxFetchBinaries
			// 
			this.checkBoxFetchBinaries.Location = new System.Drawing.Point(146, 50);
			this.checkBoxFetchBinaries.Name = "checkBoxFetchBinaries";
			this.checkBoxFetchBinaries.Size = new System.Drawing.Size(110, 24);
			this.checkBoxFetchBinaries.TabIndex = 14;
			this.checkBoxFetchBinaries.Text = "Binary Files";
			this.checkBoxFetchBinaries.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchPdfs
			// 
			this.checkBoxFetchPdfs.Location = new System.Drawing.Point(146, 20);
			this.checkBoxFetchPdfs.Name = "checkBoxFetchPdfs";
			this.checkBoxFetchPdfs.Size = new System.Drawing.Size(110, 24);
			this.checkBoxFetchPdfs.TabIndex = 13;
			this.checkBoxFetchPdfs.Text = "PDFs";
			this.checkBoxFetchPdfs.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchImages
			// 
			this.checkBoxFetchImages.Location = new System.Drawing.Point(20, 80);
			this.checkBoxFetchImages.Name = "checkBoxFetchImages";
			this.checkBoxFetchImages.Size = new System.Drawing.Size(110, 24);
			this.checkBoxFetchImages.TabIndex = 12;
			this.checkBoxFetchImages.Text = "Images";
			this.checkBoxFetchImages.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchJavascripts
			// 
			this.checkBoxFetchJavascripts.Location = new System.Drawing.Point(20, 50);
			this.checkBoxFetchJavascripts.Name = "checkBoxFetchJavascripts";
			this.checkBoxFetchJavascripts.Size = new System.Drawing.Size(110, 24);
			this.checkBoxFetchJavascripts.TabIndex = 11;
			this.checkBoxFetchJavascripts.Text = "Javascripts";
			this.checkBoxFetchJavascripts.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchStylesheets
			// 
			this.checkBoxFetchStylesheets.Location = new System.Drawing.Point(20, 20);
			this.checkBoxFetchStylesheets.Name = "checkBoxFetchStylesheets";
			this.checkBoxFetchStylesheets.Size = new System.Drawing.Size(110, 24);
			this.checkBoxFetchStylesheets.TabIndex = 10;
			this.checkBoxFetchStylesheets.Text = "CSS Stylesheets";
			this.checkBoxFetchStylesheets.UseVisualStyleBackColor = true;
			// 
			// tabPageAnalysisOptions
			// 
			this.tabPageAnalysisOptions.Controls.Add(this.flowLayoutPanel3);
			this.tabPageAnalysisOptions.Location = new System.Drawing.Point(4, 22);
			this.tabPageAnalysisOptions.Name = "tabPageAnalysisOptions";
			this.tabPageAnalysisOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAnalysisOptions.Size = new System.Drawing.Size(592, 574);
			this.tabPageAnalysisOptions.TabIndex = 2;
			this.tabPageAnalysisOptions.Text = "Analysis Options";
			this.tabPageAnalysisOptions.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.AutoScroll = true;
			this.flowLayoutPanel3.AutoSize = true;
			this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel3.Controls.Add(this.groupBox6);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(586, 568);
			this.flowLayoutPanel3.TabIndex = 7;
			// 
			// groupBox6
			// 
			this.groupBox6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox6.Controls.Add(this.checkBoxCheckHreflangs);
			this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox6.Location = new System.Drawing.Point(10, 10);
			this.groupBox6.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(500, 60);
			this.groupBox6.TabIndex = 1;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Localized Pages";
			// 
			// checkBoxCheckHreflangs
			// 
			this.checkBoxCheckHreflangs.Location = new System.Drawing.Point(20, 20);
			this.checkBoxCheckHreflangs.Name = "checkBoxCheckHreflangs";
			this.checkBoxCheckHreflangs.Size = new System.Drawing.Size(200, 24);
			this.checkBoxCheckHreflangs.TabIndex = 1;
			this.checkBoxCheckHreflangs.Text = "Check Linked HrefLang Pages";
			this.checkBoxCheckHreflangs.UseVisualStyleBackColor = true;
			// 
			// tabPageSeo
			// 
			this.tabPageSeo.AutoScroll = true;
			this.tabPageSeo.Controls.Add(this.flowLayoutPanel2);
			this.tabPageSeo.Location = new System.Drawing.Point(4, 22);
			this.tabPageSeo.Name = "tabPageSeo";
			this.tabPageSeo.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSeo.Size = new System.Drawing.Size(592, 574);
			this.tabPageSeo.TabIndex = 3;
			this.tabPageSeo.Text = "SEO Options";
			this.tabPageSeo.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.groupBox2);
			this.flowLayoutPanel2.Controls.Add(this.groupBox3);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(586, 568);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// groupBox2
			// 
			this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.numericUpDownTitleMaxWords);
			this.groupBox2.Controls.Add(this.numericUpDownTitleMinWords);
			this.groupBox2.Controls.Add(this.numericUpDownTitleMaxLen);
			this.groupBox2.Controls.Add(this.numericUpDownTitleMinLen);
			this.groupBox2.Location = new System.Drawing.Point(10, 10);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(500, 160);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Page Title Policies";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(146, 122);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(200, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Maximum Title Words";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(146, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(200, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "Minimum Title Words";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(146, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(200, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Maximum Title Length";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(146, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(200, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Minimum Title Length";
			// 
			// numericUpDownTitleMaxWords
			// 
			this.numericUpDownTitleMaxWords.Location = new System.Drawing.Point(20, 120);
			this.numericUpDownTitleMaxWords.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownTitleMaxWords.Name = "numericUpDownTitleMaxWords";
			this.numericUpDownTitleMaxWords.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownTitleMaxWords.TabIndex = 4;
			this.numericUpDownTitleMaxWords.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// numericUpDownTitleMinWords
			// 
			this.numericUpDownTitleMinWords.Location = new System.Drawing.Point(20, 90);
			this.numericUpDownTitleMinWords.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownTitleMinWords.Name = "numericUpDownTitleMinWords";
			this.numericUpDownTitleMinWords.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownTitleMinWords.TabIndex = 3;
			this.numericUpDownTitleMinWords.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// numericUpDownTitleMaxLen
			// 
			this.numericUpDownTitleMaxLen.Location = new System.Drawing.Point(20, 60);
			this.numericUpDownTitleMaxLen.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownTitleMaxLen.Name = "numericUpDownTitleMaxLen";
			this.numericUpDownTitleMaxLen.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownTitleMaxLen.TabIndex = 2;
			this.numericUpDownTitleMaxLen.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// numericUpDownTitleMinLen
			// 
			this.numericUpDownTitleMinLen.Location = new System.Drawing.Point(20, 30);
			this.numericUpDownTitleMinLen.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownTitleMinLen.Name = "numericUpDownTitleMinLen";
			this.numericUpDownTitleMinLen.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownTitleMinLen.TabIndex = 1;
			this.numericUpDownTitleMinLen.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.numericUpDownDescriptionMaxWords);
			this.groupBox3.Controls.Add(this.numericUpDownDescriptionMinWords);
			this.groupBox3.Controls.Add(this.numericUpDownDescriptionMaxLen);
			this.groupBox3.Controls.Add(this.numericUpDownDescriptionMinLen);
			this.groupBox3.Location = new System.Drawing.Point(10, 180);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(500, 160);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Page Description Policies";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(146, 122);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(200, 23);
			this.label5.TabIndex = 15;
			this.label5.Text = "Maximum Description Words";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(146, 92);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(200, 23);
			this.label6.TabIndex = 14;
			this.label6.Text = "Minimum Description Words";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(146, 62);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(200, 23);
			this.label7.TabIndex = 13;
			this.label7.Text = "Maximum Description Length";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(146, 32);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(200, 23);
			this.label8.TabIndex = 12;
			this.label8.Text = "Minimum Description Length";
			// 
			// numericUpDownDescriptionMaxWords
			// 
			this.numericUpDownDescriptionMaxWords.Location = new System.Drawing.Point(20, 120);
			this.numericUpDownDescriptionMaxWords.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownDescriptionMaxWords.Name = "numericUpDownDescriptionMaxWords";
			this.numericUpDownDescriptionMaxWords.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownDescriptionMaxWords.TabIndex = 8;
			this.numericUpDownDescriptionMaxWords.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// numericUpDownDescriptionMinWords
			// 
			this.numericUpDownDescriptionMinWords.Location = new System.Drawing.Point(20, 90);
			this.numericUpDownDescriptionMinWords.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownDescriptionMinWords.Name = "numericUpDownDescriptionMinWords";
			this.numericUpDownDescriptionMinWords.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownDescriptionMinWords.TabIndex = 7;
			this.numericUpDownDescriptionMinWords.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// numericUpDownDescriptionMaxLen
			// 
			this.numericUpDownDescriptionMaxLen.Location = new System.Drawing.Point(20, 60);
			this.numericUpDownDescriptionMaxLen.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownDescriptionMaxLen.Name = "numericUpDownDescriptionMaxLen";
			this.numericUpDownDescriptionMaxLen.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownDescriptionMaxLen.TabIndex = 6;
			this.numericUpDownDescriptionMaxLen.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// numericUpDownDescriptionMinLen
			// 
			this.numericUpDownDescriptionMinLen.Location = new System.Drawing.Point(20, 30);
			this.numericUpDownDescriptionMinLen.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownDescriptionMinLen.Name = "numericUpDownDescriptionMinLen";
			this.numericUpDownDescriptionMinLen.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownDescriptionMinLen.TabIndex = 5;
			this.numericUpDownDescriptionMinLen.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// tabPageNetworkSettings
			// 
			this.tabPageNetworkSettings.Controls.Add(this.flowLayoutPanel4);
			this.tabPageNetworkSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageNetworkSettings.Name = "tabPageNetworkSettings";
			this.tabPageNetworkSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageNetworkSettings.Size = new System.Drawing.Size(592, 574);
			this.tabPageNetworkSettings.TabIndex = 1;
			this.tabPageNetworkSettings.Text = "Network Settings";
			this.tabPageNetworkSettings.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel4
			// 
			this.flowLayoutPanel4.AutoScroll = true;
			this.flowLayoutPanel4.AutoSize = true;
			this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel4.Controls.Add(this.groupBox8);
			this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size(586, 568);
			this.flowLayoutPanel4.TabIndex = 7;
			// 
			// groupBox8
			// 
			this.groupBox8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox8.Controls.Add(label9);
			this.groupBox8.Controls.Add(this.textBoxHttpProxyHost);
			this.groupBox8.Controls.Add(label10);
			this.groupBox8.Controls.Add(this.numericUpDownHttpProxyPort);
			this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox8.Location = new System.Drawing.Point(10, 10);
			this.groupBox8.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(500, 120);
			this.groupBox8.TabIndex = 1;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "HTTP Proxy";
			// 
			// textBoxHttpProxyHost
			// 
			this.textBoxHttpProxyHost.Location = new System.Drawing.Point(17, 20);
			this.textBoxHttpProxyHost.MaxLength = 256;
			this.textBoxHttpProxyHost.Name = "textBoxHttpProxyHost";
			this.textBoxHttpProxyHost.Size = new System.Drawing.Size(200, 20);
			this.textBoxHttpProxyHost.TabIndex = 1;
			this.textBoxHttpProxyHost.WordWrap = false;
			// 
			// numericUpDownHttpProxyPort
			// 
			this.numericUpDownHttpProxyPort.Location = new System.Drawing.Point(97, 50);
			this.numericUpDownHttpProxyPort.Maximum = new decimal(new int[] {
			65535,
			0,
			0,
			0});
			this.numericUpDownHttpProxyPort.Name = "numericUpDownHttpProxyPort";
			this.numericUpDownHttpProxyPort.Size = new System.Drawing.Size(120, 20);
			this.numericUpDownHttpProxyPort.TabIndex = 2;
			this.numericUpDownHttpProxyPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDownHttpProxyPort.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
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
			this.tabPageSpideringControl.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxThreads)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPageLimit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDepth)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.tabPageAnalysisOptions.ResumeLayout(false);
			this.tabPageAnalysisOptions.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.tabPageSeo.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxWords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMinWords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxLen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMinLen)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMaxWords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMinWords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMaxLen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMinLen)).EndInit();
			this.tabPageNetworkSettings.ResumeLayout(false);
			this.tabPageNetworkSettings.PerformLayout();
			this.flowLayoutPanel4.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHttpProxyPort)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
