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
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public class MacroscopeDisplayTitles : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayTitles ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
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
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
    {

      Boolean bProcess;

      if( msDoc.GetIsExternal() )
      {
        return;
      }

      if( msDoc.GetIsHtml() )
      {
        bProcess = true;
      }
      else
      if( msDoc.GetIsPdf() )
      {
        bProcess = true;
      }
      else
      {
        bProcess = false;
      }

      if( bProcess )
      {

        string sText = msDoc.GetTitle();
        string sTextLabel = sText;
        int iTextCount = this.MainForm.GetJobMaster().GetDocCollection().GetStatsTitleCount( sText );
        string sTextLength = sText.Length.ToString();
        int iTextPixelWidth = msDoc.GetTitlePixelWidth();

        string sPairKey = string.Join( "", sUrl, sText );

        ListViewItem lvItem = null;

        if( sText.Length <= 0 )
        {
          sTextLabel = "MISSING";
        }

        if( this.lvListView.Items.ContainsKey( sPairKey ) )
        {

          try
          {

            lvItem = this.lvListView.Items[ sPairKey ];
            lvItem.SubItems[ 0 ].Text = sUrl;
            lvItem.SubItems[ 1 ].Text = iTextCount.ToString();
            lvItem.SubItems[ 2 ].Text = sTextLabel;
            lvItem.SubItems[ 3 ].Text = sTextLength;
            lvItem.SubItems[ 4 ].Text = iTextPixelWidth.ToString();

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayTitles 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem ( sPairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = sPairKey;

            lvItem.SubItems[ 0 ].Text = sUrl;
            lvItem.SubItems.Add( iTextCount.ToString() );
            lvItem.SubItems.Add( sTextLabel );
            lvItem.SubItems.Add( sTextLength );
            lvItem.SubItems.Add( iTextPixelWidth.ToString() );

            this.lvListView.Items.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayTitles 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          // Check Missing Title ---------------------------------------------//

          if( sText.Length <= 0 )
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Red;
          }
          else
          if( sText.Length < MacroscopePreferencesManager.GetTitleMinLen() )
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Red;
          }
          else
          if( sText.Length > MacroscopePreferencesManager.GetTitleMaxLen() )
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 2 ].ForeColor = Color.ForestGreen;
          }

          // Check Title Length ----------------------------------------------//

          if( sText.Length < MacroscopePreferencesManager.GetTitleMinLen() )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Red;
          }
          else
          if( sText.Length > MacroscopePreferencesManager.GetTitleMaxLen() )
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.Red;
          }
          else
          {
            lvItem.SubItems[ 3 ].ForeColor = Color.ForestGreen;
          }

          // Check Pixel Width -----------------------------------------------//

          if( iTextPixelWidth > MacroscopePreferencesManager.GetTitleMaxPixelWidth() )
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Red;
          }
          else
          if( iTextPixelWidth >= ( MacroscopePreferencesManager.GetTitleMaxPixelWidth() - 20 ) )
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.Goldenrod;
          }
          else
          if( iTextPixelWidth <= 0 )
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.OrangeRed;
          }
          else
          {
            lvItem.SubItems[ 4 ].ForeColor = Color.ForestGreen;
          }

        }

      }

    }

    /**************************************************************************/

  }

}
