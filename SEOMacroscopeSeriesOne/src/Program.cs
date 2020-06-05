/*

	This file is part of SEOMacroscope.

	Copyright 2020 Jason Holland.

	The GitHub repository may be found at:

		https://github.com/nazuke/SEOMacroscope

	SEOMacroscope is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	SEOMacroscope is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

using System.Collections.Generic;
using Mono.Options;

/*

  Reference:

  https://components.xamarin.com/gettingstarted/mono.options?version=5.3.0

 */

namespace SEOMacroscope
{

  internal sealed class Program
  {

    
    /**************************************************************************/

    [ STAThread]
    private static void Main ( string [] args )
    {




      // these variables will be set when the command line is parsed
      /*
      var verbosity = 0;
      var shouldShowHelp = false;
      var names = new List<string>();
      var repeat = 1;

      // thses are the available options, not that they set the variables
      var options = new OptionSet {
      { "n|name=", "the name of someone to greet.", n => names.Add (n) },
      { "r|repeat=", "the number of times to repeat the greeting.", (int r) => repeat = r },
      { "v", "increase debug message verbosity", v => {
      if (v != null)
      ++verbosity;
      } },
      { "h|help", "show this message and exit", h => shouldShowHelp = h != null },
      };
      */



      /*
      OptionSet CommandOptions = new OptionSet();
      CommandOptions.Add( "m|mode=", "The runtime mode.", m => (m) );
      */









      InitializeEventLog();
      
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

    private static void InitializeEventLog ()
    {
      /*
      if( !EventLog.SourceExists( MacroscopeConstants.MainEventLogSourceName ) )
      {
        EventLog.CreateEventSource(
          MacroscopeConstants.MainEventLogSourceName,
          MacroscopeConstants.MainEventLogJobMaster
        );
      }
      */
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
