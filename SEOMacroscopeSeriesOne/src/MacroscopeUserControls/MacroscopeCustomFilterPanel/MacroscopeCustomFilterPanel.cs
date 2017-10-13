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
  /// Description of MacroscopeCustomFilterPanel.
  /// </summary>

  public partial class MacroscopeCustomFilterPanel : MacroscopeUserControl
  {

    /**************************************************************************/

    private Macroscope ms;
        
    private MacroscopeCustomFilterForm ContainerForm;

    MacroscopeCustomFilters CustomFilter;

    private List<Label> TextBoxLabels;
    private List<ComboBox> StateComboBoxFilters;
    private List<TextBox> TextBoxFilters;

    private Boolean EnableValidation;

    /**************************************************************************/
	      
    public MacroscopeCustomFilterPanel ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.ms = new Macroscope ();
            
      this.TextBoxLabels = new List<Label> ();
      this.StateComboBoxFilters = new List<ComboBox> ();
      this.TextBoxFilters = new List<TextBox> ();

      this.tableLayoutPanelControlsGrid.Dock = DockStyle.Fill;

      this.SetEnableValidation( State: true );
            
    }

    /**************************************************************************/

    protected void SetEnableValidation ( Boolean State )
    {
      this.EnableValidation = State;
    }

    /** -------------------------------------------------------------------- **/

    protected Boolean GetEnableValidation ()
    {
      return( this.EnableValidation );
    }
    
    /**************************************************************************/

    public void ConfigureCustomFilterForm (
      MacroscopeCustomFilterForm NewContainerForm,
      MacroscopeCustomFilters NewCustomFilter
    )
    {

      this.ContainerForm = NewContainerForm;
      
      this.CustomFilter = NewCustomFilter;
            
      int Max = this.CustomFilter.GetSize();
      TableLayoutPanel Table = this.tableLayoutPanelControlsGrid;
      
      Table.ColumnCount = 3;
      Table.RowCount = Max + 1;

      {
        
        List<string> ColumnLabels = new List<string> ( 3 ) {
            "",
            "Filter Action",
            "Search String"
        };
        
        for( int i = 0 ; i < ColumnLabels.Count ; i++ )
        {
          Label TextLabelCol = new Label ();
          TextLabelCol.Text = ColumnLabels[ i ];
          TextLabelCol.Dock = DockStyle.Fill;
          TextLabelCol.Margin = new Padding ( 5, 5, 5, 5 );
          TextLabelCol.TextAlign = ContentAlignment.BottomLeft;
          TextLabelCol.Height = 20;
          Table.Controls.Add( TextLabelCol );
        }

      }

      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        Label TextBoxLabel = new Label ();
        ComboBox StateComboBoxFilter = new ComboBox ();
        TextBox TextBoxFilter = new TextBox ();
        
        TextBoxLabel.Text = string.Format( "Custom Filter {0}", Slot + 1 );
        TextBoxLabel.TextAlign = ContentAlignment.MiddleRight;
        TextBoxLabel.Dock = DockStyle.Fill;
        TextBoxLabel.Margin = new Padding ( 5, 5, 5, 5 );
        TextBoxLabel.Width = 50;

        StateComboBoxFilter.Name = string.Format( "ComboBoxFilter{0}", Slot + 1 );
        StateComboBoxFilter.Items.Add( "No action" );
        StateComboBoxFilter.Items.Add( "Must have" );  
        StateComboBoxFilter.Items.Add( "Must not have" );
        StateComboBoxFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        StateComboBoxFilter.SelectedIndex = 0;
        StateComboBoxFilter.Margin = new Padding ( 5, 5, 5, 5 );
        StateComboBoxFilter.Width = 100;
        
        TextBoxFilter.Name = string.Format( "TextBoxFilter{0}", Slot + 1 );
        TextBoxFilter.Dock = DockStyle.Fill;
        TextBoxFilter.Margin = new Padding ( 5, 5, 5, 5 );
        TextBoxFilter.Tag = Slot.ToString();
        TextBoxFilter.KeyUp += this.CallbackTextBoxKeyUp;
        TextBoxFilter.TextChanged += CallbackTextBoxExpressionTextChanged;
        
        Table.Controls.Add( TextBoxLabel );
        Table.Controls.Add( StateComboBoxFilter );  
        Table.Controls.Add( TextBoxFilter );

        this.TextBoxLabels.Add( TextBoxLabel );
        this.StateComboBoxFilters.Add( StateComboBoxFilter );
        this.TextBoxFilters.Add( TextBoxFilter );

      }
     
      // Add empty last row for space adjustment
      for( int i = 0 ; i < Table.ColumnCount ; i++ )
      {
        Label TextLabelCol = new Label ();
        TextLabelCol.Text = "";
        Table.Controls.Add( TextLabelCol );
      }

    }

    /**************************************************************************/

    public void SetCustomFilter ()
    {

      int Max = this.CustomFilter.GetSize();
      
      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {
      
        ComboBox ComboBoxFilter;
        TextBox TextBoxFilter;

        ComboBoxFilter = this.Controls.Find(
          string.Format( "ComboBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        TextBoxFilter = this.Controls.Find(
          string.Format( "TextBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;

        if( this.CustomFilter.IsEnabled() )
        {

          KeyValuePair<string, MacroscopeConstants.Contains> Pair = this.CustomFilter.GetPattern( Slot: Slot );

          switch( Pair.Value )
          {

            case MacroscopeConstants.Contains.MUSTHAVE:
              ComboBoxFilter.SelectedIndex = 1;
              break;

            case MacroscopeConstants.Contains.MUSTNOTHAVE:
              ComboBoxFilter.SelectedIndex = 2;
              break;

            default:
              ComboBoxFilter.SelectedIndex = 0;
              break;

          }

          TextBoxFilter.Text = Pair.Key;

        }
        else
        {
        
          ComboBoxFilter.SelectedIndex = 0;
          TextBoxFilter.Text = "";

        }

      }
     
    }

    /**************************************************************************/

    public MacroscopeCustomFilters GetCustomFilter ()
    {

      int Max = this.CustomFilter.GetSize();
      
      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {
      
        ComboBox ComboBoxFilter;
        TextBox TextBoxFilter;

        ComboBoxFilter = this.Controls.Find(
          string.Format( "ComboBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        TextBoxFilter = this.Controls.Find(
          string.Format( "TextBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;

        switch( ComboBoxFilter.SelectedIndex )
        {

          case 1:
            this.CustomFilter.SetPattern(
              Slot: Slot, 
              Text: TextBoxFilter.Text, 
              ContainsSetting: MacroscopeConstants.Contains.MUSTHAVE
            );
            break;

          case 2:
            this.CustomFilter.SetPattern(
              Slot: Slot, 
              Text: TextBoxFilter.Text, 
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

    /** Form Validator ********************************************************/

    public Boolean ValidateForm ( Boolean ShowErrorDialogue )
    {

      Boolean IsValid = true;
      int Max = this.CustomFilter.GetSize();

      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        ComboBox ComboBoxFilter;
        TextBox TextBoxFilter;

        ComboBoxFilter = this.Controls.Find(
          string.Format( "ComboBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        TextBoxFilter = this.Controls.Find(
          string.Format( "TextBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;

        switch( ComboBoxFilter.SelectedIndex )
        {

          case 1:
            if(
              !this.ValidateExpression(
                TextBoxObject: TextBoxFilter, 
                ShowErrorDialogue: ShowErrorDialogue 
              ) )
            {
              IsValid = false;
            }
            break;

          case 2:
            if(
              !this.ValidateExpression(
                TextBoxObject: TextBoxFilter, 
                ShowErrorDialogue: ShowErrorDialogue 
              ) )
            {
              IsValid = false;
            }
            break;

          default:
            break;

        }

        if( !IsValid )
        {
          break;
        }
                
      }

      return( IsValid );

    }

    /** -------------------------------------------------------------------- **/

    private Boolean ValidateExpression ( TextBox TextBoxObject, Boolean ShowErrorDialogue )
    {

      Boolean IsValid = false;
      
      if( !this.GetEnableValidation() )
      {
        IsValid = false;
      }

      try
      {

        string Value = TextBoxObject.Text;

        if( !string.IsNullOrEmpty( Value ) )
        {
          IsValid = true;
        }

      }
      catch( Exception ex )
      {
        ms.DebugMsg( ex.Message );
        
        IsValid = false;
      }

      if( ( !IsValid ) && ( ShowErrorDialogue ) )
      {
        this.DialogueBoxError( AlertTitle: "Error", AlertMessage: "Please enter a filter value." );   
        TextBoxObject.Focus();        
      }

      return( IsValid );
      
    }

    /** Expression Event Handlers *********************************************/

    protected void CallbackTextBoxExpressionTextChanged ( object sender, EventArgs e )
    {

      TextBox TextBoxObject = ( TextBox )sender;
      Boolean IsValid = false;

      TextBoxObject.Text = MacroscopeStringTools.StripNewLines( Text: TextBoxObject.Text );

      IsValid = this.ValidateExpression( TextBoxObject: TextBoxObject, ShowErrorDialogue: false );

      if( IsValid )
      {
        TextBoxObject.ForeColor = Color.Green;
      }
      else
      {
        TextBoxObject.ForeColor = Color.Red;
      }
      
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
      
      this.SetEnableValidation( State: false );
            
      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {
      
        TextBox TextBoxFilter;
        ComboBox ComboBoxFilter;
          
        TextBoxFilter = this.Controls.Find(
          string.Format( "TextBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        ComboBoxFilter = this.Controls.Find(
          string.Format( "ComboBoxFilter{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        ComboBoxFilter.SelectedIndex = 0;
        TextBoxFilter.Text = "";

      }

      this.SetEnableValidation( State: true );
            
    }

    /**************************************************************************/

    protected void DialogueBoxError ( string AlertTitle, string AlertMessage )
    {
      MessageBox.Show(
        AlertMessage,
        AlertTitle,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error,
        MessageBoxDefaultButton.Button1
      );
    }

    /**************************************************************************/

  }

}
