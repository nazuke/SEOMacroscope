---
layout: post
title: "New v1.7.3 release of SEO Macroscope: Chainlinks"
date: "2018-05-22 23:00:00 -09:00"
published: true
description: "This release of SEO Macroscope primarily fixes a number of minor bugs."
excerpt: "This release of SEO Macroscope primarily fixes a number of minor bugs."
---

This release of SEO Macroscope primarily fixes a number of minor bugs, and adds a few new features.
{: .lead }

Source code and an installer can be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.3.0](https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.3.0)

Please check the [downloads page]({{ "/downloads/" | relative_url }}) for more recent versions.

## New features in this release include:

* Where possible, Author fields are extracted from HTML and PDF documents.
* The Page Metadata Excel report has a new worksheet that combines the crawled author, title, description, and keywords fields.
  * This can be useful when crawling a list of PDF documents, as it extracts that information into a single worksheet.
* A simple check for update feature has been added. This will show an alert if a new version of SEO Macroscope appears to be available.
* HTML page character set sniffing has been enhanced.

## Bug fixes

* Rewrote the redirect chain analysis code, so that the redirect chain analysis should now be more complete for each crawl. Previously, the redirect chain list was built from the crawled document collection, which meant that some redirects were missing if they had not been crawled yet. Not, an explicit HEAD request is executed for each document that redirects, until no more redirects are encountered.
* There was a locking fault in the crawled document collection, that caused some documents to never be fetched.

Please report issues at [https://github.com/nazuke/SEOMacroscope/issues](https://github.com/nazuke/SEOMacroscope/issues).

![SEO Macroscope Application Window]({{ "/media/screenshots/seo-macroscope-main-window-v1.7.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }
