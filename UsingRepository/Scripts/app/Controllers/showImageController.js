

var ShowImageController = function () {
    var init = function(selector) {
        $('#' + selector).change(showImage);
    };
    var showImage = function() {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function(e) {
                $('#imgReader').attr('src', e.target.result);

                if ($('#imgReader').attr('src') !== '') {
                    $('#imgReader').css("display", "block");
                }
            }
            reader.readAsDataURL(this.files[0]);
        }

    };
    return{
        init: init
    }
}();