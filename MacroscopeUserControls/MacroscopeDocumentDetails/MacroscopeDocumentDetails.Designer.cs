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
		public System.Windows.Forms.TabPage tabPageLinksIn;
		public System.Windows.Forms.TabPage tabPageLinksOut;
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
		public System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		public System.Windows.Forms.ListView listViewStylesheets;
		public System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		public System.Windows.Forms.ListView listViewJavascripts;
		public System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
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
		private System.Windows.Forms.SplitContainer splitContainerDocumentDetails;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDocumentDetailsDetails;
		public System.Windows.Forms.PictureBox pictureBoxDocumentDetailsImage;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyValues;
		private System.Windows.Forms.TabPage tabPageHttpHeaders;
		public System.Windows.Forms.TextBox textBoxHttpHeaders;
		
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
			this.tabPageHrefLangAnalysis = new System.Windows.Forms.TabPage();
			this.listViewHrefLang = new System.Windows.Forms.ListView();
			this.HrefLangUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageLinksIn = new System.Windows.Forms.TabPage();
			this.listViewLinksIn = new System.Windows.Forms.ListView();
			this.LinksInClass = new System.Windows.Forms.ColumnHeader();
			this.LinksInOrigin = new System.Windows.Forms.ColumnHeader();
			this.LinksInUrl = new System.Windows.Forms.ColumnHeader();
			this.LinksInLinkText = new System.Windows.Forms.ColumnHeader();
			this.LinksInImageAltText = new System.Windows.Forms.ColumnHeader();
			this.LinksInFollow = new System.Windows.Forms.ColumnHeader();
			this.tabPageLinksOut = new System.Windows.Forms.TabPage();
			this.listViewLinksOut = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.tabPageImages = new System.Windows.Forms.TabPage();
			this.listViewImages = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.tabPageStylesheets = new System.Windows.Forms.TabPage();
			this.listViewStylesheets = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.tabPageJavaScripts = new System.Windows.Forms.TabPage();
			this.listViewJavascripts = new System.Windows.Forms.ListView();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.splitContainerDocumentDetails = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanelDocumentDetailsDetails = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBoxDocumentDetailsImage = new System.Windows.Forms.PictureBox();
			this.tabPageHttpHeaders = new System.Windows.Forms.TabPage();
			this.textBoxHttpHeaders = new System.Windows.Forms.TextBox();
			this.tabControlDocument.SuspendLayout();
			this.tabPageDocumentInfo.SuspendLayout();
			this.contextMenuStripTextDocumentDetails.SuspendLayout();
			this.tabPageHrefLangAnalysis.SuspendLayout();
			this.tabPageLinksIn.SuspendLayout();
			this.tabPageLinksOut.SuspendLayout();
			this.tabPageImages.SuspendLayout();
			this.tabPageStylesheets.SuspendLayout();
			this.tabPageJavaScripts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerDocumentDetails)).BeginInit();
			this.splitContainerDocumentDetails.Panel1.SuspendLayout();
			this.splitContainerDocumentDetails.Panel2.SuspendLayout();
			this.splitContainerDocumentDetails.SuspendLayout();
			this.tableLayoutPanelDocumentDetailsDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDocumentDetailsImage)).BeginInit();
			this.tabPageHttpHeaders.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlDocument
			// 
			this.tabControlDocument.Controls.Add(this.tabPageDocumentInfo);
			this.tabControlDocument.Controls.Add(this.tabPageHttpHeaders);
			this.tabControlDocument.Controls.Add(this.tabPageHrefLangAnalysis);
			this.tabControlDocument.Controls.Add(this.tabPageLinksIn);
			this.tabControlDocument.Controls.Add(this.tabPageLinksOut);
			this.tabControlDocument.Controls.Add(this.tabPageImages);
			this.tabControlDocument.Controls.Add(this.tabPageStylesheets);
			this.tabControlDocument.Controls.Add(this.tabPageJavaScripts);
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
			// tabPageLinksIn
			// 
			this.tabPageLinksIn.Controls.Add(this.listViewLinksIn);
			this.tabPageLinksIn.Location = new System.Drawing.Point(4, 22);
			this.tabPageLinksIn.Name = "tabPageLinksIn";
			this.tabPageLinksIn.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLinksIn.Size = new System.Drawing.Size(567, 474);
			this.tabPageLinksIn.TabIndex = 2;
			this.tabPageLinksIn.Text = "Links In";
			this.tabPageLinksIn.UseVisualStyleBackColor = true;
			// 
			// listViewLinksIn
			// 
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
			// tabPageLinksOut
			// 
			this.tabPageLinksOut.Controls.Add(this.listViewLinksOut);
			this.tabPageLinksOut.Location = new System.Drawing.Point(4, 22);
			this.tabPageLinksOut.Name = "tabPageLinksOut";
			this.tabPageLinksOut.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLinksOut.Size = new System.Drawing.Size(567, 474);
			this.tabPageLinksOut.TabIndex = 3;
			this.tabPageLinksOut.Text = "Links Out";
			this.tabPageLinksOut.UseVisualStyleBackColor = true;
			// 
			// listViewLinksOut
			// 
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
			this.listViewImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader3,
			this.columnHeader4});
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
			// columnHeader3
			// 
			this.columnHeader3.Text = "Detail";
			this.columnHeader3.Width = 200;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Value";
			this.columnHeader4.Width = 400;
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
			this.listViewStylesheets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader5,
			this.columnHeader6});
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
			// columnHeader5
			// 
			this.columnHeader5.Text = "Detail";
			this.columnHeader5.Width = 200;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Value";
			this.columnHeader6.Width = 400;
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
			this.listViewJavascripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader7,
			this.columnHeader8});
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
			// columnHeader7
			// 
			this.columnHeader7.Text = "Detail";
			this.columnHeader7.Width = 200;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Value";
			this.columnHeader8.Width = 400;
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
			// tabPageHttpHeaders
			// 
			this.tabPageHttpHeaders.Controls.Add(this.textBoxHttpHeaders);
			this.tabPageHttpHeaders.Location = new System.Drawing.Point(4, 22);
			this.tabPageHttpHeaders.Name = "tabPageHttpHeaders";
			this.tabPageHttpHeaders.Size = new System.Drawing.Size(567, 474);
			this.tabPageHttpHeaders.TabIndex = 7;
			this.tabPageHttpHeaders.Text = "HTTP Headers";
			this.tabPageHttpHeaders.UseVisualStyleBackColor = true;
			// 
			// textBoxHttpHeaders
			// 
			this.textBoxHttpHeaders.Location = new System.Drawing.Point(20, 20);
			this.textBoxHttpHeaders.Multiline = true;
			this.textBoxHttpHeaders.Name = "textBoxHttpHeaders";
			this.textBoxHttpHeaders.Size = new System.Drawing.Size(200, 200);
			this.textBoxHttpHeaders.TabIndex = 0;
			this.textBoxHttpHeaders.WordWrap = false;
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
			this.tabPageHrefLangAnalysis.ResumeLayout(false);
			this.tabPageLinksIn.ResumeLayout(false);
			this.tabPageLinksOut.ResumeLayout(false);
			this.tabPageImages.ResumeLayout(false);
			this.tabPageStylesheets.ResumeLayout(false);
			this.tabPageJavaScripts.ResumeLayout(false);
			this.splitContainerDocumentDetails.Panel1.ResumeLayout(false);
			this.splitContainerDocumentDetails.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerDocumentDetails)).EndInit();
			this.splitContainerDocumentDetails.ResumeLayout(false);
			this.tableLayoutPanelDocumentDetailsDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDocumentDetailsImage)).EndInit();
			this.tabPageHttpHeaders.ResumeLayout(false);
			this.tabPageHttpHeaders.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
