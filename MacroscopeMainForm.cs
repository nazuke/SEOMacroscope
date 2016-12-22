using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SEOMacroscope
{

	public partial class MacroscopeMainForm : Form
	{
		
		/**************************************************************************/
				
		/**************************************************************************/

		public MacroscopeMainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		/**************************************************************************/
				
		void CallbackFileExit(object sender, EventArgs e)
		{
			Program.Exit();
		}

		/**************************************************************************/
				
		void CallbackScanStart(object sender, EventArgs e)
		{
			this.UpdateDisplayStructure(); //dummy
		}

		/**************************************************************************/
				
		void CallbackScanReset(object sender, EventArgs e)
		{

					
		}

		/**************************************************************************/
		
		/**************************************************************************/
		
		
		void UpdateDisplayStructure()
		{
			
			DataGrid dg = this.dataGridStructure;
			DataSet ds = new DataSet();
	


			DataTable dt = new DataTable("Overview");
			
			DataColumn dcUrl = new DataColumn("URL", typeof(string));
			
			dt.Columns.Add(dcUrl);

			ds.Tables.Add(dt);

			DataRow dtRow;

			for (int i = 0; i < 10; i++) {
				dtRow = dt.NewRow();
				dtRow["URL"] = "bongo";
				dt.Rows.Add(dtRow);
			}

			//dg.SetDataBinding(ds, "Structure");

		}
		
		
	}
}
