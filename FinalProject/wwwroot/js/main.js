(function ($) {
    "use strict";

    /*------------------------------------
        Sticky Menu 
    --------------------------------------*/
    var windows = $(window);
    var stick = $(".header-sticky");
    windows.on('scroll', function () {
        var scroll = windows.scrollTop();
        if (scroll < 5) {
            stick.removeClass("sticky");
        } else {
            stick.addClass("sticky");
        }
    });
    /*------------------------------------
        jQuery MeanMenu 
    --------------------------------------*/
    $('.main-menu nav').meanmenu({
        meanScreenWidth: "767",
        meanMenuContainer: '.mobile-menu'
    });


    /* last  2 li child add class */
    $('ul.menu>li').slice(-2).addClass('last-elements');
    /*------------------------------------
        Owl Carousel
    --------------------------------------*/
    $('.slider-owl').owlCarousel({
        loop: true,
        nav: true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 2500,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });

    $('.partner-owl').owlCarousel({
        loop: true,
        nav: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 3
            },
            1000: {
                items: 5
            }
        }
    });

    $('.testimonial-owl').owlCarousel({
        loop: true,
        nav: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });
    /*------------------------------------
        Video Player
    --------------------------------------*/
    $('.video-popup').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        zoom: {
            enabled: true,
        }
    });

    $('.image-popup').magnificPopup({
        type: 'image',
        gallery: {
            enabled: true
        }
    });
    /*----------------------------
        Wow js active
    ------------------------------ */
    new WOW().init();
    /*------------------------------------
        Scrollup
    --------------------------------------*/
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });
    /*------------------------------------
        Nicescroll
    --------------------------------------*/
    $('body').scrollspy({
        target: '.navbar-collapse',
        offset: 95
    });
    $(".notice-left").niceScroll({
        cursorcolor: "#EC1C23",
        cursorborder: "0px solid #fff",
        autohidemode: false,

    });

})(jQuery);



/*------------------------------------
        All search
    --------------------------------------*/
$(document).on("keyup", "#all-search", function () {
    $("#search-box-all").children().slice(1).remove()
    if ($("#all-search").val().length > 0) {
        $.ajax({
            type: "get",
            url: "/Home/SearchAll/",
            data: {
                "key": $("#all-search").val()
            },
            success: function (resp) {
                $("#search-box-all").append(resp)
            }
        })
    }
})


/*------------------------------------
        Courses search
    --------------------------------------*/
$(document).on("keyup", "#course-search", function () {
    $("#search-box-course").children().slice(1).remove()
    if ($("#course-search").val().length > 0) {
        $.ajax({
            type: "get",
            url: "/Course/SearchCourse/",
            data: {
                "key": $("#course-search").val()
            },
            success: function (resp) {
                $("#search-box-course").append(resp)
            }
        })
    }
})


/*------------------------------------
        Events search
    --------------------------------------*/
$(document).on("keyup", "#event-search", function () {
    $("#search-box-event").children().slice(1).remove()
    if ($("#event-search").val().length > 0) {
        $.ajax({
            type: "get",
            url: "/Event/SearchEvent/",
            data: {
                "key": $("#event-search").val()
            },
            success: function (resp) {
                $("#search-box-event").append(resp)
            }
        })
    }
})


/*------------------------------------
        Teachers search
    --------------------------------------*/
$(document).on("keyup", "#teacher-search", function () {
    $("#search-box-teacher").children().slice(1).remove()
    if ($("#teacher-search").val().length > 0) {
        $.ajax({
            type: "get",
            url: "/Teacher/SearchTeacher/",
            data: {
                "key": $("#teacher-search").val()
            },
            success: function (resp) {
                $("#search-box-teacher").append(resp)
            }
        })
    }
})



/*------------------------------------
        Blogs search
    --------------------------------------*/
$(document).on("keyup", "#blog-search", function () {
    $("#search-box-blog").children().slice(1).remove()
    if ($("#blog-search").val().length > 0) {
        $.ajax({
            type: "get",
            url: "/Blog/SearchBlog/",
            data: {
                "key": $("#blog-search").val()
            },
            success: function (resp) {
                $("#search-box-blog").append(resp)
            }
        })
    }
})