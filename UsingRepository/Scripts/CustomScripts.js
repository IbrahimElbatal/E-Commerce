//
//$("#myCarousel").carousel({
//    interval: 4000
//});
//$("#myCarousel1").carousel({
//    interval: 5000
//});
//
//$(window).scroll(function () {
//    $("#topcontrol").css("display", "block");
//});
//$(".flexslider").flexslider({
//    animation: "fade",
//    slideshowSpeed: 3000,
//    animationSpeed: 600,
//    controlNav: false,
//    directionNav: true,
//    controlsContainer: ".flex-container"
//});
//$("#search").focus(function() {
//    $("a.logo.pull-left").css("display", "none");
//    $(".navbar-inner.main-menu").css("border-bottom", "none");
//
//}).blur(function() {
//    $("a.logo.pull-left").css("display", "block");
//    $(".navbar-inner.main-menu").css("border-bottom", "5px solid #eb4800");
//});
//var products = new Bloodhound({
//    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("Name"),
//    queryTokenizer: Bloodhound.tokenizers.whitespace,
//    remote: {
//        url: "/api/Product?query=%QUERY",
//        wildcard: "%QUERY"
//    }
//});
//$("#Search1 .typeahead").typeahead({
//        minLength: 2
//    },
//    {
//        name: "Product",
//        display: "Name",
//        source: products
//    });
//
//
//$('ul.nav.nav-list li').click(function () {
//    $('ul.nav.nav-list li').removeClass('active');
//    $(this).addClass('active');
//});
//
//
//
//$("#btnShowBillingDetails").click(function () {
//    $("#billingDetails").removeAttr("style");
//    $('#triggerBillingDetails').trigger("click");
//});
//$("#btnShowOrderConfirm").click(function () {
//    $('#triggerOrderConfirm').trigger("click");
//});




























//function ajaxDelete(selector, dataName, url, table) {
//    $("#" + selector).on("click",
//        ".buttonRemove",
//        function () {
//
//            var id = $(this).attr("data-" + dataName + "-id");
//            var name = $(this).attr("data-" + dataName + "-Name");
//
//            var button = $(this);
//            bootbox.confirm({
//                title: "Remove From " + selector,
//                message: "Are You Sure You Want To Delete " + dataName + " With Name = " + name,
//                buttons: {
//                    cancel: {
//                        label: "Cancel"
//                    },
//                    confirm: {
//                        label: "Confirm"
//                    }
//                },
//                callback:
//                    function (result) {
//                        if (result) {
//                            $.ajax({
//                                url: url,
//                                data: "{ id:" + id + "}",
//                                type: "POST",
//                                contentType: "application/json;charset=utf-8",
//                                success: function () {
//                                    table.row(button.parents("tr")).remove().draw();
//                                    toastr.success("Successfully Deleted.");
//                                }
//                            });
//                        }
//                    }
//            }).find(".modal-header").css({
//                //                'background-color': "#ff0",
//                'font-weight': "bold"
//            });
//        });
//}
//
//function AddToCart(url, redirectUrl) {
//
//    $("#btnAddToCart").on("click",
//        function () {
//            var productId = $(this).attr("data-product-id");
//            var quantity = $("#quantity").val();
//
//            var cartParams = { id: productId, quantity: quantity };
//
//            $.ajax({
//                url: url,
//                type: "Post",
//                data: cartParams
//            }).done(function () {
//                bootbox.alert("Product Added Successfully To Cart",
//                    function () {
//                        location.href = redirectUrl;
//                    });
//
//            }).fail(function () {
//                bootbox.alert('SomeThing Error');
//            });
//        });
//}
//
//
//function UpdateCartFunction(url) {
//    var removeArray = [];
//    var quantityArray = [];
//    $("#btnSubmit1").on("click",
//        function (e) {
//            e.preventDefault();
//            var button = $('input[name="chkCartRemove"]:checked');
//            if ($('input[name="chkCartRemove"]:checked').length > 0) {
//                $('input[name="chkCartRemove"]:checked').each(function (i, item) {
//                    removeArray.push(item.value);
//                });
//            }
//            if ($('input[name="quantity"]').length > 0) {
//                $('input[name="quantity"]').each(function (i, item) {
//                    quantityArray.push(item.value);
//                });
//            }
//            bootbox.confirm({
//                title: "Update And Remove From Cart",
//                message: "Are you Sure You Want To Update And Delete These Items From Your Shopping ?",
//                buttons: {
//                    cancel: {
//                        label: "Cancel"
//                    },
//                    confirm: {
//                        label: "Confirm"
//                    }
//                },
//                callback: function (result) {
//                    if (result) {
//                        $.ajax({
//                            url: url,
//                            data: JSON.stringify({ chkCartRemove: removeArray, quantity: quantityArray }),
//                            type: "POST",
//                            contentType: "application/json;charset=utf-8",
//                            success: function () {
//                                button.parents("tr").remove();
//                                if (removeArray.length > 0) {
//                                    removeArray = [];
//                                }
//                                if (quantityArray.length > 0) {
//                                    quantityArray = [];
//                                }
//                                alert("Successfully Updated And Deleted .");
//                                $("#cart").load(" #cart");
//                            }
//                        });
//                    }
//                }
//            });
//
//        });
//}
//
//function CheckUserNameAndEmail(selector, selectorErrorSpan) {
//    $('#' + selector).on('keyup',
//        function () {
//            var userNameOrEmailValue = $(this).val();
//            $.ajax({
//                url: '/api/User?userName=' + userNameOrEmailValue,
//                method: 'Get'
//            }).done(function (data) {
//                if (data != null) {
//                    $('#' + selectorErrorSpan).text(selector + " Exists ?");
//                    $('#btnSubmit').addClass('disabled');
//                } else {
//                    $('#' + selectorErrorSpan).text('');
//                    $('#btnSubmit').removeClass('disabled');
//
//                }
//            });
//        });
//}







