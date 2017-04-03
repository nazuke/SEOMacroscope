---
layout: post
title: Canonical URL Analysis with SEO Macroscope
date: 2017-02-23 18:44:00 -09:00
excerpt: Clone and build SEO Macroscope.
---

One of the first features I implemented was canonical URL analysis. This is a very important directive to use on your websites, to prevent duplicate content from being indexed by the search engines.
{: .lead }

## The Canonical Analysis Tab

Here's a screenshot of the results of scanning this block, and then inspecting the results in the **Canonical Analysis** tab (click the screenshot for a closer look):

![Canonical Analysis tab]({{ site.url }}/manual/images/overview-panel-canonical-001.png){: .img-responsive .box-shadow}

In most cases, the website scanner application highlights "internal" URLs under the URL column in green. This can help you to ignore other URLs that are of no interest.

In the screenshot above, we can see that all of the pages on the site that should have a canonical URL assigned to them, do.

The pages that are flagged with a red MISSING value are recorded as being 301 Permanently Moved redirects, which means that we do not need to worry about them being indexed as duplicate content by the search engines.

Using this tab, you can quickly identify pages that have missing or incorrect canonicals. Use the Overview Excel Report to export a report that includes the canonical URL for each page. Later, I plan to add a report generator specifically for canonical URLs.
