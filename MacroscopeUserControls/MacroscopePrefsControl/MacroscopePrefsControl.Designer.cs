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
		private System.Windows.Forms.Label label14;
		public System.Windows.Forms.NumericUpDown numericUpDownMaxRetries;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label18;
		public System.Windows.Forms.NumericUpDown numericUpDownMaxHeadingDepth;
		public System.Windows.Forms.CheckBox checkBoxFollowSitemapLinks;
		private System.Windows.Forms.Label label15;
		public System.Windows.Forms.NumericUpDown numericUpDownRequestTimeout;
		public System.Windows.Forms.CheckBox checkBoxFetchXml;
		public System.Windows.Forms.CheckBox checkBoxFetchVideo;
		private System.Windows.Forms.GroupBox groupBox10;
		private System.Windows.Forms.GroupBox groupBox11;
		public System.Windows.Forms.CheckBox checkBoxScanSitesInList;
		private System.Windows.Forms.GroupBox groupBox12;
		public System.Windows.Forms.CheckBox checkBoxAnalyzeKeywordsInText;
		private System.Windows.Forms.GroupBox groupBox13;
		public System.Windows.Forms.CheckBox checkBoxWarnAboutInsecureLinks;
		public System.Windows.Forms.CheckBox checkBoxFetchAudio;
		private System.Windows.Forms.Label label16;
		public System.Windows.Forms.NumericUpDown numericUpDownTitleMaxPixelWidth;
		private System.Windows.Forms.GroupBox groupBox14;
		private System.Windows.Forms.Label labelMaxLevenshteinSizeDifference;
		public System.Windows.Forms.NumericUpDown numericUpDownMaxLevenshteinSizeDifference;
		private System.Windows.Forms.Label label22;
		public System.Windows.Forms.NumericUpDown numericUpDownMaxLevenshteinDistance;
		public System.Windows.Forms.CheckBox checkBoxEnableLevenshteinDeduplication;
		private System.Windows.Forms.Label label17;
		public System.Windows.Forms.NumericUpDown numericUpDownCrawlDelay;
		private System.Windows.Forms.TabPage tabPageDisplaySettings;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
		private System.Windows.Forms.GroupBox groupBox15;
		public System.Windows.Forms.CheckBox checkBoxPauseDisplayDuringScan;
		public System.Windows.Forms.CheckBox checkBoxShowProgressDialogues;

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
			this.checkBoxFollowRobotsProtocol = new System.Windows.Forms.CheckBox();
			this.checkBoxFollowSitemapLinks = new System.Windows.Forms.CheckBox();
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.checkBoxFollowNoFollow = new System.Windows.Forms.CheckBox();
			this.checkBoxFollowHrefLangLinks = new System.Windows.Forms.CheckBox();
			this.checkBoxFollowCanonicalLinks = new System.Windows.Forms.CheckBox();
			this.checkBoxFollowRedirects = new System.Windows.Forms.CheckBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.checkBoxCheckExternalLinks = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label17 = new System.Windows.Forms.Label();
			this.numericUpDownCrawlDelay = new System.Windows.Forms.NumericUpDown();
			this.label15 = new System.Windows.Forms.Label();
			this.numericUpDownRequestTimeout = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.numericUpDownMaxRetries = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.numericUpDownMaxThreads = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.numericUpDownPageLimit = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownDepth = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxFetchAudio = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchVideo = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchXml = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchBinaries = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchPdfs = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchImages = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchJavascripts = new System.Windows.Forms.CheckBox();
			this.checkBoxFetchStylesheets = new System.Windows.Forms.CheckBox();
			this.tabPageAnalysisOptions = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.checkBoxCheckHreflangs = new System.Windows.Forms.CheckBox();
			this.groupBox11 = new System.Windows.Forms.GroupBox();
			this.checkBoxScanSitesInList = new System.Windows.Forms.CheckBox();
			this.groupBox13 = new System.Windows.Forms.GroupBox();
			this.checkBoxWarnAboutInsecureLinks = new System.Windows.Forms.CheckBox();
			this.groupBox14 = new System.Windows.Forms.GroupBox();
			this.checkBoxEnableLevenshteinDeduplication = new System.Windows.Forms.CheckBox();
			this.labelMaxLevenshteinSizeDifference = new System.Windows.Forms.Label();
			this.numericUpDownMaxLevenshteinSizeDifference = new System.Windows.Forms.NumericUpDown();
			this.label22 = new System.Windows.Forms.Label();
			this.numericUpDownMaxLevenshteinDistance = new System.Windows.Forms.NumericUpDown();
			this.tabPageSeo = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label16 = new System.Windows.Forms.Label();
			this.numericUpDownTitleMaxPixelWidth = new System.Windows.Forms.NumericUpDown();
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
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.label18 = new System.Windows.Forms.Label();
			this.numericUpDownMaxHeadingDepth = new System.Windows.Forms.NumericUpDown();
			this.groupBox12 = new System.Windows.Forms.GroupBox();
			this.checkBoxAnalyzeKeywordsInText = new System.Windows.Forms.CheckBox();
			this.tabPageDisplaySettings = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox15 = new System.Windows.Forms.GroupBox();
			this.checkBoxPauseDisplayDuringScan = new System.Windows.Forms.CheckBox();
			this.tabPageNetworkSettings = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.textBoxHttpProxyHost = new System.Windows.Forms.TextBox();
			this.numericUpDownHttpProxyPort = new System.Windows.Forms.NumericUpDown();
			this.checkBoxShowProgressDialogues = new System.Windows.Forms.CheckBox();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			this.tabControlPreferences.SuspendLayout();
			this.tabPageSpideringControl.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCrawlDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRequestTimeout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxRetries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxThreads)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPageLimit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDepth)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.tabPageAnalysisOptions.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox11.SuspendLayout();
			this.groupBox13.SuspendLayout();
			this.groupBox14.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLevenshteinSizeDifference)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLevenshteinDistance)).BeginInit();
			this.tabPageSeo.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxPixelWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxWords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMinWords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxLen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMinLen)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMaxWords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMinWords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMaxLen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMinLen)).BeginInit();
			this.groupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxHeadingDepth)).BeginInit();
			this.groupBox12.SuspendLayout();
			this.tabPageDisplaySettings.SuspendLayout();
			this.flowLayoutPanel5.SuspendLayout();
			this.groupBox15.SuspendLayout();
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
			label10.TabIndex = 1;
			label10.Text = "HTTP Proxy Port";
			// 
			// tabControlPreferences
			// 
			this.tabControlPreferences.Controls.Add(this.tabPageSpideringControl);
			this.tabControlPreferences.Controls.Add(this.tabPageAnalysisOptions);
			this.tabControlPreferences.Controls.Add(this.tabPageSeo);
			this.tabControlPreferences.Controls.Add(this.tabPageDisplaySettings);
			this.tabControlPreferences.Controls.Add(this.tabPageNetworkSettings);
			this.tabControlPreferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlPreferences.Location = new System.Drawing.Point(0, 0);
			this.tabControlPreferences.Name = "tabControlPreferences";
			this.tabControlPreferences.SelectedIndex = 0;
			this.tabControlPreferences.Size = new System.Drawing.Size(600, 787);
			this.tabControlPreferences.TabIndex = 0;
			// 
			// tabPageSpideringControl
			// 
			this.tabPageSpideringControl.AutoScroll = true;
			this.tabPageSpideringControl.Controls.Add(this.flowLayoutPanel1);
			this.tabPageSpideringControl.Location = new System.Drawing.Point(4, 22);
			this.tabPageSpideringControl.Name = "tabPageSpideringControl";
			this.tabPageSpideringControl.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSpideringControl.Size = new System.Drawing.Size(592, 761);
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
			this.flowLayoutPanel1.Controls.Add(this.groupBox10);
			this.flowLayoutPanel1.Controls.Add(this.groupBox7);
			this.flowLayoutPanel1.Controls.Add(this.groupBox4);
			this.flowLayoutPanel1.Controls.Add(this.groupBox1);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(586, 755);
			this.flowLayoutPanel1.TabIndex = 6;
			// 
			// groupBox5
			// 
			this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox5.Controls.Add(this.checkBoxFollowRobotsProtocol);
			this.groupBox5.Controls.Add(this.checkBoxFollowSitemapLinks);
			this.groupBox5.Location = new System.Drawing.Point(10, 10);
			this.groupBox5.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(500, 60);
			this.groupBox5.TabIndex = 1;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Standards";
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
			// checkBoxFollowSitemapLinks
			// 
			this.checkBoxFollowSitemapLinks.Location = new System.Drawing.Point(180, 20);
			this.checkBoxFollowSitemapLinks.Name = "checkBoxFollowSitemapLinks";
			this.checkBoxFollowSitemapLinks.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFollowSitemapLinks.TabIndex = 2;
			this.checkBoxFollowSitemapLinks.Text = "Follow sitemap links";
			this.checkBoxFollowSitemapLinks.UseVisualStyleBackColor = true;
			// 
			// groupBox10
			// 
			this.groupBox10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox10.Controls.Add(this.checkBoxFollowNoFollow);
			this.groupBox10.Controls.Add(this.checkBoxFollowHrefLangLinks);
			this.groupBox10.Controls.Add(this.checkBoxFollowCanonicalLinks);
			this.groupBox10.Controls.Add(this.checkBoxFollowRedirects);
			this.groupBox10.Location = new System.Drawing.Point(10, 80);
			this.groupBox10.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(500, 90);
			this.groupBox10.TabIndex = 2;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Links";
			// 
			// checkBoxFollowNoFollow
			// 
			this.checkBoxFollowNoFollow.Location = new System.Drawing.Point(180, 50);
			this.checkBoxFollowNoFollow.Name = "checkBoxFollowNoFollow";
			this.checkBoxFollowNoFollow.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFollowNoFollow.TabIndex = 4;
			this.checkBoxFollowNoFollow.Text = "Follow rel=\"nofollow\" links";
			this.checkBoxFollowNoFollow.UseVisualStyleBackColor = true;
			// 
			// checkBoxFollowHrefLangLinks
			// 
			this.checkBoxFollowHrefLangLinks.Location = new System.Drawing.Point(180, 20);
			this.checkBoxFollowHrefLangLinks.Name = "checkBoxFollowHrefLangLinks";
			this.checkBoxFollowHrefLangLinks.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFollowHrefLangLinks.TabIndex = 3;
			this.checkBoxFollowHrefLangLinks.Text = "Follow HrefLang links";
			this.checkBoxFollowHrefLangLinks.UseVisualStyleBackColor = true;
			// 
			// checkBoxFollowCanonicalLinks
			// 
			this.checkBoxFollowCanonicalLinks.Location = new System.Drawing.Point(20, 50);
			this.checkBoxFollowCanonicalLinks.Name = "checkBoxFollowCanonicalLinks";
			this.checkBoxFollowCanonicalLinks.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFollowCanonicalLinks.TabIndex = 2;
			this.checkBoxFollowCanonicalLinks.Text = "Follow canonical links";
			this.checkBoxFollowCanonicalLinks.UseVisualStyleBackColor = true;
			// 
			// checkBoxFollowRedirects
			// 
			this.checkBoxFollowRedirects.Location = new System.Drawing.Point(20, 20);
			this.checkBoxFollowRedirects.Name = "checkBoxFollowRedirects";
			this.checkBoxFollowRedirects.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFollowRedirects.TabIndex = 1;
			this.checkBoxFollowRedirects.Text = "Follow redirects";
			this.checkBoxFollowRedirects.UseVisualStyleBackColor = true;
			// 
			// groupBox7
			// 
			this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox7.Controls.Add(this.checkBoxCheckExternalLinks);
			this.groupBox7.Location = new System.Drawing.Point(10, 180);
			this.groupBox7.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(500, 60);
			this.groupBox7.TabIndex = 3;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Domain Restrictions";
			// 
			// checkBoxCheckExternalLinks
			// 
			this.checkBoxCheckExternalLinks.Location = new System.Drawing.Point(20, 20);
			this.checkBoxCheckExternalLinks.Name = "checkBoxCheckExternalLinks";
			this.checkBoxCheckExternalLinks.Size = new System.Drawing.Size(150, 24);
			this.checkBoxCheckExternalLinks.TabIndex = 1;
			this.checkBoxCheckExternalLinks.Text = "Check external links";
			this.checkBoxCheckExternalLinks.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox4.Controls.Add(this.label17);
			this.groupBox4.Controls.Add(this.numericUpDownCrawlDelay);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.numericUpDownRequestTimeout);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.numericUpDownMaxRetries);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.numericUpDownMaxThreads);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.numericUpDownPageLimit);
			this.groupBox4.Controls.Add(this.numericUpDownDepth);
			this.groupBox4.Location = new System.Drawing.Point(10, 250);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(500, 120);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Spidering Limits";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(330, 20);
			this.label17.Margin = new System.Windows.Forms.Padding(0);
			this.label17.Name = "label17";
			this.label17.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label17.Size = new System.Drawing.Size(150, 20);
			this.label17.TabIndex = 14;
			this.label17.Text = "Crawl Delay (seconds)";
			// 
			// numericUpDownCrawlDelay
			// 
			this.numericUpDownCrawlDelay.CausesValidation = false;
			this.numericUpDownCrawlDelay.Location = new System.Drawing.Point(260, 20);
			this.numericUpDownCrawlDelay.Margin = new System.Windows.Forms.Padding(0);
			this.numericUpDownCrawlDelay.Maximum = new decimal(new int[] {
			60,
			0,
			0,
			0});
			this.numericUpDownCrawlDelay.Name = "numericUpDownCrawlDelay";
			this.numericUpDownCrawlDelay.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownCrawlDelay.TabIndex = 4;
			this.numericUpDownCrawlDelay.Value = new decimal(new int[] {
			30,
			0,
			0,
			0});
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(330, 50);
			this.label15.Margin = new System.Windows.Forms.Padding(0);
			this.label15.Name = "label15";
			this.label15.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label15.Size = new System.Drawing.Size(150, 20);
			this.label15.TabIndex = 12;
			this.label15.Text = "Request timeout (seconds)";
			// 
			// numericUpDownRequestTimeout
			// 
			this.numericUpDownRequestTimeout.CausesValidation = false;
			this.numericUpDownRequestTimeout.Location = new System.Drawing.Point(260, 50);
			this.numericUpDownRequestTimeout.Margin = new System.Windows.Forms.Padding(0);
			this.numericUpDownRequestTimeout.Maximum = new decimal(new int[] {
			50,
			0,
			0,
			0});
			this.numericUpDownRequestTimeout.Minimum = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.numericUpDownRequestTimeout.Name = "numericUpDownRequestTimeout";
			this.numericUpDownRequestTimeout.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownRequestTimeout.TabIndex = 5;
			this.numericUpDownRequestTimeout.Value = new decimal(new int[] {
			30,
			0,
			0,
			0});
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(330, 80);
			this.label14.Margin = new System.Windows.Forms.Padding(0);
			this.label14.Name = "label14";
			this.label14.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label14.Size = new System.Drawing.Size(150, 20);
			this.label14.TabIndex = 10;
			this.label14.Text = "Maximum fetch retries";
			// 
			// numericUpDownMaxRetries
			// 
			this.numericUpDownMaxRetries.CausesValidation = false;
			this.numericUpDownMaxRetries.Location = new System.Drawing.Point(260, 80);
			this.numericUpDownMaxRetries.Margin = new System.Windows.Forms.Padding(0);
			this.numericUpDownMaxRetries.Maximum = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.numericUpDownMaxRetries.Name = "numericUpDownMaxRetries";
			this.numericUpDownMaxRetries.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownMaxRetries.TabIndex = 6;
			this.numericUpDownMaxRetries.Value = new decimal(new int[] {
			10,
			0,
			0,
			0});
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(90, 20);
			this.label13.Margin = new System.Windows.Forms.Padding(0);
			this.label13.Name = "label13";
			this.label13.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label13.Size = new System.Drawing.Size(150, 20);
			this.label13.TabIndex = 9;
			this.label13.Text = "Maximum worker threads";
			// 
			// numericUpDownMaxThreads
			// 
			this.numericUpDownMaxThreads.Location = new System.Drawing.Point(20, 20);
			this.numericUpDownMaxThreads.Margin = new System.Windows.Forms.Padding(0);
			this.numericUpDownMaxThreads.Maximum = new decimal(new int[] {
			64,
			0,
			0,
			0});
			this.numericUpDownMaxThreads.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownMaxThreads.Name = "numericUpDownMaxThreads";
			this.numericUpDownMaxThreads.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownMaxThreads.TabIndex = 1;
			this.numericUpDownMaxThreads.Value = new decimal(new int[] {
			4,
			0,
			0,
			0});
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(90, 80);
			this.label11.Margin = new System.Windows.Forms.Padding(0);
			this.label11.Name = "label11";
			this.label11.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label11.Size = new System.Drawing.Size(150, 20);
			this.label11.TabIndex = 5;
			this.label11.Text = "Maximum pages to fetch";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(90, 50);
			this.label12.Margin = new System.Windows.Forms.Padding(0);
			this.label12.Name = "label12";
			this.label12.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label12.Size = new System.Drawing.Size(150, 20);
			this.label12.TabIndex = 4;
			this.label12.Text = "Maximum page depth";
			// 
			// numericUpDownPageLimit
			// 
			this.numericUpDownPageLimit.Location = new System.Drawing.Point(20, 80);
			this.numericUpDownPageLimit.Margin = new System.Windows.Forms.Padding(0);
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
			this.numericUpDownPageLimit.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownPageLimit.TabIndex = 3;
			this.numericUpDownPageLimit.Value = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			// 
			// numericUpDownDepth
			// 
			this.numericUpDownDepth.Location = new System.Drawing.Point(20, 50);
			this.numericUpDownDepth.Margin = new System.Windows.Forms.Padding(0);
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
			this.numericUpDownDepth.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownDepth.TabIndex = 2;
			this.numericUpDownDepth.Value = new decimal(new int[] {
			100,
			0,
			0,
			0});
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox1.Controls.Add(this.checkBoxFetchAudio);
			this.groupBox1.Controls.Add(this.checkBoxFetchVideo);
			this.groupBox1.Controls.Add(this.checkBoxFetchXml);
			this.groupBox1.Controls.Add(this.checkBoxFetchBinaries);
			this.groupBox1.Controls.Add(this.checkBoxFetchPdfs);
			this.groupBox1.Controls.Add(this.checkBoxFetchImages);
			this.groupBox1.Controls.Add(this.checkBoxFetchJavascripts);
			this.groupBox1.Controls.Add(this.checkBoxFetchStylesheets);
			this.groupBox1.Location = new System.Drawing.Point(10, 380);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(500, 120);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Fetch Document Types";
			// 
			// checkBoxFetchAudio
			// 
			this.checkBoxFetchAudio.Location = new System.Drawing.Point(180, 50);
			this.checkBoxFetchAudio.Name = "checkBoxFetchAudio";
			this.checkBoxFetchAudio.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFetchAudio.TabIndex = 5;
			this.checkBoxFetchAudio.Text = "Audo files";
			this.checkBoxFetchAudio.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchVideo
			// 
			this.checkBoxFetchVideo.Location = new System.Drawing.Point(180, 80);
			this.checkBoxFetchVideo.Name = "checkBoxFetchVideo";
			this.checkBoxFetchVideo.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFetchVideo.TabIndex = 6;
			this.checkBoxFetchVideo.Text = "Video files";
			this.checkBoxFetchVideo.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchXml
			// 
			this.checkBoxFetchXml.Location = new System.Drawing.Point(336, 20);
			this.checkBoxFetchXml.Name = "checkBoxFetchXml";
			this.checkBoxFetchXml.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFetchXml.TabIndex = 7;
			this.checkBoxFetchXml.Text = "XML files";
			this.checkBoxFetchXml.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchBinaries
			// 
			this.checkBoxFetchBinaries.Location = new System.Drawing.Point(336, 50);
			this.checkBoxFetchBinaries.Name = "checkBoxFetchBinaries";
			this.checkBoxFetchBinaries.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFetchBinaries.TabIndex = 8;
			this.checkBoxFetchBinaries.Text = "Binary files";
			this.checkBoxFetchBinaries.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchPdfs
			// 
			this.checkBoxFetchPdfs.Location = new System.Drawing.Point(180, 20);
			this.checkBoxFetchPdfs.Name = "checkBoxFetchPdfs";
			this.checkBoxFetchPdfs.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFetchPdfs.TabIndex = 4;
			this.checkBoxFetchPdfs.Text = "PDFs";
			this.checkBoxFetchPdfs.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchImages
			// 
			this.checkBoxFetchImages.Location = new System.Drawing.Point(20, 80);
			this.checkBoxFetchImages.Name = "checkBoxFetchImages";
			this.checkBoxFetchImages.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFetchImages.TabIndex = 3;
			this.checkBoxFetchImages.Text = "Images";
			this.checkBoxFetchImages.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchJavascripts
			// 
			this.checkBoxFetchJavascripts.Location = new System.Drawing.Point(20, 50);
			this.checkBoxFetchJavascripts.Name = "checkBoxFetchJavascripts";
			this.checkBoxFetchJavascripts.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFetchJavascripts.TabIndex = 2;
			this.checkBoxFetchJavascripts.Text = "Javascripts";
			this.checkBoxFetchJavascripts.UseVisualStyleBackColor = true;
			// 
			// checkBoxFetchStylesheets
			// 
			this.checkBoxFetchStylesheets.Location = new System.Drawing.Point(20, 20);
			this.checkBoxFetchStylesheets.Name = "checkBoxFetchStylesheets";
			this.checkBoxFetchStylesheets.Size = new System.Drawing.Size(150, 24);
			this.checkBoxFetchStylesheets.TabIndex = 1;
			this.checkBoxFetchStylesheets.Text = "CSS stylesheets";
			this.checkBoxFetchStylesheets.UseVisualStyleBackColor = true;
			// 
			// tabPageAnalysisOptions
			// 
			this.tabPageAnalysisOptions.Controls.Add(this.flowLayoutPanel3);
			this.tabPageAnalysisOptions.Location = new System.Drawing.Point(4, 22);
			this.tabPageAnalysisOptions.Name = "tabPageAnalysisOptions";
			this.tabPageAnalysisOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageAnalysisOptions.Size = new System.Drawing.Size(592, 761);
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
			this.flowLayoutPanel3.Controls.Add(this.groupBox11);
			this.flowLayoutPanel3.Controls.Add(this.groupBox13);
			this.flowLayoutPanel3.Controls.Add(this.groupBox14);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(586, 755);
			this.flowLayoutPanel3.TabIndex = 7;
			// 
			// groupBox6
			// 
			this.groupBox6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox6.Controls.Add(this.checkBoxCheckHreflangs);
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
			// groupBox11
			// 
			this.groupBox11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox11.Controls.Add(this.checkBoxScanSitesInList);
			this.groupBox11.Location = new System.Drawing.Point(10, 80);
			this.groupBox11.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.groupBox11.Name = "groupBox11";
			this.groupBox11.Size = new System.Drawing.Size(500, 60);
			this.groupBox11.TabIndex = 2;
			this.groupBox11.TabStop = false;
			this.groupBox11.Text = "List File Processing";
			// 
			// checkBoxScanSitesInList
			// 
			this.checkBoxScanSitesInList.Location = new System.Drawing.Point(20, 20);
			this.checkBoxScanSitesInList.Name = "checkBoxScanSitesInList";
			this.checkBoxScanSitesInList.Size = new System.Drawing.Size(150, 24);
			this.checkBoxScanSitesInList.TabIndex = 1;
			this.checkBoxScanSitesInList.Text = "Scan sites in list";
			this.checkBoxScanSitesInList.UseVisualStyleBackColor = true;
			// 
			// groupBox13
			// 
			this.groupBox13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox13.Controls.Add(this.checkBoxWarnAboutInsecureLinks);
			this.groupBox13.Location = new System.Drawing.Point(10, 150);
			this.groupBox13.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.groupBox13.Name = "groupBox13";
			this.groupBox13.Size = new System.Drawing.Size(500, 60);
			this.groupBox13.TabIndex = 3;
			this.groupBox13.TabStop = false;
			this.groupBox13.Text = "Page Fault Analysis";
			// 
			// checkBoxWarnAboutInsecureLinks
			// 
			this.checkBoxWarnAboutInsecureLinks.Location = new System.Drawing.Point(20, 20);
			this.checkBoxWarnAboutInsecureLinks.Name = "checkBoxWarnAboutInsecureLinks";
			this.checkBoxWarnAboutInsecureLinks.Size = new System.Drawing.Size(150, 24);
			this.checkBoxWarnAboutInsecureLinks.TabIndex = 1;
			this.checkBoxWarnAboutInsecureLinks.Text = "Warn about insecure links";
			this.checkBoxWarnAboutInsecureLinks.UseVisualStyleBackColor = true;
			// 
			// groupBox14
			// 
			this.groupBox14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox14.Controls.Add(this.checkBoxEnableLevenshteinDeduplication);
			this.groupBox14.Controls.Add(this.labelMaxLevenshteinSizeDifference);
			this.groupBox14.Controls.Add(this.numericUpDownMaxLevenshteinSizeDifference);
			this.groupBox14.Controls.Add(this.label22);
			this.groupBox14.Controls.Add(this.numericUpDownMaxLevenshteinDistance);
			this.groupBox14.Location = new System.Drawing.Point(10, 220);
			this.groupBox14.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox14.Name = "groupBox14";
			this.groupBox14.Size = new System.Drawing.Size(500, 124);
			this.groupBox14.TabIndex = 4;
			this.groupBox14.TabStop = false;
			this.groupBox14.Text = "Levenshtein Edit Distance Processing";
			// 
			// checkBoxEnableLevenshteinDeduplication
			// 
			this.checkBoxEnableLevenshteinDeduplication.Location = new System.Drawing.Point(20, 20);
			this.checkBoxEnableLevenshteinDeduplication.Name = "checkBoxEnableLevenshteinDeduplication";
			this.checkBoxEnableLevenshteinDeduplication.Size = new System.Drawing.Size(376, 24);
			this.checkBoxEnableLevenshteinDeduplication.TabIndex = 1;
			this.checkBoxEnableLevenshteinDeduplication.Text = "Enable Levenshtein Duplicate Detection";
			this.checkBoxEnableLevenshteinDeduplication.UseVisualStyleBackColor = true;
			// 
			// labelMaxLevenshteinSizeDifference
			// 
			this.labelMaxLevenshteinSizeDifference.Location = new System.Drawing.Point(96, 50);
			this.labelMaxLevenshteinSizeDifference.Name = "labelMaxLevenshteinSizeDifference";
			this.labelMaxLevenshteinSizeDifference.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.labelMaxLevenshteinSizeDifference.Size = new System.Drawing.Size(250, 20);
			this.labelMaxLevenshteinSizeDifference.TabIndex = 9;
			this.labelMaxLevenshteinSizeDifference.Text = "Maximum Levenshtein Text Length Difference";
			// 
			// numericUpDownMaxLevenshteinSizeDifference
			// 
			this.numericUpDownMaxLevenshteinSizeDifference.Location = new System.Drawing.Point(20, 50);
			this.numericUpDownMaxLevenshteinSizeDifference.Maximum = new decimal(new int[] {
			512,
			0,
			0,
			0});
			this.numericUpDownMaxLevenshteinSizeDifference.Name = "numericUpDownMaxLevenshteinSizeDifference";
			this.numericUpDownMaxLevenshteinSizeDifference.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownMaxLevenshteinSizeDifference.TabIndex = 2;
			this.numericUpDownMaxLevenshteinSizeDifference.Value = new decimal(new int[] {
			64,
			0,
			0,
			0});
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(96, 80);
			this.label22.Name = "label22";
			this.label22.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label22.Size = new System.Drawing.Size(250, 20);
			this.label22.TabIndex = 4;
			this.label22.Text = "Levenshtein Edit Distance Threshold";
			// 
			// numericUpDownMaxLevenshteinDistance
			// 
			this.numericUpDownMaxLevenshteinDistance.Location = new System.Drawing.Point(20, 80);
			this.numericUpDownMaxLevenshteinDistance.Maximum = new decimal(new int[] {
			512,
			0,
			0,
			0});
			this.numericUpDownMaxLevenshteinDistance.Name = "numericUpDownMaxLevenshteinDistance";
			this.numericUpDownMaxLevenshteinDistance.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownMaxLevenshteinDistance.TabIndex = 3;
			this.numericUpDownMaxLevenshteinDistance.Value = new decimal(new int[] {
			16,
			0,
			0,
			0});
			// 
			// tabPageSeo
			// 
			this.tabPageSeo.AutoScroll = true;
			this.tabPageSeo.Controls.Add(this.flowLayoutPanel2);
			this.tabPageSeo.Location = new System.Drawing.Point(4, 22);
			this.tabPageSeo.Name = "tabPageSeo";
			this.tabPageSeo.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSeo.Size = new System.Drawing.Size(592, 761);
			this.tabPageSeo.TabIndex = 3;
			this.tabPageSeo.Text = "SEO Options";
			this.tabPageSeo.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.groupBox2);
			this.flowLayoutPanel2.Controls.Add(this.groupBox3);
			this.flowLayoutPanel2.Controls.Add(this.groupBox9);
			this.flowLayoutPanel2.Controls.Add(this.groupBox12);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(586, 755);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// groupBox2
			// 
			this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.numericUpDownTitleMaxPixelWidth);
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
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(323, 28);
			this.label16.Margin = new System.Windows.Forms.Padding(0);
			this.label16.Name = "label16";
			this.label16.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label16.Size = new System.Drawing.Size(160, 20);
			this.label16.TabIndex = 9;
			this.label16.Text = "Minimum title pixel width";
			// 
			// numericUpDownTitleMaxPixelWidth
			// 
			this.numericUpDownTitleMaxPixelWidth.Location = new System.Drawing.Point(250, 28);
			this.numericUpDownTitleMaxPixelWidth.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.numericUpDownTitleMaxPixelWidth.Minimum = new decimal(new int[] {
			100,
			0,
			0,
			0});
			this.numericUpDownTitleMaxPixelWidth.Name = "numericUpDownTitleMaxPixelWidth";
			this.numericUpDownTitleMaxPixelWidth.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownTitleMaxPixelWidth.TabIndex = 5;
			this.numericUpDownTitleMaxPixelWidth.Value = new decimal(new int[] {
			100,
			0,
			0,
			0});
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(93, 120);
			this.label4.Margin = new System.Windows.Forms.Padding(0);
			this.label4.Name = "label4";
			this.label4.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label4.Size = new System.Drawing.Size(120, 20);
			this.label4.TabIndex = 7;
			this.label4.Text = "Maximum title words";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(93, 90);
			this.label3.Margin = new System.Windows.Forms.Padding(0);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label3.Size = new System.Drawing.Size(120, 20);
			this.label3.TabIndex = 6;
			this.label3.Text = "Minimum title words";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(93, 60);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label2.Size = new System.Drawing.Size(120, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Maximum title length";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(93, 30);
			this.label1.Margin = new System.Windows.Forms.Padding(0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label1.Size = new System.Drawing.Size(120, 20);
			this.label1.TabIndex = 4;
			this.label1.Text = "Minimum title length";
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
			this.numericUpDownTitleMaxWords.Size = new System.Drawing.Size(70, 20);
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
			this.numericUpDownTitleMinWords.Size = new System.Drawing.Size(70, 20);
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
			this.numericUpDownTitleMaxLen.Size = new System.Drawing.Size(70, 20);
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
			this.numericUpDownTitleMinLen.Size = new System.Drawing.Size(70, 20);
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
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Page Description Policies";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(93, 120);
			this.label5.Margin = new System.Windows.Forms.Padding(0);
			this.label5.Name = "label5";
			this.label5.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label5.Size = new System.Drawing.Size(200, 20);
			this.label5.TabIndex = 15;
			this.label5.Text = "Maximum description words";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(93, 90);
			this.label6.Margin = new System.Windows.Forms.Padding(0);
			this.label6.Name = "label6";
			this.label6.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label6.Size = new System.Drawing.Size(200, 20);
			this.label6.TabIndex = 14;
			this.label6.Text = "Minimum description words";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(93, 60);
			this.label7.Margin = new System.Windows.Forms.Padding(0);
			this.label7.Name = "label7";
			this.label7.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label7.Size = new System.Drawing.Size(200, 20);
			this.label7.TabIndex = 13;
			this.label7.Text = "Maximum description length";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(93, 30);
			this.label8.Margin = new System.Windows.Forms.Padding(0);
			this.label8.Name = "label8";
			this.label8.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label8.Size = new System.Drawing.Size(200, 20);
			this.label8.TabIndex = 12;
			this.label8.Text = "Minimum description length";
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
			this.numericUpDownDescriptionMaxWords.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownDescriptionMaxWords.TabIndex = 4;
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
			this.numericUpDownDescriptionMinWords.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownDescriptionMinWords.TabIndex = 3;
			this.numericUpDownDescriptionMinWords.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// numericUpDownDescriptionMaxLen
			// 
			this.numericUpDownDescriptionMaxLen.Location = new System.Drawing.Point(20, 60);
			this.numericUpDownDescriptionMaxLen.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.numericUpDownDescriptionMaxLen.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownDescriptionMaxLen.Name = "numericUpDownDescriptionMaxLen";
			this.numericUpDownDescriptionMaxLen.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownDescriptionMaxLen.TabIndex = 2;
			this.numericUpDownDescriptionMaxLen.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// numericUpDownDescriptionMinLen
			// 
			this.numericUpDownDescriptionMinLen.Location = new System.Drawing.Point(20, 30);
			this.numericUpDownDescriptionMinLen.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.numericUpDownDescriptionMinLen.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownDescriptionMinLen.Name = "numericUpDownDescriptionMinLen";
			this.numericUpDownDescriptionMinLen.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownDescriptionMinLen.TabIndex = 1;
			this.numericUpDownDescriptionMinLen.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.label18);
			this.groupBox9.Controls.Add(this.numericUpDownMaxHeadingDepth);
			this.groupBox9.Location = new System.Drawing.Point(10, 350);
			this.groupBox9.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(500, 70);
			this.groupBox9.TabIndex = 3;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Page Header Element Policies";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(93, 30);
			this.label18.Margin = new System.Windows.Forms.Padding(0);
			this.label18.Name = "label18";
			this.label18.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
			this.label18.Size = new System.Drawing.Size(200, 20);
			this.label18.TabIndex = 12;
			this.label18.Text = "Analyze heading elements cutoff";
			// 
			// numericUpDownMaxHeadingDepth
			// 
			this.numericUpDownMaxHeadingDepth.Location = new System.Drawing.Point(20, 30);
			this.numericUpDownMaxHeadingDepth.Maximum = new decimal(new int[] {
			6,
			0,
			0,
			0});
			this.numericUpDownMaxHeadingDepth.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownMaxHeadingDepth.Name = "numericUpDownMaxHeadingDepth";
			this.numericUpDownMaxHeadingDepth.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownMaxHeadingDepth.TabIndex = 1;
			this.numericUpDownMaxHeadingDepth.Value = new decimal(new int[] {
			2,
			0,
			0,
			0});
			// 
			// groupBox12
			// 
			this.groupBox12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox12.Controls.Add(this.checkBoxAnalyzeKeywordsInText);
			this.groupBox12.Location = new System.Drawing.Point(10, 430);
			this.groupBox12.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.groupBox12.Name = "groupBox12";
			this.groupBox12.Size = new System.Drawing.Size(500, 60);
			this.groupBox12.TabIndex = 4;
			this.groupBox12.TabStop = false;
			this.groupBox12.Text = "Keywords";
			// 
			// checkBoxAnalyzeKeywordsInText
			// 
			this.checkBoxAnalyzeKeywordsInText.Location = new System.Drawing.Point(20, 20);
			this.checkBoxAnalyzeKeywordsInText.Name = "checkBoxAnalyzeKeywordsInText";
			this.checkBoxAnalyzeKeywordsInText.Size = new System.Drawing.Size(150, 24);
			this.checkBoxAnalyzeKeywordsInText.TabIndex = 1;
			this.checkBoxAnalyzeKeywordsInText.Text = "Analyze keywords in text";
			this.checkBoxAnalyzeKeywordsInText.UseVisualStyleBackColor = true;
			// 
			// tabPageDisplaySettings
			// 
			this.tabPageDisplaySettings.Controls.Add(this.flowLayoutPanel5);
			this.tabPageDisplaySettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageDisplaySettings.Name = "tabPageDisplaySettings";
			this.tabPageDisplaySettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDisplaySettings.Size = new System.Drawing.Size(592, 761);
			this.tabPageDisplaySettings.TabIndex = 4;
			this.tabPageDisplaySettings.Text = "Display Settings";
			this.tabPageDisplaySettings.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel5
			// 
			this.flowLayoutPanel5.Controls.Add(this.groupBox15);
			this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel5.Name = "flowLayoutPanel5";
			this.flowLayoutPanel5.Size = new System.Drawing.Size(586, 755);
			this.flowLayoutPanel5.TabIndex = 0;
			// 
			// groupBox15
			// 
			this.groupBox15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox15.Controls.Add(this.checkBoxShowProgressDialogues);
			this.groupBox15.Controls.Add(this.checkBoxPauseDisplayDuringScan);
			this.groupBox15.Location = new System.Drawing.Point(10, 10);
			this.groupBox15.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			this.groupBox15.Name = "groupBox15";
			this.groupBox15.Size = new System.Drawing.Size(500, 90);
			this.groupBox15.TabIndex = 3;
			this.groupBox15.TabStop = false;
			this.groupBox15.Text = "Overview Panels";
			// 
			// checkBoxPauseDisplayDuringScan
			// 
			this.checkBoxPauseDisplayDuringScan.Location = new System.Drawing.Point(20, 20);
			this.checkBoxPauseDisplayDuringScan.Name = "checkBoxPauseDisplayDuringScan";
			this.checkBoxPauseDisplayDuringScan.Size = new System.Drawing.Size(160, 24);
			this.checkBoxPauseDisplayDuringScan.TabIndex = 1;
			this.checkBoxPauseDisplayDuringScan.Text = "Pause display during scan";
			this.checkBoxPauseDisplayDuringScan.UseVisualStyleBackColor = true;
			// 
			// tabPageNetworkSettings
			// 
			this.tabPageNetworkSettings.Controls.Add(this.flowLayoutPanel4);
			this.tabPageNetworkSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageNetworkSettings.Name = "tabPageNetworkSettings";
			this.tabPageNetworkSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageNetworkSettings.Size = new System.Drawing.Size(592, 761);
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
			this.flowLayoutPanel4.Size = new System.Drawing.Size(586, 755);
			this.flowLayoutPanel4.TabIndex = 7;
			// 
			// groupBox8
			// 
			this.groupBox8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox8.Controls.Add(label9);
			this.groupBox8.Controls.Add(this.textBoxHttpProxyHost);
			this.groupBox8.Controls.Add(label10);
			this.groupBox8.Controls.Add(this.numericUpDownHttpProxyPort);
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
			this.numericUpDownHttpProxyPort.Location = new System.Drawing.Point(147, 50);
			this.numericUpDownHttpProxyPort.Maximum = new decimal(new int[] {
			65535,
			0,
			0,
			0});
			this.numericUpDownHttpProxyPort.Name = "numericUpDownHttpProxyPort";
			this.numericUpDownHttpProxyPort.Size = new System.Drawing.Size(70, 20);
			this.numericUpDownHttpProxyPort.TabIndex = 2;
			this.numericUpDownHttpProxyPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDownHttpProxyPort.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// checkBoxShowProgressDialogues
			// 
			this.checkBoxShowProgressDialogues.Location = new System.Drawing.Point(20, 50);
			this.checkBoxShowProgressDialogues.Name = "checkBoxShowProgressDialogues";
			this.checkBoxShowProgressDialogues.Size = new System.Drawing.Size(160, 24);
			this.checkBoxShowProgressDialogues.TabIndex = 2;
			this.checkBoxShowProgressDialogues.Text = "Show progress dialogues";
			this.checkBoxShowProgressDialogues.UseVisualStyleBackColor = true;
			// 
			// MacroscopePrefsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControlPreferences);
			this.Name = "MacroscopePrefsControl";
			this.Size = new System.Drawing.Size(600, 787);
			this.tabControlPreferences.ResumeLayout(false);
			this.tabPageSpideringControl.ResumeLayout(false);
			this.tabPageSpideringControl.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox10.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCrawlDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRequestTimeout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxRetries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxThreads)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPageLimit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDepth)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.tabPageAnalysisOptions.ResumeLayout(false);
			this.tabPageAnalysisOptions.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox11.ResumeLayout(false);
			this.groupBox13.ResumeLayout(false);
			this.groupBox14.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLevenshteinSizeDifference)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLevenshteinDistance)).EndInit();
			this.tabPageSeo.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxPixelWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxWords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMinWords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMaxLen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTitleMinLen)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMaxWords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMinWords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMaxLen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDescriptionMinLen)).EndInit();
			this.groupBox9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxHeadingDepth)).EndInit();
			this.groupBox12.ResumeLayout(false);
			this.tabPageDisplaySettings.ResumeLayout(false);
			this.flowLayoutPanel5.ResumeLayout(false);
			this.groupBox15.ResumeLayout(false);
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
