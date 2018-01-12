/*

	This file is part of SEOMacroscope.

	Copyright 2018 Jason Holland.

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
    public void TestIsWithinParentDirectory ()
    {

      MacroscopeJobMaster JobMaster;
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/index.html";
      List<string> TargetUrls = new List<string> ();

      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/image" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/page.jsp" );
      TargetUrls.Add( "http://www.companyname.com/path/to/file.pdf" );
      TargetUrls.Add( "http://www.companyname.com/path/" );
      TargetUrls.Add( "http://www.companyname.com/path/index.php" );
      TargetUrls.Add( "http://www.companyname.com/" );

      JobMaster = new MacroscopeJobMaster (
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      JobMaster.SetStartUrl( Url: StartUrl );

      JobMaster.DetermineStartingDirectory();

      foreach( string TargetUrl in TargetUrls )
      {
        Assert.IsTrue( JobMaster.IsWithinParentDirectory( TargetUrl ), string.Format( "FAIL: {0}", TargetUrl ) );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestIsNotWithinParentDirectory ()
    {

      MacroscopeJobMaster JobMaster;
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/index.html";
      List<string> TargetUrls = new List<string> ();
      
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/sub-folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/sub-folder/sub-folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/images/some-image.jpg" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/folder/media/image" );

      JobMaster = new MacroscopeJobMaster (
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      JobMaster.SetStartUrl( Url: StartUrl );

      JobMaster.DetermineStartingDirectory();

      foreach( string TargetUrl in TargetUrls )
      {
        Assert.IsFalse( JobMaster.IsWithinParentDirectory( TargetUrl ), string.Format( "FAIL: {0}", TargetUrl ) );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestIsWithinChildDirectory ()
    {

      MacroscopeJobMaster JobMaster;
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/index.html";
      List<string> TargetUrls = new List<string> ();
      
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/sub-folder/sub-folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/sub-folder/image" );

      JobMaster = new MacroscopeJobMaster (
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      JobMaster.SetStartUrl( Url: StartUrl );

      JobMaster.DetermineStartingDirectory();

      foreach( string Url in TargetUrls )
      {
        Assert.IsTrue( JobMaster.IsWithinChildDirectory( Url ), string.Format( "FAIL: {0}", Url ) );
      }

    }

    /**************************************************************************/

    [Test]
    public void TestIsNotWithinChildDirectory ()
    {

      MacroscopeJobMaster JobMaster;
      const string StartUrl = "http://www.companyname.com/path/to/some/deep/folder/sub-folder/sub-folder/index.html";
      List<string> TargetUrls = new List<string> ();

      TargetUrls.Add( "http://www.companyname.com/path/to/some/deep/folder/index.html" );
      TargetUrls.Add( "http://www.companyname.com/path/to/some/folder/image" );
      TargetUrls.Add( "http://www.companyname.com/folder/image" );

      JobMaster = new MacroscopeJobMaster (
        JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
        TaskController: this
      );

      JobMaster.SetStartUrl( Url: StartUrl );

      JobMaster.DetermineStartingDirectory();

      foreach( string Url in TargetUrls )
      {
        Assert.IsFalse( JobMaster.IsWithinChildDirectory( Url ), string.Format( "FAIL: {0}", Url ) );
      }

    }

    /**************************************************************************/

    public void ICallbackScanComplete ()
    {
    }

    public MacroscopeCredentialsHttp IGetCredentialsHttp ()
    {
      MacroscopeCredentialsHttp CredentialsHttp = new MacroscopeCredentialsHttp ();
      return( CredentialsHttp );
    }

    public void ICallbackOutOfMemory ()
    {
    }

    /**************************************************************************/

  }

}
