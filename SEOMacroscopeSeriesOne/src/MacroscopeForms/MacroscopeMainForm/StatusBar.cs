/*

  This file is part of SEOMacroscope.

  Copyright 2020 Jason Holland.

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
using System.Timers;
using System.Windows.Forms;
using System.Threading;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Status Bar ************************************************************/

    private void StartStatusBarTimer ( int Delay )
    {
      this.TimerStatusBar.Interval = Delay;
      this.TimerStatusBar.Elapsed += this.CallbackStatusBarTimer;
      this.TimerStatusBar.AutoReset = true;
      this.TimerStatusBar.Enabled = true;
      this.TimerStatusBar.Start();
    }

    /**************************************************************************/

    private void StopStatusBarTimer ()
    {
      if ( this.TimerStatusBar != null )
      {
        try
        {
          this.TimerStatusBar.Stop();
        }
        catch ( Exception ex )
        {
          DebugMsg( string.Format( "StopStatusBarTimer: {0}", ex.Message ) );
        }
      }
    }

    /**************************************************************************/

    private void CallbackStatusBarTimer ( Object self, ElapsedEventArgs e )
    {

      if ( this.IsDisposed )
      {
        return;
      }

      if ( Monitor.TryEnter( LockerTimerStatusBar, 1000 ) )
      {

        try
        {

          if ( this.InvokeRequired )
          {
            this.Invoke(
              new MethodInvoker(
                delegate
                {
                  this.UpdateStatusBar();
                }
              )
            );
          }
          else
          {
            this.UpdateStatusBar();
          }

        }
        catch ( Exception ex )
        {
          DebugMsg( string.Format( "CallbackStatusBarTimer: {0}", ex.Message ) );
        }
        finally
        {
          Monitor.Exit( LockerTimerStatusBar );
        }

      }

    }

    /**************************************************************************/

    private void UpdateStatusBar ()
    {

      if ( this.JobMaster != null )
      {

        this.TimerStatusBar.Stop();

        this.toolStripUrlCount.Text = string.Format( "URLs in Queue: {0}", this.JobMaster.CountUrlQueueItems() );
        this.toolStripFound.Text = string.Format( "URLs Crawled: {0}", this.JobMaster.GetDocCollection().CountDocuments() );

        this.TimerStatusBar.Start();

      }

    }

    /**************************************************************************/

  }

}
