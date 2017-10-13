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
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDataExtractorXpathsForm.
  /// </summary>

  public partial class MacroscopeDataExtractorXpathsForm : Form
  {

    /**************************************************************************/

    public MacroscopeDataExtractorXpathsForm ( MacroscopeDataExtractorXpaths NewDataExtractor )
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.dataExtractorInstance.ConfigureDataExtractorForm(
        NewContainerForm: this,
        NewDataExtractor: NewDataExtractor
      );

      this.dataExtractorInstance.SetDataExtractor();

      this.FormClosing += this.CallbackFormClosing;
      this.buttonClear.Click += this.ClearDataExtractorForm;
      this.buttonCancel.Click += this.CloseDataExtractorForm;

    }

    /**************************************************************************/

    public MacroscopeDataExtractorXpaths GetDataExtractor ()
    {
      return ( this.dataExtractorInstance.GetDataExtractor() );
    }

    /**************************************************************************/

    public void DisableButtonOk ()
    {
      this.buttonOK.Enabled = false;
    }

    /** -------------------------------------------------------------------- **/

    public void EnableButtonOk ()
    {
      this.buttonOK.Enabled = true;
    }

    /**************************************************************************/

    private void CallbackFormClosing ( object sender, FormClosingEventArgs e )
    {
      if( !this.dataExtractorInstance.ValidateForm( ShowErrorDialogue: true ) )
      {
        e.Cancel = true;
      }
    }

    /**************************************************************************/

    public void ClearDataExtractorForm ( object sender, EventArgs e )
    {
      this.dataExtractorInstance.ClearDataExtractorForm();
      this.EnableButtonOk();
    }

    /**************************************************************************/

    public void CloseDataExtractorForm ( object sender, EventArgs e )
    {
      this.FormClosing -= this.CallbackFormClosing;
      this.Close();
    }

    /**************************************************************************/

  }

}
