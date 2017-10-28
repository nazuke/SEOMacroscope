---
layout: page
title: "SEO Macroscope Installer Downloads"
description: "Download the current and previous releases of SEO Macroscope."
---

{% assign releases = site.collection_releases | reverse %}

## PLATFORMS

**SEO Macroscope** is developed on Windows. Currently, tested on Windows 7 and 10 with the .NET 4.5.2 framework.
{: .lead }

Macintosh users, with the Mono framework and Xamarin IDE installed, *may* be able to compile and run the project. I do not currently plan on making a native port to Macintosh.

## INSTALLER

Releases of SEO Macroscope may be found on GitHub at:

* [https://github.com/nazuke/SEOMacroscope/releases](https://github.com/nazuke/SEOMacroscope/releases)

The most recent release is at:

* [{{ releases[0].name }}](https://github.com/nazuke/SEOMacroscope/releases/tag/{{ releases[0].tag }}){: .link-release }
    * Windows {{ releases[0].arch }} bit installer: [{{ releases[0].installer }}](https://github.com/nazuke/SEOMacroscope/releases/download/{{ releases[0].tag }}/{{ releases[0].installer }}){: .link-installer }

Previous releases:

{% for release in releases %}
* [{{ release.name }}](https://github.com/nazuke/SEOMacroscope/releases/tag/{{ release.tag }}){: .link-release }
    * Windows {{ release.arch }} bit installer: [{{ release.installer }}](https://github.com/nazuke/SEOMacroscope/releases/download/{{ release.tag }}/{{ release.installer }}){: .link-installer }
{% endfor %}

## SOURCE CODE

The source code project may be cloned directly from the Git repository at:

* [https://github.com/nazuke/SEOMacroscope](https://github.com/nazuke/SEOMacroscope)

SEO Macroscope is currently developed with [Microsoft Visual Studio](https://www.visualstudio.com/). Earlier versions were developed with [SharpDevelop](http://www.icsharpcode.net/opensource/sd/).
