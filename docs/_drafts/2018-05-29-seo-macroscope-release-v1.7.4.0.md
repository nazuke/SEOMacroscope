---
layout: post
title: "New v1.7.4 release of SEO Macroscope: Divine Predecessors"
date: "2018-05-29 09:00:00 -09:00"
published: false
description: "This release of SEO Macroscope adds parent directory probing."
excerpt: "This release of SEO Macroscope adds parent directory probing."
---

This release of SEO Macroscope adds parent directory probing, and fixes a few bugs.
{: .lead }

Source code and an installer can be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.4.0](https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.4.0)

Please check the [downloads page]({{ "/downloads/" | relative_url }}) for more recent versions.

## New features in this release include:

* There is a new option to probe parent directories for each URL found on an internal site. This builds a new set of URLs to crawl, by taking the current URL, and progressively stripping off each rightmost element until it reaches the root. Each stripped URL is then added to the list of URLs to crawl.
* The body text word counter has been improved, and unit tests written.

## Bug fixes

* Not a bug as such, but the *check for update* phone home function now more precisely checks the current and updated version numbers, instead of doing a simple equals comparison.

Please report issues at [https://github.com/nazuke/SEOMacroscope/issues](https://github.com/nazuke/SEOMacroscope/issues).

![SEO Macroscope Application Window]({{ "/media/screenshots/seo-macroscope-main-window-v1.7.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }
