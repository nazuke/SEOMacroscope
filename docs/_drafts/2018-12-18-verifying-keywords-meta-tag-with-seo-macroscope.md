---
layout: post
title: "Verifying page keywords with SEO Macroscope"
date: "2018-12-18 09:00:00 -09:00"
published: true
description: "Use SEO Macroscope to check your web page's keywords match what is specified in the keywords meta tag."
excerpt: "Use SEO Macroscope to check your web page's keywords match what is specified in the keywords meta tag."
---

One fairly recent feature request that I had for SEO Macroscope, was to analyze the contents of the `keywords` meta tag, and verify that the keywords specified did actually appear in the page's body text.
{: .lead }

As we all know, the [`keywords` meta tag](https://webmasters.googleblog.com/2009/09/google-does-not-use-keywords-meta-tag.html) hasn't been used by the major search engines for quite some time now.

However, in a recent feature request, it does appear that this particular tag still lingers in some CMS systems. In particular, the request in question uses this feature as a part of the editorial process to ensure that the keywords that are *supposed* to be in the page's body text, *are* actually used in the page's body text.

Naturally, as these pages are edited by users, and as it's up to the users to ensure that their edits are consistent, this doesn't always work as intended.

To that end, there's a minor feature in SEO Macroscope now that analyzes the contents of the keywords meta tag, and verifies whether or not the keywords are mentioned in the title tag, the description tag, and the body text itself.

This can be used to determine whether or not the published web pages are consistent or not.

The process itself is automatic.

First of all, carry out teh crawl process in the normal way. Speed things up by only crawling HTML pages. Once that's done, the crawled document set will be available and the **Keywords Presence** tab can be selected.

![Crawled website results]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-crawl-results.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

The **Keywords Presence** tab show one row for each keyword-related problem on each page.

#h2 Empty Keywords Meta Tag

If the keywords meta tag is entirely empty, then this will immediately be flagged up as a problem.

![Empty keywords meta tag]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keywords-meta-tag-empty.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

For most sites, this isn't going to be a problem. In this case however, it means that the page author has forgotten to specify which keywords they're trying to target with the page in question.

#h2 Malformed Keywords Meta Tag

Another problem that arises may be where the author has pasted the wrong data into the keywords meta tag, or simply entered text that cannot be properly parsed.

The keywords are defined as a comma-separated values list. This means that of the keyword list cannot be properly parsed into a list of terms, then it will be flagged as an error.

![Malformed keywords meta tag]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/malformed-keywords-meta-tag.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

#h2 Keyword Missing in Body Text

If the keywords meta tag itself is fine, then the keywords themselves are analyzed, and then cross-referenced against the text in the page body, the title tag, and the description tag.

If a keyword is specified in the keywords meta tag, but cannot be found in the body text, then it is flagged as such:

![Keyword missing in body text]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-missing-in-body.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

Likewise, if the keyword *is* present, then it is also reported, for clarity's sake:

![Keyword present in body text]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-present-in-body.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

#h2 Keyword Missing in Title and Description

Similarly, if the keyword is missing from the title tag, or is present, then it is reported as such:

![keyword-missing-in-title]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-missing-in-title.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

Likewise, if it is missing from the description tag, or present, then it is reported too:

![keyword-missing-in-description]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-missing-in-description.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

With the title and description tags, it is up to the disgression of the author as to whether or not this is a problem.
