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

    public MacroscopeDisplayStructure ( MacroscopeMainForm MainForm, ListView lvListView )
      : base( MainForm, lvListView )
    {

      this.MainForm = MainForm;
      this.lvListView = lvListView;
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

        // BEGIN: Columns

        this.lvListView.Columns.Add( MacroscopeConstants.Url, MacroscopeConstants.Url );
        this.lvListView.Columns.Add( MacroscopeConstants.StatusCode, MacroscopeConstants.StatusCode );
        this.lvListView.Columns.Add( MacroscopeConstants.Status, MacroscopeConstants.Status );
        this.lvListView.Columns.Add( MacroscopeConstants.IsRedirect, MacroscopeConstants.IsRedirect );

        this.lvListView.Columns.Add( MacroscopeConstants.Duration, MacroscopeConstants.Duration );

        this.lvListView.Columns.Add( MacroscopeConstants.DateServer, MacroscopeConstants.DateServer );
        this.lvListView.Columns.Add( MacroscopeConstants.DateModified, MacroscopeConstants.DateModified );

        this.lvListView.Columns.Add( MacroscopeConstants.ContentType, MacroscopeConstants.ContentType );
        this.lvListView.Columns.Add( MacroscopeConstants.Lang, MacroscopeConstants.Lang );
        this.lvListView.Columns.Add( MacroscopeConstants.Canonical, MacroscopeConstants.Canonical );
        
        this.lvListView.Columns.Add( MacroscopeConstants.Inlinks, MacroscopeConstants.Inlinks );
        this.lvListView.Columns.Add( MacroscopeConstants.Outlinks, MacroscopeConstants.Outlinks );
        
        this.lvListView.Columns.Add( MacroscopeConstants.Inhyperlinks, MacroscopeConstants.Inhyperlinks );
        this.lvListView.Columns.Add( MacroscopeConstants.Outhyperlinks, MacroscopeConstants.Outhyperlinks );
        
        this.lvListView.Columns.Add( MacroscopeConstants.Title, MacroscopeConstants.Title );
        this.lvListView.Columns.Add( MacroscopeConstants.TitleLen, MacroscopeConstants.TitleLen );
        this.lvListView.Columns.Add( MacroscopeConstants.Description, MacroscopeConstants.Description );
        this.lvListView.Columns.Add( MacroscopeConstants.DescriptionLen, MacroscopeConstants.DescriptionLen );
        this.lvListView.Columns.Add( MacroscopeConstants.Keywords, MacroscopeConstants.Keywords );
        this.lvListView.Columns.Add( MacroscopeConstants.KeywordsLen, MacroscopeConstants.KeywordsLen );
        this.lvListView.Columns.Add( MacroscopeConstants.KeywordsCount, MacroscopeConstants.KeywordsCount );

        for( ushort iLevel = 1 ; iLevel <= MaxHeadingsDisplayed ; iLevel++ )
        {
          string sHeadingLevel = string.Format( MacroscopeConstants.Hn, iLevel );
          this.lvListView.Columns.Add( sHeadingLevel, sHeadingLevel );
        }

        this.lvListView.Columns.Add( MacroscopeConstants.ErrorCondition, MacroscopeConstants.ErrorCondition );

        // END: Columns

        this.ListViewResizeColumnsInitial();

        this.ListViewConfigured = true;

      }

    }

    /** Render One ************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
    {

      lock( this.lvListView )
      {

        Hashtable htItems = new Hashtable ();
        ListViewItem lvItem = null;

        // BEGIN: Columns

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

        htItems[ MacroscopeConstants.DateServer ] = msDoc.GetDateServer();
        htItems[ MacroscopeConstants.DateModified ] = msDoc.GetDateModified();

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

        for( ushort iLevel = 1 ; iLevel <= MaxHeadingsDisplayed ; iLevel++ )
        {
          List<string> aHeadings = msDoc.GetHeadings( iLevel );
          string sText = "";
          if( aHeadings.Count > 0 )
          {
            sText = aHeadings[ 0 ];
          }
          htItems[ string.Format( MacroscopeConstants.Hn, iLevel ) ] = sText;
        }

        htItems[ MacroscopeConstants.ErrorCondition ] = msDoc.GetErrorCondition();

        // END: Columns

        this.lvListView.BeginUpdate();

        if( this.lvListView.Items.ContainsKey( Url ) )
        {
          lvItem = this.lvListView.Items[ Url ];
        }
        else
        {
          lvItem = new ListViewItem ( Url );
          lvItem.Name = Url;
          foreach( string sKey in htItems.Keys )
          {
            lvItem.SubItems.Add( sKey );
          }
          this.lvListView.Items.Add( lvItem );
        }

        if( lvItem != null )
        {

          lvItem.UseItemStyleForSubItems = false;
          lvItem.ForeColor = Color.Blue;

          int iStatusColIndex = this.lvListView.Columns.IndexOfKey( MacroscopeConstants.Status );
                        
          foreach( string sKey in htItems.Keys )
          {

            int iColIndex = this.lvListView.Columns.IndexOfKey( sKey );
            string sText = htItems[ sKey ].ToString();

            if( htItems[ sKey ] != null )
            {
              lvItem.SubItems[ iColIndex ].Text = sText;
            }
            else
            {
              lvItem.SubItems[ iColIndex ].Text = "";
            }

            if( !msDoc.GetIsExternal() )
            {
              lvItem.SubItems[ iColIndex ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ iColIndex ].ForeColor = Color.Gray;
            }

            if( sKey == MacroscopeConstants.StatusCode )
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

            if( sKey == MacroscopeConstants.IsRedirect )
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

        this.DocumentCount.Text = string.Format( "Documents: {0}", lvListView.Items.Count );

        this.lvListView.EndUpdate();

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

      for( int iColIndex = 0 ; iColIndex < this.lvListView.Columns.Count ; iColIndex++ )
      {
        this.lvListView.AutoResizeColumn( iColIndex, ColumnHeaderAutoResizeStyle.HeaderSize );
      }

      foreach( string sColName in lColExplicitWidth.Keys )
      {
        this.lvListView.Columns[ sColName ].Width = lColExplicitWidth[ sColName ];
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
        this.lvListView.AutoResizeColumn( this.lvListView.Columns[ sColName ].Index, ColumnHeaderAutoResizeStyle.ColumnContent );
      }

      foreach( string sColName in lColHeaderWidth )
      {
        this.lvListView.AutoResizeColumn( this.lvListView.Columns[ sColName ].Index, ColumnHeaderAutoResizeStyle.HeaderSize );
      }

    }

    /**************************************************************************/

  }

}
