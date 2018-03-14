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
using System.Xml;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeSitemapGenerator : Macroscope
  {

    /**************************************************************************/

    [Test]
    public void TestWriteSitemapXml ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( "https://nazuke.github.io/" ) ) );
      string Filename = string.Join( ".", Path.GetTempFileName(), ".xml" );
      SitemapGenerator.WriteSitemapXml( NewPath: Filename );
      Assert.IsTrue( File.Exists( Filename ) );
      if( File.Exists( Filename ) )
      {
        File.Delete( Filename );
      }
    }


    /**************************************************************************/

    [Test]
    public void TestWriteSitemapXmlWithEmptyDocCollection ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      JobMaster.SetStartUrl( "https://nazuke.github.io/" );
      string Filename = string.Join( ".", Path.GetTempFileName(), ".xml" );
      SitemapGenerator.WriteSitemapXml( NewPath: Filename );
      Assert.IsFalse( File.Exists( Filename ) );
      if( File.Exists( Filename ) )
      {
        File.Delete( Filename );
      }
    }

    /**************************************************************************/

    /*
    [Test]
    public void TestWriteSitemapXmlPerHost ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( "https://nazuke.github.io/" ) ) );
      string Filename = string.Join( ".", Path.GetTempFileName(), ".xml" );
      SitemapGenerator.WriteSitemapXml( NewPath: Filename );
      Assert.IsTrue( File.Exists( Filename ) );
      if ( File.Exists( Filename ) )
      {
        File.Delete( Filename );
      }
    }
    */

    /**************************************************************************/

    [Test]
    public void TestWriteSitemapText ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( "https://nazuke.github.io/" ) ) );
      string Filename = string.Join( ".", Path.GetTempFileName(), ".txt" );
      SitemapGenerator.WriteSitemapText( NewPath: Filename );
      Assert.IsTrue( File.Exists( Filename ) );
      if( File.Exists( Filename ) )
      {
        File.Delete( Filename );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestWriteSitemapTextWithEmptyDocCollection ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      JobMaster.SetStartUrl( "https://nazuke.github.io/" );
      string Filename = string.Join( ".", Path.GetTempFileName(), ".txt" );
      SitemapGenerator.WriteSitemapText( NewPath: Filename );
      Assert.IsFalse( File.Exists( Filename ) );
      if( File.Exists( Filename ) )
      {
        File.Delete( Filename );
      }
    }
    
    /**************************************************************************/

    /*
    [Test]
    public void TestWriteSitemapTextPerHost ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( "https://nazuke.github.io/" ) ) );
      string Filename = string.Join( ".", Path.GetTempFileName(), ".txt" );
      SitemapGenerator.WriteSitemapText( NewPath: Filename );
      Assert.IsTrue( File.Exists( Filename ) );
      if ( File.Exists( Filename ) )
      {
        File.Delete( Filename );
      }
    }
    */

    /**************************************************************************/

    [Test]
    public void TestGenerateXmlSitemap ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( "https://nazuke.github.io/" ) ) );
      XmlDocument SitemapXML = SitemapGenerator.GenerateXmlSitemap( Host: "nazuke.github.io" );
      Assert.AreEqual( "urlset", SitemapXML.DocumentElement.LocalName );
    }

    /**************************************************************************/

    /*
    [Test]
    public void TestGenerateTextSitemap ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( "https://nazuke.github.io/" ) ) ).SetIsInternal( true );
      List<string> SitemapTxt = SitemapGenerator.GenerateTextSitemap( Host: "nazuke.github.io" );
      Assert.Greater( 0, SitemapTxt.Count );
    }
    */

    /**************************************************************************/

  }

}
