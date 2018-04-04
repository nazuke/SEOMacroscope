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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using ExCSS;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeDocumentCSS : Macroscope
  {

    /**************************************************************************/

    private Dictionary<string, string> CssGoodDocs;
    private Dictionary<string, string> CssBadDocs;

    /**************************************************************************/

    public TestMacroscopeDocumentCSS ()
    {

      StreamReader Reader;
      List<string> CssGoodDocKeys = new List<string>( 16 );
      List<string> CssBadDocKeys = new List<string>( 16 );

      this.CssGoodDocs = new Dictionary<string, string>();
      this.CssBadDocs = new Dictionary<string, string>();

      CssGoodDocKeys.Add( "SEOMacroscope.src.MacroscopeDocument.MacroscopeDocument.DocumentTypes.t.CssDocs.TestCssDocumentGood001.css" );

      CssBadDocKeys.Add( "SEOMacroscope.src.MacroscopeDocument.MacroscopeDocument.DocumentTypes.t.CssDocs.TestCssDocumentBad001.css" );

      foreach( string Filename in CssGoodDocKeys )
      {
        Reader = new StreamReader(
         stream: Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
       );
        this.CssGoodDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

      foreach( string Filename in CssBadDocKeys )
      {
        Reader = new StreamReader(
         stream: Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename )
       );
        this.CssBadDocs.Add( Filename, Reader.ReadToEnd() );
        Reader.Close();
      }

    }

    /**************************************************************************/

    [Test]
    public void TestSimpleCssParsing ()
    {
      StylesheetParser ExCssParser = new StylesheetParser();
      foreach( string Filename in CssGoodDocs.Keys )
      {
        string CssData = CssGoodDocs[ Filename ];
        Stylesheet CssStylesheet = ExCssParser.Parse( CssData );
        Assert.IsNotNull( CssStylesheet, string.Format( "FAIL: {0}", Filename ) );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestCssRules ()
    {

      this.SuppressDebugMsg = false;

      StylesheetParser CssParser = new StylesheetParser();

      foreach( string Filename in CssGoodDocs.Keys )
      {

        string CssData = CssGoodDocs[ Filename ];
        Stylesheet CssStylesheet = CssParser.Parse( CssData );




        this.RecurseNodeTypes( Node: CssStylesheet );




        //Assert.IsNotNull( CssStylesheet, string.Format( "FAIL: {0}", Filename ) );

      }

      return;

    }




    private void RecurseNodeTypes ( IStylesheetNode Node )
    {

      if( Node != null )
      {
        try
        {
          this.DebugMsg( string.Format( "NODE GetType: {0}", Node.GetType() ) );
          this.DebugMsg( string.Format( "NODE TOCSS: {0}", Node.ToCss() ) );
        }
        catch( Exception ex )
        {
        }
      }
      else
      {
        this.DebugMsg( string.Format( "NODE: {0}", "UNKNOWN" ) );
      }

      foreach( var SubNode in Node.Children )
      {
        this.RecurseNodeTypes( Node: SubNode );
      }

      return;

    }






    /**************************************************************************/

    [Test]
    public void TestBadCss ()
    {
      StylesheetParser ExCssParser = new StylesheetParser();
      foreach( string Filename in CssBadDocs.Keys )
      {
        string CssData = CssBadDocs[ Filename ];
        Stylesheet CssStylesheet = ExCssParser.Parse( CssData );
        Assert.IsNotNull( CssStylesheet, string.Format( "FAIL: {0}", Filename ) );
      }
    }

    /**************************************************************************/

  }

}
