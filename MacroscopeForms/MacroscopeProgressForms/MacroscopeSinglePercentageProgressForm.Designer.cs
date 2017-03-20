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
  partial class MacroscopeSinglePercentageProgressForm
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    public System.Windows.Forms.Label labelMessage;
    public System.Windows.Forms.ProgressBar progressBarMajor;
    public System.Windows.Forms.Label labelProgressLabelMajor;
		
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
    	this.labelProgressLabelMajor = new System.Windows.Forms.Label();
    	this.SuspendLayout();
    	// 
    	// labelMessage
    	// 
    	this.labelMessage.Location = new System.Drawing.Point(15, 15);
    	this.labelMessage.Name = "labelMessage";
    	this.labelMessage.Size = new System.Drawing.Size(450, 23);
    	this.labelMessage.TabIndex = 0;
    	this.labelMessage.Text = "Message";
    	// 
    	// progressBarMajor
    	// 
    	this.progressBarMajor.Location = new System.Drawing.Point(15, 50);
    	this.progressBarMajor.Name = "progressBarMajor";
    	this.progressBarMajor.Size = new System.Drawing.Size(310, 23);
    	this.progressBarMajor.TabIndex = 1;
    	// 
    	// labelProgressLabelMajor
    	// 
    	this.labelProgressLabelMajor.Location = new System.Drawing.Point(341, 50);
    	this.labelProgressLabelMajor.Name = "labelProgressLabelMajor";
    	this.labelProgressLabelMajor.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
    	this.labelProgressLabelMajor.Size = new System.Drawing.Size(124, 23);
    	this.labelProgressLabelMajor.TabIndex = 3;
    	this.labelProgressLabelMajor.Text = "labelProgressLabelMajor";
    	// 
    	// MacroscopeSinglePercentageProgressForm
    	// 
    	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    	this.ClientSize = new System.Drawing.Size(484, 111);
    	this.Controls.Add(this.labelProgressLabelMajor);
    	this.Controls.Add(this.progressBarMajor);
    	this.Controls.Add(this.labelMessage);
    	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
    	this.Icon = global::SEOMacroscope.Icons.MacroscopeIcon_32x32;
    	this.MaximizeBox = false;
    	this.MinimizeBox = false;
    	this.Name = "MacroscopeSinglePercentageProgressForm";
    	this.Text = "Processing";
    	this.ResumeLayout(false);

    }
  }
}
