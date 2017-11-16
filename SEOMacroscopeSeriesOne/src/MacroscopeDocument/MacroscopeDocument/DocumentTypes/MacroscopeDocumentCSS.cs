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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ExCSS;

namespace SEOMacroscope
{

  public partial class MacroscopeDocument : Macroscope
  {

    /**************************************************************************/

    private void ConfigureCssPageRequestHeadersCallback ( HttpRequestMessage Request )
    {
    }

    /** -------------------------------------------------------------------- **/

    private async Task ProcessCssPage ()
    {

      MacroscopeHttpTwoClient Client = this.DocCollection.GetJobMaster().GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      Uri DocUri;
      string ResponseErrorCondition = null;
      Boolean IsAuthenticating = false;
      
      DebugMsg( string.Format( "ProcessCssPage: {0}", "" ) );

      try
      {

        DocUri = new Uri( this.DocUrl );
        Response = await Client.Get( DocUri, this.ConfigureCssPageRequestHeadersCallback, this.PostProcessRequestHttpHeadersCallback );

        // TODO: Fix this:
        //IsAuthenticating = this.AuthenticateRequest( req );

      }
      catch( UriFormatException ex )
      {
        DebugMsg( string.Format( "ProcessCssPage :: UriFormatException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }
      catch( TimeoutException ex )
      {

        DebugMsg( string.Format( "ProcessCssPage :: TimeoutException: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;

      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "ProcessCssPage :: Exception: {0}", ex.Message ) );
        ResponseErrorCondition = ex.Message;
      }

      if( Response != null )
      {

        string RawData = "";

        this.ProcessResponseHttpHeaders( Response: Response );

        if( IsAuthenticating )
        {
          this.VerifyOrPurgeCredential();
        }

        /** Get Response Body ---------------------------------------------- **/

        try
        {

          DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );

          RawData = Response.GetContentAsString();

          this.ContentLength = RawData.Length; // May need to find bytes length
         
          this.SetWasDownloaded( true );

        }
        catch( Exception ex )
        {
          DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
          this.SetStatusCode( HttpStatusCode.Ambiguous );
          this.ContentLength = 0;
        }

        if( !string.IsNullOrEmpty( RawData ) )
        {

          ExCSS.Parser ExCssParser = new ExCSS.Parser ();
          ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( RawData );

          this.ProcessCssOutlinks( ExCssStylesheet );

        }
        else
        {
          
          DebugMsg( string.Format( "ProcessCssPage: ERROR: {0}", this.GetUrl() ) );
          
        }

        /** Custom Filters ------------------------------------------------- **/

        if( !string.IsNullOrEmpty( RawData ) )
        {

          if(
            MacroscopePreferencesManager.GetCustomFiltersEnable()
            && MacroscopePreferencesManager.GetCustomFiltersApplyToCss() )
          {
          
            MacroscopeCustomFilters CustomFilter = this.DocCollection.GetJobMaster().GetCustomFilter();

            if( ( CustomFilter != null ) && ( CustomFilter.IsEnabled() ) )
            {
              this.ProcessGenericCustomFiltered(
                CustomFilter: CustomFilter,
                GenericText: RawData
              );
            }

          }
          
        }

        /** Data Extractors ------------------------------------------------ **/

        if( !string.IsNullOrEmpty( RawData ) )
        {

          if(
            MacroscopePreferencesManager.GetDataExtractorsEnable()
            && MacroscopePreferencesManager.GetDataExtractorsApplyToCss() )
          {
            this.ProcessGenericDataExtractors( GenericText: RawData );
          }

        }

        /** Title ---------------------------------------------------------- **/

        {
          MatchCollection reMatches = Regex.Matches( this.DocUrl, "/([^/]+)$" );
          string DocumentTitle = null;
          foreach( Match match in reMatches )
          {
            if( match.Groups[ 1 ].Value.Length > 0 )
            {
              DocumentTitle = match.Groups[ 1 ].Value.ToString();
              break;
            }
          }
          if( DocumentTitle != null )
          {
            this.SetTitle( DocumentTitle, MacroscopeConstants.TextProcessingMode.NO_PROCESSING );
            DebugMsg( string.Format( "TITLE: {0}", this.GetTitle() ) );
          }
          else
          {
            DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
          }
        }

      }

      if( ResponseErrorCondition != null )
      {
        this.ProcessErrorCondition( ResponseErrorCondition );
      }
            
    }

    /**************************************************************************/

    private void ProcessCssOutlinks ( ExCSS.StyleSheet ExCssStylesheet )
    {

      if( this.GetIsExternal() )
      {
        return;
      }
            
      foreach( var CssRule in ExCssStylesheet.StyleRules )
      {

        int iRule = ExCssStylesheet.StyleRules.IndexOf( CssRule );

        foreach( Property pProp in ExCssStylesheet.StyleRules[ iRule ].Declarations.Properties )
        {
          
          string BackgroundImageUrl;
          string LinkUrlAbs;

          switch( pProp.Name.ToLower() )
          {

            case "background-image":

              if( pProp.Term != null )
              {
                
                BackgroundImageUrl = pProp.Term.ToString();
                LinkUrlAbs = this.ProcessCssBackImageUrl( BackgroundImageUrl );

                DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background-image): {0}", BackgroundImageUrl ) );
                DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background-image): {0}", LinkUrlAbs ) );

                if( LinkUrlAbs != null )
                {
                  
                  MacroscopeHyperlinkOut HyperlinkOut = null;
                  MacroscopeLink Outlink = null;

                  HyperlinkOut = this.HyperlinksOut.Add(
                    LinkType: MacroscopeConstants.HyperlinkType.CSS,
                    UrlTarget: LinkUrlAbs
                  );

                  Outlink = this.AddCssOutlink(
                    AbsoluteUrl: LinkUrlAbs,
                    LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                    Follow: true
                  );
                  
                  if( Outlink != null )
                  {
                    Outlink.SetRawTargetUrl( BackgroundImageUrl );
                  }

                }
              
              }

              break;

            case "background":

              if( pProp.Term != null )
              {
                
                BackgroundImageUrl = pProp.Term.ToString();
                LinkUrlAbs = this.ProcessCssBackImageUrl( BackgroundImageUrl );

                DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background): {0}", BackgroundImageUrl ) );
                DebugMsg( string.Format( "ProcessCssHyperlinksOut: (background): {0}", LinkUrlAbs ) );

                if( LinkUrlAbs != null )
                {     

                  MacroscopeHyperlinkOut HyperlinkOut = null;
                  MacroscopeLink Outlink = null;

                  HyperlinkOut = this.HyperlinksOut.Add(
                    LinkType: MacroscopeConstants.HyperlinkType.CSS,
                    UrlTarget: LinkUrlAbs
                  );

                  Outlink = this.AddCssOutlink(
                    AbsoluteUrl: LinkUrlAbs,
                    LinkType: MacroscopeConstants.InOutLinkType.IMAGE,
                    Follow: true
                  );
                
                  if( Outlink != null )
                  {
                    Outlink.SetRawTargetUrl( BackgroundImageUrl );
                  }
                
                }
              
              }
              
              break;

            default:
              break;

          }

        }

      }

    }

    /**************************************************************************/

    private string ProcessCssBackImageUrl ( string BackgroundImageUrl )
    {

      string LinkUrlAbs = null;
      string LinkUrlCleaned = MacroscopeUrlUtils.CleanUrlCss( BackgroundImageUrl );

      if( LinkUrlCleaned != null )
      {

        try
        {
          LinkUrlAbs = MacroscopeUrlUtils.MakeUrlAbsolute(
            BaseUrl: this.DocUrl,
            Url: LinkUrlCleaned
          );
        }
        catch( MacroscopeUriFormatException ex )
        {
          DebugMsg( string.Format( "ProcessCssBackImageUrl: {0}", ex.Message ) );
        }
        
        DebugMsg( string.Format( "ProcessCssBackImageUrl: {0}", LinkUrlCleaned ) );
        DebugMsg( string.Format( "ProcessCssBackImageUrl: this.DocUrl: {0}", this.DocUrl ) );
        DebugMsg( string.Format( "ProcessCssBackImageUrl: LinkUrlAbs: {0}", LinkUrlAbs ) );

      }

      return( LinkUrlAbs );
      
    }

    /**************************************************************************/

    private MacroscopeLink AddCssOutlink (
      string AbsoluteUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      Boolean Follow
    )
    {

      MacroscopeLink OutLink = null;
      Boolean Proceed = true;

      if( !MacroscopePreferencesManager.GetCheckExternalLinks() )
      {
        MacroscopeAllowedHosts AllowedHosts = this.DocCollection.GetAllowedHosts();
        if( AllowedHosts != null )
        {
          if( !AllowedHosts.IsAllowedFromUrl( Url: AbsoluteUrl ) )
          {
            Proceed = false;
          }
        }
      }

      switch( LinkType )
      {
        case MacroscopeConstants.InOutLinkType.STYLESHEET:
          if( !MacroscopePreferencesManager.GetFetchStylesheets() )
          {
            Proceed = false;
          }
          break;
      }
      
      if( Proceed )
      {

        OutLink = new MacroscopeLink (
          SourceUrl: this.GetUrl(),
          TargetUrl: AbsoluteUrl,
          LinkType: LinkType,
          Follow: Follow
        );

        this.Outlinks.Add( OutLink );

      }

      return( OutLink );
            
    }

    /**************************************************************************/
    
  }

}
