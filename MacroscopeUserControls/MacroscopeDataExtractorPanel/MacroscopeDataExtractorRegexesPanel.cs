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
  /// Description of MacroscopeDataExtractorRegexesPanel.
  /// </summary>

  public partial class MacroscopeDataExtractorRegexesPanel : MacroscopeDataExtractorPanel
  {

    /**************************************************************************/

    private MacroscopeDataExtractorRegexesForm ContainerForm;
        
    private MacroscopeDataExtractorRegexes DataExtractor;
   
    private List<TextBox> TextBoxLabels;
    private List<ComboBox> StateComboBoxes;
    private List<TextBox> TextBoxExpressions;

    /**************************************************************************/
	      
    public MacroscopeDataExtractorRegexesPanel ()
      : base()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.TextBoxLabels = new List<TextBox> ();
      this.StateComboBoxes = new List<ComboBox> ();
      this.TextBoxExpressions = new List<TextBox> ();

      this.tableLayoutPanelContainer.Dock = DockStyle.Fill;
      this.tableLayoutPanelControlsGrid.Dock = DockStyle.Fill;
     
    }

    /**************************************************************************/

    public void ConfigureDataExtractorForm (
      MacroscopeDataExtractorRegexesForm NewContainerForm,
      MacroscopeDataExtractorRegexes NewDataExtractor
    )
    {

      this.ContainerForm = NewContainerForm;
      
      this.DataExtractor = NewDataExtractor;
            
      int Max = this.DataExtractor.GetSize();
      TableLayoutPanel Table = this.tableLayoutPanelControlsGrid;
      
      Table.Dock = DockStyle.Fill;
      Table.ColumnCount = 4;
      Table.RowCount = Max + 1;

      {
        
        List<string> ColumnLabels = new List<string> ( 4 ) {
            "",
            "Active/Inactive",
            "Extractor Label",
            "Regular Expression Pattern"
        };
        
        for( int i = 0 ; i < ColumnLabels.Count ; i++ )
        {

          Label TextLabelCol = new Label ();

          TextLabelCol.Text = ColumnLabels[ i ];
          TextLabelCol.TextAlign = ContentAlignment.BottomLeft;
          TextLabelCol.Dock = DockStyle.Fill;
          TextLabelCol.Margin = new Padding ( 5, 5, 5, 5 );

          Table.Controls.Add( TextLabelCol );

        }
        
      }

      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        Label TextLabel = new Label ();
        ComboBox StateComboBox = new ComboBox ();
        TextBox TextBoxLabel = new TextBox ();
        TextBox TextBoxExpression = new TextBox ();

        TextLabel.Text = string.Format( "Regex {0}", Slot + 1 );
        TextLabel.TextAlign = ContentAlignment.MiddleRight;
        TextLabel.Dock = DockStyle.Fill;
        TextLabel.Margin = new Padding ( 5, 5, 5, 5 );

        StateComboBox.Name = string.Format( "StateComboBox{0}", Slot + 1 );
        StateComboBox.Items.Add( "Inactive" );
        StateComboBox.Items.Add( "Active" );
        StateComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        StateComboBox.SelectedIndex = 0;
        StateComboBox.Margin = new Padding ( 5, 5, 5, 5 );
        StateComboBox.Width = 100;
                
        TextBoxLabel.Name = string.Format( "TextBoxLabel{0}", Slot + 1 );
        TextBoxLabel.Dock = DockStyle.Fill;
        TextBoxLabel.Margin = new Padding ( 5, 5, 5, 5 );
        TextBoxLabel.Tag = Slot.ToString();
                
        TextBoxLabel.KeyUp += this.CallbackTextBoxKeyUp;
        TextBoxLabel.TextChanged += this.CallbackTextBoxLabelTextChanged;
        
        TextBoxExpression.Name = string.Format( "TextBoxExpression{0}", Slot + 1 );
        TextBoxExpression.Dock = DockStyle.Fill;
        TextBoxExpression.Margin = new Padding ( 5, 5, 5, 5 );
        TextBoxExpression.Tag = Slot.ToString();
                
        TextBoxExpression.KeyUp += this.CallbackTextBoxKeyUp;
        TextBoxExpression.TextChanged += this.CallbackTextBoxExpressionTextChanged;

        Table.Controls.Add( TextLabel );
        Table.Controls.Add( StateComboBox );  
        Table.Controls.Add( TextBoxLabel );
        Table.Controls.Add( TextBoxExpression );

        this.TextBoxLabels.Add( TextBoxLabel );
        this.StateComboBoxes.Add( StateComboBox );
        this.TextBoxExpressions.Add( TextBoxExpression );

      }

      // Add empty last row for space adjustment
      for( int i = 0 ; i < Table.ColumnCount ; i++ )
      {
        Label TextLabelCol = new Label ();
        TextLabelCol.Text = "";
        Table.Controls.Add( TextLabelCol );
      }

      Table.AutoScroll = false;
      Table.Padding = new Padding ( 0, 0, 15, 0 );
      Table.AutoScroll = true;
     
    }

    /**************************************************************************/

    public void SetDataExtractor ()
    {

      int Max = this.DataExtractor.GetSize();

      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        ComboBox StateComboBox;
        TextBox TextBoxLabel;
        TextBox TextBoxExpression;

        StateComboBox = this.Controls.Find(
          string.Format( "StateComboBox{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        TextBoxLabel = this.Controls.Find(
          string.Format( "TextBoxLabel{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        TextBoxExpression = this.Controls.Find(
          string.Format( "TextBoxExpression{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;

        if( this.DataExtractor.IsEnabled() )
        {

          MacroscopeConstants.ActiveInactive State = this.DataExtractor.GetActiveInactive( Slot: Slot );

          switch( State )
          {

            case MacroscopeConstants.ActiveInactive.ACTIVE:
              StateComboBox.SelectedIndex = 1;
              break;

            default:
              StateComboBox.SelectedIndex = 0;
              break;

          }

          TextBoxLabel.Text = this.DataExtractor.GetLabel( Slot: Slot );

          TextBoxExpression.Text = this.DataExtractor.GetRegex( Slot: Slot ).ToString();

          if(
            string.IsNullOrEmpty( TextBoxLabel.Text )
            || string.IsNullOrEmpty( TextBoxExpression.Text ) )
          {
            StateComboBox.SelectedIndex = 0;
          }
          
        }
        else
        {
        
          StateComboBox.SelectedIndex = 0;
          TextBoxLabel.Text = "";
          TextBoxExpression.Text = "";

        }

      }
      
      return;
      
    }
   
    /**************************************************************************/

    public MacroscopeDataExtractorRegexes GetDataExtractor ()
    {

      int Max = this.DataExtractor.GetSize();

      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        ComboBox StateComboBox;
        TextBox TextBoxLabel;
        TextBox TextBoxExpression;

        StateComboBox = this.Controls.Find(
          string.Format( "StateComboBox{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        TextBoxLabel = this.Controls.Find(
          string.Format( "TextBoxLabel{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        TextBoxExpression = this.Controls.Find(
          string.Format( "TextBoxExpression{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;

        switch( StateComboBox.SelectedIndex )
        {

          case 1:
            this.DataExtractor.SetActiveInactive(
              Slot: Slot,
              State: MacroscopeConstants.ActiveInactive.ACTIVE
            );
            break;

          default:
            this.DataExtractor.SetActiveInactive(
              Slot: Slot,
              State: MacroscopeConstants.ActiveInactive.INACTIVE
            );
            break;

        }

        if(
          string.IsNullOrEmpty( TextBoxLabel.Text )
          || string.IsNullOrEmpty( TextBoxExpression.Text ) )
        {
          this.DataExtractor.SetActiveInactive(
            Slot: Slot,
            State: MacroscopeConstants.ActiveInactive.INACTIVE
          );
        }

        try
        {

          this.DataExtractor.SetRegex(
            Slot: Slot,
            RegexLabel: TextBoxLabel.Text,
            RegexString: TextBoxExpression.Text
          );
          
        }
        catch( System.FormatException ex )
        {
          ms.DebugMsg( ex.Message );
        }
        catch( Exception ex )
        {
          ms.DebugMsg( ex.Message );
        }

      }

      return( this.DataExtractor );

    }

    /** Form Validator ********************************************************/

    public Boolean ValidateForm ( Boolean ShowErrorDialogue )
    {

      Boolean IsValid = true;
      int Max = this.DataExtractor.GetSize();

      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        ComboBox StateComboBox;
        TextBox TextBoxLabel;
        TextBox TextBoxExpression;

        StateComboBox = this.Controls.Find(
          string.Format( "StateComboBox{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        TextBoxLabel = this.Controls.Find(
          string.Format( "TextBoxLabel{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        TextBoxExpression = this.Controls.Find(
          string.Format( "TextBoxExpression{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;

        switch( StateComboBox.SelectedIndex )
        {
          case 1:

            if(
              !this.ValidateLabel(
                TextBoxObject: TextBoxLabel,
                ShowErrorDialogue: ShowErrorDialogue 
              ) )
            {
              IsValid = false;
            }

            if( 
              !this.ValidateExpression(
                TextBoxObject: TextBoxExpression, 
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

    protected override Boolean ValidateLabel ( TextBox TextBoxObject, Boolean ShowErrorDialogue )
    {

      Boolean IsValid = true;

      if( TextBoxObject.Text.Length > 0 )
      {
        TextBoxObject.ForeColor = Color.Green;
      }
      else
      {
        TextBoxObject.ForeColor = Color.Red;
        IsValid = false;
      }

      if( ( !IsValid ) && ( ShowErrorDialogue ) )
      {
        this.DialogueBoxError( "Error", "Please enter a label." );
        TextBoxObject.Focus();
      }
            
      return( IsValid );

    }

    /** -------------------------------------------------------------------- **/

    protected override Boolean ValidateExpression ( TextBox TextBoxObject, Boolean ShowErrorDialogue )
    {

      Boolean IsValid = false;
      
      if( !this.GetEnableValidation() )
      {
        IsValid = false;
      }

      try
      {

        string Value = TextBoxObject.Text;

        if( MacroscopeDataExtractorRegexes.SyntaxCheckRegex( RegexString: Value ) )
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
        this.DialogueBoxError( AlertTitle: "Error", AlertMessage: "Invalid Regular Expression." );   
        TextBoxObject.Focus();        
      }

      return( IsValid );
      
    }

    /**************************************************************************/

    public void ClearDataExtractorForm ()
    {

      int Max = this.DataExtractor.GetSize();

      this.SetEnableValidation( State: false );
            
      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        ComboBox StateComboBox;
        TextBox TextBoxLabel;
        TextBox TextBoxExpression;

        StateComboBox = this.Controls.Find(
          string.Format( "StateComboBox{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        TextBoxLabel = this.Controls.Find(
          string.Format( "TextBoxLabel{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;
          
        TextBoxExpression = this.Controls.Find(
          string.Format( "TextBoxExpression{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as TextBox;

        StateComboBox.SelectedIndex = 0;
        TextBoxLabel.Text = "";
        TextBoxExpression.Text = "";

      }

      this.SetEnableValidation( State: true );
            
    }
    
    /**************************************************************************/
    
  }

}
