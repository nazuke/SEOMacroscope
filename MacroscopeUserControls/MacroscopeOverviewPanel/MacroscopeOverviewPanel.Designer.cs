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
		public System.Windows.Forms.ToolStripDropDownButton toolStripStructureFilterMenu;
		private System.Windows.Forms.ToolStripMenuItem allDocumentTypesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HtmlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stylesheetsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem javaScriptsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem imagesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PdfsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miscellaneousToolStripMenuItem;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		public System.Windows.Forms.ToolStripTextBox toolStripStructureSearchTextBoxSearchUrl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sitemapXMLToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader columnHeaderUriAnalysisUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderUriAnalysisStatusCode;
		private System.Windows.Forms.ColumnHeader columnHeaderUriAnalysisChecksum;
		private System.Windows.Forms.ColumnHeader columnHeaderUriAnalysisCount;
		private System.Windows.Forms.ColumnHeader CanonicalAnalysisStatusCode;
		private System.Windows.Forms.ColumnHeader columnHeaderErrorsStatus;
		private System.Windows.Forms.ColumnHeader columnHeaderUriAnalysisStatus;
		public System.Windows.Forms.TabPage tabPageHyperlinks;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelHyperlinks;
		public System.Windows.Forms.ListView listViewHyperlinks;
		public System.Windows.Forms.ToolStrip toolStrip1;
		public System.Windows.Forms.ToolStripButton toolStripButtonHyperlinksShowAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		public System.Windows.Forms.ToolStripTextBox toolStripTextBoxHyperlinksSearchSourceUrls;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripLabel toolStripLabel5;
		public System.Windows.Forms.ToolStripTextBox toolStripTextBoxHyperlinksSearchTargetUrls;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		public System.Windows.Forms.ToolStripLabel toolStripLabelHyperlinksUrls;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksUrlSource;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksUrlTarget;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksLinkText;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksLinkTitle;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksLinkAltText;
		public System.Windows.Forms.TabPage tabPageLinks;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanelLinks;
		public System.Windows.Forms.ToolStrip toolStrip2;
		public System.Windows.Forms.ToolStripButton toolStripButtonLinksShowAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripLabel toolStripLabel6;
		public System.Windows.Forms.ToolStripTextBox toolStripTextBoxLinksSearchSourceUrls;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripLabel toolStripLabel7;
		public System.Windows.Forms.ToolStripTextBox toolStripTextBoxLinksSearchTargetUrls;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		public System.Windows.Forms.ToolStripLabel toolStripLabelLinksUrls;
		public System.Windows.Forms.ListView listViewLinks;
		public System.Windows.Forms.ColumnHeader columnHeaderLinksOutClass;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutSourceUrl;
		public System.Windows.Forms.ColumnHeader columnHeaderLinksOutTargetUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutFollow;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutAltText;

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
			this.toolStripStructureFilterMenu = new System.Windows.Forms.ToolStripDropDownButton();
			this.allDocumentTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HtmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stylesheetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.javaScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PdfsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sitemapXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miscellaneousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripStructureButtonShowAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripStructureSearchTextBoxSearchUrl = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripStructureSearchTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabelStructureItems = new System.Windows.Forms.ToolStripLabel();
			this.tabPageHierarchy = new System.Windows.Forms.TabPage();
			this.treeViewHierarchy = new System.Windows.Forms.TreeView();
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
			this.tabPageRobots = new System.Windows.Forms.TabPage();
			this.listViewRobots = new System.Windows.Forms.ListView();
			this.columnHeaderRobots = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderRobotsBlocked = new System.Windows.Forms.ColumnHeader();
			this.tabPageSitemaps = new System.Windows.Forms.TabPage();
			this.listViewSitemaps = new System.Windows.Forms.ListView();
			this.columnHeaderSitemapUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSitemapLinks = new System.Windows.Forms.ColumnHeader();
			this.tabPageCanonicalAnalysis = new System.Windows.Forms.TabPage();
			this.listViewCanonicalAnalysis = new System.Windows.Forms.ListView();
			this.CanonicalAnalysisUrl = new System.Windows.Forms.ColumnHeader();
			this.CanonicalAnalysisStatusCode = new System.Windows.Forms.ColumnHeader();
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
			this.columnHeaderErrorsStatus = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderErrorsDescription = new System.Windows.Forms.ColumnHeader();
			this.tabPageHostnames = new System.Windows.Forms.TabPage();
			this.listViewHostnames = new System.Windows.Forms.ListView();
			this.columnHeaderHostnameName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHostnameCount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderHostnameInternal = new System.Windows.Forms.ColumnHeader();
			this.tabPageLinks = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelLinks = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonLinksShowAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripTextBoxLinksSearchSourceUrls = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripTextBoxLinksSearchTargetUrls = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabelLinksUrls = new System.Windows.Forms.ToolStripLabel();
			this.listViewLinks = new System.Windows.Forms.ListView();
			this.columnHeaderLinksOutClass = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLinksOutSourceUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLinksOutTargetUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLinksOutFollow = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLinksOutAltText = new System.Windows.Forms.ColumnHeader();
			this.tabPageHyperlinks = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelHyperlinks = new System.Windows.Forms.TableLayoutPanel();
			this.listViewHyperlinks = new System.Windows.Forms.ListView();
			this.columnHeaderLinksUrlSource = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLinksUrlTarget = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLinksLinkText = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLinksLinkTitle = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderLinksLinkAltText = new System.Windows.Forms.ColumnHeader();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonHyperlinksShowAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripTextBoxHyperlinksSearchSourceUrls = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripTextBoxHyperlinksSearchTargetUrls = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabelHyperlinksUrls = new System.Windows.Forms.ToolStripLabel();
			this.tabPageUriAnalysis = new System.Windows.Forms.TabPage();
			this.listViewUriAnalysis = new System.Windows.Forms.ListView();
			this.columnHeaderUriAnalysisUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderUriAnalysisStatusCode = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderUriAnalysisStatus = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderUriAnalysisCount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderUriAnalysisChecksum = new System.Windows.Forms.ColumnHeader();
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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabControlMain.SuspendLayout();
			this.tabPageStructureOverview.SuspendLayout();
			this.tableLayoutPanelStructure.SuspendLayout();
			this.contextMenuStripStructure.SuspendLayout();
			this.toolStripSearch.SuspendLayout();
			this.tabPageHierarchy.SuspendLayout();
			this.tabPageSearch.SuspendLayout();
			this.tableLayoutPanelSearchCollection.SuspendLayout();
			this.toolStripSearchCollection.SuspendLayout();
			this.tabPageRobots.SuspendLayout();
			this.tabPageSitemaps.SuspendLayout();
			this.tabPageCanonicalAnalysis.SuspendLayout();
			this.tabPageHrefLangAnalysis.SuspendLayout();
			this.tabPageRedirectsAudit.SuspendLayout();
			this.tabPageErrors.SuspendLayout();
			this.tabPageHostnames.SuspendLayout();
			this.tabPageLinks.SuspendLayout();
			this.tableLayoutPanelLinks.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.tabPageHyperlinks.SuspendLayout();
			this.tableLayoutPanelHyperlinks.SuspendLayout();
			this.toolStrip1.SuspendLayout();
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
			this.tabPageEmailAddresses.SuspendLayout();
			this.tabPageTelephoneNumbers.SuspendLayout();
			this.tabPageHistory.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlMain
			// 
			this.tabControlMain.Controls.Add(this.tabPageStructureOverview);
			this.tabControlMain.Controls.Add(this.tabPageHierarchy);
			this.tabControlMain.Controls.Add(this.tabPageSearch);
			this.tabControlMain.Controls.Add(this.tabPageRobots);
			this.tabControlMain.Controls.Add(this.tabPageSitemaps);
			this.tabControlMain.Controls.Add(this.tabPageCanonicalAnalysis);
			this.tabControlMain.Controls.Add(this.tabPageHrefLangAnalysis);
			this.tabControlMain.Controls.Add(this.tabPageRedirectsAudit);
			this.tabControlMain.Controls.Add(this.tabPageErrors);
			this.tabControlMain.Controls.Add(this.tabPageHostnames);
			this.tabControlMain.Controls.Add(this.tabPageLinks);
			this.tabControlMain.Controls.Add(this.tabPageHyperlinks);
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
			this.tabControlMain.Controls.Add(this.tabPageEmailAddresses);
			this.tabControlMain.Controls.Add(this.tabPageTelephoneNumbers);
			this.tabControlMain.Controls.Add(this.tabPageHistory);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(0, 0);
			this.tabControlMain.Margin = new System.Windows.Forms.Padding(0);
			this.tabControlMain.Multiline = true;
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(800, 500);
			this.tabControlMain.TabIndex = 0;
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
			this.tableLayoutPanelStructure.Size = new System.Drawing.Size(700, 400);
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
			this.listViewStructure.Location = new System.Drawing.Point(250, 114);
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
			this.toolStripSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripSearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStructureFilterMenu,
			this.toolStripStructureButtonShowAll,
			this.toolStripSeparator4,
			this.toolStripLabel3,
			this.toolStripStructureSearchTextBoxSearchUrl,
			this.toolStripSeparator7,
			this.toolStripLabel1,
			this.toolStripStructureSearchTextBoxSearch,
			this.toolStripSeparator3,
			this.toolStripLabelStructureItems});
			this.toolStripSearch.Location = new System.Drawing.Point(0, 0);
			this.toolStripSearch.Name = "toolStripSearch";
			this.toolStripSearch.Padding = new System.Windows.Forms.Padding(0);
			this.toolStripSearch.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripSearch.Size = new System.Drawing.Size(700, 28);
			this.toolStripSearch.TabIndex = 1;
			this.toolStripSearch.Text = "toolStrip1";
			// 
			// toolStripStructureFilterMenu
			// 
			this.toolStripStructureFilterMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStructureFilterMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.allDocumentTypesToolStripMenuItem,
			this.HtmlToolStripMenuItem,
			this.stylesheetsToolStripMenuItem,
			this.javaScriptsToolStripMenuItem,
			this.imagesToolStripMenuItem,
			this.PdfsToolStripMenuItem,
			this.audioToolStripMenuItem,
			this.videoToolStripMenuItem,
			this.sitemapXMLToolStripMenuItem,
			this.miscellaneousToolStripMenuItem});
			this.toolStripStructureFilterMenu.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStructureFilterMenu.Image")));
			this.toolStripStructureFilterMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripStructureFilterMenu.Name = "toolStripStructureFilterMenu";
			this.toolStripStructureFilterMenu.Size = new System.Drawing.Size(46, 25);
			this.toolStripStructureFilterMenu.Text = "Filter";
			this.toolStripStructureFilterMenu.ToolTipText = "Filter results";
			// 
			// allDocumentTypesToolStripMenuItem
			// 
			this.allDocumentTypesToolStripMenuItem.Name = "allDocumentTypesToolStripMenuItem";
			this.allDocumentTypesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.allDocumentTypesToolStripMenuItem.Tag = "ALL";
			this.allDocumentTypesToolStripMenuItem.Text = "All Document Types";
			// 
			// HtmlToolStripMenuItem
			// 
			this.HtmlToolStripMenuItem.Name = "HtmlToolStripMenuItem";
			this.HtmlToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.HtmlToolStripMenuItem.Tag = "HTML";
			this.HtmlToolStripMenuItem.Text = "HTML";
			// 
			// stylesheetsToolStripMenuItem
			// 
			this.stylesheetsToolStripMenuItem.Name = "stylesheetsToolStripMenuItem";
			this.stylesheetsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.stylesheetsToolStripMenuItem.Tag = "CSS";
			this.stylesheetsToolStripMenuItem.Text = "Stylesheets";
			// 
			// javaScriptsToolStripMenuItem
			// 
			this.javaScriptsToolStripMenuItem.Name = "javaScriptsToolStripMenuItem";
			this.javaScriptsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.javaScriptsToolStripMenuItem.Tag = "JAVASCRIPT";
			this.javaScriptsToolStripMenuItem.Text = "JavaScripts";
			// 
			// imagesToolStripMenuItem
			// 
			this.imagesToolStripMenuItem.Name = "imagesToolStripMenuItem";
			this.imagesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.imagesToolStripMenuItem.Tag = "IMAGE";
			this.imagesToolStripMenuItem.Text = "Images";
			// 
			// PdfsToolStripMenuItem
			// 
			this.PdfsToolStripMenuItem.Name = "PdfsToolStripMenuItem";
			this.PdfsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.PdfsToolStripMenuItem.Tag = "PDF";
			this.PdfsToolStripMenuItem.Text = "PDFs";
			// 
			// audioToolStripMenuItem
			// 
			this.audioToolStripMenuItem.Name = "audioToolStripMenuItem";
			this.audioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.audioToolStripMenuItem.Tag = "AUDIO";
			this.audioToolStripMenuItem.Text = "Audio Files";
			// 
			// videoToolStripMenuItem
			// 
			this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
			this.videoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.videoToolStripMenuItem.Tag = "VIDEO";
			this.videoToolStripMenuItem.Text = "Video Files";
			// 
			// sitemapXMLToolStripMenuItem
			// 
			this.sitemapXMLToolStripMenuItem.Name = "sitemapXMLToolStripMenuItem";
			this.sitemapXMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.sitemapXMLToolStripMenuItem.Tag = "SITEMAPXML";
			this.sitemapXMLToolStripMenuItem.Text = "XML Sitemaps";
			// 
			// miscellaneousToolStripMenuItem
			// 
			this.miscellaneousToolStripMenuItem.Name = "miscellaneousToolStripMenuItem";
			this.miscellaneousToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
			// toolStripLabel3
			// 
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(110, 25);
			this.toolStripLabel3.Text = "Search within URLs:";
			// 
			// toolStripStructureSearchTextBoxSearchUrl
			// 
			this.toolStripStructureSearchTextBoxSearchUrl.Name = "toolStripStructureSearchTextBoxSearchUrl";
			this.toolStripStructureSearchTextBoxSearchUrl.Size = new System.Drawing.Size(150, 28);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(103, 25);
			this.toolStripLabel1.Text = "Search within text:";
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
			this.toolStripLabelStructureItems.Size = new System.Drawing.Size(80, 15);
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
			this.columnHeaderSearchCollectionPageDescription.Text = "Meta Description";
			this.columnHeaderSearchCollectionPageDescription.Width = 300;
			// 
			// columnHeaderSearchCollectionPageKeywords
			// 
			this.columnHeaderSearchCollectionPageKeywords.Text = "Meta Keywords";
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
			this.listViewRobots.ContextMenuStrip = this.contextMenuStripStructure;
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
			this.listViewSitemaps.ContextMenuStrip = this.contextMenuStripStructure;
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
			this.CanonicalAnalysisStatusCode,
			this.CanonicalAnalysisCanonical});
			this.listViewCanonicalAnalysis.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewCanonicalAnalysis.FullRowSelect = true;
			this.listViewCanonicalAnalysis.GridLines = true;
			this.listViewCanonicalAnalysis.Location = new System.Drawing.Point(10, 10);
			this.listViewCanonicalAnalysis.Margin = new System.Windows.Forms.Padding(0);
			this.listViewCanonicalAnalysis.Name = "listViewCanonicalAnalysis";
			this.listViewCanonicalAnalysis.Size = new System.Drawing.Size(500, 200);
			this.listViewCanonicalAnalysis.TabIndex = 0;
			this.listViewCanonicalAnalysis.UseCompatibleStateImageBehavior = false;
			this.listViewCanonicalAnalysis.View = System.Windows.Forms.View.Details;
			// 
			// CanonicalAnalysisUrl
			// 
			this.CanonicalAnalysisUrl.Text = "URL";
			this.CanonicalAnalysisUrl.Width = 300;
			// 
			// CanonicalAnalysisStatusCode
			// 
			this.CanonicalAnalysisStatusCode.Text = "Status Code";
			this.CanonicalAnalysisStatusCode.Width = 80;
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
			this.listViewHrefLang.ContextMenuStrip = this.contextMenuStripStructure;
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
			this.columnHeaderErrorsStatus,
			this.columnHeaderErrorsDescription});
			this.listViewErrors.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewErrors.GridLines = true;
			this.listViewErrors.Location = new System.Drawing.Point(10, 10);
			this.listViewErrors.Margin = new System.Windows.Forms.Padding(0);
			this.listViewErrors.Name = "listViewErrors";
			this.listViewErrors.Size = new System.Drawing.Size(700, 200);
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
			// columnHeaderErrorsStatus
			// 
			this.columnHeaderErrorsStatus.Text = "Status";
			this.columnHeaderErrorsStatus.Width = 150;
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
			// tabPageLinks
			// 
			this.tabPageLinks.Controls.Add(this.tableLayoutPanelLinks);
			this.tabPageLinks.Location = new System.Drawing.Point(4, 58);
			this.tabPageLinks.Name = "tabPageLinks";
			this.tabPageLinks.Size = new System.Drawing.Size(792, 438);
			this.tabPageLinks.TabIndex = 26;
			this.tabPageLinks.Text = "Links";
			this.tabPageLinks.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelLinks
			// 
			this.tableLayoutPanelLinks.ColumnCount = 1;
			this.tableLayoutPanelLinks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelLinks.Controls.Add(this.toolStrip2, 0, 0);
			this.tableLayoutPanelLinks.Controls.Add(this.listViewLinks, 0, 1);
			this.tableLayoutPanelLinks.Location = new System.Drawing.Point(46, 19);
			this.tableLayoutPanelLinks.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelLinks.Name = "tableLayoutPanelLinks";
			this.tableLayoutPanelLinks.RowCount = 2;
			this.tableLayoutPanelLinks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanelLinks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelLinks.Size = new System.Drawing.Size(700, 400);
			this.tableLayoutPanelLinks.TabIndex = 4;
			// 
			// toolStrip2
			// 
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripButtonLinksShowAll,
			this.toolStripSeparator11,
			this.toolStripLabel6,
			this.toolStripTextBoxLinksSearchSourceUrls,
			this.toolStripSeparator12,
			this.toolStripLabel7,
			this.toolStripTextBoxLinksSearchTargetUrls,
			this.toolStripSeparator13,
			this.toolStripLabelLinksUrls});
			this.toolStrip2.Location = new System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip2.Size = new System.Drawing.Size(700, 28);
			this.toolStrip2.TabIndex = 1;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// toolStripButtonLinksShowAll
			// 
			this.toolStripButtonLinksShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonLinksShowAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLinksShowAll.Image")));
			this.toolStripButtonLinksShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonLinksShowAll.Name = "toolStripButtonLinksShowAll";
			this.toolStripButtonLinksShowAll.Size = new System.Drawing.Size(55, 25);
			this.toolStripButtonLinksShowAll.Text = "Show all";
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabel6
			// 
			this.toolStripLabel6.Name = "toolStripLabel6";
			this.toolStripLabel6.Size = new System.Drawing.Size(112, 25);
			this.toolStripLabel6.Text = "Search source URLs:";
			// 
			// toolStripTextBoxLinksSearchSourceUrls
			// 
			this.toolStripTextBoxLinksSearchSourceUrls.Name = "toolStripTextBoxLinksSearchSourceUrls";
			this.toolStripTextBoxLinksSearchSourceUrls.Size = new System.Drawing.Size(150, 28);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabel7
			// 
			this.toolStripLabel7.Name = "toolStripLabel7";
			this.toolStripLabel7.Size = new System.Drawing.Size(108, 25);
			this.toolStripLabel7.Text = "Search target URLs:";
			// 
			// toolStripTextBoxLinksSearchTargetUrls
			// 
			this.toolStripTextBoxLinksSearchTargetUrls.Name = "toolStripTextBoxLinksSearchTargetUrls";
			this.toolStripTextBoxLinksSearchTargetUrls.Size = new System.Drawing.Size(150, 28);
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabelLinksUrls
			// 
			this.toolStripLabelLinksUrls.Name = "toolStripLabelLinksUrls";
			this.toolStripLabelLinksUrls.Size = new System.Drawing.Size(45, 25);
			this.toolStripLabelLinksUrls.Text = "URLs: 0";
			// 
			// listViewLinks
			// 
			this.listViewLinks.CausesValidation = false;
			this.listViewLinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderLinksOutClass,
			this.columnHeaderLinksOutSourceUrl,
			this.columnHeaderLinksOutTargetUrl,
			this.columnHeaderLinksOutFollow,
			this.columnHeaderLinksOutAltText});
			this.listViewLinks.FullRowSelect = true;
			this.listViewLinks.GridLines = true;
			this.listViewLinks.Location = new System.Drawing.Point(0, 28);
			this.listViewLinks.Margin = new System.Windows.Forms.Padding(0);
			this.listViewLinks.Name = "listViewLinks";
			this.listViewLinks.Size = new System.Drawing.Size(500, 200);
			this.listViewLinks.TabIndex = 4;
			this.listViewLinks.UseCompatibleStateImageBehavior = false;
			this.listViewLinks.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderLinksOutClass
			// 
			this.columnHeaderLinksOutClass.Text = "Class";
			this.columnHeaderLinksOutClass.Width = 100;
			// 
			// columnHeaderLinksOutSourceUrl
			// 
			this.columnHeaderLinksOutSourceUrl.Text = "Source URL";
			this.columnHeaderLinksOutSourceUrl.Width = 300;
			// 
			// columnHeaderLinksOutTargetUrl
			// 
			this.columnHeaderLinksOutTargetUrl.Text = "Target URL";
			this.columnHeaderLinksOutTargetUrl.Width = 300;
			// 
			// columnHeaderLinksOutFollow
			// 
			this.columnHeaderLinksOutFollow.DisplayIndex = 4;
			this.columnHeaderLinksOutFollow.Text = "Follow";
			// 
			// columnHeaderLinksOutAltText
			// 
			this.columnHeaderLinksOutAltText.DisplayIndex = 3;
			this.columnHeaderLinksOutAltText.Text = "Alt Text";
			// 
			// tabPageHyperlinks
			// 
			this.tabPageHyperlinks.Controls.Add(this.tableLayoutPanelHyperlinks);
			this.tabPageHyperlinks.Location = new System.Drawing.Point(4, 58);
			this.tabPageHyperlinks.Name = "tabPageHyperlinks";
			this.tabPageHyperlinks.Size = new System.Drawing.Size(792, 438);
			this.tabPageHyperlinks.TabIndex = 25;
			this.tabPageHyperlinks.Text = "Hyperlinks";
			this.tabPageHyperlinks.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelHyperlinks
			// 
			this.tableLayoutPanelHyperlinks.ColumnCount = 1;
			this.tableLayoutPanelHyperlinks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelHyperlinks.Controls.Add(this.listViewHyperlinks, 0, 1);
			this.tableLayoutPanelHyperlinks.Controls.Add(this.toolStrip1, 0, 0);
			this.tableLayoutPanelHyperlinks.Location = new System.Drawing.Point(46, 19);
			this.tableLayoutPanelHyperlinks.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelHyperlinks.Name = "tableLayoutPanelHyperlinks";
			this.tableLayoutPanelHyperlinks.RowCount = 2;
			this.tableLayoutPanelHyperlinks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanelHyperlinks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelHyperlinks.Size = new System.Drawing.Size(700, 400);
			this.tableLayoutPanelHyperlinks.TabIndex = 3;
			// 
			// listViewHyperlinks
			// 
			this.listViewHyperlinks.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.listViewHyperlinks.BackColor = System.Drawing.SystemColors.Window;
			this.listViewHyperlinks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewHyperlinks.CausesValidation = false;
			this.listViewHyperlinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderLinksUrlSource,
			this.columnHeaderLinksUrlTarget,
			this.columnHeaderLinksLinkText,
			this.columnHeaderLinksLinkTitle,
			this.columnHeaderLinksLinkAltText});
			this.listViewHyperlinks.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewHyperlinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.listViewHyperlinks.FullRowSelect = true;
			this.listViewHyperlinks.GridLines = true;
			this.listViewHyperlinks.LabelWrap = false;
			this.listViewHyperlinks.Location = new System.Drawing.Point(250, 114);
			this.listViewHyperlinks.Margin = new System.Windows.Forms.Padding(0);
			this.listViewHyperlinks.MultiSelect = false;
			this.listViewHyperlinks.Name = "listViewHyperlinks";
			this.listViewHyperlinks.ShowGroups = false;
			this.listViewHyperlinks.Size = new System.Drawing.Size(200, 200);
			this.listViewHyperlinks.TabIndex = 0;
			this.listViewHyperlinks.UseCompatibleStateImageBehavior = false;
			this.listViewHyperlinks.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderLinksUrlSource
			// 
			this.columnHeaderLinksUrlSource.Text = "Source URL";
			this.columnHeaderLinksUrlSource.Width = 300;
			// 
			// columnHeaderLinksUrlTarget
			// 
			this.columnHeaderLinksUrlTarget.Text = "Target URL";
			this.columnHeaderLinksUrlTarget.Width = 300;
			// 
			// columnHeaderLinksLinkText
			// 
			this.columnHeaderLinksLinkText.Text = "Link Text";
			this.columnHeaderLinksLinkText.Width = 150;
			// 
			// columnHeaderLinksLinkTitle
			// 
			this.columnHeaderLinksLinkTitle.Text = "Link Title";
			this.columnHeaderLinksLinkTitle.Width = 150;
			// 
			// columnHeaderLinksLinkAltText
			// 
			this.columnHeaderLinksLinkAltText.Text = "Alt Text";
			this.columnHeaderLinksLinkAltText.Width = 150;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripButtonHyperlinksShowAll,
			this.toolStripSeparator8,
			this.toolStripLabel4,
			this.toolStripTextBoxHyperlinksSearchSourceUrls,
			this.toolStripSeparator9,
			this.toolStripLabel5,
			this.toolStripTextBoxHyperlinksSearchTargetUrls,
			this.toolStripSeparator10,
			this.toolStripLabelHyperlinksUrls});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(700, 28);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonHyperlinksShowAll
			// 
			this.toolStripButtonHyperlinksShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonHyperlinksShowAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHyperlinksShowAll.Image")));
			this.toolStripButtonHyperlinksShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonHyperlinksShowAll.Name = "toolStripButtonHyperlinksShowAll";
			this.toolStripButtonHyperlinksShowAll.Size = new System.Drawing.Size(55, 25);
			this.toolStripButtonHyperlinksShowAll.Text = "Show all";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabel4
			// 
			this.toolStripLabel4.Name = "toolStripLabel4";
			this.toolStripLabel4.Size = new System.Drawing.Size(112, 25);
			this.toolStripLabel4.Text = "Search source URLs:";
			// 
			// toolStripTextBoxHyperlinksSearchSourceUrls
			// 
			this.toolStripTextBoxHyperlinksSearchSourceUrls.Name = "toolStripTextBoxHyperlinksSearchSourceUrls";
			this.toolStripTextBoxHyperlinksSearchSourceUrls.Size = new System.Drawing.Size(150, 28);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabel5
			// 
			this.toolStripLabel5.Name = "toolStripLabel5";
			this.toolStripLabel5.Size = new System.Drawing.Size(108, 25);
			this.toolStripLabel5.Text = "Search target URLs:";
			// 
			// toolStripTextBoxHyperlinksSearchTargetUrls
			// 
			this.toolStripTextBoxHyperlinksSearchTargetUrls.Name = "toolStripTextBoxHyperlinksSearchTargetUrls";
			this.toolStripTextBoxHyperlinksSearchTargetUrls.Size = new System.Drawing.Size(150, 28);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(6, 28);
			// 
			// toolStripLabelHyperlinksUrls
			// 
			this.toolStripLabelHyperlinksUrls.Name = "toolStripLabelHyperlinksUrls";
			this.toolStripLabelHyperlinksUrls.Size = new System.Drawing.Size(45, 25);
			this.toolStripLabelHyperlinksUrls.Text = "URLs: 0";
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
			this.listViewUriAnalysis.CausesValidation = false;
			this.listViewUriAnalysis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderUriAnalysisUrl,
			this.columnHeaderUriAnalysisStatusCode,
			this.columnHeaderUriAnalysisStatus,
			this.columnHeaderUriAnalysisCount,
			this.columnHeaderUriAnalysisChecksum});
			this.listViewUriAnalysis.ContextMenuStrip = this.contextMenuStripStructure;
			this.listViewUriAnalysis.FullRowSelect = true;
			this.listViewUriAnalysis.GridLines = true;
			this.listViewUriAnalysis.LabelWrap = false;
			this.listViewUriAnalysis.Location = new System.Drawing.Point(20, 20);
			this.listViewUriAnalysis.Margin = new System.Windows.Forms.Padding(0);
			this.listViewUriAnalysis.Name = "listViewUriAnalysis";
			this.listViewUriAnalysis.Size = new System.Drawing.Size(700, 200);
			this.listViewUriAnalysis.TabIndex = 0;
			this.listViewUriAnalysis.UseCompatibleStateImageBehavior = false;
			this.listViewUriAnalysis.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderUriAnalysisUrl
			// 
			this.columnHeaderUriAnalysisUrl.Text = "URL";
			this.columnHeaderUriAnalysisUrl.Width = 400;
			// 
			// columnHeaderUriAnalysisStatusCode
			// 
			this.columnHeaderUriAnalysisStatusCode.Text = "Status Code";
			this.columnHeaderUriAnalysisStatusCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderUriAnalysisStatusCode.Width = 100;
			// 
			// columnHeaderUriAnalysisStatus
			// 
			this.columnHeaderUriAnalysisStatus.Text = "Status";
			this.columnHeaderUriAnalysisStatus.Width = 150;
			// 
			// columnHeaderUriAnalysisCount
			// 
			this.columnHeaderUriAnalysisCount.Text = "Occurences";
			this.columnHeaderUriAnalysisCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderUriAnalysisCount.Width = 100;
			// 
			// columnHeaderUriAnalysisChecksum
			// 
			this.columnHeaderUriAnalysisChecksum.Text = "Checksum";
			this.columnHeaderUriAnalysisChecksum.Width = 250;
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
			this.tabPagePageDescriptions.Text = "Meta Descriptions";
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
			this.tabPagePageKeywords.Text = "Meta Keywords";
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
			this.tableLayoutPanelStructure.PerformLayout();
			this.contextMenuStripStructure.ResumeLayout(false);
			this.toolStripSearch.ResumeLayout(false);
			this.toolStripSearch.PerformLayout();
			this.tabPageHierarchy.ResumeLayout(false);
			this.tabPageSearch.ResumeLayout(false);
			this.tableLayoutPanelSearchCollection.ResumeLayout(false);
			this.toolStripSearchCollection.ResumeLayout(false);
			this.toolStripSearchCollection.PerformLayout();
			this.tabPageRobots.ResumeLayout(false);
			this.tabPageSitemaps.ResumeLayout(false);
			this.tabPageCanonicalAnalysis.ResumeLayout(false);
			this.tabPageHrefLangAnalysis.ResumeLayout(false);
			this.tabPageRedirectsAudit.ResumeLayout(false);
			this.tabPageErrors.ResumeLayout(false);
			this.tabPageHostnames.ResumeLayout(false);
			this.tabPageLinks.ResumeLayout(false);
			this.tableLayoutPanelLinks.ResumeLayout(false);
			this.tableLayoutPanelLinks.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.tabPageHyperlinks.ResumeLayout(false);
			this.tableLayoutPanelHyperlinks.ResumeLayout(false);
			this.tableLayoutPanelHyperlinks.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
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
			this.tabPageEmailAddresses.ResumeLayout(false);
			this.tabPageTelephoneNumbers.ResumeLayout(false);
			this.tabPageHistory.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
