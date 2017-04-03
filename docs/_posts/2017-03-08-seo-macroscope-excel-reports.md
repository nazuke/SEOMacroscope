---
layout: post
title: SEO Macroscope Excel Reports
date: 2017-03-08 12:59:00 -09:00
excerpt: Generating Excel reports with SEO Macroscope.
---

I've been implementing a few more Excel Reports so that we can actually dump out the information that SEO Macroscope gathers into some sort of useful format for ingestion into other tools.
{: .lead }

The most recent additions are:

* **Broken Links:** Reporting which links on the site are broken in some way, along with the pages that link to them.
* **URI Analysis:** Including a complete mapping of all from/to links within the spidered collection, indicators of duplicated content, and redirected URLs.

Additionally, I've added a new **Links** tab to the Overview panels, which gives a live and searchable from/to link map of the spidered collection.

I'm working towards another beta release, but currently, the source code can be checked out directly from GitHub. Please take a look on the [Downloads]({{ "/downloads/" | relative_url }}) page for where to grab the source code.
