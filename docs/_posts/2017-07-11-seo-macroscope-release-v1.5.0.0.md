---
layout: post
title: "New v1.5 release of SEO Macroscope: Hard Vacuum"
date: "2017-07-11 21:00:00 -09:00"
published: true
description: "This release of SEO Macroscope includes support for custom filters and data extractors (web scraping)."
excerpt: "This release of SEO Macroscope includes support for custom filters and data extractors (web scraping)."
---

This release of SEO Macroscope includes support for custom filters and data extractors (web scraping). Content may be extracted from web pages, and some text-based documents, using CSS selectors, regular expressions, and XPath queries.
{: .lead }

The custom filters, for example, may be used to verify that all of your HTML pages have a particular tracking code installed, such as a Google Analytics tracking code.

The data extractors (web scraping) may be used to extract arbitrary content from your HTML pages, and some other document types. For example, you may want to extract all HREF attributes that match a certain pattern, or build a list of specific element contents across your site.

Chiefly, the data extractors are for identifying data in the site being crawled that may be specific to your interests, and is not already being extracted by the application.

Source code and an installer can be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases/tag/v1.5.0.0](https://github.com/nazuke/SEOMacroscope/releases/tag/v1.5.0.0)

Please check the [downloads page]({{ "/downloads/" | relative_url }}) for more recent versions.

This version is 64 bit only. If all goes well, I shall be continuing to release in 64bit. This should eliminate many of the out-of-memory issues that occurred with the 32 bit versions previously.

I've also fixed many minor bugs and other issues.

## New features in this release include:

* Custom filters.

* Data extractors (web scrapers) using CSS selectors, regular expressions, and XPath queries.

* Google XML and text format sitemap generators.

* More Excel and CSV format report generators.

* Include/Exclude patterns now use regular expressions.

Please report issues at [https://github.com/nazuke/SEOMacroscope/issues](https://github.com/nazuke/SEOMacroscope/issues).

![SEO Macroscope Application Window]({{ "../media/screenshots/seo-macroscope-main-window-v1.5.png" | relative_url }}){: .img-responsive .box-shadow}
{: .screenshot }
