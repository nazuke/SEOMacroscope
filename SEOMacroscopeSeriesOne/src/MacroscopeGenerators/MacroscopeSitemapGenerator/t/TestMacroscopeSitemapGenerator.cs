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
using System.Xml;
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeSitemapGenerator : Macroscope
  {

    /**************************************************************************/

    private List<string> Urls;

    /**************************************************************************/

    public TestMacroscopeSitemapGenerator ()
    {
      this.Urls = new List<string>();
      this.Urls.Add( "https://nazuke.github.io" );
      this.Urls.Add( "https://nazuke.github.io/" );
      this.Urls.Add( "https://nazuke.github.io/SEOMacroscope/" );
      this.Urls.Add( "https://nazuke.github.io/SEOMacroscope" );
    }

    /**************************************************************************/

    [Test]
    public void TestWriteSitemapXml ()
    {
      foreach( string Url in this.Urls )
      {
        MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
        MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
        MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
        DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( Url: Url ) ) );
        string Filename = string.Join( ".", Path.GetTempFileName(), ".xml" );
        SitemapGenerator.WriteSitemapXml( NewPath: Filename );
        Assert.IsTrue( File.Exists( Filename ) );
        if( File.Exists( Filename ) )
        {
          File.Delete( Filename );
        }
      }
    }

    /**************************************************************************/

    [Test]
    public void TestWriteSitemapXmlWithEmptyDocCollection ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      string Filename = string.Join( ".", Path.GetTempFileName(), ".xml" );
      try
      {
        SitemapGenerator.WriteSitemapXml( NewPath: Filename );
      }
      catch( Exception ex )
      {
        Assert.AreEqual( ex.GetType(), new MacroscopeSitemapException().GetType() );
      }
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
      foreach( string Url in this.Urls )
      {
        MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
        MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
        MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
        DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( Url: Url ) ) );
        string Filename = string.Join( ".", Path.GetTempFileName(), ".txt" );
        SitemapGenerator.WriteSitemapText( NewPath: Filename );
        Assert.IsTrue( File.Exists( Filename ) );
        if( File.Exists( Filename ) )
        {
          File.Delete( Filename );
        }
      }
    }

    /**************************************************************************/

    [Test]
    public void TestWriteSitemapTextWithEmptyDocCollection ()
    {
      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
      MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
      string Filename = string.Join( ".", Path.GetTempFileName(), ".txt" );
      try
      {
        SitemapGenerator.WriteSitemapText( NewPath: Filename );
      }
      catch( Exception ex )
      {
        Assert.AreEqual( ex.GetType(), new MacroscopeSitemapException().GetType() );
      }
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
      foreach( string Url in this.Urls )
      {
        MacroscopeJobMaster JobMaster = new MacroscopeJobMaster( MacroscopeConstants.RunTimeMode.LIVE );
        MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );
        MacroscopeSitemapGenerator SitemapGenerator = new MacroscopeSitemapGenerator( NewDocCollection: DocCollection );
        DocCollection.AddDocument( new MacroscopeDocument( JobMaster.SetStartUrl( Url: Url ) ) );
        XmlDocument SitemapXML = SitemapGenerator.GenerateXmlSitemap( Host: new Uri( Url ).Host );
        Assert.AreEqual( "urlset", SitemapXML.DocumentElement.LocalName );
      }
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
