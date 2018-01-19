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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeConstants.
  /// </summary>

  public static class MacroscopeConstants
  {

    /** BEGIN: Event Logs *****************************************************/

    public const string MainEventLogSourceName = "SEO Macroscope";
    public const string MainEventLogJobMaster = "JobMaster";

    /** END: Event Logs *******************************************************/

    /** BEGIN: Runtime Modes **************************************************/

    public enum RunTimeMode
    {
      LIVE = 1,
      LISTFILE = 2,
      LISTTEXT = 3,
      SITEMAP = 4
    }

    /** END: Runtime Modes ****************************************************/

    /** BEGIN: Text Processing Modes ******************************************/

    public enum TextProcessingMode
    {
      NO_PROCESSING = 0,
      PRESERVE_HTML_ENTITIES = 1,
      DECODE_HTML_ENTITIES = 2
    }

    /** END: Text Processing Modes ********************************************/

    /** BEGIN: Named Queues ***************************************************/

    public const string NamedQueueUrlList = "UrlQueue";

    public const string NamedQueueDisplayQueue = "DisplayQueue";
    
    public const string NamedQueueDisplayStructure = "DisplayStructure";
    public const string NamedQueueDisplayStructureLinkCounts = "DisplayStructureLinkCounts";
    
    public const string NamedQueueDisplayHierarchy = "DisplayHierarchy";
    public const string NamedQueueDisplayCanonicalAnalysis = "DisplayCanonicalAnalysis";
    public const string NamedQueueDisplayHrefLang = "DisplayHrefLang";
    public const string NamedQueueDisplayErrors = "DisplayErrors";
    public const string NamedQueueDisplayRedirectsAudit = "DisplayRedirectsAudit";
    public const string NamedQueueDisplayLinks = "DisplayLinks";
    public const string NamedQueueDisplayHyperlinks = "DisplayHyperlinks";
    public const string NamedQueueDisplayUriAnalysis = "DisplayUriAnalysis";
    public const string NamedQueueDisplayPageTitles = "DisplayPageTitles";
    public const string NamedQueueDisplayPageDescriptions = "DisplayPageDescriptions";
    public const string NamedQueueDisplayPageKeywords = "DisplayPageKeywords";
    public const string NamedQueueDisplayPageHeadings = "DisplayPageHeadings";
    public const string NamedQueueDisplayPageText = "DisplayPageText";
    public const string NamedQueueDisplayStylesheets = "DisplayStylesheets";
    public const string NamedQueueDisplayJavascripts = "DisplayJavascripts";
    public const string NamedQueueDisplayImages = "DisplayImages";
    public const string NamedQueueDisplayAudios = "DisplayAudios";
    public const string NamedQueueDisplayVideos = "DisplayVideos";
    public const string NamedQueueDisplayRobots = "DisplayRobots";

    public const string NamedQueueDisplaySitemaps = "DisplaySitemaps";
    public const string NamedQueueDisplaySitemapErrors = "DisplaySitemapErrors";
    public const string NamedQueueDisplaySitemapsAudit = "DisplaySitemapsAudit";

    public const string NamedQueueDisplayEmailAddresses = "DisplayEmailAddresses";
    public const string NamedQueueDisplayTelephoneNumbers = "DisplayTelephoneNumbers";
    public const string NamedQueueDisplayHostnames = "DisplayHostnames";

    public const string NamedQueueDisplayCustomFilters = "DisplayCustomFilters";
    
    public const string NamedQueueDisplayDataExtractorsCssSelectors = "DisplayDataExtractorsCssSelectors";
    public const string NamedQueueDisplayDataExtractorsRegexes = "DisplayDataExtractorsRegexes";
    public const string NamedQueueDisplayDataExtractorsXpaths = "DisplayDataExtractorsXpaths";

    public const string NamedQueueDisplayRemarks = "DisplayRemarks";
        
    public const string RecalculateDocCollection = "RecalculateDocCollection";

    /** END: Named Queues *****************************************************/

    /** BEGIN: Overview Panel Tab Pages ***************************************/

    public const string tabPageStructureOverview = "tabPageStructureOverview";
    public const string tabPageStructureLinkCounts = "tabPageStructureLinkCounts";
    public const string tabPageHierarchy = "tabPageHierarchy";
    public const string tabPageRobots = "tabPageRobots";

    public const string tabPageSitemaps = "tabPageSitemaps";
    public const string tabPageSitemapErrors = "tabPageSitemapErrors";
    public const string tabPageSitemapsAudit= "tabPageSitemapsAudit";

    public const string tabPageCanonicalAnalysis = "tabPageCanonicalAnalysis";
    public const string tabPageHrefLangAnalysis = "tabPageHrefLangAnalysis";
    public const string tabPageErrors = "tabPageErrors";
    public const string tabPageHostnames = "tabPageHostnames";
    public const string tabPageRedirectsAudit = "tabPageRedirectsAudit";
    public const string tabPageLinks = "tabPageLinks";
    public const string tabPageHyperlinks = "tabPageHyperlinks";
    public const string tabPageUriAnalysis = "tabPageUriAnalysis";
    public const string tabPagePageTitles = "tabPagePageTitles";
    public const string tabPagePageDescriptions = "tabPagePageDescriptions";
    public const string tabPagePageKeywords = "tabPagePageKeywords";
    public const string tabPagePageHeadings = "tabPagePageHeadings";
    public const string tabPagePageText = "tabPagePageText";
    public const string tabPageStylesheets = "tabPageStylesheets";
    public const string tabPageJavascripts = "tabPageJavascripts";
    public const string tabPageImages = "tabPageImages";
    public const string tabPageAudios = "tabPageAudios";
    public const string tabPageVideos = "tabPageVideos";
    public const string tabPageEmailAddresses = "tabPageEmailAddresses";
    public const string tabPageTelephoneNumbers = "tabPageTelephoneNumbers";
    public const string tabPageCustomFilters = "tabPageCustomFilters";

    public const string tabPageDataExtractors = "tabPageDataExtractors";
    public const string tabPageCssSelectors = "tabPageCssSelectors";
    public const string tabPageRegexes = "tabPageRegexes";
    public const string tabPageXpaths = "tabPageXpaths";

    public const string tabPageRemarks = "tabPageRemarks";
    public const string tabPageUriQueue = "tabPageUriQueue";
    public const string tabPageHistory = "tabPageHistory";
    public const string tabPageSearch = "tabPageSearch";

    /** END: Overview Panel Tab Pages *****************************************/

    /** BEGIN: History Visited Status *****************************************/

    public enum HistoryVisitedStatus
    {
      NOT_VISITED = 0,
      VISITED = 1,
      IGNORED = 2
    }

    /** END: History Visited Status *******************************************/

    /** BEGIN: Fetch Status ***************************************************/

    public enum FetchStatus
    {
      VOID = 0,
      OK = 1,
      ERROR = 2,
      SUCCESS = 3,
      NETWORK_ERROR = 4,
      ROBOTS_DISALLOWED = 5,
      ALREADY_SEEN = 6,
      SKIPPED = 7
    }

    /** END: Fetch Status *****************************************************/

    /** BEGIN: Authentication Types *******************************************/

    public enum AuthenticationType
    {
      NONE = 0,
      UNSUPPORTED = 1,
      BASIC = 2
    }

    /** END: Authentication Types *********************************************/

    /** BEGIN: MIME Types *****************************************************/

    public const string DefaultMimeType = "application/octet-stream";

    /** END: MIME Types *******************************************************/

    /** BEGIN: Document Types *************************************************/

    public enum DocumentType
    {
      SKIPPED = 0,
      ALL = 1,
      INTERNALURL = 2,
      EXTERNALURL = 3,
      BINARY = 4,
      HTML = 5,
      CSS = 6,
      JAVASCRIPT = 7,
      IMAGE = 8,
      PDF = 9,
      AUDIO = 10,
      VIDEO = 11,
      XML = 12,
      SITEMAPXML = 13,
      TEXT = 14,
      SITEMAPTEXT = 15
    }

    /** END: Document Types ***************************************************/

    /** BEGIN: Outlink Classes ************************************************/

    public enum HyperlinkType
    {
      TEXT = 0,
      IMAGE = 1,
      CSS = 2
    }

    /** BEGIN: Outlink Types **************************************************/

    public enum InOutLinkType
    {
      ROBOTSTEXT = 0,
      SITEMAPXML = 1,
      SITEMAPTEXT = 2,
      REDIRECT = 3,
      LINK = 4,
      STYLESHEET = 5,
      CANONICAL = 6,
      HREFLANG = 7,
      AHREF = 8,
      META = 9,
      FRAME = 10,
      IFRAME = 11,
      MAP = 12,
      IMAGE = 13,
      SCRIPT = 14,
      AUDIO = 15,
      VIDEO = 16,
      EMBED = 17,
      OBJECT = 18,
      ALTERNATE = 19,
      RELATED = 20,
      PDF = 21
    }

    /** END: Outlink Types ****************************************************/

    /** BEGIN: ListView Column Names ******************************************/

    public const string Url = "URL";

    public const string StatusCode = "Status Code";
    public const string Status = "Status";
    
    public const string IsRedirect = "Redirect";

    public const string RobotsRule = "Robots";

    public const string Duration = "Duration (seconds)";

    public const string DateCrawled = "Crawled Date";
    public const string DateServer = "Server Date";
    public const string DateModified = "Modified Date";
    public const string DateExpires = "Expires Date";

    public const string ContentType = "Content Type";
    public const string Locale = "Locale";
    public const string Language = "Language";

    public const string Canonical = "Canonical";

    public const string PageDepth = "Page Depth";
    
    public const string Inlinks = "Links In";
    public const string Outlinks = "Links Out";

    public const string Inhyperlinks = "Hyperlinks In";
    public const string Outhyperlinks = "Hyperlinks Out";

    public const string Title = "Title";
    public const string TitleLen = "Title Length";
    public const string TitleLang = "Title Language";

    public const string Description = "Description";
    public const string DescriptionLen = "Description Length";
    public const string DescriptionLang = "Description Language";

    public const string Keywords = "Keywords";
    public const string KeywordsLen = "Keywords Length";
    public const string KeywordsCount = "Keywords Count";
    
    public const string BodyTextLang = "Body Text Language";

    public const string Hn = "First H{0}";

    public const string ErrorCondition = "Error Condition";

    /** END: ListView Column Names ********************************************/

    /** BEGIN: Sitemap XML Protocol *******************************************/

    // Reference: https://www.sitemaps.org/protocol.html

    public const string SitemapXmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

    /** END: Sitemap XML Protocol *********************************************/

    /** BEGIN: Contains *******************************************************/

    public enum Contains
    {
      UNDEFINED = 0,
      MUSTHAVE = 1,
      MUSTNOTHAVE = 2
    }

    public static Dictionary<MacroscopeConstants.Contains,string> ContainsLabels = new Dictionary<MacroscopeConstants.Contains,string> () {
      {
        MacroscopeConstants.Contains.UNDEFINED,
        "UNDEFINED"
      },
      {
        MacroscopeConstants.Contains.MUSTHAVE,
        "MUST HAVE STRING"
      },
      {
        MacroscopeConstants.Contains.MUSTNOTHAVE,
        "MUST NOT HAVE STRING"
      }
    };

    public enum TextPresence
    {
      UNDEFINED = 0,
      CONTAINS = 1,
      MUSTCONTAIN = 2,
      NOTCONTAINS = 3,
      SHOULDNOTCONTAIN = 4
    }

    public static Dictionary<MacroscopeConstants.TextPresence,string> TextPresenceLabels = new Dictionary<MacroscopeConstants.TextPresence,string> () {
      {
        MacroscopeConstants.TextPresence.UNDEFINED,
        @"UNDEFINED"
      },
      {
        MacroscopeConstants.TextPresence.CONTAINS,
        @"CONTAINS STRING"
      },
      {
        MacroscopeConstants.TextPresence.MUSTCONTAIN,
        @"MUST CONTAIN STRING"
      },
      {
        MacroscopeConstants.TextPresence.NOTCONTAINS,
        @"DOES NOT CONTAIN STRING"
      },
      {
        MacroscopeConstants.TextPresence.SHOULDNOTCONTAIN,
        @"SHOULD NOT CONTAIN STRING"
      }
    };

    /** END: Contains *********************************************************/

    /** BEGIN: Data Extractors ************************************************/

    public enum ActiveInactive
    {
      INACTIVE = 0,
      ACTIVE = 1
    }

    public enum DataExtractorType
    {
      INNERTEXT = 0,
      OUTERHTML = 1,
      INNERHTML = 2
    }

    /** END: Data Extractors **************************************************/

    /** BEGIN: Specifiers *****************************************************/

    public enum Specifiers
    {
      UNSPECIFIED = 0,
      SPECIFIED = 1
    }

    /** END: Specifiers *******************************************************/

    /** BEGIN: Unit Testing ***************************************************/

    public const string MacroscopeTestHarnessWebsite = "https://nazuke.github.io/MacroscopeTestHarness/";

    /** END: Unit Testing *****************************************************/

  }

}
