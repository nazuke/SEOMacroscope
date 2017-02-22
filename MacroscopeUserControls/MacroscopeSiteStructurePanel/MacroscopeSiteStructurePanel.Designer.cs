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
	partial class MacroscopeSiteStructurePanel
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TabControl tabControlSiteStructure;
		private System.Windows.Forms.TabPage tabPageSiteOverview;
		private System.Windows.Forms.TabPage tabPageSiteSpeed;
		public System.Windows.Forms.SplitContainer splitContainerSiteOverview;
		public System.Windows.Forms.TreeView treeViewSiteOverview;
		private System.Windows.Forms.TabPage tabPageKeywordAnalysis;
		public System.Windows.Forms.ListView listViewKeywordAnalysis1;
		public System.Windows.Forms.ColumnHeader columnHeaderKeywordAnalysisOccurrences;
		private System.Windows.Forms.ColumnHeader columnHeaderKeywordAnalysisTerm;
		public System.Windows.Forms.TabControl tabControlKeywordAnalysisPhrases;
		public System.Windows.Forms.TabPage tabPageKeywordAnalysisPhrases1;
		public System.Windows.Forms.TabPage tabPageKeywordAnalysisPhrases2;
		public System.Windows.Forms.TabPage tabPageKeywordAnalysisPhrases3;
		public System.Windows.Forms.TabPage tabPageKeywordAnalysisPhrases4;
		public System.Windows.Forms.ListView listViewKeywordAnalysis2;
		public System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		public System.Windows.Forms.ListView listViewKeywordAnalysis3;
		public System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		public System.Windows.Forms.ListView listViewKeywordAnalysis4;
		public System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		public System.Windows.Forms.TabControl tabControlSiteSpeed;
		public System.Windows.Forms.TabPage tabPageSiteSpeedFastest;
		public System.Windows.Forms.ListView listViewSiteSpeedFastest;
		public System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		public System.Windows.Forms.TabPage tabPageSiteSpeedSlowest;
		public System.Windows.Forms.ListView listViewSiteSpeedSlowest;
		public System.Windows.Forms.ColumnHeader columnHeaderSiteSpeedTime;
		private System.Windows.Forms.ColumnHeader columnHeaderSiteSpeedTimeUrl;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSiteSpeed;
		private System.Windows.Forms.ToolStrip toolStrip1;
		public System.Windows.Forms.ToolStripLabel toolStripLabelSiteSpeedAverage;

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
			this.tabControlSiteStructure = new System.Windows.Forms.TabControl();
			this.tabPageSiteOverview = new System.Windows.Forms.TabPage();
			this.splitContainerSiteOverview = new System.Windows.Forms.SplitContainer();
			this.treeViewSiteOverview = new System.Windows.Forms.TreeView();
			this.tabPageSiteSpeed = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelSiteSpeed = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabelSiteSpeedAverage = new System.Windows.Forms.ToolStripLabel();
			this.tabControlSiteSpeed = new System.Windows.Forms.TabControl();
			this.tabPageSiteSpeedSlowest = new System.Windows.Forms.TabPage();
			this.listViewSiteSpeedSlowest = new System.Windows.Forms.ListView();
			this.columnHeaderSiteSpeedTime = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSiteSpeedTimeUrl = new System.Windows.Forms.ColumnHeader();
			this.tabPageSiteSpeedFastest = new System.Windows.Forms.TabPage();
			this.listViewSiteSpeedFastest = new System.Windows.Forms.ListView();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.tabPageKeywordAnalysis = new System.Windows.Forms.TabPage();
			this.tabControlKeywordAnalysisPhrases = new System.Windows.Forms.TabControl();
			this.tabPageKeywordAnalysisPhrases1 = new System.Windows.Forms.TabPage();
			this.listViewKeywordAnalysis1 = new System.Windows.Forms.ListView();
			this.columnHeaderKeywordAnalysisOccurrences = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderKeywordAnalysisTerm = new System.Windows.Forms.ColumnHeader();
			this.tabPageKeywordAnalysisPhrases2 = new System.Windows.Forms.TabPage();
			this.listViewKeywordAnalysis2 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.tabPageKeywordAnalysisPhrases3 = new System.Windows.Forms.TabPage();
			this.listViewKeywordAnalysis3 = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.tabPageKeywordAnalysisPhrases4 = new System.Windows.Forms.TabPage();
			this.listViewKeywordAnalysis4 = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.tabControlSiteStructure.SuspendLayout();
			this.tabPageSiteOverview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSiteOverview)).BeginInit();
			this.splitContainerSiteOverview.Panel1.SuspendLayout();
			this.splitContainerSiteOverview.SuspendLayout();
			this.tabPageSiteSpeed.SuspendLayout();
			this.tableLayoutPanelSiteSpeed.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tabControlSiteSpeed.SuspendLayout();
			this.tabPageSiteSpeedSlowest.SuspendLayout();
			this.tabPageSiteSpeedFastest.SuspendLayout();
			this.tabPageKeywordAnalysis.SuspendLayout();
			this.tabControlKeywordAnalysisPhrases.SuspendLayout();
			this.tabPageKeywordAnalysisPhrases1.SuspendLayout();
			this.tabPageKeywordAnalysisPhrases2.SuspendLayout();
			this.tabPageKeywordAnalysisPhrases3.SuspendLayout();
			this.tabPageKeywordAnalysisPhrases4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlSiteStructure
			// 
			this.tabControlSiteStructure.Controls.Add(this.tabPageSiteOverview);
			this.tabControlSiteStructure.Controls.Add(this.tabPageSiteSpeed);
			this.tabControlSiteStructure.Controls.Add(this.tabPageKeywordAnalysis);
			this.tabControlSiteStructure.Location = new System.Drawing.Point(20, 20);
			this.tabControlSiteStructure.Name = "tabControlSiteStructure";
			this.tabControlSiteStructure.SelectedIndex = 0;
			this.tabControlSiteStructure.Size = new System.Drawing.Size(440, 440);
			this.tabControlSiteStructure.TabIndex = 0;
			this.tabControlSiteStructure.Click += new System.EventHandler(this.TabControlSiteStructureClick);
			// 
			// tabPageSiteOverview
			// 
			this.tabPageSiteOverview.BackColor = System.Drawing.Color.LightGray;
			this.tabPageSiteOverview.CausesValidation = false;
			this.tabPageSiteOverview.Controls.Add(this.splitContainerSiteOverview);
			this.tabPageSiteOverview.Location = new System.Drawing.Point(4, 22);
			this.tabPageSiteOverview.Name = "tabPageSiteOverview";
			this.tabPageSiteOverview.Size = new System.Drawing.Size(432, 414);
			this.tabPageSiteOverview.TabIndex = 0;
			this.tabPageSiteOverview.Text = "Site Overview";
			// 
			// splitContainerSiteOverview
			// 
			this.splitContainerSiteOverview.Location = new System.Drawing.Point(20, 20);
			this.splitContainerSiteOverview.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainerSiteOverview.Name = "splitContainerSiteOverview";
			this.splitContainerSiteOverview.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerSiteOverview.Panel1
			// 
			this.splitContainerSiteOverview.Panel1.Controls.Add(this.treeViewSiteOverview);
			this.splitContainerSiteOverview.Size = new System.Drawing.Size(300, 300);
			this.splitContainerSiteOverview.SplitterDistance = 200;
			this.splitContainerSiteOverview.SplitterIncrement = 10;
			this.splitContainerSiteOverview.SplitterWidth = 6;
			this.splitContainerSiteOverview.TabIndex = 0;
			// 
			// treeViewSiteOverview
			// 
			this.treeViewSiteOverview.Location = new System.Drawing.Point(20, 20);
			this.treeViewSiteOverview.Margin = new System.Windows.Forms.Padding(0);
			this.treeViewSiteOverview.Name = "treeViewSiteOverview";
			this.treeViewSiteOverview.Size = new System.Drawing.Size(150, 150);
			this.treeViewSiteOverview.TabIndex = 0;
			// 
			// tabPageSiteSpeed
			// 
			this.tabPageSiteSpeed.BackColor = System.Drawing.Color.LightGray;
			this.tabPageSiteSpeed.CausesValidation = false;
			this.tabPageSiteSpeed.Controls.Add(this.tableLayoutPanelSiteSpeed);
			this.tabPageSiteSpeed.Location = new System.Drawing.Point(4, 22);
			this.tabPageSiteSpeed.Name = "tabPageSiteSpeed";
			this.tabPageSiteSpeed.Size = new System.Drawing.Size(432, 414);
			this.tabPageSiteSpeed.TabIndex = 1;
			this.tabPageSiteSpeed.Text = "Site Speed";
			// 
			// tableLayoutPanelSiteSpeed
			// 
			this.tableLayoutPanelSiteSpeed.ColumnCount = 1;
			this.tableLayoutPanelSiteSpeed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelSiteSpeed.Controls.Add(this.toolStrip1, 0, 1);
			this.tableLayoutPanelSiteSpeed.Controls.Add(this.tabControlSiteSpeed, 0, 0);
			this.tableLayoutPanelSiteSpeed.Location = new System.Drawing.Point(20, 20);
			this.tableLayoutPanelSiteSpeed.Name = "tableLayoutPanelSiteSpeed";
			this.tableLayoutPanelSiteSpeed.RowCount = 2;
			this.tableLayoutPanelSiteSpeed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelSiteSpeed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanelSiteSpeed.Size = new System.Drawing.Size(400, 370);
			this.tableLayoutPanelSiteSpeed.TabIndex = 3;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripLabelSiteSpeedAverage});
			this.toolStrip1.Location = new System.Drawing.Point(0, 342);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(3);
			this.toolStrip1.Size = new System.Drawing.Size(400, 28);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripLabelSiteSpeedAverage
			// 
			this.toolStripLabelSiteSpeedAverage.Name = "toolStripLabelSiteSpeedAverage";
			this.toolStripLabelSiteSpeedAverage.Size = new System.Drawing.Size(13, 19);
			this.toolStripLabelSiteSpeedAverage.Text = "0";
			// 
			// tabControlSiteSpeed
			// 
			this.tabControlSiteSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tabControlSiteSpeed.Controls.Add(this.tabPageSiteSpeedSlowest);
			this.tabControlSiteSpeed.Controls.Add(this.tabPageSiteSpeedFastest);
			this.tabControlSiteSpeed.Location = new System.Drawing.Point(50, 21);
			this.tabControlSiteSpeed.Name = "tabControlSiteSpeed";
			this.tabControlSiteSpeed.SelectedIndex = 0;
			this.tabControlSiteSpeed.Size = new System.Drawing.Size(300, 300);
			this.tabControlSiteSpeed.TabIndex = 2;
			// 
			// tabPageSiteSpeedSlowest
			// 
			this.tabPageSiteSpeedSlowest.Controls.Add(this.listViewSiteSpeedSlowest);
			this.tabPageSiteSpeedSlowest.Location = new System.Drawing.Point(4, 22);
			this.tabPageSiteSpeedSlowest.Name = "tabPageSiteSpeedSlowest";
			this.tabPageSiteSpeedSlowest.Size = new System.Drawing.Size(292, 274);
			this.tabPageSiteSpeedSlowest.TabIndex = 1;
			this.tabPageSiteSpeedSlowest.Text = "Slowest Pages";
			this.tabPageSiteSpeedSlowest.UseVisualStyleBackColor = true;
			// 
			// listViewSiteSpeedSlowest
			// 
			this.listViewSiteSpeedSlowest.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderSiteSpeedTime,
			this.columnHeaderSiteSpeedTimeUrl});
			this.listViewSiteSpeedSlowest.GridLines = true;
			this.listViewSiteSpeedSlowest.Location = new System.Drawing.Point(20, 20);
			this.listViewSiteSpeedSlowest.Name = "listViewSiteSpeedSlowest";
			this.listViewSiteSpeedSlowest.Size = new System.Drawing.Size(200, 200);
			this.listViewSiteSpeedSlowest.TabIndex = 1;
			this.listViewSiteSpeedSlowest.UseCompatibleStateImageBehavior = false;
			this.listViewSiteSpeedSlowest.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderSiteSpeedTime
			// 
			this.columnHeaderSiteSpeedTime.Text = "Response Time (secs)";
			this.columnHeaderSiteSpeedTime.Width = 150;
			// 
			// columnHeaderSiteSpeedTimeUrl
			// 
			this.columnHeaderSiteSpeedTimeUrl.Text = "URL";
			this.columnHeaderSiteSpeedTimeUrl.Width = 300;
			// 
			// tabPageSiteSpeedFastest
			// 
			this.tabPageSiteSpeedFastest.Controls.Add(this.listViewSiteSpeedFastest);
			this.tabPageSiteSpeedFastest.Location = new System.Drawing.Point(4, 22);
			this.tabPageSiteSpeedFastest.Name = "tabPageSiteSpeedFastest";
			this.tabPageSiteSpeedFastest.Size = new System.Drawing.Size(292, 274);
			this.tabPageSiteSpeedFastest.TabIndex = 0;
			this.tabPageSiteSpeedFastest.Text = "Fastest Pages";
			this.tabPageSiteSpeedFastest.UseVisualStyleBackColor = true;
			// 
			// listViewSiteSpeedFastest
			// 
			this.listViewSiteSpeedFastest.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader7,
			this.columnHeader8});
			this.listViewSiteSpeedFastest.GridLines = true;
			this.listViewSiteSpeedFastest.Location = new System.Drawing.Point(20, 20);
			this.listViewSiteSpeedFastest.Name = "listViewSiteSpeedFastest";
			this.listViewSiteSpeedFastest.Size = new System.Drawing.Size(200, 200);
			this.listViewSiteSpeedFastest.TabIndex = 2;
			this.listViewSiteSpeedFastest.UseCompatibleStateImageBehavior = false;
			this.listViewSiteSpeedFastest.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Response Time (secs)";
			this.columnHeader7.Width = 150;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "URL";
			this.columnHeader8.Width = 300;
			// 
			// tabPageKeywordAnalysis
			// 
			this.tabPageKeywordAnalysis.BackColor = System.Drawing.Color.LightGray;
			this.tabPageKeywordAnalysis.CausesValidation = false;
			this.tabPageKeywordAnalysis.Controls.Add(this.tabControlKeywordAnalysisPhrases);
			this.tabPageKeywordAnalysis.Location = new System.Drawing.Point(4, 22);
			this.tabPageKeywordAnalysis.Name = "tabPageKeywordAnalysis";
			this.tabPageKeywordAnalysis.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageKeywordAnalysis.Size = new System.Drawing.Size(432, 414);
			this.tabPageKeywordAnalysis.TabIndex = 2;
			this.tabPageKeywordAnalysis.Text = "Keyword Analysis";
			// 
			// tabControlKeywordAnalysisPhrases
			// 
			this.tabControlKeywordAnalysisPhrases.Controls.Add(this.tabPageKeywordAnalysisPhrases1);
			this.tabControlKeywordAnalysisPhrases.Controls.Add(this.tabPageKeywordAnalysisPhrases2);
			this.tabControlKeywordAnalysisPhrases.Controls.Add(this.tabPageKeywordAnalysisPhrases3);
			this.tabControlKeywordAnalysisPhrases.Controls.Add(this.tabPageKeywordAnalysisPhrases4);
			this.tabControlKeywordAnalysisPhrases.Location = new System.Drawing.Point(20, 20);
			this.tabControlKeywordAnalysisPhrases.Name = "tabControlKeywordAnalysisPhrases";
			this.tabControlKeywordAnalysisPhrases.SelectedIndex = 0;
			this.tabControlKeywordAnalysisPhrases.Size = new System.Drawing.Size(400, 300);
			this.tabControlKeywordAnalysisPhrases.TabIndex = 1;
			this.tabControlKeywordAnalysisPhrases.Click += new System.EventHandler(this.TabControlKeywordAnalysisPhrasesClick);
			// 
			// tabPageKeywordAnalysisPhrases1
			// 
			this.tabPageKeywordAnalysisPhrases1.Controls.Add(this.listViewKeywordAnalysis1);
			this.tabPageKeywordAnalysisPhrases1.Location = new System.Drawing.Point(4, 22);
			this.tabPageKeywordAnalysisPhrases1.Name = "tabPageKeywordAnalysisPhrases1";
			this.tabPageKeywordAnalysisPhrases1.Size = new System.Drawing.Size(392, 274);
			this.tabPageKeywordAnalysisPhrases1.TabIndex = 0;
			this.tabPageKeywordAnalysisPhrases1.Text = "One Word Terms";
			this.tabPageKeywordAnalysisPhrases1.UseVisualStyleBackColor = true;
			// 
			// listViewKeywordAnalysis1
			// 
			this.listViewKeywordAnalysis1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderKeywordAnalysisOccurrences,
			this.columnHeaderKeywordAnalysisTerm});
			this.listViewKeywordAnalysis1.GridLines = true;
			this.listViewKeywordAnalysis1.Location = new System.Drawing.Point(20, 20);
			this.listViewKeywordAnalysis1.Name = "listViewKeywordAnalysis1";
			this.listViewKeywordAnalysis1.Size = new System.Drawing.Size(200, 200);
			this.listViewKeywordAnalysis1.TabIndex = 0;
			this.listViewKeywordAnalysis1.UseCompatibleStateImageBehavior = false;
			this.listViewKeywordAnalysis1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderKeywordAnalysisOccurrences
			// 
			this.columnHeaderKeywordAnalysisOccurrences.Text = "Occurrences";
			this.columnHeaderKeywordAnalysisOccurrences.Width = 100;
			// 
			// columnHeaderKeywordAnalysisTerm
			// 
			this.columnHeaderKeywordAnalysisTerm.Text = "Term";
			this.columnHeaderKeywordAnalysisTerm.Width = 300;
			// 
			// tabPageKeywordAnalysisPhrases2
			// 
			this.tabPageKeywordAnalysisPhrases2.Controls.Add(this.listViewKeywordAnalysis2);
			this.tabPageKeywordAnalysisPhrases2.Location = new System.Drawing.Point(4, 22);
			this.tabPageKeywordAnalysisPhrases2.Name = "tabPageKeywordAnalysisPhrases2";
			this.tabPageKeywordAnalysisPhrases2.Size = new System.Drawing.Size(392, 274);
			this.tabPageKeywordAnalysisPhrases2.TabIndex = 1;
			this.tabPageKeywordAnalysisPhrases2.Text = "Two Word Terms";
			this.tabPageKeywordAnalysisPhrases2.UseVisualStyleBackColor = true;
			// 
			// listViewKeywordAnalysis2
			// 
			this.listViewKeywordAnalysis2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2});
			this.listViewKeywordAnalysis2.GridLines = true;
			this.listViewKeywordAnalysis2.Location = new System.Drawing.Point(20, 20);
			this.listViewKeywordAnalysis2.Name = "listViewKeywordAnalysis2";
			this.listViewKeywordAnalysis2.Size = new System.Drawing.Size(200, 200);
			this.listViewKeywordAnalysis2.TabIndex = 1;
			this.listViewKeywordAnalysis2.UseCompatibleStateImageBehavior = false;
			this.listViewKeywordAnalysis2.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Occurrences";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Term";
			this.columnHeader2.Width = 300;
			// 
			// tabPageKeywordAnalysisPhrases3
			// 
			this.tabPageKeywordAnalysisPhrases3.Controls.Add(this.listViewKeywordAnalysis3);
			this.tabPageKeywordAnalysisPhrases3.Location = new System.Drawing.Point(4, 22);
			this.tabPageKeywordAnalysisPhrases3.Name = "tabPageKeywordAnalysisPhrases3";
			this.tabPageKeywordAnalysisPhrases3.Size = new System.Drawing.Size(392, 274);
			this.tabPageKeywordAnalysisPhrases3.TabIndex = 2;
			this.tabPageKeywordAnalysisPhrases3.Text = "Three Word Terms";
			this.tabPageKeywordAnalysisPhrases3.UseVisualStyleBackColor = true;
			// 
			// listViewKeywordAnalysis3
			// 
			this.listViewKeywordAnalysis3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader3,
			this.columnHeader4});
			this.listViewKeywordAnalysis3.GridLines = true;
			this.listViewKeywordAnalysis3.Location = new System.Drawing.Point(20, 20);
			this.listViewKeywordAnalysis3.Name = "listViewKeywordAnalysis3";
			this.listViewKeywordAnalysis3.Size = new System.Drawing.Size(200, 200);
			this.listViewKeywordAnalysis3.TabIndex = 1;
			this.listViewKeywordAnalysis3.UseCompatibleStateImageBehavior = false;
			this.listViewKeywordAnalysis3.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Occurrences";
			this.columnHeader3.Width = 100;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Term";
			this.columnHeader4.Width = 300;
			// 
			// tabPageKeywordAnalysisPhrases4
			// 
			this.tabPageKeywordAnalysisPhrases4.Controls.Add(this.listViewKeywordAnalysis4);
			this.tabPageKeywordAnalysisPhrases4.Location = new System.Drawing.Point(4, 22);
			this.tabPageKeywordAnalysisPhrases4.Name = "tabPageKeywordAnalysisPhrases4";
			this.tabPageKeywordAnalysisPhrases4.Size = new System.Drawing.Size(392, 274);
			this.tabPageKeywordAnalysisPhrases4.TabIndex = 3;
			this.tabPageKeywordAnalysisPhrases4.Text = "Four Word Terms";
			this.tabPageKeywordAnalysisPhrases4.UseVisualStyleBackColor = true;
			// 
			// listViewKeywordAnalysis4
			// 
			this.listViewKeywordAnalysis4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader5,
			this.columnHeader6});
			this.listViewKeywordAnalysis4.GridLines = true;
			this.listViewKeywordAnalysis4.Location = new System.Drawing.Point(20, 20);
			this.listViewKeywordAnalysis4.Name = "listViewKeywordAnalysis4";
			this.listViewKeywordAnalysis4.Size = new System.Drawing.Size(200, 200);
			this.listViewKeywordAnalysis4.TabIndex = 1;
			this.listViewKeywordAnalysis4.UseCompatibleStateImageBehavior = false;
			this.listViewKeywordAnalysis4.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Occurrences";
			this.columnHeader5.Width = 100;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Term";
			this.columnHeader6.Width = 300;
			// 
			// MacroscopeSiteStructurePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControlSiteStructure);
			this.Name = "MacroscopeSiteStructurePanel";
			this.Size = new System.Drawing.Size(500, 600);
			this.tabControlSiteStructure.ResumeLayout(false);
			this.tabPageSiteOverview.ResumeLayout(false);
			this.splitContainerSiteOverview.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSiteOverview)).EndInit();
			this.splitContainerSiteOverview.ResumeLayout(false);
			this.tabPageSiteSpeed.ResumeLayout(false);
			this.tableLayoutPanelSiteSpeed.ResumeLayout(false);
			this.tableLayoutPanelSiteSpeed.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabControlSiteSpeed.ResumeLayout(false);
			this.tabPageSiteSpeedSlowest.ResumeLayout(false);
			this.tabPageSiteSpeedFastest.ResumeLayout(false);
			this.tabPageKeywordAnalysis.ResumeLayout(false);
			this.tabControlKeywordAnalysisPhrases.ResumeLayout(false);
			this.tabPageKeywordAnalysisPhrases1.ResumeLayout(false);
			this.tabPageKeywordAnalysisPhrases2.ResumeLayout(false);
			this.tabPageKeywordAnalysisPhrases3.ResumeLayout(false);
			this.tabPageKeywordAnalysisPhrases4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
