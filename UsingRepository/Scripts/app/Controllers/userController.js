

var UserController = function (userService) {
    var selector;
    var selectorSpan;

    var init = function (sel , selSpan) {
        selector = sel;
        selectorSpan = selSpan;
        $('#' + selector).keyup(userNameAndEmailUnique);
    };
    var userNameAndEmailUnique = function () {
        var userNameOrEmailValue = $(this).val();
        userService.userCheck(userNameOrEmailValue, done);
    }
    var done = function (data) {
        if (data != null) {
            $('#' + selectorSpan).text(selector + " Exists ?");
            $('#btnSubmit').addClass('disabled');
        } else {
            $('#' + selectorSpan).text('');
            $('#btnSubmit').removeClass('disabled');

        }
    }

    return {
        init : init
    }
};
