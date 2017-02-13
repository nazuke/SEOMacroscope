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
using System.Windows.Forms;

// TODO: Finish this

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDisplayKeywords.
  /// </summary>

  public class MacroscopeDisplayKeywords : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayKeywords ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
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

        string sText = msDoc.GetKeywords();
        int iTextCount = this.MainForm.GetJobMaster().GetDocCollection().GetStatsKeywordsCount( sText );
        string sTextLength = sText.Length.ToString();
        int iTextNumber = msDoc.GetKeywordsCount();
        string sPairKey = string.Join( "", sUrl, sText );

        if( this.lvListView.Items.ContainsKey( sPairKey ) )
        {

          try
          {

            ListViewItem lvItem = this.lvListView.Items[ sPairKey ];
            lvItem.SubItems[ 0 ].Text = sUrl;
            lvItem.SubItems[ 1 ].Text = iTextCount.ToString();
            lvItem.SubItems[ 2 ].Text = sText;
            lvItem.SubItems[ 3 ].Text = sTextLength;
            lvItem.SubItems[ 4 ].Text = iTextNumber.ToString();

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "RenderListView 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            ListViewItem lvItem = new ListViewItem ( sPairKey );

            lvItem.Name = sPairKey;

            lvItem.SubItems[ 0 ].Text = sUrl;
            lvItem.SubItems.Add( iTextCount.ToString() );
            lvItem.SubItems.Add( sText );
            lvItem.SubItems.Add( sTextLength );
            lvItem.SubItems.Add( iTextNumber.ToString() );

            this.lvListView.Items.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "RenderListView 2: {0}", ex.Message ) );
          }

        }

      }

    }

    /**************************************************************************/

  }
}
