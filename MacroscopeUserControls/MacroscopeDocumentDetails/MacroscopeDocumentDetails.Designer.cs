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
	partial class MacroscopeDocumentDetails
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
		public System.Windows.Forms.ColumnHeader LinksInClass;
		private System.Windows.Forms.ColumnHeader LinksInOrigin;
		public System.Windows.Forms.ListView listViewLinksIn;
		public System.Windows.Forms.ListView listViewImages;
		public System.Windows.Forms.ColumnHeader columnHeaderImagesUrl;
		public System.Windows.Forms.ListView listViewStylesheets;
		public System.Windows.Forms.ColumnHeader columnHeaderStylesheetsUrl;
		public System.Windows.Forms.ListView listViewJavascripts;
		public System.Windows.Forms.ColumnHeader columnHeaderJavascriptsUrl;
		public System.Windows.Forms.ColumnHeader LinksInUrl;
		private System.Windows.Forms.ColumnHeader LinksInLinkText;
		private System.Windows.Forms.ColumnHeader LinksInImageAltText;
		private System.Windows.Forms.ColumnHeader LinksInFollow;
		private System.Windows.Forms.ColumnHeader HrefLangUrl;
		public System.Windows.Forms.ListView listViewLinksOut;
		public System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		public System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripTextDocumentDetails;
		public System.Windows.Forms.ToolStripMenuItem copyTextToolStripMenuItem;
		public System.Windows.Forms.SplitContainer splitContainerDocumentDetails;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDocumentDetailsDetails;
		public System.Windows.Forms.PictureBox pictureBoxDocumentDetailsImage;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyValues;
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
			this.DocDetailsDetail = new System.Windows.Forms.ColumnHeader();
			this.DocDetailsValue = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStripTextDocumentDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCopyValues = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPageHttpHeaders = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelHttpHeaders = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxHttpRequestHeaders = new System.Windows.Forms.TextBox();
			this.textBoxHttpResponseHeaders = new System.Windows.Forms.TextBox();
			this.tabPageHrefLangAnalysis = new System.Windows.Forms.TabPage();
			this.listViewHrefLang = new System.Windows.Forms.ListView();
			this.HrefLangUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageHyperlinksIn = new System.Windows.Forms.TabPage();
			this.listViewLinksIn = new System.Windows.Forms.ListView();
			this.LinksInClass = new System.Windows.Forms.ColumnHeader();
			this.LinksInOrigin = new System.Windows.Forms.ColumnHeader();
			this.LinksInUrl = new System.Windows.Forms.ColumnHeader();
			this.LinksInLinkText = new System.Windows.Forms.ColumnHeader();
			this.LinksInImageAltText = new System.Windows.Forms.ColumnHeader();
			this.LinksInFollow = new System.Windows.Forms.ColumnHeader();
			this.tabPageHyperlinksOut = new System.Windows.Forms.TabPage();
			this.listViewLinksOut = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.tabPageInsecureLinks = new System.Windows.Forms.TabPage();
			this.listViewInsecureLinks = new System.Windows.Forms.ListView();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.tabPageStylesheets = new System.Windows.Forms.TabPage();
			this.listViewStylesheets = new System.Windows.Forms.ListView();
			this.columnHeaderStylesheetsItem = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderStylesheetsUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageJavaScripts = new System.Windows.Forms.TabPage();
			this.listViewJavascripts = new System.Windows.Forms.ListView();
			this.columnHeaderJavascriptsItem = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderJavascriptsUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageImages = new System.Windows.Forms.TabPage();
			this.listViewImages = new System.Windows.Forms.ListView();
			this.columnHeaderImagesItem = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderImagesUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderImagesTitle = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderImagesAltText = new System.Windows.Forms.ColumnHeader();
			this.tabPageAudios = new System.Windows.Forms.TabPage();
			this.listViewAudios = new System.Windows.Forms.ListView();
			this.columnHeaderAudiosItem = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderAudiosUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageVideos = new System.Windows.Forms.TabPage();
			this.listViewVideos = new System.Windows.Forms.ListView();
			this.columnHeaderVideosItem = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderVideosUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageKeywordAnalysis = new System.Windows.Forms.TabPage();
			this.listViewKeywordAnalysis = new System.Windows.Forms.ListView();
			this.columnHeaderKeywordAnalysisOccurences = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderKeywordAnalysisTerm = new System.Windows.Forms.ColumnHeader();
			this.splitContainerDocumentDetails = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanelDocumentDetailsDetails = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBoxDocumentDetailsImage = new System.Windows.Forms.PictureBox();
			this.listViewDocInfo = new System.Windows.Forms.ListView();
			this.tabControlDocument.SuspendLayout();
			this.tabPageDocumentInfo.SuspendLayout();
			this.contextMenuStripTextDocumentDetails.SuspendLayout();
			this.tabPageHttpHeaders.SuspendLayout();
			this.tableLayoutPanelHttpHeaders.SuspendLayout();
			this.tabPageHrefLangAnalysis.SuspendLayout();
			this.tabPageHyperlinksIn.SuspendLayout();
			this.tabPageHyperlinksOut.SuspendLayout();
			this.tabPageInsecureLinks.SuspendLayout();
			this.tabPageStylesheets.SuspendLayout();
			this.tabPageJavaScripts.SuspendLayout();
			this.tabPageImages.SuspendLayout();
			this.tabPageAudios.SuspendLayout();
			this.tabPageVideos.SuspendLayout();
			this.tabPageKeywordAnalysis.SuspendLayout();
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
			this.tabControlDocument.Controls.Add(this.tabPageHrefLangAnalysis);
			this.tabControlDocument.Controls.Add(this.tabPageHyperlinksIn);
			this.tabControlDocument.Controls.Add(this.tabPageHyperlinksOut);
			this.tabControlDocument.Controls.Add(this.tabPageInsecureLinks);
			this.tabControlDocument.Controls.Add(this.tabPageStylesheets);
			this.tabControlDocument.Controls.Add(this.tabPageJavaScripts);
			this.tabControlDocument.Controls.Add(this.tabPageImages);
			this.tabControlDocument.Controls.Add(this.tabPageAudios);
			this.tabControlDocument.Controls.Add(this.tabPageVideos);
			this.tabControlDocument.Controls.Add(this.tabPageKeywordAnalysis);
			this.tabControlDocument.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlDocument.Location = new System.Drawing.Point(0, 0);
			this.tabControlDocument.Margin = new System.Windows.Forms.Padding(0);
			this.tabControlDocument.Name = "tabControlDocument";
			this.tabControlDocument.SelectedIndex = 0;
			this.tabControlDocument.Size = new System.Drawing.Size(575, 500);
			this.tabControlDocument.TabIndex = 0;
			// 
			// tabPageDocumentInfo
			// 
			this.tabPageDocumentInfo.Controls.Add(this.listViewDocumentInfo);
			this.tabPageDocumentInfo.Location = new System.Drawing.Point(4, 22);
			this.tabPageDocumentInfo.Name = "tabPageDocumentInfo";
			this.tabPageDocumentInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDocumentInfo.Size = new System.Drawing.Size(567, 474);
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
			this.listViewDocumentInfo.ContextMenuStrip = this.contextMenuStripTextDocumentDetails;
			this.listViewDocumentInfo.FullRowSelect = true;
			this.listViewDocumentInfo.GridLines = true;
			this.listViewDocumentInfo.Location = new System.Drawing.Point(20, 20);
			this.listViewDocumentInfo.Margin = new System.Windows.Forms.Padding(0);
			this.listViewDocumentInfo.Name = "listViewDocumentInfo";
			this.listViewDocumentInfo.Size = new System.Drawing.Size(200, 200);
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
			// contextMenuStripTextDocumentDetails
			// 
			this.contextMenuStripTextDocumentDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.copyTextToolStripMenuItem,
			this.toolStripMenuItemCopyValues});
			this.contextMenuStripTextDocumentDetails.Name = "contextMenuStripTextCopy";
			this.contextMenuStripTextDocumentDetails.Size = new System.Drawing.Size(140, 48);
			// 
			// copyTextToolStripMenuItem
			// 
			this.copyTextToolStripMenuItem.Name = "copyTextToolStripMenuItem";
			this.copyTextToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.copyTextToolStripMenuItem.Text = "Copy Rows";
			this.copyTextToolStripMenuItem.Click += new System.EventHandler(this.CallbackDocumentDetailsContextMenuStripCopyRowsClick);
			// 
			// toolStripMenuItemCopyValues
			// 
			this.toolStripMenuItemCopyValues.Name = "toolStripMenuItemCopyValues";
			this.toolStripMenuItemCopyValues.Size = new System.Drawing.Size(139, 22);
			this.toolStripMenuItemCopyValues.Text = "Copy Values";
			this.toolStripMenuItemCopyValues.Click += new System.EventHandler(this.CallbackDocumentDetailsContextMenuStripCopyValuesClick);
			// 
			// tabPageHttpHeaders
			// 
			this.tabPageHttpHeaders.Controls.Add(this.tableLayoutPanelHttpHeaders);
			this.tabPageHttpHeaders.Location = new System.Drawing.Point(4, 22);
			this.tabPageHttpHeaders.Name = "tabPageHttpHeaders";
			this.tabPageHttpHeaders.Size = new System.Drawing.Size(567, 474);
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
			this.tableLayoutPanelHttpHeaders.Margin = new System.Windows.Forms.Padding(0);
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
			// tabPageHrefLangAnalysis
			// 
			this.tabPageHrefLangAnalysis.Controls.Add(this.listViewHrefLang);
			this.tabPageHrefLangAnalysis.Location = new System.Drawing.Point(4, 22);
			this.tabPageHrefLangAnalysis.Name = "tabPageHrefLangAnalysis";
			this.tabPageHrefLangAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHrefLangAnalysis.Size = new System.Drawing.Size(567, 474);
			this.tabPageHrefLangAnalysis.TabIndex = 1;
			this.tabPageHrefLangAnalysis.Text = "HrefLang Analysis";
			this.tabPageHrefLangAnalysis.UseVisualStyleBackColor = true;
			// 
			// listViewHrefLang
			// 
			this.listViewHrefLang.CausesValidation = false;
			this.listViewHrefLang.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.HrefLangUrl});
			this.listViewHrefLang.FullRowSelect = true;
			this.listViewHrefLang.GridLines = true;
			this.listViewHrefLang.Location = new System.Drawing.Point(20, 20);
			this.listViewHrefLang.Margin = new System.Windows.Forms.Padding(0);
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
			// tabPageHyperlinksIn
			// 
			this.tabPageHyperlinksIn.Controls.Add(this.listViewLinksIn);
			this.tabPageHyperlinksIn.Location = new System.Drawing.Point(4, 22);
			this.tabPageHyperlinksIn.Name = "tabPageHyperlinksIn";
			this.tabPageHyperlinksIn.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHyperlinksIn.Size = new System.Drawing.Size(567, 474);
			this.tabPageHyperlinksIn.TabIndex = 2;
			this.tabPageHyperlinksIn.Text = "Hyperlinks In";
			this.tabPageHyperlinksIn.UseVisualStyleBackColor = true;
			// 
			// listViewLinksIn
			// 
			this.listViewLinksIn.CausesValidation = false;
			this.listViewLinksIn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.LinksInClass,
			this.LinksInOrigin,
			this.LinksInUrl,
			this.LinksInLinkText,
			this.LinksInImageAltText,
			this.LinksInFollow});
			this.listViewLinksIn.FullRowSelect = true;
			this.listViewLinksIn.GridLines = true;
			this.listViewLinksIn.Location = new System.Drawing.Point(20, 20);
			this.listViewLinksIn.Margin = new System.Windows.Forms.Padding(0);
			this.listViewLinksIn.Name = "listViewLinksIn";
			this.listViewLinksIn.Size = new System.Drawing.Size(200, 200);
			this.listViewLinksIn.TabIndex = 1;
			this.listViewLinksIn.UseCompatibleStateImageBehavior = false;
			this.listViewLinksIn.View = System.Windows.Forms.View.Details;
			// 
			// LinksInClass
			// 
			this.LinksInClass.Text = "Class";
			this.LinksInClass.Width = 100;
			// 
			// LinksInOrigin
			// 
			this.LinksInOrigin.Text = "Origin URL";
			this.LinksInOrigin.Width = 300;
			// 
			// LinksInUrl
			// 
			this.LinksInUrl.Text = "Destination URL";
			this.LinksInUrl.Width = 300;
			// 
			// LinksInLinkText
			// 
			this.LinksInLinkText.Text = "Link Text";
			// 
			// LinksInImageAltText
			// 
			this.LinksInImageAltText.Text = "Image Alt Text";
			// 
			// LinksInFollow
			// 
			this.LinksInFollow.Text = "Follow";
			// 
			// tabPageHyperlinksOut
			// 
			this.tabPageHyperlinksOut.Controls.Add(this.listViewLinksOut);
			this.tabPageHyperlinksOut.Location = new System.Drawing.Point(4, 22);
			this.tabPageHyperlinksOut.Name = "tabPageHyperlinksOut";
			this.tabPageHyperlinksOut.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHyperlinksOut.Size = new System.Drawing.Size(567, 474);
			this.tabPageHyperlinksOut.TabIndex = 3;
			this.tabPageHyperlinksOut.Text = "Hyperlinks Out";
			this.tabPageHyperlinksOut.UseVisualStyleBackColor = true;
			// 
			// listViewLinksOut
			// 
			this.listViewLinksOut.CausesValidation = false;
			this.listViewLinksOut.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2,
			this.columnHeader9,
			this.columnHeader10,
			this.columnHeader11,
			this.columnHeader12});
			this.listViewLinksOut.FullRowSelect = true;
			this.listViewLinksOut.GridLines = true;
			this.listViewLinksOut.Location = new System.Drawing.Point(20, 20);
			this.listViewLinksOut.Margin = new System.Windows.Forms.Padding(0);
			this.listViewLinksOut.Name = "listViewLinksOut";
			this.listViewLinksOut.Size = new System.Drawing.Size(200, 200);
			this.listViewLinksOut.TabIndex = 2;
			this.listViewLinksOut.UseCompatibleStateImageBehavior = false;
			this.listViewLinksOut.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Class";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Origin URL";
			this.columnHeader2.Width = 300;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Destination URL";
			this.columnHeader9.Width = 300;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Link Text";
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Image Alt Text";
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Follow";
			// 
			// tabPageInsecureLinks
			// 
			this.tabPageInsecureLinks.Controls.Add(this.listViewInsecureLinks);
			this.tabPageInsecureLinks.Location = new System.Drawing.Point(4, 22);
			this.tabPageInsecureLinks.Name = "tabPageInsecureLinks";
			this.tabPageInsecureLinks.Size = new System.Drawing.Size(567, 474);
			this.tabPageInsecureLinks.TabIndex = 10;
			this.tabPageInsecureLinks.Text = "Insecure Links";
			this.tabPageInsecureLinks.UseVisualStyleBackColor = true;
			// 
			// listViewInsecureLinks
			// 
			this.listViewInsecureLinks.CausesValidation = false;
			this.listViewInsecureLinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader13});
			this.listViewInsecureLinks.FullRowSelect = true;
			this.listViewInsecureLinks.GridLines = true;
			this.listViewInsecureLinks.Location = new System.Drawing.Point(20, 20);
			this.listViewInsecureLinks.Margin = new System.Windows.Forms.Padding(0);
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
			this.tabPageStylesheets.Location = new System.Drawing.Point(4, 22);
			this.tabPageStylesheets.Name = "tabPageStylesheets";
			this.tabPageStylesheets.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageStylesheets.Size = new System.Drawing.Size(567, 474);
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
			this.listViewStylesheets.FullRowSelect = true;
			this.listViewStylesheets.GridLines = true;
			this.listViewStylesheets.Location = new System.Drawing.Point(20, 20);
			this.listViewStylesheets.Margin = new System.Windows.Forms.Padding(0);
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
			this.tabPageJavaScripts.Location = new System.Drawing.Point(4, 22);
			this.tabPageJavaScripts.Name = "tabPageJavaScripts";
			this.tabPageJavaScripts.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageJavaScripts.Size = new System.Drawing.Size(567, 474);
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
			this.listViewJavascripts.FullRowSelect = true;
			this.listViewJavascripts.GridLines = true;
			this.listViewJavascripts.Location = new System.Drawing.Point(20, 20);
			this.listViewJavascripts.Margin = new System.Windows.Forms.Padding(0);
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
			this.tabPageImages.Location = new System.Drawing.Point(4, 22);
			this.tabPageImages.Name = "tabPageImages";
			this.tabPageImages.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageImages.Size = new System.Drawing.Size(567, 474);
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
			this.listViewImages.FullRowSelect = true;
			this.listViewImages.GridLines = true;
			this.listViewImages.Location = new System.Drawing.Point(20, 20);
			this.listViewImages.Margin = new System.Windows.Forms.Padding(0);
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
			this.tabPageAudios.Location = new System.Drawing.Point(4, 22);
			this.tabPageAudios.Name = "tabPageAudios";
			this.tabPageAudios.Size = new System.Drawing.Size(567, 474);
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
			this.listViewAudios.FullRowSelect = true;
			this.listViewAudios.GridLines = true;
			this.listViewAudios.Location = new System.Drawing.Point(20, 20);
			this.listViewAudios.Margin = new System.Windows.Forms.Padding(0);
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
			this.tabPageVideos.Location = new System.Drawing.Point(4, 22);
			this.tabPageVideos.Name = "tabPageVideos";
			this.tabPageVideos.Size = new System.Drawing.Size(567, 474);
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
			this.listViewVideos.FullRowSelect = true;
			this.listViewVideos.GridLines = true;
			this.listViewVideos.Location = new System.Drawing.Point(20, 20);
			this.listViewVideos.Margin = new System.Windows.Forms.Padding(0);
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
			this.tabPageKeywordAnalysis.Location = new System.Drawing.Point(4, 22);
			this.tabPageKeywordAnalysis.Name = "tabPageKeywordAnalysis";
			this.tabPageKeywordAnalysis.Size = new System.Drawing.Size(567, 474);
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
			this.listViewKeywordAnalysis.FullRowSelect = true;
			this.listViewKeywordAnalysis.GridLines = true;
			this.listViewKeywordAnalysis.Location = new System.Drawing.Point(20, 20);
			this.listViewKeywordAnalysis.Margin = new System.Windows.Forms.Padding(0);
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
			this.pictureBoxDocumentDetailsImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
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
			this.contextMenuStripTextDocumentDetails.ResumeLayout(false);
			this.tabPageHttpHeaders.ResumeLayout(false);
			this.tableLayoutPanelHttpHeaders.ResumeLayout(false);
			this.tableLayoutPanelHttpHeaders.PerformLayout();
			this.tabPageHrefLangAnalysis.ResumeLayout(false);
			this.tabPageHyperlinksIn.ResumeLayout(false);
			this.tabPageHyperlinksOut.ResumeLayout(false);
			this.tabPageInsecureLinks.ResumeLayout(false);
			this.tabPageStylesheets.ResumeLayout(false);
			this.tabPageJavaScripts.ResumeLayout(false);
			this.tabPageImages.ResumeLayout(false);
			this.tabPageAudios.ResumeLayout(false);
			this.tabPageVideos.ResumeLayout(false);
			this.tabPageKeywordAnalysis.ResumeLayout(false);
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
