/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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
	partial class MacroscopeGetCredentialsHttp
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		public System.Windows.Forms.Button buttonOK;
		public System.Windows.Forms.Button buttonCancel;
		public System.Windows.Forms.Label labelMessage;
		public System.Windows.Forms.TextBox textBoxUsername;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.MaskedTextBox maskedTextBoxPassword;
		
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
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.labelMessage = new System.Windows.Forms.Label();
      this.textBoxUsername = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.maskedTextBoxPassword = new System.Windows.Forms.MaskedTextBox();
      this.SuspendLayout();
      // 
      // buttonOK
      // 
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOK.Location = new System.Drawing.Point(290, 205);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(75, 23);
      this.buttonOK.TabIndex = 0;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.buttonCancel.Location = new System.Drawing.Point(385, 205);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      // 
      // labelMessage
      // 
      this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.labelMessage.Location = new System.Drawing.Point(12, 18);
      this.labelMessage.Name = "labelMessage";
      this.labelMessage.Size = new System.Drawing.Size(450, 50);
      this.labelMessage.TabIndex = 2;
      this.labelMessage.Text = "Message";
      // 
      // textBoxUsername
      // 
      this.textBoxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.textBoxUsername.Location = new System.Drawing.Point(120, 81);
      this.textBoxUsername.Name = "textBoxUsername";
      this.textBoxUsername.Size = new System.Drawing.Size(340, 23);
      this.textBoxUsername.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.label1.Location = new System.Drawing.Point(12, 80);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 24);
      this.label1.TabIndex = 4;
      this.label1.Text = "User Name:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.label2.Location = new System.Drawing.Point(12, 120);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 25);
      this.label2.TabIndex = 5;
      this.label2.Text = "Password:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // maskedTextBoxPassword
      // 
      this.maskedTextBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.maskedTextBoxPassword.Location = new System.Drawing.Point(120, 121);
      this.maskedTextBoxPassword.Name = "maskedTextBoxPassword";
      this.maskedTextBoxPassword.PasswordChar = '*';
      this.maskedTextBoxPassword.Size = new System.Drawing.Size(340, 23);
      this.maskedTextBoxPassword.TabIndex = 7;
      // 
      // MacroscopeGetCredentialsHttp
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(484, 241);
      this.ControlBox = false;
      this.Controls.Add(this.maskedTextBoxPassword);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxUsername);
      this.Controls.Add(this.labelMessage);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = global::SEOMacroscope.Resources.SEO_Macroscope_Icon_32x32;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MacroscopeGetCredentialsHttp";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SEO Macroscope: Authentication Required";
      this.ResumeLayout(false);
      this.PerformLayout();

		}
	}
}
