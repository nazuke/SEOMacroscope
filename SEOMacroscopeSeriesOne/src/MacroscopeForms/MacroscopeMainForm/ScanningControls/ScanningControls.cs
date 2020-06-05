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

    /** Scanning Controls *****************************************************/

    private void ScanningControlsConfigure ()
    {

      if( this.IsDisposed )
      {
        return;
      }

#if DEBUG
      this.toolStripButtonRecalculateClickPaths.Visible = true;
#else
      this.toolStripButtonRecalculateClickPaths.Visible = false;
#endif
    }

    /** -------------------------------------------------------------------- **/

    private void ScanningControlsButtonText ()
    {

      if( this.IsDisposed )
      {
        return;
      }

      if( this.GetJobMaster().PeekUrlQueue() )
      {
        this.ButtonStart.Text = "Continue";
      }
      else
      {
        this.ButtonStart.Text = "Start";
      }

    }

    /** -------------------------------------------------------------------- **/

    private void ScanningControlsEnable ()
    {

      if( this.IsDisposed )
      {
        return;
      }

      this.recentURLsToolStripMenuItem.Enabled = true;
      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.loadSessionToolStripMenuItem.Enabled = true;
      this.saveSessionAsToolStripMenuItem.Enabled = true;
      this.exportToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.presetsToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.ProgressBarScan.Visible = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;
      this.toolStripButtonRetryTimedOutLinks.Enabled = true;

      if( MacroscopePreferencesManager.GetAnalyzeClickPaths() )
      {
        this.toolStripButtonRecalculateClickPaths.Enabled = true;
      }
      else
      {
        this.toolStripButtonRecalculateClickPaths.Enabled = false;
      }

      this.ReconfigureFileMenu();
      this.ReconfigureReportsMenu();

      this.ScanningControlsButtonText();

      this.ReconfigureStructureOverviewControls();
      this.SetStructureOverviewControlsToEnabled();

      this.ReconfigureSearchCollectionControls();

    }

    /** -------------------------------------------------------------------- **/

    private void ScanningControlsStart ()
    {

      if( this.IsDisposed )
      {
        return;
      }

      this.recentURLsToolStripMenuItem.Enabled = false;
      this.loadUrlListToolStripMenuItem.Enabled = false;
      this.loadSessionToolStripMenuItem.Enabled = false;
      this.saveSessionAsToolStripMenuItem.Enabled = false;
      this.exportToolStripMenuItem.Enabled = false;
      this.taskParametersToolStripMenuItem.Enabled = false;
      this.presetsToolStripMenuItem.Enabled = false;
      this.reportsToolStripMenuItem.Enabled = false;

      this.textBoxStartUrl.Enabled = false;
      this.ButtonStart.Enabled = false;
      this.ButtonStop.Enabled = true;
      this.ButtonReset.Enabled = false;

      this.ProgressBarScan.Visible = true;

      this.toolStripButtonRetryBrokenLinks.Enabled = false;
      this.toolStripButtonRetryTimedOutLinks.Enabled = false;
      this.toolStripButtonRecalculateClickPaths.Enabled = false;

      this.ReconfigureFileMenu();
      this.ReconfigureReportsMenu();

      this.ScanningControlsButtonText();

      this.ReconfigureStructureOverviewControls();
      this.SetStructureOverviewControlsToScanning();

      this.ReconfigureSearchCollectionControls();

    }

    /** -------------------------------------------------------------------- **/

    private void ScanningControlsStopping ()
    {

      if( this.IsDisposed )
      {
        return;
      }

      this.recentURLsToolStripMenuItem.Enabled = false;
      this.loadUrlListToolStripMenuItem.Enabled = false;
      this.loadSessionToolStripMenuItem.Enabled = false;
      this.saveSessionAsToolStripMenuItem.Enabled = false;
      this.exportToolStripMenuItem.Enabled = false;
      this.taskParametersToolStripMenuItem.Enabled = false;
      this.presetsToolStripMenuItem.Enabled = false;
      this.reportsToolStripMenuItem.Enabled = false;

      this.textBoxStartUrl.Enabled = false;
      this.ButtonStart.Enabled = false;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.ProgressBarScan.Visible = true;

      this.toolStripButtonRetryBrokenLinks.Enabled = false;
      this.toolStripButtonRetryTimedOutLinks.Enabled = false;
      this.toolStripButtonRecalculateClickPaths.Enabled = false;

      this.ReconfigureFileMenu();
      this.ReconfigureReportsMenu();

      this.ScanningControlsButtonText();

      this.ReconfigureStructureOverviewControls();
      this.SetStructureOverviewControlsToEnabled();

      this.ReconfigureSearchCollectionControls();

    }

    /** -------------------------------------------------------------------- **/

    private void ScanningControlsStopped ()
    {

      if( this.IsDisposed )
      {
        return;
      }

      this.recentURLsToolStripMenuItem.Enabled = true;
      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.loadSessionToolStripMenuItem.Enabled = true;
      this.saveSessionAsToolStripMenuItem.Enabled = true;
      this.exportToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.presetsToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = true;

      this.ProgressBarScan.Visible = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;
      this.toolStripButtonRetryTimedOutLinks.Enabled = false;

      if( MacroscopePreferencesManager.GetAnalyzeClickPaths() )
      {
        this.toolStripButtonRecalculateClickPaths.Enabled = true;
      }
      else
      {
        this.toolStripButtonRecalculateClickPaths.Enabled = false;
      }

      this.UpdateProgressBarScan( 0 );

      this.ReconfigureFileMenu();
      this.ReconfigureReportsMenu();

      this.ScanningControlsButtonText();

      this.ReconfigureStructureOverviewControls();
      this.SetStructureOverviewControlsToEnabled();

      this.ReconfigureSearchCollectionControls();

    }

    /** -------------------------------------------------------------------- **/

    private void ScanningControlsReset ()
    {

      if( this.IsDisposed )
      {
        return;
      }

      this.recentURLsToolStripMenuItem.Enabled = true;
      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.loadSessionToolStripMenuItem.Enabled = true;
      this.saveSessionAsToolStripMenuItem.Enabled = true;
      this.exportToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.presetsToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = false;

      this.ProgressBarScan.Visible = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;
      this.toolStripButtonRetryTimedOutLinks.Enabled = true;

      if( MacroscopePreferencesManager.GetAnalyzeClickPaths() )
      {
        this.toolStripButtonRecalculateClickPaths.Enabled = true;
      }
      else
      {
        this.toolStripButtonRecalculateClickPaths.Enabled = false;
      }

      this.UpdateProgressBarScan( 0 );

      this.ReconfigureFileMenu();
      this.ReconfigureReportsMenu();

      this.ScanningControlsButtonText();

      this.ReconfigureStructureOverviewControls();
      this.SetStructureOverviewControlsToEnabled();

      this.ReconfigureSearchCollectionControls();

    }

    /** -------------------------------------------------------------------- **/

    private void ScanningControlsComplete ()
    {

      if( this.IsDisposed )
      {
        return;
      }

      this.recentURLsToolStripMenuItem.Enabled = true;
      this.loadUrlListToolStripMenuItem.Enabled = true;
      this.loadSessionToolStripMenuItem.Enabled = true;
      this.saveSessionAsToolStripMenuItem.Enabled = true;
      this.exportToolStripMenuItem.Enabled = true;
      this.taskParametersToolStripMenuItem.Enabled = true;
      this.presetsToolStripMenuItem.Enabled = true;
      this.reportsToolStripMenuItem.Enabled = true;

      this.textBoxStartUrl.Enabled = true;
      this.ButtonStart.Enabled = true;
      this.ButtonStop.Enabled = false;
      this.ButtonReset.Enabled = true;

      this.ProgressBarScan.Visible = false;

      this.toolStripButtonRetryBrokenLinks.Enabled = true;
      this.toolStripButtonRetryTimedOutLinks.Enabled = true;

      if( MacroscopePreferencesManager.GetAnalyzeClickPaths() )
      {
        this.toolStripButtonRecalculateClickPaths.Enabled = true;
      }
      else
      {
        this.toolStripButtonRecalculateClickPaths.Enabled = false;
      }

      this.ReconfigureFileMenu();
      this.ReconfigureReportsMenu();

      this.ScanningControlsButtonText();

      this.ReconfigureStructureOverviewControls();
      this.SetStructureOverviewControlsToEnabled();

      this.ReconfigureSearchCollectionControls();

    }

    /**************************************************************************/

  }

}
