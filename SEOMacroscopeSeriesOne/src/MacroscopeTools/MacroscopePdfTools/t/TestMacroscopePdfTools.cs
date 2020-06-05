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
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopePdfTools : Macroscope
  {

    // TODO: Finish this:

    /**************************************************************************/

    private Dictionary<string, byte[]> PdfDocsData;
    private Dictionary<string, string> PdfDocsTitle;
    // TODO: Add description testing
    //private Dictionary<string, string> PdfDocsDescription;

    /**************************************************************************/

    public TestMacroscopePdfTools ()
    {

      Dictionary<string, string> TestDocuments = new Dictionary<string, string>( 16 );
      this.PdfDocsData = new Dictionary<string, byte[]>( 16 );
      this.PdfDocsTitle = new Dictionary<string, string>( 16 );

      TestDocuments.Add(
        "SEOMacroscope.src.MacroscopeTools.MacroscopePdfTools.t.PdfDocs.MacroscopePdfToolsExample001.pdf",
        "New Rich Text Document.rtf"
      );

      foreach ( string Filename in TestDocuments.Keys )
      {

        Stream Reader = Assembly.GetExecutingAssembly().GetManifestResourceStream( Filename );
        byte[] PdfData = new byte[ Reader.Length ];

        for ( int i = 0 ; i < Reader.Length ; i++ )
        {
          PdfData[ i ] = (byte) Reader.ReadByte();
        }

        this.PdfDocsData.Add( Filename, PdfData );
        this.PdfDocsTitle.Add( Filename, TestDocuments[ Filename ] );

        Reader.Close();

      }

    }

    /**************************************************************************/

    [Test]
    public void TestLoadPdf ()
    {
      foreach ( string Filename in this.PdfDocsData.Keys )
      {
        MacroscopePdfTools PdfTools = new MacroscopePdfTools( PdfData: this.PdfDocsData[ Filename ] );
        Assert.IsFalse( PdfTools.GetHasError() );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestPdfTitle ()
    {
      foreach ( string Filename in this.PdfDocsData.Keys )
      {
        MacroscopePdfTools PdfTools = new MacroscopePdfTools( PdfData: this.PdfDocsData[ Filename ] );
        Assert.AreEqual( this.PdfDocsTitle[ Filename ], PdfTools.GetTitle() );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestPdfDescription ()
    {
      foreach ( string Filename in this.PdfDocsData.Keys )
      {
        MacroscopePdfTools PdfTools = new MacroscopePdfTools( PdfData: this.PdfDocsData[ Filename ] );
        //Assert.AreEqual( this.PdfDocsDescription[ Filename ], PdfTools.GetDescription() );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestPdfTextAsList ()
    {
      foreach ( string Filename in this.PdfDocsData.Keys )
      {
        MacroscopePdfTools PdfTools = new MacroscopePdfTools( PdfData: this.PdfDocsData[ Filename ] );
        Assert.Greater( PdfTools.GetTextAsList().Count, 0 );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestPdfTextAsString ()
    {
      foreach ( string Filename in this.PdfDocsData.Keys )
      {
        MacroscopePdfTools PdfTools = new MacroscopePdfTools( PdfData: this.PdfDocsData[ Filename ] );
        Assert.Greater( PdfTools.GetTextAsString().Length, 0 );
      }
    }

    /**************************************************************************/

  }

}
