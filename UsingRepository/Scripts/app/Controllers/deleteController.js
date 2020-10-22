
var DeleteController = function (deleteService) {
    var button;
    var url;

    var init = function (apiUrl) {
        url = apiUrl;
        $('.js-Delete').click(deleteFunc);
    };

    var deleteFunc = function (e) {
        button = $(e.target);
        bootbox.dialog({
            title: 'Confirm',
            message: "Are you want to to delete this Item ?",
            buttons: {
                Yes: {
                    label: "Yes",
                    className: 'btn-danger',
                    callback: function () {
                        var id = button.attr('data-id');
                        deleteService.deleteFun(url + id, done, fail);
                        if (window.location.pathname.toLowerCase().includes('roles') ||
                            window.location.pathname.toLowerCase().includes('users')) {
                            button.parents('.panel').remove();
                            toastr.info('Item deleted successfully');
                        }
                    }
                },
                No: {
                    label: "No",
                    className: 'btn-default'
                }
            }
        });
    }

    var done = function() {
        button.parents('tr').fadeOut(function() {
            $(this).remove();
        });
    }

    var fail = function(error) {
        if (error.status === 404) {
            toastr.error('This Is Deleted By Another One');
            $('#categoryTable').load(' #categoryTable');
        }else if (error.status === 401) {
            toastr.error('This Is Deleted Byhxxx Another One');
            $('#categoryTable').load(' #categoryTable');
        }else
            toastr.error('Deleted Fail ,some thing Error');
    }

    return {
        init: init
    }
}(DeleteService);
