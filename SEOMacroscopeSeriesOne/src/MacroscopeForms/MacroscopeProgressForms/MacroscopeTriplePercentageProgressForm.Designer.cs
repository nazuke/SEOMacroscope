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
  partial class MacroscopeTriplePercentageProgressForm
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    public System.Windows.Forms.Label labelMessage;
    public System.Windows.Forms.ProgressBar progressBarMajor;
    public System.Windows.Forms.ProgressBar progressBarMinor;
    public System.Windows.Forms.Label labelProgressLabelMajor;
    public System.Windows.Forms.Label labelProgressLabelMinor;
    public System.Windows.Forms.Label labelProgressLabelSubMinor;
    public System.Windows.Forms.ProgressBar progressBarSubMinor;
		
    /// <summary>
    /// Disposes resources used by the form.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose ( bool disposing )
    {
      if( disposing )
      {
        if( components != null )
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }
		
    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent ()
    {
      this.labelMessage = new System.Windows.Forms.Label();
      this.progressBarMajor = new System.Windows.Forms.ProgressBar();
      this.progressBarMinor = new System.Windows.Forms.ProgressBar();
      this.labelProgressLabelMajor = new System.Windows.Forms.Label();
      this.labelProgressLabelMinor = new System.Windows.Forms.Label();
      this.labelProgressLabelSubMinor = new System.Windows.Forms.Label();
      this.progressBarSubMinor = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      // 
      // labelMessage
      // 
      this.labelMessage.Location = new System.Drawing.Point(15, 15);
      this.labelMessage.Name = "labelMessage";
      this.labelMessage.Size = new System.Drawing.Size(550, 23);
      this.labelMessage.TabIndex = 0;
      this.labelMessage.Text = "Message";
      // 
      // progressBarMajor
      // 
      this.progressBarMajor.Location = new System.Drawing.Point(15, 80);
      this.progressBarMajor.Name = "progressBarMajor";
      this.progressBarMajor.Size = new System.Drawing.Size(550, 23);
      this.progressBarMajor.TabIndex = 1;
      // 
      // progressBarMinor
      // 
      this.progressBarMinor.Location = new System.Drawing.Point(12, 160);
      this.progressBarMinor.Name = "progressBarMinor";
      this.progressBarMinor.Size = new System.Drawing.Size(553, 23);
      this.progressBarMinor.TabIndex = 2;
      // 
      // labelProgressLabelMajor
      // 
      this.labelProgressLabelMajor.Location = new System.Drawing.Point(15, 50);
      this.labelProgressLabelMajor.Name = "labelProgressLabelMajor";
      this.labelProgressLabelMajor.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
      this.labelProgressLabelMajor.Size = new System.Drawing.Size(550, 23);
      this.labelProgressLabelMajor.TabIndex = 3;
      this.labelProgressLabelMajor.Text = "labelProgressLabelMajor";
      // 
      // labelProgressLabelMinor
      // 
      this.labelProgressLabelMinor.Location = new System.Drawing.Point(15, 130);
      this.labelProgressLabelMinor.Name = "labelProgressLabelMinor";
      this.labelProgressLabelMinor.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
      this.labelProgressLabelMinor.Size = new System.Drawing.Size(550, 23);
      this.labelProgressLabelMinor.TabIndex = 4;
      this.labelProgressLabelMinor.Text = "labelProgressLabelMinor";
      // 
      // labelProgressLabelSubMinor
      // 
      this.labelProgressLabelSubMinor.Location = new System.Drawing.Point(15, 210);
      this.labelProgressLabelSubMinor.Name = "labelProgressLabelSubMinor";
      this.labelProgressLabelSubMinor.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
      this.labelProgressLabelSubMinor.Size = new System.Drawing.Size(550, 23);
      this.labelProgressLabelSubMinor.TabIndex = 6;
      this.labelProgressLabelSubMinor.Text = "labelProgressLabelSubMinor";
      // 
      // progressBarSubMinor
      // 
      this.progressBarSubMinor.Location = new System.Drawing.Point(15, 240);
      this.progressBarSubMinor.Name = "progressBarSubMinor";
      this.progressBarSubMinor.Size = new System.Drawing.Size(550, 23);
      this.progressBarSubMinor.TabIndex = 5;
      // 
      // MacroscopeTriplePercentageProgressForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 312);
      this.Controls.Add(this.labelProgressLabelSubMinor);
      this.Controls.Add(this.progressBarSubMinor);
      this.Controls.Add(this.labelProgressLabelMinor);
      this.Controls.Add(this.labelProgressLabelMajor);
      this.Controls.Add(this.progressBarMinor);
      this.Controls.Add(this.progressBarMajor);
      this.Controls.Add(this.labelMessage);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = global::SEOMacroscope.Resources.SEO_Macroscope_Icon_32x32;
      this.MaximizeBox = false;
      this.Name = "MacroscopeTriplePercentageProgressForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Processing";
      this.TopMost = true;
      this.ResumeLayout(false);

    }
  }
}
