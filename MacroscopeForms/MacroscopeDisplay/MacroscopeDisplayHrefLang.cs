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
using System.Drawing;

namespace SEOMacroscope
{

	public class MacroscopeDisplayHrefLang : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;

		static Boolean ListViewConfigured = false;

		/**************************************************************************/

		public MacroscopeDisplayHrefLang ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayHrefLang();
							ConfigureListView( lvListView );
						}
					)
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayHrefLang();
				ConfigureListView( lvListView );
			}
		}

		/**************************************************************************/
		
		static void ConfigureListView ( ListView lvListView )
		{
			
			if( !ListViewConfigured ) {
						
				lvListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );

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
							;
						}
					)
				);
			} else {
				;
			}
		}

		/**************************************************************************/
		
		public void RefreshData ( MacroscopeDocumentCollection htDocCollection, Dictionary<string,string> htLocales )
		{

			debug_msg( string.Format( "MacroscopeDisplayHrefLang: {0}", "RefreshData" ) );
						
			if( this.msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker ( 
						delegate
						{
							ListView lvListView = this.msMainForm.GetDisplayHrefLang();
							this.RenderListView( lvListView, htDocCollection, htLocales );
						}
					) 
				);
			} else {
				ListView lvListView = this.msMainForm.GetDisplayHrefLang();
				this.RenderListView( lvListView, htDocCollection, htLocales );
			}

		}

		/**************************************************************************/
		
		void RenderListView ( ListView lvListView, MacroscopeDocumentCollection htDocCollection, Dictionary<string,string> htLocales )
		{

			Hashtable htLocaleCols = new Hashtable ();

			lvListView.Items.Clear();
			lvListView.Columns.Clear();

			{

				int iLocaleColCount = 3;

				lvListView.Columns.Add( "Site Locale", "Site Locale" );
				lvListView.Columns.Add( "Title", "Title" );
				lvListView.Columns.Add( "URL", "URL" );

				foreach( string sLocale in htLocales.Keys ) {
					lvListView.Columns.Add( sLocale, sLocale );
					htLocaleCols[ sLocale ] = iLocaleColCount;
					iLocaleColCount++;
				}

			}

			foreach( string sKeyURL in htDocCollection.Keys() ) {

				MacroscopeDocument msDoc = htDocCollection.Get( sKeyURL );

				if( msDoc.GetIsHtml() ) {

					Dictionary<string,MacroscopeHrefLang> htHrefLangs = msDoc.GetHrefLangs();

					if( htHrefLangs != null ) {
						
						string sDocLocale = msDoc.GetLocale();
						string sDocTitle = msDoc.GetTitle();
						string sDocUrl = msDoc.GetUrl();

						{

							ListViewItem lvItem;

							if( lvListView.Items.ContainsKey( sKeyURL ) ) {

								lvItem = lvListView.Items[ sKeyURL ];
							
							} else {
							
								lvItem = new ListViewItem ( sKeyURL );

								lvItem.Name = sKeyURL;
								
								lvItem.SubItems.Add( "" );
								lvItem.SubItems.Add( "" );
								lvItem.SubItems.Add( "" );

								for( int i = 0; i < htLocales.Keys.Count; i++ ) {
									lvItem.SubItems.Add( "" );
								}

								lvListView.Items.Add( lvItem );

							}

							if( lvListView.Items.ContainsKey( sKeyURL ) ) {
							
								try {

									lvItem.SubItems[ 0 ].Text = sDocLocale;
									lvItem.SubItems[ 1 ].Text = sDocTitle;
									lvItem.SubItems[ 2 ].Text = sDocUrl;

									foreach( string sLocale in htLocales.Keys ) {

										if( sLocale != null ) {

											string sHrefLangUrl = null;
											int iLocale = ( int )htLocaleCols[ sLocale ];
											
											if( htHrefLangs.ContainsKey( sLocale ) ) {
												MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[ sLocale ];
												if( msHrefLang != null ) {
													sHrefLangUrl = msHrefLang.GetUrl();
												}
											}
											
											if( sHrefLangUrl != null ) {
												lvItem.SubItems[ iLocale ].ForeColor = Color.Blue;
												lvItem.SubItems[ iLocale ].Text = sHrefLangUrl;
											} else {
												lvItem.SubItems[ iLocale ].ForeColor = Color.Red;
												lvItem.SubItems[ iLocale ].Text = "MISSSING";

											}
											
										}
								
									}

								} catch( Exception ex ) {
									debug_msg( string.Format( "MacroscopeDisplayHrefLang: {0}", ex.Message ) );
									debug_msg( string.Format( "MacroscopeDisplayHrefLang: {0}", ex.StackTrace ) );
								}

							} else {
								debug_msg( string.Format( "MacroscopeDisplayHrefLang MISSING: {0}", sKeyURL ) );
							}

						}
					}

				}
		
			}

			lvListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );

			lvListView.Columns[ "Site Locale" ].Width = 100;
			lvListView.Columns[ "Title" ].Width = 300;
			lvListView.Columns[ "URL" ].Width = 300;

		}

		/**************************************************************************/

	}

}
