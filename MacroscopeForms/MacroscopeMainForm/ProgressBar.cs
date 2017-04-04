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

using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Scan Progress Bar *****************************************************/

    private void StartProgressBarScanTimer ( int Delay )
    {
      this.TimerProgressBarScan = new System.Timers.Timer ( Delay );
      this.TimerProgressBarScan.Elapsed += this.CallbackProgressBarScanTimer;
      this.TimerProgressBarScan.AutoReset = true;
      this.TimerProgressBarScan.Enabled = true;
      this.TimerProgressBarScan.Start();
    }

    /**************************************************************************/
        
    private void StopProgressBarScanTimer ()
    {
      try
      {
        this.TimerProgressBarScan.Stop();
        this.TimerProgressBarScan.Dispose();
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "StopProgressBarScanTimer: {0}", ex.Message ) );
      }
    }

    /**************************************************************************/
            
    private void CallbackProgressBarScanTimer ( Object self, ElapsedEventArgs e )
    {
      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker (
            delegate
            {
              this.UpdateProgressBarScan();
            }
          )
        );
      }
      else
      {
        this.UpdateProgressBarScan();
      }
    }
    
    /**************************************************************************/

    private void UpdateProgressBarScan ( int Percentage )
    {
      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker (
            delegate
            {
              this.ProgressBarScan.Value = Percentage;    
            }
          )
        );
      }
      else
      {
        this.ProgressBarScan.Value = Percentage;    
      }
    }
    
    /**************************************************************************/
        
    private void UpdateProgressBarScan ()
    {

      int iPercentage = 0;

      if( this.JobMaster != null )
      {

        List<decimal> Counts = this.JobMaster.GetProgress();
        decimal iTotal = Counts[ 0 ];
        decimal iProcessed = Counts[ 1 ];
        decimal iQueued = Counts[ 2 ];
        iPercentage = ( int )( ( 100 / iTotal ) * iProcessed );

        if( iPercentage < 0 )
        {
          iPercentage = 0;
        }
        else
        if( iPercentage > 100 )
        {
          iPercentage = 100;
        }

        //DebugMsg( string.Format( "ProgressBarScan: iTotal {0}", iTotal ) );
        //DebugMsg( string.Format( "ProgressBarScan: iProcessed {0}", iProcessed ) );
        //DebugMsg( string.Format( "ProgressBarScan: iQueued {0}", iQueued ) );
        //DebugMsg( string.Format( "ProgressBarScan: iPercentage {0}", iPercentage ) );

      }

      //DebugMsg( string.Format( "ProgressBarScan: {0}", this.ProgressBarScan.Value ) );

      this.ProgressBarScan.Value = iPercentage;

    }

    /**************************************************************************/

  }

}
