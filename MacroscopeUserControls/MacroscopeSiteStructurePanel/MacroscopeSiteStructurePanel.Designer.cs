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
			this.tabPageSiteSpeed = new System.Windows.Forms.TabPage();
			this.treeViewSiteOverview = new System.Windows.Forms.TreeView();
			this.tabControlSiteStructure.SuspendLayout();
			this.tabPageSiteOverview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSiteOverview)).BeginInit();
			this.splitContainerSiteOverview.Panel1.SuspendLayout();
			this.splitContainerSiteOverview.SuspendLayout();
			this.SuspendLayout();
			//
			// tabControlSiteStructure
			//
			this.tabControlSiteStructure.Controls.Add(this.tabPageSiteOverview);
			this.tabControlSiteStructure.Controls.Add(this.tabPageSiteSpeed);
			this.tabControlSiteStructure.Location = new System.Drawing.Point(20, 20);
			this.tabControlSiteStructure.Margin = new System.Windows.Forms.Padding(0);
			this.tabControlSiteStructure.Name = "tabControlSiteStructure";
			this.tabControlSiteStructure.SelectedIndex = 0;
			this.tabControlSiteStructure.Size = new System.Drawing.Size(440, 440);
			this.tabControlSiteStructure.TabIndex = 0;
			//
			// tabPageSiteOverview
			//
			this.tabPageSiteOverview.Controls.Add(this.splitContainerSiteOverview);
			this.tabPageSiteOverview.Location = new System.Drawing.Point(4, 22);
			this.tabPageSiteOverview.Name = "tabPageSiteOverview";
			this.tabPageSiteOverview.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSiteOverview.Size = new System.Drawing.Size(432, 414);
			this.tabPageSiteOverview.TabIndex = 0;
			this.tabPageSiteOverview.Text = "Site Overview";
			this.tabPageSiteOverview.UseVisualStyleBackColor = true;
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
			// tabPageSiteSpeed
			//
			this.tabPageSiteSpeed.Location = new System.Drawing.Point(4, 22);
			this.tabPageSiteSpeed.Name = "tabPageSiteSpeed";
			this.tabPageSiteSpeed.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSiteSpeed.Size = new System.Drawing.Size(432, 414);
			this.tabPageSiteSpeed.TabIndex = 1;
			this.tabPageSiteSpeed.Text = "Site Speed";
			this.tabPageSiteSpeed.UseVisualStyleBackColor = true;
			//
			// treeViewSiteOverview
			//
			this.treeViewSiteOverview.Location = new System.Drawing.Point(20, 20);
			this.treeViewSiteOverview.Margin = new System.Windows.Forms.Padding(0);
			this.treeViewSiteOverview.Name = "treeViewSiteOverview";
			this.treeViewSiteOverview.Size = new System.Drawing.Size(150, 150);
			this.treeViewSiteOverview.TabIndex = 0;
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
			this.ResumeLayout(false);

		}
	}
}
