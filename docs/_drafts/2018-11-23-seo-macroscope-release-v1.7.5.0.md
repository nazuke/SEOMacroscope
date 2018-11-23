---
layout: post
title: "New v1.7.5 release of SEO Macroscope: Bearer of the word"
date: "2018-11-23 00:00:00 -00:00"
published: true
description: "This release of SEO Macroscope adds keyword meta tag analysis."
excerpt: "This release of SEO Macroscope adds keyword meta tag analysis."
---

This release of SEO Macroscope adds keyword meta tag analysis for some legacy processing, and fixes some bugs.
{: .lead }

Source code and an installer can be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.5.0](https://github.com/nazuke/SEOMacroscope/releases/tag/v1.7.5.0)

Please check the [downloads page]({{ "/downloads/" | relative_url }}) for more recent versions.

## New features in this release include:

* A recent request was made for processing the contents of the legacy "keywords" meta tag. This feature is enabled by default, with the results available in the new **Keywords Presence** display list. Very briefly, the contents of the keywords meta tag is examined, and then the presence or absence of each keyword in the page body text is reported. Currently, this only applies to the body text; other elements, such as the title tag, are not processed.
  * Normally, I would advise against using the keywords meta tag in any new websites; however it appears that this meta tag is still used by some CMS platforms, and is a reasonable method to check that keywords that *should* be present in the body text *are* actually there, or not.
  * Please note that this analysis step is separate to the existing keywords analysis; that analysis ignores the keyword meta tag entirely, and operates purely on the body text alone.

## Bug fixes

* Licence window was broken.
* Preferences window resized for smaller screens.

Please report issues at [https://github.com/nazuke/SEOMacroscope/issues](https://github.com/nazuke/SEOMacroscope/issues).

![SEO Macroscope Application Window]({{ "/media/screenshots/seo-macroscope-main-window-v1.7.5.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }
