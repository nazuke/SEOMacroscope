---
layout: post
title: "New v1.6 release of SEO Macroscope: The Flesch Prevails"
date: "2017-09-06 21:00:00 -09:00"
published: true
description: "This release of SEO Macroscope includes support for English language text readability scoring."
excerpt: "This release of SEO Macroscope includes support for English language text readability scoring."
---

This release of SEO Macroscope includes support for English language text readability scoring. Implementations of the Flesch-Kincaid or SMOG algorithms may be applied to the body text of web pages, giving a simple score as to the "readability" of the page text. This may then be used to further refine the page text to suit the target audience.
{: .lead }

Source code and an installer can be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases/tag/v1.6.0.0](https://github.com/nazuke/SEOMacroscope/releases/tag/v1.6.0.0)

Please check the [downloads page]({{ "/downloads/" | relative_url }}) for more recent versions.

I've also fixed many minor bugs and other issues.

## New features in this release include:

* New Flesch-Kincaid and SMOG algorithms, for scoring the readability of the body text in English language web pages.

* Include and Exclude Patterns now take a list of regular expressions.

* New overview panel selector menu.

* Most display lists may be exported to CSV or Excel formats, including search results.

* Links to external pages and files are now included in results lists.

* Some simple chart displays.

## Bug fixes

* The in and out link calculation display in the Structure Overview panel has been fixed. Previously, these values were not being displayed correctly after the link counts for each document were updated. There is a new button that may be clicked to trigger recalculation; otherwise recalculation occurs periodically, and when a scan completes.

Please report issues at [https://github.com/nazuke/SEOMacroscope/issues](https://github.com/nazuke/SEOMacroscope/issues).

![SEO Macroscope Application Window]({{ "../media/screenshots/seo-macroscope-main-window-v1.6.png" | relative_url }}){: .img-responsive .box-shadow}
{: .screenshot }
