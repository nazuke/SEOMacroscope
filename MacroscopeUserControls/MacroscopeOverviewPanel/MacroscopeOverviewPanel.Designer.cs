/*
 * Created by SharpDevelop.
 * User: jholland
 * Date: 1/20/2017
 * Time: 18:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SEOMacroscope
{
	partial class MacroscopeOverviewPanel
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		public System.Windows.Forms.TabControl tabControlMain;
		public System.Windows.Forms.TabPage tabPageStructureOverview;
		public System.Windows.Forms.ListView listViewStructure;
		public System.Windows.Forms.TabPage tabPageHostnames;
		public System.Windows.Forms.ListView listViewHostnames;
		private System.Windows.Forms.ColumnHeader columnHeaderHostnameName;
		private System.Windows.Forms.ColumnHeader columnHeaderHostnameCount;
		public System.Windows.Forms.TabPage tabPageHierarchy;
		public System.Windows.Forms.TreeView treeViewHierarchy;
		public System.Windows.Forms.TabPage tabPageCanonicalAnalysis;
		public System.Windows.Forms.ListView listViewCanonicalAnalysis;
		private System.Windows.Forms.ColumnHeader CanonicalAnalysisUrl;
		private System.Windows.Forms.ColumnHeader CanonicalAnalysisCanonical;
		public System.Windows.Forms.TabPage tabPageHrefLangAnalysis;
		public System.Windows.Forms.ListView listViewHrefLang;
		public System.Windows.Forms.TabPage tabPageRedirectsAudit;
		public System.Windows.Forms.ListView listViewRedirectsAudit;
		private System.Windows.Forms.ColumnHeader RedirectsAuditOriginUrl;
		private System.Windows.Forms.ColumnHeader RedirectsAuditStatusCode;
		private System.Windows.Forms.ColumnHeader RedirectsAuditDestinationUrl;
		public System.Windows.Forms.TabPage tabPageUriAnalysis;
		public System.Windows.Forms.TabPage tabPagePageTitles;
		public System.Windows.Forms.ListView listViewPageTitles;
		public System.Windows.Forms.ColumnHeader columnHeaderUrl;
		public System.Windows.Forms.ColumnHeader columnHeaderCount;
		public System.Windows.Forms.ColumnHeader columnHeaderPageTitle;
		public System.Windows.Forms.ColumnHeader columnHeaderLength;
		public System.Windows.Forms.ColumnHeader columnHeaderPixelWidth;
		public System.Windows.Forms.TabPage tabPagePageDescriptions;
		public System.Windows.Forms.TabPage tabPagePageKeywords;
		public System.Windows.Forms.TabPage tabPagePageHeadings;
		public System.Windows.Forms.TabPage tabPageEmailAddresses;
		public System.Windows.Forms.ListView listViewEmailAddresses;
		private System.Windows.Forms.ColumnHeader EmailAddressesEmail;
		private System.Windows.Forms.ColumnHeader EmailAddressesUrl;
		public System.Windows.Forms.TabPage tabPageTelephoneNumbers;
		public System.Windows.Forms.ListView listViewTelephoneNumbers;
		private System.Windows.Forms.ColumnHeader TelTel;
		private System.Windows.Forms.ColumnHeader TelUrl;
		public System.Windows.Forms.TabPage tabPageHistory;
		public System.Windows.Forms.ListView listViewHistory;
		private System.Windows.Forms.ColumnHeader HistoryUrl;
		private System.Windows.Forms.ColumnHeader HistoryVisited;
		public System.Windows.Forms.ListView listViewUriAnalysis;
		public System.Windows.Forms.ListView listViewPageDescriptions;
		public System.Windows.Forms.ListView listViewPageKeywords;
		public System.Windows.Forms.ListView listViewPageHeadings;
		private System.Windows.Forms.ColumnHeader columnHeaderDescriptionUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderDescriptionCount;
		private System.Windows.Forms.ColumnHeader columnHeaderDescriptionDescription;
		private System.Windows.Forms.ColumnHeader columnHeaderDescriptionLength;
		private System.Windows.Forms.ColumnHeader columnHeaderKeywordsUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderKeywordsCount;
		private System.Windows.Forms.ColumnHeader columnHeaderKeywordsKeywords;
		private System.Windows.Forms.ColumnHeader columnHeaderKeywordsLength;
		private System.Windows.Forms.ColumnHeader columnHeaderKeywordsNumber;
		private System.Windows.Forms.TabPage tabPageErrors;
		public System.Windows.Forms.ListView listViewErrors;
		private System.Windows.Forms.ColumnHeader columnHeaderErrorsUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderErrorsStatusCode;
		private System.Windows.Forms.ColumnHeader columnHeaderHostnameInternal;
		private System.Windows.Forms.ColumnHeader columnHeaderErrorsDescription;
		public System.Windows.Forms.ContextMenuStrip contextMenuStripStructure;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenInBrowser;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemResetEntry;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddHostToAllowedHosts;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRemoveFromAllowedHosts;
		private System.Windows.Forms.ColumnHeader columnHeaderHeadingsUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderHeadingsOrder;
		private System.Windows.Forms.ColumnHeader columnHeaderHeadingsH1;
		private System.Windows.Forms.ColumnHeader columnHeaderHeadingsH2;
		private System.Windows.Forms.ColumnHeader columnHeaderHeadingsH3;
		private System.Windows.Forms.ColumnHeader columnHeaderHeadingsH4;
		private System.Windows.Forms.ColumnHeader columnHeaderHeadingsH5;
		private System.Windows.Forms.ColumnHeader columnHeaderHeadingsH6;
		public System.Windows.Forms.TabPage tabPageRobots;
		public System.Windows.Forms.ListView listViewRobots;
		private System.Windows.Forms.ColumnHeader columnHeaderRobots;
		private System.Windows.Forms.ColumnHeader columnHeaderRobotsBlocked;
		public System.Windows.Forms.TabPage tabPageSitemaps;
		public System.Windows.Forms.ListView listViewSitemaps;
		private System.Windows.Forms.ColumnHeader columnHeaderSitemapUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderSitemapLinks;
		public System.Windows.Forms.TabPage tabPageStylesheets;
		public System.Windows.Forms.TabPage tabPageImages;
		public System.Windows.Forms.TabPage tabPageJavascripts;
		public System.Windows.Forms.ListView listViewImages;
		public System.Windows.Forms.ListView listViewStylesheets;
		public System.Windows.Forms.ListView listViewJavascripts;
		private System.Windows.Forms.ColumnHeader columnHeaderImagesUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderImagesMimeType;
		private System.Windows.Forms.ColumnHeader columnHeaderImagesStatusCode;
		private System.Windows.Forms.ColumnHeader columnHeaderImagesFileSize;
		private System.Windows.Forms.ColumnHeader columnHeaderJavascriptsUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderJavascriptsStatusCode;
		private System.Windows.Forms.ColumnHeader columnHeaderJavascriptsMimeType;
		private System.Windows.Forms.ColumnHeader columnHeaderJavascriptsFileSize;
		private System.Windows.Forms.ColumnHeader columnHeaderStylesheetsUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderStylesheetsStatusCode;
		private System.Windows.Forms.ColumnHeader columnHeaderStylesheetsMimeType;
		private System.Windows.Forms.ColumnHeader columnHeaderStylesheetsFileSize;
		public System.Windows.Forms.TabPage tabPageVideos;
		public System.Windows.Forms.ListView listViewVideos;
		public System.Windows.Forms.ColumnHeader columnHeaderVideosUrl;
		public System.Windows.Forms.ColumnHeader columnHeaderVideosMimeType;
		public System.Windows.Forms.ColumnHeader columnHeaderVideosStatusCode;
		public System.Windows.Forms.ColumnHeader columnHeaderVideosFileSize;
		public System.Windows.Forms.TabPage tabPageAudios;
		public System.Windows.Forms.ListView listViewAudios;
		public System.Windows.Forms.ColumnHeader columnHeaderAudiosUrl;
		public System.Windows.Forms.ColumnHeader columnHeaderAudiosMimeType;
		public System.Windows.Forms.ColumnHeader columnHeaderAudiosStatusCode;
		public System.Windows.Forms.ColumnHeader columnHeaderAudiosFileSize;
		private System.Windows.Forms.ColumnHeader RedirectsAuditUrl;
		public System.Windows.Forms.ToolStrip toolStripSearch;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelStructure;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		public System.Windows.Forms.ToolStripTextBox toolStripStructureSearchTextBoxSearch;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		public System.Windows.Forms.ToolStripLabel toolStripLabelStructureItems;
		public System.Windows.Forms.ToolStripButton toolStripStructureButtonShowAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		public System.Windows.Forms.TabPage tabPageSearch;
		public System.Windows.Forms.ListView listViewSearchCollection;
		public System.Windows.Forms.ToolStrip toolStripSearchCollection;
		public System.Windows.Forms.ToolStripButton toolStripSearchCollectionButtonClear;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		public System.Windows.Forms.ToolStripTextBox toolStripSearchCollectionTextBoxSearch;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		public System.Windows.Forms.ToolStripLabel toolStripSearchCollectionDocumentsNumber;
		private System.Windows.Forms.ColumnHeader columnHeaderSearchCollectionUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderSearchCollectionPageTitle;
		private System.Windows.Forms.ColumnHeader columnHeaderSearchCollectionPageDescription;
		private System.Windows.Forms.ColumnHeader columnHeaderSearchCollectionPageKeywords;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelSearchCollection;
		public System.Windows.Forms.ToolStripDropDownButton toolStripSearchCollectionFilterMenu;
		private System.Windows.Forms.ToolStripMenuItem allDocumentTypesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HtmlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stylesheetsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem javaScriptsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imagesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PdfsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miscellaneousToolStripMenuItem;
		
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MacroscopeOverviewPanel));
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPageStructureOverview = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelStructure = new System.Windows.Forms.TableLayoutPanel();
			this.listViewStructure = new System.Windows.Forms.ListView();
			this.contextMenuStripStructure = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemOpenInBrowser = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemAddHostToAllowedHosts = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRemoveFromAllowedHosts = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemResetEntry = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSearch = new System.Windows.Forms.ToolStrip();
			this.toolStripSearchCollectionFilterMenu = new System.Windows.Forms.ToolStripDropDownButton();
			this.allDocumentTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HtmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stylesheetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.javaScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PdfsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miscellaneousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripStructureButtonShowAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripStructureSearchTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabelStructureItems = new System.Windows.Forms.ToolStripLabel();
			this.tabPageHierarchy = new System.Windows.Forms.TabPage();
			this.treeViewHierarchy = new System.Windows.Forms.TreeView();
			this.tabPageCanonicalAnalysis = new System.Windows.Forms.TabPage();
			this.listViewCanonicalAnalysis = new System.Windows.Forms.ListView();
			this.CanonicalAnalysisUrl = new System.Windows.Forms.ColumnHeader();
			this.CanonicalAnalysisCanonical = new System.Windows.Forms.ColumnHeader();
			this.tabPageHrefLangAnalysis = new System.Windows.Forms.TabPage();
			this.listViewHrefLang = new System.Windows.Forms.ListView();
			this.tabPageRedirectsAudit = new System.Windows.Forms.TabPage();
			this.listViewRedirectsAudit = new System.Windows.Forms.ListView();
			this.RedirectsAuditUrl = new System.Windows.Forms.ColumnHeader();
			this.RedirectsAuditStatusCode = new System.Windows.Forms.ColumnHeader();
			this.RedirectsAuditOriginUrl = new System.Windows.Forms.ColumnHeader();
			this.RedirectsAuditDestinationUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageErrors = new System.Windows.Forms.TabPage();
			this.listViewErrors = new System.Windows.Forms.ListView();
			this.columnHeaderErrorsUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderErrorsStatusCode = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderErrorsDescription = new System.Windows.Forms.ColumnHeader();
			this.tabPageHostnames = new System.Windows.Forms.TabPage();
			this.listViewHostnames = new System.Windows.Forms.ListView();
			this.columnHeaderHostnameName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHostnameCount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHostnameInternal = new System.Windows.Forms.ColumnHeader();
			this.tabPageUriAnalysis = new System.Windows.Forms.TabPage();
			this.listViewUriAnalysis = new System.Windows.Forms.ListView();
			this.tabPagePageTitles = new System.Windows.Forms.TabPage();
			this.listViewPageTitles = new System.Windows.Forms.ListView();
			this.columnHeaderUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderCount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderPageTitle = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLength = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderPixelWidth = new System.Windows.Forms.ColumnHeader();
			this.tabPagePageDescriptions = new System.Windows.Forms.TabPage();
			this.listViewPageDescriptions = new System.Windows.Forms.ListView();
			this.columnHeaderDescriptionUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderDescriptionCount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderDescriptionDescription = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderDescriptionLength = new System.Windows.Forms.ColumnHeader();
			this.tabPagePageKeywords = new System.Windows.Forms.TabPage();
			this.listViewPageKeywords = new System.Windows.Forms.ListView();
			this.columnHeaderKeywordsUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderKeywordsCount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderKeywordsKeywords = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderKeywordsLength = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderKeywordsNumber = new System.Windows.Forms.ColumnHeader();
			this.tabPagePageHeadings = new System.Windows.Forms.TabPage();
			this.listViewPageHeadings = new System.Windows.Forms.ListView();
			this.columnHeaderHeadingsUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHeadingsOrder = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHeadingsH1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHeadingsH2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHeadingsH3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHeadingsH4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHeadingsH5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHeadingsH6 = new System.Windows.Forms.ColumnHeader();
			this.tabPageStylesheets = new System.Windows.Forms.TabPage();
			this.listViewStylesheets = new System.Windows.Forms.ListView();
			this.columnHeaderStylesheetsUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderStylesheetsStatusCode = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderStylesheetsMimeType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderStylesheetsFileSize = new System.Windows.Forms.ColumnHeader();
			this.tabPageJavascripts = new System.Windows.Forms.TabPage();
			this.listViewJavascripts = new System.Windows.Forms.ListView();
			this.columnHeaderJavascriptsUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderJavascriptsStatusCode = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderJavascriptsMimeType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderJavascriptsFileSize = new System.Windows.Forms.ColumnHeader();
			this.tabPageImages = new System.Windows.Forms.TabPage();
			this.listViewImages = new System.Windows.Forms.ListView();
			this.columnHeaderImagesUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderImagesStatusCode = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderImagesMimeType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderImagesFileSize = new System.Windows.Forms.ColumnHeader();
			this.tabPageAudios = new System.Windows.Forms.TabPage();
			this.listViewAudios = new System.Windows.Forms.ListView();
			this.columnHeaderAudiosUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderAudiosStatusCode = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderAudiosMimeType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderAudiosFileSize = new System.Windows.Forms.ColumnHeader();
			this.tabPageVideos = new System.Windows.Forms.TabPage();
			this.listViewVideos = new System.Windows.Forms.ListView();
			this.columnHeaderVideosUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderVideosStatusCode = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderVideosMimeType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderVideosFileSize = new System.Windows.Forms.ColumnHeader();
			this.tabPageRobots = new System.Windows.Forms.TabPage();
			this.listViewRobots = new System.Windows.Forms.ListView();
			this.columnHeaderRobots = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderRobotsBlocked = new System.Windows.Forms.ColumnHeader();
			this.tabPageSitemaps = new System.Windows.Forms.TabPage();
			this.listViewSitemaps = new System.Windows.Forms.ListView();
			this.columnHeaderSitemapUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSitemapLinks = new System.Windows.Forms.ColumnHeader();
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
			this.tabPageSearch = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelSearchCollection = new System.Windows.Forms.TableLayoutPanel();
			this.listViewSearchCollection = new System.Windows.Forms.ListView();
			this.columnHeaderSearchCollectionUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSearchCollectionPageTitle = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSearchCollectionPageDescription = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSearchCollectionPageKeywords = new System.Windows.Forms.ColumnHeader();
			this.toolStripSearchCollection = new System.Windows.Forms.ToolStrip();
			this.toolStripSearchCollectionButtonClear = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSearchCollectionTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSearchCollectionDocumentsNumber = new System.Windows.Forms.ToolStripLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabControlMain.SuspendLayout();
			this.tabPageStructureOverview.SuspendLayout();
			this.tableLayoutPanelStructure.SuspendLayout();
			this.contextMenuStripStructure.SuspendLayout();
			this.toolStripSearch.SuspendLayout();
			this.tabPageHierarchy.SuspendLayout();
			this.tabPageCanonicalAnalysis.SuspendLayout();
			this.tabPageHrefLangAnalysis.SuspendLayout();
			this.tabPageRedirectsAudit.SuspendLayout();
			this.tabPageErrors.SuspendLayout();
			this.tabPageHostnames.SuspendLayout();
			this.tabPageUriAnalysis.SuspendLayout();
			this.tabPagePageTitles.SuspendLayout();
			this.tabPagePageDescriptions.SuspendLayout();
			this.tabPagePageKeywords.SuspendLayout();
			this.tabPagePageHeadings.SuspendLayout();
			this.tabPageStylesheets.SuspendLayout();
			this.tabPageJavascripts.SuspendLayout();
			this.tabPageImages.SuspendLayout();
			this.tabPageAudios.SuspendLayout();
			this.tabPageVideos.SuspendLayout();
			this.tabPageRobots.SuspendLayout();
			this.tabPageSitemaps.SuspendLayout();
			this.tabPageEmailAddresses.SuspendLayout();
			this.tabPageTelephoneNumbers.SuspendLayout();
			this.tabPageHistory.SuspendLayout();
			this.tabPageSearch.SuspendLayout();
			this.tableLayoutPanelSearchCollection.SuspendLayout();
			this.toolStripSearchCollection.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlMain
			// 
			this.tabControlMain.Controls.Add(this.tabPageStructureOverview);
			this.tabControlMain.Controls.Add(this.tabPageHierarchy);
			this.tabControlMain.Controls.Add(this.tabPageCanonicalAnalysis);
			this.tabControlMain.Controls.Add(this.tabPageHrefLangAnalysis);
			this.tabControlMain.Controls.Add(this.tabPageRedirectsAudit);
			this.tabControlMain.Controls.Add(this.tabPageErrors);
			this.tabControlMain.Controls.Add(this.tabPageHostnames);
			this.tabControlMain.Controls.Add(this.tabPageUriAnalysis);
			this.tabControlMain.Controls.Add(this.tabPagePageTitles);
			this.tabControlMain.Controls.Add(this.tabPagePageDescriptions);
			this.tabControlMain.Controls.Add(this.tabPagePageKeywords);
			this.tabControlMain.Controls.Add(this.tabPagePageHeadings);
			this.tabControlMain.Controls.Add(this.tabPageStylesheets);
			this.tabControlMain.Controls.Add(this.tabPageJavascripts);
			this.tabControlMain.Controls.Add(this.tabPageImages);
			this.tabControlMain.Controls.Add(this.tabPageAudios);
			this.tabControlMain.Controls.Add(this.tabPageVideos);
			this.tabControlMain.Controls.Add(this.tabPageRobots);
			this.tabControlMain.Controls.Add(this.tabPageSitemaps);
			this.tabControlMain.Controls.Add(this.tabPageEmailAddresses);
			this.tabControlMain.Controls.Add(this.tabPageTelephoneNumbers);
			this.tabControlMain.Controls.Add(this.tabPageHistory);
			this.tabControlMain.Controls.Add(this.tabPageSearch);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(0, 0);
			this.tabControlMain.Margin = new System.Windows.Forms.Padding(0);
			this.tabControlMain.Multiline = true;
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(800, 500);
			this.tabControlMain.TabIndex = 4;
			// 
			// tabPageStructureOverview
			// 
			this.tabPageStructureOverview.Controls.Add(this.tableLayoutPanelStructure);
			this.tabPageStructureOverview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.tabPageStructureOverview.Location = new System.Drawing.Point(4, 58);
			this.tabPageStructureOverview.Margin = new System.Windows.Forms.Padding(0);
			this.tabPageStructureOverview.Name = "tabPageStructureOverview";
			this.tabPageStructureOverview.Size = new System.Drawing.Size(792, 438);
			this.tabPageStructureOverview.TabIndex = 0;
			this.tabPageStructureOverview.Text = "Structure Overview";
			this.tabPageStructureOverview.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelStructure
			// 
			this.tableLayoutPanelStructure.ColumnCount = 1;
			this.tableLayoutPanelStructure.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelStructure.Controls.Add(this.listViewStructure, 0, 1);
			this.tableLayoutPanelStructure.Controls.Add(this.toolStripSearch, 0, 0);
			this.tableLayoutPanelStructure.Location = new System.Drawing.Point(20, 20);
			this.tableLayoutPanelStructure.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelStructure.Name = "tableLayoutPanelStructure";
			this.tableLayoutPanelStructure.RowCount = 2;
			this.tableLayoutPanelStructure.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanelStructure.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelStructure.Size = new System.Drawing.Size(600, 400);
			this.tableLayoutPanelStructure.TabIndex = 2;
			// 
			// listViewStructure
			// 
			this.listViewStructure.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.listViewStructure.BackColor = System.Drawing.SystemColors.Window;
			this.listViewStructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewStructure.CausesValidation = false;
			this.listViewStructure.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listViewStructure.FullRowSelect = true;
			this.listViewStructure.GridLines = true;
			this.listViewStructure.LabelWrap = false;
			this.listViewStructure.Location = new System.Drawing.Point(200, 114);
			this.listViewStructure.Margin = new System.Windows.Forms.Padding(0);
			this.listViewStructure.MultiSelect = false;
			this.listViewStructure.Name = "listViewStructure";
			this.listViewStructure.ShowGroups = false;
			this.listViewStructure.Size = new System.Drawing.Size(200, 200);
			this.listViewStructure.TabIndex = 0;
			this.listViewStructure.UseCompatibleStateImageBehavior = false;
			this.listViewStructure.View = System.Windows.Forms.View.Details;
			// 
			// contextMenuStripStructure
			// 
			this.contextMenuStripStructure.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripMenuItemOpenInBrowser,
			this.toolStripSeparator1,
			this.toolStripMenuItemAddHostToAllowedHosts,
			this.toolStripMenuItemRemoveFromAllowedHosts,
			this.toolStripSeparator2,
			this.toolStripMenuItemResetEntry});
			this.contextMenuStripStructure.Name = "contextMenuStrip1";
			this.contextMenuStripStructure.Size = new System.Drawing.Size(248, 104);
			// 
			// toolStripMenuItemOpenInBrowser
			// 
			this.toolStripMenuItemOpenInBrowser.Name = "toolStripMenuItemOpenInBrowser";
			this.toolStripMenuItemOpenInBrowser.Size = new System.Drawing.Size(247, 22);
			this.toolStripMenuItemOpenInBrowser.Text = "Open in browser";
			this.toolStripMenuItemOpenInBrowser.ToolTipText = "Open this page in your web browser";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(244, 6);
			// 
			// toolStripMenuItemAddHostToAllowedHosts
			// 
			this.toolStripMenuItemAddHostToAllowedHosts.Name = "toolStripMenuItemAddHostToAllowedHosts";
			this.toolStripMenuItemAddHostToAllowedHosts.Size = new System.Drawing.Size(247, 22);
			this.toolStripMenuItemAddHostToAllowedHosts.Text = "Add host to allowed hosts";
			this.toolStripMenuItemAddHostToAllowedHosts.ToolTipText = "Enable crawling of pages within this website";
			// 
			// toolStripMenuItemRemoveFromAllowedHosts
			// 
			this.toolStripMenuItemRemoveFromAllowedHosts.Name = "toolStripMenuItemRemoveFromAllowedHosts";
			this.toolStripMenuItemRemoveFromAllowedHosts.Size = new System.Drawing.Size(247, 22);
			this.toolStripMenuItemRemoveFromAllowedHosts.Text = "Remove host from allowed hosts";
			this.toolStripMenuItemRemoveFromAllowedHosts.ToolTipText = "Remove this website from the allowed hosts list";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(244, 6);
			// 
			// toolStripMenuItemResetEntry
			// 
			this.toolStripMenuItemResetEntry.Name = "toolStripMenuItemResetEntry";
			this.toolStripMenuItemResetEntry.Size = new System.Drawing.Size(247, 22);
			this.toolStripMenuItemResetEntry.Text = "Retry fetch";
			this.toolStripMenuItemResetEntry.ToolTipText = "Try and fetch this page again";
			// 
			// toolStripSearch
			// 
			this.toolStripSearch.AutoSize = false;
			this.toolStripSearch.CanOverflow = false;
			this.toolStripSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripSearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripSearchCollectionFilterMenu,
			this.toolStripStructureButtonShowAll,
			this.toolStripSeparator4,
			this.toolStripLabel1,
			this.toolStripStructureSearchTextBoxSearch,
			this.toolStripSeparator3,
			this.toolStripLabelStructureItems});
			this.toolStripSearch.Location = new System.Drawing.Point(0, 0);
			this.toolStripSearch.Name = "toolStripSearch";
			this.toolStripSearch.Padding = new System.Windows.Forms.Padding(0);
			this.toolStripSearch.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripSearch.Size = new System.Drawing.Size(600, 28);
			this.toolStripSearch.TabIndex = 1;
			this.toolStripSearch.Text = "toolStrip1";
			// 
			// toolStripSearchCollectionFilterMenu
			// 
			this.toolStripSearchCollectionFilterMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSearchCollectionFilterMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.allDocumentTypesToolStripMenuItem,
			this.HtmlToolStripMenuItem,
			this.stylesheetsToolStripMenuItem,
			this.javaScriptsToolStripMenuItem,
			this.imagesToolStripMenuItem,
			this.PdfsToolStripMenuItem,
			this.miscellaneousToolStripMenuItem});
			this.toolStripSearchCollectionFilterMenu.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSearchCollectionFilterMenu.Image")));
			this.toolStripSearchCollectionFilterMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSearchCollectionFilterMenu.Name = "toolStripSearchCollectionFilterMenu";
			this.toolStripSearchCollectionFilterMenu.Size = new System.Drawing.Size(46, 25);
			this.toolStripSearchCollectionFilterMenu.Text = "Filter";
			this.toolStripSearchCollectionFilterMenu.ToolTipText = "Filter results";
			// 
			// allDocumentTypesToolStripMenuItem
			// 
			this.allDocumentTypesToolStripMenuItem.Name = "allDocumentTypesToolStripMenuItem";
			this.allDocumentTypesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.allDocumentTypesToolStripMenuItem.Tag = "ALL";
			this.allDocumentTypesToolStripMenuItem.Text = "All Document Types";
			// 
			// HtmlToolStripMenuItem
			// 
			this.HtmlToolStripMenuItem.Name = "HtmlToolStripMenuItem";
			this.HtmlToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.HtmlToolStripMenuItem.Tag = "HTML";
			this.HtmlToolStripMenuItem.Text = "HTML";
			// 
			// stylesheetsToolStripMenuItem
			// 
			this.stylesheetsToolStripMenuItem.Name = "stylesheetsToolStripMenuItem";
			this.stylesheetsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.stylesheetsToolStripMenuItem.Tag = "CSS";
			this.stylesheetsToolStripMenuItem.Text = "Stylesheets";
			// 
			// javaScriptsToolStripMenuItem
			// 
			this.javaScriptsToolStripMenuItem.Name = "javaScriptsToolStripMenuItem";
			this.javaScriptsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.javaScriptsToolStripMenuItem.Tag = "JAVASCRIPT";
			this.javaScriptsToolStripMenuItem.Text = "JavaScripts";
			// 
			// imagesToolStripMenuItem
			// 
			this.imagesToolStripMenuItem.Name = "imagesToolStripMenuItem";
			this.imagesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.imagesToolStripMenuItem.Tag = "IMAGE";
			this.imagesToolStripMenuItem.Text = "Images";
			// 
			// PdfsToolStripMenuItem
			// 
			this.PdfsToolStripMenuItem.Name = "PdfsToolStripMenuItem";
			this.PdfsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.PdfsToolStripMenuItem.Tag = "PDF";
			this.PdfsToolStripMenuItem.Text = "PDFs";
			// 
			// miscellaneousToolStripMenuItem
			// 
			this.miscellaneousToolStripMenuItem.Name = "miscellaneousToolStripMenuItem";
			this.miscellaneousToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.miscellaneousToolStripMenuItem.Tag = "MISC";
			this.miscellaneousToolStripMenuItem.Text = "Miscellaneous";
			// 
			// toolStripStructureButtonShowAll
			// 
			this.toolStripStructureButtonShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStructureButtonShowAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStructureButtonShowAll.Image")));
			this.toolStripStructureButtonShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripStructureButtonShowAll.Name = "toolStripStructureButtonShowAll";
			this.toolStripStructureButtonShowAll.Size = new System.Drawing.Size(55, 25);
			this.toolStripStructureButtonShowAll.Text = "Show all";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(45, 25);
			this.toolStripLabel1.Text = "Search:";
			// 
			// toolStripStructureSearchTextBoxSearch
			// 
			this.toolStripStructureSearchTextBoxSearch.Name = "toolStripStructureSearchTextBoxSearch";
			this.toolStripStructureSearchTextBoxSearch.Size = new System.Drawing.Size(150, 28);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabelStructureItems
			// 
			this.toolStripLabelStructureItems.Name = "toolStripLabelStructureItems";
			this.toolStripLabelStructureItems.Size = new System.Drawing.Size(80, 25);
			this.toolStripLabelStructureItems.Text = "Documents: 0";
			// 
			// tabPageHierarchy
			// 
			this.tabPageHierarchy.Controls.Add(this.treeViewHierarchy);
			this.tabPageHierarchy.Location = new System.Drawing.Point(4, 58);
			this.tabPageHierarchy.Name = "tabPageHierarchy";
			this.tabPageHierarchy.Size = new System.Drawing.Size(792, 438);
			this.tabPageHierarchy.TabIndex = 8;
			this.tabPageHierarchy.Text = "Hierarchy";
			this.tabPageHierarchy.UseVisualStyleBackColor = true;
			// 
			// treeViewHierarchy
			// 
			this.treeViewHierarchy.CausesValidation = false;
			this.treeViewHierarchy.FullRowSelect = true;
			this.treeViewHierarchy.Location = new System.Drawing.Point(10, 10);
			this.treeViewHierarchy.Margin = new System.Windows.Forms.Padding(0);
			this.treeViewHierarchy.Name = "treeViewHierarchy";
			this.treeViewHierarchy.PathSeparator = "/";
			this.treeViewHierarchy.Size = new System.Drawing.Size(200, 200);
			this.treeViewHierarchy.TabIndex = 0;
			// 
			// tabPageCanonicalAnalysis
			// 
			this.tabPageCanonicalAnalysis.Controls.Add(this.listViewCanonicalAnalysis);
			this.tabPageCanonicalAnalysis.Location = new System.Drawing.Point(4, 58);
			this.tabPageCanonicalAnalysis.Name = "tabPageCanonicalAnalysis";
			this.tabPageCanonicalAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageCanonicalAnalysis.Size = new System.Drawing.Size(792, 438);
			this.tabPageCanonicalAnalysis.TabIndex = 7;
			this.tabPageCanonicalAnalysis.Text = "Canonical Analysis";
			this.tabPageCanonicalAnalysis.UseVisualStyleBackColor = true;
			// 
			// listViewCanonicalAnalysis
			// 
			this.listViewCanonicalAnalysis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.CanonicalAnalysisUrl,
			this.CanonicalAnalysisCanonical});
			this.listViewCanonicalAnalysis.FullRowSelect = true;
			this.listViewCanonicalAnalysis.GridLines = true;
			this.listViewCanonicalAnalysis.Location = new System.Drawing.Point(10, 10);
			this.listViewCanonicalAnalysis.Margin = new System.Windows.Forms.Padding(0);
			this.listViewCanonicalAnalysis.Name = "listViewCanonicalAnalysis";
			this.listViewCanonicalAnalysis.Size = new System.Drawing.Size(200, 200);
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
			this.tabPageHrefLangAnalysis.Location = new System.Drawing.Point(4, 58);
			this.tabPageHrefLangAnalysis.Name = "tabPageHrefLangAnalysis";
			this.tabPageHrefLangAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHrefLangAnalysis.Size = new System.Drawing.Size(792, 438);
			this.tabPageHrefLangAnalysis.TabIndex = 1;
			this.tabPageHrefLangAnalysis.Text = "HrefLang Analysis";
			this.tabPageHrefLangAnalysis.UseVisualStyleBackColor = true;
			// 
			// listViewHrefLang
			// 
			this.listViewHrefLang.FullRowSelect = true;
			this.listViewHrefLang.GridLines = true;
			this.listViewHrefLang.Location = new System.Drawing.Point(10, 10);
			this.listViewHrefLang.Name = "listViewHrefLang";
			this.listViewHrefLang.Size = new System.Drawing.Size(200, 200);
			this.listViewHrefLang.TabIndex = 0;
			this.listViewHrefLang.UseCompatibleStateImageBehavior = false;
			this.listViewHrefLang.View = System.Windows.Forms.View.Details;
			// 
			// tabPageRedirectsAudit
			// 
			this.tabPageRedirectsAudit.Controls.Add(this.listViewRedirectsAudit);
			this.tabPageRedirectsAudit.Location = new System.Drawing.Point(4, 58);
			this.tabPageRedirectsAudit.Name = "tabPageRedirectsAudit";
			this.tabPageRedirectsAudit.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRedirectsAudit.Size = new System.Drawing.Size(792, 438);
			this.tabPageRedirectsAudit.TabIndex = 2;
			this.tabPageRedirectsAudit.Text = "Redirects Audit";
			this.tabPageRedirectsAudit.UseVisualStyleBackColor = true;
			// 
			// listViewRedirectsAudit
			// 
			this.listViewRedirectsAudit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.RedirectsAuditUrl,
			this.RedirectsAuditStatusCode,
			this.RedirectsAuditOriginUrl,
			this.RedirectsAuditDestinationUrl});
			this.listViewRedirectsAudit.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewRedirectsAudit.FullRowSelect = true;
			this.listViewRedirectsAudit.GridLines = true;
			this.listViewRedirectsAudit.Location = new System.Drawing.Point(10, 10);
			this.listViewRedirectsAudit.Margin = new System.Windows.Forms.Padding(0);
			this.listViewRedirectsAudit.Name = "listViewRedirectsAudit";
			this.listViewRedirectsAudit.Size = new System.Drawing.Size(200, 200);
			this.listViewRedirectsAudit.TabIndex = 0;
			this.listViewRedirectsAudit.UseCompatibleStateImageBehavior = false;
			this.listViewRedirectsAudit.View = System.Windows.Forms.View.Details;
			// 
			// RedirectsAuditUrl
			// 
			this.RedirectsAuditUrl.Text = "URL";
			this.RedirectsAuditUrl.Width = 300;
			// 
			// RedirectsAuditStatusCode
			// 
			this.RedirectsAuditStatusCode.Text = "Status Code";
			this.RedirectsAuditStatusCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.RedirectsAuditStatusCode.Width = 100;
			// 
			// RedirectsAuditOriginUrl
			// 
			this.RedirectsAuditOriginUrl.Text = "Origin URL";
			this.RedirectsAuditOriginUrl.Width = 300;
			// 
			// RedirectsAuditDestinationUrl
			// 
			this.RedirectsAuditDestinationUrl.Text = "Destination URL";
			this.RedirectsAuditDestinationUrl.Width = 300;
			// 
			// tabPageErrors
			// 
			this.tabPageErrors.Controls.Add(this.listViewErrors);
			this.tabPageErrors.Location = new System.Drawing.Point(4, 58);
			this.tabPageErrors.Name = "tabPageErrors";
			this.tabPageErrors.Size = new System.Drawing.Size(792, 438);
			this.tabPageErrors.TabIndex = 16;
			this.tabPageErrors.Text = "Errors";
			this.tabPageErrors.UseVisualStyleBackColor = true;
			// 
			// listViewErrors
			// 
			this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderErrorsUrl,
			this.columnHeaderErrorsStatusCode,
			this.columnHeaderErrorsDescription});
			this.listViewErrors.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewErrors.GridLines = true;
			this.listViewErrors.Location = new System.Drawing.Point(10, 10);
			this.listViewErrors.Margin = new System.Windows.Forms.Padding(0);
			this.listViewErrors.Name = "listViewErrors";
			this.listViewErrors.Size = new System.Drawing.Size(200, 200);
			this.listViewErrors.TabIndex = 3;
			this.listViewErrors.UseCompatibleStateImageBehavior = false;
			this.listViewErrors.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderErrorsUrl
			// 
			this.columnHeaderErrorsUrl.Text = "URL";
			this.columnHeaderErrorsUrl.Width = 300;
			// 
			// columnHeaderErrorsStatusCode
			// 
			this.columnHeaderErrorsStatusCode.Text = "Status Code";
			this.columnHeaderErrorsStatusCode.Width = 150;
			// 
			// columnHeaderErrorsDescription
			// 
			this.columnHeaderErrorsDescription.Text = "Error Description";
			this.columnHeaderErrorsDescription.Width = 300;
			// 
			// tabPageHostnames
			// 
			this.tabPageHostnames.Controls.Add(this.listViewHostnames);
			this.tabPageHostnames.Location = new System.Drawing.Point(4, 58);
			this.tabPageHostnames.Name = "tabPageHostnames";
			this.tabPageHostnames.Size = new System.Drawing.Size(792, 438);
			this.tabPageHostnames.TabIndex = 15;
			this.tabPageHostnames.Text = "Hostnames";
			this.tabPageHostnames.UseVisualStyleBackColor = true;
			// 
			// listViewHostnames
			// 
			this.listViewHostnames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderHostnameName,
			this.columnHeaderHostnameCount,
			this.columnHeaderHostnameInternal});
			this.listViewHostnames.GridLines = true;
			this.listViewHostnames.Location = new System.Drawing.Point(10, 10);
			this.listViewHostnames.Margin = new System.Windows.Forms.Padding(0);
			this.listViewHostnames.Name = "listViewHostnames";
			this.listViewHostnames.Size = new System.Drawing.Size(200, 200);
			this.listViewHostnames.TabIndex = 2;
			this.listViewHostnames.UseCompatibleStateImageBehavior = false;
			this.listViewHostnames.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderHostnameName
			// 
			this.columnHeaderHostnameName.Text = "Hostname";
			this.columnHeaderHostnameName.Width = 250;
			// 
			// columnHeaderHostnameCount
			// 
			this.columnHeaderHostnameCount.Text = "Count";
			// 
			// columnHeaderHostnameInternal
			// 
			this.columnHeaderHostnameInternal.Text = "Internal";
			// 
			// tabPageUriAnalysis
			// 
			this.tabPageUriAnalysis.Controls.Add(this.listViewUriAnalysis);
			this.tabPageUriAnalysis.Location = new System.Drawing.Point(4, 58);
			this.tabPageUriAnalysis.Name = "tabPageUriAnalysis";
			this.tabPageUriAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageUriAnalysis.Size = new System.Drawing.Size(792, 438);
			this.tabPageUriAnalysis.TabIndex = 9;
			this.tabPageUriAnalysis.Text = "URI Analysis";
			this.tabPageUriAnalysis.UseVisualStyleBackColor = true;
			// 
			// listViewUriAnalysis
			// 
			this.listViewUriAnalysis.FullRowSelect = true;
			this.listViewUriAnalysis.GridLines = true;
			this.listViewUriAnalysis.Location = new System.Drawing.Point(10, 10);
			this.listViewUriAnalysis.Margin = new System.Windows.Forms.Padding(0);
			this.listViewUriAnalysis.Name = "listViewUriAnalysis";
			this.listViewUriAnalysis.Size = new System.Drawing.Size(200, 200);
			this.listViewUriAnalysis.TabIndex = 0;
			this.listViewUriAnalysis.UseCompatibleStateImageBehavior = false;
			this.listViewUriAnalysis.View = System.Windows.Forms.View.Details;
			// 
			// tabPagePageTitles
			// 
			this.tabPagePageTitles.Controls.Add(this.listViewPageTitles);
			this.tabPagePageTitles.Location = new System.Drawing.Point(4, 58);
			this.tabPagePageTitles.Name = "tabPagePageTitles";
			this.tabPagePageTitles.Size = new System.Drawing.Size(792, 438);
			this.tabPagePageTitles.TabIndex = 10;
			this.tabPagePageTitles.Text = "Page Titles";
			this.tabPagePageTitles.UseVisualStyleBackColor = true;
			// 
			// listViewPageTitles
			// 
			this.listViewPageTitles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderUrl,
			this.columnHeaderCount,
			this.columnHeaderPageTitle,
			this.columnHeaderLength,
			this.columnHeaderPixelWidth});
			this.listViewPageTitles.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewPageTitles.FullRowSelect = true;
			this.listViewPageTitles.GridLines = true;
			this.listViewPageTitles.Location = new System.Drawing.Point(10, 10);
			this.listViewPageTitles.Margin = new System.Windows.Forms.Padding(0);
			this.listViewPageTitles.Name = "listViewPageTitles";
			this.listViewPageTitles.Size = new System.Drawing.Size(200, 200);
			this.listViewPageTitles.TabIndex = 0;
			this.listViewPageTitles.UseCompatibleStateImageBehavior = false;
			this.listViewPageTitles.View = System.Windows.Forms.View.Details;
			this.listViewPageTitles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.CallbackColumnClick);
			// 
			// columnHeaderUrl
			// 
			this.columnHeaderUrl.Text = "URL";
			this.columnHeaderUrl.Width = 500;
			// 
			// columnHeaderCount
			// 
			this.columnHeaderCount.Text = "Occurences";
			this.columnHeaderCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderCount.Width = 100;
			// 
			// columnHeaderPageTitle
			// 
			this.columnHeaderPageTitle.Text = "Page Title";
			this.columnHeaderPageTitle.Width = 150;
			// 
			// columnHeaderLength
			// 
			this.columnHeaderLength.Text = "Length";
			this.columnHeaderLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderLength.Width = 100;
			// 
			// columnHeaderPixelWidth
			// 
			this.columnHeaderPixelWidth.Text = "Pixel Width";
			this.columnHeaderPixelWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderPixelWidth.Width = 100;
			// 
			// tabPagePageDescriptions
			// 
			this.tabPagePageDescriptions.Controls.Add(this.listViewPageDescriptions);
			this.tabPagePageDescriptions.Location = new System.Drawing.Point(4, 58);
			this.tabPagePageDescriptions.Name = "tabPagePageDescriptions";
			this.tabPagePageDescriptions.Size = new System.Drawing.Size(792, 438);
			this.tabPagePageDescriptions.TabIndex = 11;
			this.tabPagePageDescriptions.Text = "Page Description";
			this.tabPagePageDescriptions.UseVisualStyleBackColor = true;
			// 
			// listViewPageDescriptions
			// 
			this.listViewPageDescriptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderDescriptionUrl,
			this.columnHeaderDescriptionCount,
			this.columnHeaderDescriptionDescription,
			this.columnHeaderDescriptionLength});
			this.listViewPageDescriptions.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewPageDescriptions.FullRowSelect = true;
			this.listViewPageDescriptions.GridLines = true;
			this.listViewPageDescriptions.Location = new System.Drawing.Point(10, 10);
			this.listViewPageDescriptions.Margin = new System.Windows.Forms.Padding(0);
			this.listViewPageDescriptions.Name = "listViewPageDescriptions";
			this.listViewPageDescriptions.Size = new System.Drawing.Size(200, 200);
			this.listViewPageDescriptions.TabIndex = 1;
			this.listViewPageDescriptions.UseCompatibleStateImageBehavior = false;
			this.listViewPageDescriptions.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderDescriptionUrl
			// 
			this.columnHeaderDescriptionUrl.Text = "URL";
			this.columnHeaderDescriptionUrl.Width = 500;
			// 
			// columnHeaderDescriptionCount
			// 
			this.columnHeaderDescriptionCount.Text = "Occurences";
			this.columnHeaderDescriptionCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderDescriptionCount.Width = 100;
			// 
			// columnHeaderDescriptionDescription
			// 
			this.columnHeaderDescriptionDescription.Text = "Description";
			this.columnHeaderDescriptionDescription.Width = 300;
			// 
			// columnHeaderDescriptionLength
			// 
			this.columnHeaderDescriptionLength.Text = "Length";
			this.columnHeaderDescriptionLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderDescriptionLength.Width = 100;
			// 
			// tabPagePageKeywords
			// 
			this.tabPagePageKeywords.Controls.Add(this.listViewPageKeywords);
			this.tabPagePageKeywords.Location = new System.Drawing.Point(4, 58);
			this.tabPagePageKeywords.Name = "tabPagePageKeywords";
			this.tabPagePageKeywords.Size = new System.Drawing.Size(792, 438);
			this.tabPagePageKeywords.TabIndex = 12;
			this.tabPagePageKeywords.Text = "Page Keywords";
			this.tabPagePageKeywords.UseVisualStyleBackColor = true;
			// 
			// listViewPageKeywords
			// 
			this.listViewPageKeywords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderKeywordsUrl,
			this.columnHeaderKeywordsCount,
			this.columnHeaderKeywordsKeywords,
			this.columnHeaderKeywordsLength,
			this.columnHeaderKeywordsNumber});
			this.listViewPageKeywords.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewPageKeywords.FullRowSelect = true;
			this.listViewPageKeywords.GridLines = true;
			this.listViewPageKeywords.Location = new System.Drawing.Point(10, 10);
			this.listViewPageKeywords.Margin = new System.Windows.Forms.Padding(0);
			this.listViewPageKeywords.Name = "listViewPageKeywords";
			this.listViewPageKeywords.Size = new System.Drawing.Size(200, 200);
			this.listViewPageKeywords.TabIndex = 3;
			this.listViewPageKeywords.UseCompatibleStateImageBehavior = false;
			this.listViewPageKeywords.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderKeywordsUrl
			// 
			this.columnHeaderKeywordsUrl.Text = "URL";
			this.columnHeaderKeywordsUrl.Width = 500;
			// 
			// columnHeaderKeywordsCount
			// 
			this.columnHeaderKeywordsCount.Text = "Occurences";
			this.columnHeaderKeywordsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderKeywordsCount.Width = 100;
			// 
			// columnHeaderKeywordsKeywords
			// 
			this.columnHeaderKeywordsKeywords.Text = "Keywords";
			this.columnHeaderKeywordsKeywords.Width = 300;
			// 
			// columnHeaderKeywordsLength
			// 
			this.columnHeaderKeywordsLength.Text = "Length";
			this.columnHeaderKeywordsLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderKeywordsLength.Width = 100;
			// 
			// columnHeaderKeywordsNumber
			// 
			this.columnHeaderKeywordsNumber.Text = "No. of Keywords";
			this.columnHeaderKeywordsNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderKeywordsNumber.Width = 100;
			// 
			// tabPagePageHeadings
			// 
			this.tabPagePageHeadings.Controls.Add(this.listViewPageHeadings);
			this.tabPagePageHeadings.Location = new System.Drawing.Point(4, 58);
			this.tabPagePageHeadings.Name = "tabPagePageHeadings";
			this.tabPagePageHeadings.Size = new System.Drawing.Size(792, 438);
			this.tabPagePageHeadings.TabIndex = 13;
			this.tabPagePageHeadings.Text = "Page Headings";
			this.tabPagePageHeadings.UseVisualStyleBackColor = true;
			// 
			// listViewPageHeadings
			// 
			this.listViewPageHeadings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderHeadingsUrl,
			this.columnHeaderHeadingsOrder,
			this.columnHeaderHeadingsH1,
			this.columnHeaderHeadingsH2,
			this.columnHeaderHeadingsH3,
			this.columnHeaderHeadingsH4,
			this.columnHeaderHeadingsH5,
			this.columnHeaderHeadingsH6});
			this.listViewPageHeadings.FullRowSelect = true;
			this.listViewPageHeadings.GridLines = true;
			this.listViewPageHeadings.Location = new System.Drawing.Point(10, 10);
			this.listViewPageHeadings.Margin = new System.Windows.Forms.Padding(0);
			this.listViewPageHeadings.Name = "listViewPageHeadings";
			this.listViewPageHeadings.Size = new System.Drawing.Size(200, 200);
			this.listViewPageHeadings.TabIndex = 2;
			this.listViewPageHeadings.UseCompatibleStateImageBehavior = false;
			this.listViewPageHeadings.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderHeadingsUrl
			// 
			this.columnHeaderHeadingsUrl.Text = "URL";
			this.columnHeaderHeadingsUrl.Width = 300;
			// 
			// columnHeaderHeadingsOrder
			// 
			this.columnHeaderHeadingsOrder.Text = "Order";
			this.columnHeaderHeadingsOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderHeadingsOrder.Width = 100;
			// 
			// columnHeaderHeadingsH1
			// 
			this.columnHeaderHeadingsH1.Text = "H1";
			this.columnHeaderHeadingsH1.Width = 200;
			// 
			// columnHeaderHeadingsH2
			// 
			this.columnHeaderHeadingsH2.Text = "H2";
			this.columnHeaderHeadingsH2.Width = 200;
			// 
			// columnHeaderHeadingsH3
			// 
			this.columnHeaderHeadingsH3.Text = "H3";
			this.columnHeaderHeadingsH3.Width = 200;
			// 
			// columnHeaderHeadingsH4
			// 
			this.columnHeaderHeadingsH4.Text = "H4";
			this.columnHeaderHeadingsH4.Width = 200;
			// 
			// columnHeaderHeadingsH5
			// 
			this.columnHeaderHeadingsH5.Text = "H5";
			this.columnHeaderHeadingsH5.Width = 200;
			// 
			// columnHeaderHeadingsH6
			// 
			this.columnHeaderHeadingsH6.Text = "H6";
			this.columnHeaderHeadingsH6.Width = 200;
			// 
			// tabPageStylesheets
			// 
			this.tabPageStylesheets.Controls.Add(this.listViewStylesheets);
			this.tabPageStylesheets.Location = new System.Drawing.Point(4, 58);
			this.tabPageStylesheets.Name = "tabPageStylesheets";
			this.tabPageStylesheets.Size = new System.Drawing.Size(792, 438);
			this.tabPageStylesheets.TabIndex = 20;
			this.tabPageStylesheets.Text = "Stylesheets";
			this.tabPageStylesheets.UseVisualStyleBackColor = true;
			// 
			// listViewStylesheets
			// 
			this.listViewStylesheets.BackColor = System.Drawing.SystemColors.Window;
			this.listViewStylesheets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewStylesheets.CausesValidation = false;
			this.listViewStylesheets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderStylesheetsUrl,
			this.columnHeaderStylesheetsStatusCode,
			this.columnHeaderStylesheetsMimeType,
			this.columnHeaderStylesheetsFileSize});
			this.listViewStylesheets.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewStylesheets.FullRowSelect = true;
			this.listViewStylesheets.GridLines = true;
			this.listViewStylesheets.LabelWrap = false;
			this.listViewStylesheets.Location = new System.Drawing.Point(20, 20);
			this.listViewStylesheets.Margin = new System.Windows.Forms.Padding(0);
			this.listViewStylesheets.MultiSelect = false;
			this.listViewStylesheets.Name = "listViewStylesheets";
			this.listViewStylesheets.ShowGroups = false;
			this.listViewStylesheets.Size = new System.Drawing.Size(200, 200);
			this.listViewStylesheets.TabIndex = 3;
			this.listViewStylesheets.UseCompatibleStateImageBehavior = false;
			this.listViewStylesheets.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderStylesheetsUrl
			// 
			this.columnHeaderStylesheetsUrl.Text = "URL";
			this.columnHeaderStylesheetsUrl.Width = 300;
			// 
			// columnHeaderStylesheetsStatusCode
			// 
			this.columnHeaderStylesheetsStatusCode.Text = "Status Code";
			this.columnHeaderStylesheetsStatusCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderStylesheetsStatusCode.Width = 150;
			// 
			// columnHeaderStylesheetsMimeType
			// 
			this.columnHeaderStylesheetsMimeType.Text = "MIME Type";
			this.columnHeaderStylesheetsMimeType.Width = 150;
			// 
			// columnHeaderStylesheetsFileSize
			// 
			this.columnHeaderStylesheetsFileSize.Text = "File Size";
			this.columnHeaderStylesheetsFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderStylesheetsFileSize.Width = 150;
			// 
			// tabPageJavascripts
			// 
			this.tabPageJavascripts.Controls.Add(this.listViewJavascripts);
			this.tabPageJavascripts.Location = new System.Drawing.Point(4, 58);
			this.tabPageJavascripts.Name = "tabPageJavascripts";
			this.tabPageJavascripts.Size = new System.Drawing.Size(792, 438);
			this.tabPageJavascripts.TabIndex = 21;
			this.tabPageJavascripts.Text = "Javascripts";
			this.tabPageJavascripts.UseVisualStyleBackColor = true;
			// 
			// listViewJavascripts
			// 
			this.listViewJavascripts.BackColor = System.Drawing.SystemColors.Window;
			this.listViewJavascripts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewJavascripts.CausesValidation = false;
			this.listViewJavascripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderJavascriptsUrl,
			this.columnHeaderJavascriptsStatusCode,
			this.columnHeaderJavascriptsMimeType,
			this.columnHeaderJavascriptsFileSize});
			this.listViewJavascripts.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewJavascripts.FullRowSelect = true;
			this.listViewJavascripts.GridLines = true;
			this.listViewJavascripts.LabelWrap = false;
			this.listViewJavascripts.Location = new System.Drawing.Point(20, 20);
			this.listViewJavascripts.Margin = new System.Windows.Forms.Padding(0);
			this.listViewJavascripts.MultiSelect = false;
			this.listViewJavascripts.Name = "listViewJavascripts";
			this.listViewJavascripts.ShowGroups = false;
			this.listViewJavascripts.Size = new System.Drawing.Size(200, 200);
			this.listViewJavascripts.TabIndex = 2;
			this.listViewJavascripts.UseCompatibleStateImageBehavior = false;
			this.listViewJavascripts.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderJavascriptsUrl
			// 
			this.columnHeaderJavascriptsUrl.Text = "URL";
			this.columnHeaderJavascriptsUrl.Width = 300;
			// 
			// columnHeaderJavascriptsStatusCode
			// 
			this.columnHeaderJavascriptsStatusCode.Text = "Status Code";
			this.columnHeaderJavascriptsStatusCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderJavascriptsStatusCode.Width = 150;
			// 
			// columnHeaderJavascriptsMimeType
			// 
			this.columnHeaderJavascriptsMimeType.Text = "MIME Type";
			this.columnHeaderJavascriptsMimeType.Width = 150;
			// 
			// columnHeaderJavascriptsFileSize
			// 
			this.columnHeaderJavascriptsFileSize.Text = "File Size";
			this.columnHeaderJavascriptsFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderJavascriptsFileSize.Width = 150;
			// 
			// tabPageImages
			// 
			this.tabPageImages.Controls.Add(this.listViewImages);
			this.tabPageImages.Location = new System.Drawing.Point(4, 58);
			this.tabPageImages.Name = "tabPageImages";
			this.tabPageImages.Size = new System.Drawing.Size(792, 438);
			this.tabPageImages.TabIndex = 19;
			this.tabPageImages.Text = "Images";
			this.tabPageImages.UseVisualStyleBackColor = true;
			// 
			// listViewImages
			// 
			this.listViewImages.BackColor = System.Drawing.SystemColors.Window;
			this.listViewImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewImages.CausesValidation = false;
			this.listViewImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderImagesUrl,
			this.columnHeaderImagesStatusCode,
			this.columnHeaderImagesMimeType,
			this.columnHeaderImagesFileSize});
			this.listViewImages.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewImages.FullRowSelect = true;
			this.listViewImages.GridLines = true;
			this.listViewImages.LabelWrap = false;
			this.listViewImages.Location = new System.Drawing.Point(20, 20);
			this.listViewImages.Margin = new System.Windows.Forms.Padding(0);
			this.listViewImages.MultiSelect = false;
			this.listViewImages.Name = "listViewImages";
			this.listViewImages.ShowGroups = false;
			this.listViewImages.Size = new System.Drawing.Size(200, 200);
			this.listViewImages.TabIndex = 1;
			this.listViewImages.UseCompatibleStateImageBehavior = false;
			this.listViewImages.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderImagesUrl
			// 
			this.columnHeaderImagesUrl.Text = "URL";
			this.columnHeaderImagesUrl.Width = 300;
			// 
			// columnHeaderImagesStatusCode
			// 
			this.columnHeaderImagesStatusCode.Text = "Status Code";
			this.columnHeaderImagesStatusCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderImagesStatusCode.Width = 150;
			// 
			// columnHeaderImagesMimeType
			// 
			this.columnHeaderImagesMimeType.Text = "MIME Type";
			this.columnHeaderImagesMimeType.Width = 150;
			// 
			// columnHeaderImagesFileSize
			// 
			this.columnHeaderImagesFileSize.Text = "File Size";
			this.columnHeaderImagesFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderImagesFileSize.Width = 150;
			// 
			// tabPageAudios
			// 
			this.tabPageAudios.Controls.Add(this.listViewAudios);
			this.tabPageAudios.Location = new System.Drawing.Point(4, 58);
			this.tabPageAudios.Name = "tabPageAudios";
			this.tabPageAudios.Size = new System.Drawing.Size(792, 438);
			this.tabPageAudios.TabIndex = 23;
			this.tabPageAudios.Text = "Audio";
			this.tabPageAudios.UseVisualStyleBackColor = true;
			// 
			// listViewAudios
			// 
			this.listViewAudios.BackColor = System.Drawing.SystemColors.Window;
			this.listViewAudios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewAudios.CausesValidation = false;
			this.listViewAudios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderAudiosUrl,
			this.columnHeaderAudiosStatusCode,
			this.columnHeaderAudiosMimeType,
			this.columnHeaderAudiosFileSize});
			this.listViewAudios.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewAudios.FullRowSelect = true;
			this.listViewAudios.GridLines = true;
			this.listViewAudios.LabelWrap = false;
			this.listViewAudios.Location = new System.Drawing.Point(20, 20);
			this.listViewAudios.Margin = new System.Windows.Forms.Padding(0);
			this.listViewAudios.MultiSelect = false;
			this.listViewAudios.Name = "listViewAudios";
			this.listViewAudios.ShowGroups = false;
			this.listViewAudios.Size = new System.Drawing.Size(200, 200);
			this.listViewAudios.TabIndex = 3;
			this.listViewAudios.UseCompatibleStateImageBehavior = false;
			this.listViewAudios.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderAudiosUrl
			// 
			this.columnHeaderAudiosUrl.Text = "URL";
			this.columnHeaderAudiosUrl.Width = 300;
			// 
			// columnHeaderAudiosStatusCode
			// 
			this.columnHeaderAudiosStatusCode.Text = "Status Code";
			this.columnHeaderAudiosStatusCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderAudiosStatusCode.Width = 150;
			// 
			// columnHeaderAudiosMimeType
			// 
			this.columnHeaderAudiosMimeType.Text = "MIME Type";
			this.columnHeaderAudiosMimeType.Width = 150;
			// 
			// columnHeaderAudiosFileSize
			// 
			this.columnHeaderAudiosFileSize.Text = "File Size";
			this.columnHeaderAudiosFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderAudiosFileSize.Width = 150;
			// 
			// tabPageVideos
			// 
			this.tabPageVideos.Controls.Add(this.listViewVideos);
			this.tabPageVideos.Location = new System.Drawing.Point(4, 58);
			this.tabPageVideos.Name = "tabPageVideos";
			this.tabPageVideos.Size = new System.Drawing.Size(792, 438);
			this.tabPageVideos.TabIndex = 22;
			this.tabPageVideos.Text = "Videos";
			this.tabPageVideos.UseVisualStyleBackColor = true;
			// 
			// listViewVideos
			// 
			this.listViewVideos.BackColor = System.Drawing.SystemColors.Window;
			this.listViewVideos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewVideos.CausesValidation = false;
			this.listViewVideos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderVideosUrl,
			this.columnHeaderVideosStatusCode,
			this.columnHeaderVideosMimeType,
			this.columnHeaderVideosFileSize});
			this.listViewVideos.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewVideos.FullRowSelect = true;
			this.listViewVideos.GridLines = true;
			this.listViewVideos.LabelWrap = false;
			this.listViewVideos.Location = new System.Drawing.Point(20, 20);
			this.listViewVideos.Margin = new System.Windows.Forms.Padding(0);
			this.listViewVideos.MultiSelect = false;
			this.listViewVideos.Name = "listViewVideos";
			this.listViewVideos.ShowGroups = false;
			this.listViewVideos.Size = new System.Drawing.Size(200, 200);
			this.listViewVideos.TabIndex = 2;
			this.listViewVideos.UseCompatibleStateImageBehavior = false;
			this.listViewVideos.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderVideosUrl
			// 
			this.columnHeaderVideosUrl.Text = "URL";
			this.columnHeaderVideosUrl.Width = 300;
			// 
			// columnHeaderVideosStatusCode
			// 
			this.columnHeaderVideosStatusCode.Text = "Status Code";
			this.columnHeaderVideosStatusCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderVideosStatusCode.Width = 150;
			// 
			// columnHeaderVideosMimeType
			// 
			this.columnHeaderVideosMimeType.Text = "MIME Type";
			this.columnHeaderVideosMimeType.Width = 150;
			// 
			// columnHeaderVideosFileSize
			// 
			this.columnHeaderVideosFileSize.Text = "File Size";
			this.columnHeaderVideosFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderVideosFileSize.Width = 150;
			// 
			// tabPageRobots
			// 
			this.tabPageRobots.Controls.Add(this.listViewRobots);
			this.tabPageRobots.Location = new System.Drawing.Point(4, 58);
			this.tabPageRobots.Name = "tabPageRobots";
			this.tabPageRobots.Size = new System.Drawing.Size(792, 438);
			this.tabPageRobots.TabIndex = 17;
			this.tabPageRobots.Text = "Robots";
			this.tabPageRobots.UseVisualStyleBackColor = true;
			// 
			// listViewRobots
			// 
			this.listViewRobots.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderRobots,
			this.columnHeaderRobotsBlocked});
			this.listViewRobots.GridLines = true;
			this.listViewRobots.Location = new System.Drawing.Point(20, 20);
			this.listViewRobots.Name = "listViewRobots";
			this.listViewRobots.Size = new System.Drawing.Size(200, 200);
			this.listViewRobots.TabIndex = 0;
			this.listViewRobots.UseCompatibleStateImageBehavior = false;
			this.listViewRobots.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderRobots
			// 
			this.columnHeaderRobots.Text = "URL";
			this.columnHeaderRobots.Width = 300;
			// 
			// columnHeaderRobotsBlocked
			// 
			this.columnHeaderRobotsBlocked.Text = "Blocked by Robots.txt";
			this.columnHeaderRobotsBlocked.Width = 150;
			// 
			// tabPageSitemaps
			// 
			this.tabPageSitemaps.Controls.Add(this.listViewSitemaps);
			this.tabPageSitemaps.Location = new System.Drawing.Point(4, 58);
			this.tabPageSitemaps.Name = "tabPageSitemaps";
			this.tabPageSitemaps.Size = new System.Drawing.Size(792, 438);
			this.tabPageSitemaps.TabIndex = 18;
			this.tabPageSitemaps.Text = "Sitemaps";
			this.tabPageSitemaps.UseVisualStyleBackColor = true;
			// 
			// listViewSitemaps
			// 
			this.listViewSitemaps.CausesValidation = false;
			this.listViewSitemaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderSitemapUrl,
			this.columnHeaderSitemapLinks});
			this.listViewSitemaps.GridLines = true;
			this.listViewSitemaps.Location = new System.Drawing.Point(20, 20);
			this.listViewSitemaps.Name = "listViewSitemaps";
			this.listViewSitemaps.Size = new System.Drawing.Size(200, 200);
			this.listViewSitemaps.TabIndex = 0;
			this.listViewSitemaps.UseCompatibleStateImageBehavior = false;
			this.listViewSitemaps.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderSitemapUrl
			// 
			this.columnHeaderSitemapUrl.Text = "URL";
			this.columnHeaderSitemapUrl.Width = 400;
			// 
			// columnHeaderSitemapLinks
			// 
			this.columnHeaderSitemapLinks.Text = "Links";
			this.columnHeaderSitemapLinks.Width = 150;
			// 
			// tabPageEmailAddresses
			// 
			this.tabPageEmailAddresses.Controls.Add(this.listViewEmailAddresses);
			this.tabPageEmailAddresses.Location = new System.Drawing.Point(4, 58);
			this.tabPageEmailAddresses.Name = "tabPageEmailAddresses";
			this.tabPageEmailAddresses.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEmailAddresses.Size = new System.Drawing.Size(792, 438);
			this.tabPageEmailAddresses.TabIndex = 3;
			this.tabPageEmailAddresses.Text = "Email Addresses";
			this.tabPageEmailAddresses.UseVisualStyleBackColor = true;
			// 
			// listViewEmailAddresses
			// 
			this.listViewEmailAddresses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.EmailAddressesEmail,
			this.EmailAddressesUrl});
			this.listViewEmailAddresses.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewEmailAddresses.FullRowSelect = true;
			this.listViewEmailAddresses.GridLines = true;
			this.listViewEmailAddresses.Location = new System.Drawing.Point(10, 10);
			this.listViewEmailAddresses.Margin = new System.Windows.Forms.Padding(0);
			this.listViewEmailAddresses.Name = "listViewEmailAddresses";
			this.listViewEmailAddresses.Size = new System.Drawing.Size(200, 200);
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
			this.tabPageTelephoneNumbers.Location = new System.Drawing.Point(4, 58);
			this.tabPageTelephoneNumbers.Name = "tabPageTelephoneNumbers";
			this.tabPageTelephoneNumbers.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTelephoneNumbers.Size = new System.Drawing.Size(792, 438);
			this.tabPageTelephoneNumbers.TabIndex = 4;
			this.tabPageTelephoneNumbers.Text = "Telephone Numbers";
			this.tabPageTelephoneNumbers.UseVisualStyleBackColor = true;
			// 
			// listViewTelephoneNumbers
			// 
			this.listViewTelephoneNumbers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.TelTel,
			this.TelUrl});
			this.listViewTelephoneNumbers.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewTelephoneNumbers.FullRowSelect = true;
			this.listViewTelephoneNumbers.GridLines = true;
			this.listViewTelephoneNumbers.Location = new System.Drawing.Point(10, 10);
			this.listViewTelephoneNumbers.Margin = new System.Windows.Forms.Padding(0);
			this.listViewTelephoneNumbers.Name = "listViewTelephoneNumbers";
			this.listViewTelephoneNumbers.Size = new System.Drawing.Size(200, 200);
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
			this.tabPageHistory.Location = new System.Drawing.Point(4, 58);
			this.tabPageHistory.Name = "tabPageHistory";
			this.tabPageHistory.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHistory.Size = new System.Drawing.Size(792, 438);
			this.tabPageHistory.TabIndex = 5;
			this.tabPageHistory.Text = "History";
			this.tabPageHistory.UseVisualStyleBackColor = true;
			// 
			// listViewHistory
			// 
			this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.HistoryUrl,
			this.HistoryVisited});
			this.listViewHistory.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewHistory.GridLines = true;
			this.listViewHistory.Location = new System.Drawing.Point(10, 10);
			this.listViewHistory.Margin = new System.Windows.Forms.Padding(0);
			this.listViewHistory.Name = "listViewHistory";
			this.listViewHistory.Size = new System.Drawing.Size(200, 200);
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
			// tabPageSearch
			// 
			this.tabPageSearch.Controls.Add(this.tableLayoutPanelSearchCollection);
			this.tabPageSearch.Location = new System.Drawing.Point(4, 58);
			this.tabPageSearch.Name = "tabPageSearch";
			this.tabPageSearch.Size = new System.Drawing.Size(792, 438);
			this.tabPageSearch.TabIndex = 24;
			this.tabPageSearch.Text = "Search";
			this.tabPageSearch.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelSearchCollection
			// 
			this.tableLayoutPanelSearchCollection.ColumnCount = 1;
			this.tableLayoutPanelSearchCollection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelSearchCollection.Controls.Add(this.listViewSearchCollection, 0, 1);
			this.tableLayoutPanelSearchCollection.Controls.Add(this.toolStripSearchCollection, 0, 0);
			this.tableLayoutPanelSearchCollection.Location = new System.Drawing.Point(20, 20);
			this.tableLayoutPanelSearchCollection.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelSearchCollection.Name = "tableLayoutPanelSearchCollection";
			this.tableLayoutPanelSearchCollection.RowCount = 2;
			this.tableLayoutPanelSearchCollection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanelSearchCollection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelSearchCollection.Size = new System.Drawing.Size(600, 400);
			this.tableLayoutPanelSearchCollection.TabIndex = 3;
			// 
			// listViewSearchCollection
			// 
			this.listViewSearchCollection.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.listViewSearchCollection.BackColor = System.Drawing.SystemColors.Window;
			this.listViewSearchCollection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewSearchCollection.CausesValidation = false;
			this.listViewSearchCollection.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderSearchCollectionUrl,
			this.columnHeaderSearchCollectionPageTitle,
			this.columnHeaderSearchCollectionPageDescription,
			this.columnHeaderSearchCollectionPageKeywords});
			this.listViewSearchCollection.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewSearchCollection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listViewSearchCollection.FullRowSelect = true;
			this.listViewSearchCollection.GridLines = true;
			this.listViewSearchCollection.LabelWrap = false;
			this.listViewSearchCollection.Location = new System.Drawing.Point(200, 114);
			this.listViewSearchCollection.Margin = new System.Windows.Forms.Padding(0);
			this.listViewSearchCollection.MultiSelect = false;
			this.listViewSearchCollection.Name = "listViewSearchCollection";
			this.listViewSearchCollection.ShowGroups = false;
			this.listViewSearchCollection.Size = new System.Drawing.Size(200, 200);
			this.listViewSearchCollection.TabIndex = 0;
			this.listViewSearchCollection.UseCompatibleStateImageBehavior = false;
			this.listViewSearchCollection.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderSearchCollectionUrl
			// 
			this.columnHeaderSearchCollectionUrl.Text = "URL";
			this.columnHeaderSearchCollectionUrl.Width = 300;
			// 
			// columnHeaderSearchCollectionPageTitle
			// 
			this.columnHeaderSearchCollectionPageTitle.Text = "Page Title";
			this.columnHeaderSearchCollectionPageTitle.Width = 300;
			// 
			// columnHeaderSearchCollectionPageDescription
			// 
			this.columnHeaderSearchCollectionPageDescription.Text = "Page Description";
			this.columnHeaderSearchCollectionPageDescription.Width = 300;
			// 
			// columnHeaderSearchCollectionPageKeywords
			// 
			this.columnHeaderSearchCollectionPageKeywords.Text = "Page Keywords";
			this.columnHeaderSearchCollectionPageKeywords.Width = 300;
			// 
			// toolStripSearchCollection
			// 
			this.toolStripSearchCollection.AutoSize = false;
			this.toolStripSearchCollection.CanOverflow = false;
			this.toolStripSearchCollection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripSearchCollection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripSearchCollectionButtonClear,
			this.toolStripSeparator5,
			this.toolStripLabel2,
			this.toolStripSearchCollectionTextBoxSearch,
			this.toolStripSeparator6,
			this.toolStripSearchCollectionDocumentsNumber});
			this.toolStripSearchCollection.Location = new System.Drawing.Point(0, 0);
			this.toolStripSearchCollection.Name = "toolStripSearchCollection";
			this.toolStripSearchCollection.Padding = new System.Windows.Forms.Padding(0);
			this.toolStripSearchCollection.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripSearchCollection.Size = new System.Drawing.Size(600, 28);
			this.toolStripSearchCollection.TabIndex = 1;
			this.toolStripSearchCollection.Text = "toolStrip1";
			// 
			// toolStripSearchCollectionButtonClear
			// 
			this.toolStripSearchCollectionButtonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSearchCollectionButtonClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSearchCollectionButtonClear.Image")));
			this.toolStripSearchCollectionButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSearchCollectionButtonClear.Name = "toolStripSearchCollectionButtonClear";
			this.toolStripSearchCollectionButtonClear.Size = new System.Drawing.Size(38, 25);
			this.toolStripSearchCollectionButtonClear.Text = "Clear";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(45, 25);
			this.toolStripLabel2.Text = "Search:";
			// 
			// toolStripSearchCollectionTextBoxSearch
			// 
			this.toolStripSearchCollectionTextBoxSearch.Name = "toolStripSearchCollectionTextBoxSearch";
			this.toolStripSearchCollectionTextBoxSearch.Size = new System.Drawing.Size(150, 28);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripSearchCollectionDocumentsNumber
			// 
			this.toolStripSearchCollectionDocumentsNumber.Name = "toolStripSearchCollectionDocumentsNumber";
			this.toolStripSearchCollectionDocumentsNumber.Size = new System.Drawing.Size(80, 25);
			this.toolStripSearchCollectionDocumentsNumber.Text = "Documents: 0";
			// 
			// MacroscopeOverviewPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControlMain);
			this.Name = "MacroscopeOverviewPanel";
			this.Size = new System.Drawing.Size(800, 500);
			this.tabControlMain.ResumeLayout(false);
			this.tabPageStructureOverview.ResumeLayout(false);
			this.tableLayoutPanelStructure.ResumeLayout(false);
			this.contextMenuStripStructure.ResumeLayout(false);
			this.toolStripSearch.ResumeLayout(false);
			this.toolStripSearch.PerformLayout();
			this.tabPageHierarchy.ResumeLayout(false);
			this.tabPageCanonicalAnalysis.ResumeLayout(false);
			this.tabPageHrefLangAnalysis.ResumeLayout(false);
			this.tabPageRedirectsAudit.ResumeLayout(false);
			this.tabPageErrors.ResumeLayout(false);
			this.tabPageHostnames.ResumeLayout(false);
			this.tabPageUriAnalysis.ResumeLayout(false);
			this.tabPagePageTitles.ResumeLayout(false);
			this.tabPagePageDescriptions.ResumeLayout(false);
			this.tabPagePageKeywords.ResumeLayout(false);
			this.tabPagePageHeadings.ResumeLayout(false);
			this.tabPageStylesheets.ResumeLayout(false);
			this.tabPageJavascripts.ResumeLayout(false);
			this.tabPageImages.ResumeLayout(false);
			this.tabPageAudios.ResumeLayout(false);
			this.tabPageVideos.ResumeLayout(false);
			this.tabPageRobots.ResumeLayout(false);
			this.tabPageSitemaps.ResumeLayout(false);
			this.tabPageEmailAddresses.ResumeLayout(false);
			this.tabPageTelephoneNumbers.ResumeLayout(false);
			this.tabPageHistory.ResumeLayout(false);
			this.tabPageSearch.ResumeLayout(false);
			this.tableLayoutPanelSearchCollection.ResumeLayout(false);
			this.toolStripSearchCollection.ResumeLayout(false);
			this.toolStripSearchCollection.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
