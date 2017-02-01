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

namespace SEOMacroscope
{
	
	/// <summary>
	/// Description of MacroscopeOutlink.
	/// </summary>
	
	public static class MacroscopeConstants
	{

		/** BEGIN: Runtime Modes **************************************************/

		public enum RunTimeMode
		{
			LIVE = 1,
			LISTFILE = 2,
			LISTTEXT = 3,
			SITEMAP = 4
		}

		/** END: Runtime Modes ****************************************************/

		/** BEGIN: Named Queues ***************************************************/

		public const string NamedQueueUrlList = "UrlQueue";

		public const string NamedQueueDisplayQueue = "DisplayQueue";
		public const string NamedQueueDisplayStructure = "DisplayStructure";
		public const string NamedQueueDisplayHierarchy = "DisplayHierarchy";
		public const string NamedQueueDisplayCanonicalAnalysis = "DisplayCanonicalAnalysis";
		public const string NamedQueueDisplayHrefLang = "DisplayHrefLang";
		public const string NamedQueueDisplayErrors = "DisplayErrors";
		public const string NamedQueueDisplayRedirectsAudit = "DisplayRedirectsAudit";
		public const string NamedQueueDisplayUriAnalysis = "DisplayUriAnalysis";
		public const string NamedQueueDisplayPageTitles = "DisplayPageTitles";
		public const string NamedQueueDisplayPageDescriptions = "DisplayPageDescriptions";
		public const string NamedQueueDisplayPageKeywords = "DisplayPageKeywords";
		public const string NamedQueueDisplayPageHeadings = "DisplayPageHeadings";
		public const string NamedQueueDisplayStylesheets = "DisplayStylesheets";
		public const string NamedQueueDisplayImages = "DisplayImages";
		public const string NamedQueueDisplayJavascripts = "DisplayJavascripts";
		public const string NamedQueueDisplayRobots = "DisplayRobots";
		public const string NamedQueueDisplaySitemaps = "DisplaySitemaps";
		public const string NamedQueueDisplayEmailAddresses = "DisplayEmailAddresses";
		public const string NamedQueueDisplayTelephoneNumbers = "DisplayTelephoneNumbers";
		public const string NamedQueueDisplayHostnames = "DisplayHostnames";

		public const string RecalculateDocCollection = "RecalculateDocCollection";

		/** END: Named Queues *****************************************************/

		/** BEGIN: Document Types *************************************************/

		public enum DocumentType
		{
			BINARY = 1,
			HTML = 2,
			CSS = 3,
			JAVASCRIPT = 4,
			IMAGE = 5,
			PDF = 6,
			XML = 7,
			SITEMAPXML = 8
		}
		
		/** END: Document Types ***************************************************/

		/** BEGIN: Outlink Types **************************************************/

		public enum OutlinkType
		{
			SITEMAPXML = 0,
			REDIRECT = 1,
			LINK = 2,
			CANONICAL = 3,
			HREFLANG = 4,
			AHREF = 5,
			IMAGE = 6,
			SCRIPT = 7
		}

		/** END: Outlink Types ****************************************************/

		/** BEGIN: ListView Column Names ******************************************/

		public const string Url = "URL";
		
		public const string Status = "Status";
		public const string IsRedirect = "Redirect";
				
		public const string Duration = "Duration (seconds)";

		public const string DateServer = "Date";
		public const string DateModified = "Last Modified";
		
		public const string ContentType = "Content Type";
		public const string Lang = "Lang";
		
		public const string Canonical = "Canonical";
		
		public const string Inhyperlinks = "Links In";
		public const string Outhyperlinks = "Links Out";
		
		public const string Title = "Title";
		public const string TitleLen = "Title Length";
		
		public const string Description = "Description";
		public const string DescriptionLen = "Description Length";
		
		public const string Keywords = "Keywords";
		public const string KeywordsLen = "Keywords Length";
		public const string KeywordsCount = "Keywords Count";
		
		public const string Hn = "First H{0}";
		
		public const string ErrorCondition = "Error Condition";

		/** END: ListView Column Names ********************************************/

		/** BEGIN: Sitemap XML Protocol *******************************************/

		// Reference: https://www.sitemaps.org/protocol.html

		public const string SitemapXmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

		/** END: Sitemap XML Protocol *********************************************/

	}
	
}
