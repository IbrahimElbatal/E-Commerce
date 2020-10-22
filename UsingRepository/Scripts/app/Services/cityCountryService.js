var CityCountryService = function() {
    var getCountryWithCities = function(done) {
        $.ajax({
            url: '/api/Country',
            method: 'Get'
        }).done(done);
    };
    return{
        getCountryWithCities: getCountryWithCities
    }
}();