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
              Cursor.Current = Cursors.WaitCursor;
              this.lvListView.BeginUpdate();
              this.RenderListView(
                DocCollection: DocCollection,
                LocalesList: LocalesList
              );
              this.lvListView.EndUpdate();
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.lvListView.BeginUpdate();
        this.RenderListView(
          DocCollection: DocCollection,
          LocalesList: LocalesList
        );
        this.lvListView.EndUpdate();
        Cursor.Current = Cursors.Default;
      }
    }

    /**************************************************************************/

    private void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      Dictionary<string,string> LocalesList
    )
    {

      MacroscopeAllowedHosts AllowedHosts = this.MainForm.GetJobMaster().GetAllowedHosts();
      Hashtable htLocaleCols = new Hashtable ();
      
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
      
      this.lvListView.Items.Clear();
      this.lvListView.Columns.Clear();

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

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        Boolean Proceed = false;

        if( msDoc.GetIsInternal() )
        {

          Proceed = true;

          if( !msDoc.GetIsHtml() )
          {
            Proceed = false;
          }

        }

        if( Proceed )
        {

          Dictionary<string,MacroscopeHrefLang> HrefLangsTable = msDoc.GetHrefLangs();

          if( HrefLangsTable != null )
          {

            string DocUrl = msDoc.GetUrl();
            string PairKey = DocUrl;
            HttpStatusCode StatusCode = msDoc.GetStatusCode();
            string DocLocale = msDoc.GetLocale();
            string DocTitle = msDoc.GetTitle();
            ListViewItem lvItem = null;

            if( this.lvListView.Items.ContainsKey( PairKey ) )
            {

              lvItem = this.lvListView.Items[ PairKey ];

            }
            else
            {

              lvItem = new ListViewItem ( PairKey );
              lvItem.UseItemStyleForSubItems = false;
              lvItem.Name = PairKey;

              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );
              lvItem.SubItems.Add( "" );

              for( int i = 0 ; i < LocalesList.Keys.Count ; i++ )
              {
                lvItem.SubItems.Add( "" );
              }

              ListViewItems.Add( lvItem );

            }

            if( lvItem != null )
            {

              try
              {

                lvItem.SubItems[ 0 ].Text = DocUrl;
                lvItem.SubItems[ 1 ].Text = StatusCode.ToString();
                lvItem.SubItems[ 2 ].Text = DocLocale;
                lvItem.SubItems[ 3 ].Text = DocTitle;
               
                if( AllowedHosts.IsInternalUrl( DocUrl ) )
                {
                  lvItem.SubItems[ 0 ].ForeColor = Color.Green;
                }
                else
                {
                  lvItem.SubItems[ 0 ].ForeColor = Color.Gray;
                }

                if( ( ( int )StatusCode >= 100 ) && ( ( int )StatusCode <= 299 ) )
                {
                  lvItem.SubItems[ 1 ].ForeColor = Color.Green;
                }
                else
                if( ( ( int )StatusCode >= 300 ) && ( ( int )StatusCode <= 399 ) )
                {
                  lvItem.SubItems[ 1 ].ForeColor = Color.Orange;
                }
                else
                if( ( ( int )StatusCode >= 400 ) && ( ( int )StatusCode <= 599 ) )
                {
                  lvItem.SubItems[ 1 ].ForeColor = Color.Red;
                }
                else
                {
                  lvItem.SubItems[ 2 ].ForeColor = Color.Gray;
                }

                foreach( string Locale in LocalesList.Keys )
                {

                  if( Locale != null )
                  {

                    string HrefLangUrl = null;
                    int iLocale = ( int )htLocaleCols[ Locale ];

                    if( HrefLangsTable.ContainsKey( Locale ) )
                    {
                      MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )HrefLangsTable[ Locale ];
                      if( msHrefLang != null )
                      {
                        HrefLangUrl = msHrefLang.GetUrl();
                      }
                    }

                    if( HrefLangUrl != null )
                    {
                      lvItem.SubItems[ iLocale ].ForeColor = Color.Blue;
                      lvItem.SubItems[ iLocale ].Text = HrefLangUrl;
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
              DebugMsg( string.Format( "MacroscopeDisplayHrefLang MISSING: {0}", PairKey ) );
            }

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

      this.lvListView.Items.AddRange( ListViewItems.ToArray() );

      this.lvListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );

      this.lvListView.Columns[ "Site Locale" ].Width = 100;
      this.lvListView.Columns[ "Status Code" ].Width = 80;
      this.lvListView.Columns[ "Title" ].Width = 300;
      this.lvListView.Columns[ "URL" ].Width = 300;

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
