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
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayStructure : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int MaxHeadingsDisplayed = 2;
    private ToolStripLabel DocumentCount;

    /**************************************************************************/

    public MacroscopeDisplayStructure ( MacroscopeMainForm MainForm, ListView TargetListView )
      : base( MainForm, TargetListView )
    {

      this.SuppressDebugMsg = true;
      
      this.MainForm = MainForm;
      this.DisplayListView = TargetListView;
      this.DocumentCount = this.MainForm.macroscopeOverviewTabPanelInstance.toolStripLabelStructureItems;
      
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

        this.DisplayListView.SuspendLayout();
        
        // BEGIN: Columns

        this.DisplayListView.Columns.Add( MacroscopeConstants.Url, MacroscopeConstants.Url );
        this.DisplayListView.Columns.Add( MacroscopeConstants.StatusCode, MacroscopeConstants.StatusCode );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Status, MacroscopeConstants.Status );
        this.DisplayListView.Columns.Add( MacroscopeConstants.IsRedirect, MacroscopeConstants.IsRedirect );

        this.DisplayListView.Columns.Add( MacroscopeConstants.Duration, MacroscopeConstants.Duration );

        this.DisplayListView.Columns.Add( MacroscopeConstants.DateCrawled, MacroscopeConstants.DateCrawled );
        this.DisplayListView.Columns.Add( MacroscopeConstants.DateServer, MacroscopeConstants.DateServer );
        this.DisplayListView.Columns.Add( MacroscopeConstants.DateModified, MacroscopeConstants.DateModified );
        this.DisplayListView.Columns.Add( MacroscopeConstants.DateExpires, MacroscopeConstants.DateExpires );
       
        this.DisplayListView.Columns.Add( MacroscopeConstants.ContentType, MacroscopeConstants.ContentType );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Locale, MacroscopeConstants.Locale );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Language, MacroscopeConstants.Language );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Canonical, MacroscopeConstants.Canonical );
        
        this.DisplayListView.Columns.Add( MacroscopeConstants.PageDepth, MacroscopeConstants.PageDepth );

        this.DisplayListView.Columns.Add( MacroscopeConstants.Inlinks, MacroscopeConstants.Inlinks );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Outlinks, MacroscopeConstants.Outlinks );
        
        this.DisplayListView.Columns.Add( MacroscopeConstants.Inhyperlinks, MacroscopeConstants.Inhyperlinks );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Outhyperlinks, MacroscopeConstants.Outhyperlinks );
        
        this.DisplayListView.Columns.Add( MacroscopeConstants.Title, MacroscopeConstants.Title );
        this.DisplayListView.Columns.Add( MacroscopeConstants.TitleLen, MacroscopeConstants.TitleLen );
        this.DisplayListView.Columns.Add( MacroscopeConstants.TitleLang, MacroscopeConstants.TitleLang );

        this.DisplayListView.Columns.Add( MacroscopeConstants.Description, MacroscopeConstants.Description );
        this.DisplayListView.Columns.Add( MacroscopeConstants.DescriptionLen, MacroscopeConstants.DescriptionLen );
        this.DisplayListView.Columns.Add( MacroscopeConstants.DescriptionLang, MacroscopeConstants.DescriptionLang );

        this.DisplayListView.Columns.Add( MacroscopeConstants.Keywords, MacroscopeConstants.Keywords );
        this.DisplayListView.Columns.Add( MacroscopeConstants.KeywordsLen, MacroscopeConstants.KeywordsLen );
        this.DisplayListView.Columns.Add( MacroscopeConstants.KeywordsCount, MacroscopeConstants.KeywordsCount );

        this.DisplayListView.Columns.Add( MacroscopeConstants.BodyTextLang, MacroscopeConstants.BodyTextLang );
        
        for( ushort HeadingLevel = 1 ; HeadingLevel <= MaxHeadingsDisplayed ; HeadingLevel++ )
        {
          string HeadingLevelText = string.Format( MacroscopeConstants.Hn, HeadingLevel );
          this.DisplayListView.Columns.Add( HeadingLevelText, HeadingLevelText );
        }

        this.DisplayListView.Columns.Add( MacroscopeConstants.ErrorCondition, MacroscopeConstants.ErrorCondition );

        // END: Columns

        this.ListViewResizeColumnsInitial();

        this.DisplayListView.ResumeLayout();
        
        this.ListViewConfigured = true;

      }

    }

    /**************************************************************************/

    public new void ClearData ()
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              base.ClearData();
              this.RenderUrlCount();

            }
          )
        );
      }
      else
      {
        base.ClearData();
        this.RenderUrlCount();
      }
    }

    /** Render One ************************************************************/

    protected override void RenderListView (
      List<ListViewItem> ListViewItems,
      MacroscopeDocument msDoc,
      string Url
    )
    {

      lock( this.DisplayListView )
      {

        Dictionary <string,string> StructureItems = new Dictionary <string,string> ();
        
        ListViewItem lvItem = null;

        string TitleLanguage = msDoc.GetTitleLanguage();
        string DescriptionLanguage = msDoc.GetDescriptionLanguage();
        string BodyTextLanguage = msDoc.GetDocumentTextLanguage();
        int StatusCode = ( int )msDoc.GetStatusCode();
        
        if( string.IsNullOrEmpty( TitleLanguage ) )
        {
          TitleLanguage = "";
        }
        
        if( string.IsNullOrEmpty( DescriptionLanguage ) )
        {
          DescriptionLanguage = "";
        }
        
        if( string.IsNullOrEmpty( BodyTextLanguage ) )
        {
          BodyTextLanguage = "";
        }
                
        // BEGIN: Columns ----------------------------------------------------//

        StructureItems.Add( MacroscopeConstants.Url, msDoc.GetUrl() );

        StructureItems.Add( MacroscopeConstants.StatusCode, StatusCode.ToString() );
        StructureItems.Add( MacroscopeConstants.Status, msDoc.GetStatusCode().ToString() );
        StructureItems.Add( MacroscopeConstants.IsRedirect, msDoc.GetIsRedirect().ToString() );

        StructureItems.Add( MacroscopeConstants.Duration, msDoc.GetDurationInSecondsFormatted() );

        StructureItems.Add( MacroscopeConstants.ContentType, msDoc.GetMimeType() );

        {
          string LocaleCode = msDoc.GetLocale();
          if( string.IsNullOrEmpty( LocaleCode ) )
          {
            LocaleCode = "";
          }
          StructureItems.Add( MacroscopeConstants.Locale, LocaleCode );
        }
        
        {
          string LanguageCode = msDoc.GetIsoLanguageCode();
          if( string.IsNullOrEmpty( LanguageCode ) )
          {
            LanguageCode = "";
          }
          StructureItems.Add( MacroscopeConstants.Language, LanguageCode );
        }

        StructureItems.Add( MacroscopeConstants.DateCrawled, msDoc.GetCrawledDate() );
        
        StructureItems.Add( MacroscopeConstants.DateServer, msDoc.GetDateServer() );
        StructureItems.Add( MacroscopeConstants.DateModified, msDoc.GetDateModified() );
        StructureItems.Add( MacroscopeConstants.DateExpires, msDoc.GetDateExpires() );

        StructureItems.Add( MacroscopeConstants.Canonical, msDoc.GetCanonical() );
        
        StructureItems.Add( MacroscopeConstants.PageDepth, msDoc.GetDepth().ToString() );

        StructureItems.Add( MacroscopeConstants.Inlinks, msDoc.CountInlinks().ToString() );
        StructureItems.Add( MacroscopeConstants.Outlinks, msDoc.CountOutlinks().ToString() );
        
        StructureItems.Add( MacroscopeConstants.Inhyperlinks, msDoc.CountHyperlinksIn().ToString() );
        StructureItems.Add( MacroscopeConstants.Outhyperlinks, msDoc.CountHyperlinksOut().ToString() );

        StructureItems.Add( MacroscopeConstants.Title, msDoc.GetTitle() );
        StructureItems.Add( MacroscopeConstants.TitleLen, msDoc.GetTitleLength().ToString() );
        StructureItems.Add( MacroscopeConstants.TitleLang, TitleLanguage );

        StructureItems.Add( MacroscopeConstants.Description, msDoc.GetDescription() );
        StructureItems.Add( MacroscopeConstants.DescriptionLen, msDoc.GetDescriptionLength().ToString() );
        StructureItems.Add( MacroscopeConstants.DescriptionLang, DescriptionLanguage );

        StructureItems.Add( MacroscopeConstants.Keywords, msDoc.GetKeywords() );
        StructureItems.Add( MacroscopeConstants.KeywordsLen, msDoc.GetKeywordsLength().ToString() );
        StructureItems.Add( MacroscopeConstants.KeywordsCount, msDoc.GetKeywordsCount().ToString() );
        
        StructureItems.Add( MacroscopeConstants.BodyTextLang, BodyTextLanguage );

        for( ushort HeadingLevel = 1 ; HeadingLevel <= MaxHeadingsDisplayed ; HeadingLevel++ )
        {
          List<string> HeadingList = msDoc.GetHeadings( HeadingLevel: HeadingLevel );
          string HeadingText = "";
          if( HeadingList.Count > 0 )
          {
            HeadingText = HeadingList[ 0 ];
          }
          StructureItems.Add( string.Format( MacroscopeConstants.Hn, HeadingLevel ), HeadingText );
        }

        StructureItems.Add( MacroscopeConstants.ErrorCondition, msDoc.GetErrorCondition() );

        // END: Columns ------------------------------------------------------//

        if( this.DisplayListView.Items.ContainsKey( Url ) )
        {

          lvItem = this.DisplayListView.Items[ Url ];

        }
        else
        {

          lvItem = new ListViewItem ( Url );
          lvItem.UseItemStyleForSubItems = false;
          lvItem.Name = Url;

          for( int i = 0 ; i < this.DisplayListView.Columns.Count ; i++ )
          {
            lvItem.SubItems.Add( "" );
          }

          ListViewItems.Add( lvItem );

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          int StatusCodeColIndex = this.DisplayListView.Columns.IndexOfKey( MacroscopeConstants.StatusCode );
          int StatusColIndex = this.DisplayListView.Columns.IndexOfKey( MacroscopeConstants.Status );
                        
          foreach( string ItemsKey in StructureItems.Keys )
          {

            int ColIndex = this.DisplayListView.Columns.IndexOfKey( ItemsKey );
            string Text = StructureItems[ ItemsKey ];

            if( !string.IsNullOrEmpty( StructureItems[ ItemsKey ] ) )
            {
              lvItem.SubItems[ ColIndex ].Text = Text;
            }
            else
            {
              lvItem.SubItems[ ColIndex ].Text = "";
            }

            if( msDoc.GetIsInternal() )
            {
              lvItem.SubItems[ ColIndex ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ ColIndex ].ForeColor = Color.Gray;
            }

            if( ItemsKey.Equals( MacroscopeConstants.StatusCode ) )
            {

              if( ( StatusCode >= 200 ) && ( StatusCode <= 299 ) )
              {
                lvItem.SubItems[ ColIndex ].ForeColor = Color.Green;
                lvItem.SubItems[ StatusCodeColIndex ].ForeColor = Color.Green;
                lvItem.SubItems[ StatusColIndex ].ForeColor = Color.Green;
              }
              else
              if( ( StatusCode >= 300 ) && ( StatusCode <= 399 ) )
              {
                lvItem.SubItems[ ColIndex ].ForeColor = Color.Goldenrod;
                lvItem.SubItems[ StatusCodeColIndex ].ForeColor = Color.Goldenrod;
                lvItem.SubItems[ StatusColIndex ].ForeColor = Color.Goldenrod;
              }
              else
              if( ( StatusCode >= 400 ) && ( StatusCode <= 599 ) )
              {
                lvItem.SubItems[ ColIndex ].ForeColor = Color.Red;
                lvItem.SubItems[ StatusCodeColIndex ].ForeColor = Color.Red;
                lvItem.SubItems[ StatusColIndex ].ForeColor = Color.Red;
              }
              else
              {
                lvItem.SubItems[ ColIndex ].ForeColor = Color.Blue;
                lvItem.SubItems[ StatusCodeColIndex ].ForeColor = Color.Blue;
                lvItem.SubItems[ StatusColIndex ].ForeColor = Color.Blue;
              }
            }

            if( ItemsKey == MacroscopeConstants.IsRedirect )
            {
              if( Text.ToLower() == "true" )
              {
                lvItem.SubItems[ ColIndex ].ForeColor = Color.Red;
              }
              else
              {
                lvItem.SubItems[ ColIndex ].ForeColor = Color.Gray;
              }
            }

          }

        }
        else
        {
          DebugMsg( string.Format( "MacroscopeDisplayStructure: {0}", "lvItem is NULL" ) );
        }

      }

    }

    /**************************************************************************/

    private void ListViewResizeColumnsInitial ()
    {

      Dictionary<string,int> ColExplicitWidth = new Dictionary<string,int> ( 2 );

      ColExplicitWidth.Add( MacroscopeConstants.Url, 300 );
      ColExplicitWidth.Add( MacroscopeConstants.Title, 300 );

      for( int ColIndex = 0 ; ColIndex < this.DisplayListView.Columns.Count ; ColIndex++ )
      {
        this.DisplayListView.AutoResizeColumn( ColIndex, ColumnHeaderAutoResizeStyle.HeaderSize );
      }

      foreach( string ColName in ColExplicitWidth.Keys )
      {
        this.DisplayListView.Columns[ ColName ].Width = ColExplicitWidth[ ColName ];
      }

    }

    /**************************************************************************/

    private void ListViewResizeColumns ()
    {

      List<string> ColDataWidth = new List<string> ( 4 );
      List<string> ColHeaderWidth = new List<string> ( 3 );
      
      ColDataWidth.Add( MacroscopeConstants.Url );
      ColDataWidth.Add( MacroscopeConstants.DateServer );
      ColDataWidth.Add( MacroscopeConstants.DateModified );
      ColDataWidth.Add( MacroscopeConstants.Title );

      ColHeaderWidth.Add( MacroscopeConstants.DateModified );
      ColHeaderWidth.Add( MacroscopeConstants.DateServer );
      ColHeaderWidth.Add( MacroscopeConstants.DateExpires );

      foreach( string ColName in ColDataWidth )
      {
        this.DisplayListView.AutoResizeColumn(
          this.DisplayListView.Columns[ ColName ].Index,
          ColumnHeaderAutoResizeStyle.ColumnContent
        );
      }

      foreach( string ColName in ColHeaderWidth )
      {
        this.DisplayListView.AutoResizeColumn(
          this.DisplayListView.Columns[ ColName ].Index, 
          ColumnHeaderAutoResizeStyle.HeaderSize
        );
      }

    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
      this.DocumentCount.Text = string.Format( "Documents: {0}", this.DisplayListView.Items.Count );
    }

    /**************************************************************************/
  
  }

}
