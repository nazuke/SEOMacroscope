/*

  This file is part of SEOMacroscope.

  Copyright 2018 Jason Holland.

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

  public sealed class MacroscopeDisplayPageText: MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColUrl = 0;
    private const int ColLocale = 1;
    private const int ColPageLanguage = 2;
    private const int ColDetectedLanguage = 3;
    private const int ColWordCount = 4;
    private const int ColReadabilityGradeType = 5;
    private const int ColReadabilityGrade = 6;
    private const int ColReadabilityGradeDescription = 7;
    
    /**************************************************************************/

    public MacroscopeDisplayPageText ( MacroscopeMainForm MainForm, ListView TargetListView )
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
        this.ListViewConfigured = true;
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

      bool Proceed = false;
      
      if( msDoc.GetIsExternal() )
      {
        return;
      }
            
      if( msDoc.GetIsRedirect() )
      {
        return;
      }

      switch ( msDoc.GetDocumentType() )
      {
        case MacroscopeConstants.DocumentType.HTML:
          Proceed = true;
          break;
        case MacroscopeConstants.DocumentType.PDF:
          Proceed = true;
          break;
        default:
          break;
      }
      
      if ( Proceed )
      {

        string PageLocale = msDoc.GetLocale();
        string PageLanguage = msDoc.GetIsoLanguageCode();
        string DetectedLanguage = msDoc.GetDocumentTextLanguage();
        int WordCount = msDoc.GetWordCount();
        string ReadabilityGradeType = MacroscopeAnalyzeReadability.FormatAnalyzeReadabilityMethod(
                                        ReadabilityMethod: msDoc.GetReadabilityGradeMethod()
                                      );
        string ReadabilityGrade = msDoc.GetReadabilityGrade().ToString( "00.00" );
        string ReadabilityGradeDescription = msDoc.GetReadabilityGradeDescription();
        string PairKey = string.Join( "", Url );
        ListViewItem lvItem = null;

        if( string.IsNullOrEmpty( PageLocale ) )
        {
          PageLocale = "";
        }
        
        if( string.IsNullOrEmpty( PageLanguage ) )
        {
          PageLanguage = "";
        }
        
        if( string.IsNullOrEmpty( DetectedLanguage ) )
        {
          DetectedLanguage = "";
        }

        if( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];
            lvItem.SubItems[ ColUrl ].Text = Url;
            lvItem.SubItems[ ColLocale ].Text = PageLocale;
            lvItem.SubItems[ ColPageLanguage ].Text = PageLanguage;
            lvItem.SubItems[ ColDetectedLanguage ].Text = DetectedLanguage;
            lvItem.SubItems[ ColWordCount ].Text = WordCount.ToString();
            lvItem.SubItems[ ColReadabilityGradeType ].Text = ReadabilityGradeType;
            lvItem.SubItems[ ColReadabilityGrade ].Text = ReadabilityGrade;
            lvItem.SubItems[ ColReadabilityGradeDescription ].Text = ReadabilityGradeDescription;
            
          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayPageText 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem ( PairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = PairKey;

            lvItem.SubItems[ ColUrl ].Text = Url;
            lvItem.SubItems.Add( PageLocale );     
            lvItem.SubItems.Add( PageLanguage );
            lvItem.SubItems.Add( DetectedLanguage );
            lvItem.SubItems.Add( WordCount.ToString() );
            lvItem.SubItems.Add( ReadabilityGradeType );
            lvItem.SubItems.Add( ReadabilityGrade );
            lvItem.SubItems.Add( ReadabilityGradeDescription );

            ListViewItems.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayPageText 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          // URL -------------------------------------------------------------//
          
          if( msDoc.GetIsInternal() )
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ ColUrl ].ForeColor = Color.Gray;
          }

          // Page Locale -----------------------------------------------------//
          
          if( msDoc.GetIsInternal() )
          {
            lvItem.SubItems[ ColLocale ].ForeColor = Color.Green;
          }
          else
          {
            lvItem.SubItems[ ColLocale ].ForeColor = Color.Gray;
          }

          // Page Language ---------------------------------------------------//

          if( msDoc.GetIsInternal() )
          {

            lvItem.SubItems[ ColPageLanguage ].ForeColor = Color.Green;
            lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Green;
            
            if( DetectedLanguage != PageLanguage )
            {
              lvItem.SubItems[ ColPageLanguage ].ForeColor = Color.Red;
              lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Red;
            }

          }
          else
          {
            lvItem.SubItems[ ColPageLanguage ].ForeColor = Color.Gray;
            lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Gray;
          }

          // Word Count ------------------------------------------------------//

          if( msDoc.GetIsInternal() )
          {

            if( WordCount > 0 )
            {
              lvItem.SubItems[ ColWordCount ].ForeColor = Color.Green;
            }
            else
            {
              lvItem.SubItems[ ColWordCount ].ForeColor = Color.Red;
            }

          }
          else
          {
            lvItem.SubItems[ ColWordCount ].ForeColor = Color.Gray;
          }

        }

      }

    }

    /**************************************************************************/

    protected override void RenderUrlCount ()
    {
    }

    /**************************************************************************/

  }

}
