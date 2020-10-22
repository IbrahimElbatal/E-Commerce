var SaveUserDetailsController = function(userService) {
    var userId;
    var init = function (user) {
        userId = user;
        $('#btnSaveUserDetails').click(saveDetails);
    };
    var saveDetails =function(e) {
        e.preventDefault();
        var userDetails = {
            FirstName: $('#firstName').val(),
            LastName: $('#lastName').val(),
            Fax: $('#fax').val(),
            Address: $('#address').val(),
            PostedCode: $('#postCode').val(),
            CountryId: parseInt($('#ddlCountry').val()),
            CityId: parseInt($('#ddlCity').val())
        };
        userService.saveUserDetails(userId, userDetails, done);
    }
    var done = function() {    
    $('#triggerOrderConfirm').trigger("click");
    };
    return{
        init :init
    }
}(UserService);