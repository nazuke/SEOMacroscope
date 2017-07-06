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
  /// Description of MacroscopeXpathPrefixes.
  /// </summary>

  public partial class MacroscopeXpathPrefixes : Form
  {

    /**************************************************************************/
    
    private string XpathPrefixesText;
    
    /**************************************************************************/

    public MacroscopeXpathPrefixes ( string PrefixesText )
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.XpathPrefixesText = PrefixesText;
      
      this.Shown += this.CallbackXpathPrefixesShown;

      this.textBoxXpathPrefixes.KeyUp += this.CallbackXpathPrefixesTextKeyUp;

    }

    /**************************************************************************/

    private void CallbackXpathPrefixesShown ( object sender, EventArgs e )
    {

      if( this.XpathPrefixesText.Length > 0 )
      {
        this.textBoxXpathPrefixes.Text = this.XpathPrefixesText;
      }

      this.textBoxXpathPrefixes.Focus();

    }

    /**************************************************************************/

    private void CallbackXpathPrefixesTextKeyUp ( object sender, KeyEventArgs e )
    {
      
      TextBox PrefixesTextBox = ( TextBox )sender;

      if( e.Control && ( e.KeyCode == Keys.A ) )
      {

        PrefixesTextBox.SelectAll();
        PrefixesTextBox.Focus();

      }

    }

    /**************************************************************************/

    public string GetPrefixesText ()
    {
      return( this.textBoxXpathPrefixes.Text );
    }

    /**************************************************************************/

  }

}
