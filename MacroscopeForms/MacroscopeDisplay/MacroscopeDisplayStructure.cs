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
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

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
		
		const string constH1 = "First H1";
		const string constH2 = "First H2";
				
		/**************************************************************************/

		public MacroscopeDisplayStructure ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate {
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
			
				lvListView.Columns.Add( constURL, constURL );
				lvListView.Columns.Add( constStatus, constStatus );
				lvListView.Columns.Add( constIsRedirect, constIsRedirect );
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
				lvListView.Columns.Add( constH1, constH1 );
				lvListView.Columns.Add( constH2, constH2 );
			
				lvListView.Sorting = SortOrder.Ascending;

				lvListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );

				lvListView.Columns[ constURL ].Width = 300;
				lvListView.Columns[ constTitle ].Width = 150;

				ListViewConfigured = true;
			
			}
			
		}

		/**************************************************************************/

		public void RefreshData ( MacroscopeDocumentCollection htDocCollection )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							ListView lvListView = this.msMainForm.GetDisplayStructure();
							this.RenderListView( lvListView, htDocCollection );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayStructure();
				this.RenderListView( lvListView, htDocCollection );
			}
		}

		/**************************************************************************/

		public void RefreshDataSingle ( MacroscopeDocument msDoc, string sURL )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							ListView lvListView = this.msMainForm.GetDisplayStructure();
							this.RenderListViewSingle( lvListView, msDoc, sURL );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayStructure();
				this.RenderListViewSingle( lvListView, msDoc, sURL );
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

				lvListView.SuspendLayout();

				Hashtable htItems = new Hashtable ();
				ListViewItem lvItem = null;
				
				htItems[ constURL ] = msDoc.GetUrl();

				htItems[ constStatus ] = msDoc.GetStatusCode();
				htItems[ constIsRedirect ] = msDoc.GetIsRedirect();

				htItems[ constContentType ] = msDoc.GetMimeType();

				{
					string sLang = msDoc.GetLang();
					if( sLang == null ) {
						sLang = "";
					}
					htItems[ constLang ] = sLang;
				}
								
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

				{
					ArrayList aHeadings = msDoc.GetHeadings1();
					string sText = "";
					if( aHeadings.Count > 0 ) {
						sText = ( string )aHeadings[ 0 ];
					}
					htItems[ constH1 ] = sText;
				}
				
				{
					ArrayList aHeadings = msDoc.GetHeadings2();
					string sText = "";
					if( aHeadings.Count > 0 ) {
						sText = ( string )aHeadings[ 0 ];
					}
					htItems[ constH2 ] = sText;
				}

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

				lvListView.ResumeLayout();
			
			}
			
		}

		/**************************************************************************/

	}

}
