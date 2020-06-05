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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using HtmlAgilityPack;

using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestExecuteRequest : Macroscope, IMacroscopeTaskController
  {

    /**************************************************************************/

    private int MaxUrls = 0;
    private List<string> Urls;

    /**************************************************************************/

    public TestExecuteRequest ()
    {
      this.Urls = new List<string>( MaxUrls );
      for( int i = 0 ; i < this.MaxUrls ; i++ )
      {
        this.Urls.Add( string.Format( "https://nazuke.github.io/MacroscopeTestHarness/?page={0}", i ) );
      }
    }

    /**************************************************************************/

    [Test]
    public async Task TestNRequests ()
    {

      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE, TaskController: this );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );

      Assert.AreEqual( 0, DocCollection.CountDocuments() );

      foreach( string Url in this.Urls )
      {
        MacroscopeDocument msDoc = DocCollection.CreateDocument( Url: Url );
        await msDoc.Execute();
      }

      Assert.AreEqual( this.MaxUrls, DocCollection.CountDocuments() );

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
