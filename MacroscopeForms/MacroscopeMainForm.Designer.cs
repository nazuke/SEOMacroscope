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
		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabPageStructureOverview;
		private System.Windows.Forms.TableLayoutPanel tableLayoutStructure;
		private System.Windows.Forms.TabPage tabPageHrefLangAnalysis;
		public System.Windows.Forms.ToolStripTextBox textBoxStartUrl;
		private System.Windows.Forms.TabPage tabPageRedirectsAudit;
		private System.Windows.Forms.TabPage tabPageEmailAddresses;
		private System.Windows.Forms.TabPage tabPageTelephoneNumbers;
		private System.Windows.Forms.StatusStrip statusStripMain;
		public System.Windows.Forms.ToolStripStatusLabel toolStripUrlCount;
		public System.Windows.Forms.ToolStripStatusLabel toolStripThreads;
		public System.Windows.Forms.ToolStripStatusLabel toolStripFound;
		private System.Windows.Forms.TabPage tabPageHistory;
		private System.Windows.Forms.ListView listViewHistory;
		private System.Windows.Forms.ColumnHeader HistoryUrl;
		private System.Windows.Forms.ColumnHeader HistoryVisited;
		private System.Windows.Forms.ListView listViewTelephoneNumbers;
		private System.Windows.Forms.ColumnHeader TelTel;
		private System.Windows.Forms.ColumnHeader TelUrl;
		private System.Windows.Forms.ListView listViewEmailAddresses;
		private System.Windows.Forms.ColumnHeader EmailAddressesEmail;
		private System.Windows.Forms.ColumnHeader EmailAddressesUrl;
		private System.Windows.Forms.ListView listViewStructure;
		private SEOMacroscope.MacroscopeDocumentDetails macroscopeDocumentDetailsMain;
		private System.Windows.Forms.ToolStrip toolStripViewControls;
		private System.Windows.Forms.ToolStrip toolStripExecuteControls;
		private System.Windows.Forms.ToolStripLabel toolStripLabelStartUrl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton ButtonStart;
		private System.Windows.Forms.ToolStripButton ButtonStop;
		private System.Windows.Forms.ToolStripButton ButtonReset;
		private System.Windows.Forms.ToolStripDropDownButton ToolStripFilters;
		private System.Windows.Forms.ToolStripMenuItem allDocumentTypesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stylesheetsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem javaScriptsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imagesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pDFsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miscellaneousToolStripMenuItem;
		private System.Windows.Forms.TabPage tabPageCanonicalAnalysis;
		public System.Windows.Forms.ListView listViewCanonicalAnalysis;
		private System.Windows.Forms.ColumnHeader CanonicalAnalysisUrl;
		private System.Windows.Forms.ColumnHeader CanonicalAnalysisCanonical;
		public System.Windows.Forms.ListView listViewHrefLang;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader RedirectsAuditOriginUrl;
		private System.Windows.Forms.ColumnHeader RedirectsAuditStatusCode;
		private System.Windows.Forms.ColumnHeader RedirectsAuditDestinationUrl;
		private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutSEOMacroscopeToolStripMenuItem;
		private System.Windows.Forms.TabPage tabPageHierarchy;
		private System.Windows.Forms.TabPage tabPageUriAnalysis;
		private System.Windows.Forms.TabPage tabPagePageTitles;
		private System.Windows.Forms.TabPage tabPagePageDescription;
		private System.Windows.Forms.TabPage tabPagePageKeywords;
		private System.Windows.Forms.TabPage tabPagePageHeadings;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.SplitContainer splitContainerHierarchy;
		public System.Windows.Forms.TreeView treeViewHierarchy;
		private SEOMacroscope.MacroscopeDocumentDetails macroscopeDocumentDetailsHierarchy;
		private System.Windows.Forms.SplitContainer splitContainerLeftAndRightViews;
		private System.Windows.Forms.SplitContainer splitContainerStructureAndDetails;
		
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
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveOverviewExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateHrefLangExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutSEOMacroscopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanelMainContainer = new System.Windows.Forms.TableLayoutPanel();
			this.statusStripMain = new System.Windows.Forms.StatusStrip();
			this.toolStripThreads = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripUrlCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripFound = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPageStructureOverview = new System.Windows.Forms.TabPage();
			this.tableLayoutStructure = new System.Windows.Forms.TableLayoutPanel();
			this.listViewStructure = new System.Windows.Forms.ListView();
			this.macroscopeDocumentDetailsMain = new SEOMacroscope.MacroscopeDocumentDetails();
			this.tabPageHierarchy = new System.Windows.Forms.TabPage();
			this.splitContainerHierarchy = new System.Windows.Forms.SplitContainer();
			this.treeViewHierarchy = new System.Windows.Forms.TreeView();
			this.macroscopeDocumentDetailsHierarchy = new SEOMacroscope.MacroscopeDocumentDetails();
			this.tabPageCanonicalAnalysis = new System.Windows.Forms.TabPage();
			this.listViewCanonicalAnalysis = new System.Windows.Forms.ListView();
			this.CanonicalAnalysisUrl = new System.Windows.Forms.ColumnHeader();
			this.CanonicalAnalysisCanonical = new System.Windows.Forms.ColumnHeader();
			this.tabPageHrefLangAnalysis = new System.Windows.Forms.TabPage();
			this.listViewHrefLang = new System.Windows.Forms.ListView();
			this.tabPageRedirectsAudit = new System.Windows.Forms.TabPage();
			this.listView1 = new System.Windows.Forms.ListView();
			this.RedirectsAuditOriginUrl = new System.Windows.Forms.ColumnHeader();
			this.RedirectsAuditStatusCode = new System.Windows.Forms.ColumnHeader();
			this.RedirectsAuditDestinationUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageUriAnalysis = new System.Windows.Forms.TabPage();
			this.tabPagePageTitles = new System.Windows.Forms.TabPage();
			this.tabPagePageDescription = new System.Windows.Forms.TabPage();
			this.tabPagePageKeywords = new System.Windows.Forms.TabPage();
			this.tabPagePageHeadings = new System.Windows.Forms.TabPage();
			this.tabPageEmailAddresses = new System.Windows.Forms.TabPage();
			this.listViewEmailAddresses = new System.Windows.Forms.ListView();
			this.EmailAddressesEmail = new System.Windows.Forms.ColumnHeader();
			this.EmailAddressesUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageTelephoneNumbers = new System.Windows.Forms.TabPage();
			this.listViewTelephoneNumbers = new System.Windows.Forms.ListView();
			this.TelTel = new System.Windows.Forms.ColumnHeader();
			this.TelUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageHistory = new System.Windows.Forms.TabPage();
			this.listViewHistory = new System.Windows.Forms.ListView();
			this.HistoryUrl = new System.Windows.Forms.ColumnHeader();
			this.HistoryVisited = new System.Windows.Forms.ColumnHeader();
			this.toolStripViewControls = new System.Windows.Forms.ToolStrip();
			this.ToolStripFilters = new System.Windows.Forms.ToolStripDropDownButton();
			this.allDocumentTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stylesheetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.javaScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pDFsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miscellaneousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripExecuteControls = new System.Windows.Forms.ToolStrip();
			this.toolStripLabelStartUrl = new System.Windows.Forms.ToolStripLabel();
			this.textBoxStartUrl = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ButtonStart = new System.Windows.Forms.ToolStripButton();
			this.ButtonStop = new System.Windows.Forms.ToolStripButton();
			this.ButtonReset = new System.Windows.Forms.ToolStripButton();
			this.splitContainerLeftAndRightViews = new System.Windows.Forms.SplitContainer();
			this.splitContainerStructureAndDetails = new System.Windows.Forms.SplitContainer();
			this.menuStripMain.SuspendLayout();
			this.tableLayoutPanelMainContainer.SuspendLayout();
			this.statusStripMain.SuspendLayout();
			this.tabControlMain.SuspendLayout();
			this.tabPageStructureOverview.SuspendLayout();
			this.tableLayoutStructure.SuspendLayout();
			this.tabPageHierarchy.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerHierarchy)).BeginInit();
			this.splitContainerHierarchy.Panel1.SuspendLayout();
			this.splitContainerHierarchy.Panel2.SuspendLayout();
			this.splitContainerHierarchy.SuspendLayout();
			this.tabPageCanonicalAnalysis.SuspendLayout();
			this.tabPageHrefLangAnalysis.SuspendLayout();
			this.tabPageRedirectsAudit.SuspendLayout();
			this.tabPageEmailAddresses.SuspendLayout();
			this.tabPageTelephoneNumbers.SuspendLayout();
			this.tabPageHistory.SuspendLayout();
			this.toolStripViewControls.SuspendLayout();
			this.toolStripExecuteControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftAndRightViews)).BeginInit();
			this.splitContainerLeftAndRightViews.Panel1.SuspendLayout();
			this.splitContainerLeftAndRightViews.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerStructureAndDetails)).BeginInit();
			this.splitContainerStructureAndDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStripMain
			// 
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.editToolStripMenuItem,
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
			this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
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
			this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
			this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.preferencesToolStripMenuItem.Text = "Preferences";
			this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.CallbackEditPreferencesClick);
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
			this.saveOverviewExcelReportToolStripMenuItem.Name = "saveOverviewExcelReportToolStripMenuItem";
			this.saveOverviewExcelReportToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.saveOverviewExcelReportToolStripMenuItem.Text = "Save Overview Excel Report";
			this.saveOverviewExcelReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveOverviewExcelReport);
			// 
			// generateHrefLangExcelReportToolStripMenuItem
			// 
			this.generateHrefLangExcelReportToolStripMenuItem.Name = "generateHrefLangExcelReportToolStripMenuItem";
			this.generateHrefLangExcelReportToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.generateHrefLangExcelReportToolStripMenuItem.Text = "Save HrefLang Excel Report";
			this.generateHrefLangExcelReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveHrefLangExcelReport);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.aboutSEOMacroscopeToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutSEOMacroscopeToolStripMenuItem
			// 
			this.aboutSEOMacroscopeToolStripMenuItem.Name = "aboutSEOMacroscopeToolStripMenuItem";
			this.aboutSEOMacroscopeToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
			this.aboutSEOMacroscopeToolStripMenuItem.Text = "About SEO Macroscope";
			this.aboutSEOMacroscopeToolStripMenuItem.Click += new System.EventHandler(this.CallbackHelpAboutClick);
			// 
			// tableLayoutPanelMainContainer
			// 
			this.tableLayoutPanelMainContainer.ColumnCount = 1;
			this.tableLayoutPanelMainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMainContainer.Controls.Add(this.statusStripMain, 0, 4);
			this.tableLayoutPanelMainContainer.Controls.Add(this.tabControlMain, 0, 2);
			this.tableLayoutPanelMainContainer.Controls.Add(this.toolStripViewControls, 0, 1);
			this.tableLayoutPanelMainContainer.Controls.Add(this.toolStripExecuteControls, 0, 0);
			this.tableLayoutPanelMainContainer.Controls.Add(this.splitContainerLeftAndRightViews, 0, 3);
			this.tableLayoutPanelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelMainContainer.Location = new System.Drawing.Point(0, 24);
			this.tableLayoutPanelMainContainer.Name = "tableLayoutPanelMainContainer";
			this.tableLayoutPanelMainContainer.RowCount = 5;
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanelMainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
			this.statusStripMain.Text = "statusStrip1";
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
			this.toolStripFound.Size = new System.Drawing.Size(82, 25);
			this.toolStripFound.Text = "URLs Found: 0";
			// 
			// tabControlMain
			// 
			this.tabControlMain.Controls.Add(this.tabPageStructureOverview);
			this.tabControlMain.Controls.Add(this.tabPageHierarchy);
			this.tabControlMain.Controls.Add(this.tabPageCanonicalAnalysis);
			this.tabControlMain.Controls.Add(this.tabPageHrefLangAnalysis);
			this.tabControlMain.Controls.Add(this.tabPageRedirectsAudit);
			this.tabControlMain.Controls.Add(this.tabPageUriAnalysis);
			this.tabControlMain.Controls.Add(this.tabPagePageTitles);
			this.tabControlMain.Controls.Add(this.tabPagePageDescription);
			this.tabControlMain.Controls.Add(this.tabPagePageKeywords);
			this.tabControlMain.Controls.Add(this.tabPagePageHeadings);
			this.tabControlMain.Controls.Add(this.tabPageEmailAddresses);
			this.tabControlMain.Controls.Add(this.tabPageTelephoneNumbers);
			this.tabControlMain.Controls.Add(this.tabPageHistory);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(0, 60);
			this.tabControlMain.Margin = new System.Windows.Forms.Padding(0);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(784, 248);
			this.tabControlMain.TabIndex = 3;
			this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.CallbackTabControlDisplaySelectedIndexChanged);
			// 
			// tabPageStructureOverview
			// 
			this.tabPageStructureOverview.Controls.Add(this.tableLayoutStructure);
			this.tabPageStructureOverview.Location = new System.Drawing.Point(4, 22);
			this.tabPageStructureOverview.Name = "tabPageStructureOverview";
			this.tabPageStructureOverview.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageStructureOverview.Size = new System.Drawing.Size(776, 222);
			this.tabPageStructureOverview.TabIndex = 0;
			this.tabPageStructureOverview.Text = "Structure Overview";
			this.tabPageStructureOverview.UseVisualStyleBackColor = true;
			this.tabPageStructureOverview.Enter += new System.EventHandler(this.CallbackTabPageStructureOverviewShow);
			// 
			// tableLayoutStructure
			// 
			this.tableLayoutStructure.ColumnCount = 1;
			this.tableLayoutStructure.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutStructure.Controls.Add(this.listViewStructure, 0, 0);
			this.tableLayoutStructure.Controls.Add(this.macroscopeDocumentDetailsMain, 0, 1);
			this.tableLayoutStructure.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutStructure.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutStructure.Name = "tableLayoutStructure";
			this.tableLayoutStructure.RowCount = 2;
			this.tableLayoutStructure.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutStructure.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutStructure.Size = new System.Drawing.Size(770, 216);
			this.tableLayoutStructure.TabIndex = 0;
			// 
			// listViewStructure
			// 
			this.listViewStructure.CausesValidation = false;
			this.listViewStructure.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewStructure.FullRowSelect = true;
			this.listViewStructure.GridLines = true;
			this.listViewStructure.Location = new System.Drawing.Point(0, 0);
			this.listViewStructure.Margin = new System.Windows.Forms.Padding(0);
			this.listViewStructure.Name = "listViewStructure";
			this.listViewStructure.Size = new System.Drawing.Size(770, 108);
			this.listViewStructure.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewStructure.TabIndex = 0;
			this.listViewStructure.UseCompatibleStateImageBehavior = false;
			this.listViewStructure.View = System.Windows.Forms.View.Details;
			this.listViewStructure.Click += new System.EventHandler(this.CallbackListViewStructureOverviewClick);
			// 
			// macroscopeDocumentDetailsMain
			// 
			this.macroscopeDocumentDetailsMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.macroscopeDocumentDetailsMain.Location = new System.Drawing.Point(0, 113);
			this.macroscopeDocumentDetailsMain.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.macroscopeDocumentDetailsMain.Name = "macroscopeDocumentDetailsMain";
			this.macroscopeDocumentDetailsMain.Size = new System.Drawing.Size(770, 103);
			this.macroscopeDocumentDetailsMain.TabIndex = 1;
			// 
			// tabPageHierarchy
			// 
			this.tabPageHierarchy.Controls.Add(this.splitContainerHierarchy);
			this.tabPageHierarchy.Location = new System.Drawing.Point(4, 22);
			this.tabPageHierarchy.Name = "tabPageHierarchy";
			this.tabPageHierarchy.Size = new System.Drawing.Size(776, 422);
			this.tabPageHierarchy.TabIndex = 8;
			this.tabPageHierarchy.Text = "Hierarchy";
			this.tabPageHierarchy.UseVisualStyleBackColor = true;
			// 
			// splitContainerHierarchy
			// 
			this.splitContainerHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerHierarchy.Location = new System.Drawing.Point(0, 0);
			this.splitContainerHierarchy.Name = "splitContainerHierarchy";
			this.splitContainerHierarchy.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerHierarchy.Panel1
			// 
			this.splitContainerHierarchy.Panel1.Controls.Add(this.treeViewHierarchy);
			// 
			// splitContainerHierarchy.Panel2
			// 
			this.splitContainerHierarchy.Panel2.Controls.Add(this.macroscopeDocumentDetailsHierarchy);
			this.splitContainerHierarchy.Size = new System.Drawing.Size(776, 422);
			this.splitContainerHierarchy.SplitterDistance = 200;
			this.splitContainerHierarchy.TabIndex = 0;
			// 
			// treeViewHierarchy
			// 
			this.treeViewHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewHierarchy.FullRowSelect = true;
			this.treeViewHierarchy.Location = new System.Drawing.Point(0, 0);
			this.treeViewHierarchy.Name = "treeViewHierarchy";
			this.treeViewHierarchy.Size = new System.Drawing.Size(776, 200);
			this.treeViewHierarchy.TabIndex = 0;
			// 
			// macroscopeDocumentDetailsHierarchy
			// 
			this.macroscopeDocumentDetailsHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.macroscopeDocumentDetailsHierarchy.Location = new System.Drawing.Point(0, 0);
			this.macroscopeDocumentDetailsHierarchy.Name = "macroscopeDocumentDetailsHierarchy";
			this.macroscopeDocumentDetailsHierarchy.Size = new System.Drawing.Size(776, 218);
			this.macroscopeDocumentDetailsHierarchy.TabIndex = 0;
			// 
			// tabPageCanonicalAnalysis
			// 
			this.tabPageCanonicalAnalysis.Controls.Add(this.listViewCanonicalAnalysis);
			this.tabPageCanonicalAnalysis.Location = new System.Drawing.Point(4, 22);
			this.tabPageCanonicalAnalysis.Name = "tabPageCanonicalAnalysis";
			this.tabPageCanonicalAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageCanonicalAnalysis.Size = new System.Drawing.Size(776, 422);
			this.tabPageCanonicalAnalysis.TabIndex = 7;
			this.tabPageCanonicalAnalysis.Text = "Canonical Analysis";
			this.tabPageCanonicalAnalysis.UseVisualStyleBackColor = true;
			this.tabPageCanonicalAnalysis.Enter += new System.EventHandler(this.CallbackTabPageCanonicalAnalysisShow);
			// 
			// listViewCanonicalAnalysis
			// 
			this.listViewCanonicalAnalysis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.CanonicalAnalysisUrl,
			this.CanonicalAnalysisCanonical});
			this.listViewCanonicalAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewCanonicalAnalysis.FullRowSelect = true;
			this.listViewCanonicalAnalysis.GridLines = true;
			this.listViewCanonicalAnalysis.Location = new System.Drawing.Point(3, 3);
			this.listViewCanonicalAnalysis.Margin = new System.Windows.Forms.Padding(0);
			this.listViewCanonicalAnalysis.Name = "listViewCanonicalAnalysis";
			this.listViewCanonicalAnalysis.Size = new System.Drawing.Size(770, 416);
			this.listViewCanonicalAnalysis.TabIndex = 0;
			this.listViewCanonicalAnalysis.UseCompatibleStateImageBehavior = false;
			this.listViewCanonicalAnalysis.View = System.Windows.Forms.View.Details;
			// 
			// CanonicalAnalysisUrl
			// 
			this.CanonicalAnalysisUrl.Text = "URL";
			this.CanonicalAnalysisUrl.Width = 300;
			// 
			// CanonicalAnalysisCanonical
			// 
			this.CanonicalAnalysisCanonical.Text = "Canonical";
			this.CanonicalAnalysisCanonical.Width = 300;
			// 
			// tabPageHrefLangAnalysis
			// 
			this.tabPageHrefLangAnalysis.Controls.Add(this.listViewHrefLang);
			this.tabPageHrefLangAnalysis.Location = new System.Drawing.Point(4, 22);
			this.tabPageHrefLangAnalysis.Name = "tabPageHrefLangAnalysis";
			this.tabPageHrefLangAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHrefLangAnalysis.Size = new System.Drawing.Size(776, 422);
			this.tabPageHrefLangAnalysis.TabIndex = 1;
			this.tabPageHrefLangAnalysis.Text = "HrefLang Analysis";
			this.tabPageHrefLangAnalysis.UseVisualStyleBackColor = true;
			this.tabPageHrefLangAnalysis.Enter += new System.EventHandler(this.CallbackTabPageHrefLangAnalysisShow);
			// 
			// listViewHrefLang
			// 
			this.listViewHrefLang.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewHrefLang.FullRowSelect = true;
			this.listViewHrefLang.GridLines = true;
			this.listViewHrefLang.Location = new System.Drawing.Point(3, 3);
			this.listViewHrefLang.Name = "listViewHrefLang";
			this.listViewHrefLang.Size = new System.Drawing.Size(770, 416);
			this.listViewHrefLang.TabIndex = 0;
			this.listViewHrefLang.UseCompatibleStateImageBehavior = false;
			this.listViewHrefLang.View = System.Windows.Forms.View.Details;
			// 
			// tabPageRedirectsAudit
			// 
			this.tabPageRedirectsAudit.Controls.Add(this.listView1);
			this.tabPageRedirectsAudit.Location = new System.Drawing.Point(4, 22);
			this.tabPageRedirectsAudit.Name = "tabPageRedirectsAudit";
			this.tabPageRedirectsAudit.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRedirectsAudit.Size = new System.Drawing.Size(776, 422);
			this.tabPageRedirectsAudit.TabIndex = 2;
			this.tabPageRedirectsAudit.Text = "Redirects Audit";
			this.tabPageRedirectsAudit.UseVisualStyleBackColor = true;
			this.tabPageRedirectsAudit.Enter += new System.EventHandler(this.CallbackTabPageRedirectsAuditShow);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.RedirectsAuditOriginUrl,
			this.RedirectsAuditStatusCode,
			this.RedirectsAuditDestinationUrl});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(3, 3);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(770, 416);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// RedirectsAuditOriginUrl
			// 
			this.RedirectsAuditOriginUrl.Text = "Origin URL";
			this.RedirectsAuditOriginUrl.Width = 300;
			// 
			// RedirectsAuditStatusCode
			// 
			this.RedirectsAuditStatusCode.Text = "Status Code";
			this.RedirectsAuditStatusCode.Width = 100;
			// 
			// RedirectsAuditDestinationUrl
			// 
			this.RedirectsAuditDestinationUrl.Text = "Destination URL";
			this.RedirectsAuditDestinationUrl.Width = 300;
			// 
			// tabPageUriAnalysis
			// 
			this.tabPageUriAnalysis.Location = new System.Drawing.Point(4, 22);
			this.tabPageUriAnalysis.Name = "tabPageUriAnalysis";
			this.tabPageUriAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageUriAnalysis.Size = new System.Drawing.Size(776, 422);
			this.tabPageUriAnalysis.TabIndex = 9;
			this.tabPageUriAnalysis.Text = "URI Analysis";
			this.tabPageUriAnalysis.UseVisualStyleBackColor = true;
			// 
			// tabPagePageTitles
			// 
			this.tabPagePageTitles.Location = new System.Drawing.Point(4, 22);
			this.tabPagePageTitles.Name = "tabPagePageTitles";
			this.tabPagePageTitles.Size = new System.Drawing.Size(776, 422);
			this.tabPagePageTitles.TabIndex = 10;
			this.tabPagePageTitles.Text = "Page Titles";
			this.tabPagePageTitles.UseVisualStyleBackColor = true;
			// 
			// tabPagePageDescription
			// 
			this.tabPagePageDescription.Location = new System.Drawing.Point(4, 22);
			this.tabPagePageDescription.Name = "tabPagePageDescription";
			this.tabPagePageDescription.Size = new System.Drawing.Size(776, 422);
			this.tabPagePageDescription.TabIndex = 11;
			this.tabPagePageDescription.Text = "Page Description";
			this.tabPagePageDescription.UseVisualStyleBackColor = true;
			// 
			// tabPagePageKeywords
			// 
			this.tabPagePageKeywords.Location = new System.Drawing.Point(4, 22);
			this.tabPagePageKeywords.Name = "tabPagePageKeywords";
			this.tabPagePageKeywords.Size = new System.Drawing.Size(776, 422);
			this.tabPagePageKeywords.TabIndex = 12;
			this.tabPagePageKeywords.Text = "Page Keywords";
			this.tabPagePageKeywords.UseVisualStyleBackColor = true;
			// 
			// tabPagePageHeadings
			// 
			this.tabPagePageHeadings.Location = new System.Drawing.Point(4, 22);
			this.tabPagePageHeadings.Name = "tabPagePageHeadings";
			this.tabPagePageHeadings.Size = new System.Drawing.Size(776, 422);
			this.tabPagePageHeadings.TabIndex = 13;
			this.tabPagePageHeadings.Text = "Page Headings";
			this.tabPagePageHeadings.UseVisualStyleBackColor = true;
			// 
			// tabPageEmailAddresses
			// 
			this.tabPageEmailAddresses.Controls.Add(this.listViewEmailAddresses);
			this.tabPageEmailAddresses.Location = new System.Drawing.Point(4, 22);
			this.tabPageEmailAddresses.Name = "tabPageEmailAddresses";
			this.tabPageEmailAddresses.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEmailAddresses.Size = new System.Drawing.Size(776, 422);
			this.tabPageEmailAddresses.TabIndex = 3;
			this.tabPageEmailAddresses.Text = "Email Addresses";
			this.tabPageEmailAddresses.UseVisualStyleBackColor = true;
			this.tabPageEmailAddresses.Enter += new System.EventHandler(this.CallbackTabPageEmailAddressesShow);
			// 
			// listViewEmailAddresses
			// 
			this.listViewEmailAddresses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.EmailAddressesEmail,
			this.EmailAddressesUrl});
			this.listViewEmailAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewEmailAddresses.FullRowSelect = true;
			this.listViewEmailAddresses.GridLines = true;
			this.listViewEmailAddresses.Location = new System.Drawing.Point(3, 3);
			this.listViewEmailAddresses.Margin = new System.Windows.Forms.Padding(0);
			this.listViewEmailAddresses.Name = "listViewEmailAddresses";
			this.listViewEmailAddresses.Size = new System.Drawing.Size(770, 416);
			this.listViewEmailAddresses.TabIndex = 1;
			this.listViewEmailAddresses.UseCompatibleStateImageBehavior = false;
			this.listViewEmailAddresses.View = System.Windows.Forms.View.Details;
			// 
			// EmailAddressesEmail
			// 
			this.EmailAddressesEmail.Text = "Email Address";
			this.EmailAddressesEmail.Width = 300;
			// 
			// EmailAddressesUrl
			// 
			this.EmailAddressesUrl.Text = "URL";
			this.EmailAddressesUrl.Width = 500;
			// 
			// tabPageTelephoneNumbers
			// 
			this.tabPageTelephoneNumbers.Controls.Add(this.listViewTelephoneNumbers);
			this.tabPageTelephoneNumbers.Location = new System.Drawing.Point(4, 22);
			this.tabPageTelephoneNumbers.Name = "tabPageTelephoneNumbers";
			this.tabPageTelephoneNumbers.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTelephoneNumbers.Size = new System.Drawing.Size(776, 422);
			this.tabPageTelephoneNumbers.TabIndex = 4;
			this.tabPageTelephoneNumbers.Text = "Telephone Numbers";
			this.tabPageTelephoneNumbers.UseVisualStyleBackColor = true;
			this.tabPageTelephoneNumbers.Enter += new System.EventHandler(this.CallbackTabPageTelephoneNumbersShow);
			// 
			// listViewTelephoneNumbers
			// 
			this.listViewTelephoneNumbers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.TelTel,
			this.TelUrl});
			this.listViewTelephoneNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewTelephoneNumbers.FullRowSelect = true;
			this.listViewTelephoneNumbers.GridLines = true;
			this.listViewTelephoneNumbers.Location = new System.Drawing.Point(3, 3);
			this.listViewTelephoneNumbers.Margin = new System.Windows.Forms.Padding(0);
			this.listViewTelephoneNumbers.Name = "listViewTelephoneNumbers";
			this.listViewTelephoneNumbers.Size = new System.Drawing.Size(770, 416);
			this.listViewTelephoneNumbers.TabIndex = 1;
			this.listViewTelephoneNumbers.UseCompatibleStateImageBehavior = false;
			this.listViewTelephoneNumbers.View = System.Windows.Forms.View.Details;
			// 
			// TelTel
			// 
			this.TelTel.Text = "Telephone Number";
			this.TelTel.Width = 300;
			// 
			// TelUrl
			// 
			this.TelUrl.Text = "URL";
			this.TelUrl.Width = 500;
			// 
			// tabPageHistory
			// 
			this.tabPageHistory.Controls.Add(this.listViewHistory);
			this.tabPageHistory.Location = new System.Drawing.Point(4, 22);
			this.tabPageHistory.Name = "tabPageHistory";
			this.tabPageHistory.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHistory.Size = new System.Drawing.Size(776, 422);
			this.tabPageHistory.TabIndex = 5;
			this.tabPageHistory.Text = "History";
			this.tabPageHistory.UseVisualStyleBackColor = true;
			this.tabPageHistory.Enter += new System.EventHandler(this.CallbackTabPageHistoryShow);
			// 
			// listViewHistory
			// 
			this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.HistoryUrl,
			this.HistoryVisited});
			this.listViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewHistory.GridLines = true;
			this.listViewHistory.Location = new System.Drawing.Point(3, 3);
			this.listViewHistory.Margin = new System.Windows.Forms.Padding(0);
			this.listViewHistory.Name = "listViewHistory";
			this.listViewHistory.Size = new System.Drawing.Size(770, 416);
			this.listViewHistory.TabIndex = 0;
			this.listViewHistory.UseCompatibleStateImageBehavior = false;
			this.listViewHistory.View = System.Windows.Forms.View.Details;
			// 
			// HistoryUrl
			// 
			this.HistoryUrl.Text = "URL";
			this.HistoryUrl.Width = 400;
			// 
			// HistoryVisited
			// 
			this.HistoryVisited.Text = "Visited";
			this.HistoryVisited.Width = 100;
			// 
			// toolStripViewControls
			// 
			this.toolStripViewControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripViewControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.ToolStripFilters,
			this.toolStripSeparator2,
			this.toolStripButton1});
			this.toolStripViewControls.Location = new System.Drawing.Point(0, 30);
			this.toolStripViewControls.Name = "toolStripViewControls";
			this.toolStripViewControls.Size = new System.Drawing.Size(784, 30);
			this.toolStripViewControls.TabIndex = 5;
			this.toolStripViewControls.Text = "toolStrip1";
			// 
			// ToolStripFilters
			// 
			this.ToolStripFilters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ToolStripFilters.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.allDocumentTypesToolStripMenuItem,
			this.hTMLToolStripMenuItem,
			this.stylesheetsToolStripMenuItem,
			this.javaScriptsToolStripMenuItem,
			this.imagesToolStripMenuItem,
			this.pDFsToolStripMenuItem,
			this.miscellaneousToolStripMenuItem});
			this.ToolStripFilters.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripFilters.Image")));
			this.ToolStripFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripFilters.Name = "ToolStripFilters";
			this.ToolStripFilters.Size = new System.Drawing.Size(46, 27);
			this.ToolStripFilters.Text = "Filter";
			this.ToolStripFilters.ToolTipText = "Filter results";
			// 
			// allDocumentTypesToolStripMenuItem
			// 
			this.allDocumentTypesToolStripMenuItem.Name = "allDocumentTypesToolStripMenuItem";
			this.allDocumentTypesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.allDocumentTypesToolStripMenuItem.Text = "All Document Types";
			// 
			// hTMLToolStripMenuItem
			// 
			this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
			this.hTMLToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.hTMLToolStripMenuItem.Text = "HTML";
			// 
			// stylesheetsToolStripMenuItem
			// 
			this.stylesheetsToolStripMenuItem.Name = "stylesheetsToolStripMenuItem";
			this.stylesheetsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.stylesheetsToolStripMenuItem.Text = "Stylesheets";
			// 
			// javaScriptsToolStripMenuItem
			// 
			this.javaScriptsToolStripMenuItem.Name = "javaScriptsToolStripMenuItem";
			this.javaScriptsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.javaScriptsToolStripMenuItem.Text = "JavaScripts";
			// 
			// imagesToolStripMenuItem
			// 
			this.imagesToolStripMenuItem.Name = "imagesToolStripMenuItem";
			this.imagesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.imagesToolStripMenuItem.Text = "Images";
			// 
			// pDFsToolStripMenuItem
			// 
			this.pDFsToolStripMenuItem.Name = "pDFsToolStripMenuItem";
			this.pDFsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.pDFsToolStripMenuItem.Text = "PDFs";
			// 
			// miscellaneousToolStripMenuItem
			// 
			this.miscellaneousToolStripMenuItem.Name = "miscellaneousToolStripMenuItem";
			this.miscellaneousToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.miscellaneousToolStripMenuItem.Text = "Miscellaneous";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(108, 27);
			this.toolStripButton1.Text = "Retry Broken Links";
			this.toolStripButton1.Click += new System.EventHandler(this.CallbackRetryBrokenLinksClick);
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
			this.ButtonReset});
			this.toolStripExecuteControls.Location = new System.Drawing.Point(0, 0);
			this.toolStripExecuteControls.Name = "toolStripExecuteControls";
			this.toolStripExecuteControls.Size = new System.Drawing.Size(784, 30);
			this.toolStripExecuteControls.TabIndex = 6;
			this.toolStripExecuteControls.Text = "toolStrip1";
			// 
			// toolStripLabelStartUrl
			// 
			this.toolStripLabelStartUrl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripLabelStartUrl.Name = "toolStripLabelStartUrl";
			this.toolStripLabelStartUrl.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.toolStripLabelStartUrl.Size = new System.Drawing.Size(64, 27);
			this.toolStripLabelStartUrl.Text = "Start URL:";
			// 
			// textBoxStartUrl
			// 
			this.textBoxStartUrl.Name = "textBoxStartUrl";
			this.textBoxStartUrl.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.textBoxStartUrl.Size = new System.Drawing.Size(300, 30);
			this.textBoxStartUrl.ToolTipText = "Enter a URL to begin scanning from";
			this.textBoxStartUrl.TextChanged += new System.EventHandler(this.CallbackStartUrlTextChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
			// 
			// ButtonStart
			// 
			this.ButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("ButtonStart.Image")));
			this.ButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonStart.Name = "ButtonStart";
			this.ButtonStart.Size = new System.Drawing.Size(35, 27);
			this.ButtonStart.Text = "Start";
			this.ButtonStart.ToolTipText = "Start scanning";
			this.ButtonStart.Click += new System.EventHandler(this.CallbackScanStart);
			// 
			// ButtonStop
			// 
			this.ButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("ButtonStop.Image")));
			this.ButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonStop.Name = "ButtonStop";
			this.ButtonStop.Size = new System.Drawing.Size(35, 27);
			this.ButtonStop.Text = "Stop";
			this.ButtonStop.ToolTipText = "Stop scanning";
			this.ButtonStop.Click += new System.EventHandler(this.CallbackScanStop);
			// 
			// ButtonReset
			// 
			this.ButtonReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ButtonReset.Image = ((System.Drawing.Image)(resources.GetObject("ButtonReset.Image")));
			this.ButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ButtonReset.Name = "ButtonReset";
			this.ButtonReset.Size = new System.Drawing.Size(39, 27);
			this.ButtonReset.Text = "Reset";
			this.ButtonReset.ToolTipText = "Reset all scan results";
			this.ButtonReset.Click += new System.EventHandler(this.CallbackScanReset);
			// 
			// splitContainerLeftAndRightViews
			// 
			this.splitContainerLeftAndRightViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerLeftAndRightViews.Location = new System.Drawing.Point(3, 311);
			this.splitContainerLeftAndRightViews.Name = "splitContainerLeftAndRightViews";
			// 
			// splitContainerLeftAndRightViews.Panel1
			// 
			this.splitContainerLeftAndRightViews.Panel1.Controls.Add(this.splitContainerStructureAndDetails);
			this.splitContainerLeftAndRightViews.Panel1MinSize = 75;
			this.splitContainerLeftAndRightViews.Size = new System.Drawing.Size(778, 194);
			this.splitContainerLeftAndRightViews.SplitterDistance = 533;
			this.splitContainerLeftAndRightViews.TabIndex = 7;
			this.splitContainerLeftAndRightViews.TabStop = false;
			// 
			// splitContainerStructureAndDetails
			// 
			this.splitContainerStructureAndDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerStructureAndDetails.Location = new System.Drawing.Point(0, 0);
			this.splitContainerStructureAndDetails.Name = "splitContainerStructureAndDetails";
			this.splitContainerStructureAndDetails.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.splitContainerStructureAndDetails.Size = new System.Drawing.Size(533, 194);
			this.splitContainerStructureAndDetails.SplitterDistance = 97;
			this.splitContainerStructureAndDetails.TabIndex = 0;
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
			this.Text = "MacroscopeMainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CallbackFormClosing);
			this.menuStripMain.ResumeLayout(false);
			this.menuStripMain.PerformLayout();
			this.tableLayoutPanelMainContainer.ResumeLayout(false);
			this.tableLayoutPanelMainContainer.PerformLayout();
			this.statusStripMain.ResumeLayout(false);
			this.statusStripMain.PerformLayout();
			this.tabControlMain.ResumeLayout(false);
			this.tabPageStructureOverview.ResumeLayout(false);
			this.tableLayoutStructure.ResumeLayout(false);
			this.tabPageHierarchy.ResumeLayout(false);
			this.splitContainerHierarchy.Panel1.ResumeLayout(false);
			this.splitContainerHierarchy.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerHierarchy)).EndInit();
			this.splitContainerHierarchy.ResumeLayout(false);
			this.tabPageCanonicalAnalysis.ResumeLayout(false);
			this.tabPageHrefLangAnalysis.ResumeLayout(false);
			this.tabPageRedirectsAudit.ResumeLayout(false);
			this.tabPageEmailAddresses.ResumeLayout(false);
			this.tabPageTelephoneNumbers.ResumeLayout(false);
			this.tabPageHistory.ResumeLayout(false);
			this.toolStripViewControls.ResumeLayout(false);
			this.toolStripViewControls.PerformLayout();
			this.toolStripExecuteControls.ResumeLayout(false);
			this.toolStripExecuteControls.PerformLayout();
			this.splitContainerLeftAndRightViews.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerLeftAndRightViews)).EndInit();
			this.splitContainerLeftAndRightViews.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerStructureAndDetails)).EndInit();
			this.splitContainerStructureAndDetails.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		}
}
