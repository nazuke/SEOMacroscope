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
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeSitemapGenerator.
  /// </summary>

  public class MacroscopeSitemapGenerator : Macroscope
  {

    /**************************************************************************/

    MacroscopeDocumentCollection DocCollection;

    /**************************************************************************/

    public MacroscopeSitemapGenerator ( MacroscopeDocumentCollection NewDocCollection )
    {
      this.DocCollection = NewDocCollection;
    }

    /**************************************************************************/

    public void WriteSitemapXml ( string NewPath )
    {

      string XmlSitemapSerialized = null;
      XmlDocument SitemapXml = this.GenerateXmlSitemap( Host: null );
      StringWriter SitemapXmlStringWriter = new StringWriter ();
      XmlTextWriter SitemapXmlTextWriter = new XmlTextWriter ( SitemapXmlStringWriter );
      
      SitemapXml.WriteTo( SitemapXmlTextWriter );

      XmlSitemapSerialized = SitemapXmlStringWriter.ToString();

      File.WriteAllText( NewPath, XmlSitemapSerialized, new System.Text.UTF8Encoding ( false ) );
      
    }
    
    /** -------------------------------------------------------------------- **/

    public void WriteSitemapXmlPerHost ( string NewPath )
    {

      Dictionary<string,int> HostsList = this.DocCollection.GetStatsHostnamesWithCount();

      foreach( string Host in HostsList.Keys )
      {

        string XmlSitemapSerialized = null;
        XmlDocument SitemapXml = this.GenerateXmlSitemap( Host: Host );
        StringWriter SitemapXmlStringWriter = new StringWriter ();
        XmlTextWriter SitemapXmlTextWriter = new XmlTextWriter ( SitemapXmlStringWriter );
      
        SitemapXml.WriteTo( SitemapXmlTextWriter );

        XmlSitemapSerialized = SitemapXmlStringWriter.ToString();

        string Pathname = Path.GetDirectoryName( NewPath );
        string Filename = Path.GetFileNameWithoutExtension( NewPath );

        string NewPathname = string.Join(
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
          new System.Text.UTF8Encoding ( false )
        );

      }

    }

    /**************************************************************************/

    public void WriteSitemapText ( string NewPath )
    {

      List<string> SitemapText = this.GenerateTextSitemap( Host: null );

      File.WriteAllLines( NewPath, SitemapText, new System.Text.UTF8Encoding ( false ) );

    }

    /** -------------------------------------------------------------------- **/

    public void WriteSitemapTextPerHost ( string NewPath )
    {

      Dictionary<string,int> HostsList = this.DocCollection.GetStatsHostnamesWithCount();

      foreach( string Host in HostsList.Keys )
      {

        List<string> SitemapText = this.GenerateTextSitemap( Host: null );

        string Pathname = Path.GetDirectoryName( NewPath );
        string Filename = Path.GetFileNameWithoutExtension( NewPath );

        string NewPathname = string.Join(
                               ".",
                               string.Join(
                                 Path.DirectorySeparatorChar.ToString(),
                                 Pathname,
                                 string.Join( "-", Filename, Host )
                               ),
                               "txt"
                             );

        File.WriteAllLines( NewPathname, SitemapText, new System.Text.UTF8Encoding ( false ) );
      
      }
      
    }

    /** XML Sitemap Generators ************************************************/

    public XmlDocument GenerateXmlSitemap ( string Host )
    {

      const string XmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

      XmlDocument SitemapXml = new XmlDocument ();
      XmlDeclaration SitemapXmlDeclaration = SitemapXml.CreateXmlDeclaration( "1.0", "UTF-8", null );
      XmlElement RootNode = SitemapXml.DocumentElement;
      XmlElement UrlSetNode = SitemapXml.CreateElement( string.Empty, "urlset", XmlNamespace );

      SitemapXml.InsertBefore( SitemapXmlDeclaration, RootNode );
      SitemapXml.AppendChild( UrlSetNode );

      foreach( MacroscopeDocument msDoc in this.DocCollection.IterateDocuments() )
      {

        Boolean Proceed = false;

        if( 
          msDoc.GetIsHtml()
          || msDoc.GetIsPdf() )
        {
          Proceed = true;
        }

        if(
          ( !msDoc.GetIsInternal() )
          || ( msDoc.GetIsRedirect() ) )
        {
          Proceed = false;
        }

        if( !string.IsNullOrEmpty( Host ) )
        {
          if( msDoc.GetHostname().Equals( Host ) )
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

          XmlElement UrlNode = SitemapXml.CreateElement( string.Empty, "url", XmlNamespace );

          UrlSetNode.AppendChild( UrlNode );

          {
            XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "loc", XmlNamespace );
            XmlText TextNode = SitemapXml.CreateTextNode( msDoc.GetUrl() );
            UrlNode.AppendChild( EntryNode );
            EntryNode.AppendChild( TextNode );
          }
        
          {
            XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "lastmod", XmlNamespace );
            XmlText TextNode = SitemapXml.CreateTextNode( msDoc.GetDateModifiedForSitemapXml() );
            UrlNode.AppendChild( EntryNode );
            EntryNode.AppendChild( TextNode );
          }
        
          {
            XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "changefreq", XmlNamespace );
            XmlText TextNode = SitemapXml.CreateTextNode( "daily" );
            UrlNode.AppendChild( EntryNode );
            EntryNode.AppendChild( TextNode );
          }
        
          {
            XmlElement EntryNode = SitemapXml.CreateElement( string.Empty, "priority", XmlNamespace );
            XmlText TextNode = SitemapXml.CreateTextNode( "1.0" );
            UrlNode.AppendChild( EntryNode );
            EntryNode.AppendChild( TextNode );
          }

          if(
            MacroscopePreferencesManager.GetSitemapIncludeLinkedPdfs()
            && msDoc.GetIsHtml() )
          {
            GenerateXmlSitemap( SitemapXml: SitemapXml, UrlSetNode: UrlSetNode );
          }

        }
        
      }

      return( SitemapXml );

    }

    /** -------------------------------------------------------------------- **/

    // TODO: GENERATE PDF ENTRIES
                
    private void GenerateXmlSitemap (
      MacroscopeDocument msDoc,
      XmlDocument SitemapXml,
      XmlElement UrlSetNode
    )
    {









    }

    /** TEXT Sitemap Generators ***********************************************/

    public List<string> GenerateTextSitemap ( string Host )
    {

      List<string> SitemapText = new List<string> ( this.DocCollection.CountDocuments() );

      foreach( MacroscopeDocument msDoc in this.DocCollection.IterateDocuments() )
      {

        Boolean Proceed = false;

        if( 
          msDoc.GetIsHtml()
          || msDoc.GetIsPdf() )
        {
          Proceed = true;
        }

        if(
          ( !msDoc.GetIsInternal() )
          || ( msDoc.GetIsRedirect() ) )
        {
          Proceed = false;
        }

        if( !string.IsNullOrEmpty( Host ) )
        {
          if( msDoc.GetHostname().Equals( Host ) )
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

        }
        
      }

      return( SitemapText );

    }

    /**************************************************************************/

  }

}
