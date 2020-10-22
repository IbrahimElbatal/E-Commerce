

var DeleteService = function () {
    var deleteFun = function (url, done, fail) {
        $.ajax({
            url: url,
            type: 'Delete'
        }).done(done).fail(fail);
    };

    return {
        deleteFun: deleteFun
    }
}();