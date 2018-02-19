---
layout: page
title: "SEO Macroscope Installer Downloads"
description: "Download the current and previous releases of SEO Macroscope."
---

{% assign releases = site.collection_releases | reverse %}

## PLATFORMS

**SEO Macroscope** is developed on Windows. Currently, tested on Windows 7 and 10. HTTP/2 support is currently only fully supported on Windows 10 or later.
{: .lead }

Macintosh and Linux users, with the Mono framework and Xamarin IDE installed, *may* be able to compile and run the project. I do not currently plan on making a native port to Macintosh or Linux.

## INSTALLER

Releases of SEO Macroscope may be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases](https://github.com/nazuke/SEOMacroscope/releases)

Installer downloads and sources:

{% for release in releases %}
{% if release.published == true %}
* [{{ release.name }}](https://github.com/nazuke/SEOMacroscope/releases/tag/{{ release.tag }}){: .link-release }
    * Windows {{ release.arch }} bit installer: [{{ release.installer }}](https://github.com/nazuke/SEOMacroscope/releases/download/{{ release.tag }}/{{ release.installer }}){: .link-installer }
{% endif %}
{% endfor %}

## SOURCE CODE

The source code project may be cloned directly from the Git repository at:

* [https://github.com/nazuke/SEOMacroscope](https://github.com/nazuke/SEOMacroscope)

SEO Macroscope is currently developed with [Microsoft Visual Studio](https://www.visualstudio.com/). Earlier versions were developed with [SharpDevelop](http://www.icsharpcode.net/opensource/sd/).
