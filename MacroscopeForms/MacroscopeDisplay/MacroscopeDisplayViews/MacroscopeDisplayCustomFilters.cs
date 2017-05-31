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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;


namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayCustomFilters : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColUrl = 0;
    private const int ColStatusCode = 1;
    private const int ColStatus = 2;

    private int FilterColOffset = -1;
    
    /**************************************************************************/

    public MacroscopeDisplayCustomFilters ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.ConfigureListView();
            }
          )
        );
      }
      else
      {
        this.ConfigureListView();
      }

    }

    /**************************************************************************/

    protected override void ConfigureListView ()
    {
      if( !this.ListViewConfigured )
      {
        this.DisplayListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    public void ResetColumns (
      MacroscopeCustomFilters CustomFilter
    )
    {

      Dictionary<string,int> FilterColsTable = new Dictionary<string,int> ( CustomFilter.GetSize() );
      List<ListViewItem> ListViewItems = new List<ListViewItem> ();

      this.DisplayListView.Items.Clear();
      this.DisplayListView.Columns.Clear();

      this.DisplayListView.Columns.Add( MacroscopeConstants.Url, MacroscopeConstants.Url );
      this.DisplayListView.Columns.Add( MacroscopeConstants.StatusCode, MacroscopeConstants.StatusCode );      
      this.DisplayListView.Columns.Add( MacroscopeConstants.Status, MacroscopeConstants.Status );

      this.FilterColOffset = this.DisplayListView.Columns.Count - 1;

      for( int Slot = 0 ; Slot < CustomFilter.GetSize() ; Slot++ )
      {

        string FilterPattern = CustomFilter.GetPattern( Slot ).Key;

        this.DisplayListView.Columns.Add( FilterPattern, FilterPattern );

        if( FilterColsTable.ContainsKey( FilterPattern ) || string.IsNullOrEmpty( FilterPattern ) )
        {
          FilterColsTable.Add( string.Format( "EMPTY{0}", Slot + 1 ), Slot + this.FilterColOffset );
        }
        else
        {
          FilterColsTable.Add( FilterPattern, Slot + this.FilterColOffset );
        }

      }

    }

    /**************************************************************************/

    public void RefreshData (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList,
      MacroscopeCustomFilters CustomFilter

    )
    {

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.DisplayListView.BeginUpdate();
              this.RenderListView(
                DocCollection: DocCollection,
                UrlList: UrlList,
                CustomFilter: CustomFilter
              );
              this.DisplayListView.EndUpdate();
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.DisplayListView.BeginUpdate();
        this.RenderListView(
          DocCollection: DocCollection,
          UrlList: UrlList,
          CustomFilter: CustomFilter
        );
        this.DisplayListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
      
    }
   
    /**************************************************************************/

    private void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList,
      MacroscopeCustomFilters CustomFilter
    )
    {

      if( this.FilterColOffset == -1 )
      {
        throw( new Exception ( "this.FilterColOffset invalid" ) );
      }

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      Dictionary<string,int> FilterColsTable = new Dictionary<string,int> ( CustomFilter.GetSize() );
      
      if( DocCollection.CountDocuments() == 0 )
      {
        return;
      }
            
      List<ListViewItem> ListViewItems = new List<ListViewItem> ();

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ( this.MainForm );
      decimal Count = 0;
      decimal TotalDocs = ( decimal )DocCollection.CountDocuments();
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {

        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );  

      }

      for( int Slot = 0 ; Slot < CustomFilter.GetSize() ; Slot++ )
      {
        string FilterPattern = CustomFilter.GetPattern( Slot ).Key;

        if( FilterColsTable.ContainsKey( FilterPattern ) )
        {
          FilterColsTable.Add( string.Format( "EMPTY{0}", Slot + 1 ), Slot + 1 );
        }
        else
        {
          FilterColsTable.Add( FilterPattern, Slot + 1 );
        }

      }

      foreach( string Url in UrlList )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url: Url );
        Boolean Proceed = false;

        if( msDoc == null )
        {
          continue;
        }

        if( msDoc.GetIsInternal() )
        {
          if( msDoc.GetIsHtml() )
          {
            Proceed = true;
          }
          if( msDoc.GetIsRedirect() )
          {
            Proceed = false;
          }
        }

        if( Proceed )
        {

          ListViewItem lvItem = null;
          string DocUrl = msDoc.GetUrl();
          string PairKey = DocUrl;
          string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
          string Status = msDoc.GetStatusCode().ToString();

          if( this.DisplayListView.Items.ContainsKey( PairKey ) )
          {

            lvItem = this.DisplayListView.Items[ PairKey ];

          }
          else
          {

            lvItem = new ListViewItem ( PairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = PairKey;

            lvItem.SubItems.Add( "" );
            lvItem.SubItems.Add( "" );
            lvItem.SubItems.Add( "" );

            for( int Slot = 0 ; Slot < CustomFilter.GetSize() ; Slot++ )
            {
              lvItem.SubItems.Add( "" );
            }

            ListViewItems.Add( lvItem );

          }

          if( lvItem != null )
          {

            try
            {

              lvItem.SubItems[ ColUrl ].Text = DocUrl;
              lvItem.SubItems[ ColStatusCode ].Text = StatusCode;
              lvItem.SubItems[ ColStatus ].Text = Status;

              for( int Slot = 0 ; Slot < CustomFilter.GetSize() ; Slot++ )
              {

                string FilterPattern = CustomFilter.GetPattern( Slot: Slot ).Key;
                KeyValuePair<string, MacroscopeConstants.TextPresence> Pair = msDoc.GetCustomFilteredItem( Text: FilterPattern );
                string CustomFilterItemValue;
                int ColOffset = this.FilterColOffset + FilterColsTable[ FilterPattern ];

                if( ( Pair.Key != null ) && ( Pair.Value != MacroscopeConstants.TextPresence.UNDEFINED ) )
                {

                  CustomFilterItemValue = Pair.Value.ToString();

                  lvItem.SubItems[ ColOffset ].Text = CustomFilterItemValue;

                  switch( Pair.Value )
                  {
                    case MacroscopeConstants.TextPresence.CONTAINS:
                      lvItem.SubItems[ ColOffset ].ForeColor = Color.Green;
                      break;
                    case MacroscopeConstants.TextPresence.NOTCONTAINS:
                      lvItem.SubItems[ ColOffset ].ForeColor = Color.Green;
                      break;
                    case MacroscopeConstants.TextPresence.MUSTCONTAIN:
                      lvItem.SubItems[ ColOffset ].ForeColor = Color.Red;
                      break;
                    case MacroscopeConstants.TextPresence.SHOULDNOTCONTAIN:
                      lvItem.SubItems[ ColOffset ].ForeColor = Color.Red;
                      break;
                    default:
                      lvItem.SubItems[ ColOffset ].ForeColor = Color.Gray;
                      break;
                  }

                }

              }

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeDisplayCustomFilters: {0}", ex.Message ) );
              DebugMsg( string.Format( "MacroscopeDisplayCustomFilters: {0}", ex.StackTrace ) );
            }

          }
          else
          {
            DebugMsg( string.Format( "MacroscopeDisplayCustomFilters MISSING: {0}", PairKey ) );
          }

          if( msDoc.GetIsInternal() )
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
          }          

          if( Regex.IsMatch( StatusCode, "^[2]" ) )
          {
            lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Green;
            lvItem.SubItems[ ColStatus ].ForeColor = Color.Green;
          }
          else
          if( Regex.IsMatch( StatusCode, "^[3]" ) )
          {
            lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Goldenrod;
            lvItem.SubItems[ ColStatus ].ForeColor = Color.Goldenrod;
          }
          else
          if( Regex.IsMatch( StatusCode, "^[45]" ) )
          {
            lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Red;
            lvItem.SubItems[ ColStatus ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Blue;
            lvItem.SubItems[ ColStatus ].ForeColor = Color.Blue;
          }

        }

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {
          
          Count++;

          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );
        
        }
        
      }

      this.DisplayListView.Items.AddRange( ListViewItems.ToArray() );

      this.DisplayListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );

      this.DisplayListView.Columns[ ColUrl ].Width = 300;
      this.DisplayListView.Columns[ ColStatusCode ].Width = 100;
      this.DisplayListView.Columns[ ColStatus ].Width = 100;

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }
      
      ProgressForm.Dispose();
      
    }

    /**************************************************************************/

    protected override void RenderListView ( List<ListViewItem> ListViewItems, MacroscopeDocument msDoc, string Url )
    {
    }
    
    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
    }

    /**************************************************************************/

  }

}
