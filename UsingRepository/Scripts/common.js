(function() {
    var $menu = $("#menu ul");
    $(".navbar.main-menu")
//        .after('<div class="_toggleMenu"><a class="toggleMenu" href="#">Open MENU</a><ul class="nav"></ul></div>');
        .after('<div class="_toggleMenu"><a class="toggleMenu active" href="#"></a><ul class="nav"></ul></div>');
    $("._toggleMenu .nav").html($menu.html());
})();

var ww = document.body.clientWidth;

$(document).ready(function() {
    $("._toggleMenu .nav li a").each(function() {
        if ($(this).next().length > 0) {
            $(this).addClass("parent");
        };
    });
    $("._toggleMenu .toggleMenu").click(function(e) {
        e.preventDefault();
        $(this).toggleClass("active");
        $("._toggleMenu .nav").toggle();
    });
    adjustMenu();
});
$(window).bind("resize orientationchange",
    function() {
        ww = document.body.clientWidth;
        adjustMenu();
    });

var adjustMenu = function() {
    if (ww < 767) {
        $("#menu ul").css('display', 'none');
//        $("ul.user-menu").css('display', 'none');
        $(".form-group.col-xs-12").css("width", "496px");
        if (!$(".toggleMenu").hasClass("active")) {
            $("._toggleMenu .nav").hide();
        } else {
            $("._toggleMenu .nav").show();
        }
        $("._toggleMenu .nav li").unbind("mouseenter mouseleave");
        $("._toggleMenu .nav li a.parent").unbind("click").bind("click",
            function(e) {
                // must be attached to anchor element to prevent bubbling
                e.preventDefault();
                $(this).parent("li").toggleClass("hover");
            });
    } else if (ww >= 767) {
        $("._toggleMenu").css("display", "none");
        $("#menu ul").css('display', 'block');
//        $("ul.user-menu").css('display', 'block'); top navbar
        $(".form-group.col-xs-12").css("width", "400px");
        $("._toggleMenu .nav").show();
        $("._toggleMenu .nav li").removeClass("hover");
        $("._toggleMenu .nav li a").unbind("click");
        $("._toggleMenu .nav li").unbind("mouseenter mouseleave").bind("mouseenter mouseleave",
            function() {
                // must be attached to li so that mouseleave is not triggered when hover over submenu
                $(this).toggleClass("hover");
            });
    }
};

//Menu
$("#menu > ul").superfish({
    delay: 100,
    animation: { opacity: "show", height: "show" },
    speed: "fast",
    arrowClass: false,
    autoArrows: true
});