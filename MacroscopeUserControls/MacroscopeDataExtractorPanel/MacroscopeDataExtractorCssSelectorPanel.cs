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
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDataExtractorCssSelectorPanel.
  /// </summary>

  public partial class MacroscopeDataExtractorCssSelectorPanel : UserControl
  {

    /**************************************************************************/
    
    private MacroscopeDataExtractorCssSelectorsForm ContainerForm;
        
    private MacroscopeDataExtractorCssSelectors DataExtractor;

    private Boolean Ready;
    private Boolean EnableValidation;
        
    private List<TextBox> TextBoxLabels;
    private List<ComboBox> StateComboBoxes;
    private List<TextBox> TextBoxExpressions;
    private List<ComboBox> ExtractToComboBoxes;
    
    /**************************************************************************/
	      
    public MacroscopeDataExtractorCssSelectorPanel ()
    {

      InitializeComponent(); // The InitializeComponent() call is required for Windows Forms designer support.

      this.TextBoxLabels = new List<TextBox> ();
      this.StateComboBoxes = new List<ComboBox> ();
      this.TextBoxExpressions = new List<TextBox> ();
      this.ExtractToComboBoxes = new List<ComboBox> ();

      this.tableLayoutPanelContainer.Dock = DockStyle.Fill;
      this.tableLayoutPanelControlsGrid.Dock = DockStyle.Fill;

      this.SetReady( State: true );
      this.SetEnableValidation( State: true );
      
    }

    /**************************************************************************/

    private void SetReady ( Boolean State )
    {
      this.Ready = State;
    }

    public Boolean GetReady ()
    {
      return( this.Ready );
    }

    /**************************************************************************/

    private void SetEnableValidation ( Boolean State )
    {
      this.EnableValidation = State;
    }

    private Boolean GetEnableValidation ()
    {
      return( this.EnableValidation );
    }

    /**************************************************************************/

    public void ConfigureDataExtractorForm (
      MacroscopeDataExtractorCssSelectorsForm NewContainerForm,
      MacroscopeDataExtractorCssSelectors NewDataExtractor
    )
    {

      this.ContainerForm = NewContainerForm;
      
      this.DataExtractor = NewDataExtractor;
            
      int Max = this.DataExtractor.GetSize();
      TableLayoutPanel Table = this.tableLayoutPanelControlsGrid;
      
      Table.ColumnCount = 5;
      Table.RowCount = Max + 1;

      {
        
        List<string> ColumnLabels = new List<string> ( 5 ) {
            "",
            "Active/Inactive",
            "Extractor Label",
            "CSS Selector Expression",
            "Extract To"
        };
        
        for( int i = 0 ; i < ColumnLabels.Count ; i++ )
        {
          Label TextLabelCol = new Label ();
          TextLabelCol.Text = ColumnLabels[ i ];
          TextLabelCol.Dock = DockStyle.Fill;
          TextLabelCol.Margin = new Padding ( 5, 5, 5, 5 );
          TextLabelCol.TextAlign = ContentAlignment.BottomLeft;
          Table.Controls.Add( TextLabelCol );
        }
        
      }

      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        Label TextLabel = new Label ();
        ComboBox StateComboBox = new ComboBox ();
        TextBox TextBoxLabel = new TextBox ();
        TextBox TextBoxExpression = new TextBox ();
        ComboBox ExtractToComboBox = new ComboBox ();
        
        TextLabel.Text = string.Format( "CSS Selector {0}", Slot + 1 );
        TextLabel.TextAlign = ContentAlignment.MiddleRight;
        TextLabel.Dock = DockStyle.Fill;
        TextLabel.Margin = new Padding ( 5, 5, 5, 5 );

        StateComboBox.Name = string.Format( "StateComboBox{0}", Slot + 1 );
        StateComboBox.Items.Add( "Inactive" );
        StateComboBox.Items.Add( "Active" );
        StateComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        StateComboBox.SelectedIndex = 0;
        StateComboBox.Margin = new Padding ( 5, 5, 5, 5 );
        
        TextBoxLabel.Name = string.Format( "TextBoxLabel{0}", Slot + 1 );
        TextBoxLabel.KeyUp += this.CallbackTextBoxKeyUp;
        TextBoxLabel.Dock = DockStyle.Fill;
        TextBoxLabel.Margin = new Padding ( 5, 5, 5, 5 );

        TextBoxLabel.Tag = Slot.ToString();
                
        TextBoxLabel.Enter += this.CallbackTextBoxLabelEnter;
        TextBoxLabel.Leave += this.CallbackTextBoxLabelLeave;
        TextBoxLabel.TextChanged += this.CallbackTextBoxLabelTextChanged;
        
        TextBoxExpression.Name = string.Format( "TextBoxExpression{0}", Slot + 1 );
        TextBoxExpression.KeyUp += this.CallbackTextBoxKeyUp;
        TextBoxExpression.Dock = DockStyle.Fill;
        TextBoxExpression.Margin = new Padding ( 5, 5, 5, 5 );

        TextBoxExpression.Tag = Slot.ToString();
        
        TextBoxExpression.Enter += this.CallbackTextBoxExpressionEnter;
        TextBoxExpression.Leave += this.CallbackTextBoxExpressionLeave;
        TextBoxExpression.TextChanged += this.CallbackTextBoxExpressionTextChanged;

        ExtractToComboBox.Name = string.Format( "ExtractToComboBox{0}", Slot + 1 );
        ExtractToComboBox.Items.Add( "Extract HTML Element" );
        ExtractToComboBox.Items.Add( "Extract Inner HTML Elements" );
        ExtractToComboBox.Items.Add( "Extract Text" );
        ExtractToComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        ExtractToComboBox.SelectedIndex = 0;
        ExtractToComboBox.Margin = new Padding ( 5, 5, 5, 5 );
        ExtractToComboBox.Width = 150;

        Table.Controls.Add( TextLabel );
        Table.Controls.Add( StateComboBox );  
        Table.Controls.Add( TextBoxLabel );
        Table.Controls.Add( TextBoxExpression );
        Table.Controls.Add( ExtractToComboBox );

        this.TextBoxLabels.Add( TextBoxLabel );
        this.StateComboBoxes.Add( StateComboBox );
        this.TextBoxExpressions.Add( TextBoxExpression );
        this.ExtractToComboBoxes.Add( ExtractToComboBox );

      }

      for( int i = 0 ; i < Table.ColumnCount ; i++ )
      {
        Label TextLabelCol = new Label ();
        TextLabelCol.Text = "";
        Table.Controls.Add( TextLabelCol );
      }

      {
        
        int Count = 1;

        foreach( RowStyle Style in Table.RowStyles )
        {
          decimal RowHeight = ( decimal )Table.Height / ( decimal )Table.RowCount;
          Style.SizeType = SizeType.Absolute;
          Style.Height = ( int )RowHeight * Count;
          Count++;
        }
      
      }

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
        ComboBox ExtractToComboBox;

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
        
        ExtractToComboBox = this.Controls.Find(
          string.Format( "ExtractToComboBox{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;
        
        if( this.DataExtractor.IsEnabled() )
        {

          MacroscopeConstants.ActiveInactive State = this.DataExtractor.GetActiveInactive( Slot: Slot );
          MacroscopeConstants.DataExtractorType ExtractorType = this.DataExtractor.GetExtractorType( Slot: Slot );
          
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

          TextBoxExpression.Text = this.DataExtractor.GetCssSelector( Slot: Slot );

          if(
            string.IsNullOrEmpty( TextBoxLabel.Text )
            || string.IsNullOrEmpty( TextBoxExpression.Text ) )
          {
            StateComboBox.SelectedIndex = 0;
          }
          
          switch( ExtractorType )
          {
            case MacroscopeConstants.DataExtractorType.OUTERHTML:
              ExtractToComboBox.SelectedIndex = 0;
              break;
            case MacroscopeConstants.DataExtractorType.INNERHTML:
              ExtractToComboBox.SelectedIndex = 1;
              break;
            case MacroscopeConstants.DataExtractorType.INNERTEXT:
              ExtractToComboBox.SelectedIndex = 2;
              break;
            default:
              ExtractToComboBox.SelectedIndex = 0;
              break;
          }

        }
        else
        {
        
          StateComboBox.SelectedIndex = 0;
          TextBoxLabel.Text = "";
          TextBoxExpression.Text = "";
          ExtractToComboBox.SelectedIndex = 0;

        }

      }
      
      return;
      
    }
   
    /**************************************************************************/

    public MacroscopeDataExtractorCssSelectors GetDataExtractor ()
    {

      int Max = this.DataExtractor.GetSize();

      for( int Slot = 0 ; Slot < Max ; Slot++ )
      {

        MacroscopeConstants.DataExtractorType ExtractorType;
        ComboBox StateComboBox;
        TextBox TextBoxLabel;
        TextBox TextBoxExpression;
        ComboBox ExtractToComboBox;

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
        
        ExtractToComboBox = this.Controls.Find(
          string.Format( "ExtractToComboBox{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

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

        switch( ExtractToComboBox.SelectedIndex )
        {
          case 0:
            ExtractorType = MacroscopeConstants.DataExtractorType.OUTERHTML;
            break;
          case 1:
            ExtractorType = MacroscopeConstants.DataExtractorType.INNERHTML;
            break;
          case 2:
            ExtractorType = MacroscopeConstants.DataExtractorType.INNERTEXT;
            break;

          default:
            ExtractorType = MacroscopeConstants.DataExtractorType.INNERTEXT;
            break;
        }

        this.DataExtractor.SetCssSelector(
          Slot: Slot,
          CssSelectorLabel: TextBoxLabel.Text,
          CssSelectorString: TextBoxExpression.Text,
          ExtractorType: ExtractorType
        ); 

      }

      return( this.DataExtractor );

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

    /** Label Validators ******************************************************/

    private void CallbackTextBoxLabelEnter ( object sender, EventArgs e )
    {
      this.CallbackTextBoxExpressionTextChanged( sender, e );
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTextBoxLabelLeave ( object sender, EventArgs e )
    {

      TextBox TextBoxObject = ( TextBox )sender;
      Boolean Proceed = this.CheckDoValidation( TextBoxObject: TextBoxObject );

      if( Proceed )
      {

        if( TextBoxObject.Text.Length > 0 )
        {
          TextBoxObject.ForeColor = Color.Green;
          this.ContainerForm.EnableButtonOk();
        }
        else
        {
          TextBoxObject.ForeColor = Color.Red;
          this.ContainerForm.DisableButtonOk();
          this.DialogueBoxError( "Error", "Please enter a label." );
          TextBoxObject.Focus();
        }

      }
      
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTextBoxLabelTextChanged ( object sender, EventArgs e )
    {

      TextBox TextBoxObject = ( TextBox )sender;
      Boolean Proceed = this.CheckDoValidation( TextBoxObject: TextBoxObject );

      if( Proceed )
      {
      
        if( TextBoxObject.Text.Length > 0 )
        {
          TextBoxObject.ForeColor = Color.Green;
          this.ContainerForm.EnableButtonOk();
        }
        else
        {
          TextBoxObject.ForeColor = Color.Red;
          this.ContainerForm.DisableButtonOk();
          TextBoxObject.Focus();
        }
      
      }

    }

    /** CSS Selector Expression Validators ************************************/

    private void CallbackTextBoxExpressionEnter ( object sender, EventArgs e )
    {
      this.CallbackTextBoxExpressionTextChanged( sender, e );
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTextBoxExpressionLeave ( object sender, EventArgs e )
    {

      TextBox TextBoxObject = ( TextBox )sender;
      Boolean Proceed = this.CheckDoValidation( TextBoxObject: TextBoxObject );

      if( Proceed )
      {

        if( MacroscopeDataExtractorCssSelectors.SyntaxCheckCssSelector( CssSelectorString: TextBoxObject.Text ) )
        {
          TextBoxObject.ForeColor = Color.Green;
          this.ContainerForm.EnableButtonOk();
        }
        else
        {
          TextBoxObject.ForeColor = Color.Red;
          this.ContainerForm.DisableButtonOk();
          this.DialogueBoxError( AlertTitle: "Error", AlertMessage: "Invalid CSS Selector." );
          TextBoxObject.Focus();
        }

      }
      
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackTextBoxExpressionTextChanged ( object sender, EventArgs e )
    {

      TextBox TextBoxObject = ( TextBox )sender;
      Boolean Proceed = this.CheckDoValidation( TextBoxObject: TextBoxObject );

      if( Proceed )
      {

        if( MacroscopeDataExtractorCssSelectors.SyntaxCheckCssSelector( CssSelectorString: TextBoxObject.Text ) )
        {
          TextBoxObject.ForeColor = Color.Green;
          this.ContainerForm.EnableButtonOk();
        }
        else
        {
          TextBoxObject.ForeColor = Color.Red;
          this.ContainerForm.DisableButtonOk();
          TextBoxObject.Focus();
        }

      }
      
    }

    /** -------------------------------------------------------------------- **/

    private Boolean CheckDoValidation ( TextBox TextBoxObject )
    {

      Boolean Proceed = true;
      string TagValue = TextBoxObject.Tag.ToString();
      
      if( !this.GetEnableValidation() )
      {
        Proceed = false;
      }

      try
      {

        int Slot = int.Parse( TagValue );

        ComboBox StateComboBox;

        StateComboBox = this.Controls.Find(
          string.Format( "StateComboBox{0}", Slot + 1 ),
          true
        ).FirstOrDefault() as ComboBox;

        switch( StateComboBox.SelectedIndex )
        {
          case 0:
            Proceed = false;
            break;
          default:
            break;
        }

      }
      catch( Exception ex )
      {
        this.DialogueBoxError( AlertTitle: "Error", AlertMessage: ex.Message );
      }

      return( Proceed );
      
    }

    /**************************************************************************/

    private void DialogueBoxError ( string AlertTitle, string AlertMessage )
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
