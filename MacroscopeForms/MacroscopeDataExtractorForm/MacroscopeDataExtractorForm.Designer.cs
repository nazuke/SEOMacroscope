/*
 * Created by SharpDevelop.
 * User: jholland
 * Date: 6/5/2017
 * Time: 13:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SEOMacroscope
{
	partial class MacroscopeDataExtractorForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonClear;
		public System.Windows.Forms.TabControl tabControlDataExtractors;
		private System.Windows.Forms.TabPage tabPageRegexes;
		private System.Windows.Forms.TabPage tabPageXpaths;
		private System.Windows.Forms.TabPage tabPageCssSelectors;
		private System.Windows.Forms.Label label6;
		
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonClear = new System.Windows.Forms.Button();
			this.tabControlDataExtractors = new System.Windows.Forms.TabControl();
			this.tabPageRegexes = new System.Windows.Forms.TabPage();
			this.tabPageXpaths = new System.Windows.Forms.TabPage();
			this.tabPageCssSelectors = new System.Windows.Forms.TabPage();
			this.label6 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tabControlDataExtractors.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tabControlDataExtractors, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 562);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.buttonCancel);
			this.flowLayoutPanel1.Controls.Add(this.buttonOK);
			this.flowLayoutPanel1.Controls.Add(this.buttonClear);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 505);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(878, 54);
			this.flowLayoutPanel1.TabIndex = 1;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(780, 13);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(699, 13);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(21, 13);
			this.buttonClear.Margin = new System.Windows.Forms.Padding(3, 3, 600, 3);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(75, 23);
			this.buttonClear.TabIndex = 3;
			this.buttonClear.Text = "Clear";
			this.buttonClear.UseVisualStyleBackColor = true;
			// 
			// tabControlDataExtractors
			// 
			this.tabControlDataExtractors.Controls.Add(this.tabPageRegexes);
			this.tabControlDataExtractors.Controls.Add(this.tabPageXpaths);
			this.tabControlDataExtractors.Controls.Add(this.tabPageCssSelectors);
			this.tabControlDataExtractors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlDataExtractors.Location = new System.Drawing.Point(10, 60);
			this.tabControlDataExtractors.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.tabControlDataExtractors.Name = "tabControlDataExtractors";
			this.tabControlDataExtractors.SelectedIndex = 0;
			this.tabControlDataExtractors.Size = new System.Drawing.Size(864, 442);
			this.tabControlDataExtractors.TabIndex = 2;
			// 
			// tabPageRegexes
			// 
			this.tabPageRegexes.Location = new System.Drawing.Point(4, 22);
			this.tabPageRegexes.Name = "tabPageRegexes";
			this.tabPageRegexes.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRegexes.Size = new System.Drawing.Size(856, 416);
			this.tabPageRegexes.TabIndex = 0;
			this.tabPageRegexes.Text = "Regular Expressions";
			this.tabPageRegexes.UseVisualStyleBackColor = true;
			// 
			// tabPageXpaths
			// 
			this.tabPageXpaths.Location = new System.Drawing.Point(4, 22);
			this.tabPageXpaths.Name = "tabPageXpaths";
			this.tabPageXpaths.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageXpaths.Size = new System.Drawing.Size(856, 416);
			this.tabPageXpaths.TabIndex = 1;
			this.tabPageXpaths.Text = "XPaths";
			this.tabPageXpaths.UseVisualStyleBackColor = true;
			// 
			// tabPageCssSelectors
			// 
			this.tabPageCssSelectors.Location = new System.Drawing.Point(4, 22);
			this.tabPageCssSelectors.Name = "tabPageCssSelectors";
			this.tabPageCssSelectors.Size = new System.Drawing.Size(856, 416);
			this.tabPageCssSelectors.TabIndex = 2;
			this.tabPageCssSelectors.Text = "CSS Selectors";
			this.tabPageCssSelectors.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(10, 10);
			this.label6.Margin = new System.Windows.Forms.Padding(10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(864, 40);
			this.label6.TabIndex = 3;
			this.label6.Text = "Enter text strings to search for and extract from each HTML page.";
			// 
			// MacroscopeDataExtractorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 562);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = global::SEOMacroscope.Icons.MacroscopeIcon_32x32;
			this.Name = "MacroscopeDataExtractorForm";
			this.Text = "Data Extractor";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.tabControlDataExtractors.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
