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

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeSinglePercentageProgressForm.
  /// </summary>

  public partial class MacroscopeSinglePercentageProgressForm : Form, IMacroscopeProgressForm
  {

    /**************************************************************************/

    private MacroscopeMainForm MainForm;
    private bool IsCancelled;

    private bool FormShown;

    private Stopwatch OperationDuration;
    private static long OperationDurationLimit = 5000;

    /**************************************************************************/

    public MacroscopeSinglePercentageProgressForm ( MacroscopeMainForm MainForm )
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.MainForm = MainForm;
      this.IsCancelled = false;
      this.FormShown = false;

      this.Shown += this.CallbackFormShown;
      this.FormClosing += this.CallbackFormClosing;

      this.OperationDuration = new Stopwatch();
      this.OperationDuration.Start();

    }

    /**************************************************************************/

    private void CallbackFormShown ( object sender, EventArgs e )
    {
      this.MainForm.Enabled = false;
    }

    private void CallbackFormClosing ( object sender, FormClosingEventArgs e )
    {
      this.Cancel();
      this.OperationDuration.Stop();
      this.MainForm.Enabled = true;
    }

    /**************************************************************************/

    public void DoClose ()
    {
      if( this.FormShown )
      {
        this.Close();
      }
    }

    /**************************************************************************/

    public void UpdatePercentages (
      string Title,
      string Message,
      decimal MajorPercentage,
      string ProgressLabelMajor
    )
    {

      if( ( !this.FormShown ) && ( this.OperationDuration.ElapsedMilliseconds >= OperationDurationLimit ) )
      {
        this.FormShown = true;
        this.TopLevel = true;
        this.ShowInTaskbar = true;
        this.Show();
        this.Activate();
      }

      try
      {

        if( Title != null )
        {
          this.Text = Title;
        }

        if( Message != null )
        {
          this.labelMessage.Text = Message;
        }

        if( MajorPercentage < 0 )
        {
          this.progressBarMajor.Value = 0;
        }

        if( MajorPercentage > 100 )
        {
          this.progressBarMajor.Value = 100;
        }

        if( MajorPercentage >= 0 )
        {
          this.progressBarMajor.Value = (int) MajorPercentage;
        }

        if( ProgressLabelMajor != null )
        {
          this.labelProgressLabelMajor.Text = ProgressLabelMajor;
        }

      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
      }

      this.Refresh();

      Application.DoEvents();

    }

    /**************************************************************************/

    public void UpdatePercentages (
      string Title,
      string Message,
      decimal MajorPercentage,
      string ProgressLabelMajor,
      decimal MinorPercentage,
      string ProgressLabelMinor
    )
    {
    }

    /**************************************************************************/

    public void UpdatePercentages (
      string Title,
      string Message,
      decimal MajorPercentage,
      string ProgressLabelMajor,
      decimal MinorPercentage,
      string ProgressLabelMinor,
      decimal SubMinorPercentage,
      string SubProgressLabelMinor
    )
    {
    }

    /**************************************************************************/

    public void Reset ()
    {
      this.Text = "Processing";
      this.labelMessage.Text = "";
      this.progressBarMajor.Value = 0;
      this.labelProgressLabelMajor.Text = "";
    }

    /**************************************************************************/

    public void Cancel ()
    {
      DebugMsg( string.Format( "MacroscopeSinglePercentageProgressForm: CANCELLED" ) );
      this.IsCancelled = true;
    }

    /**************************************************************************/

    public bool Cancelled ()
    {
      return ( this.IsCancelled );
    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    private void DebugMsg ( string Msg )
    {
      Debug.WriteLine( string.Format( "TID:{0} :: {1}", this.GetType(), Msg ) );
    }

    /**************************************************************************/

  }

}
