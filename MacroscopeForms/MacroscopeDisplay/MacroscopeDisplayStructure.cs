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

	public class MacroscopeDisplayStructure : Macroscope
	{

		/**************************************************************************/

		MacroscopeMainForm msMainForm;

		static Boolean ListViewConfigured = false;

		const string constURL = "URL";
		
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

		public MacroscopeDisplayStructure ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = msMainForm.GetDisplayStructure();
							ConfigureListView( lvListView );
						}
					)
				);
			} else {
				ListView lvListView = msMainForm.GetDisplayStructure();
				ConfigureListView( lvListView );
			}

		}

		/**************************************************************************/

		static void ConfigureListView ( ListView lvListView )
		{
			
			if( !ListViewConfigured ) {
			
				// BEGIN: Columns
								
				lvListView.Columns.Add( constURL, constURL );
				lvListView.Columns.Add( constStatus, constStatus );
				lvListView.Columns.Add( constIsRedirect, constIsRedirect );

				lvListView.Columns.Add( constDuration, constDuration );

				lvListView.Columns.Add( constDateServer, constDateServer );
				lvListView.Columns.Add( constDateModified, constDateModified );
				
				lvListView.Columns.Add( constContentType, constContentType );
				lvListView.Columns.Add( constLang, constLang );
				lvListView.Columns.Add( constCanonical, constCanonical );
				lvListView.Columns.Add( constInhyperlinks, constInhyperlinks );
				lvListView.Columns.Add( constOuthyperlinks, constOuthyperlinks );
				lvListView.Columns.Add( constTitle, constTitle );
				lvListView.Columns.Add( constTitleLen, constTitleLen );
				lvListView.Columns.Add( constDescription, constDescription );
				lvListView.Columns.Add( constDescriptionLen, constDescriptionLen );
				lvListView.Columns.Add( constKeywords, constKeywords );
				lvListView.Columns.Add( constKeywordsLen, constKeywordsLen );
				lvListView.Columns.Add( constKeywordsCount, constKeywordsCount );

				for( ushort iLevel = 1; iLevel <= 6; iLevel++ ) {
					string sHeadingLevel = string.Format( constHn, iLevel );
					lvListView.Columns.Add( sHeadingLevel, sHeadingLevel );
				}

				lvListView.Columns.Add( constErrorCondition, constErrorCondition );
								
				// END: Columns
								
				lvListView.Sorting = SortOrder.Ascending;

				ListViewResizeColumnsInitial( lvListView );

				ListViewConfigured = true;
			
			}
			
		}

		/**************************************************************************/

		public void ClearData ()
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayStructure();
							lvListView.Items.Clear();
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayStructure();
				lvListView.Items.Clear();
			}
		}
		
		/**************************************************************************/
		
		public void RefreshData ( MacroscopeDocumentCollection htDocCollection )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayStructure();
							lvListView.BeginUpdate();
							this.RenderListView( lvListView, htDocCollection );
							//this.ListViewResizeColumns( lvListView );
							lvListView.EndUpdate();
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayStructure();
				lvListView.BeginUpdate();
				this.RenderListView( lvListView, htDocCollection );
				//this.ListViewResizeColumns( lvListView );
				lvListView.EndUpdate();
			}
		}

		/**************************************************************************/

		public void RefreshDataSingle ( MacroscopeDocument msDoc, string sURL )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayStructure();
							lvListView.BeginUpdate();
							this.RenderListViewSingle( lvListView, msDoc, sURL );
							//this.ListViewResizeColumns( lvListView );
							lvListView.EndUpdate();
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayStructure();
				lvListView.BeginUpdate();
				this.RenderListViewSingle( lvListView, msDoc, sURL );
				//this.ListViewResizeColumns( lvListView );
				lvListView.EndUpdate();
			}
		}

		/**************************************************************************/

		void RenderListView ( ListView lvListView, MacroscopeDocumentCollection htDocCollection )
		{
			foreach( string sKeyURL in htDocCollection.Keys() ) {
				MacroscopeDocument msDoc = htDocCollection.Get( sKeyURL );
				this.RenderListViewSingle( lvListView, msDoc, sKeyURL );
			}
		}

		/**************************************************************************/

		void RenderListViewSingle ( ListView lvListView, MacroscopeDocument msDoc, string sKeyURL )
		{

			lock( lvListView ) {

				Hashtable htItems = new Hashtable ();
				ListViewItem lvItem = null;
				
				// BEGIN: Columns
				
				htItems[ constURL ] = msDoc.GetUrl();

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

				if( lvListView.Items.ContainsKey( sKeyURL ) ) {

					lvItem = lvListView.Items[ sKeyURL ];

				} else {

					lvItem = new ListViewItem ( sKeyURL );
					lvItem.Name = sKeyURL;

					foreach( string sKey in htItems.Keys ) {
						lvItem.SubItems.Add( sKey );
					}

					lvListView.Items.Add( lvItem );

				}

				if( lvItem != null ) {

					foreach( string sKey in htItems.Keys ) {

						int iColIndex = lvListView.Columns.IndexOfKey( sKey );

						if( htItems[ sKey ] != null ) {
							lvItem.SubItems[ iColIndex ].Text = htItems[ sKey ].ToString();
						} else {
							lvItem.SubItems[ iColIndex ].Text = "";
						}

					}

				} else {
					debug_msg( string.Format( "MacroscopeDisplayStructure: {0}", "lvItem is NULL" ) );
				}

			}

		}

		/**************************************************************************/

		static void ListViewResizeColumnsInitial ( ListView lvListView )
		{

			Dictionary<string,int> lColExplicitWidth = new Dictionary<string,int> ()
			{
				{
					constURL,
					300
				},
				{
					constTitle,
					300
				}
			};
			
			for( int iColIndex = 0; iColIndex < lvListView.Columns.Count; iColIndex++ ) {
				lvListView.AutoResizeColumn( iColIndex, ColumnHeaderAutoResizeStyle.HeaderSize );
			}

			foreach( string sColName in lColExplicitWidth.Keys ) {
				lvListView.Columns[ sColName ].Width = lColExplicitWidth[ sColName ];
			}

		}
				
		/**************************************************************************/
		
		void ListViewResizeColumns ( ListView lvListView )
		{

			List<string> lColDataWidth = new List<string> ()
			{
					constURL,
					constDateServer,
					constDateModified,
					constTitle
			};
			
			List<string> lColHeaderWidth = new List<string> ()
			{
					constDateModified
			};

			foreach( string sColName in lColDataWidth ) {
				lvListView.AutoResizeColumn( lvListView.Columns[ sColName ].Index, ColumnHeaderAutoResizeStyle.ColumnContent );
			}
			
			foreach( string sColName in lColHeaderWidth ) {
				lvListView.AutoResizeColumn( lvListView.Columns[ sColName ].Index, ColumnHeaderAutoResizeStyle.HeaderSize );
			}

		}

		/**************************************************************************/

	}

}
