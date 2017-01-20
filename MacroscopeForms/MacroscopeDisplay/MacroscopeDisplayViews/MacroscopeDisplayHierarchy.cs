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
	
	/// <summary>
	/// Description of MacroscopeDisplayHierarchy.
	/// </summary>

	public class MacroscopeDisplayHierarchy : MacroscopeDisplayTreeView
	{

		/**************************************************************************/
		
		static Boolean ListViewConfigured = false;
		
		/**************************************************************************/

		public MacroscopeDisplayHierarchy ( MacroscopeMainForm msMainFormNew, TreeView tvTreeView )
			: base( msMainFormNew, tvTreeView )
		{
		}

		/**************************************************************************/

		void ConfigureListView ()
		{
			if( !ListViewConfigured ) {
			}
		}

		/**************************************************************************/

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
		
		/**************************************************************************/

		void RenderTreeView ( MacroscopeDocumentCollection DocCollection, List<string> lList )
		{
			
			DebugMsg( string.Format( "RenderTreeView: {0}", "BASE" ) );
			
			foreach( string sUrl in lList ) {
				MacroscopeDocument msDoc = DocCollection.Get( sUrl );
				this.RenderTreeView( msDoc, sUrl );
			}
		
		}

		/**************************************************************************/
		
		protected override void RenderTreeView ( MacroscopeDocument msDoc, string sUrl )
		{
			
			//tvTreeView.Nodes
			
			
			
			
		}

		/**************************************************************************/
				
	}

}
