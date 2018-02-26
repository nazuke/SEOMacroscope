---
layout: post
title: "Checking content with custom filters"
date: "2018-02-25 18:00:00 -09:00"
published: true
description: "Use the SEO Macroscope Custom Filters feature to verify the presence or absence of content on your pages."
excerpt: "Use the SEO Macroscope Custom Filters feature to verify the presence or absence of content on your pages."
---

SEO Macroscope's **Custom Filters** feature may be used to verify the presence, or absence of snippets of text, or regular expressions in your HTML, CSS, Javascript, text, and XML pages.
{: .lead }

For example, you can use Custom Filters to verify the presence of:

* Specific CSS classes being used in web pages.
* The presence of tracking tags on web pages, such as Google Analytics tags.
* The proper removal of old code from CSS or Javascript files.
* The removal of old domain names in web pages, when moving to a new domain.
* And many more...

For example, to identify pages that use, or do not use Bootstrap's "lead" CSS class on the https://nazuke.github.io/ site, try the following:

> Fire up SEO Macroscope.

> Paste "https://nazuke.github.io/" into the **Start URL** field.

> ![Enter the Start URL]({{ "/media/screenshots/2018-02-25-checking-content-with-custom-filters/main-window.png" | relative_url }}){: .img-responsive .box-shadow}
{: .screenshot }

> Go to **Edit -> Preferences**.

> Click the **Custom Filter Options** preference tab.

> Ensure that the **Enable custom filter processing** checkbox is enabled.

> Set the **Custom Filter Items** count to 1.

> Ensure that only the **HTML** checkbox of **Apply Custom Filters to Document Types** is enabled.

> ![Configure the Custom Filters preferences]({{ "/media/screenshots/2018-02-25-checking-content-with-custom-filters/custom-filters-prefererences.png" | relative_url }}){: .img-responsive .box-shadow}
{: .screenshot }

> Dismiss the preferences dialogue with **OK**.

> Next, select **Task Parameters -> Custom Filters**.

> For the **Custom Filter 1**, set it's **Filter Action** to **Must have regex**.

> Type <code>class="[^"]\*lead[^"]\*"</code> into the **Search String** field.

> ![Configure the Custom Filters patterns]({{ "/media/screenshots/2018-02-25-checking-content-with-custom-filters/custom-filters-dialogue.png" | relative_url }}){: .img-responsive .box-shadow}
{: .screenshot }

> Dismiss the dialogue with **OK**.

> Start the scan...

> Once the scan is complete, the results can be found in the **View -> Custom Filters** display. There are also Excel and CSV reports available under **Reports -> Custom Filters Report**.

> ![Configure the Custom Filters patterns]({{ "/media/screenshots/2018-02-25-checking-content-with-custom-filters/custom-filters-listview.png" | relative_url }}){: .img-responsive .box-shadow}
{: .screenshot }

To conclude, this can be a simple method to verify that your pages contain data that you expect them to.

