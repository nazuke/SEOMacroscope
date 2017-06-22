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
	partial class MacroscopeDataExtractorCssSelectorPanel
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRegexGrid;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContainer;
		
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
			this.tableLayoutPanelRegexGrid = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.tableLayoutPanelContainer = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanelContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanelRegexGrid
			// 
			this.tableLayoutPanelRegexGrid.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tableLayoutPanelRegexGrid.ColumnCount = 4;
			this.tableLayoutPanelRegexGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanelRegexGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanelRegexGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanelRegexGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelRegexGrid.Location = new System.Drawing.Point(10, 50);
			this.tableLayoutPanelRegexGrid.Margin = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanelRegexGrid.Name = "tableLayoutPanelRegexGrid";
			this.tableLayoutPanelRegexGrid.RowCount = 1;
			this.tableLayoutPanelRegexGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelRegexGrid.Size = new System.Drawing.Size(600, 250);
			this.tableLayoutPanelRegexGrid.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(10, 10);
			this.label6.Margin = new System.Windows.Forms.Padding(10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(600, 20);
			this.label6.TabIndex = 2;
			this.label6.Text = "Enter CSS Selectors to search for and extract from each HTML page.";
			// 
			// tableLayoutPanelContainer
			// 
			this.tableLayoutPanelContainer.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tableLayoutPanelContainer.ColumnCount = 1;
			this.tableLayoutPanelContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelContainer.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanelContainer.Controls.Add(this.tableLayoutPanelRegexGrid, 0, 1);
			this.tableLayoutPanelContainer.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanelContainer.Name = "tableLayoutPanelContainer";
			this.tableLayoutPanelContainer.RowCount = 2;
			this.tableLayoutPanelContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanelContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelContainer.Size = new System.Drawing.Size(750, 372);
			this.tableLayoutPanelContainer.TabIndex = 0;
			// 
			// MacroscopeDataExtractorCssSelectorPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.Controls.Add(this.tableLayoutPanelContainer);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "MacroscopeDataExtractorCssSelectorPanel";
			this.Size = new System.Drawing.Size(850, 402);
			this.tableLayoutPanelContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
