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
using System.Net;
using System.Windows.Forms;
using System.Drawing;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayHrefLang : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayHrefLang ( MacroscopeMainForm MainForm, ListView lvListView )
      : base( MainForm, lvListView )
    {

      this.MainForm = MainForm;
      this.lvListView = lvListView;

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
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
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

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
    {
    }

    /**************************************************************************/

    private void RenderListView ( MacroscopeDocumentCollection DocCollection, Dictionary<string,string> LocalesList )
    {

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      Hashtable htLocaleCols = new Hashtable ();

      this.lvListView.Items.Clear();
      this.lvListView.Columns.Clear();

      this.lvListView.BeginUpdate();

      {

        int iLocaleColCount = 4;

        this.lvListView.Columns.Add( "URL", "URL" );
        this.lvListView.Columns.Add( "Status Code", "Status Code" );
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
            HttpStatusCode StatusCode = msDoc.GetStatusCode();
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
                lvItem.SubItems[ 1 ].Text = StatusCode.ToString();
                lvItem.SubItems[ 2 ].Text = sDocLocale;
                lvItem.SubItems[ 3 ].Text = sDocTitle;
               
                if( AllowedHosts.IsInternalUrl( sDocUrl ) )
                {
                  lvItem.SubItems[ 0 ].ForeColor = Color.Green;
                }
                else
                {
                  lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
                }

                if( ( (int)StatusCode >= 100 ) && ( (int)StatusCode <= 299 ) )
                {
                  lvItem.SubItems[ 1 ].ForeColor = Color.Green;
                }
                else
                if( ( (int)StatusCode >= 300 ) && ( (int)StatusCode <= 399 ) )
                {
                  lvItem.SubItems[ 1 ].ForeColor = Color.Orange;
                }
                else
                if( ( (int)StatusCode >= 400 ) && ( (int)StatusCode <= 599 ) )
                {
                  lvItem.SubItems[ 1 ].ForeColor = Color.Red;
                }
                else
                {
                  lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
                }

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
      this.lvListView.Columns[ "Status Code" ].Width = 80;
      this.lvListView.Columns[ "Title" ].Width = 300;
      this.lvListView.Columns[ "URL" ].Width = 300;

    }

    /**************************************************************************/

  }

}
