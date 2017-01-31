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

	abstract public class MacroscopeDisplayListView : Macroscope
	{

		/**************************************************************************/

		public MacroscopeMainForm MainForm;

		public ListView lvListView;

		/**************************************************************************/

		protected MacroscopeDisplayListView ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
		{
			MainForm = MainFormNew;
			lvListView = lvListViewNew;
		}

		/**************************************************************************/

		public void ClearData ()
		{
			if( this.MainForm.InvokeRequired )
			{
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.lvListView.Items.Clear();
						}
					)
				);
			}
			else
			{
				this.lvListView.Items.Clear();
			}
		}
		
		/**************************************************************************/
		
		public void RefreshData ( MacroscopeDocumentCollection DocCollection )
		{
			if( this.MainForm.InvokeRequired )
			{
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							Cursor.Current = Cursors.WaitCursor;
							this.RenderListView( DocCollection );
							Cursor.Current = Cursors.Default;
						}
					)
				);
			}
			else
			{
				Cursor.Current = Cursors.WaitCursor;
				this.RenderListView( DocCollection );
				Cursor.Current = Cursors.Default;
			}
		}

		/**************************************************************************/
		
		public void RefreshData ( MacroscopeDocumentCollection DocCollection, List<string> lList )
		{
			if( this.MainForm.InvokeRequired )
			{
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							Cursor.Current = Cursors.WaitCursor;
							this.RenderListView( DocCollection, lList );
							Cursor.Current = Cursors.Default;
						}
					)
				);
			}
			else
			{
				Cursor.Current = Cursors.WaitCursor;
				this.RenderListView( DocCollection, lList );
				Cursor.Current = Cursors.Default;
			}
		}

		/**************************************************************************/

		public void RefreshData ( MacroscopeDocument msDoc, string sUrl )
		{
			if( this.MainForm.InvokeRequired )
			{
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							Cursor.Current = Cursors.WaitCursor;
							this.RenderListView( msDoc, sUrl );
							Cursor.Current = Cursors.Default;
						}
					)
				);
			}
			else
			{
				Cursor.Current = Cursors.WaitCursor;
				this.RenderListView( msDoc, sUrl );
				Cursor.Current = Cursors.Default;
			}
		}

		/** Render Entire DocCollection *******************************************/

		public void RenderListView ( MacroscopeDocumentCollection DocCollection )
		{
			DebugMsg( string.Format( "RenderListView: {0}", "BASE" ) );
			foreach( string sUrl in DocCollection.DocumentKeys() )
			{
				MacroscopeDocument msDoc = DocCollection.GetDocument( sUrl );
				this.RenderListView( msDoc, sUrl );
			}
		}

		/** Render List ***********************************************************/
		
		public void RenderListView ( MacroscopeDocumentCollection DocCollection, List<string> lList )
		{
			foreach( string sUrl in lList )
			{
				MacroscopeDocument msDoc = DocCollection.GetDocument( sUrl );
				this.RenderListView( msDoc, sUrl );
			}
		}

		/** Render One ************************************************************/

		abstract protected void RenderListView ( MacroscopeDocument msDoc, string sUrl );

		/**************************************************************************/

	}

}
