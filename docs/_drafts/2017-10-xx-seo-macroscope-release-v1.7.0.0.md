---
layout: post
title: "New v1.7 release of SEO Macroscope: Forensic Evidence"
date: "2017-10-xx 21:00:00 -09:00"
published: false
description: "This release of SEO Macroscope includes support for HTTP/2 and accelerates near-duplicate content detection."
excerpt: "This release of SEO Macroscope includes support for HTTP/2 and accelerates near-duplicate content detection."
---

This release of SEO Macroscope includes support for HTTP/2 and accelerates near-duplicate content detection. HTTP/2 support enables the application to work with websites that do not also support HTTP/1.1.
{: .lead }

As well as numerous bugs being fixed, the Levenshtein analysis of near-duplicate content has also been accelerated, by generating a *"Levenshtein Fingerprint"* of the document text of each web page as it is crawled. There are two analysis level options, with the second being slower but may help to eliminate false positives.

In the source, I have also begun exploratory work on refactoring the core of SEO Macroscope into an "engine" library, with a view to building a CLI interface, and an updated WPF-based GUI. It would also be useful to be able to run multiple crawls concurrently, instead of only the currently supported single crawl.

Source code and an installer can be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.0.0](https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.0.0)

Please check the [downloads page]({{ "/downloads/" | relative_url }}) for more recent versions.

I've also fixed many minor bugs and other issues.

## New features in this release include:

* XXXXXXXXX.

## Bug fixes

* XXXXXXXXX.

Please report issues at [https://github.com/nazuke/SEOMacroscope/issues](https://github.com/nazuke/SEOMacroscope/issues).

![SEO Macroscope Application Window]({{ "/media/screenshots/seo-macroscope-main-window-v1.7.png" | relative_url }}){: .img-responsive .box-shadow}
{: .screenshot }
