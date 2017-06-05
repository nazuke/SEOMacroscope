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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDataExtractorPanel.
  /// </summary>

  public partial class MacroscopeDataExtractorPanel : UserControl
  {

    /**************************************************************************/

    MacroscopeCustomFilters CustomFilter;

    /**************************************************************************/
	      
    public MacroscopeDataExtractorPanel ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.textBoxFilter1.KeyUp += this.CallbackTextBoxKeyUp;
      this.textBoxFilter2.KeyUp += this.CallbackTextBoxKeyUp;
      this.textBoxFilter3.KeyUp += this.CallbackTextBoxKeyUp;
      this.textBoxFilter4.KeyUp += this.CallbackTextBoxKeyUp;
      this.textBoxFilter5.KeyUp += this.CallbackTextBoxKeyUp;
      
    }

    /**************************************************************************/
    
    public void SetCustomFilter ( MacroscopeCustomFilters NewCustomFilter )
    {

      this.CustomFilter = NewCustomFilter;

      int Max = this.CustomFilter.GetSize();
      
      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {
      
        TextBox textBoxFilter;
        ComboBox comboBoxFilter;
          
        textBoxFilter = this.Controls.Find(
          string.Format( "textBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        comboBoxFilter = this.Controls.Find(
          string.Format( "comboBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;
          
        if( this.CustomFilter.IsEnabled() )
        {

          KeyValuePair<string, MacroscopeConstants.Contains> Pair = this.CustomFilter.GetPattern( Slot: Slot );

          switch( Pair.Value )
          {

            case MacroscopeConstants.Contains.MUSTHAVE:
              comboBoxFilter.SelectedIndex = 1;
              break;

            case MacroscopeConstants.Contains.MUSTNOTHAVE:
              comboBoxFilter.SelectedIndex = 2;
              break;

            default:
              comboBoxFilter.SelectedIndex = 0;
              break;

          }

          textBoxFilter.Text = Pair.Key;

        }
        else
        {
        
          comboBoxFilter.SelectedIndex = 0;
          textBoxFilter.Text = "";

        }

      }
      
      return;
      
    }

    /**************************************************************************/

    public MacroscopeCustomFilters GetCustomFilter ()
    {

      int Max = this.CustomFilter.GetSize();
      
      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {
      
        TextBox textBoxFilter;
        ComboBox comboBoxFilter;
          
        textBoxFilter = this.Controls.Find(
          string.Format( "textBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        comboBoxFilter = this.Controls.Find(
          string.Format( "comboBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        switch( comboBoxFilter.SelectedIndex )
        {

          case 1:
            this.CustomFilter.SetPattern(
              Slot: Slot, 
              Text: textBoxFilter.Text, 
              ContainsSetting: MacroscopeConstants.Contains.MUSTHAVE
            );
            break;

          case 2:
            this.CustomFilter.SetPattern(
              Slot: Slot, 
              Text: textBoxFilter.Text, 
              ContainsSetting: MacroscopeConstants.Contains.MUSTNOTHAVE
            );
            break;

          default:
            this.CustomFilter.SetPattern(
              Slot: Slot, 
              Text: "", 
              ContainsSetting: MacroscopeConstants.Contains.UNDEFINED
            );
            break;

        }

      }

      return( this.CustomFilter );

    }

    /**************************************************************************/
        
    private void CallbackTextBoxKeyUp ( object sender, KeyEventArgs e )
    {
      
      TextBox CustomFilterTextBox = ( TextBox )sender;

      if( e.Control && ( e.KeyCode == Keys.A ) )
      {

        CustomFilterTextBox.SelectAll();
        CustomFilterTextBox.Focus();

      }

    }
       
    /**************************************************************************/
    
    public void ClearCustomFilterForm ()
    {

      int Max = this.CustomFilter.GetSize();
      
      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {
      
        TextBox textBoxFilter;
        ComboBox comboBoxFilter;
          
        textBoxFilter = this.Controls.Find(
          string.Format( "textBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        comboBoxFilter = this.Controls.Find(
          string.Format( "comboBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        comboBoxFilter.SelectedIndex = 0;
        textBoxFilter.Text = "";

      }

    }
    
    /**************************************************************************/
    
  }

}
