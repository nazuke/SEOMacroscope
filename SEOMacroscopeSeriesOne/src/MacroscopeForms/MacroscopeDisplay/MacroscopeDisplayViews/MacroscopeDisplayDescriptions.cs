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

  /// <summary>
  /// Description of MacroscopeDisplayDescriptions.
  /// </summary>

  public sealed class MacroscopeDisplayDescriptions : MacroscopeDisplayListView
  {

    /**************************************************************************/

    private const int ColUrl = 0;
    private const int ColPageLanguage = 1;
    private const int ColDetectedLanguage = 2;
    private const int ColOccurences = 3;
    private const int ColDescriptionText = 4;
    private const int ColLength = 5;
    
    /**************************************************************************/

    public MacroscopeDisplayDescriptions ( MacroscopeMainForm MainForm, ListView TargetListView )
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
      MacroscopeDocument msDoc,
      string Url
    )
    {

      Boolean Proceed = false;

      if( msDoc.GetIsExternal() )
      {
        return;
      }

      if( msDoc.GetIsRedirect() )
      {
        return;
      }
            
      if( msDoc.GetIsHtml() )
      {
        Proceed = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        Proceed = true;
      }

      if( Proceed )
      {

        MacroscopeDocumentCollection DocCollection = this.MainForm.GetJobMaster().GetDocCollection();
        ListViewItem lvItem = null;

        int Occurrences = 0;
        string PageLanguage = msDoc.GetIsoLanguageCode();
        string DetectedLanguage = msDoc.GetDescriptionLanguage();
        string Description = msDoc.GetDescription();
        int DescriptionLength = msDoc.GetDescriptionLength();

        string PairKey = string.Join( "", Url, Description );

        if( string.IsNullOrEmpty( PageLanguage ) )
        {
          PageLanguage = "";
        }
        
        if( string.IsNullOrEmpty( DetectedLanguage ) )
        {
          DetectedLanguage = "";
        }
        
        if( DescriptionLength > 0 )
        {
          Occurrences = DocCollection.GetStatsDescriptionCount( msDoc: msDoc );
        }
        else
        {
          Description = "MISSING";
        }

        if( this.DisplayListView.Items.ContainsKey( PairKey ) )
        {

          try
          {

            lvItem = this.DisplayListView.Items[ PairKey ];
            lvItem.SubItems[ ColUrl ].Text = Url;
            lvItem.SubItems[ ColPageLanguage ].Text = PageLanguage;        
            lvItem.SubItems[ ColDetectedLanguage ].Text = DetectedLanguage;        
            lvItem.SubItems[ ColOccurences ].Text = Occurrences.ToString();
            lvItem.SubItems[ ColDescriptionText ].Text = Description;
            lvItem.SubItems[ ColLength ].Text = DescriptionLength.ToString();

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayDescriptions 1: {0}", ex.Message ) );
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
            lvItem.SubItems.Add( PageLanguage );
            lvItem.SubItems.Add( DetectedLanguage );
            lvItem.SubItems.Add( Occurrences.ToString() );
            lvItem.SubItems.Add( Description );
            lvItem.SubItems.Add( DescriptionLength.ToString() );

            ListViewItems.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayDescriptions 2: {0}", ex.Message ) );
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
          
          // Description Language --------------------------------------------//

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

          // Check Description Length ----------------------------------------//
          
          if( msDoc.GetIsInternal() )
          {
            if( DescriptionLength < MacroscopePreferencesManager.GetDescriptionMinLen() )
            {
              lvItem.SubItems[ ColUrl ].ForeColor = Color.Red;
              lvItem.SubItems[ ColOccurences ].ForeColor = Color.Red;
              lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Red;
              lvItem.SubItems[ ColDescriptionText ].ForeColor = Color.Red;
              lvItem.SubItems[ ColLength ].ForeColor = Color.Red;
            }
            else
            if( DescriptionLength > MacroscopePreferencesManager.GetDescriptionMaxLen() )
            {
              lvItem.SubItems[ ColUrl ].ForeColor = Color.Red;
              lvItem.SubItems[ ColOccurences ].ForeColor = Color.Red;
              lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Red;
              lvItem.SubItems[ ColDescriptionText ].ForeColor = Color.Red;
              lvItem.SubItems[ ColLength ].ForeColor = Color.Red;
            }
            else
            {
              lvItem.SubItems[ ColOccurences ].ForeColor = Color.Green;
              lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Green;
              lvItem.SubItems[ ColDescriptionText ].ForeColor = Color.Green;
              lvItem.SubItems[ ColLength ].ForeColor = Color.Green;
            }
          }
          else
          {
            lvItem.SubItems[ ColOccurences ].ForeColor = Color.Gray;
            lvItem.SubItems[ ColDetectedLanguage ].ForeColor = Color.Gray;
            lvItem.SubItems[ ColDescriptionText ].ForeColor = Color.Gray;
            lvItem.SubItems[ ColLength ].ForeColor = Color.Gray;
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
