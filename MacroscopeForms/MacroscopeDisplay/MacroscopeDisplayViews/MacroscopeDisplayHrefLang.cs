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

    private const int ColUrl = 0;
    private const int ColStatusCode = 1;
    private const int ColSiteLocale = 2;
    private const int ColHrefLangPresent = 3;
    private const int ColTitle = 4;
            
    /**************************************************************************/

    public MacroscopeDisplayHrefLang ( MacroscopeMainForm MainForm, ListView TargetListView )
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
              this.DisplayListView.BeginUpdate();
              this.RenderListView(
                DocCollection: DocCollection,
                LocalesList: LocalesList
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
          LocalesList: LocalesList
        );
        this.DisplayListView.EndUpdate();
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
      Hashtable LocaleColsTable = new Hashtable ();
      
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
      
      this.DisplayListView.Items.Clear();
      this.DisplayListView.Columns.Clear();

      {

        int LocaleColCount = 4;

        this.DisplayListView.Columns.Add( "URL", "URL" );
        this.DisplayListView.Columns.Add( "Status Code", "Status Code" );
        this.DisplayListView.Columns.Add( "Site Locale", "Site Locale" );
        this.DisplayListView.Columns.Add( "HrefLang Present", "HrefLang Present" );
        this.DisplayListView.Columns.Add( "Title", "Title" );

        foreach( string Locale in LocalesList.Keys )
        {
          this.DisplayListView.Columns.Add( Locale, Locale );
          LocaleColsTable[ Locale ] = LocaleColCount;
          LocaleColCount++;
        }

      }

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        Boolean Proceed = false;

        if( msDoc.GetIsInternal() )
        {

          Proceed = true;

          if( msDoc.GetIsRedirect() )
          {
            Proceed = false;
          }

          if( !msDoc.GetIsHtml() )
          {
            Proceed = false;
          }

        }

        if( Proceed )
        {

          Dictionary<string,MacroscopeHrefLang> HrefLangsTable = msDoc.GetHrefLangs();
          string DocUrl = msDoc.GetUrl();
          string PairKey = DocUrl;
          HttpStatusCode StatusCode = msDoc.GetStatusCode();
          int StatusCodeNum = ( int )StatusCode;
          MacroscopeConstants.Specifiers HrefLangPresent = MacroscopeConstants.Specifiers.UNSPECIFIED;
          string HrefLangPresentLabel = "UNSPECIFIED";
          string DocLocale = msDoc.GetLocale();
          string DocTitle = msDoc.GetTitle();
          ListViewItem lvItem = null;

          if( 
            ( HrefLangsTable != null )
            && ( HrefLangsTable.Count > 1 ) )
          {
            HrefLangPresent = MacroscopeConstants.Specifiers.SPECIFIED;
          }
          else
          {
            HrefLangPresent = MacroscopeConstants.Specifiers.UNSPECIFIED;
          }

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

              lvItem.SubItems[ ColUrl ].Text = DocUrl;
              lvItem.SubItems[ ColStatusCode ].Text = StatusCode.ToString();
              lvItem.SubItems[ ColSiteLocale ].Text = DocLocale;
              lvItem.SubItems[ ColHrefLangPresent ].Text = HrefLangPresentLabel;
              lvItem.SubItems[ ColTitle ].Text = DocTitle;

              switch( HrefLangPresent )
              {
                case MacroscopeConstants.Specifiers.SPECIFIED:
                  lvItem.SubItems[ ColHrefLangPresent ].ForeColor = Color.Green;
                  lvItem.SubItems[ ColHrefLangPresent ].Text = "SPECIFIED";
                  break;
                default:
                  lvItem.SubItems[ ColHrefLangPresent ].ForeColor = Color.Red;
                  lvItem.SubItems[ ColHrefLangPresent ].Text = "UNSPECIFIED";
                  break;
              }

              if( AllowedHosts.IsInternalUrl( DocUrl ) )
              {
                lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
              }
              else
              {
                lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
              }

              if( ( StatusCodeNum >= 100 ) && ( StatusCodeNum <= 299 ) )
              {
                lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Green;
              }
              else
              if( ( StatusCodeNum >= 300 ) && ( StatusCodeNum <= 399 ) )
              {
                lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Orange;
              }
              else
              if( ( StatusCodeNum >= 400 ) && ( StatusCodeNum <= 599 ) )
              {
                lvItem.SubItems[ ColStatusCode ].ForeColor = Color.Red;
              }
              else
              {
                lvItem.SubItems[ ColSiteLocale ].ForeColor = Color.Gray;
              }

              foreach( string Locale in LocalesList.Keys )
              {

                if( !string.IsNullOrEmpty( Locale ) )
                {

                  string HrefLangUrl = null;
                  int LocaleCol = ( int )LocaleColsTable[ Locale ];

                  if( 
                    ( HrefLangsTable != null )
                    && ( HrefLangsTable.Count > 0 ) )
                  {

                    if( HrefLangsTable.ContainsKey( Locale ) )
                    {

                      MacroscopeHrefLang msHrefLang = HrefLangsTable[ Locale ];

                      if( msHrefLang != null )
                      {
                        HrefLangUrl = msHrefLang.GetUrl();
                      }

                    }

                  }

                  if( !string.IsNullOrEmpty( HrefLangUrl ) )
                  {
                    lvItem.SubItems[ LocaleCol ].ForeColor = Color.Blue;
                    lvItem.SubItems[ LocaleCol ].Text = HrefLangUrl;
                  }
                  else
                  {
                    lvItem.SubItems[ LocaleCol ].ForeColor = Color.Red;
                    lvItem.SubItems[ LocaleCol ].Text = "NOT SPECIFIED";
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
            DebugMsg( string.Format( "MacroscopeDisplayHrefLang NOT SPECIFIED: {0}", PairKey ) );
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
      this.DisplayListView.Columns[ ColStatusCode ].Width = 80;
      this.DisplayListView.Columns[ ColSiteLocale ].Width = 100;
      this.DisplayListView.Columns[ ColTitle ].Width = 100;

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
