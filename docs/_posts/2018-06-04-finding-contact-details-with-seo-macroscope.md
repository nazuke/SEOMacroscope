---
layout: post
title: "Finding contact details with SEO Macroscope"
date: "2018-06-04 09:00:00 -09:00"
published: true
description: "SEO Macroscope provides some simple features to find email and telephone links used throughout your website."
excerpt: "SEO Macroscope provides some simple features to find email and telephone links used throughout your website."
---

One of the earlier web crawlers that I wrote to help with my work as a webmaster was a Perl-based crawler that scanned the corporate website for all instances of email address and telephone number links in use. This was mostly of use for checking that no incorrect email addresses were still appearing on the website, in particular employee-specific emails in marketing campaigns.
{: .lead }

So when I started work on SEO Macroscope, I recycled this idea to test out the extraction functionality, Excel and CSV reporting, and so on.

In usage, it's very simple. When scanning a website, SEO Macroscope will automatically look for email and telephone links on each HTML page, and extract them out into a list associated with each page.

When the scan is complete, the extracted email addresses and telephone numbers can be found under their respective tab views, and the pages on which they appear.

![Email address list view]({{ "/media/screenshots/2018-06-04-finding-contact-details-with-seo-macroscope/email-addresses.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

In addition, the email addresses and telephone numbers can be exported as Excel or CSV reports with the **Contact Details Report**.

![Export contact details to an Excel report]({{ "/media/screenshots/2018-06-04-finding-contact-details-with-seo-macroscope/save-excel-report.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

The extraction process described above only operates on link elements; if you require more sophisticated extraction of email addresses or telephone numbers, then the **Data Extractors** functions can be used instead.

For example, data extractors can be configured using regular expressions to extract telephone number from the page body text, even if they do not appear in link elements. Likewise, the same is true for email addresses.

![Extract telephone numbers with regular expressions]({{ "/media/screenshots/2018-06-04-finding-contact-details-with-seo-macroscope/save-excel-report.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }
