/******************************************************************************/

$("window").ready(
  function () {
    configureGaReleaseLinks();
    configureGaInstallerLinks();
  }
);

/******************************************************************************/

function configureGaReleaseLinks() {

  $(".link-release").click(

    function ( event ) {

      event.preventDefault();

      let LinkClicked = this;
      let TagHref = $(LinkClicked).attr("href");
      let Tag = TagHref.split('/').pop().split('#')[0].split('?')[0];

      console.log( "TagHref:", TagHref );

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'downloads',
          eventAction: 'link-release',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function() {
            window.location = TagHref;
          }
        }
      );

    }

  );

  console.log("Done: configureGaReleaseLinks");

}

/******************************************************************************/

function configureGaInstallerLinks() {

  $(".link-installer").click(

    function ( event ) {

      event.preventDefault();

      let LinkClicked = this;
      let TagHref = $(LinkClicked).attr("href");
      let Tag = TagHref.split('/').pop().split('#')[0].split('?')[0];

      console.log( "TagHref:", TagHref );

      ga(
        'send',
        {
          hitType: 'event',
          eventCategory: 'downloads',
          eventAction: 'link-installer',
          eventLabel: Tag,
          eventValue: 0,
          transport: 'beacon',
          hitCallback: function() {
            window.location = TagHref;
          }
        }
      );

    }

  );

  console.log("Done: configureGaInstallerLinks");

}

/******************************************************************************/
