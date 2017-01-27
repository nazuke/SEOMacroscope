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


			Assert.IsTrue( 
				domainwrangler.IsWithinSameDomain(
					"www.host.com",
					"www.subdomain.host.com"
				), 
				string.Format( "NOT VALID: {0}", 1 )
			);


			Assert.IsTrue(
				domainwrangler.IsWithinSameDomain(
					"www.host.co.uk",
					"www.subdomain.host.co.uk"
				), string.Format( "NOT VALID: {0}", 2 ) 
			);


			Assert.IsFalse( 
				domainwrangler.IsWithinSameDomain(
					"www.host.co.uk", 
					"www.subdomain.host.com" 
				), string.Format( "NOT VALID: {0}", 3 ) 
			);


			Assert.IsTrue(
				domainwrangler.IsWithinSameDomain(
					"host.co.uk", 
					"subdomain.host.co.uk"
				), string.Format( "NOT VALID: {0}", 4 )
			);


			Assert.IsFalse(
				domainwrangler.IsWithinSameDomain(
					"co.uk", 
					"www.host.co.uk" 
				), 
				string.Format( "NOT VALID: {0}", 5 )
			);


			Assert.IsTrue(
				domainwrangler.IsWithinSameDomain(
					"bongo.bongo.bongo.com.cn", 
					"www.bongo.com.cn" 
				), 
				string.Format( "NOT VALID: {0}", 6 )
			);


			Assert.IsFalse(
				domainwrangler.IsWithinSameDomain(
					"someuser.acmebloghost.com", 
					"someotheruser.acmebloghost.com",
					3				
				), 
				string.Format( "NOT VALID: {0}", 7 )
			);


			Assert.IsTrue(
				domainwrangler.IsWithinSameDomain(
					"bongo.com", 
					"www.bongo.com"
				), 
				string.Format( "NOT VALID: {0}", 8 )
			);


		}

		/**************************************************************************/

	}

}
