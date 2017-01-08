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
