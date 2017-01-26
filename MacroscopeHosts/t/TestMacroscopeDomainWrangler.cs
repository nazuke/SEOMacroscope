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
using NUnit.Framework;

namespace SEOMacroscope
{
	
	[TestFixture]
	public class TestMacroscopeDomainWrangler
	{
	
		/**************************************************************************/

		[Test]
		public void TestIsWithinSameDomain ()
		{
			
			MacroscopeDomainWrangler domainwrangler = new MacroscopeDomainWrangler ();

			Assert.AreEqual( domainwrangler.IsWithinSameDomain(
				"www.host.com",
				"www.subdomain.host.com"
			), true, string.Format( "NOT VALID: {0}", 1 ) );

			Assert.AreEqual( domainwrangler.IsWithinSameDomain( 
				"www.host.co.uk",
				"www.subdomain.host.co.uk"
			), true, string.Format( "NOT VALID: {0}", 2 ) );

			Assert.AreEqual( domainwrangler.IsWithinSameDomain(
				"www.host.co.uk", 
				"www.subdomain.host.com" 
			), false, string.Format( "NOT VALID: {0}", 3 ) );

			Assert.AreEqual( domainwrangler.IsWithinSameDomain( 
				"host.co.uk", 
				"subdomain.host.co.uk"
			), true, string.Format( "NOT VALID: {0}", 4 ) );

			Assert.AreEqual( domainwrangler.IsWithinSameDomain(
				"co.uk", 
				"www.host.co.uk" 
			), false, string.Format( "NOT VALID: {0}", 5 ) );
			
			Assert.AreEqual( domainwrangler.IsWithinSameDomain(
				"bongo.bongo.bongo.com.cn", 
				"www.bongo.com.cn" 
			), true, string.Format( "NOT VALID: {0}", 6 ) );
			
			Assert.AreEqual( domainwrangler.IsWithinSameDomain(
				"someuser.acmebloghost.com", 
				"someotheruser.acmebloghost.com",
				3				
			), false, string.Format( "NOT VALID: {0}", 6 ) );
						
		}

		/**************************************************************************/

	}

}
