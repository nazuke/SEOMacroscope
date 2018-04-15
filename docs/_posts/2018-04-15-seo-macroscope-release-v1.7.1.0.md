---
layout: post
title: "New v1.7.1 release of SEO Macroscope: HTTP Too and a half"
date: "2018-04-15 18:00:00 -09:00"
published: true
description: "This release of SEO Macroscope primarily fixes bugs from v1.7."
excerpt: "This release of SEO Macroscope primarily fixes bugs from v1.7."
---

This release of SEO Macroscope primarily fixes bugs from v1.7.
{: .lead }

Source code and an installer can be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.1.0](https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.1.0)

Please check the [downloads page]({{ "/downloads/" | relative_url }}) for more recent versions.

## New features in this release include:

* There is a new hyperlink ratio feature found in the document details panel, and in the overview Excel and CSV reports. This calculates the percentage value for the number of hyperlinks in and out of a particular document, within the crawled collection. It does not include links from third-party sites not in the crawled collection.
* The web proxy settings may now now the systems configured proxies.

## Bug fixes

* A malformed User-Agent HTTP Header caused some websites to not be crawled at all.

Please report issues at [https://github.com/nazuke/SEOMacroscope/issues](https://github.com/nazuke/SEOMacroscope/issues).

![SEO Macroscope Application Window]({{ "/media/screenshots/seo-macroscope-main-window-v1.7.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }
