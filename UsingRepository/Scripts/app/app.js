



var width = 136;
$(document).scroll(function () {
    if (Math.ceil($(this).scrollTop()) >= 135) {
        $('.navbar .main-menu').addClass('sticky-navbar');
    } else
    {
        $('.navbar .main-menu').removeClass('sticky-navbar');
    }
});

$("#myCarousel").carousel({
    interval: 4000
});
$("#myCarousel1").carousel({
    interval: 5000
});

//aos animation
AOS.init();


//start of owl-carousel
var responsive = {
    0: {
        items: 1
    },
    500: {
        items: 2
    },
    850: {
        items: 3
    },
    1130: {
        items: 4
    }
}
$('.product-slider').owlCarousel({
    loop: true,
    autoplay: true,
    nav: true,
    responsive: responsive
});


var responsive2 = {
    0: {
        items: 1
    },
    500: {
        items: 2
    },
    1100: {
        items: 3
    }
}
$('.product-slider2').owlCarousel({
    loop: true,
    autoplay: true,
    nav: true,
    responsive: responsive2
});

//end of owl-carousel


//start of bars button
$('.nav-icons span').click(function () {
    if ($("._toggleMenu").css('display') === "block") {
        $("._toggleMenu").css("display", "none");

    } else {
        $("._toggleMenu").css("display", "block");

    }

});

//end of bars button
$(window).scroll(function () {
    $("#topcontrol").css("display", "block");
});
$(".flexslider").flexslider({
    animation: "fade",
    slideshowSpeed: 3000,
    animationSpeed: 600,
    controlNav: false,
    directionNav: true,
    controlsContainer: ".flex-container"
});
$("#search").focus(function () {
    $("a.logo.pull-left").css("display", "none");
    $(".navbar-inner.main-menu").css("border-bottom", "none");

}).blur(function () {
    $("a.logo.pull-left").css("display", "block");
    $(".navbar-inner.main-menu").css("border-bottom", "5px solid #eb4800");
});

var products = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("Name"),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    remote: {
        url: "/api/Product?term=%QUERY",
        wildcard: "%QUERY"
    }
});
$("#Search1 .typeahead").typeahead({
        minLength: 1,
        limit: 10
    },
    {
        name: "Product",
        display: "Name",
        source: products
    });




$('ul.nav.nav-list li').click(function () {
    $('ul.nav.nav-list li').removeClass('active');
    $(this).addClass('active');
});



$("#btnShowBillingDetails").click(function () {
    $("#billingDetails").removeAttr("style");
    $('#triggerBillingDetails').trigger("click");
});
$("#btnShowOrderConfirm").click(function () {
    $('#triggerOrderConfirm').trigger("click");
});
