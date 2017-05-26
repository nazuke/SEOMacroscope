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
	partial class MacroscopeCustomFilterPanel
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ComboBox comboBoxFilter1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboBoxFilter2;
		private System.Windows.Forms.ComboBox comboBoxFilter3;
		private System.Windows.Forms.ComboBox comboBoxFilter4;
		private System.Windows.Forms.ComboBox comboBoxFilter5;
		private System.Windows.Forms.TextBox textBoxFilter5;
		private System.Windows.Forms.TextBox textBoxFilter4;
		private System.Windows.Forms.TextBox textBoxFilter3;
		private System.Windows.Forms.TextBox textBoxFilter2;
		private System.Windows.Forms.TextBox textBoxFilter1;
		private System.Windows.Forms.Label label6;
		
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
			this.comboBoxFilter1 = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxFilter5 = new System.Windows.Forms.TextBox();
			this.textBoxFilter4 = new System.Windows.Forms.TextBox();
			this.textBoxFilter3 = new System.Windows.Forms.TextBox();
			this.textBoxFilter2 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBoxFilter2 = new System.Windows.Forms.ComboBox();
			this.comboBoxFilter3 = new System.Windows.Forms.ComboBox();
			this.comboBoxFilter4 = new System.Windows.Forms.ComboBox();
			this.comboBoxFilter5 = new System.Windows.Forms.ComboBox();
			this.textBoxFilter1 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxFilter1
			// 
			this.comboBoxFilter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFilter1.FormattingEnabled = true;
			this.comboBoxFilter1.Items.AddRange(new object[] {
			"Contains",
			"Does not contain"});
			this.comboBoxFilter1.Location = new System.Drawing.Point(123, 3);
			this.comboBoxFilter1.Name = "comboBoxFilter1";
			this.comboBoxFilter1.Size = new System.Drawing.Size(113, 21);
			this.comboBoxFilter1.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tableLayoutPanel1.Controls.Add(this.textBoxFilter5, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.textBoxFilter4, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.textBoxFilter3, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.textBoxFilter2, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxFilter1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxFilter2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxFilter3, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxFilter4, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxFilter5, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.textBoxFilter1, 2, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 50);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 240);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// textBoxFilter5
			// 
			this.textBoxFilter5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxFilter5.Location = new System.Drawing.Point(243, 195);
			this.textBoxFilter5.Name = "textBoxFilter5";
			this.textBoxFilter5.Size = new System.Drawing.Size(354, 20);
			this.textBoxFilter5.TabIndex = 14;
			// 
			// textBoxFilter4
			// 
			this.textBoxFilter4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxFilter4.Location = new System.Drawing.Point(243, 147);
			this.textBoxFilter4.Name = "textBoxFilter4";
			this.textBoxFilter4.Size = new System.Drawing.Size(354, 20);
			this.textBoxFilter4.TabIndex = 13;
			// 
			// textBoxFilter3
			// 
			this.textBoxFilter3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxFilter3.Location = new System.Drawing.Point(243, 99);
			this.textBoxFilter3.Name = "textBoxFilter3";
			this.textBoxFilter3.Size = new System.Drawing.Size(354, 20);
			this.textBoxFilter3.TabIndex = 12;
			// 
			// textBoxFilter2
			// 
			this.textBoxFilter2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxFilter2.Location = new System.Drawing.Point(243, 51);
			this.textBoxFilter2.Name = "textBoxFilter2";
			this.textBoxFilter2.Size = new System.Drawing.Size(354, 20);
			this.textBoxFilter2.TabIndex = 11;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
			this.label1.Size = new System.Drawing.Size(114, 48);
			this.label1.TabIndex = 1;
			this.label1.Text = "Custom Filter 1";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(3, 48);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
			this.label2.Size = new System.Drawing.Size(114, 48);
			this.label2.TabIndex = 2;
			this.label2.Text = "Custom Filter 2";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(3, 96);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
			this.label3.Size = new System.Drawing.Size(114, 48);
			this.label3.TabIndex = 3;
			this.label3.Text = "Custom Filter 3";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(3, 144);
			this.label4.Name = "label4";
			this.label4.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
			this.label4.Size = new System.Drawing.Size(114, 48);
			this.label4.TabIndex = 4;
			this.label4.Text = "Custom Filter 4";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(3, 192);
			this.label5.Name = "label5";
			this.label5.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
			this.label5.Size = new System.Drawing.Size(114, 48);
			this.label5.TabIndex = 5;
			this.label5.Text = "Custom Filter 5";
			// 
			// comboBoxFilter2
			// 
			this.comboBoxFilter2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFilter2.FormattingEnabled = true;
			this.comboBoxFilter2.Items.AddRange(new object[] {
			"Contains",
			"Does not contain"});
			this.comboBoxFilter2.Location = new System.Drawing.Point(123, 51);
			this.comboBoxFilter2.Name = "comboBoxFilter2";
			this.comboBoxFilter2.Size = new System.Drawing.Size(113, 21);
			this.comboBoxFilter2.TabIndex = 6;
			// 
			// comboBoxFilter3
			// 
			this.comboBoxFilter3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFilter3.FormattingEnabled = true;
			this.comboBoxFilter3.Items.AddRange(new object[] {
			"Contains",
			"Does not contain"});
			this.comboBoxFilter3.Location = new System.Drawing.Point(123, 99);
			this.comboBoxFilter3.Name = "comboBoxFilter3";
			this.comboBoxFilter3.Size = new System.Drawing.Size(113, 21);
			this.comboBoxFilter3.TabIndex = 7;
			// 
			// comboBoxFilter4
			// 
			this.comboBoxFilter4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFilter4.FormattingEnabled = true;
			this.comboBoxFilter4.Items.AddRange(new object[] {
			"Contains",
			"Does not contain"});
			this.comboBoxFilter4.Location = new System.Drawing.Point(123, 147);
			this.comboBoxFilter4.Name = "comboBoxFilter4";
			this.comboBoxFilter4.Size = new System.Drawing.Size(113, 21);
			this.comboBoxFilter4.TabIndex = 8;
			// 
			// comboBoxFilter5
			// 
			this.comboBoxFilter5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFilter5.FormattingEnabled = true;
			this.comboBoxFilter5.Items.AddRange(new object[] {
			"Contains",
			"Does not contain"});
			this.comboBoxFilter5.Location = new System.Drawing.Point(123, 195);
			this.comboBoxFilter5.Name = "comboBoxFilter5";
			this.comboBoxFilter5.Size = new System.Drawing.Size(113, 21);
			this.comboBoxFilter5.TabIndex = 9;
			// 
			// textBoxFilter1
			// 
			this.textBoxFilter1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxFilter1.Location = new System.Drawing.Point(243, 3);
			this.textBoxFilter1.Name = "textBoxFilter1";
			this.textBoxFilter1.Size = new System.Drawing.Size(354, 20);
			this.textBoxFilter1.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(20, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(600, 23);
			this.label6.TabIndex = 2;
			this.label6.Text = "Enter text strings to search for and extract from each HTML page.";
			// 
			// MacroscopeCustomFilterPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label6);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "MacroscopeCustomFilterPanel";
			this.Size = new System.Drawing.Size(640, 320);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
