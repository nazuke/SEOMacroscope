/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

  The GitHub repository may be found at:

    https://github.com/nazuke/SEOMacroscope

  SEOMacroscope is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  SEOMacroscope is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeTriplePercentageProgressForm.
  /// </summary>

  public partial class MacroscopeTriplePercentageProgressForm : Form, IMacroscopeProgressForm
  {

    /**************************************************************************/

    private MacroscopeMainForm MainForm;
    private bool IsCancelled;

    private bool FormShown;

    private Stopwatch OperationDuration;
    private static long OperationDurationLimit = 5000;

    /**************************************************************************/

    public MacroscopeTriplePercentageProgressForm ( MacroscopeMainForm MainForm )
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.MainForm = MainForm;
      this.IsCancelled = false;
      this.FormShown = false;

      this.Shown += this.CallbackFormShown;
      this.FormClosing += this.CallbackFormClosing;
      this.KeyUp += this.CallbackKeyUp;

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
      this.MainForm.Enabled = true;
    }

    /**************************************************************************/

    private void CallbackKeyUp ( object sender, KeyEventArgs e )
    {
      this.DebugMsg( string.Format( "CallbackKeyUp: {0}", "CALLED" ) );
      switch( e.KeyCode )
      {
        case Keys.Escape:
          this.DebugMsg( string.Format( "CallbackKeyUp: {0}", "Escape" ) );
          this.Cancel();
          break;
        default:
          break;
      }
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
      this.DebugMsg( "SINGLE" );
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
      this.DebugMsg( "DOUBLE" );
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
      string ProgressLabelSubMinor
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
          this.Refresh();
        }

        if( Message != null )
        {
          this.labelMessage.Text = Message;
          this.labelMessage.Refresh();
        }

        if( MajorPercentage >= 0 )
        {
          this.progressBarMajor.Value = (int) MajorPercentage;
          this.progressBarMajor.Refresh();
        }

        if( ProgressLabelMajor != null )
        {
          this.labelProgressLabelMajor.Text = ProgressLabelMajor;
          this.labelProgressLabelMajor.Refresh();
        }

        if( MinorPercentage >= 0 )
        {
          this.progressBarMinor.Value = (int) MinorPercentage;
          this.progressBarMinor.Refresh();
        }

        if( ProgressLabelMinor != null )
        {
          this.labelProgressLabelMinor.Text = ProgressLabelMinor;
          this.labelProgressLabelMinor.Refresh();
        }

        if( SubMinorPercentage >= 0 )
        {
          this.progressBarSubMinor.Value = (int) SubMinorPercentage;
          this.progressBarSubMinor.Refresh();
        }

        if( ProgressLabelSubMinor != null )
        {
          this.labelProgressLabelSubMinor.Text = ProgressLabelSubMinor;
          this.labelProgressLabelSubMinor.Refresh();
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

    public void Reset ()
    {
      this.Text = "Processing";
      this.labelMessage.Text = "";
      this.progressBarMajor.Value = 0;
      this.labelProgressLabelMajor.Text = "";
      this.progressBarMinor.Value = 0;
      this.labelProgressLabelMinor.Text = "";
      this.progressBarSubMinor.Value = 0;
      this.labelProgressLabelSubMinor.Text = "";
    }

    /**************************************************************************/

    public void Cancel ()
    {
      DebugMsg( string.Format( "MacroscopeTriplePercentageProgressForm: CANCELLED" ) );
      this.IsCancelled = true;
    }

    /**************************************************************************/

    public bool Cancelled ()
    {
      return ( this.IsCancelled );
    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    private void DebugMsg ( string sMsg )
    {
      Debug.WriteLine( string.Format( "TID:{0} :: {1}", this.GetType(), sMsg ) );
    }

    /**************************************************************************/

  }

}
