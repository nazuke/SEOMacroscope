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
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Scan Progress Bar *****************************************************/

    private void StartProgressBarScanTimer ( int Delay )
    {
      this.TimerProgressBarScan.Interval = Delay;
      this.TimerProgressBarScan.Elapsed += this.CallbackProgressBarScanTimer;
      this.TimerProgressBarScan.AutoReset = true;
      this.TimerProgressBarScan.Enabled = true;
      this.TimerProgressBarScan.Start();
    }

    /**************************************************************************/
        
    private void StopProgressBarScanTimer ()
    {

      if ( this.IsDisposed )
      {
        return;
      }

      if ( this.TimerProgressBarScan != null )
      {
        try
        {
          this.TimerProgressBarScan.Stop();
        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "StopProgressBarScanTimer: {0}", ex.Message ) );
        }
      }
      if( this.InvokeRequired )
      {
        this.Invoke(
          new MethodInvoker (
            delegate
            {
              this.ProgressBarScan.Value = 0;    
            }
          )
        );
      }
      else
      {
        this.ProgressBarScan.Value = 0;    
      }
    }

    /**************************************************************************/
            
    private void CallbackProgressBarScanTimer ( Object self, ElapsedEventArgs e )
    {

      if ( this.IsDisposed )
      {
        return;
      }

      if ( Monitor.TryEnter( LockerTimerProgressBarScan, 1000 ) )
      {

        try
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
        catch( Exception ex )
        {
          DebugMsg( string.Format( "CallbackProgressBarScanTimer1: {0}", ex.Message ) );
          DebugMsg( string.Format( "CallbackProgressBarScanTimer2: {0}", ex.StackTrace ) );
        }
        finally
        {
          Monitor.Exit( LockerTimerProgressBarScan );
        }

      }
            
    }
    
    /**************************************************************************/

    private void UpdateProgressBarScan ( int Percentage )
    {

      if ( this.IsDisposed )
      {
        return;
      }

      if ( this.InvokeRequired )
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

      int Percentage = 0;

      if ( this.IsDisposed )
      {
        return;
      }

      if ( this.JobMaster != null )
      {

        List<decimal> Counts = this.JobMaster.GetProgress();
        decimal Total = Counts[ 0 ];
        decimal Processed = Counts[ 1 ];
        decimal Queued = Counts[ 2 ];
        
        if( Total > 0 )
        {
          Percentage = ( int )( ( 100 / Total ) * Processed );
        }
        else
        {
          Percentage = 0;
        }

        if( Percentage < 0 )
        {
          Percentage = 0;
        }
        else
        if( Percentage > 100 )
        {
          Percentage = 100;
        }

      }

      this.TimerProgressBarScan.Stop();

      try
      {
        this.ProgressBarScan.Value = Percentage;
      }
      catch ( Exception ex )
      {
        DebugMsg( string.Format( "UpdateProgressBarScan: {0}", ex.Message ) );
      }

      this.TimerProgressBarScan.Start();
      
    }

    /**************************************************************************/

  }

}
