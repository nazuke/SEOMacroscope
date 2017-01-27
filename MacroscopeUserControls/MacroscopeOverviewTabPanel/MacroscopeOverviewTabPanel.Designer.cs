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
	partial class MacroscopeOverviewTabPanel
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
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPageStructureOverview = new System.Windows.Forms.TabPage();
			this.listViewStructure = new System.Windows.Forms.ListView();
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
			this.RedirectsAuditOriginUrl = new System.Windows.Forms.ColumnHeader();
			this.RedirectsAuditStatusCode = new System.Windows.Forms.ColumnHeader();
			this.RedirectsAuditDestinationUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageErrors = new System.Windows.Forms.TabPage();
			this.listViewErrors = new System.Windows.Forms.ListView();
			this.columnHeaderErrorsUrl = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderErrorsStatusCode = new System.Windows.Forms.ColumnHeader();
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
			this.tabControlMain.SuspendLayout();
			this.tabPageStructureOverview.SuspendLayout();
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
			this.tabPageEmailAddresses.SuspendLayout();
			this.tabPageTelephoneNumbers.SuspendLayout();
			this.tabPageHistory.SuspendLayout();
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
			this.tabControlMain.Controls.Add(this.tabPageEmailAddresses);
			this.tabControlMain.Controls.Add(this.tabPageTelephoneNumbers);
			this.tabControlMain.Controls.Add(this.tabPageHistory);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(0, 0);
			this.tabControlMain.Margin = new System.Windows.Forms.Padding(0);
			this.tabControlMain.Multiline = true;
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(600, 500);
			this.tabControlMain.TabIndex = 4;
			// 
			// tabPageStructureOverview
			// 
			this.tabPageStructureOverview.Controls.Add(this.listViewStructure);
			this.tabPageStructureOverview.Location = new System.Drawing.Point(4, 58);
			this.tabPageStructureOverview.Name = "tabPageStructureOverview";
			this.tabPageStructureOverview.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageStructureOverview.Size = new System.Drawing.Size(592, 438);
			this.tabPageStructureOverview.TabIndex = 0;
			this.tabPageStructureOverview.Text = "Structure Overview";
			this.tabPageStructureOverview.UseVisualStyleBackColor = true;
			// 
			// listViewStructure
			// 
			this.listViewStructure.BackColor = System.Drawing.SystemColors.Window;
			this.listViewStructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listViewStructure.CausesValidation = false;
			this.listViewStructure.FullRowSelect = true;
			this.listViewStructure.GridLines = true;
			this.listViewStructure.LabelWrap = false;
			this.listViewStructure.Location = new System.Drawing.Point(10, 10);
			this.listViewStructure.Margin = new System.Windows.Forms.Padding(0);
			this.listViewStructure.MultiSelect = false;
			this.listViewStructure.Name = "listViewStructure";
			this.listViewStructure.ShowGroups = false;
			this.listViewStructure.Size = new System.Drawing.Size(200, 200);
			this.listViewStructure.TabIndex = 0;
			this.listViewStructure.UseCompatibleStateImageBehavior = false;
			this.listViewStructure.View = System.Windows.Forms.View.Details;
			// 
			// tabPageHierarchy
			// 
			this.tabPageHierarchy.Controls.Add(this.treeViewHierarchy);
			this.tabPageHierarchy.Location = new System.Drawing.Point(4, 58);
			this.tabPageHierarchy.Name = "tabPageHierarchy";
			this.tabPageHierarchy.Size = new System.Drawing.Size(592, 438);
			this.tabPageHierarchy.TabIndex = 8;
			this.tabPageHierarchy.Text = "Hierarchy";
			this.tabPageHierarchy.UseVisualStyleBackColor = true;
			// 
			// treeViewHierarchy
			// 
			this.treeViewHierarchy.FullRowSelect = true;
			this.treeViewHierarchy.Location = new System.Drawing.Point(10, 10);
			this.treeViewHierarchy.Margin = new System.Windows.Forms.Padding(0);
			this.treeViewHierarchy.Name = "treeViewHierarchy";
			this.treeViewHierarchy.Size = new System.Drawing.Size(200, 200);
			this.treeViewHierarchy.TabIndex = 0;
			// 
			// tabPageCanonicalAnalysis
			// 
			this.tabPageCanonicalAnalysis.Controls.Add(this.listViewCanonicalAnalysis);
			this.tabPageCanonicalAnalysis.Location = new System.Drawing.Point(4, 58);
			this.tabPageCanonicalAnalysis.Name = "tabPageCanonicalAnalysis";
			this.tabPageCanonicalAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageCanonicalAnalysis.Size = new System.Drawing.Size(592, 438);
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
			this.tabPageHrefLangAnalysis.Size = new System.Drawing.Size(592, 438);
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
			this.tabPageRedirectsAudit.Size = new System.Drawing.Size(592, 438);
			this.tabPageRedirectsAudit.TabIndex = 2;
			this.tabPageRedirectsAudit.Text = "Redirects Audit";
			this.tabPageRedirectsAudit.UseVisualStyleBackColor = true;
			// 
			// listViewRedirectsAudit
			// 
			this.listViewRedirectsAudit.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.RedirectsAuditOriginUrl,
			this.RedirectsAuditStatusCode,
			this.RedirectsAuditDestinationUrl});
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
			// tabPageErrors
			// 
			this.tabPageErrors.Controls.Add(this.listViewErrors);
			this.tabPageErrors.Location = new System.Drawing.Point(4, 58);
			this.tabPageErrors.Name = "tabPageErrors";
			this.tabPageErrors.Size = new System.Drawing.Size(592, 438);
			this.tabPageErrors.TabIndex = 16;
			this.tabPageErrors.Text = "Errors";
			this.tabPageErrors.UseVisualStyleBackColor = true;
			// 
			// listViewErrors
			// 
			this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderErrorsUrl,
			this.columnHeaderErrorsStatusCode});
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
			this.columnHeaderErrorsStatusCode.Text = "StatusCode";
			this.columnHeaderErrorsStatusCode.Width = 150;
			// 
			// tabPageHostnames
			// 
			this.tabPageHostnames.Controls.Add(this.listViewHostnames);
			this.tabPageHostnames.Location = new System.Drawing.Point(4, 58);
			this.tabPageHostnames.Name = "tabPageHostnames";
			this.tabPageHostnames.Size = new System.Drawing.Size(592, 438);
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
			this.tabPageUriAnalysis.Size = new System.Drawing.Size(592, 438);
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
			this.tabPagePageTitles.Size = new System.Drawing.Size(592, 438);
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
			this.columnHeaderCount.Text = "Count";
			// 
			// columnHeaderPageTitle
			// 
			this.columnHeaderPageTitle.Text = "Page Title";
			this.columnHeaderPageTitle.Width = 150;
			// 
			// columnHeaderLength
			// 
			this.columnHeaderLength.Text = "Length";
			// 
			// columnHeaderPixelWidth
			// 
			this.columnHeaderPixelWidth.Text = "Pixel Width";
			// 
			// tabPagePageDescriptions
			// 
			this.tabPagePageDescriptions.Controls.Add(this.listViewPageDescriptions);
			this.tabPagePageDescriptions.Location = new System.Drawing.Point(4, 58);
			this.tabPagePageDescriptions.Name = "tabPagePageDescriptions";
			this.tabPagePageDescriptions.Size = new System.Drawing.Size(592, 438);
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
			this.columnHeaderDescriptionCount.Text = "Count";
			// 
			// columnHeaderDescriptionDescription
			// 
			this.columnHeaderDescriptionDescription.Text = "Description";
			this.columnHeaderDescriptionDescription.Width = 300;
			// 
			// columnHeaderDescriptionLength
			// 
			this.columnHeaderDescriptionLength.Text = "Length";
			// 
			// tabPagePageKeywords
			// 
			this.tabPagePageKeywords.Controls.Add(this.listViewPageKeywords);
			this.tabPagePageKeywords.Location = new System.Drawing.Point(4, 58);
			this.tabPagePageKeywords.Name = "tabPagePageKeywords";
			this.tabPagePageKeywords.Size = new System.Drawing.Size(592, 438);
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
			this.columnHeaderKeywordsCount.Text = "Count";
			// 
			// columnHeaderKeywordsKeywords
			// 
			this.columnHeaderKeywordsKeywords.Text = "Keywords";
			this.columnHeaderKeywordsKeywords.Width = 300;
			// 
			// columnHeaderKeywordsLength
			// 
			this.columnHeaderKeywordsLength.Text = "Length";
			// 
			// columnHeaderKeywordsNumber
			// 
			this.columnHeaderKeywordsNumber.Text = "No. of Keywords";
			// 
			// tabPagePageHeadings
			// 
			this.tabPagePageHeadings.Controls.Add(this.listViewPageHeadings);
			this.tabPagePageHeadings.Location = new System.Drawing.Point(4, 58);
			this.tabPagePageHeadings.Name = "tabPagePageHeadings";
			this.tabPagePageHeadings.Size = new System.Drawing.Size(592, 438);
			this.tabPagePageHeadings.TabIndex = 13;
			this.tabPagePageHeadings.Text = "Page Headings";
			this.tabPagePageHeadings.UseVisualStyleBackColor = true;
			// 
			// listViewPageHeadings
			// 
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
			// tabPageEmailAddresses
			// 
			this.tabPageEmailAddresses.Controls.Add(this.listViewEmailAddresses);
			this.tabPageEmailAddresses.Location = new System.Drawing.Point(4, 58);
			this.tabPageEmailAddresses.Name = "tabPageEmailAddresses";
			this.tabPageEmailAddresses.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEmailAddresses.Size = new System.Drawing.Size(592, 438);
			this.tabPageEmailAddresses.TabIndex = 3;
			this.tabPageEmailAddresses.Text = "Email Addresses";
			this.tabPageEmailAddresses.UseVisualStyleBackColor = true;
			// 
			// listViewEmailAddresses
			// 
			this.listViewEmailAddresses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.EmailAddressesEmail,
			this.EmailAddressesUrl});
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
			this.tabPageTelephoneNumbers.Size = new System.Drawing.Size(592, 438);
			this.tabPageTelephoneNumbers.TabIndex = 4;
			this.tabPageTelephoneNumbers.Text = "Telephone Numbers";
			this.tabPageTelephoneNumbers.UseVisualStyleBackColor = true;
			// 
			// listViewTelephoneNumbers
			// 
			this.listViewTelephoneNumbers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.TelTel,
			this.TelUrl});
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
			this.tabPageHistory.Size = new System.Drawing.Size(592, 438);
			this.tabPageHistory.TabIndex = 5;
			this.tabPageHistory.Text = "History";
			this.tabPageHistory.UseVisualStyleBackColor = true;
			// 
			// listViewHistory
			// 
			this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.HistoryUrl,
			this.HistoryVisited});
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
			// MacroscopeOverviewTabPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControlMain);
			this.Name = "MacroscopeOverviewTabPanel";
			this.Size = new System.Drawing.Size(600, 500);
			this.tabControlMain.ResumeLayout(false);
			this.tabPageStructureOverview.ResumeLayout(false);
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
			this.tabPageEmailAddresses.ResumeLayout(false);
			this.tabPageTelephoneNumbers.ResumeLayout(false);
			this.tabPageHistory.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
