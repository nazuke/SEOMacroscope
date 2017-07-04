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
  /// Description of MacroscopeDataExtractorRegexesForm.
  /// </summary>
	
  public partial class MacroscopeDataExtractorRegexesForm : Form
  {

    /**************************************************************************/

    public MacroscopeDataExtractorRegexesForm ( MacroscopeDataExtractorRegexes NewDataExtractor )
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.
			     
      this.dataExtractorInstance.ConfigureDataExtractorForm(
        NewContainerForm: this,
        NewDataExtractor: NewDataExtractor
      );

      this.dataExtractorInstance.SetDataExtractor();

      this.buttonClear.Click += ClearDataExtractorForm;

      this.FormClosing += CallbackFormClosing;
            
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
      
      Boolean IsValid = false;
      
      IsValid = this.dataExtractorInstance.ValidateForm( ShowErrorDialogue: true );

      if( !IsValid )
      {
        e.Cancel = true;
      }
      
      return;
      
    }

    /**************************************************************************/

    public MacroscopeDataExtractorRegexes GetDataExtractor ()
    {
      return( this.dataExtractorInstance.GetDataExtractor() );
    }

    /**************************************************************************/

    public void ClearDataExtractorForm ( object sender, EventArgs e )
    {
      this.dataExtractorInstance.ClearDataExtractorForm();
    }

    /**************************************************************************/

  }
	
}
