using System;
using System.Collections;
using NUnit.Framework;

namespace SEOMacroscope
{
	
	[TestFixture]
	public class TestMacroscopeURLTools
	{

		static Hashtable htUrls = new Hashtable ();

		[Test]
		public void TestMethod ()
		{

			string sBaseUrl = "http://www.host.com/path/to/page/";
			string sFilename = "index.html";
			string sUrl = string.Join( "", sBaseUrl, sFilename );

			foreach( string sRelativeUrl in htUrls.Keys ) {
				string sAbsoluteUrl = MacroscopeURLTools.MakeUrlAbsolute( sUrl, sRelativeUrl );
				Assert.AreEqual( htUrls[ sRelativeUrl ], sAbsoluteUrl, "DO NOT MATCH" );
			}

		}
		
		[TestFixtureSetUp]
		public void Init ()
		{
			htUrls[ "path/to/images/picture.gif" ] = "http://www.host.com/path/to/page/path/to/images/picture.gif";
			htUrls[ "../path/to/images/picture.gif" ] = "http://www.host.com/path/to/path/to/images/picture.gif";
			htUrls[ "../../path/to/images/picture.gif" ] = "http://www.host.com/path/path/to/images/picture.gif";
		}
		
		[TestFixtureTearDown]
		public void Dispose ()
		{
		}
		
	}
	
}
