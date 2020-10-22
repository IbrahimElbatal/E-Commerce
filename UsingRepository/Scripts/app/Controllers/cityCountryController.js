var CityCountryController = function(cityCountryService) {
    var dataFromserver;
    var init = function(){
        cityCountryService.getCountryWithCities(done);
    };
    var done = function(data) {
        dataFromserver = data;
        $(data).each(function(index, country) {
            $("#ddlCountry").append('<option value="' + country.Id + '">' + country.Name + '</option>');
        });

    };

    var fillddlCityWithData = function() {
        $("#ddlCountry").change(ddlCityWithDataFromSelectedCountry);
    };
    var ddlCityWithDataFromSelectedCountry = function () {
        var selectedCountry = $(this).val();
        $(dataFromserver).each(function (index, country) {
            if (country.Id === parseInt(selectedCountry)) {
                $("#ddlCity").empty();
                $("#ddlCity").append('<option value="-1">Select City</option>');
                $(country.Cities).each(function (index, city) {
                    $("#ddlCity").append('<option value="' + city.Id + '">' + city.Name + '</option>');
                });
            }
        });
        if (parseInt(selectedCountry) === -1) {
            $("#ddlCity").empty();
            $("#ddlCity").append('<option value="-1">Select City</option>');
        }
    }
    return{
        init: init,
        fillddlCityWithData: fillddlCityWithData
    }
}(CityCountryService);
