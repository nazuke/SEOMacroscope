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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

	public class MacroscopeDisplayStructure : MacroscopeDisplayListView
	{

		/**************************************************************************/

		static Boolean ListViewConfigured = false;

		/**************************************************************************/

		public MacroscopeDisplayStructure ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
			: base( MainFormNew, lvListViewNew )
		{

			MainForm = MainFormNew;
			lvListView = lvListViewNew;

			if( MainForm.InvokeRequired ) {
				MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ConfigureListView();
						}
					)
				);
			} else {
				ConfigureListView();
			}

		}

		/**************************************************************************/

		void ConfigureListView ()
		{
			
			if( !ListViewConfigured ) {
			
				// BEGIN: Columns

				this.lvListView.Columns.Add( MacroscopeConstants.Url, MacroscopeConstants.Url );
				this.lvListView.Columns.Add( MacroscopeConstants.Status, MacroscopeConstants.Status );
				this.lvListView.Columns.Add( MacroscopeConstants.IsRedirect, MacroscopeConstants.IsRedirect );

				this.lvListView.Columns.Add( MacroscopeConstants.Duration, MacroscopeConstants.Duration );

				this.lvListView.Columns.Add( MacroscopeConstants.DateServer, MacroscopeConstants.DateServer );
				this.lvListView.Columns.Add( MacroscopeConstants.DateModified, MacroscopeConstants.DateModified );
				
				this.lvListView.Columns.Add( MacroscopeConstants.ContentType, MacroscopeConstants.ContentType );
				this.lvListView.Columns.Add( MacroscopeConstants.Lang, MacroscopeConstants.Lang );
				this.lvListView.Columns.Add( MacroscopeConstants.Canonical, MacroscopeConstants.Canonical );
				this.lvListView.Columns.Add( MacroscopeConstants.Inhyperlinks, MacroscopeConstants.Inhyperlinks );
				this.lvListView.Columns.Add( MacroscopeConstants.Outhyperlinks, MacroscopeConstants.Outhyperlinks );
				this.lvListView.Columns.Add( MacroscopeConstants.Title, MacroscopeConstants.Title );
				this.lvListView.Columns.Add( MacroscopeConstants.TitleLen, MacroscopeConstants.TitleLen );
				this.lvListView.Columns.Add( MacroscopeConstants.Description, MacroscopeConstants.Description );
				this.lvListView.Columns.Add( MacroscopeConstants.DescriptionLen, MacroscopeConstants.DescriptionLen );
				this.lvListView.Columns.Add( MacroscopeConstants.Keywords, MacroscopeConstants.Keywords );
				this.lvListView.Columns.Add( MacroscopeConstants.KeywordsLen, MacroscopeConstants.KeywordsLen );
				this.lvListView.Columns.Add( MacroscopeConstants.KeywordsCount, MacroscopeConstants.KeywordsCount );

				for( ushort iLevel = 1; iLevel <= 6; iLevel++ ) {
					string sHeadingLevel = string.Format( MacroscopeConstants.Hn, iLevel );
					this.lvListView.Columns.Add( sHeadingLevel, sHeadingLevel );
				}

				this.lvListView.Columns.Add( MacroscopeConstants.ErrorCondition, MacroscopeConstants.ErrorCondition );
								
				// END: Columns
								
				this.lvListView.Sorting = SortOrder.Ascending;

				this.ListViewResizeColumnsInitial();

				ListViewConfigured = true;
			
			}
			
		}

		/** Render One ************************************************************/

		protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
		{

			lock( this.lvListView ) {

				Hashtable htItems = new Hashtable ();
				ListViewItem lvItem = null;
				
				// BEGIN: Columns
				
				htItems[ MacroscopeConstants.Url ] = msDoc.GetUrl();

				htItems[ MacroscopeConstants.Status ] = msDoc.GetStatusCode();
				htItems[ MacroscopeConstants.IsRedirect ] = msDoc.GetIsRedirect();

				htItems[ MacroscopeConstants.Duration ] = msDoc.GetDurationInSecondsFormatted();
								
				htItems[ MacroscopeConstants.ContentType ] = msDoc.GetMimeType();

				{
					string sLang = msDoc.GetLang();
					if( sLang == null ) {
						sLang = "";
					}
					htItems[ MacroscopeConstants.Lang ] = sLang;
				}
								
				htItems[ MacroscopeConstants.DateServer ] = msDoc.GetDateServer();
				htItems[ MacroscopeConstants.DateModified ] = msDoc.GetDateModified();

				htItems[ MacroscopeConstants.Canonical ] = msDoc.GetCanonical();

				htItems[ MacroscopeConstants.Inhyperlinks ] = msDoc.CountHyperlinksIn();
				htItems[ MacroscopeConstants.Outhyperlinks ] = msDoc.CountHyperlinksOut();
												
				htItems[ MacroscopeConstants.Title ] = msDoc.GetTitle();
				htItems[ MacroscopeConstants.TitleLen ] = msDoc.GetTitleLength();

				htItems[ MacroscopeConstants.Description ] = msDoc.GetDescription();
				htItems[ MacroscopeConstants.DescriptionLen ] = msDoc.GetDescriptionLength();
				
				htItems[ MacroscopeConstants.Keywords ] = msDoc.GetKeywords();
				htItems[ MacroscopeConstants.KeywordsLen ] = msDoc.GetKeywordsLength();
				htItems[ MacroscopeConstants.KeywordsCount ] = msDoc.GetKeywordsCount();

				for( ushort iLevel = 1; iLevel <= 6; iLevel++ ) {
					ArrayList aHeadings = msDoc.GetHeadings( iLevel );
					string sText = "";
					if( aHeadings.Count > 0 ) {
						sText = ( string )aHeadings[ 0 ];
					}
					htItems[ string.Format( MacroscopeConstants.Hn, iLevel ) ] = sText;
				}

				htItems[ MacroscopeConstants.ErrorCondition ] = msDoc.GetErrorCondition();
								
				// END: Columns				

				this.lvListView.BeginUpdate();

				if( this.lvListView.Items.ContainsKey( sUrl ) ) {
					lvItem = this.lvListView.Items[ sUrl ];
				} else {
					lvItem = new ListViewItem ( sUrl );
					lvItem.Name = sUrl;
					foreach( string sKey in htItems.Keys ) {
						lvItem.SubItems.Add( sKey );
					}
					this.lvListView.Items.Add( lvItem );
				}

				if( lvItem != null ) {

					lvItem.UseItemStyleForSubItems = false;
					lvItem.ForeColor = Color.Blue;

					foreach( string sKey in htItems.Keys ) {

						int iColIndex = this.lvListView.Columns.IndexOfKey( sKey );
						string sText = htItems[ sKey ].ToString();

						if( htItems[ sKey ] != null ) {
							lvItem.SubItems[ iColIndex ].Text = sText;
						} else {
							lvItem.SubItems[ iColIndex ].Text = "";
						}

						lvItem.SubItems[ iColIndex ].ForeColor = Color.Blue;

						if( sKey == MacroscopeConstants.Status ) {
							if( Regex.IsMatch( sText, "^[2]" ) ) {
								lvItem.SubItems[ iColIndex ].ForeColor = Color.Green;
							} else if( Regex.IsMatch( sText, "^[3]" ) ) {
								lvItem.SubItems[ iColIndex ].ForeColor = Color.Goldenrod;
							} else if( Regex.IsMatch( sText, "^[45]" ) ) {
								lvItem.SubItems[ iColIndex ].ForeColor = Color.Red;
							} else {
								lvItem.SubItems[ iColIndex ].ForeColor = Color.Blue;
							}
						}

					}

				} else {
					DebugMsg( string.Format( "MacroscopeDisplayStructure: {0}", "lvItem is NULL" ) );
				}

				this.lvListView.EndUpdate();
				
			}

		}

		/**************************************************************************/

		void ListViewResizeColumnsInitial ()
		{

			Dictionary<string,int> lColExplicitWidth = new Dictionary<string,int> ()
			{
				{
					MacroscopeConstants.Url,
					300
				},
				{
					MacroscopeConstants.Title,
					300
				}
			};
			
			for( int iColIndex = 0; iColIndex < this.lvListView.Columns.Count; iColIndex++ ) {
				this.lvListView.AutoResizeColumn( iColIndex, ColumnHeaderAutoResizeStyle.HeaderSize );
			}

			foreach( string sColName in lColExplicitWidth.Keys ) {
				this.lvListView.Columns[ sColName ].Width = lColExplicitWidth[ sColName ];
			}

		}
				
		/**************************************************************************/
		
		void ListViewResizeColumns ()
		{

			List<string> lColDataWidth = new List<string> ()
			{
					MacroscopeConstants.Url,
					MacroscopeConstants.DateServer,
					MacroscopeConstants.DateModified,
					MacroscopeConstants.Title
			};
			
			List<string> lColHeaderWidth = new List<string> ()
			{
					MacroscopeConstants.DateModified
			};

			foreach( string sColName in lColDataWidth ) {
				this.lvListView.AutoResizeColumn( this.lvListView.Columns[ sColName ].Index, ColumnHeaderAutoResizeStyle.ColumnContent );
			}
			
			foreach( string sColName in lColHeaderWidth ) {
				this.lvListView.AutoResizeColumn( this.lvListView.Columns[ sColName ].Index, ColumnHeaderAutoResizeStyle.HeaderSize );
			}

		}

		/**************************************************************************/

	}

}
