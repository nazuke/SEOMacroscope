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

	public class MacroscopeDisplayHrefLang : Macroscope
	{
		
		/**************************************************************************/

		MacroscopeMainForm msMainForm;

		/**************************************************************************/

		public MacroscopeDisplayHrefLang ( MacroscopeMainForm msMainFormNew )
		{
			
			msMainForm = msMainFormNew;

		}

		/**************************************************************************/

		public void ClearData ()
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate {
							;
						}
					)
				);
			} else {
				;
			}
		}

		/**************************************************************************/
		
		public void RefreshData ( MacroscopeDocumentCollection htDocCollection, Hashtable htLocales )
		{

			if( this.msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker ( 
						delegate {
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
		
		void RenderListView ( ListView lvListView, MacroscopeDocumentCollection htDocCollection, Hashtable htLocales )
		{

			lvListView.SuspendLayout();

			lvListView.Items.Clear();
			lvListView.Columns.Clear();

			{
				lvListView.Columns.Add( "Site Locale", "Site Locale" );
				lvListView.Columns.Add( "Title", "Title" );
				foreach( string sLocale in htLocales.Keys ) {
					lvListView.Columns.Add( sLocale, sLocale );
				}
			}

			foreach( string sKeyURL in htDocCollection.Keys() ) {

				MacroscopeDocument msDoc = htDocCollection.Get( sKeyURL );

				if( msDoc.GetIsHtml() ) {
				
					Hashtable htHrefLangs = ( Hashtable )msDoc.GetHrefLangs();

					
/*
					dtRow.SetField( "Site Locale", msDoc.GetLocale() );
					dtRow.SetField( "Title", msDoc.GetTitle() );
					dtRow.SetField( msDoc.Locale, msDoc.GetUrl() );

					foreach( string sLocale in htLocales.Keys ) {
						if( sLocale != null ) {
							if( htHrefLangs.ContainsKey( sLocale ) ) {
								MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[sLocale];
								dtRow.SetField( sLocale, msHrefLang.GetUrl() );
							} else {
								dtRow.SetField( sLocale, "MISSSING" );
							}
						}
					}

					*/
					
					
					
					


					if( lvListView.Items.ContainsKey( sKeyURL ) ) {
							
						try {

							ListViewItem lvItem = lvListView.Items[sKeyURL];

							lvItem.SubItems[0].Text = msDoc.GetLocale();
							lvItem.SubItems[1].Text = msDoc.GetTitle();
							lvItem.SubItems[2].Text = msDoc.GetUrl();

							foreach( string sLocale in htLocales.Keys ) {
								if( sLocale != null ) {
									if( htHrefLangs.ContainsKey( sLocale ) ) {
										MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[sLocale];
										lvItem.SubItems[sLocale].Text = msHrefLang.GetUrl();
									} else {
										lvItem.SubItems[sLocale].Text = "MISSSING";
									}
								}
							}

						} catch( Exception ex ) {
							debug_msg( string.Format( "MacroscopeDisplayHrefLang 1: {0}", ex.Message ) );
						}

					} else {
							
						try {

							ListViewItem lvItem = new ListViewItem ( sKeyURL );

							lvItem.Name = sKeyURL;

							lvItem.SubItems[0].Text = msDoc.GetLocale();
							lvItem.SubItems.Add( msDoc.GetTitle() );
							lvItem.SubItems.Add( msDoc.GetUrl() );

							foreach( string sLocale in htLocales.Keys ) {
								if( sLocale != null ) {
									if( htHrefLangs.ContainsKey( sLocale ) ) {
										MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[sLocale];
										lvItem.SubItems[sLocale].Text = msHrefLang.GetUrl();
									} else {
										lvItem.SubItems[sLocale].Text = "MISSSING";
									}
								}
							}
							
							lvListView.Items.Add( lvItem );

						} catch( Exception ex ) {
							debug_msg( string.Format( "MacroscopeDisplayHrefLang 2: {0}", ex.Message ) );
						}

					}

					
					
					
					
					
					
					
					
					
					

				}
		
			}
			
			lvListView.ResumeLayout();
		}

		/**************************************************************************/

	}

}
