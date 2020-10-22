var CartAddController = function(cartService) {
    var button;
    var add = function() {
        $(".product-box").on('click','button.btnAddToCart',addToCart);
        $("#btnAddToCart").click(addToCart);
    };

    var addToCart = function(e) {
        button = $(e.target);

        var productId = button.attr("data-product-id");
        var quantity = $("#quantity").val() || 1;

        var cartParams = { id: productId, quantity: quantity };

        cartService.createCart(cartParams, done, fail);

    };

    var done = function () {
        toastr.success('Product Added Successfully To Cart');
                $('#menu').load(' #menu');

//        bootbox.alert("Product Added Successfully To Cart",
//            function() {
//                $('#menu').load(' #menu');
//            });
    };

    var fail = function() {
//        bootbox.alert('SomeThing Error');
        toastr.error('SomeThing Error');
    };

    return {
        add: add
    }
}(CartService);

