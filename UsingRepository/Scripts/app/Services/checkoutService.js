var CheckoutService = function() {
    var confirmOrder = function(comment ,done) {
        $.post('/rpc/Cart/ConfirmOrder',
            { "": comment },done);
    };
    return{
        confirmOrder: confirmOrder
    }
}();