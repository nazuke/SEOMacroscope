/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Collections.Generic;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeJobMaster : IMacroscopeTaskController
  {

    /**************************************************************************/

    [Test]
    public void TestJobMasterStartUrl ()
    {

      MacroscopeJobMaster JobMaster;
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/index.html";

      JobMaster = new MacroscopeJobMaster(
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      JobMaster.SetStartUrl( Url: StartUrl );

      Assert.AreEqual( StartUrl, JobMaster.GetStartUrl(), string.Format( "FAIL: {0}", StartUrl ) );

    }

    /**************************************************************************/

    public void ICallbackScanComplete ()
    {
    }

    public MacroscopeCredentialsHttp IGetCredentialsHttp ()
    {
      MacroscopeCredentialsHttp CredentialsHttp = new MacroscopeCredentialsHttp();
      return ( CredentialsHttp );
    }

    public void ICallbackOutOfMemory ()
    {
    }

    /**************************************************************************/

  }

}
