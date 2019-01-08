/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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

  public sealed class MacroscopeDisplayDataExtractorXpaths : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColUrl = 0;
    private const int ColStatusCode = 1;
    private const int ColStatus = 2;
    private const int ColMimeType = 3;
    private const int ColXpathLabel = 4;
    private const int ColExtractedValue = 5;

    private ToolStripLabel ItemCount;

    /**************************************************************************/

    public MacroscopeDisplayDataExtractorXpaths ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.ItemCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelXpaths;

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

    public void ResetColumns ()
    {

      List<ListViewItem> ListViewItems = new List<ListViewItem> ();

      this.DisplayListView.Items.Clear();
      this.DisplayListView.Columns.Clear();

      this.DisplayListView.Columns.Add( MacroscopeConstants.Url, MacroscopeConstants.Url );
      this.DisplayListView.Columns.Add( MacroscopeConstants.StatusCode, MacroscopeConstants.StatusCode );      
      this.DisplayListView.Columns.Add( MacroscopeConstants.Status, MacroscopeConstants.Status );
      this.DisplayListView.Columns.Add( MacroscopeConstants.ContentType, MacroscopeConstants.ContentType );
      
      this.DisplayListView.Columns.Add( key: "Xpath_Label", text: "XPath Label" );
      this.DisplayListView.Columns.Add( key: "Xpath_Extracted", text: "Extracted Value" );
      
      for( int ColIndex = 0 ; ColIndex < this.DisplayListView.Columns.Count ; ColIndex++ )
      {
        this.DisplayListView.AutoResizeColumn( ColIndex, ColumnHeaderAutoResizeStyle.HeaderSize );
      }

    }
    
    /**************************************************************************/

    public void RefreshData (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList,
      MacroscopeDataExtractorXpaths DataExtractor
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
                DataExtractor: DataExtractor
              );
              this.RenderUrlCount();
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
          DataExtractor: DataExtractor
        );
        this.RenderUrlCount();
        this.DisplayListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
      
    }
   
    /**************************************************************************/

    private void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList,
      MacroscopeDataExtractorXpaths DataExtractor
    )
    {

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      
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

      foreach( string Url in UrlList )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocumentByUrl( Url: Url );
        string DocUrl = msDoc.GetUrl();
        string StatusCode = ( ( int )msDoc.GetStatusCode() ).ToString();
        string Status = msDoc.GetStatusCode().ToString();
        string MimeType = msDoc.GetMimeType();

        if( !DataExtractor.CanApplyDataExtractorsToDocument( msDoc: msDoc ) )
        {
          continue;
        }        

        foreach( KeyValuePair<string,string> DataExtractedPair in msDoc.IterateDataExtractedXpaths() )
        {

          ListViewItem lvItem = null;
          string XpathLabel = DataExtractedPair.Key;
          string ExtractedValue = DataExtractedPair.Value;
          string PairKey = null;

          if( 
            string.IsNullOrEmpty( XpathLabel )
            || string.IsNullOrEmpty( ExtractedValue ) )
          {
            continue;
          }

          PairKey = string.Join(
            ":",
            UrlToDigest( DocUrl ),
            UrlToDigest( Macroscope.GetStringDigest( Text: XpathLabel ) ),
            UrlToDigest( Macroscope.GetStringDigest( Text: ExtractedValue ) )
          );

          if( this.DisplayListView.Items.ContainsKey( PairKey ) )
          {

            lvItem = this.DisplayListView.Items[ PairKey ];

          }
          else
          {

            lvItem = new ListViewItem ( PairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = PairKey;

            for( int i = 0 ; i < 6 ; i++ )
              lvItem.SubItems.Add( "" );

            ListViewItems.Add( lvItem );

          }

          if( lvItem != null )
          {

            try
            {

              lvItem.SubItems[ ColUrl ].Text = DocUrl;
              lvItem.SubItems[ ColStatusCode ].Text = StatusCode;
              lvItem.SubItems[ ColStatus ].Text = Status;
              lvItem.SubItems[ ColMimeType ].Text = MimeType;
              lvItem.SubItems[ ColXpathLabel ].Text = XpathLabel;
              lvItem.SubItems[ ColExtractedValue ].Text = ExtractedValue;

            }
            catch( Exception ex )
            {
              DebugMsg( string.Format( "MacroscopeDisplayDataExtractorXpaths: {0}", ex.Message ) );
              DebugMsg( string.Format( "MacroscopeDisplayDataExtractorXpaths: {0}", ex.StackTrace ) );
            }

          }
          else
          {
            DebugMsg( string.Format( "MacroscopeDisplayDataExtractorXpaths MISSING: {0}", PairKey ) );
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
      this.DisplayListView.Columns[ ColMimeType ].Width = 100;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }

      if( ProgressForm != null )
      {
        ProgressForm.Dispose();
      }
      
    }

    /**************************************************************************/

    protected override void RenderListView (
      List<ListViewItem> ListViewItems,
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc,
      string Url
    )
    {
    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
      this.ItemCount.Text = string.Format( "Extracted Items: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/

  }

}
