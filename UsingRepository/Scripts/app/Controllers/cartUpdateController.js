var CartUpdateController = function (cartService) {
    var removeArray = [];
    var quantityArray = [];

    var update = function () {
        $("#btnSubmit1").on("click", updateCart);
    };

    var updateCart = function (e) {
        e.preventDefault();

        if ($('input[name="chkCartRemove"]:checked').length > 0) {
            $('input[name="chkCartRemove"]:checked').each(function (i, item) {
                removeArray.push(item.value);
            });
        }

        if ($('input[name="quantity"]').length > 0) {
            $('input[name="quantity"]').each(function (i, item) {
                quantityArray.push(item.value);
            });
        }

        bootbox.confirm({
            title: "Update And Remove From Cart",
            message: "Are you Sure You Want To Update And Delete These Items From Your Shopping ?",
            buttons: {
                cancel: {
                    label: "Cancel"
                },
                confirm: {
                    label: "Confirm"
                }
            },
            callback: function (result) {
                if (result) {
                    var data = JSON.stringify({ chkCartRemove: removeArray, quantity: quantityArray });
                    cartService.updateCart(data, done, fail);
                }
            }
        });
    };

    var done = function () {
        if (removeArray.length > 0)
            removeArray = [];

        if (quantityArray.length > 0)
            quantityArray = [];

        toastr.info("Successfully Updated And Deleted .");
        $("#cart").load(" #cart");
        $("#menu").load(" #menu");
    };

    var fail = function () {
        toastr.error('fail ...');
    };
    return {
        update: update
    }

}(CartService);
