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
	partial class MacroscopeDocumentDetails : MacroscopeUserControl
  {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		public System.Windows.Forms.TabControl tabControlDocument;
		public System.Windows.Forms.TabPage tabPageDocumentInfo;
		public System.Windows.Forms.TabPage tabPageHrefLangAnalysis;
		public System.Windows.Forms.TabPage tabPageHyperlinksIn;
		public System.Windows.Forms.TabPage tabPageHyperlinksOut;
		public System.Windows.Forms.TabPage tabPageImages;
		public System.Windows.Forms.TabPage tabPageStylesheets;
		public System.Windows.Forms.TabPage tabPageJavaScripts;
		public System.Windows.Forms.ListView listViewDocumentInfo;
		public System.Windows.Forms.ColumnHeader DocDetailsDetail;
		private System.Windows.Forms.ColumnHeader DocDetailsValue;
		public System.Windows.Forms.ListView listViewHrefLang;
		public System.Windows.Forms.ColumnHeader HyperlinksInLinkType;
		private System.Windows.Forms.ColumnHeader HyperlinksInSourceUrl;
		public System.Windows.Forms.ListView listViewHyperlinksIn;
		public System.Windows.Forms.ListView listViewImages;
		public System.Windows.Forms.ColumnHeader columnHeaderImagesUrl;
		public System.Windows.Forms.ListView listViewStylesheets;
		public System.Windows.Forms.ColumnHeader columnHeaderStylesheetsUrl;
		public System.Windows.Forms.ListView listViewJavascripts;
		public System.Windows.Forms.ColumnHeader columnHeaderJavascriptsUrl;
		public System.Windows.Forms.ColumnHeader HyperlinksInTargetUrl;
		private System.Windows.Forms.ColumnHeader HyperlinksInLinkText;
		private System.Windows.Forms.ColumnHeader HyperlinksInImageAltText;
		private System.Windows.Forms.ColumnHeader HyperlinksInFollow;
		private System.Windows.Forms.ColumnHeader HrefLangUrl;
		public System.Windows.Forms.ListView listViewHyperlinksOut;
		public System.Windows.Forms.ColumnHeader HyperlinksOutLinkType;
		private System.Windows.Forms.ColumnHeader HyperlinksOutSourceUrl;
		public System.Windows.Forms.ColumnHeader HyperlinksOutTargetUrl;
		private System.Windows.Forms.ColumnHeader HyperlinksOutLinkText;
		private System.Windows.Forms.ColumnHeader HyperlinksOutImageAltText;
		private System.Windows.Forms.ColumnHeader HyperlinksOutFollow;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripCopyRecords;
		public System.Windows.Forms.ToolStripMenuItem copyRows;
		public System.Windows.Forms.SplitContainer splitContainerDocumentDetails;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDocumentDetailsDetails;
		public System.Windows.Forms.PictureBox pictureBoxDocumentDetailsImage;
		public System.Windows.Forms.ToolStripMenuItem copyValues;
		private System.Windows.Forms.TabPage tabPageHttpHeaders;
		public System.Windows.Forms.TextBox textBoxHttpResponseHeaders;
		public System.Windows.Forms.ListView listViewDocInfo;
		public System.Windows.Forms.TabPage tabPageInsecureLinks;
		public System.Windows.Forms.TabPage tabPageAudios;
		public System.Windows.Forms.TabPage tabPageVideos;
		public System.Windows.Forms.TabPage tabPageKeywordAnalysis;
		public System.Windows.Forms.ListView listViewInsecureLinks;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		public System.Windows.Forms.ListView listViewAudios;
		public System.Windows.Forms.ColumnHeader columnHeaderAudiosUrl;
		public System.Windows.Forms.ListView listViewVideos;
		public System.Windows.Forms.ColumnHeader columnHeaderVideosUrl;
		public System.Windows.Forms.ListView listViewKeywordAnalysis;
		public System.Windows.Forms.ColumnHeader columnHeaderKeywordAnalysisTerm;
		public System.Windows.Forms.ColumnHeader columnHeaderKeywordAnalysisOccurences;
		public System.Windows.Forms.ColumnHeader columnHeaderImagesItem;
		public System.Windows.Forms.ColumnHeader columnHeaderStylesheetsItem;
		private System.Windows.Forms.ColumnHeader columnHeaderJavascriptsItem;
		private System.Windows.Forms.ColumnHeader columnHeaderAudiosItem;
		private System.Windows.Forms.ColumnHeader columnHeaderVideosItem;
		public System.Windows.Forms.ColumnHeader columnHeaderImagesTitle;
		public System.Windows.Forms.ColumnHeader columnHeaderImagesAltText;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHttpHeaders;
		public System.Windows.Forms.TextBox textBoxHttpRequestHeaders;
		public System.Windows.Forms.TabPage tabPageLinksIn;
		public System.Windows.Forms.TabPage tabPageLinksOut;
		public System.Windows.Forms.ListView listViewLinksIn;
		public System.Windows.Forms.ColumnHeader columnHeaderLinksInLinkType;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksInSourceUrl;
		public System.Windows.Forms.ColumnHeader columnHeaderLinksInTargetUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksInAltText;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksInFollow;
		public System.Windows.Forms.ListView listViewLinksOut;
		public System.Windows.Forms.ColumnHeader columnHeaderLinksOutLinkType;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutSourceUrl;
		public System.Windows.Forms.ColumnHeader columnHeaderLinksOutTargetUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutFollow;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutAltText;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksInRawSourceUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksInRawTargetUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutRawSourceUrl;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutRawTargetUrl;
		private System.Windows.Forms.TabPage tabPageMetaTags;
		public System.Windows.Forms.ListView listViewMetaTags;
		public System.Windows.Forms.ColumnHeader columnHeaderMetaTagsName;
		public System.Windows.Forms.ColumnHeader columnHeaderMetaTagsContent;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksInCount;
		private System.Windows.Forms.ColumnHeader columnHeaderLinksOutCount;
		private System.Windows.Forms.ColumnHeader HyperlinksInCount;
		private System.Windows.Forms.ColumnHeader HyperlinksOutCount;
		private System.Windows.Forms.TabPage tabPageRemarks;
		public System.Windows.Forms.ListView listViewRemarks;
		private System.Windows.Forms.ColumnHeader remarksItem;
		public System.Windows.Forms.ColumnHeader remarksUrl;
		private System.Windows.Forms.ColumnHeader remarksRemark;
		private System.Windows.Forms.ColumnHeader HyperlinksOutLinkTarget;
		private System.Windows.Forms.TabPage tabPageCustomFilters;
		public System.Windows.Forms.ListView listViewCustomFilters;
		public System.Windows.Forms.ColumnHeader customFiltersItem;
		public System.Windows.Forms.ColumnHeader customFiltersText;
		public System.Windows.Forms.ColumnHeader customFiltersRequirement;
		public System.Windows.Forms.ColumnHeader customFiltersPresence;
		private System.Windows.Forms.ColumnHeader HyperlinksOutRawTargetUrl;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripDocumentLists;
		public System.Windows.Forms.ToolStripMenuItem copyDocumentListRows;
		public System.Windows.Forms.ToolStripMenuItem copyDocumentListValues;
		private System.Windows.Forms.ToolStripMenuItem copySourceUrl;
		private System.Windows.Forms.ToolStripMenuItem copyTargetUrl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem copyRawSourceUrl;
		private System.Windows.Forms.ToolStripMenuItem copyRawTargetUrl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem copyLinkText;
		private System.Windows.Forms.ToolStripMenuItem copyTitleText;
		private System.Windows.Forms.ToolStripMenuItem copyAltText;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem openSourceUrlInBrowser;
		private System.Windows.Forms.ToolStripMenuItem openTargetUrlInBrowser;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.TabPage tabPageDocumentTextRaw;
		public System.Windows.Forms.TextBox textBoxDocumentTextRaw;
		private System.Windows.Forms.TabPage tabPageDocumentTextCleaned;
		public System.Windows.Forms.TextBox textBoxDocumentTextCleaned;
		private System.Windows.Forms.TabPage tabPageBodyTextRaw;
		public System.Windows.Forms.TextBox textBoxBodyTextRaw;

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
      this.tabControlDocument = new System.Windows.Forms.TabControl();
      this.tabPageDocumentInfo = new System.Windows.Forms.TabPage();
      this.listViewDocumentInfo = new System.Windows.Forms.ListView();
      this.DocDetailsDetail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.DocDetailsValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.contextMenuStripCopyRecords = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.copyRows = new System.Windows.Forms.ToolStripMenuItem();
      this.copyValues = new System.Windows.Forms.ToolStripMenuItem();
      this.tabPageHttpHeaders = new System.Windows.Forms.TabPage();
      this.tableLayoutPanelHttpHeaders = new System.Windows.Forms.TableLayoutPanel();
      this.textBoxHttpRequestHeaders = new System.Windows.Forms.TextBox();
      this.textBoxHttpResponseHeaders = new System.Windows.Forms.TextBox();
      this.tabPageMetaTags = new System.Windows.Forms.TabPage();
      this.listViewMetaTags = new System.Windows.Forms.ListView();
      this.columnHeaderMetaTagsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderMetaTagsContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageHrefLangAnalysis = new System.Windows.Forms.TabPage();
      this.listViewHrefLang = new System.Windows.Forms.ListView();
      this.HrefLangUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageLinksIn = new System.Windows.Forms.TabPage();
      this.listViewLinksIn = new System.Windows.Forms.ListView();
      this.columnHeaderLinksInCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksInLinkType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksInSourceUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksInTargetUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksInFollow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksInAltText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksInRawSourceUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksInRawTargetUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageLinksOut = new System.Windows.Forms.TabPage();
      this.listViewLinksOut = new System.Windows.Forms.ListView();
      this.columnHeaderLinksOutCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksOutLinkType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksOutSourceUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksOutTargetUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksOutFollow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksOutAltText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksOutRawSourceUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderLinksOutRawTargetUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.contextMenuStripDocumentLists = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.openSourceUrlInBrowser = new System.Windows.Forms.ToolStripMenuItem();
      this.openTargetUrlInBrowser = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.copySourceUrl = new System.Windows.Forms.ToolStripMenuItem();
      this.copyTargetUrl = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.copyRawSourceUrl = new System.Windows.Forms.ToolStripMenuItem();
      this.copyRawTargetUrl = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.copyLinkText = new System.Windows.Forms.ToolStripMenuItem();
      this.copyTitleText = new System.Windows.Forms.ToolStripMenuItem();
      this.copyAltText = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.copyDocumentListRows = new System.Windows.Forms.ToolStripMenuItem();
      this.copyDocumentListValues = new System.Windows.Forms.ToolStripMenuItem();
      this.tabPageHyperlinksIn = new System.Windows.Forms.TabPage();
      this.listViewHyperlinksIn = new System.Windows.Forms.ListView();
      this.HyperlinksInCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksInLinkType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksInSourceUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksInTargetUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksInFollow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksInLinkText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksInImageAltText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageHyperlinksOut = new System.Windows.Forms.TabPage();
      this.listViewHyperlinksOut = new System.Windows.Forms.ListView();
      this.HyperlinksOutCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksOutLinkType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksOutSourceUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksOutTargetUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksOutFollow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksOutLinkTarget = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksOutLinkText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksOutImageAltText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.HyperlinksOutRawTargetUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageInsecureLinks = new System.Windows.Forms.TabPage();
      this.listViewInsecureLinks = new System.Windows.Forms.ListView();
      this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageStylesheets = new System.Windows.Forms.TabPage();
      this.listViewStylesheets = new System.Windows.Forms.ListView();
      this.columnHeaderStylesheetsItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderStylesheetsUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageJavaScripts = new System.Windows.Forms.TabPage();
      this.listViewJavascripts = new System.Windows.Forms.ListView();
      this.columnHeaderJavascriptsItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderJavascriptsUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageImages = new System.Windows.Forms.TabPage();
      this.listViewImages = new System.Windows.Forms.ListView();
      this.columnHeaderImagesItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderImagesUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderImagesTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderImagesAltText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageAudios = new System.Windows.Forms.TabPage();
      this.listViewAudios = new System.Windows.Forms.ListView();
      this.columnHeaderAudiosItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderAudiosUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageVideos = new System.Windows.Forms.TabPage();
      this.listViewVideos = new System.Windows.Forms.ListView();
      this.columnHeaderVideosItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderVideosUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageKeywordAnalysis = new System.Windows.Forms.TabPage();
      this.listViewKeywordAnalysis = new System.Windows.Forms.ListView();
      this.columnHeaderKeywordAnalysisOccurences = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeaderKeywordAnalysisTerm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageRemarks = new System.Windows.Forms.TabPage();
      this.listViewRemarks = new System.Windows.Forms.ListView();
      this.remarksItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.remarksUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.remarksRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabPageDocumentTextRaw = new System.Windows.Forms.TabPage();
      this.textBoxDocumentTextRaw = new System.Windows.Forms.TextBox();
      this.tabPageDocumentTextCleaned = new System.Windows.Forms.TabPage();
      this.textBoxDocumentTextCleaned = new System.Windows.Forms.TextBox();
      this.tabPageBodyTextRaw = new System.Windows.Forms.TabPage();
      this.textBoxBodyTextRaw = new System.Windows.Forms.TextBox();
      this.tabPageCustomFilters = new System.Windows.Forms.TabPage();
      this.listViewCustomFilters = new System.Windows.Forms.ListView();
      this.customFiltersItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.customFiltersText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.customFiltersRequirement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.customFiltersPresence = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.splitContainerDocumentDetails = new System.Windows.Forms.SplitContainer();
      this.tableLayoutPanelDocumentDetailsDetails = new System.Windows.Forms.TableLayoutPanel();
      this.pictureBoxDocumentDetailsImage = new System.Windows.Forms.PictureBox();
      this.listViewDocInfo = new System.Windows.Forms.ListView();
      this.tabControlDocument.SuspendLayout();
      this.tabPageDocumentInfo.SuspendLayout();
      this.contextMenuStripCopyRecords.SuspendLayout();
      this.tabPageHttpHeaders.SuspendLayout();
      this.tableLayoutPanelHttpHeaders.SuspendLayout();
      this.tabPageMetaTags.SuspendLayout();
      this.tabPageHrefLangAnalysis.SuspendLayout();
      this.tabPageLinksIn.SuspendLayout();
      this.tabPageLinksOut.SuspendLayout();
      this.contextMenuStripDocumentLists.SuspendLayout();
      this.tabPageHyperlinksIn.SuspendLayout();
      this.tabPageHyperlinksOut.SuspendLayout();
      this.tabPageInsecureLinks.SuspendLayout();
      this.tabPageStylesheets.SuspendLayout();
      this.tabPageJavaScripts.SuspendLayout();
      this.tabPageImages.SuspendLayout();
      this.tabPageAudios.SuspendLayout();
      this.tabPageVideos.SuspendLayout();
      this.tabPageKeywordAnalysis.SuspendLayout();
      this.tabPageRemarks.SuspendLayout();
      this.tabPageDocumentTextRaw.SuspendLayout();
      this.tabPageDocumentTextCleaned.SuspendLayout();
      this.tabPageBodyTextRaw.SuspendLayout();
      this.tabPageCustomFilters.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerDocumentDetails)).BeginInit();
      this.splitContainerDocumentDetails.Panel1.SuspendLayout();
      this.splitContainerDocumentDetails.Panel2.SuspendLayout();
      this.splitContainerDocumentDetails.SuspendLayout();
      this.tableLayoutPanelDocumentDetailsDetails.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDocumentDetailsImage)).BeginInit();
      this.SuspendLayout();
      // 
      // tabControlDocument
      // 
      this.tabControlDocument.Controls.Add(this.tabPageDocumentInfo);
      this.tabControlDocument.Controls.Add(this.tabPageHttpHeaders);
      this.tabControlDocument.Controls.Add(this.tabPageMetaTags);
      this.tabControlDocument.Controls.Add(this.tabPageHrefLangAnalysis);
      this.tabControlDocument.Controls.Add(this.tabPageLinksIn);
      this.tabControlDocument.Controls.Add(this.tabPageLinksOut);
      this.tabControlDocument.Controls.Add(this.tabPageHyperlinksIn);
      this.tabControlDocument.Controls.Add(this.tabPageHyperlinksOut);
      this.tabControlDocument.Controls.Add(this.tabPageInsecureLinks);
      this.tabControlDocument.Controls.Add(this.tabPageStylesheets);
      this.tabControlDocument.Controls.Add(this.tabPageJavaScripts);
      this.tabControlDocument.Controls.Add(this.tabPageImages);
      this.tabControlDocument.Controls.Add(this.tabPageAudios);
      this.tabControlDocument.Controls.Add(this.tabPageVideos);
      this.tabControlDocument.Controls.Add(this.tabPageKeywordAnalysis);
      this.tabControlDocument.Controls.Add(this.tabPageRemarks);
      this.tabControlDocument.Controls.Add(this.tabPageDocumentTextRaw);
      this.tabControlDocument.Controls.Add(this.tabPageDocumentTextCleaned);
      this.tabControlDocument.Controls.Add(this.tabPageBodyTextRaw);
      this.tabControlDocument.Controls.Add(this.tabPageCustomFilters);
      this.tabControlDocument.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControlDocument.Location = new System.Drawing.Point(0, 0);
      this.tabControlDocument.Margin = new System.Windows.Forms.Padding(0);
      this.tabControlDocument.Multiline = true;
      this.tabControlDocument.Name = "tabControlDocument";
      this.tabControlDocument.SelectedIndex = 0;
      this.tabControlDocument.Size = new System.Drawing.Size(575, 500);
      this.tabControlDocument.TabIndex = 0;
      // 
      // tabPageDocumentInfo
      // 
      this.tabPageDocumentInfo.Controls.Add(this.listViewDocumentInfo);
      this.tabPageDocumentInfo.Location = new System.Drawing.Point(4, 58);
      this.tabPageDocumentInfo.Name = "tabPageDocumentInfo";
      this.tabPageDocumentInfo.Size = new System.Drawing.Size(567, 438);
      this.tabPageDocumentInfo.TabIndex = 0;
      this.tabPageDocumentInfo.Text = "Document Info";
      this.tabPageDocumentInfo.UseVisualStyleBackColor = true;
      // 
      // listViewDocumentInfo
      // 
      this.listViewDocumentInfo.CausesValidation = false;
      this.listViewDocumentInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DocDetailsDetail,
            this.DocDetailsValue});
      this.listViewDocumentInfo.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewDocumentInfo.FullRowSelect = true;
      this.listViewDocumentInfo.GridLines = true;
      this.listViewDocumentInfo.Location = new System.Drawing.Point(20, 20);
      this.listViewDocumentInfo.Name = "listViewDocumentInfo";
      this.listViewDocumentInfo.Size = new System.Drawing.Size(500, 200);
      this.listViewDocumentInfo.TabIndex = 0;
      this.listViewDocumentInfo.UseCompatibleStateImageBehavior = false;
      this.listViewDocumentInfo.View = System.Windows.Forms.View.Details;
      // 
      // DocDetailsDetail
      // 
      this.DocDetailsDetail.Text = "Detail";
      this.DocDetailsDetail.Width = 200;
      // 
      // DocDetailsValue
      // 
      this.DocDetailsValue.Text = "Value";
      this.DocDetailsValue.Width = 400;
      // 
      // contextMenuStripCopyRecords
      // 
      this.contextMenuStripCopyRecords.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyRows,
            this.copyValues});
      this.contextMenuStripCopyRecords.Name = "contextMenuStripTextCopy";
      this.contextMenuStripCopyRecords.Size = new System.Drawing.Size(140, 48);
      // 
      // copyRows
      // 
      this.copyRows.Name = "copyRows";
      this.copyRows.Size = new System.Drawing.Size(139, 22);
      this.copyRows.Text = "Copy Rows";
      // 
      // copyValues
      // 
      this.copyValues.Name = "copyValues";
      this.copyValues.Size = new System.Drawing.Size(139, 22);
      this.copyValues.Text = "Copy Values";
      // 
      // tabPageHttpHeaders
      // 
      this.tabPageHttpHeaders.Controls.Add(this.tableLayoutPanelHttpHeaders);
      this.tabPageHttpHeaders.Location = new System.Drawing.Point(4, 58);
      this.tabPageHttpHeaders.Name = "tabPageHttpHeaders";
      this.tabPageHttpHeaders.Size = new System.Drawing.Size(567, 438);
      this.tabPageHttpHeaders.TabIndex = 7;
      this.tabPageHttpHeaders.Text = "HTTP Headers";
      this.tabPageHttpHeaders.UseVisualStyleBackColor = true;
      // 
      // tableLayoutPanelHttpHeaders
      // 
      this.tableLayoutPanelHttpHeaders.ColumnCount = 2;
      this.tableLayoutPanelHttpHeaders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanelHttpHeaders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanelHttpHeaders.Controls.Add(this.textBoxHttpRequestHeaders, 0, 0);
      this.tableLayoutPanelHttpHeaders.Controls.Add(this.textBoxHttpResponseHeaders, 1, 0);
      this.tableLayoutPanelHttpHeaders.Location = new System.Drawing.Point(20, 20);
      this.tableLayoutPanelHttpHeaders.Name = "tableLayoutPanelHttpHeaders";
      this.tableLayoutPanelHttpHeaders.RowCount = 1;
      this.tableLayoutPanelHttpHeaders.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanelHttpHeaders.Size = new System.Drawing.Size(500, 300);
      this.tableLayoutPanelHttpHeaders.TabIndex = 1;
      // 
      // textBoxHttpRequestHeaders
      // 
      this.textBoxHttpRequestHeaders.BackColor = System.Drawing.Color.Black;
      this.textBoxHttpRequestHeaders.CausesValidation = false;
      this.textBoxHttpRequestHeaders.ForeColor = System.Drawing.Color.Lime;
      this.textBoxHttpRequestHeaders.Location = new System.Drawing.Point(0, 0);
      this.textBoxHttpRequestHeaders.Margin = new System.Windows.Forms.Padding(0);
      this.textBoxHttpRequestHeaders.Multiline = true;
      this.textBoxHttpRequestHeaders.Name = "textBoxHttpRequestHeaders";
      this.textBoxHttpRequestHeaders.Size = new System.Drawing.Size(200, 200);
      this.textBoxHttpRequestHeaders.TabIndex = 1;
      this.textBoxHttpRequestHeaders.WordWrap = false;
      // 
      // textBoxHttpResponseHeaders
      // 
      this.textBoxHttpResponseHeaders.BackColor = System.Drawing.Color.Black;
      this.textBoxHttpResponseHeaders.CausesValidation = false;
      this.textBoxHttpResponseHeaders.ForeColor = System.Drawing.Color.Gold;
      this.textBoxHttpResponseHeaders.Location = new System.Drawing.Point(250, 0);
      this.textBoxHttpResponseHeaders.Margin = new System.Windows.Forms.Padding(0);
      this.textBoxHttpResponseHeaders.Multiline = true;
      this.textBoxHttpResponseHeaders.Name = "textBoxHttpResponseHeaders";
      this.textBoxHttpResponseHeaders.Size = new System.Drawing.Size(200, 200);
      this.textBoxHttpResponseHeaders.TabIndex = 0;
      this.textBoxHttpResponseHeaders.WordWrap = false;
      // 
      // tabPageMetaTags
      // 
      this.tabPageMetaTags.Controls.Add(this.listViewMetaTags);
      this.tabPageMetaTags.Location = new System.Drawing.Point(4, 58);
      this.tabPageMetaTags.Name = "tabPageMetaTags";
      this.tabPageMetaTags.Size = new System.Drawing.Size(567, 438);
      this.tabPageMetaTags.TabIndex = 14;
      this.tabPageMetaTags.Text = "Meta Tags";
      this.tabPageMetaTags.UseVisualStyleBackColor = true;
      // 
      // listViewMetaTags
      // 
      this.listViewMetaTags.CausesValidation = false;
      this.listViewMetaTags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMetaTagsName,
            this.columnHeaderMetaTagsContent});
      this.listViewMetaTags.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewMetaTags.FullRowSelect = true;
      this.listViewMetaTags.GridLines = true;
      this.listViewMetaTags.Location = new System.Drawing.Point(20, 20);
      this.listViewMetaTags.Name = "listViewMetaTags";
      this.listViewMetaTags.Size = new System.Drawing.Size(500, 200);
      this.listViewMetaTags.TabIndex = 1;
      this.listViewMetaTags.UseCompatibleStateImageBehavior = false;
      this.listViewMetaTags.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderMetaTagsName
      // 
      this.columnHeaderMetaTagsName.Text = "Name";
      this.columnHeaderMetaTagsName.Width = 200;
      // 
      // columnHeaderMetaTagsContent
      // 
      this.columnHeaderMetaTagsContent.Text = "Content";
      this.columnHeaderMetaTagsContent.Width = 400;
      // 
      // tabPageHrefLangAnalysis
      // 
      this.tabPageHrefLangAnalysis.Controls.Add(this.listViewHrefLang);
      this.tabPageHrefLangAnalysis.Location = new System.Drawing.Point(4, 58);
      this.tabPageHrefLangAnalysis.Name = "tabPageHrefLangAnalysis";
      this.tabPageHrefLangAnalysis.Size = new System.Drawing.Size(567, 438);
      this.tabPageHrefLangAnalysis.TabIndex = 1;
      this.tabPageHrefLangAnalysis.Text = "HrefLang Analysis";
      this.tabPageHrefLangAnalysis.UseVisualStyleBackColor = true;
      // 
      // listViewHrefLang
      // 
      this.listViewHrefLang.CausesValidation = false;
      this.listViewHrefLang.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HrefLangUrl});
      this.listViewHrefLang.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewHrefLang.FullRowSelect = true;
      this.listViewHrefLang.GridLines = true;
      this.listViewHrefLang.Location = new System.Drawing.Point(20, 20);
      this.listViewHrefLang.Name = "listViewHrefLang";
      this.listViewHrefLang.Size = new System.Drawing.Size(200, 200);
      this.listViewHrefLang.TabIndex = 1;
      this.listViewHrefLang.UseCompatibleStateImageBehavior = false;
      this.listViewHrefLang.View = System.Windows.Forms.View.Details;
      // 
      // HrefLangUrl
      // 
      this.HrefLangUrl.Text = "URL";
      this.HrefLangUrl.Width = 300;
      // 
      // tabPageLinksIn
      // 
      this.tabPageLinksIn.Controls.Add(this.listViewLinksIn);
      this.tabPageLinksIn.Location = new System.Drawing.Point(4, 58);
      this.tabPageLinksIn.Name = "tabPageLinksIn";
      this.tabPageLinksIn.Size = new System.Drawing.Size(567, 438);
      this.tabPageLinksIn.TabIndex = 12;
      this.tabPageLinksIn.Text = "Links In";
      this.tabPageLinksIn.UseVisualStyleBackColor = true;
      // 
      // listViewLinksIn
      // 
      this.listViewLinksIn.CausesValidation = false;
      this.listViewLinksIn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLinksInCount,
            this.columnHeaderLinksInLinkType,
            this.columnHeaderLinksInSourceUrl,
            this.columnHeaderLinksInTargetUrl,
            this.columnHeaderLinksInFollow,
            this.columnHeaderLinksInAltText,
            this.columnHeaderLinksInRawSourceUrl,
            this.columnHeaderLinksInRawTargetUrl});
      this.listViewLinksIn.FullRowSelect = true;
      this.listViewLinksIn.GridLines = true;
      this.listViewLinksIn.Location = new System.Drawing.Point(20, 20);
      this.listViewLinksIn.Name = "listViewLinksIn";
      this.listViewLinksIn.Size = new System.Drawing.Size(500, 200);
      this.listViewLinksIn.TabIndex = 2;
      this.listViewLinksIn.UseCompatibleStateImageBehavior = false;
      this.listViewLinksIn.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderLinksInCount
      // 
      this.columnHeaderLinksInCount.Text = "Item";
      // 
      // columnHeaderLinksInLinkType
      // 
      this.columnHeaderLinksInLinkType.Text = "Link Type";
      this.columnHeaderLinksInLinkType.Width = 100;
      // 
      // columnHeaderLinksInSourceUrl
      // 
      this.columnHeaderLinksInSourceUrl.Text = "Source URL";
      this.columnHeaderLinksInSourceUrl.Width = 300;
      // 
      // columnHeaderLinksInTargetUrl
      // 
      this.columnHeaderLinksInTargetUrl.Text = "Target URL";
      this.columnHeaderLinksInTargetUrl.Width = 300;
      // 
      // columnHeaderLinksInFollow
      // 
      this.columnHeaderLinksInFollow.Text = "Follow";
      // 
      // columnHeaderLinksInAltText
      // 
      this.columnHeaderLinksInAltText.Text = "Alt Text";
      // 
      // columnHeaderLinksInRawSourceUrl
      // 
      this.columnHeaderLinksInRawSourceUrl.Text = "Raw Source URL";
      this.columnHeaderLinksInRawSourceUrl.Width = 300;
      // 
      // columnHeaderLinksInRawTargetUrl
      // 
      this.columnHeaderLinksInRawTargetUrl.Text = "Raw Target URL";
      this.columnHeaderLinksInRawTargetUrl.Width = 300;
      // 
      // tabPageLinksOut
      // 
      this.tabPageLinksOut.Controls.Add(this.listViewLinksOut);
      this.tabPageLinksOut.Location = new System.Drawing.Point(4, 58);
      this.tabPageLinksOut.Name = "tabPageLinksOut";
      this.tabPageLinksOut.Size = new System.Drawing.Size(567, 438);
      this.tabPageLinksOut.TabIndex = 13;
      this.tabPageLinksOut.Text = "Links Out";
      this.tabPageLinksOut.UseVisualStyleBackColor = true;
      // 
      // listViewLinksOut
      // 
      this.listViewLinksOut.CausesValidation = false;
      this.listViewLinksOut.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLinksOutCount,
            this.columnHeaderLinksOutLinkType,
            this.columnHeaderLinksOutSourceUrl,
            this.columnHeaderLinksOutTargetUrl,
            this.columnHeaderLinksOutFollow,
            this.columnHeaderLinksOutAltText,
            this.columnHeaderLinksOutRawSourceUrl,
            this.columnHeaderLinksOutRawTargetUrl});
      this.listViewLinksOut.ContextMenuStrip = this.contextMenuStripDocumentLists;
      this.listViewLinksOut.FullRowSelect = true;
      this.listViewLinksOut.GridLines = true;
      this.listViewLinksOut.Location = new System.Drawing.Point(20, 20);
      this.listViewLinksOut.Name = "listViewLinksOut";
      this.listViewLinksOut.Size = new System.Drawing.Size(500, 200);
      this.listViewLinksOut.TabIndex = 3;
      this.listViewLinksOut.UseCompatibleStateImageBehavior = false;
      this.listViewLinksOut.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderLinksOutCount
      // 
      this.columnHeaderLinksOutCount.Text = "Item";
      // 
      // columnHeaderLinksOutLinkType
      // 
      this.columnHeaderLinksOutLinkType.Text = "Link Type";
      this.columnHeaderLinksOutLinkType.Width = 100;
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
      this.columnHeaderLinksOutFollow.Text = "Follow";
      // 
      // columnHeaderLinksOutAltText
      // 
      this.columnHeaderLinksOutAltText.Text = "Alt Text";
      // 
      // columnHeaderLinksOutRawSourceUrl
      // 
      this.columnHeaderLinksOutRawSourceUrl.Text = "Raw Source URL";
      this.columnHeaderLinksOutRawSourceUrl.Width = 300;
      // 
      // columnHeaderLinksOutRawTargetUrl
      // 
      this.columnHeaderLinksOutRawTargetUrl.Text = "Raw Target URL";
      this.columnHeaderLinksOutRawTargetUrl.Width = 300;
      // 
      // contextMenuStripDocumentLists
      // 
      this.contextMenuStripDocumentLists.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSourceUrlInBrowser,
            this.openTargetUrlInBrowser,
            this.toolStripSeparator4,
            this.copySourceUrl,
            this.copyTargetUrl,
            this.toolStripSeparator1,
            this.copyRawSourceUrl,
            this.copyRawTargetUrl,
            this.toolStripSeparator3,
            this.copyLinkText,
            this.copyTitleText,
            this.copyAltText,
            this.toolStripSeparator2,
            this.copyDocumentListRows,
            this.copyDocumentListValues});
      this.contextMenuStripDocumentLists.Name = "contextMenuStripTextCopy";
      this.contextMenuStripDocumentLists.Size = new System.Drawing.Size(225, 270);
      // 
      // openSourceUrlInBrowser
      // 
      this.openSourceUrlInBrowser.Name = "openSourceUrlInBrowser";
      this.openSourceUrlInBrowser.Size = new System.Drawing.Size(224, 22);
      this.openSourceUrlInBrowser.Text = "Open Source URL in Browser";
      // 
      // openTargetUrlInBrowser
      // 
      this.openTargetUrlInBrowser.Name = "openTargetUrlInBrowser";
      this.openTargetUrlInBrowser.Size = new System.Drawing.Size(224, 22);
      this.openTargetUrlInBrowser.Text = "Open Target URL in Browser";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(221, 6);
      // 
      // copySourceUrl
      // 
      this.copySourceUrl.Name = "copySourceUrl";
      this.copySourceUrl.Size = new System.Drawing.Size(224, 22);
      this.copySourceUrl.Text = "Copy Source URL";
      // 
      // copyTargetUrl
      // 
      this.copyTargetUrl.Name = "copyTargetUrl";
      this.copyTargetUrl.Size = new System.Drawing.Size(224, 22);
      this.copyTargetUrl.Text = "Copy Target URL";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
      // 
      // copyRawSourceUrl
      // 
      this.copyRawSourceUrl.Name = "copyRawSourceUrl";
      this.copyRawSourceUrl.Size = new System.Drawing.Size(224, 22);
      this.copyRawSourceUrl.Text = "Copy Raw Source URL";
      // 
      // copyRawTargetUrl
      // 
      this.copyRawTargetUrl.Name = "copyRawTargetUrl";
      this.copyRawTargetUrl.Size = new System.Drawing.Size(224, 22);
      this.copyRawTargetUrl.Text = "Copy Raw Target URL";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(221, 6);
      // 
      // copyLinkText
      // 
      this.copyLinkText.Name = "copyLinkText";
      this.copyLinkText.Size = new System.Drawing.Size(224, 22);
      this.copyLinkText.Text = "Copy Link Text";
      // 
      // copyTitleText
      // 
      this.copyTitleText.Name = "copyTitleText";
      this.copyTitleText.Size = new System.Drawing.Size(224, 22);
      this.copyTitleText.Text = "Copy Title Text";
      // 
      // copyAltText
      // 
      this.copyAltText.Name = "copyAltText";
      this.copyAltText.Size = new System.Drawing.Size(224, 22);
      this.copyAltText.Text = "Copy Alt Text";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(221, 6);
      // 
      // copyDocumentListRows
      // 
      this.copyDocumentListRows.Name = "copyDocumentListRows";
      this.copyDocumentListRows.Size = new System.Drawing.Size(224, 22);
      this.copyDocumentListRows.Text = "Copy Rows";
      // 
      // copyDocumentListValues
      // 
      this.copyDocumentListValues.Name = "copyDocumentListValues";
      this.copyDocumentListValues.Size = new System.Drawing.Size(224, 22);
      this.copyDocumentListValues.Text = "Copy Values";
      // 
      // tabPageHyperlinksIn
      // 
      this.tabPageHyperlinksIn.Controls.Add(this.listViewHyperlinksIn);
      this.tabPageHyperlinksIn.Location = new System.Drawing.Point(4, 58);
      this.tabPageHyperlinksIn.Name = "tabPageHyperlinksIn";
      this.tabPageHyperlinksIn.Size = new System.Drawing.Size(567, 438);
      this.tabPageHyperlinksIn.TabIndex = 2;
      this.tabPageHyperlinksIn.Text = "Hyperlinks In";
      this.tabPageHyperlinksIn.UseVisualStyleBackColor = true;
      // 
      // listViewHyperlinksIn
      // 
      this.listViewHyperlinksIn.CausesValidation = false;
      this.listViewHyperlinksIn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HyperlinksInCount,
            this.HyperlinksInLinkType,
            this.HyperlinksInSourceUrl,
            this.HyperlinksInTargetUrl,
            this.HyperlinksInFollow,
            this.HyperlinksInLinkText,
            this.HyperlinksInImageAltText});
      this.listViewHyperlinksIn.ContextMenuStrip = this.contextMenuStripDocumentLists;
      this.listViewHyperlinksIn.FullRowSelect = true;
      this.listViewHyperlinksIn.GridLines = true;
      this.listViewHyperlinksIn.Location = new System.Drawing.Point(20, 20);
      this.listViewHyperlinksIn.Name = "listViewHyperlinksIn";
      this.listViewHyperlinksIn.Size = new System.Drawing.Size(528, 200);
      this.listViewHyperlinksIn.TabIndex = 1;
      this.listViewHyperlinksIn.UseCompatibleStateImageBehavior = false;
      this.listViewHyperlinksIn.View = System.Windows.Forms.View.Details;
      // 
      // HyperlinksInCount
      // 
      this.HyperlinksInCount.Text = "Item";
      // 
      // HyperlinksInLinkType
      // 
      this.HyperlinksInLinkType.Text = "Link Type";
      this.HyperlinksInLinkType.Width = 100;
      // 
      // HyperlinksInSourceUrl
      // 
      this.HyperlinksInSourceUrl.Text = "Source URL";
      this.HyperlinksInSourceUrl.Width = 300;
      // 
      // HyperlinksInTargetUrl
      // 
      this.HyperlinksInTargetUrl.Text = "Target URL";
      this.HyperlinksInTargetUrl.Width = 300;
      // 
      // HyperlinksInFollow
      // 
      this.HyperlinksInFollow.Text = "Follow";
      // 
      // HyperlinksInLinkText
      // 
      this.HyperlinksInLinkText.Text = "Link Text";
      // 
      // HyperlinksInImageAltText
      // 
      this.HyperlinksInImageAltText.Text = "Image Alt Text";
      // 
      // tabPageHyperlinksOut
      // 
      this.tabPageHyperlinksOut.Controls.Add(this.listViewHyperlinksOut);
      this.tabPageHyperlinksOut.Location = new System.Drawing.Point(4, 58);
      this.tabPageHyperlinksOut.Name = "tabPageHyperlinksOut";
      this.tabPageHyperlinksOut.Size = new System.Drawing.Size(567, 438);
      this.tabPageHyperlinksOut.TabIndex = 3;
      this.tabPageHyperlinksOut.Text = "Hyperlinks Out";
      this.tabPageHyperlinksOut.UseVisualStyleBackColor = true;
      // 
      // listViewHyperlinksOut
      // 
      this.listViewHyperlinksOut.CausesValidation = false;
      this.listViewHyperlinksOut.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HyperlinksOutCount,
            this.HyperlinksOutLinkType,
            this.HyperlinksOutSourceUrl,
            this.HyperlinksOutTargetUrl,
            this.HyperlinksOutFollow,
            this.HyperlinksOutLinkTarget,
            this.HyperlinksOutLinkText,
            this.HyperlinksOutImageAltText,
            this.HyperlinksOutRawTargetUrl});
      this.listViewHyperlinksOut.ContextMenuStrip = this.contextMenuStripDocumentLists;
      this.listViewHyperlinksOut.FullRowSelect = true;
      this.listViewHyperlinksOut.GridLines = true;
      this.listViewHyperlinksOut.Location = new System.Drawing.Point(20, 20);
      this.listViewHyperlinksOut.Name = "listViewHyperlinksOut";
      this.listViewHyperlinksOut.Size = new System.Drawing.Size(517, 200);
      this.listViewHyperlinksOut.TabIndex = 2;
      this.listViewHyperlinksOut.UseCompatibleStateImageBehavior = false;
      this.listViewHyperlinksOut.View = System.Windows.Forms.View.Details;
      // 
      // HyperlinksOutCount
      // 
      this.HyperlinksOutCount.Text = "Item";
      // 
      // HyperlinksOutLinkType
      // 
      this.HyperlinksOutLinkType.Text = "Link Type";
      this.HyperlinksOutLinkType.Width = 100;
      // 
      // HyperlinksOutSourceUrl
      // 
      this.HyperlinksOutSourceUrl.Text = "Source URL";
      this.HyperlinksOutSourceUrl.Width = 300;
      // 
      // HyperlinksOutTargetUrl
      // 
      this.HyperlinksOutTargetUrl.Text = "Target URL";
      this.HyperlinksOutTargetUrl.Width = 300;
      // 
      // HyperlinksOutFollow
      // 
      this.HyperlinksOutFollow.Text = "Follow";
      // 
      // HyperlinksOutLinkTarget
      // 
      this.HyperlinksOutLinkTarget.Text = "Target";
      this.HyperlinksOutLinkTarget.Width = 100;
      // 
      // HyperlinksOutLinkText
      // 
      this.HyperlinksOutLinkText.Text = "Link Text";
      // 
      // HyperlinksOutImageAltText
      // 
      this.HyperlinksOutImageAltText.Text = "Image Alt Text";
      // 
      // HyperlinksOutRawTargetUrl
      // 
      this.HyperlinksOutRawTargetUrl.Text = "Raw Target URL";
      this.HyperlinksOutRawTargetUrl.Width = 300;
      // 
      // tabPageInsecureLinks
      // 
      this.tabPageInsecureLinks.Controls.Add(this.listViewInsecureLinks);
      this.tabPageInsecureLinks.Location = new System.Drawing.Point(4, 58);
      this.tabPageInsecureLinks.Name = "tabPageInsecureLinks";
      this.tabPageInsecureLinks.Size = new System.Drawing.Size(567, 438);
      this.tabPageInsecureLinks.TabIndex = 10;
      this.tabPageInsecureLinks.Text = "Insecure Links";
      this.tabPageInsecureLinks.UseVisualStyleBackColor = true;
      // 
      // listViewInsecureLinks
      // 
      this.listViewInsecureLinks.CausesValidation = false;
      this.listViewInsecureLinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13});
      this.listViewInsecureLinks.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewInsecureLinks.FullRowSelect = true;
      this.listViewInsecureLinks.GridLines = true;
      this.listViewInsecureLinks.Location = new System.Drawing.Point(20, 20);
      this.listViewInsecureLinks.Name = "listViewInsecureLinks";
      this.listViewInsecureLinks.Size = new System.Drawing.Size(200, 200);
      this.listViewInsecureLinks.TabIndex = 2;
      this.listViewInsecureLinks.UseCompatibleStateImageBehavior = false;
      this.listViewInsecureLinks.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader13
      // 
      this.columnHeader13.Text = "URL";
      this.columnHeader13.Width = 500;
      // 
      // tabPageStylesheets
      // 
      this.tabPageStylesheets.Controls.Add(this.listViewStylesheets);
      this.tabPageStylesheets.Location = new System.Drawing.Point(4, 58);
      this.tabPageStylesheets.Name = "tabPageStylesheets";
      this.tabPageStylesheets.Size = new System.Drawing.Size(567, 438);
      this.tabPageStylesheets.TabIndex = 5;
      this.tabPageStylesheets.Text = "Stylesheets";
      this.tabPageStylesheets.UseVisualStyleBackColor = true;
      // 
      // listViewStylesheets
      // 
      this.listViewStylesheets.CausesValidation = false;
      this.listViewStylesheets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderStylesheetsItem,
            this.columnHeaderStylesheetsUrl});
      this.listViewStylesheets.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewStylesheets.FullRowSelect = true;
      this.listViewStylesheets.GridLines = true;
      this.listViewStylesheets.Location = new System.Drawing.Point(20, 20);
      this.listViewStylesheets.Name = "listViewStylesheets";
      this.listViewStylesheets.Size = new System.Drawing.Size(200, 200);
      this.listViewStylesheets.TabIndex = 1;
      this.listViewStylesheets.UseCompatibleStateImageBehavior = false;
      this.listViewStylesheets.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderStylesheetsItem
      // 
      this.columnHeaderStylesheetsItem.Text = "Item";
      // 
      // columnHeaderStylesheetsUrl
      // 
      this.columnHeaderStylesheetsUrl.Text = "URL";
      this.columnHeaderStylesheetsUrl.Width = 400;
      // 
      // tabPageJavaScripts
      // 
      this.tabPageJavaScripts.Controls.Add(this.listViewJavascripts);
      this.tabPageJavaScripts.Location = new System.Drawing.Point(4, 58);
      this.tabPageJavaScripts.Name = "tabPageJavaScripts";
      this.tabPageJavaScripts.Size = new System.Drawing.Size(567, 438);
      this.tabPageJavaScripts.TabIndex = 6;
      this.tabPageJavaScripts.Text = "JavaScripts";
      this.tabPageJavaScripts.UseVisualStyleBackColor = true;
      // 
      // listViewJavascripts
      // 
      this.listViewJavascripts.CausesValidation = false;
      this.listViewJavascripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderJavascriptsItem,
            this.columnHeaderJavascriptsUrl});
      this.listViewJavascripts.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewJavascripts.FullRowSelect = true;
      this.listViewJavascripts.GridLines = true;
      this.listViewJavascripts.Location = new System.Drawing.Point(20, 20);
      this.listViewJavascripts.Name = "listViewJavascripts";
      this.listViewJavascripts.Size = new System.Drawing.Size(200, 200);
      this.listViewJavascripts.TabIndex = 1;
      this.listViewJavascripts.UseCompatibleStateImageBehavior = false;
      this.listViewJavascripts.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderJavascriptsItem
      // 
      this.columnHeaderJavascriptsItem.Text = "Item";
      this.columnHeaderJavascriptsItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // columnHeaderJavascriptsUrl
      // 
      this.columnHeaderJavascriptsUrl.Text = "URL";
      this.columnHeaderJavascriptsUrl.Width = 400;
      // 
      // tabPageImages
      // 
      this.tabPageImages.Controls.Add(this.listViewImages);
      this.tabPageImages.Location = new System.Drawing.Point(4, 58);
      this.tabPageImages.Name = "tabPageImages";
      this.tabPageImages.Size = new System.Drawing.Size(567, 438);
      this.tabPageImages.TabIndex = 4;
      this.tabPageImages.Text = "Images";
      this.tabPageImages.UseVisualStyleBackColor = true;
      // 
      // listViewImages
      // 
      this.listViewImages.CausesValidation = false;
      this.listViewImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderImagesItem,
            this.columnHeaderImagesUrl,
            this.columnHeaderImagesTitle,
            this.columnHeaderImagesAltText});
      this.listViewImages.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewImages.FullRowSelect = true;
      this.listViewImages.GridLines = true;
      this.listViewImages.Location = new System.Drawing.Point(20, 20);
      this.listViewImages.Name = "listViewImages";
      this.listViewImages.Size = new System.Drawing.Size(200, 200);
      this.listViewImages.TabIndex = 1;
      this.listViewImages.UseCompatibleStateImageBehavior = false;
      this.listViewImages.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderImagesItem
      // 
      this.columnHeaderImagesItem.Text = "Item";
      // 
      // columnHeaderImagesUrl
      // 
      this.columnHeaderImagesUrl.Text = "URL";
      this.columnHeaderImagesUrl.Width = 400;
      // 
      // columnHeaderImagesTitle
      // 
      this.columnHeaderImagesTitle.Text = "Title";
      this.columnHeaderImagesTitle.Width = 150;
      // 
      // columnHeaderImagesAltText
      // 
      this.columnHeaderImagesAltText.Text = "Alt Text";
      this.columnHeaderImagesAltText.Width = 150;
      // 
      // tabPageAudios
      // 
      this.tabPageAudios.Controls.Add(this.listViewAudios);
      this.tabPageAudios.Location = new System.Drawing.Point(4, 58);
      this.tabPageAudios.Name = "tabPageAudios";
      this.tabPageAudios.Size = new System.Drawing.Size(567, 438);
      this.tabPageAudios.TabIndex = 8;
      this.tabPageAudios.Text = "Audio";
      this.tabPageAudios.UseVisualStyleBackColor = true;
      // 
      // listViewAudios
      // 
      this.listViewAudios.CausesValidation = false;
      this.listViewAudios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAudiosItem,
            this.columnHeaderAudiosUrl});
      this.listViewAudios.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewAudios.FullRowSelect = true;
      this.listViewAudios.GridLines = true;
      this.listViewAudios.Location = new System.Drawing.Point(20, 20);
      this.listViewAudios.Name = "listViewAudios";
      this.listViewAudios.Size = new System.Drawing.Size(200, 200);
      this.listViewAudios.TabIndex = 2;
      this.listViewAudios.UseCompatibleStateImageBehavior = false;
      this.listViewAudios.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderAudiosItem
      // 
      this.columnHeaderAudiosItem.Text = "Item";
      this.columnHeaderAudiosItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // columnHeaderAudiosUrl
      // 
      this.columnHeaderAudiosUrl.Text = "URL";
      this.columnHeaderAudiosUrl.Width = 400;
      // 
      // tabPageVideos
      // 
      this.tabPageVideos.Controls.Add(this.listViewVideos);
      this.tabPageVideos.Location = new System.Drawing.Point(4, 58);
      this.tabPageVideos.Name = "tabPageVideos";
      this.tabPageVideos.Size = new System.Drawing.Size(567, 438);
      this.tabPageVideos.TabIndex = 9;
      this.tabPageVideos.Text = "Video";
      this.tabPageVideos.UseVisualStyleBackColor = true;
      // 
      // listViewVideos
      // 
      this.listViewVideos.CausesValidation = false;
      this.listViewVideos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderVideosItem,
            this.columnHeaderVideosUrl});
      this.listViewVideos.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewVideos.FullRowSelect = true;
      this.listViewVideos.GridLines = true;
      this.listViewVideos.Location = new System.Drawing.Point(20, 20);
      this.listViewVideos.Name = "listViewVideos";
      this.listViewVideos.Size = new System.Drawing.Size(200, 200);
      this.listViewVideos.TabIndex = 3;
      this.listViewVideos.UseCompatibleStateImageBehavior = false;
      this.listViewVideos.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderVideosItem
      // 
      this.columnHeaderVideosItem.Text = "Item";
      this.columnHeaderVideosItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // columnHeaderVideosUrl
      // 
      this.columnHeaderVideosUrl.Text = "URL";
      this.columnHeaderVideosUrl.Width = 500;
      // 
      // tabPageKeywordAnalysis
      // 
      this.tabPageKeywordAnalysis.Controls.Add(this.listViewKeywordAnalysis);
      this.tabPageKeywordAnalysis.Location = new System.Drawing.Point(4, 58);
      this.tabPageKeywordAnalysis.Name = "tabPageKeywordAnalysis";
      this.tabPageKeywordAnalysis.Size = new System.Drawing.Size(567, 438);
      this.tabPageKeywordAnalysis.TabIndex = 11;
      this.tabPageKeywordAnalysis.Text = "Keyword Analysis";
      this.tabPageKeywordAnalysis.UseVisualStyleBackColor = true;
      // 
      // listViewKeywordAnalysis
      // 
      this.listViewKeywordAnalysis.CausesValidation = false;
      this.listViewKeywordAnalysis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderKeywordAnalysisOccurences,
            this.columnHeaderKeywordAnalysisTerm});
      this.listViewKeywordAnalysis.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewKeywordAnalysis.FullRowSelect = true;
      this.listViewKeywordAnalysis.GridLines = true;
      this.listViewKeywordAnalysis.Location = new System.Drawing.Point(20, 20);
      this.listViewKeywordAnalysis.Name = "listViewKeywordAnalysis";
      this.listViewKeywordAnalysis.Size = new System.Drawing.Size(200, 200);
      this.listViewKeywordAnalysis.TabIndex = 3;
      this.listViewKeywordAnalysis.UseCompatibleStateImageBehavior = false;
      this.listViewKeywordAnalysis.View = System.Windows.Forms.View.Details;
      // 
      // columnHeaderKeywordAnalysisOccurences
      // 
      this.columnHeaderKeywordAnalysisOccurences.Text = "Occurrences";
      this.columnHeaderKeywordAnalysisOccurences.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.columnHeaderKeywordAnalysisOccurences.Width = 100;
      // 
      // columnHeaderKeywordAnalysisTerm
      // 
      this.columnHeaderKeywordAnalysisTerm.Text = "Term";
      this.columnHeaderKeywordAnalysisTerm.Width = 400;
      // 
      // tabPageRemarks
      // 
      this.tabPageRemarks.Controls.Add(this.listViewRemarks);
      this.tabPageRemarks.Location = new System.Drawing.Point(4, 58);
      this.tabPageRemarks.Name = "tabPageRemarks";
      this.tabPageRemarks.Size = new System.Drawing.Size(567, 438);
      this.tabPageRemarks.TabIndex = 16;
      this.tabPageRemarks.Text = "Remarks";
      this.tabPageRemarks.UseVisualStyleBackColor = true;
      // 
      // listViewRemarks
      // 
      this.listViewRemarks.CausesValidation = false;
      this.listViewRemarks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.remarksItem,
            this.remarksUrl,
            this.remarksRemark});
      this.listViewRemarks.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewRemarks.FullRowSelect = true;
      this.listViewRemarks.GridLines = true;
      this.listViewRemarks.Location = new System.Drawing.Point(20, 20);
      this.listViewRemarks.Name = "listViewRemarks";
      this.listViewRemarks.Size = new System.Drawing.Size(400, 200);
      this.listViewRemarks.TabIndex = 2;
      this.listViewRemarks.UseCompatibleStateImageBehavior = false;
      this.listViewRemarks.View = System.Windows.Forms.View.Details;
      // 
      // remarksItem
      // 
      this.remarksItem.Text = "Item";
      this.remarksItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // remarksUrl
      // 
      this.remarksUrl.Text = "URL";
      this.remarksUrl.Width = 400;
      // 
      // remarksRemark
      // 
      this.remarksRemark.Text = "Remark";
      this.remarksRemark.Width = 200;
      // 
      // tabPageDocumentTextRaw
      // 
      this.tabPageDocumentTextRaw.Controls.Add(this.textBoxDocumentTextRaw);
      this.tabPageDocumentTextRaw.Location = new System.Drawing.Point(4, 58);
      this.tabPageDocumentTextRaw.Name = "tabPageDocumentTextRaw";
      this.tabPageDocumentTextRaw.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageDocumentTextRaw.Size = new System.Drawing.Size(567, 438);
      this.tabPageDocumentTextRaw.TabIndex = 18;
      this.tabPageDocumentTextRaw.Text = "Raw Document Text";
      this.tabPageDocumentTextRaw.UseVisualStyleBackColor = true;
      // 
      // textBoxDocumentTextRaw
      // 
      this.textBoxDocumentTextRaw.BackColor = System.Drawing.Color.White;
      this.textBoxDocumentTextRaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxDocumentTextRaw.Location = new System.Drawing.Point(183, 119);
      this.textBoxDocumentTextRaw.MaxLength = 1048576;
      this.textBoxDocumentTextRaw.Multiline = true;
      this.textBoxDocumentTextRaw.Name = "textBoxDocumentTextRaw";
      this.textBoxDocumentTextRaw.ReadOnly = true;
      this.textBoxDocumentTextRaw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBoxDocumentTextRaw.Size = new System.Drawing.Size(200, 200);
      this.textBoxDocumentTextRaw.TabIndex = 1;
      // 
      // tabPageDocumentTextCleaned
      // 
      this.tabPageDocumentTextCleaned.Controls.Add(this.textBoxDocumentTextCleaned);
      this.tabPageDocumentTextCleaned.Location = new System.Drawing.Point(4, 58);
      this.tabPageDocumentTextCleaned.Name = "tabPageDocumentTextCleaned";
      this.tabPageDocumentTextCleaned.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageDocumentTextCleaned.Size = new System.Drawing.Size(567, 438);
      this.tabPageDocumentTextCleaned.TabIndex = 19;
      this.tabPageDocumentTextCleaned.Text = "Cleaned Document Text";
      this.tabPageDocumentTextCleaned.UseVisualStyleBackColor = true;
      // 
      // textBoxDocumentTextCleaned
      // 
      this.textBoxDocumentTextCleaned.BackColor = System.Drawing.Color.White;
      this.textBoxDocumentTextCleaned.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxDocumentTextCleaned.Location = new System.Drawing.Point(183, 119);
      this.textBoxDocumentTextCleaned.MaxLength = 1048576;
      this.textBoxDocumentTextCleaned.Multiline = true;
      this.textBoxDocumentTextCleaned.Name = "textBoxDocumentTextCleaned";
      this.textBoxDocumentTextCleaned.ReadOnly = true;
      this.textBoxDocumentTextCleaned.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBoxDocumentTextCleaned.Size = new System.Drawing.Size(200, 200);
      this.textBoxDocumentTextCleaned.TabIndex = 2;
      // 
      // tabPageBodyTextRaw
      // 
      this.tabPageBodyTextRaw.Controls.Add(this.textBoxBodyTextRaw);
      this.tabPageBodyTextRaw.Location = new System.Drawing.Point(4, 58);
      this.tabPageBodyTextRaw.Name = "tabPageBodyTextRaw";
      this.tabPageBodyTextRaw.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageBodyTextRaw.Size = new System.Drawing.Size(567, 438);
      this.tabPageBodyTextRaw.TabIndex = 20;
      this.tabPageBodyTextRaw.Text = "Raw Body Text";
      this.tabPageBodyTextRaw.UseVisualStyleBackColor = true;
      // 
      // textBoxBodyTextRaw
      // 
      this.textBoxBodyTextRaw.BackColor = System.Drawing.Color.White;
      this.textBoxBodyTextRaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBoxBodyTextRaw.Location = new System.Drawing.Point(183, 119);
      this.textBoxBodyTextRaw.MaxLength = 1048576;
      this.textBoxBodyTextRaw.Multiline = true;
      this.textBoxBodyTextRaw.Name = "textBoxBodyTextRaw";
      this.textBoxBodyTextRaw.ReadOnly = true;
      this.textBoxBodyTextRaw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textBoxBodyTextRaw.Size = new System.Drawing.Size(200, 200);
      this.textBoxBodyTextRaw.TabIndex = 2;
      // 
      // tabPageCustomFilters
      // 
      this.tabPageCustomFilters.Controls.Add(this.listViewCustomFilters);
      this.tabPageCustomFilters.Location = new System.Drawing.Point(4, 58);
      this.tabPageCustomFilters.Name = "tabPageCustomFilters";
      this.tabPageCustomFilters.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageCustomFilters.Size = new System.Drawing.Size(567, 438);
      this.tabPageCustomFilters.TabIndex = 17;
      this.tabPageCustomFilters.Text = "Custom Filters";
      this.tabPageCustomFilters.UseVisualStyleBackColor = true;
      // 
      // listViewCustomFilters
      // 
      this.listViewCustomFilters.CausesValidation = false;
      this.listViewCustomFilters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.customFiltersItem,
            this.customFiltersText,
            this.customFiltersRequirement,
            this.customFiltersPresence});
      this.listViewCustomFilters.ContextMenuStrip = this.contextMenuStripCopyRecords;
      this.listViewCustomFilters.FullRowSelect = true;
      this.listViewCustomFilters.GridLines = true;
      this.listViewCustomFilters.Location = new System.Drawing.Point(20, 20);
      this.listViewCustomFilters.Name = "listViewCustomFilters";
      this.listViewCustomFilters.Size = new System.Drawing.Size(200, 200);
      this.listViewCustomFilters.TabIndex = 2;
      this.listViewCustomFilters.UseCompatibleStateImageBehavior = false;
      this.listViewCustomFilters.View = System.Windows.Forms.View.Details;
      // 
      // customFiltersItem
      // 
      this.customFiltersItem.Text = "Item";
      // 
      // customFiltersText
      // 
      this.customFiltersText.Text = "Text";
      this.customFiltersText.Width = 150;
      // 
      // customFiltersRequirement
      // 
      this.customFiltersRequirement.Text = "Requirement";
      this.customFiltersRequirement.Width = 150;
      // 
      // customFiltersPresence
      // 
      this.customFiltersPresence.Text = "Presence";
      this.customFiltersPresence.Width = 150;
      // 
      // splitContainerDocumentDetails
      // 
      this.splitContainerDocumentDetails.BackColor = System.Drawing.SystemColors.Control;
      this.splitContainerDocumentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerDocumentDetails.Location = new System.Drawing.Point(0, 0);
      this.splitContainerDocumentDetails.Margin = new System.Windows.Forms.Padding(0);
      this.splitContainerDocumentDetails.Name = "splitContainerDocumentDetails";
      // 
      // splitContainerDocumentDetails.Panel1
      // 
      this.splitContainerDocumentDetails.Panel1.Controls.Add(this.tabControlDocument);
      this.splitContainerDocumentDetails.Panel1MinSize = 75;
      // 
      // splitContainerDocumentDetails.Panel2
      // 
      this.splitContainerDocumentDetails.Panel2.Controls.Add(this.tableLayoutPanelDocumentDetailsDetails);
      this.splitContainerDocumentDetails.Size = new System.Drawing.Size(800, 500);
      this.splitContainerDocumentDetails.SplitterDistance = 575;
      this.splitContainerDocumentDetails.SplitterWidth = 6;
      this.splitContainerDocumentDetails.TabIndex = 1;
      // 
      // tableLayoutPanelDocumentDetailsDetails
      // 
      this.tableLayoutPanelDocumentDetailsDetails.BackColor = System.Drawing.SystemColors.ControlDark;
      this.tableLayoutPanelDocumentDetailsDetails.ColumnCount = 1;
      this.tableLayoutPanelDocumentDetailsDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanelDocumentDetailsDetails.Controls.Add(this.pictureBoxDocumentDetailsImage, 0, 0);
      this.tableLayoutPanelDocumentDetailsDetails.Controls.Add(this.listViewDocInfo, 0, 1);
      this.tableLayoutPanelDocumentDetailsDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanelDocumentDetailsDetails.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanelDocumentDetailsDetails.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanelDocumentDetailsDetails.Name = "tableLayoutPanelDocumentDetailsDetails";
      this.tableLayoutPanelDocumentDetailsDetails.RowCount = 2;
      this.tableLayoutPanelDocumentDetailsDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanelDocumentDetailsDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanelDocumentDetailsDetails.Size = new System.Drawing.Size(219, 500);
      this.tableLayoutPanelDocumentDetailsDetails.TabIndex = 0;
      // 
      // pictureBoxDocumentDetailsImage
      // 
      this.pictureBoxDocumentDetailsImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBoxDocumentDetailsImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.pictureBoxDocumentDetailsImage.Location = new System.Drawing.Point(0, 0);
      this.pictureBoxDocumentDetailsImage.Margin = new System.Windows.Forms.Padding(0);
      this.pictureBoxDocumentDetailsImage.Name = "pictureBoxDocumentDetailsImage";
      this.pictureBoxDocumentDetailsImage.Size = new System.Drawing.Size(219, 250);
      this.pictureBoxDocumentDetailsImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBoxDocumentDetailsImage.TabIndex = 0;
      this.pictureBoxDocumentDetailsImage.TabStop = false;
      // 
      // listViewDocInfo
      // 
      this.listViewDocInfo.CausesValidation = false;
      this.listViewDocInfo.FullRowSelect = true;
      this.listViewDocInfo.GridLines = true;
      this.listViewDocInfo.Location = new System.Drawing.Point(0, 250);
      this.listViewDocInfo.Margin = new System.Windows.Forms.Padding(0);
      this.listViewDocInfo.Name = "listViewDocInfo";
      this.listViewDocInfo.Size = new System.Drawing.Size(200, 200);
      this.listViewDocInfo.TabIndex = 1;
      this.listViewDocInfo.UseCompatibleStateImageBehavior = false;
      this.listViewDocInfo.View = System.Windows.Forms.View.Details;
      // 
      // MacroscopeDocumentDetails
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainerDocumentDetails);
      this.Name = "MacroscopeDocumentDetails";
      this.Size = new System.Drawing.Size(800, 500);
      this.Load += new System.EventHandler(this.MacroscopeDocumentDetailsLoad);
      this.tabControlDocument.ResumeLayout(false);
      this.tabPageDocumentInfo.ResumeLayout(false);
      this.contextMenuStripCopyRecords.ResumeLayout(false);
      this.tabPageHttpHeaders.ResumeLayout(false);
      this.tableLayoutPanelHttpHeaders.ResumeLayout(false);
      this.tableLayoutPanelHttpHeaders.PerformLayout();
      this.tabPageMetaTags.ResumeLayout(false);
      this.tabPageHrefLangAnalysis.ResumeLayout(false);
      this.tabPageLinksIn.ResumeLayout(false);
      this.tabPageLinksOut.ResumeLayout(false);
      this.contextMenuStripDocumentLists.ResumeLayout(false);
      this.tabPageHyperlinksIn.ResumeLayout(false);
      this.tabPageHyperlinksOut.ResumeLayout(false);
      this.tabPageInsecureLinks.ResumeLayout(false);
      this.tabPageStylesheets.ResumeLayout(false);
      this.tabPageJavaScripts.ResumeLayout(false);
      this.tabPageImages.ResumeLayout(false);
      this.tabPageAudios.ResumeLayout(false);
      this.tabPageVideos.ResumeLayout(false);
      this.tabPageKeywordAnalysis.ResumeLayout(false);
      this.tabPageRemarks.ResumeLayout(false);
      this.tabPageDocumentTextRaw.ResumeLayout(false);
      this.tabPageDocumentTextRaw.PerformLayout();
      this.tabPageDocumentTextCleaned.ResumeLayout(false);
      this.tabPageDocumentTextCleaned.PerformLayout();
      this.tabPageBodyTextRaw.ResumeLayout(false);
      this.tabPageBodyTextRaw.PerformLayout();
      this.tabPageCustomFilters.ResumeLayout(false);
      this.splitContainerDocumentDetails.Panel1.ResumeLayout(false);
      this.splitContainerDocumentDetails.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerDocumentDetails)).EndInit();
      this.splitContainerDocumentDetails.ResumeLayout(false);
      this.tableLayoutPanelDocumentDetailsDetails.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDocumentDetailsImage)).EndInit();
      this.ResumeLayout(false);

		}
	}
}
