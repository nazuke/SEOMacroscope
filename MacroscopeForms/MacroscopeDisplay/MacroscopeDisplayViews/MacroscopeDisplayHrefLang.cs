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
using System.Windows.Forms;
using System.Drawing;

namespace SEOMacroscope
{

  public class MacroscopeDisplayHrefLang : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayHrefLang ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
      : base( MainFormNew, lvListViewNew )
    {

      MainForm = MainFormNew;
      lvListView = lvListViewNew;

      if( MainForm.InvokeRequired )
      {
        MainForm.Invoke(
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
        this.lvListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    public void RefreshData (
      MacroscopeDocumentCollection DocCollection,
      Dictionary<string,string> LocalesList
    )
    {

      DebugMsg( string.Format( "MacroscopeDisplayHrefLang: {0}", "RefreshData" ) );

      if( this.MainForm.InvokeRequired )
      {
        MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.RenderListView( DocCollection: DocCollection, LocalesList: LocalesList );
            }
          )
        );
      }
      else
      {
        this.RenderListView( DocCollection, LocalesList );
      }

    }

    /**************************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
    {
    }

    /**************************************************************************/

    void RenderListView ( MacroscopeDocumentCollection DocCollection, Dictionary<string,string> LocalesList )
    {

      Hashtable htLocaleCols = new Hashtable ();

      this.lvListView.Items.Clear();
      this.lvListView.Columns.Clear();

      this.lvListView.BeginUpdate();

      {

        int iLocaleColCount = 3;

        this.lvListView.Columns.Add( "URL", "URL" );
        this.lvListView.Columns.Add( "Site Locale", "Site Locale" );
        this.lvListView.Columns.Add( "Title", "Title" );

        foreach( string sLocale in LocalesList.Keys )
        {
          this.lvListView.Columns.Add( sLocale, sLocale );
          htLocaleCols[ sLocale ] = iLocaleColCount;
          iLocaleColCount++;
        }

      }

      foreach( string sKeyUrl in DocCollection.DocumentKeys() )
      {

        MacroscopeDocument msDoc = DocCollection.GetDocument( sKeyUrl );
        Boolean bProceed = false;

        if( !msDoc.GetIsExternal() )
        {
          bProceed = true;
          if( !msDoc.GetIsHtml() )
          {
            bProceed = false;
          }
        }

        if( bProceed )
        {

          Dictionary<string,MacroscopeHrefLang> htHrefLangs = msDoc.GetHrefLangs();

          if( htHrefLangs != null )
          {

            string sDocUrl = msDoc.GetUrl();
            string sDocLocale = msDoc.GetLocale();
            string sDocTitle = msDoc.GetTitle();
            ListViewItem lvItem;

            if( this.lvListView.Items.ContainsKey( sKeyUrl ) )
            {

              lvItem = this.lvListView.Items[ sKeyUrl ];

            }
            else
            {

              lvItem = new ListViewItem ( sKeyUrl );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = sKeyUrl;

              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );

              for( int i = 0 ; i < LocalesList.Keys.Count ; i++ )
              {
                lvItem.SubItems.Add( "" );
              }

              this.lvListView.Items.Add( lvItem );

            }

            if( this.lvListView.Items.ContainsKey( sKeyUrl ) )
            {

              try
              {

                lvItem.SubItems[ 0 ].Text = sDocUrl;
                lvItem.SubItems[ 1 ].Text = sDocLocale;
                lvItem.SubItems[ 2 ].Text = sDocTitle;

                foreach( string sLocale in LocalesList.Keys )
                {

                  if( sLocale != null )
                  {

                    string sHrefLangUrl = null;
                    int iLocale = ( int )htLocaleCols[ sLocale ];

                    if( htHrefLangs.ContainsKey( sLocale ) )
                    {
                      MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[ sLocale ];
                      if( msHrefLang != null )
                      {
                        sHrefLangUrl = msHrefLang.GetUrl();
                      }
                    }

                    if( sHrefLangUrl != null )
                    {
                      lvItem.SubItems[ iLocale ].ForeColor = Color.Blue;
                      lvItem.SubItems[ iLocale ].Text = sHrefLangUrl;
                    }
                    else
                    {
                      lvItem.SubItems[ iLocale ].ForeColor = Color.Red;
                      lvItem.SubItems[ iLocale ].Text = "MISSING";

                    }

                  }

                }

              }
              catch( Exception ex )
              {
                DebugMsg( string.Format( "MacroscopeDisplayHrefLang: {0}", ex.Message ) );
                DebugMsg( string.Format( "MacroscopeDisplayHrefLang: {0}", ex.StackTrace ) );
              }

            }
            else
            {
              DebugMsg( string.Format( "MacroscopeDisplayHrefLang MISSING: {0}", sKeyUrl ) );
            }

          }

        }

      }

      this.lvListView.EndUpdate();

      this.lvListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );

      this.lvListView.Columns[ "Site Locale" ].Width = 100;
      this.lvListView.Columns[ "Title" ].Width = 300;
      this.lvListView.Columns[ "URL" ].Width = 300;

    }

    /**************************************************************************/

  }

}
