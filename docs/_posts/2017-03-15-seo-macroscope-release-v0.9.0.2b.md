---
layout: post
title: "New v0.9.0.2b release of SEO Macroscope: The Monster"
date: 2017-03-15 12:00:00 -09:00
excerpt: The second beta of SEO Macroscope has been released.
---

The second beta of SEO Macroscope has been released. Source code and an installer can be found on GitHub at:
{: .lead }

* [https://github.com/nazuke/SEOMacroscope/releases/tag/v0.9.0.2b](https://github.com/nazuke/SEOMacroscope/releases/tag/v0.9.0.2b)

Please check the downloads page for more recent versions.

I've re-implemented how inbound hyperlinks are tracked across the crawled collection, amongst other internal improvements.

I've also been working on the Excel reporting, including a new duplicate content detection scheme that uses [Levenshtein Edit Distance](https://en.wikipedia.org/wiki/Levenshtein_distance) to try and determine pages that are very similar to each other.
