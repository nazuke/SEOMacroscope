﻿/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Collections.Generic;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeJobWorker
  {

    /**************************************************************************/

    //[Test] // Not implemented
    public void TestWorker ()
    {

      List<string> alUrls = new List<string>();

      alUrls.Add( "http://localhost/" );

      for( int i = 0 ; i < alUrls.Count ; i++ )
      {

        MacroscopeJobMaster JobMaster;

        JobMaster = new MacroscopeJobMaster( JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE );

        MacroscopeJobWorker Worker = new MacroscopeJobWorker( JobMaster );

        JobMaster.AddUrlQueueItem( alUrls[ i ] );

        Worker.Execute();

      }

    }

    /**************************************************************************/

  }

}
