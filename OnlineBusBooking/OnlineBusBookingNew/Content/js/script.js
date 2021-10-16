
$(function () {

    $('#AlertBox').removeClass('hide');
    $('#AlertBox').delay(3000).slideUp(500);
});
jQuery(document).ready(function () {
        "use strict";

        /*=================== Simple Weather Plugin ===================*/
        $.simpleWeather({
            location: 'London, United Kingdom',
            woeid: '',
            unit: 'c',
            success: function(weather) {
             var html = $("#weather").after('<div class="weather-city">'+weather.city+', '+weather.region+'</div><img  src="images/weather/'+weather.code+'.png"><span class="temprature"><i class="icon-'+weather.code+'"></i>'+weather.temp+'&deg; <i>'+weather.units.temp+'<i></span>');

            $("#weather").html(html);
            },
            error: function(error) {
            $("#weather").html('<p>'+error+'</p>');
            }
        });


        /*=================== Gallery Tabs ===================*/
        $(".gallery.nav-tabs > li").each(function(){
            $(this).find("a").on("click",function(){
                $(".gallery.nav-tabs > li").removeClass("activated");
                $(this).parent().parent().parent().parent().addClass("activated");
            });
        });


        /*=================== Destination Carousel ===================*/
        $(".destination-thumb > a").on("click", function() {
            $(".destination-thumb > a").removeClass("true");
            $(this).addClass("true");
        });




        /*=================== Topbar Registration and Signup Buttons ===================*/
        var registration_height = $(".registration").innerHeight();
        $(".registration").css({
            "margin-top": -registration_height
        });

        $(".registration-buttons > a").on("click", function(e) {
            var target = $(e.target);
            $(".registration-buttons > a").removeClass("active");
            $(this).addClass("active");

            $(".registration .container > div").slideUp();
            if (target.is('#login-btn')) {
                $(".registration .login-form").slideDown();
            }
            if (target.is('#register-btn')) {
                $(".registration .registration-form").slideDown();
            }

            $(".registration").css({
                "margin-top": 0
            });
            return false;
        });

        $("html,.close-btn").on("click", function() {
            $(".registration-buttons > a").removeClass("active");
            var registration_height2 = $(".registration").innerHeight();
            $(".registration").css({
                "margin-top": -registration_height2
            });
        });

        $(".topbar, .registration").on("click", function(e) {
            e.stopPropagation();
        });




        /*=================== Date Picker Initialization ===================*/
        $('.popupDatepicker').datepicker({
            startView: 1,
            format: "dd/mm/yy",
            minViewMode: 0,
            todayBtn: "linked",
            clearBtn: true,
            calendarWeeks: true,
            autoclose: true,
            todayHighlight: true,
            datesDisabled: ['08/06/2015', '08/21/2015'],
            toggleActive: true
        });




        /* ============ Number Increamenter Decreamenter ================*/
        $(".inc-dec").data({
            'min': 0,
            'max': 20,
            'step': 1
        }).userincr();




        /*=================== Book Now Popup ===================*/
        $(".call-popup").on("click", function() {
            $(".booking-popup").fadeIn();
            return false;
        });

        $(".booking-popup").on("click", function() {
            $(this).fadeOut();
            return false;
        })

        $(".popup-inner, .datepicker").on("click", function(e) {
            e.stopPropagation();
        });

        $(".booking-popup .close-btn").on("click", function() {
            $(".booking-popup").fadeOut();
        });




        /*=================== Popup ===================*/
        $(".call-popup-alert").on("click", function() {
            $(".popup").fadeIn();
            return false;
        });

        $(".popup").on("click", function() {
            $(this).fadeOut();
            return false;
        })

        $(".simple-popup-inner").on("click", function(e) {
            e.stopPropagation();
        });

        $(".popup .close-btn").on("click", function() {
            $(".popup").fadeOut();
        });




        /*=================== Offer Widget Active Function ===================*/
        $(".offer:first-child").addClass("active");
        $(".offer").on("mouseenter", function() {
            $(this).parent().find(".offer").removeClass("active");
            $(this).addClass("active");
        });






        /*=================== Accordion ===================*/
        $('.toggle .content').hide();
        $('.toggle h3:first').addClass('active').next().slideDown(500).parent().addClass("activate");
        $('.toggle h3').on("click", function() {
            if ($(this).next().is(':hidden')) {
                $('.toggle h3').removeClass('active').next().slideUp(500).removeClass('animated zoomIn').parent().removeClass("activate");
                $(this).toggleClass('active').next().slideDown(500).addClass('animated zoomIn').parent().toggleClass("activate");
            }
        });




        /*=================== Custom Scrollbar Initialization ===================*/
        $('.menu-links > ul,.service-inner, .popup-inner').enscroll({
            showOnHover: false,
            verticalTrackClass: 'track3',
            verticalHandleClass: 'handle3'
        });

        if ($(window).width() < 980) {
            var second_menu = $(".second-menu").html();
            $('nav > ul.first-menu').append(second_menu);

            $('nav > ul').enscroll({
                showOnHover: false,
                verticalTrackClass: 'track3',
                verticalHandleClass: 'handle3'
            });

            /*=================== Responsive Menu ===================*/
            $(".responsive-menu-btn").on("click", function() {
                $(this).next("ul").slideToggle();
                return false;
            }); 
            $("li.menu-item-has-children > a").on("click", function() {
                $(this).next("ul").slideToggle();
                return false;
            }); 
        }




        /*=================== LightBox ===================*/
        var foo = $('.lightbox');
        foo.poptrox({usePopupCaption: true});




/*=================== Ajax Contact Form ===================*/
$('#contactform').submit(function() {
    var action = $(this).attr('action');
    $("#message").slideUp(750, function() {
        $('#message').hide();
        $('#submit')
            .after('<img src="images/ajax-loader.gif" class="loader" />')
            .attr('disabled', 'disabled');
        $.post(action, {
                name: $('#name').val(),
                email: $('#email').val(),
                phone: $('#phone').val(),
                comments: $('#comments').val(),
                verify: $('#verify').val()
            },
            function(data) {
                document.getElementById('message').innerHTML = data;
                $('#message').slideDown('slow');
                $('#contactform img.loader').fadeOut('slow', function() {
                    $(this).remove()
                });
                $('#submit').removeAttr('disabled');
                if (data.match('success') != null) $('#contactform').slideUp('slow');

            }
        )

        .fail(function() {
            alert( "error" );
        })
    });

    return false;
});

}); /*=== Document.Ready Ends Here ===*/


jQuery(window).load(function() {
    "use strict";

    $('.parallax').scrolly({bgParallax: true});




    /*=================== Rooms Image Height ===================*/
    $(".rooms-list").addClass("loaded");

    var l = $(".rooms-list > ul li").length;
    for (var i = 0; i <= l; i++) {
        var room_list = $(".rooms-list > ul li").eq(i);
        var room_img_height = $(room_list).find(".room > img").innerHeight();
        $(room_list).css({
            "height": room_img_height
        });
        $(room_list).find(".room > img").css({
            "width": "200%"
        });
    }




    /*=================== STICKY HEADER ===================*/
    var header_container = $(".header-container.stick").innerHeight();
    $(".theme-layout").css({
        "margin-top": header_container
    });

    var transparent_header = $(".transparent-header .topbar").innerHeight();
    $(".theme-layout").css({
        "margin-top": transparent_header
    });

    var creative_header = $(".creative-header .topbar").innerHeight();
    $(".theme-layout").css({
        "margin-top": creative_header
    });


    $(window).scroll(function() {
        var scroll = $(window).scrollTop();
        if (scroll >= 70) {
            $(".stick").addClass("sticky");
        } else {
            $(".stick").removeClass("sticky");
        }
    });



}); /*=== Window.Load Ends Here ===*/