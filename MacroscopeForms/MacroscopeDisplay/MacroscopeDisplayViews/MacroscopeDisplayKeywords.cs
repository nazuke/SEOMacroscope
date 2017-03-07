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

  /// <summary>
  /// Description of MacroscopeDisplayKeywords.
  /// </summary>

  public sealed class MacroscopeDisplayKeywords : MacroscopeDisplayListView
  {

    /**************************************************************************/

    public MacroscopeDisplayKeywords ( MacroscopeMainForm MainForm, ListView lvListView )
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
        this.ListViewConfigured = true;
      }
    }

    /**************************************************************************/

    protected override void RenderListView ( MacroscopeDocument msDoc, string Url )
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
        int iKeywordsLength = msDoc.GetKeywordsLength();
        int iTextNumber = msDoc.GetKeywordsCount();
        string sPairKey = string.Join( "", Url, sText );

        ListViewItem lvItem = null;
          
        if( this.lvListView.Items.ContainsKey( sPairKey ) )
        {

          try
          {

            lvItem = this.lvListView.Items[ sPairKey ];
            lvItem.SubItems[ 0 ].Text = Url;
            lvItem.SubItems[ 1 ].Text = iTextCount.ToString();
            lvItem.SubItems[ 2 ].Text = sText;
            lvItem.SubItems[ 3 ].Text = iKeywordsLength.ToString();
            lvItem.SubItems[ 4 ].Text = iTextNumber.ToString();

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayKeywords 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem ( sPairKey );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = sPairKey;

            lvItem.SubItems[ 0 ].Text = Url;
            lvItem.SubItems.Add( iTextCount.ToString() );
            lvItem.SubItems.Add( sText );
            lvItem.SubItems.Add( iKeywordsLength.ToString() );
            lvItem.SubItems.Add( iTextNumber.ToString() );

            this.lvListView.Items.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayKeywords 2: {0}", ex.Message ) );
          }

        }

        if( lvItem != null )
        {

          lvItem.ForeColor = Color.Blue;

          lvItem.SubItems[ 0 ].ForeColor = Color.Green;

        }

      }

    }

    /**************************************************************************/

  }
  
}
