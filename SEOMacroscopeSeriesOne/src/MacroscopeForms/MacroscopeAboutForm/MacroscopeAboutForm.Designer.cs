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
	partial class MacroscopeAboutForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label labelVersionString;
		public System.Windows.Forms.Label label3;
		public System.Windows.Forms.Label labelArchitectureString;

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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.labelVersionString = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.labelArchitectureString = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(20, 20);
      this.label1.Margin = new System.Windows.Forms.Padding(0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(300, 50);
      this.label1.TabIndex = 1;
      this.label1.Text = "SEO Macroscope";
      // 
      // label2
      // 
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(20, 320);
      this.label2.Margin = new System.Windows.Forms.Padding(0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(220, 20);
      this.label2.TabIndex = 2;
      this.label2.Text = "Copyright 2018 Jason Holland";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelVersionString
      // 
      this.labelVersionString.BackColor = System.Drawing.Color.Transparent;
      this.labelVersionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelVersionString.ForeColor = System.Drawing.Color.White;
      this.labelVersionString.Location = new System.Drawing.Point(20, 280);
      this.labelVersionString.Margin = new System.Windows.Forms.Padding(0);
      this.labelVersionString.Name = "labelVersionString";
      this.labelVersionString.Size = new System.Drawing.Size(220, 20);
      this.labelVersionString.TabIndex = 3;
      this.labelVersionString.Text = "Version";
      this.labelVersionString.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label3
      // 
      this.label3.BackColor = System.Drawing.Color.Transparent;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(20, 260);
      this.label3.Margin = new System.Windows.Forms.Padding(0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(220, 20);
      this.label3.TabIndex = 4;
      this.label3.Text = "SEO Macroscope";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelArchitectureString
      // 
      this.labelArchitectureString.BackColor = System.Drawing.Color.Transparent;
      this.labelArchitectureString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelArchitectureString.ForeColor = System.Drawing.Color.White;
      this.labelArchitectureString.Location = new System.Drawing.Point(20, 300);
      this.labelArchitectureString.Margin = new System.Windows.Forms.Padding(0);
      this.labelArchitectureString.Name = "labelArchitectureString";
      this.labelArchitectureString.Size = new System.Drawing.Size(220, 20);
      this.labelArchitectureString.TabIndex = 5;
      this.labelArchitectureString.Text = "Architecture";
      this.labelArchitectureString.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // MacroscopeAboutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.BackgroundImage = global::SEOMacroscope.Resources.SEO_Macroscope_About_Screen;
      this.ClientSize = new System.Drawing.Size(584, 361);
      this.Controls.Add(this.labelArchitectureString);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.labelVersionString);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = global::SEOMacroscope.Resources.SEO_Macroscope_Icon_32x32;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MacroscopeAboutForm";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "About SEO Macroscope";
      this.ResumeLayout(false);

		}
	}
}
