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
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDataExtractorPanel.
  /// </summary>

  public partial class MacroscopeDataExtractorRegexesPanel : UserControl
  {

    /**************************************************************************/

    MacroscopeDataExtractorRegexes DataExtractorRegexes;

    List<TextBox> TextBoxLabels;
    List<ComboBox> StateComboBoxes;
    List<TextBox> TextBoxRegexes;

    /**************************************************************************/
	      
    public MacroscopeDataExtractorRegexesPanel ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.TextBoxLabels = new List<TextBox> ();
      this.StateComboBoxes = new List<ComboBox> ();
      this.TextBoxRegexes = new List<TextBox> ();

      this.tableLayoutPanelContainer.Dock = DockStyle.Fill;
      this.tableLayoutPanelRegexGrid.Dock = DockStyle.Fill;

    }

    /**************************************************************************/

    public void ConfigureDataExtractorForm ( MacroscopeDataExtractorRegexes NewDataExtractor )
    {

      this.DataExtractorRegexes = NewDataExtractor;
            
      int Max = this.DataExtractorRegexes.GetSize();
      TableLayoutPanel Table = this.tableLayoutPanelRegexGrid;
      
      Table.ColumnCount = 4;
      Table.RowCount = Max;
      
      for( int i = 0 ; i < Max ; i++ )
      {

        Label TextLabel = new Label ();
        ComboBox StateComboBox= new ComboBox ();
        TextBox TextBoxLabel = new TextBox ();
        TextBox TextBoxRegex = new TextBox ();

        TextLabel.Text = string.Format( "Regex {0}", i );
        StateComboBox.Items.Add( "Inactive" );
        StateComboBox.Items.Add( "Active" );
        TextBoxLabel.KeyUp += this.CallbackTextBoxKeyUp;
        TextBoxRegex.KeyUp += this.CallbackTextBoxKeyUp;

        Table.Controls.Add( TextLabel );
        Table.Controls.Add( StateComboBox );  
        Table.Controls.Add( TextBoxLabel );
        Table.Controls.Add( TextBoxRegex );

        this.TextBoxLabels.Add( TextBoxLabel );
        this.StateComboBoxes.Add( StateComboBox );
        this.TextBoxRegexes.Add( TextBoxRegex );

      }
      
      
      

      
      
      
      
      
      
      
      
      
      
      
      
    }

    /**************************************************************************/
    
    
    
    
    
    
    
    
    /*
    public void SetDataExtractorRegexes ( MacroscopeDataExtractorRegexes NewDataExtractorRegexes )
    {

      this.DataExtractorRegexes = NewDataExtractorRegexes;

      int Max = this.DataExtractorRegexes.GetSize();
      
      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {
      
        TextBox textBoxRegex;
        ComboBox comboBoxRegex;
          
        textBoxRegex = this.Controls.Find(
          string.Format( "textBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        comboBoxRegex = this.Controls.Find(
          string.Format( "comboBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;
          
        if( this.DataExtractorRegexes.IsEnabled() )
        {

          MacroscopeConstants.ActiveInactive State = this.DataExtractorRegexes.GetActiveInactive( Slot: Slot );

          switch( State )
          {

            case MacroscopeConstants.ActiveInactive.ACTIVE:
              comboBoxRegex.SelectedIndex = 1;
              break;

            default:
              comboBoxRegex.SelectedIndex = 0;
              break;

          }

          textBoxRegex.Text = this.DataExtractorRegexes.GetPattern( Slot: Slot ).Value.ToString();

        }
        else
        {
        
          comboBoxRegex.SelectedIndex = 0;
          textBoxRegex.Text = "";

        }

      }
      
      return;
      
    }
    */
   
    /**************************************************************************/

    /*
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
    */
   
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
    
    /*
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
    */
   

    
    /**************************************************************************/
    
  }

}
