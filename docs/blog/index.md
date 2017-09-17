---
layout: page
title: SEO Macroscope Blog Posts
---

{% for post in site.posts %}

### {{ post.date | date_to_string }} - [{{ post.title }}]({{ post.url | relative_url }}){: .post-link }
{{ post.excerpt }}

{% endfor %}
