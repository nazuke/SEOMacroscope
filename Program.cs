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
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace SEOMacroscope
{

	internal sealed class Program
	{

		/**************************************************************************/

		[STAThread]
		private static void Main ( string[] args )
		{
			ThreadPool.SetMaxThreads( 256, 256 );
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new MacroscopeMainForm () );
		}

		/**************************************************************************/

		public static void Exit ()
		{
			DebugMsg( "Program: Exiting" );
			Application.Exit();
		}
		
		/**************************************************************************/
				
		[Conditional( "DEVMODE" )]
		static void DebugMsg ( String sMsg )
		{
			System.Diagnostics.Debug.WriteLine( sMsg );
		}

		/**************************************************************************/

	}

}
