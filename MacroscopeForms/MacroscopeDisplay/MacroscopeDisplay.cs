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
using System.Windows.Forms;

namespace SEOMacroscope
{

	abstract public class MacroscopeDisplay : Macroscope
	{

		/**************************************************************************/

		public MacroscopeMainForm msMainForm;

		public ListView lvListView;

		/**************************************************************************/

		public MacroscopeDisplay ( MacroscopeMainForm msMainFormNew, ListView lvListViewNew )
		{
			msMainForm = msMainFormNew;
			lvListView = lvListViewNew;
		}

		/**************************************************************************/

		public void ClearData ()
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.lvListView.Items.Clear();
						}
					)
				);
			} else {
				this.lvListView.Items.Clear();
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
							this.lvListView.BeginUpdate();
							this.RenderListView( htDocCollection );
							this.lvListView.EndUpdate();
						}
					)
				);
			} else {
				this.lvListView.BeginUpdate();
				this.RenderListView( htDocCollection );
				this.lvListView.EndUpdate();
			}
		}

		/**************************************************************************/
		
		public void RefreshData ( MacroscopeDocumentCollection htDocCollection, List<string> lList )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							lvListView.BeginUpdate();
							this.RenderListView( htDocCollection, lList );
							this.lvListView.EndUpdate();
						}
					)
				);
			} else {
				this.lvListView.BeginUpdate();
				this.RenderListView( htDocCollection, lList );
				this.lvListView.EndUpdate();
			}
		}

		/**************************************************************************/

		public void RefreshData ( MacroscopeDocument msDoc, string sUrl )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.lvListView.BeginUpdate();
							this.RenderListView( msDoc, sUrl );
							this.lvListView.EndUpdate();
						}
					)
				);
			} else {
				this.lvListView.BeginUpdate();
				this.RenderListView( msDoc, sUrl );
				this.lvListView.EndUpdate();
			}
		}

		/** Render Entire DocCollection *******************************************/

		public void RenderListView ( MacroscopeDocumentCollection htDocCollection )
		{
			foreach( string sUrl in htDocCollection.Keys() ) {
				MacroscopeDocument msDoc = htDocCollection.Get( sUrl );
				this.RenderListView( msDoc, sUrl );
			}
		}
		
		/** Render List ***********************************************************/
		
		public void RenderListView ( MacroscopeDocumentCollection htDocCollection, List<string> lList )
		{
			foreach( string sUrl in lList ) {
				MacroscopeDocument msDoc = htDocCollection.Get( sUrl );
				this.RenderListView( msDoc, sUrl );
			}
		}

		/** Render One ************************************************************/

		abstract protected void RenderListView ( MacroscopeDocument msDoc, string sUrl );

		/**************************************************************************/

	}

}
