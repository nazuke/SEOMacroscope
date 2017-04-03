---
layout: post
title: "Identifying Duplicate Content with SEO Macroscope"
date: 2017-03-23 10:28:00 -09:00
excerpt: The second beta of SEO Macroscope has been released.
---

I've been implementing several different methods to try and identify potentially duplicate content within the set of pages crawled.
{: .lead }

## Simple ETag and Checksum Logging

The simplest of these is achieved by recording the ETag HTTP Header, if it's returned by the remote web server, and identify which of the crawled URLs appear to have the same ETag value.

Similarly, for any documents that SEO Macroscope actually downloads (such as HTML, PDFs, etc...), a checksum value is computed. Different URLs that have the same checksum are a strong indicator that the content is exactly the same.

## Finding duplicates by Levenshtein Edit Distance

Another technique that I've used is [Levenshtein Edit Distance](https://en.wikipedia.org/wiki/Levenshtein_distance) measuring; leveraging [Dan Harltey's Fastenshtein implementation](https://github.com/DanHarltey/Fastenshtein). Currently, SEO Macroscope will only apply this to documents that have body text in them, such as HTML pages and PDFs.

Briefly, what that means is that if you have multiple pages on your site that are very closely similar, but perhaps with a few minor differences, then these may be detected and reported upon. For example, if two pages are very closely similar, but perhaps they were rendered with very slightly different text in them somewhere, then they will not have matching checksums, but they may be similar enough to fall within the Levenshtein Edit Distance threshold that you specify.

The only drawback is that this is quite an intensive process if there are a lot of pages on your site; so it may be necessary to restrict spidering to a subset.

The SEO Macroscope preferences includes options to specify the initial similarity of the documents to apply the Levenshtein algorithm to. If documents that fall within the parameters are found, they will be reported.
