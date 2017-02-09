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
	partial class MacroscopeMainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStripMain;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveOverviewExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generateHrefLangExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMainContainer;
		public System.Windows.Forms.ToolStripTextBox textBoxStartUrl;
		private System.Windows.Forms.StatusStrip statusStripMain;
		public System.Windows.Forms.ToolStripStatusLabel toolStripUrlCount;
		public System.Windows.Forms.ToolStripStatusLabel toolStripThreads;
		public System.Windows.Forms.ToolStripStatusLabel toolStripFound;
		private System.Windows.Forms.ToolStrip toolStripViewControls;
		private System.Windows.Forms.ToolStrip toolStripExecuteControls;
		private System.Windows.Forms.ToolStripLabel toolStripLabelStartUrl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton ButtonStart;
		private System.Windows.Forms.ToolStripButton ButtonStop;
		private System.Windows.Forms.ToolStripButton ButtonReset;
		private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutSEOMacroscopeToolStripMenuItem;
		public System.Windows.Forms.ToolStripButton toolStripButtonRetryBrokenLinks;
		private System.Windows.Forms.SplitContainer splitContainerLeftAndRightViews;
		public System.Windows.Forms.SplitContainer splitContainerStructureAndDocumentDetails;
		public SEOMacroscope.MacroscopeOverviewPanel macroscopeOverviewTabPanelInstance;
		private SEOMacroscope.MacroscopeDocumentDetails macroscopeDocumentDetailsInstance;
		private SEOMacroscope.MacroscopeSiteStructurePanel macroscopeSiteStructurePanelInstance;
		private System.Windows.Forms.ToolStripMenuItem loadURLListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadFromTextFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteFromClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sEOMacroscopeWebsiteToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem taskParametersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem includeURLPatternsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem excludeURLPatternsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem crawlParentDirectoriesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem crawlChildDirectoriesToolStripMenuItem;
		public System.Windows.Forms.ToolStripProgressBar ProgressBarScan;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MacroscopeMainForm));
			this.menuStripMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadURLListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadFromTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.taskParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.includeURLPatternsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.excludeURLPatternsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.crawlParentDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.crawlChildDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveOverviewExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateHrefLangExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutSEOMacroscopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sEOMacroscopeWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanelMainContainer = new System.Windows.Forms.TableLayoutPanel();
			this.statusStripMain = new System.Windows.Forms.StatusStrip();
			this.toolStripThreads = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripUrlCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripFound = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripViewControls = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonRetryBrokenLinks = new System.Windows.Forms.ToolStripButton();
			this.toolStripExecuteControls = new System.Windows.Forms.ToolStrip();
			this.toolStripLabelStartUrl = new System.Windows.Forms.ToolStripLabel();
			this.textBoxStartUrl = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ButtonStart = new System.Windows.Forms.ToolStripButton();
			this.ButtonStop = new System.Windows.Forms.ToolStripButton();
			this.ButtonReset = new System.Windows.Forms.ToolStripButton();
			this.ProgressBarScan = new System.Windows.Forms.ToolStripProgressBar();
			this.splitContainerLeftAndRightViews = new System.Windows.Forms.SplitContainer();
			this.splitContainerStructureAndDocumentDetails = new System.Windows.Forms.SplitContainer();
			this.macroscopeOverviewTabPanelInstance = new SEOMacroscope.MacroscopeOverviewPanel();
			this.macroscopeDocumentDetailsInstance = new SEOMacroscope.MacroscopeDocumentDetails();
			this.macroscopeSiteStructurePanelInstance = new SEOMacroscope.MacroscopeSiteStructurePanel();
			this.menuStripMain.SuspendLayout();
			this.tableLayoutPanelMainContainer.SuspendLayout();
			this.statusStripMain.SuspendLayout();
			this.toolStripViewControls.SuspendLayout();
			this.toolStripExecuteControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftAndRightViews)).BeginInit();
			this.splitContainerLeftAndRightViews.Panel1.SuspendLayout();
			this.splitContainerLeftAndRightViews.Panel2.SuspendLayout();
			this.splitContainerLeftAndRightViews.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerStructureAndDocumentDetails)).BeginInit();
			this.splitContainerStructureAndDocumentDetails.Panel1.SuspendLayout();
			this.splitContainerStructureAndDocumentDetails.Panel2.SuspendLayout();
			this.splitContainerStructureAndDocumentDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStripMain
			// 
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.editToolStripMenuItem,
			this.taskParametersToolStripMenuItem,
			this.reportsToolStripMenuItem,
			this.helpToolStripMenuItem});
			this.menuStripMain.Location = new System.Drawing.Point(0, 0);
			this.menuStripMain.Name = "menuStripMain";
			this.menuStripMain.Size = new System.Drawing.Size(784, 24);
			this.menuStripMain.TabIndex = 1;
			this.menuStripMain.Text = "menuStripMain";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.loadURLListToolStripMenuItem,
			this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// loadURLListToolStripMenuItem
			// 
			this.loadURLListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.loadFromTextFileToolStripMenuItem,
			this.pasteFromClipboardToolStripMenuItem});
			this.loadURLListToolStripMenuItem.Name = "loadURLListToolStripMenuItem";
			this.loadURLListToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.loadURLListToolStripMenuItem.Text = "Load URL List";
			// 
			// loadFromTextFileToolStripMenuItem
			// 
			this.loadFromTextFileToolStripMenuItem.Name = "loadFromTextFileToolStripMenuItem";
			this.loadFromTextFileToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.loadFromTextFileToolStripMenuItem.Text = "Load from Text File";
			this.loadFromTextFileToolStripMenuItem.Click += new System.EventHandler(this.CallbackLoadUrlListTextFile);
			// 
			// pasteFromClipboardToolStripMenuItem
			// 
			this.pasteFromClipboardToolStripMenuItem.Name = "pasteFromClipboardToolStripMenuItem";
			this.pasteFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.pasteFromClipboardToolStripMenuItem.Text = "Paste from Clipboard";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.CallbackFileExit);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.preferencesToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// preferencesToolStripMenuItem
			// 
			this.preferencesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("preferencesToolStripMenuItem.Image")));
			this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
			this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.preferencesToolStripMenuItem.Text = "Preferences";
			this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.CallbackEditPreferencesClick);
			// 
			// taskParametersToolStripMenuItem
			// 
			this.taskParametersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.includeURLPatternsToolStripMenuItem,
			this.excludeURLPatternsToolStripMenuItem,
			this.crawlParentDirectoriesToolStripMenuItem,
			this.crawlChildDirectoriesToolStripMenuItem});
			this.taskParametersToolStripMenuItem.Name = "taskParametersToolStripMenuItem";
			this.taskParametersToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
			this.taskParametersToolStripMenuItem.Text = "Task Parameters";
			// 
			// includeURLPatternsToolStripMenuItem
			// 
			this.includeURLPatternsToolStripMenuItem.Name = "includeURLPatternsToolStripMenuItem";
			this.includeURLPatternsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.includeURLPatternsToolStripMenuItem.Text = "Include URL Patterns";
			this.includeURLPatternsToolStripMenuItem.Click += new System.EventHandler(this.CallbackIncludeUrlItemsClick);
			// 
			// excludeURLPatternsToolStripMenuItem
			// 
			this.excludeURLPatternsToolStripMenuItem.Name = "excludeURLPatternsToolStripMenuItem";
			this.excludeURLPatternsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.excludeURLPatternsToolStripMenuItem.Text = "Exclude URL Patterns";
			this.excludeURLPatternsToolStripMenuItem.Click += new System.EventHandler(this.CallbackExcludeUrlItemsClick);
			// 
			// crawlParentDirectoriesToolStripMenuItem
			// 
			this.crawlParentDirectoriesToolStripMenuItem.Checked = true;
			this.crawlParentDirectoriesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.crawlParentDirectoriesToolStripMenuItem.Name = "crawlParentDirectoriesToolStripMenuItem";
			this.crawlParentDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.crawlParentDirectoriesToolStripMenuItem.Text = "Crawl Parent Directories";
			// 
			// crawlChildDirectoriesToolStripMenuItem
			// 
			this.crawlChildDirectoriesToolStripMenuItem.Checked = true;
			this.crawlChildDirectoriesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.crawlChildDirectoriesToolStripMenuItem.Name = "crawlChildDirectoriesToolStripMenuItem";
			this.crawlChildDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			this.crawlChildDirectoriesToolStripMenuItem.Text = "Crawl Child Directories";
			// 
			// reportsToolStripMenuItem
			// 
			this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.saveOverviewExcelReportToolStripMenuItem,
			this.generateHrefLangExcelReportToolStripMenuItem});
			this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
			this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.reportsToolStripMenuItem.Text = "Reports";
			// 
			// saveOverviewExcelReportToolStripMenuItem
			// 
			this.saveOverviewExcelReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveOverviewExcelReportToolStripMenuItem.Image")));
			this.saveOverviewExcelReportToolStripMenuItem.Name = "saveOverviewExcelReportToolStripMenuItem";
			this.saveOverviewExcelReportToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.saveOverviewExcelReportToolStripMenuItem.Text = "Save Overview Excel Report";
			this.saveOverviewExcelReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveOverviewExcelReport);
			// 
			// generateHrefLangExcelReportToolStripMenuItem
			// 
			this.generateHrefLangExcelReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("generateHrefLangExcelReportToolStripMenuItem.Image")));
			this.generateHrefLangExcelReportToolStripMenuItem.Name = "generateHrefLangExcelReportToolStripMenuItem";
			this.generateHrefLangExcelReportToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.generateHrefLangExcelReportToolStripMenuItem.Text = "Save HrefLang Excel Report";
			this.generateHrefLangExcelReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveHrefLangExcelReport);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.aboutSEOMacroscopeToolStripMenuItem,
			this.sEOMacroscopeWebsiteToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutSEOMacroscopeToolStripMenuItem
			// 
			this.aboutSEOMacroscopeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutSEOMacroscopeToolStripMenuItem.Image")));
			this.aboutSEOMacroscopeToolStripMenuItem.Name = "aboutSEOMacroscopeToolStripMenuItem";
			this.aboutSEOMacroscopeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.aboutSEOMacroscopeToolStripMenuItem.Text = "About SEO Macroscope";
			this.aboutSEOMacroscopeToolStripMenuItem.Click += new System.EventHandler(this.CallbackHelpAboutClick);
			// 
			// sEOMacroscopeWebsiteToolStripMenuItem
			// 
			this.sEOMacroscopeWebsiteToolStripMenuItem.Name = "sEOMacroscopeWebsiteToolStripMenuItem";
			this.sEOMacroscopeWebsiteToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.sEOMacroscopeWebsiteToolStripMenuItem.Text = "SEO Macroscope Website";
			// 
			// tableLayoutPanelMainContainer
			// 
			this.tableLayoutPanelMainContainer.ColumnCount = 1;
			this.tableLayoutPanelMainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMainContainer.Controls.Add(this.statusStripMain, 0, 3);
			this.tableLayoutPanelMainContainer.Controls.Add(this.toolStripViewControls, 0, 1);
			this.tableLayoutPanelMainContainer.Controls.Add(this.toolStripExecuteControls, 0, 0);
			this.tableLayoutPanelMainContainer.Controls.Add(this.splitContainerLeftAndRightViews, 0, 2);
			this.tableLayoutPanelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelMainContainer.Location = new System.Drawing.Point(0, 24);
			this.tableLayoutPanelMainContainer.Name = "tableLayoutPanelMainContainer";
			this.tableLayoutPanelMainContainer.RowCount = 4;
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanelMainContainer.Size = new System.Drawing.Size(784, 538);
			this.tableLayoutPanelMainContainer.TabIndex = 2;
			// 
			// statusStripMain
			// 
			this.statusStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripThreads,
			this.toolStripUrlCount,
			this.toolStripFound});
			this.statusStripMain.Location = new System.Drawing.Point(0, 508);
			this.statusStripMain.Name = "statusStripMain";
			this.statusStripMain.Size = new System.Drawing.Size(784, 30);
			this.statusStripMain.TabIndex = 3;
			this.statusStripMain.Text = "statusStripMain";
			// 
			// toolStripThreads
			// 
			this.toolStripThreads.Name = "toolStripThreads";
			this.toolStripThreads.Size = new System.Drawing.Size(61, 25);
			this.toolStripThreads.Text = "Threads: 0";
			// 
			// toolStripUrlCount
			// 
			this.toolStripUrlCount.Name = "toolStripUrlCount";
			this.toolStripUrlCount.Size = new System.Drawing.Size(96, 25);
			this.toolStripUrlCount.Text = "URLs in Queue: 0";
			// 
			// toolStripFound
			// 
			this.toolStripFound.Name = "toolStripFound";
			this.toolStripFound.Size = new System.Drawing.Size(91, 25);
			this.toolStripFound.Text = "URLs Crawled: 0";
			// 
			// toolStripViewControls
			// 
			this.toolStripViewControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripViewControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripButtonRetryBrokenLinks});
			this.toolStripViewControls.Location = new System.Drawing.Point(0, 30);
			this.toolStripViewControls.Name = "toolStripViewControls";
			this.toolStripViewControls.Size = new System.Drawing.Size(784, 30);
			this.toolStripViewControls.TabIndex = 5;
			this.toolStripViewControls.Text = "toolStrip1";
			// 
			// toolStripButtonRetryBrokenLinks
			// 
			this.toolStripButtonRetryBrokenLinks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonRetryBrokenLinks.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRetryBrokenLinks.Image")));
			this.toolStripButtonRetryBrokenLinks.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRetryBrokenLinks.Name = "toolStripButtonRetryBrokenLinks";
			this.toolStripButtonRetryBrokenLinks.Size = new System.Drawing.Size(108, 27);
			this.toolStripButtonRetryBrokenLinks.Text = "Retry Broken Links";
			this.toolStripButtonRetryBrokenLinks.Click += new System.EventHandler(this.CallbackRetryBrokenLinksClick);
			// 
			// toolStripExecuteControls
			// 
			this.toolStripExecuteControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripExecuteControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripLabelStartUrl,
			this.textBoxStartUrl,
			this.toolStripSeparator1,
			this.ButtonStart,
			this.ButtonStop,
			this.ButtonReset,
			this.ProgressBarScan});
			this.toolStripExecuteControls.Location = new System.Drawing.Point(0, 0);
			this.toolStripExecuteControls.Name = "toolStripExecuteControls";
			this.toolStripExecuteControls.Size = new System.Drawing.Size(784, 30);
			this.toolStripExecuteControls.TabIndex = 6;
			this.toolStripExecuteControls.Text = "toolStrip1";
			// 
			// toolStripLabelStartUrl
			// 
			this.toolStripLabelStartUrl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripLabelStartUrl.Name = "toolStripLabelStartUrl";
			this.toolStripLabelStartUrl.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripLabelStartUrl.Size = new System.Drawing.Size(58, 27);
			this.toolStripLabelStartUrl.Text = "Start URL:";
			// 
			// textBoxStartUrl
			// 
			this.textBoxStartUrl.Name = "textBoxStartUrl";
			this.textBoxStartUrl.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.textBoxStartUrl.Size = new System.Drawing.Size(300, 30);
			this.textBoxStartUrl.ToolTipText = "Enter a URL to begin scanning from";
			this.textBoxStartUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CallbackStartUrlKeyUp);
			this.textBoxStartUrl.TextChanged += new System.EventHandler(this.CallbackStartUrlTextChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
			// 
			// ButtonStart
			// 
			this.ButtonStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("ButtonStart.Image")));
			this.ButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonStart.Name = "ButtonStart";
			this.ButtonStart.Size = new System.Drawing.Size(51, 27);
			this.ButtonStart.Text = "Start";
			this.ButtonStart.ToolTipText = "Start scanning";
			this.ButtonStart.Click += new System.EventHandler(this.CallbackScanStart);
			// 
			// ButtonStop
			// 
			this.ButtonStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("ButtonStop.Image")));
			this.ButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonStop.Name = "ButtonStop";
			this.ButtonStop.Size = new System.Drawing.Size(51, 27);
			this.ButtonStop.Text = "Stop";
			this.ButtonStop.ToolTipText = "Stop scanning";
			this.ButtonStop.Click += new System.EventHandler(this.CallbackScanStop);
			// 
			// ButtonReset
			// 
			this.ButtonReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ButtonReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ButtonReset.Image = ((System.Drawing.Image)(resources.GetObject("ButtonReset.Image")));
			this.ButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonReset.Name = "ButtonReset";
			this.ButtonReset.Size = new System.Drawing.Size(39, 27);
			this.ButtonReset.Text = "Reset";
			this.ButtonReset.ToolTipText = "Reset all scan results";
			this.ButtonReset.Click += new System.EventHandler(this.CallbackScanReset);
			// 
			// ProgressBarScan
			// 
			this.ProgressBarScan.CausesValidation = false;
			this.ProgressBarScan.Margin = new System.Windows.Forms.Padding(10, 8, 10, 8);
			this.ProgressBarScan.Name = "ProgressBarScan";
			this.ProgressBarScan.Size = new System.Drawing.Size(150, 14);
			this.ProgressBarScan.Step = 1;
			// 
			// splitContainerLeftAndRightViews
			// 
			this.splitContainerLeftAndRightViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerLeftAndRightViews.Location = new System.Drawing.Point(3, 63);
			this.splitContainerLeftAndRightViews.Name = "splitContainerLeftAndRightViews";
			// 
			// splitContainerLeftAndRightViews.Panel1
			// 
			this.splitContainerLeftAndRightViews.Panel1.Controls.Add(this.splitContainerStructureAndDocumentDetails);
			this.splitContainerLeftAndRightViews.Panel1MinSize = 75;
			// 
			// splitContainerLeftAndRightViews.Panel2
			// 
			this.splitContainerLeftAndRightViews.Panel2.Controls.Add(this.macroscopeSiteStructurePanelInstance);
			this.splitContainerLeftAndRightViews.Size = new System.Drawing.Size(778, 442);
			this.splitContainerLeftAndRightViews.SplitterDistance = 533;
			this.splitContainerLeftAndRightViews.SplitterWidth = 6;
			this.splitContainerLeftAndRightViews.TabIndex = 7;
			this.splitContainerLeftAndRightViews.TabStop = false;
			// 
			// splitContainerStructureAndDocumentDetails
			// 
			this.splitContainerStructureAndDocumentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerStructureAndDocumentDetails.Location = new System.Drawing.Point(0, 0);
			this.splitContainerStructureAndDocumentDetails.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainerStructureAndDocumentDetails.Name = "splitContainerStructureAndDocumentDetails";
			this.splitContainerStructureAndDocumentDetails.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerStructureAndDocumentDetails.Panel1
			// 
			this.splitContainerStructureAndDocumentDetails.Panel1.Controls.Add(this.macroscopeOverviewTabPanelInstance);
			// 
			// splitContainerStructureAndDocumentDetails.Panel2
			// 
			this.splitContainerStructureAndDocumentDetails.Panel2.Controls.Add(this.macroscopeDocumentDetailsInstance);
			this.splitContainerStructureAndDocumentDetails.Size = new System.Drawing.Size(533, 442);
			this.splitContainerStructureAndDocumentDetails.SplitterDistance = 218;
			this.splitContainerStructureAndDocumentDetails.SplitterWidth = 6;
			this.splitContainerStructureAndDocumentDetails.TabIndex = 0;
			// 
			// macroscopeOverviewTabPanelInstance
			// 
			this.macroscopeOverviewTabPanelInstance.Location = new System.Drawing.Point(10, 10);
			this.macroscopeOverviewTabPanelInstance.Name = "macroscopeOverviewTabPanelInstance";
			this.macroscopeOverviewTabPanelInstance.Size = new System.Drawing.Size(400, 200);
			this.macroscopeOverviewTabPanelInstance.TabIndex = 0;
			// 
			// macroscopeDocumentDetailsInstance
			// 
			this.macroscopeDocumentDetailsInstance.Location = new System.Drawing.Point(10, 10);
			this.macroscopeDocumentDetailsInstance.Name = "macroscopeDocumentDetailsInstance";
			this.macroscopeDocumentDetailsInstance.Size = new System.Drawing.Size(400, 200);
			this.macroscopeDocumentDetailsInstance.TabIndex = 0;
			// 
			// macroscopeSiteStructurePanelInstance
			// 
			this.macroscopeSiteStructurePanelInstance.Location = new System.Drawing.Point(10, 10);
			this.macroscopeSiteStructurePanelInstance.Name = "macroscopeSiteStructurePanelInstance";
			this.macroscopeSiteStructurePanelInstance.Size = new System.Drawing.Size(200, 200);
			this.macroscopeSiteStructurePanelInstance.TabIndex = 0;
			// 
			// MacroscopeMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.tableLayoutPanelMainContainer);
			this.Controls.Add(this.menuStripMain);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.menuStripMain;
			this.MinimumSize = new System.Drawing.Size(800, 400);
			this.Name = "MacroscopeMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MacroscopeMainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CallbackFormClosing);
			this.menuStripMain.ResumeLayout(false);
			this.menuStripMain.PerformLayout();
			this.tableLayoutPanelMainContainer.ResumeLayout(false);
			this.tableLayoutPanelMainContainer.PerformLayout();
			this.statusStripMain.ResumeLayout(false);
			this.statusStripMain.PerformLayout();
			this.toolStripViewControls.ResumeLayout(false);
			this.toolStripViewControls.PerformLayout();
			this.toolStripExecuteControls.ResumeLayout(false);
			this.toolStripExecuteControls.PerformLayout();
			this.splitContainerLeftAndRightViews.Panel1.ResumeLayout(false);
			this.splitContainerLeftAndRightViews.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftAndRightViews)).EndInit();
			this.splitContainerLeftAndRightViews.ResumeLayout(false);
			this.splitContainerStructureAndDocumentDetails.Panel1.ResumeLayout(false);
			this.splitContainerStructureAndDocumentDetails.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerStructureAndDocumentDetails)).EndInit();
			this.splitContainerStructureAndDocumentDetails.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		}
}
