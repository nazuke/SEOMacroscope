---
layout: page
title: "SEO Macroscope Manual :: Concepts"
---

Explanations of how SEO Macroscope deals with a few details under the hood.
{: .lead }

### "Internal" and "External" Hosts

Briefly, SEO Macroscope maintains the notion of a URL as belonging to either an "internal", or an "external" host.

Internal hosts will generally be crawled, dependent on other preference settings; whereas external hosts will not. In many places, URLs are highlighted as green when they are considered internal.

An "internal" host is one that is explicitly specified either via the **Start URL** field, or is present in a loaded or pasted URL list. There is also the option of using the context menu in some of the overview panels, to mark a particular URL as belonging to an internal host.

An "external" host is generally one that is linked to from HTML pages or stylesheets, but is different to the **Start URL** or URL list host(s).

Generally, external hosts will be issued a HEAD request, but will not be crawled.
