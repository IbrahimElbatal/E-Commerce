
var UserService = function() {
    var userCheck = function (userNameOrEmailValue , done) {
        $.ajax({
            url: '/api/User?userName=' + userNameOrEmailValue,
            method: 'Get'
        }).done(done);
    };

    var saveUserDetails = function(userId,userDetails,done) {
        $.ajax({
            url: '/api/User/'+userId,
            type: 'post',
            data: JSON.stringify(userDetails),
            contentType: "application/json;charset=utf-8"
        }).done(done);
    };
    return {
        userCheck: userCheck,
        saveUserDetails: saveUserDetails
    }
}();