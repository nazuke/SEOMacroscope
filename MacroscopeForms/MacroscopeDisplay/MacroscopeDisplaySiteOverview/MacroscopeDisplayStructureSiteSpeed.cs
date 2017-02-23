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
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

  public sealed class MacroscopeDisplayStructureSiteSpeed : Macroscope
  {

    /**************************************************************************/

    private MacroscopeMainForm MainForm;
          
    private MacroscopeDecimalSorter DecimalSorterAscending;
    private MacroscopeDecimalSorter DecimalSorterDescending;
          
    private ListView lvListViewSlowest;
    private ListView lvListViewFastest;
    private ToolStripLabel AverageLabel;
    
    /**************************************************************************/

    public MacroscopeDisplayStructureSiteSpeed (
      MacroscopeMainForm MainFormNew,
      ListView lvListViewSlowest,
      ListView lvListViewFastest,
      ToolStripLabel AverageLabel
    )
    {

      this.SuppressDebugMsg = true;
      
      this.MainForm = MainFormNew;

      this.DecimalSorterAscending = new MacroscopeDecimalSorter ( MacroscopeDecimalSorter.SortOrder.ASCENDING );
      this.DecimalSorterDescending = new MacroscopeDecimalSorter ( MacroscopeDecimalSorter.SortOrder.DESCENDING );
                
      this.lvListViewSlowest = lvListViewSlowest;
      this.lvListViewFastest = lvListViewFastest;
      this.AverageLabel = AverageLabel;

    }

    /**************************************************************************/

    public void RefreshSiteSpeedData ( MacroscopeDocumentCollection DocCollection )
    {

      if( DocCollection.CountDocuments() > 0 )
      {

        const int MeasurePages = 20;
        decimal Average = 0;
        int Count = 0;
        decimal Maximus = 0;

        SortedList<decimal,string> SortedListAll = new SortedList<decimal, string> ( DocCollection.CountDocuments(), this.DecimalSorterAscending );
        SortedList<decimal,string> SortedListSlowest = new SortedList<decimal, string> ( MeasurePages, this.DecimalSorterDescending );
        SortedList<decimal,string> SortedListFastest = new SortedList<decimal, string> ( MeasurePages, this.DecimalSorterAscending );

        foreach( string sUrl in DocCollection.DocumentKeys() )
        {

          MacroscopeDocument msDoc = DocCollection.GetDocument( sUrl );
          decimal Duration = msDoc.GetDurationInSeconds();

          if( !msDoc.GetIsExternal() && msDoc.GetWasDownloaded() )
          {
          
            Count++;
            Maximus += Duration;
          
            if( SortedListAll.ContainsKey( Duration ) )
            {
              SortedListAll[ Duration ] = msDoc.GetUrl();
            }
            else
            {
              SortedListAll.Add( Duration, msDoc.GetUrl() );
            }
          
          }

        }

        foreach( decimal Duration in SortedListAll.Keys.Take(MeasurePages) )
        {
          SortedListFastest.Add( Duration, SortedListAll[ Duration ] );
        }

        foreach( decimal Duration in SortedListAll.Keys.Reverse().Take(MeasurePages) )
        {
          SortedListSlowest.Add( Duration, SortedListAll[ Duration ] );
        }

        if( Count > 0 )
        {
          Average = Maximus / Count;
        }
        
        if( this.MainForm.InvokeRequired )
        {
          this.MainForm.Invoke(
            new MethodInvoker (
              delegate
              {
                Cursor.Current = Cursors.WaitCursor;
                this.RenderSiteSpeedListView( this.lvListViewSlowest, SortedListSlowest );
                this.RenderSiteSpeedListView( this.lvListViewFastest, SortedListFastest );
                this.UpdateAverageLabel( Average );
                Cursor.Current = Cursors.Default;
              }
            )
          );
        }
        else
        {
          Cursor.Current = Cursors.WaitCursor;
          this.RenderSiteSpeedListView( this.lvListViewSlowest, SortedListSlowest );
          this.RenderSiteSpeedListView( this.lvListViewFastest, SortedListFastest );
          this.UpdateAverageLabel( Average );
          Cursor.Current = Cursors.Default;
        }
      
      }
      
    }

    /**************************************************************************/
    
    void RenderSiteSpeedListView (
      ListView lvListView,
      SortedList<decimal,string> SortedListSpeed
    )
    {

      lvListView.BeginUpdate();

      lvListView.Items.Clear();
            
      foreach( decimal Duration in SortedListSpeed.Keys )
      {

        string sDuration = string.Format( "{0:0.00}", Duration );
        ListViewItem lvItem = null;

        if( lvListView.Items.ContainsKey( sDuration ) )
        {

          try
          {

            lvItem = lvListView.Items[ sDuration ];
            lvItem.SubItems[ 0 ].Text = sDuration;
            lvItem.SubItems[ 1 ].Text = SortedListSpeed[ Duration ];

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayStructureSiteSpeed 1: {0}", ex.Message ) );
          }

        }
        else
        {

          try
          {

            lvItem = new ListViewItem ( sDuration );
            lvItem.UseItemStyleForSubItems = false;
            lvItem.Name = sDuration;

            lvItem.SubItems.Add( sDuration );
            lvItem.SubItems[ 0 ].Text = sDuration;
            lvItem.SubItems[ 1 ].Text = SortedListSpeed[ Duration ];
            
            lvListView.Items.Add( lvItem );

          }
          catch( Exception ex )
          {
            DebugMsg( string.Format( "MacroscopeDisplayStructureSiteSpeed 2: {0}", ex.Message ) );
          }

        }
        
        if( lvItem != null )
        {
          
          lvItem.ForeColor = Color.ForestGreen;
          
          if( Duration >= 1 )
          {
            lvItem.ForeColor = Color.Orange;
          }

          if( Duration >= 2 )
          {
            lvItem.ForeColor = Color.Red;
          }

        }

      }
      
      lvListView.EndUpdate();

    }

    /**************************************************************************/

    private void UpdateAverageLabel ( decimal Duration )
    {
      
      this.AverageLabel.Text = string.Format( "Average Response Time: {0:0.00}s", Duration );
      
      this.AverageLabel.ForeColor = Color.ForestGreen;
          
      if( Duration >= 1 )
      {
        this.AverageLabel.ForeColor = Color.Orange;
      }

      if( Duration >= 2 )
      {
        this.AverageLabel.ForeColor = Color.Red;
      }
    }

    /**************************************************************************/

  }

}
