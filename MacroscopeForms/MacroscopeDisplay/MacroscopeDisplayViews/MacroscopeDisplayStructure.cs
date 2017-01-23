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
using System.Windows.Forms;

namespace SEOMacroscope
{

	public class MacroscopeDisplayStructure : MacroscopeDisplayListView
	{

		/**************************************************************************/

		static Boolean ListViewConfigured = false;

		const string constUrl = "URL";
		
		const string constStatus = "Status";
		const string constIsRedirect = "Redirect";
				
		const string constDuration = "Duration (seconds)";

		const string constDateServer = "Date";
		const string constDateModified = "Last Modified";
		
		const string constContentType = "Content Type";
		const string constLang = "Lang";
		
		const string constCanonical = "Canonical";
		
		const string constInhyperlinks = "Links In";
		const string constOuthyperlinks = "Links Out";
		
		const string constTitle = "Title";
		const string constTitleLen = "Title Length";
		
		const string constDescription = "Description";
		const string constDescriptionLen = "Description Length";
		
		const string constKeywords = "Keywords";
		const string constKeywordsLen = "Keywords Length";
		const string constKeywordsCount = "Keywords Count";
		
		const string constHn = "First H{0}";
		
		const string constErrorCondition = "Error Condition";

		/**************************************************************************/

		public MacroscopeDisplayStructure ( MacroscopeMainForm msMainFormNew, ListView lvListViewNew )
			: base( msMainFormNew, lvListViewNew )
		{

			msMainForm = msMainFormNew;
			lvListView = lvListViewNew;

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
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

				this.lvListView.Columns.Add( constUrl, constUrl );
				this.lvListView.Columns.Add( constStatus, constStatus );
				this.lvListView.Columns.Add( constIsRedirect, constIsRedirect );

				this.lvListView.Columns.Add( constDuration, constDuration );

				this.lvListView.Columns.Add( constDateServer, constDateServer );
				this.lvListView.Columns.Add( constDateModified, constDateModified );
				
				this.lvListView.Columns.Add( constContentType, constContentType );
				this.lvListView.Columns.Add( constLang, constLang );
				this.lvListView.Columns.Add( constCanonical, constCanonical );
				this.lvListView.Columns.Add( constInhyperlinks, constInhyperlinks );
				this.lvListView.Columns.Add( constOuthyperlinks, constOuthyperlinks );
				this.lvListView.Columns.Add( constTitle, constTitle );
				this.lvListView.Columns.Add( constTitleLen, constTitleLen );
				this.lvListView.Columns.Add( constDescription, constDescription );
				this.lvListView.Columns.Add( constDescriptionLen, constDescriptionLen );
				this.lvListView.Columns.Add( constKeywords, constKeywords );
				this.lvListView.Columns.Add( constKeywordsLen, constKeywordsLen );
				this.lvListView.Columns.Add( constKeywordsCount, constKeywordsCount );

				for( ushort iLevel = 1; iLevel <= 6; iLevel++ ) {
					string sHeadingLevel = string.Format( constHn, iLevel );
					this.lvListView.Columns.Add( sHeadingLevel, sHeadingLevel );
				}

				this.lvListView.Columns.Add( constErrorCondition, constErrorCondition );
								
				// END: Columns
								
				this.lvListView.Sorting = SortOrder.Ascending;

				this.ListViewResizeColumnsInitial();

				ListViewConfigured = true;
			
			}
			
		}

		/** Render One ************************************************************/

		protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
		{

			DebugMsg( "MacroscopeDisplayStructure: RenderListView" );
			lock( this.lvListView ) {

				Hashtable htItems = new Hashtable ();
				ListViewItem lvItem = null;
				
				// BEGIN: Columns
				
				htItems[ constUrl ] = msDoc.GetUrl();

				htItems[ constStatus ] = msDoc.GetStatusCode();
				htItems[ constIsRedirect ] = msDoc.GetIsRedirect();

				htItems[ constDuration ] = msDoc.GetDurationInSecondsFormatted();
								
				htItems[ constContentType ] = msDoc.GetMimeType();

				{
					string sLang = msDoc.GetLang();
					if( sLang == null ) {
						sLang = "";
					}
					htItems[ constLang ] = sLang;
				}
								
				htItems[ constDateServer ] = msDoc.GetDateServer();
				htItems[ constDateModified ] = msDoc.GetDateModified();

				htItems[ constCanonical ] = msDoc.GetCanonical();

				htItems[ constInhyperlinks ] = msDoc.CountHyperlinksIn();
				htItems[ constOuthyperlinks ] = msDoc.CountHyperlinksOut();
												
				htItems[ constTitle ] = msDoc.GetTitle();
				htItems[ constTitleLen ] = msDoc.GetTitleLength();

				htItems[ constDescription ] = msDoc.GetDescription();
				htItems[ constDescriptionLen ] = msDoc.GetDescriptionLength();
				
				htItems[ constKeywords ] = msDoc.GetKeywords();
				htItems[ constKeywordsLen ] = msDoc.GetKeywordsLength();
				htItems[ constKeywordsCount ] = msDoc.GetKeywordsCount();

				for( ushort iLevel = 1; iLevel <= 6; iLevel++ ) {
					ArrayList aHeadings = msDoc.GetHeadings( iLevel );
					string sText = "";
					if( aHeadings.Count > 0 ) {
						sText = ( string )aHeadings[ 0 ];
					}
					htItems[ string.Format( constHn, iLevel ) ] = sText;
				}

				htItems[ constErrorCondition ] = msDoc.GetErrorCondition();
								
				// END: Columns				

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

					foreach( string sKey in htItems.Keys ) {

						int iColIndex = this.lvListView.Columns.IndexOfKey( sKey );

						if( htItems[ sKey ] != null ) {
							lvItem.SubItems[ iColIndex ].Text = htItems[ sKey ].ToString();
						} else {
							lvItem.SubItems[ iColIndex ].Text = "";
						}

					}

				} else {
					DebugMsg( string.Format( "MacroscopeDisplayStructure: {0}", "lvItem is NULL" ) );
				}

			}

		}

		/**************************************************************************/

		void ListViewResizeColumnsInitial ()
		{

			Dictionary<string,int> lColExplicitWidth = new Dictionary<string,int> ()
			{
				{
					constUrl,
					300
				},
				{
					constTitle,
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
				constUrl,
				constDateServer,
				constDateModified,
				constTitle
			};
			
			List<string> lColHeaderWidth = new List<string> ()
			{
				constDateModified
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
