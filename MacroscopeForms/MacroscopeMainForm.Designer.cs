/*
 * Created by SharpDevelop.
 * User: jason
 * Date: 2016/12/22
 * Time: 22:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SEOMacroscope
{
	partial class MacroscopeMainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveOverviewExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generateHrefLangExcelReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutStructure;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TextBox textBoxURL;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonReset;
		private System.Windows.Forms.DataGridView dataGridStructure;
		private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.DataGridView dataGridHrefLang;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.DataGridView dataGridEmailAddresses;
		private System.Windows.Forms.DataGridView dataGridTelephoneNumbers;
		private System.Windows.Forms.DataGridView dataGridViewRedirectsAudit;
		
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveOverviewExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateHrefLangExcelReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutStructure = new System.Windows.Forms.TableLayoutPanel();
			this.dataGridStructure = new System.Windows.Forms.DataGridView();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.dataGridHrefLang = new System.Windows.Forms.DataGridView();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.dataGridViewRedirectsAudit = new System.Windows.Forms.DataGridView();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.dataGridEmailAddresses = new System.Windows.Forms.DataGridView();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.dataGridTelephoneNumbers = new System.Windows.Forms.DataGridView();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxURL = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.buttonPause = new System.Windows.Forms.Button();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonReset = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControlMain.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutStructure.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridStructure)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridHrefLang)).BeginInit();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRedirectsAudit)).BeginInit();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridEmailAddresses)).BeginInit();
			this.tabPage5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridTelephoneNumbers)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.editToolStripMenuItem,
			this.reportsToolStripMenuItem,
			this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(584, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
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
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
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
			// 
			// generateHrefLangExcelReportToolStripMenuItem
			// 
			this.generateHrefLangExcelReportToolStripMenuItem.Name = "generateHrefLangExcelReportToolStripMenuItem";
			this.generateHrefLangExcelReportToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.generateHrefLangExcelReportToolStripMenuItem.Text = "Save HrefLang Excel Report";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tabControlMain, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 338);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// tabControlMain
			// 
			this.tabControlMain.Controls.Add(this.tabPage1);
			this.tabControlMain.Controls.Add(this.tabPage2);
			this.tabControlMain.Controls.Add(this.tabPage3);
			this.tabControlMain.Controls.Add(this.tabPage4);
			this.tabControlMain.Controls.Add(this.tabPage5);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(3, 53);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(578, 282);
			this.tabControlMain.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tableLayoutStructure);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(570, 256);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Structure Overview";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutStructure
			// 
			this.tableLayoutStructure.ColumnCount = 1;
			this.tableLayoutStructure.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutStructure.Controls.Add(this.dataGridStructure, 0, 0);
			this.tableLayoutStructure.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutStructure.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutStructure.Name = "tableLayoutStructure";
			this.tableLayoutStructure.RowCount = 2;
			this.tableLayoutStructure.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutStructure.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutStructure.Size = new System.Drawing.Size(564, 250);
			this.tableLayoutStructure.TabIndex = 0;
			// 
			// dataGridStructure
			// 
			this.dataGridStructure.AllowUserToAddRows = false;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridStructure.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
			this.dataGridStructure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridStructure.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridStructure.Location = new System.Drawing.Point(0, 0);
			this.dataGridStructure.Margin = new System.Windows.Forms.Padding(0);
			this.dataGridStructure.Name = "dataGridStructure";
			this.dataGridStructure.ReadOnly = true;
			this.dataGridStructure.Size = new System.Drawing.Size(564, 125);
			this.dataGridStructure.TabIndex = 0;
			this.dataGridStructure.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.CallbackDataBindingComplete);
			this.dataGridStructure.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.CallbackDataError);
			this.dataGridStructure.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.CallbackRowsAdded);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.tableLayoutPanel4);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(570, 256);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "HrefLang Analysis";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Controls.Add(this.dataGridHrefLang, 0, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(564, 250);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// dataGridHrefLang
			// 
			this.dataGridHrefLang.AllowUserToAddRows = false;
			dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridHrefLang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle12;
			this.dataGridHrefLang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridHrefLang.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridHrefLang.Location = new System.Drawing.Point(3, 3);
			this.dataGridHrefLang.Name = "dataGridHrefLang";
			this.dataGridHrefLang.ReadOnly = true;
			this.dataGridHrefLang.Size = new System.Drawing.Size(558, 119);
			this.dataGridHrefLang.TabIndex = 0;
			this.dataGridHrefLang.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.CallbackDataBindingComplete);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.dataGridViewRedirectsAudit);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(570, 256);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Redirects Audit";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// dataGridViewRedirectsAudit
			// 
			this.dataGridViewRedirectsAudit.AllowUserToAddRows = false;
			dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridViewRedirectsAudit.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
			this.dataGridViewRedirectsAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewRedirectsAudit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewRedirectsAudit.Location = new System.Drawing.Point(3, 3);
			this.dataGridViewRedirectsAudit.Name = "dataGridViewRedirectsAudit";
			this.dataGridViewRedirectsAudit.ReadOnly = true;
			this.dataGridViewRedirectsAudit.Size = new System.Drawing.Size(564, 250);
			this.dataGridViewRedirectsAudit.TabIndex = 0;
			this.dataGridViewRedirectsAudit.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.CallbackDataBindingComplete);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.dataGridEmailAddresses);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(570, 256);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Email Addresses";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// dataGridEmailAddresses
			// 
			this.dataGridEmailAddresses.AllowUserToAddRows = false;
			dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridEmailAddresses.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
			this.dataGridEmailAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridEmailAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridEmailAddresses.Location = new System.Drawing.Point(3, 3);
			this.dataGridEmailAddresses.Name = "dataGridEmailAddresses";
			this.dataGridEmailAddresses.ReadOnly = true;
			this.dataGridEmailAddresses.Size = new System.Drawing.Size(564, 250);
			this.dataGridEmailAddresses.TabIndex = 0;
			this.dataGridEmailAddresses.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.CallbackDataBindingComplete);
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.dataGridTelephoneNumbers);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(570, 256);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Telephone Numbers";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// dataGridTelephoneNumbers
			// 
			this.dataGridTelephoneNumbers.AllowUserToAddRows = false;
			dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dataGridTelephoneNumbers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle15;
			this.dataGridTelephoneNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridTelephoneNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridTelephoneNumbers.Location = new System.Drawing.Point(3, 3);
			this.dataGridTelephoneNumbers.Name = "dataGridTelephoneNumbers";
			this.dataGridTelephoneNumbers.ReadOnly = true;
			this.dataGridTelephoneNumbers.Size = new System.Drawing.Size(564, 250);
			this.dataGridTelephoneNumbers.TabIndex = 0;
			this.dataGridTelephoneNumbers.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.CallbackDataBindingComplete);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.tableLayoutPanel2.Controls.Add(this.textBoxURL, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(578, 44);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// textBoxURL
			// 
			this.textBoxURL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxURL.Location = new System.Drawing.Point(3, 3);
			this.textBoxURL.Name = "textBoxURL";
			this.textBoxURL.Size = new System.Drawing.Size(322, 20);
			this.textBoxURL.TabIndex = 0;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel3.Controls.Add(this.buttonPause, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.buttonStart, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.buttonReset, 2, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(328, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(250, 44);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// buttonPause
			// 
			this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPause.Location = new System.Drawing.Point(103, 3);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(69, 24);
			this.buttonPause.TabIndex = 2;
			this.buttonPause.Text = "Pause";
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Click += new System.EventHandler(this.CallbackScanPause);
			// 
			// buttonStart
			// 
			this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStart.Location = new System.Drawing.Point(3, 3);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(94, 24);
			this.buttonStart.TabIndex = 0;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.CallbackScanStart);
			// 
			// buttonReset
			// 
			this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReset.Location = new System.Drawing.Point(178, 3);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(69, 24);
			this.buttonReset.TabIndex = 1;
			this.buttonReset.Text = "Reset";
			this.buttonReset.UseVisualStyleBackColor = true;
			this.buttonReset.Click += new System.EventHandler(this.CallbackScanReset);
			// 
			// MacroscopeMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 362);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "MacroscopeMainForm";
			this.Text = "MacroscopeMainForm";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControlMain.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutStructure.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridStructure)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridHrefLang)).EndInit();
			this.tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRedirectsAudit)).EndInit();
			this.tabPage4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridEmailAddresses)).EndInit();
			this.tabPage5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridTelephoneNumbers)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
