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

	abstract public class MacroscopeDisplayTreeView : Macroscope
	{

		/**************************************************************************/

		public MacroscopeMainForm msMainForm;

		public TreeView tvTreeView;

		/**************************************************************************/

		public MacroscopeDisplayTreeView ( MacroscopeMainForm msMainFormNew, TreeView tvTreeViewNew )
		{
			msMainForm = msMainFormNew;
			tvTreeView = tvTreeViewNew;
		}

		/**************************************************************************/

		public void ClearData ()
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.tvTreeView.Nodes.Clear();
						}
					)
				);
			} else {
				this.tvTreeView.Nodes.Clear();
			}
		}
		
		/**************************************************************************/
		
		public void RefreshData ( MacroscopeDocumentCollection DocCollection )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.tvTreeView.BeginUpdate();
							this.RenderTreeView( DocCollection );
							this.tvTreeView.EndUpdate();
						}
					)
				);
			} else {
				this.tvTreeView.BeginUpdate();
				this.RenderTreeView( DocCollection );
				this.tvTreeView.EndUpdate();
			}
		}

		/**************************************************************************/
				
		/*
		public void RefreshData ( MacroscopeDocumentCollection DocCollection, List<string> lList )
		{
			if( this.msMainForm.InvokeRequired ) {
				this.msMainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							tvTreeView.BeginUpdate();
							this.RenderTreeView( DocCollection, lList );
							this.tvTreeView.EndUpdate();
						}
					)
				);
			} else {
				this.tvTreeView.BeginUpdate();
				this.RenderTreeView( DocCollection, lList );
				this.tvTreeView.EndUpdate();
			}
		}
		*/
		
		/** Render Entire DocCollection *******************************************/

		public void RenderTreeView ( MacroscopeDocumentCollection DocCollection )
		{
			DebugMsg( string.Format( "RenderListView: {0}", "BASE" ) );
			foreach( string sUrl in DocCollection.Keys() ) {
				MacroscopeDocument msDoc = DocCollection.Get( sUrl );
				this.RenderTreeView( msDoc, sUrl );
			}
		}

		/** Render One ************************************************************/

		abstract protected void RenderTreeView ( MacroscopeDocument msDoc, string sUrl );

		/**************************************************************************/

	}

}
