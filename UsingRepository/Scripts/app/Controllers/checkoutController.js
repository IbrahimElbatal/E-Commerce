var CheckoutController = function(checkoutService) {
    var init = function() {
        $('#finish').click(confirmOrder);
    };
   var confirmOrder= function (e) {
        e.preventDefault();
        var comment = $('textarea[name="comment"]').val();
        checkoutService.confirmOrder(comment, done);

    }
    var done = function () {
        bootbox.alert('success.......');
        window.location.href = window.location.origin + '/';
    };

    return{
        init: init
    }
}(CheckoutService);
