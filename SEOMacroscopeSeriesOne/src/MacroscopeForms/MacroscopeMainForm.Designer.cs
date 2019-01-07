/*

	This file is part of SEOMacroscope.

	Copyright 2018 Jason Holland.

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
		public System.Windows.Forms.ToolStripMenuItem saveOverviewExcelReportToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem generateHrefLangExcelReportToolStripMenuItem;
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
		internal System.Windows.Forms.ToolStripButton toolStripButtonRetryBrokenLinks;
		private System.Windows.Forms.SplitContainer splitContainerLeftAndRightViews;
		public System.Windows.Forms.SplitContainer splitContainerStructureAndDocumentDetails;
		public MacroscopeOverviewPanel macroscopeOverviewTabPanelInstance;
		private SEOMacroscope.MacroscopeDocumentDetails macroscopeDocumentDetailsInstance;
		internal SEOMacroscope.MacroscopeSiteStructurePanel macroscopeSiteStructurePanelInstance;
		public System.Windows.Forms.ToolStripMenuItem loadUrlListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadFromTextFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteFromClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sEOMacroscopeBlogToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem taskParametersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem includeURLPatternsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem excludeURLPatternsToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem crawlParentDirectoriesToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem crawlChildDirectoriesToolStripMenuItem;
		public System.Windows.Forms.ToolStripProgressBar ProgressBarScan;
		private System.Windows.Forms.ToolStripMenuItem sEOMacroscopeOnGitHubToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem licenceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearHTTPAuthenticationToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem generatePageContentsExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveURIAnalysisExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sEOMacroscopeManualToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem brokenLinksExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem duplicateContentExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem contactDetailsExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem keywordAnalysisExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redirectsExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem remarksExcelReportToolStripMenuItem;
		internal System.Windows.Forms.ToolStripButton toolStripButtonRetryTimedOutLinks;
		private System.Windows.Forms.ToolStripMenuItem reportABugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem errorsExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem customFiltersToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem customFiltersExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem dataExtractorsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem regularExpressionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sitemapXMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sitemapTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem sitemapXMLOnePerHostToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem sitemapTextOnePerHostToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem dataExtractorsExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pageMetadataExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cSSSelectorsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem xPathExpressionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem excelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem titlesCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem descriptionsCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem keywordsCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem excelReportToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem cSVReportToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem cSSSelectorsCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem regularExpressionsCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem xPathExpressionsCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem pageHeadingsCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pageTextCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem linksCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hyperlinksCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uRIsCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem titlesCSVReportToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem checksumsCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem duplicateETagsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pagesCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem cSVReportToolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
		private System.Windows.Forms.ToolStripMenuItem brokenLinksCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem goodLinksCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redirectedLinksCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem combinedExcelReportToolStripMenuItem8;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
		private System.Windows.Forms.ToolStripMenuItem emailAddressesCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem telephoneNumbersCSVReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem excelReportToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem cSVReportToolStripMenuItem3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
		private System.Windows.Forms.ToolStripButton toolStripButtonRecalculateStatistics;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
		private System.Windows.Forms.ToolStripMenuItem exportCurrentListToCSVToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportCurrentListToExcelToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButtonRecalculateClickPaths;


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
      this.loadUrlListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadFromTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.loadSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveSessionAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
      this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sitemapXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sitemapXMLOnePerHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.sitemapTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sitemapTextOnePerHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
      this.exportCurrentListToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportCurrentListToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.taskParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.includeURLPatternsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.excludeURLPatternsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.crawlParentDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.crawlChildDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.customFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.dataExtractorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cSSSelectorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.regularExpressionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xPathExpressionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.clearHTTPAuthenticationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveOverviewExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.saveURIAnalysisExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
      this.linksCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.hyperlinksCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.uRIsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.orphanedPagesCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redirectsExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
      this.redirectsAuditCSVReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.redirectChainsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.brokenLinksExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
      this.brokenLinksCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.goodLinksCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.redirectedLinksCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.errorsExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
      this.cSVReportToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.robotsReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.excelReportToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
      this.cSVReportToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
      this.sitemapErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.excelReportToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
      this.cSVReportToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
      this.remarksExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.excelReportToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
      this.cSVReportToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.pageMetadataExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.excelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
      this.titlesCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.descriptionsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.keywordsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.generatePageContentsExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
      this.pageHeadingsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pageTextCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.generateHrefLangExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.duplicateContentExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
      this.titlesCSVReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.checksumsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.duplicateETagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pagesCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.keywordAnalysisExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.contactDetailsExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
      this.emailAddressesCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.telephoneNumbersCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.customFiltersExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.excelReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
      this.cSVReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.dataExtractorsExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.combinedExcelReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
      this.cSSSelectorsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.regularExpressionsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xPathExpressionsCSVReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sEOMacroscopeBlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sEOMacroscopeOnGitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sEOMacroscopeManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.reportABugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.licenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.aboutSEOMacroscopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tableLayoutPanelMainContainer = new System.Windows.Forms.TableLayoutPanel();
      this.statusStripMain = new System.Windows.Forms.StatusStrip();
      this.toolStripThreads = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripUrlCount = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripFound = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripViewControls = new System.Windows.Forms.ToolStrip();
      this.toolStripButtonRetryBrokenLinks = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonRetryTimedOutLinks = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonRecalculateStatistics = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonRecalculateClickPaths = new System.Windows.Forms.ToolStripButton();
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
            this.viewToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStripMain.Location = new System.Drawing.Point(0, 0);
      this.menuStripMain.Name = "menuStripMain";
      this.menuStripMain.Size = new System.Drawing.Size(1008, 24);
      this.menuStripMain.TabIndex = 1;
      this.menuStripMain.Text = "menuStripMain";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadUrlListToolStripMenuItem,
            this.toolStripSeparator8,
            this.loadSessionToolStripMenuItem,
            this.saveSessionAsToolStripMenuItem,
            this.toolStripSeparator30,
            this.exportToolStripMenuItem,
            this.toolStripSeparator9,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // loadUrlListToolStripMenuItem
      // 
      this.loadUrlListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFromTextFileToolStripMenuItem,
            this.pasteFromClipboardToolStripMenuItem});
      this.loadUrlListToolStripMenuItem.Name = "loadUrlListToolStripMenuItem";
      this.loadUrlListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.loadUrlListToolStripMenuItem.Text = "Load URL List";
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
      this.pasteFromClipboardToolStripMenuItem.Click += new System.EventHandler(this.CallbackLoadUrlListTextFromClipboard);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(177, 6);
      // 
      // loadSessionToolStripMenuItem
      // 
      this.loadSessionToolStripMenuItem.Name = "loadSessionToolStripMenuItem";
      this.loadSessionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.loadSessionToolStripMenuItem.Text = "Load Session";
      this.loadSessionToolStripMenuItem.Visible = false;
      this.loadSessionToolStripMenuItem.Click += new System.EventHandler(this.CallbackLoadSessionFromFile);
      // 
      // saveSessionAsToolStripMenuItem
      // 
      this.saveSessionAsToolStripMenuItem.Name = "saveSessionAsToolStripMenuItem";
      this.saveSessionAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.saveSessionAsToolStripMenuItem.Text = "Save Session As...";
      this.saveSessionAsToolStripMenuItem.Visible = false;
      this.saveSessionAsToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveSessionToFile);
      // 
      // toolStripSeparator30
      // 
      this.toolStripSeparator30.Name = "toolStripSeparator30";
      this.toolStripSeparator30.Size = new System.Drawing.Size(177, 6);
      this.toolStripSeparator30.Visible = false;
      // 
      // exportToolStripMenuItem
      // 
      this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sitemapXMLToolStripMenuItem,
            this.sitemapXMLOnePerHostToolStripMenuItem,
            this.toolStripSeparator10,
            this.sitemapTextToolStripMenuItem,
            this.sitemapTextOnePerHostToolStripMenuItem,
            this.toolStripSeparator26,
            this.exportCurrentListToCSVToolStripMenuItem,
            this.exportCurrentListToExcelToolStripMenuItem});
      this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
      this.exportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.exportToolStripMenuItem.Text = "Export";
      // 
      // sitemapXMLToolStripMenuItem
      // 
      this.sitemapXMLToolStripMenuItem.Name = "sitemapXMLToolStripMenuItem";
      this.sitemapXMLToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
      this.sitemapXMLToolStripMenuItem.Text = "Sitemap XML";
      this.sitemapXMLToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveGeneratorSitemapXml);
      // 
      // sitemapXMLOnePerHostToolStripMenuItem
      // 
      this.sitemapXMLOnePerHostToolStripMenuItem.Name = "sitemapXMLOnePerHostToolStripMenuItem";
      this.sitemapXMLOnePerHostToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
      this.sitemapXMLOnePerHostToolStripMenuItem.Text = "Sitemap XML For Each Host";
      this.sitemapXMLOnePerHostToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveGeneratorSitemapXmlPerHost);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(217, 6);
      // 
      // sitemapTextToolStripMenuItem
      // 
      this.sitemapTextToolStripMenuItem.Name = "sitemapTextToolStripMenuItem";
      this.sitemapTextToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
      this.sitemapTextToolStripMenuItem.Text = "Sitemap Text";
      this.sitemapTextToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveGeneratorSitemapText);
      // 
      // sitemapTextOnePerHostToolStripMenuItem
      // 
      this.sitemapTextOnePerHostToolStripMenuItem.Name = "sitemapTextOnePerHostToolStripMenuItem";
      this.sitemapTextOnePerHostToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
      this.sitemapTextOnePerHostToolStripMenuItem.Text = "Sitemap Text For Each Host";
      this.sitemapTextOnePerHostToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveGeneratorSitemapTextPerHost);
      // 
      // toolStripSeparator26
      // 
      this.toolStripSeparator26.Name = "toolStripSeparator26";
      this.toolStripSeparator26.Size = new System.Drawing.Size(217, 6);
      // 
      // exportCurrentListToCSVToolStripMenuItem
      // 
      this.exportCurrentListToCSVToolStripMenuItem.Name = "exportCurrentListToCSVToolStripMenuItem";
      this.exportCurrentListToCSVToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
      this.exportCurrentListToCSVToolStripMenuItem.Text = "Export Current List to CSV";
      this.exportCurrentListToCSVToolStripMenuItem.Click += new System.EventHandler(this.CallbackExportListViewToCsvReport);
      // 
      // exportCurrentListToExcelToolStripMenuItem
      // 
      this.exportCurrentListToExcelToolStripMenuItem.Name = "exportCurrentListToExcelToolStripMenuItem";
      this.exportCurrentListToExcelToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
      this.exportCurrentListToExcelToolStripMenuItem.Text = "Export Current List to Excel";
      this.exportCurrentListToExcelToolStripMenuItem.Click += new System.EventHandler(this.CallbackExportListViewToExcelReport);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(177, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.exitToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
      this.preferencesToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.preferencesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
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
            this.toolStripSeparator4,
            this.crawlParentDirectoriesToolStripMenuItem,
            this.crawlChildDirectoriesToolStripMenuItem,
            this.toolStripSeparator5,
            this.customFiltersToolStripMenuItem,
            this.toolStripSeparator7,
            this.dataExtractorsToolStripMenuItem,
            this.toolStripSeparator6,
            this.clearHTTPAuthenticationToolStripMenuItem});
      this.taskParametersToolStripMenuItem.Name = "taskParametersToolStripMenuItem";
      this.taskParametersToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
      this.taskParametersToolStripMenuItem.Text = "Task Parameters";
      // 
      // includeURLPatternsToolStripMenuItem
      // 
      this.includeURLPatternsToolStripMenuItem.Name = "includeURLPatternsToolStripMenuItem";
      this.includeURLPatternsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.includeURLPatternsToolStripMenuItem.Text = "Include URL Patterns";
      this.includeURLPatternsToolStripMenuItem.Click += new System.EventHandler(this.CallbackIncludeUrlItemsClick);
      // 
      // excludeURLPatternsToolStripMenuItem
      // 
      this.excludeURLPatternsToolStripMenuItem.Name = "excludeURLPatternsToolStripMenuItem";
      this.excludeURLPatternsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.excludeURLPatternsToolStripMenuItem.Text = "Exclude URL Patterns";
      this.excludeURLPatternsToolStripMenuItem.Click += new System.EventHandler(this.CallbackExcludeUrlItemsClick);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(213, 6);
      // 
      // crawlParentDirectoriesToolStripMenuItem
      // 
      this.crawlParentDirectoriesToolStripMenuItem.Checked = true;
      this.crawlParentDirectoriesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.crawlParentDirectoriesToolStripMenuItem.Name = "crawlParentDirectoriesToolStripMenuItem";
      this.crawlParentDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.crawlParentDirectoriesToolStripMenuItem.Text = "Crawl Parent Directories";
      this.crawlParentDirectoriesToolStripMenuItem.Click += new System.EventHandler(this.CallbackCrawlParentDirectoriesToolStripMenuItemClick);
      // 
      // crawlChildDirectoriesToolStripMenuItem
      // 
      this.crawlChildDirectoriesToolStripMenuItem.Checked = true;
      this.crawlChildDirectoriesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.crawlChildDirectoriesToolStripMenuItem.Name = "crawlChildDirectoriesToolStripMenuItem";
      this.crawlChildDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.crawlChildDirectoriesToolStripMenuItem.Text = "Crawl Child Directories";
      this.crawlChildDirectoriesToolStripMenuItem.Click += new System.EventHandler(this.CallbackCrawlChildDirectoriesToolStripMenuItemClick);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(213, 6);
      // 
      // customFiltersToolStripMenuItem
      // 
      this.customFiltersToolStripMenuItem.Name = "customFiltersToolStripMenuItem";
      this.customFiltersToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.customFiltersToolStripMenuItem.Text = "Custom Filters";
      this.customFiltersToolStripMenuItem.Click += new System.EventHandler(this.CallbackCustomFilterClick);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(213, 6);
      // 
      // dataExtractorsToolStripMenuItem
      // 
      this.dataExtractorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSSSelectorsToolStripMenuItem,
            this.regularExpressionsToolStripMenuItem,
            this.xPathExpressionsToolStripMenuItem});
      this.dataExtractorsToolStripMenuItem.Name = "dataExtractorsToolStripMenuItem";
      this.dataExtractorsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.dataExtractorsToolStripMenuItem.Text = "Data Extractors";
      // 
      // cSSSelectorsToolStripMenuItem
      // 
      this.cSSSelectorsToolStripMenuItem.Name = "cSSSelectorsToolStripMenuItem";
      this.cSSSelectorsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
      this.cSSSelectorsToolStripMenuItem.Text = "CSS Selectors";
      this.cSSSelectorsToolStripMenuItem.Click += new System.EventHandler(this.CallbackDataExtractorsCssSelectorsClick);
      // 
      // regularExpressionsToolStripMenuItem
      // 
      this.regularExpressionsToolStripMenuItem.Name = "regularExpressionsToolStripMenuItem";
      this.regularExpressionsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
      this.regularExpressionsToolStripMenuItem.Text = "Regular Expressions";
      this.regularExpressionsToolStripMenuItem.Click += new System.EventHandler(this.CallbackDataExtractorsRegularExpressionsClick);
      // 
      // xPathExpressionsToolStripMenuItem
      // 
      this.xPathExpressionsToolStripMenuItem.Name = "xPathExpressionsToolStripMenuItem";
      this.xPathExpressionsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
      this.xPathExpressionsToolStripMenuItem.Text = "XPath Expressions";
      this.xPathExpressionsToolStripMenuItem.Click += new System.EventHandler(this.CallbackDataExtractorsXpathsClick);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(213, 6);
      // 
      // clearHTTPAuthenticationToolStripMenuItem
      // 
      this.clearHTTPAuthenticationToolStripMenuItem.Name = "clearHTTPAuthenticationToolStripMenuItem";
      this.clearHTTPAuthenticationToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.clearHTTPAuthenticationToolStripMenuItem.Text = "Clear HTTP Authentication";
      this.clearHTTPAuthenticationToolStripMenuItem.Click += new System.EventHandler(this.CallbackClearHTTPAuthenticationToolStripMenuItemClick);
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // reportsToolStripMenuItem
      // 
      this.reportsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveOverviewExcelReportToolStripMenuItem,
            this.toolStripSeparator11,
            this.saveURIAnalysisExcelReportToolStripMenuItem,
            this.redirectsExcelReportToolStripMenuItem,
            this.brokenLinksExcelReportToolStripMenuItem,
            this.errorsExcelReportToolStripMenuItem,
            this.robotsReportToolStripMenuItem,
            this.sitemapErrorsToolStripMenuItem,
            this.remarksExcelReportToolStripMenuItem,
            this.toolStripSeparator14,
            this.pageMetadataExcelReportToolStripMenuItem,
            this.generatePageContentsExcelReportToolStripMenuItem,
            this.generateHrefLangExcelReportToolStripMenuItem,
            this.duplicateContentExcelReportToolStripMenuItem,
            this.keywordAnalysisExcelReportToolStripMenuItem,
            this.toolStripSeparator13,
            this.contactDetailsExcelReportToolStripMenuItem,
            this.toolStripSeparator12,
            this.customFiltersExcelReportToolStripMenuItem,
            this.dataExtractorsExcelReportToolStripMenuItem});
      this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
      this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
      this.reportsToolStripMenuItem.Text = "Reports";
      // 
      // saveOverviewExcelReportToolStripMenuItem
      // 
      this.saveOverviewExcelReportToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.saveOverviewExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem,
            this.cSVReportToolStripMenuItem});
      this.saveOverviewExcelReportToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.saveOverviewExcelReportToolStripMenuItem.Name = "saveOverviewExcelReportToolStripMenuItem";
      this.saveOverviewExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.saveOverviewExcelReportToolStripMenuItem.Text = "Overview Report";
      // 
      // combinedExcelReportToolStripMenuItem
      // 
      this.combinedExcelReportToolStripMenuItem.Name = "combinedExcelReportToolStripMenuItem";
      this.combinedExcelReportToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.combinedExcelReportToolStripMenuItem.Text = "Excel Report";
      this.combinedExcelReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveOverviewExcelReport);
      // 
      // cSVReportToolStripMenuItem
      // 
      this.cSVReportToolStripMenuItem.Name = "cSVReportToolStripMenuItem";
      this.cSVReportToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
      this.cSVReportToolStripMenuItem.Text = "CSV Report";
      this.cSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveOverviewCsvReport);
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(230, 6);
      // 
      // saveURIAnalysisExcelReportToolStripMenuItem
      // 
      this.saveURIAnalysisExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem3,
            this.toolStripSeparator29,
            this.linksCSVReportToolStripMenuItem,
            this.hyperlinksCSVReportToolStripMenuItem,
            this.uRIsCSVReportToolStripMenuItem,
            this.orphanedPagesCSVReportToolStripMenuItem});
      this.saveURIAnalysisExcelReportToolStripMenuItem.Name = "saveURIAnalysisExcelReportToolStripMenuItem";
      this.saveURIAnalysisExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.saveURIAnalysisExcelReportToolStripMenuItem.Text = "URI Analysis Report";
      // 
      // combinedExcelReportToolStripMenuItem3
      // 
      this.combinedExcelReportToolStripMenuItem3.Name = "combinedExcelReportToolStripMenuItem3";
      this.combinedExcelReportToolStripMenuItem3.Size = new System.Drawing.Size(223, 22);
      this.combinedExcelReportToolStripMenuItem3.Text = "Combined Excel Report";
      this.combinedExcelReportToolStripMenuItem3.Click += new System.EventHandler(this.CallbackSaveUriAnalysisExcelReport);
      // 
      // toolStripSeparator29
      // 
      this.toolStripSeparator29.Name = "toolStripSeparator29";
      this.toolStripSeparator29.Size = new System.Drawing.Size(220, 6);
      // 
      // linksCSVReportToolStripMenuItem
      // 
      this.linksCSVReportToolStripMenuItem.Name = "linksCSVReportToolStripMenuItem";
      this.linksCSVReportToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.linksCSVReportToolStripMenuItem.Text = "Links CSV Report";
      this.linksCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveUriAnalysisCsvReportLinks);
      // 
      // hyperlinksCSVReportToolStripMenuItem
      // 
      this.hyperlinksCSVReportToolStripMenuItem.Name = "hyperlinksCSVReportToolStripMenuItem";
      this.hyperlinksCSVReportToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.hyperlinksCSVReportToolStripMenuItem.Text = "Hyperlinks CSV Report";
      this.hyperlinksCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveUriAnalysisCsvReportHyperlinks);
      // 
      // uRIsCSVReportToolStripMenuItem
      // 
      this.uRIsCSVReportToolStripMenuItem.Name = "uRIsCSVReportToolStripMenuItem";
      this.uRIsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.uRIsCSVReportToolStripMenuItem.Text = "URI Analysis CSV Report";
      this.uRIsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveUriAnalysisCsvReportUris);
      // 
      // orphanedPagesCSVReportToolStripMenuItem
      // 
      this.orphanedPagesCSVReportToolStripMenuItem.Name = "orphanedPagesCSVReportToolStripMenuItem";
      this.orphanedPagesCSVReportToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
      this.orphanedPagesCSVReportToolStripMenuItem.Text = "Orphaned Pages CSV Report";
      this.orphanedPagesCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveUriAnalysisCsvReportOrphanedPages);
      // 
      // redirectsExcelReportToolStripMenuItem
      // 
      this.redirectsExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem4,
            this.toolStripSeparator21,
            this.redirectsAuditCSVReportToolStripMenuItem1,
            this.redirectChainsCSVReportToolStripMenuItem});
      this.redirectsExcelReportToolStripMenuItem.Name = "redirectsExcelReportToolStripMenuItem";
      this.redirectsExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.redirectsExcelReportToolStripMenuItem.Text = "Redirects Report";
      // 
      // combinedExcelReportToolStripMenuItem4
      // 
      this.combinedExcelReportToolStripMenuItem4.Name = "combinedExcelReportToolStripMenuItem4";
      this.combinedExcelReportToolStripMenuItem4.Size = new System.Drawing.Size(218, 22);
      this.combinedExcelReportToolStripMenuItem4.Text = "Excel Report";
      this.combinedExcelReportToolStripMenuItem4.Click += new System.EventHandler(this.CallbackSaveRedirectsExcelReport);
      // 
      // toolStripSeparator21
      // 
      this.toolStripSeparator21.Name = "toolStripSeparator21";
      this.toolStripSeparator21.Size = new System.Drawing.Size(215, 6);
      // 
      // redirectsAuditCSVReportToolStripMenuItem1
      // 
      this.redirectsAuditCSVReportToolStripMenuItem1.Name = "redirectsAuditCSVReportToolStripMenuItem1";
      this.redirectsAuditCSVReportToolStripMenuItem1.Size = new System.Drawing.Size(218, 22);
      this.redirectsAuditCSVReportToolStripMenuItem1.Text = "Redirects Audit CSV Report";
      this.redirectsAuditCSVReportToolStripMenuItem1.Click += new System.EventHandler(this.CallbackSaveRedirectsCsvReportRedirectsAudit);
      // 
      // redirectChainsCSVReportToolStripMenuItem
      // 
      this.redirectChainsCSVReportToolStripMenuItem.Name = "redirectChainsCSVReportToolStripMenuItem";
      this.redirectChainsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
      this.redirectChainsCSVReportToolStripMenuItem.Text = "Redirect Chains CSV Report";
      this.redirectChainsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveRedirectsCsvReportRedirectChains);
      // 
      // brokenLinksExcelReportToolStripMenuItem
      // 
      this.brokenLinksExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem7,
            this.toolStripSeparator22,
            this.brokenLinksCSVReportToolStripMenuItem,
            this.goodLinksCSVReportToolStripMenuItem,
            this.redirectedLinksCSVReportToolStripMenuItem});
      this.brokenLinksExcelReportToolStripMenuItem.Name = "brokenLinksExcelReportToolStripMenuItem";
      this.brokenLinksExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.brokenLinksExcelReportToolStripMenuItem.Text = "Broken Links Report";
      // 
      // combinedExcelReportToolStripMenuItem7
      // 
      this.combinedExcelReportToolStripMenuItem7.Name = "combinedExcelReportToolStripMenuItem7";
      this.combinedExcelReportToolStripMenuItem7.Size = new System.Drawing.Size(222, 22);
      this.combinedExcelReportToolStripMenuItem7.Text = "Combined Excel Report";
      this.combinedExcelReportToolStripMenuItem7.Click += new System.EventHandler(this.CallbackSaveBrokenLinksExcelReport);
      // 
      // toolStripSeparator22
      // 
      this.toolStripSeparator22.Name = "toolStripSeparator22";
      this.toolStripSeparator22.Size = new System.Drawing.Size(219, 6);
      // 
      // brokenLinksCSVReportToolStripMenuItem
      // 
      this.brokenLinksCSVReportToolStripMenuItem.Name = "brokenLinksCSVReportToolStripMenuItem";
      this.brokenLinksCSVReportToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
      this.brokenLinksCSVReportToolStripMenuItem.Text = "Broken Links CSV Report";
      this.brokenLinksCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveBrokenLinksCsvReportBrokenLinks);
      // 
      // goodLinksCSVReportToolStripMenuItem
      // 
      this.goodLinksCSVReportToolStripMenuItem.Name = "goodLinksCSVReportToolStripMenuItem";
      this.goodLinksCSVReportToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
      this.goodLinksCSVReportToolStripMenuItem.Text = "Good Links CSV Report";
      this.goodLinksCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveBrokenLinksCsvReportGoodLinks);
      // 
      // redirectedLinksCSVReportToolStripMenuItem
      // 
      this.redirectedLinksCSVReportToolStripMenuItem.Name = "redirectedLinksCSVReportToolStripMenuItem";
      this.redirectedLinksCSVReportToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
      this.redirectedLinksCSVReportToolStripMenuItem.Text = "Redirected Links CSV Report";
      this.redirectedLinksCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveBrokenLinksCsvReportRedirectedLinks);
      // 
      // errorsExcelReportToolStripMenuItem
      // 
      this.errorsExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem6,
            this.toolStripSeparator20,
            this.cSVReportToolStripMenuItem2});
      this.errorsExcelReportToolStripMenuItem.Name = "errorsExcelReportToolStripMenuItem";
      this.errorsExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.errorsExcelReportToolStripMenuItem.Text = "Errors Report";
      // 
      // combinedExcelReportToolStripMenuItem6
      // 
      this.combinedExcelReportToolStripMenuItem6.Name = "combinedExcelReportToolStripMenuItem6";
      this.combinedExcelReportToolStripMenuItem6.Size = new System.Drawing.Size(138, 22);
      this.combinedExcelReportToolStripMenuItem6.Text = "Excel Report";
      this.combinedExcelReportToolStripMenuItem6.Click += new System.EventHandler(this.CallbackSaveErrorsExcelReport);
      // 
      // toolStripSeparator20
      // 
      this.toolStripSeparator20.Name = "toolStripSeparator20";
      this.toolStripSeparator20.Size = new System.Drawing.Size(135, 6);
      // 
      // cSVReportToolStripMenuItem2
      // 
      this.cSVReportToolStripMenuItem2.Name = "cSVReportToolStripMenuItem2";
      this.cSVReportToolStripMenuItem2.Size = new System.Drawing.Size(138, 22);
      this.cSVReportToolStripMenuItem2.Text = "CSV Report";
      this.cSVReportToolStripMenuItem2.Click += new System.EventHandler(this.CallbackSaveErrorsCsvReport);
      // 
      // robotsReportToolStripMenuItem
      // 
      this.robotsReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelReportToolStripMenuItem3,
            this.toolStripSeparator27,
            this.cSVReportToolStripMenuItem4});
      this.robotsReportToolStripMenuItem.Name = "robotsReportToolStripMenuItem";
      this.robotsReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.robotsReportToolStripMenuItem.Text = "Robots Report";
      // 
      // excelReportToolStripMenuItem3
      // 
      this.excelReportToolStripMenuItem3.Name = "excelReportToolStripMenuItem3";
      this.excelReportToolStripMenuItem3.Size = new System.Drawing.Size(221, 22);
      this.excelReportToolStripMenuItem3.Text = "Combined Excel Report";
      this.excelReportToolStripMenuItem3.Click += new System.EventHandler(this.CallbackSaveRobotsExcelReport);
      // 
      // toolStripSeparator27
      // 
      this.toolStripSeparator27.Name = "toolStripSeparator27";
      this.toolStripSeparator27.Size = new System.Drawing.Size(218, 6);
      // 
      // cSVReportToolStripMenuItem4
      // 
      this.cSVReportToolStripMenuItem4.Name = "cSVReportToolStripMenuItem4";
      this.cSVReportToolStripMenuItem4.Size = new System.Drawing.Size(221, 22);
      this.cSVReportToolStripMenuItem4.Text = "Blocked Internal CSV Report";
      this.cSVReportToolStripMenuItem4.Click += new System.EventHandler(this.CallbackSaveRobotsCsvReport);
      // 
      // sitemapErrorsToolStripMenuItem
      // 
      this.sitemapErrorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelReportToolStripMenuItem4,
            this.toolStripSeparator28,
            this.cSVReportToolStripMenuItem5});
      this.sitemapErrorsToolStripMenuItem.Name = "sitemapErrorsToolStripMenuItem";
      this.sitemapErrorsToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.sitemapErrorsToolStripMenuItem.Text = "Sitemaps Report";
      // 
      // excelReportToolStripMenuItem4
      // 
      this.excelReportToolStripMenuItem4.Name = "excelReportToolStripMenuItem4";
      this.excelReportToolStripMenuItem4.Size = new System.Drawing.Size(239, 22);
      this.excelReportToolStripMenuItem4.Text = "Combined Excel Report";
      this.excelReportToolStripMenuItem4.Click += new System.EventHandler(this.CallbackSaveSitemapErrorsExcelReport);
      // 
      // toolStripSeparator28
      // 
      this.toolStripSeparator28.Name = "toolStripSeparator28";
      this.toolStripSeparator28.Size = new System.Drawing.Size(236, 6);
      // 
      // cSVReportToolStripMenuItem5
      // 
      this.cSVReportToolStripMenuItem5.Name = "cSVReportToolStripMenuItem5";
      this.cSVReportToolStripMenuItem5.Size = new System.Drawing.Size(239, 22);
      this.cSVReportToolStripMenuItem5.Text = "Sitemap XML Errors CSV Report";
      this.cSVReportToolStripMenuItem5.Click += new System.EventHandler(this.CallbackSaveSitemapErrorsCsvReport);
      // 
      // remarksExcelReportToolStripMenuItem
      // 
      this.remarksExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelReportToolStripMenuItem2,
            this.toolStripSeparator24,
            this.cSVReportToolStripMenuItem3});
      this.remarksExcelReportToolStripMenuItem.Name = "remarksExcelReportToolStripMenuItem";
      this.remarksExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.remarksExcelReportToolStripMenuItem.Text = "Remarks Report";
      // 
      // excelReportToolStripMenuItem2
      // 
      this.excelReportToolStripMenuItem2.Name = "excelReportToolStripMenuItem2";
      this.excelReportToolStripMenuItem2.Size = new System.Drawing.Size(138, 22);
      this.excelReportToolStripMenuItem2.Text = "Excel Report";
      this.excelReportToolStripMenuItem2.Click += new System.EventHandler(this.CallbackSaveRemarksExcelReport);
      // 
      // toolStripSeparator24
      // 
      this.toolStripSeparator24.Name = "toolStripSeparator24";
      this.toolStripSeparator24.Size = new System.Drawing.Size(135, 6);
      // 
      // cSVReportToolStripMenuItem3
      // 
      this.cSVReportToolStripMenuItem3.Name = "cSVReportToolStripMenuItem3";
      this.cSVReportToolStripMenuItem3.Size = new System.Drawing.Size(138, 22);
      this.cSVReportToolStripMenuItem3.Text = "CSV Report";
      this.cSVReportToolStripMenuItem3.Click += new System.EventHandler(this.CallbackSaveRemarksCsvReport);
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(230, 6);
      // 
      // pageMetadataExcelReportToolStripMenuItem
      // 
      this.pageMetadataExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelReportToolStripMenuItem,
            this.toolStripSeparator19,
            this.titlesCSVReportToolStripMenuItem,
            this.descriptionsCSVReportToolStripMenuItem,
            this.keywordsCSVReportToolStripMenuItem});
      this.pageMetadataExcelReportToolStripMenuItem.Name = "pageMetadataExcelReportToolStripMenuItem";
      this.pageMetadataExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.pageMetadataExcelReportToolStripMenuItem.Text = "Page Metadata";
      // 
      // excelReportToolStripMenuItem
      // 
      this.excelReportToolStripMenuItem.Name = "excelReportToolStripMenuItem";
      this.excelReportToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
      this.excelReportToolStripMenuItem.Text = "Combined Excel Report";
      this.excelReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSavePageMetadataExcelReport);
      // 
      // toolStripSeparator19
      // 
      this.toolStripSeparator19.Name = "toolStripSeparator19";
      this.toolStripSeparator19.Size = new System.Drawing.Size(198, 6);
      // 
      // titlesCSVReportToolStripMenuItem
      // 
      this.titlesCSVReportToolStripMenuItem.Name = "titlesCSVReportToolStripMenuItem";
      this.titlesCSVReportToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
      this.titlesCSVReportToolStripMenuItem.Text = "Titles CSV Report";
      this.titlesCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSavePageMetadataCsvReportTitles);
      // 
      // descriptionsCSVReportToolStripMenuItem
      // 
      this.descriptionsCSVReportToolStripMenuItem.Name = "descriptionsCSVReportToolStripMenuItem";
      this.descriptionsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
      this.descriptionsCSVReportToolStripMenuItem.Text = "Descriptions CSV Report";
      this.descriptionsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSavePageMetadataCsvReportDescriptions);
      // 
      // keywordsCSVReportToolStripMenuItem
      // 
      this.keywordsCSVReportToolStripMenuItem.Name = "keywordsCSVReportToolStripMenuItem";
      this.keywordsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
      this.keywordsCSVReportToolStripMenuItem.Text = "Keywords CSV Report";
      this.keywordsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSavePageMetadataCsvReportKeywords);
      // 
      // generatePageContentsExcelReportToolStripMenuItem
      // 
      this.generatePageContentsExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem2,
            this.toolStripSeparator18,
            this.pageHeadingsCSVReportToolStripMenuItem,
            this.pageTextCSVReportToolStripMenuItem});
      this.generatePageContentsExcelReportToolStripMenuItem.Name = "generatePageContentsExcelReportToolStripMenuItem";
      this.generatePageContentsExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.generatePageContentsExcelReportToolStripMenuItem.Text = "Page Contents Report";
      // 
      // combinedExcelReportToolStripMenuItem2
      // 
      this.combinedExcelReportToolStripMenuItem2.Name = "combinedExcelReportToolStripMenuItem2";
      this.combinedExcelReportToolStripMenuItem2.Size = new System.Drawing.Size(215, 22);
      this.combinedExcelReportToolStripMenuItem2.Text = "Combined Excel Report";
      this.combinedExcelReportToolStripMenuItem2.Click += new System.EventHandler(this.CallbackSavePageContentsExcelReport);
      // 
      // toolStripSeparator18
      // 
      this.toolStripSeparator18.Name = "toolStripSeparator18";
      this.toolStripSeparator18.Size = new System.Drawing.Size(212, 6);
      // 
      // pageHeadingsCSVReportToolStripMenuItem
      // 
      this.pageHeadingsCSVReportToolStripMenuItem.Name = "pageHeadingsCSVReportToolStripMenuItem";
      this.pageHeadingsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
      this.pageHeadingsCSVReportToolStripMenuItem.Text = "Page Headings CSV Report";
      this.pageHeadingsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSavePageContentsCsvReportHeadings);
      // 
      // pageTextCSVReportToolStripMenuItem
      // 
      this.pageTextCSVReportToolStripMenuItem.Name = "pageTextCSVReportToolStripMenuItem";
      this.pageTextCSVReportToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
      this.pageTextCSVReportToolStripMenuItem.Text = "Page Text CSV Report";
      this.pageTextCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSavePageContentsCsvReportPageText);
      // 
      // generateHrefLangExcelReportToolStripMenuItem
      // 
      this.generateHrefLangExcelReportToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.generateHrefLangExcelReportToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.generateHrefLangExcelReportToolStripMenuItem.Name = "generateHrefLangExcelReportToolStripMenuItem";
      this.generateHrefLangExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.generateHrefLangExcelReportToolStripMenuItem.Text = "Languages Excel Report";
      this.generateHrefLangExcelReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveLanguagesExcelReport);
      // 
      // duplicateContentExcelReportToolStripMenuItem
      // 
      this.duplicateContentExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem5,
            this.toolStripSeparator17,
            this.titlesCSVReportToolStripMenuItem1,
            this.checksumsCSVReportToolStripMenuItem,
            this.duplicateETagsToolStripMenuItem,
            this.pagesCSVReportToolStripMenuItem});
      this.duplicateContentExcelReportToolStripMenuItem.Name = "duplicateContentExcelReportToolStripMenuItem";
      this.duplicateContentExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.duplicateContentExcelReportToolStripMenuItem.Text = "Duplicate Content Report";
      // 
      // combinedExcelReportToolStripMenuItem5
      // 
      this.combinedExcelReportToolStripMenuItem5.Name = "combinedExcelReportToolStripMenuItem5";
      this.combinedExcelReportToolStripMenuItem5.Size = new System.Drawing.Size(197, 22);
      this.combinedExcelReportToolStripMenuItem5.Text = "Combined Excel Report";
      this.combinedExcelReportToolStripMenuItem5.Click += new System.EventHandler(this.CallbackSaveDuplicateContentExcelReport);
      // 
      // toolStripSeparator17
      // 
      this.toolStripSeparator17.Name = "toolStripSeparator17";
      this.toolStripSeparator17.Size = new System.Drawing.Size(194, 6);
      // 
      // titlesCSVReportToolStripMenuItem1
      // 
      this.titlesCSVReportToolStripMenuItem1.Name = "titlesCSVReportToolStripMenuItem1";
      this.titlesCSVReportToolStripMenuItem1.Size = new System.Drawing.Size(197, 22);
      this.titlesCSVReportToolStripMenuItem1.Text = "Titles CSV Report";
      this.titlesCSVReportToolStripMenuItem1.Click += new System.EventHandler(this.CallbackSaveDuplicateContentCsvReportTitles);
      // 
      // checksumsCSVReportToolStripMenuItem
      // 
      this.checksumsCSVReportToolStripMenuItem.Name = "checksumsCSVReportToolStripMenuItem";
      this.checksumsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
      this.checksumsCSVReportToolStripMenuItem.Text = "Checksums CSV Report";
      this.checksumsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveDuplicateContentCsvReportChecksums);
      // 
      // duplicateETagsToolStripMenuItem
      // 
      this.duplicateETagsToolStripMenuItem.Name = "duplicateETagsToolStripMenuItem";
      this.duplicateETagsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
      this.duplicateETagsToolStripMenuItem.Text = "ETags CSV Report";
      this.duplicateETagsToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveDuplicateContentCsvReportEtags);
      // 
      // pagesCSVReportToolStripMenuItem
      // 
      this.pagesCSVReportToolStripMenuItem.Name = "pagesCSVReportToolStripMenuItem";
      this.pagesCSVReportToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
      this.pagesCSVReportToolStripMenuItem.Text = "Pages CSV Report";
      this.pagesCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveDuplicateContentCsvReportPages);
      // 
      // keywordAnalysisExcelReportToolStripMenuItem
      // 
      this.keywordAnalysisExcelReportToolStripMenuItem.Name = "keywordAnalysisExcelReportToolStripMenuItem";
      this.keywordAnalysisExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.keywordAnalysisExcelReportToolStripMenuItem.Text = "Keyword Analysis Excel Report";
      this.keywordAnalysisExcelReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveKeywordAnalysisExcelReport);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(230, 6);
      // 
      // contactDetailsExcelReportToolStripMenuItem
      // 
      this.contactDetailsExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem8,
            this.toolStripSeparator23,
            this.emailAddressesCSVReportToolStripMenuItem,
            this.telephoneNumbersCSVReportToolStripMenuItem});
      this.contactDetailsExcelReportToolStripMenuItem.Name = "contactDetailsExcelReportToolStripMenuItem";
      this.contactDetailsExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.contactDetailsExcelReportToolStripMenuItem.Text = "Contact Details Report";
      this.contactDetailsExcelReportToolStripMenuItem.ToolTipText = "Telephone number and email address lists";
      // 
      // combinedExcelReportToolStripMenuItem8
      // 
      this.combinedExcelReportToolStripMenuItem8.Name = "combinedExcelReportToolStripMenuItem8";
      this.combinedExcelReportToolStripMenuItem8.Size = new System.Drawing.Size(243, 22);
      this.combinedExcelReportToolStripMenuItem8.Text = "Combined Excel Report";
      this.combinedExcelReportToolStripMenuItem8.Click += new System.EventHandler(this.CallbackSaveContactDetailsExcelReport);
      // 
      // toolStripSeparator23
      // 
      this.toolStripSeparator23.Name = "toolStripSeparator23";
      this.toolStripSeparator23.Size = new System.Drawing.Size(240, 6);
      // 
      // emailAddressesCSVReportToolStripMenuItem
      // 
      this.emailAddressesCSVReportToolStripMenuItem.Name = "emailAddressesCSVReportToolStripMenuItem";
      this.emailAddressesCSVReportToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
      this.emailAddressesCSVReportToolStripMenuItem.Text = "Email Addresses CSV Report";
      this.emailAddressesCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveContactDetailsCsvReportEmail);
      // 
      // telephoneNumbersCSVReportToolStripMenuItem
      // 
      this.telephoneNumbersCSVReportToolStripMenuItem.Name = "telephoneNumbersCSVReportToolStripMenuItem";
      this.telephoneNumbersCSVReportToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
      this.telephoneNumbersCSVReportToolStripMenuItem.Text = "Telephone Numbers CSV Report";
      this.telephoneNumbersCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveContactDetailsCsvReportTelephone);
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(230, 6);
      // 
      // customFiltersExcelReportToolStripMenuItem
      // 
      this.customFiltersExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelReportToolStripMenuItem1,
            this.toolStripSeparator16,
            this.cSVReportToolStripMenuItem1});
      this.customFiltersExcelReportToolStripMenuItem.Name = "customFiltersExcelReportToolStripMenuItem";
      this.customFiltersExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.customFiltersExcelReportToolStripMenuItem.Text = "Custom Filters Report";
      // 
      // excelReportToolStripMenuItem1
      // 
      this.excelReportToolStripMenuItem1.Name = "excelReportToolStripMenuItem1";
      this.excelReportToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
      this.excelReportToolStripMenuItem1.Text = "Excel Report";
      this.excelReportToolStripMenuItem1.Click += new System.EventHandler(this.CallbackSaveCustomFilterExcelReport);
      // 
      // toolStripSeparator16
      // 
      this.toolStripSeparator16.Name = "toolStripSeparator16";
      this.toolStripSeparator16.Size = new System.Drawing.Size(135, 6);
      // 
      // cSVReportToolStripMenuItem1
      // 
      this.cSVReportToolStripMenuItem1.Name = "cSVReportToolStripMenuItem1";
      this.cSVReportToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
      this.cSVReportToolStripMenuItem1.Text = "CSV Report";
      this.cSVReportToolStripMenuItem1.Click += new System.EventHandler(this.CallbackSaveCustomFilterCsvReport);
      // 
      // dataExtractorsExcelReportToolStripMenuItem
      // 
      this.dataExtractorsExcelReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combinedExcelReportToolStripMenuItem1,
            this.toolStripSeparator15,
            this.cSSSelectorsCSVReportToolStripMenuItem,
            this.regularExpressionsCSVReportToolStripMenuItem,
            this.xPathExpressionsCSVReportToolStripMenuItem});
      this.dataExtractorsExcelReportToolStripMenuItem.Name = "dataExtractorsExcelReportToolStripMenuItem";
      this.dataExtractorsExcelReportToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
      this.dataExtractorsExcelReportToolStripMenuItem.Text = "Data Extractors Report";
      // 
      // combinedExcelReportToolStripMenuItem1
      // 
      this.combinedExcelReportToolStripMenuItem1.Name = "combinedExcelReportToolStripMenuItem1";
      this.combinedExcelReportToolStripMenuItem1.Size = new System.Drawing.Size(239, 22);
      this.combinedExcelReportToolStripMenuItem1.Text = "Combined Excel Report";
      this.combinedExcelReportToolStripMenuItem1.Click += new System.EventHandler(this.CallbackSaveDataExtractorsExcelReport);
      // 
      // toolStripSeparator15
      // 
      this.toolStripSeparator15.Name = "toolStripSeparator15";
      this.toolStripSeparator15.Size = new System.Drawing.Size(236, 6);
      // 
      // cSSSelectorsCSVReportToolStripMenuItem
      // 
      this.cSSSelectorsCSVReportToolStripMenuItem.Name = "cSSSelectorsCSVReportToolStripMenuItem";
      this.cSSSelectorsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
      this.cSSSelectorsCSVReportToolStripMenuItem.Text = "CSS Selectors CSV Report";
      this.cSSSelectorsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveDataExtractorsCsvReportCssSelectors);
      // 
      // regularExpressionsCSVReportToolStripMenuItem
      // 
      this.regularExpressionsCSVReportToolStripMenuItem.Name = "regularExpressionsCSVReportToolStripMenuItem";
      this.regularExpressionsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
      this.regularExpressionsCSVReportToolStripMenuItem.Text = "Regular Expressions CSV Report";
      this.regularExpressionsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveDataExtractorsCsvReportRegexes);
      // 
      // xPathExpressionsCSVReportToolStripMenuItem
      // 
      this.xPathExpressionsCSVReportToolStripMenuItem.Name = "xPathExpressionsCSVReportToolStripMenuItem";
      this.xPathExpressionsCSVReportToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
      this.xPathExpressionsCSVReportToolStripMenuItem.Text = "XPaths CSV Report";
      this.xPathExpressionsCSVReportToolStripMenuItem.Click += new System.EventHandler(this.CallbackSaveDataExtractorsCsvReportXpaths);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sEOMacroscopeBlogToolStripMenuItem,
            this.sEOMacroscopeOnGitHubToolStripMenuItem,
            this.sEOMacroscopeManualToolStripMenuItem,
            this.toolStripSeparator2,
            this.reportABugToolStripMenuItem,
            this.licenceToolStripMenuItem,
            this.toolStripSeparator3,
            this.aboutSEOMacroscopeToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // sEOMacroscopeBlogToolStripMenuItem
      // 
      this.sEOMacroscopeBlogToolStripMenuItem.Name = "sEOMacroscopeBlogToolStripMenuItem";
      this.sEOMacroscopeBlogToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.sEOMacroscopeBlogToolStripMenuItem.Text = "SEO Macroscope Blog";
      this.sEOMacroscopeBlogToolStripMenuItem.Click += new System.EventHandler(this.CallbackHelpBlogClick);
      // 
      // sEOMacroscopeOnGitHubToolStripMenuItem
      // 
      this.sEOMacroscopeOnGitHubToolStripMenuItem.Name = "sEOMacroscopeOnGitHubToolStripMenuItem";
      this.sEOMacroscopeOnGitHubToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.sEOMacroscopeOnGitHubToolStripMenuItem.Text = "SEO Macroscope on GitHub";
      this.sEOMacroscopeOnGitHubToolStripMenuItem.Click += new System.EventHandler(this.CallbackHelpGitHubClick);
      // 
      // sEOMacroscopeManualToolStripMenuItem
      // 
      this.sEOMacroscopeManualToolStripMenuItem.Name = "sEOMacroscopeManualToolStripMenuItem";
      this.sEOMacroscopeManualToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.sEOMacroscopeManualToolStripMenuItem.Text = "SEO Macroscope Online Manual";
      this.sEOMacroscopeManualToolStripMenuItem.Click += new System.EventHandler(this.CallbackHelpManualClick);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(241, 6);
      // 
      // reportABugToolStripMenuItem
      // 
      this.reportABugToolStripMenuItem.Name = "reportABugToolStripMenuItem";
      this.reportABugToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.reportABugToolStripMenuItem.Text = "Report a Bug";
      this.reportABugToolStripMenuItem.Click += new System.EventHandler(this.CallbackHelpReportBugClick);
      // 
      // licenceToolStripMenuItem
      // 
      this.licenceToolStripMenuItem.Name = "licenceToolStripMenuItem";
      this.licenceToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.licenceToolStripMenuItem.Text = "Licence";
      this.licenceToolStripMenuItem.Click += new System.EventHandler(this.CallbackHelpLicenceClick);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(241, 6);
      // 
      // aboutSEOMacroscopeToolStripMenuItem
      // 
      this.aboutSEOMacroscopeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.aboutSEOMacroscopeToolStripMenuItem.Name = "aboutSEOMacroscopeToolStripMenuItem";
      this.aboutSEOMacroscopeToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.aboutSEOMacroscopeToolStripMenuItem.Text = "About SEO Macroscope";
      this.aboutSEOMacroscopeToolStripMenuItem.Click += new System.EventHandler(this.CallbackHelpAboutClick);
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
      this.tableLayoutPanelMainContainer.Size = new System.Drawing.Size(1008, 706);
      this.tableLayoutPanelMainContainer.TabIndex = 2;
      // 
      // statusStripMain
      // 
      this.statusStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripThreads,
            this.toolStripUrlCount,
            this.toolStripFound});
      this.statusStripMain.Location = new System.Drawing.Point(0, 676);
      this.statusStripMain.Name = "statusStripMain";
      this.statusStripMain.Size = new System.Drawing.Size(1008, 30);
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
            this.toolStripButtonRetryBrokenLinks,
            this.toolStripButtonRetryTimedOutLinks,
            this.toolStripSeparator25,
            this.toolStripButtonRecalculateStatistics,
            this.toolStripButtonRecalculateClickPaths});
      this.toolStripViewControls.Location = new System.Drawing.Point(0, 30);
      this.toolStripViewControls.Name = "toolStripViewControls";
      this.toolStripViewControls.Size = new System.Drawing.Size(1008, 30);
      this.toolStripViewControls.TabIndex = 5;
      this.toolStripViewControls.Text = "toolStrip1";
      // 
      // toolStripButtonRetryBrokenLinks
      // 
      this.toolStripButtonRetryBrokenLinks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButtonRetryBrokenLinks.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonRetryBrokenLinks.Name = "toolStripButtonRetryBrokenLinks";
      this.toolStripButtonRetryBrokenLinks.Size = new System.Drawing.Size(108, 27);
      this.toolStripButtonRetryBrokenLinks.Text = "Retry Broken Links";
      // 
      // toolStripButtonRetryTimedOutLinks
      // 
      this.toolStripButtonRetryTimedOutLinks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButtonRetryTimedOutLinks.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRetryTimedOutLinks.Image")));
      this.toolStripButtonRetryTimedOutLinks.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonRetryTimedOutLinks.Name = "toolStripButtonRetryTimedOutLinks";
      this.toolStripButtonRetryTimedOutLinks.Size = new System.Drawing.Size(128, 27);
      this.toolStripButtonRetryTimedOutLinks.Text = "Retry Timed Out Links";
      // 
      // toolStripSeparator25
      // 
      this.toolStripSeparator25.Name = "toolStripSeparator25";
      this.toolStripSeparator25.Size = new System.Drawing.Size(6, 30);
      // 
      // toolStripButtonRecalculateStatistics
      // 
      this.toolStripButtonRecalculateStatistics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButtonRecalculateStatistics.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRecalculateStatistics.Image")));
      this.toolStripButtonRecalculateStatistics.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonRecalculateStatistics.Name = "toolStripButtonRecalculateStatistics";
      this.toolStripButtonRecalculateStatistics.Size = new System.Drawing.Size(120, 27);
      this.toolStripButtonRecalculateStatistics.Text = "Recalculate Statistics";
      // 
      // toolStripButtonRecalculateClickPaths
      // 
      this.toolStripButtonRecalculateClickPaths.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolStripButtonRecalculateClickPaths.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRecalculateClickPaths.Image")));
      this.toolStripButtonRecalculateClickPaths.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonRecalculateClickPaths.Name = "toolStripButtonRecalculateClickPaths";
      this.toolStripButtonRecalculateClickPaths.Size = new System.Drawing.Size(132, 27);
      this.toolStripButtonRecalculateClickPaths.Text = "Recalculate Click Paths";
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
      this.toolStripExecuteControls.Size = new System.Drawing.Size(1008, 30);
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
      this.ButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.ButtonStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ButtonStart.Name = "ButtonStart";
      this.ButtonStart.Size = new System.Drawing.Size(35, 27);
      this.ButtonStart.Text = "Start";
      this.ButtonStart.ToolTipText = "Start scanning";
      // 
      // ButtonStop
      // 
      this.ButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.ButtonStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ButtonStop.Name = "ButtonStop";
      this.ButtonStop.Size = new System.Drawing.Size(35, 27);
      this.ButtonStop.Text = "Stop";
      this.ButtonStop.ToolTipText = "Stop scanning";
      // 
      // ButtonReset
      // 
      this.ButtonReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.ButtonReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ButtonReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ButtonReset.Name = "ButtonReset";
      this.ButtonReset.Size = new System.Drawing.Size(39, 27);
      this.ButtonReset.Text = "Reset";
      this.ButtonReset.ToolTipText = "Reset all scan results";
      // 
      // ProgressBarScan
      // 
      this.ProgressBarScan.CausesValidation = false;
      this.ProgressBarScan.Margin = new System.Windows.Forms.Padding(10, 8, 10, 8);
      this.ProgressBarScan.Name = "ProgressBarScan";
      this.ProgressBarScan.Size = new System.Drawing.Size(200, 14);
      this.ProgressBarScan.Step = 1;
      // 
      // splitContainerLeftAndRightViews
      // 
      this.splitContainerLeftAndRightViews.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerLeftAndRightViews.Location = new System.Drawing.Point(0, 60);
      this.splitContainerLeftAndRightViews.Margin = new System.Windows.Forms.Padding(0);
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
      this.splitContainerLeftAndRightViews.Size = new System.Drawing.Size(1008, 616);
      this.splitContainerLeftAndRightViews.SplitterDistance = 712;
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
      this.splitContainerStructureAndDocumentDetails.Size = new System.Drawing.Size(712, 616);
      this.splitContainerStructureAndDocumentDetails.SplitterDistance = 301;
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
      this.ClientSize = new System.Drawing.Size(1008, 730);
      this.Controls.Add(this.tableLayoutPanelMainContainer);
      this.Controls.Add(this.menuStripMain);
      this.DoubleBuffered = true;
      this.Icon = global::SEOMacroscope.Resources.SEO_Macroscope_Icon_32x32;
      this.MainMenuStrip = this.menuStripMain;
      this.MinimumSize = new System.Drawing.Size(1024, 758);
      this.Name = "MacroscopeMainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SEO Macroscope";
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

    private System.Windows.Forms.ToolStripMenuItem robotsReportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem excelReportToolStripMenuItem3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
    private System.Windows.Forms.ToolStripMenuItem cSVReportToolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem sitemapErrorsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem excelReportToolStripMenuItem4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
    private System.Windows.Forms.ToolStripMenuItem cSVReportToolStripMenuItem5;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
    private System.Windows.Forms.ToolStripMenuItem orphanedPagesCSVReportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redirectsAuditCSVReportToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem redirectChainsCSVReportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadSessionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveSessionAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
  }
}
