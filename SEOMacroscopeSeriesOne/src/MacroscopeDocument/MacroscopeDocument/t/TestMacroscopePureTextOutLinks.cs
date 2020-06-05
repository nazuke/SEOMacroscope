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
  public class TestMacroscopePureTextOutLinks : Macroscope, IMacroscopeTaskController
  {

    /**************************************************************************/

    private string TextDoc;
    private List<string> TextLinks;

    /**************************************************************************/

    public TestMacroscopePureTextOutLinks ()
    {
      StreamReader Reader;
      List<string> DocKeys = new List<string>( 16 );
      string Filename = "SEOMacroscope.src.MacroscopeDocument.MacroscopeDocument.t.TextDocs.TestPureTextOutLinks001.txt";
      this.TextLinks = new List<string>( 5 );
      this.TextLinks.Add( @"https://www.megacorp.com/path/to/page.html" );
      this.TextLinks.Add( @"http://www.company.com/path/to/page.html" );
      this.TextLinks.Add( @"https://www.megacorp.com/some/cool/article" );
      this.TextLinks.Add( @"https://www.megacorp.com/another/cool/article/" );
      this.TextLinks.Add( @"https://nazuke.github.com/SEOMacroscope/" );
      Reader = new StreamReader( Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename ) );
      this.TextDoc = Reader.ReadToEnd();
      Reader.Close();
    }

    /**************************************************************************/

    [Test]
    public void TestLinksInTextDocs ()
    {

      string Url = @"https://nazuke.github.io/dummy.txt";
      MacroscopeJobMaster JobMaster;
      MacroscopeDocumentCollection DocCollection;

      JobMaster = new MacroscopeJobMaster(
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );

      MacroscopeDocument msDoc = DocCollection.CreateDocument( Url: Url );

      Assert.IsNotNull( msDoc, string.Format( "FAIL: {0}", Url ) );

      msDoc.ProcessPureTextOutlinks( TextDoc: this.TextDoc, LinkType: MacroscopeConstants.InOutLinkType.PURETEXT );

      foreach ( MacroscopeLink Outlink in msDoc.IterateOutlinks() )
      {
        Assert.Contains( Outlink.GetTargetUrl(), this.TextLinks );
      }

      Assert.AreEqual( 5, msDoc.CountOutlinks() );

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
