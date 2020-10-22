var CartService = function() {
    var createCart = function(data,done,fail) {
        $.ajax({
            url: '/rpc/Cart/AddToCart',
            type: "Post",
            data: data
        })
            .done(done)
            .fail(fail);
    };

    var updateCart = function (data, done, fail) {
        $.ajax({
                url: '/rpc/Cart/UpdateCart',
                data: data,
                type: "POST",
                contentType: "application/json;charset=utf-8"
            })
            .done(done)
            .fail(fail);
    };
    return {
        createCart: createCart,
        updateCart: updateCart
    }
}();