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
		DataTable dtTable;

		/**************************************************************************/

		public MacroscopeDisplayHrefLang ( MacroscopeMainForm msMainFormNew )
		{
			msMainForm = msMainFormNew;
			dtTable = new DataTable ();
			msMainForm.GetDisplayHrefLang().DataSource = null;
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

			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker ( 
						delegate {
							msMainForm.GetDisplayHrefLang().SuspendLayout();
							msMainForm.GetDisplayHrefLang().DataSource = null;
						}
					) 
				);
			} else {
				msMainForm.GetDisplayHrefLang().SuspendLayout();
				msMainForm.GetDisplayHrefLang().DataSource = null;
			}

			this.dtTable.Rows.Clear();
			this.dtTable.Columns.Clear();
			this.dtTable.Clear();

			{
				this.dtTable.Columns.Add( "Site Locale", typeof( string ) );
				this.dtTable.Columns.Add( "Title", typeof( string ) );
				foreach( string sLocale in htLocales.Keys ) {
					this.dtTable.Columns.Add( sLocale, typeof( string ) );
				}
			}
			
			foreach( string sKeyURL in htDocCollection.Keys() ) {

				MacroscopeDocument msDoc = htDocCollection.Get( sKeyURL );

				if( msDoc.GetIsHtml() ) {
				
					Hashtable htHrefLangs = ( Hashtable )msDoc.GetHrefLangs();
					DataRow dtRow = this.dtTable.NewRow();

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

					this.dtTable.Rows.Add( dtRow );
				}
		
			}
			
			if( msMainForm.InvokeRequired ) {
				msMainForm.Invoke(
					new MethodInvoker ( 
						delegate {
							msMainForm.GetDisplayHrefLang().DataSource = this.dtTable;
							msMainForm.GetDisplayHrefLang().ResumeLayout();
						}
					) 
				);
			} else {
				msMainForm.GetDisplayHrefLang().DataSource = this.dtTable;
				msMainForm.GetDisplayHrefLang().ResumeLayout();
			}

		}
						
		/**************************************************************************/

	}

}
