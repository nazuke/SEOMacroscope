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
using System.Drawing;
using System.Windows.Forms;

namespace SEOMacroscope
{

	public class MacroscopeDisplayRobots : MacroscopeDisplayListView
	{
		
		/**************************************************************************/

		static Boolean ListViewConfigured = false;
		
		/**************************************************************************/

		public MacroscopeDisplayRobots ( MacroscopeMainForm MainFormNew, ListView lvListViewNew )
			: base( MainFormNew, lvListViewNew )
		{

			MainForm = MainFormNew;
			lvListView = lvListViewNew;
			
			if( MainForm.InvokeRequired )
			{
				MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							ConfigureListView();
						}
					)
				);
			}
			else
			{
				ConfigureListView();
			}

		}

		/**************************************************************************/
		
		void ConfigureListView ()
		{
			if( !ListViewConfigured )
			{
				ListViewConfigured = true;
			}
		}

		/**************************************************************************/

		public void RefreshData ( MacroscopeJobMaster JobMaster )
		{
			if( this.MainForm.InvokeRequired )
			{
				this.MainForm.Invoke(
					new MethodInvoker (
						delegate
						{
							this.RenderListView( JobMaster );
						}
					)
				);
			}
			else
			{
				this.RenderListView( JobMaster );
			}
		}

		/**************************************************************************/

		public void RenderListView ( MacroscopeJobMaster JobMaster )
		{

			Dictionary<String,Boolean> dicBlocked = JobMaster.GetBlockedByRobotsList();

			foreach( string sUrl in dicBlocked.Keys )
			{
				this.RenderListView( sUrl, dicBlocked [ sUrl ] );
			}

		}

		/**************************************************************************/

		void RenderListView ( string sUrl, Boolean bBlocked )
		{

			string sPairKey = string.Join( "", sUrl );

			ListViewItem lvItem = null;

			if( this.lvListView.Items.ContainsKey( sPairKey ) )
			{
				
				try
				{

					lvItem = this.lvListView.Items [ sPairKey ];
					lvItem.SubItems [ 0 ].Text = sUrl;
					lvItem.SubItems [ 1 ].Text = bBlocked.ToString();

				}
				catch( Exception ex )
				{
					DebugMsg( string.Format( "MacroscopeDisplayRobots 1: {0}", ex.Message ) );
				}

			}
			else
			{
				
				try
				{

					lvItem = new ListViewItem ( sPairKey );
					lvItem.UseItemStyleForSubItems = false;
					lvItem.Name = sPairKey;

					lvItem.SubItems [ 0 ].Text = sUrl;
					lvItem.SubItems.Add( bBlocked.ToString() );

					this.lvListView.Items.Add( lvItem );

				}
				catch( Exception ex )
				{
					DebugMsg( string.Format( "MacroscopeDisplayRobots 2: {0}", ex.Message ) );
				}

			}
			
			if( lvItem != null )
			{

				lvItem.ForeColor = Color.Blue;

			}

		}
		
		/**************************************************************************/

		protected override void RenderListView ( MacroscopeDocument msDoc, string sUrl )
		{
		}
		
		/**************************************************************************/

	}


}
