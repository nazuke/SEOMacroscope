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
    public const string NamedQueueDisplayEmailAddresses = "DisplayEmailAddresses";
    public const string NamedQueueDisplayTelephoneNumbers = "DisplayTelephoneNumbers";
    public const string NamedQueueDisplayHostnames = "DisplayHostnames";

    public const string NamedQueueDisplayCustomFilters = "DisplayCustomFilters";

    public const string RecalculateDocCollection = "RecalculateDocCollection";

    /** END: Named Queues *****************************************************/

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
      ALL = 0,
      INTERNALURL = 1,
      EXTERNALURL = 2,
      BINARY = 3,
      HTML = 4,
      CSS = 5,
      JAVASCRIPT = 6,
      IMAGE = 7,
      PDF = 8,
      AUDIO = 9,
      VIDEO = 10,
      XML = 11,
      SITEMAPXML = 12,
      TEXT = 13,
      SITEMAPTEXT = 14
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
      IFRAME = 10,
      MAP = 11,
      IMAGE = 12,
      SCRIPT = 13,
      AUDIO = 14,
      VIDEO = 15,
      EMBED = 16,
      OBJECT = 17,
      ALTERNATE = 18,
      PDF = 19
    }

    /** END: Outlink Types ****************************************************/

    /** BEGIN: ListView Column Names ******************************************/

    public const string Url = "URL";

    public const string StatusCode = "Status Code";
    public const string Status = "Status";
    
    public const string IsRedirect = "Redirect";

    public const string Duration = "Duration (seconds)";

    public const string DateCrawled = "Crawled Date";
    public const string DateServer = "Server Date";
    public const string DateModified = "Modified Date";
    public const string DateExpires = "Expires Date";

    public const string ContentType = "Content Type";
    public const string Locale = "Locale";
    public const string Language = "Language";

    public const string Canonical = "Canonical";

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
        "UNDEFINED"
      },
      {
        MacroscopeConstants.TextPresence.CONTAINS,
        "CONTAINS STRING"
      },
      {
        MacroscopeConstants.TextPresence.MUSTCONTAIN,
        "MUST CONTAIN STRING"
      },
      {
        MacroscopeConstants.TextPresence.NOTCONTAINS,
        "DOES NOT CONTAIN STRING"
      },
      {
        MacroscopeConstants.TextPresence.SHOULDNOTCONTAIN,
        "SHOULD NOT CONTAIN STRING"
      }
    };

    /** END: Contains *********************************************************/

  }

}
