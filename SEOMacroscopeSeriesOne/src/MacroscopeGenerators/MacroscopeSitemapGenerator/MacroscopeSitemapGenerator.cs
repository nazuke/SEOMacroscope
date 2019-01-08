/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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
using System.Net;
using System.Xml;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeSitemapGenerator.
  /// </summary>

  public class MacroscopeSitemapGenerator : Macroscope
  {

    /**************************************************************************/

    private const string XmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

    private MacroscopeDocumentCollection DocCollection;

    /**************************************************************************/

    public MacroscopeSitemapGenerator ( MacroscopeDocumentCollection NewDocCollection )
    {
      this.SuppressDebugMsg = true;
      this.DocCollection = NewDocCollection;
    }

    /**************************************************************************/

    public void WriteSitemapXml ( string NewPath )
    {

      string StartUriHostAndPort = null;
      string XmlSitemapSerialized = null;
      XmlDocument SitemapXml;
      StringWriter SitemapXmlStringWriter;
      XmlTextWriter SitemapXmlTextWriter;
      try
      {

        StartUriHostAndPort = this.DocCollection.GetJobMaster().GetStartUriHostAndPort();

        if( StartUriHostAndPort != null )
        {
          SitemapXml = this.GenerateXmlSitemap( Host: StartUriHostAndPort );
          SitemapXmlStringWriter = new StringWriter();
          SitemapXmlTextWriter = new XmlTextWriter( SitemapXmlStringWriter );
          SitemapXml.WriteTo( SitemapXmlTextWriter );
          XmlSitemapSerialized = SitemapXmlStringWriter.ToString();
          File.WriteAllText( NewPath, XmlSitemapSerialized, new System.Text.UTF8Encoding( false ) );
        }

      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "WriteSitemapXml: {0}", ex.Message ) );
        if( File.Exists( NewPath ) )
        {
          File.Delete( NewPath );
        }
        throw new MacroscopeSitemapException( "An error occurred whilst attempting to save the Sitemap XML file." );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void WriteSitemapXmlPerHost ( string NewPath )
    {

      Dictionary<string, int> HostsList = this.DocCollection.GetStatsHostnamesWithCount();

      foreach( string Host in HostsList.Keys )
      {

        string Pathname;
        string Filename;
        string NewPathname;
        string XmlSitemapSerialized = null;
        XmlDocument SitemapXml = this.GenerateXmlSitemap( Host: Host );
        StringWriter SitemapXmlStringWriter = new StringWriter();
        XmlTextWriter SitemapXmlTextWriter = new XmlTextWriter( SitemapXmlStringWriter );

        SitemapXml.WriteTo( SitemapXmlTextWriter );

        XmlSitemapSerialized = SitemapXmlStringWriter.ToString();

        Pathname = Path.GetDirectoryName( NewPath );
        Filename = Path.GetFileNameWithoutExtension( NewPath );

        NewPathname = string.Join(
          ".",
          string.Join(
            Path.DirectorySeparatorChar.ToString(),
            Pathname,
            string.Join( "-", Filename, Host )
          ),
          "xml"
        );

        File.WriteAllText(
          NewPathname,
          XmlSitemapSerialized,
          new System.Text.UTF8Encoding( false )
        );

      }

    }

    /**************************************************************************/

    public void WriteSitemapText ( string NewPath )
    {

      string StartUriHostAndPort = null;
      List<string> SitemapText;

      try
      {

        StartUriHostAndPort = this.DocCollection.GetJobMaster().GetStartUriHostAndPort();

        if( StartUriHostAndPort != null )
        {
          SitemapText = this.GenerateTextSitemap( Host: StartUriHostAndPort );
          File.WriteAllLines( NewPath, SitemapText, new System.Text.UTF8Encoding( false ) );
        }

      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "WriteSitemapText: {0}", ex.Message ) );
        if( File.Exists( NewPath ) )
        {
          File.Delete( NewPath );
        }
        throw new MacroscopeSitemapException( "An error occurred whilst attempting to save the Text Sitemap file." );
      }

    }

    /** -------------------------------------------------------------------- **/

    public void WriteSitemapTextPerHost ( string NewPath )
    {

      Dictionary<string, int> HostsList = this.DocCollection.GetStatsHostnamesWithCount();

      foreach( string Host in HostsList.Keys )
      {

        List<string> SitemapText;
        string Pathname;
        string Filename;
        string NewPathname;

        SitemapText = this.GenerateTextSitemap( Host: Host );

        Pathname = Path.GetDirectoryName( NewPath );
        Filename = Path.GetFileNameWithoutExtension( NewPath );

        NewPathname = string.Join(
          ".",
          string.Join(
            Path.DirectorySeparatorChar.ToString(),
            Pathname,
            string.Join( "-", Filename, Host )
          ),
          "txt"
        );

        File.WriteAllLines( NewPathname, SitemapText, new System.Text.UTF8Encoding( false ) );

      }

    }

    /** XML Sitemap Generators ************************************************/

    public XmlDocument GenerateXmlSitemap ( string Host )
    {

      Dictionary<string, bool> Dedupe = new Dictionary<string, bool>( DocCollection.CountDocuments() );
      XmlDocument SitemapXml = new XmlDocument();
      XmlDeclaration SitemapXmlDeclaration = SitemapXml.CreateXmlDeclaration( "1.0", "UTF-8", null );
      XmlElement RootNode = SitemapXml.DocumentElement;
      XmlElement UrlSetNode = SitemapXml.CreateElement( string.Empty, "urlset", MacroscopeSitemapGenerator.XmlNamespace );

      SitemapXml.InsertBefore( SitemapXmlDeclaration, RootNode );
      SitemapXml.AppendChild( UrlSetNode );

      foreach( MacroscopeDocument msDoc in this.DocCollection.IterateDocuments() )
      {

        bool Proceed = false;

        if( !msDoc.GetStatusCode().Equals( HttpStatusCode.OK ) )
        {
          continue;
        }

        if(
          ( !msDoc.GetIsInternal() )
          || ( msDoc.GetIsRedirect() ) )
        {
          continue;
        }

        switch( msDoc.GetDocumentType() )
        {
          case MacroscopeConstants.DocumentType.HTML:
            Proceed = true;
            break;
          case MacroscopeConstants.DocumentType.PDF:
            Proceed = true;
            break;
          default:
            break;
        }

        if( !string.IsNullOrEmpty( Host ) )
        {
          if( msDoc.GetHostAndPort().Equals( Host ) )
          {
            Proceed = true;
          }
          else
          {
            Proceed = false;
          }
        }

        if( Proceed )
        {

          XmlElement UrlNode = SitemapXml.CreateElement( string.Empty, "url", MacroscopeSitemapGenerator.XmlNamespace );

          UrlSetNode.AppendChild( UrlNode );

          {
            XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "loc", MacroscopeSitemapGenerator.XmlNamespace );
            XmlText TextNode = SitemapXml.CreateTextNode( msDoc.GetUrl() );
            UrlNode.AppendChild( EntryNode );
            EntryNode.AppendChild( TextNode );
          }

          {
            XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "lastmod", MacroscopeSitemapGenerator.XmlNamespace );
            XmlText TextNode = SitemapXml.CreateTextNode( msDoc.GetDateModifiedForSitemapXml() );
            UrlNode.AppendChild( EntryNode );
            EntryNode.AppendChild( TextNode );
          }

          {
            XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "changefreq", MacroscopeSitemapGenerator.XmlNamespace );
            XmlText TextNode = SitemapXml.CreateTextNode( "daily" );
            UrlNode.AppendChild( EntryNode );
            EntryNode.AppendChild( TextNode );
          }

          {
            XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "priority", MacroscopeSitemapGenerator.XmlNamespace );
            XmlText TextNode = SitemapXml.CreateTextNode( "1.0" );
            UrlNode.AppendChild( EntryNode );
            EntryNode.AppendChild( TextNode );
          }

          if(
            MacroscopePreferencesManager.GetSitemapIncludeLinkedPdfs()
            && msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
          {

            this.GenerateXmlSitemapPdfEntries(
              msDoc: msDoc,
              SitemapXml: SitemapXml,
              UrlSetNode: UrlSetNode,
              Dedupe: Dedupe
            );

          }

        }

      }

      return ( SitemapXml );

    }

    /** -------------------------------------------------------------------- **/

    private void GenerateXmlSitemapPdfEntries (
      MacroscopeDocument msDoc,
      XmlDocument SitemapXml,
      XmlElement UrlSetNode,
      Dictionary<string, bool> Dedupe
    )
    {

      foreach( MacroscopeHyperlinkOut HyperlinkOut in msDoc.IterateHyperlinksOut() )
      {

        string Url = HyperlinkOut.GetTargetUrl();
        Uri UrlParsed = new Uri( uriString: Url );

        if( Dedupe.ContainsKey( Url ) )
        {
          continue;
        }
        else
        {
          Dedupe.Add( Url, true );
        }

        if( !UrlParsed.AbsolutePath.ToLower().EndsWith( ".pdf", StringComparison.InvariantCultureIgnoreCase ) )
        {
          continue;
        }

        if( !this.DocCollection.GetAllowedHosts().IsAllowedFromUrl( Url: Url ) )
        {
          continue;
        }

        if( !MacroscopeHttpUrlUtils.VerifySameHost( BaseUrl: msDoc.GetUrl(), Url: Url ) )
        {
          continue;
        }

        XmlElement UrlNode = SitemapXml.CreateElement( string.Empty, "url", MacroscopeSitemapGenerator.XmlNamespace );
        UrlSetNode.AppendChild( UrlNode );

        {
          XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "loc", MacroscopeSitemapGenerator.XmlNamespace );
          XmlText TextNode = SitemapXml.CreateTextNode( Url );
          UrlNode.AppendChild( EntryNode );
          EntryNode.AppendChild( TextNode );
        }

        {
          XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "changefreq", MacroscopeSitemapGenerator.XmlNamespace );
          XmlText TextNode = SitemapXml.CreateTextNode( "daily" );
          UrlNode.AppendChild( EntryNode );
          EntryNode.AppendChild( TextNode );
        }

        {
          XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "priority", MacroscopeSitemapGenerator.XmlNamespace );
          XmlText TextNode = SitemapXml.CreateTextNode( "1.0" );
          UrlNode.AppendChild( EntryNode );
          EntryNode.AppendChild( TextNode );
        }

      }

    }

    /** TEXT Sitemap Generators ***********************************************/

    public List<string> GenerateTextSitemap ( string Host )
    {

      Dictionary<string, bool> Dedupe = new Dictionary<string, bool>( DocCollection.CountDocuments() );
      List<string> SitemapText = new List<string>( this.DocCollection.CountDocuments() );

      foreach( MacroscopeDocument msDoc in this.DocCollection.IterateDocuments() )
      {

        bool Proceed = false;

        if( !msDoc.GetStatusCode().Equals( HttpStatusCode.OK ) )
        {
          continue;
        }

        if(
          ( !msDoc.GetIsInternal() )
          || ( msDoc.GetIsRedirect() ) )
        {
          continue;
        }

        switch( msDoc.GetDocumentType() )
        {
          case MacroscopeConstants.DocumentType.HTML:
            Proceed = true;
            break;
          case MacroscopeConstants.DocumentType.PDF:
            Proceed = true;
            break;
          default:
            break;
        }

        if( !string.IsNullOrEmpty( Host ) )
        {
          if( msDoc.GetHostAndPort().Equals( Host ) )
          {
            Proceed = true;
          }
          else
          {
            Proceed = false;
          }
        }

        if( Proceed )
        {

          SitemapText.Add( msDoc.GetUrl() );

          if(
            MacroscopePreferencesManager.GetSitemapIncludeLinkedPdfs()
            && msDoc.IsDocumentType( Type: MacroscopeConstants.DocumentType.HTML ) )
          {

            this.GenerateTextSitemapPdfEntries(
              msDoc: msDoc,
              SitemapText: SitemapText,
              Dedupe: Dedupe
            );

          }

        }

      }

      return ( SitemapText );

    }

    /** -------------------------------------------------------------------- **/

    private void GenerateTextSitemapPdfEntries (
      MacroscopeDocument msDoc,
      List<string> SitemapText,
      Dictionary<string, bool> Dedupe
    )
    {

      foreach( MacroscopeHyperlinkOut HyperlinkOut in msDoc.IterateHyperlinksOut() )
      {

        string Url = HyperlinkOut.GetTargetUrl();
        Uri UrlParsed = new Uri( uriString: Url );

        if( Dedupe.ContainsKey( Url ) )
        {
          continue;
        }
        else
        {
          Dedupe.Add( Url, true );
        }

        if( !UrlParsed.AbsolutePath.ToLower().EndsWith( ".pdf", StringComparison.InvariantCultureIgnoreCase ) )
        {
          continue;
        }

        if( !this.DocCollection.GetAllowedHosts().IsAllowedFromUrl( Url: Url ) )
        {
          continue;
        }

        if( !MacroscopeHttpUrlUtils.VerifySameHost( BaseUrl: msDoc.GetUrl(), Url: Url ) )
        {
          continue;
        }

        SitemapText.Add( Url );

      }

    }

    /**************************************************************************/

  }

}
