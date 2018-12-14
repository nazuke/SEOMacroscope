---
layout: post
title: "Verifying page keywords with SEO Macroscope"
date: "2018-12-14 09:00:00 -09:00"
published: true
description: "Use SEO Macroscope to check your web page's keywords match what is specified in the keywords meta tag."
excerpt: "Use SEO Macroscope to check your web page's keywords match what is specified in the keywords meta tag."
---

One fairly recent feature request that I had for SEO Macroscope, was to analyze the contents of the `keywords` meta tag, and verify that the keywords specified did actually appear in the page's body text.
{: .lead }





![keyword-crawl-results]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-crawl-results.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }



![keywords-meta-tag-empty]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keywords-meta-tag-empty.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }


![malformed-keywords-meta-tag]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/malformed-keywords-meta-tag.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }


![keyword-missing-in-title]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-missing-in-title.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }



![keyword-missing-in-description]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-missing-in-description.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }


![keyword-missing-in-body]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-missing-in-body.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }

![keyword-present-in-body]({{ "/media/screenshots/2018-12-14-verifying-keywords-meta-tag-with-seo-macroscope/keyword-present-in-body.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }







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

![Extract telephone numbers with regular expressions]({{ "/media/screenshots/2018-06-04-finding-contact-details-with-seo-macroscope/data-extractors-regex.png" | relative_url }}){: .img-responsive .box-shadow }
{: .screenshot }
