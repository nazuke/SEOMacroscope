/******************************************************************************/

$("window").ready(
  function ()
  {

    configureGaBlogPostLinks();

    configureGaReleaseLinks();

    configureGaInstallerLinks();

    configureDonationButtonEvents();
    configureDonationLinkEvents();

  }
);

/******************************************************************************/

function configureGaBlogPostLinks ()
{

  $(".post-link").click(

    function ( event )
    {

      event.preventDefault();

      let LinkClicked = this;
      let TagHref     = $(LinkClicked).attr("href");
      let Tag         = TagHref.split('/').pop().split('#')[0].split('?')[0];

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'blog',
          eventAction: 'post-link',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function ()
          {
            window.location = TagHref;
          }
        }
      );

    }

  );

}

/******************************************************************************/

function configureGaReleaseLinks ()
{

  $(".link-release").click(

    function ( event )
    {

      event.preventDefault();

      let LinkClicked = this;
      let TagHref     = $(LinkClicked).attr("href");
      let Tag         = TagHref.split('/').pop().split('#')[0].split('?')[0];

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'downloads',
          eventAction: 'link-release',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function ()
          {
            window.location = TagHref;
          }
        }
      );

    }

  );

}

/******************************************************************************/

function configureGaInstallerLinks ()
{

  $(".link-installer").click(

    function ( event )
    {

      event.preventDefault();

      let LinkClicked = this;
      let TagHref     = $(LinkClicked).attr("href");
      let Tag         = TagHref.split('/').pop().split('#')[0].split('?')[0];

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'downloads',
          eventAction: 'link-installer',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function ()
          {
            window.location = TagHref;
          }
        }
      );

    }

  );

}

/******************************************************************************/

function configureDonationButtonEvents ()
{

  $(".donation-button").mouseover(

    function ( event )
    {

      event.preventDefault();

      let LinkHovered = this;
      let TagHref     = $(LinkHovered).attr("href");
      let Tag         = TagHref.split('/').pop().split('#')[0].split('?')[0];

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'donations',
          eventAction: 'donation-button-mouseover',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function ()
          {
          }
        }
      );

    }

  );

  $(".donation-button").click(

    function ( event )
    {

      event.preventDefault();

      let LinkClicked = this;
      let TagHref     = $(LinkClicked).attr("href");
      let Tag         = TagHref.split('/').pop().split('#')[0].split('?')[0];

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'donations',
          eventAction: 'donation-button-click',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function ()
          {
            window.location = TagHref;
          }
        }
      );

    }

  );

}

/******************************************************************************/

function configureDonationLinkEvents ()
{

  $(".donation-link").mouseover(

    function ( event )
    {

      event.preventDefault();

      let LinkHovered = this;
      let TagHref     = $(LinkHovered).attr("href");
      let Tag         = TagHref.split('/').pop().split('#')[0].split('?')[0];

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'donations',
          eventAction: 'donation-link-mouseover',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function ()
          {
          }
        }
      );

    }

  );

  $(".donation-link").click(

    function ( event )
    {

      event.preventDefault();

      let LinkClicked = this;
      let TagHref     = $(LinkClicked).attr("href");
      let Tag         = TagHref.split('/').pop().split('#')[0].split('?')[0];

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'donations',
          eventAction: 'donation-link-click',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function ()
          {
            window.location = TagHref;
          }
        }
      );

    }

  );

}

/******************************************************************************/
