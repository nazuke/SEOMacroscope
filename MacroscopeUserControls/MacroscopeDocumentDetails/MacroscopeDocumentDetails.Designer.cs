/*
 * Created by SharpDevelop.
 * User: jholland
 * Date: 1/6/2017
 * Time: 09:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SEOMacroscope
{
	partial class MacroscopeDocumentDetails
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.ListView listViewDocumentInfo;
		public System.Windows.Forms.ColumnHeader DocDetailsDetail;
		private System.Windows.Forms.ColumnHeader DocDetailsValue;
		private System.Windows.Forms.ListView listViewHrefLang;
		public System.Windows.Forms.ColumnHeader LinksInClass;
		private System.Windows.Forms.ColumnHeader LinksInOrigin;
		private System.Windows.Forms.ListView listViewLinksIn;
		private System.Windows.Forms.ListView listViewImages;
		public System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ListView listViewStylesheets;
		public System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView listViewJavascripts;
		public System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		public System.Windows.Forms.ColumnHeader LinksInUrl;
		private System.Windows.Forms.ColumnHeader LinksInLinkText;
		private System.Windows.Forms.ColumnHeader LinksInImageAltText;
		private System.Windows.Forms.ColumnHeader LinksInFollow;
		private System.Windows.Forms.ColumnHeader HrefLangUrl;
		private System.Windows.Forms.ListView listViewLinksOut;
		public System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		public System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.tabPage7 = new System.Windows.Forms.TabPage();
			this.listViewDocumentInfo = new System.Windows.Forms.ListView();
			this.DocDetailsDetail = new System.Windows.Forms.ColumnHeader();
			this.DocDetailsValue = new System.Windows.Forms.ColumnHeader();
			this.listViewHrefLang = new System.Windows.Forms.ListView();
			this.listViewLinksIn = new System.Windows.Forms.ListView();
			this.LinksInClass = new System.Windows.Forms.ColumnHeader();
			this.LinksInOrigin = new System.Windows.Forms.ColumnHeader();
			this.listViewImages = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.listViewStylesheets = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.listViewJavascripts = new System.Windows.Forms.ListView();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.LinksInUrl = new System.Windows.Forms.ColumnHeader();
			this.LinksInLinkText = new System.Windows.Forms.ColumnHeader();
			this.LinksInImageAltText = new System.Windows.Forms.ColumnHeader();
			this.LinksInFollow = new System.Windows.Forms.ColumnHeader();
			this.HrefLangUrl = new System.Windows.Forms.ColumnHeader();
			this.listViewLinksOut = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.tabPage7.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Controls.Add(this.tabPage7);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(800, 500);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.listViewDocumentInfo);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(792, 474);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Document Info";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.listViewHrefLang);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(792, 474);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "HrefLang Analysis";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.listViewLinksIn);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(792, 474);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Links In";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.listViewLinksOut);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(792, 474);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Links Out";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.listViewImages);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(792, 474);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Images";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.listViewStylesheets);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(792, 474);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "Stylesheets";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// tabPage7
			// 
			this.tabPage7.Controls.Add(this.listViewJavascripts);
			this.tabPage7.Location = new System.Drawing.Point(4, 22);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage7.Size = new System.Drawing.Size(792, 474);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.Text = "JavaScripts";
			this.tabPage7.UseVisualStyleBackColor = true;
			// 
			// listViewDocumentInfo
			// 
			this.listViewDocumentInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.DocDetailsDetail,
			this.DocDetailsValue});
			this.listViewDocumentInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewDocumentInfo.FullRowSelect = true;
			this.listViewDocumentInfo.GridLines = true;
			this.listViewDocumentInfo.Location = new System.Drawing.Point(3, 3);
			this.listViewDocumentInfo.Name = "listViewDocumentInfo";
			this.listViewDocumentInfo.Size = new System.Drawing.Size(786, 468);
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
			// listViewHrefLang
			// 
			this.listViewHrefLang.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.HrefLangUrl});
			this.listViewHrefLang.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewHrefLang.FullRowSelect = true;
			this.listViewHrefLang.GridLines = true;
			this.listViewHrefLang.Location = new System.Drawing.Point(3, 3);
			this.listViewHrefLang.Name = "listViewHrefLang";
			this.listViewHrefLang.Size = new System.Drawing.Size(786, 468);
			this.listViewHrefLang.TabIndex = 1;
			this.listViewHrefLang.UseCompatibleStateImageBehavior = false;
			this.listViewHrefLang.View = System.Windows.Forms.View.Details;
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
			this.listViewLinksIn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewLinksIn.FullRowSelect = true;
			this.listViewLinksIn.GridLines = true;
			this.listViewLinksIn.Location = new System.Drawing.Point(3, 3);
			this.listViewLinksIn.Name = "listViewLinksIn";
			this.listViewLinksIn.Size = new System.Drawing.Size(786, 468);
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
			this.LinksInOrigin.Text = "Origin";
			this.LinksInOrigin.Width = 300;
			// 
			// listViewImages
			// 
			this.listViewImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader3,
			this.columnHeader4});
			this.listViewImages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewImages.FullRowSelect = true;
			this.listViewImages.GridLines = true;
			this.listViewImages.Location = new System.Drawing.Point(3, 3);
			this.listViewImages.Name = "listViewImages";
			this.listViewImages.Size = new System.Drawing.Size(786, 468);
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
			// listViewStylesheets
			// 
			this.listViewStylesheets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader5,
			this.columnHeader6});
			this.listViewStylesheets.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewStylesheets.FullRowSelect = true;
			this.listViewStylesheets.GridLines = true;
			this.listViewStylesheets.Location = new System.Drawing.Point(3, 3);
			this.listViewStylesheets.Name = "listViewStylesheets";
			this.listViewStylesheets.Size = new System.Drawing.Size(786, 468);
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
			// listViewJavascripts
			// 
			this.listViewJavascripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader7,
			this.columnHeader8});
			this.listViewJavascripts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewJavascripts.FullRowSelect = true;
			this.listViewJavascripts.GridLines = true;
			this.listViewJavascripts.Location = new System.Drawing.Point(3, 3);
			this.listViewJavascripts.Name = "listViewJavascripts";
			this.listViewJavascripts.Size = new System.Drawing.Size(786, 468);
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
			// LinksInUrl
			// 
			this.LinksInUrl.Text = "URL";
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
			// HrefLangUrl
			// 
			this.HrefLangUrl.Text = "URL";
			this.HrefLangUrl.Width = 300;
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
			this.listViewLinksOut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewLinksOut.FullRowSelect = true;
			this.listViewLinksOut.GridLines = true;
			this.listViewLinksOut.Location = new System.Drawing.Point(3, 3);
			this.listViewLinksOut.Name = "listViewLinksOut";
			this.listViewLinksOut.Size = new System.Drawing.Size(786, 468);
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
			this.columnHeader2.Text = "Origin";
			this.columnHeader2.Width = 300;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "URL";
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
			// MacroscopeDocumentDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "MacroscopeDocumentDetails";
			this.Size = new System.Drawing.Size(800, 500);
			this.Load += new System.EventHandler(this.MacroscopeDocumentDetailsLoad);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.tabPage6.ResumeLayout(false);
			this.tabPage7.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
