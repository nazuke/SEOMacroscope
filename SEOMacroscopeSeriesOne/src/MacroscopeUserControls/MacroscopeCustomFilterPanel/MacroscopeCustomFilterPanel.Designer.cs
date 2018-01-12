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
	partial class MacroscopeCustomFilterPanel
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelControlsGrid;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		
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
			this.tableLayoutPanelControlsGrid = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanelControlsGrid
			// 
			this.tableLayoutPanelControlsGrid.AutoScroll = true;
			this.tableLayoutPanelControlsGrid.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanelControlsGrid.ColumnCount = 3;
			this.tableLayoutPanelControlsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelControlsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelControlsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelControlsGrid.Location = new System.Drawing.Point(3, 46);
			this.tableLayoutPanelControlsGrid.MinimumSize = new System.Drawing.Size(200, 100);
			this.tableLayoutPanelControlsGrid.Name = "tableLayoutPanelControlsGrid";
			this.tableLayoutPanelControlsGrid.RowCount = 1;
			this.tableLayoutPanelControlsGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanelControlsGrid.Size = new System.Drawing.Size(627, 211);
			this.tableLayoutPanelControlsGrid.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(10, 10);
			this.label6.Margin = new System.Windows.Forms.Padding(10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(630, 23);
			this.label6.TabIndex = 2;
			this.label6.Text = "Enter text strings to search for and extract from each HTML page.";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelControlsGrid, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(650, 450);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// MacroscopeCustomFilterPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "MacroscopeCustomFilterPanel";
			this.Size = new System.Drawing.Size(650, 450);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
