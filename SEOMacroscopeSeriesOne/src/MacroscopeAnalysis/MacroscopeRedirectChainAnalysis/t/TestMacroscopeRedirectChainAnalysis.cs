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
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeRedirectChainAnalysis : Macroscope
  {

    /**************************************************************************/

    const int MaxHops = 10;

    /**************************************************************************/

    [Test]
    public async Task TestAnalyzeRedirectChains ()
    {

      MacroscopeHttpTwoClient HttpClient = new MacroscopeHttpTwoClient();
      MacroscopeRedirectChainAnalysis Analyzer = new MacroscopeRedirectChainAnalysis( Client: HttpClient );
      List<MacroscopeRedirectChainDocStruct> AnalyzedRedirectChain;

      MacroscopePreferencesManager.SetRedirectChainsMaxHops( Max: 100 );

      AnalyzedRedirectChain = await Analyzer.AnalyzeRedirectChains(
        StatusCode: HttpStatusCode.Redirect,
        StartUrl: string.Format( "https://httpbin.org/redirect/{0}", MaxHops ),
        RedirectUrl: string.Format( "https://httpbin.org/redirect/{0}", MaxHops - 1 )
      );

      this.DebugMsg( string.Format( "AnalyzedRedirectChain: {0}", AnalyzedRedirectChain.GetHashCode() ) );

      Assert.AreEqual( MaxHops + 1, AnalyzedRedirectChain.Count );

    }

    /**************************************************************************/

  }

}
