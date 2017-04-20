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
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        this.DisplayListView.Columns.Add( MacroscopeConstants.Lang, MacroscopeConstants.Lang );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Canonical, MacroscopeConstants.Canonical );
        
        this.DisplayListView.Columns.Add( MacroscopeConstants.Inlinks, MacroscopeConstants.Inlinks );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Outlinks, MacroscopeConstants.Outlinks );
        
        this.DisplayListView.Columns.Add( MacroscopeConstants.Inhyperlinks, MacroscopeConstants.Inhyperlinks );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Outhyperlinks, MacroscopeConstants.Outhyperlinks );
        
        this.DisplayListView.Columns.Add( MacroscopeConstants.Title, MacroscopeConstants.Title );
        this.DisplayListView.Columns.Add( MacroscopeConstants.TitleLen, MacroscopeConstants.TitleLen );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Description, MacroscopeConstants.Description );
        this.DisplayListView.Columns.Add( MacroscopeConstants.DescriptionLen, MacroscopeConstants.DescriptionLen );
        this.DisplayListView.Columns.Add( MacroscopeConstants.Keywords, MacroscopeConstants.Keywords );
        this.DisplayListView.Columns.Add( MacroscopeConstants.KeywordsLen, MacroscopeConstants.KeywordsLen );
        this.DisplayListView.Columns.Add( MacroscopeConstants.KeywordsCount, MacroscopeConstants.KeywordsCount );

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

        Hashtable htItems = new Hashtable ();
        ListViewItem lvItem = null;

        // BEGIN: Columns ----------------------------------------------------//

        htItems[ MacroscopeConstants.Url ] = msDoc.GetUrl();

        htItems[ MacroscopeConstants.StatusCode ] = ( ( int )msDoc.GetStatusCode() ).ToString();
        htItems[ MacroscopeConstants.Status ] = msDoc.GetStatusCode();
        htItems[ MacroscopeConstants.IsRedirect ] = msDoc.GetIsRedirect();

        htItems[ MacroscopeConstants.Duration ] = msDoc.GetDurationInSecondsFormatted();

        htItems[ MacroscopeConstants.ContentType ] = msDoc.GetMimeType();

        {
          string Lang = msDoc.GetLang();
          if( Lang == null )
          {
            Lang = "";
          }
          htItems[ MacroscopeConstants.Lang ] = Lang;
        }

        htItems[ MacroscopeConstants.DateCrawled ] = msDoc.GetCrawledDate();
        
        htItems[ MacroscopeConstants.DateServer ] = msDoc.GetDateServer();
        htItems[ MacroscopeConstants.DateModified ] = msDoc.GetDateModified();
        htItems[ MacroscopeConstants.DateExpires ] = msDoc.GetDateExpires();

        htItems[ MacroscopeConstants.Canonical ] = msDoc.GetCanonical();

        htItems[ MacroscopeConstants.Inlinks ] = msDoc.CountInlinks();
        htItems[ MacroscopeConstants.Outlinks ] = msDoc.CountOutlinks();
        
        htItems[ MacroscopeConstants.Inhyperlinks ] = msDoc.CountHyperlinksIn();
        htItems[ MacroscopeConstants.Outhyperlinks ] = msDoc.CountHyperlinksOut();

        htItems[ MacroscopeConstants.Title ] = msDoc.GetTitle();
        htItems[ MacroscopeConstants.TitleLen ] = msDoc.GetTitleLength();

        htItems[ MacroscopeConstants.Description ] = msDoc.GetDescription();
        htItems[ MacroscopeConstants.DescriptionLen ] = msDoc.GetDescriptionLength();

        htItems[ MacroscopeConstants.Keywords ] = msDoc.GetKeywords();
        htItems[ MacroscopeConstants.KeywordsLen ] = msDoc.GetKeywordsLength();
        htItems[ MacroscopeConstants.KeywordsCount ] = msDoc.GetKeywordsCount();

        for( ushort HeadingLevel = 1 ; HeadingLevel <= MaxHeadingsDisplayed ; HeadingLevel++ )
        {
          List<string> HeadingList = msDoc.GetHeadings( HeadingLevel: HeadingLevel );
          string HeadingText = "";
          if( HeadingList.Count > 0 )
          {
            HeadingText = HeadingList[ 0 ];
          }
          htItems[ string.Format( MacroscopeConstants.Hn, HeadingLevel ) ] = HeadingText;
        }

        htItems[ MacroscopeConstants.ErrorCondition ] = msDoc.GetErrorCondition();

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

          foreach( string Key in htItems.Keys )
          {
            lvItem.SubItems.Add( Key );
          }

          ListViewItems.Add( lvItem );

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          int iStatusColIndex = this.DisplayListView.Columns.IndexOfKey( MacroscopeConstants.Status );
                        
          foreach( string ItemsKey in htItems.Keys )
          {

            int iColIndex = this.DisplayListView.Columns.IndexOfKey( ItemsKey );
            string sText = htItems[ ItemsKey ].ToString();

            if( htItems[ ItemsKey ] != null )
            {
              lvItem.SubItems[ iColIndex ].Text = sText;
            }
            else
            {
              lvItem.SubItems[ iColIndex ].Text = "";
            }

            if( msDoc.GetIsInternal() )
            {
              lvItem.SubItems[ iColIndex ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ iColIndex ].ForeColor = Color.Gray;
            }

            if( ItemsKey == MacroscopeConstants.StatusCode )
            {
              if( Regex.IsMatch( sText, "^[2]" ) )
              {
                lvItem.SubItems[ iColIndex ].ForeColor = Color.Green;
                lvItem.SubItems[ iStatusColIndex ].ForeColor = Color.Green;
              }
              else
              if( Regex.IsMatch( sText, "^[3]" ) )
              {
                lvItem.SubItems[ iColIndex ].ForeColor = Color.Goldenrod;
                lvItem.SubItems[ iStatusColIndex ].ForeColor = Color.Goldenrod;
              }
              else
              if( Regex.IsMatch( sText, "^[45]" ) )
              {
                lvItem.SubItems[ iColIndex ].ForeColor = Color.Red;
                lvItem.SubItems[ iStatusColIndex ].ForeColor = Color.Red;
              }
              else
              {
                lvItem.SubItems[ iColIndex ].ForeColor = Color.Blue;
                lvItem.SubItems[ iStatusColIndex ].ForeColor = Color.Blue;
              }
            }

            if( ItemsKey == MacroscopeConstants.IsRedirect )
            {
              if( sText.ToLower() == "true" )
              {
                lvItem.SubItems[ iColIndex ].ForeColor = Color.Red;
              }
              else
              {
                lvItem.SubItems[ iColIndex ].ForeColor = Color.Gray;
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

      Dictionary<string,int> lColExplicitWidth = new Dictionary<string,int> () { {
          MacroscopeConstants.Url,
          300
        },
        {
          MacroscopeConstants.Title,
          300
        }
      };

      for( int iColIndex = 0 ; iColIndex < this.DisplayListView.Columns.Count ; iColIndex++ )
      {
        this.DisplayListView.AutoResizeColumn( iColIndex, ColumnHeaderAutoResizeStyle.HeaderSize );
      }

      foreach( string sColName in lColExplicitWidth.Keys )
      {
        this.DisplayListView.Columns[ sColName ].Width = lColExplicitWidth[ sColName ];
      }

    }

    /**************************************************************************/

    private void ListViewResizeColumns ()
    {

      List<string> lColDataWidth = new List<string> () {
        MacroscopeConstants.Url,
          MacroscopeConstants.DateServer,
        MacroscopeConstants.DateModified,
          MacroscopeConstants.Title
      };

      List<string> lColHeaderWidth = new List<string> () {
        MacroscopeConstants.DateModified
      };

      foreach( string sColName in lColDataWidth )
      {
        this.DisplayListView.AutoResizeColumn( this.DisplayListView.Columns[ sColName ].Index, ColumnHeaderAutoResizeStyle.ColumnContent );
      }

      foreach( string sColName in lColHeaderWidth )
      {
        this.DisplayListView.AutoResizeColumn( this.DisplayListView.Columns[ sColName ].Index, ColumnHeaderAutoResizeStyle.HeaderSize );
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
