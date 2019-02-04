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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDocumentWithCookies : Macroscope, IMacroscopeTaskController
  {

    /**************************************************************************/

    [Test]
    public async Task TestDocumentCookiesAreSet ()
    {

      MacroscopeJobMaster JobMaster;
      MacroscopeDocumentCollection DocCollection;

      List<string> UrlList = new List<string>(2);

      UrlList.Add( "https://httpbin.org/cookies/set?freeform=bongo" );
      UrlList.Add( "https://httpbin.org/cookies" );

      JobMaster = new MacroscopeJobMaster(
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );

      { // Set Cookies
        MacroscopeDocument msDoc = DocCollection.CreateDocument( Url: UrlList[ 0 ] );
        bool ExecuteResult = await msDoc.Execute();
        //Assert.IsTrue( ExecuteResult, string.Format( "FAIL: {0}", "Execute()" ) );
      }

      { // Get Cookies
        MacroscopeDocument msDoc = DocCollection.CreateDocument( Url: UrlList[ 1 ] );
        bool ExecuteResult = await msDoc.Execute();
        //Assert.IsTrue( ExecuteResult, string.Format( "FAIL: {0}", "Execute()" ) );
      }

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
